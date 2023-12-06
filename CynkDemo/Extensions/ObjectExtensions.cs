using System.Collections.Frozen;

namespace CynkDemo.Extensions;

public static class ObjectExtensions
{
    public static bool IsSimpleType(this Type type) =>
        type.IsPrimitive
        || type.IsValueType
        || new[]
        {
            typeof(string),
            typeof(decimal),
            typeof(bool),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid)
        }.Contains(type)
        || Convert.GetTypeCode(type) != TypeCode.Object;

    public static FrozenDictionary<string, string>
        ToPropertyDictionary(this object obj) =>
        obj.GetType().GetProperties()
            .Select(e =>
                new KeyValuePair<string, string>(
                    e.Name, e.GetValue(obj, null)!
                        .ToString() ?? string.Empty))
            .ToFrozenDictionary();
}