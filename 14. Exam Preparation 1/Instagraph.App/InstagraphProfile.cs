using AutoMapper;
using Instagraph.DataProcessor.Dto.Import;
using Instagraph.Models;

namespace Instagraph.App
{
    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            this.CreateMap<PictureDto, Picture>();
            this.CreateMap<UserDto, User>();
            this.CreateMap<UserFollowerDto, UserFollower>();
            this.CreateMap<CommentDto, Comment>();
        }
    }
}
