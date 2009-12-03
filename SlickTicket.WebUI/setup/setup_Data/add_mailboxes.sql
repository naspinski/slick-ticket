﻿USE [SlickTicket]

CREATE TABLE [dbo].[Mailboxes](
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    SubUnitID INT NOT NULL REFERENCES sub_units(id),
	Host NVARCHAR(255) NOT NULL,
	EmailAddress NVARCHAR(255) NOT NULL,
	UserName NVARCHAR(255) NOT NULL,
	Password NVARCHAR(255) NOT NULL,
	Port INT NOT NULL,
);