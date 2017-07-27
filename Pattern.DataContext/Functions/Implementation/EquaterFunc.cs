using Pattern.DataContext.Functions.Contract;
using System;
using System.Collections.Generic;

namespace Pattern.DataContext.Functions.Implementation
{
    public class EquaterFunc<T, TProperty> : IEquaterFunc<T>
    {
        private readonly Func<T, TProperty> _func;

        public EquaterFunc(Func<T, TProperty> func)
        {
            _func = func;
        }

        public bool Equals(T x, T y)
        {
            //use EqualityComparer<TProperty>.Default to avoid teh boxing
            return EqualityComparer<TProperty>.Default.Equals(_func(x), _func(y));
        }

        public int GetHashCode(T obj)
        {
            TProperty value = _func(obj);
            return ReferenceEquals(value, null) ? 0 : value.GetHashCode();
        }
    }
}
