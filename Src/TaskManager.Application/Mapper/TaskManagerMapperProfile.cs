using System;
using TaskManager.Application.Command.TaskCommand;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;
using AutoMapper;
using TaskManager.Application.Command.ProjectCommand;
using TaskManager.Application.Command.NotificationCommand;

namespace TaskManager.Application.Mapper
{
	public class TaskManagerMapperProfile:Profile
	{
		public TaskManagerMapperProfile()
		{
            //CreateMap<Tasks, TaskResponse>().ReverseMap();
            //CreateMap<Tasks, CreateTaskCommand>().ReverseMap();
            //CreateMap<Tasks, EditTaskCommand>().ReverseMap();
            //CreateMap<Project, ProjectResponse>().ReverseMap();
            //CreateMap<Project, CreateProjectCommand>().ReverseMap();
            //CreateMap<Project, EditProjectCommand>().ReverseMap();
            //CreateMap<Notification, CreateNotificationCommand>().ReverseMap();

            CreateMap<Tasks, TaskResponse>().ReverseMap();
            //THIS WAS DONE BECAUSE I'M USING ENUM
            CreateMap<Tasks, CreateTaskCommand>()
                 .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status.ToString()))
                .ForMember(dest => dest.priority, opt => opt.MapFrom(src => src.priority.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => Enum.Parse<Status>(src.status, true)))
                .ForMember(dest => dest.priority, opt => opt.MapFrom(src => Enum.Parse<Priority>(src.priority, true)));
            //THIS WAS DONE BECAUSE I'M USING ENUM
            CreateMap<Tasks, EditTaskCommand>()
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status.ToString()))
                .ForMember(dest => dest.priority, opt => opt.MapFrom(src => src.priority.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => Enum.Parse<Status>(src.status, true)))
                .ForMember(dest => dest.priority, opt => opt.MapFrom(src => Enum.Parse<Priority>(src.priority, true)));
            CreateMap<Project, ProjectResponse>().ReverseMap();
            CreateMap<Project, CreateProjectCommand>().ReverseMap();
            CreateMap<Project, EditProjectCommand>().ReverseMap();
            CreateMap<Notification, CreateNotificationCommand>().ReverseMap();
        }
	}
}

