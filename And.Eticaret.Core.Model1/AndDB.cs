using And.Eticaret.Core.Model1.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace And.Eticaret.Core.Model1
{
    public class AndDB : DbContext
    {
        //Tablolar
      public AndDB()
            :base(@"Data Source=SQLNCLI10;Server=94.73.146.4; Initial Catalog=u9556588_siparis;User ID=u9556588_kagan;Password=5412019KC**!sk2202")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserAdress> UserAdresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> orderProducts { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
