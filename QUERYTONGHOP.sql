USE [MusicWebDB]
GO

/****** Object:  Table [dbo].[BaiHat]    Script Date: 4/4/2023 11:44:55 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

drop table [BaiHat]
go

CREATE TABLE [dbo].[BaiHat](
	[idbaihat] int IDENTITY(1,1) NOT NULL,
	[idAlbum] [nchar](10) NULL,
	[idtheloai] [nchar](10) NULL,
	[idplaylist] [nchar](10) NULL,
	[Tenbaihat] [nvarchar](100) NULL,
	[Hinhbaihat] [nvarchar](100) NULL,
	[casi] [nvarchar](50) NULL,
	[linkbaihat] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.BaiHat] PRIMARY KEY CLUSTERED 
(
	[idbaihat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BaiHat]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BaiHat_dbo.Album_idtheloai] FOREIGN KEY([idtheloai])
REFERENCES [dbo].[Album] ([idAlbum])
GO

ALTER TABLE [dbo].[BaiHat] CHECK CONSTRAINT [FK_dbo.BaiHat_dbo.Album_idtheloai]
GO

ALTER TABLE [dbo].[BaiHat]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BaiHat_dbo.Playlist_idbaihat] FOREIGN KEY([idbaihat])
REFERENCES [dbo].[Playlist] ([idPlaylist])
GO

ALTER TABLE [dbo].[BaiHat] CHECK CONSTRAINT [FK_dbo.BaiHat_dbo.Playlist_idbaihat]
GO

ALTER TABLE [dbo].[BaiHat]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BaiHat_dbo.TheLoai_idAlbum] FOREIGN KEY([idAlbum])
REFERENCES [dbo].[TheLoai] ([idtheloai])
GO

ALTER TABLE [dbo].[BaiHat] CHECK CONSTRAINT [FK_dbo.BaiHat_dbo.TheLoai_idAlbum]
GO



-----------------------


USE [MusicWebDB]
GO

/****** Object:  Table [dbo].[Playlist]    Script Date: 4/4/2023 11:47:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

drop table [Playlist]
go

CREATE TABLE [dbo].[Playlist](
	[idPlaylist] INT IDENTITY(1,1) NOT NULL,
	[tenplaylist] [nvarchar](100) NULL,
	[hinh] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Playlist] PRIMARY KEY CLUSTERED 
(
	[idPlaylist] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


------------------------

drop procedure [spAddNewAudioFile]
go
drop procedure [spGetAllAudioFile]
go
drop procedure spGetAudioFileById
go
drop procedure [spDeleteAudioFile]
go
drop procedure [spGetAllPlayList]
go
drop procedure [spAddNewPlayList]
go
drop procedure spDeletePlaylist
go



CREATE procedure [dbo].[spAddNewAudioFile]  
(  

@Tenbaihat nvarchar(50),  
@casi nvarchar(50),
@linkbaihat nvarchar(100) ,
@Hinhbaihat nvarchar(100)
)  
as  
begin  
insert into BaiHat(Tenbaihat,casi,linkbaihat,Hinhbaihat)   
values (@Tenbaihat,@casi,@linkbaihat,@Hinhbaihat)   
end

CREATE procedure [dbo].[spGetAllAudioFile]  
as  
begin  
select idbaihat,Tenbaihat,casi,Hinhbaihat,linkbaihat from BaiHat 
end 

CREATE PROCEDURE spGetAudioFileById
(
    @id int
)
AS
BEGIN
    SELECT linkbaihat
    FROM BaiHat
    WHERE idbaihat = @id
END

CREATE PROCEDURE [dbo].[spDeleteAudioFile]
    @id INT
AS
BEGIN
    DELETE FROM [BaiHat]
    WHERE idbaihat = @id
END

CREATE PROCEDURE spGetAllPlayList
AS
BEGIN
    SELECT * FROM Playlist
END

CREATE PROCEDURE spAddNewPlayList
    @tenplaylist nvarchar(100),
    @hinh nvarchar(100)
AS
BEGIN
    INSERT INTO Playlist (tenplaylist, hinh)
    VALUES (@tenplaylist, @hinh)
END

CREATE PROCEDURE spDeletePlaylist
    @idPlaylist INT
AS
BEGIN
   
    DELETE FROM [dbo].[Playlist]
    WHERE [idPlaylist] = @idPlaylist;
END

