using Application.DocumentationModule.Commands.CreateDocumentation;
using Application.OperationModule.Commands.CreateOperation;
using Application.PaymentModule.Commands.CreatePayment;
using Application.PaymentModule.Commands.UpdatePayment;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
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
            //TODO: add mapping configs here
            CreateMap<UserRoleDto, AppUserRole>();
            CreateMap<AddressDto, Address>().ReverseMap();

            CreateMap<CreateDocumentationCommand, Documentation>();

            CreateMap<CreateShippingAgentCommand, ShippingAgent>();

            CreateMap<UpdatePymentCommand, Payment>();
            CreateMap<CreatePaymentCommand , Payment>();
            CreateMap<CreateOperationCommand, Operation>();

            

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