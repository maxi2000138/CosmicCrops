using System;
using System.Linq.Expressions;

namespace _Project.Scripts.Utils.Expressions
{
    public static class IsLessOrEqual<T>
    {
        public static readonly Func<T, T, bool> Do;

        static IsLessOrEqual()
        {
            var par1 = Expression.Parameter(typeof(T));
            var par2 = Expression.Parameter(typeof(T));

            var isLessOrEqual = Expression.LessThanOrEqual(par1, par2);

            Do = Expression.Lambda<Func<T, T, bool>>(isLessOrEqual, par1, par2).Compile();
        }
    }
}