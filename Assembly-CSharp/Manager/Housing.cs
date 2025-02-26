using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AIProject;
using AIProject.Animal;
using AIProject.SaveData;
using Housing;
using UniRx;
using UnityEngine;

namespace Manager
{
	// Token: 0x020008CE RID: 2254
	public class Housing : Singleton<Housing>
	{
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x0015841A File Offset: 0x0015681A
		// (set) Token: 0x06003AFF RID: 15103 RVA: 0x00158422 File Offset: 0x00156822
		public Dictionary<int, Housing.LoadInfo> dicLoadInfo { get; private set; } = new Dictionary<int, Housing.LoadInfo>();

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x0015842B File Offset: 0x0015682B
		// (set) Token: 0x06003B01 RID: 15105 RVA: 0x00158433 File Offset: 0x00156833
		public Dictionary<int, Housing.CategoryInfo> dicCategoryInfo { get; private set; } = new Dictionary<int, Housing.CategoryInfo>();

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x0015843C File Offset: 0x0015683C
		// (set) Token: 0x06003B03 RID: 15107 RVA: 0x00158444 File Offset: 0x00156844
		public Dictionary<int, Housing.AreaSizeInfo> dicAreaSizeInfo { get; private set; } = new Dictionary<int, Housing.AreaSizeInfo>();

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x0015844D File Offset: 0x0015684D
		// (set) Token: 0x06003B05 RID: 15109 RVA: 0x00158455 File Offset: 0x00156855
		public Dictionary<int, Housing.AreaInfo> dicAreaInfo { get; private set; } = new Dictionary<int, Housing.AreaInfo>();

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x0015845E File Offset: 0x0015685E
		// (set) Token: 0x06003B07 RID: 15111 RVA: 0x00158466 File Offset: 0x00156866
		public bool IsLoadList { get; private set; }

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x0015846F File Offset: 0x0015686F
		// (set) Token: 0x06003B09 RID: 15113 RVA: 0x00158477 File Offset: 0x00156877
		public CraftInfo CraftInfo { get; private set; }

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x00158480 File Offset: 0x00156880
		public List<ObjectCtrl> ObjectCtrls
		{
			get
			{
				if (this.CraftInfo == null)
				{
					return null;
				}
				return (from v in this.CraftInfo.ObjectCtrls
				orderby this.CraftInfo.ObjectInfos.FindIndex((IObjectInfo _i) => _i == v.Key)
				select v.Value).ToList<ObjectCtrl>();
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x001584DD File Offset: 0x001568DD
		// (set) Token: 0x06003B0C RID: 15116 RVA: 0x001584E5 File Offset: 0x001568E5
		private Dictionary<int, GameObject> ObjRoots { get; set; } = new Dictionary<int, GameObject>();

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x001584EE File Offset: 0x001568EE
		public bool IsCraft
		{
			[CompilerGenerated]
			get
			{
				return this.objScene && this.objScene.activeSelf;
			}
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x00158514 File Offset: 0x00156914
		private IEnumerator LoadExcelDataCoroutine()
		{
			if (this.IsLoadList)
			{
				yield break;
			}
			yield return null;
			this.waitTime = new Housing.WaitTime();
			this.dicCategoryInfo.Clear();
			this.dicLoadInfo.Clear();
			this.dicAreaSizeInfo.Clear();
			this.dicAreaInfo.Clear();
			List<string> pathList = CommonLib.GetAssetBundleNameListFromPath("housing/info/", true);
			pathList.Sort();
			Housing.FileListInfo fli = new Housing.FileListInfo(pathList);
			if (this.waitTime.isOver)
			{
				yield return null;
				this.waitTime.Next();
			}
			string[] array = null;
			for (int i = 0; i < pathList.Count; i++)
			{
				string bundlePath = pathList[i];
				string fileName = Path.GetFileNameWithoutExtension(bundlePath);
				int pack = 0;
				int.TryParse(fileName, out pack);
				if (pack < 50 || Game.isAdd50)
				{
					if (fli.Check(bundlePath, "Category_" + fileName))
					{
						this.LoadCategoryInfo(AssetUtility.LoadAsset<ExcelData>(bundlePath, "Category_" + fileName, string.Empty));
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					array = fli.FindRegex(bundlePath, string.Format("ItemList_{0}_(\\d*)", fileName));
					if (!array.IsNullOrEmpty<string>())
					{
						foreach (string f in array)
						{
							this.LoadLoadInfo(AssetUtility.LoadAsset<ExcelData>(bundlePath, f, string.Empty), pack);
							if (this.waitTime.isOver)
							{
								yield return null;
								this.waitTime.Next();
							}
						}
					}
					if (fli.Check(bundlePath, "AreaSize_" + fileName))
					{
						this.LoadAreaSizeInfo(AssetUtility.LoadAsset<ExcelData>(bundlePath, "AreaSize_" + fileName, string.Empty));
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					if (fli.Check(bundlePath, "Area_" + fileName))
					{
						this.LoadAreaInfo(AssetUtility.LoadAsset<ExcelData>(bundlePath, "Area_" + fileName, string.Empty));
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
				}
			}
			this.waitTime = null;
			this.IsLoadList = true;
			yield break;
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x00158530 File Offset: 0x00156930
		private void LoadCategoryInfo(ExcelData _ed)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(2)
			select v.list)
			{
				int key = -1;
				if (!int.TryParse(list.SafeGet(0), out key))
				{
					break;
				}
				this.dicCategoryInfo[key] = new Housing.CategoryInfo(list);
			}
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x001585E4 File Offset: 0x001569E4
		private void LoadLoadInfo(ExcelData _ed, int _pack)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(2)
			select v.list)
			{
				int key = -1;
				if (!int.TryParse(list.SafeGet(0), out key))
				{
					break;
				}
				this.dicLoadInfo[key] = new Housing.LoadInfo(_pack, list);
			}
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x00158698 File Offset: 0x00156A98
		private void LoadAreaSizeInfo(ExcelData _ed)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(2)
			select v.list)
			{
				int key = -1;
				if (!int.TryParse(list.SafeGet(0), out key))
				{
					break;
				}
				this.dicAreaSizeInfo[key] = new Housing.AreaSizeInfo(list);
			}
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x0015874C File Offset: 0x00156B4C
		private void LoadAreaInfo(ExcelData _ed)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(2)
			select v.list)
			{
				int key = -1;
				if (!int.TryParse(list.SafeGet(0), out key))
				{
					break;
				}
				this.dicAreaInfo[key] = new Housing.AreaInfo(list);
			}
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x00158800 File Offset: 0x00156C00
		public void SetCraftInfo(CraftInfo _craftInfo, bool _load = true)
		{
			this.CraftInfo = _craftInfo;
			if (_load)
			{
				this.LoadObject();
			}
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x00158818 File Offset: 0x00156C18
		public IEnumerator LoadHousing(int _type = 0)
		{
			this.waitTime = new Housing.WaitTime();
			this.ObjRoots.Clear();
			HousingData hd = Singleton<Game>.Instance.WorldData.HousingData;
			int[] keys = null;
			if (_type != 0)
			{
				if (_type == 1)
				{
					keys = new int[]
					{
						3
					};
				}
			}
			else
			{
				keys = new int[]
				{
					0,
					1,
					2
				};
			}
			foreach (KeyValuePair<int, CraftInfo> v2 in from v in hd.CraftInfos
			where keys.Contains(v.Key)
			select v)
			{
				CraftInfo info = v2.Value;
				info.ObjRoot = this.GetRoot(v2.Key);
				if (this.waitTime.isOver)
				{
					yield return null;
					this.waitTime.Next();
				}
				yield return this.LoadObjectAsync(info);
			}
			this.waitTime = null;
			yield break;
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x0015883C File Offset: 0x00156C3C
		public void Release()
		{
			if (Singleton<Game>.Instance.Data.AutoData != null)
			{
				HousingData housingData = Singleton<Game>.Instance.Data.AutoData.HousingData;
				if (housingData != null)
				{
					foreach (KeyValuePair<int, CraftInfo> keyValuePair in housingData.CraftInfos)
					{
						keyValuePair.Value.ObjectCtrls.Clear();
					}
				}
			}
			foreach (KeyValuePair<int, WorldData> keyValuePair2 in Singleton<Game>.Instance.Data.WorldList)
			{
				if (keyValuePair2.Value != null)
				{
					HousingData housingData2 = keyValuePair2.Value.HousingData;
					if (housingData2 != null)
					{
						foreach (KeyValuePair<int, CraftInfo> keyValuePair3 in housingData2.CraftInfos)
						{
							keyValuePair3.Value.ObjectCtrls.Clear();
						}
					}
				}
			}
			HousingData housingData3 = Singleton<Game>.Instance.WorldData.HousingData;
			foreach (KeyValuePair<int, CraftInfo> keyValuePair4 in housingData3.CraftInfos)
			{
				keyValuePair4.Value.ObjectCtrls.Clear();
			}
			this.ObjRoots.Clear();
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x00158A18 File Offset: 0x00156E18
		public bool StartHousing()
		{
			this.objScene = CommonLib.LoadAsset<GameObject>("housing/base/06.unity3d", "CraftScene", true, string.Empty);
			if (this.objScene == null)
			{
				return false;
			}
			this.craftScene = this.objScene.GetComponent<CraftScene>();
			int tutorialID = 13;
			this.craftScene.DisplayTutorial = (Singleton<MapUIContainer>.IsInstance() && !MapUIContainer.GetTutorialOpenState(tutorialID));
			if (this.craftScene.DisplayTutorial)
			{
				MapUIContainer.TutorialUI.BlockRaycastEnabled = true;
			}
			(from b in this.craftScene.ObserveEveryValueChanged((CraftScene cs) => cs.IsInit, FrameCountType.Update, false)
			where b
			select b).Take(1).Subscribe(delegate(bool _)
			{
				MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 1f, true).Subscribe(delegate(Unit __)
				{
				}, delegate()
				{
					if (this.craftScene.DisplayTutorial)
					{
						MapUIContainer.TutorialUI.ClosedEvent = delegate()
						{
							this.craftScene.DisplayTutorial = false;
						};
						MapUIContainer.OpenTutorialUI(tutorialID, false);
					}
				});
			});
			return true;
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x00158B1F File Offset: 0x00156F1F
		public void EndHousing()
		{
			CraftInfo craftInfo = this.CraftInfo;
			if (craftInfo != null)
			{
				craftInfo.SetOverlapColliders(false);
			}
			if (this.objScene)
			{
				UnityEngine.Object.DestroyImmediate(this.objScene);
			}
			this.objScene = null;
			this.craftScene = null;
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06003B18 RID: 15128 RVA: 0x00158B60 File Offset: 0x00156F60
		public ActionPoint[] ActionPoints
		{
			get
			{
				List<ActionPoint> list = new List<ActionPoint>();
				HousingData housingData = Singleton<Game>.Instance.WorldData.HousingData;
				foreach (KeyValuePair<int, CraftInfo> keyValuePair in housingData.CraftInfos)
				{
					CraftInfo value = keyValuePair.Value;
					if (value != null)
					{
						value.GetActionPoint(ref list);
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x00158BF0 File Offset: 0x00156FF0
		public FarmPoint[] FarmPoints
		{
			get
			{
				List<FarmPoint> list = new List<FarmPoint>();
				HousingData housingData = Singleton<Game>.Instance.WorldData.HousingData;
				foreach (KeyValuePair<int, CraftInfo> keyValuePair in housingData.CraftInfos)
				{
					CraftInfo value = keyValuePair.Value;
					if (value != null)
					{
						value.GetFarmPoint(ref list);
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06003B1A RID: 15130 RVA: 0x00158C80 File Offset: 0x00157080
		public HPoint[] HPoints
		{
			get
			{
				List<HPoint> list = new List<HPoint>();
				HousingData housingData = Singleton<Game>.Instance.WorldData.HousingData;
				foreach (KeyValuePair<int, CraftInfo> keyValuePair in housingData.CraftInfos)
				{
					CraftInfo value = keyValuePair.Value;
					if (value != null)
					{
						value.GetHPoint(ref list);
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x00158D10 File Offset: 0x00157110
		public HPoint[] GetHPoint(int _id)
		{
			List<HPoint> list = new List<HPoint>();
			HousingData housingData = Singleton<Game>.Instance.WorldData.HousingData;
			CraftInfo craftInfo = null;
			if (housingData.CraftInfos.TryGetValue(_id, out craftInfo) && craftInfo != null)
			{
				craftInfo.GetHPoint(ref list);
			}
			return list.ToArray();
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x00158D5C File Offset: 0x0015715C
		public ItemComponent.ColInfo[] GetColInfo(int _id)
		{
			List<ItemComponent.ColInfo> list = new List<ItemComponent.ColInfo>();
			HousingData housingData = Singleton<Game>.Instance.WorldData.HousingData;
			CraftInfo craftInfo = null;
			if (housingData.CraftInfos.TryGetValue(_id, out craftInfo) && craftInfo != null)
			{
				craftInfo.GetColInfo(ref list);
			}
			return list.ToArray();
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x00158DA8 File Offset: 0x001571A8
		public void StartShield(int _id)
		{
			List<ItemComponent.ColInfo> list = new List<ItemComponent.ColInfo>();
			HousingData housingData = Singleton<Game>.Instance.WorldData.HousingData;
			CraftInfo craftInfo = null;
			if (housingData.CraftInfos.TryGetValue(_id, out craftInfo) && craftInfo != null)
			{
				craftInfo.GetColInfo(ref list);
			}
			this.colInfos = list.ToArray();
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x00158E00 File Offset: 0x00157200
		public void ShieldProc(Collider _collider)
		{
			if (this.colInfos.IsNullOrEmpty<ItemComponent.ColInfo>())
			{
				return;
			}
			foreach (ItemComponent.ColInfo colInfo in this.colInfos)
			{
				colInfo.CheckCollision(_collider);
			}
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x00158E44 File Offset: 0x00157244
		public void EndShield()
		{
			this.VisibleShield();
			this.colInfos = null;
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x00158E54 File Offset: 0x00157254
		public void VisibleShield()
		{
			if (this.colInfos.IsNullOrEmpty<ItemComponent.ColInfo>())
			{
				return;
			}
			foreach (ItemComponent.ColInfo colInfo in this.colInfos)
			{
				colInfo.Visible = true;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x00158E98 File Offset: 0x00157298
		public PetHomePoint[] PetHomePoints
		{
			get
			{
				List<PetHomePoint> list = new List<PetHomePoint>();
				HousingData housingData = Singleton<Game>.Instance.WorldData.HousingData;
				foreach (KeyValuePair<int, CraftInfo> keyValuePair in housingData.CraftInfos)
				{
					CraftInfo value = keyValuePair.Value;
					if (value != null)
					{
						value.GetPetHomePoint(ref list);
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x00158F28 File Offset: 0x00157328
		public JukePoint[] JukePoints
		{
			get
			{
				List<JukePoint> list = new List<JukePoint>();
				HousingData housingData = Singleton<Game>.Instance.WorldData.HousingData;
				foreach (KeyValuePair<int, CraftInfo> keyValuePair in housingData.CraftInfos)
				{
					CraftInfo value = keyValuePair.Value;
					if (value != null)
					{
						value.GetJukePoint(ref list);
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x00158FB8 File Offset: 0x001573B8
		public CraftPoint[] CraftPoints
		{
			get
			{
				List<CraftPoint> list = new List<CraftPoint>();
				HousingData housingData = Singleton<Game>.Instance.WorldData.HousingData;
				foreach (KeyValuePair<int, CraftInfo> keyValuePair in housingData.CraftInfos)
				{
					CraftInfo value = keyValuePair.Value;
					if (value != null)
					{
						value.GetCraftPoint(ref list);
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x00159048 File Offset: 0x00157448
		public LightSwitchPoint[] LightSwitchPoints
		{
			get
			{
				List<LightSwitchPoint> list = new List<LightSwitchPoint>();
				HousingData housingData = Singleton<Game>.Instance.WorldData.HousingData;
				foreach (KeyValuePair<int, CraftInfo> keyValuePair in housingData.CraftInfos)
				{
					CraftInfo value = keyValuePair.Value;
					if (value != null)
					{
						value.GetLightSwitchPoint(ref list);
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x001590D8 File Offset: 0x001574D8
		public int GetSizeType(int _area)
		{
			Housing.AreaInfo areaInfo = null;
			return (!this.dicAreaInfo.TryGetValue(_area, out areaInfo)) ? 0 : areaInfo.size;
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x00159108 File Offset: 0x00157508
		public GameObject GetRoot(int _idx)
		{
			GameObject gameObject = null;
			if (!this.ObjRoots.TryGetValue(_idx, out gameObject))
			{
				gameObject = new GameObject(string.Format("housing {0}", _idx));
				if (Singleton<Map>.IsInstance() && Singleton<Map>.Instance.NavMeshSurface)
				{
					gameObject.transform.SetParent(Singleton<Map>.Instance.NavMeshSurface.transform);
				}
				Transform transform = null;
				if (Singleton<Map>.Instance.HousingPointTable.TryGetValue(_idx, out transform))
				{
					gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
				}
				else
				{
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.identity;
				}
				this.ObjRoots.Add(_idx, gameObject);
			}
			return gameObject;
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x001591DC File Offset: 0x001575DC
		public void DeleteRoot()
		{
			foreach (KeyValuePair<int, GameObject> keyValuePair in this.ObjRoots)
			{
				if (!(keyValuePair.Value == null))
				{
					UnityEngine.Object.DestroyImmediate(keyValuePair.Value);
				}
			}
			this.ObjRoots.Clear();
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x00159260 File Offset: 0x00157660
		public OCItem AddObject(int _id)
		{
			if (!this.dicLoadInfo.ContainsKey(_id))
			{
				return null;
			}
			return this.LoadObject(new OIItem
			{
				ID = _id
			}, null, true, true);
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x00159297 File Offset: 0x00157697
		public ObjectCtrl AddFolder()
		{
			return this.LoadFolder(new OIFolder(), null, false);
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x001592A8 File Offset: 0x001576A8
		public ObjectCtrl DuplicateObject(ObjectCtrl _src)
		{
			if (_src == null)
			{
				return null;
			}
			ObjectCtrl result = null;
			int kind = _src.ObjectInfo.Kind;
			if (kind != 0)
			{
				if (kind == 1)
				{
					result = this.LoadFolder(new OIFolder(_src.ObjectInfo as OIFolder), _src.Parent, true);
				}
			}
			else
			{
				OIItem oiitem = _src.ObjectInfo as OIItem;
				if (!this.CheckLimitNum(oiitem.ID))
				{
					return null;
				}
				result = this.LoadObject(new OIItem(_src.ObjectInfo as OIItem), _src.Parent, false, true);
			}
			return result;
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x00159344 File Offset: 0x00157744
		public bool RestoreObject(ObjectCtrl _src, ObjectCtrl _parent, int _insert, bool _info = true)
		{
			int kind = _src.ObjectInfo.Kind;
			if (kind != 0)
			{
				return kind == 1 && this.RestoreFolder(_src as OCFolder, _parent, _insert, _info);
			}
			this.RestoreItem(_src as OCItem, _parent, _insert, _info);
			return this.CheckOverlap(_src as OCItem);
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x001593A0 File Offset: 0x001577A0
		public bool Load(string _path, bool _keep = true, bool _create = false)
		{
			this.DeleteObject();
			Vector3 limitSize = this.CraftInfo.LimitSize;
			int areaNo = this.CraftInfo.AreaNo;
			if (!this.CraftInfo.Load(_path))
			{
				return false;
			}
			if (_keep)
			{
				this.CraftInfo.LimitSize = limitSize;
				this.CraftInfo.AreaNo = areaNo;
			}
			this.LoadObject();
			if (_create)
			{
				this.CraftInfo.SetOverlapColliders(true);
			}
			return true;
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x00159418 File Offset: 0x00157818
		public bool LoadObject()
		{
			if (this.CraftInfo == null)
			{
				return false;
			}
			this.DeleteObject();
			for (int i = 0; i < this.CraftInfo.ObjectInfos.Count; i++)
			{
				IObjectInfo objectInfo = this.CraftInfo.ObjectInfos[i];
				this.CreateObjectCtrl(objectInfo, null, false);
			}
			return true;
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x00159476 File Offset: 0x00157876
		public void ResetObject()
		{
			this.DeleteObject();
			CraftInfo craftInfo = this.CraftInfo;
			if (craftInfo != null)
			{
				craftInfo.ObjectCtrls.Clear();
			}
			CraftInfo craftInfo2 = this.CraftInfo;
			if (craftInfo2 != null)
			{
				craftInfo2.ObjectInfos.Clear();
			}
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x001594B0 File Offset: 0x001578B0
		public bool CheckLimitNum(int _no)
		{
			Housing.LoadInfo loadInfo = null;
			if (!this.dicLoadInfo.TryGetValue(_no, out loadInfo))
			{
				return false;
			}
			if (loadInfo.limitNum < 0)
			{
				return true;
			}
			int usedNum = this.CraftInfo.GetUsedNum(_no);
			return usedNum < loadInfo.limitNum;
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x001594F8 File Offset: 0x001578F8
		public bool CheckOverlap(ObjectCtrl _oc)
		{
			bool flag = false;
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.CraftInfo.ObjectCtrls)
			{
				flag |= keyValuePair.Value.CheckOverlap(_oc, false);
				OCFolder ocfolder = _oc as OCFolder;
				if (ocfolder != null)
				{
					foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair2 in ocfolder.Child)
					{
						flag |= this.CheckOverlap(keyValuePair2.Value);
					}
				}
			}
			return flag;
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x001595CC File Offset: 0x001579CC
		public bool CheckOverlap()
		{
			bool flag = false;
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.CraftInfo.ObjectCtrls)
			{
				flag |= this.CheckOverlap(keyValuePair.Value);
			}
			return flag;
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x0015963C File Offset: 0x00157A3C
		public bool CheckSize(Vector3 _size)
		{
			Vector3 limitSize = this.CraftInfo.LimitSize;
			return _size.x <= limitSize.x && _size.y <= limitSize.y && _size.z <= limitSize.z;
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x00159694 File Offset: 0x00157A94
		private OCItem LoadObject(OIItem _oiItem, ObjectCtrl _parent, bool _new = false, bool _create = false)
		{
			OCItem ocitem = this.CreateOCItem(_oiItem);
			if (ocitem == null)
			{
				return null;
			}
			if (_parent == null)
			{
				this.AddInfoAndCtrl(this.CraftInfo.ObjectInfos, _oiItem, this.CraftInfo.ObjectCtrls, ocitem, -1);
			}
			else
			{
				ocitem.OnAttach(_parent, -1);
			}
			if (_new)
			{
				_oiItem.Pos = ocitem.ItemComponent.initPos;
				_oiItem.Color1 = ocitem.ItemComponent.defColor1;
				_oiItem.Color2 = ocitem.ItemComponent.defColor2;
				_oiItem.Color3 = ocitem.ItemComponent.defColor3;
				_oiItem.EmissionColor = ocitem.ItemComponent.defEmissionColor;
			}
			if (_create)
			{
				ocitem.ItemComponent.SetOverlapColliders(true);
			}
			ocitem.VisibleOption = ocitem.VisibleOption;
			ocitem.UpdateColor();
			ocitem.CalcTransform();
			ActionPoint[] actionPoints = ocitem.ActionPoints;
			if (!actionPoints.IsNullOrEmpty<ActionPoint>() && (_oiItem.ActionPoints.IsNullOrEmpty<int>() || actionPoints.Length != _oiItem.ActionPoints.Length))
			{
				_oiItem.ActionPoints = Enumerable.Repeat<int>(-1, actionPoints.Length).ToArray<int>();
			}
			for (int i = 0; i < actionPoints.Length; i++)
			{
				if (!(actionPoints[i] == null))
				{
					actionPoints[i].RegisterID = _oiItem.ActionPoints[i];
					_oiItem.ActionPoints[i] = Singleton<Map>.Instance.RegisterRuntimePoint(actionPoints[i]);
				}
			}
			FarmPoint[] farmPoints = ocitem.FarmPoints;
			if (!farmPoints.IsNullOrEmpty<FarmPoint>() && (_oiItem.FarmPoints.IsNullOrEmpty<int>() || farmPoints.Length != _oiItem.FarmPoints.Length))
			{
				_oiItem.FarmPoints = Enumerable.Repeat<int>(-1, farmPoints.Length).ToArray<int>();
			}
			for (int j = 0; j < farmPoints.Length; j++)
			{
				if (!(farmPoints[j] == null))
				{
					farmPoints[j].RegisterID = _oiItem.FarmPoints[j];
					_oiItem.FarmPoints[j] = Singleton<Map>.Instance.RegisterRuntimePoint(farmPoints[j]);
				}
			}
			PetHomePoint[] petHomePoints = ocitem.PetHomePoints;
			if (!petHomePoints.IsNullOrEmpty<PetHomePoint>() && (_oiItem.PetHomePoints.IsNullOrEmpty<int>() || petHomePoints.Length != _oiItem.PetHomePoints.Length))
			{
				_oiItem.PetHomePoints = Enumerable.Repeat<int>(-1, petHomePoints.Length).ToArray<int>();
			}
			for (int k = 0; k < petHomePoints.Length; k++)
			{
				if (!(petHomePoints[k] == null))
				{
					petHomePoints[k].RegisterID = _oiItem.PetHomePoints[k];
					_oiItem.PetHomePoints[k] = Singleton<Map>.Instance.RegisterRuntimePoint(petHomePoints[k]);
				}
			}
			JukePoint[] jukePoints = ocitem.JukePoints;
			if (!jukePoints.IsNullOrEmpty<JukePoint>() && (_oiItem.JukePoints.IsNullOrEmpty<int>() || jukePoints.Length != _oiItem.JukePoints.Length))
			{
				_oiItem.JukePoints = Enumerable.Repeat<int>(-1, jukePoints.Length).ToArray<int>();
			}
			for (int l = 0; l < jukePoints.Length; l++)
			{
				if (!(jukePoints[l] == null))
				{
					jukePoints[l].RegisterID = _oiItem.JukePoints[l];
					_oiItem.JukePoints[l] = Singleton<Map>.Instance.RegisterRuntimePoint(jukePoints[l]);
				}
			}
			CraftPoint[] craftPoints = ocitem.CraftPoints;
			if (!craftPoints.IsNullOrEmpty<CraftPoint>() && (_oiItem.CraftPoints.IsNullOrEmpty<int>() || craftPoints.Length != _oiItem.CraftPoints.Length))
			{
				_oiItem.CraftPoints = Enumerable.Repeat<int>(-1, craftPoints.Length).ToArray<int>();
			}
			for (int m = 0; m < craftPoints.Length; m++)
			{
				if (!(craftPoints[m] == null))
				{
					craftPoints[m].RegisterID = _oiItem.CraftPoints[m];
					_oiItem.CraftPoints[m] = Singleton<Map>.Instance.RegisterRuntimePoint(craftPoints[m]);
				}
			}
			LightSwitchPoint[] lightSwitchPoints = ocitem.LightSwitchPoints;
			if (!lightSwitchPoints.IsNullOrEmpty<LightSwitchPoint>() && (_oiItem.LightSwitchPoints.IsNullOrEmpty<int>() || lightSwitchPoints.Length != _oiItem.LightSwitchPoints.Length))
			{
				_oiItem.LightSwitchPoints = Enumerable.Repeat<int>(-1, lightSwitchPoints.Length).ToArray<int>();
			}
			for (int n = 0; n < lightSwitchPoints.Length; n++)
			{
				if (!(lightSwitchPoints[n] == null))
				{
					lightSwitchPoints[n].RegisterID = _oiItem.LightSwitchPoints[n];
					_oiItem.LightSwitchPoints[n] = Singleton<Map>.Instance.RegisterRuntimePoint(lightSwitchPoints[n]);
				}
			}
			return ocitem;
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x00159B24 File Offset: 0x00157F24
		private ObjectCtrl LoadFolder(OIFolder _oiFolder, ObjectCtrl _parent, bool _check)
		{
			ObjectCtrl objectCtrl = this.CreateOCFolder(_oiFolder);
			if (objectCtrl == null)
			{
				return null;
			}
			if (_parent == null)
			{
				this.AddInfoAndCtrl(this.CraftInfo.ObjectInfos, _oiFolder, this.CraftInfo.ObjectCtrls, objectCtrl, -1);
			}
			else
			{
				objectCtrl.OnAttach(_parent, -1);
			}
			objectCtrl.CalcTransform();
			List<IObjectInfo> list = new List<IObjectInfo>();
			foreach (IObjectInfo objectInfo in _oiFolder.Child)
			{
				if (this.CreateObjectCtrl(objectInfo, objectCtrl, _check) == null)
				{
					list.Add(objectInfo);
				}
			}
			if (!list.IsNullOrEmpty<IObjectInfo>())
			{
				foreach (IObjectInfo item in list)
				{
					_oiFolder.Child.Remove(item);
				}
			}
			return objectCtrl;
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x00159C3C File Offset: 0x0015803C
		private ObjectCtrl CreateObjectCtrl(IObjectInfo _objectInfo, ObjectCtrl _parent, bool _check = false)
		{
			ObjectCtrl result = null;
			int kind = _objectInfo.Kind;
			if (kind != 0)
			{
				if (kind == 1)
				{
					result = this.LoadFolder(_objectInfo as OIFolder, _parent, _check);
				}
			}
			else
			{
				OIItem oiitem = _objectInfo as OIItem;
				if (_check && !this.CheckLimitNum(oiitem.ID))
				{
					return null;
				}
				OIItem oiItem = _objectInfo as OIItem;
				result = this.LoadObject(oiItem, _parent, false, _check);
			}
			return result;
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x00159CB8 File Offset: 0x001580B8
		private OCItem CreateOCItem(OIItem _objectInfo)
		{
			Housing.LoadInfo loadInfo = null;
			if (!this.dicLoadInfo.TryGetValue(_objectInfo.ID, out loadInfo))
			{
				return null;
			}
			GameObject gameObject = this.LoadObject(_objectInfo);
			return new OCItem(_objectInfo, gameObject, this.CraftInfo, loadInfo);
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x00159CF8 File Offset: 0x001580F8
		private GameObject LoadObject(OIItem _objectInfo)
		{
			Housing.LoadInfo loadInfo = null;
			if (!this.dicLoadInfo.TryGetValue(_objectInfo.ID, out loadInfo))
			{
				return null;
			}
			GameObject gameObject = CommonLib.LoadAsset<GameObject>(loadInfo.filePath.bundle, loadInfo.filePath.file, true, loadInfo.filePath.manifest);
			if (gameObject == null)
			{
				return null;
			}
			gameObject.transform.SetParent(this.CraftInfo.ObjRoot.transform);
			return gameObject;
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x00159D74 File Offset: 0x00158174
		private ObjectCtrl CreateOCFolder(OIFolder _oiFolder)
		{
			GameObject gameObject = new GameObject(_oiFolder.Name);
			if (gameObject == null)
			{
				return null;
			}
			gameObject.transform.SetParent(this.CraftInfo.ObjRoot.transform);
			return new OCFolder(_oiFolder, gameObject, this.CraftInfo);
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x00159DC3 File Offset: 0x001581C3
		public void DeleteObject()
		{
			CraftInfo craftInfo = this.CraftInfo;
			if (craftInfo != null)
			{
				craftInfo.DeleteObject();
			}
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x00159DD9 File Offset: 0x001581D9
		public void DeleteObject(ObjectCtrl _objectCtrl)
		{
			if (_objectCtrl != null)
			{
				_objectCtrl.OnDelete();
			}
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x00159DEC File Offset: 0x001581EC
		private void RestoreItem(OCItem _ocItem, ObjectCtrl _parent, int _insert, bool _info)
		{
			_ocItem.RestoreObject(this.LoadObject(_ocItem.OIItem));
			if (_info)
			{
				if (_parent == null)
				{
					this.AddInfoAndCtrl(this.CraftInfo.ObjectInfos, _ocItem.ObjectInfo, this.CraftInfo.ObjectCtrls, _ocItem, _insert);
				}
				else
				{
					_ocItem.OnAttach(_parent, _insert);
				}
			}
			_ocItem.ItemComponent.SetOverlapColliders(true);
			_ocItem.VisibleOption = _ocItem.VisibleOption;
			_ocItem.UpdateColor();
			_ocItem.CalcTransform();
			if (Singleton<Map>.IsInstance())
			{
				OIItem oiitem = _ocItem.OIItem;
				ActionPoint[] actionPoints = _ocItem.ActionPoints;
				for (int i = 0; i < actionPoints.Length; i++)
				{
					if (!(actionPoints[i] == null))
					{
						actionPoints[i].RegisterID = oiitem.ActionPoints[i];
						Singleton<Map>.Instance.RemoveRegIDCache(actionPoints[i].RegisterID);
					}
				}
				FarmPoint[] farmPoints = _ocItem.FarmPoints;
				for (int j = 0; j < farmPoints.Length; j++)
				{
					if (!(farmPoints[j] == null))
					{
						farmPoints[j].RegisterID = oiitem.FarmPoints[j];
						foreach (FarmSection farmSection in farmPoints[j].HarvestSections)
						{
							farmSection.HarvestID = farmPoints[j].RegisterID;
						}
						Singleton<Map>.Instance.RemoveRegIDCache(farmPoints[j].RegisterID);
					}
				}
				PetHomePoint[] petHomePoints = _ocItem.PetHomePoints;
				for (int l = 0; l < petHomePoints.Length; l++)
				{
					if (!(petHomePoints[l] == null))
					{
						petHomePoints[l].RegisterID = oiitem.PetHomePoints[l];
						Singleton<Map>.Instance.RemoveRegIDCache(petHomePoints[l].RegisterID);
					}
				}
				JukePoint[] jukePoints = _ocItem.JukePoints;
				for (int m = 0; m < jukePoints.Length; m++)
				{
					if (!(jukePoints[m] == null))
					{
						jukePoints[m].RegisterID = oiitem.JukePoints[m];
						Singleton<Map>.Instance.RemoveRegIDCache(jukePoints[m].RegisterID);
					}
				}
				CraftPoint[] craftPoints = _ocItem.CraftPoints;
				for (int n = 0; n < craftPoints.Length; n++)
				{
					if (!(craftPoints[n] == null))
					{
						craftPoints[n].RegisterID = oiitem.CraftPoints[n];
						Singleton<Map>.Instance.RemoveRegIDCache(craftPoints[n].RegisterID);
					}
				}
				LightSwitchPoint[] lightSwitchPoints = _ocItem.LightSwitchPoints;
				for (int num = 0; num < lightSwitchPoints.Length; num++)
				{
					if (!(lightSwitchPoints[num] == null))
					{
						lightSwitchPoints[num].RegisterID = oiitem.LightSwitchPoints[num];
						Singleton<Map>.Instance.RemoveRegIDCache(lightSwitchPoints[num].RegisterID);
					}
				}
			}
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x0015A0D8 File Offset: 0x001584D8
		private bool RestoreFolder(OCFolder _ocFolder, ObjectCtrl _parent, int _insert, bool _info)
		{
			GameObject gameObject = new GameObject(_ocFolder.Name);
			if (gameObject == null)
			{
				return false;
			}
			gameObject.transform.SetParent(this.CraftInfo.ObjRoot.transform);
			_ocFolder.RestoreObject(gameObject);
			if (_info)
			{
				if (_parent == null)
				{
					this.AddInfoAndCtrl(this.CraftInfo.ObjectInfos, _ocFolder.ObjectInfo, this.CraftInfo.ObjectCtrls, _ocFolder, _insert);
				}
				else
				{
					_ocFolder.OnAttach(_parent, _insert);
				}
			}
			_ocFolder.CalcTransform();
			bool flag = false;
			Dictionary<IObjectInfo, ObjectCtrl> child = _ocFolder.Child;
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in child)
			{
				flag |= this.RestoreObject(keyValuePair.Value, _ocFolder, -1, false);
			}
			return flag;
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x0015A1C8 File Offset: 0x001585C8
		private void AddInfoAndCtrl(List<IObjectInfo> _objectInfos, IObjectInfo _objectInfo, Dictionary<IObjectInfo, ObjectCtrl> _objectCtrls, ObjectCtrl _objectCtrl, int _insert = -1)
		{
			if (!_objectInfos.Contains(_objectInfo))
			{
				if (_insert != -1)
				{
					_objectInfos.Insert(_insert, _objectInfo);
				}
				else
				{
					_objectInfos.Add(_objectInfo);
				}
			}
			if (!_objectCtrls.ContainsKey(_objectInfo))
			{
				_objectCtrls.Add(_objectInfo, _objectCtrl);
			}
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x0015A208 File Offset: 0x00158608
		private void AddCtrl(Dictionary<IObjectInfo, ObjectCtrl> _objectCtrls, IObjectInfo _objectInfo, ObjectCtrl _objectCtrl)
		{
			if (!_objectCtrls.ContainsKey(_objectInfo))
			{
				_objectCtrls.Add(_objectInfo, _objectCtrl);
			}
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x0015A220 File Offset: 0x00158620
		public bool LoadAsync(string _path, Action<bool> _afterAction)
		{
			this.DeleteObject();
			Vector3 limitSize = this.CraftInfo.LimitSize;
			int areaNo = this.CraftInfo.AreaNo;
			if (!this.CraftInfo.Load(_path))
			{
				Action<bool> afterAction = _afterAction;
				if (afterAction != null)
				{
					afterAction(false);
				}
				return false;
			}
			this.CraftInfo.LimitSize = limitSize;
			this.CraftInfo.AreaNo = areaNo;
			Observable.FromCoroutine(new Func<IEnumerator>(this.LoadAsync), false).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				Action<bool> afterAction2 = _afterAction;
				if (afterAction2 != null)
				{
					afterAction2(true);
				}
			});
			return true;
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x0015A2DC File Offset: 0x001586DC
		private IEnumerator LoadAsync()
		{
			if (this.CraftInfo == null)
			{
				yield break;
			}
			if (this.CraftInfo.ObjectInfos.IsNullOrEmpty<IObjectInfo>())
			{
				yield break;
			}
			this.waitTime = new Housing.WaitTime();
			foreach (IObjectInfo info in this.CraftInfo.ObjectInfos)
			{
				yield return this.CreateObjectCtrlAsync(info, null);
			}
			if (this.waitTime.isOver)
			{
				yield return null;
				this.waitTime.Next();
			}
			HashSet<OCItem> checkItem = new HashSet<OCItem>();
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.CraftInfo.ObjectCtrls)
			{
				this.GetOverlapTargets(keyValuePair.Value, checkItem);
			}
			foreach (OCItem v in checkItem)
			{
				v.BeforeCheckOverlap();
				if (this.waitTime.isOver)
				{
					yield return null;
					this.waitTime.Next();
				}
			}
			yield return null;
			foreach (OCItem oci in checkItem)
			{
				foreach (OCItem v2 in checkItem)
				{
					oci.CheckOverlap(v2, true);
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
				}
			}
			foreach (OCItem ocitem in checkItem)
			{
				ocitem.AfterCheckOverlap();
			}
			if (this.waitTime.isOver)
			{
				yield return null;
				this.waitTime.Next();
			}
			this.waitTime = null;
			yield break;
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x0015A2F8 File Offset: 0x001586F8
		private void GetOverlapTargets(ObjectCtrl _oc, HashSet<OCItem> _targets)
		{
			if (_oc is OCItem)
			{
				_targets.Add(_oc as OCItem);
				return;
			}
			OCFolder ocfolder = _oc as OCFolder;
			if (ocfolder != null)
			{
				foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in ocfolder.Child)
				{
					this.GetOverlapTargets(keyValuePair.Value, _targets);
				}
			}
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x0015A384 File Offset: 0x00158784
		private IEnumerator LoadObjectAsync(CraftInfo _craftInfo)
		{
			this.CraftInfo = _craftInfo;
			this.DeleteObject();
			if (this.waitTime.isOver)
			{
				yield return null;
				this.waitTime.Next();
			}
			for (int i = 0; i < this.CraftInfo.ObjectInfos.Count; i++)
			{
				yield return this.CreateObjectCtrlAsync(this.CraftInfo.ObjectInfos[i], null);
			}
			yield break;
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x0015A3A8 File Offset: 0x001587A8
		private IEnumerator CreateObjectCtrlAsync(IObjectInfo _objectInfo, ObjectCtrl _parent)
		{
			int kind = _objectInfo.Kind;
			if (kind != 0)
			{
				if (kind == 1)
				{
					yield return this.LoadFolderAsync(_objectInfo as OIFolder, _parent);
				}
			}
			else
			{
				this.LoadObject(_objectInfo as OIItem, _parent, false, false);
				if (this.waitTime.isOver)
				{
					yield return null;
					this.waitTime.Next();
				}
			}
			yield break;
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x0015A3D4 File Offset: 0x001587D4
		private IEnumerator LoadFolderAsync(OIFolder _oiFolder, ObjectCtrl _parent)
		{
			ObjectCtrl oc = this.CreateOCFolder(_oiFolder);
			if (oc == null)
			{
				yield break;
			}
			if (this.waitTime.isOver)
			{
				yield return null;
				this.waitTime.Next();
			}
			if (_parent == null)
			{
				this.AddInfoAndCtrl(this.CraftInfo.ObjectInfos, _oiFolder, this.CraftInfo.ObjectCtrls, oc, -1);
			}
			else
			{
				oc.OnAttach(_parent, -1);
			}
			oc.CalcTransform();
			Dictionary<IObjectInfo, ObjectCtrl> Child = (oc as OCFolder).Child;
			foreach (IObjectInfo c in _oiFolder.Child)
			{
				yield return this.CreateObjectCtrlAsync(c, oc);
			}
			yield break;
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x0015A400 File Offset: 0x00158800
		public bool[] CheckMOD(CraftInfo _craftInfo, Dictionary<IObjectInfo, int> _modObjects)
		{
			bool[] array = new bool[3];
			if (_craftInfo == null)
			{
				return array;
			}
			if (!_craftInfo.ObjectInfos.IsNullOrEmpty<IObjectInfo>())
			{
				foreach (IObjectInfo objectInfo in _craftInfo.ObjectInfos)
				{
					array[0] |= this.CheckMOD(objectInfo, _modObjects);
				}
			}
			Housing.AreaInfo areaInfo = null;
			if (this.dicAreaInfo.TryGetValue(_craftInfo.AreaNo, out areaInfo))
			{
				Housing.AreaSizeInfo areaSizeInfo = null;
				if (this.dicAreaSizeInfo.TryGetValue(areaInfo.size, out areaSizeInfo))
				{
					array[1] = (_craftInfo.LimitSize != areaSizeInfo.limitSize);
				}
				else
				{
					array[1] = true;
				}
			}
			else
			{
				array[1] = true;
			}
			array[2] = !this.dicAreaInfo.ContainsKey(_craftInfo.AreaNo);
			return array;
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x0015A500 File Offset: 0x00158900
		private bool CheckMOD(IObjectInfo _objectInfo, Dictionary<IObjectInfo, int> _modObjects)
		{
			bool flag = false;
			int kind = _objectInfo.Kind;
			if (kind != 0)
			{
				if (kind == 1)
				{
					OIFolder oifolder = _objectInfo as OIFolder;
					if (!oifolder.Child.IsNullOrEmpty<IObjectInfo>())
					{
						foreach (IObjectInfo objectInfo in oifolder.Child)
						{
							flag |= this.CheckMOD(objectInfo, _modObjects);
						}
					}
				}
			}
			else
			{
				OIItem oiitem = _objectInfo as OIItem;
				int num = 0;
				if (!this.dicCategoryInfo.ContainsKey(oiitem.Category))
				{
					num++;
				}
				if (!this.dicLoadInfo.ContainsKey(oiitem.ID))
				{
					num += 2;
				}
				flag = (num != 0);
				if (flag)
				{
					_modObjects.Add(_objectInfo, num);
				}
			}
			return flag;
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x0015A5FC File Offset: 0x001589FC
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			base.StartCoroutine("LoadExcelDataCoroutine");
		}

		// Token: 0x04003A11 RID: 14865
		public const string SavePath = "housing/";

		// Token: 0x04003A19 RID: 14873
		private Housing.WaitTime waitTime;

		// Token: 0x04003A1A RID: 14874
		private GameObject objScene;

		// Token: 0x04003A1B RID: 14875
		private CraftScene craftScene;

		// Token: 0x04003A1C RID: 14876
		private ItemComponent.ColInfo[] colInfos;

		// Token: 0x020008CF RID: 2255
		public class PathInfo
		{
			// Token: 0x17000AB8 RID: 2744
			// (get) Token: 0x06003B52 RID: 15186 RVA: 0x0015A6B9 File Offset: 0x00158AB9
			public bool IsNullOrEmpty
			{
				[CompilerGenerated]
				get
				{
					return this.bundle.IsNullOrEmpty() || this.file.IsNullOrEmpty();
				}
			}

			// Token: 0x04003A25 RID: 14885
			public string bundle = string.Empty;

			// Token: 0x04003A26 RID: 14886
			public string file = string.Empty;

			// Token: 0x04003A27 RID: 14887
			public string manifest = string.Empty;
		}

		// Token: 0x020008D0 RID: 2256
		public class RequiredMaterial
		{
			// Token: 0x06003B53 RID: 15187 RVA: 0x0015A6D9 File Offset: 0x00158AD9
			public RequiredMaterial()
			{
			}

			// Token: 0x06003B54 RID: 15188 RVA: 0x0015A6F8 File Offset: 0x00158AF8
			public RequiredMaterial(string[] _s)
			{
				_s.SafeProc(0, delegate(string s)
				{
					int.TryParse(s, out this.category);
				});
				_s.SafeProc(1, delegate(string s)
				{
					int.TryParse(s, out this.no);
				});
				_s.SafeProc(2, delegate(string s)
				{
					int.TryParse(s, out this.num);
				});
			}

			// Token: 0x04003A28 RID: 14888
			public int category = -1;

			// Token: 0x04003A29 RID: 14889
			public int no = -1;

			// Token: 0x04003A2A RID: 14890
			public int num = -1;
		}

		// Token: 0x020008D1 RID: 2257
		public class LoadInfo
		{
			// Token: 0x06003B58 RID: 15192 RVA: 0x0015A78C File Offset: 0x00158B8C
			public LoadInfo(int _package, List<string> _lst)
			{
				int num = 1;
				this.package = _package;
				this.category = int.Parse(_lst[num++]);
				this.name = _lst[num++];
				this.text = _lst[num++];
				this.filePath.bundle = _lst.SafeGet(num++);
				this.filePath.file = _lst.SafeGet(num++);
				this.filePath.manifest = _lst.SafeGet(num++);
				this.thumbnailPath.bundle = _lst.SafeGet(num++);
				this.thumbnailPath.file = _lst.SafeGet(num++);
				this.thumbnailPath.manifest = _lst.SafeGet(num++);
				this.size = new Vector3(this.TryParseF(_lst.SafeGet(num++)), this.TryParseF(_lst.SafeGet(num++)), this.TryParseF(_lst.SafeGet(num++)));
				List<Housing.RequiredMaterial> list = new List<Housing.RequiredMaterial>();
				for (int i = 0; i < 8; i++)
				{
					string text = _lst.SafeGet(num++);
					if (!text.IsNullOrEmpty())
					{
						list.Add(new Housing.RequiredMaterial(text.Split(new char[]
						{
							'/'
						})));
					}
				}
				this.requiredMaterials = list.ToArray();
				bool.TryParse(_lst.SafeGet(num++), out this.useOption);
				this.isAccess = this.TryParseIntToBool(_lst.SafeGet(num++));
				this.isAction = this.TryParseIntToBool(_lst.SafeGet(num++));
				this.isHPoint = this.TryParseIntToBool(_lst.SafeGet(num++));
				if (!int.TryParse(_lst.SafeGet(num++), out this.limitNum))
				{
					this.limitNum = -1;
				}
			}

			// Token: 0x17000AB9 RID: 2745
			// (get) Token: 0x06003B59 RID: 15193 RVA: 0x0015A9BA File Offset: 0x00158DBA
			public int Category
			{
				[CompilerGenerated]
				get
				{
					return 1 << this.category;
				}
			}

			// Token: 0x06003B5A RID: 15194 RVA: 0x0015A9C8 File Offset: 0x00158DC8
			private float TryParseF(string _str)
			{
				float result = 0f;
				float.TryParse(_str, out result);
				return result;
			}

			// Token: 0x06003B5B RID: 15195 RVA: 0x0015A9E8 File Offset: 0x00158DE8
			private bool TryParseIntToBool(string _str)
			{
				int num = 0;
				return int.TryParse(_str, out num) && num == 1;
			}

			// Token: 0x04003A2B RID: 14891
			public int package;

			// Token: 0x04003A2C RID: 14892
			public int category;

			// Token: 0x04003A2D RID: 14893
			public string name = string.Empty;

			// Token: 0x04003A2E RID: 14894
			public string text = string.Empty;

			// Token: 0x04003A2F RID: 14895
			public Housing.PathInfo filePath = new Housing.PathInfo();

			// Token: 0x04003A30 RID: 14896
			public Housing.PathInfo thumbnailPath = new Housing.PathInfo();

			// Token: 0x04003A31 RID: 14897
			public Vector3 size = Vector3.one;

			// Token: 0x04003A32 RID: 14898
			public Housing.RequiredMaterial[] requiredMaterials;

			// Token: 0x04003A33 RID: 14899
			public bool useOption;

			// Token: 0x04003A34 RID: 14900
			public bool isAccess;

			// Token: 0x04003A35 RID: 14901
			public bool isAction;

			// Token: 0x04003A36 RID: 14902
			public bool isHPoint;

			// Token: 0x04003A37 RID: 14903
			public int limitNum = -1;
		}

		// Token: 0x020008D2 RID: 2258
		public class CategoryInfo
		{
			// Token: 0x06003B5C RID: 15196 RVA: 0x0015AA10 File Offset: 0x00158E10
			public CategoryInfo(List<string> _lst)
			{
				int num = 1;
				this.name = _lst.SafeGet(num++);
				this.thumbnailPath.bundle = _lst.SafeGet(num++);
				this.thumbnailPath.file = _lst.SafeGet(num++);
				this.thumbnailPath.manifest = _lst.SafeGet(num++);
			}

			// Token: 0x17000ABA RID: 2746
			// (get) Token: 0x06003B5D RID: 15197 RVA: 0x0015AA90 File Offset: 0x00158E90
			public Texture2D Thumbnail
			{
				[CompilerGenerated]
				get
				{
					string bundle = this.thumbnailPath.bundle;
					string file = this.thumbnailPath.file;
					string manifest = this.thumbnailPath.manifest;
					return CommonLib.LoadAsset<Texture2D>(bundle, file, false, manifest);
				}
			}

			// Token: 0x04003A38 RID: 14904
			public string name = string.Empty;

			// Token: 0x04003A39 RID: 14905
			public Housing.PathInfo thumbnailPath = new Housing.PathInfo();
		}

		// Token: 0x020008D3 RID: 2259
		public class AreaSizeInfo
		{
			// Token: 0x06003B5E RID: 15198 RVA: 0x0015AACC File Offset: 0x00158ECC
			public AreaSizeInfo(List<string> _lst)
			{
				int num = 0;
				this.no = int.Parse(_lst.SafeGet(num++));
				this.limitSize.Set(int.Parse(_lst.SafeGet(num++)), int.Parse(_lst.SafeGet(num++)), int.Parse(_lst.SafeGet(num++)));
				string[] array = _lst.SafeGet(num++).Split(new char[]
				{
					'/'
				});
				if (!array.IsNullOrEmpty<string>())
				{
					foreach (string s in array)
					{
						int item = 0;
						if (int.TryParse(s, out item))
						{
							this.compatibility.Add(item);
						}
					}
				}
				this.compatibility.Add(this.no);
			}

			// Token: 0x04003A3A RID: 14906
			public int no;

			// Token: 0x04003A3B RID: 14907
			public Vector3Int limitSize = new Vector3Int(100, 80, 100);

			// Token: 0x04003A3C RID: 14908
			public HashSet<int> compatibility = new HashSet<int>();
		}

		// Token: 0x020008D4 RID: 2260
		public class AreaInfo
		{
			// Token: 0x06003B5F RID: 15199 RVA: 0x0015ABC4 File Offset: 0x00158FC4
			public AreaInfo(List<string> _lst)
			{
				int num = 0;
				this.no = int.Parse(_lst.SafeGet(num++));
				this.size = int.Parse(_lst.SafeGet(num++));
				this.presetPath.bundle = _lst.SafeGet(num++);
				this.presetPath.file = _lst.SafeGet(num++);
				this.presetPath.manifest = _lst.SafeGet(num++);
			}

			// Token: 0x04003A3D RID: 14909
			public int no;

			// Token: 0x04003A3E RID: 14910
			public int size;

			// Token: 0x04003A3F RID: 14911
			public Housing.PathInfo presetPath = new Housing.PathInfo();
		}

		// Token: 0x020008D5 RID: 2261
		private class WaitTime
		{
			// Token: 0x06003B60 RID: 15200 RVA: 0x0015AC52 File Offset: 0x00159052
			public WaitTime()
			{
				this.Next();
			}

			// Token: 0x17000ABB RID: 2747
			// (get) Token: 0x06003B61 RID: 15201 RVA: 0x0015AC60 File Offset: 0x00159060
			public bool isOver
			{
				get
				{
					return Time.realtimeSinceStartup >= this.nextFrameTime;
				}
			}

			// Token: 0x06003B62 RID: 15202 RVA: 0x0015AC72 File Offset: 0x00159072
			public void Next()
			{
				this.nextFrameTime = Time.realtimeSinceStartup + 0.03f;
			}

			// Token: 0x04003A40 RID: 14912
			private const float intervalTime = 0.03f;

			// Token: 0x04003A41 RID: 14913
			private float nextFrameTime;
		}

		// Token: 0x020008D6 RID: 2262
		private class FileListInfo
		{
			// Token: 0x06003B63 RID: 15203 RVA: 0x0015AC88 File Offset: 0x00159088
			public FileListInfo(List<string> _list)
			{
				this.dicFile = _list.ToDictionary((string s) => s, (string s) => AssetBundleCheck.GetAllAssetName(s, false, null, true));
			}

			// Token: 0x06003B64 RID: 15204 RVA: 0x0015ACE4 File Offset: 0x001590E4
			public bool Check(string _path, string _file)
			{
				string[] source = null;
				if (!AssetBundleCheck.IsSimulation)
				{
					_file = _file.ToLower();
				}
				return this.dicFile.TryGetValue(_path, out source) && source.Contains(_file);
			}

			// Token: 0x06003B65 RID: 15205 RVA: 0x0015AD28 File Offset: 0x00159128
			public string[] FindRegex(string _path, string _regex)
			{
				string[] source = null;
				if (!AssetBundleCheck.IsSimulation)
				{
					_regex = _regex.ToLower();
				}
				return (!this.dicFile.TryGetValue(_path, out source)) ? null : (from s in source
				where Regex.Match(s, _regex, RegexOptions.IgnoreCase).Success
				select s).ToArray<string>();
			}

			// Token: 0x04003A42 RID: 14914
			public Dictionary<string, string[]> dicFile;
		}
	}
}
