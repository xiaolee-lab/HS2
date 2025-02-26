using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003AA RID: 938
	public class Plane
	{
		// Token: 0x0600108F RID: 4239 RVA: 0x0005FA94 File Offset: 0x0005DE94
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			this.Normal = Vector3.Cross(b - a, c - a).normalized;
			this.Distance = Vector3.Dot(this.Normal, a);
			this.Pnt = a;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0005FAE1 File Offset: 0x0005DEE1
		public Plane(Vector3 normal, Vector3 p)
		{
			this.Set(normal, p);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0005FAF1 File Offset: 0x0005DEF1
		public Plane(Plane instance)
		{
			this.Normal = instance.Normal;
			this.Distance = instance.Distance;
			this.Pnt = instance.Pnt;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x0005FB1D File Offset: 0x0005DF1D
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x0005FB25 File Offset: 0x0005DF25
		public Vector3 Pnt { get; private set; }

		// Token: 0x06001094 RID: 4244 RVA: 0x0005FB2E File Offset: 0x0005DF2E
		public void Set(Vector3 normal, Vector3 p)
		{
			this.Normal = normal.normalized;
			this.Distance = Vector3.Dot(this.Normal, p);
			this.Pnt = p;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0005FB58 File Offset: 0x0005DF58
		public Plane.PointClass ClassifyPoint(Vector3 p)
		{
			float num = Vector3.Dot(p, this.Normal) - this.Distance;
			return (num >= -0.0001f) ? ((num <= 0.0001f) ? Plane.PointClass.Coplanar : Plane.PointClass.Front) : Plane.PointClass.Back;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0005FB9C File Offset: 0x0005DF9C
		public bool GetSide(Vector3 n)
		{
			return Vector3.Dot(n, this.Normal) - this.Distance > 0.0001f;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0005FBB8 File Offset: 0x0005DFB8
		public void Flip()
		{
			this.Normal = -this.Normal;
			this.Distance = -this.Distance;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0005FBD8 File Offset: 0x0005DFD8
		public bool GetSideFix(ref Vector3 n)
		{
			float num = n.x * this.Normal.x + n.y * this.Normal.y + n.z * this.Normal.z - this.Distance;
			float num2 = 1f;
			float num3 = num;
			if (num < 0f)
			{
				num2 = -1f;
				num3 = -num;
			}
			if (num3 < 0.0011f)
			{
				n.x += this.Normal.x * 0.001f * num2;
				n.y += this.Normal.y * 0.001f * num2;
				n.z += this.Normal.z * 0.001f * num2;
				num = n.x * this.Normal.x + n.y * this.Normal.y + n.z * this.Normal.z - this.Distance;
			}
			return num > 0.0001f;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0005FCF4 File Offset: 0x0005E0F4
		public bool SameSide(Vector3 a, Vector3 b)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0005FCFC File Offset: 0x0005E0FC
		public bool IntersectSegment(Vector3 a, Vector3 b, out float t, ref Vector3 q)
		{
			float num = b.x - a.x;
			float num2 = b.y - a.y;
			float num3 = b.z - a.z;
			float num4 = this.Normal.x * a.x + this.Normal.y * a.y + this.Normal.z * a.z;
			float num5 = this.Normal.x * num + this.Normal.y * num2 + this.Normal.z * num3;
			t = (this.Distance - num4) / num5;
			if (t >= -0.0001f && t <= 1.0001f)
			{
				q.x = a.x + t * num;
				q.y = a.y + t * num2;
				q.z = a.z + t * num3;
				return true;
			}
			q = Vector3.zero;
			return false;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0005FE10 File Offset: 0x0005E210
		public void InverseTransform(ExploderTransform transform)
		{
			Vector3 vector = transform.InverseTransformDirection(this.Normal);
			Vector3 rhs = transform.InverseTransformPoint(this.Pnt);
			this.Normal = vector;
			this.Distance = Vector3.Dot(vector, rhs);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0005FE50 File Offset: 0x0005E250
		public Matrix4x4 GetPlaneMatrix()
		{
			Matrix4x4 result = default(Matrix4x4);
			Quaternion q = Quaternion.LookRotation(this.Normal);
			result.SetTRS(this.Pnt, q, Vector3.one);
			return result;
		}

		// Token: 0x04001279 RID: 4729
		private const float epsylon = 0.0001f;

		// Token: 0x0400127A RID: 4730
		public Vector3 Normal;

		// Token: 0x0400127C RID: 4732
		public float Distance;

		// Token: 0x020003AB RID: 939
		[Flags]
		public enum PointClass
		{
			// Token: 0x0400127E RID: 4734
			Coplanar = 0,
			// Token: 0x0400127F RID: 4735
			Front = 1,
			// Token: 0x04001280 RID: 4736
			Back = 2,
			// Token: 0x04001281 RID: 4737
			Intersection = 3
		}
	}
}
