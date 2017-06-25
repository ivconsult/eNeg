CREATE TABLE [dbo].[Conversation]
(
--This table will used to save all conversations that will be happened during application.  This table consists of the following fields;
 
	ConversationID uniqueidentifier primary key,
	ConversationName nvarchar(100) not null,
	NegotiationID uniqueidentifier references Negotiation(NegotiationID)  not null,
	Deleted bit,
	DeletedBy  uniqueidentifier   references [User](UserID),
	DeletedOn dateTime


)
