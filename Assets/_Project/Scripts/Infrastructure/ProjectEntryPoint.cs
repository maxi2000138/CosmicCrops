using _Project.Scripts.Infrastructure.LifeTime.Scopes;
using _Project.Scripts.Infrastructure.SceneLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
  public class ProjectEntryPoint
  {
    private static ProjectEntryPoint _instance;
  
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutoStartGame()
    {
      _instance = new ProjectEntryPoint();
      _instance.RunGame().Forget();
    }

    private async UniTaskVoid RunGame()
    {
      await new SceneLoaderService().Load(Scenes.BOOTSTRAP);
      Object.FindAnyObjectByType<BootstrapScope>().Build();
    }
  }
}