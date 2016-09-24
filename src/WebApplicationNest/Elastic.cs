using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using WebApplicationNest.Models;

namespace WebApplicationNest
{
    public static class Elastic
    {
        private static IElasticClient GetClient()
        {
            return new ElasticClient(ConnectionSettings);
        }

        private static IndexNameResolver GetResolver()
        {
            return new IndexNameResolver(ConnectionSettings);
        }

        private static ConnectionSettings ConnectionSettings
        {
            get
            {
                var local = new Uri("http://localhost:9200");
                var settings = new ConnectionSettings(local)
                    .MapDefaultTypeIndices(m => m.Add(typeof(Ticket), "tickets"));
                return settings;
            }
        }

        public static void ConfigureElatic(this IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(IElasticClient), GetClient()));
            services.Add(new ServiceDescriptor(typeof(IndexNameResolver), GetResolver()));
        }
    }
}