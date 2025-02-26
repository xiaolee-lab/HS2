using System;
using Illusion.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001336 RID: 4918
	public class PatternSelectInfoComponent : MonoBehaviour
	{
		// Token: 0x0600A49E RID: 42142 RVA: 0x00432875 File Offset: 0x00430C75
		public void Disable(bool disable)
		{
			if (this.tgl)
			{
				this.tgl.interactable = !disable;
			}
		}

		// Token: 0x0600A49F RID: 42143 RVA: 0x00432896 File Offset: 0x00430C96
		public void Disvisible(bool disvisible)
		{
			base.gameObject.SetActiveIfDifferent(!disvisible);
		}

		// Token: 0x040081A6 RID: 33190
		public PatternSelectInfo info;

		// Token: 0x040081A7 RID: 33191
		public Image img;

		// Token: 0x040081A8 RID: 33192
		public Toggle tgl;
	}
}
