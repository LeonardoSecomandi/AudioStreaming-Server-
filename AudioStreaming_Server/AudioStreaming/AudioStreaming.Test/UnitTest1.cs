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
    public class Canzoni_fixture : IDisposable
    {
          
        private bool disposedValue;
        private SqliteConnection _connection;
        public ApplicationDbContext _ContextTest;

        public Canzoni_fixture()
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


            _ContextTest.Canzoni.AddRange(
                new Canzone { SongID = 1, SongTitle = "ciao" , AlbumName = "Marco", IDUserUploader = 1, DownnloadNumber = 1, Duration=1 },
                new Canzone { SongID = 2, SongTitle = "bella", AlbumName = "Giorgio", IDUserUploader = 2, DownnloadNumber = 2, Duration = 2 },
                new Canzone { SongID = 3, SongTitle = "come", AlbumName = "Signo", IDUserUploader = 3, DownnloadNumber = 3, Duration = 3 },
                new Canzone { SongID = 4, SongTitle = "va", AlbumName = "Ghilo", IDUserUploader = 4, DownnloadNumber = 4, Duration = 4 },
                new Canzone { SongID = 5, SongTitle = "?", AlbumName = "Tudo", IDUserUploader = 5, DownnloadNumber = 5, Duration = 5 }
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
    public class UnitTest1 :IClassFixture<Canzoni_fixture>
    {
        private static Canzoni_fixture _fixture;
        public UnitTest1 (Canzoni_fixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public void AddCanzone()
        {
            var x = new CanzoniService(_fixture._ContextTest);
            //Arrange
            
            API.Models.DTOS.Requests.CreateCanzoneRequest canzone = new API.Models.DTOS.Requests.CreateCanzoneRequest();
            canzone.SongTitle = "Canzone 1";
            canzone.AlbumName = "Album 1";
            canzone.IDUserUploader = 1;
            canzone.Duration = 30;

            //Act
            var result =  x.AddCanzone(canzone);

            //Assert   
            Assert.True(result.Result.Success);

        }
        [Fact]
        public void GetCanzoni()
        {
            //Arrange

            CanzoniService canzoniService = new CanzoniService(_fixture._ContextTest);



            //Act

            var result =  canzoniService.GetCanzoni();
            


            //Assert
            Assert.Equal(5, result.Result.Count());
        }

        [Fact]
        public void Search()
        {
            //Arrange

            CanzoniService canzoniService = new CanzoniService(_fixture._ContextTest);
            AudioStreaming.API.Models.DTOS.Requests.SearchSongRequest search = new AudioStreaming.API.Models.DTOS.Requests.SearchSongRequest();
            search.Title = "ciao";
            search.Album = "Marco";


            //Act

            var result = canzoniService.Search(search);



            //Assert
            Assert.True(result.Result.Success);
        }

        [Fact]
        public void GetCanzone()
        {
            //Arrange

            CanzoniService canzoniService = new CanzoniService(_fixture._ContextTest);



            //Act

            var result = canzoniService.GetCanzone(2);



            //Assert
            Assert.True(result.Result.SongTitle == "bella" && 
                result.Result.AlbumName == "Giorgio" && 
                result.Result.IDUserUploader == 2 && 
                result.Result.Duration == 2 && 
                result.Result.DownnloadNumber == 2);
        }

        [Fact]
        public void DeleteCanzone()
        {
            //Arrange

            CanzoniService canzoniService = new CanzoniService(_fixture._ContextTest);



            //Act

            var result = canzoniService.DeleteCanzone(3);



            //Assert
            Assert.True(result.Result.Success);
        }

        [Fact]
        public void UpdateCanzone()
        {
            //Arrange

            CanzoniService canzoniService = new CanzoniService(_fixture._ContextTest);
            AudioStreaming.API.Models.DTOS.Requests.UpdateSongRequest update = new AudioStreaming.API.Models.DTOS.Requests.UpdateSongRequest();
            update.id = 4;
            update.SongTitle = "Caro amico";
            update.AlbumName = "Ti scrivo";
            update.Duration = 40;


            //Act

            var result = canzoniService.UpdateCanzone(update);



            //Assert
            Assert.True(result.Result.Success);
        }
        [Fact]
        public void ChangeDownloadNumber()
        {
            //Arrange

            CanzoniService canzoniService = new CanzoniService(_fixture._ContextTest);
            AudioStreaming.API.Models.DTOS.Requests.ChangeDownloadNumberReq changeDownload = new AudioStreaming.API.Models.DTOS.Requests.ChangeDownloadNumberReq();
            changeDownload.id = 5;
            changeDownload.Value = 13513;


            //Act

            var result = canzoniService.ChangeDownloadNumber(changeDownload);



            //Assert
            Assert.True(result.Result.Success);
        }
    }
}
