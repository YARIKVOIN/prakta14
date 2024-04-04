using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnergyApi2.Models
{
    public partial class EnergyTorpedaContext : DbContext
    {
        public EnergyTorpedaContext()
        {
        }

        public EnergyTorpedaContext(DbContextOptions<EnergyTorpedaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appuser> Appusers { get; set; } = null!;
        public virtual DbSet<Basket> Baskets { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Energy> Energies { get; set; } = null!;
        public virtual DbSet<Favoritebasket> Favoritebaskets { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Infoappenergy> Infoappenergies { get; set; } = null!;
        public virtual DbSet<Infoappuser> Infoappusers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=1234;database=EnergyTorpeda");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appuser>(entity =>
            {
                entity.HasKey(e => e.IdAppUser)
                    .HasName("PRIMARY");

                entity.ToTable("appuser");

                entity.HasIndex(e => e.RolesId, "FK_Roles_AppUser");

                entity.HasIndex(e => e.Login, "Login")
                    .IsUnique();

                entity.HasIndex(e => e.Mail, "Mail")
                    .IsUnique();

                entity.HasIndex(e => e.Salt, "Salt")
                    .IsUnique();

                entity.Property(e => e.IdAppUser).HasColumnName("ID_AppUser");

                entity.Property(e => e.AppPassword).HasMaxLength(50);

                entity.Property(e => e.IsExists).HasDefaultValueSql("'true'");

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.RolesId).HasColumnName("Roles_ID");

                entity.Property(e => e.Salt).HasMaxLength(50);


            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasKey(e => e.IdBasket)
                    .HasName("PRIMARY");

                entity.ToTable("basket");

                entity.HasIndex(e => e.AppUserId, "FK_AppUser_Basket");

                entity.Property(e => e.IdBasket).HasColumnName("ID_Basket");

                entity.Property(e => e.AppUserId).HasColumnName("AppUser_ID");

                entity.Property(e => e.IsExists).HasDefaultValueSql("'true'");


            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PRIMARY");

                entity.ToTable("category");

                entity.HasIndex(e => e.NameCategory, "Name_Category")
                    .IsUnique();

                entity.Property(e => e.IdCategory).HasColumnName("ID_Category");

                entity.Property(e => e.IsExists).HasDefaultValueSql("'true'");

                entity.Property(e => e.NameCategory).HasColumnName("Name_Category");
            });

            modelBuilder.Entity<Energy>(entity =>
            {
                entity.HasKey(e => e.IdEnergy)
                    .HasName("PRIMARY");

                entity.ToTable("energy");

                entity.HasIndex(e => e.CategoryId, "FK_Category_Energy");

                entity.HasIndex(e => e.ImageId, "FK_Image_Energy");

                entity.HasIndex(e => e.NameEnergy, "Name_Energy")
                    .IsUnique();

                entity.Property(e => e.IdEnergy).HasColumnName("ID_Energy");

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.DescriptionEnergy)
                    .HasColumnType("text")
                    .HasColumnName("Description_Energy");

                entity.Property(e => e.ImageId).HasColumnName("Image_ID");

                entity.Property(e => e.IsExists).HasDefaultValueSql("'true'");

                entity.Property(e => e.NameEnergy)
                    .HasMaxLength(100)
                    .HasColumnName("Name_Energy");


            });

            modelBuilder.Entity<Favoritebasket>(entity =>
            {
                entity.HasKey(e => e.IdFavoriteBasket)
                    .HasName("PRIMARY");

                entity.ToTable("favoritebasket");

                entity.HasIndex(e => e.BasketId, "FK_Basket_FavoriteBasket");

                entity.HasIndex(e => e.EnergyId, "FK_Energy_FavoriteBasket");

                entity.Property(e => e.IdFavoriteBasket).HasColumnName("ID_FavoriteBasket");

                entity.Property(e => e.BasketId).HasColumnName("Basket_ID");

                entity.Property(e => e.EnergyId).HasColumnName("Energy_ID");

                entity.Property(e => e.IsExists).HasDefaultValueSql("'true'");


            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.IdHistory)
                    .HasName("PRIMARY");

                entity.ToTable("history");

                entity.HasIndex(e => e.AppUserId, "FK_AppUser_History");

                entity.HasIndex(e => e.BasketId, "FK_Basket_History");

                entity.Property(e => e.IdHistory).HasColumnName("ID_History");

                entity.Property(e => e.AppUserId).HasColumnName("AppUser_ID");

                entity.Property(e => e.BasketId).HasColumnName("Basket_ID");

                entity.Property(e => e.DataBuy)
                    .HasColumnType("text")
                    .HasColumnName("Data_Buy");

                entity.Property(e => e.DataHistory)
                    .HasColumnType("date")
                    .HasColumnName("Data_History")
                    .HasDefaultValueSql("'curdate()'");

                entity.Property(e => e.IsExists).HasDefaultValueSql("'true'");

                entity.Property(e => e.PriceHistory).HasColumnName("Price_History");

            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.IdImage)
                    .HasName("PRIMARY");

                entity.ToTable("image");

                entity.Property(e => e.IdImage).HasColumnName("ID_Image");

                entity.Property(e => e.IsExists).HasDefaultValueSql("'true'");

                entity.Property(e => e.LinkImage)
                    .HasColumnType("text")
                    .HasColumnName("linkImage");
            });

            modelBuilder.Entity<Infoappenergy>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("infoappenergy");

                entity.Property(e => e.КатегорияЭнергетика).HasColumnName("Категория энергетика");

                entity.Property(e => e.НазваниеЭнергетика)
                    .HasMaxLength(100)
                    .HasColumnName("Название энергетика");

                entity.Property(e => e.ОписаниеЭнергетика)
                    .HasColumnType("text")
                    .HasColumnName("Описание энергетика");
            });

            modelBuilder.Entity<Infoappuser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("infoappuser");

                entity.Property(e => e.Логин).HasMaxLength(50);

                entity.Property(e => e.Пароль).HasMaxLength(50);

                entity.Property(e => e.Почта).HasMaxLength(50);

                entity.Property(e => e.Соль).HasMaxLength(50);

                entity.Property(e => e.СтатусАккаунта).HasColumnName("Статус аккаунта");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRoles)
                    .HasName("PRIMARY");

                entity.ToTable("roles");

                entity.Property(e => e.IdRoles).HasColumnName("ID_Roles");

                entity.Property(e => e.IsExists).HasDefaultValueSql("'true'");

                entity.Property(e => e.NameRoles).HasColumnName("Name_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
