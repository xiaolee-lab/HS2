using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Illusion.Extensions;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace Studio
{
	// Token: 0x02001347 RID: 4935
	public class SystemButtonCtrl : MonoBehaviour
	{
		// Token: 0x17002296 RID: 8854
		// (get) Token: 0x0600A527 RID: 42279 RVA: 0x00435133 File Offset: 0x00433533
		public SunShafts sunShafts
		{
			get
			{
				return this._sunShafts;
			}
		}

		// Token: 0x17002297 RID: 8855
		// (set) Token: 0x0600A528 RID: 42280 RVA: 0x0043513C File Offset: 0x0043353C
		public bool visible
		{
			set
			{
				if (value)
				{
					this.commonInfo.SafeProc(this.select, delegate(SystemButtonCtrl.CommonInfo v)
					{
						v.active = true;
					});
				}
				else
				{
					for (int i = 0; i < this.commonInfo.Length; i++)
					{
						this.commonInfo[i].active = false;
					}
				}
			}
		}

		// Token: 0x0600A529 RID: 42281 RVA: 0x004351AC File Offset: 0x004335AC
		public void OnClickSelect(int _idx)
		{
			if (MathfEx.RangeEqualOn<int>(0, this.select, this.commonInfo.Length - 1))
			{
				this.commonInfo[this.select].active = false;
				if (this.select == 2)
				{
					Singleton<Studio>.Instance.SaveOption();
				}
			}
			this.select = ((this.select != _idx) ? _idx : -1);
			if (MathfEx.RangeEqualOn<int>(0, this.select, this.commonInfo.Length - 1))
			{
				this.commonInfo[this.select].active = true;
			}
			Singleton<Studio>.Instance.colorPalette.visible = false;
		}

		// Token: 0x0600A52A RID: 42282 RVA: 0x00435254 File Offset: 0x00433654
		public void OnClickSave()
		{
			Singleton<Studio>.Instance.colorPalette.visible = false;
			Singleton<Studio>.Instance.SaveScene();
			NotificationScene.spriteMessage = this.spriteSave;
			NotificationScene.waitTime = 1f;
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioNotification",
				isAdd = true
			}, false);
		}

		// Token: 0x0600A52B RID: 42283 RVA: 0x004352B4 File Offset: 0x004336B4
		public void OnClickLoad()
		{
			Singleton<Studio>.Instance.colorPalette.visible = false;
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioSceneLoad",
				isAdd = true
			}, false);
		}

		// Token: 0x0600A52C RID: 42284 RVA: 0x004352F8 File Offset: 0x004336F8
		public void OnClickInit()
		{
			Singleton<Studio>.Instance.colorPalette.visible = false;
			CheckScene.sprite = this.spriteInit;
			CheckScene.unityActionYes = new UnityAction(this.OnSelectInitYes);
			CheckScene.unityActionNo = new UnityAction(this.OnSelectIniteNo);
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioCheck",
				isAdd = true
			}, false);
		}

		// Token: 0x0600A52D RID: 42285 RVA: 0x00435366 File Offset: 0x00433766
		private void OnSelectInitYes()
		{
			Singleton<Scene>.Instance.UnLoad();
			Singleton<Studio>.Instance.InitScene(true);
		}

		// Token: 0x0600A52E RID: 42286 RVA: 0x0043537E File Offset: 0x0043377E
		private void OnSelectIniteNo()
		{
			Singleton<Scene>.Instance.UnLoad();
		}

		// Token: 0x0600A52F RID: 42287 RVA: 0x0043538B File Offset: 0x0043378B
		public void OnClickEnd()
		{
			Singleton<Studio>.Instance.colorPalette.visible = false;
			Singleton<Scene>.Instance.GameEnd(true);
		}

		// Token: 0x0600A530 RID: 42288 RVA: 0x004353A8 File Offset: 0x004337A8
		public void Init()
		{
			if (this.isInit)
			{
				return;
			}
			Camera main = Camera.main;
			GameObject gameObject = (main != null) ? main.gameObject : null;
			if (this.depthOfField == null)
			{
				this.depthOfField = ((gameObject != null) ? gameObject.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>() : null);
			}
			if (this.globalFog == null)
			{
				this.globalFog = ((gameObject != null) ? gameObject.GetComponent<GlobalFog>() : null);
			}
			if (this._sunShafts == null)
			{
				this._sunShafts = ((gameObject != null) ? gameObject.GetComponent<SunShafts>() : null);
			}
			for (int i = 0; i < this.commonInfo.Length; i++)
			{
				this.commonInfo[i].active = false;
			}
			this.colorGradingInfo.Init(this.spriteExpansion, this.postProcessVolume.profile.GetSetting<ColorGrading>(), this.postProcessVolumeColor);
			this.ambientOcclusionInfo.Init(this.spriteExpansion, this.postProcessVolume.profile.GetSetting<AmbientOcclusion>());
			this.bloomInfo.Init(this.spriteExpansion, this.postProcessVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.Bloom>());
			this.dofInfo.Init(this.spriteExpansion, this.depthOfField);
			this.vignetteInfo.Init(this.spriteExpansion, this.postProcessVolume.profile.GetSetting<Vignette>());
			this.screenSpaceReflectionInfo.Init(this.spriteExpansion, this.postProcessVolume.profile.GetSetting<ScreenSpaceReflections>());
			this.reflectionProbeInfo.Init(this.spriteExpansion, this.reflectionProbe, this.objReflectionProbe);
			this.fogInfo.Init(this.spriteExpansion, this.globalFog);
			this.sunShaftsInfo.Init(this.spriteExpansion, this.sunShafts);
			this.selfShadowInfo.Init(this.spriteExpansion);
			this.environmentLightingInfo.Init(this.spriteExpansion);
			this.isInit = true;
			this.effectInfos = new SystemButtonCtrl.EffectInfo[]
			{
				this.colorGradingInfo,
				this.ambientOcclusionInfo,
				this.bloomInfo,
				this.dofInfo,
				this.vignetteInfo,
				this.screenSpaceReflectionInfo,
				this.reflectionProbeInfo,
				this.fogInfo,
				this.sunShaftsInfo,
				this.selfShadowInfo,
				this.environmentLightingInfo
			};
			this.UpdateInfo();
		}

		// Token: 0x0600A531 RID: 42289 RVA: 0x00435620 File Offset: 0x00433A20
		public void UpdateInfo()
		{
			foreach (SystemButtonCtrl.EffectInfo effectInfo in this.effectInfos)
			{
				effectInfo.UpdateInfo();
			}
		}

		// Token: 0x0600A532 RID: 42290 RVA: 0x00435654 File Offset: 0x00433A54
		public void Apply()
		{
			foreach (SystemButtonCtrl.EffectInfo effectInfo in this.effectInfos)
			{
				effectInfo.Apply();
			}
		}

		// Token: 0x0600A533 RID: 42291 RVA: 0x00435688 File Offset: 0x00433A88
		public void SetDepthOfFieldForcus(int _key)
		{
			Transform focalTransform = Singleton<Studio>.Instance.cameraCtrl.targetObj;
			string text = "注視点";
			ObjectCtrlInfo ctrlInfo = Studio.GetCtrlInfo(_key);
			if (ctrlInfo == null || ctrlInfo.kind != 1)
			{
				Singleton<Studio>.Instance.sceneInfo.depthForcus = -1;
			}
			else
			{
				Singleton<Studio>.Instance.sceneInfo.depthForcus = _key;
				focalTransform = (ctrlInfo as OCIItem).objectItem.transform;
				text = (ctrlInfo as OCIItem).treeNodeObject.textName;
			}
			this.depthOfField.focalTransform = focalTransform;
			this.dofInfo.selectorForcus.text = text;
		}

		// Token: 0x0600A534 RID: 42292 RVA: 0x00435728 File Offset: 0x00433B28
		public void SetSunCaster(int _key)
		{
			Transform sunTransform = null;
			string text = "なし";
			ObjectCtrlInfo ctrlInfo = Studio.GetCtrlInfo(_key);
			if (ctrlInfo == null || ctrlInfo.kind != 1)
			{
				Singleton<Studio>.Instance.sceneInfo.sunCaster = -1;
			}
			else
			{
				Singleton<Studio>.Instance.sceneInfo.sunCaster = _key;
				sunTransform = (ctrlInfo as OCIItem).objectItem.transform;
				text = (ctrlInfo as OCIItem).treeNodeObject.textName;
			}
			this.sunShafts.sunTransform = sunTransform;
			this.sunShaftsInfo.selectorCaster.text = text;
		}

		// Token: 0x0600A535 RID: 42293 RVA: 0x004357BA File Offset: 0x00433BBA
		public void MapDependent()
		{
			this.fogInfo.SetEnable(Singleton<Studio>.Instance.sceneInfo.enableFog, true);
		}

		// Token: 0x04008214 RID: 33300
		[SerializeField]
		private SystemButtonCtrl.CommonInfo[] commonInfo = new SystemButtonCtrl.CommonInfo[5];

		// Token: 0x04008215 RID: 33301
		private int select = -1;

		// Token: 0x04008216 RID: 33302
		[SerializeField]
		private Sprite spriteSave;

		// Token: 0x04008217 RID: 33303
		[SerializeField]
		private Sprite spriteInit;

		// Token: 0x04008218 RID: 33304
		[SerializeField]
		[Header("一括制御")]
		private PostProcessVolume postProcessVolume;

		// Token: 0x04008219 RID: 33305
		[SerializeField]
		[Header("ColorGrading用")]
		private PostProcessVolume postProcessVolumeColor;

		// Token: 0x0400821A RID: 33306
		[SerializeField]
		[Header("Reflection Probe制御")]
		private GameObject objReflectionProbe;

		// Token: 0x0400821B RID: 33307
		[SerializeField]
		private ReflectionProbe reflectionProbe;

		// Token: 0x0400821C RID: 33308
		[SerializeField]
		[Header("個別制御")]
		private UnityStandardAssets.ImageEffects.DepthOfField depthOfField;

		// Token: 0x0400821D RID: 33309
		[SerializeField]
		private GlobalFog globalFog;

		// Token: 0x0400821E RID: 33310
		[SerializeField]
		private SunShafts _sunShafts;

		// Token: 0x0400821F RID: 33311
		[SerializeField]
		private Sprite[] spriteExpansion;

		// Token: 0x04008220 RID: 33312
		[SerializeField]
		private SystemButtonCtrl.ColorGradingInfo colorGradingInfo = new SystemButtonCtrl.ColorGradingInfo();

		// Token: 0x04008221 RID: 33313
		[SerializeField]
		private SystemButtonCtrl.AmbientOcclusionInfo ambientOcclusionInfo = new SystemButtonCtrl.AmbientOcclusionInfo();

		// Token: 0x04008222 RID: 33314
		[SerializeField]
		private SystemButtonCtrl.BloomInfo bloomInfo = new SystemButtonCtrl.BloomInfo();

		// Token: 0x04008223 RID: 33315
		[SerializeField]
		private SystemButtonCtrl.DOFInfo dofInfo = new SystemButtonCtrl.DOFInfo();

		// Token: 0x04008224 RID: 33316
		[SerializeField]
		private SystemButtonCtrl.VignetteInfo vignetteInfo = new SystemButtonCtrl.VignetteInfo();

		// Token: 0x04008225 RID: 33317
		[SerializeField]
		private SystemButtonCtrl.ScreenSpaceReflectionInfo screenSpaceReflectionInfo = new SystemButtonCtrl.ScreenSpaceReflectionInfo();

		// Token: 0x04008226 RID: 33318
		[SerializeField]
		private SystemButtonCtrl.ReflectionProbeInfo reflectionProbeInfo = new SystemButtonCtrl.ReflectionProbeInfo();

		// Token: 0x04008227 RID: 33319
		[SerializeField]
		private SystemButtonCtrl.FogInfo fogInfo = new SystemButtonCtrl.FogInfo();

		// Token: 0x04008228 RID: 33320
		[SerializeField]
		private SystemButtonCtrl.SunShaftsInfo sunShaftsInfo = new SystemButtonCtrl.SunShaftsInfo();

		// Token: 0x04008229 RID: 33321
		[SerializeField]
		private SystemButtonCtrl.SelfShadowInfo selfShadowInfo = new SystemButtonCtrl.SelfShadowInfo();

		// Token: 0x0400822A RID: 33322
		[SerializeField]
		private SystemButtonCtrl.EnvironmentLightingInfo environmentLightingInfo = new SystemButtonCtrl.EnvironmentLightingInfo();

		// Token: 0x0400822B RID: 33323
		private SystemButtonCtrl.EffectInfo[] effectInfos;

		// Token: 0x0400822C RID: 33324
		private bool isInit;

		// Token: 0x02001348 RID: 4936
		[Serializable]
		private class CommonInfo
		{
			// Token: 0x17002298 RID: 8856
			// (set) Token: 0x0600A538 RID: 42296 RVA: 0x004357E8 File Offset: 0x00433BE8
			public bool active
			{
				set
				{
					this.group.Enable(value, false);
					this.button.image.color = ((!value) ? Color.white : Color.green);
				}
			}

			// Token: 0x0400822E RID: 33326
			public CanvasGroup group;

			// Token: 0x0400822F RID: 33327
			public Button button;
		}

		// Token: 0x02001349 RID: 4937
		[Serializable]
		private class Selector
		{
			// Token: 0x17002299 RID: 8857
			// (get) Token: 0x0600A53A RID: 42298 RVA: 0x00435824 File Offset: 0x00433C24
			// (set) Token: 0x0600A53B RID: 42299 RVA: 0x00435831 File Offset: 0x00433C31
			public string text
			{
				get
				{
					return this._text.text;
				}
				set
				{
					this._text.text = value;
				}
			}

			// Token: 0x04008230 RID: 33328
			public Button _button;

			// Token: 0x04008231 RID: 33329
			public TextMeshProUGUI _text;
		}

		// Token: 0x0200134A RID: 4938
		[Serializable]
		private class InputCombination
		{
			// Token: 0x1700229A RID: 8858
			// (set) Token: 0x0600A53D RID: 42301 RVA: 0x00435847 File Offset: 0x00433C47
			public bool interactable
			{
				set
				{
					this.input.interactable = value;
					this.slider.interactable = value;
					if (this.buttonDefault)
					{
						this.buttonDefault.interactable = value;
					}
				}
			}

			// Token: 0x1700229B RID: 8859
			// (get) Token: 0x0600A53E RID: 42302 RVA: 0x0043587D File Offset: 0x00433C7D
			// (set) Token: 0x0600A53F RID: 42303 RVA: 0x0043588A File Offset: 0x00433C8A
			public string text
			{
				get
				{
					return this.input.text;
				}
				set
				{
					this.input.text = value;
					this.slider.value = Utility.StringToFloat(value);
				}
			}

			// Token: 0x1700229C RID: 8860
			// (get) Token: 0x0600A540 RID: 42304 RVA: 0x004358A9 File Offset: 0x00433CA9
			// (set) Token: 0x0600A541 RID: 42305 RVA: 0x004358B6 File Offset: 0x00433CB6
			public float value
			{
				get
				{
					return this.slider.value;
				}
				set
				{
					this.slider.value = value;
					this.input.text = value.ToString();
				}
			}

			// Token: 0x1700229D RID: 8861
			// (set) Token: 0x0600A542 RID: 42306 RVA: 0x004358DC File Offset: 0x00433CDC
			public int IntValue
			{
				set
				{
					this.slider.value = (float)value;
					this.input.text = value.ToString();
				}
			}

			// Token: 0x1700229E RID: 8862
			// (get) Token: 0x0600A543 RID: 42307 RVA: 0x00435903 File Offset: 0x00433D03
			public float Min
			{
				[CompilerGenerated]
				get
				{
					return this.slider.minValue;
				}
			}

			// Token: 0x1700229F RID: 8863
			// (get) Token: 0x0600A544 RID: 42308 RVA: 0x00435910 File Offset: 0x00433D10
			public float Max
			{
				[CompilerGenerated]
				get
				{
					return this.slider.maxValue;
				}
			}

			// Token: 0x0600A545 RID: 42309 RVA: 0x00435920 File Offset: 0x00433D20
			public char OnValidateInput(string _text, int _charIndex, char _addedChar)
			{
				switch (_addedChar)
				{
				case '.':
				{
					int num = (this.input.selectionAnchorPosition >= this.input.selectionFocusPosition) ? this.input.selectionFocusPosition : this.input.selectionAnchorPosition;
					int num2 = (this.input.selectionAnchorPosition >= this.input.selectionFocusPosition) ? this.input.selectionAnchorPosition : this.input.selectionFocusPosition;
					if (num == 0 && _text.Length == num2)
					{
						return _addedChar;
					}
					if (_text.Contains('.'))
					{
						return '\0';
					}
					return _addedChar;
				}
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return _addedChar;
				}
				return '\0';
			}

			// Token: 0x04008232 RID: 33330
			public Slider slider;

			// Token: 0x04008233 RID: 33331
			public InputField input;

			// Token: 0x04008234 RID: 33332
			public Button buttonDefault;
		}

		// Token: 0x0200134B RID: 4939
		[Serializable]
		private class EffectInfo
		{
			// Token: 0x170022A0 RID: 8864
			// (get) Token: 0x0600A547 RID: 42311 RVA: 0x00435A14 File Offset: 0x00433E14
			// (set) Token: 0x0600A548 RID: 42312 RVA: 0x00435A21 File Offset: 0x00433E21
			public bool active
			{
				get
				{
					return this.obj.activeSelf;
				}
				set
				{
					if (this.obj.SetActiveIfDifferent(value))
					{
						this.button.image.sprite = this.sprite[(!value) ? 0 : 1];
					}
				}
			}

			// Token: 0x170022A1 RID: 8865
			// (get) Token: 0x0600A549 RID: 42313 RVA: 0x00435A58 File Offset: 0x00433E58
			// (set) Token: 0x0600A54A RID: 42314 RVA: 0x00435A60 File Offset: 0x00433E60
			public bool isUpdateInfo { get; set; }

			// Token: 0x0600A54B RID: 42315 RVA: 0x00435A69 File Offset: 0x00433E69
			public virtual void Init(Sprite[] _sprite)
			{
				this.button.onClick.AddListener(new UnityAction(this.OnClickActive));
				this.sprite = _sprite;
				this.isUpdateInfo = false;
			}

			// Token: 0x0600A54C RID: 42316 RVA: 0x00435A95 File Offset: 0x00433E95
			public virtual void UpdateInfo()
			{
			}

			// Token: 0x0600A54D RID: 42317 RVA: 0x00435A97 File Offset: 0x00433E97
			public virtual void Apply()
			{
			}

			// Token: 0x0600A54E RID: 42318 RVA: 0x00435A99 File Offset: 0x00433E99
			private void OnClickActive()
			{
				this.active = !this.active;
			}

			// Token: 0x04008235 RID: 33333
			public GameObject obj;

			// Token: 0x04008236 RID: 33334
			public Button button;

			// Token: 0x04008237 RID: 33335
			public Sprite[] sprite;
		}

		// Token: 0x0200134C RID: 4940
		[Serializable]
		private class ColorGradingInfo : SystemButtonCtrl.EffectInfo
		{
			// Token: 0x170022A2 RID: 8866
			// (get) Token: 0x0600A550 RID: 42320 RVA: 0x00435AB2 File Offset: 0x00433EB2
			// (set) Token: 0x0600A551 RID: 42321 RVA: 0x00435ABA File Offset: 0x00433EBA
			private ColorGrading ColorGrading { get; set; }

			// Token: 0x170022A3 RID: 8867
			// (get) Token: 0x0600A552 RID: 42322 RVA: 0x00435AC3 File Offset: 0x00433EC3
			// (set) Token: 0x0600A553 RID: 42323 RVA: 0x00435ACB File Offset: 0x00433ECB
			private PostProcessVolume PostProcessVolumeBlend { get; set; }

			// Token: 0x170022A4 RID: 8868
			// (get) Token: 0x0600A554 RID: 42324 RVA: 0x00435AD4 File Offset: 0x00433ED4
			// (set) Token: 0x0600A555 RID: 42325 RVA: 0x00435ADC File Offset: 0x00433EDC
			private ColorGrading ColorGradingBlend { get; set; }

			// Token: 0x170022A5 RID: 8869
			// (set) Token: 0x0600A556 RID: 42326 RVA: 0x00435AE5 File Offset: 0x00433EE5
			private float Blend
			{
				set
				{
					this.PostProcessVolumeBlend.weight = Mathf.Clamp(value, 0f, 1f);
				}
			}

			// Token: 0x170022A6 RID: 8870
			// (set) Token: 0x0600A557 RID: 42327 RVA: 0x00435B04 File Offset: 0x00433F04
			private float Saturation
			{
				set
				{
					this.ColorGrading.SafeProc(delegate(ColorGrading _cg)
					{
						_cg.saturation.value = value;
					});
					this.ColorGradingBlend.SafeProc(delegate(ColorGrading _cg)
					{
						_cg.saturation.value = value;
					});
				}
			}

			// Token: 0x170022A7 RID: 8871
			// (set) Token: 0x0600A558 RID: 42328 RVA: 0x00435B50 File Offset: 0x00433F50
			private float Brightness
			{
				set
				{
					this.ColorGrading.SafeProc(delegate(ColorGrading _cg)
					{
						_cg.brightness.value = value;
					});
					this.ColorGradingBlend.SafeProc(delegate(ColorGrading _cg)
					{
						_cg.brightness.value = value;
					});
				}
			}

			// Token: 0x170022A8 RID: 8872
			// (set) Token: 0x0600A559 RID: 42329 RVA: 0x00435B9C File Offset: 0x00433F9C
			private float Contrast
			{
				set
				{
					this.ColorGrading.SafeProc(delegate(ColorGrading _cg)
					{
						_cg.contrast.value = value;
					});
					this.ColorGradingBlend.SafeProc(delegate(ColorGrading _cg)
					{
						_cg.contrast.value = value;
					});
				}
			}

			// Token: 0x0600A55A RID: 42330 RVA: 0x00435BE8 File Offset: 0x00433FE8
			public void Init(Sprite[] _sprite, ColorGrading _colorGrading, PostProcessVolume _postProcessVolume)
			{
				base.Init(_sprite);
				this.ColorGrading = _colorGrading;
				this.PostProcessVolumeBlend = _postProcessVolume;
				this.ColorGradingBlend = this.PostProcessVolumeBlend.profile.GetSetting<ColorGrading>();
				this.dropdownLookupTexture.options = (from v in Singleton<Info>.Instance.dicColorGradingLoadInfo
				select new Dropdown.OptionData(v.Value.name)).ToList<Dropdown.OptionData>();
				this.dropdownLookupTexture.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChangedLookupTexture));
				this.icBlend.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedBlend));
				InputField input = this.icBlend.input;
				input.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input.onValidateInput, new InputField.OnValidateInput(this.icBlend.OnValidateInput));
				this.icBlend.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditBlend));
				this.icBlend.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickBlend));
				this.icSaturation.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedSaturation));
				this.icSaturation.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSaturation));
				this.icSaturation.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickSaturation));
				this.icBrightness.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedBrightness));
				this.icBrightness.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditBrightness));
				this.icBrightness.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickBrightness));
				this.icContrast.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedContrast));
				this.icContrast.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditContrast));
				this.icContrast.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickContrast));
			}

			// Token: 0x0600A55B RID: 42331 RVA: 0x00435E38 File Offset: 0x00434238
			public override void UpdateInfo()
			{
				base.UpdateInfo();
				base.isUpdateInfo = true;
				this.dropdownLookupTexture.value = Singleton<Studio>.Instance.sceneInfo.cgLookupTexture;
				this.icBlend.value = Singleton<Studio>.Instance.sceneInfo.cgBlend;
				this.icSaturation.IntValue = Singleton<Studio>.Instance.sceneInfo.cgSaturation;
				this.icBrightness.IntValue = Singleton<Studio>.Instance.sceneInfo.cgBrightness;
				this.icContrast.IntValue = Singleton<Studio>.Instance.sceneInfo.cgContrast;
				this.Apply();
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A55C RID: 42332 RVA: 0x00435EE4 File Offset: 0x004342E4
			public override void Apply()
			{
				this.Blend = Singleton<Studio>.Instance.sceneInfo.cgBlend;
				this.Saturation = (float)Singleton<Studio>.Instance.sceneInfo.cgSaturation;
				this.Brightness = (float)Singleton<Studio>.Instance.sceneInfo.cgBrightness;
				this.Contrast = (float)Singleton<Studio>.Instance.sceneInfo.cgContrast;
				this.SetLookupTexture(Singleton<Studio>.Instance.sceneInfo.cgLookupTexture);
			}

			// Token: 0x0600A55D RID: 42333 RVA: 0x00435F60 File Offset: 0x00434360
			public void SetLookupTexture(int _no)
			{
				Singleton<Studio>.Instance.sceneInfo.cgLookupTexture = _no;
				Info.LoadCommonInfo loadCommonInfo = null;
				if (!Singleton<Info>.Instance.dicColorGradingLoadInfo.TryGetValue(_no, out loadCommonInfo))
				{
					return;
				}
				Texture x = CommonLib.LoadAsset<Texture>(loadCommonInfo.bundlePath, loadCommonInfo.fileName, false, string.Empty);
				this.ColorGradingBlend.ldrLut.Override(x);
			}

			// Token: 0x0600A55E RID: 42334 RVA: 0x00435FC0 File Offset: 0x004343C0
			private void OnValueChangedLookupTexture(int _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				this.SetLookupTexture(_value);
			}

			// Token: 0x0600A55F RID: 42335 RVA: 0x00435FD5 File Offset: 0x004343D5
			private void OnValueChangedBlend(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.cgBlend = _value;
				this.Blend = _value;
				this.icBlend.value = _value;
			}

			// Token: 0x0600A560 RID: 42336 RVA: 0x00436008 File Offset: 0x00434408
			private void OnEndEditBlend(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icBlend.Min, this.icBlend.Max);
				Singleton<Studio>.Instance.sceneInfo.cgBlend = num;
				this.Blend = num;
				this.icBlend.value = num;
			}

			// Token: 0x0600A561 RID: 42337 RVA: 0x00436066 File Offset: 0x00434466
			private void OnClickBlend()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.cgBlend = ScreenEffectDefine.ColorGradingBlend;
				this.Blend = ScreenEffectDefine.ColorGradingBlend;
				this.icBlend.value = ScreenEffectDefine.ColorGradingBlend;
			}

			// Token: 0x0600A562 RID: 42338 RVA: 0x004360A4 File Offset: 0x004344A4
			private void OnValueChangedSaturation(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.cgSaturation = Mathf.FloorToInt(_value);
				this.Saturation = _value;
				this.icSaturation.IntValue = Singleton<Studio>.Instance.sceneInfo.cgSaturation;
			}

			// Token: 0x0600A563 RID: 42339 RVA: 0x004360F4 File Offset: 0x004344F4
			private void OnEndEditSaturation(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				int num = Mathf.FloorToInt(Mathf.Clamp(Utility.StringToFloat(_text), this.icSaturation.Min, this.icSaturation.Max));
				Singleton<Studio>.Instance.sceneInfo.cgSaturation = num;
				this.Saturation = (float)num;
				this.icSaturation.IntValue = num;
			}

			// Token: 0x0600A564 RID: 42340 RVA: 0x00436158 File Offset: 0x00434558
			private void OnClickSaturation()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.cgSaturation = ScreenEffectDefine.ColorGradingSaturation;
				this.Saturation = (float)ScreenEffectDefine.ColorGradingSaturation;
				this.icSaturation.IntValue = ScreenEffectDefine.ColorGradingSaturation;
			}

			// Token: 0x0600A565 RID: 42341 RVA: 0x00436198 File Offset: 0x00434598
			private void OnValueChangedBrightness(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.cgBrightness = Mathf.FloorToInt(_value);
				this.Brightness = _value;
				this.icBrightness.IntValue = Singleton<Studio>.Instance.sceneInfo.cgBrightness;
			}

			// Token: 0x0600A566 RID: 42342 RVA: 0x004361E8 File Offset: 0x004345E8
			private void OnEndEditBrightness(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				int num = Mathf.FloorToInt(Mathf.Clamp(Utility.StringToFloat(_text), this.icBrightness.Min, this.icBrightness.Max));
				Singleton<Studio>.Instance.sceneInfo.cgBrightness = num;
				this.Brightness = (float)num;
				this.icBrightness.IntValue = num;
			}

			// Token: 0x0600A567 RID: 42343 RVA: 0x0043624C File Offset: 0x0043464C
			private void OnClickBrightness()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.cgBrightness = ScreenEffectDefine.ColorGradingBrightness;
				this.Brightness = (float)ScreenEffectDefine.ColorGradingBrightness;
				this.icBrightness.IntValue = ScreenEffectDefine.ColorGradingBrightness;
			}

			// Token: 0x0600A568 RID: 42344 RVA: 0x0043628C File Offset: 0x0043468C
			private void OnValueChangedContrast(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.cgContrast = Mathf.FloorToInt(_value);
				this.Contrast = _value;
				this.icContrast.IntValue = Singleton<Studio>.Instance.sceneInfo.cgContrast;
			}

			// Token: 0x0600A569 RID: 42345 RVA: 0x004362DC File Offset: 0x004346DC
			private void OnEndEditContrast(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				int num = Mathf.FloorToInt(Mathf.Clamp(Utility.StringToFloat(_text), this.icContrast.Min, this.icContrast.Max));
				Singleton<Studio>.Instance.sceneInfo.cgContrast = num;
				this.Contrast = (float)num;
				this.icContrast.IntValue = num;
			}

			// Token: 0x0600A56A RID: 42346 RVA: 0x00436340 File Offset: 0x00434740
			private void OnClickContrast()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.cgContrast = ScreenEffectDefine.ColorGradingSaturation;
				this.Contrast = (float)ScreenEffectDefine.ColorGradingSaturation;
				this.icContrast.IntValue = ScreenEffectDefine.ColorGradingSaturation;
			}

			// Token: 0x04008239 RID: 33337
			public Dropdown dropdownLookupTexture;

			// Token: 0x0400823A RID: 33338
			public SystemButtonCtrl.InputCombination icBlend;

			// Token: 0x0400823B RID: 33339
			public SystemButtonCtrl.InputCombination icSaturation;

			// Token: 0x0400823C RID: 33340
			public SystemButtonCtrl.InputCombination icBrightness;

			// Token: 0x0400823D RID: 33341
			public SystemButtonCtrl.InputCombination icContrast;
		}

		// Token: 0x0200134D RID: 4941
		[Serializable]
		private class AmbientOcclusionInfo : SystemButtonCtrl.EffectInfo
		{
			// Token: 0x170022A9 RID: 8873
			// (get) Token: 0x0600A56D RID: 42349 RVA: 0x00436423 File Offset: 0x00434823
			// (set) Token: 0x0600A56E RID: 42350 RVA: 0x0043642B File Offset: 0x0043482B
			private AmbientOcclusion AmbientOcculusion { get; set; }

			// Token: 0x0600A56F RID: 42351 RVA: 0x00436434 File Offset: 0x00434834
			public void Init(Sprite[] _sprite, AmbientOcclusion _ambientOcculusion)
			{
				base.Init(_sprite);
				this.AmbientOcculusion = _ambientOcculusion;
				this.toggleEnable.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedEnable));
				this.buttonColor.onClick.AddListener(new UnityAction(this.OnClickColor));
				this.icIntensity.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedIntensity));
				InputField input = this.icIntensity.input;
				input.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input.onValidateInput, new InputField.OnValidateInput(this.icIntensity.OnValidateInput));
				this.icIntensity.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditIntensity));
				this.icIntensity.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickIntensity));
				this.icThicknessModeifier.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedThicknessModeifier));
				InputField input2 = this.icThicknessModeifier.input;
				input2.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input2.onValidateInput, new InputField.OnValidateInput(this.icThicknessModeifier.OnValidateInput));
				this.icThicknessModeifier.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditThicknessModeifier));
				this.icThicknessModeifier.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickThicknessModeifier));
			}

			// Token: 0x0600A570 RID: 42352 RVA: 0x004365B0 File Offset: 0x004349B0
			public override void UpdateInfo()
			{
				base.UpdateInfo();
				base.isUpdateInfo = true;
				this.toggleEnable.isOn = Singleton<Studio>.Instance.sceneInfo.enableAmbientOcclusion;
				this.buttonColor.image.color = Singleton<Studio>.Instance.sceneInfo.aoColor;
				this.icIntensity.value = Singleton<Studio>.Instance.sceneInfo.aoIntensity;
				this.icThicknessModeifier.value = Singleton<Studio>.Instance.sceneInfo.aoThicknessModeifier;
				this.Apply();
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A571 RID: 42353 RVA: 0x00436644 File Offset: 0x00434A44
			public override void Apply()
			{
				if (this.AmbientOcculusion == null)
				{
					return;
				}
				this.AmbientOcculusion.active = Singleton<Studio>.Instance.sceneInfo.enableAmbientOcclusion;
				this.AmbientOcculusion.color.value = Singleton<Studio>.Instance.sceneInfo.aoColor;
				this.AmbientOcculusion.intensity.value = Singleton<Studio>.Instance.sceneInfo.aoIntensity;
				this.AmbientOcculusion.thicknessModifier.value = Singleton<Studio>.Instance.sceneInfo.aoThicknessModeifier;
			}

			// Token: 0x0600A572 RID: 42354 RVA: 0x004366DA File Offset: 0x00434ADA
			private void OnValueChangedEnable(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.enableAmbientOcclusion = _value;
				this.AmbientOcculusion.active = _value;
			}

			// Token: 0x0600A573 RID: 42355 RVA: 0x00436704 File Offset: 0x00434B04
			private void OnClickColor()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				if (Singleton<Studio>.Instance.colorPalette.Check("アンビエントオクルージョン"))
				{
					Singleton<Studio>.Instance.colorPalette.visible = false;
					return;
				}
				Singleton<Studio>.Instance.colorPalette.Setup("アンビエントオクルージョン", Singleton<Studio>.Instance.sceneInfo.aoColor, delegate(Color _c)
				{
					Singleton<Studio>.Instance.sceneInfo.aoColor = _c;
					this.AmbientOcculusion.color.value = _c;
					this.buttonColor.image.color = _c;
				}, false);
			}

			// Token: 0x0600A574 RID: 42356 RVA: 0x00436777 File Offset: 0x00434B77
			private void OnValueChangedIntensity(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.aoIntensity = _value;
				this.AmbientOcculusion.intensity.value = _value;
				this.icIntensity.value = _value;
			}

			// Token: 0x0600A575 RID: 42357 RVA: 0x004367B4 File Offset: 0x00434BB4
			private void OnEndEditIntensity(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icIntensity.Min, this.icIntensity.Max);
				Singleton<Studio>.Instance.sceneInfo.aoIntensity = num;
				this.AmbientOcculusion.intensity.value = num;
				this.icIntensity.value = num;
			}

			// Token: 0x0600A576 RID: 42358 RVA: 0x0043681C File Offset: 0x00434C1C
			private void OnClickIntensity()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.aoIntensity = ScreenEffectDefine.AmbientOcclusionIntensity;
				this.AmbientOcculusion.intensity.value = ScreenEffectDefine.AmbientOcclusionIntensity;
				this.icIntensity.value = ScreenEffectDefine.AmbientOcclusionIntensity;
			}

			// Token: 0x0600A577 RID: 42359 RVA: 0x0043686E File Offset: 0x00434C6E
			private void OnValueChangedThicknessModeifier(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.aoThicknessModeifier = _value;
				this.AmbientOcculusion.thicknessModifier.value = _value;
				this.icThicknessModeifier.value = _value;
			}

			// Token: 0x0600A578 RID: 42360 RVA: 0x004368AC File Offset: 0x00434CAC
			private void OnEndEditThicknessModeifier(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icThicknessModeifier.Min, this.icThicknessModeifier.Max);
				Singleton<Studio>.Instance.sceneInfo.aoThicknessModeifier = num;
				this.AmbientOcculusion.thicknessModifier.value = num;
				this.icThicknessModeifier.value = num;
			}

			// Token: 0x0600A579 RID: 42361 RVA: 0x00436914 File Offset: 0x00434D14
			private void OnClickThicknessModeifier()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.aoThicknessModeifier = ScreenEffectDefine.AmbientOcclusionThicknessModeifier;
				this.AmbientOcculusion.thicknessModifier.value = ScreenEffectDefine.AmbientOcclusionThicknessModeifier;
				this.icThicknessModeifier.value = ScreenEffectDefine.AmbientOcclusionThicknessModeifier;
			}

			// Token: 0x04008242 RID: 33346
			public Toggle toggleEnable;

			// Token: 0x04008243 RID: 33347
			public Button buttonColor;

			// Token: 0x04008244 RID: 33348
			public SystemButtonCtrl.InputCombination icIntensity;

			// Token: 0x04008245 RID: 33349
			public SystemButtonCtrl.InputCombination icThicknessModeifier;
		}

		// Token: 0x0200134E RID: 4942
		[Serializable]
		private class BloomInfo : SystemButtonCtrl.EffectInfo
		{
			// Token: 0x170022AA RID: 8874
			// (get) Token: 0x0600A57C RID: 42364 RVA: 0x004369A2 File Offset: 0x00434DA2
			// (set) Token: 0x0600A57D RID: 42365 RVA: 0x004369AA File Offset: 0x00434DAA
			private UnityEngine.Rendering.PostProcessing.Bloom Bloom { get; set; }

			// Token: 0x0600A57E RID: 42366 RVA: 0x004369B4 File Offset: 0x00434DB4
			public void Init(Sprite[] _sprite, UnityEngine.Rendering.PostProcessing.Bloom _bloom)
			{
				base.Init(_sprite);
				this.Bloom = _bloom;
				this.toggleEnable.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedEnable));
				this.icIntensity.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedIntensity));
				InputField input = this.icIntensity.input;
				input.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input.onValidateInput, new InputField.OnValidateInput(this.icIntensity.OnValidateInput));
				this.icIntensity.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditIntensity));
				this.icIntensity.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickIntensityDef));
				this.icThreshold.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedThreshold));
				InputField input2 = this.icThreshold.input;
				input2.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input2.onValidateInput, new InputField.OnValidateInput(this.icThreshold.OnValidateInput));
				this.icThreshold.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditThreshold));
				this.icThreshold.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickThresholdDef));
				this.icSoftKnee.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedSoftKnee));
				InputField input3 = this.icSoftKnee.input;
				input3.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input3.onValidateInput, new InputField.OnValidateInput(this.icSoftKnee.OnValidateInput));
				this.icSoftKnee.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSoftKnee));
				this.icSoftKnee.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickSoftKnee));
				this.toggleClamp.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedClamp));
				this.icDiffusion.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedDiffusion));
				InputField input4 = this.icDiffusion.input;
				input4.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input4.onValidateInput, new InputField.OnValidateInput(this.icDiffusion.OnValidateInput));
				this.icDiffusion.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditDiffusion));
				this.icDiffusion.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickDiffusion));
				this.buttonColor.onClick.AddListener(new UnityAction(this.OnClickColor));
			}

			// Token: 0x0600A57F RID: 42367 RVA: 0x00436C74 File Offset: 0x00435074
			public override void UpdateInfo()
			{
				base.UpdateInfo();
				base.isUpdateInfo = true;
				this.toggleEnable.isOn = Singleton<Studio>.Instance.sceneInfo.enableBloom;
				this.icIntensity.value = Singleton<Studio>.Instance.sceneInfo.bloomIntensity;
				this.icThreshold.value = Singleton<Studio>.Instance.sceneInfo.bloomThreshold;
				this.icSoftKnee.value = Singleton<Studio>.Instance.sceneInfo.bloomSoftKnee;
				this.toggleClamp.isOn = Singleton<Studio>.Instance.sceneInfo.bloomClamp;
				this.icDiffusion.value = Singleton<Studio>.Instance.sceneInfo.bloomDiffusion;
				this.buttonColor.image.color = Singleton<Studio>.Instance.sceneInfo.bloomColor;
				this.Apply();
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A580 RID: 42368 RVA: 0x00436D58 File Offset: 0x00435158
			public override void Apply()
			{
				if (this.Bloom == null)
				{
					return;
				}
				this.Bloom.active = Singleton<Studio>.Instance.sceneInfo.enableBloom;
				this.Bloom.intensity.value = Singleton<Studio>.Instance.sceneInfo.bloomIntensity;
				this.Bloom.threshold.value = Singleton<Studio>.Instance.sceneInfo.bloomThreshold;
				this.Bloom.softKnee.value = Singleton<Studio>.Instance.sceneInfo.bloomSoftKnee;
				this.Bloom.clamp.overrideState = Singleton<Studio>.Instance.sceneInfo.bloomClamp;
				this.Bloom.diffusion.value = Singleton<Studio>.Instance.sceneInfo.bloomDiffusion;
				this.Bloom.color.value = Singleton<Studio>.Instance.sceneInfo.bloomColor;
			}

			// Token: 0x0600A581 RID: 42369 RVA: 0x00436E4B File Offset: 0x0043524B
			private void OnValueChangedEnable(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.enableBloom = _value;
				this.Bloom.active = _value;
			}

			// Token: 0x0600A582 RID: 42370 RVA: 0x00436E75 File Offset: 0x00435275
			private void OnValueChangedIntensity(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.bloomIntensity = _value;
				this.Bloom.intensity.value = _value;
				this.icIntensity.value = _value;
			}

			// Token: 0x0600A583 RID: 42371 RVA: 0x00436EB0 File Offset: 0x004352B0
			private void OnEndEditIntensity(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icIntensity.Min, this.icIntensity.Max);
				Singleton<Studio>.Instance.sceneInfo.bloomIntensity = num;
				this.Bloom.intensity.value = num;
				this.icIntensity.value = num;
			}

			// Token: 0x0600A584 RID: 42372 RVA: 0x00436F18 File Offset: 0x00435318
			private void OnClickIntensityDef()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.bloomIntensity = ScreenEffectDefine.BloomIntensity;
				this.Bloom.intensity.value = ScreenEffectDefine.BloomIntensity;
				this.icIntensity.value = ScreenEffectDefine.BloomIntensity;
			}

			// Token: 0x0600A585 RID: 42373 RVA: 0x00436F6A File Offset: 0x0043536A
			private void OnValueChangedThreshold(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.bloomThreshold = _value;
				this.Bloom.threshold.value = _value;
				this.icThreshold.value = _value;
			}

			// Token: 0x0600A586 RID: 42374 RVA: 0x00436FA8 File Offset: 0x004353A8
			private void OnEndEditThreshold(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icThreshold.Min, this.icThreshold.Max);
				Singleton<Studio>.Instance.sceneInfo.bloomThreshold = num;
				this.Bloom.threshold.value = num;
				this.icThreshold.value = num;
			}

			// Token: 0x0600A587 RID: 42375 RVA: 0x00437010 File Offset: 0x00435410
			private void OnClickThresholdDef()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.bloomThreshold = ScreenEffectDefine.BloomThreshold;
				this.Bloom.threshold.value = ScreenEffectDefine.BloomThreshold;
				this.icThreshold.value = ScreenEffectDefine.BloomThreshold;
			}

			// Token: 0x0600A588 RID: 42376 RVA: 0x00437062 File Offset: 0x00435462
			private void OnValueChangedSoftKnee(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.bloomSoftKnee = _value;
				this.Bloom.softKnee.value = _value;
				this.icSoftKnee.value = _value;
			}

			// Token: 0x0600A589 RID: 42377 RVA: 0x004370A0 File Offset: 0x004354A0
			private void OnEndEditSoftKnee(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icSoftKnee.Min, this.icSoftKnee.Max);
				Singleton<Studio>.Instance.sceneInfo.bloomSoftKnee = num;
				this.Bloom.softKnee.value = num;
				this.icSoftKnee.value = num;
			}

			// Token: 0x0600A58A RID: 42378 RVA: 0x00437108 File Offset: 0x00435508
			private void OnClickSoftKnee()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.bloomSoftKnee = ScreenEffectDefine.BloomSoftKnee;
				this.Bloom.softKnee.value = ScreenEffectDefine.BloomSoftKnee;
				this.icSoftKnee.value = ScreenEffectDefine.BloomSoftKnee;
			}

			// Token: 0x0600A58B RID: 42379 RVA: 0x0043715A File Offset: 0x0043555A
			private void OnValueChangedClamp(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.bloomClamp = _value;
				this.Bloom.clamp.overrideState = _value;
			}

			// Token: 0x0600A58C RID: 42380 RVA: 0x00437189 File Offset: 0x00435589
			private void OnValueChangedDiffusion(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.bloomDiffusion = _value;
				this.Bloom.diffusion.value = _value;
				this.icDiffusion.value = _value;
			}

			// Token: 0x0600A58D RID: 42381 RVA: 0x004371C4 File Offset: 0x004355C4
			private void OnEndEditDiffusion(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icDiffusion.Min, this.icDiffusion.Max);
				Singleton<Studio>.Instance.sceneInfo.bloomDiffusion = num;
				this.Bloom.diffusion.value = num;
				this.icDiffusion.value = num;
			}

			// Token: 0x0600A58E RID: 42382 RVA: 0x0043722C File Offset: 0x0043562C
			private void OnClickDiffusion()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.bloomDiffusion = ScreenEffectDefine.BloomDiffusion;
				this.Bloom.diffusion.value = ScreenEffectDefine.BloomDiffusion;
				this.icDiffusion.value = ScreenEffectDefine.BloomDiffusion;
			}

			// Token: 0x0600A58F RID: 42383 RVA: 0x00437280 File Offset: 0x00435680
			private void OnClickColor()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				if (Singleton<Studio>.Instance.colorPalette.Check("ブルーム"))
				{
					Singleton<Studio>.Instance.colorPalette.visible = false;
					return;
				}
				Singleton<Studio>.Instance.colorPalette.Setup("ブルーム", Singleton<Studio>.Instance.sceneInfo.bloomColor, delegate(Color _c)
				{
					Singleton<Studio>.Instance.sceneInfo.bloomColor = _c;
					this.Bloom.color.value = _c;
					this.buttonColor.image.color = _c;
				}, false);
			}

			// Token: 0x04008247 RID: 33351
			public Toggle toggleEnable;

			// Token: 0x04008248 RID: 33352
			public SystemButtonCtrl.InputCombination icIntensity;

			// Token: 0x04008249 RID: 33353
			public SystemButtonCtrl.InputCombination icThreshold;

			// Token: 0x0400824A RID: 33354
			public SystemButtonCtrl.InputCombination icSoftKnee;

			// Token: 0x0400824B RID: 33355
			public Toggle toggleClamp;

			// Token: 0x0400824C RID: 33356
			public SystemButtonCtrl.InputCombination icDiffusion;

			// Token: 0x0400824D RID: 33357
			public Button buttonColor;
		}

		// Token: 0x0200134F RID: 4943
		[Serializable]
		private class DOFInfo : SystemButtonCtrl.EffectInfo
		{
			// Token: 0x170022AB RID: 8875
			// (get) Token: 0x0600A592 RID: 42386 RVA: 0x0043732F File Offset: 0x0043572F
			// (set) Token: 0x0600A593 RID: 42387 RVA: 0x00437337 File Offset: 0x00435737
			private UnityStandardAssets.ImageEffects.DepthOfField depthOfField { get; set; }

			// Token: 0x0600A594 RID: 42388 RVA: 0x00437340 File Offset: 0x00435740
			public void Init(Sprite[] _sprite, UnityStandardAssets.ImageEffects.DepthOfField _dof)
			{
				base.Init(_sprite);
				this.depthOfField = _dof;
				this.toggleEnable.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedEnable));
				this.selectorForcus._button.onClick.AddListener(new UnityAction(this.OnClickForcus));
				this.icFocalSize.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedFocalSize));
				InputField input = this.icFocalSize.input;
				input.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input.onValidateInput, new InputField.OnValidateInput(this.icFocalSize.OnValidateInput));
				this.icFocalSize.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditFocalSize));
				this.icFocalSize.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickFocalSizeDef));
				this.icAperture.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedAperture));
				InputField input2 = this.icAperture.input;
				input2.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input2.onValidateInput, new InputField.OnValidateInput(this.icAperture.OnValidateInput));
				this.icAperture.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditAperture));
				this.icAperture.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickApertureDef));
			}

			// Token: 0x0600A595 RID: 42389 RVA: 0x004374C0 File Offset: 0x004358C0
			public override void UpdateInfo()
			{
				base.UpdateInfo();
				base.isUpdateInfo = true;
				this.toggleEnable.isOn = Singleton<Studio>.Instance.sceneInfo.enableDepth;
				this.icFocalSize.value = Singleton<Studio>.Instance.sceneInfo.depthFocalSize;
				this.icAperture.value = Singleton<Studio>.Instance.sceneInfo.depthAperture;
				this.Apply();
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A596 RID: 42390 RVA: 0x00437538 File Offset: 0x00435938
			public override void Apply()
			{
				if (this.depthOfField != null)
				{
					this.depthOfField.enabled = Singleton<Studio>.Instance.sceneInfo.enableDepth;
					this.depthOfField.focalSize = Singleton<Studio>.Instance.sceneInfo.depthFocalSize;
					this.depthOfField.aperture = Singleton<Studio>.Instance.sceneInfo.depthAperture;
				}
				Singleton<Studio>.Instance.SetDepthOfFieldForcus(Singleton<Studio>.Instance.sceneInfo.depthForcus);
			}

			// Token: 0x0600A597 RID: 42391 RVA: 0x004375BD File Offset: 0x004359BD
			private void OnValueChangedEnable(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.enableDepth = _value;
				this.depthOfField.enabled = _value;
			}

			// Token: 0x0600A598 RID: 42392 RVA: 0x004375E8 File Offset: 0x004359E8
			private void OnClickForcus()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				GuideObject selectObject = Singleton<GuideObjectManager>.Instance.selectObject;
				Singleton<Studio>.Instance.SetDepthOfFieldForcus((!(selectObject != null)) ? -1 : selectObject.dicKey);
			}

			// Token: 0x0600A599 RID: 42393 RVA: 0x0043762E File Offset: 0x00435A2E
			private void OnValueChangedFocalSize(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.depthFocalSize = _value;
				this.depthOfField.focalSize = _value;
				this.icFocalSize.value = _value;
			}

			// Token: 0x0600A59A RID: 42394 RVA: 0x00437664 File Offset: 0x00435A64
			private void OnEndEditFocalSize(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icFocalSize.Min, this.icFocalSize.Max);
				Singleton<Studio>.Instance.sceneInfo.depthFocalSize = num;
				this.depthOfField.focalSize = num;
				this.icFocalSize.value = num;
			}

			// Token: 0x0600A59B RID: 42395 RVA: 0x004376C8 File Offset: 0x00435AC8
			private void OnClickFocalSizeDef()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.depthFocalSize = ScreenEffectDefine.DepthOfFieldFocalSize;
				this.depthOfField.focalSize = ScreenEffectDefine.DepthOfFieldFocalSize;
				this.icFocalSize.value = ScreenEffectDefine.DepthOfFieldFocalSize;
			}

			// Token: 0x0600A59C RID: 42396 RVA: 0x00437715 File Offset: 0x00435B15
			private void OnValueChangedAperture(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.depthAperture = _value;
				this.depthOfField.aperture = _value;
				this.icAperture.value = _value;
			}

			// Token: 0x0600A59D RID: 42397 RVA: 0x0043774C File Offset: 0x00435B4C
			private void OnEndEditAperture(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icAperture.Min, this.icAperture.Max);
				Singleton<Studio>.Instance.sceneInfo.depthAperture = num;
				this.depthOfField.aperture = num;
				this.icAperture.value = num;
			}

			// Token: 0x0600A59E RID: 42398 RVA: 0x004377B0 File Offset: 0x00435BB0
			private void OnClickApertureDef()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.depthAperture = ScreenEffectDefine.DepthOfFieldAperture;
				this.depthOfField.aperture = ScreenEffectDefine.DepthOfFieldAperture;
				this.icAperture.value = ScreenEffectDefine.DepthOfFieldAperture;
			}

			// Token: 0x0400824F RID: 33359
			public Toggle toggleEnable;

			// Token: 0x04008250 RID: 33360
			public SystemButtonCtrl.Selector selectorForcus;

			// Token: 0x04008251 RID: 33361
			public SystemButtonCtrl.InputCombination icFocalSize;

			// Token: 0x04008252 RID: 33362
			public SystemButtonCtrl.InputCombination icAperture;
		}

		// Token: 0x02001350 RID: 4944
		[Serializable]
		private class VignetteInfo : SystemButtonCtrl.EffectInfo
		{
			// Token: 0x170022AC RID: 8876
			// (get) Token: 0x0600A5A0 RID: 42400 RVA: 0x00437810 File Offset: 0x00435C10
			// (set) Token: 0x0600A5A1 RID: 42401 RVA: 0x00437818 File Offset: 0x00435C18
			private Vignette Vignette { get; set; }

			// Token: 0x170022AD RID: 8877
			// (get) Token: 0x0600A5A2 RID: 42402 RVA: 0x00437821 File Offset: 0x00435C21
			// (set) Token: 0x0600A5A3 RID: 42403 RVA: 0x00437829 File Offset: 0x00435C29
			private FloatParameter fpIntensity { get; set; }

			// Token: 0x170022AE RID: 8878
			// (set) Token: 0x0600A5A4 RID: 42404 RVA: 0x00437832 File Offset: 0x00435C32
			private float Intensity
			{
				set
				{
					this.fpIntensity.value = value;
				}
			}

			// Token: 0x0600A5A5 RID: 42405 RVA: 0x00437840 File Offset: 0x00435C40
			public void Init(Sprite[] _sprite, Vignette _vignette)
			{
				base.Init(_sprite);
				this.Vignette = _vignette;
				this.fpIntensity = this.Vignette.intensity;
				this.toggleEnable.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedEnable));
				this.icIntensity.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedIntensity));
				InputField input = this.icIntensity.input;
				input.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input.onValidateInput, new InputField.OnValidateInput(this.icIntensity.OnValidateInput));
				this.icIntensity.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditIntensity));
				this.icIntensity.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickIntensity));
			}

			// Token: 0x0600A5A6 RID: 42406 RVA: 0x0043791C File Offset: 0x00435D1C
			public override void UpdateInfo()
			{
				base.UpdateInfo();
				base.isUpdateInfo = true;
				this.toggleEnable.isOn = Singleton<Studio>.Instance.sceneInfo.enableVignette;
				this.icIntensity.value = Singleton<Studio>.Instance.sceneInfo.vignetteIntensity;
				this.Apply();
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A5A7 RID: 42407 RVA: 0x00437978 File Offset: 0x00435D78
			public override void Apply()
			{
				if (this.Vignette == null)
				{
					return;
				}
				this.Vignette.active = Singleton<Studio>.Instance.sceneInfo.enableVignette;
				this.Intensity = Singleton<Studio>.Instance.sceneInfo.vignetteIntensity;
			}

			// Token: 0x0600A5A8 RID: 42408 RVA: 0x004379C6 File Offset: 0x00435DC6
			private void OnValueChangedEnable(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.enableVignette = _value;
				this.Vignette.active = _value;
			}

			// Token: 0x0600A5A9 RID: 42409 RVA: 0x004379F0 File Offset: 0x00435DF0
			private void OnValueChangedIntensity(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.vignetteIntensity = _value;
				this.Intensity = _value;
				this.icIntensity.value = _value;
			}

			// Token: 0x0600A5AA RID: 42410 RVA: 0x00437A24 File Offset: 0x00435E24
			private void OnEndEditIntensity(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icIntensity.Min, this.icIntensity.Max);
				Singleton<Studio>.Instance.sceneInfo.vignetteIntensity = num;
				this.Intensity = num;
				this.icIntensity.value = num;
			}

			// Token: 0x0600A5AB RID: 42411 RVA: 0x00437A82 File Offset: 0x00435E82
			private void OnClickIntensity()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.vignetteIntensity = ScreenEffectDefine.VignetteIntensity;
				this.Intensity = ScreenEffectDefine.VignetteIntensity;
				this.icIntensity.value = ScreenEffectDefine.VignetteIntensity;
			}

			// Token: 0x04008254 RID: 33364
			public Toggle toggleEnable;

			// Token: 0x04008255 RID: 33365
			public SystemButtonCtrl.InputCombination icIntensity = new SystemButtonCtrl.InputCombination();
		}

		// Token: 0x02001351 RID: 4945
		[Serializable]
		private class ScreenSpaceReflectionInfo : SystemButtonCtrl.EffectInfo
		{
			// Token: 0x170022AF RID: 8879
			// (get) Token: 0x0600A5AD RID: 42413 RVA: 0x00437AC7 File Offset: 0x00435EC7
			// (set) Token: 0x0600A5AE RID: 42414 RVA: 0x00437ACF File Offset: 0x00435ECF
			private ScreenSpaceReflections ScreenSpaceReflections { get; set; }

			// Token: 0x0600A5AF RID: 42415 RVA: 0x00437AD8 File Offset: 0x00435ED8
			public void Init(Sprite[] _sprite, ScreenSpaceReflections _screenSpaceReflections)
			{
				base.Init(_sprite);
				this.ScreenSpaceReflections = _screenSpaceReflections;
				this.toggleEnable.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedEnable));
			}

			// Token: 0x0600A5B0 RID: 42416 RVA: 0x00437B04 File Offset: 0x00435F04
			public override void UpdateInfo()
			{
				base.UpdateInfo();
				base.isUpdateInfo = true;
				this.toggleEnable.isOn = Singleton<Studio>.Instance.sceneInfo.enableSSR;
				this.Apply();
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A5B1 RID: 42417 RVA: 0x00437B3A File Offset: 0x00435F3A
			public override void Apply()
			{
				if (this.ScreenSpaceReflections == null)
				{
					return;
				}
				this.ScreenSpaceReflections.active = Singleton<Studio>.Instance.sceneInfo.enableSSR;
			}

			// Token: 0x0600A5B2 RID: 42418 RVA: 0x00437B68 File Offset: 0x00435F68
			private void OnValueChangedEnable(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.enableSSR = _value;
				this.ScreenSpaceReflections.active = _value;
			}

			// Token: 0x04008258 RID: 33368
			public Toggle toggleEnable;
		}

		// Token: 0x02001352 RID: 4946
		[Serializable]
		private class ReflectionProbeInfo : SystemButtonCtrl.EffectInfo
		{
			// Token: 0x170022B0 RID: 8880
			// (get) Token: 0x0600A5B4 RID: 42420 RVA: 0x00437B9A File Offset: 0x00435F9A
			// (set) Token: 0x0600A5B5 RID: 42421 RVA: 0x00437BA2 File Offset: 0x00435FA2
			private ReflectionProbe ReflectionProbe { get; set; }

			// Token: 0x170022B1 RID: 8881
			// (get) Token: 0x0600A5B6 RID: 42422 RVA: 0x00437BAB File Offset: 0x00435FAB
			// (set) Token: 0x0600A5B7 RID: 42423 RVA: 0x00437BB3 File Offset: 0x00435FB3
			private GameObject GameObject { get; set; }

			// Token: 0x0600A5B8 RID: 42424 RVA: 0x00437BBC File Offset: 0x00435FBC
			public void Init(Sprite[] _sprite, ReflectionProbe _reflectionProbe, GameObject _gameObject)
			{
				base.Init(_sprite);
				this.ReflectionProbe = _reflectionProbe;
				this.GameObject = _gameObject;
				this.dropdownCubemap.options = (from v in Singleton<Info>.Instance.dicReflectionProbeLoadInfo
				select new Dropdown.OptionData(v.Value.name)).ToList<Dropdown.OptionData>();
				this.toggleEnable.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedEnable));
				this.dropdownCubemap.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChangedCubemap));
				this.icIntensity.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedIntensity));
				InputField input = this.icIntensity.input;
				input.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input.onValidateInput, new InputField.OnValidateInput(this.icIntensity.OnValidateInput));
				this.icIntensity.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditIntensity));
				this.icIntensity.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickIntensity));
			}

			// Token: 0x0600A5B9 RID: 42425 RVA: 0x00437CE8 File Offset: 0x004360E8
			public override void UpdateInfo()
			{
				base.UpdateInfo();
				base.isUpdateInfo = true;
				this.toggleEnable.isOn = Singleton<Studio>.Instance.sceneInfo.enableReflectionProbe;
				this.dropdownCubemap.value = Singleton<Studio>.Instance.sceneInfo.reflectionProbeCubemap;
				this.icIntensity.value = Singleton<Studio>.Instance.sceneInfo.reflectionProbeIntensity;
				this.Apply();
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A5BA RID: 42426 RVA: 0x00437D60 File Offset: 0x00436160
			public override void Apply()
			{
				GameObject gameObject = this.GameObject;
				if (gameObject != null)
				{
					gameObject.SetActiveIfDifferent(Singleton<Studio>.Instance.sceneInfo.enableReflectionProbe);
				}
				if (this.ReflectionProbe != null)
				{
					this.SetCubemap(Singleton<Studio>.Instance.sceneInfo.reflectionProbeCubemap);
					this.ReflectionProbe.intensity = Singleton<Studio>.Instance.sceneInfo.reflectionProbeIntensity;
				}
			}

			// Token: 0x0600A5BB RID: 42427 RVA: 0x00437DD4 File Offset: 0x004361D4
			public void SetCubemap(int _no)
			{
				Singleton<Studio>.Instance.sceneInfo.reflectionProbeCubemap = _no;
				Info.LoadCommonInfo loadCommonInfo = null;
				if (!Singleton<Info>.Instance.dicReflectionProbeLoadInfo.TryGetValue(_no, out loadCommonInfo))
				{
					return;
				}
				Texture customBakedTexture = CommonLib.LoadAsset<Texture>(loadCommonInfo.bundlePath, loadCommonInfo.fileName, false, string.Empty);
				this.ReflectionProbe.customBakedTexture = customBakedTexture;
			}

			// Token: 0x0600A5BC RID: 42428 RVA: 0x00437E2F File Offset: 0x0043622F
			private void OnValueChangedEnable(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.enableReflectionProbe = _value;
				GameObject gameObject = this.GameObject;
				if (gameObject != null)
				{
					gameObject.SetActiveIfDifferent(_value);
				}
			}

			// Token: 0x0600A5BD RID: 42429 RVA: 0x00437E63 File Offset: 0x00436263
			private void OnValueChangedCubemap(int _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				this.SetCubemap(_value);
			}

			// Token: 0x0600A5BE RID: 42430 RVA: 0x00437E78 File Offset: 0x00436278
			private void OnValueChangedIntensity(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.reflectionProbeIntensity = _value;
				this.ReflectionProbe.intensity = _value;
				this.icIntensity.value = _value;
			}

			// Token: 0x0600A5BF RID: 42431 RVA: 0x00437EB0 File Offset: 0x004362B0
			private void OnEndEditIntensity(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icIntensity.Min, this.icIntensity.Max);
				Singleton<Studio>.Instance.sceneInfo.reflectionProbeIntensity = num;
				this.ReflectionProbe.intensity = num;
				this.icIntensity.value = num;
			}

			// Token: 0x0600A5C0 RID: 42432 RVA: 0x00437F14 File Offset: 0x00436314
			private void OnClickIntensity()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.reflectionProbeIntensity = ScreenEffectDefine.ReflectionProbeIntensity;
				this.ReflectionProbe.intensity = ScreenEffectDefine.ReflectionProbeIntensity;
				this.icIntensity.value = ScreenEffectDefine.ReflectionProbeIntensity;
			}

			// Token: 0x0400825A RID: 33370
			public Toggle toggleEnable;

			// Token: 0x0400825B RID: 33371
			public Dropdown dropdownCubemap;

			// Token: 0x0400825C RID: 33372
			public SystemButtonCtrl.InputCombination icIntensity;
		}

		// Token: 0x02001353 RID: 4947
		[Serializable]
		private class FogInfo : SystemButtonCtrl.EffectInfo
		{
			// Token: 0x170022B2 RID: 8882
			// (get) Token: 0x0600A5C3 RID: 42435 RVA: 0x00437F7C File Offset: 0x0043637C
			// (set) Token: 0x0600A5C4 RID: 42436 RVA: 0x00437F84 File Offset: 0x00436384
			private GlobalFog GlobalFog { get; set; }

			// Token: 0x0600A5C5 RID: 42437 RVA: 0x00437F90 File Offset: 0x00436390
			public void Init(Sprite[] _sprite, GlobalFog _fog)
			{
				base.Init(_sprite);
				this.GlobalFog = _fog;
				this.toggleEnable.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedEnable));
				this.toggleExcludeFarPixels.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedExcludeFarPixels));
				this.icHeight.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedHeight));
				this.icHeight.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditHeight));
				this.icHeight.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickHeight));
				this.icHeightDensity.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedHeightDensity));
				InputField input = this.icHeightDensity.input;
				input.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input.onValidateInput, new InputField.OnValidateInput(this.icHeightDensity.OnValidateInput));
				this.icHeightDensity.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditHeightDensity));
				this.icHeightDensity.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickHeightDensity));
				this.buttonColor.onClick.AddListener(new UnityAction(this.OnClickColor));
				this.icDensity.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedDensity));
				InputField input2 = this.icDensity.input;
				input2.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input2.onValidateInput, new InputField.OnValidateInput(this.icDensity.OnValidateInput));
				this.icDensity.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditDensity));
				this.icDensity.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickDensity));
			}

			// Token: 0x0600A5C6 RID: 42438 RVA: 0x0043818C File Offset: 0x0043658C
			public override void UpdateInfo()
			{
				base.UpdateInfo();
				base.isUpdateInfo = true;
				this.toggleEnable.isOn = Singleton<Studio>.Instance.sceneInfo.enableFog;
				this.toggleExcludeFarPixels.isOn = Singleton<Studio>.Instance.sceneInfo.fogExcludeFarPixels;
				this.icHeight.value = Singleton<Studio>.Instance.sceneInfo.fogHeight;
				this.icHeightDensity.value = Singleton<Studio>.Instance.sceneInfo.fogHeightDensity;
				this.buttonColor.image.color = Singleton<Studio>.Instance.sceneInfo.fogColor;
				this.icDensity.value = Singleton<Studio>.Instance.sceneInfo.fogDensity;
				this.Apply();
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A5C7 RID: 42439 RVA: 0x00438254 File Offset: 0x00436654
			public override void Apply()
			{
				if (this.GlobalFog != null)
				{
					this.GlobalFog.enabled = Singleton<Studio>.Instance.sceneInfo.enableFog;
					this.GlobalFog.excludeFarPixels = Singleton<Studio>.Instance.sceneInfo.fogExcludeFarPixels;
					this.GlobalFog.height = Singleton<Studio>.Instance.sceneInfo.fogHeight;
					this.GlobalFog.heightDensity = Singleton<Studio>.Instance.sceneInfo.fogHeightDensity;
				}
				RenderSettings.fog = Singleton<Studio>.Instance.sceneInfo.enableFog;
				RenderSettings.fogColor = Singleton<Studio>.Instance.sceneInfo.fogColor;
				RenderSettings.fogDensity = Singleton<Studio>.Instance.sceneInfo.fogDensity;
			}

			// Token: 0x0600A5C8 RID: 42440 RVA: 0x00438316 File Offset: 0x00436716
			public void SetEnable(bool _value, bool _UI = true)
			{
				Singleton<Studio>.Instance.sceneInfo.enableFog = _value;
				this.GlobalFog.enabled = _value;
				RenderSettings.fog = _value;
				if (_UI)
				{
					this.toggleEnable.isOn = _value;
				}
			}

			// Token: 0x0600A5C9 RID: 42441 RVA: 0x0043834C File Offset: 0x0043674C
			public void SetColor(Color _color)
			{
				Singleton<Studio>.Instance.sceneInfo.fogColor = _color;
				RenderSettings.fogColor = _color;
				this.buttonColor.image.color = _color;
			}

			// Token: 0x0600A5CA RID: 42442 RVA: 0x00438375 File Offset: 0x00436775
			private void OnValueChangedEnable(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				this.SetEnable(_value, false);
			}

			// Token: 0x0600A5CB RID: 42443 RVA: 0x0043838B File Offset: 0x0043678B
			private void OnValueChangedExcludeFarPixels(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.fogExcludeFarPixels = _value;
				this.GlobalFog.excludeFarPixels = _value;
			}

			// Token: 0x0600A5CC RID: 42444 RVA: 0x004383B8 File Offset: 0x004367B8
			private void OnClickColor()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				if (Singleton<Studio>.Instance.colorPalette.Check("フォグ"))
				{
					Singleton<Studio>.Instance.colorPalette.visible = false;
					return;
				}
				Singleton<Studio>.Instance.colorPalette.Setup("フォグ", Singleton<Studio>.Instance.sceneInfo.fogColor, new Action<Color>(this.SetColor), false);
			}

			// Token: 0x0600A5CD RID: 42445 RVA: 0x0043842B File Offset: 0x0043682B
			private void OnValueChangedHeight(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.fogHeight = _value;
				this.GlobalFog.height = _value;
				this.icHeight.value = _value;
			}

			// Token: 0x0600A5CE RID: 42446 RVA: 0x00438464 File Offset: 0x00436864
			private void OnEndEditHeight(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icHeight.Min, this.icHeight.Max);
				Singleton<Studio>.Instance.sceneInfo.fogHeight = num;
				this.GlobalFog.height = num;
				this.icHeight.value = num;
			}

			// Token: 0x0600A5CF RID: 42447 RVA: 0x004384C8 File Offset: 0x004368C8
			private void OnClickHeight()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.fogHeight = ScreenEffectDefine.FogHeight;
				this.GlobalFog.height = ScreenEffectDefine.FogHeight;
				this.icHeight.value = ScreenEffectDefine.FogHeight;
			}

			// Token: 0x0600A5D0 RID: 42448 RVA: 0x00438515 File Offset: 0x00436915
			private void OnValueChangedHeightDensity(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.fogHeightDensity = _value;
				this.GlobalFog.heightDensity = _value;
				this.icHeightDensity.value = _value;
			}

			// Token: 0x0600A5D1 RID: 42449 RVA: 0x0043854C File Offset: 0x0043694C
			private void OnEndEditHeightDensity(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icHeightDensity.Min, this.icHeightDensity.Max);
				Singleton<Studio>.Instance.sceneInfo.fogHeightDensity = num;
				this.GlobalFog.heightDensity = num;
				this.icHeightDensity.value = num;
			}

			// Token: 0x0600A5D2 RID: 42450 RVA: 0x004385B0 File Offset: 0x004369B0
			private void OnClickHeightDensity()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.fogHeightDensity = ScreenEffectDefine.FogHeightDensity;
				this.GlobalFog.heightDensity = ScreenEffectDefine.FogHeightDensity;
				this.icHeightDensity.value = ScreenEffectDefine.FogHeightDensity;
			}

			// Token: 0x0600A5D3 RID: 42451 RVA: 0x004385FD File Offset: 0x004369FD
			private void OnValueChangedDensity(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.fogDensity = _value;
				RenderSettings.fogDensity = _value;
				this.icDensity.value = _value;
			}

			// Token: 0x0600A5D4 RID: 42452 RVA: 0x00438630 File Offset: 0x00436A30
			private void OnEndEditDensity(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icDensity.Min, this.icDensity.Max);
				Singleton<Studio>.Instance.sceneInfo.fogDensity = num;
				RenderSettings.fogDensity = num;
				this.icDensity.value = num;
			}

			// Token: 0x0600A5D5 RID: 42453 RVA: 0x0043868D File Offset: 0x00436A8D
			private void OnClickDensity()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.fogDensity = ScreenEffectDefine.FogDensity;
				RenderSettings.fogDensity = ScreenEffectDefine.FogDensity;
				this.icDensity.value = ScreenEffectDefine.FogDensity;
			}

			// Token: 0x04008260 RID: 33376
			public Toggle toggleEnable;

			// Token: 0x04008261 RID: 33377
			public Toggle toggleExcludeFarPixels;

			// Token: 0x04008262 RID: 33378
			public SystemButtonCtrl.InputCombination icHeight;

			// Token: 0x04008263 RID: 33379
			public SystemButtonCtrl.InputCombination icHeightDensity;

			// Token: 0x04008264 RID: 33380
			public Button buttonColor;

			// Token: 0x04008265 RID: 33381
			public SystemButtonCtrl.InputCombination icDensity;
		}

		// Token: 0x02001354 RID: 4948
		[Serializable]
		private class SunShaftsInfo : SystemButtonCtrl.EffectInfo
		{
			// Token: 0x170022B3 RID: 8883
			// (get) Token: 0x0600A5D7 RID: 42455 RVA: 0x004386D1 File Offset: 0x00436AD1
			// (set) Token: 0x0600A5D8 RID: 42456 RVA: 0x004386D9 File Offset: 0x00436AD9
			private SunShafts sunShafts { get; set; }

			// Token: 0x0600A5D9 RID: 42457 RVA: 0x004386E4 File Offset: 0x00436AE4
			public void Init(Sprite[] _sprite, SunShafts _sunShafts)
			{
				base.Init(_sprite);
				this.sunShafts = _sunShafts;
				this.toggleEnable.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedEnable));
				this.selectorCaster._button.onClick.AddListener(new UnityAction(this.OnClickCaster));
				this.buttonThresholdColor.onClick.AddListener(new UnityAction(this.OnClickThresholdColor));
				this.buttonShaftsColor.onClick.AddListener(new UnityAction(this.OnClickShaftsColor));
				this.icDistanceFallOff.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedDistanceFallOff));
				InputField input = this.icDistanceFallOff.input;
				input.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(input.onValidateInput, new InputField.OnValidateInput(this.icDistanceFallOff.OnValidateInput));
				this.icDistanceFallOff.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditDistanceFallOff));
				this.icDistanceFallOff.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickDistanceFallOff));
				this.icBlurSize.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedBlurSize));
				this.icBlurSize.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditBlurSize));
				this.icBlurSize.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickBlurSize));
				this.icIntensity.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedIntensity));
				this.icIntensity.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditIntensity));
				this.icIntensity.buttonDefault.onClick.AddListener(new UnityAction(this.OnClickIntensity));
			}

			// Token: 0x0600A5DA RID: 42458 RVA: 0x004388D0 File Offset: 0x00436CD0
			public override void UpdateInfo()
			{
				base.UpdateInfo();
				base.isUpdateInfo = true;
				this.toggleEnable.isOn = Singleton<Studio>.Instance.sceneInfo.enableSunShafts;
				this.buttonThresholdColor.image.color = Singleton<Studio>.Instance.sceneInfo.sunThresholdColor;
				this.buttonShaftsColor.image.color = Singleton<Studio>.Instance.sceneInfo.sunColor;
				Singleton<Studio>.Instance.SetSunCaster(Singleton<Studio>.Instance.sceneInfo.sunCaster);
				this.icDistanceFallOff.value = Singleton<Studio>.Instance.sceneInfo.sunDistanceFallOff;
				this.icBlurSize.value = Singleton<Studio>.Instance.sceneInfo.sunBlurSize;
				this.icIntensity.value = Singleton<Studio>.Instance.sceneInfo.sunIntensity;
				this.Apply();
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A5DB RID: 42459 RVA: 0x004389B8 File Offset: 0x00436DB8
			public override void Apply()
			{
				if (this.sunShafts == null)
				{
					return;
				}
				this.sunShafts.enabled = Singleton<Studio>.Instance.sceneInfo.enableSunShafts;
				this.sunShafts.sunThreshold = Singleton<Studio>.Instance.sceneInfo.sunThresholdColor;
				this.sunShafts.sunColor = Singleton<Studio>.Instance.sceneInfo.sunColor;
				this.sunShafts.maxRadius = Singleton<Studio>.Instance.sceneInfo.sunDistanceFallOff;
				this.sunShafts.sunShaftBlurRadius = Singleton<Studio>.Instance.sceneInfo.sunBlurSize;
				this.sunShafts.sunShaftIntensity = Singleton<Studio>.Instance.sceneInfo.sunIntensity;
			}

			// Token: 0x0600A5DC RID: 42460 RVA: 0x00438A73 File Offset: 0x00436E73
			public void SetShaftsColor(Color _color)
			{
				Singleton<Studio>.Instance.sceneInfo.sunColor = _color;
				this.buttonShaftsColor.image.color = _color;
				this.sunShafts.sunColor = _color;
			}

			// Token: 0x0600A5DD RID: 42461 RVA: 0x00438AA2 File Offset: 0x00436EA2
			private void OnValueChangedEnable(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.enableSunShafts = _value;
				this.sunShafts.enabled = _value;
			}

			// Token: 0x0600A5DE RID: 42462 RVA: 0x00438ACC File Offset: 0x00436ECC
			private void OnClickThresholdColor()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				if (Singleton<Studio>.Instance.colorPalette.Check("サンシャフト しきい色"))
				{
					Singleton<Studio>.Instance.colorPalette.visible = false;
					return;
				}
				Singleton<Studio>.Instance.colorPalette.Setup("サンシャフト しきい色", Singleton<Studio>.Instance.sceneInfo.sunThresholdColor, delegate(Color _c)
				{
					Singleton<Studio>.Instance.sceneInfo.sunThresholdColor = _c;
					this.buttonThresholdColor.image.color = _c;
					this.sunShafts.sunThreshold = _c;
				}, false);
			}

			// Token: 0x0600A5DF RID: 42463 RVA: 0x00438B40 File Offset: 0x00436F40
			private void OnClickShaftsColor()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				if (Singleton<Studio>.Instance.colorPalette.Check("サンシャフト 光の色"))
				{
					Singleton<Studio>.Instance.colorPalette.visible = false;
					return;
				}
				Singleton<Studio>.Instance.colorPalette.Setup("サンシャフト 光の色", Singleton<Studio>.Instance.sceneInfo.sunColor, new Action<Color>(this.SetShaftsColor), false);
			}

			// Token: 0x0600A5E0 RID: 42464 RVA: 0x00438BB4 File Offset: 0x00436FB4
			private void OnClickCaster()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				GuideObject selectObject = Singleton<GuideObjectManager>.Instance.selectObject;
				Singleton<Studio>.Instance.SetSunCaster((!(selectObject != null)) ? -1 : selectObject.dicKey);
			}

			// Token: 0x0600A5E1 RID: 42465 RVA: 0x00438BFA File Offset: 0x00436FFA
			private void OnValueChangedDistanceFallOff(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.sunDistanceFallOff = _value;
				this.sunShafts.maxRadius = _value;
				this.icDistanceFallOff.value = _value;
			}

			// Token: 0x0600A5E2 RID: 42466 RVA: 0x00438C30 File Offset: 0x00437030
			private void OnEndEditDistanceFallOff(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icDistanceFallOff.Min, this.icDistanceFallOff.Max);
				Singleton<Studio>.Instance.sceneInfo.sunDistanceFallOff = num;
				this.sunShafts.maxRadius = num;
				this.icDistanceFallOff.value = num;
			}

			// Token: 0x0600A5E3 RID: 42467 RVA: 0x00438C94 File Offset: 0x00437094
			private void OnClickDistanceFallOff()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.sunDistanceFallOff = ScreenEffectDefine.SunShaftDistanceFallOff;
				this.sunShafts.maxRadius = ScreenEffectDefine.SunShaftDistanceFallOff;
				this.icDistanceFallOff.value = ScreenEffectDefine.SunShaftDistanceFallOff;
			}

			// Token: 0x0600A5E4 RID: 42468 RVA: 0x00438CE1 File Offset: 0x004370E1
			private void OnValueChangedBlurSize(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.sunBlurSize = _value;
				this.sunShafts.sunShaftBlurRadius = _value;
				this.icBlurSize.value = _value;
			}

			// Token: 0x0600A5E5 RID: 42469 RVA: 0x00438D18 File Offset: 0x00437118
			private void OnEndEditBlurSize(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icBlurSize.Min, this.icBlurSize.Max);
				Singleton<Studio>.Instance.sceneInfo.sunBlurSize = num;
				this.sunShafts.sunShaftBlurRadius = num;
				this.icBlurSize.value = num;
			}

			// Token: 0x0600A5E6 RID: 42470 RVA: 0x00438D7C File Offset: 0x0043717C
			private void OnClickBlurSize()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.sunBlurSize = ScreenEffectDefine.SunShaftBlurSize;
				this.sunShafts.sunShaftBlurRadius = ScreenEffectDefine.SunShaftBlurSize;
				this.icBlurSize.value = ScreenEffectDefine.SunShaftBlurSize;
			}

			// Token: 0x0600A5E7 RID: 42471 RVA: 0x00438DC9 File Offset: 0x004371C9
			private void OnValueChangedIntensity(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.sunIntensity = _value;
				this.sunShafts.sunShaftIntensity = _value;
				this.icIntensity.value = _value;
			}

			// Token: 0x0600A5E8 RID: 42472 RVA: 0x00438E00 File Offset: 0x00437200
			private void OnEndEditIntensity(string _text)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.icIntensity.Min, this.icIntensity.Max);
				Singleton<Studio>.Instance.sceneInfo.sunIntensity = num;
				this.sunShafts.sunShaftIntensity = num;
				this.icIntensity.value = num;
			}

			// Token: 0x0600A5E9 RID: 42473 RVA: 0x00438E64 File Offset: 0x00437264
			private void OnClickIntensity()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.sunIntensity = ScreenEffectDefine.SunShaftIntensity;
				this.sunShafts.sunShaftIntensity = ScreenEffectDefine.SunShaftIntensity;
				this.icIntensity.value = ScreenEffectDefine.SunShaftIntensity;
			}

			// Token: 0x04008267 RID: 33383
			public Toggle toggleEnable;

			// Token: 0x04008268 RID: 33384
			public SystemButtonCtrl.Selector selectorCaster;

			// Token: 0x04008269 RID: 33385
			public Button buttonThresholdColor;

			// Token: 0x0400826A RID: 33386
			public Button buttonShaftsColor;

			// Token: 0x0400826B RID: 33387
			public SystemButtonCtrl.InputCombination icDistanceFallOff;

			// Token: 0x0400826C RID: 33388
			public SystemButtonCtrl.InputCombination icBlurSize;

			// Token: 0x0400826D RID: 33389
			public SystemButtonCtrl.InputCombination icIntensity;
		}

		// Token: 0x02001355 RID: 4949
		[Serializable]
		private class SelfShadowInfo : SystemButtonCtrl.EffectInfo
		{
			// Token: 0x0600A5EC RID: 42476 RVA: 0x00438EE8 File Offset: 0x004372E8
			public override void Init(Sprite[] _sprite)
			{
				base.Init(_sprite);
				this.toggleEnable.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedEnable));
			}

			// Token: 0x0600A5ED RID: 42477 RVA: 0x00438F0D File Offset: 0x0043730D
			public override void UpdateInfo()
			{
				base.UpdateInfo();
				base.isUpdateInfo = true;
				this.toggleEnable.isOn = Singleton<Studio>.Instance.sceneInfo.enableShadow;
				this.Apply();
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A5EE RID: 42478 RVA: 0x00438F43 File Offset: 0x00437343
			public override void Apply()
			{
				this.Set(Singleton<Studio>.Instance.sceneInfo.enableShadow);
			}

			// Token: 0x0600A5EF RID: 42479 RVA: 0x00438F5A File Offset: 0x0043735A
			private void OnValueChangedEnable(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.enableShadow = _value;
				this.Set(_value);
			}

			// Token: 0x0600A5F0 RID: 42480 RVA: 0x00438F80 File Offset: 0x00437380
			private void Set(bool _value)
			{
				int qualityLevel = QualitySettings.GetQualityLevel() / 2 * 2 + ((!_value) ? 1 : 0);
				QualitySettings.SetQualityLevel(qualityLevel);
			}

			// Token: 0x0400826F RID: 33391
			public Toggle toggleEnable;
		}

		// Token: 0x02001356 RID: 4950
		[Serializable]
		private class EnvironmentLightingInfo : SystemButtonCtrl.EffectInfo
		{
			// Token: 0x0600A5F2 RID: 42482 RVA: 0x00438FB4 File Offset: 0x004373B4
			public override void Init(Sprite[] _sprite)
			{
				base.Init(_sprite);
				this.buttonSkyColor.onClick.AddListener(new UnityAction(this.OnClickSkyColor));
				this.buttonEquator.onClick.AddListener(new UnityAction(this.OnClickEquator));
				this.buttonGround.onClick.AddListener(new UnityAction(this.OnClickGround));
			}

			// Token: 0x0600A5F3 RID: 42483 RVA: 0x0043901C File Offset: 0x0043741C
			public override void UpdateInfo()
			{
				base.UpdateInfo();
				base.isUpdateInfo = true;
				this.buttonSkyColor.image.color = Singleton<Studio>.Instance.sceneInfo.environmentLightingSkyColor;
				this.buttonEquator.image.color = Singleton<Studio>.Instance.sceneInfo.environmentLightingEquatorColor;
				this.buttonGround.image.color = Singleton<Studio>.Instance.sceneInfo.environmentLightingGroundColor;
				this.Apply();
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A5F4 RID: 42484 RVA: 0x004390A0 File Offset: 0x004374A0
			public override void Apply()
			{
				RenderSettings.ambientSkyColor = Singleton<Studio>.Instance.sceneInfo.environmentLightingSkyColor;
				RenderSettings.ambientEquatorColor = Singleton<Studio>.Instance.sceneInfo.environmentLightingEquatorColor;
				RenderSettings.ambientGroundColor = Singleton<Studio>.Instance.sceneInfo.environmentLightingGroundColor;
			}

			// Token: 0x0600A5F5 RID: 42485 RVA: 0x004390E0 File Offset: 0x004374E0
			private void OnClickSkyColor()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				if (Singleton<Studio>.Instance.colorPalette.Check("空の環境光"))
				{
					Singleton<Studio>.Instance.colorPalette.visible = false;
					return;
				}
				Singleton<Studio>.Instance.colorPalette.Setup("空の環境光", Singleton<Studio>.Instance.sceneInfo.environmentLightingSkyColor, delegate(Color _c)
				{
					Singleton<Studio>.Instance.sceneInfo.environmentLightingSkyColor = _c;
					RenderSettings.ambientSkyColor = _c;
					this.buttonSkyColor.image.color = _c;
				}, false);
			}

			// Token: 0x0600A5F6 RID: 42486 RVA: 0x00439154 File Offset: 0x00437554
			private void OnClickEquator()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				if (Singleton<Studio>.Instance.colorPalette.Check("地平線の環境光"))
				{
					Singleton<Studio>.Instance.colorPalette.visible = false;
					return;
				}
				Singleton<Studio>.Instance.colorPalette.Setup("地平線の環境光", Singleton<Studio>.Instance.sceneInfo.environmentLightingEquatorColor, delegate(Color _c)
				{
					Singleton<Studio>.Instance.sceneInfo.environmentLightingEquatorColor = _c;
					RenderSettings.ambientEquatorColor = _c;
					this.buttonEquator.image.color = _c;
				}, false);
			}

			// Token: 0x0600A5F7 RID: 42487 RVA: 0x004391C8 File Offset: 0x004375C8
			private void OnClickGround()
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				if (Singleton<Studio>.Instance.colorPalette.Check("地面の環境光"))
				{
					Singleton<Studio>.Instance.colorPalette.visible = false;
					return;
				}
				Singleton<Studio>.Instance.colorPalette.Setup("地面の環境光", Singleton<Studio>.Instance.sceneInfo.environmentLightingGroundColor, delegate(Color _c)
				{
					Singleton<Studio>.Instance.sceneInfo.environmentLightingGroundColor = _c;
					RenderSettings.ambientGroundColor = _c;
					this.buttonGround.image.color = _c;
				}, false);
			}

			// Token: 0x04008270 RID: 33392
			public Button buttonSkyColor;

			// Token: 0x04008271 RID: 33393
			public Button buttonEquator;

			// Token: 0x04008272 RID: 33394
			public Button buttonGround;
		}
	}
}
