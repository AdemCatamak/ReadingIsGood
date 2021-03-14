namespace RIG.Shared.Specification
{
    public interface ISpecification<in T>
    {
        bool IsSatisfied(T obj);
    }
}