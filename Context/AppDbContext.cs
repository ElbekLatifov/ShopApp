using Microsoft.EntityFrameworkCore;
using ShopSystem.Models;

namespace ShopSystem.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Shop> Shops { get; set; }

    public DbSet<User> Users { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;database=wpf;user=root;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.24-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder
        //    .UseCollation("utf8mb4_0900_ai_ci")
        //    .HasCharSet("utf8mb4");

        //modelBuilder.Entity<Category>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PRIMARY");

        //    entity.ToTable("categories");

        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.ParentId).HasColumnName("parentId");
        //    entity.Property(e => e.Title)
        //        .HasColumnType("text")
        //        .HasColumnName("title");
        //});

        //modelBuilder.Entity<Product>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PRIMARY");

        //    entity.ToTable("products");

        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.Categoryid).HasColumnName("categoryid");
        //    entity.Property(e => e.Price).HasColumnName("price");
        //    entity.Property(e => e.Title)
        //        .HasColumnType("text")
        //        .HasColumnName("title");
        //});

        //modelBuilder.Entity<Shop>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PRIMARY");

        //    entity.ToTable("shops");

        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.Name)
        //        .HasColumnType("text")
        //        .HasColumnName("name");
        //    entity.Property(e => e.Owner)
        //        .HasColumnType("text")
        //        .HasColumnName("owner");
        //});

        //modelBuilder.Entity<User>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PRIMARY");

        //    entity.ToTable("users");

        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.Name)
        //        .HasColumnType("text")
        //        .HasColumnName("name");
        //    entity.Property(e => e.Password)
        //        .HasColumnType("text")
        //        .HasColumnName("password");
        //    entity.Property(e => e.RememberMe).HasColumnName("rememberMe");
        //});

        //OnModelCreatingPartial(modelBuilder);
    }

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
