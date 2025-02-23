using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Game.UI.Screens
{
    public sealed class GameScreen : BaseScreen
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            
            Show().Forget();
        }

        public override ScreenType GetScreenType() => ScreenType.Game;
    }
}