using _Project.Scripts.UI.Screens;

namespace _Project.Scripts.UI.GUIService
{
    public interface IGuiService
    {
        StaticCanvas StaticCanvas { get; }
        float ScaleFactor { get; }
        void Push(BaseScreen screen);
        void Pop();
        void CleanUp();
    }
}