using System;
using System.Collections.Generic;
using System.Linq;
using AIProject.UI;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

// Token: 0x02000EDF RID: 3807
public class CraftControler : MonoBehaviour
{
	// Token: 0x17001881 RID: 6273
	// (get) Token: 0x06007C35 RID: 31797 RVA: 0x00347882 File Offset: 0x00345C82
	// (set) Token: 0x06007C36 RID: 31798 RVA: 0x0034788A File Offset: 0x00345C8A
	public int _nPartsForm
	{
		get
		{
			return this.nPartsForm;
		}
		set
		{
			this.nPartsForm = value;
			this.ChangeKindPart(this.nPartsForm);
		}
	}

	// Token: 0x17001882 RID: 6274
	// (get) Token: 0x06007C37 RID: 31799 RVA: 0x0034789F File Offset: 0x00345C9F
	// (set) Token: 0x06007C38 RID: 31800 RVA: 0x003478A7 File Offset: 0x00345CA7
	public int _nID
	{
		get
		{
			return this.nID;
		}
		set
		{
			this.nID = value;
		}
	}

	// Token: 0x17001883 RID: 6275
	// (get) Token: 0x06007C3A RID: 31802 RVA: 0x003478C9 File Offset: 0x00345CC9
	// (set) Token: 0x06007C39 RID: 31801 RVA: 0x003478B0 File Offset: 0x00345CB0
	private int nPutPartsNum
	{
		get
		{
			return this.craftInfomationUI.nCost;
		}
		set
		{
			this.craftInfomationUI.nCost = value;
			Singleton<CraftCommandListBaseObject>.Instance.nPutPartsNum = value;
		}
	}

	// Token: 0x06007C3B RID: 31803 RVA: 0x003478D6 File Offset: 0x00345CD6
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06007C3C RID: 31804 RVA: 0x003478E0 File Offset: 0x00345CE0
	private void Init()
	{
		this.input = Singleton<Manager.Input>.Instance;
		this.MinPos = default(Vector3);
		this.nPartsForm = 0;
		this.fGridSize = 1f;
		this.GridInit();
		this.carsol.Init(this.MinPos, this.fGridSize);
		if (this.AllReset != null)
		{
			this.AllReset.onClick.AddListener(new UnityAction(this.CraftAllReset));
		}
		this.save = Singleton<CraftSave>.Instance;
		this.save.Init();
		if (Singleton<SaveLoadUI>.Instance.save != null)
		{
			Singleton<SaveLoadUI>.Instance.save.onClick.AddListener(new UnityAction(this.SaveCraft));
		}
		if (Singleton<SaveLoadUI>.Instance.dataLoad != null)
		{
			Singleton<SaveLoadUI>.Instance.dataLoad.onClick.AddListener(new UnityAction(this.LoadCraft));
		}
		this.nID = 0;
		this.gridMap = new GridMap();
		this.gridMap.Init(this.GridList, this.AreaNumX, this.AreaNumZ);
		Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt = 0;
		Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt = 1;
		this.nCarsolMode = 0;
		this.ViewMode = false;
		this.prevViewMode = false;
		this.fMoveCarsolTimelLimiter = 0f;
		this.PutHeight = 0;
		for (int i = 0; i < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt; i++)
		{
			Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight.Add(0);
		}
		if (!this.bFloorLimit)
		{
			this.Cam.isLimitDir = false;
			this.Cam.isLimitPos = false;
		}
		this.nPutPartsNum = 0;
	}

	// Token: 0x06007C3D RID: 31805 RVA: 0x00347AA4 File Offset: 0x00345EA4
	private void GridInit()
	{
		this.AllGridNum = this.AreaNumX * this.AreaNumY * this.AreaNumZ;
		this.Gridpool = new GridPool();
		if (100 > this.AllGridNum)
		{
			this.Gridpool.CreatePool(this.GridPart, this.CreateGridPlace, this.AllGridNum);
		}
		else
		{
			this.Gridpool.CreatePool(this.GridPart, this.CreateGridPlace, 100);
		}
		this.vGridPos = new Vector3[this.AllGridNum];
		float x = -this.fGridSize * (float)this.AreaNumX / 2f + 1f + this.CraftSet.position.x;
		float z = -this.fGridSize * (float)this.AreaNumZ / 2f + 1f + this.CraftSet.position.z;
		this.MinPos.x = x;
		this.MinPos.y = this.CraftSet.position.y;
		this.MinPos.z = z;
		Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList = new List<bool[]>();
		Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveListUpdate = new List<bool>();
		Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList.Add(new bool[this.AllGridNum]);
		for (int i = 0; i < this.AllGridNum; i++)
		{
			Vector3 vector;
			vector.x = this.MinPos.x + this.fGridSize * (float)(i % this.AreaNumX);
			vector.y = this.MinPos.y + this.fGridSize * (float)(i / (this.AreaNumX * this.AreaNumZ));
			vector.z = this.MinPos.z + this.fGridSize * (float)(i / this.AreaNumX % this.AreaNumZ);
			GameObject gameObject = this.Gridpool.Get();
			GridInfo component = gameObject.GetComponent<GridInfo>();
			gameObject.SetActive(true);
			gameObject.transform.localPosition = vector;
			component.Init(0);
			component.InitPos = vector;
			component.nID = i;
			component.SetUseState(0, true);
			this.vGridPos[i] = gameObject.transform.localPosition;
			Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList[0][i] = true;
		}
		Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveListUpdate.Add(true);
		this.GridList = this.Gridpool.GetList();
		for (int j = 0; j < this.AllGridNum; j++)
		{
			this.MinPos.x = Mathf.Min(this.MinPos.x, this.GridList[j].transform.localPosition.x);
			this.MinPos.z = Mathf.Min(this.MinPos.z, this.GridList[j].transform.localPosition.z);
		}
		Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo = new List<GridInfo>();
		foreach (GameObject gameObject2 in this.GridList)
		{
			Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo.Add(gameObject2.GetComponent<GridInfo>());
		}
		this.BaseGridInfo = Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo;
	}

	// Token: 0x06007C3E RID: 31806 RVA: 0x00347E44 File Offset: 0x00346244
	private void Update()
	{
		if (!Singleton<CraftResource>.Instance.bEnd)
		{
			return;
		}
		if (Singleton<CraftCommandListBaseObject>.Instance.BaseParts == null)
		{
			this.partsMgr.Init();
			Singleton<CraftCommandListBaseObject>.Instance.BaseParts = this.partsMgr.GetPool();
			this.craftItemButtonUI.CreateCategoryButton();
			for (int i = 0; i < this.craftItemButtonUI.ItemKind.Length; i++)
			{
				int tmpID = i;
				if (this.craftItemButtonUI.ItemKind[tmpID] != null)
				{
					this.craftItemButtonUI.ItemKind[tmpID].onClick.AddListener(delegate()
					{
						this.ChangeItemBox(tmpID);
					});
					EventSystem.current.SetSelectedGameObject(null);
				}
			}
			this.carsol.SetMoveLimit(this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.MoveVal);
			this.craftItem.EndLoad = true;
		}
		else if (EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject())
		{
			Vector3 position = this.carsol.transform.position;
			if (this.fMoveCarsolTimelLimiter == 0f)
			{
				if (!this.carsol.bMouseOperate)
				{
					if (!this.craftItemButtonUI.isActive && this.Cam.bLock)
					{
						if (this.input.IsPressedKey(KeyCode.UpArrow))
						{
							this.carsol.MoveCarsol(0);
						}
						else if (this.input.IsPressedKey(KeyCode.RightArrow))
						{
							this.carsol.MoveCarsol(1);
						}
						else if (this.input.IsPressedKey(KeyCode.DownArrow))
						{
							this.carsol.MoveCarsol(2);
						}
						else if (this.input.IsPressedKey(KeyCode.LeftArrow))
						{
							this.carsol.MoveCarsol(3);
						}
					}
				}
				else
				{
					this.carsol.MoveCarsol(-1);
				}
				this.CarsolOffset();
			}
			this.fMoveCarsolTimelLimiter += Time.deltaTime;
			if (this.fMoveCarsolTimelLimiter > 0.05f)
			{
				this.fMoveCarsolTimelLimiter = 0f;
			}
			int num = this.carsol.GetDirection();
			bool flag = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm].Count == 0 || this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.MoveVal != 1f;
			if (this.partsMgr.BuildPartPoolDic[this.nPartsForm].Count > 0 && (this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Itemkind == 10 || this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Itemkind == 13))
			{
				flag = false;
			}
			Quaternion localRotation = this.carsol.transform.localRotation;
			if (Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm].Count != 0 && Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][this.nID].GetCategoryKind() != 1)
			{
				if (this.input.IsPressedKey(KeyCode.Q))
				{
					if (flag)
					{
						this.carsol.transform.localRotation = Quaternion.Euler(0f, -45f, 0f) * this.carsol.transform.localRotation;
						num--;
					}
					else
					{
						this.carsol.transform.localRotation = Quaternion.Euler(0f, -90f, 0f) * this.carsol.transform.localRotation;
						num -= 2;
					}
				}
				else if (this.input.IsPressedKey(KeyCode.E))
				{
					if (flag)
					{
						this.carsol.transform.localRotation = Quaternion.Euler(0f, 45f, 0f) * this.carsol.transform.localRotation;
						num++;
					}
					else
					{
						this.carsol.transform.localRotation = Quaternion.Euler(0f, 90f, 0f) * this.carsol.transform.localRotation;
						num += 2;
					}
				}
				if (this.Cam.bLock)
				{
					if (flag)
					{
						if (this.input.ScrollValue() > 0f)
						{
							this.carsol.transform.localRotation = Quaternion.Euler(0f, -45f, 0f) * this.carsol.transform.localRotation;
							num--;
						}
						else if (this.input.ScrollValue() < 0f)
						{
							this.carsol.transform.localRotation = Quaternion.Euler(0f, 45f, 0f) * this.carsol.transform.localRotation;
							num++;
						}
					}
					else if (this.input.ScrollValue() > 0f)
					{
						this.carsol.transform.localRotation = Quaternion.Euler(0f, -90f, 0f) * this.carsol.transform.localRotation;
						num -= 2;
					}
					else if (this.input.ScrollValue() < 0f)
					{
						this.carsol.transform.localRotation = Quaternion.Euler(0f, 90f, 0f) * this.carsol.transform.localRotation;
						num += 2;
					}
				}
				if (num < 0)
				{
					if (flag)
					{
						num = 7;
					}
					else
					{
						num = 6;
					}
				}
				else if (num == 8)
				{
					num = 0;
				}
				this.carsol.SetDirection(num);
			}
			if (this.carsol.transform.position != position || this.carsol.transform.localRotation != localRotation)
			{
				this.CarsolUnderCheck();
			}
			if (this.Cam.bLock && (this.carsol.bMouseOperate || (!this.craftItemButtonUI.isActive && !this.carsol.bMouseOperate)))
			{
				if (this.nCarsolMode == 0)
				{
					if (this.input.IsPressedKey(KeyCode.X) || this.input.IsPressedKey(ActionID.MouseLeft))
					{
						this.PutBuldPart(this.carsol.gameObject, num);
					}
					else if (this.input.IsPressedKey(KeyCode.Z) || this.input.IsPressedKey(ActionID.MouseRight))
					{
						this.DeadBuildPart();
					}
				}
				else if (this.nCarsolMode == 1)
				{
					if (this.input.IsPressedKey(KeyCode.X) || this.input.IsPressedKey(ActionID.MouseLeft))
					{
						this.SelectBuldPart();
					}
					if (this.SelectObj.Count > 0)
					{
						List<CraftCommandList.ChangeValParts> valAutoParts = new List<CraftCommandList.ChangeValParts>();
						this.nCarsolMode++;
						this.gridMap.ChangeCraftMap(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
						this.gridMap.CraftMapSearchRoom(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
						GridInfo.ChangeGridInfo(this.BaseGridInfo, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
						bool flag2 = this.BaseGridInfo[0].GetFloorNum() > Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1 && !this.gridMap.CraftMapRoofDecide();
						flag2 = (flag2 && this.PillarOnGridNum() == 0);
						if (flag2)
						{
							this.AllFloorDel(valAutoParts);
						}
						this.FloorDel(valAutoParts);
						this.gridMap.ChangeCraftMap(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
						this.gridMap.CraftMapSearchRoom(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
						GridInfo.ChangeGridInfo(this.BaseGridInfo, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
					}
				}
				else if (this.nCarsolMode == 2)
				{
					bool flag3 = false;
					if (this.input.IsPressedKey(KeyCode.X) || this.input.IsPressedKey(ActionID.MouseLeft))
					{
						flag3 = this.SelectPut();
					}
					if (flag3)
					{
						this.carsol.SelectModeCarsolUnvisible();
						this.carsol.ResetCursolDirPos();
						this.nCarsolMode = 1;
						this.SelectObj.Clear();
						this.URPutPartsSetG(ref this.SelectPutGridInfo, 1);
						this.SelectPutMaxFloorCnt[1] = Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt;
						Singleton<UndoRedoMgr>.Instance.Push(new CraftCommandList.SelectBuildPartCommand.Command(this.SelectPutGridInfo, this.SelectPutPartInfo.ToArray(), this.SelectPutAutoPartInfo.ToArray(), this.SelectPutMaxFloorCnt));
					}
				}
			}
			if (!Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveListUpdate[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt])
			{
				this.TmpGridActiveUpdate();
			}
			if (this.nCarsolMode == 0 && this.input.IsPressedKey(KeyCode.W))
			{
				this.OpelateFloorUp();
			}
			if (this.input.IsPressedKey(KeyCode.C) && !this.ViewMode)
			{
				this.CamLock.isOn ^= true;
				this.craftItemButtonUI.isActive = this.CamLock.isOn;
			}
			if (this.nCarsolMode == 0)
			{
				if (this.input.IsPressedKey(KeyCode.A))
				{
					this.nPartsForm++;
					this.ChangeKindPart(this.nPartsForm);
					this.ChangeParts();
				}
				else if (this.input.IsPressedKey(KeyCode.S))
				{
					this.nPartsForm--;
					this.ChangeKindPart(this.nPartsForm);
					this.ChangeParts();
				}
				if (this.input.IsPressedKey(KeyCode.D))
				{
					this.nID++;
					this.nID %= Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm].Count;
					this.ChangeParts();
				}
			}
			if (this.input.IsPressedKey(KeyCode.V))
			{
				if (this.nCarsolMode == 0)
				{
					this.nCarsolMode = 1;
					this.SelectMode.isOn = true;
				}
				else if (this.nCarsolMode == 1)
				{
					this.nCarsolMode = 0;
					this.SelectObj.Clear();
					this.SelectMode.isOn = false;
				}
			}
			if (this.SelectMode.isOn)
			{
				if (this.nCarsolMode == 0)
				{
					this.nCarsolMode = 1;
				}
			}
			else if (this.nCarsolMode == 2)
			{
				this.SelectMode.isOn = true;
			}
			else if (this.nCarsolMode == 1)
			{
				this.nCarsolMode = 0;
			}
		}
		if (this.craftInfomationUI.GetWarningActive() && !this.craftInfomationUI.bFade)
		{
			this.craftInfomationUI.fWarningExist += Time.deltaTime;
			if (this.craftInfomationUI.fWarningExist > 1.5f)
			{
				this.craftInfomationUI.bFade = true;
			}
		}
		if (this.FloorUpTex.activeSelf)
		{
			this.fFloorUpTexExist += Time.deltaTime;
			if (this.fFloorUpTexExist > 1.5f)
			{
				this.fFloorUpTexExist = 0f;
				this.FloorUpTex.SetActive(false);
			}
		}
		if (this.craftItemButtonUI.isActive)
		{
			foreach (MenuUIBehaviour menuUIBehaviour in this.input.MenuElements)
			{
				if (this.input.IsPressedKey(KeyCode.DownArrow))
				{
					if (menuUIBehaviour.EnabledInput && menuUIBehaviour.FocusLevel == this.input.FocusLevel)
					{
						menuUIBehaviour.OnInputMoveDirection(MoveDirection.Down);
					}
				}
				else if (this.input.IsPressedKey(KeyCode.UpArrow) && menuUIBehaviour.EnabledInput && menuUIBehaviour.FocusLevel == this.input.FocusLevel)
				{
					menuUIBehaviour.OnInputMoveDirection(MoveDirection.Up);
				}
			}
		}
		if (this.craftItem.isActive)
		{
			foreach (MenuUIBehaviour menuUIBehaviour2 in this.input.MenuElements)
			{
				if (this.input.IsPressedKey(KeyCode.RightArrow))
				{
					if (menuUIBehaviour2.EnabledInput && menuUIBehaviour2.FocusLevel == this.input.FocusLevel)
					{
						menuUIBehaviour2.OnInputMoveDirection(MoveDirection.Right);
					}
				}
				else if (this.input.IsPressedKey(KeyCode.LeftArrow) && menuUIBehaviour2.EnabledInput && menuUIBehaviour2.FocusLevel == this.input.FocusLevel)
				{
					menuUIBehaviour2.OnInputMoveDirection(MoveDirection.Left);
				}
			}
		}
		if (this.input.IsPressedKey(KeyCode.G))
		{
			this.carsol.bMouseOperate ^= true;
		}
		if ((this.input.IsDown(KeyCode.LeftShift) || this.input.IsDown(KeyCode.RightShift)) && this.input.IsPressedKey(KeyCode.Z))
		{
			this.Undo();
		}
		else if ((this.input.IsDown(KeyCode.LeftAlt) || this.input.IsDown(KeyCode.RightAlt)) && this.input.IsPressedKey(KeyCode.Z))
		{
			this.Redo();
		}
		else if (this.input.IsPressedKey(KeyCode.B))
		{
			Singleton<UndoRedoMgr>.Instance.Clear();
		}
		if ((this.input.IsPressedKey(KeyCode.LeftControl) || this.input.IsPressedKey(KeyCode.RightControl)) && this.input.IsPressedKey(KeyCode.A))
		{
			this.SaveCraft();
		}
		if (!this.Cam.bLock)
		{
			if (this.input.IsPressedKey(KeyCode.O))
			{
				this.ViewMode ^= true;
			}
			if (this.ViewMode)
			{
				this.CamLock.interactable = false;
			}
			else
			{
				this.CamLock.interactable = true;
			}
		}
		if (this.prevViewMode != this.ViewMode)
		{
			this.prevViewMode = this.ViewMode;
			for (int l = 0; l < this.GridList.Count; l++)
			{
				if (this.ViewMode)
				{
					if (this.GridList[l].activeSelf)
					{
						this.GridList[l].SetActive(false);
					}
				}
				else
				{
					this.FloatingObjGrid(this.GridList);
				}
			}
		}
		if (this.input.IsPressedKey(KeyCode.M))
		{
			this.PutHeight++;
			this.PutHeight %= 5;
		}
		else if (this.input.IsPressedKey(KeyCode.N))
		{
			this.PutHeight--;
			if (this.PutHeight < 0)
			{
				this.PutHeight = 4;
			}
		}
	}

	// Token: 0x06007C3F RID: 31807 RVA: 0x00348E44 File Offset: 0x00347244
	private void DeadBuildPart()
	{
		List<GameObject> list = new List<GameObject>();
		List<int> list2 = new List<int>();
		List<BuildPartsPool> list3 = new List<BuildPartsPool>();
		CraftCommandList.ChangeValGrid[] array = new CraftCommandList.ChangeValGrid[this.AllGridNum];
		List<CraftCommandList.ChangeValParts> list4 = new List<CraftCommandList.ChangeValParts>();
		List<CraftCommandList.ChangeValParts> list5 = new List<CraftCommandList.ChangeValParts>();
		int[] array2 = new int[2];
		array2[0] = Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt;
		GameObject gameObject = null;
		int num = -1;
		int itemKind = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][this.nID].GetItemKind();
		this.DecideDelParts(Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][this.nID], ref list, ref list2, ref list3, 0);
		if (list.Count > 0)
		{
			gameObject = list[0];
			num = list2[0];
			for (int i = 1; i < list.Count; i++)
			{
				if (gameObject.transform.localPosition.y <= list[i].transform.position.y)
				{
					gameObject = list[i];
					num = list2[i];
				}
			}
		}
		if (gameObject == null)
		{
			return;
		}
		for (int j = 0; j < array.Length; j++)
		{
			array[j] = new CraftCommandList.ChangeValGrid();
		}
		this.URPutPartsSetG(ref array, 0);
		List<GridInfo> list6 = new List<GridInfo>();
		List<int> list7 = new List<int>();
		this.GridCheck(this.GridList, this.carsol, list6, list7, gameObject.transform.localRotation, this.nPartsForm, null);
		int count = list6.Count;
		if (count == 0)
		{
			return;
		}
		if (!this.DelConditionCheck(count, list6, list7, itemKind, gameObject))
		{
			return;
		}
		this.DedPartsGridChange(count, list6, list7, itemKind, this.partsMgr.GetBuildPartsInfo(gameObject).nHeight);
		this.gridMap.ChangeCraftMap(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		this.gridMap.CraftMapSearchRoom(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		GridInfo.ChangeGridInfo(this.BaseGridInfo, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		bool flag = this.BaseGridInfo[0].GetFloorNum() > Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1 && !this.gridMap.CraftMapRoofDecide();
		flag = (flag && this.PillarOnGridNum() == 0);
		if (flag)
		{
			this.AllFloorDel(list5);
		}
		this.FloorDel(list5);
		this.gridMap.ChangeCraftMap(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		this.gridMap.CraftMapSearchRoom(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		GridInfo.ChangeGridInfo(this.BaseGridInfo, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		int num2 = (from n in this.BaseGridInfo
		select n.nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt]).Max();
		int moveval = num2 - Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt];
		this.FloorHeightMove(moveval, (float)(Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] + 5), list5);
		this.URPutPartsSetG(ref array, 1);
		Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] = num2;
		if (itemKind != 6 && itemKind != 3 && itemKind != 4)
		{
			list4.Add(new CraftCommandList.ChangeValParts());
			list4[list4.Count - 1].nFormKind = this.nPartsForm;
			list4[list4.Count - 1].nPoolID = this.nID;
			list4[list4.Count - 1].nItemID = num;
			this.URPutPartsSetB(list4[list4.Count - 1], gameObject, 0);
			gameObject.SetActive(false);
			gameObject.GetComponent<BuildPartsInfo>().nPutFloor = -1;
			Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][this.nID].ReserveListDel(num, 0);
			this.nPutPartsNum--;
			this.URPutPartsSetB(list4[list4.Count - 1], gameObject, 1);
		}
		else if (itemKind == 3 || itemKind == 4)
		{
			list.Clear();
			list2.Clear();
			list3.Clear();
			for (int k = 0; k < Singleton<CraftCommandListBaseObject>.Instance.BaseParts.Length; k++)
			{
				for (int l = 0; l < Singleton<CraftCommandListBaseObject>.Instance.BaseParts[k].Count; l++)
				{
					int itemKind2 = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[k][l].GetItemKind();
					if (itemKind2 == 3 || itemKind2 == 4 || itemKind2 == 10 || itemKind2 == 13)
					{
						this.DecideDelParts(Singleton<CraftCommandListBaseObject>.Instance.BaseParts[k][l], ref list, ref list2, ref list3, 0);
					}
				}
			}
			if (itemKind == 3)
			{
				for (int m = 0; m < list.Count; m++)
				{
					list4.Add(new CraftCommandList.ChangeValParts());
					list4[list4.Count - 1].nFormKind = list3[m].GetFormKind();
					list4[list4.Count - 1].nPoolID = list[m].GetComponent<BuildPartsInfo>().GetInfo(1);
					list4[list4.Count - 1].nItemID = list2[m];
					this.URPutPartsSetB(list4[list4.Count - 1], list[m], 0);
					list[m].SetActive(false);
					list[m].GetComponent<BuildPartsInfo>().nPutFloor = -1;
					list3[m].ReserveListDel(list2[m], 0);
					this.URPutPartsSetB(list4[list4.Count - 1], list[m], 1);
					this.nPutPartsNum--;
				}
			}
			else
			{
				list4.Add(new CraftCommandList.ChangeValParts());
				list4[list4.Count - 1].nFormKind = this.nPartsForm;
				list4[list4.Count - 1].nPoolID = this.nID;
				list4[list4.Count - 1].nItemID = num;
				this.URPutPartsSetB(list4[list4.Count - 1], gameObject, 0);
				gameObject.SetActive(false);
				gameObject.GetComponent<BuildPartsInfo>().nPutFloor = -1;
				Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][this.nID].ReserveListDel(num, 0);
				this.nPutPartsNum--;
				this.URPutPartsSetB(list4[list4.Count - 1], gameObject, 1);
				Rect rect = new Rect(gameObject.transform.position.x - (float)(this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Horizontal / 2), gameObject.transform.position.z - (float)(this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Vertical / 2), (float)this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Horizontal, (float)this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Vertical);
				for (int n2 = 0; n2 < list.Count; n2++)
				{
					int info = list[n2].GetComponent<BuildPartsInfo>().GetInfo(3);
					if (info == 10 || info == 13)
					{
						int info2 = list[n2].GetComponent<BuildPartsInfo>().GetInfo(2);
						int info3 = list[n2].GetComponent<BuildPartsInfo>().GetInfo(1);
						int info4 = list[n2].GetComponent<BuildPartsInfo>().GetInfo(0);
						CraftItemInfo item = this.partsMgr.BuildPartPoolDic[info2][info3].Item2;
						Rect other = new Rect(list[n2].transform.position.x - (float)(item.Horizontal / 2), list[n2].transform.position.z - (float)(item.Vertical / 2), (float)item.Horizontal, (float)item.Vertical);
						if (rect.Overlaps(other, true))
						{
							if (gameObject.transform.position.y == list[n2].transform.position.y)
							{
								list4.Add(new CraftCommandList.ChangeValParts());
								list4[list4.Count - 1].nFormKind = info2;
								list4[list4.Count - 1].nPoolID = info3;
								list4[list4.Count - 1].nItemID = info4;
								this.URPutPartsSetB(list4[list4.Count - 1], gameObject, 0);
								list[n2].SetActive(false);
								list[n2].GetComponent<BuildPartsInfo>().nPutFloor = -1;
								Singleton<CraftCommandListBaseObject>.Instance.BaseParts[info2][info3].ReserveListDel(info4, 0);
								this.nPutPartsNum--;
								this.URPutPartsSetB(list4[list4.Count - 1], gameObject, 1);
							}
						}
					}
				}
			}
		}
		else
		{
			list.Clear();
			list2.Clear();
			list3.Clear();
			for (int num3 = 0; num3 < Singleton<CraftCommandListBaseObject>.Instance.BaseParts.Length; num3++)
			{
				for (int num4 = 0; num4 < Singleton<CraftCommandListBaseObject>.Instance.BaseParts[num3].Count; num4++)
				{
					int itemKind3 = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[num3][num4].GetItemKind();
					if (itemKind3 == 3 || itemKind3 == 4 || itemKind3 == 10 || itemKind3 == 13)
					{
						this.DecideDelParts(Singleton<CraftCommandListBaseObject>.Instance.BaseParts[num3][num4], ref list, ref list2, ref list3, 0);
					}
				}
			}
			list4.Add(new CraftCommandList.ChangeValParts());
			list4[list4.Count - 1].nFormKind = this.nPartsForm;
			list4[list4.Count - 1].nPoolID = this.nID;
			list4[list4.Count - 1].nItemID = num;
			this.URPutPartsSetB(list4[list4.Count - 1], gameObject, 0);
			gameObject.SetActive(false);
			gameObject.GetComponent<BuildPartsInfo>().nPutFloor = -1;
			Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][this.nID].ReserveListDel(num, 0);
			this.URPutPartsSetB(list4[list4.Count - 1], gameObject, 1);
			this.nPutPartsNum--;
			for (int num5 = 0; num5 < list.Count; num5++)
			{
				list4.Add(new CraftCommandList.ChangeValParts());
				list4[list4.Count - 1].nFormKind = list3[num5].GetFormKind();
				list4[list4.Count - 1].nPoolID = list[num5].GetComponent<BuildPartsInfo>().GetInfo(1);
				list4[list4.Count - 1].nItemID = list2[num5];
				this.URPutPartsSetB(list4[list4.Count - 1], list[num5], 0);
				list[num5].SetActive(false);
				list[num5].GetComponent<BuildPartsInfo>().nPutFloor = -1;
				list3[num5].ReserveListDel(list2[num5], 0);
				this.URPutPartsSetB(list4[list4.Count - 1], list[num5], 1);
				this.nPutPartsNum--;
			}
		}
		array2[1] = Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt;
		Singleton<UndoRedoMgr>.Instance.Push(new CraftCommandList.DeadBuildPartCommand.Command(array, list4.ToArray(), list5.ToArray(), array2));
	}

	// Token: 0x06007C40 RID: 31808 RVA: 0x00349B40 File Offset: 0x00347F40
	private void PutBuldPart(GameObject Carsol, int nDirection)
	{
		CraftCommandList.ChangeValGrid[] array = new CraftCommandList.ChangeValGrid[this.AllGridNum];
		List<CraftCommandList.ChangeValParts> list = new List<CraftCommandList.ChangeValParts>();
		List<CraftCommandList.ChangeValParts> list2 = new List<CraftCommandList.ChangeValParts>();
		int[] array2 = new int[2];
		array2[0] = Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt;
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new CraftCommandList.ChangeValGrid();
		}
		this.URPutPartsSetG(ref array, 0);
		list.Add(new CraftCommandList.ChangeValParts());
		int num = -1;
		GameObject gameObject = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][this.nID].Get(ref num);
		list[0].nFormKind = this.nPartsForm;
		list[0].nPoolID = this.nID;
		list[0].nItemID = num;
		this.URPutPartsSetB(list[0], gameObject, 0);
		this.partsMgr.GetBuildPartsInfo(gameObject).SetDirection(nDirection);
		List<DeleatWall> list3 = new List<DeleatWall>();
		List<GridInfo> list4 = new List<GridInfo>();
		List<int> list5 = new List<int>();
		bool flag = this.GridPutCheck(this.GridList, this.carsol, gameObject, ref list3, ref list4, ref list5);
		int info = this.partsMgr.GetBuildPartsInfo(gameObject).GetInfo(3);
		if (flag)
		{
			this.FloorPartsDel(gameObject, list4, list2);
			this.ChangeGridInfo(gameObject, list4, list5, list5.Count, list2);
			this.partsMgr.GetBuildPartsInfo(gameObject).putGridInfos = list4;
			this.partsMgr.GetBuildPartsInfo(gameObject).putSmallGridInfos = list5;
			if (info == 6 || info == 16)
			{
				Transform transform = list3[0].Wall.transform;
				for (int j = 0; j < list3.Count; j++)
				{
					list.Add(new CraftCommandList.ChangeValParts());
					list[list.Count - 1].nFormKind = list3[j].PartForm;
					list[list.Count - 1].nPoolID = list3[j].PartKind;
					list[list.Count - 1].nItemID = list3[j].WallID;
					this.URPutPartsSetB(list[list.Count - 1], list3[j].Wall, 0);
					list3[j].Wall.SetActive(false);
					list3[j].Wall.GetComponent<BuildPartsInfo>().nPutFloor = -1;
					this.URPutPartsSetB(list[list.Count - 1], list3[j].Wall, 1);
					this.nPutPartsNum--;
					Singleton<CraftCommandListBaseObject>.Instance.BaseParts[list3[j].PartForm][list3[j].PartKind].ReserveListDel(list3[j].WallID, 0);
				}
				Vector3 position = transform.position;
				position.y = (list3.Max((DeleatWall v) => v.Wall.transform.position.y) - list3.Min((DeleatWall v) => v.Wall.transform.position.y)) / 2f;
				transform.position = position;
				this.SetParts(transform, list3[0].Wall.transform.rotation, gameObject, this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Horizontal, this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Vertical, 1);
			}
			else
			{
				Vector3 position2 = Carsol.transform.position;
				if (info == 3 || info == 4 || info == 15)
				{
					for (int k = 0; k < list4.Count; k++)
					{
						if (list4[k].GetPartOnSmallGrid(0, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[0] == 1)
						{
							bool flag2 = true;
							List<GameObject> list6 = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[0][0].GetList();
							for (int l = 0; l < list6.Count; l++)
							{
								if (list6[l].activeSelf)
								{
									if (list6[l].GetComponent<BuildPartsInfo>().nPutFloor == Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)
									{
										if (list6[l].transform.position.x == list4[k].transform.position.x && list6[l].transform.position.z == list4[k].transform.position.z)
										{
											flag2 = false;
											break;
										}
									}
								}
							}
							if (flag2)
							{
								int nItemID = -1;
								GameObject gameObject2 = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[0][0].Get(ref nItemID);
								list.Add(new CraftCommandList.ChangeValParts());
								list[list.Count - 1].nFormKind = 0;
								list[list.Count - 1].nPoolID = 0;
								list[list.Count - 1].nItemID = nItemID;
								this.URPutPartsSetB(list[list.Count - 1], gameObject2, 0);
								this.SetParts(list4[k].transform, Carsol.transform.rotation, gameObject2, 1, 1, 1);
								gameObject2.SetActive(true);
								this.URPutPartsSetB(list[list.Count - 1], gameObject2, 1);
							}
						}
						if (this.partsMgr.GetBuildPartsInfo(gameObject).GetInfo(3) == 4)
						{
							int num2 = 0;
							int[] stackWallOnSmallGrid = list4[k].GetStackWallOnSmallGrid(list5[k], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
							for (int m = 0; m < stackWallOnSmallGrid.Length; m++)
							{
								if (stackWallOnSmallGrid[m] == -1)
								{
									break;
								}
								num2++;
							}
						}
					}
					Carsol.transform.position = position2;
					this.SetParts(Carsol.transform, Carsol.transform.rotation, gameObject, this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Horizontal, this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Vertical, 0);
					position2.y += (float)this.partsMgr.GetBuildPartsInfo(gameObject).nHeight;
					Carsol.transform.position = position2;
				}
				else
				{
					this.SetParts(Carsol.transform, Carsol.transform.rotation, gameObject, this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Horizontal, this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Vertical, 0);
					Carsol.transform.position = position2;
				}
			}
			gameObject.SetActive(true);
			this.CheckUnderRoof();
		}
		else
		{
			gameObject.GetComponent<BuildPartsInfo>().nPutFloor = -1;
			Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][this.nID].ReserveListDel(num, 0);
		}
		this.gridMap.ChangeCraftMap(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		this.gridMap.CraftMapSearchRoom(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		GridInfo.ChangeGridInfo(this.BaseGridInfo, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		int num3 = this.PillarOnGridNum();
		if (this.gridMap.CraftMapRoofDecide() || (info == 15 && num3 == 1))
		{
			this.FloorUP(list2);
		}
		this.URPutPartsSetG(ref array, 1);
		this.URPutPartsSetB(list[0], gameObject, 1);
		array2[1] = Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt;
		if (flag)
		{
			Singleton<UndoRedoMgr>.Instance.Push(new CraftCommandList.PutBuildPartCommand.Command(array, list.ToArray(), list2.ToArray(), array2));
		}
	}

	// Token: 0x06007C41 RID: 31809 RVA: 0x0034A404 File Offset: 0x00348804
	private bool GridPutCheck(List<GameObject> GridList, Carsol carsol, GameObject buildPart, ref List<DeleatWall> wall, ref List<GridInfo> tmpGridInfo, ref List<int> tmpSmallGridID)
	{
		this.GridCheck(GridList, carsol, tmpGridInfo, tmpSmallGridID, carsol.transform.rotation, this.nPartsForm, null);
		return tmpSmallGridID.Count != 0 && this.PutCheck(tmpGridInfo, tmpSmallGridID, buildPart, carsol, ref wall) && this.BoxCastRange.MinX >= this.MinPos.x - this.fGridSize / 2f && this.BoxCastRange.MaxX <= this.MinPos.x - this.fGridSize / 2f + this.fGridSize * (float)this.AreaNumX && this.BoxCastRange.MinZ >= this.MinPos.z - this.fGridSize / 2f && this.BoxCastRange.MaxZ <= this.MinPos.z - this.fGridSize / 2f + this.fGridSize * (float)this.AreaNumZ;
	}

	// Token: 0x06007C42 RID: 31810 RVA: 0x0034A520 File Offset: 0x00348920
	private void FloorPartsDel(GameObject buildPart, List<GridInfo> tmpGridInfo, List<CraftCommandList.ChangeValParts> AutoFloor)
	{
		if (this.partsMgr.GetBuildPartsInfo(buildPart).GetInfo(3) == 9)
		{
			this.PartsOnStairsDel(tmpGridInfo, AutoFloor);
		}
		else if (this.partsMgr.GetBuildPartsInfo(buildPart).GetInfo(3) == 11)
		{
			this.PartsOnElevatorDel(tmpGridInfo, AutoFloor);
		}
	}

	// Token: 0x06007C43 RID: 31811 RVA: 0x0034A574 File Offset: 0x00348974
	private void GridCheck(List<GameObject> GridList, Carsol carsol, List<GridInfo> tmpGridInfo, List<int> tmpSmallGridID, Quaternion Rayrot, int nkind, Vector3? pos = null)
	{
		bool[] array = new bool[GridList.Count];
		for (int i = 0; i < GridList.Count; i++)
		{
			array[i] = GridList[i].activeSelf;
			GridList[i].SetActive(true);
		}
		List<RaycastHit[]> hitsObjInfo = carsol.CheckCarsol(carsol.transform.rotation, Rayrot, nkind, ref this.BoxCastRange, pos);
		this.GridCheck(hitsObjInfo, GridList, tmpGridInfo, tmpSmallGridID);
		for (int j = 0; j < GridList.Count; j++)
		{
			GridList[j].SetActive(array[j]);
		}
	}

	// Token: 0x06007C44 RID: 31812 RVA: 0x0034A610 File Offset: 0x00348A10
	private void GridCheck(List<RaycastHit[]> HitsObjInfo, List<GameObject> GridList, List<GridInfo> GridInfo, List<int> SmallGridID)
	{
		for (int i = 0; i < GridList.Count; i++)
		{
			Transform[] componentsInChildren = GridList[i].GetComponentsInChildren<Transform>();
			GridInfo gridinfo = this.BaseGridInfo[i];
			for (int j = 0; j < HitsObjInfo.Count; j++)
			{
				for (int k = 0; k < HitsObjInfo[j].Length; k++)
				{
					this.GridCheck(HitsObjInfo[j][k].transform, componentsInChildren, GridInfo, SmallGridID, gridinfo);
				}
			}
		}
	}

	// Token: 0x06007C45 RID: 31813 RVA: 0x0034A6A4 File Offset: 0x00348AA4
	private void GridCheck(Transform HitsObjInfo, Transform[] smallGrids, List<GridInfo> gridInfos, List<int> SmallGridID, GridInfo Gridinfo)
	{
		for (int i = 1; i < smallGrids.Length; i++)
		{
			if (HitsObjInfo.position == smallGrids[i].position)
			{
				gridInfos.Add(Gridinfo);
				SmallGridID.Add(i - 1);
			}
		}
	}

	// Token: 0x06007C46 RID: 31814 RVA: 0x0034A6F0 File Offset: 0x00348AF0
	private bool PutCheck(List<GridInfo> gridInfo, List<int> SmallGridID, GameObject buildPart, Carsol carsol, ref List<DeleatWall> wall)
	{
		int info = this.partsMgr.GetBuildPartsInfo(buildPart).GetInfo(3);
		int num = gridInfo[0].nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt];
		for (int i = 1; i < gridInfo.Count; i++)
		{
			if (num != gridInfo[i].nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt])
			{
				return false;
			}
		}
		int[] array = new int[2];
		array[0] = gridInfo[0].GetStackPartsOnSmallGrid(SmallGridID[0], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt).Count((int v) => v != -1);
		for (int j = 1; j < gridInfo.Count; j++)
		{
			int[] stackPartsOnSmallGrid = gridInfo[j].GetStackPartsOnSmallGrid(SmallGridID[j], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
			array[1] = 0;
			for (int k = 0; k < stackPartsOnSmallGrid.Length; k++)
			{
				if (stackPartsOnSmallGrid[k] == -1)
				{
					break;
				}
				array[1]++;
			}
			if (array[0] != array[1])
			{
				return false;
			}
		}
		for (int l = 0; l < gridInfo.Count; l++)
		{
			if (this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.JudgeCondition[0] == 0 && this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.JudgeCondition[1] == 0)
			{
				break;
			}
			List<int> smallGridPutElement = gridInfo[l].GetSmallGridPutElement(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[l]);
			if (smallGridPutElement == null)
			{
				return false;
			}
			if (this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.JudgeCondition[0] != smallGridPutElement[smallGridPutElement.Count - 1])
			{
				return false;
			}
			if (this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.JudgeCondition[1] == smallGridPutElement[smallGridPutElement.Count - 1])
			{
				return false;
			}
		}
		int m = 0;
		while (m < gridInfo.Count)
		{
			if (info != 14 && info != 7 && !gridInfo[m].gameObject.activeSelf)
			{
				return false;
			}
			if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt != 0)
			{
				int[] partOnSmallGrid = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt - 1);
				if (partOnSmallGrid[2] == 9)
				{
					return false;
				}
			}
			switch (info)
			{
			case 1:
			case 2:
			{
				int[] partOnSmallGrid2 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (partOnSmallGrid2[0] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (info != 1)
				{
					if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt != 0)
					{
						return false;
					}
				}
				break;
			}
			case 3:
			{
				if (this.bFloorLimit && Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1 == 3)
				{
					return false;
				}
				if (gridInfo[m].GetStateSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				int[] partOnSmallGrid3 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (partOnSmallGrid3[2] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (gridInfo[m].GetStackPartsOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[0] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (partOnSmallGrid3[4] == 12)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (partOnSmallGrid3[5] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt != 0 && partOnSmallGrid3[0] == -1)
				{
					return false;
				}
				break;
			}
			case 4:
			{
				if (this.bFloorLimit && Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1 == 3)
				{
					return false;
				}
				int num2 = 0;
				int[] stackWallOnSmallGrid = gridInfo[m].GetStackWallOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				for (int n = 0; n < stackWallOnSmallGrid.Length; n++)
				{
					if (stackWallOnSmallGrid[n] != -1)
					{
						num2++;
					}
				}
				if (gridInfo[m].GetStateSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 1)
				{
					if (num2 == 0)
					{
						this.craftInfomationUI.SetWarningMessage(0);
						return false;
					}
					if (num2 >= Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] + 5 - gridInfo[m].nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt])
					{
						return false;
					}
					if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt == 0)
					{
						if (!this.WallDireCheck((float)gridInfo[m].nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt]))
						{
							return false;
						}
					}
					else if (!this.WallDireCheck((float)(gridInfo[m].nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] + Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[0])))
					{
						return false;
					}
				}
				int[] partOnSmallGrid4 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (partOnSmallGrid4[2] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (gridInfo[m].GetStackPartsOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[0] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (partOnSmallGrid4[4] == 12)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (partOnSmallGrid4[5] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt != 0 && gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[0] == -1)
				{
					return false;
				}
				break;
			}
			case 5:
			{
				if (gridInfo[m].GetCanRoofState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) != 1)
				{
					return false;
				}
				int[] partOnSmallGrid5 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (partOnSmallGrid5[6] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				break;
			}
			case 6:
			case 16:
			{
				int[] partOnSmallGrid6 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (!this.WhitchDoorOnWall(carsol, this.partsMgr.GetBuildPartsInfo(buildPart).nHeight, ref wall))
				{
					return false;
				}
				if (partOnSmallGrid6[3] != -1)
				{
					return false;
				}
				break;
			}
			case 7:
			{
				if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt != 0 && m == 0)
				{
					bool flag = false;
					for (int num3 = 0; num3 < gridInfo.Count; num3++)
					{
						flag = (gridInfo[num3].GetPartOnSmallGrid(0, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[0] != -1);
						if (flag)
						{
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				if (gridInfo[m].GetStateSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				int[] partOnSmallGrid7 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (partOnSmallGrid7[2] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (gridInfo[m].GetStackPartsOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[0] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (partOnSmallGrid7[5] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				break;
			}
			case 8:
			{
				if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt != 0 && gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[0] == -1)
				{
					return false;
				}
				if (gridInfo[m].GetStateSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				int num4 = 0;
				int[] stackPartsOnSmallGrid2 = gridInfo[m].GetStackPartsOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				for (int num5 = 0; num5 < stackPartsOnSmallGrid2.Length; num5++)
				{
					if (stackPartsOnSmallGrid2[num5] != -1)
					{
						num4++;
					}
				}
				if (num4 >= Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] + 5 - gridInfo[m].nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt])
				{
					return false;
				}
				int[] partOnSmallGrid8 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (partOnSmallGrid8[5] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				break;
			}
			case 9:
			{
				if (gridInfo[m].GetStateSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				int[] partOnSmallGrid9 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (partOnSmallGrid9[2] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				int[] stackPartsOnSmallGrid3 = gridInfo[m].GetStackPartsOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (stackPartsOnSmallGrid3[0] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (partOnSmallGrid9[4] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (partOnSmallGrid9[5] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1 != Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt)
				{
					if (gridInfo[m].GetStateSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1) == 1)
					{
						this.craftInfomationUI.SetWarningMessage(0);
						return false;
					}
					partOnSmallGrid9 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1);
					if (partOnSmallGrid9[2] != -1)
					{
						this.craftInfomationUI.SetWarningMessage(0);
						return false;
					}
					if (partOnSmallGrid9[4] != -1)
					{
						this.craftInfomationUI.SetWarningMessage(0);
						return false;
					}
					if (partOnSmallGrid9[5] != -1)
					{
						this.craftInfomationUI.SetWarningMessage(0);
						return false;
					}
					stackPartsOnSmallGrid3 = gridInfo[m].GetStackPartsOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1);
					if (stackPartsOnSmallGrid3[0] != -1)
					{
						this.craftInfomationUI.SetWarningMessage(0);
						return false;
					}
				}
				if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt > 0 && gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[0] == -1)
				{
					return false;
				}
				break;
			}
			case 10:
			{
				int[] partOnSmallGrid10 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				int[] stackWallOnSmallGrid2 = gridInfo[m].GetStackWallOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (gridInfo[m].GetStateSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 0)
				{
					return false;
				}
				bool flag2 = true;
				for (int num6 = 0; num6 < stackWallOnSmallGrid2.Length; num6++)
				{
					if (stackWallOnSmallGrid2[num6] != -1)
					{
						flag2 = false;
						break;
					}
				}
				if (partOnSmallGrid10[1] == -1 && flag2)
				{
					return false;
				}
				if (partOnSmallGrid10[3] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (partOnSmallGrid10[4] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				break;
			}
			case 11:
			{
				if (Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt < 3)
				{
					return false;
				}
				int num7 = 0;
				for (int num8 = 0; num8 < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt; num8++)
				{
					if (gridInfo[m].GetUseState(num8))
					{
						num7++;
					}
				}
				if (gridInfo[m].GetPartOnSmallGrid(0, num7 - 1)[0] != 1)
				{
					return false;
				}
				if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt == 0)
				{
					if (gridInfo[m].GetStateSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 1)
					{
						this.craftInfomationUI.SetWarningMessage(0);
						return false;
					}
					int[] partOnSmallGrid11 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], 0);
					for (int num9 = 0; num9 < partOnSmallGrid11.Length; num9++)
					{
						if (num9 != 0 && num9 != 6)
						{
							if (partOnSmallGrid11[num9] != -1)
							{
								this.craftInfomationUI.SetWarningMessage(0);
								return false;
							}
						}
					}
					if (gridInfo[m].GetStackPartsOnSmallGrid(SmallGridID[m], 0)[0] != -1)
					{
						this.craftInfomationUI.SetWarningMessage(0);
						return false;
					}
				}
				for (int num10 = 1; num10 < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt - 1; num10++)
				{
					if (gridInfo[m].GetStateSmallGrid(SmallGridID[m], num10) == 1)
					{
						this.craftInfomationUI.SetWarningMessage(0);
						return false;
					}
					int[] partOnSmallGrid11 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], num10);
					for (int num11 = 0; num11 < partOnSmallGrid11.Length; num11++)
					{
						if (num11 != 0 && num11 != 6)
						{
							if (partOnSmallGrid11[num11] != -1)
							{
								this.craftInfomationUI.SetWarningMessage(0);
								return false;
							}
						}
					}
					if (gridInfo[m].GetStackPartsOnSmallGrid(SmallGridID[m], num10)[0] != -1)
					{
						this.craftInfomationUI.SetWarningMessage(0);
						return false;
					}
				}
				break;
			}
			case 12:
			{
				if (gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[6] == -1)
				{
					return false;
				}
				int[] partOnSmallGrid12 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				bool flag3 = false;
				int[] stackWallOnSmallGrid3 = gridInfo[m].GetStackWallOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				for (int num12 = 0; num12 < stackWallOnSmallGrid3.Length; num12++)
				{
					if (stackWallOnSmallGrid3[num12] != -1)
					{
						flag3 = true;
						break;
					}
				}
				if (partOnSmallGrid12[1] != -1 && flag3)
				{
					return false;
				}
				if (partOnSmallGrid12[4] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				break;
			}
			case 13:
			{
				int[] partOnSmallGrid13 = gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				int[] stackWallOnSmallGrid4 = gridInfo[m].GetStackWallOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (gridInfo[m].GetStateSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 0)
				{
					return false;
				}
				int num13 = 0;
				for (int num14 = 0; num14 < stackWallOnSmallGrid4.Length; num14++)
				{
					if (stackWallOnSmallGrid4[num14] != -1)
					{
						num13++;
					}
				}
				bool flag4 = num13 < this.PutHeight + 1;
				if (partOnSmallGrid13[1] == -1 && flag4)
				{
					return false;
				}
				if (partOnSmallGrid13[4] != -1)
				{
					return false;
				}
				if (partOnSmallGrid13[3] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				break;
			}
			case 14:
			{
				if (gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[5] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				if (gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[1] != -1)
				{
					this.craftInfomationUI.SetWarningMessage(0);
					return false;
				}
				int[] stackWallOnSmallGrid5 = gridInfo[m].GetStackWallOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				int num15 = 0;
				for (int num16 = 0; num16 < stackWallOnSmallGrid5.Length; num16++)
				{
					if (stackWallOnSmallGrid5[num16] == -1)
					{
						break;
					}
					num15++;
				}
				if (num15 > 0)
				{
					return false;
				}
				if (!gridInfo[m].gameObject.activeSelf)
				{
					gridInfo[m].gameObject.SetActive(true);
				}
				break;
			}
			case 15:
				goto IL_1194;
			default:
				goto IL_1194;
			}
			IL_1205:
			m++;
			continue;
			IL_1194:
			if (gridInfo[m].GetStateSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 1)
			{
				this.craftInfomationUI.SetWarningMessage(0);
				return false;
			}
			if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt > 0 && gridInfo[m].GetPartOnSmallGrid(SmallGridID[m], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[0] == -1)
			{
				return false;
			}
			goto IL_1205;
		}
		return true;
	}

	// Token: 0x06007C47 RID: 31815 RVA: 0x0034B918 File Offset: 0x00349D18
	private void SetParts(Transform CarsolTransform, Quaternion rot, GameObject buildPart, int numX, int numZ, int mode = 0)
	{
		Vector3 localPosition = CarsolTransform.localPosition;
		int info = this.partsMgr.GetBuildPartsInfo(buildPart).GetInfo(3);
		switch (info)
		{
		case 12:
			localPosition.y = (float)(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt * 5 + 5 + Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[0] - this.partsMgr.GetBuildPartsInfo(buildPart).nHeight / 2);
			goto IL_104;
		case 13:
		case 14:
			break;
		default:
			switch (info)
			{
			case 2:
				break;
			case 3:
			case 4:
				goto IL_104;
			case 5:
				localPosition.y = (float)(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt * 5 + 5 + Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[0]);
				goto IL_104;
			default:
				goto IL_104;
			}
			break;
		}
		localPosition.y = this.CarsolUnderGridList[0].transform.position.y + this.fGridSize / 2f;
		localPosition.y += (float)this.PutHeight;
		IL_104:
		if (mode == 0)
		{
			localPosition.y -= this.fGridSize / 2f;
		}
		buildPart.transform.localPosition = localPosition;
		buildPart.transform.localRotation = Quaternion.Euler(buildPart.transform.localRotation.eulerAngles.x, 0f, buildPart.transform.localRotation.eulerAngles.z);
		buildPart.GetComponent<BuildPartsInfo>().nPutFloor = Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt;
		if (info != 1)
		{
			buildPart.transform.localRotation = rot * buildPart.transform.localRotation;
		}
		this.nPutPartsNum++;
	}

	// Token: 0x06007C48 RID: 31816 RVA: 0x0034BAEA File Offset: 0x00349EEA
	private void ChangeKindPart(int kind)
	{
		this.nPartsForm = kind;
		this.nPartsForm %= 10;
		if (this.nPartsForm < 0)
		{
			this.nPartsForm = 9;
		}
		this.ChangeCarsol();
		this.nID = 0;
	}

	// Token: 0x06007C49 RID: 31817 RVA: 0x0034BB23 File Offset: 0x00349F23
	private void ChangeCarsol()
	{
		this.carsol.ChangeCarsol(this.nPartsForm);
	}

	// Token: 0x06007C4A RID: 31818 RVA: 0x0034BB38 File Offset: 0x00349F38
	private void CraftAllReset()
	{
		for (int i = 0; i < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt; i++)
		{
			this.TargetFloorReset(i);
			Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveListUpdate[i] = false;
		}
		for (int j = 0; j < Singleton<CraftCommandListBaseObject>.Instance.BaseParts.Length; j++)
		{
			for (int k = 0; k < Singleton<CraftCommandListBaseObject>.Instance.BaseParts[j].Count; k++)
			{
				Singleton<CraftCommandListBaseObject>.Instance.BaseParts[j][k].ReserveListDel(0, 1);
			}
		}
		this.FloorResetGrid();
		Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt = 0;
		Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt = 1;
		this.gridMap.ChangeCraftMap(this.GridList, 0);
		this.gridMap.CraftMapSearchRoom(this.GridList, 0);
		GridInfo.ChangeGridInfo(this.BaseGridInfo, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		this.nCarsolMode = 0;
		this.Cam.Reset(0);
		this.Cam.bLock = false;
		this.Cam.nFloorCnt = 0;
		this.Cam.ForceMoveCam(0f);
	}

	// Token: 0x06007C4B RID: 31819 RVA: 0x0034BC60 File Offset: 0x0034A060
	private void TargetFloorReset(int targetfloor)
	{
		for (int i = 0; i < this.GridList.Count; i++)
		{
			GridInfo gridInfo = this.BaseGridInfo[i];
			Transform[] componentsInChildren = this.GridList[i].GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren.Length - 1; j++)
			{
				gridInfo.ChangeSmallGrid(j, 0, -1, targetfloor, false);
				gridInfo.ChangeSmallGridColor(targetfloor, j);
				gridInfo.SetInRoomSmallGrid(j, false, targetfloor);
				gridInfo.SetCanRoofSmallGrid(j, targetfloor, 0);
			}
		}
		for (int k = 0; k < 10; k++)
		{
			for (int l = 0; l < Singleton<CraftCommandListBaseObject>.Instance.BaseParts[k].Count; l++)
			{
				List<GameObject> list = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[k][l].GetList();
				for (int m = 0; m < list.Count; m++)
				{
					BuildPartsInfo component = list[m].GetComponent<BuildPartsInfo>();
					if (list[m].activeSelf || component.nPutFloor != -1)
					{
						if (component.nPutFloor == targetfloor)
						{
							list[m].SetActive(false);
							component.nPutFloor = -1;
						}
					}
				}
			}
		}
	}

	// Token: 0x06007C4C RID: 31820 RVA: 0x0034BDB8 File Offset: 0x0034A1B8
	private void FloorResetGrid()
	{
		for (int i = 0; i < this.GridList.Count; i++)
		{
			GridInfo gridInfo = this.BaseGridInfo[i];
			this.GridList[i].SetActive(true);
			Vector3 position = this.GridList[i].transform.position;
			position.y = this.MinPos.y;
			this.GridList[i].transform.position = position;
			gridInfo.DelFloor(0);
			gridInfo.AddFloor();
			gridInfo.SetUseState(0, true);
		}
	}

	// Token: 0x06007C4D RID: 31821 RVA: 0x0034BE55 File Offset: 0x0034A255
	private void SaveCraft()
	{
		this.save.Save(this.Gridpool);
		Singleton<SaveLoadUI>.Instance.saveEnd.SetActive(true);
	}

	// Token: 0x06007C4E RID: 31822 RVA: 0x0034BE78 File Offset: 0x0034A278
	private void LoadCraft()
	{
		this.save.Load(this.Gridpool, Singleton<SaveLoadUI>.Instance.saveFiles[Singleton<SaveLoadUI>.Instance.nSaveID]);
		Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt = 0;
		this.TargetFloorUp();
		this.gridMap.ChangeCraftMap(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		this.gridMap.CraftMapSearchRoom(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		GridInfo.ChangeGridInfo(this.BaseGridInfo, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		Singleton<SaveLoadUI>.Instance.loadEnd.SetActive(true);
		Singleton<UndoRedoMgr>.Instance.Clear();
	}

	// Token: 0x06007C4F RID: 31823 RVA: 0x0034BF28 File Offset: 0x0034A328
	private bool WhitchDoorOnWall(Carsol carsol, int height, ref List<DeleatWall> wall)
	{
		for (int i = 0; i < Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm].Count; i++)
		{
			int itemKind = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][i].GetItemKind();
			if (itemKind == 3 || itemKind == 4)
			{
				List<GameObject> list = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][i].GetList();
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j].activeSelf)
					{
						if (list[j].GetComponent<BuildPartsInfo>().nPutFloor == Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)
						{
							if (list[j].transform.position.x == carsol.transform.position.x && list[j].transform.position.y <= (float)(height - 1 + Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt * 5) && list[j].transform.position.z == carsol.transform.position.z)
							{
								DeleatWall item;
								item.Wall = list[j];
								item.PartForm = this.nPartsForm;
								item.PartKind = i;
								item.WallID = j;
								wall.Add(item);
							}
						}
					}
				}
			}
		}
		wall = wall.Distinct<DeleatWall>().ToList<DeleatWall>();
		return wall.Count == height;
	}

	// Token: 0x06007C50 RID: 31824 RVA: 0x0034C0E0 File Offset: 0x0034A4E0
	private void FloorUP(List<CraftCommandList.ChangeValParts> AutoFloor)
	{
		bool flag = false;
		if (Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt - 1 == Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)
		{
			bool flag2 = !this.bFloorLimit || Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt < 3;
			if (flag2)
			{
				for (int i = 0; i < this.GridList.Count; i++)
				{
					this.BaseGridInfo[i].AddFloor();
				}
				Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList.Add(new bool[this.AllGridNum]);
				Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveListUpdate.Add(false);
				Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt++;
				flag = true;
				Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight.Add(0);
				this.Cam.setLimitPos(10f);
			}
		}
		if (Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt == Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1)
		{
			return;
		}
		int num = -1;
		GameObject gameObject = null;
		Vector3 position = default(Vector3);
		int num2 = 0;
		int num3 = 0;
		int num4 = 6;
		for (int j = 0; j < this.GridList.Count; j++)
		{
			if (Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo[j].nFloorPartsHeight[0] > 0)
			{
				if (num4 > Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo[j].nFloorPartsHeight[0])
				{
					num4 = Singleton<CraftCommandListBaseObject>.Instance.BaseGridInfo[j].nFloorPartsHeight[0];
				}
			}
		}
		for (int k = 0; k < this.GridList.Count; k++)
		{
			GridInfo gridInfo = this.BaseGridInfo[k];
			if (gridInfo.GetFloorNum() <= Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1)
			{
				break;
			}
			if (gridInfo.GetCanRoofState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 1 || gridInfo.GetPartOnSmallGrid(0, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[2] == 9)
			{
				if (!gridInfo.GetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1))
				{
					bool flag3 = false;
					for (int l = 0; l < 4; l++)
					{
						int[] partOnSmallGrid = gridInfo.GetPartOnSmallGrid(l, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
						int[] partOnSmallGrid2 = gridInfo.GetPartOnSmallGrid(l, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1);
						if (partOnSmallGrid[2] == 9 || partOnSmallGrid2[0] != -1)
						{
							if (partOnSmallGrid[2] == 9)
							{
								for (int m = 0; m < 4; m++)
								{
									gridInfo.ChangeSmallGrid(m, 1, -1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, false);
									gridInfo.ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, m, 0, -1);
									gridInfo.ChangeSmallGridColor(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, m);
								}
							}
							flag3 = true;
							break;
						}
					}
					if (!flag3)
					{
						num = -1;
						gameObject = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[0][0].Get(ref num);
						AutoFloor.Add(new CraftCommandList.ChangeValParts());
						AutoFloor[AutoFloor.Count - 1].nFormKind = 0;
						AutoFloor[AutoFloor.Count - 1].nPoolID = 0;
						AutoFloor[AutoFloor.Count - 1].nItemID = num;
						this.URPutPartsSetB(AutoFloor[AutoFloor.Count - 1], gameObject, 0);
						if (this.GridList[k].GetComponent<GridInfo>().GetPartOnSmallGrid(0, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[2] != 15)
						{
							position = this.GridList[k].transform.position;
							position.y = 0f;
							for (int n = 0; n < Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1; n++)
							{
								position.y += (float)(Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[n] + 5);
							}
							gameObject.transform.position = position;
							gameObject.GetComponent<BuildPartsInfo>().nPutFloor = Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1;
							gameObject.SetActive(false);
							for (int num5 = 0; num5 < 4; num5++)
							{
								gridInfo.ChangeSmallGrid(num5, 2, 1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, false);
								gridInfo.SetCanRoofSmallGrid(num5, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, 2);
							}
						}
						else
						{
							for (int num6 = 0; num6 < 4; num6++)
							{
								gridInfo.ChangeSmallGrid(num6, 0, -1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, false);
								gridInfo.SetCanRoofSmallGrid(num6, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, 2);
							}
						}
						gridInfo.SetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, true);
						Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1][gridInfo.nID] = true;
						this.URPutPartsSetB(AutoFloor[AutoFloor.Count - 1], gameObject, 1);
						this.nPutPartsNum++;
					}
					if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt == 0)
					{
						num = -1;
						List<BuildPartsPool>[] baseParts = Singleton<CraftCommandListBaseObject>.Instance.BaseParts;
						for (int num7 = 0; num7 < baseParts.Length; num7++)
						{
							for (int num8 = 0; num8 < baseParts[num7].Count; num8++)
							{
								if (Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[0] != 0 || baseParts[num7][num8].GetItemKind() == 1)
								{
									if (Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[0] == 0 || baseParts[num7][num8].GetItemKind() == 2)
									{
										gameObject = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[num7][num8].Get(ref num);
										num2 = num7;
										num3 = num8;
										break;
									}
								}
							}
						}
						AutoFloor.Add(new CraftCommandList.ChangeValParts());
						AutoFloor[AutoFloor.Count - 1].nFormKind = num2;
						AutoFloor[AutoFloor.Count - 1].nPoolID = num3;
						AutoFloor[AutoFloor.Count - 1].nItemID = num;
						this.URPutPartsSetB(AutoFloor[AutoFloor.Count - 1], gameObject, 0);
						position = this.GridList[k].transform.position;
						if (Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[0] != 0)
						{
							position.y += (float)num4;
						}
						gameObject.transform.position = position;
						BuildPartsInfo buildPartsInfo = this.partsMgr.GetBuildPartsInfo(gameObject);
						buildPartsInfo.nPutFloor = Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt;
						if (gridInfo.GetPartOnSmallGrid(0, 0)[0] != -1)
						{
							gameObject.SetActive(false);
							Singleton<CraftCommandListBaseObject>.Instance.BaseParts[num2][num3].ReserveListDel(num, 0);
							buildPartsInfo.nPutFloor = -1;
						}
						else
						{
							gameObject.SetActive(true);
							for (int num9 = 0; num9 < 4; num9++)
							{
								gridInfo.ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, num9, 0, buildPartsInfo.GetInfo(3));
								if (gridInfo.GetStateSmallGrid(num9, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) != 1)
								{
									gridInfo.ChangeSmallGrid(num9, 2, buildPartsInfo.GetInfo(3), Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
								}
								gridInfo.SetSmallGridPutElement(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, num9, this.partsMgr.BuildPartPoolDic[num2][num3].Item2.Element, false, true);
							}
							if (num4 > 5)
							{
								gridInfo.nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] = 0;
							}
							else
							{
								gridInfo.nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] = num4;
							}
							this.nPutPartsNum++;
						}
						this.URPutPartsSetB(AutoFloor[AutoFloor.Count - 1], gameObject, 1);
					}
					gameObject = null;
					num = -1;
					for (int num10 = 0; num10 < 10; num10++)
					{
						for (int num11 = 0; num11 < Singleton<CraftCommandListBaseObject>.Instance.BaseParts[num10].Count; num11++)
						{
							if (Singleton<CraftCommandListBaseObject>.Instance.BaseParts[num10][num11].GetItemKind() == 5)
							{
								gameObject = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[num10][num11].Get(ref num);
								break;
							}
						}
						if (gameObject != null)
						{
							break;
						}
					}
					BuildPartsInfo buildPartsInfo2 = this.partsMgr.GetBuildPartsInfo(gameObject);
					AutoFloor.Add(new CraftCommandList.ChangeValParts());
					AutoFloor[AutoFloor.Count - 1].nFormKind = buildPartsInfo2.GetInfo(2);
					AutoFloor[AutoFloor.Count - 1].nPoolID = buildPartsInfo2.GetInfo(1);
					AutoFloor[AutoFloor.Count - 1].nItemID = num;
					this.URPutPartsSetB(AutoFloor[AutoFloor.Count - 1], gameObject, 0);
					position = this.GridList[k].transform.position;
					position.y = (float)(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt * 5 + 5 + Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[0]);
					gameObject.transform.position = position;
					BuildPartsInfo buildPartsInfo3 = this.partsMgr.GetBuildPartsInfo(gameObject);
					buildPartsInfo3.nPutFloor = Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt;
					if (gridInfo.GetPartOnSmallGrid(0, buildPartsInfo3.nPutFloor)[6] == 5)
					{
						gameObject.SetActive(false);
						Singleton<CraftCommandListBaseObject>.Instance.BaseParts[AutoFloor[AutoFloor.Count - 1].nFormKind][AutoFloor[AutoFloor.Count - 1].nPoolID].ReserveListDel(num, 0);
						buildPartsInfo3.nPutFloor = -1;
					}
					else
					{
						gameObject.SetActive(true);
						for (int num12 = 0; num12 < 4; num12++)
						{
							gridInfo.ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, num12, 6, 5);
							if (gridInfo.GetStateSmallGrid(num12, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) != 1)
							{
								gridInfo.ChangeSmallGrid(num12, 2, 5, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
							}
						}
						this.nPutPartsNum++;
					}
					this.URPutPartsSetB(AutoFloor[AutoFloor.Count - 1], gameObject, 1);
				}
			}
		}
		this.PartsOnElevatorDelFU(AutoFloor);
		if (flag)
		{
			if (!this.FloorUpTex.activeSelf)
			{
				this.FloorUpTex.SetActive(true);
			}
			Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt++;
			Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt %= Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt;
			this.TargetFloorUp();
		}
		GridInfo.ChangeGridInfo(this.BaseGridInfo, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		this.gridMap.ChangeCraftMap(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		this.gridMap.CraftMapSearchRoom(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
	}

	// Token: 0x06007C51 RID: 31825 RVA: 0x0034CC18 File Offset: 0x0034B018
	private void AllFloorDel(List<CraftCommandList.ChangeValParts> valAutoParts)
	{
		for (int i = 0; i < this.GridList.Count; i++)
		{
			GridInfo gridInfo = this.BaseGridInfo[i];
			for (int j = 0; j < 4; j++)
			{
				gridInfo.ChangeSmallGrid(j, 0, -1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, false);
			}
			gridInfo.DelFloor(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1);
		}
		this.FloorDeleteBuildParts(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, valAutoParts);
		Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt = Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1;
		Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight.RemoveAt(Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight.Count - 1);
		this.Cam.setLimitPos(-10f);
	}

	// Token: 0x06007C52 RID: 31826 RVA: 0x0034CCE4 File Offset: 0x0034B0E4
	private void FloorDel(List<CraftCommandList.ChangeValParts> valAutoParts)
	{
		for (int i = 0; i < this.GridList.Count; i++)
		{
			GridInfo gridInfo = this.BaseGridInfo[i];
			if (gridInfo.GetCanRoofState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 0)
			{
				if (gridInfo.GetPartOnSmallGrid(0, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[2] != 9)
				{
					for (int j = Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1; j < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt; j++)
					{
						for (int k = 0; k < 4; k++)
						{
							gridInfo.ChangeSmallGrid(k, 0, -1, j, false);
						}
						gridInfo.SetUseState(j, false);
						Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList[j][i] = false;
					}
				}
			}
		}
		this.FloorDeleteBuildParts(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, this.BaseGridInfo, valAutoParts);
	}

	// Token: 0x06007C53 RID: 31827 RVA: 0x0034CDCC File Offset: 0x0034B1CC
	private void FloorDeleteBuildParts(int nFloorCnt, List<CraftCommandList.ChangeValParts> valAutoParts)
	{
		List<List<GameObject>> list = new List<List<GameObject>>();
		List<BuildPartsPool> list2 = new List<BuildPartsPool>();
		for (int i = 0; i < Singleton<CraftCommandListBaseObject>.Instance.BaseParts.Length; i++)
		{
			for (int j = 0; j < Singleton<CraftCommandListBaseObject>.Instance.BaseParts[i].Count; j++)
			{
				list.Add(Singleton<CraftCommandListBaseObject>.Instance.BaseParts[i][j].GetList());
				list2.Add(Singleton<CraftCommandListBaseObject>.Instance.BaseParts[i][j]);
			}
		}
		for (int k = 0; k < list.Count; k++)
		{
			for (int l = 0; l < list[k].Count; l++)
			{
				BuildPartsInfo component = list[k][l].GetComponent<BuildPartsInfo>();
				if (component.nPutFloor >= nFloorCnt)
				{
					valAutoParts.Add(new CraftCommandList.ChangeValParts());
					valAutoParts[valAutoParts.Count - 1].nFormKind = list2[k].GetFormKind();
					valAutoParts[valAutoParts.Count - 1].nPoolID = component.GetInfo(1);
					valAutoParts[valAutoParts.Count - 1].nItemID = component.GetInfo(0);
					this.URPutPartsSetB(valAutoParts[valAutoParts.Count - 1], list[k][l], 0);
					component.nPutFloor = -1;
					list[k][l].SetActive(false);
					list2[k].ReserveListDel(l, 0);
					this.URPutPartsSetB(valAutoParts[valAutoParts.Count - 1], list[k][l], 1);
					this.nPutPartsNum--;
				}
			}
		}
	}

	// Token: 0x06007C54 RID: 31828 RVA: 0x0034CFA4 File Offset: 0x0034B3A4
	private void FloorDeleteBuildParts(int nFloorCnt, List<GridInfo> Grid, List<CraftCommandList.ChangeValParts> valAutoParts)
	{
		List<List<GameObject>> list = new List<List<GameObject>>();
		List<BuildPartsPool> list2 = new List<BuildPartsPool>();
		for (int i = 0; i < Singleton<CraftCommandListBaseObject>.Instance.BaseParts.Length; i++)
		{
			for (int j = 0; j < Singleton<CraftCommandListBaseObject>.Instance.BaseParts[i].Count; j++)
			{
				list.Add(Singleton<CraftCommandListBaseObject>.Instance.BaseParts[i][j].GetList());
				list2.Add(Singleton<CraftCommandListBaseObject>.Instance.BaseParts[i][j]);
			}
		}
		for (int k = 0; k < list.Count; k++)
		{
			for (int l = 0; l < list[k].Count; l++)
			{
				BuildPartsInfo component = list[k][l].GetComponent<BuildPartsInfo>();
				if (component.nPutFloor >= nFloorCnt)
				{
					for (int m = 0; m < Grid.Count; m++)
					{
						if (Grid[m].GetCanRoofState(nFloorCnt - 1) == 0)
						{
							Rect rect = new Rect(Grid[m].transform.position.x - this.fGridSize / 2f, Grid[m].transform.position.z - this.fGridSize / 2f, this.fGridSize, this.fGridSize);
							Vector2 point = new Vector2(list[k][l].transform.position.x, list[k][l].transform.position.z);
							if (rect.Contains(point))
							{
								valAutoParts.Add(new CraftCommandList.ChangeValParts());
								valAutoParts[valAutoParts.Count - 1].nFormKind = list2[k].GetFormKind();
								valAutoParts[valAutoParts.Count - 1].nPoolID = component.GetInfo(1);
								valAutoParts[valAutoParts.Count - 1].nItemID = component.GetInfo(0);
								this.URPutPartsSetB(valAutoParts[valAutoParts.Count - 1], list[k][l], 0);
								component.nPutFloor = -1;
								list[k][l].SetActive(false);
								list2[k].ReserveListDel(l, 0);
								this.URPutPartsSetB(valAutoParts[valAutoParts.Count - 1], list[k][l], 1);
								this.nPutPartsNum--;
								break;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06007C55 RID: 31829 RVA: 0x0034D270 File Offset: 0x0034B670
	private void TargetFloorUp()
	{
		this.TargetFloorUpCamera();
		this.TargetFloorUpPart();
		this.TargetFloorUpGrid();
		for (int i = 0; i < this.GridList.Count; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				this.BaseGridInfo[i].ChangeSmallGridColor(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, j);
			}
		}
	}

	// Token: 0x06007C56 RID: 31830 RVA: 0x0034D2D9 File Offset: 0x0034B6D9
	private void TargetFloorUpCamera()
	{
		this.Cam.CameraUp(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
	}

	// Token: 0x06007C57 RID: 31831 RVA: 0x0034D2F0 File Offset: 0x0034B6F0
	private void TargetFloorUpGrid()
	{
		for (int i = 0; i < this.GridList.Count; i++)
		{
			GridInfo component = this.GridList[i].GetComponent<GridInfo>();
			if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt == 0)
			{
				if (!this.ViewMode)
				{
					this.GridList[i].SetActive(true);
				}
			}
			else
			{
				bool flag = false;
				GridInfo gridInfo = this.BaseGridInfo[i];
				if (gridInfo.GetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt))
				{
					flag = true;
				}
				if (!flag)
				{
					for (int j = 0; j < 4; j++)
					{
						int[] partOnSmallGrid = gridInfo.GetPartOnSmallGrid(j, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
						if (partOnSmallGrid[0] != -1)
						{
							flag = true;
							gridInfo.SetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, true);
							break;
						}
						if (partOnSmallGrid[5] == 14)
						{
							flag = true;
							gridInfo.SetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, true);
							break;
						}
						partOnSmallGrid = gridInfo.GetPartOnSmallGrid(j, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt - 1);
						if (partOnSmallGrid[0] == 5)
						{
							flag = true;
							gridInfo.SetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, true);
							break;
						}
						if (partOnSmallGrid[2] == 9)
						{
							flag = true;
							gridInfo.SetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, true);
							break;
						}
					}
				}
				if (!flag)
				{
					this.GridList[i].SetActive(false);
				}
			}
			Vector3 position = this.GridList[i].transform.position;
			position.y = this.MinPos.y;
			position.y += (float)(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt * 5);
			if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt > 0)
			{
				position.y += (float)Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[0];
			}
			this.GridList[i].transform.position = position;
			if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt > 0 && component.GetPartOnSmallGrid(0, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt - 1)[2] == 15)
			{
				Rect rect = new Rect(this.GridList[i].transform.position.x - this.fGridSize * 2f, this.GridList[i].transform.position.z - this.fGridSize * 2f, this.fGridSize * 5f, this.fGridSize * 5f);
				for (int k = 0; k < this.GridList.Count; k++)
				{
					Vector2 point = new Vector2(this.GridList[k].transform.position.x, this.GridList[k].transform.position.z);
					if (rect.Contains(point))
					{
						this.GridList[k].gameObject.SetActive(true);
						component.SetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, true);
					}
				}
			}
		}
	}

	// Token: 0x06007C58 RID: 31832 RVA: 0x0034D63C File Offset: 0x0034BA3C
	private void TargetFloorUpPart()
	{
		for (int i = 0; i < Singleton<CraftCommandListBaseObject>.Instance.BaseParts.Length; i++)
		{
			for (int j = 0; j < Singleton<CraftCommandListBaseObject>.Instance.BaseParts[i].Count; j++)
			{
				List<GameObject> list = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[i][j].GetList();
				for (int k = 0; k < list.Count; k++)
				{
					BuildPartsInfo component = list[k].GetComponent<BuildPartsInfo>();
					if (component.nPutFloor >= 0 && component.nPutFloor <= Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)
					{
						list[k].SetActive(true);
					}
					else
					{
						list[k].SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x06007C59 RID: 31833 RVA: 0x0034D708 File Offset: 0x0034BB08
	private void ChangeGridInfo(GameObject buildPart, List<GridInfo> gridInfo, List<int> SmallGridID, int targetCnt, List<CraftCommandList.ChangeValParts> valAutoParts)
	{
		int info = this.partsMgr.GetBuildPartsInfo(buildPart).GetInfo(3);
		int info2 = this.partsMgr.GetBuildPartsInfo(buildPart).GetInfo(2);
		int info3 = this.partsMgr.GetBuildPartsInfo(buildPart).GetInfo(1);
		bool floor = this.partsMgr.BuildPartPoolDic[info2][info3].Item2.Catkind == 1;
		int i = 0;
		while (i < targetCnt)
		{
			switch (info)
			{
			case 3:
			case 4:
				goto IL_8F;
			default:
				if (info == 15)
				{
					goto IL_8F;
				}
				break;
			case 7:
				if (!gridInfo[i].gameObject.activeSelf)
				{
					gridInfo[i].gameObject.SetActive(true);
					gridInfo[i].SetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, true);
					Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt][gridInfo[i].nID] = true;
				}
				break;
			}
			IL_16B:
			switch (info)
			{
			case 1:
			case 2:
				if (gridInfo[i].GetStateSmallGrid(SmallGridID[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 0)
				{
					gridInfo[i].ChangeSmallGrid(SmallGridID[i], 2, info, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
				}
				else
				{
					gridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], 0, info);
				}
				if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt != 0)
				{
					for (int j = 0; j < 4; j++)
					{
						gridInfo[i].SetCanRoofSmallGrid(j, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt - 1, 2);
					}
				}
				if (info != 1)
				{
					if (Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] < this.PutHeight)
					{
						int moveval = this.PutHeight - Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt];
						this.FloorHeightMove(moveval, (float)(Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] + 5), valAutoParts);
						Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] = this.PutHeight;
					}
				}
				break;
			case 3:
			case 4:
				for (int k = 0; k < this.partsMgr.GetBuildPartsInfo(buildPart).nHeight; k++)
				{
					gridInfo[i].ChangeSmallGrid(SmallGridID[i], 1, info, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
				}
				break;
			case 5:
				gridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], 6, 5);
				break;
			case 6:
				for (int l = 0; l < this.partsMgr.GetBuildPartsInfo(buildPart).nHeight; l++)
				{
					gridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], 1, -1);
				}
				gridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], 2, 6);
				break;
			case 7:
			case 8:
			case 9:
			case 11:
			case 15:
				goto IL_588;
			case 10:
				gridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], 3, 10);
				break;
			case 12:
				gridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], 4, 12);
				break;
			case 13:
				gridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], 4, 13);
				break;
			case 14:
				if (gridInfo[i].GetStateSmallGrid(SmallGridID[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) != 1)
				{
					gridInfo[i].ChangeSmallGrid(SmallGridID[i], 1, 14, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
				}
				else
				{
					gridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], 5, 14);
				}
				gridInfo[i].SetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, true);
				Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt][gridInfo[i].nID] = gridInfo[i].GetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				break;
			case 16:
				for (int m = 0; m < buildPart.GetComponent<BuildPartsInfo>().nHeight; m++)
				{
					gridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], 1, -1);
				}
				gridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], 2, 16);
				break;
			default:
				goto IL_588;
			}
			IL_622:
			if (info == 2)
			{
				gridInfo[i].nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] = this.PutHeight;
			}
			if (info != 12 && info != 5 && info != 14)
			{
				gridInfo[i].SetSmallGridPutElement(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], this.partsMgr.BuildPartPoolDic[info2][info3].Item2.Element, false, floor);
			}
			gridInfo[i].ChangeSmallGridColor(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i]);
			i++;
			continue;
			IL_588:
			if (this.partsMgr.BuildPartPoolDic[info2][info3].Item2.PutFlag != 0)
			{
				for (int n = 0; n < this.partsMgr.GetBuildPartsInfo(buildPart).nHeight; n++)
				{
					gridInfo[i].ChangeSmallGrid(SmallGridID[i], 2, info, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, true);
				}
			}
			else
			{
				gridInfo[i].ChangeSmallGrid(SmallGridID[i], 1, info, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
			}
			goto IL_622;
			IL_8F:
			gridInfo[i].SetCanRoofSmallGrid(SmallGridID[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, 2);
			if (gridInfo[i].GetPartOnSmallGrid(SmallGridID[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[0] == -1)
			{
				gridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, SmallGridID[i], 0, 1);
			}
			goto IL_16B;
		}
	}

	// Token: 0x06007C5A RID: 31834 RVA: 0x0034DDE8 File Offset: 0x0034C1E8
	private void CheckUnderRoof()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		for (int i = 0; i < this.GridList.Count; i++)
		{
			GridInfo gridInfo = this.BaseGridInfo[i];
			for (int j = Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt - 1; j >= 0; j--)
			{
				for (int k = 0; k < 4; k++)
				{
					bool flag = false;
					for (int l = 0; l < list.Count; l++)
					{
						if (list[l] == i && list2[l] == k)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						int[] partOnSmallGrid = gridInfo.GetPartOnSmallGrid(k, j);
						if (partOnSmallGrid[2] == 7)
						{
							list.Add(i);
							list2.Add(k);
							list3.Add(j);
						}
					}
				}
			}
		}
		for (int m = 0; m < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt; m++)
		{
			for (int n = 0; n < list.Count; n++)
			{
				if (m < list3[n])
				{
					GridInfo gridInfo2 = this.BaseGridInfo[list[n]];
					gridInfo2.SetInRoomSmallGrid(list2[n], true, m);
				}
			}
			GridInfo.ChangeGridInfo(this.BaseGridInfo, m);
		}
	}

	// Token: 0x06007C5B RID: 31835 RVA: 0x0034DF6C File Offset: 0x0034C36C
	private void PartsOnStairsDel(List<GridInfo> HitGridInfo, List<CraftCommandList.ChangeValParts> valAutoParts)
	{
		List<BuildPartsPool>[] baseParts = Singleton<CraftCommandListBaseObject>.Instance.BaseParts;
		for (int i = 0; i < HitGridInfo.Count; i++)
		{
			GridInfo gridInfo = HitGridInfo[i];
			if (gridInfo != null && gridInfo.GetFloorNum() > Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1)
			{
				bool flag = false;
				List<List<GameObject>> list = new List<List<GameObject>>();
				List<Tuple<int, int>> list2 = new List<Tuple<int, int>>();
				for (int j = 0; j < baseParts.Length; j++)
				{
					for (int k = 0; k < baseParts[j].Count; k++)
					{
						int itemKind = baseParts[j][k].GetItemKind();
						if (itemKind == 1 || itemKind == 5)
						{
							list2.Add(new Tuple<int, int>(j, k));
							list.Add(baseParts[j][k].GetList());
						}
					}
				}
				for (int l = 0; l < list.Count; l++)
				{
					for (int m = 0; m < list[l].Count; m++)
					{
						BuildPartsInfo component = list[l][m].GetComponent<BuildPartsInfo>();
						if (!list[l][m].activeSelf && component.nPutFloor == Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1)
						{
							if (list[l][m].transform.position.x == gridInfo.transform.position.x && list[l][m].transform.position.z == gridInfo.transform.position.z)
							{
								valAutoParts.Add(new CraftCommandList.ChangeValParts());
								valAutoParts[valAutoParts.Count - 1].nFormKind = list2[l].Item1;
								valAutoParts[valAutoParts.Count - 1].nPoolID = list2[l].Item2;
								valAutoParts[valAutoParts.Count - 1].nItemID = m;
								this.URPutPartsSetB(valAutoParts[valAutoParts.Count - 1], list[l][m], 0);
								list[l][m].SetActive(false);
								component.nPutFloor = -1;
								Singleton<CraftCommandListBaseObject>.Instance.BaseParts[list2[l].Item1][list2[l].Item2].ReserveListDel(m, 0);
								flag = true;
								this.URPutPartsSetB(valAutoParts[valAutoParts.Count - 1], list[l][m], 1);
								this.nPutPartsNum--;
								break;
							}
							if (flag)
							{
								break;
							}
						}
					}
					for (int n = 0; n < 4; n++)
					{
						gridInfo.ChangeSmallGrid(n, 1, -1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, false);
						gridInfo.ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, n, 0, -1);
						gridInfo.ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, n, 6, -1);
						gridInfo.ChangeSmallGridColor(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, n);
					}
				}
			}
		}
	}

	// Token: 0x06007C5C RID: 31836 RVA: 0x0034E2F8 File Offset: 0x0034C6F8
	private void PartsOnElevatorDel(List<GridInfo> gridinfo, List<CraftCommandList.ChangeValParts> valAutoParts)
	{
		if (this.BaseGridInfo[0].GetFloorNum() <= 2)
		{
			return;
		}
		if (gridinfo.Count == 0)
		{
			return;
		}
		List<Tuple<int, int>> list = null;
		List<List<GameObject>> list2 = null;
		List<BuildPartsPool>[] baseParts = Singleton<CraftCommandListBaseObject>.Instance.BaseParts;
		for (int i = 0; i < baseParts.Length; i++)
		{
			for (int j = 0; j < baseParts[i].Count; j++)
			{
				int itemKind = baseParts[i][j].GetItemKind();
				if (itemKind == 1 || itemKind == 5)
				{
					list.Add(new Tuple<int, int>(i, j));
					list2.Add(baseParts[i][j].GetList());
				}
			}
		}
		for (int k = 0; k < gridinfo.Count; k++)
		{
			for (int l = 0; l < list2.Count; l++)
			{
				for (int m = 0; m < list2[l].Count; m++)
				{
					BuildPartsInfo component = list2[l][m].GetComponent<BuildPartsInfo>();
					if (!list2[l][m].activeSelf && component.nPutFloor != Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt - 1)
					{
						if (list2[l][m].transform.position.x == gridinfo[k].transform.position.x && list2[l][m].transform.position.z == gridinfo[k].transform.position.z)
						{
							valAutoParts.Add(new CraftCommandList.ChangeValParts());
							valAutoParts[valAutoParts.Count - 1].nFormKind = list[l].Item1;
							valAutoParts[valAutoParts.Count - 1].nPoolID = list[l].Item2;
							valAutoParts[valAutoParts.Count - 1].nItemID = m;
							this.URPutPartsSetB(valAutoParts[valAutoParts.Count - 1], list2[l][m], 0);
							component.nPutFloor = -1;
							baseParts[list[l].Item1][list[l].Item2].ReserveListDel(m, 0);
							this.URPutPartsSetB(valAutoParts[valAutoParts.Count - 1], list2[l][m], 1);
							this.nPutPartsNum--;
						}
					}
				}
			}
		}
		for (int n = 0; n < gridinfo.Count; n++)
		{
			for (int num = 0; num < 4; num++)
			{
				for (int num2 = 1; num2 < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt - 1; num2++)
				{
					gridinfo[n].ChangeSmallGrid(num, 1, -1, num2, false);
					gridinfo[n].ChangeSmallGridItemKind(num2, num, 0, -1);
					gridinfo[n].ChangeSmallGridItemKind(num2 - 1, num, 6, -1);
					gridinfo[n].ChangeSmallGridColor(num2, num);
				}
			}
		}
	}

	// Token: 0x06007C5D RID: 31837 RVA: 0x0034E66C File Offset: 0x0034CA6C
	private void PartsOnElevatorDelFU(List<CraftCommandList.ChangeValParts> valAutoParts)
	{
		if (this.BaseGridInfo[0].GetFloorNum() <= 2)
		{
			return;
		}
		List<int> list = new List<int>();
		List<GridInfo> list2 = new List<GridInfo>();
		for (int i = 0; i < this.GridList.Count; i++)
		{
			GridInfo gridInfo = this.BaseGridInfo[i];
			if (gridInfo.GetPartOnSmallGrid(0, 0)[2] == 11)
			{
				list2.Add(gridInfo);
			}
		}
		if (list2.Count == 0)
		{
			return;
		}
		List<Tuple<int, int>> list3 = null;
		List<List<GameObject>> list4 = null;
		List<BuildPartsPool>[] baseParts = Singleton<CraftCommandListBaseObject>.Instance.BaseParts;
		for (int j = 0; j < baseParts.Length; j++)
		{
			for (int k = 0; k < baseParts[j].Count; k++)
			{
				int itemKind = baseParts[j][k].GetItemKind();
				if (itemKind == 1 || itemKind == 5)
				{
					list3.Add(new Tuple<int, int>(j, k));
					list4.Add(baseParts[j][k].GetList());
				}
			}
		}
		for (int l = 0; l < list2.Count; l++)
		{
			int num = 0;
			for (int m = 0; m < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt; m++)
			{
				if (list2[l].GetUseState(m))
				{
					num++;
				}
			}
			for (int n = 0; n < list4.Count; n++)
			{
				for (int num2 = 0; num2 < list4[n].Count; num2++)
				{
					BuildPartsInfo component = list4[n][num2].GetComponent<BuildPartsInfo>();
					if (component.nPutFloor > 0 && component.nPutFloor != num - 1)
					{
						if (list4[n][num2].transform.position.x == list2[l].transform.position.x && list4[n][num2].transform.position.z == list2[l].transform.position.z)
						{
							valAutoParts.Add(new CraftCommandList.ChangeValParts());
							valAutoParts[valAutoParts.Count - 1].nFormKind = list3[n].Item1;
							valAutoParts[valAutoParts.Count - 1].nPoolID = list3[n].Item2;
							valAutoParts[valAutoParts.Count - 1].nItemID = num2;
							this.URPutPartsSetB(valAutoParts[valAutoParts.Count - 1], list4[n][num2], 0);
							list4[n][num2].SetActive(false);
							component.nPutFloor = -1;
							Singleton<CraftCommandListBaseObject>.Instance.BaseParts[list3[n].Item1][list3[n].Item2].ReserveListDel(num2, 0);
							list.Add(l);
							this.URPutPartsSetB(valAutoParts[valAutoParts.Count - 1], list4[n][num2], 1);
							this.nPutPartsNum--;
						}
					}
				}
			}
		}
		for (int num3 = 0; num3 < list2.Count; num3++)
		{
			if (list.Contains(num3))
			{
				for (int num4 = 0; num4 < 4; num4++)
				{
					for (int num5 = 1; num5 < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt - 1; num5++)
					{
						list2[num3].ChangeSmallGrid(num4, 1, -1, num5, false);
						list2[num3].ChangeSmallGridItemKind(num5, num4, 0, -1);
						list2[num3].ChangeSmallGridItemKind(num5 - 1, num4, 6, -1);
						list2[num3].ChangeSmallGridColor(num5, num4);
					}
				}
			}
		}
	}

	// Token: 0x06007C5E RID: 31838 RVA: 0x0034EAAC File Offset: 0x0034CEAC
	private bool WallDireCheck(float floorHeight)
	{
		bool flag = false;
		int num = 0;
		while (num < Singleton<CraftCommandListBaseObject>.Instance.BaseParts.Length && !flag)
		{
			int num2 = 0;
			while (num2 < Singleton<CraftCommandListBaseObject>.Instance.BaseParts[num].Count && !flag)
			{
				int itemKind = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[num][num2].GetItemKind();
				if (itemKind == 4 || itemKind == 6)
				{
					List<GameObject> list = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[num][num2].GetList();
					int num3 = 0;
					while (num3 < list.Count && !flag)
					{
						if (list[num3].activeSelf && list[num3].GetComponent<BuildPartsInfo>().nPutFloor != -1)
						{
							if (list[num3].transform.position.x == this.carsol.transform.position.x && list[num3].transform.position.y == (float)(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt * 5) + floorHeight && list[num3].transform.position.z == this.carsol.transform.position.z && list[num3].transform.rotation.eulerAngles == this.carsol.transform.rotation.eulerAngles)
							{
								flag = true;
							}
						}
						num3++;
					}
				}
				num2++;
			}
			num++;
		}
		return flag;
	}

	// Token: 0x06007C5F RID: 31839 RVA: 0x0034EC84 File Offset: 0x0034D084
	private void DecideDelParts(BuildPartsPool pool, ref List<GameObject> tmptarget, ref List<int> tmptargetID, ref List<BuildPartsPool> tmptargetpool, int mode = 0)
	{
		List<GameObject> list = pool.GetList();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].activeSelf)
			{
				BuildPartsInfo component = list[i].GetComponent<BuildPartsInfo>();
				if (mode == 1)
				{
					if (component.nPutFloor != Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt - 1)
					{
						goto IL_107;
					}
				}
				else if (component.nPutFloor != Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)
				{
					goto IL_107;
				}
				if (list[i].transform.localPosition.x == this.carsol.transform.position.x && list[i].transform.localPosition.z == this.carsol.transform.position.z)
				{
					tmptarget.Add(list[i]);
					tmptargetID.Add(i);
					tmptargetpool.Add(pool);
				}
			}
			IL_107:;
		}
	}

	// Token: 0x06007C60 RID: 31840 RVA: 0x0034EDA8 File Offset: 0x0034D1A8
	private void CarsolUnderCheck()
	{
		this.CarsolUnderGridList.Clear();
		this.CarsolUnderSmallGridIDList.Clear();
		for (int i = 0; i < this.GridList.Count; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				this.BaseGridInfo[i].ChangeSmallGridUnderCarsol(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, j, false);
			}
		}
		List<RaycastHit[]> hitsObjInfo = this.carsol.CheckCarsol(this.carsol.transform.rotation, this.carsol.transform.rotation, this.nPartsForm, ref this.BoxCastRange, null);
		List<GridInfo> list = new List<GridInfo>();
		List<int> list2 = new List<int>();
		this.GridCheck(hitsObjInfo, this.GridList, list, list2);
		for (int k = 0; k < list.Count; k++)
		{
			list[k].ChangeSmallGridUnderCarsol(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, list2[k], true);
		}
		List<GridInfo> list3 = list.ToList<GridInfo>();
		for (int l = 0; l < list3.Count; l++)
		{
			this.CarsolUnderGridList.Add(list3[l].gameObject);
			this.CarsolUnderSmallGridIDList.Add(list2[l]);
		}
	}

	// Token: 0x06007C61 RID: 31841 RVA: 0x0034EF08 File Offset: 0x0034D308
	private void TmpGridActiveUpdate()
	{
		if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt > 0)
		{
			for (int i = 0; i < this.AllGridNum; i++)
			{
				Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt][i] = this.BaseGridInfo[i].GetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
			}
		}
	}

	// Token: 0x06007C62 RID: 31842 RVA: 0x0034EF74 File Offset: 0x0034D374
	private void FloatingObjGrid(List<GameObject> gridList)
	{
		if (Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm].Count == 0)
		{
			return;
		}
		if (Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][this.nID].GetItemKind() == 14)
		{
			Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveListUpdate[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] = true;
			for (int i = 0; i < gridList.Count; i++)
			{
				gridList[i].SetActive(true);
				GridInfo gridInfo = this.BaseGridInfo[i];
				for (int j = 0; j < 4; j++)
				{
					gridInfo.ChangeSmallGridColor(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, j);
				}
			}
		}
		else
		{
			for (int k = 0; k < gridList.Count; k++)
			{
				gridList[k].SetActive(Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt][k]);
			}
			Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveListUpdate[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] = false;
		}
	}

	// Token: 0x06007C63 RID: 31843 RVA: 0x0034F090 File Offset: 0x0034D490
	public void SelectBuldPart()
	{
		int itemKind = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm][this.nID].GetItemKind();
		if (itemKind == 1 || itemKind == 2 || itemKind == 5 || itemKind == 6 || itemKind == 16)
		{
			return;
		}
		this.SelectPutGridInfo = new CraftCommandList.ChangeValGrid[this.AllGridNum];
		for (int i = 0; i < this.AllGridNum; i++)
		{
			this.SelectPutGridInfo[i] = new CraftCommandList.ChangeValGrid();
		}
		this.URPutPartsSetG(ref this.SelectPutGridInfo, 0);
		this.SelectPutMaxFloorCnt[0] = Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt;
		int i2 = (from v in this.CarsolUnderGridList
		select v.GetComponent<GridInfo>().nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt]).Max();
		List<BuildPartsPool>[] baseParts = Singleton<CraftCommandListBaseObject>.Instance.BaseParts;
		List<List<GameObject>> list = new List<List<GameObject>>();
		for (int j = 0; j < baseParts.Length; j++)
		{
			for (int k = 0; k < baseParts[j].Count; k++)
			{
				list.Add(baseParts[j][k].GetList());
			}
		}
		List<UnityEx.ValueTuple<GameObject, List<GridInfo>, List<int>>> list2 = new List<UnityEx.ValueTuple<GameObject, List<GridInfo>, List<int>>>();
		for (int l = 0; l < list.Count; l++)
		{
			for (int m = 0; m < list[l].Count; m++)
			{
				if (this.partsMgr.GetBuildPartsInfo(list[l][m]).nPutFloor == Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)
				{
					list2.Add(new UnityEx.ValueTuple<GameObject, List<GridInfo>, List<int>>(list[l][m], this.partsMgr.GetBuildPartsInfo(list[l][m]).putGridInfos, this.partsMgr.GetBuildPartsInfo(list[l][m]).putSmallGridInfos));
				}
			}
		}
		for (int n2 = 0; n2 < this.CarsolUnderGridList.Count; n2++)
		{
			for (int num = 0; num < list2.Count; num++)
			{
				for (int num2 = 0; num2 < list2[num].Item2.Count; num2++)
				{
					if (!(this.CarsolUnderGridList[n2] != list2[num].Item2[num2].gameObject))
					{
						if (this.CarsolUnderSmallGridIDList[n2] == list2[num].Item3[num2])
						{
							this.SelectObj.Add(new UnityEx.ValueTuple<GameObject, int, float, GameObject>(list2[num].Item1, i2, list2[num].Item1.transform.position.y, list2[num].Item1.transform.parent.gameObject));
							break;
						}
					}
				}
			}
		}
		this.SelectObj = this.SelectObj.Distinct<UnityEx.ValueTuple<GameObject, int, float, GameObject>>().ToList<UnityEx.ValueTuple<GameObject, int, float, GameObject>>();
		this.SelectObj = (from n in this.SelectObj
		orderby n.Item1.transform.position.y descending
		select n).ToList<UnityEx.ValueTuple<GameObject, int, float, GameObject>>();
		GameObject gameObject = null;
		for (int num3 = 0; num3 < this.SelectObj.Count; num3++)
		{
			BuildPartsInfo buildPartsInfo = this.partsMgr.GetBuildPartsInfo(this.SelectObj[num3].Item1);
			if (buildPartsInfo.GetInfo(2) == this.nPartsForm && buildPartsInfo.GetInfo(1) == this.nID)
			{
				if (!Mathf.Approximately(this.SelectObj[num3].Item1.transform.position.x, this.carsol.transform.position.x) || !Mathf.Approximately(this.SelectObj[num3].Item1.transform.position.z, this.carsol.transform.position.z))
				{
					break;
				}
				if (this.SelectObj[num3].Item1.transform.rotation.y == this.carsol.transform.rotation.y)
				{
					if (gameObject == null)
					{
						gameObject = this.SelectObj[num3].Item1;
					}
					else if (gameObject.transform.position.y > this.SelectObj[num3].Item1.transform.position.y)
					{
						gameObject = this.SelectObj[num3].Item1;
					}
				}
			}
		}
		if (gameObject == null)
		{
			this.SelectObj.Clear();
			return;
		}
		List<UnityEx.ValueTuple<GameObject, int, float, GameObject>> list3 = new List<UnityEx.ValueTuple<GameObject, int, float, GameObject>>();
		if (itemKind == 10 || itemKind == 13)
		{
			list3.Clear();
			for (int num4 = 0; num4 < this.SelectObj.Count; num4++)
			{
				if (this.partsMgr.GetBuildPartsInfo(this.SelectObj[num4].Item1).GetInfo(3) != itemKind)
				{
					list3.Add(this.SelectObj[num4]);
				}
			}
		}
		else
		{
			for (int num5 = 0; num5 < this.SelectObj.Count; num5++)
			{
				int info = this.partsMgr.GetBuildPartsInfo(this.SelectObj[num5].Item1).GetInfo(3);
				if (gameObject.transform.position.y > this.SelectObj[num5].Item1.transform.position.y || info == 1 || info == 5)
				{
					list3.Add(this.SelectObj[num5]);
				}
			}
		}
		for (int num6 = 0; num6 < list3.Count; num6++)
		{
			this.SelectObj.Remove(list3[num6]);
		}
		if (this.SelectObj.Count == 0)
		{
			return;
		}
		this.SelectObj = (from n in this.SelectObj
		orderby n.Item1.transform.position.y
		select n).ToList<UnityEx.ValueTuple<GameObject, int, float, GameObject>>();
		List<Tuple<GridInfo, int>> list4 = new List<Tuple<GridInfo, int>>();
		for (int num7 = 0; num7 < this.SelectObj.Count; num7++)
		{
			List<GridInfo> putGridInfos = this.partsMgr.GetBuildPartsInfo(this.SelectObj[num7].Item1).putGridInfos;
			List<int> putSmallGridInfos = this.partsMgr.GetBuildPartsInfo(this.SelectObj[num7].Item1).putSmallGridInfos;
			for (int num8 = 0; num8 < putGridInfos.Count; num8++)
			{
				Tuple<GridInfo, int> tuple = new Tuple<GridInfo, int>(putGridInfos[num8], putSmallGridInfos[num8]);
				bool flag = false;
				for (int num9 = 0; num9 < list4.Count; num9++)
				{
					if (list4[num9].Item1.nID == tuple.Item1.nID && list4[num9].Item2 == tuple.Item2)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					if (num7 > 0)
					{
						this.SelectObj.Clear();
						return;
					}
					list4.Add(tuple);
				}
			}
		}
		float num10 = this.carsol.transform.position.y - this.SelectObj[0].Item1.transform.position.y;
		Vector3 localPosition = default(Vector3);
		this.carsol.SelectModeCarsolUnvisible();
		for (int num11 = 0; num11 < this.SelectObj.Count; num11++)
		{
			this.SelectObj[num11].Item1.transform.SetParent(this.carsol.transform);
			localPosition = this.SelectObj[num11].Item1.transform.localPosition;
			localPosition.y += num10 - this.fGridSize / 2f;
			this.SelectObj[num11].Item1.transform.localPosition = localPosition;
		}
		localPosition = this.carsol.cursoldir.transform.localPosition;
		localPosition.y = 0f;
		localPosition.y += num10;
		this.carsol.cursoldir.transform.localPosition = localPosition;
		for (int num12 = 0; num12 < this.SelectObj.Count; num12++)
		{
			BuildPartsInfo buildPartsInfo2 = this.partsMgr.GetBuildPartsInfo(this.SelectObj[num12].Item1);
			List<GridInfo> putGridInfos2 = buildPartsInfo2.putGridInfos;
			List<int> putSmallGridInfos2 = buildPartsInfo2.putSmallGridInfos;
			for (int num13 = 0; num13 < putGridInfos2.Count; num13++)
			{
				int info2 = this.partsMgr.GetBuildPartsInfo(this.SelectObj[num12].Item1).GetInfo(3);
				switch (info2)
				{
				case 12:
				case 13:
					putGridInfos2[num13].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, putSmallGridInfos2[num13], 4, -1);
					break;
				case 14:
					putGridInfos2[num13].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, putSmallGridInfos2[num13], 5, -1);
					break;
				case 15:
					putGridInfos2[num13].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, putSmallGridInfos2[num13], 2, -1);
					break;
				default:
					if (info2 != 3)
					{
						if (info2 != 4)
						{
							if (this.partsMgr.BuildPartPoolDic[buildPartsInfo2.GetInfo(2)][buildPartsInfo2.GetInfo(1)].Item2.PutFlag != 0)
							{
								putGridInfos2[num13].ChangeSmallGridStack(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, putSmallGridInfos2[num13], 8, 1);
							}
							else
							{
								putGridInfos2[num13].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, putSmallGridInfos2[num13], 2, -1);
							}
						}
						else
						{
							putGridInfos2[num13].ChangeSmallGridStack(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, putSmallGridInfos2[num13], 4, 1);
						}
					}
					else
					{
						putGridInfos2[num13].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, putSmallGridInfos2[num13], 1, -1);
					}
					break;
				}
				putGridInfos2[num13].SetSmallGridPutElement(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, putSmallGridInfos2[num13], -1, false, false);
			}
		}
		for (int num14 = 0; num14 < list4.Count; num14++)
		{
			int[] partOnSmallGrid = list4[num14].Item1.GetPartOnSmallGrid(list4[num14].Item2, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
			int[] stackWallOnSmallGrid = list4[num14].Item1.GetStackWallOnSmallGrid(list4[num14].Item2, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
			int[] stackPartsOnSmallGrid = list4[num14].Item1.GetStackPartsOnSmallGrid(list4[num14].Item2, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
			int num15 = 0;
			int num16 = 0;
			int num17 = 0;
			for (int num18 = 0; num18 < partOnSmallGrid.Length; num18++)
			{
				if (partOnSmallGrid[num18] != -1)
				{
					num15++;
				}
			}
			for (int num19 = 0; num19 < stackWallOnSmallGrid.Length; num19++)
			{
				if (stackWallOnSmallGrid[num19] == -1)
				{
					break;
				}
				num16++;
			}
			for (int num20 = 0; num20 < stackPartsOnSmallGrid.Length; num20++)
			{
				if (stackPartsOnSmallGrid[num20] == -1)
				{
					break;
				}
				num17++;
			}
			if (num15 + num16 + num17 == 0)
			{
				list4[num14].Item1.ChangeSmallGrid(list4[num14].Item2, 0, -1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
			}
			else
			{
				list4[num14].Item1.ChangeSmallGrid(list4[num14].Item2, 2, -1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
			}
			list4[num14].Item1.ChangeSmallGridColor(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, list4[num14].Item2);
		}
		this.SelectPutPartInfo = new List<CraftCommandList.ChangeValParts>();
		for (int num21 = 0; num21 < this.SelectObj.Count; num21++)
		{
			this.SelectPutPartInfo.Add(new CraftCommandList.ChangeValParts());
			BuildPartsInfo buildPartsInfo3 = this.partsMgr.GetBuildPartsInfo(this.SelectObj[num21].Item1);
			this.SelectPutPartInfo[this.SelectPutPartInfo.Count - 1].nFormKind = buildPartsInfo3.GetInfo(2);
			this.SelectPutPartInfo[this.SelectPutPartInfo.Count - 1].nPoolID = buildPartsInfo3.GetInfo(1);
			this.SelectPutPartInfo[this.SelectPutPartInfo.Count - 1].nItemID = buildPartsInfo3.GetInfo(0);
			this.URPutPartsSetB(this.SelectPutPartInfo[this.SelectPutPartInfo.Count - 1], this.SelectObj[num21].Item1, 0);
		}
	}

	// Token: 0x06007C64 RID: 31844 RVA: 0x0034FF6C File Offset: 0x0034E36C
	private bool SelectPut()
	{
		this.SelectPutAutoPartInfo = new List<CraftCommandList.ChangeValParts>();
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.SelectObj.Count; i++)
		{
			int info = this.partsMgr.GetBuildPartsInfo(this.SelectObj[i].Item1).GetInfo(3);
			if (info == 3 || info == 4 || info == 15)
			{
				list.Add(this.SelectObj[i].Item1);
			}
		}
		List<DeleatWall> list2 = new List<DeleatWall>();
		List<GridInfo> list3 = new List<GridInfo>();
		List<int> list4 = new List<int>();
		if (!this.GridPutCheck(this.GridList, this.carsol, this.SelectObj[0].Item1, ref list2, ref list3, ref list4))
		{
			return false;
		}
		this.FloorPartsDel(this.SelectObj[0].Item1, list3, this.SelectPutAutoPartInfo);
		this.ChangeGridInfo(this.SelectObj[0].Item1, list3, list4, list4.Count, this.SelectPutAutoPartInfo);
		this.partsMgr.GetBuildPartsInfo(this.SelectObj[0].Item1).putGridInfos.Clear();
		this.partsMgr.GetBuildPartsInfo(this.SelectObj[0].Item1).putSmallGridInfos.Clear();
		for (int j = 0; j < list3.Count; j++)
		{
			this.partsMgr.GetBuildPartsInfo(this.SelectObj[0].Item1).putGridInfos.Add(list3[j]);
			this.partsMgr.GetBuildPartsInfo(this.SelectObj[0].Item1).putSmallGridInfos.Add(list4[j]);
		}
		this.gridMap.ChangeCraftMap(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		this.gridMap.CraftMapSearchRoom(this.GridList, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		GridInfo.ChangeGridInfo(this.BaseGridInfo, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
		int num = this.PillarOnGridNum();
		if (this.gridMap.CraftMapRoofDecide() || (this.partsMgr.GetBuildPartsInfo(this.SelectObj[0].Item1).GetInfo(3) == 15 && num == 1))
		{
			this.FloorUP(this.SelectPutAutoPartInfo);
		}
		Vector3 position = this.carsol.transform.position;
		for (int k = 0; k < this.SelectObj.Count; k++)
		{
			if (k != 0)
			{
				list3.Clear();
				list4.Clear();
				this.GridCheck(this.GridList, this.carsol, list3, list4, this.carsol.transform.rotation, this.partsMgr.GetBuildPartsInfo(this.SelectObj[k].Item1).GetInfo(2), new Vector3?(this.SelectObj[k].Item1.transform.position));
				this.ChangeGridInfo(this.SelectObj[k].Item1, list3, list4, list4.Count, this.SelectPutAutoPartInfo);
				this.partsMgr.GetBuildPartsInfo(this.SelectObj[k].Item1).putGridInfos.Clear();
				this.partsMgr.GetBuildPartsInfo(this.SelectObj[k].Item1).putSmallGridInfos.Clear();
				for (int l = 0; l < list3.Count; l++)
				{
					this.partsMgr.GetBuildPartsInfo(this.SelectObj[k].Item1).putGridInfos.Add(list3[l]);
					this.partsMgr.GetBuildPartsInfo(this.SelectObj[k].Item1).putSmallGridInfos.Add(list4[l]);
				}
			}
			position.y = this.SelectObj[k].Item1.transform.position.y - this.fGridSize / 2f;
			if (position.y >= (float)(Singleton<CraftCommandListBaseObject>.Instance.MaxPutHeight[0] + 5 * Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 5))
			{
				BuildPartsInfo component = this.SelectObj[k].Item1.GetComponent<BuildPartsInfo>();
				this.DedPartsGridChange(component.putGridInfos.Count, component.putGridInfos, component.putSmallGridInfos, component.GetInfo(3), component.nHeight);
				this.SelectObj[k].Item1.SetActive(false);
				component.nPutFloor = -1;
				Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.SelectPutPartInfo[k].nFormKind][this.SelectPutPartInfo[k].nPoolID].ReserveListDel(this.SelectPutPartInfo[k].nItemID, 0);
				this.nPutPartsNum--;
			}
			else
			{
				this.SelectObj[k].Item1.transform.SetParent(this.SelectObj[k].Item4.transform);
			}
			this.URPutPartsSetB(this.SelectPutPartInfo[k], this.SelectObj[k].Item1, 1);
		}
		if (list.Count > 0)
		{
			CraftItemInfo item = this.partsMgr.BuildPartPoolDic[0][0].Item2;
			List<GameObject> list5 = this.partsMgr.BuildPartPoolDic[0][0].Item1.GetList();
			List<GameObject> list6 = this.CarsolUnderGridList.Distinct<GameObject>().ToList<GameObject>();
			for (int m = 0; m < list6.Count; m++)
			{
				GridInfo component2 = list6[m].GetComponent<GridInfo>();
				if (component2.GetPartOnSmallGrid(0, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)[0] == 1)
				{
					bool flag = true;
					for (int n = 0; n < list5.Count; n++)
					{
						if (list5[n].activeSelf)
						{
							if (list5[n].GetComponent<BuildPartsInfo>().nPutFloor == Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)
							{
								if (list5[n].transform.position.x == component2.transform.position.x && list5[n].transform.position.z == component2.transform.position.z)
								{
									flag = false;
									break;
								}
							}
						}
					}
					if (flag)
					{
						int nItemID = -1;
						GameObject gameObject = this.partsMgr.BuildPartPoolDic[0][0].Item1.Get(ref nItemID);
						this.SelectPutAutoPartInfo.Add(new CraftCommandList.ChangeValParts());
						this.SelectPutAutoPartInfo[this.SelectPutAutoPartInfo.Count - 1].nFormKind = 0;
						this.SelectPutAutoPartInfo[this.SelectPutAutoPartInfo.Count - 1].nPoolID = 0;
						this.SelectPutAutoPartInfo[this.SelectPutAutoPartInfo.Count - 1].nItemID = nItemID;
						this.URPutPartsSetB(this.SelectPutAutoPartInfo[this.SelectPutAutoPartInfo.Count - 1], gameObject, 0);
						this.SetParts(component2.transform, this.carsol.transform.rotation, gameObject, item.Horizontal, item.Vertical, 1);
						gameObject.SetActive(true);
						this.URPutPartsSetB(this.SelectPutAutoPartInfo[this.SelectPutAutoPartInfo.Count - 1], gameObject, 1);
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06007C65 RID: 31845 RVA: 0x00350810 File Offset: 0x0034EC10
	private void URPutPartsSetG(ref CraftCommandList.ChangeValGrid[] Val, int mode)
	{
		for (int i = 0; i < this.GridList.Count; i++)
		{
			Val[i].nFloorNum[mode] = this.BaseGridInfo[i].GetFloorNum();
			Val[i].Pos[mode] = this.GridList[i].transform.position;
			Renderer[] componentsInChildren = this.GridList[i].GetComponentsInChildren<Renderer>();
			for (int j = 0; j < this.BaseGridInfo[i].GetFloorNum(); j++)
			{
				Val[i].smallGrids[mode].Add(new SmallGrid[4]);
				Val[i].colors[mode].Add(new Color[4]);
				for (int k = 0; k < 4; k++)
				{
					Val[i].smallGrids[mode][j][k].m_canRoof = this.BaseGridInfo[i].GetSmallGridCanRoof(k, j);
					Val[i].smallGrids[mode][j][k].m_inRoom = this.BaseGridInfo[i].GetSmallGridInRoom(k, j);
					Val[i].smallGrids[mode][j][k].m_state = this.BaseGridInfo[i].GetStateSmallGrid(k, j);
					Val[i].smallGrids[mode][j][k].m_itemkind = new int[7];
					for (int l = 0; l < 7; l++)
					{
						Val[i].smallGrids[mode][j][k].m_itemkind[l] = this.BaseGridInfo[i].GetPartOnSmallGrid(k, j)[l];
					}
					Val[i].smallGrids[mode][j][k].m_itemstackwall = new int[GridInfo.nSmallGridStackWallMax];
					Val[i].smallGrids[mode][j][k].m_itemdupulication = new int[GridInfo.nSmallGridStackWallMax];
					int num = 0;
					int[] stackWallOnSmallGrid = this.BaseGridInfo[i].GetStackWallOnSmallGrid(k, j);
					for (int m = 0; m < stackWallOnSmallGrid.Length; m++)
					{
						if (stackWallOnSmallGrid[m] == -1)
						{
							break;
						}
						num++;
					}
					for (int n = 0; n < Val[i].smallGrids[mode][j][k].m_itemstackwall.Length; n++)
					{
						if (n < num)
						{
							Val[i].smallGrids[mode][j][k].m_itemstackwall[n] = 4;
						}
						else
						{
							Val[i].smallGrids[mode][j][k].m_itemstackwall[n] = -1;
						}
					}
					int num2 = 0;
					int[] stackPartsOnSmallGrid = this.BaseGridInfo[i].GetStackPartsOnSmallGrid(k, j);
					for (int num3 = 0; num3 < stackPartsOnSmallGrid.Length; num3++)
					{
						if (stackPartsOnSmallGrid[num3] == -1)
						{
							break;
						}
						num2++;
					}
					for (int num4 = 0; num4 < Val[i].smallGrids[mode][j][k].m_itemdupulication.Length; num4++)
					{
						if (num4 < num2)
						{
							Val[i].smallGrids[mode][j][k].m_itemdupulication[num4] = stackPartsOnSmallGrid[num4];
						}
						else
						{
							Val[i].smallGrids[mode][j][k].m_itemdupulication[num4] = -1;
						}
					}
					Val[i].smallGrids[mode][j][k].m_color = componentsInChildren[k];
					Val[i].colors[mode][j][k] = this.BaseGridInfo[i].GetSmallGridColor(j, k);
					Val[i].smallGrids[mode][j][k].m_UnderCarsol = this.BaseGridInfo[i].GetUnderTheCarsol(j, k);
					Val[i].smallGrids[mode][j][k].m_PutElement = this.BaseGridInfo[i].GetSmallGridPutElement(j, k);
				}
				Val[i].bUse[mode].Add(this.BaseGridInfo[i].GetUseState(j));
				Val[i].nFloorPartsHeight[mode].Add(this.BaseGridInfo[i].nFloorPartsHeight[j]);
				Val[i].nCanRoof[mode].Add(this.BaseGridInfo[i].GetCanRoofState(j));
				Val[i].nInRoom[mode].Add(this.BaseGridInfo[i].GetInRoomState(j));
			}
		}
	}

	// Token: 0x06007C66 RID: 31846 RVA: 0x00350D74 File Offset: 0x0034F174
	private void URPutPartsSetB(CraftCommandList.ChangeValParts Val, GameObject parts, int mode)
	{
		Val.active[mode] = parts.activeSelf;
		Val.Pos[mode] = parts.transform.position;
		Val.Rot[mode] = parts.transform.rotation;
		BuildPartsInfo buildPartsInfo = this.partsMgr.GetBuildPartsInfo(parts);
		Val.nPutFloor[mode] = buildPartsInfo.nPutFloor;
		Val.nDirection[mode] = buildPartsInfo.GetInfo(4);
		if (Singleton<CraftCommandListBaseObject>.Instance.BaseParts[buildPartsInfo.GetInfo(2)][buildPartsInfo.GetInfo(1)].ReserveListCheck(buildPartsInfo.GetInfo(0)))
		{
			Val.ReserveList[mode] = buildPartsInfo.GetInfo(0);
		}
		else
		{
			Val.ReserveList[mode] = -1;
		}
	}

	// Token: 0x06007C67 RID: 31847 RVA: 0x00350E40 File Offset: 0x0034F240
	public void ChangeParts()
	{
		if (Singleton<CraftCommandListBaseObject>.Instance.BaseParts[this.nPartsForm].Count != 0)
		{
			this.craftSelectPartsInfo.szItemName = this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.Name;
			this.carsol.SetMoveLimit(this.partsMgr.BuildPartPoolDic[this.nPartsForm][this.nID].Item2.MoveVal);
		}
		else
		{
			this.craftSelectPartsInfo.szItemName = "Non";
			this.carsol.SetMoveLimit(0.5f);
		}
		this.CarsolUnderCheck();
		this.FloatingObjGrid(this.GridList);
		this.PutHeight = 0;
	}

	// Token: 0x06007C68 RID: 31848 RVA: 0x00350F14 File Offset: 0x0034F314
	private int PillarOnGridNum()
	{
		List<int[]> list = new List<int[]>();
		for (int i = 0; i < this.GridList.Count; i++)
		{
			list.Add(this.BaseGridInfo[i].GetPartOnSmallGrid(0, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt));
		}
		int num = 0;
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j][2] == 15)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06007C69 RID: 31849 RVA: 0x00350F94 File Offset: 0x0034F394
	private void ChangeItemBox(int tmpID)
	{
		if (!this.craftItem.isActive)
		{
			this.craftItem.isActive = true;
			this.craftItem.ChangeList(tmpID + 1);
		}
		else
		{
			this.craftItem.ChangeList(tmpID + 1);
		}
		this.craftItemButtonUI.isActive = false;
	}

	// Token: 0x06007C6A RID: 31850 RVA: 0x00350FEC File Offset: 0x0034F3EC
	public void Undo()
	{
		Singleton<UndoRedoMgr>.Instance.Undo();
		if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt >= Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt)
		{
			Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt %= Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt;
		}
		this.TargetFloorUp();
		this.CarsolUnderCheck();
	}

	// Token: 0x06007C6B RID: 31851 RVA: 0x00351044 File Offset: 0x0034F444
	public void Redo()
	{
		Singleton<UndoRedoMgr>.Instance.Redo();
		if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt >= Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt)
		{
			Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt %= Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt;
		}
		this.TargetFloorUp();
		this.CarsolUnderCheck();
	}

	// Token: 0x06007C6C RID: 31852 RVA: 0x0035109B File Offset: 0x0034F49B
	public void OpelateFloorUp()
	{
		Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt++;
		Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt %= Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt;
		this.TargetFloorUp();
	}

	// Token: 0x06007C6D RID: 31853 RVA: 0x003510D0 File Offset: 0x0034F4D0
	private void CarsolOffset()
	{
		int num = 0;
		List<GameObject> list = this.CarsolUnderGridList.Distinct<GameObject>().ToList<GameObject>();
		for (int i = 0; i < list.Count; i++)
		{
			int num2 = list[i].GetComponent<GridInfo>().nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt];
			if (num2 > num)
			{
				num = num2;
			}
		}
		int num3 = 0;
		for (int j = 0; j < list.Count; j++)
		{
			int[] array = list[j].GetComponent<GridInfo>().GetStackWallOnSmallGrid(0, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
			int num4 = 0;
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k] == -1)
				{
					break;
				}
				num4++;
			}
			if (num3 < num4)
			{
				num3 = num4;
			}
		}
		int num5 = 0;
		for (int l = 0; l < this.CarsolUnderGridList.Count; l++)
		{
			int[] array = this.CarsolUnderGridList[l].GetComponent<GridInfo>().GetStackPartsOnSmallGrid(this.CarsolUnderSmallGridIDList[l], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
			int num6 = 0;
			for (int m = 0; m < array.Length; m++)
			{
				if (array[m] == -1)
				{
					break;
				}
				num6++;
			}
			if (num5 < num6)
			{
				num5 = num6;
			}
		}
		Vector3 position = this.carsol.transform.position;
		if (list.Count > 0)
		{
			position.y = list[0].transform.position.y + this.fGridSize / 2f;
			position.y += (float)num;
			position.y += (float)num3;
			position.y += (float)num5;
		}
		this.carsol.transform.position = position;
	}

	// Token: 0x06007C6E RID: 31854 RVA: 0x003512DC File Offset: 0x0034F6DC
	private void FloorHeightMove(int moveval, float targetPartsHeight, List<CraftCommandList.ChangeValParts> AutoParts)
	{
		List<BuildPartsPool>[] baseParts = Singleton<CraftCommandListBaseObject>.Instance.BaseParts;
		List<GameObject> list = new List<GameObject>();
		Vector3 position = default(Vector3);
		for (int i = 0; i < baseParts.Length; i++)
		{
			for (int j = 0; j < baseParts[i].Count; j++)
			{
				list = baseParts[i][j].GetList();
				for (int k = 0; k < list.Count; k++)
				{
					BuildPartsInfo component = list[k].GetComponent<BuildPartsInfo>();
					if (component.nPutFloor != -1)
					{
						if (list[k].transform.position.y < targetPartsHeight)
						{
							if (list[k].transform.position.y >= targetPartsHeight + (float)moveval)
							{
								this.DedPartsGridChange(component.putGridInfos.Count, component.putGridInfos, component.putSmallGridInfos, component.GetInfo(3), component.nHeight);
								AutoParts.Add(new CraftCommandList.ChangeValParts());
								AutoParts[AutoParts.Count - 1].nFormKind = i;
								AutoParts[AutoParts.Count - 1].nPoolID = j;
								AutoParts[AutoParts.Count - 1].nItemID = k;
								this.URPutPartsSetB(AutoParts[AutoParts.Count - 1], list[k], 0);
								list[k].SetActive(false);
								component.nPutFloor = -1;
								baseParts[i][j].ReserveListDel(k, 0);
								this.URPutPartsSetB(AutoParts[AutoParts.Count - 1], list[k], 1);
								this.nPutPartsNum--;
							}
						}
						else
						{
							AutoParts.Add(new CraftCommandList.ChangeValParts());
							AutoParts[AutoParts.Count - 1].nFormKind = i;
							AutoParts[AutoParts.Count - 1].nPoolID = j;
							AutoParts[AutoParts.Count - 1].nItemID = k;
							this.URPutPartsSetB(AutoParts[AutoParts.Count - 1], list[k], 0);
							position = list[k].transform.position;
							position.y += (float)moveval;
							list[k].transform.position = position;
							this.URPutPartsSetB(AutoParts[AutoParts.Count - 1], list[k], 1);
						}
					}
				}
			}
		}
	}

	// Token: 0x06007C6F RID: 31855 RVA: 0x00351574 File Offset: 0x0034F974
	private void DedPartsGridChange(int tmpCnt, List<GridInfo> tmpGridInfo, List<int> tmpSmallGridCount, int TargetKind, int height)
	{
		for (int i = 0; i < tmpCnt; i++)
		{
			int[] partOnSmallGrid = tmpGridInfo[i].GetPartOnSmallGrid(tmpSmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
			int[] stackWallOnSmallGrid = tmpGridInfo[i].GetStackWallOnSmallGrid(tmpSmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
			int[] stackPartsOnSmallGrid = tmpGridInfo[i].GetStackPartsOnSmallGrid(tmpSmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int j = 0; j < partOnSmallGrid.Length; j++)
			{
				if (partOnSmallGrid[j] != -1)
				{
					num++;
				}
			}
			for (int k = 0; k < stackWallOnSmallGrid.Length; k++)
			{
				if (stackWallOnSmallGrid[k] == -1)
				{
					break;
				}
				num2++;
			}
			for (int l = 0; l < stackPartsOnSmallGrid.Length; l++)
			{
				if (stackPartsOnSmallGrid[l] == -1)
				{
					break;
				}
				num3++;
			}
			if (num + num2 + num3 - height < 1)
			{
				tmpGridInfo[i].ChangeSmallGrid(tmpSmallGridCount[i], 0, -1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
				tmpGridInfo[i].nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] = 0;
				if (TargetKind == 9 && Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1 < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt)
				{
					tmpGridInfo[i].ChangeSmallGrid(tmpSmallGridCount[i], 0, -1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, false);
				}
				if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt > 0 && TargetKind == 14)
				{
					tmpGridInfo[i].SetUseState(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
					Singleton<CraftCommandListBaseObject>.Instance.tmpGridActiveList[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt][tmpGridInfo[i].nID] = false;
				}
			}
			else
			{
				switch (TargetKind)
				{
				case 1:
					tmpGridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 0, -1);
					goto IL_5F4;
				case 2:
					tmpGridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 0, -1);
					tmpGridInfo[i].nFloorPartsHeight[Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt] = 0;
					goto IL_5F4;
				case 3:
				case 4:
					if (TargetKind == 3)
					{
						tmpGridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 1, -1);
					}
					else
					{
						for (int m = 0; m < height; m++)
						{
							tmpGridInfo[i].ChangeSmallGridStack(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 4, 1);
						}
					}
					tmpGridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 3, -1);
					stackWallOnSmallGrid = tmpGridInfo[i].GetStackWallOnSmallGrid(tmpSmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
					num2 = 0;
					for (int n = 0; n < stackWallOnSmallGrid.Length; n++)
					{
						if (stackWallOnSmallGrid[n] == -1)
						{
							break;
						}
						num2++;
					}
					if (partOnSmallGrid[1] == -1 && num2 == 0)
					{
						tmpGridInfo[i].ChangeSmallGrid(tmpSmallGridCount[i], 2, partOnSmallGrid[0], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
					}
					goto IL_5F4;
				case 5:
					tmpGridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 6, -1);
					goto IL_5F4;
				case 6:
					tmpGridInfo[i].ChangeSmallGridStack(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 4, -1);
					tmpGridInfo[i].ChangeSmallGrid(tmpSmallGridCount[i], 2, 1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
					tmpGridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 2, -1);
					goto IL_5F4;
				case 10:
					tmpGridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 3, -1);
					goto IL_5F4;
				case 12:
				case 13:
					tmpGridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 4, -1);
					goto IL_5F4;
				case 14:
					tmpGridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 5, -1);
					goto IL_5F4;
				}
				if (tmpGridInfo[i].GetStateSmallGrid(tmpSmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) != 1)
				{
					for (int num4 = 0; num4 < height; num4++)
					{
						tmpGridInfo[i].ChangeSmallGridStack(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], TargetKind, 1);
					}
				}
				else
				{
					tmpGridInfo[i].ChangeSmallGridItemKind(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], 2, -1);
					tmpGridInfo[i].ChangeSmallGrid(tmpSmallGridCount[i], 2, -1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, false);
				}
				if (TargetKind == 9)
				{
					if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1 < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt)
					{
						tmpGridInfo[i].ChangeSmallGrid(tmpSmallGridCount[i], 0, -1, Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt + 1, false);
					}
				}
				else if (TargetKind == 11)
				{
					for (int num5 = 1; num5 < Singleton<CraftCommandListBaseObject>.Instance.nMaxFloorCnt - 1; num5++)
					{
						tmpGridInfo[i].ChangeSmallGrid(tmpSmallGridCount[i], 0, -1, num5, false);
					}
				}
			}
			IL_5F4:
			if (TargetKind == 7)
			{
				for (int num6 = 0; num6 <= Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt; num6++)
				{
					tmpGridInfo[i].SetInRoomSmallGrid(tmpSmallGridCount[i], false, num6);
				}
			}
			tmpGridInfo[i].ChangeSmallGridColor(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i]);
			tmpGridInfo[i].SetCanRoofSmallGrid(tmpSmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, 0);
			if (TargetKind != 1)
			{
				tmpGridInfo[i].SetSmallGridPutElement(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], -1, true, false);
			}
			else
			{
				tmpGridInfo[i].SetSmallGridPutElement(Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt, tmpSmallGridCount[i], -1, false, false);
			}
		}
	}

	// Token: 0x06007C70 RID: 31856 RVA: 0x00351C54 File Offset: 0x00350054
	private bool DelConditionCheck(int GridCnt, List<GridInfo> GridInfo, List<int> SmallGridCount, int TargetKind, GameObject targetObj)
	{
		bool flag = false;
		List<BuildPartsPool>[] baseParts = Singleton<CraftCommandListBaseObject>.Instance.BaseParts;
		List<List<GameObject>> list = new List<List<GameObject>>();
		List<BuildPartsInfo> list2 = new List<BuildPartsInfo>();
		for (int i = 0; i < GridCnt; i++)
		{
			if (GridInfo[i].GetStateSmallGrid(SmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 0)
			{
				return false;
			}
			if (TargetKind == 1 || TargetKind == 2)
			{
				if (SmallGridCount[i] == 0)
				{
					int[] partOnSmallGrid;
					if (Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt > 0)
					{
						partOnSmallGrid = GridInfo[i].GetPartOnSmallGrid(SmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt - 1);
						int[] stackWallOnSmallGrid = GridInfo[i].GetStackWallOnSmallGrid(SmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt - 1);
						for (int j = 0; j < stackWallOnSmallGrid.Length; j++)
						{
							if (stackWallOnSmallGrid[j] == 4)
							{
								flag = true;
								break;
							}
						}
						if (partOnSmallGrid[1] == 3 || flag)
						{
							return false;
						}
					}
					if (GridInfo[i].GetStateSmallGrid(SmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt) == 1)
					{
						return false;
					}
					partOnSmallGrid = GridInfo[i].GetPartOnSmallGrid(SmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
					if (partOnSmallGrid[2] != -1)
					{
						return false;
					}
				}
			}
			else if (TargetKind == 5)
			{
				int[] partOnSmallGrid = GridInfo[i].GetPartOnSmallGrid(SmallGridCount[i], Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt);
				if (partOnSmallGrid[4] != -1)
				{
					return false;
				}
			}
			else
			{
				for (int k = 0; k < baseParts.Length; k++)
				{
					for (int l = 0; l < baseParts[k].Count; l++)
					{
						int itemKind = baseParts[k][l].GetItemKind();
						if (itemKind == 8)
						{
							list.Add(baseParts[k][l].GetList());
						}
					}
				}
				for (int m = 0; m < list.Count; m++)
				{
					for (int n = 0; n < list[m].Count; n++)
					{
						BuildPartsInfo buildPartsInfo = this.partsMgr.GetBuildPartsInfo(list[m][n]);
						if (buildPartsInfo.nPutFloor == Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt)
						{
							list2.Add(buildPartsInfo);
						}
					}
				}
				for (int num = 0; num < list2.Count; num++)
				{
					for (int num2 = 0; num2 < list2[num].putGridInfos.Count; num2++)
					{
						if (!(list2[num].putGridInfos[num2].gameObject != GridInfo[i].gameObject))
						{
							if (list2[num].putSmallGridInfos[num2] == SmallGridCount[i])
							{
								if (targetObj.transform.position.y < list2[num].gameObject.transform.position.y)
								{
									return false;
								}
							}
						}
					}
				}
			}
		}
		return true;
	}

	// Token: 0x0400641D RID: 25629
	public GameObject GridPart;

	// Token: 0x0400641E RID: 25630
	public Transform CreateGridPlace;

	// Token: 0x0400641F RID: 25631
	public BuildPartsMgr partsMgr;

	// Token: 0x04006420 RID: 25632
	public Transform CraftSet;

	// Token: 0x04006421 RID: 25633
	public Carsol carsol;

	// Token: 0x04006422 RID: 25634
	public Button AllReset;

	// Token: 0x04006423 RID: 25635
	public Toggle CamLock;

	// Token: 0x04006424 RID: 25636
	public Toggle SelectMode;

	// Token: 0x04006425 RID: 25637
	public int AreaNumX;

	// Token: 0x04006426 RID: 25638
	public int AreaNumY;

	// Token: 0x04006427 RID: 25639
	public int AreaNumZ;

	// Token: 0x04006428 RID: 25640
	public GameObject FloorUpTex;

	// Token: 0x04006429 RID: 25641
	public CraftCamera Cam;

	// Token: 0x0400642A RID: 25642
	public CraftItemButtonUI craftItemButtonUI;

	// Token: 0x0400642B RID: 25643
	public CraftInfomationUI craftInfomationUI;

	// Token: 0x0400642C RID: 25644
	public CraftItemBox craftItem;

	// Token: 0x0400642D RID: 25645
	public CraftSelectPartsInfo craftSelectPartsInfo;

	// Token: 0x0400642E RID: 25646
	public bool bFloorLimit;

	// Token: 0x0400642F RID: 25647
	public bool bOparate;

	// Token: 0x04006430 RID: 25648
	private Manager.Input input;

	// Token: 0x04006431 RID: 25649
	private List<GridInfo> BaseGridInfo;

	// Token: 0x04006432 RID: 25650
	private BoxRange BoxCastRange;

	// Token: 0x04006433 RID: 25651
	private GridPool Gridpool;

	// Token: 0x04006434 RID: 25652
	private List<GameObject> GridList;

	// Token: 0x04006435 RID: 25653
	private List<GameObject> CarsolUnderGridList = new List<GameObject>();

	// Token: 0x04006436 RID: 25654
	private List<int> CarsolUnderSmallGridIDList = new List<int>();

	// Token: 0x04006437 RID: 25655
	private int AllGridNum;

	// Token: 0x04006438 RID: 25656
	private Vector3[] vGridPos;

	// Token: 0x04006439 RID: 25657
	private Vector3 MinPos;

	// Token: 0x0400643A RID: 25658
	private float fGridSize;

	// Token: 0x0400643B RID: 25659
	private GridMap gridMap;

	// Token: 0x0400643C RID: 25660
	private CraftSave save;

	// Token: 0x0400643D RID: 25661
	private int nCarsolMode;

	// Token: 0x0400643E RID: 25662
	private bool ViewMode;

	// Token: 0x0400643F RID: 25663
	private bool prevViewMode;

	// Token: 0x04006440 RID: 25664
	private List<UnityEx.ValueTuple<GameObject, int, float, GameObject>> SelectObj = new List<UnityEx.ValueTuple<GameObject, int, float, GameObject>>();

	// Token: 0x04006441 RID: 25665
	private int nPartsForm;

	// Token: 0x04006442 RID: 25666
	private int nID;

	// Token: 0x04006443 RID: 25667
	private float fMoveCarsolTimelLimiter;

	// Token: 0x04006444 RID: 25668
	private float fFloorUpTexExist;

	// Token: 0x04006445 RID: 25669
	private CraftCommandList.ChangeValGrid[] SelectPutGridInfo;

	// Token: 0x04006446 RID: 25670
	private List<CraftCommandList.ChangeValParts> SelectPutPartInfo = new List<CraftCommandList.ChangeValParts>();

	// Token: 0x04006447 RID: 25671
	private List<CraftCommandList.ChangeValParts> SelectPutAutoPartInfo = new List<CraftCommandList.ChangeValParts>();

	// Token: 0x04006448 RID: 25672
	private int[] SelectPutMaxFloorCnt = new int[2];

	// Token: 0x04006449 RID: 25673
	private int PutHeight;

	// Token: 0x0400644A RID: 25674
	private const int MAX_FLOOR_NUM = 3;

	// Token: 0x0400644B RID: 25675
	private const int PreGridMaxNum = 100;

	// Token: 0x0400644C RID: 25676
	public const int FloorHeight = 5;
}
