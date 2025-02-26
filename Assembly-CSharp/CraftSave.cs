using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AIProject;
using UnityEngine;

// Token: 0x02000EE2 RID: 3810
public class CraftSave : Singleton<CraftSave>
{
	// Token: 0x06007C80 RID: 31872 RVA: 0x00352964 File Offset: 0x00350D64
	public void Init()
	{
		this.data.MaxFloorNum = 0;
		this.data.nPutPartsNum = 0;
		this.data.GridPos = new List<Vector3>();
		this.data.GridUseState = new List<bool>();
		this.data.nFloorPartsHeight = new List<int>();
		this.data.SmallGridState = new List<List<List<int>>>();
		this.data.SmallGridOnParts = new List<List<List<int[]>>>();
		this.data.SmallGridOnStackWall = new List<List<List<int[]>>>();
		this.data.SmallGridCanRoofState = new List<List<List<int>>>();
		this.data.SmallGridInRoomState = new List<List<List<bool>>>();
		this.data.BuildPartsGridKind = new List<int>();
		this.data.BuildPartsKind = new List<int>();
		this.data.BuildPartsFloor = new List<int>();
		this.data.BuildPartsPos = new List<Vector3>();
		this.data.BuildPartsRot = new List<Quaternion>();
		this.data.BuildPartsPutGridInfos = new List<List<int>>();
		this.data.BuildPartsPutSmallGridInfos = new List<List<int>>();
		this.data.BuildPartsPutGridInfosNum = new List<int>();
		this.data.tmpGridActiveList = new List<bool[]>();
		this.data.tmpGridActiveListUpdate = new List<bool>();
		this.data.MaxPutHeight = new List<int>();
		this.gameScreenShotAssist = this.virtualCamera.GetComponent<GameScreenShotAssist>();
	}

	// Token: 0x06007C81 RID: 31873 RVA: 0x00352ACC File Offset: 0x00350ECC
	public void Save(ObjPool Grid)
	{
		List<BuildPartsPool>[] baseParts = Singleton<CraftCommandListBaseObject>.Instance.BaseParts;
		int nMaxFloorCnt = Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt;
		this.data.MaxFloorNum = nMaxFloorCnt;
		this.data.nPutPartsNum = Singleton<CraftCommandListBaseObject>.Instance.nPutPartsNum;
		List<GameObject> list = Grid.GetList();
		for (int i = 0; i < list.Count; i++)
		{
			this.data.SmallGridState.Add(new List<List<int>>());
			this.data.SmallGridOnParts.Add(new List<List<int[]>>());
			this.data.SmallGridOnStackWall.Add(new List<List<int[]>>());
			this.data.SmallGridCanRoofState.Add(new List<List<int>>());
			this.data.SmallGridInRoomState.Add(new List<List<bool>>());
			GridInfo component = list[i].GetComponent<GridInfo>();
			this.data.GridPos.Add(component.InitPos);
			for (int j = 0; j < nMaxFloorCnt; j++)
			{
				this.data.SmallGridState[i].Add(new List<int>());
				this.data.SmallGridOnParts[i].Add(new List<int[]>());
				this.data.SmallGridOnStackWall[i].Add(new List<int[]>());
				this.data.SmallGridCanRoofState[i].Add(new List<int>());
				this.data.SmallGridInRoomState[i].Add(new List<bool>());
				this.data.GridUseState.Add(component.GetUseState(j));
				this.data.nFloorPartsHeight.Add(component.nFloorPartsHeight[j]);
				for (int k = 0; k < 4; k++)
				{
					this.data.SmallGridState[i][j].Add(component.GetStateSmallGrid(k, j));
					int[] partOnSmallGrid = component.GetPartOnSmallGrid(k, j);
					this.data.SmallGridOnParts[i][j].Add(partOnSmallGrid);
					this.data.SmallGridOnStackWall[i][j].Add(component.GetStackWallOnSmallGrid(k, j));
					this.data.SmallGridCanRoofState[i][j].Add(component.GetSmallGridCanRoof(k, j));
					this.data.SmallGridInRoomState[i][j].Add(component.GetSmallGridInRoom(k, j));
				}
			}
		}
		for (int l = 0; l < baseParts.Length; l++)
		{
			for (int m = 0; m < baseParts[l].Count; m++)
			{
				List<GameObject> list2 = baseParts[l][m].GetList();
				for (int n = 0; n < list2.Count; n++)
				{
					if (baseParts[l][m].ReserveListCheck(n))
					{
						BuildPartsInfo component2 = list2[n].GetComponent<BuildPartsInfo>();
						this.data.BuildPartsGridKind.Add(l);
						this.data.BuildPartsKind.Add(m);
						this.data.BuildPartsFloor.Add(component2.nPutFloor);
						this.data.BuildPartsPos.Add(list2[n].transform.localPosition);
						this.data.BuildPartsRot.Add(list2[n].transform.localRotation);
						this.data.BuildPartsPutGridInfos.Add((from v in component2.putGridInfos
						select v.nID).ToList<int>());
						this.data.BuildPartsPutSmallGridInfos.Add(component2.putSmallGridInfos);
						this.data.BuildPartsPutGridInfosNum.Add(component2.putGridInfos.Count);
					}
				}
			}
		}
		this.data.tmpGridActiveList = Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList;
		this.data.tmpGridActiveListUpdate = Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveListUpdate;
		this.data.MaxPutHeight = Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight;
		string str = string.Format("/CraftSave{0}_{1:MM}{2}_{3:HH}{4:mm}_{5:ss}{6:ff}.png", new object[]
		{
			DateTime.Now.Year,
			DateTime.Now,
			DateTime.Now.Day,
			DateTime.Now,
			DateTime.Now,
			DateTime.Now,
			DateTime.Now
		});
		string path = Application.dataPath + "/in-house/Scripts/Game/Scene/Map/Craft/SaveData" + str;
		FileStream output = new FileStream(path, FileMode.Create, FileAccess.Write);
		BinaryWriter binaryWriter = new BinaryWriter(output, Encoding.UTF8);
		this.pngData = this.CreatePngScreen(320, 180);
		binaryWriter.Write(this.pngData);
		binaryWriter.Write(this.data.MaxFloorNum);
		binaryWriter.Write(this.data.nPutPartsNum);
		for (int num = 0; num < this.data.GridPos.Count; num++)
		{
			binaryWriter.Write((double)this.data.GridPos[num].x);
			binaryWriter.Write((double)this.data.GridPos[num].y);
			binaryWriter.Write((double)this.data.GridPos[num].z);
		}
		binaryWriter.Write("P");
		for (int num2 = 0; num2 < this.data.GridUseState.Count; num2++)
		{
			binaryWriter.Write(this.data.GridUseState[num2]);
		}
		binaryWriter.Write("U");
		for (int num3 = 0; num3 < this.data.nFloorPartsHeight.Count; num3++)
		{
			binaryWriter.Write(this.data.nFloorPartsHeight[num3]);
		}
		binaryWriter.Write("F");
		for (int num4 = 0; num4 < list.Count; num4++)
		{
			for (int num5 = 0; num5 < nMaxFloorCnt; num5++)
			{
				for (int num6 = 0; num6 < this.data.SmallGridState[num4][num5].Count; num6++)
				{
					binaryWriter.Write(this.data.SmallGridState[num4][num5][num6]);
					for (int num7 = 0; num7 < this.data.SmallGridOnParts[num4][num5][num6].Length; num7++)
					{
						binaryWriter.Write(this.data.SmallGridOnParts[num4][num5][num6][num7]);
					}
					for (int num8 = 0; num8 < this.data.SmallGridOnStackWall[num4][num5][num6].Length; num8++)
					{
						binaryWriter.Write(this.data.SmallGridOnStackWall[num4][num5][num6][num8]);
					}
					binaryWriter.Write(this.data.SmallGridCanRoofState[num4][num5][num6]);
					binaryWriter.Write(this.data.SmallGridInRoomState[num4][num5][num6]);
				}
			}
		}
		binaryWriter.Write("S");
		for (int num9 = 0; num9 < this.data.BuildPartsPos.Count; num9++)
		{
			binaryWriter.Write(this.data.BuildPartsGridKind[num9]);
			binaryWriter.Write(this.data.BuildPartsKind[num9]);
			binaryWriter.Write(this.data.BuildPartsFloor[num9]);
			binaryWriter.Write((double)this.data.BuildPartsPos[num9].x);
			binaryWriter.Write((double)this.data.BuildPartsPos[num9].y);
			binaryWriter.Write((double)this.data.BuildPartsPos[num9].z);
			binaryWriter.Write((double)this.data.BuildPartsRot[num9].x);
			binaryWriter.Write((double)this.data.BuildPartsRot[num9].y);
			binaryWriter.Write((double)this.data.BuildPartsRot[num9].z);
			binaryWriter.Write((double)this.data.BuildPartsRot[num9].w);
			binaryWriter.Write(this.data.BuildPartsPutGridInfosNum[num9]);
			for (int num10 = 0; num10 < this.data.BuildPartsPutGridInfos[num9].Count; num10++)
			{
				binaryWriter.Write(this.data.BuildPartsPutGridInfos[num9][num10]);
				binaryWriter.Write(this.data.BuildPartsPutSmallGridInfos[num9][num10]);
			}
		}
		binaryWriter.Write("B");
		for (int num11 = 0; num11 < this.data.MaxFloorNum; num11++)
		{
			for (int num12 = 0; num12 < this.data.GridPos.Count; num12++)
			{
				binaryWriter.Write(this.data.tmpGridActiveList[num11][num12]);
			}
			binaryWriter.Write(this.data.tmpGridActiveListUpdate[num11]);
			binaryWriter.Write(this.data.MaxPutHeight[num11]);
		}
		binaryWriter.Write("A");
		binaryWriter.Close();
		this.data.MaxFloorNum = 0;
		this.data.nPutPartsNum = 0;
		this.data.GridPos.Clear();
		this.data.GridUseState.Clear();
		this.data.nFloorPartsHeight.Clear();
		this.data.SmallGridState.Clear();
		this.data.SmallGridOnParts.Clear();
		this.data.SmallGridOnStackWall.Clear();
		this.data.SmallGridCanRoofState.Clear();
		this.data.SmallGridInRoomState.Clear();
		this.data.BuildPartsGridKind.Clear();
		this.data.BuildPartsKind.Clear();
		this.data.BuildPartsFloor.Clear();
		this.data.BuildPartsPos.Clear();
		this.data.BuildPartsRot.Clear();
		this.data.BuildPartsPutGridInfos.Clear();
		this.data.BuildPartsPutSmallGridInfos.Clear();
		this.data.BuildPartsPutGridInfosNum.Clear();
		this.data.tmpGridActiveList.Clear();
		this.data.tmpGridActiveListUpdate.Clear();
		this.data.MaxPutHeight.Clear();
	}

	// Token: 0x06007C82 RID: 31874 RVA: 0x003536E4 File Offset: 0x00351AE4
	public void Load(GridPool Grid, string loadpath)
	{
		List<BuildPartsPool>[] baseParts = Singleton<CraftCommandListBaseObject>.Instance.BaseParts;
		FileStream fileStream = new FileStream(loadpath, FileMode.Open, FileAccess.Read);
		Encoding utf = Encoding.UTF8;
		BinaryReader binaryReader = new BinaryReader(fileStream, utf);
		if (binaryReader != null)
		{
			PngFile.SkipPng(binaryReader);
			binaryReader.ReadChar();
			char c = binaryReader.ReadChar();
			if (!c.Equals('P'))
			{
				fileStream.Seek(-2L, SeekOrigin.Current);
			}
			this.data.MaxFloorNum = binaryReader.ReadInt32();
			this.data.nPutPartsNum = binaryReader.ReadInt32();
			Vector3 item;
			while (!c.Equals('P'))
			{
				item.x = (float)binaryReader.ReadDouble();
				item.y = (float)binaryReader.ReadDouble();
				item.z = (float)binaryReader.ReadDouble();
				this.data.GridPos.Add(item);
				binaryReader.ReadChar();
				c = binaryReader.ReadChar();
				if (!c.Equals('P'))
				{
					fileStream.Seek(-2L, SeekOrigin.Current);
				}
			}
			binaryReader.ReadChar();
			c = binaryReader.ReadChar();
			if (!c.Equals('U'))
			{
				fileStream.Seek(-2L, SeekOrigin.Current);
			}
			while (!c.Equals('U'))
			{
				this.data.GridUseState.Add(binaryReader.ReadBoolean());
				binaryReader.ReadChar();
				c = binaryReader.ReadChar();
				if (!c.Equals('U'))
				{
					fileStream.Seek(-2L, SeekOrigin.Current);
				}
			}
			binaryReader.ReadChar();
			c = binaryReader.ReadChar();
			if (!c.Equals('F'))
			{
				fileStream.Seek(-2L, SeekOrigin.Current);
			}
			while (!c.Equals('F'))
			{
				this.data.nFloorPartsHeight.Add(binaryReader.ReadInt32());
				binaryReader.ReadChar();
				c = binaryReader.ReadChar();
				if (!c.Equals('F'))
				{
					fileStream.Seek(-2L, SeekOrigin.Current);
				}
			}
			binaryReader.ReadChar();
			c = binaryReader.ReadChar();
			if (!c.Equals('S'))
			{
				fileStream.Seek(-2L, SeekOrigin.Current);
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.data.SmallGridState.Add(new List<List<int>>());
			this.data.SmallGridOnParts.Add(new List<List<int[]>>());
			this.data.SmallGridOnStackWall.Add(new List<List<int[]>>());
			this.data.SmallGridCanRoofState.Add(new List<List<int>>());
			this.data.SmallGridInRoomState.Add(new List<List<bool>>());
			this.data.SmallGridState[num].Add(new List<int>());
			this.data.SmallGridOnParts[num].Add(new List<int[]>());
			this.data.SmallGridOnStackWall[num].Add(new List<int[]>());
			this.data.SmallGridCanRoofState[num].Add(new List<int>());
			this.data.SmallGridInRoomState[num].Add(new List<bool>());
			while (!c.Equals('S'))
			{
				this.data.SmallGridState[num][num2].Add(binaryReader.ReadInt32());
				int[] array = new int[7];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = binaryReader.ReadInt32();
				}
				this.data.SmallGridOnParts[num][num2].Add(array);
				array = new int[9];
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = binaryReader.ReadInt32();
				}
				this.data.SmallGridOnStackWall[num][num2].Add(array);
				this.data.SmallGridCanRoofState[num][num2].Add(binaryReader.ReadInt32());
				this.data.SmallGridInRoomState[num][num2].Add(binaryReader.ReadBoolean());
				binaryReader.ReadChar();
				c = binaryReader.ReadChar();
				if (!c.Equals('S'))
				{
					fileStream.Seek(-2L, SeekOrigin.Current);
				}
				num3++;
				if (num3 == 4)
				{
					num3 = 0;
					num2++;
					if (num2 != this.data.MaxFloorNum)
					{
						this.data.SmallGridState[num].Add(new List<int>());
						this.data.SmallGridOnParts[num].Add(new List<int[]>());
						this.data.SmallGridOnStackWall[num].Add(new List<int[]>());
						this.data.SmallGridCanRoofState[num].Add(new List<int>());
						this.data.SmallGridInRoomState[num].Add(new List<bool>());
					}
					if (num2 == this.data.MaxFloorNum)
					{
						num2 = 0;
						num++;
						this.data.SmallGridState.Add(new List<List<int>>());
						this.data.SmallGridOnParts.Add(new List<List<int[]>>());
						this.data.SmallGridOnStackWall.Add(new List<List<int[]>>());
						this.data.SmallGridCanRoofState.Add(new List<List<int>>());
						this.data.SmallGridInRoomState.Add(new List<List<bool>>());
						this.data.SmallGridState[num].Add(new List<int>());
						this.data.SmallGridOnParts[num].Add(new List<int[]>());
						this.data.SmallGridOnStackWall[num].Add(new List<int[]>());
						this.data.SmallGridCanRoofState[num].Add(new List<int>());
						this.data.SmallGridInRoomState[num].Add(new List<bool>());
					}
				}
			}
			binaryReader.ReadChar();
			c = binaryReader.ReadChar();
			if (!c.Equals('B'))
			{
				fileStream.Seek(-2L, SeekOrigin.Current);
			}
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			while (!c.Equals('B'))
			{
				this.data.BuildPartsGridKind.Add(binaryReader.ReadInt32());
				this.data.BuildPartsKind.Add(binaryReader.ReadInt32());
				this.data.BuildPartsFloor.Add(binaryReader.ReadInt32());
				item.x = (float)binaryReader.ReadDouble();
				item.y = (float)binaryReader.ReadDouble();
				item.z = (float)binaryReader.ReadDouble();
				this.data.BuildPartsPos.Add(item);
				Quaternion item2;
				item2.x = (float)binaryReader.ReadDouble();
				item2.y = (float)binaryReader.ReadDouble();
				item2.z = (float)binaryReader.ReadDouble();
				item2.w = (float)binaryReader.ReadDouble();
				this.data.BuildPartsRot.Add(item2);
				this.data.BuildPartsPutGridInfosNum.Add(binaryReader.ReadInt32());
				list.Clear();
				list2.Clear();
				for (int k = 0; k < this.data.BuildPartsPutGridInfosNum[this.data.BuildPartsPutGridInfosNum.Count - 1]; k++)
				{
					list.Add(binaryReader.ReadInt32());
					list2.Add(binaryReader.ReadInt32());
				}
				this.data.BuildPartsPutGridInfos.Add(list);
				this.data.BuildPartsPutSmallGridInfos.Add(list2);
				binaryReader.ReadChar();
				c = binaryReader.ReadChar();
				if (!c.Equals('B'))
				{
					fileStream.Seek(-2L, SeekOrigin.Current);
				}
			}
			binaryReader.ReadChar();
			c = binaryReader.ReadChar();
			if (!c.Equals('A'))
			{
				fileStream.Seek(-2L, SeekOrigin.Current);
			}
			bool[] array2 = new bool[this.data.GridPos.Count];
			while (!c.Equals('A'))
			{
				for (int l = 0; l < array2.Length; l++)
				{
					array2[l] = binaryReader.ReadBoolean();
				}
				this.data.tmpGridActiveList.Add(array2);
				this.data.tmpGridActiveListUpdate.Add(binaryReader.ReadBoolean());
				this.data.MaxPutHeight.Add(binaryReader.ReadInt32());
				binaryReader.ReadChar();
				c = binaryReader.ReadChar();
				if (!c.Equals('A'))
				{
					fileStream.Seek(-2L, SeekOrigin.Current);
				}
			}
			binaryReader.Close();
		}
		if (this.data.SmallGridState[this.data.SmallGridState.Count - 1][this.data.SmallGridState[this.data.SmallGridState.Count - 1].Count - 1].Count == 0)
		{
			this.data.SmallGridState.RemoveAt(this.data.SmallGridState.Count - 1);
			this.data.SmallGridOnParts.RemoveAt(this.data.SmallGridOnParts.Count - 1);
			this.data.SmallGridOnStackWall.RemoveAt(this.data.SmallGridOnStackWall.Count - 1);
			this.data.SmallGridCanRoofState.RemoveAt(this.data.SmallGridCanRoofState.Count - 1);
			this.data.SmallGridInRoomState.RemoveAt(this.data.SmallGridInRoomState.Count - 1);
		}
		Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt = this.data.MaxFloorNum;
		Singleton<CraftCommandListBaseObject>.Instance.nPutPartsNum = this.data.nPutPartsNum;
		List<GameObject> list3 = Grid.GetList();
		List<GridInfo> list4 = new List<GridInfo>();
		foreach (GameObject gameObject in list3)
		{
			list4.Add(gameObject.GetComponent<GridInfo>());
		}
		int num4 = this.data.GridPos.Count - list3.Count;
		if (num4 > 0)
		{
			for (int m = 0; m < num4; m++)
			{
				Grid.Get();
			}
		}
		for (int n = 0; n < list3.Count; n++)
		{
			list3[n].SetActive(true);
			list3[n].transform.localPosition = this.data.GridPos[n];
			list4[n].DelFloor(0);
			for (int num5 = list4[n].GetFloorNum(); num5 < this.data.MaxFloorNum; num5++)
			{
				list4[n].AddFloor();
			}
			for (int num6 = 0; num6 < this.data.MaxFloorNum; num6++)
			{
				list4[n].SetUseState(num6, this.data.GridUseState[num6 + list4[n].GetFloorNum() * n]);
				list4[n].nFloorPartsHeight[num6] = this.data.nFloorPartsHeight[num6];
				for (int num7 = 0; num7 < 4; num7++)
				{
					for (int num8 = 0; num8 < 7; num8++)
					{
						list4[n].ChangeSmallGrid(num7, this.data.SmallGridState[n][num6][num7], this.data.SmallGridOnParts[n][num6][num7][num8], num6, false);
					}
					for (int num9 = 0; num9 < 5; num9++)
					{
						list4[n].ChangeSmallGrid(num7, this.data.SmallGridState[n][num6][num7], this.data.SmallGridOnStackWall[n][num6][num7][num9], num6, false);
					}
					if (num6 == 0)
					{
						list4[n].ChangeSmallGridColor(num6, num7);
					}
					list4[n].SetCanRoofSmallGrid(num7, num6, this.data.SmallGridCanRoofState[n][num6][num7]);
					list4[n].SetInRoomSmallGrid(num7, this.data.SmallGridInRoomState[n][num6][num7], num6);
				}
			}
		}
		for (int num10 = 0; num10 < this.data.MaxFloorNum; num10++)
		{
			GridInfo.ChangeGridInfo(list4, num10);
		}
		List<GameObject> list5 = new List<GameObject>();
		for (int num11 = 0; num11 < baseParts.Length; num11++)
		{
			for (int num12 = 0; num12 < baseParts[num11].Count; num12++)
			{
				List<GameObject> list6 = baseParts[num11][num12].GetList();
				for (int num13 = 0; num13 < list6.Count; num13++)
				{
					if (list6[num13].activeSelf)
					{
						list6[num13].SetActive(false);
					}
					if (list6[num13].GetComponent<BuildPartsInfo>().nPutFloor != -1)
					{
						list6[num13].GetComponent<BuildPartsInfo>().nPutFloor = -1;
					}
				}
				baseParts[num11][num12].ReserveListDel(0, 1);
			}
		}
		for (int num14 = 0; num14 < this.data.BuildPartsPos.Count; num14++)
		{
			if (list5 != baseParts[this.data.BuildPartsGridKind[num14]][this.data.BuildPartsKind[num14]].GetList())
			{
				list5 = baseParts[this.data.BuildPartsGridKind[num14]][this.data.BuildPartsKind[num14]].GetList();
			}
			int index = -1;
			baseParts[this.data.BuildPartsGridKind[num14]][this.data.BuildPartsKind[num14]].Get(ref index);
			BuildPartsInfo component = list5[index].GetComponent<BuildPartsInfo>();
			list5[index].SetActive(true);
			list5[index].transform.localPosition = this.data.BuildPartsPos[num14];
			list5[index].transform.localRotation = this.data.BuildPartsRot[num14];
			component.nPutFloor = this.data.BuildPartsFloor[num14];
			component.putGridInfos.Clear();
			component.putSmallGridInfos.Clear();
			for (int num15 = 0; num15 < this.data.BuildPartsPutGridInfos.Count; num15++)
			{
				component.putGridInfos.Add(Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo[this.data.BuildPartsPutGridInfos[num14][num15]]);
				component.putSmallGridInfos.Add(this.data.BuildPartsPutSmallGridInfos[num14][num15]);
			}
		}
		Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList = this.data.tmpGridActiveList;
		Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveListUpdate = this.data.tmpGridActiveListUpdate;
		Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight = this.data.MaxPutHeight;
		this.data.MaxFloorNum = 0;
		this.data.nPutPartsNum = 0;
		this.data.GridPos.Clear();
		this.data.GridUseState.Clear();
		this.data.nFloorPartsHeight.Clear();
		this.data.SmallGridState.Clear();
		this.data.SmallGridOnParts.Clear();
		this.data.SmallGridOnStackWall.Clear();
		this.data.SmallGridCanRoofState.Clear();
		this.data.SmallGridInRoomState.Clear();
		this.data.BuildPartsGridKind.Clear();
		this.data.BuildPartsKind.Clear();
		this.data.BuildPartsFloor.Clear();
		this.data.BuildPartsPos.Clear();
		this.data.BuildPartsRot.Clear();
		this.data.BuildPartsPutGridInfos.Clear();
		this.data.BuildPartsPutSmallGridInfos.Clear();
		this.data.BuildPartsPutGridInfosNum.Clear();
		this.data.tmpGridActiveList.Clear();
		this.data.tmpGridActiveListUpdate.Clear();
		this.data.MaxPutHeight.Clear();
	}

	// Token: 0x06007C83 RID: 31875 RVA: 0x00354860 File Offset: 0x00352C60
	private byte[] CreatePngScreen(int _width, int _height)
	{
		Texture2D texture2D = new Texture2D(_width, _height, TextureFormat.RGB24, false);
		int antiAliasing = (QualitySettings.antiAliasing != 0) ? QualitySettings.antiAliasing : 1;
		RenderTexture temporary = RenderTexture.GetTemporary(_width, _height, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, antiAliasing);
		Graphics.Blit(this.gameScreenShotAssist.rtCamera, temporary);
		RenderTexture.active = temporary;
		texture2D.ReadPixels(new Rect(0f, 0f, (float)_width, (float)_height), 0, 0);
		texture2D.Apply();
		RenderTexture.active = null;
		byte[] result = texture2D.EncodeToPNG();
		RenderTexture.ReleaseTemporary(temporary);
		return result;
	}

	// Token: 0x0400646C RID: 25708
	private CraftSaveData data;

	// Token: 0x0400646D RID: 25709
	[SerializeField]
	private VirtualCameraController virtualCamera;

	// Token: 0x0400646E RID: 25710
	private GameScreenShotAssist gameScreenShotAssist;

	// Token: 0x0400646F RID: 25711
	private byte[] pngData;

	// Token: 0x04006470 RID: 25712
	private const int nOffset = 2;
}
