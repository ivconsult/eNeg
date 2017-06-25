CREATE TABLE [dbo].[Right]
(
-- the user's rights on system
	RightID uniqueidentifier primary key,
	RightName nvarchar(100),
	RightDescription nvarchar(300)

)
