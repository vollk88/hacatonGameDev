using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BaseClasses.Utils
{
	public class RandomTexture : MonoBehaviour
	{
		public Texture[] textures;

		private void Start()
		{
			if (textures.Length == 0)
				return;
			
			int index = UnityEngine.Random.Range(0, textures.Length);
			foreach (var childRend in GetComponentsInChildren<Renderer>())
			{
				childRend.material.mainTexture = textures[index];
			}
		}
	}
}