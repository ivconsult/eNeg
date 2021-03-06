﻿CREATE TABLE [dbo].[Culture](
	[CultureID] [int] NOT NULL,
	[CultureName] [Nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Culture] PRIMARY KEY CLUSTERED 
(
	[CultureID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO