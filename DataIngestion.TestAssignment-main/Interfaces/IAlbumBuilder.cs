using DataIngestion.TestAssignment.Model.Dtos;
using DataIngestion.TestAssignment.Model.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Interfaces
{
    public interface IAlbumBuilder
    {
        Task<IList<Album>> LoadFromDtos(IList<ArtistDto> artistDtos, IList<CollectionDto> collectionDtos, IList<ArtistCollectionDto> artistCollectionDtos, IList<CollectionMatchDto> collectionMatchDtos);
    }
}
