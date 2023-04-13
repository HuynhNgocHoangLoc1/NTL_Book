using AppDev.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NTL_Book.Areas.Identity.Data;
using ApplicationUser = NTL_Book.Models.ApplicationUser;

namespace NTL_Book.Areas.Identity.Data;

public class NTL_BookDbContext : IdentityDbContext<ApplicationUser>
{
    public NTL_BookDbContext(DbContextOptions<NTL_BookDbContext> options)
        : base(options)
    {
    }

    /*protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }*/
    
    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<RequestCategory> RequestCategories { get; set; } = null!;


    public DbSet<Book> Books { get; set; } = null!;

    public DbSet<Store> Stores { get; set; } = null!;

    public DbSet<CartItem> CartItems { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<OrderItem> OrderItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        builder.Entity<ApplicationUser>()
            .HasOne<Store>()
            .WithOne(s => s.StoreOwner)
            .HasForeignKey<Store>(s => s.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>(entity =>
        {
            entity.HasOne(o => o.Store)
                .WithMany()
                .HasForeignKey(o => o.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(i => new
            {
                i.BookId,
                i.OrderId,
            });

            entity.HasOne(e => e.Book)
                .WithMany()
                .HasForeignKey(e => e.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<CartItem>(entity =>
        {
            entity.HasKey(i => new
            {
                i.BookId,
                i.CustomerId,
            });

            entity.HasOne(e => e.Book)
                .WithMany()
                .HasForeignKey(e => e.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(builder);
    }
}

