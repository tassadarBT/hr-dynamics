CREATE TABLE [dbo].[ResponseQuestionAnswers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),	
	[ResponseId] INT NOT NULL,
	[QuestionId] INT NOT  NULL,
	[Value] NVARCHAR(1024) NULL,
	[Text] NVARCHAR(1024) NULL,
	CONSTRAINT FK_Answer_Response FOREIGN KEY(ResponseId) REFERENCES Responses(Id),
	CONSTRAINT FK_Answer_Question FOREIGN KEY(QuestionId) REFERENCES Questions(Id)
)
