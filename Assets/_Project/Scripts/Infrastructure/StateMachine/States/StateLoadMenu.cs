using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  public class StateLoadMenu : IEnterState
  {
    private readonly ISceneLoaderService _sceneLoaderService;

    public StateLoadMenu(ISceneLoaderService sceneLoaderService)
    {
      _sceneLoaderService = sceneLoaderService;
    }
    
    public async UniTask Enter(IGameStateMachine gameStateMachine)
    {
      await _sceneLoaderService.Load(Scenes.MENU);
    }
  }
}