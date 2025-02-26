using System;
using UnityEngine;

namespace UnityStandardAssets.CinematicEffects
{
	// Token: 0x02000429 RID: 1065
	public static class ME_ImageEffectHelper
	{
		// Token: 0x06001372 RID: 4978 RVA: 0x00077F74 File Offset: 0x00076374
		public static bool IsSupported(Shader s, bool needDepth, bool needHdr, MonoBehaviour effect)
		{
			return !(s == null) && s.isSupported && SystemInfo.supportsImageEffects && (!needDepth || SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth)) && (!needHdr || SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf));
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00077FD0 File Offset: 0x000763D0
		public static Material CheckShaderAndCreateMaterial(Shader s)
		{
			if (s == null || !s.isSupported)
			{
				return null;
			}
			return new Material(s)
			{
				hideFlags = HideFlags.DontSave
			};
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00078006 File Offset: 0x00076406
		public static bool supportsDX11
		{
			get
			{
				return SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;
			}
		}
	}
}
