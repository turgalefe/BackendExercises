using ecommerce.dataaccess;
using ECommerce.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess
{
    public partial class ECommerceContext : DbContext
    {
        public ECommerceContext()
        {
        }

        public ECommerceContext(DbContextOptions<ECommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        //public virtual DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ECommerce;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer entity configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Customer");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()// Ensures the database generates the value
                    .HasColumnName("Customer_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength()
                    .HasColumnName("name");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsFixedLength()
                    .HasColumnName("email");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Order");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Order_id");

                entity.Property(e => e.OrderDate)
                    .IsRequired()
                    .HasColumnName("OrderDate");

                entity.Property(e => e.TotalAmount)
                    .IsRequired()
                    .HasColumnName("TotalAmount");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("Customer_id");

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(e => e.OrderItems)
                    .WithOne()
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });



            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("OrderItem");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd() // Ensure Id is generated
                    .HasColumnName("Id"); // "OrderItem_id" değil, sadece "Id"

                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.Price).IsRequired();

                entity.Property(e => e.OrderId)
                    .HasColumnName("Order_id")
                    .IsRequired(false); // Allow null

                entity.Property(e => e.ProductId)
                    .HasColumnName("Product_id")
                    .IsRequired(false); // Allow null

                entity.HasOne(e => e.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.SetNull);
            });


            // Product entity configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Product");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd() 
                    .HasColumnName("Product_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength()
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnName("price");
            });
            // User entity configuration
            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.ToTable("User");

            //    entity.Property(e => e.Id)
            //        .ValueGeneratedOnAdd()
            //        .HasColumnName("User_id");

            //    entity.Property(e => e.Email)
            //        .HasMaxLength(100)
            //        .HasColumnName("email");

            //    entity.Property(e => e.Password)
            //        .HasMaxLength(100)
            //        .HasColumnName("password");

            //    entity.Property(e => e.JwtToken)
            //        .HasMaxLength(500)
            //        .HasColumnName("jwt_token");
            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
