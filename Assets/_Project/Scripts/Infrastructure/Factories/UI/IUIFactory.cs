using System.Threading.Tasks;
using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Game.Entities._Components.UI;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.UI.Screens;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factories.UI
{
    public interface IUIFactory
    {
        UniTask<BaseScreen> CreateScreen(ScreenType type);
        UniTask<BaseScreen> CreatePopUp(ScreenType type);
        UniTask<EnemyHealthViewComponent> CreateEnemyHealth(IEnemy enemy, Transform parent);
    }
}