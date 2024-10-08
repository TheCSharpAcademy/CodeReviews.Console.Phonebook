using AutoMapper;
using Model;

namespace PhoneBook;
public class AutoMapperConfig
{
    public static IMapper InitializeAutoMapper()
    {
        MapperConfiguration config = new MapperConfiguration(x =>
        {
            x.CreateMap<Contact, ContactDTO>().ReverseMap();
        });
        IMapper mapper = config.CreateMapper();
        return mapper;
    }
}
