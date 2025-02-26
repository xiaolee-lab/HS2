using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000E74 RID: 3700
	public class FadeItem : MonoBehaviour
	{
		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x060075C0 RID: 30144 RVA: 0x0031E454 File Offset: 0x0031C854
		// (set) Token: 0x060075C1 RID: 30145 RVA: 0x0031E45C File Offset: 0x0031C85C
		public CanvasGroup CanvasGroup
		{
			get
			{
				return this._canvasGroup;
			}
			set
			{
				this._canvasGroup = value;
			}
		}

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x060075C2 RID: 30146 RVA: 0x0031E465 File Offset: 0x0031C865
		public Graphic Graphic
		{
			[CompilerGenerated]
			get
			{
				return this._graphic;
			}
		}

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x060075C3 RID: 30147 RVA: 0x0031E46D File Offset: 0x0031C86D
		public Text UIText
		{
			[CompilerGenerated]
			get
			{
				return this._uiText;
			}
		}

		// Token: 0x04005FF6 RID: 24566
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005FF7 RID: 24567
		[SerializeField]
		private Graphic _graphic;

		// Token: 0x04005FF8 RID: 24568
		[SerializeField]
		private Text _uiText;
	}
}
