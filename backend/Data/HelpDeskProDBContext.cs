using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend.Models.Entities;

namespace backend.Data;

public partial class HelpDeskProDBContext : DbContext
{
    public HelpDeskProDBContext()
    {
    }

    public HelpDeskProDBContext(DbContextOptions<HelpDeskProDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<SystemEntity> SystemEntities { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketPriority> TicketPriorities { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<TicketStatusChange> TicketStatusChanges { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=HelpDeskProDBConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("Branch");

            entity.Property(e => e.address).HasMaxLength(100);
            entity.Property(e => e.branchName).HasMaxLength(50);
            entity.Property(e => e.createdAt).HasColumnType("datetime");
            entity.Property(e => e.createdBy).HasMaxLength(50);
            entity.Property(e => e.isActive).HasDefaultValue(true);
            entity.Property(e => e.updatedAt).HasColumnType("datetime");
            entity.Property(e => e.updatedBy).HasMaxLength(50);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.createdAt).HasColumnType("datetime");
            entity.Property(e => e.createdBy).HasMaxLength(50);
            entity.Property(e => e.departmentName).HasMaxLength(50);
            entity.Property(e => e.isActive).HasDefaultValue(true);
            entity.Property(e => e.updatedAt).HasColumnType("datetime");
            entity.Property(e => e.updatedBy).HasMaxLength(50);
        });

        modelBuilder.Entity<SystemEntity>(entity =>
        {
            entity.HasKey(e => e.systemId).HasName("PK_System");

            entity.ToTable("SystemEntity");

            entity.Property(e => e.createdAt).HasColumnType("datetime");
            entity.Property(e => e.createdBy).HasMaxLength(50);
            entity.Property(e => e.isActive).HasDefaultValue(true);
            entity.Property(e => e.systemName).HasMaxLength(50);
            entity.Property(e => e.updatedAt).HasColumnType("datetime");
            entity.Property(e => e.updatedBy).HasMaxLength(50);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");

            entity.Property(e => e.createdAt).HasColumnType("datetime");
            entity.Property(e => e.createdBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.title).HasMaxLength(150);
            entity.Property(e => e.updatedAt).HasColumnType("datetime");
            entity.Property(e => e.updatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.branch).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.branchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Branch");

            entity.HasOne(d => d.department).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.departmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Department");

            entity.HasOne(d => d.priority).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.priorityId)
                .HasConstraintName("FK_Ticket_TicketPriority");

            entity.HasOne(d => d.status).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.statusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_TicketStatus");

            entity.HasOne(d => d.system).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.systemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_System");
        });

        modelBuilder.Entity<TicketPriority>(entity =>
        {
            entity.HasKey(e => e.priorityId);

            entity.ToTable("TicketPriority");

            entity.Property(e => e.priority)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.remarks).HasMaxLength(150);
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasKey(e => e.statusId);

            entity.ToTable("TicketStatus");

            entity.Property(e => e.remarks).HasMaxLength(150);
            entity.Property(e => e.status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TicketStatusChange>(entity =>
        {
            entity.HasKey(e => e.changeId);

            entity.Property(e => e.dateTime).HasColumnType("datetime");

            entity.HasOne(d => d.status).WithMany(p => p.TicketStatusChanges)
                .HasForeignKey(d => d.statusId)
                .HasConstraintName("FK_TicketStatusChanges_TicketStatus");

            entity.HasOne(d => d.ticket).WithMany(p => p.TicketStatusChanges)
                .HasForeignKey(d => d.ticketId)
                .HasConstraintName("FK_TicketStatusChanges_Ticket");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
