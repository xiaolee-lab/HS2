using System;
using UnityEngine;

// Token: 0x02000622 RID: 1570
public class bl_SelectableTextManager : MonoBehaviour
{
	// Token: 0x0600257B RID: 9595 RVA: 0x000D6AED File Offset: 0x000D4EED
	private void Awake()
	{
		this.AllSelectables = base.GetComponentsInChildren<bl_SelectableText>();
	}

	// Token: 0x0600257C RID: 9596 RVA: 0x000D6AFC File Offset: 0x000D4EFC
	public void OnEnter()
	{
		if (this.AllSelectables.Length > 0)
		{
			foreach (bl_SelectableText bl_SelectableText in this.AllSelectables)
			{
				bl_SelectableText.OnEnter();
			}
		}
	}

	// Token: 0x0600257D RID: 9597 RVA: 0x000D6B3C File Offset: 0x000D4F3C
	public void OnExit()
	{
		if (this.AllSelectables.Length > 0)
		{
			foreach (bl_SelectableText bl_SelectableText in this.AllSelectables)
			{
				bl_SelectableText.OnExit();
			}
		}
	}

	// Token: 0x0400253B RID: 9531
	private bl_SelectableText[] AllSelectables;
}
