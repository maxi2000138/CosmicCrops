using _Project.Scripts._Infrastructure.UI;
using _Project.Scripts._Infrastructure.UI.Screens;

namespace CodeBase.Infrastructure.GUI
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