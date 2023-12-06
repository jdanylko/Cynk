using CynkDemo.Extensions;

namespace CynkDemo.CynkLib;

public abstract class BaseCynk<T>(IList<T> source) where T : class
{
    protected IList<T> Source { get; } = source;

    public virtual CynkResult<T> Sync(IList<T> target) => null!;

    protected bool Compare(T target, T source)
    {
        if (target.GetType().IsSimpleType())
        {
            return target == source;
        }

        if (source == null) return false;

        var sourceProperties = source.ToPropertyDictionary();
        var targetProperties = target.ToPropertyDictionary();

        return sourceProperties
            .Select(sourceProperty => new
            {
                sourceProperty,
                targetProperty = targetProperties
                    .First(r => r.Key == sourceProperty.Key)
            })
            .Where(t => !t.targetProperty.Value.Equals(t.sourceProperty.Value))
            .Select(t => t.sourceProperty)
            .Any();
    }
}



