using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTech.Infrastructure.Extensions
{
    public static class DataBaseAccess
    {

        public static string GetDatabaseName(this IConfiguration configuration)
        {
            var databaseName = configuration.GetConnectionString("DatabaseName");

            return databaseName;
        }

        public static string GetDatabaseConnection(this IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Connection");

            return connection;
        }

        public static string GetFullConnection(this IConfiguration configuration)
        {
            var databaseName = configuration.GetConnectionString("DatabaseName");
            var connection = configuration.GetConnectionString("Connection");

            return $"{connection} Database={databaseName}";
        }
    }
}   
