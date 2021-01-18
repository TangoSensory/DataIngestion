using DataIngestion.TestAssignment.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Interfaces
{
    public interface ISourceRepo
    {
        Task<IList<ArtistDto>> RetrieveArtistDtos();
        Task<IList<CollectionDto>> RetrieveCollectionDtos();
        Task<IList<ArtistCollectionDto>> RetrieveArtistCollectionDtos();
        Task<IList<CollectionMatchDto>> RetrieveCollectionMatchDtos();
    }
}
