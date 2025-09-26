using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Menu.Features.Shop.Systems;
using _Project.Scripts.Scenes.Menu.Features.Shop.Systems;
using VContainer;

namespace _Project.Scripts.Menu.Features
{
  public class MenuFeature : Feature
  {
    public MenuFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new ShopCharacterRendererSystem());
      Add(new ShopCharacterPreviewSystem());
    }
  }
}