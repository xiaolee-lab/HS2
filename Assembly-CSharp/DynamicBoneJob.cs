using System;
using System.Collections.Generic;
using UniRx;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

// Token: 0x0200030D RID: 781
[AddComponentMenu("Dynamic Bone/Dynamic Bone Job")]
public class DynamicBoneJob : MonoBehaviour
{
	// Token: 0x06000DA2 RID: 3490 RVA: 0x0003F3FC File Offset: 0x0003D7FC
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
		this.CreateNativeArray();
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x0003F47B File Offset: 0x0003D87B
	private void FixedUpdate()
	{
		if (this.m_UpdateMode == DynamicBoneJob.UpdateMode.AnimatePhysics)
		{
			this.PreUpdate();
		}
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x0003F48F File Offset: 0x0003D88F
	private void OnUpdate()
	{
		if (this.m_UpdateMode != DynamicBoneJob.UpdateMode.AnimatePhysics)
		{
			this.PreUpdate();
		}
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x0003F4A4 File Offset: 0x0003D8A4
	private void OnLateUpdate()
	{
		if (this.m_DistantDisable)
		{
			this.CheckDistance();
		}
		if (this.m_Weight > 0f && (!this.m_DistantDisable || !this.m_DistantDisabled))
		{
			this.UpdateDynamicBones(Time.deltaTime);
		}
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x0003F4F3 File Offset: 0x0003D8F3
	private void PreUpdate()
	{
		if (this.m_Weight > 0f && (!this.m_DistantDisable || !this.m_DistantDisabled))
		{
			this.InitTransforms();
		}
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x0003F524 File Offset: 0x0003D924
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

	// Token: 0x06000DA8 RID: 3496 RVA: 0x0003F5BA File Offset: 0x0003D9BA
	private void OnEnable()
	{
		this.ResetParticlesPosition();
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x0003F5C2 File Offset: 0x0003D9C2
	private void OnDisable()
	{
		this.InitTransforms();
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x0003F5CC File Offset: 0x0003D9CC
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

	// Token: 0x06000DAB RID: 3499 RVA: 0x0003F66C File Offset: 0x0003DA6C
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
			DynamicBoneJob.Particle particle = this.m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				DynamicBoneJob.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
				Gizmos.DrawLine(particle.m_Position, particle2.m_Position);
			}
			if (particle.m_Radius > 0f)
			{
				Gizmos.DrawWireSphere(particle.m_Position, particle.m_Radius * this.m_ObjectScale);
			}
		}
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x0003F750 File Offset: 0x0003DB50
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

	// Token: 0x06000DAD RID: 3501 RVA: 0x0003F79C File Offset: 0x0003DB9C
	public float GetWeight()
	{
		return this.m_Weight;
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x0003F7A4 File Offset: 0x0003DBA4
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
				this.UpdateParticles4ForIJob();
				this.m_ObjectMove = Vector3.zero;
			}
		}
		else
		{
			this.SkipUpdateParticles();
		}
		this.ApplyParticlesToTransforms();
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x0003F8B8 File Offset: 0x0003DCB8
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
		this.UpdateParameters();
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x0003F958 File Offset: 0x0003DD58
	private void AppendParticles(Transform b, int parentIndex, float boneLength)
	{
		DynamicBoneJob.Particle particle = new DynamicBoneJob.Particle();
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
				else if (this.m_EndLength > 0f || this.m_EndOffset != Vector3.zero)
				{
					this.AppendParticles(null, count, boneLength);
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

	// Token: 0x06000DB1 RID: 3505 RVA: 0x0003FD4C File Offset: 0x0003E14C
	public void UpdateParameters()
	{
		if (this.m_Root == null)
		{
			return;
		}
		this.m_LocalGravity = this.m_Root.InverseTransformDirection(this.m_Gravity);
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBoneJob.Particle particle = this.m_Particles[i];
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

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0003FF74 File Offset: 0x0003E374
	private void InitTransforms()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBoneJob.Particle particle = this.m_Particles[i];
			if (particle.m_Transform != null)
			{
				particle.m_Transform.localPosition = particle.m_InitLocalPosition;
				particle.m_Transform.localRotation = particle.m_InitLocalRotation;
			}
		}
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x0003FFE0 File Offset: 0x0003E3E0
	public void ResetParticlesPosition()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBoneJob.Particle particle = this.m_Particles[i];
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

	// Token: 0x06000DB4 RID: 3508 RVA: 0x0004008C File Offset: 0x0003E48C
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
			DynamicBoneJob.Particle particle = this.m_Particles[i];
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

	// Token: 0x06000DB5 RID: 3509 RVA: 0x000401CC File Offset: 0x0003E5CC
	private void UpdateParticles2()
	{
		Plane plane = default(Plane);
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBoneJob.Particle particle = this.m_Particles[i];
			DynamicBoneJob.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
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
					DynamicBoneColliderBase dynamicBoneColliderBase = this.m_Colliders[j];
					if (dynamicBoneColliderBase != null && dynamicBoneColliderBase.enabled)
					{
						dynamicBoneColliderBase.Collide(ref particle.m_Position, particleRadius);
					}
				}
			}
			if (this.m_FreezeAxis != DynamicBoneJob.FreezeAxis.None)
			{
				DynamicBoneJob.FreezeAxis freezeAxis = this.m_FreezeAxis;
				if (freezeAxis != DynamicBoneJob.FreezeAxis.X)
				{
					if (freezeAxis != DynamicBoneJob.FreezeAxis.Y)
					{
						if (freezeAxis == DynamicBoneJob.FreezeAxis.Z)
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

	// Token: 0x06000DB6 RID: 3510 RVA: 0x00040504 File Offset: 0x0003E904
	private void SkipUpdateParticles()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBoneJob.Particle particle = this.m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				particle.m_PrevPosition += this.m_ObjectMove;
				particle.m_Position += this.m_ObjectMove;
				DynamicBoneJob.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
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

	// Token: 0x06000DB7 RID: 3511 RVA: 0x0004071E File Offset: 0x0003EB1E
	private static Vector3 MirrorVector(Vector3 v, Vector3 axis)
	{
		return v - axis * (Vector3.Dot(v, axis) * 2f);
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x0004073C File Offset: 0x0003EB3C
	private void ApplyParticlesToTransforms()
	{
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBoneJob.Particle particle = this.m_Particles[i];
			DynamicBoneJob.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
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

	// Token: 0x06000DB9 RID: 3513 RVA: 0x00040828 File Offset: 0x0003EC28
	private void UpdateParticles2ForJobSystem()
	{
		Plane plane = default(Plane);
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBoneJob.Particle particle = this.m_Particles[i];
			DynamicBoneJob.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
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
				NativeArray<Vector3> nativeArray = new NativeArray<Vector3>(count, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<int> nativeArray2 = new NativeArray<int>(count, Allocator.Temp, NativeArrayOptions.ClearMemory);
				nativeArray.Dispose();
				nativeArray2.Dispose();
			}
			if (this.m_FreezeAxis != DynamicBoneJob.FreezeAxis.None)
			{
				DynamicBoneJob.FreezeAxis freezeAxis = this.m_FreezeAxis;
				if (freezeAxis != DynamicBoneJob.FreezeAxis.X)
				{
					if (freezeAxis != DynamicBoneJob.FreezeAxis.Y)
					{
						if (freezeAxis == DynamicBoneJob.FreezeAxis.Z)
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

	// Token: 0x06000DBA RID: 3514 RVA: 0x00040B28 File Offset: 0x0003EF28
	private void UpdateParticles2ForIJob()
	{
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBoneJob.Particle particle = this.m_Particles[i];
			DynamicBoneJob.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
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
			Vector3 a3 = particle2.m_Position - particle.m_Position;
			float magnitude3 = a3.magnitude;
			if (magnitude3 > 0f)
			{
				particle.m_Position += a3 * ((magnitude3 - magnitude) / magnitude3);
			}
		}
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x00040D30 File Offset: 0x0003F130
	private void UpdateParticles3ForIJob()
	{
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBoneJob.Particle particle = this.m_Particles[i];
			DynamicBoneJob.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
			float magnitude;
			if (particle.m_Transform != null)
			{
				magnitude = (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
			}
			else
			{
				magnitude = particle2.m_Transform.localToWorldMatrix.MultiplyVector(particle.m_EndOffset).magnitude;
			}
			if (this.m_Colliders != null)
			{
			}
			Vector3 a = particle2.m_Position - particle.m_Position;
			float magnitude2 = a.magnitude;
			if (magnitude2 > 0f)
			{
				particle.m_Position += a * ((magnitude2 - magnitude) / magnitude2);
			}
		}
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x00040E2C File Offset: 0x0003F22C
	private void UpdateParticles4ForIJob()
	{
		NativeArray<DynamicBoneJob.ParticleStruct> calcs = new NativeArray<DynamicBoneJob.ParticleStruct>(this.m_Particles.Count, Allocator.Temp, NativeArrayOptions.ClearMemory);
		NativeArray<DynamicBoneJob.CollisionStruct> colls = new NativeArray<DynamicBoneJob.CollisionStruct>(this.m_Colliders.Count, Allocator.Temp, NativeArrayOptions.ClearMemory);
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBoneJob.Particle particle = this.m_Particles[i];
			calcs[i] = new DynamicBoneJob.ParticleStruct
			{
				parentIndex = particle.m_ParentIndex,
				damping = particle.m_Damping,
				elasticity = particle.m_Elasticity,
				stiffness = particle.m_Stiffness,
				inert = particle.m_Inert,
				radius = particle.m_Radius,
				boneLength = particle.m_BoneLength,
				isTransform = ((!particle.m_Transform) ? 0 : 1),
				transWorldPosition = ((!particle.m_Transform) ? Vector3.zero : particle.m_Transform.position),
				transLocalPosition = ((!particle.m_Transform) ? Vector3.zero : particle.m_Transform.localPosition),
				position = particle.m_Position,
				prevPosition = particle.m_PrevPosition,
				endOffset = particle.m_EndOffset,
				initLocalPosition = particle.m_InitLocalPosition,
				initLocalRotation = particle.m_InitLocalRotation,
				worldMatrix = ((!particle.m_Transform) ? Matrix4x4.identity : particle.m_Transform.localToWorldMatrix)
			};
		}
		for (int j = 0; j < this.m_Colliders.Count; j++)
		{
			DynamicBoneCollider dynamicBoneCollider = this.m_Colliders[j] as DynamicBoneCollider;
			if (!(dynamicBoneCollider == null))
			{
				if (dynamicBoneCollider.enabled)
				{
					colls[j] = new DynamicBoneJob.CollisionStruct
					{
						direction = dynamicBoneCollider.m_Direction,
						center = dynamicBoneCollider.m_Center,
						bound = dynamicBoneCollider.m_Bound,
						radius = dynamicBoneCollider.m_Radius,
						height = dynamicBoneCollider.m_Height,
						lossyScale = dynamicBoneCollider.transform.lossyScale,
						worldMatrix = dynamicBoneCollider.transform.localToWorldMatrix
					};
				}
			}
		}
		DynamicBoneJob.CalcJob4 jobData = new DynamicBoneJob.CalcJob4
		{
			calcs = calcs,
			colls = colls,
			weight = this.m_Weight,
			objectScale = this.m_ObjectScale
		};
		jobData.Schedule(default(JobHandle)).Complete();
		for (int k = 0; k < this.m_Particles.Count; k++)
		{
			this.m_Particles[k].m_Position = calcs[k].position;
		}
		calcs.Dispose();
		colls.Dispose();
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x00041152 File Offset: 0x0003F552
	private void OnDestroy()
	{
		this.JobHandle.Complete();
		if (this.datas.IsCreated)
		{
			this.datas.Dispose();
		}
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x0004117C File Offset: 0x0003F57C
	public void CreateNativeArray()
	{
		if (this.datas.IsCreated)
		{
			this.datas.Dispose();
		}
		if (this.m_Colliders.Count != 0)
		{
			this.datas = new NativeArray<DynamicBoneJob.CalcData>(this.m_Colliders.Count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < this.m_Colliders.Count; i++)
			{
				if (!(this.m_Colliders[i] == null))
				{
					this.datas[i] = new DynamicBoneJob.CalcData
					{
						direction = this.m_Colliders[i].m_Direction,
						center = this.m_Colliders[i].m_Center,
						bound = this.m_Colliders[i].m_Bound,
						radius = (this.m_Colliders[i] as DynamicBoneCollider).m_Radius,
						height = (this.m_Colliders[i] as DynamicBoneCollider).m_Height * 0.5f - (this.m_Colliders[i] as DynamicBoneCollider).m_Radius
					};
				}
			}
		}
	}

	// Token: 0x04000D43 RID: 3395
	public Transform m_Root;

	// Token: 0x04000D44 RID: 3396
	public float m_UpdateRate = 60f;

	// Token: 0x04000D45 RID: 3397
	public DynamicBoneJob.UpdateMode m_UpdateMode;

	// Token: 0x04000D46 RID: 3398
	[Range(0f, 1f)]
	public float m_Damping = 0.1f;

	// Token: 0x04000D47 RID: 3399
	public AnimationCurve m_DampingDistrib;

	// Token: 0x04000D48 RID: 3400
	[Range(0f, 1f)]
	public float m_Elasticity = 0.1f;

	// Token: 0x04000D49 RID: 3401
	public AnimationCurve m_ElasticityDistrib;

	// Token: 0x04000D4A RID: 3402
	[Range(0f, 1f)]
	public float m_Stiffness = 0.1f;

	// Token: 0x04000D4B RID: 3403
	public AnimationCurve m_StiffnessDistrib;

	// Token: 0x04000D4C RID: 3404
	[Range(0f, 1f)]
	public float m_Inert;

	// Token: 0x04000D4D RID: 3405
	public AnimationCurve m_InertDistrib;

	// Token: 0x04000D4E RID: 3406
	public float m_Radius;

	// Token: 0x04000D4F RID: 3407
	public AnimationCurve m_RadiusDistrib;

	// Token: 0x04000D50 RID: 3408
	public float m_EndLength;

	// Token: 0x04000D51 RID: 3409
	public Vector3 m_EndOffset = Vector3.zero;

	// Token: 0x04000D52 RID: 3410
	public Vector3 m_Gravity = Vector3.zero;

	// Token: 0x04000D53 RID: 3411
	public Vector3 m_Force = Vector3.zero;

	// Token: 0x04000D54 RID: 3412
	public List<DynamicBoneColliderBase> m_Colliders;

	// Token: 0x04000D55 RID: 3413
	public List<Transform> m_Exclusions;

	// Token: 0x04000D56 RID: 3414
	public DynamicBoneJob.FreezeAxis m_FreezeAxis;

	// Token: 0x04000D57 RID: 3415
	public bool m_DistantDisable;

	// Token: 0x04000D58 RID: 3416
	public Transform m_ReferenceObject;

	// Token: 0x04000D59 RID: 3417
	public float m_DistanceToObject = 20f;

	// Token: 0x04000D5A RID: 3418
	public List<Transform> m_notRolls;

	// Token: 0x04000D5B RID: 3419
	private Vector3 m_LocalGravity = Vector3.zero;

	// Token: 0x04000D5C RID: 3420
	private Vector3 m_ObjectMove = Vector3.zero;

	// Token: 0x04000D5D RID: 3421
	private Vector3 m_ObjectPrevPosition = Vector3.zero;

	// Token: 0x04000D5E RID: 3422
	private float m_BoneTotalLength;

	// Token: 0x04000D5F RID: 3423
	private float m_ObjectScale = 1f;

	// Token: 0x04000D60 RID: 3424
	private float m_Time;

	// Token: 0x04000D61 RID: 3425
	private float m_Weight = 1f;

	// Token: 0x04000D62 RID: 3426
	private bool m_DistantDisabled;

	// Token: 0x04000D63 RID: 3427
	private List<DynamicBoneJob.Particle> m_Particles = new List<DynamicBoneJob.Particle>();

	// Token: 0x04000D64 RID: 3428
	private JobHandle JobHandle;

	// Token: 0x04000D65 RID: 3429
	private NativeArray<DynamicBoneJob.CalcData> datas;

	// Token: 0x0200030E RID: 782
	public enum UpdateMode
	{
		// Token: 0x04000D67 RID: 3431
		Normal,
		// Token: 0x04000D68 RID: 3432
		AnimatePhysics,
		// Token: 0x04000D69 RID: 3433
		UnscaledTime
	}

	// Token: 0x0200030F RID: 783
	public enum FreezeAxis
	{
		// Token: 0x04000D6B RID: 3435
		None,
		// Token: 0x04000D6C RID: 3436
		X,
		// Token: 0x04000D6D RID: 3437
		Y,
		// Token: 0x04000D6E RID: 3438
		Z
	}

	// Token: 0x02000310 RID: 784
	private class Particle
	{
		// Token: 0x04000D6F RID: 3439
		public Transform m_Transform;

		// Token: 0x04000D70 RID: 3440
		public int m_ParentIndex = -1;

		// Token: 0x04000D71 RID: 3441
		public float m_Damping;

		// Token: 0x04000D72 RID: 3442
		public float m_Elasticity;

		// Token: 0x04000D73 RID: 3443
		public float m_Stiffness;

		// Token: 0x04000D74 RID: 3444
		public float m_Inert;

		// Token: 0x04000D75 RID: 3445
		public float m_Radius;

		// Token: 0x04000D76 RID: 3446
		public float m_BoneLength;

		// Token: 0x04000D77 RID: 3447
		public Vector3 m_Position = Vector3.zero;

		// Token: 0x04000D78 RID: 3448
		public Vector3 m_PrevPosition = Vector3.zero;

		// Token: 0x04000D79 RID: 3449
		public Vector3 m_EndOffset = Vector3.zero;

		// Token: 0x04000D7A RID: 3450
		public Vector3 m_InitLocalPosition = Vector3.zero;

		// Token: 0x04000D7B RID: 3451
		public Quaternion m_InitLocalRotation = Quaternion.identity;
	}

	// Token: 0x02000311 RID: 785
	public struct CalcData
	{
		// Token: 0x06000DC4 RID: 3524 RVA: 0x00041329 File Offset: 0x0003F729
		public void SetDirection(DynamicBoneColliderBase.Direction _dir)
		{
			this.direction = _dir;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00041332 File Offset: 0x0003F732
		public void SetCenter(Vector3 _center)
		{
			this.center = _center;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0004133B File Offset: 0x0003F73B
		public void SetBound(DynamicBoneColliderBase.Bound _bound)
		{
			this.bound = _bound;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00041344 File Offset: 0x0003F744
		public void SetRadius(float _radius)
		{
			this.radius = _radius;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0004134D File Offset: 0x0003F74D
		public void SetHeight(float _height)
		{
			this.height = _height;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00041356 File Offset: 0x0003F756
		public void SetParticlePosition(Vector3 _particlePosition)
		{
			this.particlePosition = _particlePosition;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0004135F File Offset: 0x0003F75F
		public void SetParticleRadius(float _particleRadius)
		{
			this.particleRadius = _particleRadius;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00041368 File Offset: 0x0003F768
		public void SetLossyScale(float _lossyScale)
		{
			this.lossyScale = _lossyScale;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00041371 File Offset: 0x0003F771
		public void SetWorldMatrix(Matrix4x4 _worldMatrix)
		{
			this.worldMatrix = _worldMatrix;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0004137A File Offset: 0x0003F77A
		public void SetPos0(Vector3 _pos)
		{
			this.pos0 = _pos;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00041383 File Offset: 0x0003F783
		public void SetPos1(Vector3 _pos)
		{
			this.pos1 = _pos;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0004138C File Offset: 0x0003F78C
		public void SetPos2(Vector3 _pos)
		{
			this.pos2 = _pos;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00041395 File Offset: 0x0003F795
		public void SetIsHit(int _isHit)
		{
			this.isHit = _isHit;
		}

		// Token: 0x04000D7C RID: 3452
		public DynamicBoneColliderBase.Direction direction;

		// Token: 0x04000D7D RID: 3453
		public Vector3 center;

		// Token: 0x04000D7E RID: 3454
		public DynamicBoneColliderBase.Bound bound;

		// Token: 0x04000D7F RID: 3455
		public float radius;

		// Token: 0x04000D80 RID: 3456
		public float height;

		// Token: 0x04000D81 RID: 3457
		public Vector3 particlePosition;

		// Token: 0x04000D82 RID: 3458
		public float particleRadius;

		// Token: 0x04000D83 RID: 3459
		public float lossyScale;

		// Token: 0x04000D84 RID: 3460
		public Matrix4x4 worldMatrix;

		// Token: 0x04000D85 RID: 3461
		public Vector3 pos0;

		// Token: 0x04000D86 RID: 3462
		public Vector3 pos1;

		// Token: 0x04000D87 RID: 3463
		public Vector3 pos2;

		// Token: 0x04000D88 RID: 3464
		public int isHit;
	}

	// Token: 0x02000312 RID: 786
	public struct NowData
	{
		// Token: 0x04000D89 RID: 3465
		public Vector3 particlePosition;

		// Token: 0x04000D8A RID: 3466
		public float particleRadius;

		// Token: 0x04000D8B RID: 3467
		public float lossyScale;

		// Token: 0x04000D8C RID: 3468
		public Vector3 pos0;

		// Token: 0x04000D8D RID: 3469
		public Vector3 pos1;

		// Token: 0x04000D8E RID: 3470
		public Vector3 pos2;

		// Token: 0x04000D8F RID: 3471
		public int isHit;

		// Token: 0x04000D90 RID: 3472
		public Matrix4x4 worldMatrix;
	}

	// Token: 0x02000313 RID: 787
	[BurstCompile]
	private struct Parallel : IJobParallelFor
	{
		// Token: 0x06000DD1 RID: 3537 RVA: 0x000413A0 File Offset: 0x0003F7A0
		public void Execute(int index)
		{
			DynamicBoneJob.CalcData calcData = this.calcs[index];
			float num = calcData.radius * Mathf.Abs(calcData.lossyScale);
			Matrix4x4 identity = Matrix4x4.identity;
			identity.SetTRS(this.positions[index], this.rotations[index], this.lossyScales[index]);
			if (calcData.height <= 0f)
			{
				Vector3 vector = identity * calcData.center;
				if (calcData.bound == DynamicBoneColliderBase.Bound.Outside)
				{
					float num2 = num + this.particleRadius;
					float num3 = num2 * num2;
					Vector3 a = this.particlePosition - vector;
					float sqrMagnitude = a.sqrMagnitude;
					if (sqrMagnitude > 0f && sqrMagnitude < num3)
					{
						float num4 = Mathf.Sqrt(sqrMagnitude);
						this.particlePosition = vector + a * (num2 / num4);
						this.isHits[index] = 1;
					}
					else
					{
						this.isHits[index] = 0;
					}
				}
				else
				{
					float num5 = num - this.particleRadius;
					float num6 = num5 * num5;
					Vector3 a2 = this.particlePosition - vector;
					float sqrMagnitude2 = a2.sqrMagnitude;
					if (sqrMagnitude2 > num6)
					{
						float num7 = Mathf.Sqrt(sqrMagnitude2);
						this.particlePosition = vector + a2 * (num5 / num7);
						this.isHits[index] = 1;
					}
					else
					{
						this.isHits[index] = 0;
					}
				}
			}
			else
			{
				Vector3 vector2 = calcData.center;
				Vector3 vector3 = calcData.center;
				DynamicBoneColliderBase.Direction direction = calcData.direction;
				if (direction != DynamicBoneColliderBase.Direction.X)
				{
					if (direction != DynamicBoneColliderBase.Direction.Y)
					{
						if (direction == DynamicBoneColliderBase.Direction.Z)
						{
							vector2.z -= calcData.height;
							vector3.z += calcData.height;
						}
					}
					else
					{
						vector2.y -= calcData.height;
						vector3.y += calcData.height;
					}
				}
				else
				{
					vector2.x -= calcData.height;
					vector3.x += calcData.height;
				}
				vector2 = identity * vector2;
				vector3 = identity * vector3;
				if (calcData.bound == DynamicBoneColliderBase.Bound.Outside)
				{
					float num8 = num + this.particleRadius;
					float num9 = num8 * num8;
					Vector3 vector4 = vector3 - vector2;
					Vector3 vector5 = this.particlePosition - vector2;
					float num10 = Vector3.Dot(vector5, vector4);
					if (num10 <= 0f)
					{
						float sqrMagnitude3 = vector5.sqrMagnitude;
						if (sqrMagnitude3 > 0f && sqrMagnitude3 < num9)
						{
							float num11 = Mathf.Sqrt(sqrMagnitude3);
							this.particlePosition = vector2 + vector5 * (num8 / num11);
							this.isHits[index] = 1;
						}
						else
						{
							this.isHits[index] = 0;
						}
					}
					else
					{
						float sqrMagnitude4 = vector4.sqrMagnitude;
						if (num10 >= sqrMagnitude4)
						{
							vector5 = this.particlePosition - vector3;
							float sqrMagnitude5 = vector5.sqrMagnitude;
							if (sqrMagnitude5 > 0f && sqrMagnitude5 < num9)
							{
								float num12 = Mathf.Sqrt(sqrMagnitude5);
								this.particlePosition = vector3 + vector5 * (num8 / num12);
								this.isHits[index] = 1;
							}
						}
						else if (sqrMagnitude4 > 0f)
						{
							num10 /= sqrMagnitude4;
							vector5 -= vector4 * num10;
							float sqrMagnitude6 = vector5.sqrMagnitude;
							if (sqrMagnitude6 > 0f && sqrMagnitude6 < num9)
							{
								float num13 = Mathf.Sqrt(sqrMagnitude6);
								this.particlePosition += vector5 * ((num8 - num13) / num13);
								this.isHits[index] = 1;
							}
						}
						else
						{
							this.isHits[index] = 0;
						}
					}
				}
				else
				{
					float num14 = num - this.particleRadius;
					float num15 = num14 * num14;
					Vector3 vector6 = vector3 - vector2;
					Vector3 vector7 = this.particlePosition - vector2;
					float num16 = Vector3.Dot(vector7, vector6);
					if (num16 <= 0f)
					{
						float sqrMagnitude7 = vector7.sqrMagnitude;
						if (sqrMagnitude7 > num15)
						{
							float num17 = Mathf.Sqrt(sqrMagnitude7);
							this.particlePosition = vector2 + vector7 * (num14 / num17);
							this.isHits[index] = 1;
						}
						else
						{
							this.isHits[index] = 0;
						}
					}
					else
					{
						float sqrMagnitude8 = vector6.sqrMagnitude;
						if (num16 >= sqrMagnitude8)
						{
							vector7 = this.particlePosition - vector3;
							float sqrMagnitude9 = vector7.sqrMagnitude;
							if (sqrMagnitude9 > num15)
							{
								float num18 = Mathf.Sqrt(sqrMagnitude9);
								this.particlePosition = vector3 + vector7 * (num14 / num18);
								this.isHits[index] = 1;
							}
						}
						else if (sqrMagnitude8 > 0f)
						{
							num16 /= sqrMagnitude8;
							vector7 -= vector6 * num16;
							float sqrMagnitude10 = vector7.sqrMagnitude;
							if (sqrMagnitude10 > num15)
							{
								float num19 = Mathf.Sqrt(sqrMagnitude10);
								this.particlePosition += vector7 * ((num14 - num19) / num19);
								this.isHits[index] = 1;
							}
						}
						else
						{
							this.isHits[index] = 0;
						}
					}
				}
			}
		}

		// Token: 0x04000D91 RID: 3473
		public Vector3 particlePosition;

		// Token: 0x04000D92 RID: 3474
		public float particleRadius;

		// Token: 0x04000D93 RID: 3475
		public NativeArray<DynamicBoneJob.CalcData> calcs;

		// Token: 0x04000D94 RID: 3476
		public NativeArray<Vector3> positions;

		// Token: 0x04000D95 RID: 3477
		public NativeArray<Quaternion> rotations;

		// Token: 0x04000D96 RID: 3478
		public NativeArray<Vector3> lossyScales;

		// Token: 0x04000D97 RID: 3479
		public NativeArray<int> isHits;
	}

	// Token: 0x02000314 RID: 788
	[BurstCompile]
	private struct CalcJob2 : IJob
	{
		// Token: 0x06000DD2 RID: 3538 RVA: 0x0004194E File Offset: 0x0003FD4E
		public void Execute()
		{
		}

		// Token: 0x04000D98 RID: 3480
		public Vector3 particlePosition;

		// Token: 0x04000D99 RID: 3481
		public float particleRadius;

		// Token: 0x04000D9A RID: 3482
		public NativeArray<DynamicBoneJob.CalcData> calcs;

		// Token: 0x04000D9B RID: 3483
		public NativeArray<Vector3> positions;

		// Token: 0x04000D9C RID: 3484
		public NativeArray<Quaternion> rotations;

		// Token: 0x04000D9D RID: 3485
		public NativeArray<Vector3> lossyScales;

		// Token: 0x04000D9E RID: 3486
		public NativeArray<int> isHits;
	}

	// Token: 0x02000315 RID: 789
	[BurstCompile]
	private struct CalcJob3 : IJob
	{
		// Token: 0x06000DD3 RID: 3539 RVA: 0x00041950 File Offset: 0x0003FD50
		public void Execute()
		{
		}

		// Token: 0x04000D9F RID: 3487
		public Vector3 particlePosition;

		// Token: 0x04000DA0 RID: 3488
		public float particleRadius;

		// Token: 0x04000DA1 RID: 3489
		public NativeArray<DynamicBoneJob.CalcData> calcs;

		// Token: 0x04000DA2 RID: 3490
		public NativeArray<Vector3> positions;

		// Token: 0x04000DA3 RID: 3491
		public NativeArray<Quaternion> rotations;

		// Token: 0x04000DA4 RID: 3492
		public NativeArray<Vector3> lossyScales;

		// Token: 0x04000DA5 RID: 3493
		public NativeArray<int> isHits;
	}

	// Token: 0x02000316 RID: 790
	[BurstCompile]
	private struct CalcJob4 : IJob
	{
		// Token: 0x06000DD4 RID: 3540 RVA: 0x00041954 File Offset: 0x0003FD54
		public void Execute()
		{
			for (int i = 1; i < this.calcs.Length; i++)
			{
				DynamicBoneJob.ParticleStruct value = this.calcs[i];
				DynamicBoneJob.ParticleStruct particleStruct = this.calcs[value.parentIndex];
				float magnitude;
				if (value.isTransform == 1)
				{
					magnitude = (particleStruct.transWorldPosition - value.transWorldPosition).magnitude;
				}
				else
				{
					magnitude = particleStruct.worldMatrix.MultiplyVector(value.endOffset).magnitude;
				}
				float num = Mathf.Lerp(1f, value.stiffness, this.weight);
				if (num > 0f || value.elasticity > 0f)
				{
					Matrix4x4 worldMatrix = particleStruct.worldMatrix;
					worldMatrix.SetColumn(3, particleStruct.position);
					Vector3 a;
					if (value.isTransform == 1)
					{
						a = worldMatrix.MultiplyPoint3x4(value.transLocalPosition);
					}
					else
					{
						a = worldMatrix.MultiplyPoint3x4(value.endOffset);
					}
					Vector3 a2 = a - value.position;
					value.position += a2 * value.elasticity;
					if (num > 0f)
					{
						a2 = a - value.position;
						float magnitude2 = a2.magnitude;
						float num2 = magnitude * (1f - num) * 2f;
						if (magnitude2 > num2)
						{
							value.position += a2 * ((magnitude2 - num2) / magnitude2);
						}
					}
				}
				float particleRadius = value.radius * this.objectScale;
				value.position = this.CalcCollider(value.position, particleRadius);
				Vector3 a3 = particleStruct.position - value.position;
				float magnitude3 = a3.magnitude;
				if (magnitude3 > 0f)
				{
					value.position += a3 * ((magnitude3 - magnitude) / magnitude3);
				}
				this.calcs[i] = value;
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00041B7C File Offset: 0x0003FF7C
		private Vector3 CalcCollider(Vector3 _position, float _particleRadius)
		{
			for (int i = 0; i < this.colls.Length; i++)
			{
				DynamicBoneJob.CollisionStruct collisionStruct = this.colls[i];
				float num = collisionStruct.radius * Mathf.Abs(collisionStruct.lossyScale.x);
				float num2 = collisionStruct.height * 0.5f - collisionStruct.radius;
				Matrix4x4 worldMatrix = collisionStruct.worldMatrix;
				if (num2 <= 0f)
				{
					Vector3 vector = worldMatrix * collisionStruct.center;
					if (collisionStruct.bound == DynamicBoneColliderBase.Bound.Outside)
					{
						float num3 = num + _particleRadius;
						float num4 = num3 * num3;
						Vector3 a = _position - vector;
						float sqrMagnitude = a.sqrMagnitude;
						if (sqrMagnitude > 0f && sqrMagnitude < num4)
						{
							float num5 = Mathf.Sqrt(sqrMagnitude);
							_position = vector + a * (num3 / num5);
						}
					}
					else
					{
						float num6 = num - _particleRadius;
						float num7 = num6 * num6;
						Vector3 a2 = _position - vector;
						float sqrMagnitude2 = a2.sqrMagnitude;
						if (sqrMagnitude2 > num7)
						{
							float num8 = Mathf.Sqrt(sqrMagnitude2);
							_position = vector + a2 * (num6 / num8);
						}
					}
				}
				else
				{
					Vector3 vector2 = collisionStruct.center;
					Vector3 vector3 = collisionStruct.center;
					DynamicBoneColliderBase.Direction direction = collisionStruct.direction;
					if (direction != DynamicBoneColliderBase.Direction.X)
					{
						if (direction != DynamicBoneColliderBase.Direction.Y)
						{
							if (direction == DynamicBoneColliderBase.Direction.Z)
							{
								vector2.z -= num2;
								vector3.z += num2;
							}
						}
						else
						{
							vector2.y -= num2;
							vector3.y += num2;
						}
					}
					else
					{
						vector2.x -= num2;
						vector3.x += num2;
					}
					vector2 = worldMatrix.MultiplyPoint3x4(vector2);
					vector3 = worldMatrix.MultiplyPoint3x4(vector3);
					if (collisionStruct.bound == DynamicBoneColliderBase.Bound.Outside)
					{
						float num9 = num + _particleRadius;
						float num10 = num9 * num9;
						Vector3 vector4 = vector3 - vector2;
						Vector3 vector5 = _position - vector2;
						float num11 = Vector3.Dot(vector5, vector4);
						if (num11 <= 0f)
						{
							float sqrMagnitude3 = vector5.sqrMagnitude;
							if (sqrMagnitude3 > 0f && sqrMagnitude3 < num10)
							{
								float num12 = Mathf.Sqrt(sqrMagnitude3);
								_position = vector2 + vector5 * (num9 / num12);
							}
						}
						else
						{
							float sqrMagnitude4 = vector4.sqrMagnitude;
							if (num11 >= sqrMagnitude4)
							{
								vector5 = _position - vector3;
								float sqrMagnitude5 = vector5.sqrMagnitude;
								if (sqrMagnitude5 > 0f && sqrMagnitude5 < num10)
								{
									float num13 = Mathf.Sqrt(sqrMagnitude5);
									_position = vector3 + vector5 * (num9 / num13);
								}
							}
							else if (sqrMagnitude4 > 0f)
							{
								num11 /= sqrMagnitude4;
								vector5 -= vector4 * num11;
								float sqrMagnitude6 = vector5.sqrMagnitude;
								if (sqrMagnitude6 > 0f && sqrMagnitude6 < num10)
								{
									float num14 = Mathf.Sqrt(sqrMagnitude6);
									_position += vector5 * ((num9 - num14) / num14);
								}
							}
						}
					}
					else
					{
						float num15 = num - _particleRadius;
						float num16 = num15 * num15;
						Vector3 vector6 = vector3 - vector2;
						Vector3 vector7 = _position - vector2;
						float num17 = Vector3.Dot(vector7, vector6);
						if (num17 <= 0f)
						{
							float sqrMagnitude7 = vector7.sqrMagnitude;
							if (sqrMagnitude7 > num16)
							{
								float num18 = Mathf.Sqrt(sqrMagnitude7);
								_position = vector2 + vector7 * (num15 / num18);
							}
						}
						else
						{
							float sqrMagnitude8 = vector6.sqrMagnitude;
							if (num17 >= sqrMagnitude8)
							{
								vector7 = _position - vector3;
								float sqrMagnitude9 = vector7.sqrMagnitude;
								if (sqrMagnitude9 > num16)
								{
									float num19 = Mathf.Sqrt(sqrMagnitude9);
									_position = vector3 + vector7 * (num15 / num19);
								}
							}
							else if (sqrMagnitude8 > 0f)
							{
								num17 /= sqrMagnitude8;
								vector7 -= vector6 * num17;
								float sqrMagnitude10 = vector7.sqrMagnitude;
								if (sqrMagnitude10 > num16)
								{
									float num20 = Mathf.Sqrt(sqrMagnitude10);
									_position += vector7 * ((num15 - num20) / num20);
								}
							}
						}
					}
				}
			}
			return _position;
		}

		// Token: 0x04000DA6 RID: 3494
		public NativeArray<DynamicBoneJob.ParticleStruct> calcs;

		// Token: 0x04000DA7 RID: 3495
		[ReadOnly]
		public NativeArray<DynamicBoneJob.CollisionStruct> colls;

		// Token: 0x04000DA8 RID: 3496
		[ReadOnly]
		public float weight;

		// Token: 0x04000DA9 RID: 3497
		[ReadOnly]
		public float objectScale;
	}

	// Token: 0x02000317 RID: 791
	private struct ParticleStruct
	{
		// Token: 0x04000DAA RID: 3498
		public int parentIndex;

		// Token: 0x04000DAB RID: 3499
		public float damping;

		// Token: 0x04000DAC RID: 3500
		public float elasticity;

		// Token: 0x04000DAD RID: 3501
		public float stiffness;

		// Token: 0x04000DAE RID: 3502
		public float inert;

		// Token: 0x04000DAF RID: 3503
		public float radius;

		// Token: 0x04000DB0 RID: 3504
		public float boneLength;

		// Token: 0x04000DB1 RID: 3505
		public int isTransform;

		// Token: 0x04000DB2 RID: 3506
		public Vector3 transWorldPosition;

		// Token: 0x04000DB3 RID: 3507
		public Vector3 transLocalPosition;

		// Token: 0x04000DB4 RID: 3508
		public Vector3 position;

		// Token: 0x04000DB5 RID: 3509
		public Vector3 prevPosition;

		// Token: 0x04000DB6 RID: 3510
		public Vector3 endOffset;

		// Token: 0x04000DB7 RID: 3511
		public Vector3 initLocalPosition;

		// Token: 0x04000DB8 RID: 3512
		public Quaternion initLocalRotation;

		// Token: 0x04000DB9 RID: 3513
		public Matrix4x4 worldMatrix;
	}

	// Token: 0x02000318 RID: 792
	private struct CollisionStruct
	{
		// Token: 0x04000DBA RID: 3514
		public DynamicBoneColliderBase.Direction direction;

		// Token: 0x04000DBB RID: 3515
		public Vector3 center;

		// Token: 0x04000DBC RID: 3516
		public DynamicBoneColliderBase.Bound bound;

		// Token: 0x04000DBD RID: 3517
		public float radius;

		// Token: 0x04000DBE RID: 3518
		public float height;

		// Token: 0x04000DBF RID: 3519
		public Vector3 lossyScale;

		// Token: 0x04000DC0 RID: 3520
		public Matrix4x4 worldMatrix;
	}
}
