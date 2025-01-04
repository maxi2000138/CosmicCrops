using _Project.Scripts._Infrastructure.SceneLoader;
using _Project.Scripts._Infrastructure.Scopes;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts._Infrastructure
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

    private ProjectEntryPoint()
    {
    
    }

    private async UniTaskVoid RunGame()
    {
      var sceneName = SceneManager.GetActiveScene().name;

      await new SceneLoaderService().Load(Scenes.BOOTSTRAP);

      Object.FindAnyObjectByType<BootstrapScope>().SetupAndBuild(sceneName);
    }
  }
}