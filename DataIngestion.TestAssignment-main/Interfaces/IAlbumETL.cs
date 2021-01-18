using DataIngestion.TestAssignment.Model.Dtos;
using DataIngestion.TestAssignment.Model.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Interfaces
{
    public interface IAlbumETL
    {
        Task<IList<ArtistDto>> ExtractArtistDtosFromRaw(byte[] raw);
        Task<IList<CollectionDto>> ExtractCollectionDtosFromRaw(byte[] raw);
        Task<IList<ArtistCollectionDto>> ExtractArtistCollectionDtosFromRaw(byte[] raw);
        Task<IList<CollectionMatchDto>> ExtractCollectionMatchDtosFromRaw(byte[] raw);
    }
}
