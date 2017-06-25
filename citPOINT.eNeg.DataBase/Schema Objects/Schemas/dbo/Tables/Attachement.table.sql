CREATE TABLE [dbo].[Attachement]
(
	--This table represents All attachement content in specific message.  This table consists of the following fields;

 AttachementID uniqueidentifier primary key,
 AttachementName nvarchar(100),
 AttachementContent varBinary(max),
 MessageID uniqueidentifier references [Message](MessageID)

)
