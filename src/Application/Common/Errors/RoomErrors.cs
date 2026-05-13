namespace Application.Common.Errors;

public static class RoomErrors
{
    public const string ERR_NAME_LENGTH =
        "Tên phòng không được vượt quá 50 ký tự.";

    public const string ERR_DESC_LENGTH =
        "Mô tả không được vượt quá 200 ký tự.";

    public const string ERR_NAME_EMPTY =
        "Tên phòng không được để trống.";

    public const string ERR_NOT_FOUND =
        "Không tìm thấy phòng.";

    public const string ERR_DUPLICATE =
        "Tên phòng đã tồn tại.";

    public const string ERR_IN_USE =
        "Phòng hiện đang được sử dụng và không thể xóa.";

    public const string ERR_CREATE_FAILED =
        "Không thể tạo phòng. Vui lòng thử lại sau.";

    public const string ERR_UPDATE_FAILED =
        "Không thể cập nhật phòng. Vui lòng thử lại sau.";

    public const string ERR_DELETE_FAILED =
        "Không thể xóa phòng. Vui lòng thử lại sau.";
}