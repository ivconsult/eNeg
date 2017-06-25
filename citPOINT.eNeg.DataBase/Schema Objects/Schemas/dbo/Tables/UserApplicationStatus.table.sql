CREATE TABLE [dbo].[UserApplicationStatus]
(
	UserAppStatusID uniqueidentifier Primary key, 
	ApplicationID uniqueidentifier references [Application](ApplicationID) not null,
	UserID uniqueidentifier references [User](UserID) not null,
	IsDMActive bit
)
