using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace TTMTool
{
    public class db
    {
        SqlConnection con;
        public static SqlConnection Gdb()
        {
            IConfigurationRoot configuration = db.GetConfiguration();
            SqlConnection con = new SqlConnection(configuration.GetSection("Data").GetSection("ConnectionString").Value);
            return con;
        }
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
