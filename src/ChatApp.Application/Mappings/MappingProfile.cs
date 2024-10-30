using AutoMapper;
using ChatApp.Core.DTOs.Chats;
using ChatApp.Core.DTOs.Messages;
using ChatApp.Core.DTOs.Users;
using ChatApp.Core.Entities;

namespace ChatApp.Application.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Message, MessageResponse>();
    CreateMap<Message, CreateMessageResponse>();
    CreateMap<CreateMessageRequest, Message>();
    CreateMap<UpdateMessageRequest, Message>()
      .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    
    CreateMap<Chat, ChatResponse>();
    CreateMap<Chat, CreateChatResponse>();
    CreateMap<CreateChatRequest, Chat>()
      .ForMember(dest => dest.Users, opt => opt.Ignore());
    CreateMap<UpdateChatRequest, Chat>()
      .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

    CreateMap<User, UserResponse>();
    CreateMap<User, CreateUserResponse>();
    CreateMap<CreateUserRequest, User>()
      .ForMember(dest => dest.Password, opt => opt.Ignore());
    CreateMap<UpdateUserRequest, User>()
      .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
  }
}