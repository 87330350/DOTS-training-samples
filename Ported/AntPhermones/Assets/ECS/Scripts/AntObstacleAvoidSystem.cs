﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

public class AntObstacleAvoidSystem : SystemBase
{
    //Query for getting all the world obstacles
    EntityQuery obstacleQuery;

    protected override void OnCreate()
    {
        //Cache our obstacle query and require it to return something for OnUpdate to run
        var obstacleQueryDesc = new EntityQueryDesc
        {
            All = new ComponentType[] { ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<Radius>() }
        };
        obstacleQuery = GetEntityQuery(obstacleQueryDesc);
        RequireForUpdate(obstacleQuery);
    }

    protected override void OnUpdate()
    {
        /*var obstRadiusArray = obstacleQuery.ToComponentDataArray<Radius>(Allocator.TempJob);
        var obstTranslationArray = obstacleQuery.ToComponentDataArray<Translation>(Allocator.TempJob);
       
        //Update all ant entities and check that we are not going to collide with
        //a obstacle
        Entities
            .WithNativeDisableParallelForRestriction(obstRadiusArray) //It's safe here because we are only reading from the array
            .WithNativeDisableParallelForRestriction(obstTranslationArray) //It's safe here because we are only reading from the array
            .WithAll<ObstacleAvoid>()
            .ForEach((ref Direction dir, ref Translation antTranslation) =>
            {

                //Check this entity for collisions with all other entites
                float dx, dy, sqrDist, dist;
                for(int i = 0; i < obstRadiusArray.Length; ++i)
                {
                    //Get difference in x and y, calculate the sqrd distance to the 
                    dx = antTranslation.Value.x - obstTranslationArray[i].Value.x;
                    dy = antTranslation.Value.z - obstTranslationArray[i].Value.z;
                    sqrDist = (dx * dx) + (dy * dy);

                    //If we are less than the sqrd distance away from the obstacle then reflect the ant
                    if(sqrDist < (obstRadiusArray[i].Value * obstRadiusArray[i].Value))
                    {
                        //Reflect
                        dir.Value += Mathf.PI;
                        dir.Value = (dir.Value >= 2 * Mathf.PI) ? dir.Value - 2 * Mathf.PI : dir.Value;


                        //Move ant out of collision
                        dist = Mathf.Sqrt(sqrDist);
                        dx /= dist;
                        dy /= dist;
                        antTranslation.Value.x = obstTranslationArray[i].Value.x + dx * obstRadiusArray[i].Value;
                        antTranslation.Value.z = obstTranslationArray[i].Value.z + dy * obstRadiusArray[i].Value;
                    }

                }

            }).WithDisposeOnCompletion(obstRadiusArray)
            .WithDisposeOnCompletion(obstTranslationArray)
            .ScheduleParallel();*/
    }

}
