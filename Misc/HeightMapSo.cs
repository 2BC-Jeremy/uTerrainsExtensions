//////
/// Based on https://github.com/ofux/uTerrainsExtensions/blob/master/Nodes/Primitives/2D/Heightmap2DPrimitive.cs
/// Todo: Add way to trigger data copy Tx2Array, when UltimateTerrains.cs:StartFromEditor() is called.
//////

using System;
using UnityEngine;

namespace UltimateTerrains
{
	[Serializable, CreateAssetMenu(fileName = "UtHeightMap_SO New", menuName = "Ultimate Terrains/New Height Map ScriptableObject")]
	public class HeightMapSo : ScriptableObject
	{
		[SerializeField] public Texture2D heightMapTx;
		[SerializeField, HideInInspector] public int width;
		[SerializeField, HideInInspector] public int height;
		/// The Pixel Data is stored to a private float that is not Serialized.
		/// If the data is Serialized it will be stored to the project .asset file, creating uncompressed float array storage.	
		private float[] pixelData = new float[0];
		
		public void Tx2Array(Texture2D hmTx)
		{

			if (heightMapTx == null || heightMapTx != hmTx)
			{
				heightMapTx = hmTx;
			}
			width = hmTx.width;
			height = hmTx.height;

			pixelData = new float[width * height];

			for (var x = 0; x < width; ++x)
			{
				for (var y = 0; y < height; ++y)
				{
					pixelData[x + width * y] = (float)(hmTx.GetPixel(x, y).r);
				}
			}
		}
		///////
		/// This will trigger at the start of the Scene containing the UltimateTerrains flow graph, that references The Height Map ScriptableObject.
		///////
		private void Awake()
		{
			if (heightMapTx != null) { Tx2Array(heightMapTx); }
			else { Debug.LogError("There is no Height Map assigned to this ScriptableObject.");}
		}
		
		public int GetLength()
		{
			return pixelData.Length;
		}

		public float GetData(int i)
		{
			return pixelData[i];
		}
	}
}