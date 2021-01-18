using DataIngestion.TestAssignment.Interfaces;
using DataIngestion.TestAssignment.Workflows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Assert = NUnit.Framework.Assert;

namespace DataIngestion.TestAssignment.Tests
{
    [TestClass]
    public class AlbumWorkflowTests
    {
        IAlbumWorkflow sut;
        Mock<ISourceRepo> mockSourceRepo;
        Mock<ISearchRepo> mockSearchRepo;
        Mock<IAlbumBuilder> mockAlbumBuiler;

        [TestInitialize]
        public void Initilise()
        {
            this.mockSourceRepo = new Mock<ISourceRepo>();
            this.mockSearchRepo = new Mock<ISearchRepo>();
            this.mockAlbumBuiler = new Mock<IAlbumBuilder>();

            this.sut = new AlbumWorkflow(mockSourceRepo.Object, mockSearchRepo.Object, mockAlbumBuiler.Object);
        }

        [TestMethod]
        public void Run_Invokes_SoureRepo_RetrieveArtistDtos()
        {
            //Arrange

            //Act
            this.sut.Run();

            //Assert
            mockSourceRepo.Verify(x => x.RetrieveArtistDtos(), Times.Once());
        }
    }
}
