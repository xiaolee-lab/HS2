using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000ED2 RID: 3794
public class CraftCommandList : Singleton<CraftCommandList>
{
	// Token: 0x06007C26 RID: 31782 RVA: 0x00346BE0 File Offset: 0x00344FE0
	public void GridChange(GridInfo source, CraftCommandList.ChangeValGrid val, int mode)
	{
		source.DelFloor(0);
		for (int i = 0; i < val.nFloorNum[mode]; i++)
		{
			source.AddFloor();
		}
		for (int j = 0; j < val.nFloorNum[mode]; j++)
		{
			for (int k = 0; k < 4; k++)
			{
				source.SetCanRoofSmallGrid(k, j, val.smallGrids[mode][j][k].m_canRoof);
				source.SetInRoomSmallGrid(k, val.smallGrids[mode][j][k].m_inRoom, j);
				source.ChangeSmallGrid(k, val.smallGrids[mode][j][k].m_state, -1, j, false);
				for (int l = 0; l < 7; l++)
				{
					source.ChangeSmallGridItemKind(j, k, l, val.smallGrids[mode][j][k].m_itemkind[l]);
				}
				int num = 0;
				for (int m = 0; m < val.smallGrids[mode][j][k].m_itemstackwall.Length; m++)
				{
					if (val.smallGrids[mode][j][k].m_itemstackwall[m] == -1)
					{
						break;
					}
					num++;
				}
				for (int n = 0; n < num; n++)
				{
					source.ChangeSmallGridStack(j, k, 4, 0);
				}
				int num2 = 0;
				for (int num3 = 0; num3 < val.smallGrids[mode][j][k].m_itemstackwall.Length; num3++)
				{
					if (val.smallGrids[mode][j][k].m_itemdupulication[num3] == -1)
					{
						break;
					}
					num2++;
				}
				for (int num4 = 0; num4 < num2; num4++)
				{
					source.ChangeSmallGridStack(j, k, val.smallGrids[mode][j][k].m_itemdupulication[num4], 0);
				}
				source.ChangeSmallGridUnderCarsol(j, k, source.GetUnderTheCarsol(j, k));
				source.ChangeSmallGridColor(j, k);
				if (val.smallGrids[mode][j][k].m_PutElement != null)
				{
					for (int num5 = 0; num5 < val.smallGrids[mode][j][k].m_PutElement.Count; num5++)
					{
						source.SetSmallGridPutElement(j, k, val.smallGrids[mode][j][k].m_PutElement[num5], false, false);
					}
				}
			}
			source.SetUseState(j, val.bUse[mode][j]);
		}
		source.nFloorPartsHeight = val.nFloorPartsHeight[mode];
		if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt >= source.GetFloorNum())
		{
			Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt--;
		}
		source.gameObject.transform.position = val.Pos[mode];
		if (source.GetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt))
		{
			source.gameObject.SetActive(true);
		}
	}

	// Token: 0x06007C27 RID: 31783 RVA: 0x00346F34 File Offset: 0x00345334
	public void PartsChange(List<BuildPartsPool>[] source, CraftCommandList.ChangeValParts val, int mode, bool Auto)
	{
		GameObject gameObject = source[val.nFormKind][val.nPoolID].GetList()[val.nItemID];
		if (!Auto)
		{
			if (val.active[mode])
			{
				source[val.nFormKind][val.nPoolID].ReserveListChange(val.nItemID, 0);
			}
			else
			{
				source[val.nFormKind][val.nPoolID].ReserveListChange(val.nItemID, -1);
			}
			gameObject.SetActive(val.active[mode]);
		}
		else
		{
			if (val.ReserveList[mode] >= 0)
			{
				source[val.nFormKind][val.nPoolID].ReserveListChange(val.nItemID, 0);
			}
			else
			{
				source[val.nFormKind][val.nPoolID].ReserveListChange(val.nItemID, -1);
			}
			if (val.nPutFloor[mode] == 0)
			{
				gameObject.SetActive(val.active[mode]);
			}
			else if (val.nPutFloor[mode] > 0 && val.nPutFloor[mode] <= Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)
			{
				gameObject.SetActive(source[val.nFormKind][val.nPoolID].ReserveListCheck(val.nItemID));
			}
			else
			{
				if (val.nPutFloor[mode] >= 0)
				{
					source[val.nFormKind][val.nPoolID].ReserveListChange(val.nItemID, 0);
				}
				else
				{
					source[val.nFormKind][val.nPoolID].ReserveListChange(val.nItemID, -1);
				}
				gameObject.SetActive(false);
			}
		}
		gameObject.transform.position = val.Pos[mode];
		gameObject.transform.rotation = val.Rot[mode];
		BuildPartsInfo component = gameObject.GetComponent<BuildPartsInfo>();
		component.nPutFloor = val.nPutFloor[mode];
		component.SetDirection(val.nDirection[mode]);
	}

	// Token: 0x02000ED3 RID: 3795
	public class ChangeValParts
	{
		// Token: 0x040063E5 RID: 25573
		public int nFormKind;

		// Token: 0x040063E6 RID: 25574
		public int nPoolID;

		// Token: 0x040063E7 RID: 25575
		public int nItemID;

		// Token: 0x040063E8 RID: 25576
		public bool[] active = new bool[2];

		// Token: 0x040063E9 RID: 25577
		public Vector3[] Pos = new Vector3[2];

		// Token: 0x040063EA RID: 25578
		public Quaternion[] Rot = new Quaternion[2];

		// Token: 0x040063EB RID: 25579
		public int[] nPutFloor = new int[2];

		// Token: 0x040063EC RID: 25580
		public int[] nDirection = new int[2];

		// Token: 0x040063ED RID: 25581
		public int[] ReserveList = new int[2];
	}

	// Token: 0x02000ED4 RID: 3796
	public class ChangeValGrid
	{
		// Token: 0x06007C29 RID: 31785 RVA: 0x003471A0 File Offset: 0x003455A0
		public ChangeValGrid()
		{
			for (int i = 0; i < 2; i++)
			{
				this.smallGrids[i] = new List<SmallGrid[]>();
				this.nInRoom[i] = new List<int>();
				this.nCanRoof[i] = new List<int>();
				this.bUse[i] = new List<bool>();
				this.nFloorPartsHeight[i] = new List<int>();
				this.colors[i] = new List<Color[]>();
			}
		}

		// Token: 0x040063EE RID: 25582
		public Vector3[] Pos = new Vector3[]
		{
			default(Vector3),
			default(Vector3)
		};

		// Token: 0x040063EF RID: 25583
		public List<SmallGrid[]>[] smallGrids = new List<SmallGrid[]>[2];

		// Token: 0x040063F0 RID: 25584
		public List<int>[] nInRoom = new List<int>[2];

		// Token: 0x040063F1 RID: 25585
		public List<int>[] nCanRoof = new List<int>[2];

		// Token: 0x040063F2 RID: 25586
		public List<bool>[] bUse = new List<bool>[2];

		// Token: 0x040063F3 RID: 25587
		public List<int>[] nFloorPartsHeight = new List<int>[2];

		// Token: 0x040063F4 RID: 25588
		public int[] nFloorNum = new int[2];

		// Token: 0x040063F5 RID: 25589
		public List<Color[]>[] colors = new List<Color[]>[2];
	}

	// Token: 0x02000ED5 RID: 3797
	public static class PutBuildPartCommand
	{
		// Token: 0x02000ED6 RID: 3798
		public class Command : ICommand
		{
			// Token: 0x06007C2A RID: 31786 RVA: 0x0034729D File Offset: 0x0034569D
			public Command(CraftCommandList.ChangeValGrid[] _changeValGrids, CraftCommandList.ChangeValParts[] _changeValParts, CraftCommandList.ChangeValParts[] _changeValAutoFloor, int[] _maxFloorCnt)
			{
				this.changeValGrids = _changeValGrids;
				this.changeValParts = _changeValParts;
				this.changeValAutoFloor = _changeValAutoFloor;
				this.maxFloorCnt = _maxFloorCnt;
			}

			// Token: 0x06007C2B RID: 31787 RVA: 0x003472D0 File Offset: 0x003456D0
			public void Undo()
			{
				for (int i = 0; i < Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo.Count; i++)
				{
					Singleton<CraftCommandList>.Instance.GridChange(Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo[i], this.changeValGrids[i], 0);
				}
				for (int j = 0; j < this.changeValAutoFloor.Length; j++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValAutoFloor[j], 0, true);
				}
				for (int k = 0; k < this.changeValParts.Length; k++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValParts[k], 0, false);
				}
				Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt = this.maxFloorCnt[0];
			}

			// Token: 0x06007C2C RID: 31788 RVA: 0x003473A0 File Offset: 0x003457A0
			public void Redo()
			{
				for (int i = 0; i < Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo.Count; i++)
				{
					Singleton<CraftCommandList>.Instance.GridChange(Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo[i], this.changeValGrids[i], 1);
				}
				for (int j = 0; j < this.changeValAutoFloor.Length; j++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValAutoFloor[j], 1, true);
				}
				for (int k = 0; k < this.changeValParts.Length; k++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValParts[k], 1, false);
				}
				Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt = this.maxFloorCnt[1];
			}

			// Token: 0x040063F6 RID: 25590
			private int[] maxFloorCnt = new int[2];

			// Token: 0x040063F7 RID: 25591
			private CraftCommandList.ChangeValGrid[] changeValGrids;

			// Token: 0x040063F8 RID: 25592
			private CraftCommandList.ChangeValParts[] changeValParts;

			// Token: 0x040063F9 RID: 25593
			private CraftCommandList.ChangeValParts[] changeValAutoFloor;
		}
	}

	// Token: 0x02000ED7 RID: 3799
	public static class DeadBuildPartCommand
	{
		// Token: 0x02000ED8 RID: 3800
		public class Command : ICommand
		{
			// Token: 0x06007C2D RID: 31789 RVA: 0x00347470 File Offset: 0x00345870
			public Command(CraftCommandList.ChangeValGrid[] _changeValGrids, CraftCommandList.ChangeValParts[] _changeValParts, CraftCommandList.ChangeValParts[] _changeValAutoParts, int[] _maxFloorCnt)
			{
				this.changeValGrids = _changeValGrids;
				this.changeValParts = _changeValParts;
				this.changeValAutoParts = _changeValAutoParts;
				this.maxFloorCnt = _maxFloorCnt;
			}

			// Token: 0x06007C2E RID: 31790 RVA: 0x003474A4 File Offset: 0x003458A4
			public void Undo()
			{
				for (int i = 0; i < Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo.Count; i++)
				{
					Singleton<CraftCommandList>.Instance.GridChange(Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo[i], this.changeValGrids[i], 0);
				}
				for (int j = 0; j < this.changeValAutoParts.Length; j++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValAutoParts[j], 0, true);
				}
				for (int k = 0; k < this.changeValParts.Length; k++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValParts[k], 0, false);
				}
				Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt = this.maxFloorCnt[0];
			}

			// Token: 0x06007C2F RID: 31791 RVA: 0x00347574 File Offset: 0x00345974
			public void Redo()
			{
				for (int i = 0; i < Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo.Count; i++)
				{
					Singleton<CraftCommandList>.Instance.GridChange(Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo[i], this.changeValGrids[i], 1);
				}
				for (int j = 0; j < this.changeValAutoParts.Length; j++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValAutoParts[j], 1, true);
				}
				for (int k = 0; k < this.changeValParts.Length; k++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValParts[k], 1, false);
				}
				Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt = this.maxFloorCnt[1];
			}

			// Token: 0x040063FA RID: 25594
			private int[] maxFloorCnt = new int[2];

			// Token: 0x040063FB RID: 25595
			private CraftCommandList.ChangeValGrid[] changeValGrids;

			// Token: 0x040063FC RID: 25596
			private CraftCommandList.ChangeValParts[] changeValParts;

			// Token: 0x040063FD RID: 25597
			private CraftCommandList.ChangeValParts[] changeValAutoParts;
		}
	}

	// Token: 0x02000ED9 RID: 3801
	public static class SelectBuildPartCommand
	{
		// Token: 0x02000EDA RID: 3802
		public class Command : ICommand
		{
			// Token: 0x06007C30 RID: 31792 RVA: 0x00347644 File Offset: 0x00345A44
			public Command(CraftCommandList.ChangeValGrid[] _changeValGrids, CraftCommandList.ChangeValParts[] _changeValParts, CraftCommandList.ChangeValParts[] _changeValAutoParts, int[] _maxFloorCnt)
			{
				this.changeValGrids = _changeValGrids;
				this.changeValParts = _changeValParts;
				this.changeValAutoParts = _changeValAutoParts;
				this.maxFloorCnt = _maxFloorCnt;
			}

			// Token: 0x06007C31 RID: 31793 RVA: 0x00347678 File Offset: 0x00345A78
			public void Undo()
			{
				for (int i = 0; i < this.changeValParts.Length; i++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValParts[i], 0, false);
				}
				for (int j = 0; j < this.changeValAutoParts.Length; j++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValAutoParts[j], 0, true);
				}
				for (int k = 0; k < Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo.Count; k++)
				{
					Singleton<CraftCommandList>.Instance.GridChange(Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo[k], this.changeValGrids[k], 0);
				}
				Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt = this.maxFloorCnt[0];
			}

			// Token: 0x06007C32 RID: 31794 RVA: 0x00347748 File Offset: 0x00345B48
			public void Redo()
			{
				for (int i = 0; i < this.changeValParts.Length; i++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValParts[i], 1, false);
				}
				for (int j = 0; j < this.changeValAutoParts.Length; j++)
				{
					Singleton<CraftCommandList>.Instance.PartsChange(Singleton<CraftCommandListBaseObject>.Instance.BaseParts, this.changeValAutoParts[j], 1, true);
				}
				for (int k = 0; k < Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo.Count; k++)
				{
					Singleton<CraftCommandList>.Instance.GridChange(Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo[k], this.changeValGrids[k], 1);
				}
				Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt = this.maxFloorCnt[1];
			}

			// Token: 0x040063FE RID: 25598
			private int[] maxFloorCnt = new int[2];

			// Token: 0x040063FF RID: 25599
			private CraftCommandList.ChangeValGrid[] changeValGrids;

			// Token: 0x04006400 RID: 25600
			private CraftCommandList.ChangeValParts[] changeValParts;

			// Token: 0x04006401 RID: 25601
			private CraftCommandList.ChangeValParts[] changeValAutoParts;
		}
	}
}
