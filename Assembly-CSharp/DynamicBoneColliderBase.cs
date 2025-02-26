using System;
using UnityEngine;

// Token: 0x0200030A RID: 778
public class DynamicBoneColliderBase : MonoBehaviour
{
	// Token: 0x06000DA0 RID: 3488 RVA: 0x0003ED0E File Offset: 0x0003D10E
	public virtual void Collide(ref Vector3 particlePosition, float particleRadius)
	{
	}

	// Token: 0x04000D39 RID: 3385
	public DynamicBoneColliderBase.Direction m_Direction = DynamicBoneColliderBase.Direction.Y;

	// Token: 0x04000D3A RID: 3386
	public Vector3 m_Center = Vector3.zero;

	// Token: 0x04000D3B RID: 3387
	public DynamicBoneColliderBase.Bound m_Bound;

	// Token: 0x0200030B RID: 779
	public enum Direction
	{
		// Token: 0x04000D3D RID: 3389
		X,
		// Token: 0x04000D3E RID: 3390
		Y,
		// Token: 0x04000D3F RID: 3391
		Z
	}

	// Token: 0x0200030C RID: 780
	public enum Bound
	{
		// Token: 0x04000D41 RID: 3393
		Outside,
		// Token: 0x04000D42 RID: 3394
		Inside
	}
}
