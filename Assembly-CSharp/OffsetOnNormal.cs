using System;
using UnityEngine;

// Token: 0x0200044D RID: 1101
public class OffsetOnNormal : MonoBehaviour
{
	// Token: 0x0600142E RID: 5166 RVA: 0x0007E5F1 File Offset: 0x0007C9F1
	private void Awake()
	{
		this.startPosition = base.transform.position;
	}

	// Token: 0x0600142F RID: 5167 RVA: 0x0007E604 File Offset: 0x0007CA04
	private void OnEnable()
	{
		RaycastHit raycastHit;
		Physics.Raycast(this.startPosition, Vector3.down, out raycastHit);
		if (this.offsetGameObject != null)
		{
			base.transform.position = this.offsetGameObject.transform.position + raycastHit.normal * this.offset;
		}
		else
		{
			base.transform.position = raycastHit.point + raycastHit.normal * this.offset;
		}
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x0007E695 File Offset: 0x0007CA95
	private void Update()
	{
	}

	// Token: 0x040016F3 RID: 5875
	public float offset = 1f;

	// Token: 0x040016F4 RID: 5876
	public GameObject offsetGameObject;

	// Token: 0x040016F5 RID: 5877
	private Vector3 startPosition;
}
