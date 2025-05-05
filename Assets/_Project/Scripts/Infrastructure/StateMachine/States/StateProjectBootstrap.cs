using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateProjectBootstrap : IEnterState
  {
    private readonly ILoadingCurtainService _loadingCurtain;
    
    public StateProjectBootstrap(ILoadingCurtainService loadingCurtain)
    {
      _loadingCurtain = loadingCurtain;
    }
    
    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      _loadingCurtain.Show();
      
      Application.targetFrameRate = 60; 
      QualitySettings.vSyncCount = 0;
      
      gameStateMachine.Enter<StateLoadStaticData>();
    }
  }
}