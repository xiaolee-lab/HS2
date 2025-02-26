using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace CTS
{
	// Token: 0x02000692 RID: 1682
	public static class CTSMaterials
	{
		// Token: 0x0600279D RID: 10141 RVA: 0x000E9C54 File Offset: 0x000E8054
		public static Material GetMaterial(string shaderType, CTSProfile profile)
		{
			Material material;
			if (profile.m_useMaterialControlBlock && CTSMaterials.m_materialLookup.TryGetValue(shaderType + ":" + profile.name, out material))
			{
				return material;
			}
			Shader shader = CTSShaders.GetShader(shaderType);
			if (shader == null)
			{
				UnityEngine.Debug.LogErrorFormat("Could not create CTS material for shader : {0}. Make sure you add your CTS shader is pre-loaded!", new object[]
				{
					shaderType
				});
				return null;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			material = new Material(shader);
			material.name = shaderType + ":" + profile.name;
			if (profile.m_useMaterialControlBlock)
			{
				material.hideFlags = HideFlags.DontSave;
				CTSMaterials.m_materialLookup.Add(material.name, material);
			}
			if (stopwatch.ElapsedMilliseconds > 5L)
			{
			}
			return material;
		}

		// Token: 0x040027E3 RID: 10211
		private static Dictionary<string, Material> m_materialLookup = new Dictionary<string, Material>();
	}
}
