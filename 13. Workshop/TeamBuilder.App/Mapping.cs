namespace TeamBuilder.App
{
    using AutoMapper;
    using TeamBuilder.App.ModelDto;
    using TeamBuilder.Models;

    public class Mapping : Profile
    {
        public Mapping()
        {
            this.CreateMap<RegisterUserDto, User>();
            this.CreateMap<CreateTeamDto, Team>();
        }
    }
}