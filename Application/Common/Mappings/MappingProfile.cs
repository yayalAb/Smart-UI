using Application.CompanyModule.Commands.CreateCompanyCommand;
using Application.CompanyModule.Commands.UpdateCompanyCommand;
using Application.ContactPersonModule.Commands.ContactPersonCreateCommand;
using Application.ContactPersonModule.Commands.ContactPersonUpdateCommand;
using Application.ContainerModule;
using Application.ContainerModule.Commands.CreateContainer;
using Application.ContainerModule.Commands.CreateSingleContainer;
using Application.DocumentationModule.Commands.CreateDocumentation;
using Application.GoodModule;
using Application.GoodModule.Commands.AssignGoodsCommand;
using Application.GoodModule.Commands.UpdateGoodCommand;
using Application.GoodModule.Queries;
using Application.OperationDocuments.Queries;
using Application.OperationModule.Commands.CreateOperation;
using Application.OperationModule.Commands.UpdateOperation;
using Application.PaymentModule.Commands.CreatePayment;
using Application.PaymentModule.Commands.UpdatePayment;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using Application.ShippingAgentModule.Commands.UpdateShippingAgent;
using Application.TruckModule.Commands.CreateTruckCommand;
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

            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<UpdateAddressDto , Address>().ReverseMap();
            CreateMap<ContactPersonCreateCommand , ContactPerson>();
            CreateMap<ContactPersonUpdateCommand , ContactPerson>();
            CreateMap<CreateCompanyCommand , Company>();
            CreateMap<UpdateCompanyCommand , Company>();
            CreateMap<BankInformationDto , BankInformation>();
            CreateMap<UpdateBankInformationDto , BankInformation>();

            CreateMap<CreateDocumentationCommand, Documentation>();

            CreateMap<CreateShippingAgentCommand, ShippingAgent>();

            CreateMap<UpdatePymentCommand, Payment>();
            CreateMap<CreatePaymentCommand , Payment>();
            CreateMap<CreateOperationCommand, Operation>();
            CreateMap<UpdateOperationCommand, Operation>();
            CreateMap<CreateContainerCommand, Container>();
            CreateMap<CreateSingleContainer, Container>();
            CreateMap<GoodDto , Good>();
            CreateMap<ASgContainerDto, Container>();
            CreateMap<ContainerDto, Container>();
            CreateMap<CreateTruckCommand , Truck>();
            CreateMap<Good, DocGoodDto>().ReverseMap();
            CreateMap<UpdateGoodContainerDto , Container>().ReverseMap();
            CreateMap<UpdateGoodDto , Good>().ReverseMap();

            

            var mapFromType = typeof(IMapFrom<>);

            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types) {

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