using Aspian.Application.Core.AttachmentServices.AdminServices;
using Aspian.Domain.AttachmentModel;
using Aspian.Domain.SiteModel;
using Aspian.Domain.UserModel;
using AutoMapper;

namespace Aspian.Application.Core.AttachmentServices.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AttachmentDto, Attachment>()
                .ForMember(d => d.CreatedAt, o => o.UseDestinationValue())
                .ForMember(d => d.CreatedById, o => o.UseDestinationValue());
            CreateMap<Attachment, AttachmentDto>();
            CreateMap<FileUploadResult, Attachment>();
            CreateMap<FileUploadResult, AttachmentDto>();
            CreateMap<User, UserDto>();
            CreateMap<Site, SiteDto>();
        }
    }
}