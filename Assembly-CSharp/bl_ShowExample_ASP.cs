using System;
using UnityEngine;

// Token: 0x02000626 RID: 1574
public class bl_ShowExample_ASP : MonoBehaviour
{
	// Token: 0x06002585 RID: 9605 RVA: 0x000D6C19 File Offset: 0x000D5019
	private void Awake()
	{
		this.AllSettings = UnityEngine.Object.FindObjectOfType<bl_AllOptionsPro>();
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x000D6C26 File Offset: 0x000D5026
	private void Update()
	{
		if (bl_Input.GetKeyDown("Pause"))
		{
			this.AllSettings.ShowMenu();
		}
	}

	// Token: 0x04002556 RID: 9558
	private bl_AllOptionsPro AllSettings;
}
