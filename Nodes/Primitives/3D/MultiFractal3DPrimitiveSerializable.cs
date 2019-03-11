using System;
using System.Collections.Generic;
using LibNoise;
using UltimateTerrains;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

[PrettyTypeName("LibNoise Multifractal 3D")]
[Serializable]
public class MultiFractal3DPrimitiveSerializable : Primitive3DNodeSerializable
{
    public override string Title {
        get { return "LibNoise Multifractal 3D"; }
    }

    // Useful properties for the module
    [SerializeField] private float frequency = 0.01f;
    [SerializeField] private float scale = 1f;
    [SerializeField] private int seed;
    [SerializeField] private NoiseQuality quality = NoiseQuality.Standard;

    public float Frequency {
        get { return frequency; }
        set { frequency = value; }
    }

    public float Scale {
        get { return scale; }
        set { scale = value; }
    }

    public int Seed {
        get { return seed; }
        set { seed = value; }
    }

    public NoiseQuality Quality {
        get { return quality; }
        set { quality = value; }
    }

    public override void OnEditorGUI(UltimateTerrain uTerrain, IReadOnlyFlowGraph graph)
    {
#if UNITY_EDITOR
        Frequency = EditorGUILayout.FloatField("Frequency:", Frequency);
        Scale = EditorGUILayout.FloatField("Scale:", Scale);
        Seed = EditorGUILayout.IntField("Seed:", Seed);
        Quality = (NoiseQuality) EditorGUILayout.EnumPopup("Quality:", Quality);
#endif
    }

    public override IGeneratorNode CreateModule(UltimateTerrain uTerrain, List<CallableNode> inputs)
    {
        return new MultiFractal3DPrimitive(Frequency, Scale, Seed, Quality);
    }
}