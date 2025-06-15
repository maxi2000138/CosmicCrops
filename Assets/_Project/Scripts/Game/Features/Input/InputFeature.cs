using _Project.Scripts.Game.Features.Input.Systems;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.Features.Input
{
  public class InputFeature : Feature
  {
    public InputFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new JoystickUpdateSystem());
    }
  }
}