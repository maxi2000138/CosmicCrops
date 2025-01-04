using Cysharp.Threading.Tasks;

namespace _Project.Scripts._Infrastructure.MVVM.UI
{
    public interface IWindowBinder
    {
        UniTask Bind(WindowViewModel viewModel);
        UniTask Close();
    }
}