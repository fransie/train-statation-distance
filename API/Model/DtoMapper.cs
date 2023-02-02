using AutoMapper;
using BusinessLogic.Model;

namespace TrainStationDistance.Model;

public class DtoMapper
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