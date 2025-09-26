using System;
using R3;

namespace _Project.Scripts.Utils.Extensions
{
  public static class R3Extensions
  {
    public static Observable<T> First<T>(this Observable<T> source) => source.Take(1);
    public static Observable<T> First<T>(this Observable<T> source, Func<T, bool> predicate) => source.Where(predicate).Take(1);
    public static void SetValueAndForceNotify<T>(this ReactiveProperty<T> source, T value)
    {
      source.Value = value;
      source.ForceNotify();
    }
    public static Observable<TResult> ObserveEveryValueChanged<TSource, TResult>(this Observable<TSource> source, Func<TSource, TResult> selector)
    {
      return source
        .Select(selector)
        .DistinctUntilChanged();
    }
  }
}