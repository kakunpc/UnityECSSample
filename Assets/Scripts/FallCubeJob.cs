using System.Threading;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class FallCubeJob : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new JobFall {dt = Time.DeltaTime};
        return  job.Schedule(this, inputDeps);
    }

    struct JobFall : IJobForEach<Translation, Rotation, FallCubeData>
    {
        public float dt;
        public void Execute(ref Translation pos, ref Rotation rot, ref FallCubeData data)
        {
            pos.Value.y -= data.fallSpeed * dt;
            if (pos.Value.y <= -100)
                pos.Value.y = 100f;
            rot.Value = math.mul(Quaternion.Euler(data.rotationSpeed* dt,data.rotationSpeed* dt,data.rotationSpeed* dt), rot.Value);
        }
    }
}
