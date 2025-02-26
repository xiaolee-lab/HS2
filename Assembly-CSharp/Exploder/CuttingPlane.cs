using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x0200039F RID: 927
	internal class CuttingPlane
	{
		// Token: 0x06001067 RID: 4199 RVA: 0x0005BF86 File Offset: 0x0005A386
		public CuttingPlane(Core core)
		{
			this.random = new System.Random();
			this.plane = new Plane(Vector3.one, Vector3.zero);
			this.core = core;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0005BFB8 File Offset: 0x0005A3B8
		private Plane GetRandomPlane(ExploderMesh mesh)
		{
			Vector3 normal = new Vector3((float)this.random.NextDouble() * 2f - 1f, (float)this.random.NextDouble() * 2f - 1f, (float)this.random.NextDouble() * 2f - 1f);
			this.plane.Set(normal, mesh.centroid);
			return this.plane;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0005C02C File Offset: 0x0005A42C
		private Plane GetRectangularRegularPlane(ExploderMesh mesh, int attempt)
		{
			float num = mesh.max.x - mesh.min.x;
			float num2 = mesh.max.y - mesh.min.y;
			float num3 = mesh.max.z - mesh.min.z;
			int num4;
			if (num > num2)
			{
				if (num > num3)
				{
					num4 = 0;
				}
				else
				{
					num4 = 2;
				}
			}
			else if (num2 > num3)
			{
				num4 = 1;
			}
			else
			{
				num4 = 2;
			}
			num4 += attempt;
			if (num4 > 2)
			{
				return this.GetRandomPlane(mesh);
			}
			this.plane.Set(CuttingPlane.rectAxis[num4], mesh.centroid);
			return this.plane;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0005C0EC File Offset: 0x0005A4EC
		private Plane GetRectangularRandom(ExploderMesh mesh, int attempt)
		{
			int num = this.random.Next(0, 3);
			num += attempt;
			if (num > 2)
			{
				return this.GetRandomPlane(mesh);
			}
			this.plane.Set(CuttingPlane.rectAxis[num], mesh.centroid);
			return this.plane;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0005C144 File Offset: 0x0005A544
		public Plane GetPlane(ExploderMesh mesh, int attempt)
		{
			ExploderObject.CuttingStyleOption cuttingStyle = this.core.parameters.CuttingStyle;
			if (cuttingStyle == ExploderObject.CuttingStyleOption.Random)
			{
				return this.GetRandomPlane(mesh);
			}
			if (cuttingStyle == ExploderObject.CuttingStyleOption.RectangularRandom)
			{
				return this.GetRectangularRandom(mesh, attempt);
			}
			if (cuttingStyle != ExploderObject.CuttingStyleOption.RectangularRegular)
			{
				return null;
			}
			return this.GetRectangularRegularPlane(mesh, attempt);
		}

		// Token: 0x0400121E RID: 4638
		private readonly System.Random random;

		// Token: 0x0400121F RID: 4639
		private readonly Plane plane;

		// Token: 0x04001220 RID: 4640
		private readonly Core core;

		// Token: 0x04001221 RID: 4641
		private static Vector3[] rectAxis = new Vector3[]
		{
			new Vector3(1f, 0f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 0f, 1f)
		};
	}
}
