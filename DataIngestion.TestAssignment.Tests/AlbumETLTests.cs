using DataIngestion.TestAssignment.Interfaces;
using DataIngestion.TestAssignment.Model.Dtos;
using DataIngestion.TestAssignment.Services;
using DataIngestion.TestAssignment.Workflows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace DataIngestion.TestAssignment.Tests
{
    [TestClass]
    public class AlbumETLTests
    {
        IAlbumETL sut;
        Mock<IBzipWrapper> mockBzipWrapper;
        string testArtistTableExport = @"#export_dateartist_idnameis_actual_artistview_urlartist_type_id
#primaryKey:artist_id
#dbTypes:BIGINTBIGINTVARCHAR(1000)BOOLEANVARCHAR(1000)INTEGER
199999313091Walter Melrose1http://ms.com/artist/walter-melrose/id313091?uo=51
199999313101Charles Luke1http://ms.com/artist/charles-luke/id313101?uo=51
199999313109Santo Pecora1http://ms.com/artist/santo-pecora/id313109?uo=51
199999313592Susanne Norin1http://ms.com/artist/susanne-norin/id313592?uo=51
199999313605Gundula Anders1http://ms.com/artist/gundula-anders/id313605?uo=51";

        [TestInitialize]
        public void Initilise()
        {
            this.mockBzipWrapper = new Mock<IBzipWrapper>();

            this.sut = new AlbumETL(this.mockBzipWrapper.Object);
        }

        [TestMethod]
        public async Task ExtractArtistDtosFromRaw_Empty_Input_Returns_Null()
        {
            //Arrange - empty array
            byte[] testArchive = null;

            //Act
            var result = await this.sut.ExtractArtistDtosFromRaw(testArchive);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task ExtractArtistDtosFromRaw_Null_Input_Returns_Null()
        {
            //Arrange - empty array
            byte[] testArchive = new byte[0];

            //Act
            var result = await this.sut.ExtractArtistDtosFromRaw(testArchive);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task ExtractArtistDtosFromRaw_Valid_Input_Returns_Dtos()
        {
            //Arrange 
            byte[] testArchive = new byte[] { 1, 2, 3 }; // this will be ignored

            //Arrange - would need a test array built from valid Artist table entries
            byte[] testUncompressedArray = Encoding.ASCII.GetBytes(this.testArtistTableExport);

            this.mockBzipWrapper.Setup(x => x.Decompress(testArchive)).ReturnsAsync(testUncompressedArray);
            this.sut = new AlbumETL(this.mockBzipWrapper.Object);

            //Act
            var result = await this.sut.ExtractArtistDtosFromRaw(testArchive);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is IList<ArtistDto>);
            Assert.IsTrue(result.Any());
            Assert.AreEqual(result.Count, 5);
        }

    }
}
