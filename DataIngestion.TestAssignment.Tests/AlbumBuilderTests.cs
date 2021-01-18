using DataIngestion.TestAssignment.Interfaces;
using DataIngestion.TestAssignment.Model.Dtos;
using DataIngestion.TestAssignment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Tests
{
    [TestClass]
    public class AlbumBuilderTests
    {
        IAlbumBuilder sut;

        //TestData - normally, I might create a separate testData file/project 
        long collectionId1 = 1;
        long collectionId2 = 2;
        long artistId1 = 11;
        long artistId2 = 22;
        long artistId3 = 33;
        long artistId4 = 44;
        long upc1 = 111;
        long upc2 = 222;
        string collectionName1 = "collectionName1";
        string collectionName2 = "collectionName2";
        string artistName1 = "artistName11";
        string artistName2 = "artistName22";
        string artistName3 = "artistName33";
        string artistName4 = "artistName44";
        CollectionDto collection1;
        CollectionDto collection2;
        ArtistDto artist1;
        ArtistDto artist2;
        ArtistDto artist3;
        ArtistDto artist4;
        CollectionMatchDto collectionMatch1;
        CollectionMatchDto collectionMatch2;
        ArtistCollectionDto artistCollection1;
        ArtistCollectionDto artistCollection2;
        ArtistCollectionDto artistCollection3;
        ArtistCollectionDto artistCollection4;
        ArtistCollectionDto artistCollection5;
        List<CollectionDto> collectionDtos;
        List<ArtistDto> artistDtos;
        List<CollectionMatchDto> collectionMatchDtos;
        List<ArtistCollectionDto> artistCollectionDtos;

        [TestInitialize]
        public void Initilise()
        {
            //TestData - normally, I might create a separate testData file/project 
            this.collection1 = new CollectionDto
            {
                CollectionId = this.collectionId1,
                Name = this.collectionName1,
                ViewUrl = "",
                ArtworkUrl = "",
                OriginalReleaseDate = DateTime.UtcNow,
                LabelStudio = "",
                IsCompilation = false
            };
            this.collection2 = new CollectionDto
            {
                CollectionId = this.collectionId2,
                Name = this.collectionName2,
                ViewUrl = "",
                ArtworkUrl = "",
                OriginalReleaseDate = DateTime.UtcNow,
                LabelStudio = "",
                IsCompilation = true
            };
            this.collectionMatch1 = new CollectionMatchDto { CollectionId = this.collectionId1, Upc = this.upc1 };
            this.collectionMatch2 = new CollectionMatchDto { CollectionId = this.collectionId2, Upc = this.upc2 };
            this.artistCollection1 = new ArtistCollectionDto { ArtistId = this.artistId1, CollectionId = this.collectionId1 };
            this.artistCollection2 = new ArtistCollectionDto { ArtistId = this.artistId2, CollectionId = this.collectionId1 };
            this.artistCollection3 = new ArtistCollectionDto { ArtistId = this.artistId2, CollectionId = this.collectionId2 };
            this.artistCollection4 = new ArtistCollectionDto { ArtistId = this.artistId3, CollectionId = this.collectionId2 };
            this.artistCollection5 = new ArtistCollectionDto { ArtistId = this.artistId4, CollectionId = this.collectionId2 };
            this.artist1 = new ArtistDto { ArtistId = this.artistId1, Name = this.artistName1 };
            this.artist2 = new ArtistDto { ArtistId = this.artistId2, Name = this.artistName2 };
            this.artist3 = new ArtistDto { ArtistId = this.artistId3, Name = this.artistName3 };
            this.artist4 = new ArtistDto { ArtistId = this.artistId4, Name = this.artistName4 };
            this.collectionDtos = new List<CollectionDto> { this.collection1, this.collection2 };
            this.artistDtos = new List<ArtistDto> { this.artist1, this.artist2, this.artist3, this.artist4 };
            this.artistCollectionDtos = new List<ArtistCollectionDto> {
                this.artistCollection1,
                this.artistCollection2,
                this.artistCollection3,
                this.artistCollection4,
                this.artistCollection5
            };
            this.collectionMatchDtos = new List<CollectionMatchDto> { this.collectionMatch1, this.collectionMatch2 };
            this.sut = new AlbumBuilder();
        }

        [TestMethod]
        public async Task LoadFromDtos_Builds_Albums()
        {
            //Arrange

            //Act
            var result = await this.sut.LoadFromDtos(this.artistDtos, this.collectionDtos, this.artistCollectionDtos, this.collectionMatchDtos);

            //Assert result
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            Assert.AreEqual(result.Count, 2);

            //Assert Album details
            var album1 = result.First();
            var album2 = result.Last();
            Assert.AreEqual(album1.Id, this.collectionId1);
            Assert.AreEqual(album1.Upc, this.upc1);
            Assert.AreEqual(album1.Name, this.collectionName1);
            Assert.AreEqual(album2.Id, this.collectionId2);
            Assert.AreEqual(album2.Upc, this.upc2);
            Assert.AreEqual(album2.Name, this.collectionName2);

            //Assert Artist details
            var album1Artists = album1.Artists;
            var album2Artists = album2.Artists;
            Assert.AreEqual(album1Artists.Count, 2);
            Assert.AreEqual(album2Artists.Count, 3);
            Assert.AreEqual(album1Artists.First().Id, this.artistId1);
            Assert.AreEqual(album2Artists.Last().Id, this.artistId4);
        }
    }
}
