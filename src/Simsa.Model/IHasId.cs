namespace Simsa.Model;

public interface IHasId<out TId>
{
    public TId Id { get; }
}