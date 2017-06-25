CREATE TABLE [dbo].[AccountType]
(
	/* This table represent all account types for eNeg users, e.g. Free or Premium */
	AccountTypeID uniqueidentifier primary key,
	TypeName nvarchar(100) not null ,
	TypeDescription nvarchar(200) 
)
