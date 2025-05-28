using UnityEngine;

namespace _Project.Scripts.Utils.Extensions
{
  public static class Mathematics
  {
    public static float Remap(float inputMin, float inputMax, float outputMin, float outputMax, float value)
    {
      return Mathf.Lerp(outputMin, outputMax, Mathf.InverseLerp(inputMin, inputMax, value));
    }
    
    public static Vector3 GenerateRandomPoint(float radius)
    {
      float angle = Random.Range(0f, 1f) * (2f * Mathf.PI) - Mathf.PI;
                    
      float x = Mathf.Sin(angle) * radius;
      float z = Mathf.Cos(angle) * radius;

      return new Vector3(x, 0f, z);
    }
  }
}