using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000EEE RID: 3822
public abstract class ObjPool
{
	// Token: 0x06007CC6 RID: 31942 RVA: 0x00248764 File Offset: 0x00246B64
	public void CreatePool(GameObject PoolObj, Transform createPlace, int PreMaxNum)
	{
		this.poolObj = PoolObj;
		this.CreatePlace = createPlace;
		for (int i = 0; i < PreMaxNum; i++)
		{
			this.pool.Add(this.CreatePoolObject());
		}
	}

	// Token: 0x06007CC7 RID: 31943 RVA: 0x002487A4 File Offset: 0x00246BA4
	protected GameObject CreatePoolObject()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.poolObj, this.CreatePlace);
		gameObject.SetActive(false);
		return gameObject;
	}

	// Token: 0x06007CC8 RID: 31944 RVA: 0x002487CB File Offset: 0x00246BCB
	public List<GameObject> GetList()
	{
		return this.pool;
	}

	// Token: 0x040064B5 RID: 25781
	protected List<GameObject> pool = new List<GameObject>();

	// Token: 0x040064B6 RID: 25782
	private GameObject poolObj;

	// Token: 0x040064B7 RID: 25783
	private Transform CreatePlace;
}
