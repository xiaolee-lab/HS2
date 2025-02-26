using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Illusion.Component.UI.ColorPicker
{
	// Token: 0x02001054 RID: 4180
	public class PickerRectA : PickerRect
	{
		// Token: 0x17001EBE RID: 7870
		// (get) Token: 0x06008CA0 RID: 36000 RVA: 0x003AF84C File Offset: 0x003ADC4C
		// (set) Token: 0x06008CA1 RID: 36001 RVA: 0x003AF86E File Offset: 0x003ADC6E
		public override Color ColorRGB
		{
			get
			{
				Color colorRGB = base.ColorRGB;
				colorRGB.a = this.Alpha;
				return colorRGB;
			}
			set
			{
				base.ColorRGB = value;
				this.Alpha = value.a;
			}
		}

		// Token: 0x17001EBF RID: 7871
		// (get) Token: 0x06008CA2 RID: 36002 RVA: 0x003AF884 File Offset: 0x003ADC84
		// (set) Token: 0x06008CA3 RID: 36003 RVA: 0x003AF8A5 File Offset: 0x003ADCA5
		public override float[] RGB
		{
			get
			{
				return base.RGB.Concat(new float[]
				{
					this.Alpha
				}).ToArray<float>();
			}
			set
			{
				base.RGB = value;
				if (value.Length >= 4)
				{
					this.Alpha = value[3];
				}
			}
		}

		// Token: 0x17001EC0 RID: 7872
		// (get) Token: 0x06008CA4 RID: 36004 RVA: 0x003AF8C0 File Offset: 0x003ADCC0
		// (set) Token: 0x06008CA5 RID: 36005 RVA: 0x003AF8C8 File Offset: 0x003ADCC8
		public float Alpha
		{
			get
			{
				return this.alpha;
			}
			set
			{
				this.alpha = value;
			}
		}

		// Token: 0x06008CA6 RID: 36006 RVA: 0x003AF8D1 File Offset: 0x003ADCD1
		public override void SetColor(HsvColor hsv, PickerRect.Control ctrlType)
		{
			base.ColorHSV = hsv;
			base.ColorRGB = HsvColor.ToRgb(hsv);
			this.SetColor(ctrlType);
		}

		// Token: 0x06008CA7 RID: 36007 RVA: 0x003AF8ED File Offset: 0x003ADCED
		public override void SetColor(Color color)
		{
			base.SetColor(color);
			this.CalcSliderAValue();
		}

		// Token: 0x06008CA8 RID: 36008 RVA: 0x003AF8FC File Offset: 0x003ADCFC
		public void CalcSliderAValue()
		{
			if (this.sliderA == null)
			{
				return;
			}
			this.sliderA.value = this.Alpha;
		}

		// Token: 0x06008CA9 RID: 36009 RVA: 0x003AF924 File Offset: 0x003ADD24
		protected override void Start()
		{
			this._mode.TakeUntilDestroy(this).Subscribe(delegate(PickerRect.Mode _)
			{
				this.CalcSliderAValue();
			});
			if (this.sliderA != null)
			{
				this.isAlpha.TakeUntilDestroy(this).Subscribe(new Action<bool>(this.sliderA.gameObject.SetActive));
				this.sliderA.onValueChanged.AsObservable<float>().Subscribe(delegate(float value)
				{
					this.Alpha = value;
					this.SetColor(this.ColorRGB, PickerRect.Control.None);
				});
			}
			base.Start();
		}

		// Token: 0x04007270 RID: 29296
		[SerializeField]
		private Slider sliderA;

		// Token: 0x04007271 RID: 29297
		public BoolReactiveProperty isAlpha = new BoolReactiveProperty(true);

		// Token: 0x04007272 RID: 29298
		private float alpha;
	}
}
