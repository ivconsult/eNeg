CREATE TABLE [dbo].[Channel]
(
	--This table represents All channels that may be used during application like chat channel, mail channel ....  This table consists of the following fields;

	ChannelID uniqueidentifier primary key,
	ChannelName nvarchar(100) not null,
	ChannelTypeID uniqueidentifier references ChannelType(ChannelTypeID)
)
