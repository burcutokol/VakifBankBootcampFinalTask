using AutoMapper;
using DataProject.Context;
using DataProject.Entites;
using SchemaProject;
using System;


namespace OperationProject.Mapper
{
    public class MapperConfig : Profile
    {

        private IMapper mapper;
        public MapperConfig() 
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Payment, PaymentResponse>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId));

                cfg.CreateMap<ProductRequest, Product>()
                .BeforeMap((src, dest) => dest.InsertDate = DateTime.Now);//ProductRequest to Product
                cfg.CreateMap<Product, ProductResponse>(); //Product to ProductResponse

                cfg.CreateMap<OrderItem, OrderItemResponse>();
                cfg.CreateMap<Payment, PaymentResponse>();
                cfg.CreateMap<Order, OrderResponse>()
                    .ForMember(dest => dest.DealerId, opt => opt.MapFrom(src => src.Dealer.DealerId))
                    .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payment.PaymentId))
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                    .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount)); // Order to OrderResponse
                cfg.CreateMap<PaymentRequest, Payment>()
                     .ForMember(dest => dest.PaymentId, opt => opt.Ignore()) 
                     .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                     .ForMember(dest => dest.PaidAmount, opt => opt.Ignore()) // PaidAmount'ı Ignore et
                     .ForMember(dest => dest.PaymentStatus, opt => opt.Ignore())
                     .ForMember(dest => dest.Order, opt => opt.Ignore()) // Order'ı Ignore et
                     .BeforeMap((src, dest) => dest.InsertDate = DateTime.Now);
                    

            cfg.CreateMap<OrderItemRequest, OrderItem>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore 'Id'
                    .ForMember(dest => dest.InsertDate, opt => opt.Ignore()) // Ignore 'InsertDate'
                    .ForMember(dest => dest.UpdateDate, opt => opt.Ignore()) // Ignore 'UpdateDate'
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore())// Ignore 'IsActive'
                    .ForMember(dest => dest.OrderId, opt => opt.Ignore()) // OrderId'yi Ignore et
                    .ForMember(dest => dest.Order, opt => opt.Ignore()) // Order'ı Ignore et
                    .ForMember(dest => dest.Product, opt => opt.Ignore())
                    .BeforeMap((src, dest) => dest.InsertDate = DateTime.Now) // InsertDate'ı Ignore et
                    .ForMember(dest => dest.UpdateDate, opt => opt.Ignore()); // Product'ı Ignore et
                cfg.CreateMap<PaymentRequest, Payment>()
                    .ForMember(dest => dest.PaymentId, opt => opt.Ignore()) // Ignore 'PaymentId'
                    .ForMember(dest => dest.OrderId, opt => opt.Ignore()) // Ignore 'OrderId'
                    .ForMember(dest => dest.PaidAmount, opt => opt.Ignore()) // Ignore 'PaidAmount'
                    .ForMember(dest => dest.PaymentStatus, opt => opt.Ignore())
                    .ForMember(dest => dest.Order, opt => opt.Ignore()) // Order'ı Ignore et
                    .BeforeMap((src, dest) => dest.InsertDate = DateTime.Now) // InsertDate'ı Ignore et
                    .ForMember(dest => dest.UpdateDate, opt => opt.Ignore()) // UpdateDate'ı Ignore et
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore());
                cfg.CreateMap<OrderRequest, Order>()
                    .ForMember(dest => dest.PaymentId, opt => opt.Ignore())
                    .ForMember(dest => dest.DealerId, opt => opt.Ignore())
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                    .ForMember(dest => dest.Dealer, opt => opt.Ignore())
                    .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                    .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                    .ForMember(dest => dest.UpdateDate, opt => opt.Ignore())
                    .BeforeMap((src, dest) => dest.OrderDate = DateTime.Now)
                    .ForMember(dest => dest.OrderDate, opt => opt.Ignore()); // OrderDate'i Ignore et

                    cfg.CreateMap<Message, MessageResponse>()
                    .ForMember(dest => dest.SenderUserName, opt => opt.Ignore());

                     cfg.CreateMap<MessageRequest, Message>()
                    .ForMember(dest => dest.SenderId, opt => opt.Ignore())
                    .BeforeMap((src, dest) => dest.InsertDate = DateTime.Now);

            });

            mapper = config.CreateMapper();


        }
        public IMapper GetMapper()
        {
            return mapper;
        }
    }
}
