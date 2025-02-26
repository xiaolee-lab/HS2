using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Scene;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000FB4 RID: 4020
	public class AllAreaMapUI : MonoBehaviour
	{
		// Token: 0x17001D40 RID: 7488
		// (get) Token: 0x060085B8 RID: 34232 RVA: 0x0037A38A File Offset: 0x0037878A
		public List<Button> _WarpNodes
		{
			[CompilerGenerated]
			get
			{
				return this.WarpNodes;
			}
		}

		// Token: 0x17001D41 RID: 7489
		// (get) Token: 0x060085B9 RID: 34233 RVA: 0x0037A392 File Offset: 0x00378792
		public Button _WorldMap
		{
			[CompilerGenerated]
			get
			{
				return this.WorldMap;
			}
		}

		// Token: 0x17001D42 RID: 7490
		// (get) Token: 0x060085BB RID: 34235 RVA: 0x0037A3A3 File Offset: 0x003787A3
		// (set) Token: 0x060085BA RID: 34234 RVA: 0x0037A39A File Offset: 0x0037879A
		public bool GameClear
		{
			get
			{
				return this.gameClear;
			}
			set
			{
				this.gameClear = value;
			}
		}

		// Token: 0x060085BC RID: 34236 RVA: 0x0037A3AC File Offset: 0x003787AC
		public void Init(MiniMapControler miniMap, GameObject allCamera)
		{
			this.Input = Singleton<Manager.Input>.Instance;
			this.miniMapcontroler = miniMap;
			this.allMapcontroler = allCamera;
			int mapID = Singleton<Map>.Instance.MapID;
			AssetBundleInfo assetBundleInfo;
			if (Singleton<Manager.Resources>.Instance.Map.MapList.TryGetValue(mapID, out assetBundleInfo))
			{
				this.IslandName.text = assetBundleInfo.name;
			}
			else
			{
				this.IslandName.text = "廃墟の島";
			}
			this.SetSelectGirlList();
			this.RefreshWarpPointNode();
			this.WorldMap.onClick.RemoveAllListeners();
			this.WorldMap.onClick.AddListener(delegate()
			{
			});
			DefinePack.AssetBundleManifestsDefine abmanifests = Singleton<Manager.Resources>.Instance.DefinePack.ABManifests;
			if (this.Guid[0] == null)
			{
				this.Guid[0] = UnityEngine.Object.Instantiate<GameObject>(this.PlayGuideNode);
				this.Guid[0].transform.SetParent(this.PlayGuideList.transform, false);
				this.Guid[0].transform.localScale = this.LocalScaleDef;
				Sprite sprite = Singleton<Manager.Resources>.Instance.itemIconTables.InputIconTable[2];
				this.Guid[0].GetComponentInChildren<Image>().sprite = sprite;
				this.Guid[0].GetComponentInChildren<Text>().text = "拡大・縮小";
				this.Guid[0].gameObject.SetActive(true);
			}
			if (this.Guid[1] == null)
			{
				this.Guid[1] = UnityEngine.Object.Instantiate<GameObject>(this.PlayGuideNode);
				this.Guid[1].transform.SetParent(this.PlayGuideList.transform, false);
				this.Guid[1].transform.localScale = this.LocalScaleDef;
				Sprite sprite = Singleton<Manager.Resources>.Instance.itemIconTables.InputIconTable[0];
				this.Guid[1].GetComponentInChildren<Image>().sprite = sprite;
				this.Guid[1].GetComponentInChildren<Text>().text = "決定";
				this.Guid[1].gameObject.SetActive(true);
			}
			if (this.Guid[2] == null)
			{
				this.Guid[2] = UnityEngine.Object.Instantiate<GameObject>(this.PlayGuideNode);
				this.Guid[2].transform.SetParent(this.PlayGuideList.transform, false);
				this.Guid[2].transform.localScale = this.LocalScaleDef;
				Sprite sprite = Singleton<Manager.Resources>.Instance.itemIconTables.InputIconTable[1];
				this.Guid[2].GetComponentInChildren<Image>().sprite = sprite;
				this.Guid[2].GetComponentInChildren<Text>().text = "マップを閉じる";
				this.Guid[2].gameObject.SetActive(true);
			}
			this.WorldMap.gameObject.SetActive(false);
			this.mapAction.Init();
			this.warpListUI.DisposeWarpListUI();
			this.warpListUI.Init();
			this.ActionFilter.Init(miniMap, this);
		}

		// Token: 0x060085BD RID: 34237 RVA: 0x0037A6C8 File Offset: 0x00378AC8
		private void SetSelectGirlList()
		{
			if (this.GirlsNameList != null && this.GirlsNameList.Count > 0)
			{
				for (int i = 0; i < this.GirlsNameList.Count; i++)
				{
					UnityEngine.Object.Destroy(this.GirlsNameList[i].gameObject);
					this.GirlsNameList[i] = null;
				}
				this.GirlsNameList.Clear();
			}
			this.sortedDic = this.miniMapcontroler.SortGirlDictionary();
			if (this.sortedDic != null)
			{
				foreach (AgentActor agentActor in this.sortedDic.Values)
				{
					if (!(agentActor == null))
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GirlsListNode);
						Image componentInChildren = gameObject.GetComponentInChildren<Image>();
						componentInChildren.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActorIconTable[agentActor.ID];
						gameObject.GetComponentInChildren<Text>().text = agentActor.CharaName;
						gameObject.gameObject.SetActive(true);
						gameObject.transform.SetParent(this.GirlsList.transform, false);
						gameObject.transform.localScale = this.LocalScaleDef;
						this.GirlsNameList.Add(gameObject);
					}
				}
			}
		}

		// Token: 0x060085BE RID: 34238 RVA: 0x0037A840 File Offset: 0x00378C40
		public void Refresh()
		{
			this.WorldMap.gameObject.SetActive(false);
			this.RefreshWarpPointNode();
			Singleton<Manager.Input>.Instance.FocusLevel = this.mapAction.FocusLevel;
			Singleton<Manager.Input>.Instance.MenuElements = this.mapAction.MenuUIList;
			this.mapAction.DelCursor();
			this.warpListUI.DelCursor();
			this.SetSelectGirlList();
			this.ActionFilter.Refresh();
		}

		// Token: 0x060085BF RID: 34239 RVA: 0x0037A8B8 File Offset: 0x00378CB8
		public void RefreshWarpPointNode()
		{
			this.DestroyWarpPointNode();
			List<MiniMapControler.IconInfo> baseIconInfos = this.miniMapcontroler.GetBaseIconInfos();
			if (baseIconInfos == null)
			{
				return;
			}
			List<MiniMapControler.IconInfo> list = new List<MiniMapControler.IconInfo>();
			foreach (KeyValuePair<int, string> keyValuePair in Singleton<Manager.Resources>.Instance.itemIconTables.BaseName)
			{
				for (int i = 0; i < baseIconInfos.Count; i++)
				{
					int index = i;
					BasePoint component = baseIconInfos[index].Point.GetComponent<BasePoint>();
					if (keyValuePair.Key == component.ID)
					{
						list.Add(new MiniMapControler.IconInfo(baseIconInfos[index].Icon, baseIconInfos[index].Name, baseIconInfos[index].Point));
						break;
					}
				}
			}
			int mapID = Singleton<Map>.Instance.MapID;
			for (int j = 0; j < list.Count; j++)
			{
				int index2 = j;
				if (list[index2].Point.gameObject.activeSelf)
				{
					if (list[index2].Icon.gameObject.activeSelf)
					{
						bool flag = true;
						Dictionary<int, MinimapNavimesh.AreaGroupInfo> areaGroupInfo = this.miniMapcontroler.GetAreaGroupInfo(mapID);
						if (areaGroupInfo != null)
						{
							foreach (KeyValuePair<int, MinimapNavimesh.AreaGroupInfo> keyValuePair2 in areaGroupInfo)
							{
								int areaID = list[index2].Point.OwnerArea.AreaID;
								if (list[index2].Point.OwnerArea != null && keyValuePair2.Value.areaID.Contains(areaID))
								{
									flag = this.miniMapcontroler.RoadNaviMesh.areaGroupActive[keyValuePair2.Key];
								}
							}
						}
						Image componentInChildren = list[index2].Icon.GetComponentInChildren<Image>(true);
						if (!(componentInChildren == null) && (componentInChildren.enabled || flag))
						{
							this.AddWarpPointNode(list[index2].Point.GetComponent<BasePoint>(), index2);
						}
					}
				}
			}
		}

		// Token: 0x060085C0 RID: 34240 RVA: 0x0037AB44 File Offset: 0x00378F44
		private void AddWarpPointNode(BasePoint add, int _index)
		{
			Button button = UnityEngine.Object.Instantiate<Button>(this.WarpContentNode);
			button.transform.SetParent(this.WarpContent.transform, false);
			button.transform.localScale = this.LocalScaleDef;
			string name;
			if (!Singleton<Manager.Resources>.Instance.itemIconTables.BaseName.TryGetValue(add.ID, out name))
			{
				name = string.Format("拠点{0:00}", add.ID);
			}
			WarpListNode node = button.GetComponent<WarpListNode>();
			node.Set(add, name);
			int baseIconID = Singleton<Manager.Resources>.Instance.DefinePack.MinimapUIDefine.BaseIconID;
			bool flag = false;
			if (Singleton<Map>.Instance.GetBasePointOpenState(node.basePoint.ID, out flag) && flag && !node.canWarp)
			{
				node.IconSet(Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable[baseIconID]);
			}
			button.onClick.AddListener(delegate()
			{
				if (AllAreaCameraControler.NowWarp)
				{
					return;
				}
				Vector3 Pos = add.Position;
				Vector3 camPos = this.allMapcontroler.transform.position;
				Pos.y = camPos.y;
				AllAreaCameraControler component = this.allMapcontroler.GetComponent<AllAreaCameraControler>();
				Sprite sprite = null;
				Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(Singleton<Manager.Resources>.Instance.DefinePack.MinimapUIDefine.BaseIconID, out sprite);
				if (component.ClickedLabelImage != null)
				{
					component.ClickedLabelImage.sprite = sprite;
				}
				if (component.ClickedLabelText != null)
				{
					component.ClickedLabelText.text = string.Format("：{0}", name);
				}
				if (this.WarpSelectSubscriber != null)
				{
					if (AllAreaCameraControler.NowWarp)
					{
						AllAreaCameraControler.NowWarp = false;
					}
					this.WarpSelectSubscriber.Dispose();
				}
				this.WarpSelectSubscriber = ObservableEasing.EaseOutQuint(1f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
				{
					if (!AllAreaCameraControler.NowWarp)
					{
						AllAreaCameraControler.NowWarp = true;
					}
					<AddWarpPointNode>c__AnonStorey.allMapcontroler.transform.position = Vector3.Lerp(camPos, Pos, x.Value);
				}, delegate()
				{
					AllAreaCameraControler.NowWarp = false;
				});
				ConfirmScene.Sentence = "このポイントに移動しますか";
				ConfirmScene.OnClickedYes = delegate()
				{
					MiniMapControler.OnWarp warpProc = <AddWarpPointNode>c__AnonStorey.miniMapcontroler.WarpProc;
					if (warpProc != null)
					{
						warpProc(node.basePoint);
					}
					<AddWarpPointNode>c__AnonStorey.miniMapcontroler.WarpProc = null;
					<AddWarpPointNode>c__AnonStorey.miniMapcontroler.ChangeCamera(false, true);
					<AddWarpPointNode>c__AnonStorey.Input.ReserveState(Manager.Input.ValidType.Action);
					<AddWarpPointNode>c__AnonStorey.Input.FocusLevel = 0;
					<AddWarpPointNode>c__AnonStorey.Input.SetupState();
					Singleton<Map>.Instance.Player.SetScheduledInteractionState(true);
					Singleton<Map>.Instance.Player.ReleaseInteraction();
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
					if (AllAreaCameraControler.NowWarp)
					{
						AllAreaCameraControler.NowWarp = false;
					}
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					<AddWarpPointNode>c__AnonStorey.Input.ClearMenuElements();
					<AddWarpPointNode>c__AnonStorey.Input.FocusLevel = <AddWarpPointNode>c__AnonStorey.warpListUI.FocusLevel;
					<AddWarpPointNode>c__AnonStorey.Input.MenuElements = <AddWarpPointNode>c__AnonStorey.warpListUI.MenuUIList;
					<AddWarpPointNode>c__AnonStorey.Input.ReserveState(Manager.Input.ValidType.UI);
					<AddWarpPointNode>c__AnonStorey.Input.SetupState();
					Singleton<Map>.Instance.Player.SetScheduledInteractionState(false);
					Singleton<Map>.Instance.Player.ReleaseInteraction();
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					if (AllAreaCameraControler.NowWarp)
					{
						AllAreaCameraControler.NowWarp = false;
					}
				};
				this.miniMapcontroler.AllAreaMap.GetComponent<AllAreaCameraControler>().ChangeTargetPos(Pos);
				bool flag2 = false;
				if (!Singleton<Map>.Instance.GetBasePointOpenState(node.basePoint.ID, out flag2))
				{
					flag2 = false;
				}
				if (flag2)
				{
					Singleton<Game>.Instance.LoadDialog();
				}
			});
			this.enterTrigger = button.gameObject.GetComponent<PointerEnterTrigger>();
			if (this.enterTrigger == null)
			{
				this.enterTrigger = button.gameObject.AddComponent<PointerEnterTrigger>();
			}
			this.onEnter = new UITrigger.TriggerEvent();
			this.enterTrigger.Triggers.Add(this.onEnter);
			int id = this.WarpNodes.Count;
			this.onEnter.AddListener(delegate(BaseEventData x)
			{
				if (this.Input.FocusLevel == this.mapAction.FocusLevel)
				{
					this.Input.FocusLevel = this.warpListUI.FocusLevel;
					this.Input.MenuElements = this.warpListUI.MenuUIList;
					this.mapAction.DelCursor();
				}
				this.warpListUI._WarpID = id + 1;
				int num = Mathf.RoundToInt(-(this.warpListUI.scrollRect.verticalNormalizedPosition - 1f) * (float)(this._WarpNodes.Count - 5));
				if (this.warpListUI._WarpID < this.WarpNodes.Count - 4 && (this.warpListUI.StartShowID > id + 1 || this.warpListUI.EndShowID < id + 1))
				{
					if (num == id + 1)
					{
						this.warpListUI.StartShowID = id + 1;
					}
					else
					{
						this.warpListUI.StartShowID = num;
					}
					this.warpListUI.EndShowID = this.warpListUI.StartShowID + 4;
				}
			});
			button.gameObject.SetActive(true);
			this.WarpNodes.Add(button);
		}

		// Token: 0x060085C1 RID: 34241 RVA: 0x0037AD20 File Offset: 0x00379120
		private void DestroyWarpPointNode()
		{
			foreach (Button button in this.WarpNodes)
			{
				if (!(button.gameObject == null))
				{
					UnityEngine.Object.Destroy(button.gameObject);
				}
			}
			this.WarpNodes.Clear();
		}

		// Token: 0x04006C83 RID: 27779
		[SerializeField]
		private Image[] IslandLabel;

		// Token: 0x04006C84 RID: 27780
		[SerializeField]
		private Text IslandName;

		// Token: 0x04006C85 RID: 27781
		[SerializeField]
		private GameObject GirlsList;

		// Token: 0x04006C86 RID: 27782
		[SerializeField]
		private GameObject GirlsListNode;

		// Token: 0x04006C87 RID: 27783
		private List<GameObject> GirlsNameList = new List<GameObject>();

		// Token: 0x04006C88 RID: 27784
		[SerializeField]
		private Image WarpPanelLabel;

		// Token: 0x04006C89 RID: 27785
		[SerializeField]
		private Image WarpBG;

		// Token: 0x04006C8A RID: 27786
		[SerializeField]
		private GameObject WarpContent;

		// Token: 0x04006C8B RID: 27787
		[SerializeField]
		private Button WarpContentNode;

		// Token: 0x04006C8C RID: 27788
		private List<Button> WarpNodes = new List<Button>();

		// Token: 0x04006C8D RID: 27789
		[SerializeField]
		private Button WorldMap;

		// Token: 0x04006C8E RID: 27790
		[SerializeField]
		private AllAreaMapActionFilter ActionFilter;

		// Token: 0x04006C8F RID: 27791
		[SerializeField]
		private GameObject PlayGuideList;

		// Token: 0x04006C90 RID: 27792
		[SerializeField]
		private GameObject PlayGuideNode;

		// Token: 0x04006C91 RID: 27793
		private MiniMapControler miniMapcontroler;

		// Token: 0x04006C92 RID: 27794
		private GameObject allMapcontroler;

		// Token: 0x04006C93 RID: 27795
		private Manager.Input Input;

		// Token: 0x04006C94 RID: 27796
		private AllAreaMapUI.ListComparer basePointComparer = new AllAreaMapUI.ListComparer();

		// Token: 0x04006C95 RID: 27797
		private bool gameClear;

		// Token: 0x04006C96 RID: 27798
		[SerializeField]
		private MapActionCategoryUI mapAction;

		// Token: 0x04006C97 RID: 27799
		[SerializeField]
		private WarpListUI warpListUI;

		// Token: 0x04006C98 RID: 27800
		private Vector3 LocalScaleDef = new Vector3(1f, 1f, 1f);

		// Token: 0x04006C99 RID: 27801
		private GameObject[] Guid = new GameObject[3];

		// Token: 0x04006C9A RID: 27802
		private Dictionary<int, AgentActor> sortedDic;

		// Token: 0x04006C9B RID: 27803
		private PointerEnterTrigger enterTrigger;

		// Token: 0x04006C9C RID: 27804
		private UITrigger.TriggerEvent onEnter;

		// Token: 0x04006C9D RID: 27805
		public IDisposable WarpSelectSubscriber;

		// Token: 0x02000FB5 RID: 4021
		private class ListComparer : IComparer<MiniMapControler.IconInfo>
		{
			// Token: 0x060085C4 RID: 34244 RVA: 0x0037ADAE File Offset: 0x003791AE
			public int Compare(MiniMapControler.IconInfo a, MiniMapControler.IconInfo b)
			{
				this.pointA = a.Point.GetComponent<BasePoint>();
				this.pointB = b.Point.GetComponent<BasePoint>();
				return this.SortCompare<int>(this.pointA.ID, this.pointB.ID);
			}

			// Token: 0x060085C5 RID: 34245 RVA: 0x0037ADEE File Offset: 0x003791EE
			private int SortCompare<T>(T a, T b) where T : IComparable
			{
				return a.CompareTo(b);
			}

			// Token: 0x04006C9F RID: 27807
			private BasePoint pointA;

			// Token: 0x04006CA0 RID: 27808
			private BasePoint pointB;
		}
	}
}
