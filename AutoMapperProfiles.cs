using AutoMapper;
using TravelLoggerGroup.Models;
using TravelLoggerGroup.Models.DTOs;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
        CreateMap<City, CityDTO>();
        CreateMap<CityDTO, City>();
        CreateMap<Log, LogDTO>();
        CreateMap<LogDTO, Log>().ForMember(l => l.Id, opt => opt.Ignore());
        CreateMap<Recommendation, RecommendationDTO>();
        CreateMap<RecommendationDTO, Recommendation>();
        CreateMap<UpVote, UpVoteDTO>();
        CreateMap<UpVoteDTO, UpVote>();
        CreateMap<Recommendation, CityRecommendationDTO>();
        CreateMap<CityRecommendationDTO, Recommendation>();
    }
}