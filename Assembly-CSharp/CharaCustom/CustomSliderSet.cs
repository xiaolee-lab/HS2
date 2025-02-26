using System;
using AIChara;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009A2 RID: 2466
	public class CustomSliderSet : MonoBehaviour
	{
		// Token: 0x060046FB RID: 18171 RVA: 0x001B5098 File Offset: 0x001B3498
		public void Reset()
		{
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				string name = child.name;
				if (name != null)
				{
					if (!(name == "Text"))
					{
						if (!(name == "Slider"))
						{
							if (!(name == "SldInputField"))
							{
								if (name == "Button")
								{
									this.button = child.GetComponent<Button>();
								}
							}
							else
							{
								this.input = child.GetComponent<InputField>();
							}
						}
						else
						{
							this.slider = child.GetComponent<Slider>();
						}
					}
					else
					{
						this.title = child.GetComponent<Text>();
					}
				}
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060046FC RID: 18172 RVA: 0x001B5164 File Offset: 0x001B3564
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x060046FD RID: 18173 RVA: 0x001B516B File Offset: 0x001B356B
		protected ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x001B5178 File Offset: 0x001B3578
		public void SetSliderValue(float value)
		{
			if (this.slider)
			{
				this.slider.value = value;
			}
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x001B5196 File Offset: 0x001B3596
		public void SetInputTextValue(string value)
		{
			if (this.input)
			{
				this.input.text = value;
			}
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x001B51B4 File Offset: 0x001B35B4
		public void Start()
		{
			this.customBase.lstInputField.Add(this.input);
			if (this.slider)
			{
				this.slider.onValueChanged.AsObservable<float>().Subscribe(delegate(float value)
				{
					if (this.onChange != null)
					{
						this.onChange(value);
					}
					if (this.input)
					{
						this.input.text = CustomBase.ConvertTextFromRate(0, 100, value);
					}
				});
				this.slider.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
				{
					if (this.customBase.sliderControlWheel)
					{
						this.slider.value = Mathf.Clamp(this.slider.value + scl.scrollDelta.y * -0.01f, 0f, 100f);
					}
				});
				this.slider.OnPointerUpAsObservable().Subscribe(delegate(PointerEventData _)
				{
					if (this.onPointerUp != null)
					{
						this.onPointerUp();
					}
				});
			}
			if (this.input)
			{
				this.input.onEndEdit.AsObservable<string>().Subscribe(delegate(string value)
				{
					if (this.slider)
					{
						this.slider.value = CustomBase.ConvertRateFromText(0, 100, value);
					}
				});
			}
			if (this.button)
			{
				this.button.onClick.AsObservable().Subscribe(delegate(Unit _)
				{
					if (this.onSetDefaultValue != null)
					{
						float num = this.onSetDefaultValue();
						if (this.slider)
						{
							if (this.input && this.slider.value != num)
							{
								this.input.text = CustomBase.ConvertTextFromRate(0, 100, num);
							}
							this.slider.value = num;
						}
						if (this.onChange != null)
						{
							this.onChange(num);
						}
						if (this.onEndSetDefaultValue != null)
						{
							this.onEndSetDefaultValue();
						}
					}
				});
			}
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x001B52A7 File Offset: 0x001B36A7
		private void OnDestroy()
		{
			if (Singleton<CustomBase>.IsInstance())
			{
				this.customBase.lstInputField.Remove(this.input);
			}
		}

		// Token: 0x04004207 RID: 16903
		public Text title;

		// Token: 0x04004208 RID: 16904
		public Slider slider;

		// Token: 0x04004209 RID: 16905
		public InputField input;

		// Token: 0x0400420A RID: 16906
		public Button button;

		// Token: 0x0400420B RID: 16907
		public Action<float> onChange;

		// Token: 0x0400420C RID: 16908
		public Action onPointerUp;

		// Token: 0x0400420D RID: 16909
		public Func<float> onSetDefaultValue;

		// Token: 0x0400420E RID: 16910
		public Action onEndSetDefaultValue;
	}
}
