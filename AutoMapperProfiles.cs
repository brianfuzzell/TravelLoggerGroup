using AutoMapper;
using TravelLoggerGroup.Models;
using TravelLoggerGroup.Models.DTOs;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, UserDTO>();
        CreateMap<City, CityDTO>();
        CreateMap<Log, LogDTO>();
        CreateMap<Recommendation, RecommendationDTO>();
        CreateMap<UpVote, UpVoteDTO>();
    }
}