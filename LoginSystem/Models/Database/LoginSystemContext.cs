using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Models.Database;

public partial class LoginSystemContext : DbContext
{
    public LoginSystemContext()
    {
    }

    public LoginSystemContext(DbContextOptions<LoginSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.UserName).IsUnicode(false);
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailsId);

            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.FirstName).IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDetails_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
