using System;
using UnityEngine;

// Token: 0x02000A5E RID: 2654
[RequireComponent(typeof(Camera))]
public class FarCamera : MonoBehaviour
{
	// Token: 0x06004EB3 RID: 20147 RVA: 0x001E2F53 File Offset: 0x001E1353
	private void Start()
	{
		this._relativePosition = base.transform.position - this.target.transform.position;
	}

	// Token: 0x06004EB4 RID: 20148 RVA: 0x001E2F7B File Offset: 0x001E137B
	private void FixedUpdate()
	{
		base.transform.position = this.target.transform.position + this._relativePosition;
	}

	// Token: 0x040047A0 RID: 18336
	public GameObject target;

	// Token: 0x040047A1 RID: 18337
	private Vector3 _relativePosition;
}
