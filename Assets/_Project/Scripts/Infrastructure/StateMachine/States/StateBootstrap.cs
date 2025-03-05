using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Haptic;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.Pool.Service;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.Time;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateBootstrap : IEnterState
  {
    private readonly ILoadingCurtainService _loadingCurtain;
    private readonly IStaticDataService _staticDataService;
    private readonly IJoystickService _joystickService;
    private readonly IHapticService _hapticService;
    private readonly IPoolProvider _poolProvider;
    private readonly ICameraService _cameraService;
    private readonly IAssetService _assetService;
    private readonly ITimeService _time;


    public StateBootstrap(IStaticDataService staticDataService, IAssetService assetService, 
      IJoystickService joystickService, ICameraService cameraService, ILoadingCurtainService loadingCurtain,
      ITimeService time, IHapticService hapticService, IPoolProvider poolProvider)
    {
      _assetService = assetService;
      _joystickService = joystickService;
      _cameraService = cameraService;
      _loadingCurtain = loadingCurtain;
      _time = time;
      _hapticService = hapticService;
      _poolProvider = poolProvider;
      _staticDataService = staticDataService;
    }


    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      _loadingCurtain.Show();
      
      LoadResources();
      await InitAsset();
      InitJoystick();
      InitCameraService();
      InitHaptic();
      InitPool();
      
      EnterStateLoadProgressState(gameStateMachine);
    }

    private void LoadResources() => _staticDataService.Load();
    private async UniTask InitAsset() => await _assetService.Init();
    private void InitJoystick() => _joystickService.Init(_time);
    private void InitCameraService() => _cameraService.Init();
    private void InitHaptic() => _hapticService.Init();
    private void InitPool() => _poolProvider.Init();


    private void EnterStateLoadProgressState(IGameStateMachine gameStateMachine) => 
      gameStateMachine.Enter<StateLoadConfigs>();
  }
}