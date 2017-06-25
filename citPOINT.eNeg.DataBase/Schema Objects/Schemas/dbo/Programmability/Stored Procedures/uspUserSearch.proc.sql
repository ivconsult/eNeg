CREATE PROCEDURE [dbo].[uspUserSearch]
	@key nvarchar(300),
	@UserID uniqueidentifier,
	@IsForPublicProfile bit
AS
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN
		select 1 as rank,*
		from dbo.[User] as u
		where (u.FirstName like @key +'%' or
			  u.LastName like @key +'%' or
			  u.CompanyName like @key +'%' or
			  u.EmailAddress like @key +'%' or 
			  (@IsForPublicProfile=1 AND u.ProfileDescription like @key +'%')) and
			  u.Disabled = 0 and u.UserID != @UserID and 
			  (@IsForPublicProfile=0 OR u.HasPublicProfile = 1)
	union
		select 2 as rank,*
		from dbo.[User] as u
		where (u.FirstName like '%'+ @key +'%' or
			  u.LastName like '%'+ @key +'%' or
			  u.CompanyName like '%'+ @key +'%' or
			  u.EmailAddress like '%'+ @key +'%' or
			  (@IsForPublicProfile=1 AND u.ProfileDescription like '%'+ @key +'%')) and
			  u.Disabled = 0  and u.UserID != @UserID and 
			  (@IsForPublicProfile=0 OR u.HasPublicProfile = 1)
			  except 
			  (
		        select 2 as rank,*
			    from dbo.[User] as u
				where (u.FirstName like @key +'%' or
					  u.LastName like @key +'%' or
					  u.CompanyName like @key +'%' or
					  u.EmailAddress like @key +'%' or
					  (@IsForPublicProfile=1 AND u.ProfileDescription like @key +'%')) and
					  u.Disabled = 0  and u.UserID != @UserID and 
					  (@IsForPublicProfile=0 OR u.HasPublicProfile = 1)
				union
				select 2 as rank,* 
				from dbo.[User] as u
				where (u.FirstName like '%'+ @key or
					  u.LastName like '%'+ @key or
					  u.CompanyName like '%'+ @key or
					  u.EmailAddress like '%'+ @key or
					  (@IsForPublicProfile=1 AND u.ProfileDescription like '%'+ @key)) and
					  u.Disabled = 0  and u.UserID != @UserID and 
					  (@IsForPublicProfile=0 OR u.HasPublicProfile = 1)
				)	  	
	union
		select 3 as rank,*
		from dbo.[User] as u
		where (u.FirstName like '%'+ @key or
			  u.LastName like '%'+ @key or
			  u.CompanyName like '%'+ @key or
			  u.EmailAddress like '%'+ @key or
			  (@IsForPublicProfile=1 AND u.ProfileDescription like '%'+ @key)) and
			  u.Disabled = 0  and u.UserID != @UserID and 
			  (@IsForPublicProfile=0 OR u.HasPublicProfile = 1)  
			  except
			  (
				select 3 as rank,*
			    from dbo.[User] as u
				where (u.FirstName like @key +'%' or
					  u.LastName like @key +'%' or
					  u.CompanyName like @key +'%' or
					  u.EmailAddress like @key +'%' or
					  (@IsForPublicProfile=1 AND u.ProfileDescription like @key +'%')) and
					  u.Disabled = 0  and u.UserID != @UserID and 
					  (@IsForPublicProfile=0 OR u.HasPublicProfile = 1)	
			  )
			
	order by rank, EmailAddress
	COMMIT
	
	