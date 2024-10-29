using ChatApp.Core.Interfaces.Services;
using ChatApp.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using ChatApp.Core.DTOs.Users;
using ChatApp.Application.Validators.User;
using ChatApp.Core.Interfaces.Validators;
using ChatApp.Application.Validators;
using ChatApp.Core.DTOs.Chats;
using ChatApp.Application.Validators.Chat;
using ChatApp.Core.DTOs.Messages;
using ChatApp.Application.Validators.Message;

namespace ChatApp.Application.DependencyInjection;

public static class DependencyInjection
{
  public static void AddApplication(this IServiceCollection services)
  {
    services.AddAutoMapper(typeof(MappingProfile));
    InitServices(services);
  }

  public static void InitServices(this IServiceCollection services)
  {
    services.AddScoped<IUserService, Services.UserService>();
    services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>();
    services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>();
    services.AddScoped<IValidator<DeleteUserRequest>, DeleteUserRequestValidator>();
    services.AddScoped<IUserValidator, UserValidator>();

    services.AddScoped<IChatService, Services.ChatService>();
    services.AddScoped<IValidator<CreateChatRequest>, CreateChatRequestValidator>();
    services.AddScoped<IValidator<UpdateChatRequest>, UpdateChatRequestValidator>();
    services.AddScoped<IValidator<DeleteChatRequest>, DeleteChatRequestValidator>();
    services.AddScoped<IChatValidator, ChatValidator>();

    services.AddScoped<IMessageService, Services.MessageService>();
    services.AddScoped<IValidator<CreateMessageRequest>, CreateMessageRequestValidator>();
    services.AddScoped<IValidator<UpdateMessageRequest>, UpdateMessageRequestValidator>();
    services.AddScoped<IValidator<DeleteMessageRequest>, DeleteMessageRequestValidator>();
    services.AddScoped<IMessageValidator, MessageValidator>();
  }
}