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
           ('Läger','2014-11-06','0')
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