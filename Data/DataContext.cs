using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASPAPI.Data;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }


    public virtual DbSet<TblRefreshtoken> TblRefreshtoken { get; set; }


    public virtual DbSet<User> Users { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreditLimit).HasPrecision(18, 2);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<TblRefreshtoken>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("Refreshtoken_pkey");

            entity.ToTable("Refreshtoken");

            entity.Property(e => e.Userid)
                .HasMaxLength(50)
                .HasColumnName("userid");
            entity.Property(e => e.Refreshtoken)
                .HasMaxLength(9999999)
                .HasColumnName("refreshtoken");
            entity.Property(e => e.Tokenid)
                .HasMaxLength(50)
                .HasColumnName("tokenid");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
