using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000FC6 RID: 4038
public class TrajectoryPool
{
	// Token: 0x06008630 RID: 34352 RVA: 0x00381E80 File Offset: 0x00380280
	public void CreatePool(GameObject obj, int nMaxCount, Transform createPlace)
	{
		this.PoolObj = obj;
		this.PoolobjList = new List<GameObject>();
		this.CreatePlace = createPlace;
		for (int i = 0; i < nMaxCount; i++)
		{
			GameObject gameObject = this.CreatePoolObject();
			gameObject.transform.SetParent(this.CreatePlace, false);
			gameObject.SetActive(false);
			this.PoolobjList.Add(gameObject);
		}
	}

	// Token: 0x06008631 RID: 34353 RVA: 0x00381EE4 File Offset: 0x003802E4
	public GameObject GetObject()
	{
		for (int i = 0; i < this.PoolobjList.Count; i++)
		{
			int index = i;
			if (!this.PoolobjList[index].activeSelf)
			{
				this.PoolobjList[index].SetActive(true);
				return this.PoolobjList[index];
			}
		}
		GameObject gameObject = this.CreatePoolObject();
		gameObject.transform.SetParent(this.CreatePlace, false);
		gameObject.SetActive(true);
		this.PoolobjList.Add(gameObject);
		return gameObject;
	}

	// Token: 0x06008632 RID: 34354 RVA: 0x00381F74 File Offset: 0x00380374
	private GameObject CreatePoolObject()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.PoolObj);
	}

	// Token: 0x06008633 RID: 34355 RVA: 0x00381F8E File Offset: 0x0038038E
	public int getListCount()
	{
		return this.PoolobjList.Count;
	}

	// Token: 0x06008634 RID: 34356 RVA: 0x00381F9B File Offset: 0x0038039B
	public List<GameObject> getList()
	{
		return this.PoolobjList;
	}

	// Token: 0x04006D46 RID: 27974
	private List<GameObject> PoolobjList;

	// Token: 0x04006D47 RID: 27975
	private GameObject PoolObj;

	// Token: 0x04006D48 RID: 27976
	private Transform CreatePlace;
}
