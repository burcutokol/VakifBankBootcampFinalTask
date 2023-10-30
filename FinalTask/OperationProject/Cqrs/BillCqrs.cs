using MediatR;
using SchemaProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationProject.Cqrs
{
  public record CreateBillCommand() : IRequest<BillRequest>;
}
