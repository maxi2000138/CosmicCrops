using System.Collections.Generic;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Loot.Interface;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Level.Interface;
using ObservableCollections;
using R3;

namespace _Project.Scripts.Game.Level.Model
{
  public class LevelModel
  {
    private readonly ObservableList<ILoot> _loot = new ();
    private readonly ObservableList<IEnemy> _enemies = new ();

    public ILevel Level { get; private set; }
    public ICharacter Character { get; private set; }
    public ReactiveProperty<ITarget> Target { get; } = new ReactiveProperty<ITarget>();
    public ReactiveProperty<ILoot> CurrentLoot { get; } = new ReactiveProperty<ILoot>();
    public IReadOnlyObservableList<ILoot> Loot => _loot;
    public IReadOnlyObservableList<IEnemy> Enemies => _enemies;


    public void SetCharacter(ICharacter character) => Character = character;
    public void SetLevel(ILevel level) => Level = level;
    public void AddLoot(ILoot enemy) => _loot.Add(enemy);
    public void RemoveLoot(ILoot enemy) => _loot.Remove(enemy);
    public void AddEnemy(IEnemy enemy) => _enemies.Add(enemy);
    public void RemoveEnemy(IEnemy enemy) => _enemies.Remove(enemy);
   
    public void Cleanup()
    {
      Character = null;
      Level = null;
      _loot.Clear();
    }
  }
}