using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003BF RID: 959
	public class Fragment : MonoBehaviour
	{
		// Token: 0x060010F8 RID: 4344 RVA: 0x000636A1 File Offset: 0x00061AA1
		public bool IsSleeping()
		{
			if (this.rigid2D)
			{
				return this.rigid2D.IsSleeping();
			}
			return this.rigidBody.IsSleeping();
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000636CA File Offset: 0x00061ACA
		public void Sleep()
		{
			if (this.rigid2D)
			{
				this.rigid2D.Sleep();
			}
			else
			{
				this.rigidBody.Sleep();
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000636F7 File Offset: 0x00061AF7
		public void WakeUp()
		{
			if (this.rigid2D)
			{
				this.rigid2D.WakeUp();
			}
			else
			{
				this.rigidBody.WakeUp();
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00063724 File Offset: 0x00061B24
		public void SetConstraints(RigidbodyConstraints constraints)
		{
			if (this.rigidBody)
			{
				this.rigidBody.constraints = constraints;
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00063744 File Offset: 0x00061B44
		public void InitSFX(FragmentSFX sfx)
		{
			if (sfx.FragmentEmitter)
			{
				if (!this.particleChild)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(sfx.FragmentEmitter);
					if (gameObject)
					{
						gameObject.transform.position = Vector3.zero;
						this.particleChild = new GameObject("Particles");
						this.particleChild.transform.parent = base.gameObject.transform;
						gameObject.transform.parent = this.particleChild.transform;
					}
				}
				if (this.particleChild)
				{
					this.particleSystems = this.particleChild.GetComponentsInChildren<ParticleSystem>();
				}
				this.emmitersTimeout = sfx.ParticleTimeout;
				this.stopEmitOnTimeout = (this.emmitersTimeout > 0f);
			}
			else if (this.particleChild)
			{
				UnityEngine.Object.Destroy(this.particleChild);
				sfx.ParticleTimeout = -1f;
				this.stopEmitOnTimeout = false;
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0006384C File Offset: 0x00061C4C
		private void OnCollisionEnter()
		{
			if (!this.settings.FragmentSFX.MixMultipleSounds && Fragment.activePlayback && Fragment.activePlayback.isPlaying)
			{
				return;
			}
			bool flag = !this.settings.FragmentSFX.PlayOnlyOnce || !this.collided;
			bool flag2 = UnityEngine.Random.Range(0, 100) <= this.settings.FragmentSFX.ChanceToPlay;
			this.collided = true;
			if (flag && flag2 && this.audioSource)
			{
				this.audioSource.Play();
				Fragment.activePlayback = this.audioSource;
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00063908 File Offset: 0x00061D08
		public void DisableColliders(bool disable, bool meshColliders, bool physics2d)
		{
			if (disable)
			{
				if (physics2d)
				{
					UnityEngine.Object.Destroy(this.polygonCollider2D);
				}
				else
				{
					if (this.meshCollider)
					{
						UnityEngine.Object.Destroy(this.meshCollider);
					}
					if (this.boxCollider)
					{
						UnityEngine.Object.Destroy(this.boxCollider);
					}
				}
			}
			else if (physics2d)
			{
				if (!this.polygonCollider2D)
				{
					this.polygonCollider2D = base.gameObject.AddComponent<PolygonCollider2D>();
				}
			}
			else if (meshColliders)
			{
				if (!this.meshCollider)
				{
					this.meshCollider = base.gameObject.AddComponent<MeshCollider>();
				}
			}
			else if (!this.boxCollider)
			{
				this.boxCollider = base.gameObject.AddComponent<BoxCollider>();
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000639E8 File Offset: 0x00061DE8
		public void ApplyExplosion(ExploderTransform meshTransform, Vector3 centroid, float force, GameObject original, ExploderParams set)
		{
			this.settings = set;
			if (this.rigid2D)
			{
				this.ApplyExplosion2D(meshTransform, centroid, force, original);
				return;
			}
			Rigidbody rigidbody = this.rigidBody;
			Vector3 b = Vector3.zero;
			Vector3 b2 = Vector3.zero;
			float mass = this.settings.FragmentOptions.Mass;
			bool useGravity = this.settings.FragmentOptions.UseGravity;
			rigidbody.maxAngularVelocity = this.settings.FragmentOptions.MaxAngularVelocity;
			if (this.settings.FragmentOptions.InheritParentPhysicsProperty && original && original.GetComponent<Rigidbody>())
			{
				Rigidbody component = original.GetComponent<Rigidbody>();
				b = component.velocity;
				b2 = component.angularVelocity;
				mass = component.mass / (float)this.settings.TargetFragments;
				useGravity = component.useGravity;
			}
			Vector3 a = (meshTransform.TransformPoint(centroid) - this.settings.Position).normalized;
			Vector3 a2 = this.settings.FragmentOptions.AngularVelocity * ((!this.settings.FragmentOptions.RandomAngularVelocityVector) ? this.settings.FragmentOptions.AngularVelocityVector : UnityEngine.Random.onUnitSphere);
			if (this.settings.UseForceVector)
			{
				a = this.settings.ForceVector;
			}
			rigidbody.velocity = a * force + b;
			rigidbody.angularVelocity = a2 + b2;
			rigidbody.mass = mass;
			rigidbody.useGravity = useGravity;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00063B88 File Offset: 0x00061F88
		private void ApplyExplosion2D(ExploderTransform meshTransform, Vector3 centroid, float force, GameObject original)
		{
			Rigidbody2D rigidbody2D = this.rigid2D;
			Vector2 b = Vector2.zero;
			float num = 0f;
			float mass = this.settings.FragmentOptions.Mass;
			if (this.settings.FragmentOptions.InheritParentPhysicsProperty && original && original.GetComponent<Rigidbody2D>())
			{
				Rigidbody2D component = original.GetComponent<Rigidbody2D>();
				b = component.velocity;
				num = component.angularVelocity;
				mass = component.mass / (float)this.settings.TargetFragments;
			}
			Vector2 a = (meshTransform.TransformPoint(centroid) - this.settings.Position).normalized;
			float num2 = this.settings.FragmentOptions.AngularVelocity * ((!this.settings.FragmentOptions.RandomAngularVelocityVector) ? this.settings.FragmentOptions.AngularVelocityVector.y : UnityEngine.Random.insideUnitCircle.x);
			if (this.settings.UseForceVector)
			{
				a = this.settings.ForceVector;
			}
			rigidbody2D.velocity = a * force + b;
			rigidbody2D.angularVelocity = num2 + num;
			rigidbody2D.mass = mass;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00063CD8 File Offset: 0x000620D8
		public void RefreshComponentsCache()
		{
			this.audioSource = base.gameObject.GetComponent<AudioSource>();
			this.meshFilter = base.GetComponent<MeshFilter>();
			this.meshRenderer = base.GetComponent<MeshRenderer>();
			this.meshCollider = base.GetComponent<MeshCollider>();
			this.boxCollider = base.GetComponent<BoxCollider>();
			this.rigidBody = base.GetComponent<Rigidbody>();
			this.rigid2D = base.GetComponent<Rigidbody2D>();
			this.polygonCollider2D = base.GetComponent<PolygonCollider2D>();
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00063D4C File Offset: 0x0006214C
		public void Explode(ExploderParams parameters)
		{
			this.settings = parameters;
			this.IsActive = true;
			ExploderUtils.SetActiveRecursively(base.gameObject, true);
			this.visibilityCheckTimer = 0.1f;
			this.Visible = true;
			this.Cracked = false;
			this.collided = false;
			this.deactivateTimer = this.settings.FragmentDeactivation.DeactivateTimeout;
			this.originalScale = base.transform.localScale;
			if (this.settings.FragmentOptions.ExplodeFragments)
			{
				base.tag = ExploderObject.Tag;
			}
			this.Emit(true);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00063DE4 File Offset: 0x000621E4
		public void Emit(bool centerToBound)
		{
			if (this.particleSystems != null)
			{
				if (centerToBound && this.particleChild && this.meshRenderer)
				{
					this.particleChild.transform.position = this.meshRenderer.bounds.center;
				}
				foreach (ParticleSystem particleSystem in this.particleSystems)
				{
					particleSystem.Clear();
					particleSystem.Play();
				}
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00063E70 File Offset: 0x00062270
		public void Deactivate()
		{
			if (Fragment.activePlayback == this.audioSource)
			{
				Fragment.activePlayback = null;
			}
			this.Sleep();
			ExploderUtils.SetActiveRecursively(base.gameObject, false);
			this.Visible = false;
			this.IsActive = false;
			if (this.meshFilter && this.meshFilter.sharedMesh)
			{
				UnityEngine.Object.DestroyImmediate(this.meshFilter.sharedMesh, true);
			}
			if (this.particleSystems != null)
			{
				foreach (ParticleSystem particleSystem in this.particleSystems)
				{
					particleSystem.Clear();
				}
			}
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00063F1E File Offset: 0x0006231E
		public void AssignMesh(Mesh mesh)
		{
			if (this.meshFilter.sharedMesh)
			{
				UnityEngine.Object.DestroyImmediate(this.meshFilter.sharedMesh, true);
			}
			this.meshFilter.sharedMesh = mesh;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00063F52 File Offset: 0x00062352
		private void Start()
		{
			this.visibilityCheckTimer = 1f;
			this.RefreshComponentsCache();
			this.Visible = false;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00063F6C File Offset: 0x0006236C
		private void Update()
		{
			if (this.IsActive)
			{
				float maxVelocity = this.settings.FragmentOptions.MaxVelocity;
				if (this.rigidBody)
				{
					if (this.rigidBody.velocity.sqrMagnitude > maxVelocity * maxVelocity)
					{
						Vector3 normalized = this.rigidBody.velocity.normalized;
						this.rigidBody.velocity = normalized * maxVelocity;
					}
				}
				else if (this.rigid2D && this.rigid2D.velocity.sqrMagnitude > maxVelocity * maxVelocity)
				{
					Vector2 normalized2 = this.rigid2D.velocity.normalized;
					this.rigid2D.velocity = normalized2 * maxVelocity;
				}
				if (this.settings.FragmentDeactivation.DeactivateOptions == DeactivateOptions.Timeout)
				{
					this.deactivateTimer -= Time.deltaTime;
					if (this.deactivateTimer < 0f)
					{
						this.Deactivate();
						FadeoutOptions fadeoutOptions = this.settings.FragmentDeactivation.FadeoutOptions;
						if (fadeoutOptions != FadeoutOptions.FadeoutAlpha)
						{
						}
					}
					else
					{
						float num = this.deactivateTimer / this.settings.FragmentDeactivation.DeactivateTimeout;
						FadeoutOptions fadeoutOptions2 = this.settings.FragmentDeactivation.FadeoutOptions;
						if (fadeoutOptions2 != FadeoutOptions.FadeoutAlpha)
						{
							if (fadeoutOptions2 == FadeoutOptions.ScaleDown)
							{
								base.gameObject.transform.localScale = this.originalScale * num;
							}
						}
						else if (this.meshRenderer.material && this.meshRenderer.material.HasProperty("_Color"))
						{
							Color color = this.meshRenderer.material.color;
							color.a = num;
							this.meshRenderer.material.color = color;
						}
					}
				}
				if (this.stopEmitOnTimeout)
				{
					this.emmitersTimeout -= Time.deltaTime;
					if (this.emmitersTimeout < 0f)
					{
						if (this.particleChild != null)
						{
							ParticleSystem componentInChildren = this.particleChild.GetComponentInChildren<ParticleSystem>();
							if (componentInChildren)
							{
								componentInChildren.Stop();
							}
						}
						this.stopEmitOnTimeout = false;
					}
				}
				this.visibilityCheckTimer -= Time.deltaTime;
				if (this.visibilityCheckTimer < 0f && Camera.main)
				{
					Vector3 vector = Camera.main.WorldToViewportPoint(base.transform.position);
					if (vector.z < 0f || vector.x < 0f || vector.y < 0f || vector.x > 1f || vector.y > 1f)
					{
						if (this.settings.FragmentDeactivation.DeactivateOptions == DeactivateOptions.OutsideOfCamera)
						{
							this.Deactivate();
						}
						this.Visible = false;
					}
					else
					{
						this.Visible = true;
					}
					this.visibilityCheckTimer = UnityEngine.Random.Range(0.1f, 0.3f);
				}
			}
		}

		// Token: 0x040012DA RID: 4826
		[NonSerialized]
		public bool Cracked;

		// Token: 0x040012DB RID: 4827
		[NonSerialized]
		public bool Visible;

		// Token: 0x040012DC RID: 4828
		[NonSerialized]
		public bool IsActive;

		// Token: 0x040012DD RID: 4829
		[NonSerialized]
		public MeshRenderer meshRenderer;

		// Token: 0x040012DE RID: 4830
		[NonSerialized]
		public MeshCollider meshCollider;

		// Token: 0x040012DF RID: 4831
		[NonSerialized]
		public MeshFilter meshFilter;

		// Token: 0x040012E0 RID: 4832
		[NonSerialized]
		public BoxCollider boxCollider;

		// Token: 0x040012E1 RID: 4833
		[NonSerialized]
		public PolygonCollider2D polygonCollider2D;

		// Token: 0x040012E2 RID: 4834
		[NonSerialized]
		public AudioSource audioSource;

		// Token: 0x040012E3 RID: 4835
		private ParticleSystem[] particleSystems;

		// Token: 0x040012E4 RID: 4836
		private GameObject particleChild;

		// Token: 0x040012E5 RID: 4837
		private Rigidbody2D rigid2D;

		// Token: 0x040012E6 RID: 4838
		private Rigidbody rigidBody;

		// Token: 0x040012E7 RID: 4839
		private ExploderParams settings;

		// Token: 0x040012E8 RID: 4840
		private Vector3 originalScale;

		// Token: 0x040012E9 RID: 4841
		private float visibilityCheckTimer;

		// Token: 0x040012EA RID: 4842
		private float deactivateTimer;

		// Token: 0x040012EB RID: 4843
		private float emmitersTimeout;

		// Token: 0x040012EC RID: 4844
		private bool stopEmitOnTimeout;

		// Token: 0x040012ED RID: 4845
		private bool collided;

		// Token: 0x040012EE RID: 4846
		private static AudioSource activePlayback;
	}
}
