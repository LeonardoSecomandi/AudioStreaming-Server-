using System;
using Xunit;
using AudioStreaming.API.Models;
using AudioStreaming.API.Data;
using AudioStreaming.Models;
using System.Collections.Generic;

namespace AudioStreaming.Test
{
    public class UnitTest1
    {
        private ApplicationDbContext _context { get; set; }

        [Fact]
        public void AddCanzone()
        {
            //Arrange
            CanzoniService canzoniService = new CanzoniService(_context);
            API.Models.DTOS.Requests.CreateCanzoneRequest canzone = new API.Models.DTOS.Requests.CreateCanzoneRequest();
            canzone.SongTitle = "Canzone 1";
            canzone.AlbumName = "Album 1";
            canzone.IDUserUploader = 1;
            canzone.Duration = 30;

            //Act
            var result = canzoniService.AddCanzone(canzone);

            //Assert   
            Assert.True(result.Result.Success);

        }
        [Fact]
        public void GetCanzoni()
        {
            //Arrange

            CanzoniService canzoniService = new CanzoniService(_context);
            API.Models.DTOS.Requests.CreateCanzoneRequest canzone = new API.Models.DTOS.Requests.CreateCanzoneRequest();
            canzone.SongTitle = "Canzone 1";
            canzone.AlbumName = "Album 1";
            canzone.IDUserUploader = 1;
            canzone.Duration = 30;


            //Act
           canzoniService.AddCanzone(canzone).Wait();

           var result =  canzoniService.GetCanzoni().Result.GetEnumerator() as List<CanzoniService>;


            //Assert
            Assert.True(result.Count.Equals(1));
        }
    }
}
