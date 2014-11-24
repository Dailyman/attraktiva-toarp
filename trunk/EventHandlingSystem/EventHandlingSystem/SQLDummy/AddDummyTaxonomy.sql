﻿USE [EventHandlingSystem]
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
           ('Dam Senior','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Dam Junior','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Herr Senior','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Herr Junior','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Vikingen IF','2014-11-24','0')
GO
INSERT INTO [dbo].[Terms]([Name],[Created],[IsDeleted])
     VALUES
           ('Kultur','2014-11-24','0')
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
           ('Dalsjöfors Hajklubb','2014-11-24','0')
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
