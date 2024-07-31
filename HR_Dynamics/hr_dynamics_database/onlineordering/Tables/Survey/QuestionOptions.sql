CREATE TABLE [dbo].[QuestionOptions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[OptionGroupId] INT NOT NULL,
	[DisplayOrder] INT NOT NULL, 
	[Color] NVARCHAR(128) NULL,
	[Value] NVARCHAR(128) NOT NULL,
	[Text] NVARCHAR(256) NOT NULL,
	[Active] BIT NOT NULL,
	CONSTRAINT FK_Option_OptionGroup FOREIGN KEY(OptionGroupId) REFERENCES QuestionOptionGroups(Id)
)
