CREATE TABLE [dbo].[UserRole]
(

 UserRoleID uniqueidentifier primary key,
 UserID uniqueidentifier references [User](UserID),
 RoleID uniqueidentifier references [Role](RoleID)

)
