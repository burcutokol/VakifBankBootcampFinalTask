using DataProject.Context;
using DataProject.Entites;
using DataProject.Repository;

namespace DataProject.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass dbContextClass;
        public UnitOfWork(DbContextClass dbContextClass)
        {
           this.dbContextClass = dbContextClass;
            BillRepository = new GenericRepository<Bill>(dbContextClass);
            DealerRepository = new GenericRepository<Dealer>(dbContextClass);
            OrderRepository = new GenericRepository<Order>(dbContextClass);
            OrderItemRepository = new GenericRepository<OrderItem>(dbContextClass);
            PaymentRepository = new GenericRepository<Payment>(dbContextClass);
            ProductRepository = new GenericRepository<Product>(dbContextClass);
            ReportRepository = new GenericRepository<Report>(dbContextClass);
            UserRepository = new GenericRepository<User>(dbContextClass);


        }   
        public IGenericRepository<Bill> BillRepository { get; private set; }

        public IGenericRepository<Dealer> DealerRepository { get; private set; }

        public IGenericRepository<Order> OrderRepository { get; private set; }

        public IGenericRepository<OrderItem> OrderItemRepository { get; private set; }

        public IGenericRepository<Payment> PaymentRepository { get; private set; }

        public IGenericRepository<Product> ProductRepository { get; private set; }

        public IGenericRepository<Report> ReportRepository { get; private set; }

        public IGenericRepository<User> UserRepository { get; private set; }

        public void Complete()
        {
            dbContextClass.SaveChanges();
        }
        public void CompleteTransaction()
        {
            using (var transaction = dbContextClass.Database.BeginTransaction())
            {
                try
                {
                    dbContextClass.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //log TODO
                }
            }
        }
    }
}
