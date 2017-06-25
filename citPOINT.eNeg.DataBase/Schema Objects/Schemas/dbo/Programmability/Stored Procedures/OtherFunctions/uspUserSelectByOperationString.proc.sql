
Create PROCEDURE [dbo].[uspUserSelectByOperationString]
 @OperationString nvarchar(200),
 @OperationType Tinyint
AS 
	SELECT     dbo.[User].UserID,
	           dbo.[User].EmailAddress,
			   dbo.[User].PasswordHash,
			   dbo.[User].PasswordSalt,
			   dbo.[User].Locked,
			   dbo.[User].LockedDate, 
               dbo.[User].LastLoginDate,
			   dbo.[User].CreateDate,
			   dbo.[User].AccountTypeID,
			   dbo.[User].IPAddress,
			   dbo.[User].SecurityQuestionID,
			   dbo.[User].AnswerHash,
			   dbo.[User].AnswerSalt,
			   dbo.[User].Online, 
			   dbo.[User].Disabled, 
			   dbo.[User].FirstName, 
			   dbo.[User].LastName, 
			   dbo.[User].CompanyName, 
			   dbo.[User].LCID, 
               dbo.[User].Address, 
			   dbo.[User].City, 
			   dbo.[User].CountryID, 
			   dbo.[User].Phone, 
			   dbo.[User].Mobile, 
			   dbo.[User].Gender, 
			   dbo.[User].Reset,
			   dbo.[User].[CultureID],
			   dbo.[User].[HasPublicProfile],
			   dbo.[User].[ProfileDescription],
			   dbo.[User].[PostalCode]
FROM         dbo.[User] INNER JOIN
                      dbo.UserOperation ON dbo.[User].UserID = dbo.UserOperation.UserID
WHERE     (dbo.UserOperation.OperationString =@OperationString) AND (dbo.UserOperation.Type =@OperationType)


  