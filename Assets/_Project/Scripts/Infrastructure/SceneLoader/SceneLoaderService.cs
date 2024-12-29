using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Loader
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class SceneLoaderService : ISceneLoaderService
    {
        public async UniTask Load(string name)
        {
            if (SceneManager.GetActiveScene().name.Equals(name))
                return;
            
            await Addressables.LoadSceneAsync(name).ToUniTask();
        }
    }
}