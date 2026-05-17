using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<MedicineStatus> MedicineStatuses { get; set; }

    public virtual DbSet<MedicineType> MedicineTypes { get; set; }

    public virtual DbSet<MedicineUnit> MedicineUnits { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomStatus> RoomStatuses { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceType> ServiceTypes { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGender> UserGenders { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS", "VECTOR" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("medicines_pkey");

            entity.ToTable("medicines");

            entity.HasIndex(e => e.MedicineStatusId, "idx_medicines_status");

            entity.HasIndex(e => e.MedicineTypeId, "idx_medicines_type");

            entity.HasIndex(e => e.MedicineUnitId, "idx_medicines_unit");

            entity.HasIndex(e => e.MedicineCode, "medicines_medicine_code_key").IsUnique();

            entity.HasIndex(e => e.MedicineName, "medicines_medicine_name_key").IsUnique();

            entity.Property(e => e.MedicineId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("medicine_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.MedicineCode)
                .HasMaxLength(20)
                .HasColumnName("medicine_code");
            entity.Property(e => e.MedicineName)
                .HasMaxLength(100)
                .HasColumnName("medicine_name");
            entity.Property(e => e.MedicineStatusId).HasColumnName("medicine_status_id");
            entity.Property(e => e.MedicineTypeId).HasColumnName("medicine_type_id");
            entity.Property(e => e.MedicineUnitId).HasColumnName("medicine_unit_id");
            entity.Property(e => e.SellPrice)
                .HasPrecision(18)
                .HasColumnName("sell_price");
            entity.Property(e => e.StockQuantity)
                .HasDefaultValue(0)
                .HasColumnName("stock_quantity");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MedicineCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("medicines_created_by_fkey");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.MedicineDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("medicines_deleted_by_fkey");

            entity.HasOne(d => d.MedicineStatus).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.MedicineStatusId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("medicines_medicine_status_id_fkey");

            entity.HasOne(d => d.MedicineType).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.MedicineTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("medicines_medicine_type_id_fkey");

            entity.HasOne(d => d.MedicineUnit).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.MedicineUnitId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("medicines_medicine_unit_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.MedicineUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("medicines_updated_by_fkey");
        });

        modelBuilder.Entity<MedicineStatus>(entity =>
        {
            entity.HasKey(e => e.MedicineStatusId).HasName("medicine_statuses_pkey");

            entity.ToTable("medicine_statuses");

            entity.HasIndex(e => e.MedicineStatusName, "medicine_statuses_medicine_status_name_key").IsUnique();

            entity.Property(e => e.MedicineStatusId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("medicine_status_id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.MedicineStatusName)
                .HasMaxLength(50)
                .HasColumnName("medicine_status_name");
        });

        modelBuilder.Entity<MedicineType>(entity =>
        {
            entity.HasKey(e => e.MedicineTypeId).HasName("medicine_types_pkey");

            entity.ToTable("medicine_types");

            entity.HasIndex(e => e.MedicineTypeName, "medicine_types_medicine_type_name_key").IsUnique();

            entity.Property(e => e.MedicineTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("medicine_type_id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.MedicineTypeName)
                .HasMaxLength(50)
                .HasColumnName("medicine_type_name");
        });

        modelBuilder.Entity<MedicineUnit>(entity =>
        {
            entity.HasKey(e => e.MedicineUnitId).HasName("medicine_units_pkey");

            entity.ToTable("medicine_units");

            entity.HasIndex(e => e.MedicineUnitName, "medicine_units_medicine_unit_name_key").IsUnique();

            entity.Property(e => e.MedicineUnitId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("medicine_unit_id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.MedicineUnitName)
                .HasMaxLength(50)
                .HasColumnName("medicine_unit_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "roles_role_name_key").IsUnique();

            entity.Property(e => e.RoleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("role_id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("rooms_pkey");

            entity.ToTable("rooms");

            entity.Property(e => e.RoomId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("room_id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.RoomName)
                .HasMaxLength(50)
                .HasColumnName("room_name");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Status).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("rooms_status_id_fkey");
        });

        modelBuilder.Entity<RoomStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("room_statuses_pkey");

            entity.ToTable("room_statuses");

            entity.Property(e => e.StatusId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("status_id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("services_pkey");

            entity.ToTable("services");

            entity.HasIndex(e => e.ServiceName, "idx_services_name");

            entity.HasIndex(e => e.ServiceTypeId, "idx_services_type");

            entity.HasIndex(e => e.ServiceCode, "services_service_code_key").IsUnique();

            entity.Property(e => e.ServiceId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("service_id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Price)
                .HasPrecision(18)
                .HasColumnName("price");
            entity.Property(e => e.ServiceCode)
                .HasMaxLength(20)
                .HasColumnName("service_code");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(150)
                .HasColumnName("service_name");
            entity.Property(e => e.ServiceTypeId).HasColumnName("service_type_id");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.Services)
                .HasForeignKey(d => d.ServiceTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("services_service_type_id_fkey");
        });

        modelBuilder.Entity<ServiceType>(entity =>
        {
            entity.HasKey(e => e.ServiceTypeId).HasName("service_types_pkey");

            entity.ToTable("service_types");

            entity.Property(e => e.ServiceTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("service_type_id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.ServiceTypeName)
                .HasMaxLength(50)
                .HasColumnName("service_type_name");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("suppliers_pkey");

            entity.ToTable("suppliers");

            entity.HasIndex(e => e.ContactEmail, "suppliers_contact_email_key").IsUnique();

            entity.HasIndex(e => e.ContactPhone, "suppliers_contact_phone_key").IsUnique();

            entity.HasIndex(e => e.SupplierCode, "suppliers_supplier_code_key").IsUnique();

            entity.Property(e => e.SupplierId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("supplier_id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(15)
                .HasColumnName("contact_phone");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.SupplierCode)
                .HasMaxLength(20)
                .HasColumnName("supplier_code");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(150)
                .HasColumnName("supplier_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SupplierCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("suppliers_created_by_fkey");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.SupplierDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("suppliers_deleted_by_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.SupplierUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("suppliers_updated_by_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.CreatedBy, "idx_users_created_by");

            entity.HasIndex(e => e.DeletedAt, "idx_users_deleted_at");

            entity.HasIndex(e => e.Email, "idx_users_email_unique")
                .IsUnique()
                .HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => e.GenderId, "idx_users_gender");

            entity.HasIndex(e => e.StatusId, "idx_users_status");

            entity.HasIndex(e => e.Username, "idx_users_username_unique")
                .IsUnique()
                .HasFilter("(deleted_at IS NULL)");

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.MustChangePassword)
                .HasDefaultValue(false)
                .HasColumnName("must_change_password");
            entity.Property(e => e.MustUpdateAccount)
                .HasDefaultValue(true)
                .HasColumnName("must_update_account");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_created_by_fkey");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.InverseDeletedByNavigation)
                .HasForeignKey(d => d.DeletedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_deleted_by_fkey");

            entity.HasOne(d => d.Gender).WithMany(p => p.Users)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("users_gender_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("users_status_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_updated_by_fkey");
        });

        modelBuilder.Entity<UserGender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("user_genders_pkey");

            entity.ToTable("user_genders");

            entity.Property(e => e.GenderId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("gender_id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.GenderName)
                .HasMaxLength(10)
                .HasColumnName("gender_name");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("user_roles_pkey");

            entity.ToTable("user_roles");

            entity.HasIndex(e => e.RoleId, "idx_user_roles_role");

            entity.HasIndex(e => e.UserId, "idx_user_roles_user");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("assigned_at");
            entity.Property(e => e.AssignedBy).HasColumnName("assigned_by");

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.UserRoleAssignedByNavigations)
                .HasForeignKey(d => d.AssignedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("user_roles_assigned_by_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("user_roles_role_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_roles_user_id_fkey");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("user_statuses_pkey");

            entity.ToTable("user_statuses");

            entity.Property(e => e.StatusId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("status_id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
