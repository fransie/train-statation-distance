using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Model;
using BusinessLogic.Service;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;

namespace DataAccess;

public class TrainStationRepository : ITrainStationRepository
{
    private readonly ILogger<ITrainStationRepository> _logger;
    private readonly IReadOnlyCollection<TrainStation> _trainStations;

    public TrainStationRepository(ILogger<ITrainStationRepository> logger)
    {
        _logger = logger;
        _trainStations = GetTrainStations();
    }

    public IReadOnlyCollection<TrainStation> GetAll()
    {
        return _trainStations;
    }

    public TrainStation GetByDs100Code(string ds100Code)
    {
        var trainStation = _trainStations.SingleOrDefault(station => station.Ds100Code == ds100Code);

        if (trainStation is null)
        {
            throw new ArgumentException($"The DS100Code {ds100Code} is invalid.");
        }

        return trainStation;
    }

    private IReadOnlyCollection<TrainStation> GetTrainStations()
    {
        var trainStations = new List<TrainStation>();

        using var parser = GetParser();
        
        // skip header line
        parser.ReadLine();

        while (!parser.EndOfData)
        {
            var trainStationRow = parser.ReadFields();
            var trainStation = ParseRow(trainStationRow);
            trainStations.Add(trainStation);
        }

        return trainStations;
    }

    private static TextFieldParser GetParser()
    {
        var path = $"{CsvConfig.Directory}/{CsvConfig.FileName}";
        var parser = new TextFieldParser(path);
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(CsvConfig.Delimiter);
        return parser;
    }

    private TrainStation ParseRow(string[] trainStationRow)
    {
        if (trainStationRow is null)
        {
            _logger.LogError($"Null row while parsing csv file.");
            throw new MalformedLineException($"Null row while parsing csv file.");
        }

        return new TrainStation
        {
            Ds100Code = trainStationRow![CsvConfig.Ds100CodeColumn],
            Name = trainStationRow[CsvConfig.NameColumn],
            Longitude = double.Parse(trainStationRow[CsvConfig.LongitudeColumn]),
            Latitude = double.Parse(trainStationRow[CsvConfig.LatitudeColumn])
        };
    }
}