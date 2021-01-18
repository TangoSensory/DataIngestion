using DataIngestion.TestAssignment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Repositories
{
    /// <summary>
    /// Need target API details to complete this
    /// </summary>
    public class SearchRepo : ISearchRepo
    {
        IRestClient restClient;

        public SearchRepo(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<bool> PushCollection<T>(IList<T> collection) where T : class
        {
            if (collection?.Any() != true)
            {
                return false;
            }

            //Need target API details 
            throw new NotImplementedException();
        }

        public async Task<T> PushSingle<T>(T item) where T : class
        {
            if (item == null)
            {
                return null;
            }

            //Need target API details 
            throw new NotImplementedException();
        }
    }
}
