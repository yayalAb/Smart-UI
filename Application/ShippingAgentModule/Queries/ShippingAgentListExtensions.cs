using Domain.Entities;

namespace Application.ShippingAgentModule.Queries;
public static class ShippingAgentListExtensions
{
    public static List<ShippingAgentDto> ToShippingAgentDto(this List<ShippingAgent> items){
         List<ShippingAgentDto> itemDtos  = new List<ShippingAgentDto>();
       for(int i = 0; i<items.Count; i++){
          itemDtos.Add(new ShippingAgentDto{
            Id = items[i].Id,
            FullName = items[i].FullName,
            CompanyName = items[i].CompanyName,
            Image = items[i].Image,
            Email = items[i].Address.Email,
            Phone = items[i].Address.Phone,
            Country = items[i].Address.Country
          });
       }
       return itemDtos;
    }
        public static ShippingAgentDto ToShippingAgentDto(this ShippingAgent item){
         ShippingAgentDto itemDto  = new ShippingAgentDto{
            Id = item.Id,
            FullName = item.FullName,
            CompanyName = item.CompanyName,
            Image = item.Image,
            Email = item.Address.Email,
            Phone = item.Address.Phone,
            Country = item.Address.Country
        
       };
       return itemDto;
    }
}