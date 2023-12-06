
namespace CynkDemo.CynkLib;

public class CynkResult<T> where T : class
{
    public IList<T> Added { get; set; } = new List<T>();
    public IList<T> Updated { get; set; } = new List<T>();
    public IList<T> Deleted { get; set; } = new List<T>();
}
