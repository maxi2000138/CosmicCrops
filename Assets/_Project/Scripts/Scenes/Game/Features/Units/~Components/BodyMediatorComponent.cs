using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Menu.Features.CharacterPreview.Components;
using UnityEngine;

namespace _Project.Scripts.Scenes.Game.Features.Units._Components
{
  public class BodyMediatorComponent : MonoComponent<BodyMediatorComponent>
  {
    [SerializeField] private SkinData[] _skins;
        
    public SkinData[] Skins => _skins;
  }
}