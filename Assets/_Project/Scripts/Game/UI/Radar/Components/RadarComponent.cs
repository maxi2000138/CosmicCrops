using _Project.Scripts.Infrastructure.Systems.Components;
using R3;
using UnityEngine;

public class RadarComponent : MonoComponent<RadarComponent>
{
  [SerializeField] private LineRenderer _lineRenderer;
  [SerializeField] private float _scale = 0.02f;
  [SerializeField] private float _width = 0.2f;

  public LineRenderer LineRenderer => _lineRenderer;
  public float Scale => _scale; 
  public float Width => _width;
  public float Radius { get; private set; }
        
  public ReactiveCommand Draw { get; } = new();
  public ReactiveCommand Clear { get; } = new();

  public void SetRadius(float radius) => Radius = radius;
}
