using System;
using System.Linq.Expressions;

namespace _Project.Scripts.Utils.Expressions
{
  public static class CreateInstance
  {
    public static Func<object> Do(Type type)
    {
      var ctor = type.GetConstructor(Type.EmptyTypes);

      if (ctor == null)
        throw new InvalidOperationException($"Тип {type} не имеет конструктора без параметров.");

      var newExpression = Expression.New(ctor);

      return Expression.Lambda<Func<object>>(newExpression).Compile();
    }
  }
}