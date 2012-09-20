
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/20/2012 16:11:32
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
IF OBJECT_ID(N'[dbo].[FK_StatisticAthlete]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Statistics] DROP CONSTRAINT [FK_StatisticAthlete];
GO
IF OBJECT_ID(N'[dbo].[FK_AthleteLeaderboard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LeadershipBoards] DROP CONSTRAINT [FK_AthleteLeaderboard];
GO
IF OBJECT_ID(N'[dbo].[FK_ResultPeriodStatistic]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Statistics] DROP CONSTRAINT [FK_ResultPeriodStatistic];
GO
IF OBJECT_ID(N'[dbo].[FK_ResultPeriodLeaderBoard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LeadershipBoards] DROP CONSTRAINT [FK_ResultPeriodLeaderBoard];
GO
IF OBJECT_ID(N'[dbo].[FK_SegmentAreaSegment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Segments] DROP CONSTRAINT [FK_SegmentAreaSegment];
GO
IF OBJECT_ID(N'[dbo].[FK_SegmentResultPeriod]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResultPeriods] DROP CONSTRAINT [FK_SegmentResultPeriod];
GO
IF OBJECT_ID(N'[dbo].[FK_SegmentAreaResultPeriod]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResultPeriods] DROP CONSTRAINT [FK_SegmentAreaResultPeriod];
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
IF OBJECT_ID(N'[dbo].[Athletes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Athletes];
GO
IF OBJECT_ID(N'[dbo].[Statistics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Statistics];
GO
IF OBJECT_ID(N'[dbo].[LeadershipBoards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LeadershipBoards];
GO
IF OBJECT_ID(N'[dbo].[ResultPeriods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ResultPeriods];
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
    [Distance] float  NULL,
    [ElevDifference] int  NULL,
    [AvgGrade] float  NULL,
    [LastUpdated] datetime  NOT NULL
);
GO

-- Creating table 'Segments'
CREATE TABLE [dbo].[Segments] (
    [SegmentID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [StravaID] int  NOT NULL,
    [GradeType] int  NOT NULL,
    [Distance] float  NULL,
    [ElevDifference] int  NULL,
    [AvgGrade] float  NULL,
    [NoRiders] int  NULL,
    [NoRidden] int  NULL,
    [LastUpdated] datetime  NOT NULL,
    [SegmentAreaID] int  NOT NULL
);
GO

-- Creating table 'Athletes'
CREATE TABLE [dbo].[Athletes] (
    [AthleteID] int  NOT NULL,
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
    [ResultPeriodID] int  NOT NULL,
    [Athlete_AthleteID] int  NOT NULL
);
GO

-- Creating table 'LeadershipBoards'
CREATE TABLE [dbo].[LeadershipBoards] (
    [LeaderBoardID] int IDENTITY(1,1) NOT NULL,
    [Rank] nvarchar(max)  NOT NULL,
    [Duration] nvarchar(max)  NOT NULL,
    [GreenPoints] nvarchar(max)  NOT NULL,
    [PolkaDotPoints] nvarchar(max)  NOT NULL,
    [NoSegmentsRidden] nvarchar(max)  NOT NULL,
    [AthleteAthleteID] int  NOT NULL,
    [ResultPeriodID] int  NOT NULL
);
GO

-- Creating table 'ResultPeriods'
CREATE TABLE [dbo].[ResultPeriods] (
    [ResultPeriodID] int IDENTITY(1,1) NOT NULL,
    [SegmentID] int  NULL,
    [SegmentAreaID] int  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [From] datetime  NOT NULL,
    [To] datetime  NOT NULL
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

-- Creating primary key on [LeaderBoardID] in table 'LeadershipBoards'
ALTER TABLE [dbo].[LeadershipBoards]
ADD CONSTRAINT [PK_LeadershipBoards]
    PRIMARY KEY CLUSTERED ([LeaderBoardID] ASC);
GO

-- Creating primary key on [ResultPeriodID] in table 'ResultPeriods'
ALTER TABLE [dbo].[ResultPeriods]
ADD CONSTRAINT [PK_ResultPeriods]
    PRIMARY KEY CLUSTERED ([ResultPeriodID] ASC);
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

-- Creating foreign key on [AthleteAthleteID] in table 'LeadershipBoards'
ALTER TABLE [dbo].[LeadershipBoards]
ADD CONSTRAINT [FK_AthleteLeaderboard]
    FOREIGN KEY ([AthleteAthleteID])
    REFERENCES [dbo].[Athletes]
        ([AthleteID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AthleteLeaderboard'
CREATE INDEX [IX_FK_AthleteLeaderboard]
ON [dbo].[LeadershipBoards]
    ([AthleteAthleteID]);
GO

-- Creating foreign key on [ResultPeriodID] in table 'Statistics'
ALTER TABLE [dbo].[Statistics]
ADD CONSTRAINT [FK_ResultPeriodStatistic]
    FOREIGN KEY ([ResultPeriodID])
    REFERENCES [dbo].[ResultPeriods]
        ([ResultPeriodID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ResultPeriodStatistic'
CREATE INDEX [IX_FK_ResultPeriodStatistic]
ON [dbo].[Statistics]
    ([ResultPeriodID]);
GO

-- Creating foreign key on [ResultPeriodID] in table 'LeadershipBoards'
ALTER TABLE [dbo].[LeadershipBoards]
ADD CONSTRAINT [FK_ResultPeriodLeaderBoard]
    FOREIGN KEY ([ResultPeriodID])
    REFERENCES [dbo].[ResultPeriods]
        ([ResultPeriodID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ResultPeriodLeaderBoard'
CREATE INDEX [IX_FK_ResultPeriodLeaderBoard]
ON [dbo].[LeadershipBoards]
    ([ResultPeriodID]);
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

-- Creating foreign key on [SegmentID] in table 'ResultPeriods'
ALTER TABLE [dbo].[ResultPeriods]
ADD CONSTRAINT [FK_SegmentResultPeriod]
    FOREIGN KEY ([SegmentID])
    REFERENCES [dbo].[Segments]
        ([SegmentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SegmentResultPeriod'
CREATE INDEX [IX_FK_SegmentResultPeriod]
ON [dbo].[ResultPeriods]
    ([SegmentID]);
GO

-- Creating foreign key on [SegmentAreaID] in table 'ResultPeriods'
ALTER TABLE [dbo].[ResultPeriods]
ADD CONSTRAINT [FK_SegmentAreaResultPeriod]
    FOREIGN KEY ([SegmentAreaID])
    REFERENCES [dbo].[SegmentAreas]
        ([SegmentAreaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SegmentAreaResultPeriod'
CREATE INDEX [IX_FK_SegmentAreaResultPeriod]
ON [dbo].[ResultPeriods]
    ([SegmentAreaID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------