using AutoMapper;
using ChatApp.Application;
using ChatApp.Core.Entities;

namespace ChatApp.Infrastructure.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Message, MessageResponse>();
  }
}