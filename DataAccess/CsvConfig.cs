using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace DataAccess;

public static class CsvConfig
{
    public static string Directory = GetDirectory();
    public static string FileName = "train_stations.csv";
    public static string Delimiter = ";";

    public static int Ds100CodeColumn = 1;
    public static int NameColumn = 3;
    public static int LongitudeColumn = 5;
    public static int LatitudeColumn = 6;

    // CallerFilePath gets the path of caller's file, which is this file here. It can only be used
    // as an attribute in a method call.
    private static string GetDirectory([CallerFilePath] string callerFilePath = null)
    {
        if (callerFilePath is null)
        {
            throw new NullReferenceException("CallerFilePath could not be determined.");
        }
        return Path.GetDirectoryName(callerFilePath);
    }
}