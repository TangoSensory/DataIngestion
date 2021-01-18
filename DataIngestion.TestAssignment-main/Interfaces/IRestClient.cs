using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Interfaces
{
    public interface IRestClient
    {
        Task<T> Create<T>(string apiUrl, T item, JsonSerializerSettings jsonSerializerSettings = null);
    }
}
