using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003A0 RID: 928
	public class ExploderParams
	{
		// Token: 0x0600106D RID: 4205 RVA: 0x0005C210 File Offset: 0x0005A610
		public ExploderParams(ExploderObject exploder)
		{
			this.Position = ExploderUtils.GetCentroid(exploder.gameObject);
			this.DontUseTag = exploder.DontUseTag;
			this.Radius = exploder.Radius;
			this.UseCubeRadius = exploder.UseCubeRadius;
			this.CubeRadius = exploder.CubeRadius;
			this.ForceVector = exploder.ForceVector;
			this.UseForceVector = exploder.UseForceVector;
			this.Force = exploder.Force;
			this.FrameBudget = exploder.FrameBudget;
			this.TargetFragments = exploder.TargetFragments;
			this.ExplodeSelf = exploder.ExplodeSelf;
			this.HideSelf = exploder.HideSelf;
			this.ThreadOptions = exploder.ThreadOption;
			this.DestroyOriginalObject = exploder.DestroyOriginalObject;
			this.SplitMeshIslands = exploder.SplitMeshIslands;
			this.FragmentOptions = exploder.FragmentOptions.Clone();
			this.FragmentDeactivation = exploder.FragmentDeactivation.Clone();
			this.FragmentSFX = exploder.FragmentSFX.Clone();
			this.Use2DCollision = exploder.Use2DCollision;
			this.FragmentPoolSize = exploder.FragmentPoolSize;
			this.DisableRadiusScan = exploder.DisableRadiusScan;
			this.UniformFragmentDistribution = exploder.UniformFragmentDistribution;
			this.DisableTriangulation = exploder.DisableTriangulation;
			this.ExploderGameObject = exploder.gameObject;
			this.CuttingStyle = exploder.CuttingStyle;
		}

		// Token: 0x04001222 RID: 4642
		public Vector3 Position;

		// Token: 0x04001223 RID: 4643
		public Vector3 ForceVector;

		// Token: 0x04001224 RID: 4644
		public Vector3 CubeRadius;

		// Token: 0x04001225 RID: 4645
		public Vector3 HitPosition;

		// Token: 0x04001226 RID: 4646
		public Vector3 ShotDir;

		// Token: 0x04001227 RID: 4647
		public float Force;

		// Token: 0x04001228 RID: 4648
		public float FrameBudget;

		// Token: 0x04001229 RID: 4649
		public float Radius;

		// Token: 0x0400122A RID: 4650
		public float BulletSize;

		// Token: 0x0400122B RID: 4651
		public int id;

		// Token: 0x0400122C RID: 4652
		public int TargetFragments;

		// Token: 0x0400122D RID: 4653
		public int FragmentPoolSize;

		// Token: 0x0400122E RID: 4654
		public ExploderObject.ThreadOptions ThreadOptions;

		// Token: 0x0400122F RID: 4655
		public ExploderObject.OnExplosion Callback;

		// Token: 0x04001230 RID: 4656
		public FragmentOption FragmentOptions;

		// Token: 0x04001231 RID: 4657
		public FragmentDeactivation FragmentDeactivation;

		// Token: 0x04001232 RID: 4658
		public FragmentSFX FragmentSFX;

		// Token: 0x04001233 RID: 4659
		public ExploderObject.CuttingStyleOption CuttingStyle;

		// Token: 0x04001234 RID: 4660
		public GameObject[] Targets;

		// Token: 0x04001235 RID: 4661
		public GameObject ExploderGameObject;

		// Token: 0x04001236 RID: 4662
		public bool UseCubeRadius;

		// Token: 0x04001237 RID: 4663
		public bool DontUseTag;

		// Token: 0x04001238 RID: 4664
		public bool UseForceVector;

		// Token: 0x04001239 RID: 4665
		public bool ExplodeSelf;

		// Token: 0x0400123A RID: 4666
		public bool HideSelf;

		// Token: 0x0400123B RID: 4667
		public bool DestroyOriginalObject;

		// Token: 0x0400123C RID: 4668
		public bool SplitMeshIslands;

		// Token: 0x0400123D RID: 4669
		public bool Use2DCollision;

		// Token: 0x0400123E RID: 4670
		public bool DisableRadiusScan;

		// Token: 0x0400123F RID: 4671
		public bool UniformFragmentDistribution;

		// Token: 0x04001240 RID: 4672
		public bool DisableTriangulation;

		// Token: 0x04001241 RID: 4673
		public bool Crack;

		// Token: 0x04001242 RID: 4674
		public bool processing;
	}
}
