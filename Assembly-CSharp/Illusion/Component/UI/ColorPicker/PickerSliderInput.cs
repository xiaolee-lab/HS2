using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Illusion.Component.UI.ColorPicker
{
	// Token: 0x02001056 RID: 4182
	public class PickerSliderInput : PickerSlider
	{
		// Token: 0x06008CBE RID: 36030 RVA: 0x003AFFF8 File Offset: 0x003AE3F8
		public string ConvertTextFromValue(int min, int max, float value)
		{
			return ((int)Mathf.Lerp((float)min, (float)max, value)).ToString();
		}

		// Token: 0x06008CBF RID: 36031 RVA: 0x003B0020 File Offset: 0x003AE420
		public float ConvertValueFromText(int min, int max, string buf)
		{
			if (buf.IsNullOrEmpty())
			{
				return 0f;
			}
			int num;
			if (!int.TryParse(buf, out num))
			{
				return 0f;
			}
			return Mathf.InverseLerp((float)min, (float)max, (float)num);
		}

		// Token: 0x06008CC0 RID: 36032 RVA: 0x003B005C File Offset: 0x003AE45C
		public void SetInputText()
		{
			if (base.isHSV)
			{
				this.inputR.text = this.ConvertTextFromValue(0, 360, base.color.r);
				this.inputG.text = this.ConvertTextFromValue(0, 100, base.color.g);
				this.inputB.text = this.ConvertTextFromValue(0, 100, base.color.b);
				this.inputA.text = this.ConvertTextFromValue(0, 100, base.color.a);
			}
			else
			{
				this.inputR.text = this.ConvertTextFromValue(0, 255, base.color.r);
				this.inputG.text = this.ConvertTextFromValue(0, 255, base.color.g);
				this.inputB.text = this.ConvertTextFromValue(0, 255, base.color.b);
				this.inputA.text = this.ConvertTextFromValue(0, 100, base.color.a);
			}
		}

		// Token: 0x06008CC1 RID: 36033 RVA: 0x003B019C File Offset: 0x003AE59C
		protected override void Start()
		{
			base.Start();
			this.sliderR.onValueChanged.AsObservable<float>().Subscribe(delegate(float value)
			{
				if (base.isHSV)
				{
					this.inputR.text = this.ConvertTextFromValue(0, 360, value);
				}
				else
				{
					this.inputR.text = this.ConvertTextFromValue(0, 255, value);
				}
			});
			this.inputR.onEndEdit.AddListener(delegate(string s)
			{
				if (base.isHSV)
				{
					this.sliderR.value = this.ConvertValueFromText(0, 360, s);
				}
				else
				{
					this.sliderR.value = this.ConvertValueFromText(0, 255, s);
				}
			});
			this.sliderG.onValueChanged.AsObservable<float>().Subscribe(delegate(float value)
			{
				if (base.isHSV)
				{
					this.inputG.text = this.ConvertTextFromValue(0, 100, value);
				}
				else
				{
					this.inputG.text = this.ConvertTextFromValue(0, 255, value);
				}
			});
			this.inputG.onEndEdit.AddListener(delegate(string s)
			{
				if (base.isHSV)
				{
					this.sliderG.value = this.ConvertValueFromText(0, 100, s);
				}
				else
				{
					this.sliderG.value = this.ConvertValueFromText(0, 255, s);
				}
			});
			this.sliderB.onValueChanged.AsObservable<float>().Subscribe(delegate(float value)
			{
				if (base.isHSV)
				{
					this.inputB.text = this.ConvertTextFromValue(0, 100, value);
				}
				else
				{
					this.inputB.text = this.ConvertTextFromValue(0, 255, value);
				}
			});
			this.inputB.onEndEdit.AddListener(delegate(string s)
			{
				if (base.isHSV)
				{
					this.sliderB.value = this.ConvertValueFromText(0, 100, s);
				}
				else
				{
					this.sliderB.value = this.ConvertValueFromText(0, 255, s);
				}
			});
			this.sliderA.onValueChanged.AsObservable<float>().Subscribe(delegate(float value)
			{
				if (base.isHSV)
				{
					this.inputA.text = this.ConvertTextFromValue(0, 100, value);
				}
				else
				{
					this.inputA.text = this.ConvertTextFromValue(0, 100, value);
				}
			});
			this.inputA.onEndEdit.AddListener(delegate(string s)
			{
				if (base.isHSV)
				{
					this.sliderA.value = this.ConvertValueFromText(0, 100, s);
				}
				else
				{
					this.sliderA.value = this.ConvertValueFromText(0, 100, s);
				}
			});
			this._isHSV.TakeUntilDestroy(this).Subscribe(delegate(bool isOn)
			{
				if (isOn)
				{
					this.textR.text = "色合い";
					this.textG.text = "鮮やかさ";
					this.textB.text = "明るさ";
				}
				else
				{
					this.textR.text = "赤";
					this.textG.text = "緑";
					this.textB.text = "青";
					this.inputR.text = this.ConvertTextFromValue(0, 255, base.color.r);
				}
			});
		}

		// Token: 0x0400727E RID: 29310
		[Tooltip("RedInputField")]
		[SerializeField]
		private InputField inputR;

		// Token: 0x0400727F RID: 29311
		[Tooltip("GreenInputField")]
		[SerializeField]
		private InputField inputG;

		// Token: 0x04007280 RID: 29312
		[Tooltip("BlueInputField")]
		[SerializeField]
		private InputField inputB;

		// Token: 0x04007281 RID: 29313
		[Tooltip("AlphaInputField")]
		[SerializeField]
		private InputField inputA;

		// Token: 0x04007282 RID: 29314
		[Tooltip("R or H")]
		[SerializeField]
		private Text textR;

		// Token: 0x04007283 RID: 29315
		[Tooltip("G or S")]
		[SerializeField]
		private Text textG;

		// Token: 0x04007284 RID: 29316
		[Tooltip("B or V")]
		[SerializeField]
		private Text textB;
	}
}
