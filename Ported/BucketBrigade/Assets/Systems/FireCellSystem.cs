﻿
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Collections;

public class FireCellSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var time = Time.ElapsedTime;
        FireSim fireSim = GetSingleton<FireSim>();

        EntityCommandBufferSystem sys = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
        var ecb = sys.CreateCommandBuffer().AsParallelWriter();

        Entities
            .ForEach((Entity entity, int entityInQueryIndex, ref Translation translation, ref URPMaterialPropertyBaseColor color, in FireCell fireCell) =>
            {
                var temperature = fireCell.Temperature;
                //temperature += (float)(Unity.Mathematics.math.sin(time)*0.1f);
                
                if (temperature < fireSim.FlashPoint)
                {
                    color.Value = new float4(math.lerp(new float3(125, 202, 117), new float3(255, 252, 131), temperature), 1.0f);
                    color.Value /= 255;
                    translation.Value.y = -1.6f;
                } else
                {
                    color.Value = new float4(1.0f, 0.0f, 0.0f, 0.0f);
                    translation.Value.y = temperature * 3.3f - 1.6f;
                    ecb.AddComponent(entityInQueryIndex, entity, new OnFire() { });
                }
                
            }).ScheduleParallel();

        sys.AddJobHandleForProducer(Dependency);
    }
}