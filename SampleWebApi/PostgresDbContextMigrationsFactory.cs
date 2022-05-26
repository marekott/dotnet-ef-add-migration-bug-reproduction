using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace SampleWebApi
{
    public class PostgresDbContextMigrationsFactory : IDesignTimeDbContextFactory<SampleWebApiContext>
    {
        public SampleWebApiContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SampleWebApiContext>();

            var connectionString = new NpgsqlConnectionStringBuilder
            {
                Host = "127.0.0.1",
                Port = 5432,
                Database = "SampleWebApiDatabase"
            }.ConnectionString;

            builder.UseNpgsql(connectionString);

            return Activator.CreateInstance(typeof(SampleWebApiContext), builder.Options) as SampleWebApiContext;
        }
    }
}
