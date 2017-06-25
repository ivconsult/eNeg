CREATE PROCEDURE [dbo].[uspUserSearchCount]
	@key nvarchar(300),
	@UserID uniqueidentifier,
	@IsForPublicProfile bit
AS
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN
		select COUNT(*) 
		from dbo.[User] as u
		where (u.FirstName like '%'+ @key +'%' or
			  u.LastName like '%'+ @key +'%' or
			  u.CompanyName like '%'+ @key +'%' or
			  u.EmailAddress like '%'+ @key +'%'or
			  (@IsForPublicProfile=1 AND u.ProfileDescription like '%'+ @key +'%')) and
			  u.Disabled = 0  and 
			  u.UserID != @UserID and 
			  (@IsForPublicProfile=0 OR u.HasPublicProfile = 1)
	COMMIT

