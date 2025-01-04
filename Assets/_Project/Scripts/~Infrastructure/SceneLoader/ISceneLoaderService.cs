using Cysharp.Threading.Tasks;

namespace _Project.Scripts._Infrastructure.SceneLoader
{
    public interface ISceneLoaderService
    {
        UniTask Load(string name);
    }
}