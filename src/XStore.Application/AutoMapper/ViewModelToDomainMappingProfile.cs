using AutoMapper;
using XStore.Application.ViewModel;
using XStore.Domain.Entities;

namespace XStore.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<OrderViewModel, Order>()
                .ForMember(dest => dest.AddressId, opt => opt.MapFrom(map => map.Address.Id));
        }
    }
}
