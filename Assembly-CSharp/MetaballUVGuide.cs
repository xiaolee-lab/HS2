using System;
using UnityEngine;

// Token: 0x02000A47 RID: 2631
public class MetaballUVGuide : MonoBehaviour
{
	// Token: 0x06004E32 RID: 20018 RVA: 0x001DEE04 File Offset: 0x001DD204
	private void Start()
	{
	}

	// Token: 0x06004E33 RID: 20019 RVA: 0x001DEE06 File Offset: 0x001DD206
	private void Update()
	{
	}

	// Token: 0x06004E34 RID: 20020 RVA: 0x001DEE08 File Offset: 0x001DD208
	private void OnDrawGizmosSelected()
	{
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(new Vector3(this.uScale * 0.5f, this.vScale * 0.5f, 0f), new Vector3(this.uScale, this.vScale, 15f));
		Gizmos.matrix = matrix;
	}

	// Token: 0x04004764 RID: 18276
	public float uScale = 1f;

	// Token: 0x04004765 RID: 18277
	public float vScale = 1f;
}
