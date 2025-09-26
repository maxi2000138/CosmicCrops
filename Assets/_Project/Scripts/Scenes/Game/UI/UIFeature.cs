using _Project.Scripts.Game.UI.Haptic.Systems;
using _Project.Scripts.Game.UI.Pause.Systems;
using _Project.Scripts.Game.UI.PointerArrow.Systems;
using _Project.Scripts.Game.UI.Radar.Systems;
using _Project.Scripts.Game.UI.Settings;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.UI
{
  public class UIFeature : Feature
  {
    public UIFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new HapticButtonSystem());
      Add(new HapticActiveSystem());
                
      Add(new RadarDrawSystem());
      Add(new PointerArrowProviderSystem());
      Add(new PointerArrowUpdateSystem());
      
      Add(new PauseSystem());
                
      Add(new SettingsFeature(objectResolver));
    }
  }
}