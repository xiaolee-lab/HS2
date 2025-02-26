using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000EEB RID: 3819
public class GridInfo : MonoBehaviour
{
	// Token: 0x06007C99 RID: 31897 RVA: 0x0035588A File Offset: 0x00353C8A
	private void Start()
	{
		this.nFloorNum = 1;
	}

	// Token: 0x06007C9A RID: 31898 RVA: 0x00355894 File Offset: 0x00353C94
	public void Init(int floorcnt = 0)
	{
		this.smallGrids.Add(new SmallGrid[4]);
		Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
		GridInfo.nSmallGridStackWallMax = 9;
		for (int i = 0; i < 4; i++)
		{
			this.smallGrids[floorcnt][i].m_state = 0;
			this.smallGrids[floorcnt][i].m_canRoof = 0;
			this.smallGrids[floorcnt][i].m_itemkind = new int[7];
			this.smallGrids[floorcnt][i].m_itemstackwall = new int[GridInfo.nSmallGridStackWallMax];
			this.smallGrids[floorcnt][i].m_itemdupulication = new int[GridInfo.nSmallGridStackWallMax];
			this.smallGrids[floorcnt][i].m_PutElement = new List<int>();
			this.ChangeSmallGrid(i, 0, -1, floorcnt, false);
			this.smallGrids[floorcnt][i].m_inRoom = false;
			this.smallGrids[floorcnt][i].m_UnderCarsol = false;
			this.smallGrids[floorcnt][i].m_color = componentsInChildren[i];
			this.ClearSmallGridItemKind(floorcnt, i);
			if (floorcnt != 0)
			{
				this.ChangeSmallGrid(i, 0, -1, floorcnt, false);
			}
		}
		this.nInRoom.Add(0);
		this.nCanRoof.Add(0);
		this.bUse.Add(false);
		this.nFloorPartsHeight.Add(0);
	}

	// Token: 0x06007C9B RID: 31899 RVA: 0x00355A20 File Offset: 0x00353E20
	public static void ChangeGridInfo(List<GridInfo> Grid, int floorcnt)
	{
		for (int i = 0; i < Grid.Count; i++)
		{
			int num = 0;
			GridInfo gridInfo = Grid[i];
			for (int j = 0; j < 4; j++)
			{
				if (gridInfo.smallGrids[floorcnt][j].m_inRoom)
				{
					num++;
				}
			}
			if (num == 4)
			{
				gridInfo.nInRoom[floorcnt] = 1;
			}
			else
			{
				gridInfo.nInRoom[floorcnt] = 0;
			}
			num = 0;
			for (int k = 0; k < 4; k++)
			{
				if (gridInfo.smallGrids[floorcnt][k].m_canRoof == 2)
				{
					num++;
				}
			}
			if (num == 4)
			{
				gridInfo.nCanRoof[floorcnt] = 1;
			}
			else
			{
				gridInfo.nCanRoof[floorcnt] = 0;
			}
		}
	}

	// Token: 0x06007C9C RID: 31900 RVA: 0x00355B08 File Offset: 0x00353F08
	public void ChangeSmallGrid(int ID, int state, int itemkind, int floorcnt, bool duplication = false)
	{
		if (state != 0 && itemkind >= 0)
		{
			if (!duplication)
			{
				switch (itemkind)
				{
				case 1:
					this.ChangeSmallGridItemKind(floorcnt, ID, 0, itemkind);
					goto IL_E9;
				case 2:
					this.ChangeSmallGridItemKind(floorcnt, ID, 0, itemkind);
					goto IL_E9;
				case 3:
					this.ChangeSmallGridItemKind(floorcnt, ID, 1, itemkind);
					goto IL_E9;
				case 4:
					this.ChangeSmallGridStack(floorcnt, ID, itemkind, 0);
					goto IL_E9;
				case 5:
					this.ChangeSmallGridItemKind(floorcnt, ID, 6, itemkind);
					goto IL_E9;
				case 10:
					this.ChangeSmallGridItemKind(floorcnt, ID, 3, itemkind);
					goto IL_E9;
				case 12:
					this.ChangeSmallGridItemKind(floorcnt, ID, 4, itemkind);
					goto IL_E9;
				case 14:
					this.ChangeSmallGridItemKind(floorcnt, ID, 5, itemkind);
					goto IL_E9;
				}
				this.ChangeSmallGridItemKind(floorcnt, ID, 2, itemkind);
				IL_E9:;
			}
			else
			{
				this.ChangeSmallGridStack(floorcnt, ID, itemkind, 0);
			}
		}
		else if (state == 0)
		{
			this.ClearSmallGridItemKind(floorcnt, ID);
			this.ChangeSmallGridStack(floorcnt, ID, 4, -1);
			this.ChangeSmallGridStack(floorcnt, ID, itemkind, -1);
		}
		if (this.smallGrids[floorcnt][ID].m_state != state)
		{
			this.smallGrids[floorcnt][ID].m_state = state;
		}
	}

	// Token: 0x06007C9D RID: 31901 RVA: 0x00355C70 File Offset: 0x00354070
	public void ChangeSmallGridColor(int floorcnt, int ID)
	{
		Color value;
		switch (this.smallGrids[floorcnt][ID].m_state)
		{
		case 0:
			value.r = 0.5019608f;
			value.g = 0.5019608f;
			value.b = 0.5019608f;
			value.a = 0.039215688f;
			break;
		case 1:
			value.r = 1f;
			value.g = 0f;
			value.b = 0f;
			value.a = 0.9411765f;
			break;
		case 2:
			value.r = 0.5019608f;
			value.g = 0.5019608f;
			value.b = 0.5019608f;
			value.a = 0.11764706f;
			break;
		default:
			value.r = 0.5019608f;
			value.g = 0.5019608f;
			value.b = 0.5019608f;
			value.a = 0.9411765f;
			break;
		}
		if (this.smallGrids[floorcnt][ID].m_UnderCarsol)
		{
			value.g = 1f;
		}
		if (this.smallGrids[floorcnt][ID].m_inRoom)
		{
			value.b = 1f;
		}
		this.smallGrids[floorcnt][ID].m_color.material.SetColor("_TintColor", value);
	}

	// Token: 0x06007C9E RID: 31902 RVA: 0x00355DF7 File Offset: 0x003541F7
	public void ChangeSmallGridColor(int floorcnt, int ID, Color changeColor)
	{
		this.smallGrids[floorcnt][ID].m_color.material.SetColor("_TintColor", changeColor);
	}

	// Token: 0x06007C9F RID: 31903 RVA: 0x00355E20 File Offset: 0x00354220
	public Color GetSmallGridColor(int floorcnt, int ID)
	{
		return this.smallGrids[floorcnt][ID].m_color.material.GetColor("_TintColor");
	}

	// Token: 0x06007CA0 RID: 31904 RVA: 0x00355E48 File Offset: 0x00354248
	public void SetInRoomSmallGrid(int id, bool roomstate, int floorcnt)
	{
		if (this.smallGrids[floorcnt][id].m_inRoom == roomstate)
		{
			return;
		}
		this.smallGrids[floorcnt][id].m_inRoom = roomstate;
	}

	// Token: 0x06007CA1 RID: 31905 RVA: 0x00355E80 File Offset: 0x00354280
	public void SetCanRoofSmallGrid(int id, int floorCnt, int setstate)
	{
		this.smallGrids[floorCnt][id].m_canRoof = setstate;
	}

	// Token: 0x06007CA2 RID: 31906 RVA: 0x00355E9A File Offset: 0x0035429A
	public void SetUseState(int floorcnt, bool use)
	{
		this.bUse[floorcnt] = use;
	}

	// Token: 0x06007CA3 RID: 31907 RVA: 0x00355EA9 File Offset: 0x003542A9
	public int GetStateSmallGrid(int id, int floorcnt)
	{
		return this.smallGrids[floorcnt][id].m_state;
	}

	// Token: 0x06007CA4 RID: 31908 RVA: 0x00355EC2 File Offset: 0x003542C2
	public int[] GetPartOnSmallGrid(int id, int floorcnt)
	{
		return this.smallGrids[floorcnt][id].m_itemkind;
	}

	// Token: 0x06007CA5 RID: 31909 RVA: 0x00355EDB File Offset: 0x003542DB
	public int[] GetStackWallOnSmallGrid(int id, int floorcnt)
	{
		return this.smallGrids[floorcnt][id].m_itemstackwall;
	}

	// Token: 0x06007CA6 RID: 31910 RVA: 0x00355EF4 File Offset: 0x003542F4
	public int[] GetStackPartsOnSmallGrid(int id, int floorcnt)
	{
		return this.smallGrids[floorcnt][id].m_itemdupulication;
	}

	// Token: 0x06007CA7 RID: 31911 RVA: 0x00355F0D File Offset: 0x0035430D
	public bool GetSmallGridInRoom(int id, int floorcnt)
	{
		return this.smallGrids[floorcnt][id].m_inRoom;
	}

	// Token: 0x06007CA8 RID: 31912 RVA: 0x00355F26 File Offset: 0x00354326
	public int GetSmallGridCanRoof(int id, int floorcnt)
	{
		return this.smallGrids[floorcnt][id].m_canRoof;
	}

	// Token: 0x06007CA9 RID: 31913 RVA: 0x00355F3F File Offset: 0x0035433F
	public int GetInRoomState(int floorcnt)
	{
		return this.nInRoom[floorcnt];
	}

	// Token: 0x06007CAA RID: 31914 RVA: 0x00355F4D File Offset: 0x0035434D
	public int GetCanRoofState(int floorcnt)
	{
		return this.nCanRoof[floorcnt];
	}

	// Token: 0x06007CAB RID: 31915 RVA: 0x00355F5B File Offset: 0x0035435B
	public bool GetUseState(int floorcnt)
	{
		return this.bUse[floorcnt];
	}

	// Token: 0x06007CAC RID: 31916 RVA: 0x00355F69 File Offset: 0x00354369
	public int GetFloorNum()
	{
		return this.nFloorNum;
	}

	// Token: 0x06007CAD RID: 31917 RVA: 0x00355F71 File Offset: 0x00354371
	public bool GetUnderTheCarsol(int nfloor, int smallid)
	{
		return this.smallGrids[nfloor][smallid].m_UnderCarsol;
	}

	// Token: 0x06007CAE RID: 31918 RVA: 0x00355F8A File Offset: 0x0035438A
	public void AddFloor()
	{
		this.Init(this.smallGrids.Count);
		this.nFloorNum++;
	}

	// Token: 0x06007CAF RID: 31919 RVA: 0x00355FAC File Offset: 0x003543AC
	public void DelFloor(int floorcnt)
	{
		this.smallGrids.RemoveRange(floorcnt, this.smallGrids.Count - floorcnt);
		this.nInRoom.RemoveRange(floorcnt, this.nInRoom.Count - floorcnt);
		this.nCanRoof.RemoveRange(floorcnt, this.nCanRoof.Count - floorcnt);
		this.bUse.RemoveRange(floorcnt, this.bUse.Count - floorcnt);
		this.nFloorPartsHeight.RemoveRange(floorcnt, this.nCanRoof.Count - floorcnt);
		this.nFloorNum = this.smallGrids.Count;
		if (this.nFloorNum > 0 && this.smallGrids[0][0].m_itemkind[2] == 11)
		{
			for (int i = 0; i < 4; i++)
			{
				this.ClearSmallGridItemKind(this.nFloorNum - 1, i);
				this.smallGrids[this.nFloorNum - 1][i].m_state = 0;
				this.ChangeSmallGridColor(this.nFloorNum - 1, i);
			}
		}
	}

	// Token: 0x06007CB0 RID: 31920 RVA: 0x003560C4 File Offset: 0x003544C4
	private void ClearSmallGridItemKind(int floor, int smallID)
	{
		for (int i = 0; i < 7; i++)
		{
			this.ChangeSmallGridItemKind(floor, smallID, i, -1);
		}
	}

	// Token: 0x06007CB1 RID: 31921 RVA: 0x003560ED File Offset: 0x003544ED
	public void ChangeSmallGridItemKind(int floor, int smallID, int changePlace, int changeState)
	{
		if (this.smallGrids[floor][smallID].m_itemkind[changePlace] == changeState)
		{
			return;
		}
		this.smallGrids[floor][smallID].m_itemkind[changePlace] = changeState;
	}

	// Token: 0x06007CB2 RID: 31922 RVA: 0x0035612B File Offset: 0x0035452B
	public void ChangeSmallGridUnderCarsol(int floor, int smallID, bool changeState)
	{
		this.smallGrids[floor][smallID].m_UnderCarsol = changeState;
		this.ChangeSmallGridColor(floor, smallID);
	}

	// Token: 0x06007CB3 RID: 31923 RVA: 0x00356150 File Offset: 0x00354550
	public void ChangeSmallGridStack(int floor, int smallID, int itemkind, int mode = 0)
	{
		int[] array;
		if (itemkind == 4)
		{
			array = this.smallGrids[floor][smallID].m_itemstackwall;
		}
		else
		{
			array = this.smallGrids[floor][smallID].m_itemdupulication;
		}
		int num = array.Count((int n) => n != -1);
		if (mode == 0)
		{
			if (num > GridInfo.nSmallGridStackWallMax)
			{
				return;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == -1)
				{
					array[i] = itemkind;
					break;
				}
			}
		}
		else if (mode == 1)
		{
			if (num <= 0)
			{
				return;
			}
			for (int j = GridInfo.nSmallGridStackWallMax - 1; j >= 0; j--)
			{
				if (array[j] != -1)
				{
					array[j] = -1;
					break;
				}
			}
		}
		else
		{
			for (int k = 0; k < GridInfo.nSmallGridStackWallMax; k++)
			{
				if (array[k] != -1)
				{
					array[k] = -1;
				}
			}
		}
	}

	// Token: 0x06007CB4 RID: 31924 RVA: 0x00356274 File Offset: 0x00354674
	public void SetSmallGridPutElement(int floorCnt, int smallgridID, int element, bool del = false, bool floor = false)
	{
		if (!del)
		{
			if (!floor || this.smallGrids[floorCnt][smallgridID].m_PutElement.Count == 0)
			{
				this.smallGrids[floorCnt][smallgridID].m_PutElement.Add(element);
			}
			else
			{
				this.smallGrids[floorCnt][smallgridID].m_PutElement.Insert(0, element);
			}
		}
		else
		{
			this.smallGrids[floorCnt][smallgridID].m_PutElement.RemoveAt(this.smallGrids[floorCnt][smallgridID].m_PutElement.Count - 1);
		}
	}

	// Token: 0x06007CB5 RID: 31925 RVA: 0x0035632F File Offset: 0x0035472F
	public List<int> GetSmallGridPutElement(int floorCnt, int smallgridID)
	{
		if (this.smallGrids[floorCnt][smallgridID].m_PutElement.Count <= 0)
		{
			return null;
		}
		return this.smallGrids[floorCnt][smallgridID].m_PutElement;
	}

	// Token: 0x040064A2 RID: 25762
	private List<SmallGrid[]> smallGrids = new List<SmallGrid[]>();

	// Token: 0x040064A3 RID: 25763
	private List<int> nInRoom = new List<int>();

	// Token: 0x040064A4 RID: 25764
	private List<int> nCanRoof = new List<int>();

	// Token: 0x040064A5 RID: 25765
	private List<bool> bUse = new List<bool>();

	// Token: 0x040064A6 RID: 25766
	public Vector3 InitPos;

	// Token: 0x040064A7 RID: 25767
	public int nID;

	// Token: 0x040064A8 RID: 25768
	public List<int> nFloorPartsHeight = new List<int>();

	// Token: 0x040064A9 RID: 25769
	public static int nSmallGridStackWallMax;

	// Token: 0x040064AA RID: 25770
	private int nFloorNum;

	// Token: 0x040064AB RID: 25771
	public const int nSmallGridNum = 4;

	// Token: 0x040064AC RID: 25772
	public const int nSmallGridItemKindMax = 7;
}
