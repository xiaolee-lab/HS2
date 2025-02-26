using System;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001362 RID: 4962
	public class VoiceEndChecker : MonoBehaviour
	{
		// Token: 0x0600A657 RID: 42583 RVA: 0x0043A2CE File Offset: 0x004386CE
		private void OnDestroy()
		{
			if (Scene.isGameEnd)
			{
				return;
			}
			if (this.onEndFunc != null)
			{
				this.onEndFunc();
			}
		}

		// Token: 0x040082AC RID: 33452
		public VoiceEndChecker.OnEndFunc onEndFunc;

		// Token: 0x02001363 RID: 4963
		// (Invoke) Token: 0x0600A659 RID: 42585
		public delegate void OnEndFunc();
	}
}
