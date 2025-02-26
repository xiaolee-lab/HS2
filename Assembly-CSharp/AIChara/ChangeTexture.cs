using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007B8 RID: 1976
	public class ChangeTexture
	{
		// Token: 0x0600305B RID: 12379 RVA: 0x00120D1C File Offset: 0x0011F11C
		public static void SetTexture(string propertyName, Texture tex, int idx, params Renderer[] arrRend)
		{
			int propertyID = Shader.PropertyToID(propertyName);
			ChangeTexture.SetTexture(propertyID, tex, idx, arrRend);
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x00120D3C File Offset: 0x0011F13C
		public static void SetTexture(int propertyID, Texture tex, int idx, params Renderer[] arrRend)
		{
			if (arrRend == null || arrRend.Length == 0)
			{
				return;
			}
			foreach (Renderer renderer in arrRend)
			{
				if (idx < renderer.materials.Length)
				{
					Material material = renderer.materials[idx];
					if (null != material)
					{
						material.SetTexture(propertyID, tex);
					}
				}
			}
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x00120DA4 File Offset: 0x0011F1A4
		public static void SetTexture(string propertyName, Texture tex, params Material[] mat)
		{
			int propertyID = Shader.PropertyToID(propertyName);
			ChangeTexture.SetTexture(propertyID, tex, mat);
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x00120DC0 File Offset: 0x0011F1C0
		public static void SetTexture(int propertyID, Texture tex, params Material[] mat)
		{
			if (mat == null || mat.Length == 0)
			{
				return;
			}
			foreach (Material material in mat)
			{
				material.SetTexture(propertyID, tex);
			}
		}
	}
}
