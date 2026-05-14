namespace Application.Common.Errors;

public static class RoomErrors
{
    public const string ERR_NOT_FOUND =
        "Không tìm thấy phòng.";

    public const string ERR_DUPLICATE =
        "Tên phòng đã tồn tại.";

    public const string ERR_IN_USE =
        "Phòng hiện đang được sử dụng và không thể xóa.";
}