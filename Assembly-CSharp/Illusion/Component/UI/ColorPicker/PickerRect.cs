using System;
using System.Diagnostics;
using System.Linq;
using Illusion.CustomAttributes;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Illusion.Component.UI.ColorPicker
{
	// Token: 0x02001050 RID: 4176
	public class PickerRect : MonoBehaviour
	{
		// Token: 0x17001EB2 RID: 7858
		// (get) Token: 0x06008C6F RID: 35951 RVA: 0x003AE964 File Offset: 0x003ACD64
		// (set) Token: 0x06008C70 RID: 35952 RVA: 0x003AE986 File Offset: 0x003ACD86
		public virtual Color ColorRGB
		{
			get
			{
				float[] rgb = this.RGB;
				return new Color(rgb[0], rgb[1], rgb[2]);
			}
			set
			{
				this.RGB = new float[]
				{
					value[0],
					value[1],
					value[2]
				};
			}
		}

		// Token: 0x17001EB3 RID: 7859
		// (get) Token: 0x06008C71 RID: 35953 RVA: 0x003AE9B5 File Offset: 0x003ACDB5
		// (set) Token: 0x06008C72 RID: 35954 RVA: 0x003AE9D8 File Offset: 0x003ACDD8
		public virtual float[] RGB
		{
			get
			{
				return new float[]
				{
					this.Red,
					this.Green,
					this.Blue
				};
			}
			set
			{
				this.Red = value[0];
				this.Green = value[1];
				this.Blue = value[2];
			}
		}

		// Token: 0x17001EB4 RID: 7860
		// (get) Token: 0x06008C73 RID: 35955 RVA: 0x003AE9F8 File Offset: 0x003ACDF8
		// (set) Token: 0x06008C74 RID: 35956 RVA: 0x003AEA1A File Offset: 0x003ACE1A
		public HsvColor ColorHSV
		{
			get
			{
				float[] hsv = this.HSV;
				return new HsvColor(hsv[0], hsv[1], hsv[2]);
			}
			set
			{
				this.HSV = new float[]
				{
					value[0],
					value[1],
					value[2]
				};
			}
		}

		// Token: 0x17001EB5 RID: 7861
		// (get) Token: 0x06008C75 RID: 35957 RVA: 0x003AEA46 File Offset: 0x003ACE46
		// (set) Token: 0x06008C76 RID: 35958 RVA: 0x003AEA69 File Offset: 0x003ACE69
		public float[] HSV
		{
			get
			{
				return new float[]
				{
					this.Hue,
					this.Saturation,
					this.Value
				};
			}
			set
			{
				this.Hue = value[0];
				this.Saturation = value[1];
				this.Value = value[2];
			}
		}

		// Token: 0x17001EB6 RID: 7862
		// (get) Token: 0x06008C77 RID: 35959 RVA: 0x003AEA86 File Offset: 0x003ACE86
		// (set) Token: 0x06008C78 RID: 35960 RVA: 0x003AEA90 File Offset: 0x003ACE90
		public float Hue
		{
			get
			{
				return this._values[0];
			}
			set
			{
				this._values[0] = value;
			}
		}

		// Token: 0x17001EB7 RID: 7863
		// (get) Token: 0x06008C79 RID: 35961 RVA: 0x003AEA9B File Offset: 0x003ACE9B
		// (set) Token: 0x06008C7A RID: 35962 RVA: 0x003AEAA5 File Offset: 0x003ACEA5
		public float Saturation
		{
			get
			{
				return this._values[1];
			}
			set
			{
				this._values[1] = value;
			}
		}

		// Token: 0x17001EB8 RID: 7864
		// (get) Token: 0x06008C7B RID: 35963 RVA: 0x003AEAB0 File Offset: 0x003ACEB0
		// (set) Token: 0x06008C7C RID: 35964 RVA: 0x003AEABA File Offset: 0x003ACEBA
		public float Value
		{
			get
			{
				return this._values[2];
			}
			set
			{
				this._values[2] = value;
			}
		}

		// Token: 0x17001EB9 RID: 7865
		// (get) Token: 0x06008C7D RID: 35965 RVA: 0x003AEAC5 File Offset: 0x003ACEC5
		// (set) Token: 0x06008C7E RID: 35966 RVA: 0x003AEACF File Offset: 0x003ACECF
		public float Red
		{
			get
			{
				return this._values[3];
			}
			set
			{
				this._values[3] = value;
			}
		}

		// Token: 0x17001EBA RID: 7866
		// (get) Token: 0x06008C7F RID: 35967 RVA: 0x003AEADA File Offset: 0x003ACEDA
		// (set) Token: 0x06008C80 RID: 35968 RVA: 0x003AEAE4 File Offset: 0x003ACEE4
		public float Green
		{
			get
			{
				return this._values[4];
			}
			set
			{
				this._values[4] = value;
			}
		}

		// Token: 0x17001EBB RID: 7867
		// (get) Token: 0x06008C81 RID: 35969 RVA: 0x003AEAEF File Offset: 0x003ACEEF
		// (set) Token: 0x06008C82 RID: 35970 RVA: 0x003AEAF9 File Offset: 0x003ACEF9
		public float Blue
		{
			get
			{
				return this._values[5];
			}
			set
			{
				this._values[5] = value;
			}
		}

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x06008C83 RID: 35971 RVA: 0x003AEB04 File Offset: 0x003ACF04
		// (remove) Token: 0x06008C84 RID: 35972 RVA: 0x003AEB3C File Offset: 0x003ACF3C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Color> updateColorAction;

		// Token: 0x17001EBC RID: 7868
		// (get) Token: 0x06008C85 RID: 35973 RVA: 0x003AEB72 File Offset: 0x003ACF72
		// (set) Token: 0x06008C86 RID: 35974 RVA: 0x003AEB7F File Offset: 0x003ACF7F
		public PickerRect.Mode mode
		{
			get
			{
				return this._mode.Value;
			}
			set
			{
				this._mode.Value = value;
			}
		}

		// Token: 0x17001EBD RID: 7869
		// (get) Token: 0x06008C87 RID: 35975 RVA: 0x003AEB8D File Offset: 0x003ACF8D
		private float[] RateHSV
		{
			get
			{
				return new float[]
				{
					Mathf.InverseLerp(0f, 360f, this.Hue),
					this.Saturation,
					this.Value
				};
			}
		}

		// Token: 0x06008C88 RID: 35976 RVA: 0x003AEBC0 File Offset: 0x003ACFC0
		public void ChangeRectColor()
		{
			ImagePack imagePack = this.imgPack[0];
			if (imagePack == null || !imagePack.isTex)
			{
				return;
			}
			int num = (int)(this.mode % PickerRect.Mode.Red);
			Vector2 size = imagePack.size;
			int num2 = (int)size.x;
			int num3 = (int)size.y;
			Color[] array = new Color[num3 * num2];
			switch (this.mode)
			{
			case PickerRect.Mode.Hue:
			case PickerRect.Mode.Saturation:
			case PickerRect.Mode.Value:
			{
				float[] array2 = this.RateHSV;
				int[,] array3 = new int[,]
				{
					{
						1,
						2,
						0
					},
					{
						0,
						2,
						1
					},
					{
						0,
						1,
						2
					}
				};
				for (int i = 0; i < num3; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						array2[array3[num, 0]] = Mathf.InverseLerp(0f, size.x, (float)j);
						array2[array3[num, 1]] = Mathf.InverseLerp(0f, size.y, (float)i);
						array[i * num2 + j] = HsvColor.ToRgb(360f * array2[0], array2[1], array2[2]);
					}
				}
				break;
			}
			case PickerRect.Mode.Red:
			case PickerRect.Mode.Green:
			case PickerRect.Mode.Blue:
			{
				float[] array2 = this.RGB;
				int[,] array3 = new int[,]
				{
					{
						2,
						1,
						0
					},
					{
						2,
						0,
						1
					},
					{
						0,
						1,
						2
					}
				};
				for (int k = 0; k < num3; k++)
				{
					for (int l = 0; l < num2; l++)
					{
						array2[array3[num, 0]] = Mathf.InverseLerp(0f, size.x, (float)l);
						array2[array3[num, 1]] = Mathf.InverseLerp(0f, size.y, (float)k);
						array[k * num2 + l] = new Color(array2[0], array2[1], array2[2]);
					}
				}
				break;
			}
			}
			imagePack.SetPixels(array);
		}

		// Token: 0x06008C89 RID: 35977 RVA: 0x003AEDBC File Offset: 0x003AD1BC
		public void ChangeSliderColor()
		{
			ImagePack imagePack = this.imgPack[1];
			if (imagePack == null || !imagePack.isTex)
			{
				return;
			}
			int num = (int)(this.mode % PickerRect.Mode.Red);
			Vector2 size = imagePack.size;
			int num2 = (int)size.x;
			int num3 = (int)size.y;
			Color[] array = new Color[num3 * num2];
			switch (this.mode)
			{
			case PickerRect.Mode.Hue:
			case PickerRect.Mode.Saturation:
			case PickerRect.Mode.Value:
			{
				float[] array2 = this.RateHSV;
				if (this.mode == PickerRect.Mode.Hue)
				{
					array2[1] = 1f;
					array2[2] = 1f;
				}
				for (int i = 0; i < num3; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						array2[num] = Mathf.InverseLerp(0f, size.y, (float)i);
						array[i * num2 + j] = HsvColor.ToRgb(array2[0] * 360f, array2[1], array2[2]);
					}
				}
				break;
			}
			case PickerRect.Mode.Red:
			case PickerRect.Mode.Green:
			case PickerRect.Mode.Blue:
			{
				float[] array2 = this.RGB;
				for (int k = 0; k < num3; k++)
				{
					for (int l = 0; l < num2; l++)
					{
						array2[num] = Mathf.InverseLerp(0f, size.y, (float)k);
						array[k * num2 + l] = new Color(array2[0], array2[1], array2[2]);
					}
				}
				break;
			}
			}
			imagePack.SetPixels(array);
		}

		// Token: 0x06008C8A RID: 35978 RVA: 0x003AEF58 File Offset: 0x003AD358
		public void CalcRectPointer()
		{
			if (this.pointer == null)
			{
				return;
			}
			Rect rect = this.imgPack[0].rectTransform.rect;
			Action<float[], int, int> action = delegate(float[] val, int x, int y)
			{
				this.pointer.anchoredPosition = new Vector2(rect.width * val[x], rect.height * val[y]);
			};
			switch (this.mode)
			{
			case PickerRect.Mode.Hue:
				action(this.RateHSV, 1, 2);
				break;
			case PickerRect.Mode.Saturation:
				action(this.RateHSV, 0, 2);
				break;
			case PickerRect.Mode.Value:
				action(this.RateHSV, 0, 1);
				break;
			case PickerRect.Mode.Red:
				action(this.RGB, 2, 1);
				break;
			case PickerRect.Mode.Green:
				action(this.RGB, 2, 0);
				break;
			case PickerRect.Mode.Blue:
				action(this.RGB, 0, 1);
				break;
			}
		}

		// Token: 0x06008C8B RID: 35979 RVA: 0x003AF048 File Offset: 0x003AD448
		public void CalcSliderValue()
		{
			if (this.slider == null)
			{
				return;
			}
			switch (this.mode)
			{
			case PickerRect.Mode.Hue:
			case PickerRect.Mode.Saturation:
			case PickerRect.Mode.Value:
				this.slider.value = this.RateHSV[(int)this.mode];
				break;
			case PickerRect.Mode.Red:
			case PickerRect.Mode.Green:
			case PickerRect.Mode.Blue:
				this.slider.value = this.RGB[(int)(this.mode % PickerRect.Mode.Red)];
				break;
			}
		}

		// Token: 0x06008C8C RID: 35980 RVA: 0x003AF0CD File Offset: 0x003AD4CD
		public virtual void SetColor(HsvColor hsv, PickerRect.Control ctrlType)
		{
			this.ColorHSV = hsv;
			this.ColorRGB = HsvColor.ToRgb(hsv);
			this.SetColor(ctrlType);
		}

		// Token: 0x06008C8D RID: 35981 RVA: 0x003AF0E9 File Offset: 0x003AD4E9
		public virtual void SetColor(Color rgb, PickerRect.Control ctrlType)
		{
			this.ColorRGB = rgb;
			this.ColorHSV = HsvColor.FromRgb(rgb);
			this.SetColor(ctrlType);
		}

		// Token: 0x06008C8E RID: 35982 RVA: 0x003AF105 File Offset: 0x003AD505
		public virtual void SetColor(PickerRect.Control ctrlType)
		{
			if (ctrlType != PickerRect.Control.Rect)
			{
				if (ctrlType == PickerRect.Control.Slider)
				{
					this.ChangeRectColor();
				}
			}
			else
			{
				this.ChangeSliderColor();
			}
			this.updateColorAction.Call(this.ColorRGB);
		}

		// Token: 0x06008C8F RID: 35983 RVA: 0x003AF141 File Offset: 0x003AD541
		public virtual void SetColor(Color color)
		{
			this.ColorRGB = color;
			this.ColorHSV = HsvColor.FromRgb(color);
			this.CalcRectPointer();
			this.CalcSliderValue();
		}

		// Token: 0x06008C90 RID: 35984 RVA: 0x003AF164 File Offset: 0x003AD564
		protected void Awake()
		{
			this.ColorHSV = new HsvColor(0f, 0f, 1f);
			this.ColorRGB = Color.white;
			Image[] array = new Image[]
			{
				this.info.GetOrAddComponent<Image>(),
				this.slider.GetOrAddComponent<Image>()
			};
			this.imgPack = new ImagePack[array.Length];
			for (int i = 0; i < this.imgPack.Length; i++)
			{
				this.imgPack[i] = new ImagePack(array[i]);
			}
		}

		// Token: 0x06008C91 RID: 35985 RVA: 0x003AF1F0 File Offset: 0x003AD5F0
		protected virtual void Start()
		{
			this._mode.TakeUntilDestroy(this).Subscribe(delegate(PickerRect.Mode _)
			{
				this.CalcRectPointer();
				this.CalcSliderValue();
				this.ChangeRectColor();
				this.ChangeSliderColor();
			});
			if (this.modeChangeToggles.Any<Toggle>())
			{
				(from item in this.modeChangeToggles.Select((Toggle toggle, int index) => new
				{
					toggle = toggle,
					mode = (PickerRect.Mode)index
				})
				where item.toggle != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.toggle.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool _)
					{
						this.mode = item.mode;
					});
				});
			}
			if (this.slider != null)
			{
				PickerRect.Control ctrl = PickerRect.Control.Slider;
				Action<Func<HsvColor, HsvColor>> hsv = delegate(Func<HsvColor, HsvColor> func)
				{
					this.SetColor(func(this.ColorHSV), ctrl);
				};
				Action<Func<Color, Color>> rgb = delegate(Func<Color, Color> func)
				{
					this.SetColor(func(this.ColorRGB), ctrl);
				};
				this.slider.onValueChanged.AsObservable<float>().Subscribe(delegate(float value)
				{
					switch (this.mode)
					{
					case PickerRect.Mode.Hue:
						hsv(delegate(HsvColor c)
						{
							c.H = value * 360f;
							return c;
						});
						break;
					case PickerRect.Mode.Saturation:
						hsv(delegate(HsvColor c)
						{
							c.S = value;
							return c;
						});
						break;
					case PickerRect.Mode.Value:
						hsv(delegate(HsvColor c)
						{
							c.V = value;
							return c;
						});
						break;
					case PickerRect.Mode.Red:
						rgb(delegate(Color c)
						{
							c.r = value;
							return c;
						});
						break;
					case PickerRect.Mode.Green:
						rgb(delegate(Color c)
						{
							c.g = value;
							return c;
						});
						break;
					case PickerRect.Mode.Blue:
						rgb(delegate(Color c)
						{
							c.b = value;
							return c;
						});
						break;
					}
				});
			}
			(from _ in this.UpdateAsObservable().SkipWhile((Unit _) => this.info == null || this.pointer == null)
			where base.enabled
			where this.info.isOn
			select this.info.imagePos).DistinctUntilChanged<Vector2>().Subscribe(delegate(Vector2 pos)
			{
				this.pointer.anchoredPosition = pos;
				Vector2 imageRate = this.info.imageRate;
				PickerRect.Control ctrlType = PickerRect.Control.Rect;
				switch (this.mode)
				{
				case PickerRect.Mode.Hue:
				{
					HsvColor colorHSV = this.ColorHSV;
					colorHSV.S = imageRate.x;
					colorHSV.V = imageRate.y;
					this.SetColor(colorHSV, ctrlType);
					break;
				}
				case PickerRect.Mode.Saturation:
				{
					HsvColor colorHSV2 = this.ColorHSV;
					colorHSV2.H = imageRate.x * 360f;
					colorHSV2.V = imageRate.y;
					this.SetColor(colorHSV2, ctrlType);
					break;
				}
				case PickerRect.Mode.Value:
				{
					HsvColor colorHSV3 = this.ColorHSV;
					colorHSV3.H = imageRate.x * 360f;
					colorHSV3.S = imageRate.y;
					this.SetColor(colorHSV3, ctrlType);
					break;
				}
				case PickerRect.Mode.Red:
				{
					Color colorRGB = this.ColorRGB;
					colorRGB.b = imageRate.x;
					colorRGB.g = imageRate.y;
					this.SetColor(colorRGB, ctrlType);
					break;
				}
				case PickerRect.Mode.Green:
				{
					Color colorRGB2 = this.ColorRGB;
					colorRGB2.b = imageRate.x;
					colorRGB2.r = imageRate.y;
					this.SetColor(colorRGB2, ctrlType);
					break;
				}
				case PickerRect.Mode.Blue:
				{
					Color colorRGB3 = this.ColorRGB;
					colorRGB3.r = imageRate.x;
					colorRGB3.g = imageRate.y;
					this.SetColor(colorRGB3, ctrlType);
					break;
				}
				}
			});
		}

		// Token: 0x06008C92 RID: 35986 RVA: 0x003AF358 File Offset: 0x003AD758
		[ContextMenu("Setup")]
		protected void Setup()
		{
			this.modeChangeToggles = base.GetComponentsInChildren<Toggle>();
			this.info = base.GetComponentInChildren<Info>();
			if (this.info.transform.childCount != 0)
			{
				this.pointer = this.info.transform.GetChild(0).GetComponentInChildren<RectTransform>();
			}
		}

		// Token: 0x0400725A RID: 29274
		private float[] _values = new float[Utils.Enum<PickerRect.Mode>.Length];

		// Token: 0x0400725C RID: 29276
		[SerializeField]
		protected PickerRect.ModeReactiveProperty _mode = new PickerRect.ModeReactiveProperty(PickerRect.Mode.Hue);

		// Token: 0x0400725D RID: 29277
		[NamedArray(typeof(PickerRect.Mode))]
		[SerializeField]
		private Toggle[] modeChangeToggles = new Toggle[Utils.Enum<PickerRect.Mode>.Length];

		// Token: 0x0400725E RID: 29278
		[SerializeField]
		public Info info;

		// Token: 0x0400725F RID: 29279
		[SerializeField]
		private Slider slider;

		// Token: 0x04007260 RID: 29280
		[SerializeField]
		private RectTransform pointer;

		// Token: 0x04007261 RID: 29281
		private ImagePack[] imgPack;

		// Token: 0x02001051 RID: 4177
		public enum Mode
		{
			// Token: 0x04007266 RID: 29286
			Hue,
			// Token: 0x04007267 RID: 29287
			Saturation,
			// Token: 0x04007268 RID: 29288
			Value,
			// Token: 0x04007269 RID: 29289
			Red,
			// Token: 0x0400726A RID: 29290
			Green,
			// Token: 0x0400726B RID: 29291
			Blue
		}

		// Token: 0x02001052 RID: 4178
		public enum Control
		{
			// Token: 0x0400726D RID: 29293
			None,
			// Token: 0x0400726E RID: 29294
			Rect,
			// Token: 0x0400726F RID: 29295
			Slider
		}

		// Token: 0x02001053 RID: 4179
		[Serializable]
		public class ModeReactiveProperty : ReactiveProperty<PickerRect.Mode>
		{
			// Token: 0x06008C9D RID: 35997 RVA: 0x003AF614 File Offset: 0x003ADA14
			public ModeReactiveProperty()
			{
			}

			// Token: 0x06008C9E RID: 35998 RVA: 0x003AF61C File Offset: 0x003ADA1C
			public ModeReactiveProperty(PickerRect.Mode initialValue) : base(initialValue)
			{
			}
		}
	}
}
