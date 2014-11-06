
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/06/2014 10:19:35
-- Generated from EDMX file: C:\Users\Robin\Documents\Visual Studio 2012\Projects\EventHandlingSystem\EventHandlingSystem\EventHandlingDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EventHandlingSystem];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TaxonomyTermSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TermSets] DROP CONSTRAINT [FK_TaxonomyTermSet];
GO
IF OBJECT_ID(N'[dbo].[FK_TermsInTermSets_Term]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TermsInTermSets] DROP CONSTRAINT [FK_TermsInTermSets_Term];
GO
IF OBJECT_ID(N'[dbo].[FK_TermsInTermSets_TermSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TermsInTermSets] DROP CONSTRAINT [FK_TermsInTermSets_TermSet];
GO
IF OBJECT_ID(N'[dbo].[FK_TermsInEvents_Term]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TermsInEvents] DROP CONSTRAINT [FK_TermsInEvents_Term];
GO
IF OBJECT_ID(N'[dbo].[FK_TermsInEvents_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TermsInEvents] DROP CONSTRAINT [FK_TermsInEvents_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_TermsInCalendars_Term]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TermsInCalendars] DROP CONSTRAINT [FK_TermsInCalendars_Term];
GO
IF OBJECT_ID(N'[dbo].[FK_TermsInCalendars_Calendar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TermsInCalendars] DROP CONSTRAINT [FK_TermsInCalendars_Calendar];
GO
IF OBJECT_ID(N'[dbo].[FK_EventsInCalendars_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventsInCalendars] DROP CONSTRAINT [FK_EventsInCalendars_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_EventsInCalendars_Calendar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventsInCalendars] DROP CONSTRAINT [FK_EventsInCalendars_Calendar];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Taxonomies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Taxonomies];
GO
IF OBJECT_ID(N'[dbo].[TermSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TermSets];
GO
IF OBJECT_ID(N'[dbo].[Terms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Terms];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[Calendars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Calendars];
GO
IF OBJECT_ID(N'[dbo].[TermsInTermSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TermsInTermSets];
GO
IF OBJECT_ID(N'[dbo].[TermsInEvents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TermsInEvents];
GO
IF OBJECT_ID(N'[dbo].[TermsInCalendars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TermsInCalendars];
GO
IF OBJECT_ID(N'[dbo].[EventsInCalendars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventsInCalendars];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Taxonomies'
CREATE TABLE [dbo].[Taxonomies] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'TermSets'
CREATE TABLE [dbo].[TermSets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TaxonomyId] int  NOT NULL
);
GO

-- Creating table 'Terms'
CREATE TABLE [dbo].[Terms] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Calendars'
CREATE TABLE [dbo].[Calendars] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'WebPages'
CREATE TABLE [dbo].[WebPages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Calendar_Id] int  NOT NULL
);
GO

-- Creating table 'TermsInTermSets'
CREATE TABLE [dbo].[TermsInTermSets] (
    [Term_Id] int  NOT NULL,
    [TermSet_Id] int  NOT NULL
);
GO

-- Creating table 'TermsInEvents'
CREATE TABLE [dbo].[TermsInEvents] (
    [Term_Id] int  NOT NULL,
    [Event_Id] int  NOT NULL
);
GO

-- Creating table 'TermsInCalendars'
CREATE TABLE [dbo].[TermsInCalendars] (
    [Term_Id] int  NOT NULL,
    [Calendar_Id] int  NOT NULL
);
GO

-- Creating table 'EventsInCalendars'
CREATE TABLE [dbo].[EventsInCalendars] (
    [Event_Id] int  NOT NULL,
    [Calendar_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Taxonomies'
ALTER TABLE [dbo].[Taxonomies]
ADD CONSTRAINT [PK_Taxonomies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TermSets'
ALTER TABLE [dbo].[TermSets]
ADD CONSTRAINT [PK_TermSets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Terms'
ALTER TABLE [dbo].[Terms]
ADD CONSTRAINT [PK_Terms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Calendars'
ALTER TABLE [dbo].[Calendars]
ADD CONSTRAINT [PK_Calendars]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WebPages'
ALTER TABLE [dbo].[WebPages]
ADD CONSTRAINT [PK_WebPages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Term_Id], [TermSet_Id] in table 'TermsInTermSets'
ALTER TABLE [dbo].[TermsInTermSets]
ADD CONSTRAINT [PK_TermsInTermSets]
    PRIMARY KEY NONCLUSTERED ([Term_Id], [TermSet_Id] ASC);
GO

-- Creating primary key on [Term_Id], [Event_Id] in table 'TermsInEvents'
ALTER TABLE [dbo].[TermsInEvents]
ADD CONSTRAINT [PK_TermsInEvents]
    PRIMARY KEY NONCLUSTERED ([Term_Id], [Event_Id] ASC);
GO

-- Creating primary key on [Term_Id], [Calendar_Id] in table 'TermsInCalendars'
ALTER TABLE [dbo].[TermsInCalendars]
ADD CONSTRAINT [PK_TermsInCalendars]
    PRIMARY KEY NONCLUSTERED ([Term_Id], [Calendar_Id] ASC);
GO

-- Creating primary key on [Event_Id], [Calendar_Id] in table 'EventsInCalendars'
ALTER TABLE [dbo].[EventsInCalendars]
ADD CONSTRAINT [PK_EventsInCalendars]
    PRIMARY KEY NONCLUSTERED ([Event_Id], [Calendar_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TaxonomyId] in table 'TermSets'
ALTER TABLE [dbo].[TermSets]
ADD CONSTRAINT [FK_TaxonomyTermSet]
    FOREIGN KEY ([TaxonomyId])
    REFERENCES [dbo].[Taxonomies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TaxonomyTermSet'
CREATE INDEX [IX_FK_TaxonomyTermSet]
ON [dbo].[TermSets]
    ([TaxonomyId]);
GO

-- Creating foreign key on [Term_Id] in table 'TermsInTermSets'
ALTER TABLE [dbo].[TermsInTermSets]
ADD CONSTRAINT [FK_TermsInTermSets_Term]
    FOREIGN KEY ([Term_Id])
    REFERENCES [dbo].[Terms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TermSet_Id] in table 'TermsInTermSets'
ALTER TABLE [dbo].[TermsInTermSets]
ADD CONSTRAINT [FK_TermsInTermSets_TermSet]
    FOREIGN KEY ([TermSet_Id])
    REFERENCES [dbo].[TermSets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TermsInTermSets_TermSet'
CREATE INDEX [IX_FK_TermsInTermSets_TermSet]
ON [dbo].[TermsInTermSets]
    ([TermSet_Id]);
GO

-- Creating foreign key on [Term_Id] in table 'TermsInEvents'
ALTER TABLE [dbo].[TermsInEvents]
ADD CONSTRAINT [FK_TermsInEvents_Term]
    FOREIGN KEY ([Term_Id])
    REFERENCES [dbo].[Terms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Event_Id] in table 'TermsInEvents'
ALTER TABLE [dbo].[TermsInEvents]
ADD CONSTRAINT [FK_TermsInEvents_Event]
    FOREIGN KEY ([Event_Id])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TermsInEvents_Event'
CREATE INDEX [IX_FK_TermsInEvents_Event]
ON [dbo].[TermsInEvents]
    ([Event_Id]);
GO

-- Creating foreign key on [Term_Id] in table 'TermsInCalendars'
ALTER TABLE [dbo].[TermsInCalendars]
ADD CONSTRAINT [FK_TermsInCalendars_Term]
    FOREIGN KEY ([Term_Id])
    REFERENCES [dbo].[Terms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Calendar_Id] in table 'TermsInCalendars'
ALTER TABLE [dbo].[TermsInCalendars]
ADD CONSTRAINT [FK_TermsInCalendars_Calendar]
    FOREIGN KEY ([Calendar_Id])
    REFERENCES [dbo].[Calendars]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TermsInCalendars_Calendar'
CREATE INDEX [IX_FK_TermsInCalendars_Calendar]
ON [dbo].[TermsInCalendars]
    ([Calendar_Id]);
GO

-- Creating foreign key on [Event_Id] in table 'EventsInCalendars'
ALTER TABLE [dbo].[EventsInCalendars]
ADD CONSTRAINT [FK_EventsInCalendars_Event]
    FOREIGN KEY ([Event_Id])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Calendar_Id] in table 'EventsInCalendars'
ALTER TABLE [dbo].[EventsInCalendars]
ADD CONSTRAINT [FK_EventsInCalendars_Calendar]
    FOREIGN KEY ([Calendar_Id])
    REFERENCES [dbo].[Calendars]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventsInCalendars_Calendar'
CREATE INDEX [IX_FK_EventsInCalendars_Calendar]
ON [dbo].[EventsInCalendars]
    ([Calendar_Id]);
GO

-- Creating foreign key on [Calendar_Id] in table 'WebPages'
ALTER TABLE [dbo].[WebPages]
ADD CONSTRAINT [FK_WebPageCalendar]
    FOREIGN KEY ([Calendar_Id])
    REFERENCES [dbo].[Calendars]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WebPageCalendar'
CREATE INDEX [IX_FK_WebPageCalendar]
ON [dbo].[WebPages]
    ([Calendar_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------