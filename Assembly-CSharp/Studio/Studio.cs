using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Illusion.Elements.Xml;
using Manager;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001282 RID: 4738
	public sealed class Studio : Singleton<Studio>
	{
		// Token: 0x1700217D RID: 8573
		// (get) Token: 0x06009C9B RID: 40091 RVA: 0x004015BC File Offset: 0x003FF9BC
		public TreeNodeCtrl treeNodeCtrl
		{
			get
			{
				return this.m_TreeNodeCtrl;
			}
		}

		// Token: 0x1700217E RID: 8574
		// (get) Token: 0x06009C9C RID: 40092 RVA: 0x004015C4 File Offset: 0x003FF9C4
		public RootButtonCtrl rootButtonCtrl
		{
			get
			{
				return this.m_RootButtonCtrl;
			}
		}

		// Token: 0x1700217F RID: 8575
		// (get) Token: 0x06009C9D RID: 40093 RVA: 0x004015CC File Offset: 0x003FF9CC
		public ManipulatePanelCtrl manipulatePanelCtrl
		{
			get
			{
				return this._manipulatePanelCtrl;
			}
		}

		// Token: 0x17002180 RID: 8576
		// (get) Token: 0x06009C9E RID: 40094 RVA: 0x004015D4 File Offset: 0x003FF9D4
		public CameraControl cameraCtrl
		{
			get
			{
				return this.m_CameraCtrl;
			}
		}

		// Token: 0x17002181 RID: 8577
		// (get) Token: 0x06009C9F RID: 40095 RVA: 0x004015DC File Offset: 0x003FF9DC
		public SystemButtonCtrl systemButtonCtrl
		{
			get
			{
				return this.m_SystemButtonCtrl;
			}
		}

		// Token: 0x17002182 RID: 8578
		// (get) Token: 0x06009CA0 RID: 40096 RVA: 0x004015E4 File Offset: 0x003FF9E4
		public BGMCtrl bgmCtrl
		{
			get
			{
				return this.sceneInfo.bgmCtrl;
			}
		}

		// Token: 0x17002183 RID: 8579
		// (get) Token: 0x06009CA1 RID: 40097 RVA: 0x004015F1 File Offset: 0x003FF9F1
		public ENVCtrl envCtrl
		{
			get
			{
				return this.sceneInfo.envCtrl;
			}
		}

		// Token: 0x17002184 RID: 8580
		// (get) Token: 0x06009CA2 RID: 40098 RVA: 0x004015FE File Offset: 0x003FF9FE
		public OutsideSoundCtrl outsideSoundCtrl
		{
			get
			{
				return this.sceneInfo.outsideSoundCtrl;
			}
		}

		// Token: 0x17002185 RID: 8581
		// (get) Token: 0x06009CA3 RID: 40099 RVA: 0x0040160B File Offset: 0x003FFA0B
		public CameraLightCtrl cameraLightCtrl
		{
			get
			{
				return this.m_CameraLightCtrl;
			}
		}

		// Token: 0x17002186 RID: 8582
		// (get) Token: 0x06009CA4 RID: 40100 RVA: 0x00401613 File Offset: 0x003FFA13
		public MapList mapList
		{
			get
			{
				return this._mapList;
			}
		}

		// Token: 0x17002187 RID: 8583
		// (get) Token: 0x06009CA5 RID: 40101 RVA: 0x0040161B File Offset: 0x003FFA1B
		public ColorPalette colorPalette
		{
			get
			{
				return this._colorPalette;
			}
		}

		// Token: 0x17002188 RID: 8584
		// (get) Token: 0x06009CA6 RID: 40102 RVA: 0x00401623 File Offset: 0x003FFA23
		public PatternSelectListCtrl patternSelectListCtrl
		{
			get
			{
				return this._patternSelectListCtrl;
			}
		}

		// Token: 0x17002189 RID: 8585
		// (get) Token: 0x06009CA7 RID: 40103 RVA: 0x0040162B File Offset: 0x003FFA2B
		public GameScreenShot gameScreenShot
		{
			get
			{
				return this._gameScreenShot;
			}
		}

		// Token: 0x1700218A RID: 8586
		// (get) Token: 0x06009CA8 RID: 40104 RVA: 0x00401633 File Offset: 0x003FFA33
		public FrameCtrl frameCtrl
		{
			get
			{
				return this._frameCtrl;
			}
		}

		// Token: 0x1700218B RID: 8587
		// (get) Token: 0x06009CA9 RID: 40105 RVA: 0x0040163B File Offset: 0x003FFA3B
		public LogoList logoList
		{
			get
			{
				return this._logoList;
			}
		}

		// Token: 0x1700218C RID: 8588
		// (get) Token: 0x06009CAA RID: 40106 RVA: 0x00401644 File Offset: 0x003FFA44
		public bool isInputNow
		{
			get
			{
				return (!this._inputFieldNow) ? (this._inputFieldTMPNow && this._inputFieldTMPNow.isFocused) : this._inputFieldNow.isFocused;
			}
		}

		// Token: 0x1700218D RID: 8589
		// (get) Token: 0x06009CAB RID: 40107 RVA: 0x00401692 File Offset: 0x003FFA92
		public CameraSelector cameraSelector
		{
			get
			{
				return this._cameraSelector;
			}
		}

		// Token: 0x1700218E RID: 8590
		// (get) Token: 0x06009CAC RID: 40108 RVA: 0x0040169A File Offset: 0x003FFA9A
		public Texture textureLine
		{
			get
			{
				return this._textureLine;
			}
		}

		// Token: 0x1700218F RID: 8591
		// (get) Token: 0x06009CAD RID: 40109 RVA: 0x004016A2 File Offset: 0x003FFAA2
		public RouteControl routeControl
		{
			get
			{
				return this._routeControl;
			}
		}

		// Token: 0x17002190 RID: 8592
		// (get) Token: 0x06009CAE RID: 40110 RVA: 0x004016AA File Offset: 0x003FFAAA
		// (set) Token: 0x06009CAF RID: 40111 RVA: 0x004016B2 File Offset: 0x003FFAB2
		public SceneInfo sceneInfo { get; private set; }

		// Token: 0x17002191 RID: 8593
		// (get) Token: 0x06009CB0 RID: 40112 RVA: 0x004016BB File Offset: 0x003FFABB
		// (set) Token: 0x06009CB1 RID: 40113 RVA: 0x004016C2 File Offset: 0x003FFAC2
		public static OptionSystem optionSystem { get; private set; }

		// Token: 0x17002192 RID: 8594
		// (get) Token: 0x06009CB2 RID: 40114 RVA: 0x004016CA File Offset: 0x003FFACA
		// (set) Token: 0x06009CB3 RID: 40115 RVA: 0x004016D2 File Offset: 0x003FFAD2
		public OCICamera ociCamera
		{
			get
			{
				return this._ociCamera;
			}
			private set
			{
				this._ociCamera = value;
			}
		}

		// Token: 0x17002193 RID: 8595
		// (get) Token: 0x06009CB4 RID: 40116 RVA: 0x004016DB File Offset: 0x003FFADB
		// (set) Token: 0x06009CB5 RID: 40117 RVA: 0x004016E3 File Offset: 0x003FFAE3
		public int cameraCount { get; private set; }

		// Token: 0x17002194 RID: 8596
		// (get) Token: 0x06009CB6 RID: 40118 RVA: 0x004016EC File Offset: 0x003FFAEC
		// (set) Token: 0x06009CB7 RID: 40119 RVA: 0x004016F4 File Offset: 0x003FFAF4
		public bool isVRMode { get; private set; }

		// Token: 0x06009CB8 RID: 40120 RVA: 0x00401700 File Offset: 0x003FFB00
		public void AddFemale(string _path)
		{
			OCICharFemale ocicharFemale = AddObjectFemale.Add(_path);
			Singleton<UndoRedoManager>.Instance.Clear();
			if (Studio.optionSystem.autoHide)
			{
				this.rootButtonCtrl.OnClick(-1);
			}
			if (Studio.optionSystem.autoSelect && ocicharFemale != null)
			{
				this.m_TreeNodeCtrl.SelectSingle(ocicharFemale.treeNodeObject, true);
			}
		}

		// Token: 0x06009CB9 RID: 40121 RVA: 0x00401760 File Offset: 0x003FFB60
		private IEnumerator AddFemaleCoroutine(string _path)
		{
			AddObjectFemale.NecessaryInfo ni = new AddObjectFemale.NecessaryInfo(_path);
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioWait",
				isAdd = true
			}, false);
			yield return null;
			yield return AddObjectFemale.AddCoroutine(ni);
			Singleton<Scene>.Instance.UnLoad();
			Singleton<UndoRedoManager>.Instance.Clear();
			if (Studio.optionSystem.autoHide)
			{
				this.rootButtonCtrl.OnClick(-1);
			}
			if (Studio.optionSystem.autoSelect && ni.ocicf != null)
			{
				this.m_TreeNodeCtrl.SelectSingle(ni.ocicf.treeNodeObject, true);
			}
			yield break;
		}

		// Token: 0x06009CBA RID: 40122 RVA: 0x00401784 File Offset: 0x003FFB84
		public void AddMale(string _path)
		{
			OCICharMale ocicharMale = AddObjectMale.Add(_path);
			Singleton<UndoRedoManager>.Instance.Clear();
			if (Studio.optionSystem.autoHide)
			{
				this.rootButtonCtrl.OnClick(-1);
			}
			if (Studio.optionSystem.autoSelect && ocicharMale != null)
			{
				this.m_TreeNodeCtrl.SelectSingle(ocicharMale.treeNodeObject, true);
			}
		}

		// Token: 0x06009CBB RID: 40123 RVA: 0x004017E4 File Offset: 0x003FFBE4
		public void AddMap(int _no, bool _close = true, bool _wait = true, bool _coroutine = true)
		{
			if (_coroutine)
			{
				base.StartCoroutine(this.AddMapCoroutine(_no, _close, _wait));
			}
			else
			{
				Singleton<Map>.Instance.LoadMap(_no);
				this.SetupMap(_no, _close);
			}
		}

		// Token: 0x06009CBC RID: 40124 RVA: 0x00401818 File Offset: 0x003FFC18
		private IEnumerator AddMapCoroutine(int _no, bool _close, bool _wait)
		{
			yield return Singleton<Map>.Instance.LoadMapCoroutine(_no, _wait);
			this.SetupMap(_no, _close);
			yield break;
		}

		// Token: 0x06009CBD RID: 40125 RVA: 0x00401848 File Offset: 0x003FFC48
		private void SetupMap(int _no, bool _close)
		{
			this.sceneInfo.mapInfo.no = _no;
			this.sceneInfo.mapInfo.ca.Reset();
			if (this.onChangeMap != null)
			{
				this.onChangeMap();
			}
			this.m_CameraCtrl.CloerListCollider();
			Info.MapLoadInfo mapLoadInfo = null;
			if (Singleton<Info>.Instance.dicMapLoadInfo.TryGetValue(Singleton<Map>.Instance.no, out mapLoadInfo))
			{
				this.m_CameraCtrl.LoadVanish(mapLoadInfo.vanish.bundlePath, mapLoadInfo.vanish.fileName, Singleton<Map>.Instance.MapRoot);
			}
			if (_close)
			{
				this.rootButtonCtrl.OnClick(-1);
			}
		}

		// Token: 0x06009CBE RID: 40126 RVA: 0x004018FC File Offset: 0x003FFCFC
		public void AddItem(int _group, int _category, int _no)
		{
			OCIItem ociitem = AddObjectItem.Add(_group, _category, _no);
			Singleton<UndoRedoManager>.Instance.Clear();
			if (Studio.optionSystem.autoHide)
			{
				this.rootButtonCtrl.OnClick(-1);
			}
			if (Studio.optionSystem.autoSelect && ociitem != null)
			{
				this.m_TreeNodeCtrl.SelectSingle(ociitem.treeNodeObject, true);
			}
		}

		// Token: 0x06009CBF RID: 40127 RVA: 0x00401960 File Offset: 0x003FFD60
		public void AddLight(int _no)
		{
			OCILight ocilight = AddObjectLight.Add(_no);
			Singleton<UndoRedoManager>.Instance.Clear();
			if (Studio.optionSystem.autoHide)
			{
				this.rootButtonCtrl.OnClick(-1);
			}
			if (Studio.optionSystem.autoSelect && ocilight != null)
			{
				this.m_TreeNodeCtrl.SelectSingle(ocilight.treeNodeObject, true);
			}
		}

		// Token: 0x06009CC0 RID: 40128 RVA: 0x004019C0 File Offset: 0x003FFDC0
		public void AddFolder()
		{
			OCIFolder ocifolder = AddObjectFolder.Add();
			Singleton<UndoRedoManager>.Instance.Clear();
			if (Studio.optionSystem.autoSelect && ocifolder != null)
			{
				this.m_TreeNodeCtrl.SelectSingle(ocifolder.treeNodeObject, true);
			}
		}

		// Token: 0x06009CC1 RID: 40129 RVA: 0x00401A04 File Offset: 0x003FFE04
		public void AddCamera()
		{
			if (this.cameraCount != 2147483647)
			{
				this.cameraCount++;
			}
			OCICamera ocicamera = AddObjectCamera.Add();
			Singleton<UndoRedoManager>.Instance.Clear();
			if (Studio.optionSystem.autoSelect && ocicamera != null)
			{
				this.m_TreeNodeCtrl.SelectSingle(ocicamera.treeNodeObject, true);
			}
			this._cameraSelector.Init();
		}

		// Token: 0x06009CC2 RID: 40130 RVA: 0x00401A74 File Offset: 0x003FFE74
		public void ChangeCamera(OCICamera _ociCamera, bool _active, bool _force = false)
		{
			if (_active)
			{
				if (this.ociCamera != null && this.ociCamera != _ociCamera)
				{
					this.ociCamera.SetActive(false);
					this.ociCamera = null;
				}
				if (_ociCamera != null)
				{
					_ociCamera.SetActive(true);
					this.ociCamera = _ociCamera;
				}
			}
			else if (_force)
			{
				if (this.ociCamera != null)
				{
					this.ociCamera.SetActive(false);
				}
				if (_ociCamera != null)
				{
					_ociCamera.SetActive(false);
				}
				this.ociCamera = null;
			}
			else if (this.ociCamera == _ociCamera)
			{
				if (_ociCamera != null)
				{
					_ociCamera.SetActive(false);
				}
				this.ociCamera = null;
			}
			Singleton<Studio>.Instance.cameraCtrl.IsOutsideSetting = (this.ociCamera != null);
			this.textCamera.text = ((this.ociCamera != null) ? this.ociCamera.cameraInfo.name : "-");
			this._cameraSelector.SetCamera(this.ociCamera);
		}

		// Token: 0x06009CC3 RID: 40131 RVA: 0x00401B7C File Offset: 0x003FFF7C
		public void ChangeCamera(OCICamera _ociCamera)
		{
			this.ChangeCamera(_ociCamera, this.ociCamera != _ociCamera, false);
		}

		// Token: 0x06009CC4 RID: 40132 RVA: 0x00401B94 File Offset: 0x003FFF94
		public void DeleteCamera(OCICamera _ociCamera)
		{
			if (this.ociCamera != _ociCamera)
			{
				this._cameraSelector.Init();
				return;
			}
			this.ociCamera.SetActive(false);
			this.ociCamera = null;
			Singleton<Studio>.Instance.cameraCtrl.enabled = true;
			this.textCamera.text = "-";
			this._cameraSelector.Init();
		}

		// Token: 0x06009CC5 RID: 40133 RVA: 0x00401BF8 File Offset: 0x003FFFF8
		public void AddRoute()
		{
			OCIRoute ociroute = AddObjectRoute.Add();
			if (this._routeControl.visible)
			{
				this._routeControl.Init();
			}
			Singleton<UndoRedoManager>.Instance.Clear();
			if (Studio.optionSystem.autoSelect && ociroute != null)
			{
				this.m_TreeNodeCtrl.SelectSingle(ociroute.treeNodeObject, true);
			}
		}

		// Token: 0x06009CC6 RID: 40134 RVA: 0x00401C57 File Offset: 0x00400057
		public void SetDepthOfFieldForcus(int _key)
		{
			this.m_SystemButtonCtrl.SetDepthOfFieldForcus(_key);
		}

		// Token: 0x06009CC7 RID: 40135 RVA: 0x00401C65 File Offset: 0x00400065
		public void SetSunCaster(int _key)
		{
			this.m_SystemButtonCtrl.SetSunCaster(_key);
		}

		// Token: 0x06009CC8 RID: 40136 RVA: 0x00401C74 File Offset: 0x00400074
		public void UpdateCharaFKColor()
		{
			foreach (KeyValuePair<int, ObjectCtrlInfo> keyValuePair in from v in this.dicObjectCtrl
			where v.Value is OCIChar
			select v)
			{
				(keyValuePair.Value as OCIChar).UpdateFKColor(FKCtrl.parts);
			}
		}

		// Token: 0x06009CC9 RID: 40137 RVA: 0x00401D00 File Offset: 0x00400100
		public void UpdateItemFKColor()
		{
			foreach (KeyValuePair<int, ObjectCtrlInfo> keyValuePair in from v in this.dicObjectCtrl
			where v.Value is OCIItem
			select v)
			{
				(keyValuePair.Value as OCIItem).UpdateFKColor();
			}
		}

		// Token: 0x06009CCA RID: 40138 RVA: 0x00401D88 File Offset: 0x00400188
		public void SetVisibleGimmick()
		{
			bool visibleGimmick = this.workInfo.visibleGimmick;
			foreach (OCIItem ociitem in from v in this.dicObjectCtrl
			where v.Value is OCIItem
			select v.Value as OCIItem)
			{
				ociitem.VisibleIcon = visibleGimmick;
			}
		}

		// Token: 0x06009CCB RID: 40139 RVA: 0x00401E34 File Offset: 0x00400234
		public void Duplicate()
		{
			Dictionary<int, ObjectInfo> dictionary = new Dictionary<int, ObjectInfo>();
			Dictionary<int, Studio.DuplicateParentInfo> dictionary2 = new Dictionary<int, Studio.DuplicateParentInfo>();
			TreeNodeObject[] selectNodes = this.treeNodeCtrl.selectNodes;
			for (int i = 0; i < selectNodes.Length; i++)
			{
				this.SavePreprocessingLoop(selectNodes[i]);
				ObjectCtrlInfo objectCtrlInfo = null;
				if (this.dicInfo.TryGetValue(selectNodes[i], out objectCtrlInfo))
				{
					dictionary.Add(objectCtrlInfo.objectInfo.dicKey, objectCtrlInfo.objectInfo);
					if (objectCtrlInfo.parentInfo != null)
					{
						dictionary2.Add(objectCtrlInfo.objectInfo.dicKey, new Studio.DuplicateParentInfo(objectCtrlInfo.parentInfo, objectCtrlInfo.treeNodeObject.parent));
					}
				}
			}
			if (dictionary.Count == 0)
			{
				return;
			}
			byte[] buffer = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					this.sceneInfo.Save(binaryWriter, dictionary);
					buffer = memoryStream.ToArray();
				}
			}
			using (MemoryStream memoryStream2 = new MemoryStream(buffer))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream2))
				{
					this.sceneInfo.Import(binaryReader, this.sceneInfo.version);
				}
			}
			foreach (KeyValuePair<int, ObjectInfo> keyValuePair in this.sceneInfo.dicImport)
			{
				Studio.DuplicateParentInfo duplicateParentInfo = null;
				if (dictionary2.TryGetValue(this.sceneInfo.dicChangeKey[keyValuePair.Key], out duplicateParentInfo))
				{
					AddObjectAssist.LoadChild(keyValuePair.Value, duplicateParentInfo.info, duplicateParentInfo.node);
				}
				else
				{
					AddObjectAssist.LoadChild(keyValuePair.Value, null, null);
				}
			}
			if (this._routeControl.visible)
			{
				this._routeControl.Init();
			}
			this.treeNodeCtrl.RefreshHierachy();
			this._cameraSelector.Init();
		}

		// Token: 0x06009CCC RID: 40140 RVA: 0x00402094 File Offset: 0x00400494
		public void SaveScene()
		{
			foreach (KeyValuePair<int, ObjectCtrlInfo> keyValuePair in this.dicObjectCtrl)
			{
				keyValuePair.Value.OnSavePreprocessing();
			}
			this.sceneInfo.cameraSaveData = this.m_CameraCtrl.Export();
			DateTime now = DateTime.Now;
			string str = string.Format("{0}_{1:00}{2:00}_{3:00}{4:00}_{5:00}_{6:000}.png", new object[]
			{
				now.Year,
				now.Month,
				now.Day,
				now.Hour,
				now.Minute,
				now.Second,
				now.Millisecond
			});
			string path = UserData.Create("studio/scene") + str;
			this.sceneInfo.Save(path);
		}

		// Token: 0x06009CCD RID: 40141 RVA: 0x004021AC File Offset: 0x004005AC
		public bool LoadScene(string _path)
		{
			if (!File.Exists(_path))
			{
				return false;
			}
			this.InitScene(false);
			this.sceneInfo = new SceneInfo();
			if (!this.sceneInfo.Load(_path))
			{
				return false;
			}
			AddObjectAssist.LoadChild(this.sceneInfo.dicObject, null, null);
			ChangeAmount source = this.sceneInfo.mapInfo.ca.Clone();
			this.AddMap(this.sceneInfo.mapInfo.no, false, false, true);
			this.mapList.UpdateInfo();
			this.sceneInfo.mapInfo.ca.Copy(source, true, true, true);
			Singleton<MapCtrl>.Instance.Reflect();
			this.bgmCtrl.Play(this.bgmCtrl.no);
			this.envCtrl.Play(this.envCtrl.no);
			this.outsideSoundCtrl.Play(this.outsideSoundCtrl.fileName);
			this.m_BackgroundCtrl.Load(this.sceneInfo.background);
			this.m_BackgroundList.UpdateUI();
			this._frameCtrl.Load(this.sceneInfo.frame);
			this._frameList.UpdateUI();
			this.m_SystemButtonCtrl.UpdateInfo();
			this.treeNodeCtrl.RefreshHierachy();
			if (this.sceneInfo.cameraSaveData != null)
			{
				this.m_CameraCtrl.Import(this.sceneInfo.cameraSaveData);
			}
			this.cameraLightCtrl.Reflect();
			this._cameraSelector.Init();
			this.sceneInfo.dataVersion = this.sceneInfo.version;
			this.rootButtonCtrl.OnClick(-1);
			return true;
		}

		// Token: 0x06009CCE RID: 40142 RVA: 0x00402358 File Offset: 0x00400758
		public IEnumerator LoadSceneCoroutine(string _path)
		{
			if (!File.Exists(_path))
			{
				yield break;
			}
			this.InitScene(false);
			yield return null;
			this.sceneInfo = new SceneInfo();
			if (!this.sceneInfo.Load(_path))
			{
				yield break;
			}
			AddObjectAssist.LoadChild(this.sceneInfo.dicObject, null, null);
			ChangeAmount ca = this.sceneInfo.mapInfo.ca.Clone();
			yield return this.AddMapCoroutine(this.sceneInfo.mapInfo.no, false, false);
			this.mapList.UpdateInfo();
			this.sceneInfo.mapInfo.ca.Copy(ca, true, true, true);
			Singleton<MapCtrl>.Instance.Reflect();
			this.bgmCtrl.Play(this.bgmCtrl.no);
			this.envCtrl.Play(this.envCtrl.no);
			this.outsideSoundCtrl.Play(this.outsideSoundCtrl.fileName);
			this.m_BackgroundCtrl.Load(this.sceneInfo.background);
			this.m_BackgroundList.UpdateUI();
			this._frameCtrl.Load(this.sceneInfo.frame);
			this._frameList.UpdateUI();
			this.m_SystemButtonCtrl.UpdateInfo();
			this.treeNodeCtrl.RefreshHierachy();
			if (this.sceneInfo.cameraSaveData != null)
			{
				this.m_CameraCtrl.Import(this.sceneInfo.cameraSaveData);
			}
			this.cameraLightCtrl.Reflect();
			this._cameraSelector.Init();
			this.rootButtonCtrl.OnClick(-1);
			yield break;
		}

		// Token: 0x06009CCF RID: 40143 RVA: 0x0040237C File Offset: 0x0040077C
		public bool ImportScene(string _path)
		{
			if (!File.Exists(_path))
			{
				return false;
			}
			if (!this.sceneInfo.Import(_path))
			{
				return false;
			}
			AddObjectAssist.LoadChild(this.sceneInfo.dicImport, null, null);
			this.treeNodeCtrl.RefreshHierachy();
			this._cameraSelector.Init();
			return true;
		}

		// Token: 0x06009CD0 RID: 40144 RVA: 0x004023D4 File Offset: 0x004007D4
		public void InitScene(bool _close = true)
		{
			this.ChangeCamera(null, false, true);
			this.cameraCount = 0;
			this.treeNodeCtrl.DeleteAllNode();
			Singleton<GuideObjectManager>.Instance.DeleteAll();
			this.m_RootButtonCtrl.OnClick(-1);
			this.m_RootButtonCtrl.objectCtrlInfo = null;
			foreach (KeyValuePair<TreeNodeObject, ObjectCtrlInfo> keyValuePair in this.dicInfo)
			{
				int kind = keyValuePair.Value.kind;
				if (kind != 0)
				{
					if (kind == 4)
					{
						OCIRoute ociroute = keyValuePair.Value as OCIRoute;
						ociroute.DeleteLine();
					}
				}
				else
				{
					OCIChar ocichar = keyValuePair.Value as OCIChar;
					if (ocichar != null)
					{
						ocichar.StopVoice();
					}
				}
				UnityEngine.Object.Destroy(keyValuePair.Value.guideObject.transformTarget.gameObject);
			}
			Singleton<Character>.Instance.DeleteCharaAll();
			this.dicInfo.Clear();
			this.dicChangeAmount.Clear();
			this.dicObjectCtrl.Clear();
			Singleton<Map>.Instance.ReleaseMap();
			this.cameraCtrl.CloerListCollider();
			this.bgmCtrl.Stop();
			this.envCtrl.Stop();
			this.outsideSoundCtrl.Stop();
			this.sceneInfo.Init();
			this.m_SystemButtonCtrl.UpdateInfo();
			this.cameraCtrl.Reset(0);
			this.cameraLightCtrl.Reflect();
			this._cameraSelector.Init();
			this.mapList.UpdateInfo();
			if (this.onChangeMap != null)
			{
				this.onChangeMap();
			}
			this.m_BackgroundCtrl.Load(this.sceneInfo.background);
			this.m_BackgroundList.UpdateUI();
			this._frameCtrl.Load(this.sceneInfo.frame);
			this._frameList.UpdateUI();
			this.m_WorkspaceCtrl.UpdateUI();
			Singleton<UndoRedoManager>.Instance.Clear();
			if (_close)
			{
				this.rootButtonCtrl.OnClick(-1);
			}
		}

		// Token: 0x06009CD1 RID: 40145 RVA: 0x00402600 File Offset: 0x00400A00
		public void OnDeleteNode(TreeNodeObject _node)
		{
			ObjectCtrlInfo objectCtrlInfo = null;
			if (!this.dicInfo.TryGetValue(_node, out objectCtrlInfo))
			{
				return;
			}
			if (this.onDelete != null)
			{
				this.onDelete(objectCtrlInfo);
			}
			objectCtrlInfo.OnDelete();
			this.dicInfo.Remove(_node);
		}

		// Token: 0x06009CD2 RID: 40146 RVA: 0x00402650 File Offset: 0x00400A50
		public void OnParentage(TreeNodeObject _parent, TreeNodeObject _child)
		{
			if (_parent)
			{
				ObjectCtrlInfo objectCtrlInfo = this.FindLoop(_parent);
				if (objectCtrlInfo == null)
				{
					return;
				}
				objectCtrlInfo.OnAttach(_parent, this.dicInfo[_child]);
			}
			else
			{
				this.dicInfo[_child].OnDetach();
			}
		}

		// Token: 0x06009CD3 RID: 40147 RVA: 0x004026A0 File Offset: 0x00400AA0
		public void ResetOption()
		{
			if (this.xmlCtrl != null)
			{
				this.xmlCtrl.Init();
			}
		}

		// Token: 0x06009CD4 RID: 40148 RVA: 0x004026B8 File Offset: 0x00400AB8
		public void LoadOption()
		{
			if (this.xmlCtrl != null)
			{
				this.xmlCtrl.Read();
			}
		}

		// Token: 0x06009CD5 RID: 40149 RVA: 0x004026D0 File Offset: 0x00400AD0
		public void SaveOption()
		{
			if (this.xmlCtrl != null)
			{
				this.xmlCtrl.Write();
			}
		}

		// Token: 0x06009CD6 RID: 40150 RVA: 0x004026E8 File Offset: 0x00400AE8
		public static void AddInfo(ObjectInfo _info, ObjectCtrlInfo _ctrlInfo)
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return;
			}
			if (_info == null || _ctrlInfo == null)
			{
				return;
			}
			Singleton<Studio>.Instance.sceneInfo.dicObject.Add(_info.dicKey, _info);
			Singleton<Studio>.Instance.dicObjectCtrl[_info.dicKey] = _ctrlInfo;
		}

		// Token: 0x06009CD7 RID: 40151 RVA: 0x00402740 File Offset: 0x00400B40
		public static void DeleteInfo(ObjectInfo _info, bool _delKey = true)
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return;
			}
			if (_info == null)
			{
				return;
			}
			if (Singleton<Studio>.Instance.sceneInfo.dicObject.ContainsKey(_info.dicKey))
			{
				Singleton<Studio>.Instance.sceneInfo.dicObject.Remove(_info.dicKey);
			}
			if (_delKey)
			{
				Singleton<Studio>.Instance.dicObjectCtrl.Remove(_info.dicKey);
				_info.DeleteKey();
				if (Singleton<Studio>.Instance.sceneInfo.sunCaster == _info.dicKey)
				{
					Singleton<Studio>.Instance.SetSunCaster(-1);
				}
			}
		}

		// Token: 0x06009CD8 RID: 40152 RVA: 0x004027E0 File Offset: 0x00400BE0
		public static ObjectInfo GetInfo(int _key)
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return null;
			}
			ObjectInfo objectInfo = null;
			return (!Singleton<Studio>.Instance.sceneInfo.dicObject.TryGetValue(_key, out objectInfo)) ? null : objectInfo;
		}

		// Token: 0x06009CD9 RID: 40153 RVA: 0x0040281E File Offset: 0x00400C1E
		public static void AddObjectCtrlInfo(ObjectCtrlInfo _ctrlInfo)
		{
			if (!Singleton<Studio>.IsInstance() || _ctrlInfo == null)
			{
				return;
			}
			Singleton<Studio>.Instance.dicObjectCtrl[_ctrlInfo.objectInfo.dicKey] = _ctrlInfo;
		}

		// Token: 0x06009CDA RID: 40154 RVA: 0x0040284C File Offset: 0x00400C4C
		public static ObjectCtrlInfo GetCtrlInfo(int _key)
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return null;
			}
			ObjectCtrlInfo objectCtrlInfo = null;
			return (!Singleton<Studio>.Instance.dicObjectCtrl.TryGetValue(_key, out objectCtrlInfo)) ? null : objectCtrlInfo;
		}

		// Token: 0x06009CDB RID: 40155 RVA: 0x00402885 File Offset: 0x00400C85
		public static TreeNodeObject AddNode(string _name, TreeNodeObject _parent = null)
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return null;
			}
			return Singleton<Studio>.Instance.treeNodeCtrl.AddNode(_name, _parent);
		}

		// Token: 0x06009CDC RID: 40156 RVA: 0x004028A4 File Offset: 0x00400CA4
		public static void DeleteNode(TreeNodeObject _node)
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return;
			}
			Singleton<Studio>.Instance.treeNodeCtrl.DeleteNode(_node);
		}

		// Token: 0x06009CDD RID: 40157 RVA: 0x004028C1 File Offset: 0x00400CC1
		public static void AddCtrlInfo(ObjectCtrlInfo _info)
		{
			if (!Singleton<Studio>.IsInstance() || _info == null)
			{
				return;
			}
			Singleton<Studio>.Instance.dicInfo.Add(_info.treeNodeObject, _info);
		}

		// Token: 0x06009CDE RID: 40158 RVA: 0x004028EC File Offset: 0x00400CEC
		public static ObjectCtrlInfo GetCtrlInfo(TreeNodeObject _node)
		{
			if (!Singleton<Studio>.IsInstance() || _node == null)
			{
				return null;
			}
			ObjectCtrlInfo objectCtrlInfo = null;
			return (!Singleton<Studio>.Instance.dicInfo.TryGetValue(_node, out objectCtrlInfo)) ? null : objectCtrlInfo;
		}

		// Token: 0x06009CDF RID: 40159 RVA: 0x00402931 File Offset: 0x00400D31
		public static int GetNewIndex()
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return -1;
			}
			return Singleton<Studio>.Instance.sceneInfo.GetNewIndex();
		}

		// Token: 0x06009CE0 RID: 40160 RVA: 0x0040294E File Offset: 0x00400D4E
		public static int SetNewIndex(int _index)
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return _index;
			}
			return (!Singleton<Studio>.Instance.sceneInfo.SetNewIndex(_index)) ? Singleton<Studio>.Instance.sceneInfo.GetNewIndex() : _index;
		}

		// Token: 0x06009CE1 RID: 40161 RVA: 0x00402988 File Offset: 0x00400D88
		public static bool DeleteIndex(int _index)
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return false;
			}
			bool flag = Singleton<Studio>.Instance.sceneInfo.DeleteIndex(_index);
			return Studio.DeleteChangeAmount(_index) || flag;
		}

		// Token: 0x06009CE2 RID: 40162 RVA: 0x004029BC File Offset: 0x00400DBC
		public static void AddChangeAmount(int _key, ChangeAmount _ca)
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return;
			}
			try
			{
				Singleton<Studio>.Instance.dicChangeAmount.Add(_key, _ca);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06009CE3 RID: 40163 RVA: 0x00402A00 File Offset: 0x00400E00
		public static bool DeleteChangeAmount(int _key)
		{
			return Singleton<Studio>.IsInstance() && Singleton<Studio>.Instance.dicChangeAmount.Remove(_key);
		}

		// Token: 0x06009CE4 RID: 40164 RVA: 0x00402A20 File Offset: 0x00400E20
		public static ChangeAmount GetChangeAmount(int _key)
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return null;
			}
			ChangeAmount changeAmount = null;
			return (!Singleton<Studio>.Instance.dicChangeAmount.TryGetValue(_key, out changeAmount)) ? null : changeAmount;
		}

		// Token: 0x06009CE5 RID: 40165 RVA: 0x00402A59 File Offset: 0x00400E59
		public static ObjectCtrlInfo[] GetSelectObjectCtrl()
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return null;
			}
			return Singleton<Studio>.Instance.treeNodeCtrl.selectObjectCtrl;
		}

		// Token: 0x06009CE6 RID: 40166 RVA: 0x00402A78 File Offset: 0x00400E78
		public void Init()
		{
			this.sceneInfo = new SceneInfo();
			this.cameraLightCtrl.Init();
			this.systemButtonCtrl.Init();
			this.mapList.Init();
			this.logoList.Init();
			this._inputFieldNow = null;
			this._inputFieldTMPNow = null;
			TreeNodeCtrl treeNodeCtrl = this.treeNodeCtrl;
			treeNodeCtrl.onDelete = (Action<TreeNodeObject>)Delegate.Combine(treeNodeCtrl.onDelete, new Action<TreeNodeObject>(this.OnDeleteNode));
			TreeNodeCtrl treeNodeCtrl2 = this.treeNodeCtrl;
			treeNodeCtrl2.onParentage = (Action<TreeNodeObject, TreeNodeObject>)Delegate.Combine(treeNodeCtrl2.onParentage, new Action<TreeNodeObject, TreeNodeObject>(this.OnParentage));
		}

		// Token: 0x06009CE7 RID: 40167 RVA: 0x00402B18 File Offset: 0x00400F18
		public void SelectInputField(InputField _input, TMP_InputField _inputTMP)
		{
			this._inputFieldNow = _input;
			this._inputFieldTMPNow = _inputTMP;
		}

		// Token: 0x06009CE8 RID: 40168 RVA: 0x00402B28 File Offset: 0x00400F28
		public void DeselectInputField(InputField _input, TMP_InputField _inputTMP)
		{
			if (this._inputFieldNow == _input)
			{
				this._inputFieldNow = null;
			}
			if (this._inputFieldTMPNow == _inputTMP)
			{
				this._inputFieldTMPNow = null;
			}
		}

		// Token: 0x06009CE9 RID: 40169 RVA: 0x00402B5C File Offset: 0x00400F5C
		public void ShowName(Transform _transform, string _name)
		{
			this.rectName.gameObject.SetActive(true);
			this.rectName.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _transform.position);
			this.textName.text = _name;
			if (this.disposableName != null)
			{
				this.disposableName.Dispose();
			}
			this.disposableName = new SingleAssignmentDisposable();
			IObservable<long> other = Observable.Timer(TimeSpan.FromSeconds(2.0));
			this.disposableName.Disposable = Observable.EveryUpdate().TakeUntil(other).Subscribe(delegate(long _)
			{
				if (_transform != null)
				{
					this.rectName.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _transform.position);
				}
			}, delegate()
			{
				this.rectName.gameObject.SetActive(false);
			});
		}

		// Token: 0x06009CEA RID: 40170 RVA: 0x00402C28 File Offset: 0x00401028
		private ObjectCtrlInfo FindLoop(TreeNodeObject _node)
		{
			if (_node == null)
			{
				return null;
			}
			ObjectCtrlInfo result = null;
			if (this.dicInfo.TryGetValue(_node, out result))
			{
				return result;
			}
			return this.FindLoop(_node.parent);
		}

		// Token: 0x06009CEB RID: 40171 RVA: 0x00402C68 File Offset: 0x00401068
		private void SavePreprocessingLoop(TreeNodeObject _node)
		{
			if (_node == null)
			{
				return;
			}
			ObjectCtrlInfo objectCtrlInfo = null;
			if (this.dicInfo.TryGetValue(_node, out objectCtrlInfo))
			{
				objectCtrlInfo.OnSavePreprocessing();
			}
			if (_node.child.IsNullOrEmpty<TreeNodeObject>())
			{
				return;
			}
			foreach (TreeNodeObject node in _node.child)
			{
				this.SavePreprocessingLoop(node);
			}
		}

		// Token: 0x06009CEC RID: 40172 RVA: 0x00402D00 File Offset: 0x00401100
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.imageCamera.gameObject.SetActive(true);
			Studio.optionSystem = new OptionSystem("Option");
			this.xmlCtrl = new Control("studio", "option.xml", "Option", new Data[]
			{
				Studio.optionSystem
			});
			this.LoadOption();
			this._logoList.UpdateInfo();
			if (this.workInfo == null)
			{
				this.workInfo = new WorkInfo();
			}
			this.workInfo.Load();
		}

		// Token: 0x06009CED RID: 40173 RVA: 0x00402D9E File Offset: 0x0040119E
		private void OnApplicationQuit()
		{
			this.SaveOption();
			this.workInfo.Save();
		}

		// Token: 0x04007CB0 RID: 31920
		public const string savePath = "studio/scene";

		// Token: 0x04007CB1 RID: 31921
		[SerializeField]
		private TreeNodeCtrl m_TreeNodeCtrl;

		// Token: 0x04007CB2 RID: 31922
		[SerializeField]
		private RootButtonCtrl m_RootButtonCtrl;

		// Token: 0x04007CB3 RID: 31923
		[SerializeField]
		private ManipulatePanelCtrl _manipulatePanelCtrl;

		// Token: 0x04007CB4 RID: 31924
		[SerializeField]
		private CameraControl m_CameraCtrl;

		// Token: 0x04007CB5 RID: 31925
		[SerializeField]
		private SystemButtonCtrl m_SystemButtonCtrl;

		// Token: 0x04007CB6 RID: 31926
		[SerializeField]
		private CameraLightCtrl m_CameraLightCtrl;

		// Token: 0x04007CB7 RID: 31927
		[SerializeField]
		private MapList _mapList;

		// Token: 0x04007CB8 RID: 31928
		[SerializeField]
		private ColorPalette _colorPalette;

		// Token: 0x04007CB9 RID: 31929
		[SerializeField]
		private WorkspaceCtrl m_WorkspaceCtrl;

		// Token: 0x04007CBA RID: 31930
		[SerializeField]
		private BackgroundCtrl m_BackgroundCtrl;

		// Token: 0x04007CBB RID: 31931
		[SerializeField]
		private BackgroundList m_BackgroundList;

		// Token: 0x04007CBC RID: 31932
		[SerializeField]
		private PatternSelectListCtrl _patternSelectListCtrl;

		// Token: 0x04007CBD RID: 31933
		[SerializeField]
		private GameScreenShot _gameScreenShot;

		// Token: 0x04007CBE RID: 31934
		[SerializeField]
		private FrameCtrl _frameCtrl;

		// Token: 0x04007CBF RID: 31935
		[SerializeField]
		private FrameList _frameList;

		// Token: 0x04007CC0 RID: 31936
		[SerializeField]
		private LogoList _logoList;

		// Token: 0x04007CC1 RID: 31937
		[SerializeField]
		private InputField _inputFieldNow;

		// Token: 0x04007CC2 RID: 31938
		[SerializeField]
		private TMP_InputField _inputFieldTMPNow;

		// Token: 0x04007CC3 RID: 31939
		[Space]
		[SerializeField]
		private RectTransform rectName;

		// Token: 0x04007CC4 RID: 31940
		[SerializeField]
		private TextMeshProUGUI textName;

		// Token: 0x04007CC5 RID: 31941
		private SingleAssignmentDisposable disposableName;

		// Token: 0x04007CC6 RID: 31942
		[Space]
		[SerializeField]
		private Sprite spriteLight;

		// Token: 0x04007CC7 RID: 31943
		[Space]
		[SerializeField]
		private Image imageCamera;

		// Token: 0x04007CC8 RID: 31944
		[SerializeField]
		private TextMeshProUGUI textCamera;

		// Token: 0x04007CC9 RID: 31945
		[SerializeField]
		private CameraSelector _cameraSelector;

		// Token: 0x04007CCA RID: 31946
		[Space]
		[SerializeField]
		private Texture _textureLine;

		// Token: 0x04007CCB RID: 31947
		[SerializeField]
		private RouteControl _routeControl;

		// Token: 0x04007CCD RID: 31949
		public Dictionary<TreeNodeObject, ObjectCtrlInfo> dicInfo = new Dictionary<TreeNodeObject, ObjectCtrlInfo>();

		// Token: 0x04007CCE RID: 31950
		public Dictionary<int, ObjectCtrlInfo> dicObjectCtrl = new Dictionary<int, ObjectCtrlInfo>();

		// Token: 0x04007CCF RID: 31951
		public Dictionary<int, ChangeAmount> dicChangeAmount = new Dictionary<int, ChangeAmount>();

		// Token: 0x04007CD0 RID: 31952
		private const string UserPath = "studio";

		// Token: 0x04007CD1 RID: 31953
		private const string FileName = "option.xml";

		// Token: 0x04007CD2 RID: 31954
		private const string RootName = "Option";

		// Token: 0x04007CD4 RID: 31956
		private Control xmlCtrl;

		// Token: 0x04007CD5 RID: 31957
		private OCICamera _ociCamera;

		// Token: 0x04007CD8 RID: 31960
		public Action<ObjectCtrlInfo> onDelete;

		// Token: 0x04007CD9 RID: 31961
		public Action onChangeMap;

		// Token: 0x04007CDA RID: 31962
		public WorkInfo workInfo = new WorkInfo();

		// Token: 0x02001283 RID: 4739
		private class DuplicateParentInfo
		{
			// Token: 0x06009CF2 RID: 40178 RVA: 0x00402DF2 File Offset: 0x004011F2
			public DuplicateParentInfo(ObjectCtrlInfo _info, TreeNodeObject _node)
			{
				this.info = _info;
				this.node = _node;
			}

			// Token: 0x04007CDF RID: 31967
			public ObjectCtrlInfo info;

			// Token: 0x04007CE0 RID: 31968
			public TreeNodeObject node;
		}
	}
}
