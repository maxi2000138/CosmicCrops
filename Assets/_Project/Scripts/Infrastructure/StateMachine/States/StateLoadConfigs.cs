using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateLoadConfigs : IEnterState
  {
    private readonly IConfigsLoader _configsLoader;
    public StateLoadConfigs(IConfigsLoader configsLoader)
    {
      _configsLoader = configsLoader;
    }
    
    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      await _configsLoader.LoadConfigs(null);

      gameStateMachine.Enter<StateMenu>();
    }
  }
}