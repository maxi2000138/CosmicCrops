using _Project.Scripts.Game.UI.Screens;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.Factories.UI
{
    public interface IUIFactory
    {
        UniTask<BaseScreen> CreateScreen(ScreenType type);
        UniTask<BaseScreen> CreatePopUp(ScreenType type);
    }
}