using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts.Utils.Extensions
{
  public static class EnumerableExtensions

  {
    public static IEnumerable<T> WithoutOrAll<T>(this IEnumerable<T> source, T possibleExclude)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      if (possibleExclude is T itemToExclude && itemToExclude != null)
        return source.Where(e => !EqualityComparer<T>.Default.Equals(e, itemToExclude));

      return source;
    }
    
    public static T FindMax<T, TComp>(this IEnumerable<T> enumerable, Func<T, TComp> selector) where TComp : IComparable<TComp> => Find(enumerable, selector, false);
    public static T FindMin<T, TComp>(this IEnumerable<T> enumerable, Func<T, TComp> selector) where TComp : IComparable<TComp> => Find(enumerable, selector, true);
    
    private static T Find<T, TComp>(IEnumerable<T> enumerable, Func<T, TComp> selector, bool selectMin) where TComp : IComparable<TComp>
    {
      if (enumerable == null)
        return default;

      var first = true;
      T selected = default(T);
      TComp selectedComp = default(TComp);

      foreach (T current in enumerable)
      {
        TComp comp = selector(current);
        if (first)
        {
          first = false;
          selected = current;
          selectedComp = comp;
          continue;
        }

        int res = selectMin
          ? comp.CompareTo(selectedComp)
          : selectedComp.CompareTo(comp);

        if (res < 0)
        {
          selected = current;
          selectedComp = comp;
        }
      }

      return selected;
    }
  }
}