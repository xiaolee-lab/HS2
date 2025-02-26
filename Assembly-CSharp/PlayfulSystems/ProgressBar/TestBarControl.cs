using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayfulSystems.ProgressBar
{
	// Token: 0x0200063E RID: 1598
	public class TestBarControl : MonoBehaviour
	{
		// Token: 0x060025F7 RID: 9719 RVA: 0x000D84A0 File Offset: 0x000D68A0
		private void Start()
		{
			if (this.barParent != null)
			{
				this.bars = this.barParent.GetComponentsInChildren<ProgressBarPro>(true);
			}
			if (this.sizeButtonParent != null)
			{
				this.buttons = this.sizeButtonParent.GetComponentsInChildren<Button>();
				this.slider = base.GetComponentInChildren<Slider>();
				this.SetupButtons();
			}
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000D8504 File Offset: 0x000D6904
		private void SetupButtons()
		{
			for (int i = 0; i < this.buttons.Length; i++)
			{
				float currentValue = (float)i / (float)(this.buttons.Length - 1);
				Button button = this.buttons[i];
				button.name = "Button_" + currentValue;
				Text componentInChildren = button.GetComponentInChildren<Text>();
				componentInChildren.text = currentValue.ToString();
				button.onClick.AddListener(delegate()
				{
					this.SetSlider(currentValue);
				});
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000D85A4 File Offset: 0x000D69A4
		private void SetSlider(float value)
		{
			if (this.slider != null)
			{
				this.slider.value = value;
			}
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000D85C4 File Offset: 0x000D69C4
		public void SetBars(float value)
		{
			if (this.bars != null)
			{
				for (int i = 0; i < this.bars.Length; i++)
				{
					this.bars[i].SetValue(value, false);
				}
			}
			if (this.barSelectors != null)
			{
				for (int j = 0; j < this.barSelectors.Length; j++)
				{
					this.barSelectors[j].SetValue(value);
				}
			}
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x000D8636 File Offset: 0x000D6A36
		public void SetRandomColor()
		{
			this.SetColor(new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value));
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x000D8654 File Offset: 0x000D6A54
		public void SetColor(Color color)
		{
			if (this.bars != null)
			{
				for (int i = 0; i < this.bars.Length; i++)
				{
					this.bars[i].SetBarColor(color);
				}
			}
			if (this.barSelectors != null)
			{
				for (int j = 0; j < this.barSelectors.Length; j++)
				{
					this.barSelectors[j].SetBarColor(color);
				}
			}
		}

		// Token: 0x040025B0 RID: 9648
		[SerializeField]
		private Transform barParent;

		// Token: 0x040025B1 RID: 9649
		[SerializeField]
		private Transform sizeButtonParent;

		// Token: 0x040025B2 RID: 9650
		[SerializeField]
		private TestSwitchBar[] barSelectors;

		// Token: 0x040025B3 RID: 9651
		private ProgressBarPro[] bars;

		// Token: 0x040025B4 RID: 9652
		private Button[] buttons;

		// Token: 0x040025B5 RID: 9653
		private Slider slider;
	}
}
