using UnityEngine;

namespace _Project.Scripts.Utils
{
  public static class Animations
  {
    public static int Velocity => Animator.StringToHash(nameof(Velocity));
    public static int Collect => Animator.StringToHash(nameof(Collect));
    public static int Death => Animator.StringToHash(nameof(Death));
    public static int Victory => Animator.StringToHash(nameof(Victory));
  }
}