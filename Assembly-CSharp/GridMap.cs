using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000EE6 RID: 3814
public class GridMap
{
	// Token: 0x06007C86 RID: 31878 RVA: 0x0035491A File Offset: 0x00352D1A
	private void Start()
	{
	}

	// Token: 0x06007C87 RID: 31879 RVA: 0x0035491C File Offset: 0x00352D1C
	public void Init(List<GameObject> GridList, int numX, int numZ)
	{
		this.nNumX = numX * 2;
		this.nNumZ = numZ * 2;
		this.Map = new GridMapCel[this.nNumZ, this.nNumX];
		this.Serched = new SerchMarcker[this.nNumZ, this.nNumX];
		this.ChangeCraftMap(GridList, 0);
	}

	// Token: 0x06007C88 RID: 31880 RVA: 0x00354974 File Offset: 0x00352D74
	public void ChangeCraftMap(List<GameObject> GridList, int floorCnt = 0)
	{
		int[] stackWallOnMap = new int[5];
		for (int i = 0; i < this.nNumZ; i++)
		{
			for (int j = 0; j < this.nNumX; j++)
			{
				GridInfo component = GridList[j / 2 + this.nNumX / 2 * (i / 2)].GetComponent<GridInfo>();
				if (i % 2 == 0)
				{
					stackWallOnMap = component.GetStackWallOnSmallGrid(j % 2, floorCnt);
					this.Map[i, j].PartsOnMap = component.GetPartOnSmallGrid(j % 2, floorCnt);
					this.Map[i, j].StackWallOnMap = stackWallOnMap;
					this.Map[i, j].GridID = j / 2 + this.nNumX / 2 * (i / 2);
					this.Map[i, j].smallGridID = j % 2;
					this.Map[i, j].smallGridCanRoof = component.GetSmallGridCanRoof(j % 2, floorCnt);
				}
				else
				{
					stackWallOnMap = component.GetStackWallOnSmallGrid(2 + j % 2, floorCnt);
					this.Map[i, j].PartsOnMap = component.GetPartOnSmallGrid(2 + j % 2, floorCnt);
					this.Map[i, j].StackWallOnMap = stackWallOnMap;
					this.Map[i, j].GridID = j / 2 + this.nNumX / 2 * (i / 2);
					this.Map[i, j].smallGridID = 2 + j % 2;
					this.Map[i, j].smallGridCanRoof = component.GetSmallGridCanRoof(j % 2, floorCnt);
				}
			}
		}
	}

	// Token: 0x06007C89 RID: 31881 RVA: 0x00354AFF File Offset: 0x00352EFF
	public void CraftMapSearchRoom(List<GameObject> GridList, int floorcnt)
	{
		this.WallCheck();
		this.SmallGridInRoomArrangement();
		this.ChangeGrid(GridList, floorcnt);
		this.RoofReserve.Clear();
	}

	// Token: 0x06007C8A RID: 31882 RVA: 0x00354B20 File Offset: 0x00352F20
	private void WallCheck()
	{
		for (int i = 0; i < this.nNumZ; i++)
		{
			for (int j = 0; j < this.nNumX; j++)
			{
				bool flag = false;
				for (int k = 0; k < this.nJudgeItemKind.Length; k++)
				{
					flag = (this.Map[i, j].PartsOnMap[1] == this.nJudgeItemKind[k] || this.Map[i, j].PartsOnMap[2] == this.nJudgeItemKind[k]);
					if (flag)
					{
						break;
					}
				}
				if (!flag)
				{
					for (int l = 0; l < this.Map[i, j].StackWallOnMap.Length; l++)
					{
						flag = (flag || this.Map[i, j].StackWallOnMap[l] == 4);
						if (flag)
						{
							break;
						}
					}
				}
				if (flag)
				{
					this.Map[i, j].smallGridCanRoof = 2;
				}
				else
				{
					int num = this.CheckWallNum(j, i, 0);
					if (num > 2)
					{
						this.Map[i, j].smallGridCanRoof = 1;
						RoofReservePos item;
						item.PosX = j;
						item.PosZ = i;
						item.WallHitNum = num;
						this.RoofReserve.Add(item);
					}
					else
					{
						this.Map[i, j].smallGridCanRoof = 0;
					}
				}
			}
		}
	}

	// Token: 0x06007C8B RID: 31883 RVA: 0x00354CAC File Offset: 0x003530AC
	private void SmallGridInRoomArrangement()
	{
		for (int i = 0; i < this.RoofReserve.Count; i++)
		{
			this.SerchedAllFalse();
			bool[] array = new bool[]
			{
				true,
				true,
				true,
				true
			};
			this.Serched[this.RoofReserve[i].PosZ, this.RoofReserve[i].PosX].Serched = true;
			array[0] = this.SerchCanRoof(this.RoofReserve[i].PosX, this.RoofReserve[i].PosZ + 1, 0);
			array[1] = this.SerchCanRoof(this.RoofReserve[i].PosX + 1, this.RoofReserve[i].PosZ, 1);
			array[2] = this.SerchCanRoof(this.RoofReserve[i].PosX, this.RoofReserve[i].PosZ - 1, 2);
			array[3] = this.SerchCanRoof(this.RoofReserve[i].PosX - 1, this.RoofReserve[i].PosZ, 3);
			if (array[0] & array[1] & array[2] & array[3])
			{
				this.Serched[this.RoofReserve[i].PosZ, this.RoofReserve[i].PosX].rootTrue = true;
				this.Map[this.RoofReserve[i].PosZ, this.RoofReserve[i].PosX].smallGridCanRoof = 2;
				this.ChangeMapCanRoof(this.Serched);
				this.CheckWallNum(this.RoofReserve[i].PosX, this.RoofReserve[i].PosZ, 1);
			}
			else
			{
				this.Map[this.RoofReserve[i].PosZ, this.RoofReserve[i].PosX].smallGridCanRoof = 0;
			}
		}
	}

	// Token: 0x06007C8C RID: 31884 RVA: 0x00354F08 File Offset: 0x00353308
	private void ChangeGrid(List<GameObject> GridList, int floorCnt = 0)
	{
		for (int i = 0; i < this.nNumZ; i++)
		{
			for (int j = 0; j < this.nNumX; j++)
			{
				GridList[this.Map[i, j].GridID].GetComponent<GridInfo>().SetCanRoofSmallGrid(this.Map[i, j].smallGridID, floorCnt, this.Map[i, j].smallGridCanRoof);
			}
		}
	}

	// Token: 0x06007C8D RID: 31885 RVA: 0x00354F8C File Offset: 0x0035338C
	public bool CraftMapRoofDecide()
	{
		for (int i = 0; i < this.nNumZ; i++)
		{
			for (int j = 0; j < this.nNumX; j++)
			{
				if (this.Map[i, j].smallGridCanRoof == 2)
				{
					bool flag = false;
					for (int k = 0; k < this.nJudgeItemKind.Length; k++)
					{
						flag = (this.Map[i, j].PartsOnMap[1] == this.nJudgeItemKind[k] || this.Map[i, j].PartsOnMap[2] == this.nJudgeItemKind[k]);
						if (flag)
						{
							break;
						}
					}
					if (!flag)
					{
						for (int l = 0; l < this.Map[i, j].StackWallOnMap.Length; l++)
						{
							flag = (flag || this.Map[i, j].StackWallOnMap[l] == 4);
							if (flag)
							{
								break;
							}
						}
					}
					if (!flag)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06007C8E RID: 31886 RVA: 0x003550BC File Offset: 0x003534BC
	private int CheckWallNum(int x, int z, int Mode = 0)
	{
		bool[] array = new bool[]
		{
			this.CheckWall(x, z + 1, Mode),
			this.CheckWall(x + 1, z, Mode),
			this.CheckWall(x, z - 1, Mode),
			this.CheckWall(x - 1, z, Mode),
			this.CheckWall(x + 1, z + 1, Mode),
			this.CheckWall(x + 1, z - 1, Mode),
			this.CheckWall(x - 1, z - 1, Mode),
			this.CheckWall(x - 1, z + 1, Mode)
		};
		int num = 0;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06007C8F RID: 31887 RVA: 0x00355170 File Offset: 0x00353570
	private bool CheckWall(int x, int z, int Mode = 0)
	{
		if (x < 0 || z < 0 || x == this.nNumX || z == this.nNumZ)
		{
			return false;
		}
		for (int i = 0; i < this.nJudgeItemKind.Length; i++)
		{
			bool flag = this.Map[z, x].PartsOnMap[1] == this.nJudgeItemKind[i] || this.Map[z, x].PartsOnMap[2] == this.nJudgeItemKind[i];
			if (flag)
			{
				if (Mode == 1)
				{
					this.Map[z, x].smallGridCanRoof = 2;
				}
				return true;
			}
		}
		for (int j = 0; j < this.Map[z, x].StackWallOnMap.Length; j++)
		{
			bool flag = this.Map[z, x].StackWallOnMap[j] == 4;
			if (flag)
			{
				if (Mode == 1)
				{
					this.Map[z, x].smallGridCanRoof = 2;
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06007C90 RID: 31888 RVA: 0x0035528C File Offset: 0x0035368C
	private bool SerchCanRoof(int x, int z, int Dir)
	{
		if (x < 0 || z < 0 || x == this.nNumX - 1 || z == this.nNumZ - 1)
		{
			return false;
		}
		if (this.Serched[z, x].Serched)
		{
			return true;
		}
		this.Serched[z, x].Serched = true;
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < this.nJudgeItemKind.Length; i++)
		{
			flag2 = (this.Map[z, x].PartsOnMap[1] == this.nJudgeItemKind[i] || this.Map[z, x].PartsOnMap[2] == this.nJudgeItemKind[i]);
			if (flag2)
			{
				break;
			}
		}
		if (!flag2)
		{
			for (int j = 0; j < this.Map[z, x].StackWallOnMap.Length; j++)
			{
				flag2 = (this.Map[z, x].StackWallOnMap[j] == 4);
				if (flag2)
				{
					break;
				}
			}
		}
		if (flag2)
		{
			switch (Dir)
			{
			case 0:
				flag = this.CheckCanRoof(x, z - 1);
				break;
			case 1:
				flag = this.CheckCanRoof(x - 1, z);
				break;
			case 2:
				flag = this.CheckCanRoof(x, z + 1);
				break;
			case 3:
				flag = this.CheckCanRoof(x + 1, z);
				break;
			}
		}
		if (flag)
		{
			return flag;
		}
		flag = false;
		switch (Dir)
		{
		case 0:
			flag = this.SerchCanRoof(x, z + 1, 0);
			if (!flag)
			{
				this.Serched[z, x].rootTrue = false;
				return flag;
			}
			flag = this.SerchCanRoof(x + 1, z, 1);
			if (!flag)
			{
				this.Serched[z, x].rootTrue = false;
				return flag;
			}
			flag = this.SerchCanRoof(x - 1, z, 3);
			break;
		case 1:
			flag = this.SerchCanRoof(x, z + 1, 0);
			if (!flag)
			{
				this.Serched[z, x].rootTrue = false;
				return flag;
			}
			flag = this.SerchCanRoof(x + 1, z, 1);
			if (!flag)
			{
				this.Serched[z, x].rootTrue = false;
				return flag;
			}
			flag = this.SerchCanRoof(x, z - 1, 2);
			break;
		case 2:
			flag = this.SerchCanRoof(x + 1, z, 1);
			if (!flag)
			{
				this.Serched[z, x].rootTrue = false;
				return flag;
			}
			flag = this.SerchCanRoof(x, z - 1, 2);
			if (!flag)
			{
				this.Serched[z, x].rootTrue = false;
				return flag;
			}
			flag = this.SerchCanRoof(x - 1, z, 3);
			break;
		case 3:
			flag = this.SerchCanRoof(x, z + 1, 0);
			if (!flag)
			{
				this.Serched[z, x].rootTrue = false;
				return flag;
			}
			flag = this.SerchCanRoof(x, z - 1, 2);
			if (!flag)
			{
				this.Serched[z, x].rootTrue = false;
				return flag;
			}
			flag = this.SerchCanRoof(x - 1, z, 3);
			break;
		}
		if (!flag)
		{
			this.Serched[z, x].rootTrue = false;
		}
		else
		{
			this.Serched[z, x].rootTrue = true;
		}
		return flag;
	}

	// Token: 0x06007C91 RID: 31889 RVA: 0x003555E4 File Offset: 0x003539E4
	private bool CheckCanRoof(int x, int z)
	{
		bool[] array = new bool[]
		{
			this.CheckWall(x, z + 1, 0),
			this.CheckWall(x + 1, z, 0),
			this.CheckWall(x, z - 1, 0),
			this.CheckWall(x - 1, z, 0),
			this.CheckWall(x + 1, z + 1, 0),
			this.CheckWall(x + 1, z - 1, 0),
			this.CheckWall(x - 1, z - 1, 0),
			this.CheckWall(x - 1, z + 1, 0)
		};
		return (array[0] && array[4] && array[7]) || (array[1] && array[4] && array[5]) || (array[2] && array[5] && array[6]) || (array[3] && array[6] && array[7]);
	}

	// Token: 0x06007C92 RID: 31890 RVA: 0x003556DC File Offset: 0x00353ADC
	private void ChangeMapCanRoof(SerchMarcker[,] checker)
	{
		for (int i = 0; i < this.nNumZ; i++)
		{
			for (int j = 0; j < this.nNumX; j++)
			{
				if (checker[i, j].rootTrue)
				{
					this.Map[i, j].smallGridCanRoof = 2;
				}
			}
		}
	}

	// Token: 0x06007C93 RID: 31891 RVA: 0x00355744 File Offset: 0x00353B44
	private void SerchedAllFalse()
	{
		for (int i = 0; i < this.nNumZ; i++)
		{
			for (int j = 0; j < this.nNumX; j++)
			{
				this.Serched[i, j].Serched = false;
				this.Serched[i, j].rootTrue = false;
			}
		}
	}

	// Token: 0x0400647C RID: 25724
	private int nNumX;

	// Token: 0x0400647D RID: 25725
	private int nNumZ;

	// Token: 0x0400647E RID: 25726
	private GridMapCel[,] Map;

	// Token: 0x0400647F RID: 25727
	private SerchMarcker[,] Serched;

	// Token: 0x04006480 RID: 25728
	private List<RoofReservePos> RoofReserve = new List<RoofReservePos>();

	// Token: 0x04006481 RID: 25729
	private int[] nJudgeItemKind = new int[]
	{
		3,
		16,
		6,
		15
	};
}
