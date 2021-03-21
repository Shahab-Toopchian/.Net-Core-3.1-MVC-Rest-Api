using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Source -> Target
            //Get loadData
            CreateMap<Command,CommandReadDto>();
            //CreateData
            CreateMap<CommandCreateDto,Command>();
            //Update Data
            CreateMap<CommandUpdateDto,Command>();
            //Patch
            CreateMap<Command,CommandUpdateDto>();
        }
    }
}