CREATE DATABASE CaseManagementDB;
GO

USE CaseManagementDB;
GO

CREATE TABLE CustomerCases (
    CaseID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100) NOT NULL,
    Query NVARCHAR(MAX) NOT NULL,
    Channel NVARCHAR(50) CHECK (Channel IN ('WhatsApp', 'Email', 'Call', 'AI', 'Visit')) NOT NULL,
    Status NVARCHAR(50) CHECK (Status IN ('Open', 'In Progress', 'Closed')) NOT NULL DEFAULT 'Open',
    CreatedAt DATETIME DEFAULT GETDATE()
);