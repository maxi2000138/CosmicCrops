using _Project.Scripts.Game.Features.Level.Interface;
using _Project.Scripts.Game.Features.Loot.Interface;
using _Project.Scripts.Game.Features.Units._Interfaces;
using ObservableCollections;
using R3;

namespace _Project.Scripts.Game.Features.Level.Model
{
  public class LevelModel
  {
    private readonly ObservableList<ILoot> _loot = new ();
    private readonly ObservableList<IEnemy> _enemies = new ();

    public ReactiveCommand<bool> OnPause { get; } = new ReactiveCommand<bool>();
    public ReactiveProperty<IUnit> Target { get; } = new ReactiveProperty<IUnit>();
    public ReactiveProperty<ILoot> CurrentLoot { get; } = new ReactiveProperty<ILoot>();
    
      public ILevel Level { get; private set; }
      public ICharacter Character { get; private set; }

    public IReadOnlyObservableList<ILoot> Loot => _loot;
    public IReadOnlyObservableList<IEnemy> Enemies => _enemies;

    
    public void SetCharacter(ICharacter character) => Character = character;
    public void SetLevel(ILevel level) => Level = level;
    public void AddLoot(ILoot enemy) => _loot.Add(enemy);
    public void RemoveLoot(ILoot enemy) => _loot.Remove(enemy);
    public void AddEnemy(IEnemy enemy) => _enemies.Add(enemy);
    public void RemoveEnemy(IEnemy enemy) => _enemies.Remove(enemy);
  }
}