using System;
using System.Collections.Generic;
using UniRx;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

// Token: 0x0200031A RID: 794
[AddComponentMenu("Dynamic Bone/Dynamic Bone V2")]
public class DynamicBoneV2 : MonoBehaviour
{
	// Token: 0x06000DDB RID: 3547 RVA: 0x00042234 File Offset: 0x00040634
	private void Start()
	{
		this.SetupParticles();
		(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
		where base.isActiveAndEnabled
		select _).Subscribe(delegate(long _)
		{
			this.OnUpdate();
		});
		(from _ in Observable.EveryLateUpdate().TakeUntilDestroy(base.gameObject)
		where base.isActiveAndEnabled
		select _).Subscribe(delegate(long _)
		{
			this.OnLateUpdate();
		});
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x000422AD File Offset: 0x000406AD
	private void FixedUpdate()
	{
		if (this.m_UpdateMode == DynamicBoneV2.UpdateMode.AnimatePhysics)
		{
			this.PreUpdate();
		}
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x000422C1 File Offset: 0x000406C1
	private void OnUpdate()
	{
		if (this.m_UpdateMode != DynamicBoneV2.UpdateMode.AnimatePhysics)
		{
			this.PreUpdate();
		}
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x000422D8 File Offset: 0x000406D8
	private void OnLateUpdate()
	{
		if (this.m_DistantDisable)
		{
			this.CheckDistance();
		}
		if (this.m_Weight > 0f && (!this.m_DistantDisable || !this.m_DistantDisabled))
		{
			float deltaTime = Time.deltaTime;
			this.UpdateDynamicBones(deltaTime);
		}
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x00042329 File Offset: 0x00040729
	private void PreUpdate()
	{
		if (this.m_Weight > 0f && (!this.m_DistantDisable || !this.m_DistantDisabled))
		{
			this.InitTransforms();
		}
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x00042358 File Offset: 0x00040758
	private void CheckDistance()
	{
		Transform transform = this.m_ReferenceObject;
		if (transform == null && Camera.main != null)
		{
			transform = Camera.main.transform;
		}
		if (transform != null)
		{
			float sqrMagnitude = (transform.position - base.transform.position).sqrMagnitude;
			bool flag = sqrMagnitude > this.m_DistanceToObject * this.m_DistanceToObject;
			if (flag != this.m_DistantDisabled)
			{
				if (!flag)
				{
					this.ResetParticlesPosition();
				}
				this.m_DistantDisabled = flag;
			}
		}
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x000423EE File Offset: 0x000407EE
	private void OnEnable()
	{
		this.ResetParticlesPosition();
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x000423F6 File Offset: 0x000407F6
	private void OnDisable()
	{
		this.InitTransforms();
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x00042400 File Offset: 0x00040800
	private void OnValidate()
	{
		this.m_UpdateRate = Mathf.Max(this.m_UpdateRate, 0f);
		this.m_Damping = Mathf.Clamp01(this.m_Damping);
		this.m_Elasticity = Mathf.Clamp01(this.m_Elasticity);
		this.m_Stiffness = Mathf.Clamp01(this.m_Stiffness);
		this.m_Inert = Mathf.Clamp01(this.m_Inert);
		this.m_Radius = Mathf.Max(this.m_Radius, 0f);
		if (Application.isEditor && Application.isPlaying)
		{
			this.InitTransforms();
			this.SetupParticles();
		}
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x000424A0 File Offset: 0x000408A0
	private void OnDrawGizmosSelected()
	{
		if (!base.enabled || this.m_Root == null)
		{
			return;
		}
		if (Application.isEditor && !Application.isPlaying && base.transform.hasChanged)
		{
			this.InitTransforms();
			this.SetupParticles();
		}
		Gizmos.color = Color.white;
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBoneV2.Particle particle = this.m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				DynamicBoneV2.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
				Gizmos.DrawLine(particle.m_Position, particle2.m_Position);
			}
			if (particle.m_Radius > 0f)
			{
				Gizmos.DrawWireSphere(particle.m_Position, particle.m_Radius * this.m_ObjectScale);
			}
		}
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x00042584 File Offset: 0x00040984
	public void SetWeight(float w)
	{
		if (this.m_Weight != w)
		{
			if (w == 0f)
			{
				this.InitTransforms();
			}
			else if (this.m_Weight == 0f)
			{
				this.ResetParticlesPosition();
			}
			this.m_Weight = w;
		}
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x000425D0 File Offset: 0x000409D0
	public float GetWeight()
	{
		return this.m_Weight;
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x000425D8 File Offset: 0x000409D8
	private void UpdateDynamicBones(float t)
	{
		if (this.m_Root == null)
		{
			return;
		}
		this.m_ObjectScale = Mathf.Abs(base.transform.lossyScale.x);
		this.m_ObjectMove = base.transform.position - this.m_ObjectPrevPosition;
		this.m_ObjectPrevPosition = base.transform.position;
		int num = 1;
		if (this.m_UpdateRate > 0f)
		{
			float num2 = 1f / this.m_UpdateRate;
			this.m_Time += t;
			num = 0;
			while (this.m_Time >= num2)
			{
				this.m_Time -= num2;
				if (++num >= 3)
				{
					this.m_Time = 0f;
					break;
				}
			}
		}
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				this.UpdateParticles1();
				this.UpdateParticles2();
				this.m_ObjectMove = Vector3.zero;
			}
		}
		else
		{
			this.SkipUpdateParticles();
		}
		this.ApplyParticlesToTransforms();
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x000426EC File Offset: 0x00040AEC
	private void SetupParticles()
	{
		this.m_Particles.Clear();
		if (this.m_Root == null)
		{
			return;
		}
		this.m_LocalGravity = this.m_Root.InverseTransformDirection(this.m_Gravity);
		this.m_ObjectScale = Mathf.Abs(base.transform.lossyScale.x);
		this.m_ObjectPrevPosition = base.transform.position;
		this.m_ObjectMove = Vector3.zero;
		this.m_BoneTotalLength = 0f;
		this.AppendParticles(this.m_Root, -1, 0f);
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBoneV2.Particle particle = this.m_Particles[i];
			particle.m_Damping = this.m_Damping;
			particle.m_Elasticity = this.m_Elasticity;
			particle.m_Stiffness = this.m_Stiffness;
			particle.m_Inert = this.m_Inert;
			particle.m_Radius = this.m_Radius;
			if (this.m_BoneTotalLength > 0f)
			{
				float time = particle.m_BoneLength / this.m_BoneTotalLength;
				if (this.m_DampingDistrib != null && this.m_DampingDistrib.keys.Length > 0)
				{
					particle.m_Damping *= this.m_DampingDistrib.Evaluate(time);
				}
				if (this.m_ElasticityDistrib != null && this.m_ElasticityDistrib.keys.Length > 0)
				{
					particle.m_Elasticity *= this.m_ElasticityDistrib.Evaluate(time);
				}
				if (this.m_StiffnessDistrib != null && this.m_StiffnessDistrib.keys.Length > 0)
				{
					particle.m_Stiffness *= this.m_StiffnessDistrib.Evaluate(time);
				}
				if (this.m_InertDistrib != null && this.m_InertDistrib.keys.Length > 0)
				{
					particle.m_Inert *= this.m_InertDistrib.Evaluate(time);
				}
				if (this.m_RadiusDistrib != null && this.m_RadiusDistrib.keys.Length > 0)
				{
					particle.m_Radius *= this.m_RadiusDistrib.Evaluate(time);
				}
			}
			particle.m_Damping = Mathf.Clamp01(particle.m_Damping);
			particle.m_Elasticity = Mathf.Clamp01(particle.m_Elasticity);
			particle.m_Stiffness = Mathf.Clamp01(particle.m_Stiffness);
			particle.m_Inert = Mathf.Clamp01(particle.m_Inert);
			particle.m_Radius = Mathf.Max(particle.m_Radius, 0f);
		}
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x00042974 File Offset: 0x00040D74
	private void AppendParticles(Transform b, int parentIndex, float boneLength)
	{
		DynamicBoneV2.Particle particle = new DynamicBoneV2.Particle();
		particle.m_Transform = b;
		particle.m_ParentIndex = parentIndex;
		if (b != null)
		{
			particle.m_Position = (particle.m_PrevPosition = b.position);
			particle.m_InitLocalPosition = b.localPosition;
			particle.m_InitLocalRotation = b.localRotation;
		}
		else
		{
			Transform transform = this.m_Particles[parentIndex].m_Transform;
			if (this.m_EndLength > 0f)
			{
				Transform parent = transform.parent;
				if (parent != null)
				{
					particle.m_EndOffset = transform.InverseTransformPoint(transform.position * 2f - parent.position) * this.m_EndLength;
				}
				else
				{
					particle.m_EndOffset = new Vector3(this.m_EndLength, 0f, 0f);
				}
			}
			else
			{
				particle.m_EndOffset = transform.InverseTransformPoint(base.transform.TransformDirection(this.m_EndOffset) + transform.position);
			}
			particle.m_Position = (particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset));
		}
		if (parentIndex >= 0)
		{
			boneLength += (this.m_Particles[parentIndex].m_Transform.position - particle.m_Position).magnitude;
			particle.m_BoneLength = boneLength;
			this.m_BoneTotalLength = Mathf.Max(this.m_BoneTotalLength, boneLength);
		}
		int count = this.m_Particles.Count;
		this.m_Particles.Add(particle);
		bool flag = false;
		int index = 0;
		if (b != null)
		{
			for (int i = 0; i < b.childCount; i++)
			{
				bool flag2 = false;
				if (this.m_Exclusions != null)
				{
					for (int j = 0; j < this.m_Exclusions.Count; j++)
					{
						Transform x = this.m_Exclusions[j];
						if (x == b.GetChild(i))
						{
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					for (int k = 0; k < this.m_notRolls.Count; k++)
					{
						Transform x2 = this.m_notRolls[k];
						if (x2 == b.GetChild(i))
						{
							flag = true;
							flag2 = true;
							index = i;
							break;
						}
					}
				}
				if (!flag2)
				{
					this.AppendParticles(b.GetChild(i), count, boneLength);
				}
			}
			if (flag)
			{
				for (int l = 0; l < b.GetChild(index).childCount; l++)
				{
					bool flag3 = false;
					for (int m = 0; m < this.m_Exclusions.Count; m++)
					{
						Transform x3 = this.m_Exclusions[m];
						if (x3 == b.GetChild(index).GetChild(l))
						{
							flag3 = true;
							break;
						}
					}
					if (!flag3)
					{
						for (int n = 0; n < this.m_notRolls.Count; n++)
						{
							Transform x4 = this.m_notRolls[n];
							if (x4 == b.GetChild(index).GetChild(l))
							{
								flag3 = true;
								break;
							}
						}
					}
					if (!flag3)
					{
						this.AppendParticles(b.GetChild(index).GetChild(l), count, boneLength);
					}
				}
			}
			if (b.childCount == 0 && (this.m_EndLength > 0f || this.m_EndOffset != Vector3.zero))
			{
				this.AppendParticles(null, count, boneLength);
			}
		}
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x00042D34 File Offset: 0x00041134
	private void InitTransforms()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBoneV2.Particle particle = this.m_Particles[i];
			if (particle.m_Transform != null)
			{
				particle.m_Transform.localPosition = particle.m_InitLocalPosition;
				particle.m_Transform.localRotation = particle.m_InitLocalRotation;
			}
		}
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x00042DA0 File Offset: 0x000411A0
	public void ResetParticlesPosition()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBoneV2.Particle particle = this.m_Particles[i];
			if (particle.m_Transform != null)
			{
				particle.m_Position = (particle.m_PrevPosition = particle.m_Transform.position);
			}
			else
			{
				Transform transform = this.m_Particles[particle.m_ParentIndex].m_Transform;
				particle.m_Position = (particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset));
			}
		}
		this.m_ObjectPrevPosition = base.transform.position;
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x00042E4C File Offset: 0x0004124C
	private void UpdateParticles1()
	{
		Vector3 vector = this.m_Gravity;
		Vector3 normalized = this.m_Gravity.normalized;
		Vector3 lhs = this.m_Root.TransformDirection(this.m_LocalGravity);
		Vector3 b = normalized * Mathf.Max(Vector3.Dot(lhs, normalized), 0f);
		vector -= b;
		vector = (vector + this.m_Force) * this.m_ObjectScale;
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBoneV2.Particle particle = this.m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				Vector3 a = particle.m_Position - particle.m_PrevPosition;
				Vector3 b2 = this.m_ObjectMove * particle.m_Inert;
				particle.m_PrevPosition = particle.m_Position + b2;
				particle.m_Position += a * (1f - particle.m_Damping) + vector + b2;
			}
			else
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.position;
			}
		}
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x00042F8C File Offset: 0x0004138C
	private void UpdateParticles2WithJob()
	{
		Plane plane = default(Plane);
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBoneV2.Particle particle = this.m_Particles[i];
			DynamicBoneV2.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
			float magnitude;
			if (particle.m_Transform != null)
			{
				magnitude = (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
			}
			else
			{
				magnitude = particle2.m_Transform.localToWorldMatrix.MultiplyVector(particle.m_EndOffset).magnitude;
			}
			float num = Mathf.Lerp(1f, particle.m_Stiffness, this.m_Weight);
			if (num > 0f || particle.m_Elasticity > 0f)
			{
				Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
				localToWorldMatrix.SetColumn(3, particle2.m_Position);
				Vector3 a;
				if (particle.m_Transform != null)
				{
					a = localToWorldMatrix.MultiplyPoint3x4(particle.m_Transform.localPosition);
				}
				else
				{
					a = localToWorldMatrix.MultiplyPoint3x4(particle.m_EndOffset);
				}
				Vector3 a2 = a - particle.m_Position;
				particle.m_Position += a2 * particle.m_Elasticity;
				if (num > 0f)
				{
					a2 = a - particle.m_Position;
					float magnitude2 = a2.magnitude;
					float num2 = magnitude * (1f - num) * 2f;
					if (magnitude2 > num2)
					{
						particle.m_Position += a2 * ((magnitude2 - num2) / magnitude2);
					}
				}
			}
			if (this.m_Colliders != null)
			{
				int count = this.m_Colliders.Count;
				NativeArray<DynamicBoneColliderBase.Bound> boundAry = new NativeArray<DynamicBoneColliderBase.Bound>(count, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<float> particleRadiusAry = new NativeArray<float>(count, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<float> capsuleHeightAry = new NativeArray<float>(count, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<Vector3> centerAry = new NativeArray<Vector3>(count, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<Vector3> c0Ary = new NativeArray<Vector3>(count, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<Vector3> c1Ary = new NativeArray<Vector3>(count, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<float> radiusAry = new NativeArray<float>(count, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<bool> enabledAry = new NativeArray<bool>(count, Allocator.Temp, NativeArrayOptions.ClearMemory);
				DynamicBoneV2.ColliderJob jobData = new DynamicBoneV2.ColliderJob
				{
					boundAry = boundAry,
					particlePosition = particle.m_Position,
					particleRadiusAry = particleRadiusAry,
					capsuleHeightAry = capsuleHeightAry,
					centerAry = centerAry,
					c0Ary = c0Ary,
					c1Ary = c1Ary,
					radiusAry = radiusAry,
					enabledAry = enabledAry
				};
				jobData.Schedule(count, 0, default(JobHandle)).Complete();
				particle.m_Position = jobData.particlePosition;
				boundAry.Dispose();
				particleRadiusAry.Dispose();
				capsuleHeightAry.Dispose();
				centerAry.Dispose();
				c0Ary.Dispose();
				c1Ary.Dispose();
				radiusAry.Dispose();
				enabledAry.Dispose();
			}
			if (this.m_FreezeAxis != DynamicBoneV2.FreezeAxis.None)
			{
				DynamicBoneV2.FreezeAxis freezeAxis = this.m_FreezeAxis;
				if (freezeAxis != DynamicBoneV2.FreezeAxis.X)
				{
					if (freezeAxis != DynamicBoneV2.FreezeAxis.Y)
					{
						if (freezeAxis == DynamicBoneV2.FreezeAxis.Z)
						{
							plane.SetNormalAndPosition(particle2.m_Transform.forward, particle2.m_Position);
						}
					}
					else
					{
						plane.SetNormalAndPosition(particle2.m_Transform.up, particle2.m_Position);
					}
				}
				else
				{
					plane.SetNormalAndPosition(particle2.m_Transform.right, particle2.m_Position);
				}
				particle.m_Position -= plane.normal * plane.GetDistanceToPoint(particle.m_Position);
			}
			Vector3 a3 = particle2.m_Position - particle.m_Position;
			float magnitude3 = a3.magnitude;
			if (magnitude3 > 0f)
			{
				particle.m_Position += a3 * ((magnitude3 - magnitude) / magnitude3);
			}
		}
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x00043384 File Offset: 0x00041784
	private void UpdateParticles2()
	{
		Plane plane = default(Plane);
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBoneV2.Particle particle = this.m_Particles[i];
			DynamicBoneV2.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
			float magnitude;
			if (particle.m_Transform != null)
			{
				magnitude = (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
			}
			else
			{
				magnitude = particle2.m_Transform.localToWorldMatrix.MultiplyVector(particle.m_EndOffset).magnitude;
			}
			float num = Mathf.Lerp(1f, particle.m_Stiffness, this.m_Weight);
			if (num > 0f || particle.m_Elasticity > 0f)
			{
				Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
				localToWorldMatrix.SetColumn(3, particle2.m_Position);
				Vector3 a;
				if (particle.m_Transform != null)
				{
					a = localToWorldMatrix.MultiplyPoint3x4(particle.m_Transform.localPosition);
				}
				else
				{
					a = localToWorldMatrix.MultiplyPoint3x4(particle.m_EndOffset);
				}
				Vector3 a2 = a - particle.m_Position;
				particle.m_Position += a2 * particle.m_Elasticity;
				if (num > 0f)
				{
					a2 = a - particle.m_Position;
					float magnitude2 = a2.magnitude;
					float num2 = magnitude * (1f - num) * 2f;
					if (magnitude2 > num2)
					{
						particle.m_Position += a2 * ((magnitude2 - num2) / magnitude2);
					}
				}
			}
			if (this.m_Colliders != null)
			{
				float particleRadius = particle.m_Radius * this.m_ObjectScale;
				for (int j = 0; j < this.m_Colliders.Count; j++)
				{
					DynamicBoneCollider dynamicBoneCollider = this.m_Colliders[j];
					if (dynamicBoneCollider != null && dynamicBoneCollider.enabled)
					{
						dynamicBoneCollider.Collide(ref particle.m_Position, particleRadius);
					}
				}
			}
			if (this.m_FreezeAxis != DynamicBoneV2.FreezeAxis.None)
			{
				DynamicBoneV2.FreezeAxis freezeAxis = this.m_FreezeAxis;
				if (freezeAxis != DynamicBoneV2.FreezeAxis.X)
				{
					if (freezeAxis != DynamicBoneV2.FreezeAxis.Y)
					{
						if (freezeAxis == DynamicBoneV2.FreezeAxis.Z)
						{
							plane.SetNormalAndPosition(particle2.m_Transform.forward, particle2.m_Position);
						}
					}
					else
					{
						plane.SetNormalAndPosition(particle2.m_Transform.up, particle2.m_Position);
					}
				}
				else
				{
					plane.SetNormalAndPosition(particle2.m_Transform.right, particle2.m_Position);
				}
				particle.m_Position -= plane.normal * plane.GetDistanceToPoint(particle.m_Position);
			}
			Vector3 a3 = particle2.m_Position - particle.m_Position;
			float magnitude3 = a3.magnitude;
			if (magnitude3 > 0f)
			{
				particle.m_Position += a3 * ((magnitude3 - magnitude) / magnitude3);
			}
		}
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x000436BC File Offset: 0x00041ABC
	private void SkipUpdateParticles()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBoneV2.Particle particle = this.m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				particle.m_PrevPosition += this.m_ObjectMove;
				particle.m_Position += this.m_ObjectMove;
				DynamicBoneV2.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
				float magnitude;
				if (particle.m_Transform != null)
				{
					magnitude = (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
				}
				else
				{
					magnitude = particle2.m_Transform.localToWorldMatrix.MultiplyVector(particle.m_EndOffset).magnitude;
				}
				float num = Mathf.Lerp(1f, particle.m_Stiffness, this.m_Weight);
				if (num > 0f)
				{
					Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
					localToWorldMatrix.SetColumn(3, particle2.m_Position);
					Vector3 a;
					if (particle.m_Transform != null)
					{
						a = localToWorldMatrix.MultiplyPoint3x4(particle.m_Transform.localPosition);
					}
					else
					{
						a = localToWorldMatrix.MultiplyPoint3x4(particle.m_EndOffset);
					}
					Vector3 a2 = a - particle.m_Position;
					float magnitude2 = a2.magnitude;
					float num2 = magnitude * (1f - num) * 2f;
					if (magnitude2 > num2)
					{
						particle.m_Position += a2 * ((magnitude2 - num2) / magnitude2);
					}
				}
				Vector3 a3 = particle2.m_Position - particle.m_Position;
				float magnitude3 = a3.magnitude;
				if (magnitude3 > 0f)
				{
					particle.m_Position += a3 * ((magnitude3 - magnitude) / magnitude3);
				}
			}
			else
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.position;
			}
		}
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x000438D6 File Offset: 0x00041CD6
	private static Vector3 MirrorVector(Vector3 v, Vector3 axis)
	{
		return v - axis * (Vector3.Dot(v, axis) * 2f);
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x000438F4 File Offset: 0x00041CF4
	private void ApplyParticlesToTransforms()
	{
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBoneV2.Particle particle = this.m_Particles[i];
			DynamicBoneV2.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
			if (particle2.m_Transform.childCount <= 1)
			{
				Vector3 direction;
				if (particle.m_Transform != null)
				{
					direction = particle.m_Transform.localPosition;
				}
				else
				{
					direction = particle.m_EndOffset;
				}
				Vector3 toDirection = particle.m_Position - particle2.m_Position;
				Quaternion lhs = Quaternion.FromToRotation(particle2.m_Transform.TransformDirection(direction), toDirection);
				particle2.m_Transform.rotation = lhs * particle2.m_Transform.rotation;
			}
			if (particle.m_Transform != null)
			{
				particle.m_Transform.position = particle.m_Position;
			}
		}
	}

	// Token: 0x04000DC1 RID: 3521
	public Transform m_Root;

	// Token: 0x04000DC2 RID: 3522
	public float m_UpdateRate = 60f;

	// Token: 0x04000DC3 RID: 3523
	public DynamicBoneV2.UpdateMode m_UpdateMode;

	// Token: 0x04000DC4 RID: 3524
	[Range(0f, 1f)]
	public float m_Damping = 0.1f;

	// Token: 0x04000DC5 RID: 3525
	public AnimationCurve m_DampingDistrib;

	// Token: 0x04000DC6 RID: 3526
	[Range(0f, 1f)]
	public float m_Elasticity = 0.1f;

	// Token: 0x04000DC7 RID: 3527
	public AnimationCurve m_ElasticityDistrib;

	// Token: 0x04000DC8 RID: 3528
	[Range(0f, 1f)]
	public float m_Stiffness = 0.1f;

	// Token: 0x04000DC9 RID: 3529
	public AnimationCurve m_StiffnessDistrib;

	// Token: 0x04000DCA RID: 3530
	[Range(0f, 1f)]
	public float m_Inert;

	// Token: 0x04000DCB RID: 3531
	public AnimationCurve m_InertDistrib;

	// Token: 0x04000DCC RID: 3532
	public float m_Radius;

	// Token: 0x04000DCD RID: 3533
	public AnimationCurve m_RadiusDistrib;

	// Token: 0x04000DCE RID: 3534
	public float m_EndLength;

	// Token: 0x04000DCF RID: 3535
	public Vector3 m_EndOffset = Vector3.zero;

	// Token: 0x04000DD0 RID: 3536
	public Vector3 m_Gravity = Vector3.zero;

	// Token: 0x04000DD1 RID: 3537
	public Vector3 m_Force = Vector3.zero;

	// Token: 0x04000DD2 RID: 3538
	public List<DynamicBoneCollider> m_Colliders;

	// Token: 0x04000DD3 RID: 3539
	public List<Transform> m_Exclusions;

	// Token: 0x04000DD4 RID: 3540
	public DynamicBoneV2.FreezeAxis m_FreezeAxis;

	// Token: 0x04000DD5 RID: 3541
	public bool m_DistantDisable;

	// Token: 0x04000DD6 RID: 3542
	public Transform m_ReferenceObject;

	// Token: 0x04000DD7 RID: 3543
	public float m_DistanceToObject = 20f;

	// Token: 0x04000DD8 RID: 3544
	public List<Transform> m_notRolls;

	// Token: 0x04000DD9 RID: 3545
	private Vector3 m_LocalGravity = Vector3.zero;

	// Token: 0x04000DDA RID: 3546
	private Vector3 m_ObjectMove = Vector3.zero;

	// Token: 0x04000DDB RID: 3547
	private Vector3 m_ObjectPrevPosition = Vector3.zero;

	// Token: 0x04000DDC RID: 3548
	private float m_BoneTotalLength;

	// Token: 0x04000DDD RID: 3549
	private float m_ObjectScale = 1f;

	// Token: 0x04000DDE RID: 3550
	private float m_Time;

	// Token: 0x04000DDF RID: 3551
	private float m_Weight = 1f;

	// Token: 0x04000DE0 RID: 3552
	private bool m_DistantDisabled;

	// Token: 0x04000DE1 RID: 3553
	private List<DynamicBoneV2.Particle> m_Particles = new List<DynamicBoneV2.Particle>();

	// Token: 0x0200031B RID: 795
	public enum UpdateMode
	{
		// Token: 0x04000DE3 RID: 3555
		Normal,
		// Token: 0x04000DE4 RID: 3556
		AnimatePhysics,
		// Token: 0x04000DE5 RID: 3557
		UnscaledTime
	}

	// Token: 0x0200031C RID: 796
	public enum FreezeAxis
	{
		// Token: 0x04000DE7 RID: 3559
		None,
		// Token: 0x04000DE8 RID: 3560
		X,
		// Token: 0x04000DE9 RID: 3561
		Y,
		// Token: 0x04000DEA RID: 3562
		Z
	}

	// Token: 0x0200031D RID: 797
	private class Particle
	{
		// Token: 0x04000DEB RID: 3563
		public Transform m_Transform;

		// Token: 0x04000DEC RID: 3564
		public int m_ParentIndex = -1;

		// Token: 0x04000DED RID: 3565
		public float m_Damping;

		// Token: 0x04000DEE RID: 3566
		public float m_Elasticity;

		// Token: 0x04000DEF RID: 3567
		public float m_Stiffness;

		// Token: 0x04000DF0 RID: 3568
		public float m_Inert;

		// Token: 0x04000DF1 RID: 3569
		public float m_Radius;

		// Token: 0x04000DF2 RID: 3570
		public float m_BoneLength;

		// Token: 0x04000DF3 RID: 3571
		public Vector3 m_Position = Vector3.zero;

		// Token: 0x04000DF4 RID: 3572
		public Vector3 m_PrevPosition = Vector3.zero;

		// Token: 0x04000DF5 RID: 3573
		public Vector3 m_EndOffset = Vector3.zero;

		// Token: 0x04000DF6 RID: 3574
		public Vector3 m_InitLocalPosition = Vector3.zero;

		// Token: 0x04000DF7 RID: 3575
		public Quaternion m_InitLocalRotation = Quaternion.identity;
	}

	// Token: 0x0200031E RID: 798
	private struct ColliderJob : IJobParallelFor
	{
		// Token: 0x06000DF7 RID: 3575 RVA: 0x00043A54 File Offset: 0x00041E54
		public void Execute(int index)
		{
			if (this.capsuleHeightAry[index] <= 0f)
			{
				Vector3 a = this.particlePosition - this.centerAry[index];
				float sqrMagnitude = a.sqrMagnitude;
				if (this.boundAry[index] == DynamicBoneColliderBase.Bound.Outside)
				{
					float num = this.radiusAry[index] + this.particleRadiusAry[index];
					float num2 = num * num;
					if (sqrMagnitude > 0f && sqrMagnitude < num2)
					{
						float num3 = Mathf.Sqrt(sqrMagnitude);
						this.particlePosition = this.centerAry[index] + a * (num / num3);
					}
				}
				else
				{
					float num4 = this.radiusAry[index] - this.particleRadiusAry[index];
					float num5 = num4 * num4;
					if (sqrMagnitude > num5)
					{
						float num6 = Mathf.Sqrt(sqrMagnitude);
						this.particlePosition = this.centerAry[index] + a * (num4 / num6);
					}
				}
			}
			else if (this.boundAry[index] == DynamicBoneColliderBase.Bound.Outside)
			{
				float num7 = this.radiusAry[index] + this.particleRadiusAry[index];
				float num8 = num7 * num7;
				Vector3 vector = this.c1Ary[index] - this.c0Ary[index];
				Vector3 vector2 = this.particlePosition - this.c0Ary[index];
				float num9 = Vector3.Dot(vector2, vector);
				if (num9 <= 0f)
				{
					float sqrMagnitude2 = vector2.sqrMagnitude;
					if (sqrMagnitude2 > 0f && sqrMagnitude2 < num8)
					{
						float num10 = Mathf.Sqrt(sqrMagnitude2);
						this.particlePosition = this.c0Ary[index] + vector2 * (num7 / num10);
					}
				}
				else
				{
					float sqrMagnitude3 = vector.sqrMagnitude;
					if (num9 >= sqrMagnitude3)
					{
						vector2 = this.particlePosition - this.c1Ary[index];
						float sqrMagnitude4 = vector2.sqrMagnitude;
						if (sqrMagnitude4 > 0f && sqrMagnitude4 < num8)
						{
							float num11 = Mathf.Sqrt(sqrMagnitude4);
							this.particlePosition = this.c1Ary[index] + vector2 * (num7 / num11);
						}
					}
					else if (sqrMagnitude3 > 0f)
					{
						num9 /= sqrMagnitude3;
						vector2 -= vector * num9;
						float sqrMagnitude5 = vector2.sqrMagnitude;
						if (sqrMagnitude5 > 0f && sqrMagnitude5 < num8)
						{
							float num12 = Mathf.Sqrt(sqrMagnitude5);
							this.particlePosition += vector2 * ((num7 - num12) / num12);
						}
					}
				}
			}
			else
			{
				float num13 = this.radiusAry[index] - this.particleRadiusAry[index];
				float num14 = num13 * num13;
				Vector3 vector3 = this.c1Ary[index] - this.c0Ary[index];
				Vector3 vector4 = this.particlePosition - this.c0Ary[index];
				float num15 = Vector3.Dot(vector4, vector3);
				if (num15 <= 0f)
				{
					float sqrMagnitude6 = vector4.sqrMagnitude;
					if (sqrMagnitude6 > num14)
					{
						float num16 = Mathf.Sqrt(sqrMagnitude6);
						this.particlePosition = this.c0Ary[index] + vector4 * (num13 / num16);
					}
				}
				else
				{
					float sqrMagnitude7 = vector3.sqrMagnitude;
					if (num15 >= sqrMagnitude7)
					{
						vector4 = this.particlePosition - this.c1Ary[index];
						float sqrMagnitude8 = vector4.sqrMagnitude;
						if (sqrMagnitude8 > num14)
						{
							float num17 = Mathf.Sqrt(sqrMagnitude8);
							this.particlePosition = this.c1Ary[index] + vector4 * (num13 / num17);
						}
					}
					else if (sqrMagnitude7 > 0f)
					{
						num15 /= sqrMagnitude7;
						vector4 -= vector3 * num15;
						float sqrMagnitude9 = vector4.sqrMagnitude;
						if (sqrMagnitude9 > num14)
						{
							float num18 = Mathf.Sqrt(sqrMagnitude9);
							this.particlePosition += vector4 * ((num13 - num18) / num18);
						}
					}
				}
			}
		}

		// Token: 0x04000DF8 RID: 3576
		[ReadOnly]
		public NativeArray<DynamicBoneColliderBase.Bound> boundAry;

		// Token: 0x04000DF9 RID: 3577
		public Vector3 particlePosition;

		// Token: 0x04000DFA RID: 3578
		[ReadOnly]
		public NativeArray<float> particleRadiusAry;

		// Token: 0x04000DFB RID: 3579
		[ReadOnly]
		public NativeArray<float> capsuleHeightAry;

		// Token: 0x04000DFC RID: 3580
		[ReadOnly]
		public NativeArray<Vector3> centerAry;

		// Token: 0x04000DFD RID: 3581
		[ReadOnly]
		public NativeArray<Vector3> c0Ary;

		// Token: 0x04000DFE RID: 3582
		[ReadOnly]
		public NativeArray<Vector3> c1Ary;

		// Token: 0x04000DFF RID: 3583
		[ReadOnly]
		public NativeArray<float> radiusAry;

		// Token: 0x04000E00 RID: 3584
		[ReadOnly]
		public NativeArray<bool> enabledAry;
	}
}
