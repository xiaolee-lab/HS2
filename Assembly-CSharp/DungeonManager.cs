using System;
using UnityEngine;

// Token: 0x020003FD RID: 1021
[DefaultExecutionOrder(-200)]
public class DungeonManager : MonoBehaviour
{
	// Token: 0x0600122F RID: 4655 RVA: 0x00071D44 File Offset: 0x00070144
	private void Awake()
	{
		UnityEngine.Random.InitState(23431);
		int[] array = new int[this.m_Width * this.m_Height];
		for (int i = 0; i < this.m_Height; i++)
		{
			for (int j = 0; j < this.m_Width; j++)
			{
				bool flag = false;
				bool flag2 = false;
				if (j > 0)
				{
					flag = ((array[j - 1 + i * this.m_Width] & 1) != 0);
				}
				if (i > 0)
				{
					flag2 = ((array[j + (i - 1) * this.m_Width] & 2) != 0);
				}
				int num = 0;
				if (flag)
				{
					num |= 4;
				}
				if (flag2)
				{
					num |= 8;
				}
				if (j + 1 < this.m_Width && UnityEngine.Random.value > 0.5f)
				{
					num |= 1;
				}
				if (i + 1 < this.m_Height && UnityEngine.Random.value > 0.5f)
				{
					num |= 2;
				}
				array[j + i * this.m_Width] = num;
			}
		}
		for (int k = 0; k < this.m_Height; k++)
		{
			for (int l = 0; l < this.m_Width; l++)
			{
				Vector3 position = new Vector3((float)l * this.m_Spacing, 0f, (float)k * this.m_Spacing);
				if (this.m_Tiles[array[l + k * this.m_Width]] != null)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.m_Tiles[array[l + k * this.m_Width]], position, Quaternion.identity);
				}
			}
		}
	}

	// Token: 0x040014B4 RID: 5300
	public int m_Width = 10;

	// Token: 0x040014B5 RID: 5301
	public int m_Height = 10;

	// Token: 0x040014B6 RID: 5302
	public float m_Spacing = 4f;

	// Token: 0x040014B7 RID: 5303
	public GameObject[] m_Tiles = new GameObject[16];
}
