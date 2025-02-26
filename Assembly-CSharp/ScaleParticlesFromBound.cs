using System;
using UnityEngine;

// Token: 0x02000447 RID: 1095
public class ScaleParticlesFromBound : MonoBehaviour
{
	// Token: 0x06001414 RID: 5140 RVA: 0x0007DC9C File Offset: 0x0007C09C
	private void GetMeshFilterParent(Transform t)
	{
		Collider component = t.parent.GetComponent<Collider>();
		if (component == null)
		{
			this.GetMeshFilterParent(t.parent);
		}
		else
		{
			this.targetCollider = component;
		}
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x0007DCDC File Offset: 0x0007C0DC
	private void Start()
	{
		this.GetMeshFilterParent(base.transform);
		if (this.targetCollider == null)
		{
			return;
		}
		Vector3 size = this.targetCollider.bounds.size;
		base.transform.localScale = size;
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x0007DD27 File Offset: 0x0007C127
	private void Update()
	{
	}

	// Token: 0x040016D1 RID: 5841
	private Collider targetCollider;
}
