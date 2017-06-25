CREATE TABLE [dbo].[Negotiation]
(
	--This table represents All Negotiations that will be saved during the application.  This table consists of the following fields;

 NegotiationID uniqueidentifier primary key,
 NegotiationName nvarchar(150) not null,
 CreatedDate dateTime not null,
 StatusID uniqueidentifier references NegotiationStatus(StatusID), 
 Deleted  bit,
 DeletedDate dateTime,
 UserID  uniqueidentifier not null

)
