﻿using LibNoise;
using LibNoise.Primitive;
using UltimateTerrains;


public class Simplex3DPrimitive : Primitive3DNode
{
    private readonly double frequency;
    private readonly double scale;
    private readonly SimplexPerlin simplex;

    public Simplex3DPrimitive(double frequency, double scale, int seed, NoiseQuality quality)
    {
        this.frequency = frequency;
        this.scale = scale;
        simplex = new SimplexPerlin(seed, quality);
    }

    public override double Execute(double x, double y, double z, CallableNode flow)
    {
        return simplex.GetValue(x * frequency, y * frequency, z * frequency) * scale;
    }
}