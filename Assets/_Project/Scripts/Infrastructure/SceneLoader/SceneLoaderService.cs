using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Infrastructure.SceneLoader
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class SceneLoaderService : ISceneLoaderService
    {
        public async UniTask Load(string name)
        {
            if (SceneManager.GetActiveScene().name.Equals(name))
                return;
            
            await Addressables.LoadSceneAsync(name);
        }
    }
}