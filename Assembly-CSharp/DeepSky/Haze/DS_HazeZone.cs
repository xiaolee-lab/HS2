using System;
using UnityEngine;

namespace DeepSky.Haze
{
	// Token: 0x020002F6 RID: 758
	[ExecuteInEditMode]
	[AddComponentMenu("DeepSky Haze/Zone", 52)]
	public class DS_HazeZone : MonoBehaviour
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0003733D File Offset: 0x0003573D
		public DS_HazeContext Context
		{
			get
			{
				return this.m_Context;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00037345 File Offset: 0x00035745
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x0003734D File Offset: 0x0003574D
		public int Priority
		{
			get
			{
				return this.m_Priority;
			}
			set
			{
				this.m_Priority = ((value <= 0) ? 0 : value);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00037363 File Offset: 0x00035763
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x0003736B File Offset: 0x0003576B
		public float BlendRange
		{
			get
			{
				return this.m_BlendRange;
			}
			set
			{
				this.m_BlendRange = Mathf.Clamp01(value);
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0003737C File Offset: 0x0003577C
		private void Setup()
		{
			this.m_AABB = new Bounds(Vector3.zero, base.transform.localScale);
			this.m_BlendRangeInverse = 1f / Mathf.Max(Mathf.Min(new float[]
			{
				this.m_AABB.extents.x,
				this.m_AABB.extents.y,
				this.m_AABB.extents.z
			}) * this.m_BlendRange, Mathf.Epsilon);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0003740E File Offset: 0x0003580E
		private void Start()
		{
			this.Setup();
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00037416 File Offset: 0x00035816
		private void OnValidate()
		{
			this.Setup();
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00037420 File Offset: 0x00035820
		public bool Contains(Vector3 position)
		{
			if (base.transform.hasChanged)
			{
				this.Setup();
			}
			Vector3 point = base.transform.InverseTransformPoint(position);
			point.Scale(base.transform.localScale);
			return this.m_AABB.Contains(point);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00037470 File Offset: 0x00035870
		public float GetBlendWeight(Vector3 position)
		{
			Vector3 vector = base.transform.InverseTransformPoint(position);
			vector.Scale(base.transform.localScale);
			float num = Mathf.Abs(this.m_AABB.extents.x - Mathf.Abs(vector.x));
			float num2 = Mathf.Abs(this.m_AABB.extents.y - Mathf.Abs(vector.y));
			float num3 = Mathf.Abs(this.m_AABB.extents.z - Mathf.Abs(vector.z));
			float num4 = Mathf.Min(new float[]
			{
				num,
				num2,
				num3
			});
			return Mathf.Clamp01(num4 * this.m_BlendRangeInverse);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0003753C File Offset: 0x0003593C
		public static bool operator >(DS_HazeZone c1, DS_HazeZone c2)
		{
			if (c1.m_Priority == c2.m_Priority)
			{
				return Vector3.Dot(c1.m_AABB.extents, c1.m_AABB.extents) > Vector3.Dot(c2.m_AABB.extents, c2.m_AABB.extents);
			}
			return c1.m_Priority > c2.m_Priority;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000375B8 File Offset: 0x000359B8
		public static bool operator <(DS_HazeZone c1, DS_HazeZone c2)
		{
			if (c1.m_Priority == c2.m_Priority)
			{
				return Vector3.Dot(c1.m_AABB.extents, c1.m_AABB.extents) < Vector3.Dot(c2.m_AABB.extents, c2.m_AABB.extents);
			}
			return c1.m_Priority < c2.m_Priority;
		}

		// Token: 0x04000C43 RID: 3139
		[SerializeField]
		private DS_HazeContext m_Context = new DS_HazeContext();

		// Token: 0x04000C44 RID: 3140
		[SerializeField]
		[Range(0f, 250f)]
		private int m_Priority;

		// Token: 0x04000C45 RID: 3141
		[SerializeField]
		[Range(0.001f, 1f)]
		private float m_BlendRange = 0.1f;

		// Token: 0x04000C46 RID: 3142
		private Bounds m_AABB;

		// Token: 0x04000C47 RID: 3143
		private float m_BlendRangeInverse;
	}
}
