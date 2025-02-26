using System;
using System.Collections.Generic;
using AIProject.Animal;
using AIProject.Definitions;
using AIProject.Player;
using IllusionUtility.SetUtility;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000FB9 RID: 4025
	public class MiniMapControler : MonoBehaviour
	{
		// Token: 0x060085DA RID: 34266 RVA: 0x0037BA88 File Offset: 0x00379E88
		public void Init(int MapID = -1)
		{
			this.input = Singleton<Manager.Input>.Instance;
			this.Player = Singleton<Manager.Map>.Instance.Player;
			this.allAreaMapUI = MapUIContainer.AllAreaMapUI;
			if (this.MinimapUIDefine == null)
			{
				this.MinimapUIDefine = Singleton<Manager.Resources>.Instance.DefinePack.MinimapUIDefine;
			}
			this.areaGroupTable = Singleton<Manager.Resources>.Instance.Map.AreaGroupTable;
			this.AllAreaMapObjects = this.allAreaMapUI.GetComponent<AllAreaMapObjects>();
			this.ShowMiniMapArea = MapUIContainer.MiniMapRenderTex;
			this.ShowAllMapArea = this.AllAreaMapObjects.ShowAllMapArea;
			this.AllAreaMapCanvasGroup = this.AllAreaMapObjects.AllAreaMapCanvasGroup;
			this.AllAreaMapCanvasGroupTrans = this.AllAreaMapCanvasGroup.transform;
			this.SetMiniMapCamera();
			this.MiniMap.GetComponent<Camera>().RemoveAllCommandBuffers();
			this.MiniMap.GetComponent<MiniMapDepthTexture>().Initialize();
			Dictionary<int, MiniMapControler.MinimapInfo> minimapInfoTable = Singleton<Manager.Resources>.Instance.Map.MinimapInfoTable;
			int num;
			if (MapID < 0)
			{
				num = Singleton<Manager.Map>.Instance.MapID;
			}
			else
			{
				num = MapID;
			}
			if (this.Roads == null)
			{
				this.Roads = new Dictionary<int, GameObject>();
			}
			if (this.commonSpace == null)
			{
				this.commonSpace = GameObject.Find("CommonSpace");
			}
			Transform transform = this.commonSpace.transform;
			MiniMapControler.MinimapInfo minimapInfo;
			if (minimapInfoTable.TryGetValue(num, out minimapInfo) && GlobalMethod.AssetFileExist(minimapInfo.assetPath, minimapInfo.asset, minimapInfo.manifest))
			{
				if (!this.Roads.ContainsKey(num))
				{
					GameObject gameObject = CommonLib.LoadAsset<GameObject>(minimapInfo.assetPath, minimapInfo.asset, true, minimapInfo.manifest);
					gameObject.transform.SetParent(transform);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.identity;
					this.Roads.Add(num, gameObject);
					AssetBundleManager.UnloadAssetBundle(minimapInfo.assetPath, false, minimapInfo.manifest, false);
				}
				else if (this.Roads[num] == null)
				{
					this.Roads[num] = CommonLib.LoadAsset<GameObject>(minimapInfo.assetPath, minimapInfo.asset, true, minimapInfo.manifest);
					this.Roads[num].transform.SetParent(transform);
					this.Roads[num].transform.localPosition = Vector3.zero;
					this.Roads[num].transform.localRotation = Quaternion.identity;
					AssetBundleManager.UnloadAssetBundle(minimapInfo.assetPath, false, minimapInfo.manifest, false);
				}
			}
			if (this.Roads.TryGetValue(num, out this.Road) && this.Road != null)
			{
				this.Road.transform.SetLocalPositionY(this.fOffSetY);
				this.RoadNaviMesh = this.Road.GetComponent<MinimapNavimesh>();
				this.RoadNaviMesh.Init();
			}
			else if (num == 0 && this.DefRoad != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.DefRoad, transform);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
				gameObject2.transform.SetLocalPositionY(this.fOffSetY);
				this.RoadNaviMesh = gameObject2.GetComponent<MinimapNavimesh>();
				this.RoadNaviMesh.Init();
				this.Roads.Add(num, gameObject2);
			}
			foreach (KeyValuePair<int, GameObject> keyValuePair in this.Roads)
			{
				if (!(keyValuePair.Value == null))
				{
					if (keyValuePair.Key - num != 0)
					{
						keyValuePair.Value.SetActive(false);
					}
					else
					{
						keyValuePair.Value.SetActive(true);
					}
				}
			}
			this.playerMask = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.PlayerEventMask;
			this.playerMask = (this.playerMask | EventType.Toilet | EventType.Bath | EventType.Wash | EventType.Eat | EventType.DressIn | EventType.Play | EventType.Lesbian);
			if (this.PlayerActionEvents != null)
			{
				this.PlayerActionEvents.Clear();
			}
			for (int i = 0; i < 24; i++)
			{
				if ((this.playerMask >> (i & 31) & EventType.Sleep) == EventType.Sleep)
				{
					this.PlayerActionEvents.Add((EventType)(1 << i));
				}
			}
			this.actionPoints = Singleton<Manager.Map>.Instance.PointAgent.ActionPoints;
			this.basePoints = Singleton<Manager.Map>.Instance.PointAgent.BasePoints;
			this.devicePoints = Singleton<Manager.Map>.Instance.PointAgent.DevicePoints;
			this.farmPoints = Singleton<Manager.Map>.Instance.PointAgent.FarmPoints;
			this.actionPointsHousing = Singleton<Manager.Housing>.Instance.ActionPoints;
			this.farmPointsHousing = Singleton<Manager.Housing>.Instance.FarmPoints;
			this.eventPoints = new List<EventPoint>(Singleton<Manager.Map>.Instance.PointAgent.EventPoints);
			this.TutorialSearchIcons = new List<MiniMapControler.TutorialSearchIconInfo>();
			this.shipPoints = Singleton<Manager.Map>.Instance.PointAgent.ShipPoints;
			this.craftPoints = Singleton<Manager.Housing>.Instance.CraftPoints;
			this.jukePoints = Singleton<Manager.Housing>.Instance.JukePoints;
			this.SetActionPointIcons();
			this.SetBasePointIcons();
			this.SetDevicePointIcons();
			this.SetFarmPointIcons();
			this.SetEventPointIcons();
			this.SetActionHousingIcons();
			this.SetFarmHousingIcons();
			this.SetShipPointIcons();
			this.SetCraftPointIcons();
			this.SetJukePointIcons();
			this.icon = this.PlayerIconArea.transform;
			this.TrajectoryPool = new TrajectoryPool();
			this.TrajectoryPool.CreatePool(this.Trajectory, 3, this.icon);
			this.trajePool = this.TrajectoryPool.getList();
			for (int j = 0; j < 3; j++)
			{
				int index = j;
				this.trajePool[index].GetComponent<Trajectory>().Init(this.TrajectoryExistTime);
			}
			this.tmpAgentNullCheckTable.Clear();
			foreach (KeyValuePair<int, AgentActor> keyValuePair2 in Singleton<Manager.Map>.Instance.AgentTable)
			{
				this.tmpAgentNullCheckTable.Add(keyValuePair2.Key, keyValuePair2.Value == null);
			}
			this.GirlIconInit(this.icon, this.trajePool);
			this.icon = this.MerchantIconArea.transform;
			this.MerchantIcon.gameObject.SetActive(Singleton<Manager.Map>.Instance.Merchant.CurrentMode != Merchant.ActionType.Absent);
			if (this.TrajectoryMerchant != null)
			{
				this.TrajectoryMerchantPool = new TrajectoryPool();
				this.TrajectoryMerchantPool.CreatePool(this.TrajectoryMerchant, 3, this.icon);
				this.trajePool = this.TrajectoryMerchantPool.getList();
				for (int k = 0; k < 3; k++)
				{
					int index2 = k;
					this.trajePool[index2].GetComponent<Trajectory>().Init(this.TrajectoryExistTime);
				}
			}
			this.PetIconInit(this.icon, this.trajePool);
			this.SetLastPos();
			this.IconObj = null;
			this.fTimer = 0f;
			this.PlayerLookAreaWidth = this.PlayerlookArea.GetComponent<RectTransform>().sizeDelta.y;
			this.MiniMap.GetComponent<MiniMapCameraMove>().Init();
			this.SetAllMapCamera();
			this.allArea = this.AllAreaMap.GetComponent<AllAreaCameraControler>();
			this.allArea.Init();
			this.allArea.SetCameraCommandBuffer();
			this.allAreaMapUI.Init(this, this.AllAreaMap);
			this.endInit = true;
		}

		// Token: 0x060085DB RID: 34267 RVA: 0x0037C278 File Offset: 0x0037A678
		public void MinimapLockAreaInit()
		{
			if (this.LockIcons != null)
			{
				foreach (Canvas canvas in this.LockIcons)
				{
					if (!(canvas.gameObject == null))
					{
						UnityEngine.Object.Destroy(canvas.gameObject);
					}
				}
				this.LockIcons.Clear();
			}
			GameObject tutorialLockAreaObject = Singleton<Manager.Map>.Instance.TutorialLockAreaObject;
			if (tutorialLockAreaObject != null)
			{
				LockArea[] componentsInChildren = tutorialLockAreaObject.GetComponentsInChildren<LockArea>();
				if (componentsInChildren != null)
				{
					this.LockIcons = new List<Canvas>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						Canvas canvas2 = UnityEngine.Object.Instantiate<Canvas>(this.LockIcon, this.LockIconArea.transform);
						Vector3 position = componentsInChildren[i].transform.position;
						position.y += this.fOffSetY;
						canvas2.transform.position = position;
						Quaternion identity = Quaternion.identity;
						identity.eulerAngles = new Vector3(90f, 0f, 0f);
						canvas2.transform.rotation = identity;
						this.LockIcons.Add(canvas2);
					}
				}
				this.TutorialLockRelease = false;
			}
			else
			{
				this.TutorialLockRelease = true;
			}
		}

		// Token: 0x060085DC RID: 34268 RVA: 0x0037C3E8 File Offset: 0x0037A7E8
		public void MinimapTutorialActionPointInit(TutorialSearchActionPoint point, int iconID)
		{
			Sprite sprite = null;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(iconID, out sprite);
			if (sprite == null)
			{
				return;
			}
			Canvas canvas = UnityEngine.Object.Instantiate<Canvas>(this.TutorialSearchIcon, this.TutorialSearchIconArea.transform);
			canvas.GetComponentInChildren<Image>().sprite = sprite;
			Vector3 position = point.transform.position;
			position.y += this.fOffSetY;
			canvas.transform.position = position;
			Quaternion identity = Quaternion.identity;
			identity.eulerAngles = new Vector3(90f, 180f, 0f);
			canvas.transform.rotation = identity;
			if (!canvas.gameObject.activeSelf)
			{
				canvas.gameObject.SetActive(true);
			}
			int num = -1;
			for (int i = 0; i < this.TutorialSearchIcons.Count; i++)
			{
				if (!(this.TutorialSearchIcons[i].Point != point))
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				this.TutorialSearchIcons.Add(new MiniMapControler.TutorialSearchIconInfo(canvas, point));
			}
			else
			{
				this.TutorialSearchIcons[num] = new MiniMapControler.TutorialSearchIconInfo(canvas, point);
			}
		}

		// Token: 0x060085DD RID: 34269 RVA: 0x0037C538 File Offset: 0x0037A938
		public void MinimapTutorialActionPointDestroy(TutorialSearchActionPoint point)
		{
			if (this.TutorialSearchIcons == null || this.TutorialSearchIcons.Count == 0)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < this.TutorialSearchIcons.Count; i++)
			{
				if (this.TutorialSearchIcons[i] != null && !(this.TutorialSearchIcons[i].Point != point))
				{
					num = i;
					break;
				}
			}
			if (num == -1 || this.TutorialSearchIcons[num] == null || this.TutorialSearchIcons[num].Icon == null)
			{
				return;
			}
			UnityEngine.Object.Destroy(this.TutorialSearchIcons[num].Icon.gameObject);
			this.TutorialSearchIcons.RemoveAt(num);
		}

		// Token: 0x060085DE RID: 34270 RVA: 0x0037C614 File Offset: 0x0037AA14
		private void GirlIconInit(Transform icon, List<GameObject> trajePool)
		{
			icon = this.GirlIconArea.transform;
			this.nGirlCount = 0;
			foreach (AgentActor x in Singleton<Manager.Map>.Instance.AgentTable.Values)
			{
				if (!(x == null))
				{
					this.nGirlCount++;
				}
			}
			if (this.GirlIcons != null && this.GirlIcons.Count > 0)
			{
				for (int i = 0; i < this.GirlIcons.Count; i++)
				{
					UnityEngine.Object.Destroy(this.GirlIcons[i].gameObject);
					this.GirlIcons[i] = null;
				}
			}
			if (this.TrajectoryGirlPool != null && this.TrajectoryGirlPool.Length > 0)
			{
				for (int j = 0; j < this.TrajectoryGirlPool.Length; j++)
				{
					List<GameObject> list = this.TrajectoryGirlPool[j].getList();
					for (int k = 0; k < list.Count; k++)
					{
						UnityEngine.Object.Destroy(list[k].gameObject);
						list[k] = null;
					}
				}
			}
			this.GirlIcons = new List<Canvas>();
			this.TrajectoryGirlPool = new TrajectoryPool[this.nGirlCount];
			this.sortedDic = this.SortGirlDictionary();
			if (this.sortedDic != null)
			{
				foreach (KeyValuePair<int, AgentActor> keyValuePair in this.sortedDic)
				{
					if (!(keyValuePair.Value == null))
					{
						int count = this.GirlIcons.Count;
						this.GirlIcons.Add(UnityEngine.Object.Instantiate<Canvas>(this.GirlIcon));
						Image componentInChildren = this.GirlIcons[count].GetComponentInChildren<Image>();
						componentInChildren.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActorIconTable[keyValuePair.Value.ID];
						this.GirlIcons[count].transform.SetParent(icon, false);
						this.GirlIcons[count].transform.localScale = Vector3.one;
						this.GirlIcons[count].gameObject.SetActive(true);
						this.TrajectoryGirlPool[count] = new TrajectoryPool();
						this.TrajectoryGirlPool[count].CreatePool(this.TrajectoryGirl[keyValuePair.Key], 3, icon);
						trajePool = this.TrajectoryGirlPool[count].getList();
						for (int l = 0; l < 3; l++)
						{
							int index = l;
							trajePool[index].GetComponent<Trajectory>().Init(this.TrajectoryExistTime);
						}
					}
				}
			}
		}

		// Token: 0x060085DF RID: 34271 RVA: 0x0037C938 File Offset: 0x0037AD38
		private void PetIconInit(Transform icon, List<GameObject> trajePool)
		{
			icon = this.PetIconArea.transform;
			List<AnimalBase> petAnimals = Singleton<AnimalManager>.Instance.PetAnimals;
			if (this.PetIcons != null && this.PetIcons.Count > 0)
			{
				for (int i = 0; i < this.PetIcons.Count; i++)
				{
					this.PetIcons[i].Icon.gameObject.SetActive(false);
					UnityEngine.Object.Destroy(this.PetIcons[i].Icon.gameObject);
					this.PetIcons[i].Icon = null;
				}
			}
			if (this.TrajectoryPetPool != null && this.TrajectoryPetPool.Count > 0)
			{
				for (int j = 0; j < this.TrajectoryPetPool.Count; j++)
				{
					List<GameObject> list = this.TrajectoryPetPool[j].getList();
					for (int k = 0; k < list.Count; k++)
					{
						UnityEngine.Object.Destroy(list[k].gameObject);
						list[k] = null;
					}
				}
			}
			this.nPetCount = 0;
			this.Pets.Clear();
			foreach (AnimalBase animalBase in petAnimals)
			{
				if (animalBase.gameObject.activeSelf)
				{
					if (animalBase.AnimalTypeID == 0)
					{
						this.Pets.Add(animalBase);
						this.nPetCount++;
					}
				}
			}
			this.PetIcons = new List<MiniMapControler.PetIconInfo>();
			this.TrajectoryPetPool = new List<TrajectoryPool>();
			for (int l = 0; l < this.nPetCount; l++)
			{
				this.PetIcons.Add(new MiniMapControler.PetIconInfo(UnityEngine.Object.Instantiate<Canvas>(this.PetIcon), this.Pets[l].Name, this.Pets[l].gameObject));
				this.PetIcons[l].Icon.transform.SetParent(icon);
				this.PetIcons[l].Icon.transform.localScale = Vector3.one;
				this.PetIcons[l].Icon.gameObject.SetActive(true);
				Image componentInChildren = this.PetIcons[l].Icon.GetComponentInChildren<Image>();
				Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Category, 9, componentInChildren, true);
				this.TrajectoryPetPool.Add(new TrajectoryPool());
				this.TrajectoryPetPool[l].CreatePool(this.TrajectoryPet, 3, icon);
				trajePool = this.TrajectoryPetPool[l].getList();
				for (int m = 0; m < 3; m++)
				{
					int index = m;
					trajePool[index].GetComponent<Trajectory>().Init(this.TrajectoryExistTime);
				}
			}
			if (this.allArea != null)
			{
				this.allArea.SetPetPointIcon();
			}
		}

		// Token: 0x060085E0 RID: 34272 RVA: 0x0037CC78 File Offset: 0x0037B078
		public void PetIconSet()
		{
			this.PetIconInit(this.icon, this.trajePool);
			this.SetLastPos();
		}

		// Token: 0x060085E1 RID: 34273 RVA: 0x0037CC94 File Offset: 0x0037B094
		private void SetLastPos()
		{
			this.LastPos = new Dictionary<int, List<Vector3>>();
			this.LastPos.Add(0, new List<Vector3>());
			if (this.PlayerIcon != null)
			{
				Vector3 position = this.PlayerIcon.transform.position;
				this.LastPos[0].Add(position);
			}
			this.LastPos.Add(1, new List<Vector3>());
			if (this.GirlIcons != null)
			{
				for (int i = 0; i < this.GirlIcons.Count; i++)
				{
					this.LastPos[1].Add(this.GirlIcons[i].transform.position);
				}
			}
			this.LastPos.Add(2, new List<Vector3>());
			if (this.MerchantIcon != null)
			{
				this.LastPos[2].Add(this.MerchantIcon.transform.position);
			}
			this.LastPos.Add(3, new List<Vector3>());
			for (int j = 0; j < this.nPetCount; j++)
			{
				if (!(this.PetIcons[j].Icon == null))
				{
					this.LastPos[3].Add(this.PetIcons[j].Icon.transform.position);
				}
			}
		}

		// Token: 0x060085E2 RID: 34274 RVA: 0x0037CE08 File Offset: 0x0037B208
		private void Update()
		{
			if (!Singleton<Manager.Map>.IsInstance() || !this.endInit)
			{
				return;
			}
			if (this._prevMinimapConfig != Config.GameData.MiniMap)
			{
				this._prevMinimapConfig = Config.GameData.MiniMap;
				if (Config.GameData.MiniMap)
				{
					if (!this.MiniMap.activeSelf && this.VisibleMode == 0)
					{
						this.OpenMiniMap();
					}
				}
				else if (!Config.GameData.MiniMap)
				{
					this._visibleModeMinimapConfigOFF = this.VisibleMode;
					if (this.MiniMap.activeSelf)
					{
						this.CloseMiniMap();
					}
				}
			}
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			if (player != null)
			{
				this.IconMove(this.PlayerIcon, player.Position, player.Rotation, false);
				if (this.MiniMap.activeSelf)
				{
					this.LookAreaMove(player);
				}
			}
			bool flag = false;
			if (this.tmpAgentNullCheckTable.Count != Singleton<Manager.Map>.Instance.AgentTable.Count)
			{
				flag = true;
			}
			else
			{
				foreach (KeyValuePair<int, AgentActor> keyValuePair in Singleton<Manager.Map>.Instance.AgentTable)
				{
					if (!this.tmpAgentNullCheckTable.ContainsKey(keyValuePair.Key))
					{
						flag = true;
						break;
					}
					if (this.tmpAgentNullCheckTable[keyValuePair.Key] != (keyValuePair.Value == null))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				this.GirlIconInit(this.icon, this.trajePool);
				this.SetLastPos();
				this.tmpAgentNullCheckTable.Clear();
				foreach (KeyValuePair<int, AgentActor> keyValuePair2 in Singleton<Manager.Map>.Instance.AgentTable)
				{
					this.tmpAgentNullCheckTable.Add(keyValuePair2.Key, keyValuePair2.Value == null);
				}
			}
			int num = 0;
			this.CheckRefleshGirls();
			if (this.sortedDic != null)
			{
				foreach (AgentActor agentActor in this.sortedDic.Values)
				{
					if (!(agentActor == null))
					{
						Vector3 position = agentActor.Position;
						position.y = player.Position.y;
						this.IconMove(this.GirlIcons[num++], position, agentActor.Rotation, true);
					}
				}
			}
			MerchantActor merchant = Singleton<Manager.Map>.Instance.Merchant;
			if (merchant != null)
			{
				if (!this.TutorialLockRelease)
				{
					this.MerchantIcon.gameObject.SetActive(false);
				}
				else if (merchant.CurrentMode != Merchant.ActionType.Absent)
				{
					if (!this.MerchantIcon.gameObject.activeSelf)
					{
						this.MerchantIcon.gameObject.SetActive(true);
					}
					Vector3 position = merchant.Position;
					position.y = player.Position.y;
					this.IconMove(this.MerchantIcon, position, merchant.Rotation, true);
				}
				else
				{
					this.MerchantIcon.gameObject.SetActive(false);
				}
			}
			for (int i = 0; i < this.nPetCount; i++)
			{
				this.IconMove(this.PetIcons[i].Icon, this.Pets[i].Position, this.Pets[i].Rotation, true);
			}
			this.fTimer += Time.deltaTime;
			if (this.LastPos == null)
			{
				return;
			}
			if (this.fTimer >= this.PutTrajectoryTime)
			{
				this.SetTrajectory(this.LastPos[0][0], this.PlayerIcon, this.TrajectoryPool);
				Image[] girlsIcon = this.GetGirlsIcon();
				if (girlsIcon != null)
				{
					for (int j = 0; j < this.nGirlCount; j++)
					{
						if (girlsIcon[j].enabled)
						{
							this.SetTrajectory(this.LastPos[1][j], this.GirlIcons[j], this.TrajectoryGirlPool[j]);
						}
					}
				}
				if (this.MerchantIcon.gameObject.activeSelf && this.GetMerchantIcon().enabled)
				{
					this.SetTrajectory(this.LastPos[2][0], this.MerchantIcon, this.TrajectoryMerchantPool);
				}
				for (int k = 0; k < this.nPetCount; k++)
				{
					this.SetTrajectory(this.LastPos[3][k], this.PetIcons[k].Icon, this.TrajectoryPetPool[k]);
				}
				this.fTimer = 0f;
			}
			if (this.VisibleMode == 1 && this.allArea.isActiveAndEnabled && !this.nowCloseAllMap && (this.input.IsPressedKey(ActionID.MouseRight) || this.input.IsPressedKey(ActionID.Cancel)))
			{
				this.AllMapClosedAction = (System.Action)Delegate.Combine(this.AllMapClosedAction, new System.Action(delegate()
				{
					if (this.prevVisibleMode == 0 && Config.GameData.MiniMap)
					{
						this.OpenMiniMap();
					}
				}));
				this.ChangeCamera(true, false);
				this.WarpMoveDispose();
			}
			if (this.PlayerIcon != null)
			{
				this.LastPos[0][0] = this.PlayerIcon.transform.position;
			}
			for (int l = 0; l < this.nGirlCount; l++)
			{
				if (this.GirlIcons[l] != null)
				{
					this.LastPos[1][l] = this.GirlIcons[l].transform.position;
				}
			}
			if (this.MerchantIcon != null)
			{
				this.LastPos[2][0] = this.MerchantIcon.transform.position;
			}
			for (int m = 0; m < this.nPetCount; m++)
			{
				if (this.PetIcons[m] != null)
				{
					this.LastPos[3][m] = this.PetIcons[m].Icon.transform.position;
				}
			}
			this.IconVisibleChange();
			this.ClearCheck();
			if (!this.TutorialLockRelease && this.LockIcons != null)
			{
				if (Singleton<Manager.Map>.Instance.TutorialLockAreaObject == null)
				{
					for (int n = 0; n < this.LockIcons.Count; n++)
					{
						if (!(this.LockIcons[n] == null))
						{
							UnityEngine.Object.Destroy(this.LockIcons[n].gameObject);
							this.LockIcons[n] = null;
						}
					}
					this.LockIcons = null;
					this.TutorialLockRelease = true;
				}
			}
			else if (!this.TutorialLockRelease && this.LockIcons == null)
			{
				this.TutorialLockRelease = true;
			}
		}

		// Token: 0x060085E3 RID: 34275 RVA: 0x0037D5D4 File Offset: 0x0037B9D4
		private void IconVisibleChange()
		{
			int mapID = Singleton<Manager.Map>.Instance.MapID;
			Dictionary<int, MinimapNavimesh.AreaGroupInfo> areaGroupInfo = this.GetAreaGroupInfo(mapID);
			if (areaGroupInfo != null)
			{
				foreach (KeyValuePair<int, MinimapNavimesh.AreaGroupInfo> keyValuePair in areaGroupInfo)
				{
					for (int i = 0; i < this.actionPointIcon.Count; i++)
					{
						if (!(this.actionPointIcon[i].Point == null) && !(this.actionPointIcon[i].Point.gameObject == null))
						{
							bool flag = this.actionPointIcon[i].Point.gameObject.activeSelf;
							if (this.actionPointIcon[i].Point.OwnerArea == null)
							{
								this.actionPointIcon[i].Icon.SetActive(flag);
							}
							else
							{
								int areaID = this.actionPointIcon[i].Point.OwnerArea.AreaID;
								if (keyValuePair.Value.areaID.Contains(areaID))
								{
									flag &= this.RoadNaviMesh.areaGroupActive[keyValuePair.Key];
									flag &= !this.actionPointIcon[i].Point.TutorialHideMode();
									this.actionPointIcon[i].Icon.SetActive(flag);
								}
							}
						}
					}
					for (int j = 0; j < this.basePoints.Length; j++)
					{
						if (!(this.basePoints[j] == null) && !(this.basePoints[j].gameObject == null))
						{
							bool flag2 = this.basePoints[j].gameObject.activeSelf;
							if (this.basePoints[j].OwnerArea == null)
							{
								this.BaseIcons[j].Icon.gameObject.SetActive(flag2);
							}
							else
							{
								int areaID2 = this.basePoints[j].OwnerArea.AreaID;
								if (keyValuePair.Value.areaID.Contains(areaID2))
								{
									flag2 &= this.RoadNaviMesh.areaGroupActive[keyValuePair.Key];
									BasePoint component = this.BaseIcons[j].Point.GetComponent<BasePoint>();
									flag2 &= !component.TutorialHideMode();
									this.BaseIcons[j].Icon.gameObject.SetActive(flag2);
								}
							}
						}
					}
					for (int k = 0; k < this.devicePoints.Length; k++)
					{
						if (!(this.devicePoints[k] == null) && !(this.devicePoints[k].gameObject == null))
						{
							bool flag3 = this.devicePoints[k].gameObject.activeSelf;
							if (this.devicePoints[k].OwnerArea == null)
							{
								this.DeviceIcons[k].Icon.gameObject.SetActive(flag3);
							}
							else
							{
								int areaID3 = this.devicePoints[k].OwnerArea.AreaID;
								if (keyValuePair.Value.areaID.Contains(areaID3))
								{
									flag3 &= this.RoadNaviMesh.areaGroupActive[keyValuePair.Key];
									DevicePoint component2 = this.DeviceIcons[k].Point.GetComponent<DevicePoint>();
									flag3 &= !component2.TutorialHideMode();
									this.DeviceIcons[k].Icon.gameObject.SetActive(flag3);
								}
							}
						}
					}
					for (int l = 0; l < this.farmPoints.Length; l++)
					{
						if (!(this.farmPoints[l] == null) && !(this.farmPoints[l].gameObject == null))
						{
							bool flag4 = this.farmPoints[l].gameObject.activeSelf;
							if (this.farmPoints[l].OwnerArea == null)
							{
								this.FarmIcons[l].Icon.gameObject.SetActive(flag4);
							}
							else
							{
								int areaID4 = this.farmPoints[l].OwnerArea.AreaID;
								if (keyValuePair.Value.areaID.Contains(areaID4))
								{
									flag4 &= this.RoadNaviMesh.areaGroupActive[keyValuePair.Key];
									FarmPoint component3 = this.FarmIcons[l].Point.GetComponent<FarmPoint>();
									flag4 &= !component3.TutorialHideMode();
									this.FarmIcons[l].Icon.gameObject.SetActive(flag4);
								}
							}
						}
					}
					for (int m = 0; m < this.eventPoints.Count; m++)
					{
						if (!(this.eventPoints[m] == null) && !(this.eventPoints[m].gameObject == null))
						{
							bool flag5 = this.eventPoints[m].gameObject.activeSelf;
							if (this.eventPoints[m].OwnerArea == null)
							{
								this.EventIcons[m].Icon.gameObject.SetActive(flag5);
							}
							else
							{
								int areaID5 = this.eventPoints[m].OwnerArea.AreaID;
								if (keyValuePair.Value.areaID.Contains(areaID5))
								{
									flag5 &= this.RoadNaviMesh.areaGroupActive[keyValuePair.Key];
									EventPoint component4 = this.EventIcons[m].Point.GetComponent<EventPoint>();
									flag5 &= (component4.IsNeutralCommand && component4.TargetPoint);
									this.EventIcons[m].Icon.gameObject.SetActive(flag5);
								}
							}
						}
					}
					for (int n = 0; n < this.craftPoints.Length; n++)
					{
						if (!(this.craftPoints[n] == null) && !(this.craftPoints[n].gameObject == null))
						{
							bool flag6 = this.craftPoints[n].gameObject.activeSelf;
							if (this.craftPoints[n].OwnerArea == null)
							{
								this.CraftIcons[n].Icon.gameObject.SetActive(flag6);
							}
							else
							{
								int areaID6 = this.craftPoints[n].OwnerArea.AreaID;
								if (keyValuePair.Value.areaID.Contains(areaID6))
								{
									flag6 &= this.RoadNaviMesh.areaGroupActive[keyValuePair.Key];
									CraftPoint component5 = this.CraftIcons[n].Point.GetComponent<CraftPoint>();
									flag6 &= !component5.TutorialHideMode();
									this.CraftIcons[n].Icon.gameObject.SetActive(flag6);
								}
							}
						}
					}
					for (int num = 0; num < this.jukePoints.Length; num++)
					{
						if (!(this.jukePoints[num] == null) && !(this.jukePoints[num].gameObject == null))
						{
							bool flag7 = this.jukePoints[num].gameObject.activeSelf;
							if (this.jukePoints[num].OwnerArea == null)
							{
								this.JukeIcons[num].Icon.gameObject.SetActive(flag7);
							}
							else
							{
								int areaID7 = this.jukePoints[num].OwnerArea.AreaID;
								if (keyValuePair.Value.areaID.Contains(areaID7))
								{
									flag7 &= this.RoadNaviMesh.areaGroupActive[keyValuePair.Key];
									JukePoint component6 = this.JukeIcons[num].Point.GetComponent<JukePoint>();
									flag7 &= !Manager.Map.TutorialMode;
									this.JukeIcons[num].Icon.gameObject.SetActive(flag7);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060085E4 RID: 34276 RVA: 0x0037DF08 File Offset: 0x0037C308
		private void ClearCheck()
		{
			for (int i = 0; i < this.shipPoints.Length; i++)
			{
				if (!(this.shipPoints[i] == null) && !(this.shipPoints[i].gameObject == null))
				{
					bool flag = this.shipPoints[i].gameObject.activeSelf;
					this.allAreaMapUI.GameClear = Singleton<Game>.Instance.WorldData.Cleared;
					flag &= this.allAreaMapUI.GameClear;
					ShipPoint component = this.ShipIcons[i].Point.GetComponent<ShipPoint>();
					this.ShipIcons[i].Icon.gameObject.SetActive(flag);
				}
			}
		}

		// Token: 0x060085E5 RID: 34277 RVA: 0x0037DFD0 File Offset: 0x0037C3D0
		private void IconMove(Canvas Icon, Vector3 pos, Quaternion rot, bool ignoreRot = false)
		{
			Vector3 position = pos;
			Camera component = this.MiniMapIcon.GetComponent<Camera>();
			Camera component2 = this.AllAreaIconMap.GetComponent<Camera>();
			if (component != null && component.isActiveAndEnabled)
			{
				position.y = this.MiniMap.transform.position.y - 1f;
			}
			else if (component2 != null && component2.isActiveAndEnabled)
			{
				position.y = component2.transform.position.y - 1f;
			}
			Icon.transform.position = position;
			if (ignoreRot)
			{
				return;
			}
			Icon.transform.rotation = rot * Quaternion.LookRotation(new Vector3(0f, -1f, 0f));
		}

		// Token: 0x060085E6 RID: 34278 RVA: 0x0037E0B0 File Offset: 0x0037C4B0
		private void SetTrajectory(Vector3 lastPos, Canvas Target, TrajectoryPool pool)
		{
			this.CalcIconPos[0] = Target.transform.position;
			this.CalcIconPos[1] = lastPos;
			if (((this.CalcIconPos[0] - this.CalcIconPos[1]) / Time.deltaTime).sqrMagnitude < 0.5f)
			{
				return;
			}
			this.IconObj = pool.GetObject();
			if (this.IconObj.GetComponent<Trajectory>() == null)
			{
				this.IconObj.AddComponent<Trajectory>();
			}
			Vector3 position = Target.transform.position;
			position.y -= 1f;
			this.IconObj.GetComponent<Trajectory>().Set(position, Target.transform.rotation);
		}

		// Token: 0x060085E7 RID: 34279 RVA: 0x0037E198 File Offset: 0x0037C598
		private void DeadTrajectory(TrajectoryPool pool)
		{
			foreach (GameObject gameObject in pool.getList())
			{
				if (gameObject.activeSelf)
				{
					gameObject.SetActive(false);
					Trajectory component = gameObject.GetComponent<Trajectory>();
					if (component != null)
					{
						component.Dead();
					}
				}
			}
		}

		// Token: 0x060085E8 RID: 34280 RVA: 0x0037E218 File Offset: 0x0037C618
		public void SetMiniMapCamera()
		{
			if (!this.MiniMap.activeSelf && Config.GameData.MiniMap)
			{
				this.MiniMap.SetActive(true);
				this.VisibleMode = 0;
			}
			if (this.MiniRenderTex == null)
			{
				this.MiniRenderTex = new RenderTexture(256, 256, 24, RenderTextureFormat.ARGB32);
			}
			this.ShowMiniMapArea.SetActive(this.MiniMap.activeSelf);
			this.MiniMap.GetComponent<Camera>().targetTexture = this.MiniRenderTex;
			this.MiniMapIcon.GetComponent<Camera>().targetTexture = this.MiniRenderTex;
			this.ShowMiniMapArea.GetComponent<RawImage>().texture = this.MiniRenderTex;
		}

		// Token: 0x060085E9 RID: 34281 RVA: 0x0037E2D8 File Offset: 0x0037C6D8
		public void SetAllMapCamera()
		{
			if (this.AllAreaMap.activeSelf)
			{
				this.AllAreaMap.SetActive(false);
			}
			if (this.AllRenderTex == null)
			{
				this.AllRenderTex = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
			}
			this.ShowAllMapArea.SetActive(true);
			this.AllAreaMap.GetComponent<Camera>().targetTexture = this.AllRenderTex;
			this.AllAreaIconMap.GetComponent<Camera>().targetTexture = this.AllRenderTex;
			this.ShowAllMapArea.GetComponent<RawImage>().texture = this.AllRenderTex;
			this.ShowAllMapArea.SetActive(false);
		}

		// Token: 0x060085EA RID: 34282 RVA: 0x0037E384 File Offset: 0x0037C784
		public void DeadMiniMap()
		{
			this.MiniMap.SetActive(false);
			this.MiniMapIcon.SetActive(false);
			this.ShowMiniMapArea.SetActive(false);
			UnityEngine.Object.Destroy(this.MiniRenderTex);
			this.MiniRenderTex = null;
			this.AllAreaMap.SetActive(false);
			this.AllAreaIconMap.SetActive(false);
			this.ShowAllMapArea.SetActive(false);
			UnityEngine.Object.Destroy(this.AllRenderTex);
			this.AllRenderTex = null;
		}

		// Token: 0x060085EB RID: 34283 RVA: 0x0037E400 File Offset: 0x0037C800
		public void ChangeCamera(bool MouseRight = false, bool WarpUI = false)
		{
			bool flag = Singleton<Game>.Instance.Dialog != null;
			if (!this.AllAreaMap.activeSelf)
			{
				return;
			}
			if (!WarpUI && flag)
			{
				return;
			}
			this.nowCloseAllMap = true;
			this.allArea.InitPosition();
			this.allArea.ShowAllIcon();
			if (!this.FromHomeMenu)
			{
				this.input.ReserveState(Manager.Input.ValidType.Action);
				this.input.FocusLevel = 0;
				Singleton<Manager.Map>.Instance.Player.SetScheduledInteractionState(true);
				Singleton<Manager.Map>.Instance.Player.ReleaseInteraction();
			}
			if (!this.CheckEndDelegate("SetupState"))
			{
				this.AllMapClosedAction = (System.Action)Delegate.Combine(this.AllMapClosedAction, new System.Action(this.SetupState));
			}
			int from = 1;
			int to = 0;
			if (this.CamActivateSubscriber != null)
			{
				this.CamActivateSubscriber.Dispose();
			}
			this.CamActivateSubscriber = ObservableEasing.EaseOutQuint(0.3f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				float alpha = Mathf.Lerp((float)from, (float)to, x.Value);
				this.AllAreaMapCanvasGroup.alpha = alpha;
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				if (Config.GameData.MiniMap)
				{
					this.VisibleMode = 2;
				}
				else
				{
					this.VisibleMode = this._visibleModeMinimapConfigOFF;
				}
				if (this.AllMapClosedAction != null)
				{
					this.AllMapClosedAction();
				}
				this.AllMapClosedAction = null;
				this.AllAreaMap.SetActive(false);
				this.ShowAllMapArea.SetActive(false);
				MapUIContainer.SetVisibleHUD(true);
				this.nowCloseAllMap = false;
			});
			this.AllAreaMapCanvasGroup.blocksRaycasts = false;
			if (!this.FromHomeMenu)
			{
				this.Player.enabled = true;
				this.Player.CameraControl.enabled = true;
				if (!this.CheckEndDelegate("PlayerNomalChange"))
				{
					this.AllMapClosedAction = (System.Action)Delegate.Combine(this.AllMapClosedAction, new System.Action(this.PlayerNomalChange));
				}
			}
			this.DeadTrajectory(this.TrajectoryPool);
			for (int i = 0; i < this.TrajectoryGirlPool.Length; i++)
			{
				this.DeadTrajectory(this.TrajectoryGirlPool[i]);
			}
			this.DeadTrajectory(this.TrajectoryMerchantPool);
			for (int j = 0; j < this.TrajectoryPetPool.Count; j++)
			{
				this.DeadTrajectory(this.TrajectoryPetPool[j]);
			}
		}

		// Token: 0x060085EC RID: 34284 RVA: 0x0037E630 File Offset: 0x0037CA30
		public void OpenAllMap(int prevMode = -1)
		{
			this.allArea.InitPosition();
			this.AllAreaMap.SetActive(true);
			this.allArea.Restart();
			this.allAreaMapUI.Refresh();
			this.ShowAllMapArea.SetActive(true);
			this.prevVisibleMode = prevMode;
			CanvasGroup minimapCanvasGroup = this.ShowMiniMapArea.transform.parent.GetComponent<CanvasGroup>();
			if (this.MiniMap.activeSelf)
			{
				this.CloseMiniMap();
			}
			int from = 0;
			float fromMiniMap = minimapCanvasGroup.alpha;
			int to = 1;
			int toMiniMap = 0;
			if (this.CamActivateSubscriber != null)
			{
				this.CamActivateSubscriber.Dispose();
			}
			this.CamActivateSubscriber = ObservableEasing.EaseOutQuint(1f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				float alpha = Mathf.Lerp((float)from, (float)to, x.Value);
				this.AllAreaMapCanvasGroup.alpha = alpha;
				this.AllAreaMapObjects.Cursor.color = new Color(this.AllAreaMapObjects.Cursor.color.r, this.AllAreaMapObjects.Cursor.color.g, this.AllAreaMapObjects.Cursor.color.b, 0f);
				alpha = Mathf.Lerp(fromMiniMap, (float)toMiniMap, x.Value);
				minimapCanvasGroup.alpha = alpha;
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				this.allArea.CursorSet();
			});
			this.AllAreaMapCanvasGroup.blocksRaycasts = true;
			this.PlayerlookArea.gameObject.SetActive(false);
			this.DeadTrajectory(this.TrajectoryPool);
			for (int i = 0; i < this.TrajectoryGirlPool.Length; i++)
			{
				this.DeadTrajectory(this.TrajectoryGirlPool[i]);
			}
			this.DeadTrajectory(this.TrajectoryMerchantPool);
			for (int j = 0; j < this.TrajectoryPetPool.Count; j++)
			{
				this.DeadTrajectory(this.TrajectoryPetPool[j]);
			}
		}

		// Token: 0x060085ED RID: 34285 RVA: 0x0037E7D4 File Offset: 0x0037CBD4
		public void OpenMiniMap()
		{
			this.MiniMap.GetComponent<MiniMapCameraMove>().Init();
			this.MiniMap.SetActive(true);
			this.ShowMiniMapArea.SetActive(true);
			this.allArea.ShowAllIcon();
			this.prevVisibleMode = -1;
			this.VisibleMode = 0;
			CanvasGroup minimapCanvasGroup = this.ShowMiniMapArea.transform.parent.GetComponent<CanvasGroup>();
			float from = minimapCanvasGroup.alpha;
			int to = 1;
			if (this.CamActivateSubscriber != null)
			{
				this.CamActivateSubscriber.Dispose();
			}
			this.CamActivateSubscriber = ObservableEasing.EaseOutQuint(1f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				minimapCanvasGroup.alpha = Mathf.Lerp(from, (float)to, x.Value);
			}, delegate(Exception ex)
			{
			});
			this.PlayerlookArea.gameObject.SetActive(true);
			this.DeadTrajectory(this.TrajectoryPool);
			for (int i = 0; i < this.TrajectoryGirlPool.Length; i++)
			{
				this.DeadTrajectory(this.TrajectoryGirlPool[i]);
			}
			this.DeadTrajectory(this.TrajectoryMerchantPool);
			for (int j = 0; j < this.TrajectoryPetPool.Count; j++)
			{
				this.DeadTrajectory(this.TrajectoryPetPool[j]);
			}
		}

		// Token: 0x060085EE RID: 34286 RVA: 0x0037E938 File Offset: 0x0037CD38
		public void CloseMiniMap()
		{
			this.MiniMap.SetActive(false);
			this.ShowMiniMapArea.SetActive(false);
			this.PlayerlookArea.gameObject.SetActive(false);
			this.DeadTrajectory(this.TrajectoryPool);
			for (int i = 0; i < this.TrajectoryGirlPool.Length; i++)
			{
				this.DeadTrajectory(this.TrajectoryGirlPool[i]);
			}
			this.DeadTrajectory(this.TrajectoryMerchantPool);
			for (int j = 0; j < this.TrajectoryPetPool.Count; j++)
			{
				this.DeadTrajectory(this.TrajectoryPetPool[j]);
			}
		}

		// Token: 0x060085EF RID: 34287 RVA: 0x0037E9DC File Offset: 0x0037CDDC
		private void LookAreaMove(PlayerActor player)
		{
			Quaternion rotation = player.CameraControl.CameraComponent.gameObject.transform.rotation;
			Vector3 position = player.Position;
			rotation.x = 0f;
			rotation.z = 0f;
			Camera component = this.MiniMapIcon.GetComponent<Camera>();
			Camera component2 = this.AllAreaIconMap.GetComponent<Camera>();
			if (component != null && component.isActiveAndEnabled)
			{
				position.y = this.MiniMap.transform.position.y - 0.5f;
			}
			else if (component2 != null && component2.isActiveAndEnabled)
			{
				position.y = component2.transform.position.y - 0.5f;
			}
			position.z += this.PlayerLookAreaWidth / 2f;
			this.PlayerlookArea.transform.position = position;
			this.PlayerlookArea.transform.RotateAround(player.Position, Vector3.up, rotation.eulerAngles.y);
			this.PlayerlookArea.transform.rotation = rotation * Quaternion.LookRotation(new Vector3(0f, 1f, 0f));
		}

		// Token: 0x060085F0 RID: 34288 RVA: 0x0037EB3C File Offset: 0x0037CF3C
		private void SetActionPointIcons()
		{
			Transform transform = this.ActionIconArea.transform;
			int num = -1;
			foreach (MiniMapControler.PointIconInfo pointIconInfo in this.actionPointIcon)
			{
				UnityEngine.Object.Destroy(pointIconInfo.Icon.gameObject);
			}
			this.actionPointIcon.Clear();
			for (int i = 0; i < this.actionPoints.Length; i++)
			{
				int num2 = i;
				int num3 = -1;
				for (int j = 0; j < this.PlayerActionEvents.Count; j++)
				{
					if (this.actionPoints[num2].PlayerEventType.Contains(this.PlayerActionEvents[j]) || this.actionPoints[num2].AgentEventType.Contains(this.PlayerActionEvents[j]))
					{
						num3 = j;
						break;
					}
					if (this.actionPoints[num2].PlayerDateEventType[0].Contains(this.PlayerActionEvents[j]) || this.actionPoints[num2].PlayerDateEventType[1].Contains(this.PlayerActionEvents[j]))
					{
						num3 = j;
						break;
					}
				}
				if (num3 >= 0)
				{
					bool flag = this.actionPoints[num2].IDList.IsNullOrEmpty<int>();
					if (Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIcon.TryGetValue(this.actionPoints[num2].ID, out num) || !flag)
					{
						if (flag || Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIcon.TryGetValue(this.actionPoints[num2].IDList[0], out num))
						{
							Vector3 position = this.actionPoints[num2].Position;
							position.y += this.fOffSetY;
							if (num != -1)
							{
								string empty;
								if (!Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIconName.TryGetValue(num, out empty))
								{
									empty = string.Empty;
								}
								switch (num)
								{
								case 0:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.BED, empty));
									break;
								case 1:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.CHAIR, empty));
									break;
								case 2:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.DESK, empty));
									break;
								case 3:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.COOK, empty));
									break;
								case 4:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.TOILET, empty));
									break;
								case 5:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.BATH, empty));
									break;
								case 6:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.SHOWER, empty));
									break;
								case 7:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.STORAGE, empty));
									break;
								case 10:
								case 11:
								case 12:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.PICKEL, empty));
									break;
								case 13:
								case 14:
								case 15:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.SHOVEL, empty));
									break;
								case 16:
								case 17:
								case 18:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.NET, empty));
									break;
								case 19:
								case 20:
								case 21:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.GLOVE, empty));
									break;
								case 28:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.WATERPET, empty));
									break;
								case 31:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.CLOTHET, empty));
									break;
								case 32:
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.CLOTHETBOX, empty));
									break;
								case 34:
									if (!Game.isAdd01)
									{
										goto IL_677;
									}
									this.actionPointIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPoints[num2], MapUIActionCategory.H, empty));
									break;
								}
								int index = this.actionPointIcon.Count - 1;
								if (!(this.actionPointIcon[index].Point != this.actionPoints[num2]))
								{
									this.actionPointIcon[index].Icon.transform.SetParent(transform, false);
									this.actionPointIcon[index].Icon.transform.localScale = Vector3.one;
									this.actionPointIcon[index].Icon.transform.position = position;
									Image componentInChildren = this.actionPointIcon[index].Icon.GetComponentInChildren<Image>();
									componentInChildren.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable[num];
									if (componentInChildren.sprite != null)
									{
										componentInChildren.color = Color.white;
									}
								}
							}
						}
					}
				}
				IL_677:;
			}
		}

		// Token: 0x060085F1 RID: 34289 RVA: 0x0037F1E8 File Offset: 0x0037D5E8
		public void SetActionPointHousingIcons()
		{
			this.SetActionHousingIcons();
			this.SetFarmHousingIcons();
			this.SetCraftPointIcons();
			this.SetJukePointIcons();
		}

		// Token: 0x060085F2 RID: 34290 RVA: 0x0037F204 File Offset: 0x0037D604
		public void SetActionHousingIcons()
		{
			Transform transform = this.ActionIconArea.transform;
			int num = -1;
			foreach (MiniMapControler.PointIconInfo pointIconInfo in this.actionPointHousingIcon)
			{
				UnityEngine.Object.Destroy(pointIconInfo.Icon.gameObject);
			}
			this.actionPointHousingIcon.Clear();
			this.actionPointsHousing = Singleton<Manager.Housing>.Instance.ActionPoints;
			for (int i = 0; i < this.actionPointsHousing.Length; i++)
			{
				int num2 = i;
				int num3 = -1;
				for (int j = 0; j < this.PlayerActionEvents.Count; j++)
				{
					if (this.actionPointsHousing[num2].PlayerEventType.Contains(this.PlayerActionEvents[j]) || this.actionPointsHousing[num2].AgentEventType.Contains(this.PlayerActionEvents[j]))
					{
						num3 = j;
						break;
					}
					if (this.actionPointsHousing[num2].PlayerDateEventType[0].Contains(this.PlayerActionEvents[j]) || this.actionPointsHousing[num2].PlayerDateEventType[1].Contains(this.PlayerActionEvents[j]))
					{
						num3 = j;
						break;
					}
				}
				if (num3 >= 0)
				{
					bool flag = this.actionPointsHousing[num2].IDList.IsNullOrEmpty<int>();
					if (Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIcon.TryGetValue(this.actionPointsHousing[num2].ID, out num) || !flag)
					{
						if (flag || Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIcon.TryGetValue(this.actionPointsHousing[num2].IDList[0], out num))
						{
							Vector3 position = this.actionPointsHousing[num2].Position;
							position.y += this.fOffSetY;
							if (num != -1)
							{
								string empty;
								if (!Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIconName.TryGetValue(num, out empty))
								{
									empty = string.Empty;
								}
								switch (num)
								{
								case 0:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.BED, empty));
									break;
								case 1:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.CHAIR, empty));
									break;
								case 2:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.DESK, empty));
									break;
								case 3:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.COOK, empty));
									break;
								case 4:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.TOILET, empty));
									break;
								case 5:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.BATH, empty));
									break;
								case 6:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.SHOWER, empty));
									break;
								case 7:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.STORAGE, empty));
									break;
								case 10:
								case 11:
								case 12:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.PICKEL, empty));
									break;
								case 13:
								case 14:
								case 15:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.SHOVEL, empty));
									break;
								case 16:
								case 17:
								case 18:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.NET, empty));
									break;
								case 19:
								case 20:
								case 21:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.GLOVE, empty));
									break;
								case 28:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.WATERPET, empty));
									break;
								case 31:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.CLOTHET, empty));
									break;
								case 32:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.CLOTHETBOX, empty));
									break;
								case 34:
									if (!Game.isAdd01)
									{
										goto IL_6CC;
									}
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.H, empty));
									break;
								case 37:
								case 38:
								case 39:
								case 40:
									this.actionPointHousingIcon.Add(new MiniMapControler.PointIconInfo(UnityEngine.Object.Instantiate<GameObject>(this.ActionAreaImage), this.actionPointsHousing[num2], MapUIActionCategory.WARP, empty));
									break;
								}
								int index = this.actionPointHousingIcon.Count - 1;
								if (!(this.actionPointHousingIcon[index].Point != this.actionPointsHousing[num2]))
								{
									this.actionPointHousingIcon[index].Icon.transform.SetParent(transform, false);
									this.actionPointHousingIcon[index].Icon.transform.localScale = Vector3.one;
									this.actionPointHousingIcon[index].Icon.transform.position = position;
									Image componentInChildren = this.actionPointHousingIcon[index].Icon.GetComponentInChildren<Image>();
									componentInChildren.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable[num];
									if (componentInChildren.sprite != null)
									{
										componentInChildren.color = Color.white;
									}
								}
							}
						}
					}
				}
				IL_6CC:;
			}
			if (this.allArea != null)
			{
				this.allArea.RefleshActionHousingIcon();
			}
		}

		// Token: 0x060085F3 RID: 34291 RVA: 0x0037F920 File Offset: 0x0037DD20
		private void SetBasePointIcons()
		{
			Transform transform = this.BaseIconArea.transform;
			if (this.BaseIcons != null)
			{
				foreach (MiniMapControler.IconInfo iconInfo in this.BaseIcons)
				{
					UnityEngine.Object.Destroy(iconInfo.Icon.gameObject);
				}
			}
			this.BaseIcons = new List<MiniMapControler.IconInfo>();
			int baseIconID = this.MinimapUIDefine.BaseIconID;
			for (int i = 0; i < this.basePoints.Length; i++)
			{
				int num = i;
				Canvas canvas = UnityEngine.Object.Instantiate<Canvas>(this.BaseIcon);
				string name;
				if (!Singleton<Manager.Resources>.Instance.itemIconTables.BaseName.TryGetValue(this.basePoints[num].ID, out name))
				{
					name = string.Format("拠点{0}", i);
				}
				this.BaseIcons.Add(new MiniMapControler.IconInfo(canvas, name, this.basePoints[num]));
				Image componentInChildren = this.BaseIcons[num].Icon.GetComponentInChildren<Image>();
				componentInChildren.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable[baseIconID];
				this.BaseIcons[num].Icon.transform.SetParent(transform, false);
				this.BaseIcons[num].Icon.transform.localScale = Vector3.one;
				Vector3 position = this.basePoints[num].Position;
				position.y += this.fOffSetY;
				this.BaseIcons[num].Icon.transform.position = position;
				this.BaseIcons[num].Icon.gameObject.SetActive(true);
			}
		}

		// Token: 0x060085F4 RID: 34292 RVA: 0x0037FB10 File Offset: 0x0037DF10
		private void SetDevicePointIcons()
		{
			Transform transform = this.DeviceIconArea.transform;
			if (this.DeviceIcons != null)
			{
				foreach (MiniMapControler.IconInfo iconInfo in this.DeviceIcons)
				{
					UnityEngine.Object.Destroy(iconInfo.Icon.gameObject);
				}
			}
			this.DeviceIcons = new List<MiniMapControler.IconInfo>();
			int deviceIconID = this.MinimapUIDefine.DeviceIconID;
			for (int i = 0; i < this.devicePoints.Length; i++)
			{
				int num = i;
				Canvas canvas = UnityEngine.Object.Instantiate<Canvas>(this.DeviceIcon);
				string name;
				if (!Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIconName.TryGetValue(deviceIconID, out name))
				{
					name = "端末";
				}
				this.DeviceIcons.Add(new MiniMapControler.IconInfo(canvas, name, this.devicePoints[num]));
				Image componentInChildren = this.DeviceIcons[num].Icon.GetComponentInChildren<Image>();
				componentInChildren.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable[deviceIconID];
				this.DeviceIcons[num].Icon.transform.SetParent(transform, false);
				this.DeviceIcons[num].Icon.transform.localScale = Vector3.one;
				Vector3 position = this.devicePoints[num].Position;
				position.y += this.fOffSetY;
				this.DeviceIcons[num].Icon.transform.position = position;
				this.DeviceIcons[num].Icon.gameObject.SetActive(true);
			}
		}

		// Token: 0x060085F5 RID: 34293 RVA: 0x0037FCE8 File Offset: 0x0037E0E8
		private void SetFarmPointIcons()
		{
			Transform transform = this.FarmIconArea.transform;
			if (this.FarmIcons != null)
			{
				foreach (MiniMapControler.IconInfo iconInfo in this.FarmIcons)
				{
					UnityEngine.Object.Destroy(iconInfo.Icon.gameObject);
				}
			}
			this.FarmIcons = new List<MiniMapControler.IconInfo>();
			int key = this.MinimapUIDefine.FarmIconID;
			int i = 0;
			while (i < this.farmPoints.Length)
			{
				int num = i;
				if (this.farmPoints[num].Kind == FarmPoint.FarmKind.Plant)
				{
					key = this.MinimapUIDefine.FarmIconID;
					goto IL_D9;
				}
				if (this.farmPoints[num].Kind == FarmPoint.FarmKind.ChickenCoop)
				{
					key = this.MinimapUIDefine.ChickenIconID;
					goto IL_D9;
				}
				IL_228:
				i++;
				continue;
				IL_D9:
				Canvas canvas = UnityEngine.Object.Instantiate<Canvas>(this.FarmIcon);
				string name;
				if (!Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIconName.TryGetValue(key, out name))
				{
					if (this.farmPoints[num].Kind == FarmPoint.FarmKind.Plant)
					{
						name = "畑";
					}
					else if (this.farmPoints[num].Kind == FarmPoint.FarmKind.ChickenCoop)
					{
						name = "鶏小屋";
					}
				}
				this.FarmIcons.Add(new MiniMapControler.IconInfo(canvas, name, this.farmPoints[num]));
				Image componentInChildren = this.FarmIcons[num].Icon.GetComponentInChildren<Image>();
				componentInChildren.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable[key];
				this.FarmIcons[num].Icon.transform.SetParent(transform, false);
				this.FarmIcons[num].Icon.transform.localScale = Vector3.one;
				Vector3 position = this.farmPoints[num].Position;
				position.y += this.fOffSetY;
				this.FarmIcons[num].Icon.transform.position = position;
				this.FarmIcons[num].Icon.gameObject.SetActive(true);
				goto IL_228;
			}
		}

		// Token: 0x060085F6 RID: 34294 RVA: 0x0037FF44 File Offset: 0x0037E344
		private void SetFarmHousingIcons()
		{
			Transform transform = this.FarmIconArea.transform;
			if (this.FarmHousingIcons != null)
			{
				foreach (MiniMapControler.IconInfo iconInfo in this.FarmHousingIcons)
				{
					UnityEngine.Object.Destroy(iconInfo.Icon.gameObject);
				}
			}
			this.FarmHousingIcons = new List<MiniMapControler.IconInfo>();
			this.farmPointsHousing = Singleton<Manager.Housing>.Instance.FarmPoints;
			int i = 0;
			while (i < this.farmPointsHousing.Length)
			{
				int num = i;
				int key;
				if (this.farmPointsHousing[num].Kind == FarmPoint.FarmKind.Plant)
				{
					key = this.MinimapUIDefine.FarmIconID;
					goto IL_DD;
				}
				if (this.farmPointsHousing[num].Kind == FarmPoint.FarmKind.ChickenCoop)
				{
					key = this.MinimapUIDefine.ChickenIconID;
					goto IL_DD;
				}
				IL_22C:
				i++;
				continue;
				IL_DD:
				Canvas canvas = UnityEngine.Object.Instantiate<Canvas>(this.FarmIcon);
				string name;
				if (!Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIconName.TryGetValue(key, out name))
				{
					if (this.farmPointsHousing[num].Kind == FarmPoint.FarmKind.Plant)
					{
						name = "畑";
					}
					else if (this.farmPointsHousing[num].Kind == FarmPoint.FarmKind.ChickenCoop)
					{
						name = "鶏小屋";
					}
				}
				this.FarmHousingIcons.Add(new MiniMapControler.IconInfo(canvas, name, this.farmPointsHousing[num]));
				Image componentInChildren = this.FarmHousingIcons[num].Icon.GetComponentInChildren<Image>();
				componentInChildren.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable[key];
				this.FarmHousingIcons[num].Icon.transform.SetParent(transform, false);
				this.FarmHousingIcons[num].Icon.transform.localScale = Vector3.one;
				Vector3 position = this.farmPointsHousing[num].Position;
				position.y += this.fOffSetY;
				this.FarmHousingIcons[num].Icon.transform.position = position;
				this.FarmHousingIcons[num].Icon.gameObject.SetActive(true);
				goto IL_22C;
			}
			if (this.allArea != null)
			{
				this.allArea.RefleshFarmHousingIcon();
			}
		}

		// Token: 0x060085F7 RID: 34295 RVA: 0x003801C0 File Offset: 0x0037E5C0
		private void SetEventPointIcons()
		{
			Transform transform = this.EventIconArea.transform;
			if (this.EventIcons != null)
			{
				foreach (MiniMapControler.IconInfo iconInfo in this.EventIcons)
				{
					UnityEngine.Object.Destroy(iconInfo.Icon.gameObject);
				}
			}
			this.EventIcons = new List<MiniMapControler.IconInfo>();
			for (int i = 0; i < this.eventPoints.Count; i++)
			{
				int index = i;
				Canvas canvas = UnityEngine.Object.Instantiate<Canvas>(this.EventIcon);
				string name = "？？？";
				this.EventIcons.Add(new MiniMapControler.IconInfo(canvas, name, this.eventPoints[index]));
				this.EventIcons[index].Icon.transform.SetParent(transform, false);
				this.EventIcons[index].Icon.transform.localScale = Vector3.one;
				Vector3 position = this.eventPoints[index].Position;
				position.y += this.fOffSetY + 5f;
				position.z -= this.EventIcons[index].Icon.GetComponent<RectTransform>().sizeDelta.y / 4f;
				this.EventIcons[index].Icon.transform.position = position;
				this.EventIcons[index].Icon.gameObject.SetActive(true);
			}
		}

		// Token: 0x060085F8 RID: 34296 RVA: 0x00380380 File Offset: 0x0037E780
		private void SetShipPointIcons()
		{
			Transform transform = this.ShipIconArea.transform;
			if (this.ShipIcons != null)
			{
				foreach (MiniMapControler.IconInfo iconInfo in this.ShipIcons)
				{
					UnityEngine.Object.Destroy(iconInfo.Icon.gameObject);
				}
			}
			this.ShipIcons = new List<MiniMapControler.IconInfo>();
			for (int i = 0; i < this.shipPoints.Length; i++)
			{
				int num = i;
				Canvas canvas = UnityEngine.Object.Instantiate<Canvas>(this.ShipIcon);
				int shipIconID = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.ShipIconID;
				string name;
				if (!Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIconName.TryGetValue(shipIconID, out name))
				{
					name = "船";
				}
				this.ShipIcons.Add(new MiniMapControler.IconInfo(canvas, name, this.shipPoints[num]));
				Image componentInChildren = this.ShipIcons[num].Icon.GetComponentInChildren<Image>();
				componentInChildren.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable[shipIconID];
				this.ShipIcons[num].Icon.transform.SetParent(transform, false);
				this.ShipIcons[num].Icon.transform.localScale = Vector3.one;
				Vector3 position = this.shipPoints[num].Position;
				position.y += this.fOffSetY;
				this.ShipIcons[num].Icon.transform.position = position;
				this.ShipIcons[num].Icon.gameObject.SetActive(true);
			}
		}

		// Token: 0x060085F9 RID: 34297 RVA: 0x00380560 File Offset: 0x0037E960
		private void SetCraftPointIcons()
		{
			Transform transform = this.CraftIconArea.transform;
			if (this.CraftIcons != null)
			{
				foreach (MiniMapControler.IconInfo iconInfo in this.CraftIcons)
				{
					UnityEngine.Object.Destroy(iconInfo.Icon.gameObject);
				}
			}
			this.CraftIcons = new List<MiniMapControler.IconInfo>();
			this.craftPoints = Singleton<Manager.Housing>.Instance.CraftPoints;
			int i = 0;
			while (i < this.craftPoints.Length)
			{
				int num = i;
				int key;
				if (this.craftPoints[num].Kind == CraftPoint.CraftKind.Medicine)
				{
					key = this.MinimapUIDefine.DragDeskIconID;
					goto IL_102;
				}
				if (this.craftPoints[num].Kind == CraftPoint.CraftKind.Pet)
				{
					key = this.MinimapUIDefine.PetUnionIconID;
					goto IL_102;
				}
				if (this.craftPoints[num].Kind == CraftPoint.CraftKind.Recycling)
				{
					key = this.MinimapUIDefine.RecycleIconID;
					goto IL_102;
				}
				IL_271:
				i++;
				continue;
				IL_102:
				Canvas canvas = UnityEngine.Object.Instantiate<Canvas>(this.CraftIcon);
				string name;
				if (!Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIconName.TryGetValue(key, out name))
				{
					if (this.craftPoints[num].Kind == CraftPoint.CraftKind.Medicine)
					{
						name = "薬台";
					}
					else if (this.craftPoints[num].Kind == CraftPoint.CraftKind.Pet)
					{
						name = "ペット合成装置";
					}
					else if (this.craftPoints[num].Kind == CraftPoint.CraftKind.Recycling)
					{
						name = "リサイクル装置";
					}
				}
				this.CraftIcons.Add(new MiniMapControler.IconInfo(canvas, name, this.craftPoints[num]));
				Image componentInChildren = this.CraftIcons[num].Icon.GetComponentInChildren<Image>();
				componentInChildren.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable[key];
				this.CraftIcons[num].Icon.transform.SetParent(transform, false);
				this.CraftIcons[num].Icon.transform.localScale = Vector3.one;
				Vector3 position = this.craftPoints[num].Position;
				position.y += this.fOffSetY;
				this.CraftIcons[num].Icon.transform.position = position;
				this.CraftIcons[num].Icon.gameObject.SetActive(true);
				goto IL_271;
			}
			if (this.allArea != null)
			{
				this.allArea.RefleshCraftIcon();
			}
		}

		// Token: 0x060085FA RID: 34298 RVA: 0x00380820 File Offset: 0x0037EC20
		private void SetJukePointIcons()
		{
			Transform transform = this.JukeIconArea.transform;
			if (this.JukeIcons != null)
			{
				foreach (MiniMapControler.IconInfo iconInfo in this.JukeIcons)
				{
					UnityEngine.Object.Destroy(iconInfo.Icon.gameObject);
				}
			}
			this.JukeIcons = new List<MiniMapControler.IconInfo>();
			this.jukePoints = Singleton<Manager.Housing>.Instance.JukePoints;
			for (int i = 0; i < this.jukePoints.Length; i++)
			{
				int num = i;
				int jukeIconID = this.MinimapUIDefine.JukeIconID;
				Canvas canvas = UnityEngine.Object.Instantiate<Canvas>(this.JukeIcon);
				string name;
				if (!Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIconName.TryGetValue(jukeIconID, out name))
				{
					name = "ジュークボックス";
				}
				this.JukeIcons.Add(new MiniMapControler.IconInfo(canvas, name, this.jukePoints[num]));
				Image componentInChildren = this.JukeIcons[num].Icon.GetComponentInChildren<Image>();
				componentInChildren.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable[jukeIconID];
				this.JukeIcons[num].Icon.transform.SetParent(transform, false);
				this.JukeIcons[num].Icon.transform.localScale = Vector3.one;
				Vector3 position = this.jukePoints[num].Position;
				position.y += this.fOffSetY;
				this.JukeIcons[num].Icon.transform.position = position;
				this.JukeIcons[num].Icon.gameObject.SetActive(true);
			}
			if (this.allArea != null)
			{
				this.allArea.RefleshJukeIcon();
			}
		}

		// Token: 0x060085FB RID: 34299 RVA: 0x00380A24 File Offset: 0x0037EE24
		public Image[] GetGirlsIcon()
		{
			if (this.GirlIcons.Count <= 0)
			{
				return null;
			}
			Image[] array = new Image[this.GirlIcons.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GirlIcons[i].GetComponentInChildren<Image>();
			}
			return array;
		}

		// Token: 0x060085FC RID: 34300 RVA: 0x00380A7E File Offset: 0x0037EE7E
		public Image GetMerchantIcon()
		{
			return this.MerchantIcon.GetComponentInChildren<Image>();
		}

		// Token: 0x060085FD RID: 34301 RVA: 0x00380A8B File Offset: 0x0037EE8B
		public Canvas GetMerchantCanvas()
		{
			return this.MerchantIcon;
		}

		// Token: 0x060085FE RID: 34302 RVA: 0x00380A94 File Offset: 0x0037EE94
		private bool CheckEndDelegate(string MethodName)
		{
			bool result = false;
			if (this.AllMapClosedAction == null)
			{
				return result;
			}
			Delegate[] invocationList = this.AllMapClosedAction.GetInvocationList();
			foreach (Delegate @delegate in invocationList)
			{
				if (!(@delegate.Method.Name != MethodName))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060085FF RID: 34303 RVA: 0x00380B00 File Offset: 0x0037EF00
		private void PlayerNomalChange()
		{
			if (this.Player.Controller.State is WMap)
			{
				if (this.Player.PlayerController.PrevStateName == "Onbu")
				{
					this.Player.PlayerController.ChangeState("Onbu");
				}
				else
				{
					this.Player.PlayerController.ChangeState("Normal");
				}
			}
		}

		// Token: 0x06008600 RID: 34304 RVA: 0x00380B75 File Offset: 0x0037EF75
		private void SetupState()
		{
			this.input.SetupState();
		}

		// Token: 0x06008601 RID: 34305 RVA: 0x00380B84 File Offset: 0x0037EF84
		public Dictionary<int, MinimapNavimesh.AreaGroupInfo> GetAreaGroupInfo(int mapID)
		{
			Dictionary<int, MinimapNavimesh.AreaGroupInfo> result = null;
			if (!this.areaGroupTable.TryGetValue(mapID, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06008602 RID: 34306 RVA: 0x00380BAC File Offset: 0x0037EFAC
		public List<MiniMapControler.IconInfo> GetBaseIconInfos()
		{
			List<MiniMapControler.IconInfo> result = null;
			if (this.BaseIcons != null)
			{
				result = new List<MiniMapControler.IconInfo>(this.BaseIcons);
			}
			return result;
		}

		// Token: 0x06008603 RID: 34307 RVA: 0x00380BD3 File Offset: 0x0037EFD3
		public List<MiniMapControler.PointIconInfo> GetActionIconList()
		{
			return this.actionPointIcon;
		}

		// Token: 0x06008604 RID: 34308 RVA: 0x00380BDB File Offset: 0x0037EFDB
		public List<MiniMapControler.PointIconInfo> GetActionHousingIconList()
		{
			return this.actionPointHousingIcon;
		}

		// Token: 0x06008605 RID: 34309 RVA: 0x00380BE4 File Offset: 0x0037EFE4
		public void IconAllDel()
		{
			foreach (MiniMapControler.PointIconInfo pointIconInfo in this.actionPointIcon)
			{
				if (!(pointIconInfo.Icon == null) && !(pointIconInfo.Icon.gameObject == null))
				{
					UnityEngine.Object.Destroy(pointIconInfo.Icon.gameObject);
				}
			}
			this.actionPointIcon.Clear();
			foreach (MiniMapControler.PointIconInfo pointIconInfo2 in this.actionPointHousingIcon)
			{
				if (!(pointIconInfo2.Icon == null) && !(pointIconInfo2.Icon.gameObject == null))
				{
					UnityEngine.Object.Destroy(pointIconInfo2.Icon.gameObject);
				}
			}
			this.actionPointHousingIcon.Clear();
			foreach (Canvas canvas in this.GirlIcons)
			{
				if (!(canvas == null) && !(canvas.gameObject == null))
				{
					UnityEngine.Object.Destroy(canvas.gameObject);
				}
			}
			this.GirlIcons.Clear();
			foreach (MiniMapControler.PetIconInfo petIconInfo in this.PetIcons)
			{
				if (!(petIconInfo.Icon == null) && !(petIconInfo.Icon.gameObject == null))
				{
					UnityEngine.Object.Destroy(petIconInfo.Icon.gameObject);
				}
			}
			this.PetIcons.Clear();
			foreach (MiniMapControler.IconInfo iconInfo in this.BaseIcons)
			{
				if (!(iconInfo.Icon == null) && !(iconInfo.Icon.gameObject == null))
				{
					UnityEngine.Object.Destroy(iconInfo.Icon.gameObject);
				}
			}
			this.BaseIcons.Clear();
			foreach (MiniMapControler.IconInfo iconInfo2 in this.DeviceIcons)
			{
				if (!(iconInfo2.Icon == null) && !(iconInfo2.Icon.gameObject == null))
				{
					UnityEngine.Object.Destroy(iconInfo2.Icon.gameObject);
				}
			}
			this.DeviceIcons.Clear();
			foreach (MiniMapControler.IconInfo iconInfo3 in this.FarmIcons)
			{
				if (!(iconInfo3.Icon == null) && !(iconInfo3.Icon.gameObject == null))
				{
					UnityEngine.Object.Destroy(iconInfo3.Icon.gameObject);
				}
			}
			this.FarmIcons.Clear();
			foreach (MiniMapControler.IconInfo iconInfo4 in this.FarmHousingIcons)
			{
				if (!(iconInfo4.Icon == null) && !(iconInfo4.Icon.gameObject == null))
				{
					UnityEngine.Object.Destroy(iconInfo4.Icon.gameObject);
				}
			}
			this.FarmHousingIcons.Clear();
			foreach (MiniMapControler.IconInfo iconInfo5 in this.EventIcons)
			{
				if (!(iconInfo5.Icon == null) && !(iconInfo5.Icon.gameObject == null))
				{
					UnityEngine.Object.Destroy(iconInfo5.Icon.gameObject);
				}
			}
			this.EventIcons.Clear();
			foreach (Canvas canvas2 in this.LockIcons)
			{
				if (!(canvas2 == null) && !(canvas2.gameObject == null))
				{
					UnityEngine.Object.Destroy(canvas2.gameObject);
				}
			}
			this.LockIcons.Clear();
			foreach (MiniMapControler.TutorialSearchIconInfo tutorialSearchIconInfo in this.TutorialSearchIcons)
			{
				if (!(tutorialSearchIconInfo.Icon == null) && !(tutorialSearchIconInfo.Icon.gameObject == null))
				{
					UnityEngine.Object.Destroy(tutorialSearchIconInfo.Icon.gameObject);
				}
			}
			this.TutorialSearchIcons.Clear();
			foreach (MiniMapControler.IconInfo iconInfo6 in this.ShipIcons)
			{
				if (!(iconInfo6.Icon == null) && !(iconInfo6.Icon.gameObject == null))
				{
					UnityEngine.Object.Destroy(iconInfo6.Icon.gameObject);
				}
			}
			this.ShipIcons.Clear();
			if (this.allArea != null)
			{
				this.allArea.AllIconReflesh();
			}
		}

		// Token: 0x06008606 RID: 34310 RVA: 0x003812AC File Offset: 0x0037F6AC
		public List<MiniMapControler.IconInfo> GetIconList(int kind)
		{
			switch (kind)
			{
			case 0:
				return this.BaseIcons;
			case 1:
				return this.DeviceIcons;
			case 2:
				return this.FarmIcons;
			case 3:
				return this.EventIcons;
			case 4:
				return this.FarmHousingIcons;
			case 5:
				return this.ShipIcons;
			case 6:
				return this.CraftIcons;
			case 7:
				return this.JukeIcons;
			default:
				return null;
			}
		}

		// Token: 0x06008607 RID: 34311 RVA: 0x0038131D File Offset: 0x0037F71D
		public List<MiniMapControler.PetIconInfo> GetPetIconList()
		{
			return this.PetIcons;
		}

		// Token: 0x06008608 RID: 34312 RVA: 0x00381328 File Offset: 0x0037F728
		public void WarpMoveDispose()
		{
			if (this.allArea.WarpSelectSubscriber != null)
			{
				this.allArea.WarpSelectSubscriber.Dispose();
				this.allArea.WarpSelectSubscriber = null;
			}
			if (this.allAreaMapUI.WarpSelectSubscriber != null)
			{
				this.allAreaMapUI.WarpSelectSubscriber.Dispose();
				this.allAreaMapUI.WarpSelectSubscriber = null;
			}
		}

		// Token: 0x06008609 RID: 34313 RVA: 0x00381390 File Offset: 0x0037F790
		public Dictionary<int, AgentActor> SortGirlDictionary()
		{
			Dictionary<int, AgentActor> dictionary = new Dictionary<int, AgentActor>();
			List<KeyValuePair<int, AgentActor>> list = new List<KeyValuePair<int, AgentActor>>(Singleton<Manager.Map>.Instance.AgentTable);
			list.Sort((KeyValuePair<int, AgentActor> a, KeyValuePair<int, AgentActor> b) => a.Key.CompareTo(b.Key));
			for (int i = 0; i < list.Count; i++)
			{
				dictionary.Add(list[i].Key, list[i].Value);
			}
			return dictionary;
		}

		// Token: 0x0600860A RID: 34314 RVA: 0x00381414 File Offset: 0x0037F814
		private void CheckRefleshGirls()
		{
			bool flag = false;
			if (this.sortedDic.Count != Singleton<Manager.Map>.Instance.AgentTable.Count)
			{
				flag = true;
			}
			if (!flag)
			{
				foreach (KeyValuePair<int, AgentActor> keyValuePair in this.sortedDic)
				{
					if (flag)
					{
						break;
					}
					if (Singleton<Manager.Map>.Instance.AgentTable[keyValuePair.Key] != keyValuePair.Value)
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			this.sortedDic = this.SortGirlDictionary();
		}

		// Token: 0x0600860B RID: 34315 RVA: 0x003814DC File Offset: 0x0037F8DC
		private void OnDestroy()
		{
			foreach (KeyValuePair<int, GameObject> keyValuePair in this.Roads)
			{
				if (!(keyValuePair.Value == null))
				{
					UnityEngine.Object.Destroy(this.Roads[keyValuePair.Key]);
				}
			}
			this.Roads.Clear();
		}

		// Token: 0x04006CAD RID: 27821
		public GameObject ActionAreaImage;

		// Token: 0x04006CAE RID: 27822
		[Header("それぞれの軌跡")]
		public GameObject Trajectory;

		// Token: 0x04006CAF RID: 27823
		public GameObject[] TrajectoryGirl;

		// Token: 0x04006CB0 RID: 27824
		public GameObject TrajectoryMerchant;

		// Token: 0x04006CB1 RID: 27825
		public GameObject TrajectoryPet;

		// Token: 0x04006CB2 RID: 27826
		[Header("それぞれのCanvas")]
		public Canvas PlayerIcon;

		// Token: 0x04006CB3 RID: 27827
		public Canvas GirlIcon;

		// Token: 0x04006CB4 RID: 27828
		public Canvas MerchantIcon;

		// Token: 0x04006CB5 RID: 27829
		public Canvas PetIcon;

		// Token: 0x04006CB6 RID: 27830
		public Canvas BaseIcon;

		// Token: 0x04006CB7 RID: 27831
		public Canvas DeviceIcon;

		// Token: 0x04006CB8 RID: 27832
		public Canvas FarmIcon;

		// Token: 0x04006CB9 RID: 27833
		public Canvas EventIcon;

		// Token: 0x04006CBA RID: 27834
		public Canvas LockIcon;

		// Token: 0x04006CBB RID: 27835
		public Canvas TutorialSearchIcon;

		// Token: 0x04006CBC RID: 27836
		public Canvas ShipIcon;

		// Token: 0x04006CBD RID: 27837
		public Canvas PlayerlookArea;

		// Token: 0x04006CBE RID: 27838
		public Canvas CraftIcon;

		// Token: 0x04006CBF RID: 27839
		public Canvas JukeIcon;

		// Token: 0x04006CC0 RID: 27840
		[Header("カメラ")]
		public GameObject MiniMap;

		// Token: 0x04006CC1 RID: 27841
		public GameObject MiniMapIcon;

		// Token: 0x04006CC2 RID: 27842
		public GameObject AllAreaMap;

		// Token: 0x04006CC3 RID: 27843
		public GameObject AllAreaIconMap;

		// Token: 0x04006CC4 RID: 27844
		[Header("ミニマップの描画エリア")]
		[SerializeField]
		private GameObject ShowMiniMapArea;

		// Token: 0x04006CC5 RID: 27845
		[Header("全体マップの描画エリア")]
		[SerializeField]
		private GameObject ShowAllMapArea;

		// Token: 0x04006CC6 RID: 27846
		[SerializeField]
		private CanvasGroup AllAreaMapCanvasGroup;

		// Token: 0x04006CC7 RID: 27847
		private Transform AllAreaMapCanvasGroupTrans;

		// Token: 0x04006CC8 RID: 27848
		[SerializeField]
		[Header("表示する道")]
		private GameObject Road;

		// Token: 0x04006CC9 RID: 27849
		[SerializeField]
		private GameObject DefRoad;

		// Token: 0x04006CCA RID: 27850
		private Dictionary<int, GameObject> Roads = new Dictionary<int, GameObject>();

		// Token: 0x04006CCB RID: 27851
		[HideInInspector]
		public MinimapNavimesh RoadNaviMesh;

		// Token: 0x04006CCC RID: 27852
		[Header("アイコンを置くためのヒエラルキー位置")]
		public GameObject PlayerIconArea;

		// Token: 0x04006CCD RID: 27853
		public GameObject GirlIconArea;

		// Token: 0x04006CCE RID: 27854
		public GameObject MerchantIconArea;

		// Token: 0x04006CCF RID: 27855
		public GameObject PetIconArea;

		// Token: 0x04006CD0 RID: 27856
		public GameObject ActionIconArea;

		// Token: 0x04006CD1 RID: 27857
		public GameObject BaseIconArea;

		// Token: 0x04006CD2 RID: 27858
		public GameObject DeviceIconArea;

		// Token: 0x04006CD3 RID: 27859
		public GameObject FarmIconArea;

		// Token: 0x04006CD4 RID: 27860
		public GameObject EventIconArea;

		// Token: 0x04006CD5 RID: 27861
		public GameObject LockIconArea;

		// Token: 0x04006CD6 RID: 27862
		public GameObject TutorialSearchIconArea;

		// Token: 0x04006CD7 RID: 27863
		public GameObject ShipIconArea;

		// Token: 0x04006CD8 RID: 27864
		public GameObject CraftIconArea;

		// Token: 0x04006CD9 RID: 27865
		public GameObject JukeIconArea;

		// Token: 0x04006CDA RID: 27866
		[Header("軌跡の設定")]
		public float PutTrajectoryTime;

		// Token: 0x04006CDB RID: 27867
		public float TrajectoryExistTime;

		// Token: 0x04006CDC RID: 27868
		[Space]
		public float fOffSetY = -20f;

		// Token: 0x04006CDD RID: 27869
		[Space]
		public bool FromHomeMenu;

		// Token: 0x04006CDE RID: 27870
		public MiniMapControler.OnWarp WarpProc;

		// Token: 0x04006CDF RID: 27871
		private TrajectoryPool TrajectoryPool;

		// Token: 0x04006CE0 RID: 27872
		private TrajectoryPool[] TrajectoryGirlPool;

		// Token: 0x04006CE1 RID: 27873
		private TrajectoryPool TrajectoryMerchantPool;

		// Token: 0x04006CE2 RID: 27874
		private List<TrajectoryPool> TrajectoryPetPool;

		// Token: 0x04006CE3 RID: 27875
		private AllAreaMapUI allAreaMapUI;

		// Token: 0x04006CE4 RID: 27876
		private List<Canvas> GirlIcons;

		// Token: 0x04006CE5 RID: 27877
		private List<MiniMapControler.PetIconInfo> PetIcons;

		// Token: 0x04006CE6 RID: 27878
		private List<MiniMapControler.IconInfo> BaseIcons;

		// Token: 0x04006CE7 RID: 27879
		private List<MiniMapControler.IconInfo> DeviceIcons;

		// Token: 0x04006CE8 RID: 27880
		private List<MiniMapControler.IconInfo> FarmIcons;

		// Token: 0x04006CE9 RID: 27881
		private List<MiniMapControler.IconInfo> FarmHousingIcons;

		// Token: 0x04006CEA RID: 27882
		private List<MiniMapControler.IconInfo> EventIcons;

		// Token: 0x04006CEB RID: 27883
		private List<Canvas> LockIcons;

		// Token: 0x04006CEC RID: 27884
		private List<MiniMapControler.TutorialSearchIconInfo> TutorialSearchIcons;

		// Token: 0x04006CED RID: 27885
		private List<MiniMapControler.IconInfo> ShipIcons;

		// Token: 0x04006CEE RID: 27886
		private List<MiniMapControler.IconInfo> CraftIcons;

		// Token: 0x04006CEF RID: 27887
		private List<MiniMapControler.IconInfo> JukeIcons;

		// Token: 0x04006CF0 RID: 27888
		private ActionPoint[] actionPoints;

		// Token: 0x04006CF1 RID: 27889
		private ActionPoint[] actionPointsHousing;

		// Token: 0x04006CF2 RID: 27890
		private BasePoint[] basePoints;

		// Token: 0x04006CF3 RID: 27891
		private DevicePoint[] devicePoints;

		// Token: 0x04006CF4 RID: 27892
		private FarmPoint[] farmPoints;

		// Token: 0x04006CF5 RID: 27893
		private FarmPoint[] farmPointsHousing;

		// Token: 0x04006CF6 RID: 27894
		private List<EventPoint> eventPoints;

		// Token: 0x04006CF7 RID: 27895
		private ShipPoint[] shipPoints;

		// Token: 0x04006CF8 RID: 27896
		private CraftPoint[] craftPoints;

		// Token: 0x04006CF9 RID: 27897
		private JukePoint[] jukePoints;

		// Token: 0x04006CFA RID: 27898
		private List<MiniMapControler.PointIconInfo> actionPointIcon = new List<MiniMapControler.PointIconInfo>();

		// Token: 0x04006CFB RID: 27899
		private List<MiniMapControler.PointIconInfo> actionPointHousingIcon = new List<MiniMapControler.PointIconInfo>();

		// Token: 0x04006CFC RID: 27900
		private int nGirlCount;

		// Token: 0x04006CFD RID: 27901
		private int nPetCount;

		// Token: 0x04006CFE RID: 27902
		private float fTimer;

		// Token: 0x04006CFF RID: 27903
		private float PlayerLookAreaWidth;

		// Token: 0x04006D00 RID: 27904
		private GameObject IconObj;

		// Token: 0x04006D01 RID: 27905
		private RenderTexture MiniRenderTex;

		// Token: 0x04006D02 RID: 27906
		private RenderTexture AllRenderTex;

		// Token: 0x04006D03 RID: 27907
		private Dictionary<int, List<Vector3>> LastPos;

		// Token: 0x04006D04 RID: 27908
		private AllAreaCameraControler allArea;

		// Token: 0x04006D05 RID: 27909
		private EventType playerMask;

		// Token: 0x04006D06 RID: 27910
		private List<EventType> PlayerActionEvents = new List<EventType>();

		// Token: 0x04006D07 RID: 27911
		private Manager.Input input;

		// Token: 0x04006D08 RID: 27912
		private PlayerActor Player;

		// Token: 0x04006D09 RID: 27913
		private List<AnimalBase> Pets = new List<AnimalBase>();

		// Token: 0x04006D0A RID: 27914
		private Transform icon;

		// Token: 0x04006D0B RID: 27915
		private List<GameObject> trajePool = new List<GameObject>();

		// Token: 0x04006D0C RID: 27916
		private Vector3[] CalcIconPos = new Vector3[2];

		// Token: 0x04006D0D RID: 27917
		private bool endInit;

		// Token: 0x04006D0E RID: 27918
		private AllAreaMapObjects AllAreaMapObjects;

		// Token: 0x04006D0F RID: 27919
		private Dictionary<int, Dictionary<int, MinimapNavimesh.AreaGroupInfo>> areaGroupTable;

		// Token: 0x04006D10 RID: 27920
		public int VisibleMode;

		// Token: 0x04006D11 RID: 27921
		public int prevVisibleMode;

		// Token: 0x04006D12 RID: 27922
		public bool nowCloseAllMap;

		// Token: 0x04006D13 RID: 27923
		public bool TutorialLockRelease = true;

		// Token: 0x04006D14 RID: 27924
		private Dictionary<int, bool> tmpAgentNullCheckTable = new Dictionary<int, bool>();

		// Token: 0x04006D15 RID: 27925
		private GameObject commonSpace;

		// Token: 0x04006D16 RID: 27926
		private Dictionary<int, AgentActor> sortedDic;

		// Token: 0x04006D17 RID: 27927
		private const int nTrajectoryNum = 3;

		// Token: 0x04006D18 RID: 27928
		private const int nPosYoffset = 10;

		// Token: 0x04006D19 RID: 27929
		private const float fLookPosYoffset = 13f;

		// Token: 0x04006D1A RID: 27930
		private DefinePack.MinimapUI MinimapUIDefine;

		// Token: 0x04006D1B RID: 27931
		public static string RoadPath = "list/map/minimap/";

		// Token: 0x04006D1C RID: 27932
		private bool _prevMinimapConfig = true;

		// Token: 0x04006D1D RID: 27933
		private int _visibleModeMinimapConfigOFF;

		// Token: 0x04006D1E RID: 27934
		private IDisposable CamActivateSubscriber;

		// Token: 0x04006D1F RID: 27935
		public System.Action AllMapClosedAction;

		// Token: 0x02000FBA RID: 4026
		// (Invoke) Token: 0x06008613 RID: 34323
		public delegate void OnWarp(BasePoint basePoint);

		// Token: 0x02000FBB RID: 4027
		public class PointIconInfo
		{
			// Token: 0x06008616 RID: 34326 RVA: 0x003815C3 File Offset: 0x0037F9C3
			public PointIconInfo(GameObject _icon, ActionPoint _point, MapUIActionCategory _category, string _name)
			{
				this.Icon = _icon;
				this.Point = _point;
				this.Category = _category;
				this.Name = _name;
			}

			// Token: 0x04006D24 RID: 27940
			public GameObject Icon;

			// Token: 0x04006D25 RID: 27941
			public ActionPoint Point;

			// Token: 0x04006D26 RID: 27942
			public MapUIActionCategory Category;

			// Token: 0x04006D27 RID: 27943
			public string Name;
		}

		// Token: 0x02000FBC RID: 4028
		public class IconInfo
		{
			// Token: 0x06008617 RID: 34327 RVA: 0x003815E8 File Offset: 0x0037F9E8
			public IconInfo(Canvas _icon, string _name, Point _point)
			{
				this.Icon = _icon;
				this.Name = _name;
				this.Point = _point;
			}

			// Token: 0x04006D28 RID: 27944
			public Canvas Icon;

			// Token: 0x04006D29 RID: 27945
			public Point Point;

			// Token: 0x04006D2A RID: 27946
			public string Name;
		}

		// Token: 0x02000FBD RID: 4029
		public class PetIconInfo
		{
			// Token: 0x06008618 RID: 34328 RVA: 0x00381605 File Offset: 0x0037FA05
			public PetIconInfo(Canvas _icon, string _name, GameObject _obj)
			{
				this.Icon = _icon;
				this.Name = _name;
				this.obj = _obj;
			}

			// Token: 0x04006D2B RID: 27947
			public Canvas Icon;

			// Token: 0x04006D2C RID: 27948
			public GameObject obj;

			// Token: 0x04006D2D RID: 27949
			public string Name;
		}

		// Token: 0x02000FBE RID: 4030
		public class TutorialSearchIconInfo
		{
			// Token: 0x06008619 RID: 34329 RVA: 0x00381622 File Offset: 0x0037FA22
			public TutorialSearchIconInfo(Canvas _icon, TutorialSearchActionPoint _point)
			{
				this.Icon = _icon;
				this.Point = _point;
			}

			// Token: 0x04006D2E RID: 27950
			public Canvas Icon;

			// Token: 0x04006D2F RID: 27951
			public TutorialSearchActionPoint Point;
		}

		// Token: 0x02000FBF RID: 4031
		public class MinimapInfo
		{
			// Token: 0x04006D30 RID: 27952
			public string assetPath;

			// Token: 0x04006D31 RID: 27953
			public string asset;

			// Token: 0x04006D32 RID: 27954
			public string manifest;
		}
	}
}
