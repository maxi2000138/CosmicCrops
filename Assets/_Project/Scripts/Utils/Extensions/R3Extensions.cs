using R3;

namespace _Project.Scripts.Utils.Extensions
{
  public static class R3Extensions
  {
    public static Observable<T> First<T>(this Observable<T> source) => source.Take(1);
  }
}