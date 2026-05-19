using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public int? GenderId { get; set; }

    public string? Address { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? StatusId { get; set; }

    public bool MustChangePassword { get; set; }

    public bool MustUpdateAccount { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? DeletedByNavigation { get; set; }

    public virtual UserGender? Gender { get; set; }

    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<User> InverseDeletedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<User> InverseUpdatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<Medicine> MedicineCreatedByNavigations { get; set; } = new List<Medicine>();

    public virtual ICollection<Medicine> MedicineDeletedByNavigations { get; set; } = new List<Medicine>();

    public virtual ICollection<Medicine> MedicineUpdatedByNavigations { get; set; } = new List<Medicine>();

    public virtual ICollection<Room> RoomCreatedByNavigations { get; set; } = new List<Room>();

    public virtual ICollection<Room> RoomDeletedByNavigations { get; set; } = new List<Room>();

    public virtual ICollection<Room> RoomUpdatedByNavigations { get; set; } = new List<Room>();

    public virtual UserStatus? Status { get; set; }

    public virtual ICollection<Supplier> SupplierCreatedByNavigations { get; set; } = new List<Supplier>();

    public virtual ICollection<Supplier> SupplierDeletedByNavigations { get; set; } = new List<Supplier>();

    public virtual ICollection<Supplier> SupplierUpdatedByNavigations { get; set; } = new List<Supplier>();

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual ICollection<UserRole> UserRoleAssignedByNavigations { get; set; } = new List<UserRole>();

    public virtual ICollection<UserRole> UserRoleUsers { get; set; } = new List<UserRole>();
}
