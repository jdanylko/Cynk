namespace CynkDemo.CynkLib;

public class CynkWithGuid<T>(IList<T> source, Func<T, Guid> key)
    : BaseCynk<T>(source)
    where T : class
{
    public override CynkResult<T> Sync(IList<T> target) =>
        new()
        {
            Added = target
                .Where(e => Source.All(r => key(e) != key(r)))
                .ToList(),
            Updated = target.Where(e =>
                    Compare(e, Source.FirstOrDefault(r => key(e) == key(r))!))
                .ToList(),
            Deleted = Source
                .Where(e => target.All(r => key(e) != key(r)))
                .ToList()
        };
}