using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}