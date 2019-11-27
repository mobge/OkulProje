
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/27/2019 01:21:36
-- Generated from EDMX file: C:\Users\Atakan\source\repos\Proje\Proje\Models\OkulModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [okul];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Acilan_Dersler_Bolum]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Acilan_Dersler] DROP CONSTRAINT [FK_Acilan_Dersler_Bolum];
GO
IF OBJECT_ID(N'[dbo].[FK_Acilan_Dersler_Dersler]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Acilan_Dersler] DROP CONSTRAINT [FK_Acilan_Dersler_Dersler];
GO
IF OBJECT_ID(N'[dbo].[FK_Acilan_Dersler_Donem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Acilan_Dersler] DROP CONSTRAINT [FK_Acilan_Dersler_Donem];
GO
IF OBJECT_ID(N'[dbo].[FK_Acilan_Dersler_Fakulte]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Acilan_Dersler] DROP CONSTRAINT [FK_Acilan_Dersler_Fakulte];
GO
IF OBJECT_ID(N'[dbo].[FK_Acilan_Dersler_Kullanici]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Acilan_Dersler] DROP CONSTRAINT [FK_Acilan_Dersler_Kullanici];
GO
IF OBJECT_ID(N'[dbo].[FK_Acilan_Dersler_Siniflar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Acilan_Dersler] DROP CONSTRAINT [FK_Acilan_Dersler_Siniflar];
GO
IF OBJECT_ID(N'[dbo].[FK_Bolum_Bolum]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bolum] DROP CONSTRAINT [FK_Bolum_Bolum];
GO
IF OBJECT_ID(N'[dbo].[FK_Bolum_Kazanim_Bolum]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bolum_Kazanim] DROP CONSTRAINT [FK_Bolum_Kazanim_Bolum];
GO
IF OBJECT_ID(N'[dbo].[FK_Ders_Kazanim_Dersler]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ders_Kazanim] DROP CONSTRAINT [FK_Ders_Kazanim_Dersler];
GO
IF OBJECT_ID(N'[dbo].[FK_Dersler_Bolum]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dersler] DROP CONSTRAINT [FK_Dersler_Bolum];
GO
IF OBJECT_ID(N'[dbo].[FK_Dersler_Fakulte]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dersler] DROP CONSTRAINT [FK_Dersler_Fakulte];
GO
IF OBJECT_ID(N'[dbo].[FK_Sinav_Sonuclari_Bolum]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sinav_Sonuclari] DROP CONSTRAINT [FK_Sinav_Sonuclari_Bolum];
GO
IF OBJECT_ID(N'[dbo].[FK_Sinav_Sonuclari_Dersler]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sinav_Sonuclari] DROP CONSTRAINT [FK_Sinav_Sonuclari_Dersler];
GO
IF OBJECT_ID(N'[dbo].[FK_Sinav_Sonuclari_Donem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sinav_Sonuclari] DROP CONSTRAINT [FK_Sinav_Sonuclari_Donem];
GO
IF OBJECT_ID(N'[dbo].[FK_Sinav_Sonuclari_Fakulte]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sinav_Sonuclari] DROP CONSTRAINT [FK_Sinav_Sonuclari_Fakulte];
GO
IF OBJECT_ID(N'[dbo].[FK_Sinav_Sonuclari_Kullanici]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sinav_Sonuclari] DROP CONSTRAINT [FK_Sinav_Sonuclari_Kullanici];
GO
IF OBJECT_ID(N'[dbo].[FK_Sinav_Sonuclari_Sınav_Turu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sinav_Sonuclari] DROP CONSTRAINT [FK_Sinav_Sonuclari_Sınav_Turu];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Acilan_Dersler]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Acilan_Dersler];
GO
IF OBJECT_ID(N'[dbo].[Bolum]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bolum];
GO
IF OBJECT_ID(N'[dbo].[Bolum_Kazanim]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bolum_Kazanim];
GO
IF OBJECT_ID(N'[dbo].[Ders_Kazanim]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ders_Kazanim];
GO
IF OBJECT_ID(N'[dbo].[Dersler]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dersler];
GO
IF OBJECT_ID(N'[dbo].[Donem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Donem];
GO
IF OBJECT_ID(N'[dbo].[Fakulte]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fakulte];
GO
IF OBJECT_ID(N'[dbo].[Kullanici]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kullanici];
GO
IF OBJECT_ID(N'[dbo].[Roller]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roller];
GO
IF OBJECT_ID(N'[dbo].[Sınav_Turu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sınav_Turu];
GO
IF OBJECT_ID(N'[dbo].[Sinav_Grup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sinav_Grup];
GO
IF OBJECT_ID(N'[dbo].[Sinav_Sonuclari]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sinav_Sonuclari];
GO
IF OBJECT_ID(N'[dbo].[Siniflar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Siniflar];
GO
IF OBJECT_ID(N'[dbo].[Soru_Kazanim]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Soru_Kazanim];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Bolum'
CREATE TABLE [dbo].[Bolum] (
    [Bolum_Id] int IDENTITY(1,1) NOT NULL,
    [Bolum_Adi] nvarchar(50)  NOT NULL,
    [Fakulte_No] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Bolum_Kazanim'
CREATE TABLE [dbo].[Bolum_Kazanim] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Bolum_Id] int  NOT NULL,
    [Bolum_Yeterlilik] nvarchar(4000)  NOT NULL
);
GO

-- Creating table 'Ders_Kazanim'
CREATE TABLE [dbo].[Ders_Kazanim] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ders_Kodu] nvarchar(50)  NOT NULL,
    [Ders_Ogrenme] nvarchar(4000)  NOT NULL
);
GO

-- Creating table 'Dersler'
CREATE TABLE [dbo].[Dersler] (
    [Ders_Kodu] nvarchar(50)  NOT NULL,
    [Ders_Adi] nvarchar(50)  NOT NULL,
    [Fakulte_No] nvarchar(50)  NOT NULL,
    [Bolum_Id] int  NOT NULL
);
GO

-- Creating table 'Fakulte'
CREATE TABLE [dbo].[Fakulte] (
    [Fakulte_No] nvarchar(50)  NOT NULL,
    [Fakulte_Adi] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Sınav_Turu'
CREATE TABLE [dbo].[Sınav_Turu] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Sinav_Turu] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Sinav_Grup'
CREATE TABLE [dbo].[Sinav_Grup] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Grup_Adi] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'Soru_Kazanim'
CREATE TABLE [dbo].[Soru_Kazanim] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Soru_Cevabi] nvarchar(50)  NOT NULL,
    [Ogrenci_Cevabi] nvarchar(50)  NULL,
    [Kazanim] nvarchar(200)  NULL
);
GO

-- Creating table 'Donem'
CREATE TABLE [dbo].[Donem] (
    [Donem_Id] int IDENTITY(1,1) NOT NULL,
    [Donem_Adi] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Siniflar'
CREATE TABLE [dbo].[Siniflar] (
    [Sinif_Id] int IDENTITY(1,1) NOT NULL,
    [Sinif_No] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Kullanici'
CREATE TABLE [dbo].[Kullanici] (
    [Sicil_No] nvarchar(50)  NOT NULL,
    [Ad] nvarchar(50)  NOT NULL,
    [Soyad] nvarchar(50)  NOT NULL,
    [Sifre] nvarchar(50)  NOT NULL,
    [Rol_Id] int  NOT NULL
);
GO

-- Creating table 'Roller'
CREATE TABLE [dbo].[Roller] (
    [Rol_Id] int IDENTITY(1,1) NOT NULL,
    [Rol_Adi] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Acilan_Dersler'
CREATE TABLE [dbo].[Acilan_Dersler] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Donem_Id] int  NOT NULL,
    [Fakulte_No] nvarchar(50)  NOT NULL,
    [Bolum_Id] int  NOT NULL,
    [Ders_Kodu] nvarchar(50)  NOT NULL,
    [Sicil_No] nvarchar(50)  NOT NULL,
    [Sinif] int  NOT NULL
);
GO

-- Creating table 'Sinav_Sonuclari'
CREATE TABLE [dbo].[Sinav_Sonuclari] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Fakulte_No] nvarchar(50)  NOT NULL,
    [Bolum_ıd] int  NOT NULL,
    [Ders_Kodu] nvarchar(50)  NOT NULL,
    [Donem_Id] int  NOT NULL,
    [Sicil_No] nvarchar(50)  NULL,
    [Sonuc] nvarchar(1000)  NULL,
    [Sinav_Turu_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Bolum_Id] in table 'Bolum'
ALTER TABLE [dbo].[Bolum]
ADD CONSTRAINT [PK_Bolum]
    PRIMARY KEY CLUSTERED ([Bolum_Id] ASC);
GO

-- Creating primary key on [Id] in table 'Bolum_Kazanim'
ALTER TABLE [dbo].[Bolum_Kazanim]
ADD CONSTRAINT [PK_Bolum_Kazanim]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ders_Kazanim'
ALTER TABLE [dbo].[Ders_Kazanim]
ADD CONSTRAINT [PK_Ders_Kazanim]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Ders_Kodu] in table 'Dersler'
ALTER TABLE [dbo].[Dersler]
ADD CONSTRAINT [PK_Dersler]
    PRIMARY KEY CLUSTERED ([Ders_Kodu] ASC);
GO

-- Creating primary key on [Fakulte_No] in table 'Fakulte'
ALTER TABLE [dbo].[Fakulte]
ADD CONSTRAINT [PK_Fakulte]
    PRIMARY KEY CLUSTERED ([Fakulte_No] ASC);
GO

-- Creating primary key on [Id] in table 'Sınav_Turu'
ALTER TABLE [dbo].[Sınav_Turu]
ADD CONSTRAINT [PK_Sınav_Turu]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sinav_Grup'
ALTER TABLE [dbo].[Sinav_Grup]
ADD CONSTRAINT [PK_Sinav_Grup]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Soru_Kazanim'
ALTER TABLE [dbo].[Soru_Kazanim]
ADD CONSTRAINT [PK_Soru_Kazanim]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Donem_Id] in table 'Donem'
ALTER TABLE [dbo].[Donem]
ADD CONSTRAINT [PK_Donem]
    PRIMARY KEY CLUSTERED ([Donem_Id] ASC);
GO

-- Creating primary key on [Sinif_Id] in table 'Siniflar'
ALTER TABLE [dbo].[Siniflar]
ADD CONSTRAINT [PK_Siniflar]
    PRIMARY KEY CLUSTERED ([Sinif_Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Sicil_No] in table 'Kullanici'
ALTER TABLE [dbo].[Kullanici]
ADD CONSTRAINT [PK_Kullanici]
    PRIMARY KEY CLUSTERED ([Sicil_No] ASC);
GO

-- Creating primary key on [Rol_Id] in table 'Roller'
ALTER TABLE [dbo].[Roller]
ADD CONSTRAINT [PK_Roller]
    PRIMARY KEY CLUSTERED ([Rol_Id] ASC);
GO

-- Creating primary key on [Id] in table 'Acilan_Dersler'
ALTER TABLE [dbo].[Acilan_Dersler]
ADD CONSTRAINT [PK_Acilan_Dersler]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sinav_Sonuclari'
ALTER TABLE [dbo].[Sinav_Sonuclari]
ADD CONSTRAINT [PK_Sinav_Sonuclari]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Bolum_Id] in table 'Bolum_Kazanim'
ALTER TABLE [dbo].[Bolum_Kazanim]
ADD CONSTRAINT [FK_Bolum_Kazanim_Bolum]
    FOREIGN KEY ([Bolum_Id])
    REFERENCES [dbo].[Bolum]
        ([Bolum_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bolum_Kazanim_Bolum'
CREATE INDEX [IX_FK_Bolum_Kazanim_Bolum]
ON [dbo].[Bolum_Kazanim]
    ([Bolum_Id]);
GO

-- Creating foreign key on [Ders_Kodu] in table 'Ders_Kazanim'
ALTER TABLE [dbo].[Ders_Kazanim]
ADD CONSTRAINT [FK_Ders_Kazanim_Dersler]
    FOREIGN KEY ([Ders_Kodu])
    REFERENCES [dbo].[Dersler]
        ([Ders_Kodu])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Ders_Kazanim_Dersler'
CREATE INDEX [IX_FK_Ders_Kazanim_Dersler]
ON [dbo].[Ders_Kazanim]
    ([Ders_Kodu]);
GO

-- Creating foreign key on [Fakulte_No] in table 'Bolum'
ALTER TABLE [dbo].[Bolum]
ADD CONSTRAINT [FK_Bolum_Bolum]
    FOREIGN KEY ([Fakulte_No])
    REFERENCES [dbo].[Fakulte]
        ([Fakulte_No])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bolum_Bolum'
CREATE INDEX [IX_FK_Bolum_Bolum]
ON [dbo].[Bolum]
    ([Fakulte_No]);
GO

-- Creating foreign key on [Bolum_Id] in table 'Dersler'
ALTER TABLE [dbo].[Dersler]
ADD CONSTRAINT [FK_Dersler_Bolum]
    FOREIGN KEY ([Bolum_Id])
    REFERENCES [dbo].[Bolum]
        ([Bolum_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Dersler_Bolum'
CREATE INDEX [IX_FK_Dersler_Bolum]
ON [dbo].[Dersler]
    ([Bolum_Id]);
GO

-- Creating foreign key on [Fakulte_No] in table 'Dersler'
ALTER TABLE [dbo].[Dersler]
ADD CONSTRAINT [FK_Dersler_Fakulte]
    FOREIGN KEY ([Fakulte_No])
    REFERENCES [dbo].[Fakulte]
        ([Fakulte_No])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Dersler_Fakulte'
CREATE INDEX [IX_FK_Dersler_Fakulte]
ON [dbo].[Dersler]
    ([Fakulte_No]);
GO

-- Creating foreign key on [Bolum_Id] in table 'Acilan_Dersler'
ALTER TABLE [dbo].[Acilan_Dersler]
ADD CONSTRAINT [FK_Acilan_Dersler_Bolum]
    FOREIGN KEY ([Bolum_Id])
    REFERENCES [dbo].[Bolum]
        ([Bolum_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Acilan_Dersler_Bolum'
CREATE INDEX [IX_FK_Acilan_Dersler_Bolum]
ON [dbo].[Acilan_Dersler]
    ([Bolum_Id]);
GO

-- Creating foreign key on [Ders_Kodu] in table 'Acilan_Dersler'
ALTER TABLE [dbo].[Acilan_Dersler]
ADD CONSTRAINT [FK_Acilan_Dersler_Dersler]
    FOREIGN KEY ([Ders_Kodu])
    REFERENCES [dbo].[Dersler]
        ([Ders_Kodu])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Acilan_Dersler_Dersler'
CREATE INDEX [IX_FK_Acilan_Dersler_Dersler]
ON [dbo].[Acilan_Dersler]
    ([Ders_Kodu]);
GO

-- Creating foreign key on [Donem_Id] in table 'Acilan_Dersler'
ALTER TABLE [dbo].[Acilan_Dersler]
ADD CONSTRAINT [FK_Acilan_Dersler_Donem]
    FOREIGN KEY ([Donem_Id])
    REFERENCES [dbo].[Donem]
        ([Donem_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Acilan_Dersler_Donem'
CREATE INDEX [IX_FK_Acilan_Dersler_Donem]
ON [dbo].[Acilan_Dersler]
    ([Donem_Id]);
GO

-- Creating foreign key on [Fakulte_No] in table 'Acilan_Dersler'
ALTER TABLE [dbo].[Acilan_Dersler]
ADD CONSTRAINT [FK_Acilan_Dersler_Fakulte]
    FOREIGN KEY ([Fakulte_No])
    REFERENCES [dbo].[Fakulte]
        ([Fakulte_No])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Acilan_Dersler_Fakulte'
CREATE INDEX [IX_FK_Acilan_Dersler_Fakulte]
ON [dbo].[Acilan_Dersler]
    ([Fakulte_No]);
GO

-- Creating foreign key on [Sicil_No] in table 'Acilan_Dersler'
ALTER TABLE [dbo].[Acilan_Dersler]
ADD CONSTRAINT [FK_Acilan_Dersler_Kullanici]
    FOREIGN KEY ([Sicil_No])
    REFERENCES [dbo].[Kullanici]
        ([Sicil_No])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Acilan_Dersler_Kullanici'
CREATE INDEX [IX_FK_Acilan_Dersler_Kullanici]
ON [dbo].[Acilan_Dersler]
    ([Sicil_No]);
GO

-- Creating foreign key on [Sinif] in table 'Acilan_Dersler'
ALTER TABLE [dbo].[Acilan_Dersler]
ADD CONSTRAINT [FK_Acilan_Dersler_Siniflar]
    FOREIGN KEY ([Sinif])
    REFERENCES [dbo].[Siniflar]
        ([Sinif_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Acilan_Dersler_Siniflar'
CREATE INDEX [IX_FK_Acilan_Dersler_Siniflar]
ON [dbo].[Acilan_Dersler]
    ([Sinif]);
GO

-- Creating foreign key on [Bolum_ıd] in table 'Sinav_Sonuclari'
ALTER TABLE [dbo].[Sinav_Sonuclari]
ADD CONSTRAINT [FK_Sinav_Sonuclari_Bolum]
    FOREIGN KEY ([Bolum_ıd])
    REFERENCES [dbo].[Bolum]
        ([Bolum_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sinav_Sonuclari_Bolum'
CREATE INDEX [IX_FK_Sinav_Sonuclari_Bolum]
ON [dbo].[Sinav_Sonuclari]
    ([Bolum_ıd]);
GO

-- Creating foreign key on [Ders_Kodu] in table 'Sinav_Sonuclari'
ALTER TABLE [dbo].[Sinav_Sonuclari]
ADD CONSTRAINT [FK_Sinav_Sonuclari_Dersler]
    FOREIGN KEY ([Ders_Kodu])
    REFERENCES [dbo].[Dersler]
        ([Ders_Kodu])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sinav_Sonuclari_Dersler'
CREATE INDEX [IX_FK_Sinav_Sonuclari_Dersler]
ON [dbo].[Sinav_Sonuclari]
    ([Ders_Kodu]);
GO

-- Creating foreign key on [Donem_Id] in table 'Sinav_Sonuclari'
ALTER TABLE [dbo].[Sinav_Sonuclari]
ADD CONSTRAINT [FK_Sinav_Sonuclari_Donem]
    FOREIGN KEY ([Donem_Id])
    REFERENCES [dbo].[Donem]
        ([Donem_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sinav_Sonuclari_Donem'
CREATE INDEX [IX_FK_Sinav_Sonuclari_Donem]
ON [dbo].[Sinav_Sonuclari]
    ([Donem_Id]);
GO

-- Creating foreign key on [Fakulte_No] in table 'Sinav_Sonuclari'
ALTER TABLE [dbo].[Sinav_Sonuclari]
ADD CONSTRAINT [FK_Sinav_Sonuclari_Fakulte]
    FOREIGN KEY ([Fakulte_No])
    REFERENCES [dbo].[Fakulte]
        ([Fakulte_No])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sinav_Sonuclari_Fakulte'
CREATE INDEX [IX_FK_Sinav_Sonuclari_Fakulte]
ON [dbo].[Sinav_Sonuclari]
    ([Fakulte_No]);
GO

-- Creating foreign key on [Sicil_No] in table 'Sinav_Sonuclari'
ALTER TABLE [dbo].[Sinav_Sonuclari]
ADD CONSTRAINT [FK_Sinav_Sonuclari_Kullanici]
    FOREIGN KEY ([Sicil_No])
    REFERENCES [dbo].[Kullanici]
        ([Sicil_No])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sinav_Sonuclari_Kullanici'
CREATE INDEX [IX_FK_Sinav_Sonuclari_Kullanici]
ON [dbo].[Sinav_Sonuclari]
    ([Sicil_No]);
GO

-- Creating foreign key on [Sinav_Turu_Id] in table 'Sinav_Sonuclari'
ALTER TABLE [dbo].[Sinav_Sonuclari]
ADD CONSTRAINT [FK_Sinav_Sonuclari_Sınav_Turu]
    FOREIGN KEY ([Sinav_Turu_Id])
    REFERENCES [dbo].[Sınav_Turu]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sinav_Sonuclari_Sınav_Turu'
CREATE INDEX [IX_FK_Sinav_Sonuclari_Sınav_Turu]
ON [dbo].[Sinav_Sonuclari]
    ([Sinav_Turu_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------