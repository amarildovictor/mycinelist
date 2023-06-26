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

CREATE TABLE [GENRE] (
    [IMDBGenreID] nvarchar(50) NOT NULL,
    [IMDBGenreText] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_GENRE] PRIMARY KEY ([IMDBGenreID])
);
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
    [IMDBID] nvarchar(10) NOT NULL,
    [IMDBAggregateRatting] decimal(3,2) NULL,
    [IMDBTitleTypeID] nvarchar(50) NULL,
    [IMDBTitleTypeText] nvarchar(50) NULL,
    [IMDBTitleText] nvarchar(200) NOT NULL,
    [ReleaseYear] int NULL,
    [ImageMovieID] int NULL,
    CONSTRAINT [PK_MOVIE] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_MOVIE_IMAGE_MOVIE_ImageMovieID] FOREIGN KEY ([ImageMovieID]) REFERENCES [IMAGE_MOVIE] ([ID])
);
GO

CREATE TABLE [GENRE_MOVIE] (
    [GenresIMDBGenreID] nvarchar(50) NOT NULL,
    [MovieID] int NOT NULL,
    CONSTRAINT [PK_GENRE_MOVIE] PRIMARY KEY ([GenresIMDBGenreID], [MovieID]),
    CONSTRAINT [FK_GENRE_MOVIE_GENRE_GenresIMDBGenreID] FOREIGN KEY ([GenresIMDBGenreID]) REFERENCES [GENRE] ([IMDBGenreID]) ON DELETE CASCADE,
    CONSTRAINT [FK_GENRE_MOVIE_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID]) ON DELETE CASCADE
);
GO

CREATE TABLE [PLOT_MOVIE] (
    [ID] int NOT NULL IDENTITY,
    [MovieID] int NOT NULL,
    [IMDBPlainText] nvarchar(max) NOT NULL,
    [IMDBLanguageID] nvarchar(5) NULL,
    CONSTRAINT [PK_PLOT_MOVIE] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_PLOT_MOVIE_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID]) ON DELETE CASCADE
);
GO

CREATE TABLE [PRINCIPAL_CAST_MOVIE] (
    [ID] int NOT NULL IDENTITY,
    [MovieID] int NULL,
    [IMDBNameID] nvarchar(10) NOT NULL,
    [IMDBName] nvarchar(100) NOT NULL,
    [ImageID] int NULL,
    CONSTRAINT [PK_PRINCIPAL_CAST_MOVIE] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_PRINCIPAL_CAST_MOVIE_IMAGE_MOVIE_ImageID] FOREIGN KEY ([ImageID]) REFERENCES [IMAGE_MOVIE] ([ID]),
    CONSTRAINT [FK_PRINCIPAL_CAST_MOVIE_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID])
);
GO

CREATE TABLE [RELEASE_DATE] (
    [ID] int NOT NULL IDENTITY,
    [MovieID] int NOT NULL,
    [Day] int NULL,
    [Month] int NULL,
    [Year] int NULL,
    CONSTRAINT [PK_RELEASE_DATE] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_RELEASE_DATE_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID]) ON DELETE CASCADE
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

CREATE INDEX [IX_GENRE_MOVIE_MovieID] ON [GENRE_MOVIE] ([MovieID]);
GO

CREATE INDEX [IX_IMAGE_MOVIE_ImdbPrimaryImageUrl] ON [IMAGE_MOVIE] ([ImdbPrimaryImageUrl]);
GO

CREATE INDEX [IX_MOVIE_ImageMovieID] ON [MOVIE] ([ImageMovieID]);
GO

CREATE UNIQUE INDEX [IX_MOVIE_IMDBID] ON [MOVIE] ([IMDBID]);
GO

CREATE INDEX [IX_MOVIE_IMDBTitleText] ON [MOVIE] ([IMDBTitleText]);
GO

CREATE INDEX [IX_PLOT_MOVIE_IMDBLanguageID] ON [PLOT_MOVIE] ([IMDBLanguageID]);
GO

CREATE UNIQUE INDEX [IX_PLOT_MOVIE_MovieID] ON [PLOT_MOVIE] ([MovieID]);
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

CREATE UNIQUE INDEX [IX_RELEASE_DATE_MovieID] ON [RELEASE_DATE] ([MovieID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230523103106_firstMigration', N'7.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MOVIE]') AND [c].[name] = N'IMDBAggregateRatting');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [MOVIE] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [MOVIE] ALTER COLUMN [IMDBAggregateRatting] decimal(4,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230523225042_fix001', N'7.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MOVIE_DOWNLOAD_YEAR_CONTROL] (
    [Year] int NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [InfoUpdateDate] datetime2 NOT NULL,
    [ToUpdateNextCall] bit NOT NULL,
    CONSTRAINT [PK_MOVIE_DOWNLOAD_YEAR_CONTROL] PRIMARY KEY ([Year])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230602092901_fix002', N'7.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [IMAGE_MOVIE] ADD [ConsidererToResizingFunction] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [IMAGE_MOVIE] ADD [MediumImageUrl] nvarchar(1000) NULL;
GO

ALTER TABLE [IMAGE_MOVIE] ADD [SmallImageUrl] nvarchar(1000) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230605182931_fix003', N'7.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [Token] nvarchar(max) NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230615121343_fix004', N'7.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [Discriminator] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetUsers] ADD [Password] nvarchar(max) NULL;
GO

CREATE TABLE [USER_MOVIE_LIST] (
    [ID] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [MovieID] int NOT NULL,
    [Date] datetime2 NOT NULL,
    [Rating] int NOT NULL,
    [isToEmailNotificate] bit NOT NULL,
    CONSTRAINT [PK_USER_MOVIE_LIST] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_USER_MOVIE_LIST_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_USER_MOVIE_LIST_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_USER_MOVIE_LIST_MovieID] ON [USER_MOVIE_LIST] ([MovieID]);
GO

CREATE INDEX [IX_USER_MOVIE_LIST_UserId] ON [USER_MOVIE_LIST] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230621204449_fix005', N'7.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [USER_MOVIE_LIST] DROP CONSTRAINT [FK_USER_MOVIE_LIST_AspNetUsers_UserId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Discriminator');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AspNetUsers] DROP COLUMN [Discriminator];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USER_MOVIE_LIST]') AND [c].[name] = N'UserId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [USER_MOVIE_LIST] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [USER_MOVIE_LIST] ALTER COLUMN [UserId] nvarchar(450) NULL;
GO

ALTER TABLE [USER_MOVIE_LIST] ADD CONSTRAINT [FK_USER_MOVIE_LIST_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230622105758_fix006', N'7.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_USER_MOVIE_LIST_UserId] ON [USER_MOVIE_LIST];
GO

EXEC sp_rename N'[USER_MOVIE_LIST].[isToEmailNotificate]', N'IsToEmailNotificate', N'COLUMN';
GO

CREATE UNIQUE INDEX [IX_USER_MOVIE_LIST_UserId_MovieID] ON [USER_MOVIE_LIST] ([UserId], [MovieID]) WHERE [UserId] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230622162841_fix007', N'7.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USER_MOVIE_LIST]') AND [c].[name] = N'Rating');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [USER_MOVIE_LIST] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [USER_MOVIE_LIST] ALTER COLUMN [Rating] int NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USER_MOVIE_LIST]') AND [c].[name] = N'IsToEmailNotificate');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [USER_MOVIE_LIST] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [USER_MOVIE_LIST] ALTER COLUMN [IsToEmailNotificate] bit NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230623152944_fix008', N'7.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USER_MOVIE_LIST]') AND [c].[name] = N'Rating');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [USER_MOVIE_LIST] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [USER_MOVIE_LIST] DROP COLUMN [Rating];
GO

CREATE TABLE [USER_MOVIES_RATING] (
    [ID] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NULL,
    [MovieID] int NOT NULL,
    [Date] datetime2 NOT NULL,
    [Rating] int NULL,
    CONSTRAINT [PK_USER_MOVIES_RATING] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_USER_MOVIES_RATING_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_USER_MOVIES_RATING_MOVIE_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [MOVIE] ([ID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_USER_MOVIES_RATING_MovieID] ON [USER_MOVIES_RATING] ([MovieID]);
GO

CREATE UNIQUE INDEX [IX_USER_MOVIES_RATING_UserId_MovieID] ON [USER_MOVIES_RATING] ([UserId], [MovieID]) WHERE [UserId] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230625115609_fix009', N'7.0.7');
GO

COMMIT;
GO

