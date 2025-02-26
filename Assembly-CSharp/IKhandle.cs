using System;
using UnityEngine;

// Token: 0x02000B28 RID: 2856
public class IKhandle : MonoBehaviour
{
	// Token: 0x060053C4 RID: 21444 RVA: 0x0024EA47 File Offset: 0x0024CE47
	public void Init()
	{
		base.transform.position = this.target.position;
		base.transform.rotation = this.target.rotation;
	}

	// Token: 0x060053C5 RID: 21445 RVA: 0x0024EA75 File Offset: 0x0024CE75
	public void BaseReset()
	{
		this.target = null;
	}

	// Token: 0x04004EBE RID: 20158
	public Transform target;
}
