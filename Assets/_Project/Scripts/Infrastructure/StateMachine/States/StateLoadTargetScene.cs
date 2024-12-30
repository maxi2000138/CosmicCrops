using _Project.Scripts.Infrastructure.StateMachine.Machine;
using _Project.Scripts.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  public sealed class StateLoadTargetScene : IEnterState
  {
    private readonly IGameStateMachine _gameStateMachine;
    private readonly string _startScene;

    private IStaticDataService _staticDataService;

    public StateLoadTargetScene(IGameStateMachine gameStateMachine, string startScene)
    {
      _gameStateMachine = gameStateMachine;
      _startScene = startScene;
    }

    [Inject]
    private void Construct(IStaticDataService staticDataService)
    {
      _staticDataService = staticDataService;
    }


    UniTask IEnterState.Enter()
    {
      if (_startScene == Scenes.GAME)
      {
        _gameStateMachine.Enter<StateLoadGameScene,int>(1);
      }
      else if (_startScene == Scenes.BOOTSTRAP || _startScene == Scenes.MENU)
      {
        _gameStateMachine.Enter<StateLoadMenuScene>();
      }
    
      return UniTask.CompletedTask;
    }

    UniTask IExitState.Exit()
    {
      return UniTask.CompletedTask;
    }
  }
}