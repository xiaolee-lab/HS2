using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007B7 RID: 1975
	public class ChangeColor
	{
		// Token: 0x06003056 RID: 12374 RVA: 0x00120C30 File Offset: 0x0011F030
		public static void SetColor(string propertyName, Color color, int idx, params Renderer[] arrRend)
		{
			int propertyID = Shader.PropertyToID(propertyName);
			ChangeColor.SetColor(propertyID, color, idx, arrRend);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x00120C50 File Offset: 0x0011F050
		public static void SetColor(int propertyID, Color color, int idx, params Renderer[] arrRend)
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
						material.SetColor(propertyID, color);
					}
				}
			}
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x00120CB8 File Offset: 0x0011F0B8
		public static void SetColor(string propertyName, Color color, params Material[] mat)
		{
			int propertyID = Shader.PropertyToID(propertyName);
			ChangeColor.SetColor(propertyID, color, mat);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x00120CD4 File Offset: 0x0011F0D4
		public static void SetColor(int propertyID, Color color, params Material[] mat)
		{
			if (mat == null || mat.Length == 0)
			{
				return;
			}
			foreach (Material material in mat)
			{
				material.SetColor(propertyID, color);
			}
		}
	}
}
