USE [EventHandlingSystem]
GO

INSERT INTO [dbo].[Taxonomies]([Name],[Created],[IsDeleted])
     VALUES('Publiceringstaxonomi','2014-11-06','0')           
GO
INSERT INTO [dbo].[Taxonomies]([Name],[Created],[IsDeleted])
     VALUES('Kategoriseringstaxonomi','2014-11-06','0')           
GO
INSERT INTO [dbo].[Taxonomies]([Name],[Created],[IsDeleted])
     VALUES('Anpassadkategoriseringstaxonomi','2014-11-06','0')           
GO


INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Äspered',1,null,'2014-11-06','0') 		   
GO


INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Äspered','2014-11-06','0')
GO

INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (1,1)
GO


INSERT INTO [dbo].[Events]([Title],[Description],[Summary],[Other],[Location],[ImageUrl],[DayEvent],[StartDate],[EndDate],
[TargetGroup],[ApproximateAttendees],[AssociationId],[Created],[CreatedBy],[IsDeleted])
     VALUES
           ('Introfest till A.T. Success!','Introduktion, stor fest', 'Nice','','Löwaskog','http://www.photolakedistrict.co.uk/wp-content/uploads/events-FIREWORKS.jpg', '1','2014-11-03','2014-11-04',
		   'Kompisar','15',1,'2014-11-01','Robin','0')
GO
INSERT INTO [dbo].[Events]([Title],[Description],[Summary],[Other],[Location],[ImageUrl],[DayEvent],[StartDate],[EndDate],
[TargetGroup],[ApproximateAttendees],[AssociationId],[Created],[CreatedBy],[IsDeleted])
     VALUES
           ('Fest1','Introduktion, stor fest', 'Nice','','Löwaskog','http://www.photolakedistrict.co.uk/wp-content/uploads/events-FIREWORKS.jpg', '1','2014-08-03','2014-08-04',
		   'Kompisar','15',1,'2014-11-01','Robin','0')
GO
INSERT INTO [dbo].[Events]([Title],[Description],[Summary],[Other],[Location],[ImageUrl],[DayEvent],[StartDate],[EndDate],
[TargetGroup],[ApproximateAttendees],[AssociationId],[Created],[CreatedBy],[IsDeleted])
     VALUES
           ('Fest2','Introduktion, stor fest', 'Nice','','Löwaskog','http://www.photolakedistrict.co.uk/wp-content/uploads/events-FIREWORKS.jpg', '1','2015-01-15','2015-01-16',
		   'Kompisar','15',1,'2014-11-01','Robin','0')
GO
INSERT INTO [dbo].[Events]([Title],[Description],[Summary],[Other],[Location],[ImageUrl],[DayEvent],[StartDate],[EndDate],
[TargetGroup],[ApproximateAttendees],[AssociationId],[Created],[CreatedBy],[IsDeleted])
     VALUES
           ('Julafton','Julfest beskrivning', 'Julfest sammanfatting','Notering','Nordpolen','http://www.photolakedistrict.co.uk/wp-content/uploads/events-FIREWORKS.jpg', '1','2014-12-24','2015-12-24',
		   'Familj och vänner','15',1,'2014-11-01','Robin','1')
GO


INSERT INTO [dbo].[Calendars]([ViewMode],[Mode],[Created],[IsDeleted])
     VALUES
           ('Table', 'M', '2014-10-27', '0')
GO


INSERT INTO [dbo].[WebPages]([AssociationId],[ContentId],[LogoUrl],[Created],[IsDeleted],[Calendar_Id])
     VALUES
           (1,1,'http://upload.wikimedia.org/wikipedia/commons/6/6f/Bor%C3%A5s_municipal_arms.svg','2014-10-27', '0',1)
GO