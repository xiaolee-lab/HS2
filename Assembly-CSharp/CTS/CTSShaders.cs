using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace CTS
{
	// Token: 0x02000698 RID: 1688
	public static class CTSShaders
	{
		// Token: 0x060027F0 RID: 10224 RVA: 0x000ED620 File Offset: 0x000EBA20
		static CTSShaders()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			Shader shader = Shader.Find("CTS/CTS Terrain Shader Lite");
			if (shader != null)
			{
				CTSShaders.m_shaderLookup.Add("CTS/CTS Terrain Shader Lite", shader);
			}
			shader = Shader.Find("CTS/CTS Terrain Shader Basic");
			if (shader != null)
			{
				CTSShaders.m_shaderLookup.Add("CTS/CTS Terrain Shader Basic", shader);
			}
			shader = Shader.Find("CTS/CTS Terrain Shader Basic CutOut");
			if (shader != null)
			{
				CTSShaders.m_shaderLookup.Add("CTS/CTS Terrain Shader Basic CutOut", shader);
			}
			shader = Shader.Find("CTS/CTS Terrain Shader Advanced");
			if (shader != null)
			{
				CTSShaders.m_shaderLookup.Add("CTS/CTS Terrain Shader Advanced", shader);
			}
			shader = Shader.Find("CTS/CTS Terrain Shader Advanced CutOut");
			if (shader != null)
			{
				CTSShaders.m_shaderLookup.Add("CTS/CTS Terrain Shader Advanced CutOut", shader);
			}
			shader = Shader.Find("CTS/CTS Terrain Shader Advanced Tess");
			if (shader != null)
			{
				CTSShaders.m_shaderLookup.Add("CTS/CTS Terrain Shader Advanced Tess", shader);
			}
			shader = Shader.Find("CTS/CTS Terrain Shader Advanced Tess CutOut");
			if (shader != null)
			{
				CTSShaders.m_shaderLookup.Add("CTS/CTS Terrain Shader Advanced Tess CutOut", shader);
			}
			if (stopwatch.ElapsedMilliseconds > 0L)
			{
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000ED75C File Offset: 0x000EBB5C
		public static Shader GetShader(string shaderType)
		{
			Shader result;
			if (CTSShaders.m_shaderLookup.TryGetValue(shaderType, out result))
			{
				return result;
			}
			UnityEngine.Debug.LogErrorFormat("Could not load CTS shader : {0}. Make sure you add your CTS shader to pre-loaded assets!", new object[]
			{
				shaderType
			});
			return null;
		}

		// Token: 0x040028FA RID: 10490
		private static Dictionary<string, Shader> m_shaderLookup = new Dictionary<string, Shader>();
	}
}
