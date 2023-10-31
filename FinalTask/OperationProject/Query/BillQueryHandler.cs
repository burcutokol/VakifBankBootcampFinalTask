using AutoMapper;
using BaseProject.Response;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationProject.Cqrs;
using SchemaProject;

namespace OperationProject.Query
{
    public class BillQueryHandler
        : IRequestHandler<GetAllBillsQuery, ApiResponse<List<BillResponse>>>,
          IRequestHandler<GetBillByIdQuery, ApiResponse<BillResponse>>
    {
        private readonly DbContextClass dbContextClass;
        private readonly IMapper mapper;

        public BillQueryHandler(DbContextClass dbContextClass, IMapper mapper)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<BillResponse>>> Handle(GetAllBillsQuery request, CancellationToken cancellationToken)
        {
            List<Bill> list = dbContextClass.Set<Bill>().Include(x => x.Payment).ToList();
            var mappedList = mapper.Map<List<BillResponse>>(list);
            return new ApiResponse<List<BillResponse>>(mappedList);
        }

        public async Task<ApiResponse<BillResponse>> Handle(GetBillByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
