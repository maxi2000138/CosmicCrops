
using UnityEngine;

namespace _Project.Scripts.Utils.Extensions
{
  public static class MathExtensions
  {
    public static float HorizontalProjectedSqrDistance(this Vector3 from, Vector3 to)
    {
      var distanceVector = to - from;
      distanceVector.y = 0;
      
      return distanceVector.sqrMagnitude;
    }
  }
}