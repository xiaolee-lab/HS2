using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003B9 RID: 953
	public class ExploderObject : MonoBehaviour
	{
		// Token: 0x060010DA RID: 4314 RVA: 0x000633F8 File Offset: 0x000617F8
		public void ExplodeRadius()
		{
			this.ExplodeRadius(null);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00063401 File Offset: 0x00061801
		public void ExplodeRadius(ExploderObject.OnExplosion callback)
		{
			this.core.Enqueue(this, callback, false, null);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00063412 File Offset: 0x00061812
		public void ExplodeObject(GameObject obj)
		{
			this.ExplodeObject(obj, null);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0006341C File Offset: 0x0006181C
		public void ExplodeObject(GameObject obj, ExploderObject.OnExplosion callback)
		{
			this.core.Enqueue(this, callback, false, new GameObject[]
			{
				obj
			});
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00063436 File Offset: 0x00061836
		public void ExplodeObjects(params GameObject[] objects)
		{
			this.ExplodeObjects(null, objects);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00063440 File Offset: 0x00061840
		public void ExplodeObjects(ExploderObject.OnExplosion callback, params GameObject[] objects)
		{
			this.core.Enqueue(this, callback, false, objects);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00063451 File Offset: 0x00061851
		public void ExplodePartial(GameObject obj, Vector3 shotDir, Vector3 hitPosition, float bulletSize)
		{
			this.ExplodePartial(obj, shotDir, hitPosition, bulletSize, null);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0006345F File Offset: 0x0006185F
		public void ExplodePartial(GameObject obj, Vector3 shotDir, Vector3 hitPosition, float bulletSize, ExploderObject.OnExplosion callback)
		{
			this.core.ExplodePartial(obj, shotDir, hitPosition, bulletSize, callback);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00063473 File Offset: 0x00061873
		public void CrackRadius()
		{
			this.CrackRadius(null);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0006347C File Offset: 0x0006187C
		public void CrackRadius(ExploderObject.OnExplosion callback)
		{
			this.core.Enqueue(this, callback, true, null);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0006348D File Offset: 0x0006188D
		public void CrackObject(GameObject obj)
		{
			this.CrackObject(obj, null);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00063497 File Offset: 0x00061897
		public void CrackObject(GameObject obj, ExploderObject.OnExplosion callback)
		{
			this.core.Enqueue(this, callback, true, new GameObject[]
			{
				obj
			});
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x000634B1 File Offset: 0x000618B1
		public bool CanCrack()
		{
			return this.TargetFragments < FragmentPool.Instance.GetAvailableCrackFragmentsCount();
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000634C5 File Offset: 0x000618C5
		public bool IsCracked(GameObject gm)
		{
			return this.core.IsCracked(gm);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000634D3 File Offset: 0x000618D3
		public void ExplodeCracked(GameObject obj, ExploderObject.OnExplosion callback)
		{
			this.core.ExplodeCracked(obj, callback);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000634E2 File Offset: 0x000618E2
		public void ExplodeCracked(GameObject obj)
		{
			this.core.ExplodeCracked(obj, null);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000634F1 File Offset: 0x000618F1
		public void ExplodeCracked(ExploderObject.OnExplosion callback)
		{
			this.ExplodeCracked(null, callback);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000634FB File Offset: 0x000618FB
		public void ExplodeCracked()
		{
			this.ExplodeCracked(null, null);
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00063505 File Offset: 0x00061905
		public int ProcessingFrames
		{
			get
			{
				return this.core.processingFrames;
			}
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00063512 File Offset: 0x00061912
		private void Awake()
		{
			this.core = Singleton<Core>.Instance;
			this.core.Initialize(this);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0006352C File Offset: 0x0006192C
		private void OnDrawGizmos()
		{
			if (base.enabled && (!this.ExplodeSelf || !this.DisableRadiusScan))
			{
				Gizmos.color = Color.red;
				if (this.UseCubeRadius)
				{
					Vector3 centroid = ExploderUtils.GetCentroid(base.gameObject);
					Gizmos.matrix = base.transform.localToWorldMatrix;
					Gizmos.DrawWireCube(base.transform.InverseTransformPoint(centroid), this.CubeRadius);
				}
				else
				{
					Gizmos.DrawWireSphere(ExploderUtils.GetCentroid(base.gameObject), this.Radius);
				}
			}
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x000635BD File Offset: 0x000619BD
		private bool IsExplodable(GameObject obj)
		{
			if (this.core.parameters.DontUseTag)
			{
				return obj.GetComponent<Explodable>() != null;
			}
			return obj.CompareTag(ExploderObject.Tag);
		}

		// Token: 0x040012AD RID: 4781
		public static string Tag = "Exploder";

		// Token: 0x040012AE RID: 4782
		public bool DontUseTag;

		// Token: 0x040012AF RID: 4783
		public float Radius = 10f;

		// Token: 0x040012B0 RID: 4784
		public Vector3 CubeRadius = Vector3.zero;

		// Token: 0x040012B1 RID: 4785
		public bool UseCubeRadius;

		// Token: 0x040012B2 RID: 4786
		public Vector3 ForceVector = Vector3.up;

		// Token: 0x040012B3 RID: 4787
		public bool UseForceVector;

		// Token: 0x040012B4 RID: 4788
		public float Force = 30f;

		// Token: 0x040012B5 RID: 4789
		public float FrameBudget = 15f;

		// Token: 0x040012B6 RID: 4790
		public int TargetFragments = 30;

		// Token: 0x040012B7 RID: 4791
		public ExploderObject.ThreadOptions ThreadOption = ExploderObject.ThreadOptions.WorkerThread3x;

		// Token: 0x040012B8 RID: 4792
		public bool ExplodeSelf = true;

		// Token: 0x040012B9 RID: 4793
		public bool DisableRadiusScan;

		// Token: 0x040012BA RID: 4794
		public bool HideSelf = true;

		// Token: 0x040012BB RID: 4795
		public bool DestroyOriginalObject;

		// Token: 0x040012BC RID: 4796
		public bool UniformFragmentDistribution;

		// Token: 0x040012BD RID: 4797
		public bool SplitMeshIslands;

		// Token: 0x040012BE RID: 4798
		public bool DisableTriangulation;

		// Token: 0x040012BF RID: 4799
		public int FragmentPoolSize = 200;

		// Token: 0x040012C0 RID: 4800
		public bool Use2DCollision;

		// Token: 0x040012C1 RID: 4801
		public ExploderObject.CuttingStyleOption CuttingStyle;

		// Token: 0x040012C2 RID: 4802
		public FragmentDeactivation FragmentDeactivation = new FragmentDeactivation
		{
			DeactivateOptions = DeactivateOptions.OutsideOfCamera,
			DeactivateTimeout = 10f,
			FadeoutOptions = FadeoutOptions.None
		};

		// Token: 0x040012C3 RID: 4803
		public FragmentSFX FragmentSFX = new FragmentSFX
		{
			ChanceToPlay = 100,
			PlayOnlyOnce = false,
			MixMultipleSounds = false,
			EmitersMax = 1000,
			ParticleTimeout = 5f
		};

		// Token: 0x040012C4 RID: 4804
		public FragmentOption FragmentOptions = new FragmentOption
		{
			FreezePositionX = false,
			FreezePositionY = false,
			FreezePositionZ = false,
			FreezeRotationX = false,
			FreezeRotationY = false,
			FreezeRotationZ = false,
			Layer = "Default",
			Mass = 20f,
			MaxVelocity = 1000f,
			DisableColliders = false,
			MeshColliders = false,
			UseGravity = true,
			InheritParentPhysicsProperty = true,
			AngularVelocity = 1f,
			AngularVelocityVector = Vector3.up,
			MaxAngularVelocity = 7f,
			RandomAngularVelocityVector = true,
			FragmentMaterial = null
		};

		// Token: 0x040012C5 RID: 4805
		private Core core;

		// Token: 0x020003BA RID: 954
		public enum ThreadOptions
		{
			// Token: 0x040012C7 RID: 4807
			WorkerThread1x,
			// Token: 0x040012C8 RID: 4808
			WorkerThread2x,
			// Token: 0x040012C9 RID: 4809
			WorkerThread3x,
			// Token: 0x040012CA RID: 4810
			Disabled
		}

		// Token: 0x020003BB RID: 955
		public enum CuttingStyleOption
		{
			// Token: 0x040012CC RID: 4812
			Random,
			// Token: 0x040012CD RID: 4813
			RectangularRandom,
			// Token: 0x040012CE RID: 4814
			RectangularRegular
		}

		// Token: 0x020003BC RID: 956
		// (Invoke) Token: 0x060010F2 RID: 4338
		public delegate void OnExplosion(float timeMS, ExploderObject.ExplosionState state);

		// Token: 0x020003BD RID: 957
		public enum ExplosionState
		{
			// Token: 0x040012D0 RID: 4816
			ExplosionStarted,
			// Token: 0x040012D1 RID: 4817
			ExplosionFinished,
			// Token: 0x040012D2 RID: 4818
			ObjectCracked
		}
	}
}
