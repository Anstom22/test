using AutoMapper;
using Sample1.Models.Domain;
using Sample1.Models.DTO;
using System.Runtime.CompilerServices;

namespace Sample1.Mappings
{
    public class AutomapperProfiles:Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Region,RegionDTO>().ReverseMap();
            CreateMap<AddRegionDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionDTO,Region>().ReverseMap();
            CreateMap<AddWalkDTO,Walk>().ReverseMap();
            CreateMap<Walk,WalkDTO>().ReverseMap();
            CreateMap<UpdatewalkDTO, Walk>().ReverseMap();
            CreateMap<Difficulty,DifficultyDTO>().ReverseMap();
        }
    }
}
