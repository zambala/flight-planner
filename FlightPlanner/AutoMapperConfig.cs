using AutoMapper;
using FlightPlanner.API.Models;
using FlightPlanner.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.API
{
    public static class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddAirportRequest, Airport>()
                    .ForMember(d => d.AirportCode, opt => opt.MapFrom(s => s.Airport))
                    .ForMember(d => d.Id, opt => opt.Ignore());
                cfg.CreateMap<Airport, AddAirportRequest>()
                    .ForMember(d => d.Airport, opt => opt.MapFrom(s => s.AirportCode));
                cfg.CreateMap<Airport, AirportSearchResult>()
                    .ForMember(d => d.Airport, opt => opt.MapFrom(s => s.AirportCode))
                    .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
                    .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country));
                cfg.CreateMap<Flight, FlightSearchResult>()
                    .ForMember(d => d.From, opt => opt.MapFrom(src => src.From))
                    .ForMember(d => d.To, opt => opt.MapFrom(src => src.To));
                cfg.CreateMap<Flight, FlightSearchResult>();
                cfg.CreateMap<PageResult, PagedFlightSearchResult>()
                    .ForMember(d => d.Items, opts => opts.MapFrom(src => src.Items));

                cfg.CreateMap<AddFlightRequest, Flight>();
                cfg.CreateMap<Flight, AddFlightRequest>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}