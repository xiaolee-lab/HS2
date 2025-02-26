using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Illusion.Extensions;
using Manager;
using Studio;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200129F RID: 4767
public class StudioScene : BaseLoader
{
	// Token: 0x06009D9B RID: 40347 RVA: 0x0040594C File Offset: 0x00403D4C
	public void OnClickDraw(int _no)
	{
		Singleton<Studio>.Instance.workInfo.visibleFlags[_no] = !Singleton<Studio>.Instance.workInfo.visibleFlags[_no];
		SortCanvas.select = this.drawInfo.canvasSystemMenu;
		this.UpdateUI(_no);
	}

	// Token: 0x06009D9C RID: 40348 RVA: 0x0040598C File Offset: 0x00403D8C
	private void UpdateUI(int _no)
	{
		bool flag = Singleton<Studio>.Instance.workInfo.visibleFlags[_no];
		switch (_no)
		{
		case 0:
			this.drawInfo.canvasMainMenuG.Enable(flag, false);
			this.drawInfo.systemButtonCtrl.visible = flag;
			this.drawInfo.imageMenu.sprite = this.drawInfo.spriteMenu[(!flag) ? 1 : 0];
			break;
		case 1:
			this.drawInfo.canvasList.enabled = flag;
			this.drawInfo.imageWork.sprite = this.drawInfo.spriteWork[(!flag) ? 1 : 0];
			break;
		case 2:
			Singleton<GuideObjectManager>.Instance.guideInput.outsideVisible = flag;
			this.drawInfo.imageMove.sprite = this.drawInfo.spriteMove[(!flag) ? 1 : 0];
			break;
		case 3:
			Singleton<Studio>.Instance.colorPalette.outsideVisible = flag;
			this.drawInfo.imageColor.sprite = this.drawInfo.spriteColor[(!flag) ? 1 : 0];
			break;
		case 4:
			this.drawInfo.objOption.SetActive(flag);
			if (!flag)
			{
				this.mapInfo.mapCtrl.active = false;
				this.inputInfo.outsideVisible = false;
			}
			else
			{
				this.mapInfo.Update();
				this.inputInfo.outsideVisible = true;
			}
			this.drawInfo.imageOption.sprite = this.drawInfo.spriteOption[(!flag) ? 1 : 0];
			break;
		case 5:
			this.drawInfo.objCamera.SetActive(flag);
			this.drawInfo.imageCamera.sprite = this.drawInfo.spriteCamera[(!flag) ? 1 : 0];
			break;
		}
	}

	// Token: 0x06009D9D RID: 40349 RVA: 0x00405B98 File Offset: 0x00403F98
	private void UpdateUI()
	{
		for (int i = 0; i < 6; i++)
		{
			this.UpdateUI(i);
		}
		this.cameraInfo.center = Singleton<Studio>.Instance.workInfo.visibleCenter;
		this.cameraInfo.axis = Singleton<Studio>.Instance.workInfo.visibleAxis;
		this.cameraInfo.axisTrans = Singleton<Studio>.Instance.workInfo.visibleAxisTranslation;
		this.cameraInfo.axisCenter = Singleton<Studio>.Instance.workInfo.visibleAxisCenter;
		this.cameraInfo.Gimmick = Singleton<Studio>.Instance.workInfo.visibleGimmick;
		this.cameraInfo.imageCamera.sprite = this.cameraInfo.spriteCamera[(!Singleton<Studio>.Instance.workInfo.useAlt) ? 0 : 1];
		this.mapInfo.Update();
	}

	// Token: 0x06009D9E RID: 40350 RVA: 0x00405C82 File Offset: 0x00404082
	public void OnClickMode(int _mode)
	{
		SortCanvas.select = this.modeInfo.canvasInput;
		Singleton<GuideObjectManager>.Instance.mode = _mode;
	}

	// Token: 0x06009D9F RID: 40351 RVA: 0x00405CA0 File Offset: 0x004040A0
	private void Instance_ModeChangeEvent(object sender, EventArgs e)
	{
		int mode = Singleton<GuideObjectManager>.Instance.mode;
		this.modeInfo.imageMode[0].sprite = this.modeInfo.spriteModeMove[(mode != 0) ? 0 : 1];
		this.modeInfo.imageMode[1].sprite = this.modeInfo.spriteModeRotate[(mode != 1) ? 0 : 1];
		this.modeInfo.imageMode[2].sprite = this.modeInfo.spriteModeScale[(mode != 2) ? 0 : 1];
	}

	// Token: 0x06009DA0 RID: 40352 RVA: 0x00405D3C File Offset: 0x0040413C
	private bool NoCtrlCondition()
	{
		bool flag = Singleton<Studio>.IsInstance() && Singleton<Studio>.Instance.workInfo.useAlt;
		return flag && (!UnityEngine.Input.GetKey(KeyCode.LeftAlt) && !UnityEngine.Input.GetKey(KeyCode.RightAlt));
	}

	// Token: 0x06009DA1 RID: 40353 RVA: 0x00405D94 File Offset: 0x00404194
	private bool KeyCondition()
	{
		return !Singleton<Studio>.IsInstance() || !Singleton<Studio>.Instance.isInputNow;
	}

	// Token: 0x06009DA2 RID: 40354 RVA: 0x00405DB4 File Offset: 0x004041B4
	public void OnClickCamera()
	{
		bool flag = !Singleton<Studio>.Instance.workInfo.useAlt;
		Singleton<Studio>.Instance.workInfo.useAlt = flag;
		this.cameraInfo.imageCamera.sprite = this.cameraInfo.spriteCamera[(!flag) ? 0 : 1];
	}

	// Token: 0x06009DA3 RID: 40355 RVA: 0x00405E0D File Offset: 0x0040420D
	public void OnClickSaveCamera(int _no)
	{
		Singleton<Studio>.Instance.sceneInfo.cameraData[_no] = this.cameraInfo.cameraCtrl.Export();
	}

	// Token: 0x06009DA4 RID: 40356 RVA: 0x00405E30 File Offset: 0x00404230
	public void OnClickLoadCamera(int _no)
	{
		this.cameraInfo.cameraCtrl.Import(Singleton<Studio>.Instance.sceneInfo.cameraData[_no]);
	}

	// Token: 0x06009DA5 RID: 40357 RVA: 0x00405E54 File Offset: 0x00404254
	public void OnClickCenter()
	{
		bool flag = !Singleton<Studio>.Instance.workInfo.visibleCenter;
		Singleton<Studio>.Instance.workInfo.visibleCenter = flag;
		this.cameraInfo.center = flag;
	}

	// Token: 0x06009DA6 RID: 40358 RVA: 0x00405E90 File Offset: 0x00404290
	public void OnClickAxis()
	{
		bool flag = !Singleton<Studio>.Instance.workInfo.visibleAxis;
		Singleton<Studio>.Instance.workInfo.visibleAxis = flag;
		this.cameraInfo.axis = flag;
	}

	// Token: 0x06009DA7 RID: 40359 RVA: 0x00405ECC File Offset: 0x004042CC
	public void OnClickAxisTrans()
	{
		this.cameraInfo.axisTrans = !Singleton<Studio>.Instance.workInfo.visibleAxisTranslation;
		Singleton<GuideObjectManager>.Instance.SetVisibleTranslation();
	}

	// Token: 0x06009DA8 RID: 40360 RVA: 0x00405EF5 File Offset: 0x004042F5
	public void OnClickAxisCenter()
	{
		this.cameraInfo.axisCenter = !Singleton<Studio>.Instance.workInfo.visibleAxisCenter;
		Singleton<GuideObjectManager>.Instance.SetVisibleCenter();
	}

	// Token: 0x06009DA9 RID: 40361 RVA: 0x00405F1E File Offset: 0x0040431E
	public void OnClickGimmick()
	{
		this.cameraInfo.Gimmick = !Singleton<Studio>.Instance.workInfo.visibleGimmick;
		Singleton<Studio>.Instance.SetVisibleGimmick();
	}

	// Token: 0x06009DAA RID: 40362 RVA: 0x00405F47 File Offset: 0x00404347
	public void OnClickTarget()
	{
	}

	// Token: 0x06009DAB RID: 40363 RVA: 0x00405F49 File Offset: 0x00404349
	public void OnClickUndo()
	{
		Singleton<UndoRedoManager>.Instance.Undo();
	}

	// Token: 0x06009DAC RID: 40364 RVA: 0x00405F55 File Offset: 0x00404355
	public void OnClickRedo()
	{
		Singleton<UndoRedoManager>.Instance.Redo();
	}

	// Token: 0x06009DAD RID: 40365 RVA: 0x00405F61 File Offset: 0x00404361
	private void Instance_CanUndoChange(object sender, EventArgs e)
	{
		this.buttonUndo.interactable = Singleton<UndoRedoManager>.Instance.CanUndo;
	}

	// Token: 0x06009DAE RID: 40366 RVA: 0x00405F78 File Offset: 0x00404378
	private void Instance_CanRedoChange(object sender, EventArgs e)
	{
		this.buttonRedo.interactable = Singleton<UndoRedoManager>.Instance.CanRedo;
	}

	// Token: 0x06009DAF RID: 40367 RVA: 0x00405F90 File Offset: 0x00404390
	private void ChangeScale()
	{
		float num = UnityEngine.Input.GetAxis("Mouse X") * 0.1f;
		OptionCtrl.InputCombination inputSize = this.optionCtrl.inputSize;
		Studio.optionSystem.manipulateSize = Mathf.Clamp(Studio.optionSystem.manipulateSize + num, inputSize.min, inputSize.max);
		Singleton<GuideObjectManager>.Instance.SetScale();
		this.optionCtrl.UpdateUIManipulateSize();
	}

	// Token: 0x06009DB0 RID: 40368 RVA: 0x00405FF6 File Offset: 0x004043F6
	public void OnClickMap()
	{
		this.mapInfo.active = !this.mapInfo.active;
	}

	// Token: 0x06009DB1 RID: 40369 RVA: 0x00406014 File Offset: 0x00404414
	private void CreatePatternList()
	{
		PatternSelectListCtrl pslc = Singleton<Studio>.Instance.patternSelectListCtrl;
		ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
		Dictionary<int, ListInfoBase> categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_pattern);
		List<ListInfoBase> list = categoryInfo.Values.ToList<ListInfoBase>();
		list.ForEach(delegate(ListInfoBase info)
		{
			pslc.AddList(info.Id, info.Name, info.GetInfo(ChaListDefine.KeyType.ThumbAB), info.GetInfo(ChaListDefine.KeyType.ThumbTex));
		});
		pslc.AddOutside(list.Max((ListInfoBase l) => l.Id) + 1);
		pslc.Create(null);
	}

	// Token: 0x06009DB2 RID: 40370 RVA: 0x004060A8 File Offset: 0x004044A8
	public void OnClickInput()
	{
		this.inputInfo.active = !this.inputInfo.active;
	}

	// Token: 0x06009DB3 RID: 40371 RVA: 0x004060C3 File Offset: 0x004044C3
	public void LoadItem()
	{
	}

	// Token: 0x06009DB4 RID: 40372 RVA: 0x004060C8 File Offset: 0x004044C8
	private IEnumerator LoadItemCoroutine()
	{
		Info.WaitTime waitTime = new Info.WaitTime();
		if (waitTime.isOver)
		{
			yield return null;
			waitTime.Next();
		}
		foreach (KeyValuePair<int, Dictionary<int, Dictionary<int, Info.ItemLoadInfo>>> g in Singleton<Info>.Instance.dicItemLoadInfo)
		{
			foreach (KeyValuePair<int, Dictionary<int, Info.ItemLoadInfo>> c in g.Value)
			{
				foreach (KeyValuePair<int, Info.ItemLoadInfo> i in c.Value)
				{
					Singleton<Studio>.Instance.AddItem(g.Key, c.Key, i.Key);
					if (waitTime.isOver)
					{
						yield return null;
						waitTime.Next();
					}
				}
			}
		}
		yield break;
	}

	// Token: 0x06009DB5 RID: 40373 RVA: 0x004060DC File Offset: 0x004044DC
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x06009DB6 RID: 40374 RVA: 0x004060E4 File Offset: 0x004044E4
	private IEnumerator Start()
	{
		if (this.textAdjustment != null)
		{
			this.textAdjustment.font.material.mainTexture.filterMode = FilterMode.Point;
		}
		if (!Singleton<Info>.Instance.isLoadList)
		{
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioWait",
				isAdd = true
			}, false);
			yield return Singleton<Info>.Instance.LoadExcelDataCoroutine();
			yield return new WaitWhile(() => !Config.initialized);
			Singleton<Scene>.Instance.UnLoad();
		}
		this.cameraInfo.Init();
		Studio.CameraControl cameraCtrl = this.cameraInfo.cameraCtrl;
		cameraCtrl.noCtrlCondition = (Studio.CameraControl.NoCtrlFunc)Delegate.Combine(cameraCtrl.noCtrlCondition, new Studio.CameraControl.NoCtrlFunc(this.NoCtrlCondition));
		Studio.CameraControl cameraCtrl2 = this.cameraInfo.cameraCtrl;
		cameraCtrl2.keyCondition = (Studio.CameraControl.NoCtrlFunc)Delegate.Combine(cameraCtrl2.keyCondition, new Studio.CameraControl.NoCtrlFunc(this.KeyCondition));
		Singleton<GuideObjectManager>.Instance.ModeChangeEvent += this.Instance_ModeChangeEvent;
		this.Instance_ModeChangeEvent(null, null);
		Singleton<UndoRedoManager>.Instance.CanRedoChange += this.Instance_CanRedoChange;
		Singleton<UndoRedoManager>.Instance.CanUndoChange += this.Instance_CanUndoChange;
		this.buttonUndo.interactable = false;
		this.buttonRedo.interactable = false;
		Studio instance = Singleton<Studio>.Instance;
		instance.onChangeMap = (Action)Delegate.Combine(instance.onChangeMap, new Action(this.mapInfo.OnChangeMap));
		this.CreatePatternList();
		this.inputInfo.Init();
		GuideInput guideInput = Singleton<GuideObjectManager>.Instance.guideInput;
		guideInput.onVisible = (GuideInput.OnVisible)Delegate.Combine(guideInput.onVisible, new GuideInput.OnVisible(this.inputInfo.OnVisible));
		this.inputInfo.active = false;
		this.optionCtrl.Init();
		Singleton<Studio>.Instance.Init();
		Singleton<Studio>.Instance.cameraCtrl.SetCamera(new Vector3(0f, 10f, 0f), Quaternion.Euler(15f, 180f, 0f), 25f, true, true);
		Camera mainCamera = Camera.main;
		if (mainCamera != null)
		{
			mainCamera.backgroundColor = Config.GraphicData.BackColor;
		}
		if (Singleton<Sound>.Instance.AudioListener)
		{
			Singleton<Sound>.Instance.AudioListener.enabled = true;
		}
		Sound instance2 = Singleton<Sound>.Instance;
		Camera camera = mainCamera;
		instance2.Listener = ((camera != null) ? camera.transform : null);
		this.UpdateUI();
		(from _ in this.UpdateAsObservable()
		where !this.cameraInfo.cameraCtrl.isControlNow
		where UnityEngine.Input.GetKey(KeyCode.B) && UnityEngine.Input.GetMouseButton(1)
		select _).Subscribe(delegate(Unit _)
		{
			this.ChangeScale();
		}).AddTo(this);
		if (Studio.optionSystem.startupLoad)
		{
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioSceneLoad",
				isAdd = true
			}, false);
		}
		yield break;
	}

	// Token: 0x04007D5E RID: 32094
	[SerializeField]
	private StudioScene.DrawInfo drawInfo = new StudioScene.DrawInfo();

	// Token: 0x04007D5F RID: 32095
	[SerializeField]
	private StudioScene.ModeInfo modeInfo = new StudioScene.ModeInfo();

	// Token: 0x04007D60 RID: 32096
	public StudioScene.CameraInfo cameraInfo = new StudioScene.CameraInfo();

	// Token: 0x04007D61 RID: 32097
	[SerializeField]
	private Button buttonUndo;

	// Token: 0x04007D62 RID: 32098
	[SerializeField]
	private Button buttonRedo;

	// Token: 0x04007D63 RID: 32099
	[SerializeField]
	private StudioScene.MapInfo mapInfo = new StudioScene.MapInfo();

	// Token: 0x04007D64 RID: 32100
	[SerializeField]
	private Text textAdjustment;

	// Token: 0x04007D65 RID: 32101
	[SerializeField]
	private OptionCtrl optionCtrl;

	// Token: 0x04007D66 RID: 32102
	[SerializeField]
	private StudioScene.InputInfo inputInfo = new StudioScene.InputInfo();

	// Token: 0x020012A0 RID: 4768
	[Serializable]
	private class DrawInfo
	{
		// Token: 0x04007D68 RID: 32104
		public Canvas canvasMainMenu;

		// Token: 0x04007D69 RID: 32105
		public CanvasGroup canvasMainMenuG;

		// Token: 0x04007D6A RID: 32106
		public SystemButtonCtrl systemButtonCtrl;

		// Token: 0x04007D6B RID: 32107
		public Canvas canvasList;

		// Token: 0x04007D6C RID: 32108
		public Canvas canvasColor;

		// Token: 0x04007D6D RID: 32109
		public GameObject objOption;

		// Token: 0x04007D6E RID: 32110
		public GameObject objCamera;

		// Token: 0x04007D6F RID: 32111
		public Image imageMenu;

		// Token: 0x04007D70 RID: 32112
		public Sprite[] spriteMenu;

		// Token: 0x04007D71 RID: 32113
		public Image imageWork;

		// Token: 0x04007D72 RID: 32114
		public Sprite[] spriteWork;

		// Token: 0x04007D73 RID: 32115
		public Image imageMove;

		// Token: 0x04007D74 RID: 32116
		public Sprite[] spriteMove;

		// Token: 0x04007D75 RID: 32117
		public Image imageColor;

		// Token: 0x04007D76 RID: 32118
		public Sprite[] spriteColor;

		// Token: 0x04007D77 RID: 32119
		public Image imageOption;

		// Token: 0x04007D78 RID: 32120
		public Sprite[] spriteOption;

		// Token: 0x04007D79 RID: 32121
		public Image imageCamera;

		// Token: 0x04007D7A RID: 32122
		public Sprite[] spriteCamera;

		// Token: 0x04007D7B RID: 32123
		public Canvas canvasSystemMenu;
	}

	// Token: 0x020012A1 RID: 4769
	[Serializable]
	private class ModeInfo
	{
		// Token: 0x04007D7C RID: 32124
		public Image[] imageMode;

		// Token: 0x04007D7D RID: 32125
		public Sprite[] spriteModeMove;

		// Token: 0x04007D7E RID: 32126
		public Sprite[] spriteModeRotate;

		// Token: 0x04007D7F RID: 32127
		public Sprite[] spriteModeScale;

		// Token: 0x04007D80 RID: 32128
		public Canvas canvasInput;
	}

	// Token: 0x020012A2 RID: 4770
	[Serializable]
	public class CameraInfo
	{
		// Token: 0x170021B0 RID: 8624
		// (get) Token: 0x06009DBB RID: 40379 RVA: 0x0040611F File Offset: 0x0040451F
		// (set) Token: 0x06009DBC RID: 40380 RVA: 0x0040612C File Offset: 0x0040452C
		public bool center
		{
			get
			{
				return this.cameraCtrl.isOutsideTargetTex;
			}
			set
			{
				if (this.cameraCtrl.isOutsideTargetTex != value)
				{
					this.cameraCtrl.isOutsideTargetTex = value;
					this.imageCenter.color = ((!this.cameraCtrl.isOutsideTargetTex) ? Color.white : Color.green);
				}
			}
		}

		// Token: 0x170021B1 RID: 8625
		// (set) Token: 0x06009DBD RID: 40381 RVA: 0x00406180 File Offset: 0x00404580
		public bool axis
		{
			set
			{
				Singleton<Studio>.Instance.workInfo.visibleAxis = value;
				this.physicsRaycaster.enabled = value;
				this.imageAxis.color = ((!value) ? Color.white : Color.green);
				this.cameraCtrl.ReflectOption();
			}
		}

		// Token: 0x170021B2 RID: 8626
		// (set) Token: 0x06009DBE RID: 40382 RVA: 0x004061D4 File Offset: 0x004045D4
		public bool axisTrans
		{
			set
			{
				Singleton<Studio>.Instance.workInfo.visibleAxisTranslation = value;
				this.imageAxisTrans.color = ((!value) ? Color.white : Color.green);
			}
		}

		// Token: 0x170021B3 RID: 8627
		// (set) Token: 0x06009DBF RID: 40383 RVA: 0x00406206 File Offset: 0x00404606
		public bool axisCenter
		{
			set
			{
				Singleton<Studio>.Instance.workInfo.visibleAxisCenter = value;
				this.imageAxisCenter.color = ((!value) ? Color.white : Color.green);
			}
		}

		// Token: 0x170021B4 RID: 8628
		// (set) Token: 0x06009DC0 RID: 40384 RVA: 0x00406238 File Offset: 0x00404638
		public bool Gimmick
		{
			set
			{
				Singleton<Studio>.Instance.workInfo.visibleGimmick = value;
				this.imageGimmick.color = ((!value) ? Color.white : Color.green);
			}
		}

		// Token: 0x06009DC1 RID: 40385 RVA: 0x0040626C File Offset: 0x0040466C
		public void Init()
		{
			this.imageCenter.color = ((!this.cameraCtrl.isOutsideTargetTex) ? Color.white : Color.green);
			this.imageAxis.color = ((!this.cameraSub.enabled) ? Color.white : Color.green);
		}

		// Token: 0x04007D81 RID: 32129
		public Image imageCamera;

		// Token: 0x04007D82 RID: 32130
		public Sprite[] spriteCamera;

		// Token: 0x04007D83 RID: 32131
		public Studio.CameraControl cameraCtrl;

		// Token: 0x04007D84 RID: 32132
		public Image imageCenter;

		// Token: 0x04007D85 RID: 32133
		public Image imageAxis;

		// Token: 0x04007D86 RID: 32134
		public Image imageAxisTrans;

		// Token: 0x04007D87 RID: 32135
		public Image imageAxisCenter;

		// Token: 0x04007D88 RID: 32136
		public Image imageGimmick;

		// Token: 0x04007D89 RID: 32137
		public Camera cameraSub;

		// Token: 0x04007D8A RID: 32138
		public PhysicsRaycaster physicsRaycaster;

		// Token: 0x04007D8B RID: 32139
		public Camera cameraUI;
	}

	// Token: 0x020012A3 RID: 4771
	[Serializable]
	private class MapInfo
	{
		// Token: 0x170021B5 RID: 8629
		// (get) Token: 0x06009DC3 RID: 40387 RVA: 0x004062D5 File Offset: 0x004046D5
		private Image image
		{
			get
			{
				if (this.m_Image == null)
				{
					this.m_Image = this.button.image;
				}
				return this.m_Image;
			}
		}

		// Token: 0x170021B6 RID: 8630
		// (get) Token: 0x06009DC4 RID: 40388 RVA: 0x004062FF File Offset: 0x004046FF
		// (set) Token: 0x06009DC5 RID: 40389 RVA: 0x00406307 File Offset: 0x00404707
		public bool active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
				this.Update();
			}
		}

		// Token: 0x06009DC6 RID: 40390 RVA: 0x00406316 File Offset: 0x00404716
		public void Update()
		{
			this.image.color = ((!this.m_Active) ? Color.white : Color.green);
			this.mapCtrl.active = this.m_Active;
		}

		// Token: 0x06009DC7 RID: 40391 RVA: 0x00406350 File Offset: 0x00404750
		public void OnChangeMap()
		{
			this.button.interactable = (Singleton<Studio>.Instance.sceneInfo.mapInfo.no != -1);
			if (!this.button.interactable && this.active)
			{
				this.active = false;
			}
			else
			{
				this.mapCtrl.Reflect();
			}
		}

		// Token: 0x04007D8C RID: 32140
		public MapCtrl mapCtrl;

		// Token: 0x04007D8D RID: 32141
		public Button button;

		// Token: 0x04007D8E RID: 32142
		private Image m_Image;

		// Token: 0x04007D8F RID: 32143
		private bool m_Active;
	}

	// Token: 0x020012A4 RID: 4772
	[Serializable]
	private class InputInfo
	{
		// Token: 0x170021B7 RID: 8631
		// (get) Token: 0x06009DC9 RID: 40393 RVA: 0x004063C8 File Offset: 0x004047C8
		private Image image
		{
			get
			{
				if (this._image == null)
				{
					this._image = this.button.image;
				}
				return this._image;
			}
		}

		// Token: 0x170021B8 RID: 8632
		// (get) Token: 0x06009DCA RID: 40394 RVA: 0x004063F2 File Offset: 0x004047F2
		// (set) Token: 0x06009DCB RID: 40395 RVA: 0x004063FA File Offset: 0x004047FA
		public bool active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
				this.Update();
			}
		}

		// Token: 0x170021B9 RID: 8633
		// (get) Token: 0x06009DCC RID: 40396 RVA: 0x00406409 File Offset: 0x00404809
		// (set) Token: 0x06009DCD RID: 40397 RVA: 0x00406416 File Offset: 0x00404816
		public bool outsideVisible
		{
			get
			{
				return this._outsideVisible.Value;
			}
			set
			{
				this._outsideVisible.Value = value;
			}
		}

		// Token: 0x06009DCE RID: 40398 RVA: 0x00406424 File Offset: 0x00404824
		public void Init()
		{
			this._outsideVisible.Subscribe(delegate(bool _b)
			{
				this.UpdateVisible();
			});
		}

		// Token: 0x06009DCF RID: 40399 RVA: 0x0040643E File Offset: 0x0040483E
		public void Update()
		{
			this.image.color = ((!this._active) ? Color.white : Color.green);
			this.objectCtrl.active = this._active;
		}

		// Token: 0x06009DD0 RID: 40400 RVA: 0x00406476 File Offset: 0x00404876
		public void OnVisible(bool _value)
		{
			this.button.interactable = _value;
			this.UpdateVisible();
		}

		// Token: 0x06009DD1 RID: 40401 RVA: 0x0040648A File Offset: 0x0040488A
		private void UpdateVisible()
		{
			this.objectCtrl.active = (this.button.interactable & this.active & this.outsideVisible);
		}

		// Token: 0x04007D90 RID: 32144
		public ObjectCtrl objectCtrl;

		// Token: 0x04007D91 RID: 32145
		public Button button;

		// Token: 0x04007D92 RID: 32146
		private Image _image;

		// Token: 0x04007D93 RID: 32147
		private bool _active;

		// Token: 0x04007D94 RID: 32148
		private BoolReactiveProperty _outsideVisible = new BoolReactiveProperty(true);
	}
}
