using System;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200132E RID: 4910
	public class OptionCtrl : MonoBehaviour
	{
		// Token: 0x17002275 RID: 8821
		// (get) Token: 0x0600A454 RID: 42068 RVA: 0x0043181E File Offset: 0x0042FC1E
		public OptionCtrl.InputCombination inputSize
		{
			get
			{
				return this._inputSize;
			}
		}

		// Token: 0x17002276 RID: 8822
		// (get) Token: 0x0600A455 RID: 42069 RVA: 0x00431826 File Offset: 0x0042FC26
		// (set) Token: 0x0600A456 RID: 42070 RVA: 0x0043182E File Offset: 0x0042FC2E
		public bool IsInit { get; private set; }

		// Token: 0x0600A457 RID: 42071 RVA: 0x00431838 File Offset: 0x0042FC38
		public void UpdateUI()
		{
			this.inputCameraX.value = Studio.optionSystem.cameraSpeedY;
			this.inputCameraY.value = Studio.optionSystem.cameraSpeedX;
			this.inputCameraSpeed.value = Studio.optionSystem.cameraSpeed;
			this._inputSize.value = Studio.optionSystem.manipulateSize;
			this.inputSpeed.value = Studio.optionSystem.manipuleteSpeed;
			this.toggleInitialPosition[0].isOn = (Studio.optionSystem.initialPosition == 0);
			this.toggleInitialPosition[1].isOn = (Studio.optionSystem.initialPosition == 1);
			this.toggleSelectedState[0].isOn = (Studio.optionSystem.selectedState == 0);
			this.toggleSelectedState[1].isOn = (Studio.optionSystem.selectedState == 1);
			this.toggleAutoHide[0].isOn = !Studio.optionSystem.autoHide;
			this.toggleAutoHide[1].isOn = Studio.optionSystem.autoHide;
			this.toggleAutoSelect[0].isOn = Studio.optionSystem.autoSelect;
			this.toggleAutoSelect[1].isOn = !Studio.optionSystem.autoSelect;
			for (int i = 0; i < this.toggleSnap.Length; i++)
			{
				this.toggleSnap[i].isOn = (Studio.optionSystem.snap == i);
			}
			this.charaFKColor.UpdateInfo();
			this.itemFKColor.UpdateInfo();
			this.routeSystem.UpdateInfo();
			this.etc.UpdateInfo();
		}

		// Token: 0x0600A458 RID: 42072 RVA: 0x004319D7 File Offset: 0x0042FDD7
		public void UpdateUIManipulateSize()
		{
			this._inputSize.value = Studio.optionSystem.manipulateSize;
		}

		// Token: 0x0600A459 RID: 42073 RVA: 0x004319EE File Offset: 0x0042FDEE
		private void OnValueChangedSelectedState(int _state)
		{
			Studio.optionSystem.selectedState = _state;
			this.cameraControl.ReflectOption();
		}

		// Token: 0x0600A45A RID: 42074 RVA: 0x00431A06 File Offset: 0x0042FE06
		private void OnValueChangedCameraX(float _value)
		{
			Studio.optionSystem.cameraSpeedY = _value;
			this.inputCameraX.value = _value;
			this.cameraControl.ReflectOption();
		}

		// Token: 0x0600A45B RID: 42075 RVA: 0x00431A2C File Offset: 0x0042FE2C
		private void OnEndEditCameraX(string _text)
		{
			float num = Mathf.Clamp(Utility.StringToFloat(_text), this.inputCameraX.min, this.inputCameraX.max);
			Studio.optionSystem.cameraSpeedY = num;
			this.inputCameraX.value = num;
			this.cameraControl.ReflectOption();
		}

		// Token: 0x0600A45C RID: 42076 RVA: 0x00431A7D File Offset: 0x0042FE7D
		private void OnValueChangedCameraY(float _value)
		{
			Studio.optionSystem.cameraSpeedX = _value;
			this.inputCameraY.value = _value;
			this.cameraControl.ReflectOption();
		}

		// Token: 0x0600A45D RID: 42077 RVA: 0x00431AA4 File Offset: 0x0042FEA4
		private void OnEndEditCameraY(string _text)
		{
			float num = Mathf.Clamp(Utility.StringToFloat(_text), this.inputCameraY.min, this.inputCameraY.max);
			Studio.optionSystem.cameraSpeedX = num;
			this.inputCameraY.value = num;
			this.cameraControl.ReflectOption();
		}

		// Token: 0x0600A45E RID: 42078 RVA: 0x00431AF5 File Offset: 0x0042FEF5
		private void OnValueChangedCameraSpeed(float _value)
		{
			Studio.optionSystem.cameraSpeed = _value;
			this.inputCameraSpeed.value = _value;
			this.cameraControl.ReflectOption();
		}

		// Token: 0x0600A45F RID: 42079 RVA: 0x00431B1C File Offset: 0x0042FF1C
		private void OnEndEditCameraSpeed(string _text)
		{
			float num = Mathf.Clamp(Utility.StringToFloat(_text), this.inputCameraSpeed.min, this.inputCameraSpeed.max);
			Studio.optionSystem.cameraSpeed = num;
			this.inputCameraSpeed.value = num;
			this.cameraControl.ReflectOption();
		}

		// Token: 0x0600A460 RID: 42080 RVA: 0x00431B6D File Offset: 0x0042FF6D
		private void OnValueChangedSize(float _value)
		{
			Studio.optionSystem.manipulateSize = _value;
			this._inputSize.value = _value;
			Singleton<GuideObjectManager>.Instance.SetScale();
		}

		// Token: 0x0600A461 RID: 42081 RVA: 0x00431B90 File Offset: 0x0042FF90
		private void OnEndEditSize(string _text)
		{
			float num = Mathf.Clamp(Utility.StringToFloat(_text), this._inputSize.min, this._inputSize.max);
			Studio.optionSystem.manipulateSize = num;
			this._inputSize.value = num;
			Singleton<GuideObjectManager>.Instance.SetScale();
		}

		// Token: 0x0600A462 RID: 42082 RVA: 0x00431BE0 File Offset: 0x0042FFE0
		private void OnValueChangedSpeed(float _value)
		{
			Studio.optionSystem.manipuleteSpeed = _value;
			this.inputSpeed.value = _value;
		}

		// Token: 0x0600A463 RID: 42083 RVA: 0x00431BFC File Offset: 0x0042FFFC
		private void OnEndEditSpeed(string _text)
		{
			float num = Mathf.Clamp(Utility.StringToFloat(_text), this.inputSpeed.min, this.inputSpeed.max);
			Studio.optionSystem.manipuleteSpeed = num;
			this.inputSpeed.value = num;
		}

		// Token: 0x0600A464 RID: 42084 RVA: 0x00431C44 File Offset: 0x00430044
		public void Init()
		{
			if (this.IsInit)
			{
				return;
			}
			this.UpdateUI();
			this.inputCameraX.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedCameraX));
			this.inputCameraX.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditCameraX));
			this.inputCameraY.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedCameraY));
			this.inputCameraY.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditCameraY));
			this.inputCameraSpeed.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedCameraSpeed));
			this.inputCameraSpeed.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditCameraSpeed));
			this._inputSize.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedSize));
			this._inputSize.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSize));
			this.inputSpeed.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedSpeed));
			this.inputSpeed.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSpeed));
			this.toggleInitialPosition[0].onValueChanged.AddListener(delegate(bool v)
			{
				Studio.optionSystem.initialPosition = 0;
			});
			this.toggleInitialPosition[1].onValueChanged.AddListener(delegate(bool v)
			{
				Studio.optionSystem.initialPosition = 1;
			});
			this.toggleSelectedState[0].onValueChanged.AddListener(delegate(bool v)
			{
				this.OnValueChangedSelectedState(0);
			});
			this.toggleSelectedState[1].onValueChanged.AddListener(delegate(bool v)
			{
				this.OnValueChangedSelectedState(1);
			});
			this.toggleAutoHide[0].onValueChanged.AddListener(delegate(bool v)
			{
				Studio.optionSystem.autoHide = !v;
			});
			this.toggleAutoHide[1].onValueChanged.AddListener(delegate(bool v)
			{
				Studio.optionSystem.autoHide = v;
			});
			this.toggleAutoSelect[0].onValueChanged.AddListener(delegate(bool v)
			{
				Studio.optionSystem.autoSelect = v;
			});
			this.toggleAutoSelect[1].onValueChanged.AddListener(delegate(bool v)
			{
				Studio.optionSystem.autoSelect = !v;
			});
			this.toggleSnap[0].onValueChanged.AddListener(delegate(bool v)
			{
				Studio.optionSystem.snap = 0;
			});
			this.toggleSnap[1].onValueChanged.AddListener(delegate(bool v)
			{
				Studio.optionSystem.snap = 1;
			});
			this.toggleSnap[2].onValueChanged.AddListener(delegate(bool v)
			{
				Studio.optionSystem.snap = 2;
			});
			this.charaFKColor.Init(this.spriteActive);
			this.itemFKColor.Init(this.spriteActive);
			this.routeSystem.Init(this.spriteActive);
			this.etc.Init(this.spriteActive);
			this.IsInit = true;
		}

		// Token: 0x0600A465 RID: 42085 RVA: 0x00431FDB File Offset: 0x004303DB
		private void Start()
		{
			this.Init();
		}

		// Token: 0x04008173 RID: 33139
		[SerializeField]
		private OptionCtrl.InputCombination inputCameraX = new OptionCtrl.InputCombination();

		// Token: 0x04008174 RID: 33140
		[SerializeField]
		private OptionCtrl.InputCombination inputCameraY = new OptionCtrl.InputCombination();

		// Token: 0x04008175 RID: 33141
		[SerializeField]
		private OptionCtrl.InputCombination inputCameraSpeed = new OptionCtrl.InputCombination();

		// Token: 0x04008176 RID: 33142
		[SerializeField]
		private OptionCtrl.InputCombination _inputSize = new OptionCtrl.InputCombination();

		// Token: 0x04008177 RID: 33143
		[SerializeField]
		private OptionCtrl.InputCombination inputSpeed = new OptionCtrl.InputCombination();

		// Token: 0x04008178 RID: 33144
		[SerializeField]
		private Toggle[] toggleInitialPosition;

		// Token: 0x04008179 RID: 33145
		[SerializeField]
		private Toggle[] toggleSelectedState;

		// Token: 0x0400817A RID: 33146
		[SerializeField]
		private Toggle[] toggleAutoHide;

		// Token: 0x0400817B RID: 33147
		[SerializeField]
		private Toggle[] toggleAutoSelect;

		// Token: 0x0400817C RID: 33148
		[SerializeField]
		private Toggle[] toggleSnap;

		// Token: 0x0400817D RID: 33149
		[SerializeField]
		private CameraControl cameraControl;

		// Token: 0x0400817E RID: 33150
		[SerializeField]
		private Sprite[] spriteActive;

		// Token: 0x0400817F RID: 33151
		[SerializeField]
		private OptionCtrl.CharaFKColor charaFKColor = new OptionCtrl.CharaFKColor();

		// Token: 0x04008180 RID: 33152
		[SerializeField]
		private OptionCtrl.ItemFKColor itemFKColor = new OptionCtrl.ItemFKColor();

		// Token: 0x04008181 RID: 33153
		[SerializeField]
		private OptionCtrl.RouteSystem routeSystem = new OptionCtrl.RouteSystem();

		// Token: 0x04008182 RID: 33154
		[SerializeField]
		private OptionCtrl.Etc etc = new OptionCtrl.Etc();

		// Token: 0x0200132F RID: 4911
		[Serializable]
		private class CommonInfo
		{
			// Token: 0x17002277 RID: 8823
			// (get) Token: 0x0600A472 RID: 42098 RVA: 0x00432078 File Offset: 0x00430478
			// (set) Token: 0x0600A473 RID: 42099 RVA: 0x00432085 File Offset: 0x00430485
			public bool active
			{
				get
				{
					return this.root.activeSelf;
				}
				set
				{
					if (this.root.SetActiveIfDifferent(value))
					{
						this.button.image.sprite = this.sprite[(!value) ? 0 : 1];
					}
				}
			}

			// Token: 0x17002278 RID: 8824
			// (get) Token: 0x0600A474 RID: 42100 RVA: 0x004320BC File Offset: 0x004304BC
			// (set) Token: 0x0600A475 RID: 42101 RVA: 0x004320C4 File Offset: 0x004304C4
			protected bool isUpdateInfo { get; set; }

			// Token: 0x0600A476 RID: 42102 RVA: 0x004320CD File Offset: 0x004304CD
			public virtual void Init(Sprite[] _sprite)
			{
				this.button.onClick.AddListener(delegate()
				{
					this.active = !this.active;
				});
				this.sprite = _sprite;
				this.isUpdateInfo = false;
			}

			// Token: 0x0600A477 RID: 42103 RVA: 0x004320F9 File Offset: 0x004304F9
			public virtual void UpdateInfo()
			{
			}

			// Token: 0x0400818D RID: 33165
			public GameObject root;

			// Token: 0x0400818E RID: 33166
			public Button button;

			// Token: 0x04008190 RID: 33168
			private Sprite[] sprite;
		}

		// Token: 0x02001330 RID: 4912
		[Serializable]
		public class InputCombination
		{
			// Token: 0x17002279 RID: 8825
			// (set) Token: 0x0600A47A RID: 42106 RVA: 0x00432114 File Offset: 0x00430514
			public bool interactable
			{
				set
				{
					this.input.interactable = value;
					this.slider.interactable = value;
				}
			}

			// Token: 0x1700227A RID: 8826
			// (get) Token: 0x0600A47B RID: 42107 RVA: 0x0043212E File Offset: 0x0043052E
			// (set) Token: 0x0600A47C RID: 42108 RVA: 0x0043213B File Offset: 0x0043053B
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

			// Token: 0x1700227B RID: 8827
			// (get) Token: 0x0600A47D RID: 42109 RVA: 0x0043215A File Offset: 0x0043055A
			// (set) Token: 0x0600A47E RID: 42110 RVA: 0x00432167 File Offset: 0x00430567
			public float value
			{
				get
				{
					return this.slider.value;
				}
				set
				{
					this.slider.value = value;
					this.input.text = value.ToString("0.00");
				}
			}

			// Token: 0x1700227C RID: 8828
			// (get) Token: 0x0600A47F RID: 42111 RVA: 0x0043218C File Offset: 0x0043058C
			public float min
			{
				get
				{
					return this.slider.minValue;
				}
			}

			// Token: 0x1700227D RID: 8829
			// (get) Token: 0x0600A480 RID: 42112 RVA: 0x00432199 File Offset: 0x00430599
			public float max
			{
				get
				{
					return this.slider.maxValue;
				}
			}

			// Token: 0x04008191 RID: 33169
			public Slider slider;

			// Token: 0x04008192 RID: 33170
			public InputField input;
		}

		// Token: 0x02001331 RID: 4913
		[Serializable]
		private class CharaFKColor : OptionCtrl.CommonInfo
		{
			// Token: 0x0600A482 RID: 42114 RVA: 0x004321B0 File Offset: 0x004305B0
			public override void Init(Sprite[] _sprite)
			{
				base.Init(_sprite);
				for (int i = 0; i < this.buttons.Length; i++)
				{
					int no = i;
					this.buttons[i].onClick.AddListener(delegate()
					{
						this.OnClickColor(no);
					});
				}
				this.toggleLine.OnValueChangedAsObservable().Subscribe(delegate(bool _b)
				{
					Studio.optionSystem.lineFK = _b;
				});
			}

			// Token: 0x0600A483 RID: 42115 RVA: 0x00432240 File Offset: 0x00430640
			public override void UpdateInfo()
			{
				base.isUpdateInfo = true;
				this.buttons[0].image.color = Studio.optionSystem.colorFKHair;
				this.buttons[1].image.color = Studio.optionSystem.colorFKNeck;
				this.buttons[2].image.color = Studio.optionSystem.colorFKBreast;
				this.buttons[3].image.color = Studio.optionSystem.colorFKBody;
				this.buttons[4].image.color = Studio.optionSystem.colorFKRightHand;
				this.buttons[5].image.color = Studio.optionSystem.colorFKLeftHand;
				this.buttons[6].image.color = Studio.optionSystem.colorFKSkirt;
				this.toggleLine.isOn = Studio.optionSystem.lineFK;
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A484 RID: 42116 RVA: 0x00432334 File Offset: 0x00430734
			private void OnClickColor(int _idx)
			{
				string[] array = new string[]
				{
					"髪",
					"首",
					"胸",
					"体",
					"右手",
					"左手",
					"スカート"
				};
				if (Singleton<Studio>.Instance.colorPalette.Check(string.Format("FKカラー {0}", array[_idx])))
				{
					Singleton<Studio>.Instance.colorPalette.visible = false;
					return;
				}
				Singleton<Studio>.Instance.colorPalette.Setup(string.Format("FKカラー {0}", array[_idx]), Studio.optionSystem.GetFKColor(_idx), delegate(Color _c)
				{
					this.SetColor(_idx, _c);
				}, false);
			}

			// Token: 0x0600A485 RID: 42117 RVA: 0x00432409 File Offset: 0x00430809
			private void SetColor(int _idx, Color _color)
			{
				Studio.optionSystem.SetFKColor(_idx, _color);
				this.buttons[_idx].image.color = _color;
				Singleton<Studio>.Instance.UpdateCharaFKColor();
			}

			// Token: 0x0600A486 RID: 42118 RVA: 0x00432434 File Offset: 0x00430834
			private void OnValueChangedLine(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				Studio.optionSystem.lineFK = _value;
			}

			// Token: 0x04008193 RID: 33171
			public Button[] buttons;

			// Token: 0x04008194 RID: 33172
			public Toggle toggleLine;
		}

		// Token: 0x02001332 RID: 4914
		[Serializable]
		private class ItemFKColor : OptionCtrl.CommonInfo
		{
			// Token: 0x0600A489 RID: 42121 RVA: 0x00432499 File Offset: 0x00430899
			public override void Init(Sprite[] _sprite)
			{
				base.Init(_sprite);
				this.buttonColor.onClick.AddListener(new UnityAction(this.OnClickColor));
			}

			// Token: 0x0600A48A RID: 42122 RVA: 0x004324BE File Offset: 0x004308BE
			public override void UpdateInfo()
			{
				base.isUpdateInfo = true;
				this.buttonColor.image.color = Studio.optionSystem.colorFKItem;
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A48B RID: 42123 RVA: 0x004324E8 File Offset: 0x004308E8
			private void OnClickColor()
			{
				if (Singleton<Studio>.Instance.colorPalette.Check("FKカラー アイテム"))
				{
					Singleton<Studio>.Instance.colorPalette.visible = false;
					return;
				}
				Singleton<Studio>.Instance.colorPalette.Setup("FKカラー アイテム", Studio.optionSystem.colorFKItem, new Action<Color>(this.SetColor), false);
			}

			// Token: 0x0600A48C RID: 42124 RVA: 0x0043254A File Offset: 0x0043094A
			private void SetColor(Color _color)
			{
				Studio.optionSystem.colorFKItem = _color;
				this.buttonColor.image.color = _color;
				Singleton<Studio>.Instance.UpdateItemFKColor();
			}

			// Token: 0x04008196 RID: 33174
			public Button buttonColor;
		}

		// Token: 0x02001333 RID: 4915
		[Serializable]
		private class RouteSystem : OptionCtrl.CommonInfo
		{
			// Token: 0x0600A48E RID: 42126 RVA: 0x00432588 File Offset: 0x00430988
			public override void Init(Sprite[] _sprite)
			{
				base.Init(_sprite);
				this.inputWidth.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedRouteWidth));
				this.inputWidth.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditRouteWidth));
				this.toggleLimit[0].onValueChanged.AddListener(delegate(bool _b)
				{
					if (_b)
					{
						Studio.optionSystem.routePointLimit = true;
					}
				});
				this.toggleLimit[1].onValueChanged.AddListener(delegate(bool _b)
				{
					if (_b)
					{
						Studio.optionSystem.routePointLimit = false;
					}
				});
			}

			// Token: 0x0600A48F RID: 42127 RVA: 0x0043263C File Offset: 0x00430A3C
			public override void UpdateInfo()
			{
				base.isUpdateInfo = true;
				this.inputWidth.value = Studio.optionSystem._routeLineWidth;
				this.toggleLimit[(!Studio.optionSystem.routePointLimit) ? 1 : 0].isOn = true;
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A490 RID: 42128 RVA: 0x0043268F File Offset: 0x00430A8F
			private void OnValueChangedRouteWidth(float _value)
			{
				Studio.optionSystem._routeLineWidth = _value;
				this.inputWidth.value = _value;
				Singleton<Studio>.Instance.routeControl.ReflectOption();
			}

			// Token: 0x0600A491 RID: 42129 RVA: 0x004326B8 File Offset: 0x00430AB8
			private void OnEndEditRouteWidth(string _text)
			{
				float num = Mathf.Clamp(Utility.StringToFloat(_text), this.inputWidth.min, this.inputWidth.max);
				Studio.optionSystem._routeLineWidth = num;
				this.inputWidth.value = num;
				Singleton<Studio>.Instance.routeControl.ReflectOption();
			}

			// Token: 0x04008197 RID: 33175
			public OptionCtrl.InputCombination inputWidth = new OptionCtrl.InputCombination();

			// Token: 0x04008198 RID: 33176
			public Toggle[] toggleLimit;
		}

		// Token: 0x02001334 RID: 4916
		[Serializable]
		private class Etc : OptionCtrl.CommonInfo
		{
			// Token: 0x0600A495 RID: 42133 RVA: 0x0043273C File Offset: 0x00430B3C
			public override void Init(Sprite[] _sprite)
			{
				base.Init(_sprite);
				this.toggleStartup[0].onValueChanged.AddListener(delegate(bool _b)
				{
					if (_b)
					{
						Studio.optionSystem.startupLoad = true;
					}
				});
				this.toggleStartup[1].onValueChanged.AddListener(delegate(bool _b)
				{
					if (_b)
					{
						Studio.optionSystem.startupLoad = false;
					}
				});
			}

			// Token: 0x0600A496 RID: 42134 RVA: 0x004327AE File Offset: 0x00430BAE
			public override void UpdateInfo()
			{
				base.isUpdateInfo = true;
				this.toggleStartup[(!Studio.optionSystem.startupLoad) ? 1 : 0].isOn = true;
				base.isUpdateInfo = false;
			}

			// Token: 0x0400819B RID: 33179
			public Toggle[] toggleStartup;
		}
	}
}
