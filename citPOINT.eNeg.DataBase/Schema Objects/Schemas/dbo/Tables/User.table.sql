CREATE TABLE [dbo].[User](
	[UserID] [uniqueidentifier] NOT NULL,
	[EmailAddress] [nvarchar](300) NOT NULL,
	[PasswordHash] [nvarchar](150) NOT NULL,
	[PasswordSalt] [nvarchar](150) NOT NULL,
	[Locked] [bit] NOT NULL,
	[LockedDate] [datetime] NULL,
	[LastLoginDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[AccountTypeID] [uniqueidentifier] NULL,
	[IPAddress] [nvarchar](50) NOT NULL,
	[SecurityQuestionID] [uniqueidentifier] NULL,
	[AnswerHash] [nvarchar](150) NULL,
	[AnswerSalt] [nvarchar](150) NULL,
	[Online] [bit] NOT NULL,
	[Disabled] [bit] NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[CompanyName] [nvarchar](100) NULL,
	[LCID] [int] NULL,
	[Address] [nvarchar](300) NULL,
	[City] [nvarchar](100) NULL,
	[CountryID] [uniqueidentifier] NULL,
	[Phone] [nvarchar](100) NULL,
	[Mobile] [nvarchar](100) NULL,
	[Gender] [bit] NULL,
	[Reset] [bit] NULL,
	[CultureID] [int] NULL,
	[HasPublicProfile] bit default 0,
	[ProfileDescription] Nvarchar(300),
	[PostalCode] Nvarchar(150)
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[User]  WITH NOCHECK ADD FOREIGN KEY([AccountTypeID])
REFERENCES [dbo].[AccountType] ([AccountTypeID])
GO

ALTER TABLE [dbo].[User]  WITH NOCHECK ADD FOREIGN KEY([CountryID])
REFERENCES [dbo].[Country] ([CountryID])
GO

ALTER TABLE [dbo].[User]  WITH NOCHECK ADD FOREIGN KEY([LCID])
REFERENCES [dbo].[PreferedLanguage] ([LCID])
GO

ALTER TABLE [dbo].[User]  WITH NOCHECK ADD FOREIGN KEY([SecurityQuestionID])
REFERENCES [dbo].[SecurityQuestion] ([SecurityQuestionID])
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Culture] FOREIGN KEY([CultureID])
REFERENCES [dbo].[Culture] ([CultureID])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Culture]
GO


