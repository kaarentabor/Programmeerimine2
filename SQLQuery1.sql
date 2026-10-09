SELECT 'Agents' AS Tabel, COUNT(*) AS Ridu FROM dbo.Agents
UNION ALL SELECT 'Clients', COUNT(*) FROM dbo.Clients
UNION ALL SELECT 'Events', COUNT(*) FROM dbo.Events
UNION ALL SELECT 'Transactions', COUNT(*) FROM dbo.Transactions
UNION ALL SELECT 'Registrations', COUNT(*) FROM dbo.Registrations
UNION ALL SELECT 'Payments', COUNT(*) FROM dbo.Payments
UNION ALL SELECT 'FileAttachments', COUNT(*) FROM dbo.FileAttachments;