using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using R3;
using UnityEngine;

public class RadarDrawSystem : SystemComponent<RadarComponent>
{
    protected override void OnEnableComponent(RadarComponent armament)
    {
        base.OnEnableComponent(armament);

        armament.Draw
            .Subscribe(_ => DrawCircle(armament))
            .AddTo(armament.LifetimeDisposable);

        armament.Clear
            .Subscribe(_ => Clear(armament))
            .AddTo(armament.LifetimeDisposable);
    }
    
    protected override void OnLateUpdate()
    {
        base.OnLateUpdate();
        
        Components.Foreach(SetQuaternionIdentity);
    }

    private void DrawCircle(RadarComponent component)
    {
        float offset = 0f;
        int size = Mathf.RoundToInt(1f / component.Scale + 1f);
                
        component.LineRenderer.positionCount = size;
        component.LineRenderer.widthMultiplier = component.Width;
                
        for (int i = 0; i < size; i++)
        {
            offset += 2f * Mathf.PI * component.Scale;
                    
            float x = component.Radius * Mathf.Sin(offset);
            float z = component.Radius * Mathf.Cos(offset);
                    
            component.LineRenderer.SetPosition(i, new Vector3(x, 0f, z));
        }
    }

    private void Clear(RadarComponent component) => component.LineRenderer.positionCount = 0;
    private void SetQuaternionIdentity(RadarComponent radar) => radar.transform.rotation = Quaternion.identity;
}
