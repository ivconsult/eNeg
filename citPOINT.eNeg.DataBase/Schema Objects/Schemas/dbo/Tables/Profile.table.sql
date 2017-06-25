CREATE TABLE [dbo].[Profile]
(
--This table will be used to save all data about the user selected profile

 ProfileID uniqueidentifier primary key,
 CurrentTheme nvarchar(100) not null,
 UserID uniqueidentifier references [User](UserID)
)
