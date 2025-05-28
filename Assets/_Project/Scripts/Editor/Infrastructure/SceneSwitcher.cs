using _Project.Scripts.Infrastructure;
using _Project.Scripts.Infrastructure.SceneLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Editor.Infrastructure
{
  public class SceneSwitcher
  {
    private static SceneSwitcher _instance;

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutoStartGame()
    {
      _instance = new SceneSwitcher();
      _instance.LoadBootstrapScene().Forget();
    }
#endif
    
    private async UniTaskVoid LoadBootstrapScene()
    {
      await new SceneLoaderService().Load(Scenes.BOOTSTRAP);
    }
  }
}