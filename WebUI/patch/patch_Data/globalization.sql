﻿USE [SlickTicket]

ALTER TABLE dbo.allowed_email_domains
  ALTER COLUMN domain NVARCHAR(100) NOT NULL;
  
ALTER TABLE dbo.attachments
  ALTER COLUMN attachment_name NVARCHAR(100) NOT NULL;
ALTER TABLE dbo.attachments
  ALTER COLUMN attachment_size NVARCHAR(50) NOT NULL;

ALTER TABLE dbo.comments
  ALTER COLUMN comment NVARCHAR(MAX) NOT NULL;

ALTER TABLE dbo.faq
  ALTER COLUMN title NVARCHAR(MAX) NOT NULL;
ALTER TABLE dbo.faq
  ALTER COLUMN body NVARCHAR(MAX) NOT NULL;
  
ALTER TABLE dbo.priority
  ALTER COLUMN priority_name NVARCHAR(50) NOT NULL;
  
ALTER TABLE dbo.security_levels
  ALTER COLUMN security_level_name NVARCHAR(50) NOT NULL;
  
ALTER TABLE dbo.statuses
  ALTER COLUMN status_name NVARCHAR(50) NOT NULL;
  
ALTER TABLE dbo.styles
  ALTER COLUMN style_name NVARCHAR(50) NOT NULL;

GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[styles]') AND name = N'UQ__styles__0EA330E9')
ALTER TABLE [dbo].[styles] DROP CONSTRAINT [UQ__styles__0EA330E9]
GO

ALTER TABLE dbo.styles
  ALTER COLUMN style_name NVARCHAR(50) NOT NULL;  
ALTER TABLE dbo.styles
ADD UNIQUE (style_name);
ALTER TABLE dbo.styles
  ALTER COLUMN text_color NVARCHAR(10) NOT NULL;  
ALTER TABLE dbo.styles
  ALTER COLUMN borders NVARCHAR(10) NOT NULL;  
ALTER TABLE dbo.styles
  ALTER COLUMN body NVARCHAR(10) NOT NULL;  
ALTER TABLE dbo.styles
  ALTER COLUMN links NVARCHAR(10) NOT NULL;  
ALTER TABLE dbo.styles
  ALTER COLUMN hover NVARCHAR(10) NOT NULL;  
ALTER TABLE dbo.styles
  ALTER COLUMN button_text NVARCHAR(10) NOT NULL;  
ALTER TABLE dbo.styles
  ALTER COLUMN header NVARCHAR(10) NOT NULL;  
ALTER TABLE dbo.styles
  ALTER COLUMN alt_rows NVARCHAR(10) NOT NULL;  
ALTER TABLE dbo.styles
  ALTER COLUMN text_color NVARCHAR(10) NOT NULL;  
ALTER TABLE dbo.styles
  ALTER COLUMN background NVARCHAR(10) NOT NULL;  
  
ALTER TABLE dbo.sub_units
  ALTER COLUMN sub_unit_name NVARCHAR(MAX) NOT NULL;
ALTER TABLE dbo.sub_units
  ALTER COLUMN mailto NVARCHAR(100) NOT NULL;
  
ALTER TABLE dbo.tickets
  ALTER COLUMN title NVARCHAR(100) NOT NULL;
ALTER TABLE dbo.tickets
  ALTER COLUMN details NVARCHAR(MAX) NOT NULL;
  
ALTER TABLE dbo.units
  ALTER COLUMN unit_name NVARCHAR(MAX) NOT NULL;
  
ALTER TABLE dbo.user_groups
  ALTER COLUMN ad_group NVARCHAR(100) NOT NULL;
  
ALTER TABLE dbo.users
  ALTER COLUMN userName NVARCHAR(MAX) NOT NULL;
ALTER TABLE dbo.users
  ALTER COLUMN phone NVARCHAR(20) NOT NULL;
ALTER TABLE dbo.users
  ALTER COLUMN email NVARCHAR(MAX) NOT NULL;