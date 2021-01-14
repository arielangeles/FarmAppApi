using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class FarmAppDBContext : DbContext
    {
        public FarmAppDBContext()
        {
        }

        public FarmAppDBContext(DbContextOptions<FarmAppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdministrationForm> AdministrationForms { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PharmacyBranch> PharmacyBranches { get; set; }
        public virtual DbSet<PharmacyChain> PharmacyChains { get; set; }
        public virtual DbSet<PharmacyProduct> PharmacyProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=xamarinproject.database.windows.net;Initial Catalog=FarmAppDB;User ID=ArielXamarin;Password=Pa$$w0rd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdministrationForm>(entity =>
            {
                entity.HasKey(e => e.IdAdministrationForm)
                    .HasName("PK__Administ__016471B99A815CEF");

                entity.ToTable("AdministrationForm");

                entity.Property(e => e.AdministrationName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AdministrationVia)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK__Category__CBD747060F26ECB8");

                entity.ToTable("Category");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IdCountry)
                    .HasName("PK__Country__F99F104DA67676B2");

                entity.ToTable("Country");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdPerson)
                    .HasName("PK__Person__A5D4E15B6E2DFA27");

                entity.ToTable("Person");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.EmailAddress).HasMaxLength(120);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.HomeAddress).HasMaxLength(200);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PharmacyBranch>(entity =>
            {
                entity.HasKey(e => e.IdPharmacyBranch)
                    .HasName("PK__Pharmacy__62C9C6DA4F44DB0C");

                entity.ToTable("PharmacyBranch");

                entity.Property(e => e.AddressName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Latitude).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PharmacyName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Schedule)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPharmacyChainNavigation)
                    .WithMany(p => p.PharmacyBranches)
                    .HasForeignKey(d => d.IdPharmacyChain)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PharmacyB__IdPha__6B24EA82");

                entity.HasOne(d => d.IdTownNavigation)
                    .WithMany(p => p.PharmacyBranches)
                    .HasForeignKey(d => d.IdTown)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PharmacyB__IdTow__6C190EBB");
            });

            modelBuilder.Entity<PharmacyChain>(entity =>
            {
                entity.HasKey(e => e.IdPharmacyChain)
                    .HasName("PK__Pharmacy__750A9F9A1B42597D");

                entity.ToTable("PharmacyChain");

                entity.Property(e => e.AddressName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.PharmacyName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Rnc)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("RNC");

                entity.Property(e => e.WebPage)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<PharmacyProduct>(entity =>
            {
                entity.HasKey(e => e.IdPharmacyProduct)
                    .HasName("PK__Pharmacy__AE5F5DCA182E00BD");

                entity.ToTable("PharmacyProduct");

                entity.HasOne(d => d.IdPharmacyBranchNavigation)
                    .WithMany(p => p.PharmacyProducts)
                    .HasForeignKey(d => d.IdPharmacyBranch)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PharmacyP__IdPha__76969D2E");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.PharmacyProducts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PharmacyP__IdPro__778AC167");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__Product__2E8946D49D37BC32");

                entity.ToTable("Product");

                entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdAdministrationFormNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdAdministrationForm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__IdAdmin__73BA3083");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__IdCateg__72C60C4A");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.IdReview);

                entity.ToTable("Review");

                entity.Property(e => e.IdReview)
                    .ValueGeneratedNever()
                    .HasColumnName("idReview");

                entity.Property(e => e.ReviewDescription).HasMaxLength(250);

                entity.Property(e => e.ReviewName).HasMaxLength(80);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.IdState)
                    .HasName("PK__State__2E1972BCC827261B");

                entity.ToTable("State");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.IdCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__State__IdCountry__5EBF139D");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasKey(e => e.IdTown)
                    .HasName("PK__Town__860C0321B645B486");

                entity.ToTable("Town");

                entity.Property(e => e.TownName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdStateNavigation)
                    .WithMany(p => p.Towns)
                    .HasForeignKey(d => d.IdState)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Town__IdState__619B8048");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__User__B7C92638FEACADF7");

                entity.ToTable("User");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__IdPerson__66603565");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
