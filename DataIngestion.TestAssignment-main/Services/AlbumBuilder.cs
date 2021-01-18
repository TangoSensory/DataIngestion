using DataIngestion.TestAssignment.Interfaces;
using DataIngestion.TestAssignment.Model.Dtos;
using DataIngestion.TestAssignment.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Services
{
    public class AlbumBuilder : IAlbumBuilder
    {
        public async Task<IList<Album>> LoadFromDtos(IList<ArtistDto> artistDtos, IList<CollectionDto> collectionDtos, IList<ArtistCollectionDto> artistCollectionDtos, IList<CollectionMatchDto> collectionMatchDtos)
        {
            var albums = new List<Album>();
            foreach (CollectionDto coll in collectionDtos)
            {
                long upc = default;
                try
                {
                    upc = collectionMatchDtos.SingleOrDefault(x => x.CollectionId == coll.CollectionId).Upc;
                }
                catch (Exception ex)
                {
                    // log error but don't take further action??
                }

                var artistIds = artistCollectionDtos.Where(x => x.CollectionId == coll.CollectionId).Select(x => x.ArtistId);
                var artists = artistDtos.Where(x => artistIds.Contains(x.ArtistId)).Select(x => new Artist { Id = x.ArtistId, Name = x.Name }).ToList();
                var album = new Album
                {
                    Id = coll.CollectionId,
                    Name = coll.Name,
                    Url = coll.ViewUrl,
                    Upc = upc,
                    ReleaseDate = coll.OriginalReleaseDate,
                    IsCompilation = coll.IsCompilation,
                    Label = coll.LabelStudio,
                    ImageUrl = coll.ArtworkUrl,
                    Artists = artists
                };

                albums.Add(album);
            }

            return albums;
        }
    }
}
