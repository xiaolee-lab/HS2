using System;
using System.Collections.Generic;
using AIProject;
using Manager;
using UnityEngine;

// Token: 0x02000AC0 RID: 2752
public class HMeshData : MonoBehaviour
{
	// Token: 0x060050A5 RID: 20645 RVA: 0x001F7EEC File Offset: 0x001F62EC
	private void Reset()
	{
		this.Areas = new List<GameObject>();
		for (int i = 0; i < base.transform.childCount; i++)
		{
			int index = i;
			this.Areas.Add(base.transform.GetChild(index).gameObject);
		}
		this.Areas = this.Sort(this.Areas);
	}

	// Token: 0x060050A6 RID: 20646 RVA: 0x001F7F50 File Offset: 0x001F6350
	private void Start()
	{
		this.dicColliders = new Dictionary<int, Collider[]>();
		for (int i = 0; i < this.Areas.Count; i++)
		{
			int num = i;
			this.colliders = this.Areas[num].GetComponentsInChildren<Collider>();
			this.dicColliders.Add(num, this.colliders);
		}
	}

	// Token: 0x060050A7 RID: 20647 RVA: 0x001F7FB0 File Offset: 0x001F63B0
	private List<GameObject> Sort(List<GameObject> Areas)
	{
		List<GameObject> list = new List<GameObject>();
		string[] array = new string[Areas.Count];
		for (int i = 0; i < Areas.Count; i++)
		{
			array[i] = Areas[i].name;
		}
		Array.Sort<string>(array);
		for (int j = 0; j < array.Length; j++)
		{
			foreach (GameObject gameObject in Areas)
			{
				if (!(array[j] != gameObject.name))
				{
					list.Add(gameObject);
					break;
				}
			}
		}
		return list;
	}

	// Token: 0x060050A8 RID: 20648 RVA: 0x001F807C File Offset: 0x001F647C
	public void SetColliderAreaMap()
	{
		Dictionary<int, Chunk> chunkTable = Singleton<Map>.Instance.ChunkTable;
		foreach (KeyValuePair<int, Collider[]> keyValuePair in this.dicColliders)
		{
			foreach (Chunk chunk in chunkTable.Values)
			{
				foreach (MapArea mapArea in chunk.MapAreas)
				{
					if (mapArea.AreaID == keyValuePair.Key)
					{
						mapArea.hColliders = keyValuePair.Value;
					}
				}
			}
		}
	}

	// Token: 0x04004A3D RID: 19005
	[SerializeField]
	private List<GameObject> Areas;

	// Token: 0x04004A3E RID: 19006
	private Collider[] colliders;

	// Token: 0x04004A3F RID: 19007
	public Dictionary<int, Collider[]> dicColliders;
}
