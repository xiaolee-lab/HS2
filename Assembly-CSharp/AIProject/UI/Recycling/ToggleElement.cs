using System;
using System.Runtime.CompilerServices;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI.Recycling
{
	// Token: 0x02000EAA RID: 3754
	[Serializable]
	public class ToggleElement
	{
		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x06007A58 RID: 31320 RVA: 0x00337393 File Offset: 0x00335793
		public Image Cursor
		{
			[CompilerGenerated]
			get
			{
				return this._cursor;
			}
		}

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x06007A59 RID: 31321 RVA: 0x0033739B File Offset: 0x0033579B
		public Toggle Toggle
		{
			[CompilerGenerated]
			get
			{
				return this._toggle;
			}
		}

		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x06007A5A RID: 31322 RVA: 0x003373A3 File Offset: 0x003357A3
		// (set) Token: 0x06007A5B RID: 31323 RVA: 0x003373AB File Offset: 0x003357AB
		public int Index { get; set; } = -1;

		// Token: 0x06007A5C RID: 31324 RVA: 0x003373B4 File Offset: 0x003357B4
		public void Start()
		{
			this.Refresh();
			if (this._toggle == null || this._cursor == null)
			{
				return;
			}
			(from _ in this._toggle.OnPointerEnterAsObservable()
			where this._cursor != null
			select _).Subscribe(delegate(PointerEventData _)
			{
				this._cursor.enabled = true;
			}).AddTo(this._toggle);
			(from _ in this._toggle.OnPointerExitAsObservable()
			where this._cursor != null
			select _).Subscribe(delegate(PointerEventData _)
			{
				this._cursor.enabled = false;
			}).AddTo(this._toggle);
		}

		// Token: 0x06007A5D RID: 31325 RVA: 0x0033745C File Offset: 0x0033585C
		public void Refresh()
		{
			if (this._cursor != null)
			{
				this._cursor.enabled = false;
			}
		}

		// Token: 0x04006257 RID: 25175
		[SerializeField]
		private Image _cursor;

		// Token: 0x04006258 RID: 25176
		[SerializeField]
		private Toggle _toggle;
	}
}
