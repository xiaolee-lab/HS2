using System;
using Illusion.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200128F RID: 4751
	public class RoutePointComponent : MonoBehaviour
	{
		// Token: 0x1700219E RID: 8606
		// (set) Token: 0x06009D34 RID: 40244 RVA: 0x00403F86 File Offset: 0x00402386
		public Color color
		{
			set
			{
				this.imageBack.color = value;
			}
		}

		// Token: 0x1700219F RID: 8607
		// (set) Token: 0x06009D35 RID: 40245 RVA: 0x00403F94 File Offset: 0x00402394
		public string textName
		{
			set
			{
				this._textName.text = value;
			}
		}

		// Token: 0x170021A0 RID: 8608
		// (get) Token: 0x06009D36 RID: 40246 RVA: 0x00403FA2 File Offset: 0x004023A2
		// (set) Token: 0x06009D37 RID: 40247 RVA: 0x00403FB9 File Offset: 0x004023B9
		public bool visible
		{
			get
			{
				return this.canvasGroup.alpha != 0f;
			}
			set
			{
				this.canvasGroup.Enable(value, false);
			}
		}

		// Token: 0x170021A1 RID: 8609
		// (get) Token: 0x06009D38 RID: 40248 RVA: 0x00403FC8 File Offset: 0x004023C8
		public GameObject objAid
		{
			get
			{
				return this._objAid;
			}
		}

		// Token: 0x04007D13 RID: 32019
		[SerializeField]
		private Image imageBack;

		// Token: 0x04007D14 RID: 32020
		[SerializeField]
		private TextMeshProUGUI _textName;

		// Token: 0x04007D15 RID: 32021
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04007D16 RID: 32022
		[Space]
		[SerializeField]
		private GameObject _objAid;
	}
}
