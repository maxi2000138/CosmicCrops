using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Menu.Infrastructure._Presets;

namespace _Project.Scripts.Menu.Features.RenderTexture.Factory
{
    public class RenderTextureFactory : IRenderTextureFactory
    {
        private readonly IStaticDataService _staticDataService;

        public RenderTextureFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        UnityEngine.RenderTexture IRenderTextureFactory.CreateRenderTexture()
        {
            RenderTextureSettings data = _staticDataService.TexturePreset().RenderTextureSettings;
            UnityEngine.RenderTexture renderTexture = new UnityEngine.RenderTexture(data.Resolution.x, data.Resolution.y, 
                data.ColorFormat, data.DepthStensilFormat);
            renderTexture.Create();
            return renderTexture;
        }
    }
}
