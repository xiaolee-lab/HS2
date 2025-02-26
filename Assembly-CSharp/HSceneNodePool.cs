using System;
using UnityEngine;

// Token: 0x02000B17 RID: 2839
public class HSceneNodePool : ObjPool
{
	// Token: 0x0600534F RID: 21327 RVA: 0x002487DC File Offset: 0x00246BDC
	public GameObject Get(int startID = 0)
	{
		for (int i = startID; i < this.pool.Count; i++)
		{
			if (!this.pool[i].activeSelf)
			{
				this.pool[i].SetActive(false);
				return this.pool[i];
			}
		}
		GameObject gameObject = base.CreatePoolObject();
		gameObject.SetActive(false);
		this.pool.Add(gameObject);
		return gameObject;
	}
}
