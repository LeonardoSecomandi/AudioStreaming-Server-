using System;
using Xunit;
using AudioStreaming.API.Models;
using AudioStreaming.API.Data;
using AudioStreaming.Models;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AudioStreaming.Test
{
    public class Playlist_fixture : IDisposable
    {

        private bool disposedValue;
        private SqliteConnection _connection;
        public ApplicationDbContext _ContextTest;

        public Playlist_fixture()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            var _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema and seed some data
            _ContextTest = new ApplicationDbContext(_contextOptions);

            _ContextTest.Database.EnsureCreated();


            _ContextTest.Playlist.AddRange(
                new Playlist { PlaylistID = 1, Name = "ciao", Private = true , UserID = 1, CanzonePlaylist = new List<CanzonePlaylist>() },
                new Playlist { PlaylistID = 2, Name = "bella", Private = false, UserID = 2, CanzonePlaylist = new List<CanzonePlaylist>() },
                new Playlist { PlaylistID = 3, Name = "come", Private = false, UserID = 3, CanzonePlaylist = new List<CanzonePlaylist>() },
                new Playlist { PlaylistID = 4, Name = "va", Private = true, UserID = 4, CanzonePlaylist = new List<CanzonePlaylist>() },
                new Playlist { PlaylistID = 5, Name = "?", Private = false, UserID = 5, CanzonePlaylist = new List<CanzonePlaylist>() }
                );
            _ContextTest.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {

                _connection.Close();

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Non modificare questo codice. Inserire il codice di pulizia nel metodo 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
    public class UnitTest2 : IClassFixture<Playlist_fixture>
    {
        private static Playlist_fixture _fixture;
        public UnitTest2(Playlist_fixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public void CreatePlaylist()
        {
            var x = new PlaylistRepository(_fixture._ContextTest);
            //Arrange

            API.Models.DTOS.Requests.CreatePlaylistRequest playlist = new API.Models.DTOS.Requests.CreatePlaylistRequest();
            playlist.PlayListTitle = "Playlist nuova";
            playlist.UserId = 2;
            playlist.Private = true;

            //Act
            var result = x.CreatePlaylist(playlist);

            //Assert   
            Assert.True(result.Result.Success);

        }
        [Fact]
        public void GetPlaylist()
        {
            //Arrange

            var x = new PlaylistRepository(_fixture._ContextTest);


            //Act
            var result = x.GetPlaylist();

            //Assert   
            Assert.Equal(5, result.Result.Count());

        }
        [Fact]
        public void GetUserPlaylist()
        {
            //Arrange

            var x = new PlaylistRepository(_fixture._ContextTest);


            //Act
            var result = x.GetUserPlaylist(1);

            //Assert   
            Assert.True(result.Result.Success);

        }
        [Fact]
        public void DeletePlaylist()
        {
            //Arrange
            var x = new PlaylistRepository(_fixture._ContextTest);
            

            //Act
            var result = x.DeletePlaylist(2);

            //Assert   
            Assert.True(result.Result.Success);

        }
        //[Fact]
        //public void AddCanzoneToPlaylist()
        //{
        //    //Non si può testare
        //    var x = new PlaylistRepository(_fixture._ContextTest);

        //    //Arrange

        //    API.Models.DTOS.Requests.AddCanzoneToPlaylistRequest playlist = new API.Models.DTOS.Requests.AddCanzoneToPlaylistRequest();
        //    playlist.idCanzone = 2;
        //    playlist.IdPlaylist = 3;

        //    //Act
        //    var result = x.AddCanzoneToPlaylist(playlist);

        //    //Assert   
        //    Assert.True(result.Result.Success);

        //}

    }
}
