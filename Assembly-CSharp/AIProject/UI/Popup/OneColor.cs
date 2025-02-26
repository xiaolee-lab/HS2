using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject.UI.Popup
{
	// Token: 0x02000FD4 RID: 4052
	[Serializable]
	public struct OneColor
	{
		// Token: 0x060086D7 RID: 34519 RVA: 0x00384A70 File Offset: 0x00382E70
		public OneColor(float _r, float _g, float _b, float _a = 255f)
		{
			this.color = new Color(_r / 255f, _g / 255f, _b / 255f, _a / 255f);
			this.colorCode = string.Empty;
			this.Init = false;
			this.Apply();
		}

		// Token: 0x060086D8 RID: 34520 RVA: 0x00384ABD File Offset: 0x00382EBD
		public OneColor(Color _color)
		{
			this.color = _color;
			this.colorCode = string.Empty;
			this.Init = false;
			this.Apply();
		}

		// Token: 0x17001D69 RID: 7529
		// (get) Token: 0x060086D9 RID: 34521 RVA: 0x00384ADE File Offset: 0x00382EDE
		// (set) Token: 0x060086DA RID: 34522 RVA: 0x00384AE6 File Offset: 0x00382EE6
		public bool Init { get; private set; }

		// Token: 0x060086DB RID: 34523 RVA: 0x00384AEF File Offset: 0x00382EEF
		public static implicit operator string(OneColor c)
		{
			if (!c.Init)
			{
				c.Apply();
			}
			return c.colorCode;
		}

		// Token: 0x060086DC RID: 34524 RVA: 0x00384B0B File Offset: 0x00382F0B
		public static implicit operator Color(OneColor c)
		{
			return c.color;
		}

		// Token: 0x060086DD RID: 34525 RVA: 0x00384B14 File Offset: 0x00382F14
		public void Apply()
		{
			this.Init = true;
			this.colorCode = "#" + ColorUtility.ToHtmlStringRGBA(this.color);
		}

		// Token: 0x060086DE RID: 34526 RVA: 0x00384B38 File Offset: 0x00382F38
		public override string ToString()
		{
			return this.colorCode;
		}

		// Token: 0x04006D9A RID: 28058
		[SerializeField]
		private Color color;

		// Token: 0x04006D9B RID: 28059
		[SerializeField]
		[ReadOnly]
		private string colorCode;
	}
}
