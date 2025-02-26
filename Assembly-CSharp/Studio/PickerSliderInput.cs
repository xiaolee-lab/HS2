using System;
using Illusion.Component.UI.ColorPicker;
using TMPro;
using UniRx;
using UnityEngine;

namespace Studio
{
	// Token: 0x020012D4 RID: 4820
	public class PickerSliderInput : PickerSlider
	{
		// Token: 0x0600A0C2 RID: 41154 RVA: 0x00420BD0 File Offset: 0x0041EFD0
		public string ConvertTextFromValue(int min, int max, float value)
		{
			return ((int)Mathf.Lerp((float)min, (float)max, value)).ToString();
		}

		// Token: 0x0600A0C3 RID: 41155 RVA: 0x00420BF8 File Offset: 0x0041EFF8
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

		// Token: 0x0600A0C4 RID: 41156 RVA: 0x00420C34 File Offset: 0x0041F034
		public void SetInputText()
		{
			float[] array = new float[]
			{
				this.sliderR.value,
				this.sliderG.value,
				this.sliderB.value,
				this.sliderA.value
			};
			int num = 0;
			if (base.isHSV)
			{
				this.inputR.text = this.ConvertTextFromValue(0, 360, array[num++]);
				this.inputG.text = this.ConvertTextFromValue(0, 100, array[num++]);
				this.inputB.text = this.ConvertTextFromValue(0, 100, array[num++]);
				this.inputA.text = this.ConvertTextFromValue(0, 100, array[num++]);
			}
			else
			{
				this.inputR.text = this.ConvertTextFromValue(0, 255, array[num++]);
				this.inputG.text = this.ConvertTextFromValue(0, 255, array[num++]);
				this.inputB.text = this.ConvertTextFromValue(0, 255, array[num++]);
				this.inputA.text = this.ConvertTextFromValue(0, 100, array[num++]);
			}
		}

		// Token: 0x0600A0C5 RID: 41157 RVA: 0x00420D78 File Offset: 0x0041F178
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
				float[] array = new float[]
				{
					this.sliderR.value,
					this.sliderG.value,
					this.sliderB.value
				};
				int num = 0;
				if (isOn)
				{
					this.textR.text = "色合い";
					this.textG.text = "鮮やかさ";
					this.textB.text = "明るさ";
					this.inputR.text = this.ConvertTextFromValue(0, 360, array[num++]);
					this.inputG.text = this.ConvertTextFromValue(0, 100, array[num++]);
					this.inputB.text = this.ConvertTextFromValue(0, 100, array[num++]);
				}
				else
				{
					this.textR.text = "赤";
					this.textG.text = "緑";
					this.textB.text = "青";
					this.inputR.text = this.ConvertTextFromValue(0, 255, array[num++]);
					this.inputG.text = this.ConvertTextFromValue(0, 255, array[num++]);
					this.inputB.text = this.ConvertTextFromValue(0, 255, array[num++]);
				}
			});
		}

		// Token: 0x04007F01 RID: 32513
		[Tooltip("RedInputField")]
		[SerializeField]
		private TMP_InputField inputR;

		// Token: 0x04007F02 RID: 32514
		[Tooltip("GreenInputField")]
		[SerializeField]
		private TMP_InputField inputG;

		// Token: 0x04007F03 RID: 32515
		[Tooltip("BlueInputField")]
		[SerializeField]
		private TMP_InputField inputB;

		// Token: 0x04007F04 RID: 32516
		[Tooltip("AlphaInputField")]
		[SerializeField]
		private TMP_InputField inputA;

		// Token: 0x04007F05 RID: 32517
		[Tooltip("R or H")]
		[SerializeField]
		private TextMeshProUGUI textR;

		// Token: 0x04007F06 RID: 32518
		[Tooltip("G or S")]
		[SerializeField]
		private TextMeshProUGUI textG;

		// Token: 0x04007F07 RID: 32519
		[Tooltip("B or V")]
		[SerializeField]
		private TextMeshProUGUI textB;
	}
}
