using System;
using System.Collections.Generic;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;
using _Project.Scripts.Game.Features.AI.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.Services.AIReporter
{
  public interface IAIReporter
  {
    event Action<DecisionDetails> DecisionDetailsReported;
    event Action<DecisionScore> DecisionScoreReported;
    void ReportDecisionDetails(IUnit unit, BattleAction battleAction, List<ScoreFactor> scoreFactors);
    void ReportDecisionScores(IUnit unit, List<ScoredAction> choices);
  }
}