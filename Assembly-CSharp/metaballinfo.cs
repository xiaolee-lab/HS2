using System;
using UnityEngine;

// Token: 0x02000AF4 RID: 2804
public class metaballinfo : MonoBehaviour
{
	// Token: 0x060051CC RID: 20940 RVA: 0x0021C831 File Offset: 0x0021AC31
	private void Reset()
	{
		this.aRigid = base.GetComponentsInChildren<Rigidbody>(true);
		if (this.aRigid.Length > 0)
		{
			this.rigidBeginning = this.aRigid[0];
		}
	}

	// Token: 0x060051CD RID: 20941 RVA: 0x0021C85C File Offset: 0x0021AC5C
	private void Update()
	{
	}

	// Token: 0x04004C55 RID: 19541
	public Rigidbody[] aRigid;

	// Token: 0x04004C56 RID: 19542
	public Rigidbody rigidBeginning;
}
