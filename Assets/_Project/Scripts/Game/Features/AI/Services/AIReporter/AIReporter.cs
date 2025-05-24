using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;
using _Project.Scripts.Game.Features.AI.UtilityAI;
using _Project.Scripts.Game.Features.Level.Model;

namespace _Project.Scripts.Game.Features.AI.Services.AIReporter
{
  public class AIReporter : IAIReporter
  {
    private readonly LevelModel _levelModel;
    
    public event Action<DecisionDetails> DecisionDetailsReported;
    public event Action<DecisionScore> DecisionScoreReported;

    public AIReporter(LevelModel levelModel)
    {
      _levelModel = levelModel;
    }
    
    public void ReportDecisionDetails(BattleAction battleAction, ITarget target, List<ScoreFactor> scoreFactors)
    {
      DecisionDetails decisionDetails = new DecisionDetails
      {
        ProducerName = $"{ battleAction.Producer }",
        TargetName = $"{ target }",
        ActionName = $"{ battleAction.ActionType }",
        
        Scores = scoreFactors,
        FormattedLine = string.Join(Environment.NewLine, 
          scoreFactors.OrderByDescending(x => x.Score)
            .Select(x => x.ToString())
            .ToArray()),
      };
      
      DecisionDetailsReported?.Invoke(decisionDetails);
    }

    public void ReportDecisionScores(ITarget producer, List<ScoredAction> choices)
    {
      DecisionScore decisionScore = new DecisionScore
      {
        ProducerName = $"{ producer }",
        Choices = choices,
        
        FormattedLine = string.Join(Environment.NewLine, 
          choices.OrderByDescending(x => x.Score)
            .Select(x => x.ToString())
            .ToArray()),
      };
      
      DecisionScoreReported?.Invoke(decisionScore);
    }
  }
}