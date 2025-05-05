using _Project.Scripts.Infrastructure.LifeTime.EntryPoints.Core;
using _Project.Scripts.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine.States;

namespace _Project.Scripts.Infrastructure.LifeTime.EntryPoints
{
  public class BootstrapEntryPoint : EntryPointBase
  {
    private readonly IGameStateMachine _gameStateMachine;
    
    public BootstrapEntryPoint(IGameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
    }
    
    protected override void Entry()
    {
      base.Entry();
      
      _gameStateMachine.Enter<StateProjectBootstrap>();
    }
  }
}