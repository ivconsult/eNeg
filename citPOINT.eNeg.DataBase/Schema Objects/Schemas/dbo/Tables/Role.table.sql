CREATE TABLE [dbo].[Role]
(
	--This table represent all available roles that can be assigned to any users.
	RoleID uniqueidentifier primary key,
	RoleName nvarchar(100) not null,
	RoleDescription nvarchar(300) not null

)
