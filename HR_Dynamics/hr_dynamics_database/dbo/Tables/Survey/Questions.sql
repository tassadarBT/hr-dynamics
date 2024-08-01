CREATE TABLE [dbo].[Questions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[SurveyId] INT NOT NULL,
	[ParentId] INT NULL,
	[OptionGroupId] INT NULL,
	[DisplayOrder] INT NOT NULL, 
	[DisplayOrderText] NVARCHAR(128) NOT NULL, 
	[Type] nvarchar(128) NOT NULL, 
	[Text] nvarchar(1024) NOT NULL,
	[IsSection] BIT NOT NULL,
	[Required] BIT NOT NULL,
	[Active] BIT NOT NULL,
	CONSTRAINT FK_Question_Survey FOREIGN KEY(SurveyId) REFERENCES Surveys(Id),
	CONSTRAINT FK_Question_Question FOREIGN KEY(ParentId) REFERENCES Questions(Id),
	CONSTRAINT FK_Question_OptionGroup FOREIGN KEY(OptionGroupId) REFERENCES QuestionOptionGroups(Id)
)
