namespace Application.Common.Helpers;

public static class GenerateCode
{
    public static string CodeGenerator(
        string prefix,
        string? lastCode,
        int padding = 3)
    {
        if (string.IsNullOrWhiteSpace(lastCode))
            return $"{prefix}{1.ToString().PadLeft(padding, '0')}";

        var numberPart = lastCode.Substring(prefix.Length);

        var number = int.Parse(numberPart);

        number++;

        return $"{prefix}{number.ToString().PadLeft(padding, '0')}";
    }
}