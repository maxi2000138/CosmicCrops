using _Project.Scripts._Infrastructure.MVVM.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.MainMenu.UI.Screen
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