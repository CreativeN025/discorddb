using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    // Define the format we want to handle
    private readonly string _format = "yyyy-MM-dd HH:mm:ss";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string dateString = reader.GetString();

        // Try parsing the date with the custom format
        if (DateTime.TryParseExact(dateString, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            return result;
        }

        // If parsing fails, throw an exception
        throw new JsonException($"Unable to parse '{dateString}' as a valid DateTime.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // Write in a standard format, like ISO 8601
        writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
