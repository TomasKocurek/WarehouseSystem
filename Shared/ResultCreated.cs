namespace Shared;

public class ResultCreated<TKey>
{
    public TKey Id { get; set; }
    public ResultCreated(TKey id)
    {
        Id = id;
    }
}
