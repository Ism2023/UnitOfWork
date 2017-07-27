namespace Pattern.DataContext.Functions.Contract
{
    public interface IEquaterFunc<T>
    {
        bool Equals(T x, T y);
        int GetHashCode(T obj);
    }
}
