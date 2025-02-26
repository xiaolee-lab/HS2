using System;
using UnityEngine;

// Token: 0x0200061F RID: 1567
[RequireComponent(typeof(CanvasGroup))]
public class bl_BrightnessImage : MonoBehaviour
{
	// Token: 0x06002568 RID: 9576 RVA: 0x000D660C File Offset: 0x000D4A0C
	private void Start()
	{
		base.transform.SetAsLastSibling();
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x000D6619 File Offset: 0x000D4A19
	public void SetValue(float val)
	{
		this.Value = val;
		this.Alpha.alpha = 1f - this.Value;
	}

	// Token: 0x1700057F RID: 1407
	// (get) Token: 0x0600256A RID: 9578 RVA: 0x000D6639 File Offset: 0x000D4A39
	private CanvasGroup Alpha
	{
		get
		{
			if (this._Alpha == null)
			{
				this._Alpha = base.GetComponent<CanvasGroup>();
			}
			return this._Alpha;
		}
	}

	// Token: 0x04002529 RID: 9513
	private float Value = 1f;

	// Token: 0x0400252A RID: 9514
	private CanvasGroup _Alpha;
}
