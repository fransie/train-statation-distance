# Train Station Distance

This ASP.NET Core Web Api calculates the air-line distance between German intercity train stations. The locations of
the train stations are provided by Deutsche Bahn: https://data.deutschebahn.com/dataset/data-haltestellen.html.
This service uses the 2020 dataset.

The air-line distance is calculated as the great-circle distance between two points on a sphere ([Haversine formula](https://en.wikipedia.org/wiki/Haversine_formula)).

## Build and run

```
git clone https://github.com/fransie/train-statation-distance.git
dotnet build
dotnet run --project Api
```

The service will be available at https://localhost:5001/swagger/index.html.