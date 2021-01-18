using DataIngestion.TestAssignment.Interfaces;
using DataIngestion.TestAssignment.Model.Dtos;
using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Services
{
    /// <summary>
    /// Extract, Transform &amp; Load data relating to Albums
    /// </summary>
    public class AlbumETL : IAlbumETL
    {
        IBzipWrapper bzipWrapper;

        public AlbumETL(IBzipWrapper bzipWrapper)
        {
            this.bzipWrapper = bzipWrapper;
        }

        public async Task<IList<ArtistCollectionDto>> ExtractArtistCollectionDtosFromRaw(byte[] compressedBytes)
        {
            if (compressedBytes?.Any() != true)
            {
                return null;
            }

            var result = new List<ArtistCollectionDto>();
            byte[] decompressedBytes = null;
            try
            {
                decompressedBytes = await this.bzipWrapper.Decompress(compressedBytes);
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            try
            {
                using (MemoryStream parseStream = new MemoryStream(decompressedBytes))
                {
                    using (TextReader textReader = new StreamReader(parseStream))
                    {
                        string line;
                        while ((line = textReader.ReadLine()) != null)
                        {
                            if (line.StartsWith("#") || line.StartsWith("tar"))
                            {
                                continue;
                            }

                            var data = line.Split(new char[] { (char)1, (char)2 });
                            if (data.Length < 3)
                            {
                                continue;
                            }

                            long artist_id;
                            if (!long.TryParse(data[1], out artist_id))
                            {
                                continue;
                            }

                            long collection_id;
                            if (!long.TryParse(data[2], out collection_id))
                            {
                                continue;
                            }

                            var dto = new ArtistCollectionDto { ArtistId = artist_id, CollectionId = collection_id };

                            result.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return result;
        }

        public async Task<IList<ArtistDto>> ExtractArtistDtosFromRaw(byte[] compressedBytes)
        {
            if (compressedBytes?.Any() != true)
            {
                return null;
            }

            var result = new List<ArtistDto>();
            byte[] decompressedBytes = null;
            try
            {
                decompressedBytes = await this.bzipWrapper.Decompress(compressedBytes);
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }


            try
            {
                using (MemoryStream parseStream = new MemoryStream(decompressedBytes))
                {
                    using (TextReader textReader = new StreamReader(parseStream))
                    {
                        string line;
                        while ((line = textReader.ReadLine()) != null)
                        {
                            if (line.StartsWith("#") || line.StartsWith("tar"))
                            {
                                continue;
                            }

                            var data = line.Split(new char[] { (char)1, (char)2 });
                            if (data.Length < 3)
                            {
                                continue;
                            }

                            long artist_id;
                            if (!long.TryParse(data[1], out artist_id))
                            {
                                continue;
                            }

                            var dto = new ArtistDto { ArtistId = artist_id, Name = data[2] };

                            result.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return result;
        }

        public async Task<IList<CollectionDto>> ExtractCollectionDtosFromRaw(byte[] compressedBytes)
        {
            if (compressedBytes?.Any() != true)
            {
                return null;
            }

            var result = new List<CollectionDto>();
            byte[] decompressedBytes = null;
            try
            {
                decompressedBytes = await this.bzipWrapper.Decompress(compressedBytes);
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            try
            {
                using (MemoryStream parseStream = new MemoryStream(decompressedBytes))
                {
                    using (TextReader textReader = new StreamReader(parseStream))
                    {
                        string line;
                        while ((line = textReader.ReadLine()) != null)
                        {
                            if (line.StartsWith("#") || line.StartsWith("tar"))
                            {
                                continue;
                            }

                            var data = line.Split(new char[] { (char)1, (char)2 });
                            if (data.Length < 17)
                            {
                                continue;
                            }

                            long collection_id;
                            if (!long.TryParse(data[1], out collection_id))
                            {
                                continue;
                            }

                            long upc;
                            if (!long.TryParse(data[2], out upc))
                            {
                                continue;
                            }

                            DateTime releaseDate;
                            if (!DateTime.TryParse(data[9], out releaseDate))
                            {
                                continue;
                            }

                            bool isCompilation;
                            if (!bool.TryParse(data[16], out isCompilation))
                            {
                                continue;
                            }

                            var dto = new CollectionDto
                            {
                                CollectionId = collection_id,
                                Name = data[2],
                                ViewUrl = data[7],
                                ArtworkUrl = data[8],
                                OriginalReleaseDate = releaseDate,
                                LabelStudio = data[11],
                                IsCompilation = isCompilation
                            };

                            result.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return result;
        }

        public async Task<IList<CollectionMatchDto>> ExtractCollectionMatchDtosFromRaw(byte[] compressedBytes)
        {
            if (compressedBytes?.Any() != true)
            {
                return null;
            }

            var result = new List<CollectionMatchDto>();
            byte[] decompressedBytes = null;
            try
            {
                decompressedBytes = await this.bzipWrapper.Decompress(compressedBytes);
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            try
            {
                using (MemoryStream parseStream = new MemoryStream(decompressedBytes))
                {
                    using (TextReader textReader = new StreamReader(parseStream))
                    {
                        string line;
                        while ((line = textReader.ReadLine()) != null)
                        {
                            if (line.StartsWith("#") || line.StartsWith("tar"))
                            {
                                continue;
                            }

                            var data = line.Split(new char[] { (char)1, (char)2 });
                            if (data.Length < 3)
                            {
                                continue;
                            }

                            long collection_id;
                            if (!long.TryParse(data[1], out collection_id))
                            {
                                continue;
                            }

                            long upc;
                            if (!long.TryParse(data[2], out upc))
                            {
                                continue;
                            }

                            var dto = new CollectionMatchDto { CollectionId = collection_id, Upc = upc };

                            result.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return result;
        }
    }
}
