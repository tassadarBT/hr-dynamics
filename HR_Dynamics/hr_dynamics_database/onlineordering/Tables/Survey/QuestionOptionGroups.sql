﻿CREATE TABLE [dbo].[QuestionOptionGroups]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[Name] NVARCHAR(256) NOT NULL,
	[Active] BIT NOT NULL
)
