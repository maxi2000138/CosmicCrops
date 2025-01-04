using _Project.Scripts._Infrastructure.StateMachine.Machine;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts._Infrastructure.StateMachine.States
{
  public sealed class StateLoadTargetScene : IEnterState
  {
    private readonly string _startScene;

    public StateLoadTargetScene(string startScene)
    {
      _startScene = startScene;
    }


    UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      if (_startScene == Scenes.GAME)
      {
        gameStateMachine.Enter<StateLoadGameScene,int>(1);
      }
      else if (_startScene == Scenes.BOOTSTRAP || _startScene == Scenes.MENU)
      {
        gameStateMachine.Enter<StateLoadMenuScene>();
      }
    
      return UniTask.CompletedTask;
    }
  }
}