using _Project.Scripts.Gameplay.UI.Screen;
using _Project.Scripts.Infrastructure.MVVM.UI;
using UnityEngine;
using UnityEngine.UI;

namespace mBuilding.Scripts.Game.Gameplay.View.UI.ScreenGameplay
{
    public class ScreenMainMenuBinder : WindowBinder<ScreenMainMenuViewModel>
    {
        [SerializeField] private Button _btnGoToGameplay;

        private void OnEnable()
        {
            _btnGoToGameplay.onClick.AddListener(OnGoToGameplayButtonClicked);
        }

        private void OnDisable()
        {
            _btnGoToGameplay.onClick.RemoveListener(OnGoToGameplayButtonClicked);
        }

        private void OnGoToGameplayButtonClicked()
        {
            ViewModel.RequestGoToGameplay();
        }
    }
}