using DataIngestion.TestAssignment.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Interfaces
{
    public interface ISearchRepo
    {
        Task<T> PushSingle<T>(T item) where T : class;
        Task<bool> PushCollection<T>(IList<T> collection) where T : class;
    }
}
