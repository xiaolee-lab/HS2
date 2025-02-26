using System;
using UnityEngine;

// Token: 0x02000EED RID: 3821
public class GridPool : ObjPool
{
	// Token: 0x06007CC4 RID: 31940 RVA: 0x00356650 File Offset: 0x00354A50
	public GameObject Get()
	{
		for (int i = 0; i < this.pool.Count; i++)
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
