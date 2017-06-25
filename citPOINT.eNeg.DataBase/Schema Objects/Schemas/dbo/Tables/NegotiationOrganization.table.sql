
CREATE TABLE [dbo].[NegotiationOrganization](
	[NegotiationOrganizationID] [uniqueidentifier] NOT NULL,
	[NegotiationID] [uniqueidentifier] NOT NULL,
	[OrganizationID] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_NegotiationOrganization] PRIMARY KEY CLUSTERED 
(
	[NegotiationOrganizationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[NegotiationOrganization]  WITH CHECK ADD  CONSTRAINT [FK_NegotiationOrganization_Negotiation] FOREIGN KEY([NegotiationID])
REFERENCES [dbo].[Negotiation] ([NegotiationID])
GO

ALTER TABLE [dbo].[NegotiationOrganization] CHECK CONSTRAINT [FK_NegotiationOrganization_Negotiation]
GO

ALTER TABLE [dbo].[NegotiationOrganization]  WITH CHECK ADD  CONSTRAINT [FK_NegotiationOrganization_Organization] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO

ALTER TABLE [dbo].[NegotiationOrganization] CHECK CONSTRAINT [FK_NegotiationOrganization_Organization]
GO


