using DataIngestion.TestAssignment.Interfaces;
using DataIngestion.TestAssignment.Model.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Workflows
{
    /// <summary>
    /// Manages the E2E workflow for Data download, ETL, Album creation &amp; Upload to the Search repo
    /// </summary>
    public class AlbumWorkflow : IAlbumWorkflow
    {
        ISourceRepo sourceRepo;
        ISearchRepo searchRepo;
        IAlbumBuilder albumBuilder;

        public AlbumWorkflow(ISourceRepo sourceRepo, ISearchRepo searchRepo, IAlbumBuilder albumBuilder)
        {
            this.sourceRepo = sourceRepo;
            this.searchRepo = searchRepo;
            this.albumBuilder = albumBuilder;
        }

        public async Task<bool> Run()
        {
            var isSuccess = false;
            try
            {
                //Retrieve Data
                var collectionDtos = await this.sourceRepo.RetrieveCollectionDtos();
                //Store Data in LocalRepo - original design was just to work in-memory (to speed up dev time), but data too big 

                var collectionMatchDtos = await this.sourceRepo.RetrieveCollectionMatchDtos();
                //Store Data in LocalRepo - original design was just to work in-memory (to speed up dev time), but data too big 

                var artistDtos = await this.sourceRepo.RetrieveArtistDtos();
                //Store Data in LocalRepo - original design was just to work in-memory (to speed up dev time), but data too big 

                var artistCollectionDtos = await this.sourceRepo.RetrieveArtistCollectionDtos();
                //Store Data in LocalRepo - original design was just to work in-memory (to speed up dev time), but data too big 

                //Build Albums
                //This approach will blow memory for such large collections, I think. 
                //Would need to shift to SQL / DB query from LocalRepo approach
                var albums = await this.albumBuilder.LoadFromDtos(artistDtos, collectionDtos, artistCollectionDtos, collectionMatchDtos);

                //Push Albums
                isSuccess = await this.searchRepo.PushCollection(albums);
            }
            catch (Exception ex)
            {
                //Log error
                Console.WriteLine(ex.Message);
            }

            return isSuccess;
        }
    }
}
