CREATE TABLE [DBO].[Application]
(
  [ApplicationID]		       uniqueidentifier PRIMARY KEY,
  [ApplicationRank]            int           NOT NULL   ,
  [ApplicationTitle]           nvarchar(100) NOT NULL   ,
  [ApplicationBaseAddress]     nvarchar(200)     NULL   ,
  [ApplicationMainServicePath] nvarchar(255)     NULL   ,
  [AssemblyName]               nvarchar(150)     NULL   ,
  [ModuleName]                 nvarchar(100)     NULL   ,
  [XapFilePath]                nvarchar(255)     NULL
)


	
	