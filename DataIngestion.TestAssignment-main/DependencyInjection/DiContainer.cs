using DataIngestion.TestAssignment.Clients;
using DataIngestion.TestAssignment.Interfaces;
using DataIngestion.TestAssignment.Repositories;
using DataIngestion.TestAssignment.Services;
using DataIngestion.TestAssignment.Workflows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataIngestion.TestAssignment.DependencyInjection
{
    public class DiContainer
    {
        public static ServiceProvider ServiceProvider = null;

        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBzipWrapper, BzipWrapper>();
            serviceCollection.AddTransient<IAlbumWorkflow, AlbumWorkflow>();
            serviceCollection.AddTransient<ISourceRepo, SourceRepo>();
            serviceCollection.AddTransient<ISearchRepo, SearchRepo>();
            serviceCollection.AddTransient<IAlbumBuilder, AlbumBuilder>();
            serviceCollection.AddTransient<IAlbumETL, AlbumETL>();

            // These could be singletons if heavily used
            serviceCollection.AddTransient<IGoogleFileClient, GoogleFileClient>();
            serviceCollection.AddTransient<IRestClient, RestClient>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
