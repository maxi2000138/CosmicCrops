using _Project.Scripts.Infrastructure.LifeTime.EntryPoints.Core;
using _Project.Scripts.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine.States;

namespace _Project.Scripts.Infrastructure.LifeTime.EntryPoints
{
  public class MenuEntryPoint : EntryPointBase
  {
    private readonly IGameStateMachine _gameStateMachine;
    
    public MenuEntryPoint(IGameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
    }

    protected override void Entry()
    {
      base.Entry();
      
      _gameStateMachine.Enter<StateMenuBootstrap>();
    }
  }
}