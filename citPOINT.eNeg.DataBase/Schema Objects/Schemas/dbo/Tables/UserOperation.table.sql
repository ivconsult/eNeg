
CREATE TABLE [dbo].[UserOperation](
	[OperationID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[OperationString] [nvarchar](200) NULL,
	[Type] [tinyint] NULL,
	[NewEmailAddress] [nvarchar](300) NULL,
	[RequestUserID] uniqueidentifier Null,
	[OrganizationID] uniqueidentifier Null,
PRIMARY KEY CLUSTERED 
(
	[OperationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserOperation]  WITH CHECK ADD  CONSTRAINT [FK_UserOperation_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[UserOperation] CHECK CONSTRAINT [FK_UserOperation_User]
GO


