namespace CodeClash.Core.Interfaces;

public interface IModel<out TEntity>
{
    public Guid Id { get; }
    public TEntity GetEntity();
}