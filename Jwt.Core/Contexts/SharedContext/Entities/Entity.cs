namespace Jwt.Core.Contexts.SharedContext.Entities;

public abstract class Entity : IEquatable<Guid>
{
    public Guid Id { get; } = Guid.NewGuid();

    public bool Equals(Guid id)
        => id == Id;

    public override int GetHashCode()
        => Id.GetHashCode();
}