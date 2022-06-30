//////
/// Based on https://github.com/ofux/uTerrainsExtensions/blob/master/Nodes/Primitives/2D/Heightmap2DPrimitiveSerializable.cs
//////
using System;
using System.Collections.Generic;
using UltimateTerrains;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

[PrettyTypeName("Heightmap ScriptableObject")]
[Serializable]
public class Heightmap2DPrimitiveSoSerializable : Primitive2DNodeSerializable
{
    public override string Title {
        get { return "Heightmap ScriptableObject"; }
    }

    // Useful properties for the module
    [SerializeField] private int fromX, fromZ, toX, toZ;
    [SerializeField] private HeightMapSo heightmapSo; //Replaces Texture2D for Drag & Drop of Height Map ScriptableObject
    [SerializeField] private float heightScale;

    public override void OnEditorGUI(UltimateTerrain uTerrain, IReadOnlyFlowGraph graph)
    {
#if UNITY_EDITOR
        fromX = EditorGUILayout.IntField("min X:", fromX);
        fromZ = EditorGUILayout.IntField("min Z:", fromZ);
        toX = EditorGUILayout.IntField("max X:", toX);
        toZ = EditorGUILayout.IntField("max Z:", toZ);
        heightmapSo = (HeightMapSo) EditorGUILayout.ObjectField("Heightmap:", heightmapSo, typeof(HeightMapSo), false);
        heightScale = EditorGUILayout.FloatField("Height scale", heightScale);
#endif
    }

    public override IGeneratorNode CreateModule(UltimateTerrain uTerrain, List<CallableNode> inputs)
    {
        return new Heightmap2DPrimitiveSo(fromX, fromZ, toX, toZ, heightmapSo, heightScale);
    }
}