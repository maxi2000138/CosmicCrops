using System.Collections.Generic;
using _Project.Scripts.Game.UI;
using _Project.Scripts.Game.UI.Canvas;
using _Project.Scripts.Game.UI.Screens;
using _Project.Scripts.Infrastructure.Camera;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Infrastructure.GUI
{
    public sealed class GuiService : MonoBehaviour, IGuiService
    {
        [SerializeField] private StaticCanvas _staticCanvas;
        
        private ICameraService _cameraService;

        private readonly Stack<BaseScreen> _screens = new ();

        [Inject]
        private void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }
        
        StaticCanvas IGuiService.StaticCanvas => _staticCanvas;
        
        float IGuiService.ScaleFactor => _staticCanvas.Canvas.scaleFactor;

        void IGuiService.Push(BaseScreen screen)
        {
            if (_screens.TryPeek(out BaseScreen oldScreen))
            {
                oldScreen.SetActive(false);
            }
            
            _cameraService.ActivateCamera(screen.GetScreenType());
            
            _screens.Push(screen);
        }

        void IGuiService.Pop()
        {
            if (_screens.TryPop(out BaseScreen oldScreen))
            {
                Destroy(oldScreen.gameObject);
            }
            
            if (_screens.TryPeek(out BaseScreen screen))
            {
                _cameraService.ActivateCamera(screen.GetScreenType());

                screen.SetActive(true);
            }
        }

        void IGuiService.Cleanup()
        {
            foreach (BaseScreen screen in _screens)
            {
                Destroy(screen.gameObject);
            }
            
            _screens.Clear();
        }
    }
}