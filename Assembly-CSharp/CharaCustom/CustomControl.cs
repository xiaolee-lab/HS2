using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using AIProject;
using ConfigScene;
using Illusion;
using Illusion.Extensions;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009CB RID: 2507
	public class CustomControl : MonoBehaviour
	{
		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x0600494E RID: 18766 RVA: 0x001BDE16 File Offset: 0x001BC216
		public CustomCapture customCap
		{
			get
			{
				return this._customCap;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x0600494F RID: 18767 RVA: 0x001BDE1E File Offset: 0x001BC21E
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06004950 RID: 18768 RVA: 0x001BDE25 File Offset: 0x001BC225
		// (set) Token: 0x06004951 RID: 18769 RVA: 0x001BDE32 File Offset: 0x001BC232
		private ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
			set
			{
				this.customBase.chaCtrl = value;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x001BDE40 File Offset: 0x001BC240
		private ChaFileControl chaFile
		{
			get
			{
				return this.chaCtrl.chaFile;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06004953 RID: 18771 RVA: 0x001BDE4D File Offset: 0x001BC24D
		private ChaFileBody body
		{
			get
			{
				return this.chaFile.custom.body;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x001BDE5F File Offset: 0x001BC25F
		private ChaFileFace face
		{
			get
			{
				return this.chaFile.custom.face;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06004955 RID: 18773 RVA: 0x001BDE71 File Offset: 0x001BC271
		private ChaFileHair hair
		{
			get
			{
				return this.chaFile.custom.hair;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x001BDE83 File Offset: 0x001BC283
		// (set) Token: 0x06004957 RID: 18775 RVA: 0x001BDE90 File Offset: 0x001BC290
		private bool modeNew
		{
			get
			{
				return this.customBase.modeNew;
			}
			set
			{
				this.customBase.modeNew = value;
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x001BDE9E File Offset: 0x001BC29E
		// (set) Token: 0x06004959 RID: 18777 RVA: 0x001BDEAB File Offset: 0x001BC2AB
		private byte modeSex
		{
			get
			{
				return this.customBase.modeSex;
			}
			set
			{
				this.customBase.modeSex = value;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x0600495A RID: 18778 RVA: 0x001BDEB9 File Offset: 0x001BC2B9
		// (set) Token: 0x0600495B RID: 18779 RVA: 0x001BDEC6 File Offset: 0x001BC2C6
		public bool saveMode
		{
			get
			{
				return this._saveMode.Value;
			}
			set
			{
				this._saveMode.Value = value;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x001BDED4 File Offset: 0x001BC2D4
		// (set) Token: 0x0600495D RID: 18781 RVA: 0x001BDEDC File Offset: 0x001BC2DC
		public string overwriteSavePath { get; set; } = string.Empty;

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x0600495E RID: 18782 RVA: 0x001BDEE5 File Offset: 0x001BC2E5
		// (set) Token: 0x0600495F RID: 18783 RVA: 0x001BDEF2 File Offset: 0x001BC2F2
		public bool updatePng
		{
			get
			{
				return this._updatePng.Value;
			}
			set
			{
				this._updatePng.Value = value;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06004960 RID: 18784 RVA: 0x001BDF00 File Offset: 0x001BC300
		// (set) Token: 0x06004961 RID: 18785 RVA: 0x001BDF08 File Offset: 0x001BC308
		public bool showMainCvs { get; set; } = true;

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x001BDF11 File Offset: 0x001BC311
		// (set) Token: 0x06004963 RID: 18787 RVA: 0x001BDF19 File Offset: 0x001BC319
		public bool showFusionCvs { get; set; }

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06004964 RID: 18788 RVA: 0x001BDF22 File Offset: 0x001BC322
		// (set) Token: 0x06004965 RID: 18789 RVA: 0x001BDF2A File Offset: 0x001BC32A
		public bool showDrawMenu { get; set; } = true;

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06004966 RID: 18790 RVA: 0x001BDF33 File Offset: 0x001BC333
		// (set) Token: 0x06004967 RID: 18791 RVA: 0x001BDF3B File Offset: 0x001BC33B
		public bool showColorCvs { get; set; }

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06004968 RID: 18792 RVA: 0x001BDF44 File Offset: 0x001BC344
		// (set) Token: 0x06004969 RID: 18793 RVA: 0x001BDF4C File Offset: 0x001BC34C
		public bool showFileList { get; set; }

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x0600496A RID: 18794 RVA: 0x001BDF55 File Offset: 0x001BC355
		// (set) Token: 0x0600496B RID: 18795 RVA: 0x001BDF5D File Offset: 0x001BC35D
		public bool showPattern { get; set; }

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x0600496C RID: 18796 RVA: 0x001BDF66 File Offset: 0x001BC366
		// (set) Token: 0x0600496D RID: 18797 RVA: 0x001BDF6E File Offset: 0x001BC36E
		public bool showShortcut { get; set; }

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x0600496E RID: 18798 RVA: 0x001BDF77 File Offset: 0x001BC377
		// (set) Token: 0x0600496F RID: 18799 RVA: 0x001BDF7F File Offset: 0x001BC37F
		public bool showInputCoordinate { get; set; }

		// Token: 0x06004970 RID: 18800 RVA: 0x001BDF88 File Offset: 0x001BC388
		public void Initialize(byte _sex, bool _new, string _nextScene, string _editCharaFileName = "")
		{
			this.modeSex = _sex;
			this.modeNew = _new;
			this.customBase.nextSceneName = _nextScene;
			if (!this.modeNew)
			{
				this.customBase.editSaveFileName = _editCharaFileName;
			}
			this.customBase.customCtrl = this;
			this.customBase.saveFrameAssist.Initialize();
			this.customBase.drawSaveFrameTop = false;
			this.customBase.drawSaveFrameBack = true;
			this.customBase.drawSaveFrameFront = true;
			if (this.modeNew)
			{
				this.customBase.defChaCtrl.LoadFromAssetBundle((this.modeSex != 0) ? "custom/00/presets_f_00.unity3d" : "custom/00/presets_m_00.unity3d", (this.modeSex != 0) ? "ill_Default_Female" : "ill_Default_Male", false, true);
			}
			else
			{
				this.customBase.defChaCtrl.LoadCharaFile(this.customBase.editSaveFileName, this.modeSex, false, true);
			}
			VoiceInfo.Param[] array = (from x in Singleton<Voice>.Instance.voiceInfoDic.Values
			where 0 <= x.No
			select x).ToArray<VoiceInfo.Param>();
			foreach (VoiceInfo.Param param in array)
			{
				this.customBase.dictPersonality[param.No] = param.Personality;
			}
			this.InitializeMapControl();
			this.LoadChara();
			this.customBase.poseNo = 1;
			this.customBase.customMotionIK = new MotionIK(this.chaCtrl, false, null);
			this.customBase.customMotionIK.MapIK = false;
			this.customBase.customMotionIK.SetPartners(new MotionIK[]
			{
				this.customBase.customMotionIK
			});
			this.customBase.customMotionIK.Reset();
			if (this.modeSex == 0)
			{
				foreach (GameObject gameObject in this.hideByCondition.objMale)
				{
					if (gameObject)
					{
						gameObject.SetActiveIfDifferent(false);
					}
				}
			}
			else
			{
				foreach (GameObject gameObject2 in this.hideByCondition.objFemale)
				{
					if (gameObject2)
					{
						gameObject2.SetActiveIfDifferent(false);
					}
				}
			}
			if (this.modeNew)
			{
				foreach (GameObject gameObject3 in this.hideByCondition.objNew)
				{
					if (gameObject3)
					{
						gameObject3.SetActiveIfDifferent(false);
					}
				}
			}
			else
			{
				foreach (GameObject gameObject4 in this.hideByCondition.objEdit)
				{
					if (gameObject4)
					{
						gameObject4.SetActiveIfDifferent(false);
					}
				}
			}
			if (this.hideTrialOnly != null && this.hideTrialOnly.objHide != null)
			{
				foreach (GameObject self in this.hideTrialOnly.objHide)
				{
					self.SetActiveIfDifferent(false);
				}
			}
			this.customBase.forceUpdateAcsList = true;
			this.customBase.updateCustomUI = true;
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x001BE2F8 File Offset: 0x001BC6F8
		private void LoadChara()
		{
			Singleton<Character>.Instance.BeginLoadAssetBundle();
			if (this.modeNew)
			{
				this.chaCtrl = Singleton<Character>.Instance.CreateChara(this.modeSex, base.gameObject, 0, null);
				this.chaCtrl.chaFile.pngData = null;
				this.chaCtrl.chaFile.userID = Singleton<GameSystem>.Instance.UserUUID;
				this.chaCtrl.chaFile.dataID = YS_Assist.CreateUUID();
			}
			else
			{
				this.chaCtrl = Singleton<Character>.Instance.CreateChara(this.modeSex, base.gameObject, 0, null);
				this.chaCtrl.chaFile.LoadCharaFile(this.customBase.editSaveFileName, this.modeSex, false, true);
				this.chaCtrl.ChangeNowCoordinate(false, true);
			}
			this.chaCtrl.releaseCustomInputTexture = false;
			this.chaCtrl.Load(false);
			this.chaCtrl.ChangeEyebrowPtn(0, true);
			this.chaCtrl.ChangeEyesPtn(0, true);
			this.chaCtrl.ChangeMouthPtn(0, true);
			this.chaCtrl.ChangeLookEyesPtn(1);
			this.chaCtrl.ChangeLookNeckPtn(0, 1f);
			this.chaCtrl.hideMoz = true;
			this.chaCtrl.fileStatus.visibleSon = false;
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x001BE448 File Offset: 0x001BC848
		public void InitializeScneUI()
		{
			string[] source = new string[]
			{
				"CustomControl",
				"MainMenu",
				"SubMenuFace",
				"SettingWindow",
				"WinFace",
				"DefaultWin",
				"F_FaceType",
				"B_ShapeWhole",
				"H_Hair",
				"C_Clothes",
				"A_Slot",
				"O_Chara",
				"dwChara",
				"Setting01",
				"menuPicker",
				"DrawWindow"
			};
			CanvasGroup[] componentsInChildren = base.GetComponentsInChildren<CanvasGroup>(true);
			if (componentsInChildren != null && componentsInChildren.Length != 0)
			{
				foreach (CanvasGroup canvasGroup in componentsInChildren)
				{
					canvasGroup.Enable(source.Contains(canvasGroup.name), false);
				}
			}
			string[] source2 = new string[]
			{
				"tglFace",
				"SameSettingEyes",
				"AutoHairColor",
				"SameHairColor",
				"ControlTogether",
				"imgRbCol00",
				"imgRB00",
				"tgl01",
				"tglControl",
				"tglCtrlMove",
				"tglDay",
				"tglChangeParentLR",
				"TglType01",
				"tglPlay",
				"TglLoadType01",
				"TglLoadType02",
				"TglLoadType03",
				"TglLoadType04",
				"TglLoadType05",
				"RbHSV",
				"rbPicker",
				"rbSample",
				"ToggleH"
			};
			Toggle[] componentsInChildren2 = base.GetComponentsInChildren<Toggle>(true);
			if (componentsInChildren2 != null && componentsInChildren2.Length != 0)
			{
				foreach (Toggle toggle in componentsInChildren2)
				{
					toggle.isOn = source2.Contains(toggle.name);
				}
			}
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x001BE64D File Offset: 0x001BCA4D
		public void UpdateCharaNameText()
		{
			this.textFullName.text = this.chaCtrl.chaFile.parameter.fullname;
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x001BE670 File Offset: 0x001BCA70
		private void Start()
		{
			if (null == this.camCtrl)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("MainCamera");
				if (gameObject)
				{
					this.camCtrl = gameObject.GetComponent<CameraControl_Ver2>();
				}
			}
			this._saveMode.Subscribe(delegate(bool m)
			{
				this.showMainCvs = !m;
				if (this.objCapCanvas)
				{
					this.objCapCanvas.SetActiveIfDifferent(m);
				}
				if (m)
				{
					this.customBase.cvsCapMenu.BeginCapture();
				}
			});
			this._updatePng.Subscribe(delegate(bool m)
			{
				this.showMainCvs = !m;
				if (this.objCapCanvas)
				{
					this.objCapCanvas.SetActiveIfDifferent(m);
				}
				if (m)
				{
					this.customBase.cvsCapMenu.BeginCapture();
				}
			});
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x001BE6E0 File Offset: 0x001BCAE0
		private void Update()
		{
			bool flag = null != Singleton<Game>.Instance.Config;
			this.lstShow.Clear();
			this.lstShow.Add(this.showMainCvs);
			this.lstShow.Add(!flag);
			this.lstShow.Add(!this.showFusionCvs);
			this.lstShow.Add(!this.showInputCoordinate);
			bool flag2 = YS_Assist.CheckFlagsList(this.lstShow);
			if (this.objMainCanvas)
			{
				this.objMainCanvas.SetActiveIfDifferent(flag2);
			}
			if (this.objSubCanvas)
			{
				this.objSubCanvas.SetActiveIfDifferent(flag2);
			}
			this.lstShow.Clear();
			this.lstShow.Add(this.showFusionCvs);
			this.lstShow.Add(!flag);
			flag2 = YS_Assist.CheckFlagsList(this.lstShow);
			if (this.cvgFusionCanvas)
			{
				this.cvgFusionCanvas.Enable(flag2, false);
			}
			this.lstShow.Clear();
			this.lstShow.Add(this.showDrawMenu);
			this.lstShow.Add(!this.showFusionCvs);
			this.lstShow.Add(!this.showFileList);
			this.lstShow.Add(!flag);
			flag2 = YS_Assist.CheckFlagsList(this.lstShow);
			if (this.cvgDrawCanvas)
			{
				this.cvgDrawCanvas.Enable(flag2, false);
			}
			this.lstShow.Clear();
			this.lstShow.Add(this.showColorCvs);
			this.lstShow.Add(!this.showFusionCvs);
			this.lstShow.Add(this.saveMode || this.updatePng || !this.showFileList);
			this.lstShow.Add(!flag);
			flag2 = YS_Assist.CheckFlagsList(this.lstShow);
			if (this.cvgColorPanel)
			{
				this.cvgColorPanel.Enable(flag2, false);
			}
			this.lstShow.Clear();
			this.lstShow.Add(this.showPattern);
			this.lstShow.Add(!flag);
			flag2 = YS_Assist.CheckFlagsList(this.lstShow);
			if (this.cvgPattern)
			{
				this.cvgPattern.Enable(flag2, false);
			}
			this.lstShow.Clear();
			this.lstShow.Add(this.showInputCoordinate);
			flag2 = YS_Assist.CheckFlagsList(this.lstShow);
			if (this.cvsInputCoordinate)
			{
				this.cvsInputCoordinate.Enable(flag2, false);
			}
			if (this.cvgShortcut)
			{
				this.cvgShortcut.Enable(this.showShortcut, false);
			}
			if (this.saveMode || this.updatePng)
			{
				if (UnityEngine.Input.GetMouseButtonUp(0) || UnityEngine.Input.GetMouseButtonUp(1))
				{
					if (this.camCtrl)
					{
						this.camCtrl.NoCtrlCondition = (() => false);
					}
				}
				else if ((UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButtonDown(1)) && Illusion.Utils.uGUI.isMouseHit && this.camCtrl)
				{
					this.camCtrl.NoCtrlCondition = (() => true);
				}
			}
			else if (UnityEngine.Input.GetMouseButtonUp(0) || UnityEngine.Input.GetMouseButtonUp(1))
			{
				if (this.camCtrl)
				{
					this.camCtrl.NoCtrlCondition = (() => false);
				}
			}
			else if ((UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButtonDown(1)) && Illusion.Utils.uGUI.isMouseHit && this.camCtrl)
			{
				this.camCtrl.NoCtrlCondition = (() => true);
			}
			this.customBase.UpdateIKCalc();
			if (this.customBase.playVoiceBackup.playSampleVoice && !Singleton<Sound>.Instance.IsPlay(Sound.Type.SystemSE, null))
			{
				this.chaCtrl.ChangeEyebrowPtn(this.customBase.playVoiceBackup.backEyebrowPtn, true);
				this.chaCtrl.ChangeEyesPtn(this.customBase.playVoiceBackup.backEyesPtn, true);
				this.chaCtrl.HideEyeHighlight(false);
				this.chaCtrl.ChangeEyesBlinkFlag(this.customBase.playVoiceBackup.backBlink);
				this.chaCtrl.ChangeEyesOpenMax(this.customBase.playVoiceBackup.backEyesOpen);
				this.chaCtrl.ChangeMouthPtn(this.customBase.playVoiceBackup.backMouthPtn, true);
				this.chaCtrl.ChangeMouthFixed(this.customBase.playVoiceBackup.backMouthFix);
				this.chaCtrl.ChangeMouthOpenMax(this.customBase.playVoiceBackup.backMouthOpen);
				this.customBase.playVoiceBackup.playSampleVoice = false;
			}
			if (this.showShortcut)
			{
				if (UnityEngine.Input.GetKeyDown(KeyCode.F2) || UnityEngine.Input.GetMouseButtonUp(0) || UnityEngine.Input.GetMouseButtonUp(1))
				{
					this.showShortcut = false;
					return;
				}
			}
			else if (null != Singleton<Game>.Instance.Config && UnityEngine.Input.GetKeyDown(KeyCode.F3))
			{
				Singleton<Game>.Instance.Config.OnBack();
			}
			bool isInputFocused = this.customBase.IsInputFocused();
			if (!isInputFocused && "CharaCustom" == Singleton<Scene>.Instance.NowSceneNames[0] && !this.showShortcut && null == Singleton<Game>.Instance.Config && null == Singleton<Game>.Instance.ExitScene)
			{
				if (UnityEngine.Input.GetKeyDown(KeyCode.F2))
				{
					this.showShortcut = true;
				}
				else if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
				{
					if (null == Singleton<Game>.Instance.ExitScene)
					{
						AIProject.GameUtil.GameEnd(true);
					}
				}
				else if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
				{
					Config.ActData.Look = !Config.ActData.Look;
					this.customBase.centerDraw = Config.ActData.Look;
				}
				else if (UnityEngine.Input.GetKeyDown(KeyCode.W))
				{
					if (this.customBase.objAcs01ControllerTop.activeSelf || this.customBase.objAcs02ControllerTop.activeSelf)
					{
						this.cvsA_Slot.ShortcutChangeGuidType(0);
					}
					else if (this.customBase.objHairControllerTop.activeSelf)
					{
						this.cvsH_Hair.ShortcutChangeGuidType(0);
					}
				}
				else if (UnityEngine.Input.GetKeyDown(KeyCode.E))
				{
					if (this.customBase.objAcs01ControllerTop.activeSelf || this.customBase.objAcs02ControllerTop.activeSelf)
					{
						this.cvsA_Slot.ShortcutChangeGuidType(1);
					}
					else if (this.customBase.objHairControllerTop.activeSelf)
					{
						this.cvsH_Hair.ShortcutChangeGuidType(1);
					}
				}
				else if (UnityEngine.Input.GetKeyDown(KeyCode.F3) && null == Singleton<Game>.Instance.Config)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					ConfigWindow.UnLoadAction = delegate()
					{
						this.customBase.centerDraw = Config.ActData.Look;
					};
					Singleton<Game>.Instance.LoadConfig();
				}
			}
			if (!this.firstUpdate && Singleton<CustomBase>.IsInstance() && this.customBase.updateCustomUI)
			{
				this.customBase.changeCharaName = true;
				this.customBase.updateCvsFaceType = true;
				this.customBase.updateCvsFaceShapeWhole = true;
				this.customBase.updateCvsFaceShapeChin = true;
				this.customBase.updateCvsFaceShapeCheek = true;
				this.customBase.updateCvsFaceShapeEyebrow = true;
				this.customBase.updateCvsFaceShapeEyes = true;
				this.customBase.updateCvsFaceShapeNose = true;
				this.customBase.updateCvsFaceShapeMouth = true;
				this.customBase.updateCvsFaceShapeEar = true;
				this.customBase.updateCvsMole = true;
				this.customBase.updateCvsEyeLR = true;
				this.customBase.updateCvsEyeEtc = true;
				this.customBase.updateCvsEyeHL = true;
				this.customBase.updateCvsEyebrow = true;
				this.customBase.updateCvsEyelashes = true;
				this.customBase.updateCvsEyeshadow = true;
				this.customBase.updateCvsCheek = true;
				this.customBase.updateCvsLip = true;
				this.customBase.updateCvsFacePaint = true;
				this.customBase.updateCvsBeard = true;
				this.customBase.updateCvsBodyShapeWhole = true;
				this.customBase.updateCvsBodyShapeBreast = true;
				this.customBase.updateCvsBodyShapeUpper = true;
				this.customBase.updateCvsBodyShapeLower = true;
				this.customBase.updateCvsBodyShapeArm = true;
				this.customBase.updateCvsBodyShapeLeg = true;
				this.customBase.updateCvsBodySkinType = true;
				this.customBase.updateCvsSunburn = true;
				this.customBase.updateCvsNip = true;
				this.customBase.updateCvsUnderhair = true;
				this.customBase.updateCvsNail = true;
				this.customBase.updateCvsBodyPaint = true;
				this.customBase.updateCvsFutanari = true;
				this.customBase.updateCvsHair = true;
				this.customBase.updateCvsClothes = true;
				this.customBase.updateCvsClothesSaveDelete = true;
				this.customBase.updateCvsClothesLoad = true;
				this.customBase.updateCvsAccessory = true;
				this.customBase.updateCvsAcsCopy = true;
				this.customBase.updateCvsChara = true;
				this.customBase.updateCvsType = true;
				this.customBase.updateCvsStatus = true;
				this.customBase.updateCvsCharaSaveDelete = true;
				this.customBase.updateCvsCharaLoad = true;
				this.customBase.updateCustomUI = false;
			}
			if (this.camCtrl)
			{
				this.camCtrl.KeyCondition = (() => !isInputFocused);
			}
			this.firstUpdate = false;
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06004976 RID: 18806 RVA: 0x001BF14D File Offset: 0x001BD54D
		// (set) Token: 0x06004977 RID: 18807 RVA: 0x001BF15A File Offset: 0x001BD55A
		public bool draw3D
		{
			get
			{
				return this._draw3D.Value;
			}
			set
			{
				this._draw3D.Value = value;
			}
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x001BF168 File Offset: 0x001BD568
		public void ChangeBGImage(int bf)
		{
			if (this.bgCtrl)
			{
				this.bgCtrl.ChangeBGImage((byte)bf, true);
			}
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x001BF18C File Offset: 0x001BD58C
		public void ChangeBGColor(Color color)
		{
			if (null == this.rendBG)
			{
				return;
			}
			Material material = this.rendBG.material;
			material.SetColor(ChaShader.Color2, color);
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x001BF1C4 File Offset: 0x001BD5C4
		public Color GetBGColor()
		{
			if (null == this.rendBG)
			{
				return Color.white;
			}
			Material material = this.rendBG.material;
			return material.GetColor(ChaShader.Color2);
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x001BF1FF File Offset: 0x001BD5FF
		public void InitializeMapControl()
		{
			this._draw3D.Subscribe(delegate(bool isOn)
			{
				if (this.obj2DTop)
				{
					this.obj2DTop.SetActiveIfDifferent(!isOn);
				}
				if (this.obj3DTop)
				{
					this.obj3DTop.SetActiveIfDifferent(isOn);
				}
			});
		}

		// Token: 0x040043E2 RID: 17378
		[Header("デバッグ ------------------------------")]
		[Button("InitializeScneUI", "シーンUI初期設定", new object[]
		{

		})]
		public int initializescneui;

		// Token: 0x040043E3 RID: 17379
		[Header("メンバ -------------------------------")]
		public CameraControl_Ver2 camCtrl;

		// Token: 0x040043E4 RID: 17380
		[SerializeField]
		private CustomCapture _customCap;

		// Token: 0x040043E5 RID: 17381
		[SerializeField]
		private CvsA_Slot cvsA_Slot;

		// Token: 0x040043E6 RID: 17382
		[SerializeField]
		private CvsH_Hair cvsH_Hair;

		// Token: 0x040043E7 RID: 17383
		public Canvas cvsChangeScene;

		// Token: 0x040043E8 RID: 17384
		[SerializeField]
		private Text textFullName;

		// Token: 0x040043E9 RID: 17385
		[SerializeField]
		private GameObject objMainCanvas;

		// Token: 0x040043EA RID: 17386
		[SerializeField]
		private GameObject objSubCanvas;

		// Token: 0x040043EB RID: 17387
		[SerializeField]
		private CanvasGroup cvgDrawCanvas;

		// Token: 0x040043EC RID: 17388
		[SerializeField]
		private CanvasGroup cvgFusionCanvas;

		// Token: 0x040043ED RID: 17389
		[SerializeField]
		private GameObject objCapCanvas;

		// Token: 0x040043EE RID: 17390
		[SerializeField]
		private CanvasGroup cvgColorPanel;

		// Token: 0x040043EF RID: 17391
		[SerializeField]
		private CanvasGroup cvgPattern;

		// Token: 0x040043F0 RID: 17392
		[SerializeField]
		private CanvasGroup cvgShortcut;

		// Token: 0x040043F1 RID: 17393
		[SerializeField]
		private CanvasGroup cvsInputCoordinate;

		// Token: 0x040043F2 RID: 17394
		private bool firstUpdate = true;

		// Token: 0x040043F3 RID: 17395
		private BoolReactiveProperty _saveMode = new BoolReactiveProperty(false);

		// Token: 0x040043F5 RID: 17397
		private BoolReactiveProperty _updatePng = new BoolReactiveProperty(false);

		// Token: 0x040043F6 RID: 17398
		private List<bool> lstShow = new List<bool>();

		// Token: 0x040043FF RID: 17407
		[Header("スクロールのRaycast -------------------")]
		public CustomControl.SliderScrollRaycast sliderScrollRaycast;

		// Token: 0x04004400 RID: 17408
		[Header("条件による表示 ------------------------")]
		[SerializeField]
		private CustomControl.HideTrial hideTrial;

		// Token: 0x04004401 RID: 17409
		[Header("条件による表示 ------------------------")]
		[SerializeField]
		private CustomControl.HideTrialOnly hideTrialOnly;

		// Token: 0x04004402 RID: 17410
		[Header("条件による表示 ------------------------")]
		[SerializeField]
		private CustomControl.DisplayByCondition hideByCondition;

		// Token: 0x04004403 RID: 17411
		[Header("背景関連 ------------------------------")]
		[SerializeField]
		private BackgroundCtrl bgCtrl;

		// Token: 0x04004404 RID: 17412
		[SerializeField]
		private BoolReactiveProperty _draw3D = new BoolReactiveProperty(true);

		// Token: 0x04004405 RID: 17413
		[SerializeField]
		private GameObject obj2DTop;

		// Token: 0x04004406 RID: 17414
		[SerializeField]
		private GameObject obj3DTop;

		// Token: 0x04004407 RID: 17415
		[SerializeField]
		private Renderer rendBG;

		// Token: 0x020009CC RID: 2508
		[Serializable]
		public class SliderScrollRaycast
		{
			// Token: 0x06004985 RID: 18821 RVA: 0x001BF2F8 File Offset: 0x001BD6F8
			public void ChangeActiveRaycast(bool enable)
			{
				if (this.imgScrollRaycast == null)
				{
					return;
				}
				foreach (Image image in this.imgScrollRaycast)
				{
					if (!(null == image))
					{
						image.raycastTarget = enable;
					}
				}
			}

			// Token: 0x0400440D RID: 17421
			public Image[] imgScrollRaycast;
		}

		// Token: 0x020009CD RID: 2509
		[Serializable]
		public class HideTrial
		{
			// Token: 0x0400440E RID: 17422
			public GameObject[] objHide;
		}

		// Token: 0x020009CE RID: 2510
		[Serializable]
		public class HideTrialOnly
		{
			// Token: 0x0400440F RID: 17423
			public GameObject[] objHide;
		}

		// Token: 0x020009CF RID: 2511
		[Serializable]
		public class DisplayByCondition
		{
			// Token: 0x04004410 RID: 17424
			public GameObject[] objFemale;

			// Token: 0x04004411 RID: 17425
			public GameObject[] objMale;

			// Token: 0x04004412 RID: 17426
			public GameObject[] objNew;

			// Token: 0x04004413 RID: 17427
			public GameObject[] objEdit;
		}
	}
}
