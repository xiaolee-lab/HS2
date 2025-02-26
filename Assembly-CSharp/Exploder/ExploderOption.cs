using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003BE RID: 958
	public class ExploderOption : MonoBehaviour
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x00063638 File Offset: 0x00061A38
		public void DuplicateSettings(ExploderOption options)
		{
			options.Plane2D = this.Plane2D;
			options.CrossSectionVertexColor = this.CrossSectionVertexColor;
			options.CrossSectionUV = this.CrossSectionUV;
			options.SplitMeshIslands = this.SplitMeshIslands;
			options.UseLocalForce = this.UseLocalForce;
			options.Force = this.Force;
			options.FragmentMaterial = this.FragmentMaterial;
		}

		// Token: 0x040012D3 RID: 4819
		public bool Plane2D;

		// Token: 0x040012D4 RID: 4820
		public Color CrossSectionVertexColor = Color.white;

		// Token: 0x040012D5 RID: 4821
		public Vector4 CrossSectionUV = new Vector4(0f, 0f, 1f, 1f);

		// Token: 0x040012D6 RID: 4822
		public bool SplitMeshIslands;

		// Token: 0x040012D7 RID: 4823
		public bool UseLocalForce;

		// Token: 0x040012D8 RID: 4824
		public float Force = 30f;

		// Token: 0x040012D9 RID: 4825
		public Material FragmentMaterial;
	}
}
