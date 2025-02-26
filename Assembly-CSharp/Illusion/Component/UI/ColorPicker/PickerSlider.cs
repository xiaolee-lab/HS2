using System;
using System.Diagnostics;
using System.Linq;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Illusion.Component.UI.ColorPicker
{
	// Token: 0x02001055 RID: 4181
	public class PickerSlider : MonoBehaviour
	{
		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x06008CAD RID: 36013 RVA: 0x003AF9FC File Offset: 0x003ADDFC
		// (remove) Token: 0x06008CAE RID: 36014 RVA: 0x003AFA34 File Offset: 0x003ADE34
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Color> updateColorAction;

		// Token: 0x17001EC1 RID: 7873
		// (get) Token: 0x06008CAF RID: 36015 RVA: 0x003AFA6A File Offset: 0x003ADE6A
		// (set) Token: 0x06008CB0 RID: 36016 RVA: 0x003AFA8D File Offset: 0x003ADE8D
		public Color color
		{
			get
			{
				return this.isHSV ? this._color.HSVToRGB() : this._color;
			}
			set
			{
				this._color = (this.isHSV ? value.RGBToHSV() : value);
				this.SetColor(this._color);
			}
		}

		// Token: 0x17001EC2 RID: 7874
		// (get) Token: 0x06008CB1 RID: 36017 RVA: 0x003AFAB8 File Offset: 0x003ADEB8
		// (set) Token: 0x06008CB2 RID: 36018 RVA: 0x003AFAC5 File Offset: 0x003ADEC5
		public bool isHSV
		{
			get
			{
				return this._isHSV.Value;
			}
			set
			{
				this._isHSV.Value = value;
			}
		}

		// Token: 0x06008CB3 RID: 36019 RVA: 0x003AFAD4 File Offset: 0x003ADED4
		public void ChangeSliderColor()
		{
			for (int i = 0; i < 3; i++)
			{
				this.ChangeSliderColor(i);
			}
		}

		// Token: 0x06008CB4 RID: 36020 RVA: 0x003AFAFC File Offset: 0x003ADEFC
		public void ChangeSliderColor(int index)
		{
			ImagePack imagePack = this.imgPack[index];
			if (imagePack == null || !imagePack.isTex)
			{
				return;
			}
			Vector2 size = imagePack.size;
			int num = (int)size.x;
			int num2 = (int)size.y;
			Color[] colors = new Color[num2 * num];
			float[] val = new float[]
			{
				this._color[0],
				this._color[1],
				this._color[2]
			};
			Action<int> action;
			if (!this.isHSV)
			{
				action = delegate(int i)
				{
					colors[i] = new Color(val[0], val[1], val[2]);
				};
			}
			else
			{
				if (index == 0)
				{
					val[1] = 1f;
					val[2] = 1f;
				}
				action = delegate(int i)
				{
					colors[i] = Color.HSVToRGB(val[0], val[1], val[2]);
				};
			}
			if (num2 > num)
			{
				for (int m = 0; m < num2; m++)
				{
					for (int j = 0; j < num; j++)
					{
						val[index] = Mathf.InverseLerp(0f, size.y, (float)m);
						action(m * num + j);
					}
				}
			}
			else
			{
				for (int k = 0; k < num2; k++)
				{
					for (int l = 0; l < num; l++)
					{
						val[index] = Mathf.InverseLerp(0f, size.x, (float)l);
						action(k * num + l);
					}
				}
			}
			imagePack.SetPixels(colors);
		}

		// Token: 0x06008CB5 RID: 36021 RVA: 0x003AFCA0 File Offset: 0x003AE0A0
		public void CalcSliderValue()
		{
			for (int i = 0; i < 3; i++)
			{
				if (!(this.sliders[i] == null))
				{
					this.sliders[i].value = this._color[i];
				}
			}
			if (this.sliderA != null)
			{
				this.sliderA.value = this._color.a;
			}
		}

		// Token: 0x06008CB6 RID: 36022 RVA: 0x003AFD17 File Offset: 0x003AE117
		public virtual void SetColor(Color color)
		{
			this._color = color;
			this.ChangeSliderColor();
			this.CalcSliderValue();
			this.updateColorAction.Call(this.color);
		}

		// Token: 0x06008CB7 RID: 36023 RVA: 0x003AFD40 File Offset: 0x003AE140
		protected virtual void Awake()
		{
			this.sliders = new Slider[]
			{
				this.sliderR,
				this.sliderG,
				this.sliderB
			};
			this.imgPack = new ImagePack[this.sliders.Length];
			for (int i = 0; i < this.sliders.Length; i++)
			{
				Slider slider = this.sliders[i];
				if (!(slider == null))
				{
					this.imgPack[i] = new ImagePack(slider.GetOrAddComponent<Image>());
				}
			}
		}

		// Token: 0x06008CB8 RID: 36024 RVA: 0x003AFDD0 File Offset: 0x003AE1D0
		protected virtual void Start()
		{
			this._isHSV.TakeUntilDestroy(this).Subscribe(delegate(bool isOn)
			{
				this.SetColor(isOn ? this._color.RGBToHSV() : this._color.HSVToRGB());
			});
			this.sliders.Select((Slider p, int index) => new
			{
				slider = p,
				index = index
			}).ToList().ForEach(delegate(p)
			{
				p.slider.onValueChanged.AsObservable<float>().Subscribe(delegate(float value)
				{
					this._color[p.index] = p.slider.value;
					this.SetColor(this._color);
				});
			});
			if (this.sliderA != null)
			{
				this.useAlpha.TakeUntilDestroy(this).Subscribe(new Action<bool>(this.sliderA.gameObject.SetActive));
				this.sliderA.onValueChanged.AsObservable<float>().Subscribe(delegate(float value)
				{
					this._color.a = value;
					this.updateColorAction.Call(this.color);
				});
			}
		}

		// Token: 0x04007274 RID: 29300
		[Tooltip("RedSlider")]
		[SerializeField]
		protected Slider sliderR;

		// Token: 0x04007275 RID: 29301
		[Tooltip("GreenSlider")]
		[SerializeField]
		protected Slider sliderG;

		// Token: 0x04007276 RID: 29302
		[Tooltip("BlueSlider")]
		[SerializeField]
		protected Slider sliderB;

		// Token: 0x04007277 RID: 29303
		[Tooltip("AlphaSlider")]
		[SerializeField]
		protected Slider sliderA;

		// Token: 0x04007278 RID: 29304
		public BoolReactiveProperty useAlpha = new BoolReactiveProperty(true);

		// Token: 0x04007279 RID: 29305
		[SerializeField]
		protected BoolReactiveProperty _isHSV = new BoolReactiveProperty(false);

		// Token: 0x0400727A RID: 29306
		private Slider[] sliders;

		// Token: 0x0400727B RID: 29307
		private ImagePack[] imgPack;

		// Token: 0x0400727C RID: 29308
		private Color _color = Color.white;
	}
}
