using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entities.Models;

public partial class MoneyManagementContext : DbContext
{
    public static MoneyManagementContext Ins = new MoneyManagementContext();
    public MoneyManagementContext()
    {
        if(Ins == null)
        {
            Ins = this;
        }
    }

    public MoneyManagementContext(DbContextOptions<MoneyManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Expenditure> Expenditures { get; set; }

    public virtual DbSet<Jar> Jars { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Revenue> Revenues { get; set; }

    public virtual DbSet<TypeUsage> TypeUsages { get; set; }

    public virtual DbSet<Usage> Usages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("MyCnn"));
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__customer__3213E83F92250CE4");

            entity.ToTable("customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deposit).HasColumnName("deposit");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Expenditure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Expendit__3213E83F78479E6C");

            entity.ToTable("Expenditure");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note).HasMaxLength(4000);

            entity.HasOne(d => d.Jar).WithMany(p => p.Expenditures)
                .HasForeignKey(d => d.JarId)
                .HasConstraintName("FK__Expenditu__JarId__44FF419A");

            entity.HasOne(d => d.Usage).WithMany(p => p.Expenditures)
                .HasForeignKey(d => d.UsageId)
                .HasConstraintName("FK__Expenditu__Usage__440B1D61");
        });

        modelBuilder.Entity<Jar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jar__3213E83F14EA45D0");

            entity.ToTable("jar");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Name).HasMaxLength(400);
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.Customer).WithMany(p => p.Jars)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__jar__customerId__52593CB8");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3213E83F8B108DA3");

            entity.ToTable("RefreshToken");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpireTime).HasColumnType("datetime");
            entity.Property(e => e.Token)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("token");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RefreshTo__UserI__5AEE82B9");
        });

        modelBuilder.Entity<Revenue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Revenue__3213E83F7B94455C");

            entity.ToTable("Revenue");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note).HasMaxLength(4000);

            entity.HasOne(d => d.Jar).WithMany(p => p.Revenues)
                .HasForeignKey(d => d.JarId)
                .HasConstraintName("FK__Revenue__JarId__412EB0B6");

            entity.HasOne(d => d.Usage).WithMany(p => p.Revenues)
                .HasForeignKey(d => d.UsageId)
                .HasConstraintName("FK__Revenue__UsageId__403A8C7D");
        });

        modelBuilder.Entity<TypeUsage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TypeUsag__3213E83F29CF6B06");

            entity.ToTable("TypeUsage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<Usage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usage__3213E83FCF34A463");

            entity.ToTable("Usage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(400);

            entity.HasOne(d => d.Type).WithMany(p => p.Usages)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Usage__TypeId__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
