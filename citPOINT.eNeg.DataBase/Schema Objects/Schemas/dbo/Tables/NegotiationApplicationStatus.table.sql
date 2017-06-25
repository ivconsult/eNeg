CREATE TABLE [dbo].[NegotiationApplicationStatus]
(
	NegotiationApplicationStatusID uniqueidentifier primary key,
	ApplicationID uniqueidentifier references [Application](ApplicationID) not null,
	NegotiationID uniqueidentifier references [Negotiation](NegotiationID) not null,
	UserID uniqueidentifier references [User](UserID) not null,
	Active bit not null
)
