using _Project.Scripts.Game.UI.Settings.Systems;
using _Project.Scripts.Game.UI.Settings.Toggle.Systems;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.UI.Settings
{
  public class SettingsFeature : Feature
  {
    public SettingsFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new SettingsButtonSystem());
      Add(new SettingsMediatorSystem());
      
      Add(new ToggleSystem());
    }
  }
}