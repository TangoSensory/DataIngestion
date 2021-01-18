using DataIngestion.TestAssignment.Interfaces;
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
    /// A wrapper for Bzip Decompression
    /// </summary>
    public class BzipWrapper : IBzipWrapper
    {
        public async Task<byte[]> Decompress(byte[] compressedBytes)
        {
            if (compressedBytes?.Any() != true)
            {
                return null;
            }

            byte[] result = null;
            using (MemoryStream compressedStream = new MemoryStream(compressedBytes))
            {
                using (MemoryStream decompressedStream = new MemoryStream())
                {
                    try
                    {
                        // Ideally would want something async here
                        BZip2.Decompress(compressedStream, decompressedStream, true);

                        result = decompressedStream.ToArray();
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        Console.WriteLine(ex.Message);
                        throw ex;
                    }
                }
            }

            return result;
        }
    }
}
