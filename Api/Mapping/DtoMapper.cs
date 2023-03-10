using AutoMapper;
using BusinessLogic.Model;
using TrainStationDistance.Model;

namespace TrainStationDistance.Mapping;

public class DtoMapper : IDtoMapper
{
    private readonly IMapper _mapper;


    public DtoMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DistanceCalculation, DistanceCalculationDto>();
        });
        _mapper = config.CreateMapper();
    }

    public DistanceCalculationDto MapToDto(DistanceCalculation distanceCalculation)
    {
        return _mapper.Map<DistanceCalculationDto>(distanceCalculation);
    }
}