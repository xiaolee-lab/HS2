using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000408 RID: 1032
[DefaultExecutionOrder(-200)]
public class RandomInstancing : MonoBehaviour
{
	// Token: 0x06001266 RID: 4710 RVA: 0x00072DAC File Offset: 0x000711AC
	private void Awake()
	{
		for (int i = 0; i < this.m_PoolSize; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_Prefab, Vector3.zero, Quaternion.identity);
			gameObject.SetActive(false);
			this.m_Instances.Add(gameObject.transform);
		}
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x00072DFE File Offset: 0x000711FE
	private void OnEnable()
	{
		this.m_LocX = -1;
		this.m_LocZ = -1;
		this.UpdateInstances();
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x00072E14 File Offset: 0x00071214
	private void OnDestroy()
	{
		for (int i = 0; i < this.m_Instances.Count; i++)
		{
			if (this.m_Instances[i])
			{
				UnityEngine.Object.Destroy(this.m_Instances[i].gameObject);
			}
		}
		this.m_Instances.Clear();
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x00072E74 File Offset: 0x00071274
	private void Update()
	{
		this.UpdateInstances();
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x00072E7C File Offset: 0x0007127C
	private void UpdateInstances()
	{
		int num = (int)Mathf.Floor(base.transform.position.x / this.m_Size);
		int num2 = (int)Mathf.Floor(base.transform.position.z / this.m_Size);
		if (num == this.m_LocX && num2 == this.m_LocZ)
		{
			return;
		}
		this.m_LocX = num;
		this.m_LocZ = num2;
		this.m_Used = 0;
		for (int i = num - 2; i <= num + 2; i++)
		{
			for (int j = num2 - 2; j <= num2 + 2; j++)
			{
				int num3 = this.UpdateTileInstances(i, j);
				if (num3 != this.m_InstancesPerTile)
				{
					return;
				}
			}
		}
		int num4 = this.m_Used;
		while (num4 < this.m_PoolSize && this.m_Instances[num4].gameObject.activeSelf)
		{
			this.m_Instances[num4].gameObject.SetActive(false);
			num4++;
		}
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x00072F9C File Offset: 0x0007139C
	private int UpdateTileInstances(int i, int j)
	{
		int num = RandomInstancing.Hash2(i, j) ^ this.m_BaseHash;
		int num2 = Math.Min(this.m_InstancesPerTile, this.m_PoolSize - this.m_Used);
		int num3 = this.m_Used + num2;
		while (this.m_Used < num3)
		{
			float num4 = 0f;
			float num5 = 0f;
			if (this.m_RandomPosition)
			{
				num4 = RandomInstancing.Random(ref num);
				num5 = RandomInstancing.Random(ref num);
			}
			Vector3 position = new Vector3(((float)i + num4) * this.m_Size, this.m_Height, ((float)j + num5) * this.m_Size);
			if (this.m_RandomOrientation)
			{
				float angle = 360f * RandomInstancing.Random(ref num);
				this.m_Instances[this.m_Used].rotation = Quaternion.AngleAxis(angle, Vector3.up);
			}
			this.m_Instances[this.m_Used].position = position;
			this.m_Instances[this.m_Used].gameObject.SetActive(true);
			this.m_Used++;
		}
		if (num2 < this.m_InstancesPerTile)
		{
		}
		return num2;
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x000730C3 File Offset: 0x000714C3
	private static int Hash2(int i, int j)
	{
		return i * 73856093 ^ j * 19349663;
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x000730D4 File Offset: 0x000714D4
	private static float Random(ref int seed)
	{
		seed ^= 123459876;
		int num = seed / 127773;
		seed = 16807 * (seed - num * 127773) - 2836 * num;
		if (seed < 0)
		{
			seed += int.MaxValue;
		}
		float result = (float)seed * 1f / 2.1474836E+09f;
		seed ^= 123459876;
		return result;
	}

	// Token: 0x040014E8 RID: 5352
	public GameObject m_Prefab;

	// Token: 0x040014E9 RID: 5353
	public int m_PoolSize = 250;

	// Token: 0x040014EA RID: 5354
	public int m_InstancesPerTile = 10;

	// Token: 0x040014EB RID: 5355
	public bool m_RandomPosition = true;

	// Token: 0x040014EC RID: 5356
	public bool m_RandomOrientation = true;

	// Token: 0x040014ED RID: 5357
	public float m_Height;

	// Token: 0x040014EE RID: 5358
	public int m_BaseHash = 347652783;

	// Token: 0x040014EF RID: 5359
	public float m_Size = 100f;

	// Token: 0x040014F0 RID: 5360
	private List<Transform> m_Instances = new List<Transform>();

	// Token: 0x040014F1 RID: 5361
	private int m_Used;

	// Token: 0x040014F2 RID: 5362
	private int m_LocX;

	// Token: 0x040014F3 RID: 5363
	private int m_LocZ;
}
