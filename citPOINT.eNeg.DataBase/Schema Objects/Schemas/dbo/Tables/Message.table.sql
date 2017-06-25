CREATE TABLE [dbo].[Message]
(
	--This table represents All messages that will be send and received during the application.  This table consists of the following fields;

	 MessageID uniqueidentifier primary key,
	 ConversationID uniqueidentifier references Conversation(ConversationID),
	 MessageContent nText not null,
	 MessageSubject nvarchar(200),
	 MessageSender nvarchar(300) not null,
	 MessageReceiver nvarchar(300) not null,
	 MessageDate  DateTime,
	 ChannelID  uniqueidentifier references Channel(ChannelID) not null,
	 IsSent bit not null,
	 IsAppsMessage bit not null,
	 Deleted bit,
	 DeletedBy uniqueidentifier references [User](UserID),
	 DeletedOn dateTime


)
