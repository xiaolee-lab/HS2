using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000EEC RID: 3820
public class BuildPartsPool : ObjPool
{
	// Token: 0x06007CB8 RID: 31928 RVA: 0x00356388 File Offset: 0x00354788
	public void CreatePool(GameObject PoolObj, Transform createPlace, int PreMaxNum, int formkind, int itemkind, int id, int catkind, int height)
	{
		this.nPartsFormKind = formkind;
		this.nItemKind = itemkind;
		this.nCatKind = catkind;
		this.nID = id;
		this.nHeight = height;
		base.CreatePool(PoolObj, createPlace, PreMaxNum);
		for (int i = 0; i < this.pool.Count; i++)
		{
			if (this.pool[i].GetComponent<BuildPartsInfo>() == null)
			{
				this.pool[i].AddComponent<BuildPartsInfo>();
			}
			BuildPartsInfo component = this.pool[i].GetComponent<BuildPartsInfo>();
			component.Init(i, this.nPartsFormKind, this.nItemKind, this.nCatKind, 0, this.nID, this.nHeight);
			component.nPutFloor = -1;
		}
	}

	// Token: 0x06007CB9 RID: 31929 RVA: 0x00356450 File Offset: 0x00354850
	public GameObject Get(ref int ID)
	{
		for (int i = 0; i < this.pool.Count; i++)
		{
			if (!this.pool[i].activeSelf)
			{
				if (!this.ReserveListCheck(i))
				{
					this.pool[i].SetActive(false);
					if (this.ReserveList.Count > i)
					{
						this.ReserveList[i] = i;
					}
					else
					{
						this.ReserveList.Add(i);
					}
					ID = i;
					return this.pool[i];
				}
			}
		}
		GameObject gameObject = base.CreatePoolObject();
		gameObject.SetActive(false);
		this.pool.Add(gameObject);
		this.ReserveList.Add(this.ReserveList.Count);
		if (gameObject.GetComponent<BuildPartsInfo>() == null)
		{
			gameObject.AddComponent<BuildPartsInfo>();
		}
		BuildPartsInfo component = gameObject.GetComponent<BuildPartsInfo>();
		component.Init(this.ReserveList.Count - 1, this.nPartsFormKind, this.nItemKind, this.nCatKind, 0, this.nID, this.nHeight);
		component.nPutFloor = -1;
		ID = this.ReserveList.Count - 1;
		return gameObject;
	}

	// Token: 0x06007CBA RID: 31930 RVA: 0x0035658E File Offset: 0x0035498E
	public int GetFormKind()
	{
		return this.nPartsFormKind;
	}

	// Token: 0x06007CBB RID: 31931 RVA: 0x00356596 File Offset: 0x00354996
	public int GetItemKind()
	{
		return this.nItemKind;
	}

	// Token: 0x06007CBC RID: 31932 RVA: 0x0035659E File Offset: 0x0035499E
	public int GetCategoryKind()
	{
		return this.nCatKind;
	}

	// Token: 0x06007CBD RID: 31933 RVA: 0x003565A6 File Offset: 0x003549A6
	public bool ReserveListCheck(int ID)
	{
		return ID < this.ReserveList.Count && this.ReserveList[ID] == ID;
	}

	// Token: 0x06007CBE RID: 31934 RVA: 0x003565D0 File Offset: 0x003549D0
	public void ReserveListDel(int ID, int mode = 0)
	{
		if (mode == 0)
		{
			this.ReserveList[ID] = -1;
		}
		else
		{
			this.ReserveList.Clear();
		}
	}

	// Token: 0x06007CBF RID: 31935 RVA: 0x003565F5 File Offset: 0x003549F5
	public void ReserveListChange(int ID, int mode = 0)
	{
		if (ID >= this.ReserveList.Count)
		{
			return;
		}
		if (mode == 0)
		{
			this.ReserveList[ID] = ID;
		}
		else
		{
			this.ReserveList[ID] = -1;
		}
	}

	// Token: 0x06007CC0 RID: 31936 RVA: 0x0035662E File Offset: 0x00354A2E
	public void SetLock()
	{
		this.bLock = true;
	}

	// Token: 0x06007CC1 RID: 31937 RVA: 0x00356637 File Offset: 0x00354A37
	public void UnLock()
	{
		this.bLock = false;
	}

	// Token: 0x06007CC2 RID: 31938 RVA: 0x00356640 File Offset: 0x00354A40
	public bool CheckLock()
	{
		return this.bLock;
	}

	// Token: 0x040064AE RID: 25774
	private int nPartsFormKind;

	// Token: 0x040064AF RID: 25775
	private int nItemKind;

	// Token: 0x040064B0 RID: 25776
	private int nCatKind;

	// Token: 0x040064B1 RID: 25777
	private int nID;

	// Token: 0x040064B2 RID: 25778
	private int nHeight;

	// Token: 0x040064B3 RID: 25779
	private bool bLock;

	// Token: 0x040064B4 RID: 25780
	private List<int> ReserveList = new List<int>();
}
