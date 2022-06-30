using UnityEngine;
using UnityEditor;

namespace UltimateTerrains
{
	
	//////
	/// Custom Inspector for Height Map Scriptable Object. 
	/// Use "Copy Pixel Data" to manually trigger the copy into memory. Useful for rendering UltimateTerrains in Editor:
	/// "Biomes Tab -> Enable terrain preview" or "Editor tool Tab -> Generate & display terrain in scene view".
	/// Otherwise a "NullReferenceException" will be thrown at UltimateTerrains.cs:407.
	/// Todo: Add way to trigger data copy when UltimateTerrains.cs:StartFromEditor() is called.
	//////
    [CustomEditor(typeof(HeightMapSo))]
    public class HeightMapSoInspector : Editor
    {
        HeightMapSo so;

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Scriptable Object for storing UltimateTerrains Height Map data.");

            so = (HeightMapSo)target;

            int pixelCount = EditorGUILayout.IntField("Pixel Count:", so.GetLength());
            int h = EditorGUILayout.IntField("Height:", so.height);
            int w = EditorGUILayout.IntField("Width:", so.width);
            base.OnInspectorGUI();

            if (GUILayout.Button("Copy Pixel Data"))
            {
                so.Tx2Array(so.heightMapTx);
            }
            EditorUtility.SetDirty(so);
        }
    }
}