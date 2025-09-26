using _Project.Scripts.Game.Features.AI.Services.AIReporter;
using _Project.Scripts.Game.Features.Level.Model;

namespace _Project.Scripts.Game._Editor
{
  public static class EditorBridge
  {
    public static IAIReporter AiReporter { get; private set; }
    public static LevelModel LevelModel { get; private set; }
    
    public static void Init(IAIReporter aiReporter, LevelModel levelModel)
    {
      AiReporter = aiReporter;
      LevelModel = levelModel;
    }
  }
}