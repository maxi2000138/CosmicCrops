﻿using UnityEngine;

namespace _Project.Scripts.Utils.Constants
{
  public static class Layers
  {
    public static int Ground => 1 << LayerMask.NameToLayer(nameof(Ground));
    public static int Wall => 1 << LayerMask.NameToLayer(nameof(Wall));
  }
}