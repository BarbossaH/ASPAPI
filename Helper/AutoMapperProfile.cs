using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ASPAPI.Models;
namespace ASPAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //mapper.Map<DestinationClass>(source)
            //CreateMap<SourceClass, DestinationClass>();
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();

            CreateMap<Customer, GetCustomerDto>().ForMember(item=>item.StatusName,option=>option.MapFrom(
                item=>(item.IsActive !=null && item.IsActive.Value) ? "Active" : "Inactive"
            ));
        }
    }
}