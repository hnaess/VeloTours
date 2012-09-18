
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/18/2012 15:29:38
-- Generated from EDMX file: C:\Users\hna\Documents\Visual Studio 2012\Projects\VeloTours\VeloTours\Models\TourModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VeloTours];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CountryRegion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Regions] DROP CONSTRAINT [FK_CountryRegion];
GO
IF OBJECT_ID(N'[dbo].[FK_RegionSegmentArea]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SegmentAreas] DROP CONSTRAINT [FK_RegionSegmentArea];
GO
IF OBJECT_ID(N'[dbo].[FK_SegmentAreaSegment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Segments] DROP CONSTRAINT [FK_SegmentAreaSegment];
GO
IF OBJECT_ID(N'[dbo].[FK_SegmentSegmentInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Segments] DROP CONSTRAINT [FK_SegmentSegmentInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_SegmentAreaSegmentInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SegmentAreas] DROP CONSTRAINT [FK_SegmentAreaSegmentInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_StatisticAthlete]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Statistics] DROP CONSTRAINT [FK_StatisticAthlete];
GO
IF OBJECT_ID(N'[dbo].[FK_SegmentInfoStatistic]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Statistics] DROP CONSTRAINT [FK_SegmentInfoStatistic];
GO
IF OBJECT_ID(N'[dbo].[FK_SegmentInfoGrade]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SegmentInfos] DROP CONSTRAINT [FK_SegmentInfoGrade];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[Regions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Regions];
GO
IF OBJECT_ID(N'[dbo].[SegmentAreas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SegmentAreas];
GO
IF OBJECT_ID(N'[dbo].[Segments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Segments];
GO
IF OBJECT_ID(N'[dbo].[SegmentInfos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SegmentInfos];
GO
IF OBJECT_ID(N'[dbo].[Athletes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Athletes];
GO
IF OBJECT_ID(N'[dbo].[Statistics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Statistics];
GO
IF OBJECT_ID(N'[dbo].[Grades]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Grades];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [CountryID] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Regions'
CREATE TABLE [dbo].[Regions] (
    [RegionID] int IDENTITY(1,1) NOT NULL,
    [CountryID] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SegmentAreas'
CREATE TABLE [dbo].[SegmentAreas] (
    [SegmentAreaID] int IDENTITY(1,1) NOT NULL,
    [RegionID] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LastUpdated] datetime  NOT NULL,
    [SegmentInfo_SegmentInfoID] int  NOT NULL
);
GO

-- Creating table 'Segments'
CREATE TABLE [dbo].[Segments] (
    [SegmentID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [StravaID] int  NOT NULL,
    [SegmentAreaID] int  NOT NULL,
    [SegmentInfo_SegmentInfoID] int  NOT NULL
);
GO

-- Creating table 'SegmentInfos'
CREATE TABLE [dbo].[SegmentInfos] (
    [SegmentInfoID] int IDENTITY(1,1) NOT NULL,
    [Distance] float  NULL,
    [ElevDifference] int  NULL,
    [AvgGrade] float  NULL,
    [Riders] int  NULL,
    [Ridden] int  NULL,
    [GradeGradeID] int  NOT NULL
);
GO

-- Creating table 'Athletes'
CREATE TABLE [dbo].[Athletes] (
    [AthleteID] int IDENTITY(1,1) NOT NULL,
    [StravaID] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [PrivacyMode] int  NULL,
    [LastUpdated] datetime  NOT NULL
);
GO

-- Creating table 'Statistics'
CREATE TABLE [dbo].[Statistics] (
    [StatisticID] int IDENTITY(1,1) NOT NULL,
    [Period] nvarchar(max)  NOT NULL,
    [YerseyType] nvarchar(max)  NOT NULL,
    [Duration] time  NULL,
    [Points] int  NOT NULL,
    [SegmentSegmentID] int  NOT NULL,
    [SegmentInfoSegmentInfoID] int  NOT NULL,
    [Athlete_AthleteID] int  NOT NULL
);
GO

-- Creating table 'Grades'
CREATE TABLE [dbo].[Grades] (
    [GradeID] int IDENTITY(1,1) NOT NULL,
    [Climb] int  NOT NULL,
    [Sprint] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CountryID] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([CountryID] ASC);
GO

-- Creating primary key on [RegionID] in table 'Regions'
ALTER TABLE [dbo].[Regions]
ADD CONSTRAINT [PK_Regions]
    PRIMARY KEY CLUSTERED ([RegionID] ASC);
GO

-- Creating primary key on [SegmentAreaID] in table 'SegmentAreas'
ALTER TABLE [dbo].[SegmentAreas]
ADD CONSTRAINT [PK_SegmentAreas]
    PRIMARY KEY CLUSTERED ([SegmentAreaID] ASC);
GO

-- Creating primary key on [SegmentID] in table 'Segments'
ALTER TABLE [dbo].[Segments]
ADD CONSTRAINT [PK_Segments]
    PRIMARY KEY CLUSTERED ([SegmentID] ASC);
GO

-- Creating primary key on [SegmentInfoID] in table 'SegmentInfos'
ALTER TABLE [dbo].[SegmentInfos]
ADD CONSTRAINT [PK_SegmentInfos]
    PRIMARY KEY CLUSTERED ([SegmentInfoID] ASC);
GO

-- Creating primary key on [AthleteID] in table 'Athletes'
ALTER TABLE [dbo].[Athletes]
ADD CONSTRAINT [PK_Athletes]
    PRIMARY KEY CLUSTERED ([AthleteID] ASC);
GO

-- Creating primary key on [StatisticID] in table 'Statistics'
ALTER TABLE [dbo].[Statistics]
ADD CONSTRAINT [PK_Statistics]
    PRIMARY KEY CLUSTERED ([StatisticID] ASC);
GO

-- Creating primary key on [GradeID] in table 'Grades'
ALTER TABLE [dbo].[Grades]
ADD CONSTRAINT [PK_Grades]
    PRIMARY KEY CLUSTERED ([GradeID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CountryID] in table 'Regions'
ALTER TABLE [dbo].[Regions]
ADD CONSTRAINT [FK_CountryRegion]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[Countries]
        ([CountryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryRegion'
CREATE INDEX [IX_FK_CountryRegion]
ON [dbo].[Regions]
    ([CountryID]);
GO

-- Creating foreign key on [RegionID] in table 'SegmentAreas'
ALTER TABLE [dbo].[SegmentAreas]
ADD CONSTRAINT [FK_RegionSegmentArea]
    FOREIGN KEY ([RegionID])
    REFERENCES [dbo].[Regions]
        ([RegionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RegionSegmentArea'
CREATE INDEX [IX_FK_RegionSegmentArea]
ON [dbo].[SegmentAreas]
    ([RegionID]);
GO

-- Creating foreign key on [SegmentAreaID] in table 'Segments'
ALTER TABLE [dbo].[Segments]
ADD CONSTRAINT [FK_SegmentAreaSegment]
    FOREIGN KEY ([SegmentAreaID])
    REFERENCES [dbo].[SegmentAreas]
        ([SegmentAreaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SegmentAreaSegment'
CREATE INDEX [IX_FK_SegmentAreaSegment]
ON [dbo].[Segments]
    ([SegmentAreaID]);
GO

-- Creating foreign key on [SegmentInfo_SegmentInfoID] in table 'Segments'
ALTER TABLE [dbo].[Segments]
ADD CONSTRAINT [FK_SegmentSegmentInfo]
    FOREIGN KEY ([SegmentInfo_SegmentInfoID])
    REFERENCES [dbo].[SegmentInfos]
        ([SegmentInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SegmentSegmentInfo'
CREATE INDEX [IX_FK_SegmentSegmentInfo]
ON [dbo].[Segments]
    ([SegmentInfo_SegmentInfoID]);
GO

-- Creating foreign key on [SegmentInfo_SegmentInfoID] in table 'SegmentAreas'
ALTER TABLE [dbo].[SegmentAreas]
ADD CONSTRAINT [FK_SegmentAreaSegmentInfo]
    FOREIGN KEY ([SegmentInfo_SegmentInfoID])
    REFERENCES [dbo].[SegmentInfos]
        ([SegmentInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SegmentAreaSegmentInfo'
CREATE INDEX [IX_FK_SegmentAreaSegmentInfo]
ON [dbo].[SegmentAreas]
    ([SegmentInfo_SegmentInfoID]);
GO

-- Creating foreign key on [Athlete_AthleteID] in table 'Statistics'
ALTER TABLE [dbo].[Statistics]
ADD CONSTRAINT [FK_StatisticAthlete]
    FOREIGN KEY ([Athlete_AthleteID])
    REFERENCES [dbo].[Athletes]
        ([AthleteID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StatisticAthlete'
CREATE INDEX [IX_FK_StatisticAthlete]
ON [dbo].[Statistics]
    ([Athlete_AthleteID]);
GO

-- Creating foreign key on [SegmentInfoSegmentInfoID] in table 'Statistics'
ALTER TABLE [dbo].[Statistics]
ADD CONSTRAINT [FK_SegmentInfoStatistic]
    FOREIGN KEY ([SegmentInfoSegmentInfoID])
    REFERENCES [dbo].[SegmentInfos]
        ([SegmentInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SegmentInfoStatistic'
CREATE INDEX [IX_FK_SegmentInfoStatistic]
ON [dbo].[Statistics]
    ([SegmentInfoSegmentInfoID]);
GO

-- Creating foreign key on [GradeGradeID] in table 'SegmentInfos'
ALTER TABLE [dbo].[SegmentInfos]
ADD CONSTRAINT [FK_SegmentInfoGrade]
    FOREIGN KEY ([GradeGradeID])
    REFERENCES [dbo].[Grades]
        ([GradeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SegmentInfoGrade'
CREATE INDEX [IX_FK_SegmentInfoGrade]
ON [dbo].[SegmentInfos]
    ([GradeGradeID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------