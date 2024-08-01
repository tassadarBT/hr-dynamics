CREATE TABLE [dbo].[Campaigns]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[SurveyId] INT NOT NULL,
	[RecipientEmails] NVARCHAR(MAX), 
	[Name] NVARCHAR(256) NOT NULL,
	[Description] NVARCHAR(1024) NULL,
	[StartTime] DATETIME NULL,
	[EndTime] DATETIME NULL,
	[Active] BIT NOT NULL,
	CONSTRAINT FK_Campaign_Survey FOREIGN KEY(SurveyId) REFERENCES Surveys(Id)
)
