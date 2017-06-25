CREATE TABLE [dbo].[History]
(
	--This table represents All history data for the negotiation table, it acts like log table .  This table consists of the following fields;

	 HistoryID uniqueidentifier primary key,
	 OldValue  XML,--if user did any modification I will log all of the the selected row old data as xml and insert this xml row in this field.
	 NewValue  XML,-- if user did any modification I will log all of the the selected row newdata as xml and insert this xml row in this field.
	 TableName nvarchar(50),
	 ActionTypeID uniqueidentifier references ActionType(ActionTypeID) not null,
	 ActionDate dateTime not null,
	 UserID uniqueidentifier   not null references [User](UserID)
)
