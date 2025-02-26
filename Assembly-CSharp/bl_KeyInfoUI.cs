using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000625 RID: 1573
public class bl_KeyInfoUI : MonoBehaviour
{
	// Token: 0x06002582 RID: 9602 RVA: 0x000D6BC1 File Offset: 0x000D4FC1
	public void Init(bl_KeyInfo info, bl_KeyOptionsUI koui)
	{
		this.cacheInfo = info;
		this.KeyOptions = koui;
		this.FunctionText.text = info.Description;
		this.KeyText.text = info.Key.ToString();
	}

	// Token: 0x06002583 RID: 9603 RVA: 0x000D6BFE File Offset: 0x000D4FFE
	public void SetKeyChange()
	{
		this.KeyOptions.SetWaitKeyProcess(this.cacheInfo);
	}

	// Token: 0x04002552 RID: 9554
	[SerializeField]
	private Text FunctionText;

	// Token: 0x04002553 RID: 9555
	[SerializeField]
	private Text KeyText;

	// Token: 0x04002554 RID: 9556
	private bl_KeyInfo cacheInfo;

	// Token: 0x04002555 RID: 9557
	private bl_KeyOptionsUI KeyOptions;
}
