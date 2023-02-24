using System;
using System.IO;

namespace SQLiteFunction.Models;

public class Utils
{
    const string DevEnvValue = "Development";
    const string DBPath = "./school.db";
    public const string Azure_DBPath = "D:/home/school.db";
    public static string GetSQLiteConnectionString()
    {
        bool isDevEnv = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == DevEnvValue ? true : false;
        if (!isDevEnv && !File.Exists(Azure_DBPath))
        {
            CopyDb();
        }

        //var home = Environment.GetEnvironmentVariable("HOME") ?? "";
        //Console.WriteLine($"home: {home}");
        //if (!string.IsNullOrEmpty(home))
        //{
        //    home = System.IO.Path.Combine(home, "site", "wwwroot");
        //}
        //var databasePath = System.IO.Path.Combine(home, "school.db");
        //var connStr = $"Data Source={databasePath}";

        var connStr = $"Data Source={(isDevEnv ? DBPath : Azure_DBPath)};";

        return connStr;
    }

    private static void CopyDb()
    {
        File.Copy(DBPath, Azure_DBPath);
        File.SetAttributes(Azure_DBPath, FileAttributes.Normal);
    }
}

