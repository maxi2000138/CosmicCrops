using System.Threading.Tasks;
using _Project.Scripts.Game.Features.Units._Components.UI;
using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Game.UI.PointerArrow.Components;
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
        UniTask<PointerArrowComponent> CreatePointerArrow(Transform parent);
    }
}