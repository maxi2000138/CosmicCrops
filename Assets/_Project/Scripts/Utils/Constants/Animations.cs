using UnityEngine;

namespace _Project.Scripts.Utils.Constants
{
  public static class Animations
  {
    public static int Attack => Animator.StringToHash(nameof(Attack));
    public static int AttackSpeed => Animator.StringToHash(nameof(AttackSpeed));
    public static int Velocity => Animator.StringToHash(nameof(Velocity));
    public static int Collect => Animator.StringToHash(nameof(Collect));
    public static int Death => Animator.StringToHash(nameof(Death));
    public static int Victory => Animator.StringToHash(nameof(Victory));
  }
}