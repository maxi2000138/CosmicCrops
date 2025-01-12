using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.Input.Systems
{
  public class JoystickUpdateSystem : SystemBase
  {
    private IJoystickService _joystickService;

    [Inject]
    private void Construct(IJoystickService joystickService)
    {
      _joystickService = joystickService;
    }
        
    protected override void OnUpdate()
    {
      base.OnUpdate();
            
      _joystickService.Execute();
    }

  }
}