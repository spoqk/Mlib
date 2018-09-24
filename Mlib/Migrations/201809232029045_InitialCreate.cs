namespace Mlib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Year = c.String(),
                        ImageId = c.String(),
                        Artist_Name = c.String(nullable: false, maxLength: 128),
                        Playlist_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Artists", t => t.Artist_Name, cascadeDelete: true)
                .ForeignKey("dbo.Playlists", t => t.Playlist_Name)
                .Index(t => t.Artist_Name)
                .Index(t => t.Playlist_Name);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        ImageId = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        TrackId = c.Int(nullable: false, identity: true),
                        FullPath = c.String(nullable: false),
                        Title = c.String(),
                        Artist = c.String(),
                        Album = c.String(),
                        Length = c.Long(nullable: false),
                        Album_AlbumId = c.Int(),
                    })
                .PrimaryKey(t => t.TrackId)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumId)
                .Index(t => t.Album_AlbumId);
            
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        ImageId = c.String(),
                        Playlist_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Playlists", t => t.Playlist_Name)
                .Index(t => t.Playlist_Name);
            
            CreateTable(
                "dbo.PlaylistTracks",
                c => new
                    {
                        Playlist_Name = c.String(nullable: false, maxLength: 128),
                        Track_TrackId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Playlist_Name, t.Track_TrackId })
                .ForeignKey("dbo.Playlists", t => t.Playlist_Name, cascadeDelete: true)
                .ForeignKey("dbo.Tracks", t => t.Track_TrackId, cascadeDelete: true)
                .Index(t => t.Playlist_Name)
                .Index(t => t.Track_TrackId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "Album_AlbumId", "dbo.Albums");
            DropForeignKey("dbo.PlaylistTracks", "Track_TrackId", "dbo.Tracks");
            DropForeignKey("dbo.PlaylistTracks", "Playlist_Name", "dbo.Playlists");
            DropForeignKey("dbo.Playlists", "Playlist_Name", "dbo.Playlists");
            DropForeignKey("dbo.Albums", "Playlist_Name", "dbo.Playlists");
            DropForeignKey("dbo.Albums", "Artist_Name", "dbo.Artists");
            DropIndex("dbo.PlaylistTracks", new[] { "Track_TrackId" });
            DropIndex("dbo.PlaylistTracks", new[] { "Playlist_Name" });
            DropIndex("dbo.Playlists", new[] { "Playlist_Name" });
            DropIndex("dbo.Tracks", new[] { "Album_AlbumId" });
            DropIndex("dbo.Albums", new[] { "Playlist_Name" });
            DropIndex("dbo.Albums", new[] { "Artist_Name" });
            DropTable("dbo.PlaylistTracks");
            DropTable("dbo.Playlists");
            DropTable("dbo.Tracks");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}
