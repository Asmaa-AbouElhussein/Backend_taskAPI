using AutoMapper;
using TaskAPI.DTO;
using TaskAPI.Models;

namespace TaskAPI.profiles
{
    public class UserDTOProfile : Profile
    {
          public UserDTOProfile()
           {
     
              CreateMap<UserDTO, User>();
           }

    }
}
