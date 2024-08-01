CREATE TABLE [dbo].[Responses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[SurveyId] INT NOT NULL,
	[CampaignId] INT NULL,
	[UserId] NVARCHAR(450)  NULL,		
	[StartTime] DATETIME NOT NULL,
	[EndTime] DATETIME NOT NULL,	
	CONSTRAINT FK_Response_Survey FOREIGN KEY([SurveyId]) REFERENCES Surveys(Id),
	CONSTRAINT FK_Response_Campaign FOREIGN KEY(CampaignId) REFERENCES Campaigns(Id),
	CONSTRAINT FK_Response_User FOREIGN KEY(UserId) REFERENCES onlineordering.AspNetUsers(Id)
)
