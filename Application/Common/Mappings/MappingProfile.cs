﻿using Application.AddressModule.Commands.AddressUpdateCommand;
using Application.Button.Commands.CreateButtonCommand;
using Application.Button.Queries.GetAllButtons;
using Application.Component.Commands.createComponent;
using Application.Project.Query;
using Application.UserGroupModule.Commands;
using AutoMapper;
using Domain.Entities;
using System.Reflection;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {

            CreateMap<UserRoleDto, AppUserRole>();
            CreateMap<FetchUserRoleDto, AppUserRole>();
            CreateMap<ProjectModel,ProjectsDto>();
            CreateMap<feildsDto,feildsModel>();
            CreateMap<ButtonFeildsDto,ButtonFeilds>();
            CreateMap<GetButtonDto,ButtonModel>();
           
            CreateMap<AddressUpdateCommand, Address>().ReverseMap();
            CreateMap<CreateComponentCommand, ComponentModel>().ReverseMap();
            CreateMap<CreateButtonCommand, ButtonModel>().ReverseMap();

             


            var mapFromType = typeof(IMapFrom<>);

            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                            interfaceMethodInfo?.Invoke(instance, new object[] { this });
                        }
                    }
                }

            }
        }
    }
}