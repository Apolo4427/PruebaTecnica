namespace PruebaTecnica1.Core.Models
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }
    }
}