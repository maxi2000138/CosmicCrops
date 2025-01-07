using _Project.Scripts._Infrastructure.AssetData;
using _Project.Scripts._Infrastructure.Camera;
using _Project.Scripts._Infrastructure.Curtain;
using _Project.Scripts._Infrastructure.Input;
using _Project.Scripts._Infrastructure.StateMachine.Machine;
using _Project.Scripts._Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace _Project.Scripts._Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateBootstrap : IEnterState
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IAssetService _assetService;
    private readonly IJoystickService _joystickService;
    private readonly ICameraService _cameraService;
    private readonly ILoadingCurtainService _loadingCurtain;


    public StateBootstrap(IStaticDataService staticDataService, IAssetService assetService, 
      IJoystickService joystickService, ICameraService cameraService, ILoadingCurtainService loadingCurtain)
    {
      _assetService = assetService;
      _joystickService = joystickService;
      _cameraService = cameraService;
      _loadingCurtain = loadingCurtain;
      _staticDataService = staticDataService;
    }


    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      _loadingCurtain.Show();
      
      LoadResources();
      await InitAsset();
      InitJoystick();
      InitCameraService();
      
      EnterStateLoadProgressState(gameStateMachine);
    }

    private void LoadResources() => _staticDataService.Load();
    private async UniTask InitAsset() => await _assetService.Init();
    private void InitJoystick() => _joystickService.Init();
    private void InitCameraService() => _cameraService.Init();


    private void EnterStateLoadProgressState(IGameStateMachine gameStateMachine) => 
      gameStateMachine.Enter<StateLoadProgress>();
  }
}