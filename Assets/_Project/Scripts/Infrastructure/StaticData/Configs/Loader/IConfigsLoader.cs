using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.StaticData.Configs
{
  public interface IConfigsLoader
  {
    UniTask LoadConfigs(Action<float> onProgress);
  }
}