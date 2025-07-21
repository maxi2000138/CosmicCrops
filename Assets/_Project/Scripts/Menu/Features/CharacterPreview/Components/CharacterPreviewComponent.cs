using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Menu.Features.CharacterPreview.Components;
using UnityEngine;

public class CharacterPreviewComponent : MonoComponent<CharacterPreviewComponent>
{
    [SerializeField] private CharacterPreviewModelComponent _characterPreviewModel;
    [SerializeField] private CharacterPreviewAnimatorComponent _characterPreviewAnimator;
    [SerializeField] private Camera _camera;

    public CharacterPreviewModelComponent CharacterPreviewModel => _characterPreviewModel;
    public CharacterPreviewAnimatorComponent CharacterPreviewAnimator => _characterPreviewAnimator;
    public Camera Camera => _camera;
}
