using _Project.Scripts.UI.Screens;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts._Infrastructure.Factories.UI
{
    public interface IUIFactory
    {
        UniTask<BaseScreen> CreateScreen(ScreenType type);
        UniTask<BaseScreen> CreatePopUp(ScreenType type);
    }
}