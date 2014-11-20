USE [EventHandlingSystem]
GO




INSERT INTO [dbo].[Contents]([Created],[IsDeleted])
     VALUES
           ('2014-11-10', '0')
GO





INSERT INTO [dbo].[Components]([ContentId],[Title],[Width],[Height],[Created],[IsDeleted])
     VALUES
           (1, 'Calendar1', 400, 300, '2014-10-28', '0')
GO
INSERT INTO [dbo].[Components]([ContentId],[Title],[Width],[Height],[Created],[IsDeleted])
     VALUES
           (1, 'Calendar2', 200, 150, '2014-10-28', '0')
GO
INSERT INTO [dbo].[Components]([ContentId],[Title],[Width],[Height],[Created],[IsDeleted])
     VALUES
           (1, 'Navigation', null, null, '2014-10-28', '0')
GO




INSERT INTO [dbo].[WebPages]([LogoUrl],[NavigationComponentId],[Created],[IsDeleted],[Content_Id])
     VALUES
           ('http://upload.wikimedia.org/wikipedia/commons/6/6f/Bor%C3%A5s_municipal_arms.svg', 3, '2014-11-10', '0', 1)
GO
INSERT INTO [dbo].[WebPages]([LogoUrl],[NavigationComponentId],[Created],[IsDeleted],[Content_Id])
     VALUES
           ('http://upload.wikimedia.org/wikipedia/commons/6/6f/Bor%C3%A5s_municipal_arms.svg', 3, '2014-11-10', '0', 1)
GO
INSERT INTO [dbo].[WebPages]([LogoUrl],[NavigationComponentId],[Created],[IsDeleted],[Content_Id])
     VALUES
           ('http://upload.wikimedia.org/wikipedia/commons/6/6f/Bor%C3%A5s_municipal_arms.svg', 3, '2014-11-10', '0', 1)
GO




INSERT INTO [dbo].[Communities]([PublishingTermSetId],[Created],[CreatedBy],[IsDeleted],[WebPage_Id])
     VALUES
           (1, '2014-11-10', 'Erica', '0', 2)
GO
INSERT INTO [dbo].[Associations]([CommunityId],[ParentAssociationId],[PublishingTermSetId],[AssociationType],[Created],[CreatedBy],[IsDeleted],[WebPage_Id])
     VALUES
           (1, null, 3, 2, '2014-11-10', 'Erica', '0', 1)
GO





INSERT INTO [dbo].[Events]([Title],[Description],[Summary],[Other],[Location],[ImageUrl],[DayEvent],[StartDate],[EndDate],
[TargetGroup],[ApproximateAttendees],[AssociationId],[Created],[CreatedBy],[LatestUpdate],[UpdatedBy],[IsDeleted])
     VALUES
           ('Introfest till A.T. Success!','Introduktion, stor fest', 'Nice','','Löwaskog','http://www.photolakedistrict.co.uk/wp-content/uploads/events-FIREWORKS.jpg', '1','2014-11-03','2014-11-04',
		   'Kompisar','15',1,'2014-11-01','Robin','2014-11-10','Erica','0')
GO
INSERT INTO [dbo].[Events]([Title],[Description],[Summary],[Other],[Location],[ImageUrl],[DayEvent],[StartDate],[EndDate],
[TargetGroup],[ApproximateAttendees],[AssociationId],[Created],[CreatedBy],[LatestUpdate],[UpdatedBy],[IsDeleted])
     VALUES
           ('Fest1','Introduktion, stor fest', 'Nice','','Löwaskog','http://www.photolakedistrict.co.uk/wp-content/uploads/events-FIREWORKS.jpg', '1','2014-08-03','2014-08-04',
		   'Kompisar','15',1,'2014-11-01','Robin','2014-11-10','Erica','0')
GO
INSERT INTO [dbo].[Events]([Title],[Description],[Summary],[Other],[Location],[ImageUrl],[DayEvent],[StartDate],[EndDate],
[TargetGroup],[ApproximateAttendees],[AssociationId],[Created],[CreatedBy],[LatestUpdate],[UpdatedBy],[IsDeleted])
     VALUES
           ('Fest2','Introduktion, stor fest', 'Nice','','Löwaskog','http://www.photolakedistrict.co.uk/wp-content/uploads/events-FIREWORKS.jpg', '1','2015-01-15','2015-01-16',
		   'Kompisar','15',1,'2014-11-01','Robin','2014-11-10','Erica','0')
GO
INSERT INTO [dbo].[Events]([Title],[Description],[Summary],[Other],[Location],[ImageUrl],[DayEvent],[StartDate],[EndDate],
[TargetGroup],[ApproximateAttendees],[AssociationId],[Created],[CreatedBy],[LatestUpdate],[UpdatedBy],[IsDeleted])
     VALUES
           ('Julafton','Julfest beskrivning', 'Julfest sammanfatting','Notering','Nordpolen','http://www.photolakedistrict.co.uk/wp-content/uploads/events-FIREWORKS.jpg', '1','2014-12-24','2015-12-24',
		   'Familj och vänner','15',1,'2014-11-01','Robin','2014-11-10','Erica','1')
GO






INSERT INTO [dbo].[Calendars]([ViewMode],[Mode],[Created],[IsDeleted],[Component_Id])
     VALUES
           ('Table', 'M', '2014-10-27', '0', 1)
GO

INSERT INTO [dbo].[Calendars]([ViewMode],[Mode],[Created],[IsDeleted],[Component_Id])
     VALUES
           ('List', 'M', '2014-10-27', '0', 2)
GO
INSERT INTO [dbo].[Navigations]([PublishingTermSetId],[Created],[IsDeleted],[Component_Id])
     VALUES
           (1, '2014-11-10', '0', 3)
GO












