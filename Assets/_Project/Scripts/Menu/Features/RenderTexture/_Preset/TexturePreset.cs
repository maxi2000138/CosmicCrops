using _Project.Scripts.Infrastructure.StaticData.Presets;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace _Project.Scripts.Menu.Infrastructure._Presets
{
  [CreateAssetMenu(fileName = nameof(TexturePreset), order = -1, menuName = "Presets/" + nameof(TexturePreset))]
  public class TexturePreset : BasePreset
  {
    public RenderTextureSettings RenderTextureSettings;
  }

  [System.Serializable]
  public struct RenderTextureSettings
  {
    public Vector2Int Resolution;
    public GraphicsFormat ColorFormat;
    public GraphicsFormat DepthStensilFormat;
  }
}