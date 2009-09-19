USE [SlickTicket]
GO

/****** Object:  Table [dbo].[security_levels]    Script Date: 10/21/2008 15:44:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[security_levels](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[security_level_name] [varchar](50) NULL,
	[security_level_description] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF


INSERT INTO [dbo].[security_levels] VALUES ('Minimum', '')
GO
INSERT INTO [dbo].[security_levels] VALUES ('', '')
GO
INSERT INTO [dbo].[security_levels] VALUES ('', '')
GO
INSERT INTO [dbo].[security_levels] VALUES ('', '')
GO
INSERT INTO [dbo].[security_levels] VALUES ('', '')
GO
INSERT INTO [dbo].[security_levels] VALUES ('Medium', '')
GO
INSERT INTO [dbo].[security_levels] VALUES ('', '')
GO
INSERT INTO [dbo].[security_levels] VALUES ('', '')
GO
INSERT INTO [dbo].[security_levels] VALUES ('', '')
GO
INSERT INTO [dbo].[security_levels] VALUES ('Maximum', '')
GO

/****** Object:  Table [dbo].[units]    Script Date: 10/21/2008 15:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[units](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[unit_name] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

INSERT INTO [dbo].[units] VALUES ('test_unit')
GO

/****** Object:  Table [dbo].[sub_units]    Script Date: 10/21/2008 15:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sub_units](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[unit_ref] [int] NOT NULL,
	[sub_unit_name] [varchar](max) NOT NULL,
	[access_level] [int] NOT NULL,
	[mailto] [varchar](100) NOT NULL,
 CONSTRAINT [PK__sub_units__00551192] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[sub_units]  WITH CHECK ADD  CONSTRAINT [FK__sub_units__acces__023D5A04] FOREIGN KEY([access_level])
REFERENCES [dbo].[security_levels] ([id])
GO
ALTER TABLE [dbo].[sub_units] CHECK CONSTRAINT [FK__sub_units__acces__023D5A04]
GO
ALTER TABLE [dbo].[sub_units]  WITH CHECK ADD  CONSTRAINT [FK__sub_units__unit___014935CB] FOREIGN KEY([unit_ref])
REFERENCES [dbo].[units] ([id])
GO
ALTER TABLE [dbo].[sub_units] CHECK CONSTRAINT [FK__sub_units__unit___014935CB]

INSERT INTO [dbo].[sub_units] VALUES (1, 'test_sub_unit', 2, 'dummy@dummy.org')


/****** Object:  Table [dbo].[priority]    Script Date: 10/21/2008 15:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[priority](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[priority_name] [varchar](50) NOT NULL,
	[level] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

INSERT INTO [dbo].[priority] VALUES ('Low', 1)
GO
INSERT INTO [dbo].[priority] VALUES ('Medium', 2)
GO
INSERT INTO [dbo].[priority] VALUES ('High', 3)
GO
INSERT INTO [dbo].[priority] VALUES ('Urgent', 10)
GO

/****** Object:  Table [dbo].[users]    Script Date: 10/21/2008 15:55:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](max) NOT NULL,
	[phone] [varchar](20) NOT NULL,
	[email] [varchar](max) NOT NULL,
	[sub_unit] [int] NOT NULL,
	[is_admin] [bit] NOT NULL CONSTRAINT [DF__users__is_admin__07F6335A]  DEFAULT ((0)),
 CONSTRAINT [PK__users__060DEAE8] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK__users__sub_unit__07020F21] FOREIGN KEY([sub_unit])
REFERENCES [dbo].[sub_units] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK__users__sub_unit__07020F21]

/****** Object:  Table [dbo].[allowed_email_domains]    Script Date: 10/21/2008 15:55:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[allowed_email_domains](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[domain] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
/****** Object:  Table [dbo].[statuses]    Script Date: 10/21/2008 15:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[statuses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[status_order] [int] NOT NULL,
	[status_name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

INSERT INTO [dbo].[statuses] VALUES (1, 'New')
GO
INSERT INTO [dbo].[statuses] VALUES (2, 'Assigned')
GO
INSERT INTO [dbo].[statuses] VALUES (3, 'In Progress')
GO
INSERT INTO [dbo].[statuses] VALUES (4, 'Resolved')
GO
INSERT INTO [dbo].[statuses] VALUES (10, 'Closed')
GO

/****** Object:  Table [dbo].[tickets]    Script Date: 10/21/2008 16:00:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tickets](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](max) NOT NULL,
	[details] [text] NOT NULL,
	[submitter] [int] NOT NULL,
	[submitted] [datetime] NOT NULL,
	[last_action] [datetime] NOT NULL,
	[closed] [datetime] NOT NULL DEFAULT ('1/1/2001'),
	[assigned_to_group] [int] NOT NULL,
	[assigned_to_group_last] [int] NULL,
	[ticket_status] [int] NOT NULL,
	[priority] [int] NOT NULL,
	[originating_group] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD FOREIGN KEY([assigned_to_group])
REFERENCES [dbo].[sub_units] ([id])
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD FOREIGN KEY([assigned_to_group_last])
REFERENCES [dbo].[sub_units] ([id])
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD FOREIGN KEY([originating_group])
REFERENCES [dbo].[sub_units] ([id])
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD FOREIGN KEY([priority])
REFERENCES [dbo].[priority] ([id])
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD FOREIGN KEY([submitter])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD FOREIGN KEY([ticket_status])
REFERENCES [dbo].[statuses] ([id])

/****** Object:  Table [dbo].[comments]    Script Date: 10/21/2008 16:01:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[comment] [text] NOT NULL,
	[ticket_ref] [int] NOT NULL,
	[submitter] [int] NOT NULL,
	[submitted] [datetime] NOT NULL,
	[assigned_to] [int] NOT NULL,
	[priority_id] [int] NOT NULL,
	[status_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[comments]  WITH CHECK ADD FOREIGN KEY([submitter])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[comments]  WITH CHECK ADD FOREIGN KEY([ticket_ref])
REFERENCES [dbo].[tickets] ([id])
GO
ALTER TABLE [dbo].[comments]  WITH CHECK ADD FOREIGN KEY([assigned_to])
REFERENCES [dbo].[sub_units] ([id])
GO
ALTER TABLE [dbo].[comments]  WITH CHECK ADD FOREIGN KEY([priority_id])
REFERENCES [dbo].[priority] ([id])
GO
ALTER TABLE [dbo].[comments]  WITH CHECK ADD FOREIGN KEY([status_id])
REFERENCES [dbo].[statuses] ([id])

/****** Object:  Table [dbo].[attachments]    Script Date: 10/21/2008 16:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[attachments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ticket_ref] [int] NOT NULL,
	[comment_ref] [int] NULL,
	[attachment_name] [varchar](100) NOT NULL,
	[attachment_size] [varchar](50) NOT NULL,
	[submitted] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[attachments]  WITH CHECK ADD FOREIGN KEY([comment_ref])
REFERENCES [dbo].[comments] ([id])
GO
ALTER TABLE [dbo].[attachments]  WITH CHECK ADD FOREIGN KEY([ticket_ref])
REFERENCES [dbo].[tickets] ([id])

/****** Object:  Table [dbo].[user_groups]    Script Date: 10/21/2008 16:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_groups](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ad_group] [varchar](100) NOT NULL,
	[security_level] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[user_groups]  WITH CHECK ADD FOREIGN KEY([security_level])
REFERENCES [dbo].[security_levels] ([id])

/****** Object:  Table [dbo].[styles]    Script Date: 10/21/2008 16:02:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[styles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[style_name] [varchar](50) NOT NULL,
	[text_color] [varchar](10) NOT NULL,
	[borders] [varchar](10) NOT NULL,
	[body] [varchar](10) NOT NULL,
	[links] [varchar](10) NOT NULL,
	[hover] [varchar](10) NOT NULL,
	[button_text] [varchar](10) NOT NULL,
	[header] [varchar](10) NOT NULL,
	[alt_rows] [varchar](10) NOT NULL,
	[background] [varchar](10) NOT NULL,
 CONSTRAINT [PK__styles__0DAF0CB0] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ__styles__0EA330E9] UNIQUE NONCLUSTERED 
(
	[style_name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

/****** Object:  Table [dbo].[faq]    Script Date: 10/21/2008 16:03:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[faq](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](max) NOT NULL,
	[body] [text] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF