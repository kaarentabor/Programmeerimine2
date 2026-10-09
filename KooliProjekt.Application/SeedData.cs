using System;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application;

public static class SeedData
{
    // Väljamõeldud näidisandmed. example.com aadressid ei ole klientide päris kontaktid.
    public static async Task InitializeAsync(ApplicationDbContext db)
    {
        string[] firstNames = { "Kaisa", "Martin", "Laura", "Rasmus", "Liis", "Karl", "Anna" };
        string[] lastNames = { "Tamm", "Saar", "Sepp", "Kask", "Mägi" };
        string[] places = { "Tallinna konverentsikeskus", "Tartu koolituskeskus", "Pärnu seminariruum", "Viljandi kultuurimaja", "Rakvere ettevõtluskeskus" };
        string[] topics = { "Projektijuhtimise alused", "Digiturunduse töötuba", "Meeskonnatöö koolitus", "Exceli praktiline kursus", "Klienditeeninduse seminar", "Ettevõtluse alustamise koolitus", "Avaliku esinemise töötuba" };
        string[] roles = { "Projektijuht", "Koolituskoordinaator", "Kliendihaldur", "Müügikonsultant", "Ürituste korraldaja" };

        // Kõik 35 komplekti salvestatakse koos; vea korral ei jää poolikuid algandmeid.
        using var transaction = await db.Database.BeginTransactionAsync();
        for (int i = 0; i < 35; i++)
        {
            var number = i + 1;
            var name = firstNames[i % 7] + " " + lastNames[i / 7];
            var email = $"klient{number:00}@example.com";
            var agentEmail = $"korraldaja{number:00}@example.com";
            var invoiceNumber = $"KOOL-2026-{number:000}";
            var start = new DateTime(2026, 10, 12, 9, 0, 0).AddDays(i * 2);
            var amount = 120m + (i % 7) * 25m;

            if (!await db.Agents.AnyAsync(x => x.Email == agentEmail))
                db.Agents.Add(new Agent { Name = firstNames[(i + 3) % 7] + " " + lastNames[i / 7], Email = agentEmail, Role = roles[i % 5] });

            var client = await db.Clients.FirstOrDefaultAsync(x => x.Email == email);
            if (client == null)
            {
                client = new Client { Name = name, Email = email };
                db.Clients.Add(client);
            }

            var eventEntity = await db.Events.FirstOrDefaultAsync(x => x.InvoiceNumber == invoiceNumber);
            if (eventEntity == null)
            {
                eventEntity = new Event
                {
                    Client = client, EventTypeId = i % 7 + 1, InvoiceNumber = invoiceNumber,
                    Summary = amount, StartDate = start, EndDate = start.AddHours(6),
                    EventPlace = places[i % 5], Status = "Planeeritud",
                    Title = topics[i % 7] + " – " + places[i % 5]
                };
                db.Events.Add(eventEntity);
            }
            await db.SaveChangesAsync();

            var orderName = $"Koolituse tellimus {invoiceNumber}";
            var order = await db.Transactions.FirstOrDefaultAsync(x => x.ClientId == client.ClientId && x.Name == orderName);
            if (order == null)
            {
                order = new Transaction
                {
                    Client = client, Name = orderName, StartTime = start.AddDays(-14),
                    EndTime = start.AddHours(6), TotalPrice = amount, PaidAmount = amount,
                    PaymentMethod = "Pangaülekanne", Summary = $"Ühe osaleja koolitus: {topics[i % 7]}"
                };
                db.Transactions.Add(order);
                await db.SaveChangesAsync();
            }

            var registration = await db.Registrations.FirstOrDefaultAsync(x => x.EventId == eventEntity.EventId && x.TransactionId == order.TransactionId);
            if (registration == null)
            {
                registration = new Registration { Event = eventEntity, Transaction = order, RegistrationTime = start.AddDays(-14), Status = "Kinnitatud" };
                db.Registrations.Add(registration);
                await db.SaveChangesAsync();
            }

            if (!await db.Payments.AnyAsync(x => x.RegistrationId == registration.RegistrationId && x.EventId == eventEntity.EventId))
                db.Payments.Add(new Payment
                {
                    Registration = registration, Event = eventEntity, InvoiceId = number,
                    Amount = amount, PaymentDate = start.AddDays(-10), Status = "Tasutud", PaymentMethod = "Pangaülekanne"
                });

            var filename = $"koolitus-{number:00}-paevakava.pdf";
            if (!await db.FileAttachments.AnyAsync(x => x.EventId == eventEntity.EventId && x.Filename == filename))
                db.FileAttachments.Add(new FileAttachment { Event = eventEntity, Filename = filename, FileType = "application/pdf", UploadTime = start.AddDays(-21) });

            await db.SaveChangesAsync();
        }
        await transaction.CommitAsync();
    }
}
