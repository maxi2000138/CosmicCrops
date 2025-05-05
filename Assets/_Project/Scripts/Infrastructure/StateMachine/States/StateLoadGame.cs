using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateLoadGame : IEnterState
  {
    private readonly ISceneLoaderService _sceneLoaderService;

    public StateLoadGame(ISceneLoaderService sceneLoaderService)
    {
      _sceneLoaderService = sceneLoaderService;
    }

    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      await _sceneLoaderService.Load(Scenes.GAMEPLAY);
    }
  }

}