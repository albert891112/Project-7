using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Team_7_WebApi_Client.Models.EFModels
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=AppDbContext")
        {
        }

        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Coupons_Members> Coupons_Members { get; set; }
        public virtual DbSet<DiscountType> DiscountTypes { get; set; }
        public virtual DbSet<GenderCategory> GenderCategories { get; set; }
        public virtual DbSet<GenderCategories_Categories> GenderCategories_Categories { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatu> OrderStatus { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Premission> Premissions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Roles_Permissions> Roles_Permissions { get; set; }
        public virtual DbSet<Shipping> Shippings { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Users_Roles> Users_Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.GenderCategories_Categories)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Coupon>()
                .Property(e => e.DiscountValue)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Coupon>()
                .HasMany(e => e.Coupons_Members)
                .WithRequired(e => e.Coupon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiscountType>()
                .HasMany(e => e.Coupons)
                .WithRequired(e => e.DiscountType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiscountType>()
                .HasOptional(e => e.DiscountType1)
                .WithRequired(e => e.DiscountType2);

            modelBuilder.Entity<GenderCategory>()
                .HasMany(e => e.GenderCategories_Categories)
                .WithRequired(e => e.GenderCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GenderCategory>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.GenderCategory)
                .HasForeignKey(e => e.GenderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Coupons_Members)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Payment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Premission>()
                .HasMany(e => e.Roles_Permissions)
                .WithRequired(e => e.Premission)
                .HasForeignKey(e => e.PermissionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.CartItems)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Stocks)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Roles_Permissions)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users_Roles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipping>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Shipping)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Stock)
                .HasForeignKey(e => e.StockId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Users_Roles)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<Team_7_WebApi_Client.Models.Views.CartVM> CartVMs { get; set; }

        public System.Data.Entity.DbSet<Team_7_WebApi_Client.Models.Views.CartItemVM> CartItemVMs { get; set; }
    }
}
