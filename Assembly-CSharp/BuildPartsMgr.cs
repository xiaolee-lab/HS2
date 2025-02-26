using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

// Token: 0x02000ECD RID: 3789
public class BuildPartsMgr : MonoBehaviour
{
	// Token: 0x06007C0E RID: 31758 RVA: 0x00344C90 File Offset: 0x00343090
	public void Init()
	{
		CraftResource instance = Singleton<CraftResource>.Instance;
		foreach (int id in instance.GetCraftItemCategories())
		{
			ReadOnlyDictionary<int, CraftItemInfo> itemTable = instance.GetItemTable(id);
			foreach (KeyValuePair<int, CraftItemInfo> keyValuePair in itemTable)
			{
				CraftItemInfo value = keyValuePair.Value;
				this.infos.Add(value);
			}
		}
		this.Allpool = new List<BuildPartsPool>[10];
		for (int j = 0; j < this.Allpool.Length; j++)
		{
			this.Allpool[j] = new List<BuildPartsPool>();
		}
		Singleton<CraftCommandListBaseObject>.Instance.CategoryNames = new Dictionary<int, string>();
		List<CraftItemInfo>[] array = new List<CraftItemInfo>[17];
		for (int k = 0; k < array.Length; k++)
		{
			array[k] = new List<CraftItemInfo>();
		}
		for (int l = 0; l < this.infos.Count; l++)
		{
			array[this.infos[l].Itemkind - 1].Add(this.infos[l]);
		}
		for (int m = 0; m < array.Length; m++)
		{
			for (int n = 0; n < array[m].Count; n++)
			{
				int id2 = n;
				array[m][n].Id = id2;
			}
		}
		for (int num = 0; num < 10; num++)
		{
			this.BuildPartPoolDic.Add(num, new Dictionary<int, Tuple<BuildPartsPool, CraftItemInfo>>());
		}
		for (int num2 = 1; num2 < 17; num2++)
		{
			this.BuildPartDic.Add(num2, new List<GameObject>());
		}
		for (int num3 = 0; num3 < this.infos.Count; num3++)
		{
			this.BuildPartDic[this.infos[num3].Itemkind].Add(this.infos[num3].obj);
			BuildPartsPool buildPartsPool = new BuildPartsPool();
			buildPartsPool.CreatePool(this.BuildPartDic[this.infos[num3].Itemkind][this.infos[num3].Id], this.CreatePlace[this.infos[num3].Itemkind - 1], 50, this.infos[num3].Formkind, this.infos[num3].Itemkind, this.Allpool[this.infos[num3].Formkind].Count, this.infos[num3].Catkind, this.infos[num3].Height);
			this.Allpool[this.infos[num3].Formkind].Add(buildPartsPool);
			if (!this.BuildPartPoolDic[this.infos[num3].Formkind].ContainsKey(this.Allpool[this.infos[num3].Formkind].Count - 1))
			{
				this.BuildPartPoolDic[this.infos[num3].Formkind].Add(this.Allpool[this.infos[num3].Formkind].Count - 1, new Tuple<BuildPartsPool, CraftItemInfo>(buildPartsPool, this.infos[num3]));
			}
		}
		for (int num4 = 0; num4 < this.infos.Count; num4++)
		{
			if (!Singleton<CraftCommandListBaseObject>.Instance.CategoryNames.ContainsKey(this.infos[num4].Catkind))
			{
				Singleton<CraftCommandListBaseObject>.Instance.CategoryNames.Add(this.infos[num4].Catkind, this.infos[num4].CategoryName);
			}
		}
		this.Allpool[2][0].SetLock();
	}

	// Token: 0x06007C0F RID: 31759 RVA: 0x003450F0 File Offset: 0x003434F0
	public List<BuildPartsPool>[] GetPool()
	{
		return this.Allpool;
	}

	// Token: 0x06007C10 RID: 31760 RVA: 0x003450F8 File Offset: 0x003434F8
	public BuildPartsInfo GetBuildPartsInfo(GameObject buildPart)
	{
		return buildPart.GetComponent<BuildPartsInfo>();
	}

	// Token: 0x040063C2 RID: 25538
	private List<BuildPartsPool>[] Allpool;

	// Token: 0x040063C3 RID: 25539
	private List<CraftItemInfo> infos = new List<CraftItemInfo>();

	// Token: 0x040063C4 RID: 25540
	public List<Transform> CreatePlace;

	// Token: 0x040063C5 RID: 25541
	private Dictionary<int, List<GameObject>> BuildPartDic = new Dictionary<int, List<GameObject>>();

	// Token: 0x040063C6 RID: 25542
	public Dictionary<int, Dictionary<int, Tuple<BuildPartsPool, CraftItemInfo>>> BuildPartPoolDic = new Dictionary<int, Dictionary<int, Tuple<BuildPartsPool, CraftItemInfo>>>();

	// Token: 0x040063C7 RID: 25543
	private const int PreBuildMaxNum = 50;
}
