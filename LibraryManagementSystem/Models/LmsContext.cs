using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Models;

public partial class LmsContext : DbContext
{
    public LmsContext()
    {
    }

    public LmsContext(DbContextOptions<LmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookAuthor> BookAuthors { get; set; }

    public virtual DbSet<BookCopy> BookCopies { get; set; }

    public virtual DbSet<BookReservation> BookReservations { get; set; }

    public virtual DbSet<BookReview> BookReviews { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberRole> MemberRoles { get; set; }

    public virtual DbSet<MemberSubscription> MemberSubscriptions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC0753AF0893");

            entity.ToTable("Author");

            entity.Property(e => e.AuthorName).HasMaxLength(150);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC07BE16D460");

            entity.ToTable("Book");

            entity.Property(e => e.Height).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Image).HasMaxLength(300);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Length).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Publication).HasMaxLength(200);
            entity.Property(e => e.ShortDesc).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Width).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Book__GenreId__4CA06362");
        });

        modelBuilder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookAuth__3214EC072FB208F3");

            entity.ToTable("BookAuthor");

            entity.HasOne(d => d.Author).WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__BookAutho__Autho__52593CB8");

            entity.HasOne(d => d.Book).WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__BookAutho__BookI__5165187F");
        });

        modelBuilder.Entity<BookCopy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookCopy__3214EC07A51FBECC");

            entity.ToTable("BookCopy");

            entity.Property(e => e.Barcode).HasMaxLength(100);
            entity.Property(e => e.Condition).HasMaxLength(50);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);

            entity.HasOne(d => d.Book).WithMany(p => p.BookCopies)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__BookCopy__BookId__6754599E");
        });

        modelBuilder.Entity<BookReservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookRese__3214EC0785EB59BE");

            entity.ToTable("BookReservation");

            entity.Property(e => e.IsComplete).HasDefaultValue(false);
            entity.Property(e => e.IsViolation).HasDefaultValue(false);
            entity.Property(e => e.LastUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.ViolationReason).HasMaxLength(200);
            entity.Property(e => e.ViolationRemarks).HasMaxLength(500);

            entity.HasOne(d => d.BookCopy).WithMany(p => p.BookReservations)
                .HasForeignKey(d => d.BookCopyId)
                .HasConstraintName("FK__BookReser__BookC__6D0D32F4");

            entity.HasOne(d => d.Member).WithMany(p => p.BookReservations)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__BookReser__Membe__6C190EBB");
        });

        modelBuilder.Entity<BookReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookRevi__3214EC070C56E857");

            entity.ToTable("BookReview");

            entity.Property(e => e.Comments).HasMaxLength(1000);
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.ReviewedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Book).WithMany(p => p.BookReviews)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__BookRevie__BookI__71D1E811");

            entity.HasOne(d => d.Member).WithMany(p => p.BookReviews)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__BookRevie__Membe__70DDC3D8");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genre__3214EC076AFCCFAF");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreName).HasMaxLength(100);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Member__3214EC07D59BC419");

            entity.ToTable("Member");

            entity.HasIndex(e => e.Email, "UQ__Member__A9D10534159AF88B").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.Remarks).HasMaxLength(500);
        });

        modelBuilder.Entity<MemberRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MemberRo__3214EC07A3AE7948");

            entity.ToTable("MemberRole");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Member).WithMany(p => p.MemberRoles)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__MemberRol__Membe__5EBF139D");

            entity.HasOne(d => d.Role).WithMany(p => p.MemberRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__MemberRol__RoleI__5FB337D6");
        });

        modelBuilder.Entity<MemberSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MemberSu__3214EC0782C38811");

            entity.ToTable("MemberSubscription");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentMode).HasMaxLength(50);

            entity.HasOne(d => d.Member).WithMany(p => p.MemberSubscriptions)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__MemberSub__Membe__628FA481");

            entity.HasOne(d => d.Subscription).WithMany(p => p.MemberSubscriptions)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("FK__MemberSub__Subsc__6383C8BA");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC075F1FFD60");

            entity.ToTable("Role");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrip__3214EC07962F81D5");

            entity.ToTable("Subscription");

            entity.Property(e => e.SubscriptionName).HasMaxLength(50);
            entity.Property(e => e.YearlyFee).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
