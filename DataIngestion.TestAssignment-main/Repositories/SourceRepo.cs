using DataIngestion.TestAssignment.Infrastructure;
using DataIngestion.TestAssignment.Interfaces;
using DataIngestion.TestAssignment.Model.Dtos;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Repositories
{
    public class SourceRepo : ISourceRepo
    {
        IGoogleFileClient googleFileClient;
        IAlbumETL albumETL;
        IBzipWrapper bzipWrapper;
        string baseUrl;
        public SourceRepo(IGoogleFileClient googleFileClient, IAlbumETL albumETL, IBzipWrapper bzipWrapper)
        {
            this.googleFileClient = googleFileClient;
            this.albumETL = albumETL;
            this.bzipWrapper = bzipWrapper;
            this.baseUrl = GlobalConstants.GoogleDriveDirectDownloadUrlRoot;
        }

        public async Task<IList<ArtistCollectionDto>> RetrieveArtistCollectionDtos()
        {
            byte[] rawBytes = null;
            IList<ArtistCollectionDto> collection = null;

            //Fetch Data
            try
            {
                rawBytes = await this.googleFileClient.Retrieve($"{GlobalConstants.ArtistCollectionFileId}");
            }
            catch (Exception ex)
            {
                //Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            //Parse Data
            try
            {
                collection = await this.albumETL.ExtractArtistCollectionDtosFromRaw(rawBytes);
                rawBytes = null;
            }
            catch (Exception ex)
            {
                //Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return collection;
        }

        public async Task<IList<ArtistDto>> RetrieveArtistDtos()
        {
            byte[] rawBytes = null;
            IList<ArtistDto> collection = null;

            //Fetch Data
            try
            {
                rawBytes = await this.googleFileClient.Retrieve($"{GlobalConstants.ArtistFileId}");
            }
            catch (Exception ex)
            {
                //Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            //Parse Data
            try
            {
                collection = await this.albumETL.ExtractArtistDtosFromRaw(rawBytes);
                rawBytes = null;
            }
            catch (Exception ex)
            {
                //Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return collection;
        }

        public async Task<IList<CollectionDto>> RetrieveCollectionDtos()
        {
            byte[] rawBytes = null;
            IList<CollectionDto> collection = null;

            //Fetch Data
            try
            {
                rawBytes = await this.googleFileClient.Retrieve($"{GlobalConstants.CollectionFileId}");
            }
            catch (Exception ex)
            {
                //Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            //Parse Data
            try
            {
                collection = await this.albumETL.ExtractCollectionDtosFromRaw(rawBytes);
                rawBytes = null;
            }
            catch (Exception ex)
            {
                //Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return collection;
        }

        public async Task<IList<CollectionMatchDto>> RetrieveCollectionMatchDtos()
        {
            byte[] rawBytes = null;
            IList<CollectionMatchDto> collection = null;

            //Fetch Data
            try
            {
                rawBytes = await this.googleFileClient.Retrieve($"{GlobalConstants.CollectionMatchFileId}");
            }
            catch (Exception ex)
            {
                //Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            //Parse Data
            try
            {
                collection = await this.albumETL.ExtractCollectionMatchDtosFromRaw(rawBytes);
                rawBytes = null;
            }
            catch (Exception ex)
            {
                //Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return collection;
        }
    }
}
