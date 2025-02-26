using System;
using UnityEngine;

// Token: 0x02000AF5 RID: 2805
public class MetaballJoint : MonoBehaviour
{
	// Token: 0x060051CF RID: 20943 RVA: 0x0021C866 File Offset: 0x0021AC66
	private void Start()
	{
		if (this.transformJoint)
		{
			this.length = Vector3.Distance(this.transformJoint.position, base.transform.position);
		}
	}

	// Token: 0x060051D0 RID: 20944 RVA: 0x0021C89C File Offset: 0x0021AC9C
	private void Update()
	{
		Vector3 a = this.transformJoint.position - base.transform.position;
		float magnitude = a.magnitude;
		float num = magnitude - this.length;
		if (num > 0f)
		{
			base.transform.Translate(a * ((num - this.limitLength) / magnitude), Space.World);
		}
		else if (num < 0f)
		{
			base.transform.Translate(a * ((num + this.limitLength) / magnitude), Space.World);
		}
	}

	// Token: 0x04004C57 RID: 19543
	public Transform transformJoint;

	// Token: 0x04004C58 RID: 19544
	public float limitLength;

	// Token: 0x04004C59 RID: 19545
	[SerializeField]
	[Tooltip("確認用表示")]
	private float length;
}
