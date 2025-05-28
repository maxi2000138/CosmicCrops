using System.Collections.Generic;
using _Project.Scripts.Utils.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Game.Entities.Unit
{
  public class AnimatorWrapper : SerializedMonoBehaviour
  {
    [field: SerializeField] public Animator Animator { get; private set; }

    [SerializeField, ReadOnly] private Dictionary<int, float> _animationDurations = new ();
    
    private bool _isInit;

    private void Awake()
    {
      TryInit();
    }
    
    public void PlayAnimation(int animationHash, float transitionDuration = 0)
    {
      Animator.CrossFade(animationHash, transitionDuration);
    }
    
    public void SetSpeed(int animationHash, int animationSpeedHash, float duration)
    {
      Animator.SetFloat(animationSpeedHash, _animationDurations[animationHash] / duration);
    }
    
    public void SetAnimatorController(RuntimeAnimatorController animatorController)
    {
      Animator.runtimeAnimatorController = animatorController;
    }

    private void TryInit()
    {
      if (_isInit)
        return;

      FillAnimationDurations();

      _isInit = true;
    }

    private void FillAnimationDurations()
    {
#if UNITY_EDITOR
      _animationDurations = new ();
      var data = AnimatorEditorUtils.GetAnimationDurations(GetComponent<Animator>());
      foreach (var (hash, duration) in data) 
        _animationDurations.Add(hash, duration);
#endif
    }
  }
}