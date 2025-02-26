using System;
using System.Collections.Generic;
using UniRx;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

// Token: 0x020002F8 RID: 760
[AddComponentMenu("Dynamic Bone/Dynamic Bone")]
public class DynamicBone : MonoBehaviour
{
	// Token: 0x06000D2B RID: 3371 RVA: 0x00037880 File Offset: 0x00035C80
	private void Start()
	{
		this.SetupParticles();
		(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
		where base.isActiveAndEnabled
		select _).Subscribe(delegate(long _)
		{
			this.OnUpdate();
		});
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x000378BB File Offset: 0x00035CBB
	private void FixedUpdate()
	{
		if (this.m_UpdateMode == DynamicBone.UpdateMode.AnimatePhysics)
		{
			this.PreUpdate();
		}
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x000378CF File Offset: 0x00035CCF
	private void OnUpdate()
	{
		if (this.m_UpdateMode != DynamicBone.UpdateMode.AnimatePhysics)
		{
			this.PreUpdate();
		}
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x000378E4 File Offset: 0x00035CE4
	private void LateUpdate()
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

	// Token: 0x06000D2F RID: 3375 RVA: 0x00037933 File Offset: 0x00035D33
	private void PreUpdate()
	{
		if (this.m_Weight > 0f && (!this.m_DistantDisable || !this.m_DistantDisabled))
		{
			this.InitTransforms();
		}
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x00037964 File Offset: 0x00035D64
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

	// Token: 0x06000D31 RID: 3377 RVA: 0x000379FA File Offset: 0x00035DFA
	private void OnEnable()
	{
		this.ResetParticlesPosition();
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x00037A02 File Offset: 0x00035E02
	private void OnDisable()
	{
		this.InitTransforms();
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x00037A0C File Offset: 0x00035E0C
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

	// Token: 0x06000D34 RID: 3380 RVA: 0x00037AAC File Offset: 0x00035EAC
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
			DynamicBone.Particle particle = this.m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
				Gizmos.DrawLine(particle.m_Position, particle2.m_Position);
			}
			if (particle.m_Radius > 0f)
			{
				Gizmos.DrawWireSphere(particle.m_Position, particle.m_Radius * this.m_ObjectScale);
			}
		}
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x00037B90 File Offset: 0x00035F90
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

	// Token: 0x06000D36 RID: 3382 RVA: 0x00037BDC File Offset: 0x00035FDC
	public float GetWeight()
	{
		return this.m_Weight;
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x00037BE4 File Offset: 0x00035FE4
	private void UpdateDynamicBones(float t)
	{
		if (this.m_Root == null)
		{
			return;
		}
		this.m_ObjectScale = Mathf.Abs(base.transform.lossyScale.x);
		this.m_ObjectMove = base.transform.position - this.m_ObjectPrevPosition;
		this.m_ObjectPrevPosition = base.transform.position;
		if (this.m_UpdateRate > 0f)
		{
			float num = 1f / this.m_UpdateRate;
			this.m_Time += t;
			int num2 = 0;
			while (this.m_Time >= num)
			{
				this.m_Time -= num;
				if (++num2 >= 3)
				{
					this.m_Time = 0f;
					break;
				}
			}
		}
		this.UpdateParticles1();
		this.UpdateParticlesForIJob();
		this.m_ObjectMove = Vector3.zero;
		this.ApplyParticlesToTransforms();
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x00037CD4 File Offset: 0x000360D4
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

	// Token: 0x06000D39 RID: 3385 RVA: 0x00037D74 File Offset: 0x00036174
	private void AppendParticles(Transform b, int parentIndex, float boneLength)
	{
		DynamicBone.Particle particle = new DynamicBone.Particle();
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

	// Token: 0x06000D3A RID: 3386 RVA: 0x00038168 File Offset: 0x00036568
	public void UpdateParameters()
	{
		if (this.m_Root == null)
		{
			return;
		}
		this.m_LocalGravity = this.m_Root.InverseTransformDirection(this.m_Gravity);
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
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

	// Token: 0x06000D3B RID: 3387 RVA: 0x00038390 File Offset: 0x00036790
	private void InitTransforms()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			if (particle.m_Transform != null)
			{
				particle.m_Transform.localPosition = particle.m_InitLocalPosition;
				particle.m_Transform.localRotation = particle.m_InitLocalRotation;
			}
		}
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x000383FC File Offset: 0x000367FC
	public void ResetParticlesPosition()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
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

	// Token: 0x06000D3D RID: 3389 RVA: 0x000384A8 File Offset: 0x000368A8
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
			DynamicBone.Particle particle = this.m_Particles[i];
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

	// Token: 0x06000D3E RID: 3390 RVA: 0x000385E8 File Offset: 0x000369E8
	private void UpdateParticles2()
	{
		Plane plane = default(Plane);
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
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
			if (this.m_FreezeAxis != DynamicBone.FreezeAxis.None)
			{
				DynamicBone.FreezeAxis freezeAxis = this.m_FreezeAxis;
				if (freezeAxis != DynamicBone.FreezeAxis.X)
				{
					if (freezeAxis != DynamicBone.FreezeAxis.Y)
					{
						if (freezeAxis == DynamicBone.FreezeAxis.Z)
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

	// Token: 0x06000D3F RID: 3391 RVA: 0x00038920 File Offset: 0x00036D20
	private void SkipUpdateParticles()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				particle.m_PrevPosition += this.m_ObjectMove;
				particle.m_Position += this.m_ObjectMove;
				DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
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

	// Token: 0x06000D40 RID: 3392 RVA: 0x00038B3A File Offset: 0x00036F3A
	private static Vector3 MirrorVector(Vector3 v, Vector3 axis)
	{
		return v - axis * (Vector3.Dot(v, axis) * 2f);
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x00038B58 File Offset: 0x00036F58
	private void ApplyParticlesToTransforms()
	{
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
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

	// Token: 0x06000D42 RID: 3394 RVA: 0x00038C44 File Offset: 0x00037044
	private void UpdateParticlesForIJob()
	{
		NativeArray<DynamicBone.ParticleStruct> calcs = new NativeArray<DynamicBone.ParticleStruct>(this.m_Particles.Count, Allocator.Temp, NativeArrayOptions.ClearMemory);
		NativeArray<DynamicBone.CollisionStruct> colls = new NativeArray<DynamicBone.CollisionStruct>(this.m_Colliders.Count, Allocator.Temp, NativeArrayOptions.ClearMemory);
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			calcs[i] = new DynamicBone.ParticleStruct
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
					colls[j] = new DynamicBone.CollisionStruct
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
		DynamicBone.CalcJob jobData = new DynamicBone.CalcJob
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

	// Token: 0x06000D43 RID: 3395 RVA: 0x00038F6A File Offset: 0x0003736A
	private void OnDestroy()
	{
		this.JobHandle.Complete();
	}

	// Token: 0x04000C49 RID: 3145
	public string Comment = string.Empty;

	// Token: 0x04000C4A RID: 3146
	public Transform m_Root;

	// Token: 0x04000C4B RID: 3147
	public float m_UpdateRate = 60f;

	// Token: 0x04000C4C RID: 3148
	public DynamicBone.UpdateMode m_UpdateMode;

	// Token: 0x04000C4D RID: 3149
	[Range(0f, 1f)]
	public float m_Damping = 0.1f;

	// Token: 0x04000C4E RID: 3150
	public AnimationCurve m_DampingDistrib;

	// Token: 0x04000C4F RID: 3151
	[Range(0f, 1f)]
	public float m_Elasticity = 0.1f;

	// Token: 0x04000C50 RID: 3152
	public AnimationCurve m_ElasticityDistrib;

	// Token: 0x04000C51 RID: 3153
	[Range(0f, 1f)]
	public float m_Stiffness = 0.1f;

	// Token: 0x04000C52 RID: 3154
	public AnimationCurve m_StiffnessDistrib;

	// Token: 0x04000C53 RID: 3155
	[Range(0f, 1f)]
	public float m_Inert;

	// Token: 0x04000C54 RID: 3156
	public AnimationCurve m_InertDistrib;

	// Token: 0x04000C55 RID: 3157
	public float m_Radius;

	// Token: 0x04000C56 RID: 3158
	public AnimationCurve m_RadiusDistrib;

	// Token: 0x04000C57 RID: 3159
	public float m_EndLength;

	// Token: 0x04000C58 RID: 3160
	public Vector3 m_EndOffset = Vector3.zero;

	// Token: 0x04000C59 RID: 3161
	public Vector3 m_Gravity = Vector3.zero;

	// Token: 0x04000C5A RID: 3162
	public Vector3 m_Force = Vector3.zero;

	// Token: 0x04000C5B RID: 3163
	public List<DynamicBoneColliderBase> m_Colliders;

	// Token: 0x04000C5C RID: 3164
	public List<Transform> m_Exclusions;

	// Token: 0x04000C5D RID: 3165
	public DynamicBone.FreezeAxis m_FreezeAxis;

	// Token: 0x04000C5E RID: 3166
	public bool m_DistantDisable;

	// Token: 0x04000C5F RID: 3167
	public Transform m_ReferenceObject;

	// Token: 0x04000C60 RID: 3168
	public float m_DistanceToObject = 20f;

	// Token: 0x04000C61 RID: 3169
	public List<Transform> m_notRolls;

	// Token: 0x04000C62 RID: 3170
	private Vector3 m_LocalGravity = Vector3.zero;

	// Token: 0x04000C63 RID: 3171
	private Vector3 m_ObjectMove = Vector3.zero;

	// Token: 0x04000C64 RID: 3172
	private Vector3 m_ObjectPrevPosition = Vector3.zero;

	// Token: 0x04000C65 RID: 3173
	private float m_BoneTotalLength;

	// Token: 0x04000C66 RID: 3174
	private float m_ObjectScale = 1f;

	// Token: 0x04000C67 RID: 3175
	private float m_Time;

	// Token: 0x04000C68 RID: 3176
	private float m_Weight = 1f;

	// Token: 0x04000C69 RID: 3177
	private bool m_DistantDisabled;

	// Token: 0x04000C6A RID: 3178
	private List<DynamicBone.Particle> m_Particles = new List<DynamicBone.Particle>();

	// Token: 0x04000C6B RID: 3179
	private JobHandle JobHandle;

	// Token: 0x020002F9 RID: 761
	public enum UpdateMode
	{
		// Token: 0x04000C6D RID: 3181
		Normal,
		// Token: 0x04000C6E RID: 3182
		AnimatePhysics,
		// Token: 0x04000C6F RID: 3183
		UnscaledTime
	}

	// Token: 0x020002FA RID: 762
	public enum FreezeAxis
	{
		// Token: 0x04000C71 RID: 3185
		None,
		// Token: 0x04000C72 RID: 3186
		X,
		// Token: 0x04000C73 RID: 3187
		Y,
		// Token: 0x04000C74 RID: 3188
		Z
	}

	// Token: 0x020002FB RID: 763
	private class Particle
	{
		// Token: 0x04000C75 RID: 3189
		public Transform m_Transform;

		// Token: 0x04000C76 RID: 3190
		public int m_ParentIndex = -1;

		// Token: 0x04000C77 RID: 3191
		public float m_Damping;

		// Token: 0x04000C78 RID: 3192
		public float m_Elasticity;

		// Token: 0x04000C79 RID: 3193
		public float m_Stiffness;

		// Token: 0x04000C7A RID: 3194
		public float m_Inert;

		// Token: 0x04000C7B RID: 3195
		public float m_Radius;

		// Token: 0x04000C7C RID: 3196
		public float m_BoneLength;

		// Token: 0x04000C7D RID: 3197
		public Vector3 m_Position = Vector3.zero;

		// Token: 0x04000C7E RID: 3198
		public Vector3 m_PrevPosition = Vector3.zero;

		// Token: 0x04000C7F RID: 3199
		public Vector3 m_EndOffset = Vector3.zero;

		// Token: 0x04000C80 RID: 3200
		public Vector3 m_InitLocalPosition = Vector3.zero;

		// Token: 0x04000C81 RID: 3201
		public Quaternion m_InitLocalRotation = Quaternion.identity;
	}

	// Token: 0x020002FC RID: 764
	[BurstCompile]
	private struct CalcJob : IJob
	{
		// Token: 0x06000D47 RID: 3399 RVA: 0x00038FDC File Offset: 0x000373DC
		public void Execute()
		{
			for (int i = 1; i < this.calcs.Length; i++)
			{
				DynamicBone.ParticleStruct value = this.calcs[i];
				DynamicBone.ParticleStruct particleStruct = this.calcs[value.parentIndex];
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

		// Token: 0x06000D48 RID: 3400 RVA: 0x00039204 File Offset: 0x00037604
		private Vector3 CalcCollider(Vector3 _position, float _particleRadius)
		{
			for (int i = 0; i < this.colls.Length; i++)
			{
				DynamicBone.CollisionStruct collisionStruct = this.colls[i];
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

		// Token: 0x04000C82 RID: 3202
		public NativeArray<DynamicBone.ParticleStruct> calcs;

		// Token: 0x04000C83 RID: 3203
		[ReadOnly]
		public NativeArray<DynamicBone.CollisionStruct> colls;

		// Token: 0x04000C84 RID: 3204
		[ReadOnly]
		public float weight;

		// Token: 0x04000C85 RID: 3205
		[ReadOnly]
		public float objectScale;
	}

	// Token: 0x020002FD RID: 765
	private struct ParticleStruct
	{
		// Token: 0x04000C86 RID: 3206
		public int parentIndex;

		// Token: 0x04000C87 RID: 3207
		public float damping;

		// Token: 0x04000C88 RID: 3208
		public float elasticity;

		// Token: 0x04000C89 RID: 3209
		public float stiffness;

		// Token: 0x04000C8A RID: 3210
		public float inert;

		// Token: 0x04000C8B RID: 3211
		public float radius;

		// Token: 0x04000C8C RID: 3212
		public float boneLength;

		// Token: 0x04000C8D RID: 3213
		public int isTransform;

		// Token: 0x04000C8E RID: 3214
		public Vector3 transWorldPosition;

		// Token: 0x04000C8F RID: 3215
		public Vector3 transLocalPosition;

		// Token: 0x04000C90 RID: 3216
		public Vector3 position;

		// Token: 0x04000C91 RID: 3217
		public Vector3 prevPosition;

		// Token: 0x04000C92 RID: 3218
		public Vector3 endOffset;

		// Token: 0x04000C93 RID: 3219
		public Vector3 initLocalPosition;

		// Token: 0x04000C94 RID: 3220
		public Quaternion initLocalRotation;

		// Token: 0x04000C95 RID: 3221
		public Matrix4x4 worldMatrix;
	}

	// Token: 0x020002FE RID: 766
	private struct CollisionStruct
	{
		// Token: 0x04000C96 RID: 3222
		public DynamicBoneColliderBase.Direction direction;

		// Token: 0x04000C97 RID: 3223
		public Vector3 center;

		// Token: 0x04000C98 RID: 3224
		public DynamicBoneColliderBase.Bound bound;

		// Token: 0x04000C99 RID: 3225
		public float radius;

		// Token: 0x04000C9A RID: 3226
		public float height;

		// Token: 0x04000C9B RID: 3227
		public Vector3 lossyScale;

		// Token: 0x04000C9C RID: 3228
		public Matrix4x4 worldMatrix;
	}
}
