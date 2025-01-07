using _Project.Scripts._Infrastructure.Camera;
using _Project.Scripts._Infrastructure.ComponentSystemsCore.Systems;
using _Project.Scripts._Infrastructure.Factories.Game;
using _Project.Scripts.Game.Character.Components;
using _Project.Scripts.Game.Level.Components;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.Character.Systems
{
  public class CharacterSpawnerSystem : SystemComponent<CharacterSpawnerComponent>
  {
    private IGameFactory _gameFactory;
    private ICameraService _cameraService;

    [Inject]
    private void Construct(IGameFactory gameFactory, ICameraService cameraService)
    {
      _cameraService = cameraService;
      _gameFactory = gameFactory;
    }
    
    protected override void OnEnableComponent(CharacterSpawnerComponent component)
    {
      base.OnEnableComponent(component);
      
      CreateCharacter(component).Forget();
    }
    
    private async UniTaskVoid CreateCharacter(CharacterSpawnerComponent component)
    {
      CharacterComponent character = await _gameFactory.CreateCharacter(component.Position, component.transform.parent);
            
      SetCameraTarget(character);
    }
    
    private void SetCameraTarget(CharacterComponent character)
    {
      _cameraService.SetTarget(character.transform);
    }
  }
}