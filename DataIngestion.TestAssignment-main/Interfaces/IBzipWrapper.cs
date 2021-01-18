using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Interfaces
{
    public interface IBzipWrapper
    {
        Task<byte[]> Decompress(byte[] compressedBytes);
    }
}
