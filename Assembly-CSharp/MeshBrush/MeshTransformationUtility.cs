using System;
using UnityEngine;

namespace MeshBrush
{
	// Token: 0x020003F5 RID: 1013
	public static class MeshTransformationUtility
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x00070554 File Offset: 0x0006E954
		public static void ApplyRandomScale(Transform targetTransform, Vector2 range)
		{
			float num = Mathf.Abs(UnityEngine.Random.Range(range.x, range.y));
			targetTransform.localScale = new Vector3(num, num, num);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00070588 File Offset: 0x0006E988
		public static void ApplyRandomScale(Transform targetTransform, Vector4 scaleRanges)
		{
			float f = UnityEngine.Random.Range(scaleRanges.x, scaleRanges.y);
			float f2 = UnityEngine.Random.Range(scaleRanges.z, scaleRanges.w);
			targetTransform.localScale = new Vector3
			{
				x = Mathf.Abs(f),
				y = Mathf.Abs(f2),
				z = Mathf.Abs(f)
			};
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000705F4 File Offset: 0x0006E9F4
		public static void ApplyRandomScale(Transform targetTransform, Vector2 rangeX, Vector2 rangeY, Vector2 rangeZ)
		{
			targetTransform.localScale = new Vector3
			{
				x = Mathf.Abs(UnityEngine.Random.Range(rangeX.x, rangeX.y)),
				y = Mathf.Abs(UnityEngine.Random.Range(rangeY.x, rangeY.y)),
				z = Mathf.Abs(UnityEngine.Random.Range(rangeZ.x, rangeZ.y))
			};
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00070670 File Offset: 0x0006EA70
		public static void AddConstantScale(Transform targetTransform, Vector2 range)
		{
			float num = UnityEngine.Random.Range(range.x, range.y);
			Vector3 localScale = targetTransform.localScale + new Vector3(num, num, num);
			localScale.x = Mathf.Abs(localScale.x);
			localScale.y = Mathf.Abs(localScale.y);
			localScale.z = Mathf.Abs(localScale.z);
			targetTransform.localScale = localScale;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x000706E8 File Offset: 0x0006EAE8
		public static void AddConstantScale(Transform targetTransform, float x, float y, float z)
		{
			Vector3 localScale = targetTransform.localScale + new Vector3
			{
				x = Mathf.Abs(x),
				y = Mathf.Abs(y),
				z = Mathf.Abs(z)
			};
			targetTransform.localScale = localScale;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00070738 File Offset: 0x0006EB38
		public static void ApplyRandomRotation(Transform targetTransform, float randomRotationIntensityPercentage)
		{
			float y = UnityEngine.Random.Range(0f, 3.6f * randomRotationIntensityPercentage);
			targetTransform.Rotate(new Vector3(0f, y, 0f));
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0007076D File Offset: 0x0006EB6D
		public static void ApplyMeshOffset(Transform targetTransform, float offset, Vector3 direction)
		{
			targetTransform.Translate(direction.normalized * offset * 0.01f, Space.World);
		}
	}
}
