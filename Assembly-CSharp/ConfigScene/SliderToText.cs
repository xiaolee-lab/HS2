using System;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x02000867 RID: 2151
	public class SliderToText : MonoBehaviour
	{
		// Token: 0x060036DC RID: 14044 RVA: 0x00145940 File Offset: 0x00143D40
		public void Start()
		{
			this.OnValueChanged();
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x00145948 File Offset: 0x00143D48
		public void OnValueChanged()
		{
			if (this.silder == null || this.text == null)
			{
				return;
			}
			this.text.text = this.silder.value.ToString("0");
		}

		// Token: 0x04003778 RID: 14200
		[SerializeField]
		private Slider silder;

		// Token: 0x04003779 RID: 14201
		[SerializeField]
		private Text text;
	}
}
