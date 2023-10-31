using AutoMapper;
using DataProject.Entites;
using SchemaProject;
using System;


namespace OperationProject.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<BillRequest, Bill>(); //BillRequest to Bill
            CreateMap<Bill, BillResponse>(); //Bill to BillResponse
            
             
            CreateMap<ProductRequest, Product>(); //ProductRequest to Product
            CreateMap<Product, ProductResponse>(); //Product to ProductResponse
        }
    }
}
