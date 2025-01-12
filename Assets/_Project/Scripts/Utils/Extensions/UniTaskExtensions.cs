using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Utils.Extensions
{
  public static class UniTaskExtensions
  {
    public static UniTask.Awaiter GetAwaiter(this UniTask? uniTask)
    {
      if (!uniTask.HasValue)
      {
        return UniTask.CompletedTask.GetAwaiter();
      }
        
      return uniTask.Value.GetAwaiter();
    }
  }
}