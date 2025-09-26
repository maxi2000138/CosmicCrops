using _Project.Scripts.Game.Features.Units.Enemy;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Menu.Features.CharacterPreview.Components
{
  public class CharacterPreviewAnimatorComponent : MonoComponent<CharacterPreviewAnimatorComponent>
  {
    [SerializeField] private AnimatorWrapper _animator;
        
    public AnimatorWrapper Animator => _animator;

  }
}