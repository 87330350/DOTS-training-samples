﻿using Unity.Entities;

[GenerateAuthoringComponent]
public struct DoubleIntersection : IComponentData
{
    public Entity laneIn0;
    public Entity laneIn1;
    public Entity laneOut0;
    public Entity laneOut1;
    public Entity car0;
    public Entity car1;
    public Entity spline0;
    public Entity spline1;
}
