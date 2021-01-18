using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Interfaces
{
    public interface IGoogleFileClient
    {
        Task<byte[]> Retrieve(string fileId);
    }
}
