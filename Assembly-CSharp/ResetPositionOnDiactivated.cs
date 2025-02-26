using System;
using UnityEngine;

// Token: 0x0200044F RID: 1103
public class ResetPositionOnDiactivated : MonoBehaviour
{
	// Token: 0x06001436 RID: 5174 RVA: 0x0007E711 File Offset: 0x0007CB11
	private void Start()
	{
		this.EffectSettings.EffectDeactivated += this.EffectSettings_EffectDeactivated;
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x0007E72A File Offset: 0x0007CB2A
	private void EffectSettings_EffectDeactivated(object sender, EventArgs e)
	{
		base.transform.localPosition = Vector3.zero;
	}

	// Token: 0x040016F7 RID: 5879
	public EffectSettings EffectSettings;
}
