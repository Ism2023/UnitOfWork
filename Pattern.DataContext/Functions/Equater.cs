using Pattern.DataContext.Functions.Contract;
using Pattern.DataContext.Functions.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pattern.DataContext.Functions
{
    public class Equater<T>
    {
        private readonly List<IEquaterFunc<T>> _equaterFuncs = new List<IEquaterFunc<T>>();
        private readonly Guid _guid = Guid.NewGuid();

        public bool Equals(T x, T y)
        {
            return _equaterFuncs.All(equaterFunc => equaterFunc.Equals(x, y));
        }

        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }

        public void AddEquaterFunc<TProperty>(Func<T, TProperty> equaterFunc)
        {
            _equaterFuncs.Add(new EquaterFunc<T, TProperty>(equaterFunc));
        }
    }
}
