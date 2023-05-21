IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [IMAGE_MOVIE] (
    [ID] int NOT NULL IDENTITY,
    [ImdbPrimaryImageUrl] nvarchar(1000) NOT NULL,
    [Width] int NOT NULL,
    [Height] int NOT NULL,
    CONSTRAINT [PK_IMAGE_MOVIE] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [MOVIE] (
    [ID] int NOT NULL IDENTITY,
    [ImageMovieID] int NULL,
    [IMDBID] nvarchar(10) NOT NULL,
    [IMDBAggregateRatting] decimal(3,2) NULL,
    [IMDBTitleTypeID] nvarchar(50) NULL,
    [IMDBTiltleText] nvarchar(200) NOT NULL,
    [ReleaseYear] int NULL,
    [ReleaseDate] datetime2 NULL,
    CONSTRAINT [PK_MOVIE] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_MOVIE_IMAGE_MOVIE_ImageMovieID] FOREIGN KEY ([ImageMovieID]) REFERENCES [IMAGE_MOVIE] ([ID])
);
GO

CREATE TABLE [GENRE_MOVIE] (
    [ID] int NOT NULL IDENTITY,
    [MovieID] int NULL,
    [IMDBGenreID] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_GENRE_MOVIE] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_GENRE_MOVIE_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID])
);
GO

CREATE TABLE [PLOT_MOVIE] (
    [ID] int NOT NULL IDENTITY,
    [MovieID] int NULL,
    [IMDBPlainText] nvarchar(max) NOT NULL,
    [IMDBLanguageID] nvarchar(5) NULL,
    CONSTRAINT [PK_PLOT_MOVIE] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_PLOT_MOVIE_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID])
);
GO

CREATE TABLE [PRINCIPAL_CAST_MOVIE] (
    [ID] int NOT NULL IDENTITY,
    [MovieID] int NULL,
    [ImageID] int NULL,
    [IMDBNameID] nvarchar(10) NOT NULL,
    [IMDBName] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_PRINCIPAL_CAST_MOVIE] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_PRINCIPAL_CAST_MOVIE_IMAGE_MOVIE_ImageID] FOREIGN KEY ([ImageID]) REFERENCES [IMAGE_MOVIE] ([ID]),
    CONSTRAINT [FK_PRINCIPAL_CAST_MOVIE_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID])
);
GO

CREATE TABLE [PRINCIPAL_CAST_MOVIE_CHARACTER] (
    [ID] int NOT NULL IDENTITY,
    [PrincipalCastMovieID] int NULL,
    [IMDBCharacterName] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_PRINCIPAL_CAST_MOVIE_CHARACTER] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_PRINCIPAL_CAST_MOVIE_CHARACTER_PRINCIPAL_CAST_MOVIE_PrincipalCastMovieID] FOREIGN KEY ([PrincipalCastMovieID]) REFERENCES [PRINCIPAL_CAST_MOVIE] ([ID])
);
GO

CREATE INDEX [IX_GENRE_MOVIE_IMDBGenreID] ON [GENRE_MOVIE] ([IMDBGenreID]);
GO

CREATE INDEX [IX_GENRE_MOVIE_MovieID] ON [GENRE_MOVIE] ([MovieID]);
GO

CREATE INDEX [IX_IMAGE_MOVIE_ImdbPrimaryImageUrl] ON [IMAGE_MOVIE] ([ImdbPrimaryImageUrl]);
GO

CREATE INDEX [IX_MOVIE_ImageMovieID] ON [MOVIE] ([ImageMovieID]);
GO

CREATE UNIQUE INDEX [IX_MOVIE_IMDBID] ON [MOVIE] ([IMDBID]);
GO

CREATE INDEX [IX_MOVIE_IMDBTiltleText] ON [MOVIE] ([IMDBTiltleText]);
GO

CREATE INDEX [IX_PLOT_MOVIE_IMDBLanguageID] ON [PLOT_MOVIE] ([IMDBLanguageID]);
GO

CREATE INDEX [IX_PLOT_MOVIE_MovieID] ON [PLOT_MOVIE] ([MovieID]);
GO

CREATE INDEX [IX_PRINCIPAL_CAST_MOVIE_ImageID] ON [PRINCIPAL_CAST_MOVIE] ([ImageID]);
GO

CREATE INDEX [IX_PRINCIPAL_CAST_MOVIE_IMDBName] ON [PRINCIPAL_CAST_MOVIE] ([IMDBName]);
GO

CREATE INDEX [IX_PRINCIPAL_CAST_MOVIE_MovieID] ON [PRINCIPAL_CAST_MOVIE] ([MovieID]);
GO

CREATE INDEX [IX_PRINCIPAL_CAST_MOVIE_CHARACTER_IMDBCharacterName] ON [PRINCIPAL_CAST_MOVIE_CHARACTER] ([IMDBCharacterName]);
GO

CREATE INDEX [IX_PRINCIPAL_CAST_MOVIE_CHARACTER_PrincipalCastMovieID] ON [PRINCIPAL_CAST_MOVIE_CHARACTER] ([PrincipalCastMovieID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230516120628_initial', N'7.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[MOVIE].[IMDBTiltleText]', N'IMDBTitleText', N'COLUMN';
GO

EXEC sp_rename N'[MOVIE].[IX_MOVIE_IMDBTiltleText]', N'IX_MOVIE_IMDBTitleText', N'INDEX';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230519105302_fix001', N'7.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MOVIE]') AND [c].[name] = N'ReleaseDate');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [MOVIE] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [MOVIE] DROP COLUMN [ReleaseDate];
GO

ALTER TABLE [MOVIE] ADD [IMDBTitleTypeText] nvarchar(max) NULL;
GO

ALTER TABLE [MOVIE] ADD [ReleaseDateID] int NULL;
GO

CREATE TABLE [ReleaseDate] (
    [ID] int NOT NULL IDENTITY,
    [day] int NOT NULL,
    [month] int NOT NULL,
    [year] int NOT NULL,
    CONSTRAINT [PK_ReleaseDate] PRIMARY KEY ([ID])
);
GO

CREATE INDEX [IX_MOVIE_ReleaseDateID] ON [MOVIE] ([ReleaseDateID]);
GO

ALTER TABLE [MOVIE] ADD CONSTRAINT [FK_MOVIE_ReleaseDate_ReleaseDateID] FOREIGN KEY ([ReleaseDateID]) REFERENCES [ReleaseDate] ([ID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230521174455_fix002', N'7.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [MOVIE] DROP CONSTRAINT [FK_MOVIE_ReleaseDate_ReleaseDateID];
GO

ALTER TABLE [ReleaseDate] DROP CONSTRAINT [PK_ReleaseDate];
GO

EXEC sp_rename N'[ReleaseDate]', N'RELEASE_DATE';
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MOVIE]') AND [c].[name] = N'IMDBTitleTypeText');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [MOVIE] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [MOVIE] ALTER COLUMN [IMDBTitleTypeText] nvarchar(50) NULL;
GO

ALTER TABLE [RELEASE_DATE] ADD CONSTRAINT [PK_RELEASE_DATE] PRIMARY KEY ([ID]);
GO

ALTER TABLE [MOVIE] ADD CONSTRAINT [FK_MOVIE_RELEASE_DATE_ReleaseDateID] FOREIGN KEY ([ReleaseDateID]) REFERENCES [RELEASE_DATE] ([ID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230521174800_fix003', N'7.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [GENRE_MOVIE] DROP CONSTRAINT [FK_GENRE_MOVIE_MOVIE_MovieID];
GO

ALTER TABLE [MOVIE] DROP CONSTRAINT [FK_MOVIE_PLOT_MOVIE_PlotID];
GO

ALTER TABLE [MOVIE] DROP CONSTRAINT [FK_MOVIE_RELEASE_DATE_ReleaseDateID];
GO

DROP INDEX [IX_MOVIE_PlotID] ON [MOVIE];
GO

DROP INDEX [IX_MOVIE_ReleaseDateID] ON [MOVIE];
GO

DROP INDEX [IX_GENRE_MOVIE_IMDBGenreID] ON [GENRE_MOVIE];
GO

DROP INDEX [IX_GENRE_MOVIE_MovieID] ON [GENRE_MOVIE];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MOVIE]') AND [c].[name] = N'PlotID');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [MOVIE] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [MOVIE] DROP COLUMN [PlotID];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MOVIE]') AND [c].[name] = N'ReleaseDateID');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [MOVIE] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [MOVIE] DROP COLUMN [ReleaseDateID];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GENRE_MOVIE]') AND [c].[name] = N'IMDBGenreID');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [GENRE_MOVIE] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [GENRE_MOVIE] DROP COLUMN [IMDBGenreID];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GENRE_MOVIE]') AND [c].[name] = N'IMDBGenreText');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [GENRE_MOVIE] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [GENRE_MOVIE] DROP COLUMN [IMDBGenreText];
GO

ALTER TABLE [RELEASE_DATE] ADD [MovieID] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [PLOT_MOVIE] ADD [MovieID] int NOT NULL DEFAULT 0;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GENRE_MOVIE]') AND [c].[name] = N'MovieID');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [GENRE_MOVIE] DROP CONSTRAINT [' + @var6 + '];');
UPDATE [GENRE_MOVIE] SET [MovieID] = 0 WHERE [MovieID] IS NULL;
ALTER TABLE [GENRE_MOVIE] ALTER COLUMN [MovieID] int NOT NULL;
ALTER TABLE [GENRE_MOVIE] ADD DEFAULT 0 FOR [MovieID];
GO

CREATE TABLE [GENRE] (
    [ID] int NOT NULL IDENTITY,
    [IMDBGenreID] nvarchar(50) NOT NULL,
    [IMDBGenreText] nvarchar(50) NOT NULL,
    [GenreMovieID] int NULL,
    CONSTRAINT [PK_GENRE] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_GENRE_GENRE_MOVIE_GenreMovieID] FOREIGN KEY ([GenreMovieID]) REFERENCES [GENRE_MOVIE] ([ID])
);
GO

CREATE UNIQUE INDEX [IX_RELEASE_DATE_MovieID] ON [RELEASE_DATE] ([MovieID]);
GO

CREATE UNIQUE INDEX [IX_PLOT_MOVIE_MovieID] ON [PLOT_MOVIE] ([MovieID]);
GO

CREATE UNIQUE INDEX [IX_GENRE_MOVIE_MovieID] ON [GENRE_MOVIE] ([MovieID]);
GO

CREATE INDEX [IX_GENRE_GenreMovieID] ON [GENRE] ([GenreMovieID]);
GO

CREATE INDEX [IX_GENRE_IMDBGenreID] ON [GENRE] ([IMDBGenreID]);
GO

ALTER TABLE [GENRE_MOVIE] ADD CONSTRAINT [FK_GENRE_MOVIE_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID]) ON DELETE CASCADE;
GO

ALTER TABLE [PLOT_MOVIE] ADD CONSTRAINT [FK_PLOT_MOVIE_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID]) ON DELETE CASCADE;
GO

ALTER TABLE [RELEASE_DATE] ADD CONSTRAINT [FK_RELEASE_DATE_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230521202351_fix004', N'7.0.5');
GO

COMMIT;
GO