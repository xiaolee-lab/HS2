using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003C3 RID: 963
	[Serializable]
	public class FragmentOption
	{
		// Token: 0x0600110B RID: 4363 RVA: 0x0006430C File Offset: 0x0006270C
		public FragmentOption Clone()
		{
			return new FragmentOption
			{
				ExplodeFragments = this.ExplodeFragments,
				FreezePositionX = this.FreezePositionX,
				FreezePositionY = this.FreezePositionY,
				FreezePositionZ = this.FreezePositionZ,
				FreezeRotationX = this.FreezeRotationX,
				FreezeRotationY = this.FreezeRotationY,
				FreezeRotationZ = this.FreezeRotationZ,
				Layer = this.Layer,
				Mass = this.Mass,
				DisableColliders = this.DisableColliders,
				MeshColliders = this.MeshColliders,
				UseGravity = this.UseGravity,
				MaxVelocity = this.MaxVelocity,
				MaxAngularVelocity = this.MaxAngularVelocity,
				InheritParentPhysicsProperty = this.InheritParentPhysicsProperty,
				AngularVelocity = this.AngularVelocity,
				AngularVelocityVector = this.AngularVelocityVector,
				RandomAngularVelocityVector = this.RandomAngularVelocityVector,
				FragmentMaterial = this.FragmentMaterial
			};
		}

		// Token: 0x040012FA RID: 4858
		public GameObject FragmentPrefab;

		// Token: 0x040012FB RID: 4859
		public bool FreezePositionX;

		// Token: 0x040012FC RID: 4860
		public bool FreezePositionY;

		// Token: 0x040012FD RID: 4861
		public bool FreezePositionZ;

		// Token: 0x040012FE RID: 4862
		public bool FreezeRotationX;

		// Token: 0x040012FF RID: 4863
		public bool FreezeRotationY;

		// Token: 0x04001300 RID: 4864
		public bool FreezeRotationZ;

		// Token: 0x04001301 RID: 4865
		public string Layer;

		// Token: 0x04001302 RID: 4866
		public bool ExplodeFragments = true;

		// Token: 0x04001303 RID: 4867
		public float MaxVelocity;

		// Token: 0x04001304 RID: 4868
		public bool InheritParentPhysicsProperty;

		// Token: 0x04001305 RID: 4869
		public float Mass;

		// Token: 0x04001306 RID: 4870
		public bool UseGravity;

		// Token: 0x04001307 RID: 4871
		public bool DisableColliders;

		// Token: 0x04001308 RID: 4872
		public bool MeshColliders;

		// Token: 0x04001309 RID: 4873
		public float AngularVelocity;

		// Token: 0x0400130A RID: 4874
		public float MaxAngularVelocity;

		// Token: 0x0400130B RID: 4875
		public Vector3 AngularVelocityVector;

		// Token: 0x0400130C RID: 4876
		public bool RandomAngularVelocityVector;

		// Token: 0x0400130D RID: 4877
		public Material FragmentMaterial;
	}
}
