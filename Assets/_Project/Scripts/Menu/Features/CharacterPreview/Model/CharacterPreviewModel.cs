using System;
using _Project.Scripts.Infrastructure.GUI;
using _Project.Scripts.Menu.Features.RenderTexture.Factory;
using _Project.Scripts.Menu.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer.Unity;

namespace _Project.Scripts.Menu.Features.CharacterPreview.Model
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class CharacterPreviewModel : IInitializable, IDisposable
    {
        private readonly IMenuFactory _menuFactory;
        private readonly IGuiService _guiService;
        private readonly IRenderTextureFactory _renderTextureFactory;

        public CharacterPreviewComponent CharacterPreview { get; private set; }
        public UnityEngine.RenderTexture RenderTexture { get; private set; }

        public CharacterPreviewModel(IMenuFactory menuFactory, IGuiService guiService, IRenderTextureFactory renderTextureFactory)
        {
            _menuFactory = menuFactory;
            _guiService = guiService;
            _renderTextureFactory = renderTextureFactory;
        }
        
        void IInitializable.Initialize()
        {
            InitCharacterPreview().Forget();
        }

        private async UniTaskVoid InitCharacterPreview()
        {
            RenderTexture = _renderTextureFactory.CreateRenderTexture();
            CharacterPreview = await _menuFactory.CreateCharacterPreview();
            CharacterPreview.Camera.targetTexture = RenderTexture;
            CharacterPreview.Camera.orthographicSize *= _guiService.ScaleFactor;
            CharacterPreview.Camera.enabled = true;
        }
        
        void IDisposable.Dispose()
        {
            RenderTexture.Release();
            RenderTexture = null;
        }
    }
}