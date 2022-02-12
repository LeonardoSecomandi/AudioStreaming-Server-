using System;
using Xunit;
using AudioStreaming.API.Models;
using AudioStreaming.API.Data;

namespace AudioStreaming.Test
{
    public class UnitTest1
    {
        private ApplicationDbContext _context { get; set; }

        [Fact]
        public void AddCanzone()
        {
            CanzoniService canzoniService = new CanzoniService();

        }
    }
}
