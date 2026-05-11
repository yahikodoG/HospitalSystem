namespace Domain.Enums;

public enum UserStatus
{
    Active,
    EmailNotVerified,
    PhoneNotVerified,
    Locked,
    Deleted,
    PendingApproval,
    Restricted
}