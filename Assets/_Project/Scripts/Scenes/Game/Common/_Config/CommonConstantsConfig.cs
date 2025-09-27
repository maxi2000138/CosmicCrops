using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Infrastructure.StaticData.Configs.Data;
using _Project.Scripts.Utils.Parse;

namespace _Project.Scripts.Scenes.Game.Common._Config
{
  public class CommonConstantsConfig : BaseConfigConstants
  {
    public override string ConfigName => "CommonConstants";
    
    public ConfigPrefab EnemyPrefab => _enemyPrefab ??= StringParseUtils.ToPrefab(Data["enemy_prefab"]);
    private ConfigPrefab _enemyPrefab;
    
    public ConfigPrefab CharacterPrefab => _characterPrefab ??= StringParseUtils.ToPrefab(Data["character_prefab"]);
    private ConfigPrefab _characterPrefab;
    
    public int CharacterHealth => _characterHealth ??= int.Parse(Data["character_health"]);
    private int? _characterHealth;
    
    public float CharacterSpeed => _characterSpeed ??= float.Parse(Data["character_speed"]);
    private float? _characterSpeed;
    
    public float CharacterHeight => _characterHeight ??= float.Parse(Data["character_height"]);
    private float? _characterHeight;
  }
}