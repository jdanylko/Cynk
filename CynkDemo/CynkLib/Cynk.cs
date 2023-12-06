namespace CynkDemo.CynkLib;

public class Cynk<T>(IList<T> source) : BaseCynk<T>(source) 
    where T: class
{
    public override CynkResult<T> Sync(IList<T> target) =>
        new()
        {
            Added = target
                .Where(e => Source.All(r => e != r))
                .ToList(),
            Updated = new List<T>(),
            Deleted = Source
                .Where(e => target.All(r => e != r))
                .ToList()
        };
}
