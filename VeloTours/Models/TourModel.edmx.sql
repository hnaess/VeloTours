
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/16/2012 13:48:48
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
IF OBJECT_ID(N'[dbo].[FK_SegmentAreasSegments_SegmentAreas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SegmentAreasSegments] DROP CONSTRAINT [FK_SegmentAreasSegments_SegmentAreas];
GO
IF OBJECT_ID(N'[dbo].[FK_SegmentAreasSegments_Segments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SegmentAreasSegments] DROP CONSTRAINT [FK_SegmentAreasSegments_Segments];
GO
IF OBJECT_ID(N'[dbo].[FK_SegmentResult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResultSet] DROP CONSTRAINT [FK_SegmentResult];
GO
IF OBJECT_ID(N'[dbo].[FK_SegmentAreaResult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResultSet] DROP CONSTRAINT [FK_SegmentAreaResult];
GO
IF OBJECT_ID(N'[dbo].[FK_AthleteLeaderBoard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LeaderBoards] DROP CONSTRAINT [FK_AthleteLeaderBoard];
GO
IF OBJECT_ID(N'[dbo].[FK_AthleteEffort]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Efforts] DROP CONSTRAINT [FK_AthleteEffort];
GO
IF OBJECT_ID(N'[dbo].[FK_ResultEffort]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Efforts] DROP CONSTRAINT [FK_ResultEffort];
GO
IF OBJECT_ID(N'[dbo].[FK_ResultLeaderBoard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LeaderBoards] DROP CONSTRAINT [FK_ResultLeaderBoard];
GO
IF OBJECT_ID(N'[dbo].[FK_RegionResult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResultSet] DROP CONSTRAINT [FK_RegionResult];
GO
IF OBJECT_ID(N'[dbo].[FK_YellowYersey]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResultSet] DROP CONSTRAINT [FK_YellowYersey];
GO
IF OBJECT_ID(N'[dbo].[FK_GreenYersey]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResultSet] DROP CONSTRAINT [FK_GreenYersey];
GO
IF OBJECT_ID(N'[dbo].[FK_PolkaDot]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResultSet] DROP CONSTRAINT [FK_PolkaDot];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Athletes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Athletes];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[LeaderBoards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LeaderBoards];
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
IF OBJECT_ID(N'[dbo].[Efforts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Efforts];
GO
IF OBJECT_ID(N'[dbo].[ResultSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ResultSet];
GO
IF OBJECT_ID(N'[dbo].[SegmentAreasSegments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SegmentAreasSegments];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Athletes'
CREATE TABLE [dbo].[Athletes] (
    [AthleteID] int  NOT NULL,
    [Name] nvarchar(max)  NULL,
    [PrivacyMode] int  NULL,
    [LastUpdated] datetime  NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [CountryID] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LeaderBoards'
CREATE TABLE [dbo].[LeaderBoards] (
    [LeaderBoardID] int IDENTITY(1,1) NOT NULL,
    [ResultID] int  NOT NULL,
    [AthleteID] int  NOT NULL,
    [Rank] int  NOT NULL,
    [YellowPoints] int  NOT NULL,
    [GreenPoints] int  NOT NULL,
    [PolkaDotPoints] int  NOT NULL,
    [ElapsedTimes_Min] int  NOT NULL,
    [ElapsedTimes_Median] int  NOT NULL,
    [ElapsedTimes_Average] int  NOT NULL,
    [ElapsedTimes_Max] int  NOT NULL,
    [ElapsedTimes_Stdev] float  NOT NULL,
    [ElapsedTimes_Percentile90] int  NOT NULL,
    [NoRidden] int  NOT NULL
);
GO

-- Creating table 'Regions'
CREATE TABLE [dbo].[Regions] (
    [RegionID] int IDENTITY(1,1) NOT NULL,
    [CountryID] int  NOT NULL,
    [Info_Name] nvarchar(max)  NOT NULL,
    [Info_Description] nvarchar(max)  NULL,
    [Info_PictureUri] nvarchar(max)  NULL,
    [Info_Distance] float  NOT NULL,
    [Info_AvgGrade] float  NOT NULL,
    [Info_ClimbCategory] nvarchar(max)  NULL,
    [Info_ElevationHigh] float  NULL,
    [Info_ElevationLow] float  NULL,
    [Info_ElevationGain] float  NULL,
    [Info_NoRiders] int  NOT NULL,
    [Info_NoRidden] int  NOT NULL,
    [Info_LastUpdated] datetime  NULL
);
GO

-- Creating table 'SegmentAreas'
CREATE TABLE [dbo].[SegmentAreas] (
    [SegmentAreaID] int IDENTITY(1,1) NOT NULL,
    [RegionID] int  NOT NULL,
    [Info_Name] nvarchar(max)  NOT NULL,
    [Info_Description] nvarchar(max)  NULL,
    [Info_PictureUri] nvarchar(max)  NULL,
    [Info_Distance] float  NOT NULL,
    [Info_AvgGrade] float  NOT NULL,
    [Info_ClimbCategory] nvarchar(max)  NULL,
    [Info_ElevationHigh] float  NULL,
    [Info_ElevationLow] float  NULL,
    [Info_ElevationGain] float  NULL,
    [Info_NoRiders] int  NOT NULL,
    [Info_NoRidden] int  NOT NULL,
    [Info_LastUpdated] datetime  NULL,
    [IsMtb] bit  NOT NULL,
    [SecretKey] uniqueidentifier  NULL
);
GO

-- Creating table 'Segments'
CREATE TABLE [dbo].[Segments] (
    [SegmentID] int  NOT NULL,
    [Info_Name] nvarchar(max)  NOT NULL,
    [Info_Description] nvarchar(max)  NULL,
    [Info_PictureUri] nvarchar(max)  NULL,
    [Info_Distance] float  NOT NULL,
    [Info_AvgGrade] float  NOT NULL,
    [Info_ClimbCategory] nvarchar(max)  NULL,
    [Info_ElevationHigh] float  NULL,
    [Info_ElevationLow] float  NULL,
    [Info_ElevationGain] float  NULL,
    [Info_NoRiders] int  NOT NULL,
    [Info_NoRidden] int  NOT NULL,
    [Info_LastUpdated] datetime  NULL
);
GO

-- Creating table 'Efforts'
CREATE TABLE [dbo].[Efforts] (
    [EffortID] int IDENTITY(1,1) NOT NULL,
    [ResultID] int  NOT NULL,
    [AthleteID] int  NOT NULL,
    [StravaActivityID] int  NOT NULL,
    [StravaID] int  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [ElapsedTime] int  NOT NULL,
    [VAM] int  NULL
);
GO

-- Creating table 'ResultSet'
CREATE TABLE [dbo].[ResultSet] (
    [ResultID] int IDENTITY(1,1) NOT NULL,
    [LastUpdated] datetime  NOT NULL,
    [Segment_SegmentID] int  NULL,
    [SegmentArea_SegmentAreaID] int  NULL,
    [Region_RegionID] int  NULL,
    [YellowYerseyLB_LeaderBoardID] int  NULL,
    [GreenYerseyLB_LeaderBoardID] int  NULL,
    [PolkaDotYerseyLB_LeaderBoardID] int  NULL
);
GO

-- Creating table 'SegmentAreasSegments'
CREATE TABLE [dbo].[SegmentAreasSegments] (
    [SegmentAreas_SegmentAreaID] int  NOT NULL,
    [Segments_SegmentID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AthleteID] in table 'Athletes'
ALTER TABLE [dbo].[Athletes]
ADD CONSTRAINT [PK_Athletes]
    PRIMARY KEY CLUSTERED ([AthleteID] ASC);
GO

-- Creating primary key on [CountryID] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([CountryID] ASC);
GO

-- Creating primary key on [LeaderBoardID] in table 'LeaderBoards'
ALTER TABLE [dbo].[LeaderBoards]
ADD CONSTRAINT [PK_LeaderBoards]
    PRIMARY KEY CLUSTERED ([LeaderBoardID] ASC);
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

-- Creating primary key on [EffortID] in table 'Efforts'
ALTER TABLE [dbo].[Efforts]
ADD CONSTRAINT [PK_Efforts]
    PRIMARY KEY CLUSTERED ([EffortID] ASC);
GO

-- Creating primary key on [ResultID] in table 'ResultSet'
ALTER TABLE [dbo].[ResultSet]
ADD CONSTRAINT [PK_ResultSet]
    PRIMARY KEY CLUSTERED ([ResultID] ASC);
GO

-- Creating primary key on [SegmentAreas_SegmentAreaID], [Segments_SegmentID] in table 'SegmentAreasSegments'
ALTER TABLE [dbo].[SegmentAreasSegments]
ADD CONSTRAINT [PK_SegmentAreasSegments]
    PRIMARY KEY NONCLUSTERED ([SegmentAreas_SegmentAreaID], [Segments_SegmentID] ASC);
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
    ON DELETE CASCADE ON UPDATE NO ACTION;

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
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RegionSegmentArea'
CREATE INDEX [IX_FK_RegionSegmentArea]
ON [dbo].[SegmentAreas]
    ([RegionID]);
GO

-- Creating foreign key on [SegmentAreas_SegmentAreaID] in table 'SegmentAreasSegments'
ALTER TABLE [dbo].[SegmentAreasSegments]
ADD CONSTRAINT [FK_SegmentAreasSegments_SegmentAreas]
    FOREIGN KEY ([SegmentAreas_SegmentAreaID])
    REFERENCES [dbo].[SegmentAreas]
        ([SegmentAreaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Segments_SegmentID] in table 'SegmentAreasSegments'
ALTER TABLE [dbo].[SegmentAreasSegments]
ADD CONSTRAINT [FK_SegmentAreasSegments_Segments]
    FOREIGN KEY ([Segments_SegmentID])
    REFERENCES [dbo].[Segments]
        ([SegmentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SegmentAreasSegments_Segments'
CREATE INDEX [IX_FK_SegmentAreasSegments_Segments]
ON [dbo].[SegmentAreasSegments]
    ([Segments_SegmentID]);
GO

-- Creating foreign key on [Segment_SegmentID] in table 'ResultSet'
ALTER TABLE [dbo].[ResultSet]
ADD CONSTRAINT [FK_SegmentResult]
    FOREIGN KEY ([Segment_SegmentID])
    REFERENCES [dbo].[Segments]
        ([SegmentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SegmentResult'
CREATE INDEX [IX_FK_SegmentResult]
ON [dbo].[ResultSet]
    ([Segment_SegmentID]);
GO

-- Creating foreign key on [SegmentArea_SegmentAreaID] in table 'ResultSet'
ALTER TABLE [dbo].[ResultSet]
ADD CONSTRAINT [FK_SegmentAreaResult]
    FOREIGN KEY ([SegmentArea_SegmentAreaID])
    REFERENCES [dbo].[SegmentAreas]
        ([SegmentAreaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SegmentAreaResult'
CREATE INDEX [IX_FK_SegmentAreaResult]
ON [dbo].[ResultSet]
    ([SegmentArea_SegmentAreaID]);
GO

-- Creating foreign key on [AthleteID] in table 'LeaderBoards'
ALTER TABLE [dbo].[LeaderBoards]
ADD CONSTRAINT [FK_AthleteLeaderBoard]
    FOREIGN KEY ([AthleteID])
    REFERENCES [dbo].[Athletes]
        ([AthleteID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AthleteLeaderBoard'
CREATE INDEX [IX_FK_AthleteLeaderBoard]
ON [dbo].[LeaderBoards]
    ([AthleteID]);
GO

-- Creating foreign key on [AthleteID] in table 'Efforts'
ALTER TABLE [dbo].[Efforts]
ADD CONSTRAINT [FK_AthleteEffort]
    FOREIGN KEY ([AthleteID])
    REFERENCES [dbo].[Athletes]
        ([AthleteID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AthleteEffort'
CREATE INDEX [IX_FK_AthleteEffort]
ON [dbo].[Efforts]
    ([AthleteID]);
GO

-- Creating foreign key on [ResultID] in table 'Efforts'
ALTER TABLE [dbo].[Efforts]
ADD CONSTRAINT [FK_ResultEffort]
    FOREIGN KEY ([ResultID])
    REFERENCES [dbo].[ResultSet]
        ([ResultID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ResultEffort'
CREATE INDEX [IX_FK_ResultEffort]
ON [dbo].[Efforts]
    ([ResultID]);
GO

-- Creating foreign key on [ResultID] in table 'LeaderBoards'
ALTER TABLE [dbo].[LeaderBoards]
ADD CONSTRAINT [FK_ResultLeaderBoard]
    FOREIGN KEY ([ResultID])
    REFERENCES [dbo].[ResultSet]
        ([ResultID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ResultLeaderBoard'
CREATE INDEX [IX_FK_ResultLeaderBoard]
ON [dbo].[LeaderBoards]
    ([ResultID]);
GO

-- Creating foreign key on [Region_RegionID] in table 'ResultSet'
ALTER TABLE [dbo].[ResultSet]
ADD CONSTRAINT [FK_RegionResult]
    FOREIGN KEY ([Region_RegionID])
    REFERENCES [dbo].[Regions]
        ([RegionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RegionResult'
CREATE INDEX [IX_FK_RegionResult]
ON [dbo].[ResultSet]
    ([Region_RegionID]);
GO

-- Creating foreign key on [YellowYerseyLB_LeaderBoardID] in table 'ResultSet'
ALTER TABLE [dbo].[ResultSet]
ADD CONSTRAINT [FK_YellowYersey]
    FOREIGN KEY ([YellowYerseyLB_LeaderBoardID])
    REFERENCES [dbo].[LeaderBoards]
        ([LeaderBoardID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_YellowYersey'
CREATE INDEX [IX_FK_YellowYersey]
ON [dbo].[ResultSet]
    ([YellowYerseyLB_LeaderBoardID]);
GO

-- Creating foreign key on [GreenYerseyLB_LeaderBoardID] in table 'ResultSet'
ALTER TABLE [dbo].[ResultSet]
ADD CONSTRAINT [FK_GreenYersey]
    FOREIGN KEY ([GreenYerseyLB_LeaderBoardID])
    REFERENCES [dbo].[LeaderBoards]
        ([LeaderBoardID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GreenYersey'
CREATE INDEX [IX_FK_GreenYersey]
ON [dbo].[ResultSet]
    ([GreenYerseyLB_LeaderBoardID]);
GO

-- Creating foreign key on [PolkaDotYerseyLB_LeaderBoardID] in table 'ResultSet'
ALTER TABLE [dbo].[ResultSet]
ADD CONSTRAINT [FK_PolkaDot]
    FOREIGN KEY ([PolkaDotYerseyLB_LeaderBoardID])
    REFERENCES [dbo].[LeaderBoards]
        ([LeaderBoardID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PolkaDot'
CREATE INDEX [IX_FK_PolkaDot]
ON [dbo].[ResultSet]
    ([PolkaDotYerseyLB_LeaderBoardID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------