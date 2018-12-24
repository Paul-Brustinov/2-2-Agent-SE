using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _2x2_Agent.StartSp
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine(DateTime.Now.DayOfWeek.CompareTo(DayOfWeek.Saturday));

            var reader = new AppSettingsReader();

            if (args.Length < 1)
            {
                throw new Exception("Please pass command to command line! For example \"MakeFullBackup\". Command list is setted in 2x2-Agent.StartSp.exe.config");
            }

            var procName = reader.GetValue(args[0], typeof(string)).ToString();

            var connstringName = "Default";
            if (args.Length > 1 && args[1] != "") connstringName = args[1];

            string connetionString;
            try
            {
                connetionString = GetConnectionString(connstringName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine("Going to execute sp {0}", procName);

            

            var cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                Console.WriteLine("Operation done!");
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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
