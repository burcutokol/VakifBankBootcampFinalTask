using AutoMapper;
using DataProject.Entites;
using SchemaProject;
using System;


namespace OperationProject.Mapper
{
    public class MapperConfig : Profile
    {
     
        private readonly IMapper mapper;

        public MapperConfig( IMapper mapper)
        {
          
            this.mapper = mapper;
        }
        public MapperConfig() 
        {
            CreateMap<BillRequest, Bill>(); //BillRequest to Bill
            CreateMap<Bill, BillResponse>(); //Bill to BillResponse
            
             
            CreateMap<ProductRequest, Product>(); //ProductRequest to Product
            CreateMap<Product, ProductResponse>(); //Product to ProductResponse

            CreateMap<OrderRequest, Order>(); //OrderRequest to Order

            CreateMap<Order, OrderResponse>()
           .ForMember(dest => dest.DealerId, opt => opt.MapFrom(src => src.Dealer.DealerId))
           .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payment.PaymentId))
           .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(item => mapper.Map<OrderItemResponse>(item)))); //Order to OrderResponse
            //TODO

        }
    }
}
