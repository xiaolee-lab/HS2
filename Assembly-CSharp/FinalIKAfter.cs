using System;
using UnityEngine;

// Token: 0x02000AEE RID: 2798
public class FinalIKAfter : MonoBehaviour
{
	// Token: 0x060051BD RID: 20925 RVA: 0x0021B3CD File Offset: 0x002197CD
	private void Start()
	{
		if (this.objUpdateMeta)
		{
			this.updateMeta = this.objUpdateMeta.GetComponent<UpdateMeta>();
		}
	}

	// Token: 0x060051BE RID: 20926 RVA: 0x0021B3F0 File Offset: 0x002197F0
	private void Update()
	{
	}

	// Token: 0x060051BF RID: 20927 RVA: 0x0021B3F2 File Offset: 0x002197F2
	private void LateUpdate()
	{
		if (this.updateMeta != null)
		{
			this.updateMeta.ConstMetaMesh();
		}
	}

	// Token: 0x04004C3B RID: 19515
	public GameObject objUpdateMeta;

	// Token: 0x04004C3C RID: 19516
	[TagSelector]
	public string sss = "body";

	// Token: 0x04004C3D RID: 19517
	private UpdateMeta updateMeta;
}
