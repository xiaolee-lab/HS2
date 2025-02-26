using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FF RID: 767
[AddComponentMenu("Dynamic Bone/Dynamic Bone Ver01")]
public class DynamicBone_Ver01 : MonoBehaviour
{
	// Token: 0x06000D4A RID: 3402 RVA: 0x00039709 File Offset: 0x00037B09
	private void Start()
	{
		this.SetupParticles();
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x00039711 File Offset: 0x00037B11
	private void Update()
	{
		if (this.m_Weight > 0f)
		{
			this.InitTransforms();
		}
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x00039729 File Offset: 0x00037B29
	private void LateUpdate()
	{
		if (this.m_Weight > 0f)
		{
			this.UpdateDynamicBones(Time.deltaTime);
		}
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x00039746 File Offset: 0x00037B46
	private void OnEnable()
	{
		this.ResetParticlesPosition();
		this.m_ObjectPrevPosition = base.transform.position;
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0003975F File Offset: 0x00037B5F
	private void OnDisable()
	{
		this.InitTransforms();
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x00039768 File Offset: 0x00037B68
	private void OnValidate()
	{
		this.m_UpdateRate = Mathf.Max(this.m_UpdateRate, 0f);
		this.m_Damping = Mathf.Clamp01(this.m_Damping);
		this.m_Elasticity = Mathf.Clamp01(this.m_Elasticity);
		this.m_Stiffness = Mathf.Clamp01(this.m_Stiffness);
		this.m_Inert = Mathf.Clamp01(this.m_Inert);
		this.m_Radius = Mathf.Max(this.m_Radius, 0f);
		this.m_AddAngleScale = Mathf.Max(this.m_AddAngleScale, 0f);
		if (Application.isEditor && Application.isPlaying)
		{
			this.InitTransforms();
			this.SetupParticles();
		}
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x0003981C File Offset: 0x00037C1C
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
		foreach (DynamicBone_Ver01.Particle particle in this.m_Particles)
		{
			if (particle.m_ParentIndex >= 0)
			{
				DynamicBone_Ver01.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
				Gizmos.DrawLine(particle.m_Position, particle2.m_Position);
			}
			if (particle.m_Radius > 0f)
			{
				Gizmos.DrawWireSphere(particle.m_Position, particle.m_Radius * this.m_ObjectScale);
			}
		}
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x00039920 File Offset: 0x00037D20
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
				this.m_ObjectPrevPosition = base.transform.position;
			}
			this.m_Weight = w;
		}
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0003997D File Offset: 0x00037D7D
	public float GetWeight()
	{
		return this.m_Weight;
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x00039985 File Offset: 0x00037D85
	public void setRoot(Transform _transRoot)
	{
		this.m_Root = _transRoot;
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x00039990 File Offset: 0x00037D90
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

	// Token: 0x06000D55 RID: 3413 RVA: 0x00039AA4 File Offset: 0x00037EA4
	public void SetupParticles()
	{
		this.m_Particles.Clear();
		if (this.m_Root == null && this.m_Nodes.Count > 0)
		{
			this.m_Root = this.m_Nodes[0].Transform;
		}
		if (this.m_Root == null)
		{
			return;
		}
		this.m_ObjectScale = base.transform.lossyScale.x;
		this.m_ObjectPrevPosition = base.transform.position;
		this.m_ObjectMove = Vector3.zero;
		this.m_BoneTotalLength = 0f;
		DynamicBone_Ver01.Particle particle = null;
		int num = -1;
		foreach (DynamicBone_Ver01.BoneNode b in this.m_Nodes)
		{
			float boneLength = (particle == null) ? 0f : particle.m_BoneLength;
			particle = this.AppendParticles(b, num, boneLength);
			num++;
		}
		if (this.m_EndLength > 0f || this.m_EndOffset.magnitude != 0f)
		{
			this.AppendParticles(new DynamicBone_Ver01.BoneNode(), num, particle.m_BoneLength);
		}
		float num2 = (this.m_Particles.Count > 1) ? (1f / (float)(this.m_Particles.Count - 1)) : 0f;
		float num3 = 0f;
		foreach (DynamicBone_Ver01.Particle particle2 in this.m_Particles)
		{
			particle2.m_Damping = this.m_Damping;
			particle2.m_Elasticity = this.m_Elasticity;
			particle2.m_Stiffness = this.m_Stiffness;
			particle2.m_Inert = this.m_Inert;
			particle2.m_Radius = this.m_Radius;
			particle2.m_AddAngleScale = this.m_AddAngleScale;
			if (this.m_DampingDistrib.keys.Length > 0)
			{
				particle2.m_Damping *= this.m_DampingDistrib.Evaluate(num3);
			}
			if (this.m_ElasticityDistrib.keys.Length > 0)
			{
				particle2.m_Elasticity *= this.m_ElasticityDistrib.Evaluate(num3);
			}
			if (this.m_StiffnessDistrib.keys.Length > 0)
			{
				particle2.m_Stiffness *= this.m_StiffnessDistrib.Evaluate(num3);
			}
			if (this.m_InertDistrib.keys.Length > 0)
			{
				particle2.m_Inert *= this.m_InertDistrib.Evaluate(num3);
			}
			if (this.m_RadiusDistrib.keys.Length > 0)
			{
				particle2.m_Radius *= this.m_RadiusDistrib.Evaluate(num3);
			}
			if (this.m_AddAngleScaleDistrib.keys.Length > 0)
			{
				particle2.m_AddAngleScale *= this.m_AddAngleScaleDistrib.Evaluate(num3);
			}
			num3 += num2;
			particle2.m_Damping = Mathf.Clamp01(particle2.m_Damping);
			particle2.m_Elasticity = Mathf.Clamp01(particle2.m_Elasticity);
			particle2.m_Stiffness = Mathf.Clamp01(particle2.m_Stiffness);
			particle2.m_Inert = Mathf.Clamp01(particle2.m_Inert);
			particle2.m_Radius = Mathf.Max(particle2.m_Radius, 0f);
			particle2.m_AddAngleScale = Mathf.Max(particle2.m_AddAngleScale, 0f);
		}
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x00039E70 File Offset: 0x00038270
	public void reloadParameter()
	{
		foreach (DynamicBone_Ver01.Particle particle in this.m_Particles)
		{
			particle.m_Damping = this.m_Damping;
			particle.m_Elasticity = this.m_Elasticity;
			particle.m_Stiffness = this.m_Stiffness;
			particle.m_Inert = this.m_Inert;
			particle.m_Radius = this.m_Radius;
			particle.m_AddAngleScale = this.m_AddAngleScale;
			particle.m_Damping = Mathf.Clamp01(particle.m_Damping);
			particle.m_Elasticity = Mathf.Clamp01(particle.m_Elasticity);
			particle.m_Stiffness = Mathf.Clamp01(particle.m_Stiffness);
			particle.m_Inert = Mathf.Clamp01(particle.m_Inert);
			particle.m_Radius = Mathf.Max(particle.m_Radius, 0f);
			particle.m_AddAngleScale = Mathf.Max(particle.m_AddAngleScale, 0f);
		}
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x00039F80 File Offset: 0x00038380
	private DynamicBone_Ver01.Particle AppendParticles(DynamicBone_Ver01.BoneNode b, int parentIndex, float boneLength)
	{
		DynamicBone_Ver01.Particle particle = new DynamicBone_Ver01.Particle();
		particle.m_Transform = b.Transform;
		particle.m_bRotationCalc = b.RotationCalc;
		particle.m_ParentIndex = parentIndex;
		if (b.Transform != null)
		{
			particle.m_Position = (particle.m_PrevPosition = b.Transform.position);
			particle.m_InitLocalPosition = b.Transform.localPosition;
			particle.m_InitLocalRotation = b.Transform.localRotation;
			particle.m_InitEuler = b.Transform.localEulerAngles;
			if (parentIndex >= 0)
			{
				this.CalcLocalPosition(particle, this.m_Particles[parentIndex]);
			}
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
				particle.m_EndOffset = this.m_EndOffset;
			}
			particle.m_Position = (particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset));
		}
		if (parentIndex >= 0)
		{
			boneLength += (this.m_Particles[parentIndex].m_Transform.position - particle.m_Position).magnitude;
			particle.m_BoneLength = boneLength;
			this.m_BoneTotalLength = Mathf.Max(this.m_BoneTotalLength, boneLength);
		}
		this.m_Particles.Add(particle);
		return particle;
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x0003A13C File Offset: 0x0003853C
	public void InitTransforms()
	{
		foreach (DynamicBone_Ver01.Particle particle in this.m_Particles)
		{
			if (particle.m_Transform != null)
			{
				particle.m_Transform.localPosition = particle.m_InitLocalPosition;
				particle.m_Transform.localRotation = particle.m_InitLocalRotation;
			}
		}
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0003A1C4 File Offset: 0x000385C4
	public void ResetParticlesPosition()
	{
		this.m_ObjectPrevPosition = base.transform.position;
		foreach (DynamicBone_Ver01.Particle particle in this.m_Particles)
		{
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
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0003A28C File Offset: 0x0003868C
	private void UpdateParticles1()
	{
		Vector3 vector = this.m_Gravity;
		vector = (vector + this.m_Force) * this.m_ObjectScale;
		foreach (DynamicBone_Ver01.Particle particle in this.m_Particles)
		{
			if (particle.m_ParentIndex >= 0)
			{
				Vector3 a = particle.m_Position - particle.m_PrevPosition;
				Vector3 b = this.m_ObjectMove * particle.m_Inert;
				particle.m_PrevPosition = particle.m_Position + b;
				particle.m_Position += a * (1f - particle.m_Damping) + vector + b;
			}
			else
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.position;
			}
		}
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0003A398 File Offset: 0x00038798
	private void UpdateParticles2()
	{
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBone_Ver01.Particle particle = this.m_Particles[i];
			DynamicBone_Ver01.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
			float num;
			if (particle.m_Transform != null)
			{
				num = (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
			}
			else
			{
				num = particle.m_EndOffset.magnitude * this.m_ObjectScale;
			}
			float num2 = Mathf.Lerp(1f, particle.m_Stiffness, this.m_Weight);
			if (num2 > 0f || particle.m_Elasticity > 0f)
			{
				Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
				localToWorldMatrix.SetColumn(3, particle2.m_Position);
				Vector3 a;
				if (particle.m_Transform != null)
				{
					a = localToWorldMatrix.MultiplyPoint3x4(particle.m_LocalPosition);
				}
				else
				{
					a = localToWorldMatrix.MultiplyPoint3x4(particle.m_EndOffset);
				}
				Vector3 a2 = a - particle.m_Position;
				particle.m_Position += a2 * particle.m_Elasticity;
				if (num2 > 0f)
				{
					a2 = a - particle.m_Position;
					float magnitude = a2.magnitude;
					float num3 = num * (1f - num2) * 2f;
					if (magnitude > num3)
					{
						particle.m_Position += a2 * ((magnitude - num3) / magnitude);
					}
				}
			}
			float particleRadius = particle.m_Radius * this.m_ObjectScale;
			foreach (DynamicBoneCollider dynamicBoneCollider in this.m_Colliders)
			{
				if (dynamicBoneCollider != null && dynamicBoneCollider.enabled)
				{
					dynamicBoneCollider.Collide(ref particle.m_Position, particleRadius);
				}
			}
			Vector3 a3 = particle2.m_Position - particle.m_Position;
			float magnitude2 = a3.magnitude;
			if (magnitude2 > 0f)
			{
				particle.m_Position += a3 * ((magnitude2 - num) / magnitude2);
			}
		}
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0003A60C File Offset: 0x00038A0C
	private void SkipUpdateParticles()
	{
		foreach (DynamicBone_Ver01.Particle particle in this.m_Particles)
		{
			if (particle.m_ParentIndex >= 0)
			{
				Vector3 b = this.m_ObjectMove * particle.m_Inert;
				particle.m_PrevPosition += b;
				particle.m_Position += b;
				DynamicBone_Ver01.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
				float num;
				if (particle.m_Transform != null)
				{
					num = (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
				}
				else
				{
					num = particle.m_EndOffset.magnitude * this.m_ObjectScale;
				}
				float num2 = Mathf.Lerp(1f, particle.m_Stiffness, this.m_Weight);
				if (num2 > 0f)
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
					float magnitude = a2.magnitude;
					float num3 = num * (1f - num2) * 2f;
					if (magnitude > num3)
					{
						particle.m_Position += a2 * ((magnitude - num3) / magnitude);
					}
				}
				Vector3 a3 = particle2.m_Position - particle.m_Position;
				float magnitude2 = a3.magnitude;
				if (magnitude2 > 0f)
				{
					particle.m_Position += a3 * ((magnitude2 - num) / magnitude2);
				}
			}
			else
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.position;
			}
		}
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0003A84C File Offset: 0x00038C4C
	private void ApplyParticlesToTransforms()
	{
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBone_Ver01.Particle particle = this.m_Particles[i];
			DynamicBone_Ver01.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
			if (particle2.m_bRotationCalc)
			{
				Vector3 direction;
				if (particle.m_Transform != null)
				{
					direction = particle.m_LocalPosition;
				}
				else
				{
					direction = particle.m_EndOffset;
				}
				Vector3 toDirection = particle.m_Position - particle2.m_Position;
				float num;
				Vector3 axis;
				Quaternion.FromToRotation(particle2.m_Transform.TransformDirection(direction), toDirection).ToAngleAxis(out num, out axis);
				num *= particle.m_AddAngleScale;
				Quaternion lhs = Quaternion.AngleAxis(num, axis);
				particle2.m_Transform.rotation = lhs * particle2.m_Transform.rotation;
			}
			if (particle.m_Transform)
			{
				particle.m_Transform.position = particle.m_Position;
			}
		}
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0003A94A File Offset: 0x00038D4A
	private void CalcLocalPosition(DynamicBone_Ver01.Particle particle, DynamicBone_Ver01.Particle parent)
	{
		particle.m_LocalPosition = parent.m_Transform.InverseTransformPoint(particle.m_Position);
	}

	// Token: 0x04000C9D RID: 3229
	public string comment = string.Empty;

	// Token: 0x04000C9E RID: 3230
	public Transform m_Root;

	// Token: 0x04000C9F RID: 3231
	public float m_UpdateRate = 60f;

	// Token: 0x04000CA0 RID: 3232
	[Range(0f, 1f)]
	public float m_Damping = 0.1f;

	// Token: 0x04000CA1 RID: 3233
	public AnimationCurve m_DampingDistrib;

	// Token: 0x04000CA2 RID: 3234
	[Range(0f, 1f)]
	public float m_Elasticity = 0.1f;

	// Token: 0x04000CA3 RID: 3235
	public AnimationCurve m_ElasticityDistrib;

	// Token: 0x04000CA4 RID: 3236
	[Range(0f, 1f)]
	public float m_Stiffness = 0.1f;

	// Token: 0x04000CA5 RID: 3237
	public AnimationCurve m_StiffnessDistrib;

	// Token: 0x04000CA6 RID: 3238
	[Range(0f, 1f)]
	public float m_Inert;

	// Token: 0x04000CA7 RID: 3239
	public AnimationCurve m_InertDistrib;

	// Token: 0x04000CA8 RID: 3240
	public float m_Radius;

	// Token: 0x04000CA9 RID: 3241
	public AnimationCurve m_RadiusDistrib;

	// Token: 0x04000CAA RID: 3242
	public float m_AddAngleScale = 1f;

	// Token: 0x04000CAB RID: 3243
	public AnimationCurve m_AddAngleScaleDistrib;

	// Token: 0x04000CAC RID: 3244
	public float m_EndLength;

	// Token: 0x04000CAD RID: 3245
	public Vector3 m_EndOffset = Vector3.zero;

	// Token: 0x04000CAE RID: 3246
	public Vector3 m_Gravity = Vector3.zero;

	// Token: 0x04000CAF RID: 3247
	public Vector3 m_Force = Vector3.zero;

	// Token: 0x04000CB0 RID: 3248
	public List<DynamicBoneCollider> m_Colliders;

	// Token: 0x04000CB1 RID: 3249
	public List<DynamicBone_Ver01.BoneNode> m_Nodes;

	// Token: 0x04000CB2 RID: 3250
	private Vector3 m_ObjectMove = Vector3.zero;

	// Token: 0x04000CB3 RID: 3251
	private Vector3 m_ObjectPrevPosition = Vector3.zero;

	// Token: 0x04000CB4 RID: 3252
	private float m_BoneTotalLength;

	// Token: 0x04000CB5 RID: 3253
	private float m_ObjectScale = 1f;

	// Token: 0x04000CB6 RID: 3254
	private float m_Time;

	// Token: 0x04000CB7 RID: 3255
	private float m_Weight = 1f;

	// Token: 0x04000CB8 RID: 3256
	private List<DynamicBone_Ver01.Particle> m_Particles = new List<DynamicBone_Ver01.Particle>();

	// Token: 0x02000300 RID: 768
	[Serializable]
	public class BoneNode
	{
		// Token: 0x04000CB9 RID: 3257
		public Transform Transform;

		// Token: 0x04000CBA RID: 3258
		public bool RotationCalc = true;
	}

	// Token: 0x02000301 RID: 769
	private class Particle
	{
		// Token: 0x04000CBB RID: 3259
		public Transform m_Transform;

		// Token: 0x04000CBC RID: 3260
		public int m_ParentIndex = -1;

		// Token: 0x04000CBD RID: 3261
		public float m_Damping;

		// Token: 0x04000CBE RID: 3262
		public float m_Elasticity;

		// Token: 0x04000CBF RID: 3263
		public float m_Stiffness;

		// Token: 0x04000CC0 RID: 3264
		public float m_Inert;

		// Token: 0x04000CC1 RID: 3265
		public float m_Radius;

		// Token: 0x04000CC2 RID: 3266
		public float m_BoneLength;

		// Token: 0x04000CC3 RID: 3267
		public float m_AddAngleScale;

		// Token: 0x04000CC4 RID: 3268
		public bool m_bRotationCalc = true;

		// Token: 0x04000CC5 RID: 3269
		public Vector3 m_Position = Vector3.zero;

		// Token: 0x04000CC6 RID: 3270
		public Vector3 m_PrevPosition = Vector3.zero;

		// Token: 0x04000CC7 RID: 3271
		public Vector3 m_EndOffset = Vector3.zero;

		// Token: 0x04000CC8 RID: 3272
		public Vector3 m_InitLocalPosition = Vector3.zero;

		// Token: 0x04000CC9 RID: 3273
		public Quaternion m_InitLocalRotation = Quaternion.identity;

		// Token: 0x04000CCA RID: 3274
		public Vector3 m_InitEuler = Vector3.zero;

		// Token: 0x04000CCB RID: 3275
		public Vector3 m_LocalPosition = Vector3.zero;
	}
}
