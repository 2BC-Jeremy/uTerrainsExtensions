//////
/// Based on https://github.com/ofux/uTerrainsExtensions/blob/master/Nodes/Primitives/2D/Heightmap2DPrimitive.cs
//////
using UltimateTerrains;
using UnityEngine;

public class Heightmap2DPrimitiveSo : Primitive2DNode
{
    private readonly int fromX;
    private readonly int fromZ;
    private readonly int width;
    private readonly int height;
    private readonly double voxel2PixelPosX;
    private readonly double voxel2PixelPosZ;
    private readonly float heightScale;
    private readonly HeightMapSo heightMap;

    public Heightmap2DPrimitiveSo(int fromX, int fromZ, int toX, int toZ, HeightMapSo hm, float hScale)
    {

        heightMap = hm;
        heightScale = hScale;
        this.fromX = fromX;
        this.fromZ = fromZ;
        voxel2PixelPosX = heightMap.width / (double) (toX - fromX);
        voxel2PixelPosZ = heightMap.height / (double) (toZ - fromZ);
        width = heightMap.width;
        height = heightMap.height;
    }

    public override double Execute(double x, double y, double z, CallableNode flow)
    {
        var ix = (x - fromX) * voxel2PixelPosX;
        var iz = (z - fromZ) * voxel2PixelPosZ;
        var hx = Mathf.Clamp((int) ix, 0, width - 1);
        var hz = Mathf.Clamp((int) iz, 0, height - 1);

        if (hx < 0 || hz < 0 || hx >= width - 1 || hz >= height - 1) {
            return 0;
        }

        var h00 = heightMap.GetData(hx + width * hz) * heightScale;
        var h01 = heightMap.GetData(hx + width * (hz + 1)) * heightScale;
        var h10 = heightMap.GetData((hx + 1) + width * hz) * heightScale;
        var h11 = heightMap.GetData((hx + 1) + width * (hz + 1)) * heightScale;
        return UMath.BilinearInterpolate(h00, h01, h10, h11, ix - hx, iz - hz);
    }
}