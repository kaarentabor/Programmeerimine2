#nullable enable annotations
using System;

namespace KooliProjekt.Application.Features.Clients;

public class ClientDto
{
    public int ClientId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
}
