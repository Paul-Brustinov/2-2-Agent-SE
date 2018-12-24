using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2x2_Agent.Repo.Task
{
    public static class SpExecuter
    {
        public static int RunAlias(string spAlias, string connstringName="Default")
        {
            //int res = 0;

            var reader = new AppSettingsReader();
            string spName = reader.GetValue(spAlias, typeof(string)).ToString();

            string connectionString;
            try
            {
                connectionString = GetConnectionString(connstringName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

            return Run(spName, connectionString);
        }


        public static int Run(string spName, string connectionString)
        {
            int res = 0;

            var cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                res = cmd.ExecuteNonQuery();
                Console.WriteLine("Operation done!");
                cnn.Close();
            }
            catch (Exception ex)
            {
                res = -1;
                Console.WriteLine(ex.Message);
                throw;
            }
            return res;
        }

        private static string GetConnectionString(string connstringName)
        {
            // Look for the name in the connectionStrings section.
            var settings = ConfigurationManager.ConnectionStrings[connstringName];

            // If found, return the connection string.
            if (settings != null)
                return settings.ConnectionString;

            throw new Exception($"Connection string \"{connstringName}\" not found! Please edit 2x2-Agent.StartSp.exe.config");
        }
    }
}
