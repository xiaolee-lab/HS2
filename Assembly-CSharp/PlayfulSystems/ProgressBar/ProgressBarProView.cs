using System;
using UnityEngine;

namespace PlayfulSystems.ProgressBar
{
	// Token: 0x0200064D RID: 1613
	public abstract class ProgressBarProView : MonoBehaviour
	{
		// Token: 0x06002642 RID: 9794 RVA: 0x000D89A5 File Offset: 0x000D6DA5
		public virtual void NewChangeStarted(float currentValue, float targetValue)
		{
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000D89A7 File Offset: 0x000D6DA7
		public virtual void SetBarColor(Color color)
		{
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x000D89A9 File Offset: 0x000D6DA9
		public virtual bool CanUpdateView(float currentValue, float targetValue)
		{
			return base.isActiveAndEnabled;
		}

		// Token: 0x06002645 RID: 9797
		public abstract void UpdateView(float currentValue, float targetValue);
	}
}
