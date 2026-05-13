namespace Application.Common.Errors;
public static class UserErrors
{
    public const string ERR_USERNAME_LENGTH =
        "Tên đăng nhập không được vượt quá 50 ký tự.";
    public const string ERR_PASSWORD_LENGTH =
        "Mật khẩu không được vượt quá 256 ký tự.";
    public const string ERR_FULLNAME_LENGTH =
        "Họ tên không được vượt quá 100 ký tự.";
    public const string ERR_EMAIL_LENGTH =
        "Email không được vượt quá 255 ký tự.";
    public const string ERR_PHONE_LENGTH =
        "Số điện thoại không được vượt quá 10 ký tự.";
    public const string ERR_ADDRESS_LENGTH =
        "Địa chỉ không được vượt quá 200 ký tự.";
    public const string ERR_USERNAME_EMPTY =
        "Tên đăng nhập không được để trống.";
    public const string ERR_PASSWORD_EMPTY =
        "Mật khẩu không được để trống.";
    public const string ERR_FULLNAME_EMPTY =
        "Họ tên không được để trống.";
    public const string ERR_EMAIL_EMPTY =
        "Email không được để trống.";
    public const string ERR_PHONE_EMPTY =
        "Số điện thoại không được để trống.";
    public const string ERR_GENDER_EMPTY =
        "Giới tính không được để trống.";
    public const string ERR_ADDRESS_EMPTY =
        "Địa chỉ không được để trống.";
    public const string ERR_NOT_FOUND =
        "Không tìm thấy tài khoản người dùng trong hệ thống.";
    public const string ERR_INCORRECT_PASSWORD =
        "Mật khẩu hiện tại không đúng.";
    public const string ERR_ISACTIVE =
        "Tài khoản người dùng chưa kích hoạt.";
}