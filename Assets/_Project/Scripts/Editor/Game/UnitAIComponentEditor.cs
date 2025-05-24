using System.Collections.Generic;
using _Project.Scripts.Game._Editor;
using UnityEditor;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.AI.Services.AIReporter;
using _Project.Scripts.Game.Features.Level.Model;
using VContainer;

[CustomEditor(typeof(UnitAIComponent))]
public class UnitAIComponentEditor : Editor
{
    private static Dictionary<UnitAIComponent, DecisionDetails> _decisionDetails = new();
    private static Dictionary<UnitAIComponent, DecisionScore> _decisionScores = new();

    private static IAIReporter _aiReporter;
    private static LevelModel _levelModel;

    [Inject]
    private void Construct(IAIReporter aiReporter, LevelModel levelModel)
    {
        _aiReporter = aiReporter;
        _levelModel = levelModel;
    }

    private void OnEnable()
    {
        SetupServices();

        if (_aiReporter != null)
        {
            _aiReporter.DecisionDetailsReported += OnDecisionDetailsReported;
            _aiReporter.DecisionScoreReported += OnDecisionScoreReported;
        }
    }

    private void OnDisable()
    {
        if (_aiReporter != null)
        {
            _aiReporter.DecisionDetailsReported -= OnDecisionDetailsReported;
            _aiReporter.DecisionScoreReported -= OnDecisionScoreReported;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var unit = (UnitAIComponent)target;
        if (_aiReporter == null || _levelModel == null)
        {
            EditorGUILayout.HelpBox("No AI data", MessageType.Info);
            return;
        }

        if (_decisionDetails.TryGetValue(unit, out var details))
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("🧠 Last AI Decision Details", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Producer:", details.ProducerName);
            EditorGUILayout.LabelField("Target:", details.TargetName);
            EditorGUILayout.LabelField("Action:", details.ActionName);
            EditorGUILayout.LabelField("Scores:");
            EditorGUILayout.TextArea(details.FormattedLine);
        }

        if (_decisionScores.TryGetValue(unit, out var scores))
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("📊 Last AI Decision Scores", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Producer:", scores.ProducerName);
            EditorGUILayout.LabelField("Choices:");
            EditorGUILayout.TextArea(scores.FormattedLine);
        }
    }

    private static void OnDecisionDetailsReported(DecisionDetails details)
    {
        if (TryGetProducer(details.ProducerName, out UnitAIComponent unit))
            _decisionDetails[unit] = details;
    }

    private static void OnDecisionScoreReported(DecisionScore score)
    {
        if (TryGetProducer(score.ProducerName, out UnitAIComponent unit))
            _decisionScores[unit] = score;
    }

    private static bool TryGetProducer(string name, out UnitAIComponent unit)
    {
        unit = null;
        if (_levelModel == null) return false;

        foreach (var enemy in _levelModel.Enemies)
        {
            if (enemy.ToString() == name)
            {
                unit = enemy.AI;
                return true;
            }
        }
        
        return false;
    }

    private static void SetupServices()
    {
        _levelModel = EditorBridge.LevelModel;
        _aiReporter = EditorBridge.AiReporter;
    }
}
