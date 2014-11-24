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
           ('Äspered', 1, null, '2014-11-06','0') 		   
GO

INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Föreningstyp', 2, null, '2014-11-06','0')
GO

INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('KanotKlubben', 1, 1, '2014-11-06','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Vikingen IF', 1, 1, '2014-11-06','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Dalsjöfors', 1, null, '2014-11-06','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Silverhajen IF', 1, 5, '2014-11-06','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Dalsjöfors Scoutförening', 1, 5, '2014-11-06','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Rångedala', 1, null, '2014-11-06','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Rödhaken IF', 1, 8, '2014-11-06','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Rödhaken IF Herr Senior', 1, 9, '2014-11-06','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Rödhaken IF Dam Senior', 1, 9, '2014-11-06','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Rödhaken IF Herr Junior', 1, 10, '2014-11-06','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Rödhaken IF Dam Junior', 1, 11, '2014-11-06','0') 		   
GO

INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Rödhaken IF', 3, null, '2014-11-06','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Hashtags', 3, 14, '2014-11-06','0') 		   
GO

INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Mästerätarna', 1, 1, '2014-11-24','0') 		   
GO
INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Dalsjöfors Hajklubb', 1, 5, '2014-11-22','0') 		   
GO

INSERT INTO [dbo].[TermSets]([Name],[TaxonomyId],[ParentTermSetId],[Created],[IsDeleted])
     VALUES
           ('Eventtyp', 2, null, '2014-11-22','0') 		   
GO




INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Äspered','2014-11-06','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Sport','2014-11-06','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Friluftsliv','2014-11-06','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('KanotKlubben','2014-11-06','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('#Mål','2014-11-06','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('#Röd haka','2014-11-06','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Dalsjöfors','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Dalsjöfors Scoutförening','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Silverhajen IF','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Rångedala','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Rödhaken IF','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Rödhaken IF Dam Senior','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Rödhaken IF Dam Junior','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Rödhaken IF Herr Senior','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Rödhaken IF Herr Junior','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Vikingen IF','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Gastronomi','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Mästerätarna','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Dalsjöfors Hajklubb','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Musik','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Kultur','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Djur','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Match','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Tävling','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Träning','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Konsert','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Kurs','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Läger','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Avslutning','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Auktion','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Musikafton','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Fest','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Film','2014-11-24','0')
GO



INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (1,1)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (2,2)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (3,2)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (4,3)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (5,15)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (6,15)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (7,5)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (8,7)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (9,6)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (10,8)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (11,9)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (12,11)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (13,13)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (14,10)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (15,12)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (16,4)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (17,2)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (18,16)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (19,17)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (20,2)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (21,2)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (22,2)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (23,18)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (24,18)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (25,18)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (26,18)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (27,18)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (28,18)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (29,18)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (30,18)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (31,18)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (32,18)
GO
INSERT INTO [dbo].[TermsInTermSets]([Term_Id],[TermSet_Id])
     VALUES
           (33,18)
GO
