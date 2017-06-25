﻿CREATE PROCEDURE [dbo].[uspUserSearch]
	@key nvarchar(300),
	@UserID uniqueidentifier
AS
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN
		select * 
		from dbo.[User] as u
		where u.FirstName like '%'+ @key +'%' or
			  u.LastName like '%'+ @key +'%' or
			  u.CompanyName like '%'+ @key +'%' or
			  u.EmailAddress like '%'+ @key +'%' and
			  u.Disabled = 0  and u.UserID != @UserID
	COMMIT