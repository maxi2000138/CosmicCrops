﻿using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Factories.Systems;
using _Project.Scripts.Infrastructure.GUI;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  public class StateGameBootstrap : IEnterState
  {
    private readonly IGameFactory _gameFactory;
    private readonly LevelModel _levelModel;
    private readonly IGuiService _guiService;
    private readonly ICameraService _cameraService;
    private readonly IAssetProvider _assetProvider;

    public StateGameBootstrap(IGameFactory gameFactory, LevelModel levelModel, 
      IGuiService guiService, ICameraService cameraService, IAssetProvider assetProvider)
    {
      _gameFactory = gameFactory;
      _levelModel = levelModel;
      _guiService = guiService;
      _cameraService = cameraService;
      _assetProvider = assetProvider;
    }
    

    public async UniTask Enter(IGameStateMachine gameStateMachine)
    {
      CleanupWorld();
      await _gameFactory.CreateLevel();
      
      gameStateMachine.Enter<StateLobby>();
    }
    
    private void CleanupWorld()
    {
      _assetProvider.Cleanup();
      _levelModel.Cleanup();
      _guiService.Cleanup();
      _cameraService.Cleanup();
    }
  }
}