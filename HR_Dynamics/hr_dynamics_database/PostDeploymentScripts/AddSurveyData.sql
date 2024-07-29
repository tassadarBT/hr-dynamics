IF NOT EXISTS ( select 1 from Surveys)
BEGIN
	set identity_insert dbo.Surveys ON;
	insert into dbo.Surveys([Id], [Name], [Description], [IsAnonymous],[Active]) values (1, 'Company Feedback Survey', 'Company Feedback Survey', 1, 1);
	set identity_insert dbo.Surveys OFF;
END

IF NOT EXISTS ( select 1 from Campaigns)
BEGIN
	set identity_insert dbo.Campaigns ON;
	insert into Campaigns(Id, SurveyId, [Name], [Description], [RecipientEmails], [StartTime], [EndTime], Active)	
	values (1,1,  'Q1 - 2024', 'Company Feedback Survey Q1 - 2024',  NULL,  '2024-06-01', '2024-09-30 23:59',  1);
	set identity_insert dbo.Campaigns OFF;
END

IF NOT EXISTS ( select 1 from QuestionOptionGroups)
BEGIN
	set identity_insert dbo.QuestionOptionGroups ON;
	insert into dbo.QuestionOptionGroups([Id], [Name], [Active]) values (1, '1-3 SA', 1);
	set identity_insert dbo.QuestionOptionGroups OFF;
END
GO


IF NOT EXISTS ( select 1 from QuestionOptions)
BEGIN
	set identity_insert dbo.QuestionOptions ON;
	insert into dbo.QuestionOptions([Id], [OptionGroupId], [DisplayOrder], [Value], [Text], [Active]) values (1, 1, 1, '1',  '1', 1);
	insert into dbo.QuestionOptions([Id], [OptionGroupId], [DisplayOrder], [Value], [Text], [Active]) values (2, 1, 2, '2',  '2', 1);
	insert into dbo.QuestionOptions([Id], [OptionGroupId], [DisplayOrder], [Value], [Text], [Active]) values (3, 1, 3, '3',  '3', 1);
	insert into dbo.QuestionOptions([Id], [OptionGroupId], [DisplayOrder], [Value], [Text], [Active]) values (4, 1, 4, '-1','SA', 1);
	set identity_insert dbo.QuestionOptions OFF;
END
GO


IF NOT EXISTS ( select 1 from Questions)
BEGIN	
	set identity_insert dbo.Questions ON;
	-- A
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (1, 1, NULL, NULL, 1, 'A', 'section', 'Cum evaluati atmosfera la locul de munca:', 1, 0, 1);
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (2, 1, 1, 1, 2, 'A1', 'radio-list', '- in companie?', 0, 1, 1);
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (3, 1, 1, 1, 3, 'A2', 'radio-list', '- in atelierul/departamentul in care lucrati?', 0, 1, 1);
	-- B 
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (4, 1, NULL, NULL, 4, 'B', 'section', 'Cum evaluati colaborarea:', 1, 0, 1);
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (5, 1, 4, 1, 5, 'B1', 'radio-list', '- cu colegii de munca?', 0, 1, 1);
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (6, 1, 4, 1, 6, 'B2', 'radio-list', '- cu managerii?', 0, 1, 1);
	-- C 
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (7, 1, NULL, NULL, 7, 'C', 'section', 'Cum evaluati locul  dumneavoastra de munca in ceea ce priveste:', 1, 0, 1);
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (8, 1, 7, 1, 8, 'C1', 'radio-list', '- organizarea si eficacitatea muncii in intreprindere?', 0, 1, 1);
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (9, 1, 7, 1, 9, 'C2', 'radio-list', '- echipamentele(masinile) si mijloacele logistice(calculatoare, programe informatice, telefon, fax...) puse la dispozitie?', 0, 1, 1);
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (10, 1, 7, 1, 10, 'C3', 'radio-list', '- comunicarea si transparenta in ceea ce priveste politica intreprinderii, abordarea problemelor, discutarea chestiunilor sociale', 0, 1, 1);
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (11, 1, 7, 1, 11, 'C4', 'radio-list', '- mediul social (sala de mese, toalete, sala de sedinte, spatii verzi, etc) ?', 0, 1, 1);
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (12, 1, 7, 1, 12, 'C5', 'radio-list', '- amenajarea locului dvs.de lucru(amplasare, ergonomie, aerisire, iluminat, caldura/ umiditate, zgomot, vibratii, poluare)?', 0, 1, 1);
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (13, 1, 7, 1, 13, 'C6', 'radio-list', '- securitatea si igiena muncii in serviciul dvs. (securitatea masinilor, echipamente de protectie individuale si colective, dispozitii privind securitatea  si curatenia la locul de munca)?', 0, 1, 1);
	-- D 
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (14, 1, NULL, 1, 14, 'D', 'section-radio-list', '- securitatea si igiena muncii in serviciul dvs. (securitatea masinilor, echipamente de protectie individuale si colective, dispozitii privind securitatea  si curatenia la locul de munca)?', 1, 1, 1);
	-- E 
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (15, 1, NULL, 1, 15, 'E', 'section-radio-list', 'Sunteti satisfacut de nivelul de autonomie la locul de munca?', 1, 1, 1);
	-- F 
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (16, 1, NULL, 1, 16, 'F', 'section-radio-list', 'Fata de as teptarile dvs., considerati  ca ati progresat si acumulat  competente la Leman Industrie?', 1, 1, 1);
	-- G
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (17, 1, NULL, 1, 17, 'G', 'section-radio-list', 'Va simtiti motivat sa va indepliniti obiectivele?', 1, 1, 1);
	-- H
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (18, 1, NULL, 1, 18, 'H', 'section-radio-list', 'Pe parcursul unei saptamani obisnuite cum percepeti situatiile de stres si presiune?', 1, 1, 1);
	-- I
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (19, 1, NULL, 1, 19, 'I', 'section-radio-list', 'Cum simtiti ca sunteti remunerat pentru munca depusa? Mentionati in campul "Comentarii" tipurile de beneficii salariale pe care vi le doriti.', 1, 1, 1);
	-- J
	insert into dbo.Questions([Id], [SurveyId], [ParentId], [OptionGroupId], [DisplayOrder],  [DisplayOrderText], [Type], [Text], [IsSection], [Required],  [Active]) 
	values (20, 1, NULL, 1, 2, 'J', 'section-radio-list', 'Cat de mandru sunteti de imaginea companiei la care lucrati? Mentionati in campul "Comentarii" ce apreciati cel mai mult la angajatorul Leman?', 1, 1, 1);
	set identity_insert dbo.Questions OFF;
END
GO
