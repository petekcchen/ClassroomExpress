namespace ClassroomExpress.Domain
{
    public interface IEntity<T>
    {
        T Id { get; }
    }
}