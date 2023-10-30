using DataProject.Entites;
using DataProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Uow
{
    public interface IUnitOfWork
    {
        void Complete();
        void CompleteTransaction(); //for data consistency
        IGenericRepository<Bill> BillRepository { get; }
        IGenericRepository<Dealer> DealerRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<OrderItem> OrderItemRepository { get; }
        IGenericRepository<Payment> PaymentRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Report> ReportRepository { get; }
        IGenericRepository<User> UserRepository { get; }
    }
}
