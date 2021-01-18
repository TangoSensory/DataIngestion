using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Interfaces
{
    public interface IAlbumWorkflow
    {
        Task<bool> Run();
    }
}
