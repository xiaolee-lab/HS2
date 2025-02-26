using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E43 RID: 3651
	public class MigrateMapSelectNodeUI : MonoBehaviour
	{
		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06007290 RID: 29328 RVA: 0x0030D2A7 File Offset: 0x0030B6A7
		public Button Button
		{
			[CompilerGenerated]
			get
			{
				return this._button;
			}
		}

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06007291 RID: 29329 RVA: 0x0030D2AF File Offset: 0x0030B6AF
		public Text Text
		{
			[CompilerGenerated]
			get
			{
				return this._text;
			}
		}

		// Token: 0x06007292 RID: 29330 RVA: 0x0030D2B7 File Offset: 0x0030B6B7
		private void Reset()
		{
			this._button = base.GetComponent<Button>();
			this._text = base.GetComponentInChildren<Text>();
		}

		// Token: 0x04005DCF RID: 24015
		[SerializeField]
		private Button _button;

		// Token: 0x04005DD0 RID: 24016
		[SerializeField]
		private Text _text;
	}
}
