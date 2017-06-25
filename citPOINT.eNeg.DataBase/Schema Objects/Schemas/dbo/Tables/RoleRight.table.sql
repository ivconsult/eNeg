CREATE TABLE [dbo].[RoleRight]
(
	--This  table will contain the rights that's associated to a role,  it will contain the following fields:

	 RoleRightID uniqueidentifier primary key,
	 RightID uniqueidentifier references [Right](RightID),
	 RoleID uniqueidentifier references [Role](RoleID)

)
