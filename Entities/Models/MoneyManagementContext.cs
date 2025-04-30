using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entities.Models;

public partial class MoneyManagementContext : DbContext
{
    public readonly MoneyManagementContext Ins = new MoneyManagementContext();
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
            entity.HasKey(e => e.Id).HasName("PK__customer__3213E83F3F0799FD");

            entity.ToTable("customer");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.HasKey(e => e.Id).HasName("PK__Expendit__3213E83FFA76A7A8");

            entity.ToTable("Expenditure");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note).HasMaxLength(4000);

            entity.HasOne(d => d.Jar).WithMany(p => p.Expenditures)
                .HasForeignKey(d => d.JarId)
                .HasConstraintName("FK__Expenditu__JarId__5629CD9C");

            entity.HasOne(d => d.Usage).WithMany(p => p.Expenditures)
                .HasForeignKey(d => d.UsageId)
                .HasConstraintName("FK__Expenditu__Usage__5535A963");
        });

        modelBuilder.Entity<Jar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jar__3213E83F311E7787");

            entity.ToTable("jar");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Name).HasMaxLength(400);
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.Customer).WithMany(p => p.Jars)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__jar__customerId__59063A47");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3213E83F9C83FEA1");

            entity.ToTable("RefreshToken");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpireTime).HasColumnType("datetime");
            entity.Property(e => e.Token)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("token");
        });

        modelBuilder.Entity<Revenue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Revenue__3213E83F6A6476D5");

            entity.ToTable("Revenue");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note).HasMaxLength(4000);

            entity.HasOne(d => d.Jar).WithMany(p => p.Revenues)
                .HasForeignKey(d => d.JarId)
                .HasConstraintName("FK__Revenue__JarId__52593CB8");

            entity.HasOne(d => d.Usage).WithMany(p => p.Revenues)
                .HasForeignKey(d => d.UsageId)
                .HasConstraintName("FK__Revenue__UsageId__5165187F");
        });

        modelBuilder.Entity<TypeUsage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TypeUsag__3213E83F396E5F45");

            entity.ToTable("TypeUsage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<Usage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usage__3213E83F0404F92B");

            entity.ToTable("Usage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(400);

            entity.HasOne(d => d.Type).WithMany(p => p.Usages)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Usage__TypeId__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
