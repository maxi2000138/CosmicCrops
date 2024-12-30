using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.SceneLoader
{
    public interface ISceneLoaderService
    {
        UniTask Load(string name);
    }
}