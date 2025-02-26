using System;
using UnityEngine;

// Token: 0x020010E1 RID: 4321
public class EyeLookController : MonoBehaviour
{
	// Token: 0x06008F9F RID: 36767 RVA: 0x003BC74B File Offset: 0x003BAB4B
	private void Start()
	{
		if (!this.target && Camera.main)
		{
			this.target = Camera.main.transform;
		}
	}

	// Token: 0x06008FA0 RID: 36768 RVA: 0x003BC77C File Offset: 0x003BAB7C
	private void LateUpdate()
	{
		if (this.target != null && null != this.eyeLookScript)
		{
			this.eyeLookScript.EyeUpdateCalc(this.target.position, this.ptnNo);
		}
	}

	// Token: 0x06008FA1 RID: 36769 RVA: 0x003BC7BC File Offset: 0x003BABBC
	public void ForceLateUpdate()
	{
		this.LateUpdate();
	}

	// Token: 0x0400742B RID: 29739
	public EyeLookCalc eyeLookScript;

	// Token: 0x0400742C RID: 29740
	public int ptnNo;

	// Token: 0x0400742D RID: 29741
	public Transform target;
}
