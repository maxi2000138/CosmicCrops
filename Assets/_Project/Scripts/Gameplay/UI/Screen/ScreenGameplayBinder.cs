using _Project.Scripts.Gameplay.UI.Screen;
using _Project.Scripts.Infrastructure.MVVM.UI;
using UnityEngine;
using UnityEngine.UI;

namespace mBuilding.Scripts.Game.Gameplay.View.UI.ScreenGameplay
{
    public class ScreenGameplayBinder : WindowBinder<ScreenGameplayViewModel>
    {
        [SerializeField] private Button _btnPopupA;
        [SerializeField] private Button _btnGoToMenu;

        private void OnEnable()
        {
            _btnPopupA.onClick.AddListener(OnPopupAButtonClicked);
            _btnGoToMenu.onClick.AddListener(OnGoToMainMenuButtonClicked);
        }

        private void OnDisable()
        {
            _btnPopupA.onClick.RemoveListener(OnPopupAButtonClicked);
            _btnGoToMenu.onClick.RemoveListener(OnGoToMainMenuButtonClicked);
        }

        private void OnPopupAButtonClicked()
        {
            ViewModel.RequestOpenPopupA();
        }

        private void OnGoToMainMenuButtonClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }
    }
}