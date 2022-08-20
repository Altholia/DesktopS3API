using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DesktopS3API.Entities
{
    public partial class S3Context : DbContext
    {
        public S3Context()
        {
        }

        public S3Context(DbContextOptions<S3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetCategory> AssetCategories { get; set; }
        public virtual DbSet<AssetPhoto> AssetPhotos { get; set; }
        public virtual DbSet<AssetTransfer> AssetTransfers { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<PartCategory> PartCategories { get; set; }
        public virtual DbSet<PartUsedInUpkeep> PartUsedInUpkeeps { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TaskAssign> TaskAssigns { get; set; }
        public virtual DbSet<TaskStatus> TaskStatuses { get; set; }
        public virtual DbSet<TransporationTask> TransporationTasks { get; set; }
        public virtual DbSet<UpkeepRecord> UpkeepRecords { get; set; }
        public virtual DbSet<UpkeepType> UpkeepTypes { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("Asset");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AssetNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ManufactureDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.RegistrationTime).HasColumnType("datetime");

                entity.Property(e => e.ServiceDate).HasColumnType("date");

                entity.Property(e => e.Specification)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_FunctionCategory");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_Department");

                entity.HasOne(d => d.UpkeepType)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.UpkeepTypeId)
                    .HasConstraintName("FK_Asset_UpkeepType");
            });

            modelBuilder.Entity<AssetCategory>(entity =>
            {
                entity.ToTable("AssetCategory");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AssetPhoto>(entity =>
            {
                entity.ToTable("AssetPhoto");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetPhotos)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetPhoto_Asset");
            });

            modelBuilder.Entity<AssetTransfer>(entity =>
            {
                entity.ToTable("AssetTransfer");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TransferTime).HasColumnType("datetime");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetTransfers)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetTransfer_Asset");

                entity.HasOne(d => d.FromDepartment)
                    .WithMany(p => p.AssetTransferFromDepartments)
                    .HasForeignKey(d => d.FromDepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetTransfer_FromDepartment");

                entity.HasOne(d => d.ToDepartment)
                    .WithMany(p => p.AssetTransferToDepartments)
                    .HasForeignKey(d => d.ToDepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetTransfer_ToDepartment");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Province");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_City");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("Driver");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateofBirth).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.ToTable("Part");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Specification)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Parts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Part_PartCategory");
            });

            modelBuilder.Entity<PartCategory>(entity =>
            {
                entity.ToTable("PartCategory");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PartUsedInUpkeep>(entity =>
            {
                entity.ToTable("PartUsedInUpkeep");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.PartUsedInUpkeeps)
                    .HasForeignKey(d => d.PartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartUsedInMaintenance_Part");

                entity.HasOne(d => d.UpkeepRecord)
                    .WithMany(p => p.PartUsedInUpkeeps)
                    .HasForeignKey(d => d.UpkeepRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartUsedInUpkeep_UpkeepRecord");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TaskAssign>(entity =>
            {
                entity.ToTable("TaskAssign");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskAssigns)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskAssign_TransporationTask");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.TaskAssigns)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskAssign_Vehicle");
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.ToTable("TaskStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransporationTask>(entity =>
            {
                entity.ToTable("TransporationTask");

                entity.Property(e => e.ActualCompletionDate).HasColumnType("date");

                entity.Property(e => e.DestinationStreetAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RequiredCompletionDate).HasColumnType("date");

                entity.Property(e => e.StartStreetAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.DestinationDistrict)
                    .WithMany(p => p.TransporationTaskDestinationDistricts)
                    .HasForeignKey(d => d.DestinationDistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransporationTask_District1");

                entity.HasOne(d => d.StartDistrict)
                    .WithMany(p => p.TransporationTaskStartDistricts)
                    .HasForeignKey(d => d.StartDistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransporationTask_District");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TransporationTasks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransporationTask_TaskStatus");

                entity.HasOne(d => d.VehicleTeamAdministratorNavigation)
                    .WithMany(p => p.TransporationTasks)
                    .HasForeignKey(d => d.VehicleTeamAdministrator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransporationTask_Staff");
            });

            modelBuilder.Entity<UpkeepRecord>(entity =>
            {
                entity.ToTable("UpkeepRecord");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UpkeepTime).HasColumnType("datetime");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.UpkeepRecords)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UpkeepRecord_Asset");
            });

            modelBuilder.Entity<UpkeepType>(entity =>
            {
                entity.ToTable("UpkeepType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlateNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicle_Brand");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicle_Driver");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
