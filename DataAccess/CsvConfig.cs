using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace DataAccess;

public static class CsvConfig
{
    public static readonly string Directory = GetDirectory();
    public const string FileName = "train_stations.csv";
    public const string Delimiter = ";";

    public const int Ds100CodeColumn = 1;
    public const int NameColumn = 3;
    public const int LongitudeColumn = 5;
    public const int LatitudeColumn = 6;

    // CallerFilePath gets the path of caller's file, which is this file here. It can only be used
    // as an attribute in a method call.
    private static string GetDirectory([CallerFilePath] string? callerFilePath = null)
    {
        var path = Path.GetDirectoryName(callerFilePath);
        if (path is null)
        {
            throw new ArgumentException("CallerFilePath could not be determined.");
        }
        return path;
    }
}