using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// Token: 0x02000302 RID: 770
[AddComponentMenu("Dynamic Bone/Dynamic Bone Ver02")]
public class DynamicBone_Ver02 : MonoBehaviour
{
	// Token: 0x06000D62 RID: 3426 RVA: 0x0003AA77 File Offset: 0x00038E77
	private void Awake()
	{
		this.InitNodeParticle();
		this.SetupParticles();
		this.InitLocalPosition();
		if (this.IsRefTransform())
		{
			this.setPtn(0, true);
		}
		this.InitTransforms();
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x0003AAA5 File Offset: 0x00038EA5
	private void Update()
	{
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x0003AAA7 File Offset: 0x00038EA7
	private void LateUpdate()
	{
		if (this.Weight > 0f)
		{
			this.InitTransforms();
			this.UpdateDynamicBones(Time.deltaTime);
		}
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x0003AACA File Offset: 0x00038ECA
	private void OnEnable()
	{
		this.ResetParticlesPosition();
		if (this.Root != null)
		{
			this.ObjectPrevPosition = this.Root.position;
		}
		else
		{
			this.ObjectPrevPosition = base.transform.position;
		}
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x0003AB0A File Offset: 0x00038F0A
	private void OnDisable()
	{
		this.InitTransforms();
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0003AB14 File Offset: 0x00038F14
	private void OnValidate()
	{
		this.UpdateRate = Mathf.Max(this.UpdateRate, 0f);
		if (Application.isEditor)
		{
			this.InitNodeParticle();
			this.SetupParticles();
			this.InitLocalPosition();
			if (this.IsRefTransform())
			{
				this.setPtn(this.PtnNo, true);
			}
			this.InitTransforms();
		}
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x0003AB74 File Offset: 0x00038F74
	private void OnDrawGizmosSelected()
	{
		if (!base.enabled || this.Root == null)
		{
			return;
		}
		if (Application.isEditor && !Application.isPlaying && base.transform.hasChanged)
		{
			this.InitNodeParticle();
			this.SetupParticles();
			this.InitLocalPosition();
			if (this.IsRefTransform())
			{
				this.setPtn(this.PtnNo, true);
			}
			this.InitTransforms();
		}
		Gizmos.color = Color.white;
		foreach (DynamicBone_Ver02.Particle particle in this.Particles)
		{
			if (particle.ParentIndex >= 0)
			{
				DynamicBone_Ver02.Particle particle2 = this.Particles[particle.ParentIndex];
				Gizmos.DrawLine(particle.Position, particle2.Position);
			}
			if (particle.Radius > 0f)
			{
				Gizmos.DrawWireSphere(particle.Position, particle.Radius * this.ObjectScale);
			}
		}
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0003AC9C File Offset: 0x0003909C
	public void SetWeight(float _weight)
	{
		if (this.Weight != _weight)
		{
			if (_weight == 0f)
			{
				this.InitTransforms();
			}
			else if (this.Weight == 0f)
			{
				this.ResetParticlesPosition();
				if (this.Root != null)
				{
					this.ObjectPrevPosition = this.Root.position;
				}
				else
				{
					this.ObjectPrevPosition = base.transform.position;
				}
			}
			this.Weight = _weight;
		}
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x0003AD20 File Offset: 0x00039120
	public float GetWeight()
	{
		return this.Weight;
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0003AD28 File Offset: 0x00039128
	public void setRoot(Transform _transRoot)
	{
		this.Root = _transRoot;
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x0003AD31 File Offset: 0x00039131
	public DynamicBone_Ver02.Particle getParticle(int _idx)
	{
		if (this.Particles.Count >= _idx)
		{
			return null;
		}
		return this.Particles[_idx];
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x0003AD52 File Offset: 0x00039152
	public int getParticleCount()
	{
		return this.Particles.Count;
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0003AD60 File Offset: 0x00039160
	public bool setPtn(int _ptn, bool _isSameForcePtn = false)
	{
		if (this.Particles == null || this.Patterns == null)
		{
			return false;
		}
		if (this.Particles.Count == 0 || this.Patterns.Count == 0)
		{
			return false;
		}
		if (this.Patterns.Count <= _ptn)
		{
			return false;
		}
		if (this.Particles.Count != this.Patterns[_ptn].ParticlePtns.Count)
		{
			return false;
		}
		if (this.PtnNo == _ptn && !_isSameForcePtn)
		{
			return false;
		}
		this.PtnNo = _ptn;
		this.Gravity = this.Patterns[_ptn].Gravity;
		for (int i = 0; i < this.Particles.Count; i++)
		{
			DynamicBone_Ver02.Particle particle = this.Particles[i];
			DynamicBone_Ver02.ParticlePtn particlePtn = this.Patterns[_ptn].ParticlePtns[i];
			particle.IsRotationCalc = particlePtn.IsRotationCalc;
			particle.Damping = particlePtn.Damping;
			particle.Elasticity = particlePtn.Elasticity;
			particle.Stiffness = particlePtn.Stiffness;
			particle.Inert = particlePtn.Inert;
			particle.ScaleNextBoneLength = particlePtn.ScaleNextBoneLength;
			particle.Radius = particlePtn.Radius;
			particle.IsMoveLimit = particlePtn.IsMoveLimit;
			particle.MoveLimitMin = particlePtn.MoveLimitMin;
			particle.MoveLimitMax = particlePtn.MoveLimitMax;
			particle.KeepLengthLimitMin = particlePtn.KeepLengthLimitMin;
			particle.KeepLengthLimitMax = particlePtn.KeepLengthLimitMax;
			particle.IsCrush = particlePtn.IsCrush;
			particle.CrushMoveAreaMin = particlePtn.CrushMoveAreaMin;
			particle.CrushMoveAreaMax = particlePtn.CrushMoveAreaMax;
			particle.CrushAddXYMin = particlePtn.CrushAddXYMin;
			particle.CrushAddXYMax = particlePtn.CrushAddXYMax;
			particle.Damping = Mathf.Clamp01(particle.Damping);
			particle.Elasticity = Mathf.Clamp01(particle.Elasticity);
			particle.Stiffness = Mathf.Clamp01(particle.Stiffness);
			particle.Inert = Mathf.Clamp01(particle.Inert);
			particle.ScaleNextBoneLength = Mathf.Max(particle.ScaleNextBoneLength, 0f);
			particle.Radius = Mathf.Max(particle.Radius, 0f);
			particle.InitLocalPosition = particlePtn.InitLocalPosition;
			particle.InitLocalRotation = particlePtn.InitLocalRotation;
			particle.InitLocalScale = particlePtn.InitLocalScale;
			particle.refTrans = particlePtn.refTrans;
			particle.LocalPosition = particlePtn.LocalPosition;
			particle.EndOffset = particlePtn.EndOffset;
		}
		return true;
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x0003AFDC File Offset: 0x000393DC
	public void ResetParticlesPosition()
	{
		if (this.Root != null)
		{
			this.ObjectPrevPosition = this.Root.position;
		}
		else
		{
			this.ObjectPrevPosition = base.transform.position;
		}
		foreach (DynamicBone_Ver02.Particle particle in this.Particles)
		{
			if (particle.Transform != null)
			{
				particle.Position = (particle.PrevPosition = particle.Transform.position);
			}
			else
			{
				Transform transform = this.Particles[particle.ParentIndex].Transform;
				particle.Position = (particle.PrevPosition = transform.TransformPoint(particle.EndOffset));
			}
		}
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x0003B0CC File Offset: 0x000394CC
	public void InitLocalPosition()
	{
		List<DynamicBone_Ver02.TransformParam> list = new List<DynamicBone_Ver02.TransformParam>();
		for (int i = 0; i < this.Particles.Count; i++)
		{
			DynamicBone_Ver02.Particle particle = this.Particles[i];
			DynamicBone_Ver02.TransformParam transformParam = new DynamicBone_Ver02.TransformParam();
			if (particle.Transform == null)
			{
				list.Add(transformParam);
			}
			else
			{
				transformParam.pos = particle.Transform.localPosition;
				transformParam.rot = particle.Transform.localRotation;
				transformParam.scale = particle.Transform.localScale;
				list.Add(transformParam);
			}
		}
		for (int j = 0; j < this.Patterns.Count; j++)
		{
			DynamicBone_Ver02.BonePtn bonePtn = this.Patterns[j];
			for (int k = 0; k < bonePtn.Params.Count; k++)
			{
				bonePtn.ParticlePtns[k].InitLocalPosition = bonePtn.Params[k].RefTransform.localPosition;
				bonePtn.ParticlePtns[k].InitLocalRotation = bonePtn.Params[k].RefTransform.localRotation;
				bonePtn.ParticlePtns[k].InitLocalScale = bonePtn.Params[k].RefTransform.localScale;
				bonePtn.ParticlePtns[k].refTrans = bonePtn.Params[k].RefTransform;
			}
			if (bonePtn.ParticlePtns.Count == this.Particles.Count)
			{
				for (int l = 0; l < this.Particles.Count; l++)
				{
					DynamicBone_Ver02.Particle particle2 = this.Particles[l];
					if (!(particle2.Transform == null))
					{
						particle2.Transform.localPosition = bonePtn.ParticlePtns[l].InitLocalPosition;
						particle2.Transform.localRotation = bonePtn.ParticlePtns[l].InitLocalRotation;
						particle2.Transform.localScale = bonePtn.ParticlePtns[l].InitLocalScale;
					}
				}
			}
			for (int m = 1; m < bonePtn.Params.Count; m++)
			{
				if (bonePtn.Params[m].RefTransform && bonePtn.Params[m - 1].RefTransform)
				{
					bonePtn.ParticlePtns[m].LocalPosition = this.CalcLocalPosition(bonePtn.Params[m].RefTransform.position, bonePtn.Params[m - 1].RefTransform);
				}
			}
		}
		for (int n = 0; n < this.Particles.Count; n++)
		{
			DynamicBone_Ver02.Particle particle3 = this.Particles[n];
			if (!(particle3.Transform == null))
			{
				particle3.Transform.localPosition = list[n].pos;
				particle3.Transform.localRotation = list[n].rot;
				particle3.Transform.localScale = list[n].scale;
			}
		}
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0003B44F File Offset: 0x0003984F
	public void ResetPosition()
	{
		this.InitLocalPosition();
		this.setPtn(this.PtnNo, true);
		if (base.enabled)
		{
			this.InitTransforms();
		}
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x0003B478 File Offset: 0x00039878
	public bool PtnBlend(int _blendAnswerPtn, int _blendPtn1, int _blendPtn2, float _t)
	{
		if (this.Patterns == null)
		{
			return false;
		}
		int count = this.Patterns.Count;
		if (count <= _blendAnswerPtn || count <= _blendPtn1 || count <= _blendPtn2)
		{
			return false;
		}
		if (this.Patterns[_blendAnswerPtn].ParticlePtns.Count != this.Patterns[_blendPtn1].ParticlePtns.Count || this.Patterns[_blendPtn2].ParticlePtns.Count != this.Patterns[_blendPtn1].ParticlePtns.Count)
		{
			return false;
		}
		this.Patterns[_blendAnswerPtn].Gravity = Vector3.Lerp(this.Patterns[_blendPtn1].Gravity, this.Patterns[_blendPtn2].Gravity, _t);
		for (int i = 0; i < this.Patterns[_blendAnswerPtn].ParticlePtns.Count; i++)
		{
			DynamicBone_Ver02.ParticlePtn particlePtn = this.Patterns[_blendAnswerPtn].ParticlePtns[i];
			DynamicBone_Ver02.ParticlePtn particlePtn2 = this.Patterns[_blendPtn1].ParticlePtns[i];
			DynamicBone_Ver02.ParticlePtn particlePtn3 = this.Patterns[_blendPtn2].ParticlePtns[i];
			particlePtn.IsRotationCalc = particlePtn3.IsRotationCalc;
			particlePtn.Damping = Mathf.Lerp(particlePtn2.Damping, particlePtn3.Damping, _t);
			particlePtn.Elasticity = Mathf.Lerp(particlePtn2.Elasticity, particlePtn3.Elasticity, _t);
			particlePtn.Stiffness = Mathf.Lerp(particlePtn2.Stiffness, particlePtn3.Stiffness, _t);
			particlePtn.Inert = Mathf.Lerp(particlePtn2.Inert, particlePtn3.Inert, _t);
			particlePtn.ScaleNextBoneLength = Mathf.Lerp(particlePtn2.ScaleNextBoneLength, particlePtn3.ScaleNextBoneLength, _t);
			particlePtn.Radius = Mathf.Lerp(particlePtn2.Radius, particlePtn3.Radius, _t);
			particlePtn.IsMoveLimit = particlePtn3.IsMoveLimit;
			particlePtn.MoveLimitMin = Vector3.Lerp(particlePtn2.MoveLimitMin, particlePtn3.MoveLimitMin, _t);
			particlePtn.MoveLimitMax = Vector3.Lerp(particlePtn2.MoveLimitMax, particlePtn3.MoveLimitMax, _t);
			particlePtn.KeepLengthLimitMin = Mathf.Lerp(particlePtn2.KeepLengthLimitMin, particlePtn3.KeepLengthLimitMin, _t);
			particlePtn.KeepLengthLimitMax = Mathf.Lerp(particlePtn2.KeepLengthLimitMax, particlePtn3.KeepLengthLimitMax, _t);
			particlePtn.IsCrush = particlePtn3.IsCrush;
			particlePtn.CrushMoveAreaMin = Mathf.Lerp(particlePtn2.CrushMoveAreaMin, particlePtn3.CrushMoveAreaMin, _t);
			particlePtn.CrushMoveAreaMax = Mathf.Lerp(particlePtn2.CrushMoveAreaMax, particlePtn3.CrushMoveAreaMax, _t);
			particlePtn.CrushAddXYMin = Mathf.Lerp(particlePtn2.CrushAddXYMin, particlePtn3.CrushAddXYMin, _t);
			particlePtn.CrushAddXYMax = Mathf.Lerp(particlePtn2.CrushAddXYMax, particlePtn3.CrushAddXYMax, _t);
			particlePtn.Damping = Mathf.Clamp01(particlePtn.Damping);
			particlePtn.Elasticity = Mathf.Clamp01(particlePtn.Elasticity);
			particlePtn.Stiffness = Mathf.Clamp01(particlePtn.Stiffness);
			particlePtn.Inert = Mathf.Clamp01(particlePtn.Inert);
			particlePtn.ScaleNextBoneLength = Mathf.Max(particlePtn.ScaleNextBoneLength, 0f);
			particlePtn.Radius = Mathf.Max(particlePtn.Radius, 0f);
			particlePtn.InitLocalPosition = Vector3.Lerp(particlePtn2.InitLocalPosition, particlePtn3.InitLocalPosition, _t);
			particlePtn.InitLocalRotation = Quaternion.Lerp(particlePtn2.InitLocalRotation, particlePtn3.InitLocalRotation, _t);
			particlePtn.InitLocalScale = Vector3.Lerp(particlePtn2.InitLocalScale, particlePtn3.InitLocalScale, _t);
			particlePtn.refTrans = particlePtn3.refTrans;
			particlePtn.LocalPosition = Vector3.Lerp(particlePtn2.LocalPosition, particlePtn3.LocalPosition, _t);
			particlePtn.EndOffset = Vector3.Lerp(particlePtn2.EndOffset, particlePtn3.EndOffset, _t);
		}
		return true;
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x0003B858 File Offset: 0x00039C58
	public bool setGravity(int _ptn, Vector3 _gravity, bool _isNowGravity = true)
	{
		if (this.Particles == null || this.Patterns == null)
		{
			return false;
		}
		if (this.Particles.Count == 0 || this.Patterns.Count == 0)
		{
			return false;
		}
		if (this.Patterns.Count <= _ptn)
		{
			return false;
		}
		if (_isNowGravity)
		{
			this.Gravity = _gravity;
		}
		if (_ptn < 0)
		{
			for (int i = 0; i < this.Patterns.Count; i++)
			{
				this.Patterns[i].Gravity = _gravity;
			}
		}
		else
		{
			if (this.Patterns.Count <= _ptn)
			{
				return false;
			}
			this.Patterns[_ptn].Gravity = _gravity;
		}
		return true;
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x0003B924 File Offset: 0x00039D24
	public bool setSoftParams(int _ptn, int _bone, float _damping, float _elasticity, float _stiffness, bool _isNowParam = true)
	{
		if (this.Particles == null || this.Patterns == null)
		{
			return false;
		}
		if (this.Particles.Count == 0 || this.Patterns.Count == 0)
		{
			return false;
		}
		if (this.Patterns.Count <= _ptn)
		{
			return false;
		}
		if (_isNowParam)
		{
			if (_bone == -1)
			{
				for (int i = 0; i < this.Particles.Count; i++)
				{
					this.Particles[i].Damping = _damping;
					this.Particles[i].Elasticity = _elasticity;
					this.Particles[i].Stiffness = _stiffness;
				}
			}
			else if (this.Particles.Count > _bone)
			{
				this.Particles[_bone].Damping = _damping;
				this.Particles[_bone].Elasticity = _elasticity;
				this.Particles[_bone].Stiffness = _stiffness;
			}
		}
		if (_ptn < 0)
		{
			for (int j = 0; j < this.Patterns.Count; j++)
			{
				if (_bone == -1)
				{
					for (int k = 0; k < this.Patterns[j].ParticlePtns.Count; k++)
					{
						this.setSoftParams(this.Patterns[j].ParticlePtns[k], _damping, _elasticity, _stiffness);
					}
					for (int l = 0; l < this.Patterns[j].Params.Count; l++)
					{
						this.setSoftParams(this.Patterns[j].Params[l], _damping, _elasticity, _stiffness);
					}
				}
				else
				{
					if (this.Patterns[j].ParticlePtns.Count > _bone)
					{
						this.setSoftParams(this.Patterns[j].ParticlePtns[_bone], _damping, _elasticity, _stiffness);
					}
					if (this.Patterns[j].Params.Count > _bone)
					{
						this.setSoftParams(this.Patterns[j].Params[_bone], _damping, _elasticity, _stiffness);
					}
				}
			}
		}
		else
		{
			if (this.Patterns.Count <= _ptn)
			{
				return false;
			}
			if (_bone == -1)
			{
				for (int m = 0; m < this.Patterns[_ptn].ParticlePtns.Count; m++)
				{
					this.setSoftParams(this.Patterns[_ptn].ParticlePtns[m], _damping, _elasticity, _stiffness);
				}
				for (int n = 0; n < this.Patterns[_ptn].Params.Count; n++)
				{
					this.setSoftParams(this.Patterns[_ptn].Params[n], _damping, _elasticity, _stiffness);
				}
			}
			else
			{
				if (this.Patterns[_ptn].ParticlePtns.Count > _bone)
				{
					this.setSoftParams(this.Patterns[_ptn].ParticlePtns[_bone], _damping, _elasticity, _stiffness);
				}
				if (this.Patterns[_ptn].Params.Count > _bone)
				{
					this.setSoftParams(this.Patterns[_ptn].Params[_bone], _damping, _elasticity, _stiffness);
				}
			}
		}
		return true;
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x0003BCB5 File Offset: 0x0003A0B5
	private bool setSoftParams(DynamicBone_Ver02.ParticlePtn _ptn, float _damping, float _elasticity, float _stiffness)
	{
		_ptn.Damping = _damping;
		_ptn.Elasticity = _elasticity;
		_ptn.Stiffness = _stiffness;
		return true;
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x0003BCCE File Offset: 0x0003A0CE
	private bool setSoftParams(DynamicBone_Ver02.BoneParameter _ptn, float _damping, float _elasticity, float _stiffness)
	{
		_ptn.Damping = _damping;
		_ptn.Elasticity = _elasticity;
		_ptn.Stiffness = _stiffness;
		return true;
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x0003BCE8 File Offset: 0x0003A0E8
	public bool setSoftParamsEx(int _ptn, int _bone, float _inert, bool _isNowParam = true)
	{
		if (this.Particles == null || this.Patterns == null)
		{
			return false;
		}
		if (this.Particles.Count == 0 || this.Patterns.Count == 0)
		{
			return false;
		}
		if (this.Patterns.Count <= _ptn)
		{
			return false;
		}
		if (_isNowParam)
		{
			if (_bone == -1)
			{
				for (int i = 0; i < this.Particles.Count; i++)
				{
					this.Particles[i].Inert = _inert;
				}
			}
			else if (this.Particles.Count > _bone)
			{
				this.Particles[_bone].Inert = _inert;
			}
		}
		if (_ptn < 0)
		{
			for (int j = 0; j < this.Patterns.Count; j++)
			{
				if (_bone == -1)
				{
					for (int k = 0; k < this.Patterns[j].ParticlePtns.Count; k++)
					{
						this.Patterns[j].ParticlePtns[k].Inert = _inert;
					}
					for (int l = 0; l < this.Patterns[j].Params.Count; l++)
					{
						this.Patterns[j].Params[l].Inert = _inert;
					}
				}
				else
				{
					if (this.Patterns[j].ParticlePtns.Count > _bone)
					{
						this.Patterns[j].ParticlePtns[_bone].Inert = _inert;
					}
					if (this.Patterns[j].Params.Count > _bone)
					{
						this.Patterns[j].Params[_bone].Inert = _inert;
					}
				}
			}
		}
		else
		{
			if (this.Patterns.Count <= _ptn)
			{
				return false;
			}
			if (_bone == -1)
			{
				for (int m = 0; m < this.Patterns[_ptn].ParticlePtns.Count; m++)
				{
					this.Patterns[_ptn].ParticlePtns[m].Inert = _inert;
				}
				for (int n = 0; n < this.Patterns[_ptn].Params.Count; n++)
				{
					this.Patterns[_ptn].Params[n].Inert = _inert;
				}
			}
			else
			{
				if (this.Patterns[_ptn].ParticlePtns.Count > _bone)
				{
					this.Patterns[_ptn].ParticlePtns[_bone].Inert = _inert;
				}
				if (this.Patterns[_ptn].Params.Count > _bone)
				{
					this.Patterns[_ptn].Params[_bone].Inert = _inert;
				}
			}
		}
		return true;
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x0003C000 File Offset: 0x0003A400
	public bool LoadTextList(List<string> _list)
	{
		DynamicBone_Ver02.LoadInfo loadInfo = new DynamicBone_Ver02.LoadInfo();
		int num = 0;
		while (_list.Count > num)
		{
			if (!this.LoadText(loadInfo, _list, ref num))
			{
				break;
			}
		}
		if (_list.Count > num)
		{
			return false;
		}
		this.Comment = loadInfo.Comment;
		this.ReflectSpeed = loadInfo.ReflectSpeed;
		this.HeavyLoopMaxCount = loadInfo.HeavyLoopMaxCount;
		this.Colliders = new List<DynamicBoneCollider>(loadInfo.Colliders);
		this.Bones = new List<Transform>(loadInfo.Bones);
		this.Patterns = new List<DynamicBone_Ver02.BonePtn>();
		foreach (DynamicBone_Ver02.BonePtn bonePtn in loadInfo.Patterns)
		{
			DynamicBone_Ver02.BonePtn bonePtn2 = new DynamicBone_Ver02.BonePtn();
			bonePtn2.Name = bonePtn.Name;
			bonePtn2.Gravity = bonePtn.Gravity;
			bonePtn2.EndOffset = bonePtn.EndOffset;
			bonePtn2.EndOffsetDamping = bonePtn.EndOffsetDamping;
			bonePtn2.EndOffsetElasticity = bonePtn.EndOffsetElasticity;
			bonePtn2.EndOffsetStiffness = bonePtn.EndOffsetStiffness;
			bonePtn2.EndOffsetInert = bonePtn.EndOffsetInert;
			foreach (DynamicBone_Ver02.BoneParameter boneParameter in bonePtn.Params)
			{
				DynamicBone_Ver02.BoneParameter boneParameter2 = new DynamicBone_Ver02.BoneParameter();
				boneParameter2.Name = boneParameter.Name;
				boneParameter2.RefTransform = boneParameter.RefTransform;
				boneParameter2.IsRotationCalc = boneParameter.IsRotationCalc;
				boneParameter2.Damping = boneParameter.Damping;
				boneParameter2.Elasticity = boneParameter.Elasticity;
				boneParameter2.Stiffness = boneParameter.Stiffness;
				boneParameter2.Inert = boneParameter.Inert;
				boneParameter2.NextBoneLength = boneParameter.NextBoneLength;
				boneParameter2.CollisionRadius = boneParameter.CollisionRadius;
				boneParameter2.IsMoveLimit = boneParameter.IsMoveLimit;
				boneParameter2.MoveLimitMin = boneParameter.MoveLimitMin;
				boneParameter2.MoveLimitMax = boneParameter.MoveLimitMax;
				boneParameter2.KeepLengthLimitMin = boneParameter.KeepLengthLimitMin;
				boneParameter2.KeepLengthLimitMax = boneParameter.KeepLengthLimitMax;
				boneParameter2.IsCrush = boneParameter.IsCrush;
				boneParameter2.CrushMoveAreaMin = boneParameter.CrushMoveAreaMin;
				boneParameter2.CrushMoveAreaMax = boneParameter.CrushMoveAreaMax;
				boneParameter2.CrushAddXYMin = boneParameter.CrushAddXYMin;
				boneParameter2.CrushAddXYMax = boneParameter.CrushAddXYMax;
				bonePtn2.Params.Add(boneParameter2);
			}
			this.Patterns.Add(bonePtn2);
		}
		this.InitNodeParticle();
		this.SetupParticles();
		this.InitLocalPosition();
		if (this.IsRefTransform())
		{
			this.setPtn(0, true);
		}
		this.InitTransforms();
		return true;
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x0003C2FC File Offset: 0x0003A6FC
	private void UpdateDynamicBones(float _deltaTime)
	{
		if (this.Root == null)
		{
			return;
		}
		this.ObjectScale = Mathf.Abs(this.Root.lossyScale.x);
		this.ObjectMove = this.Root.position - this.ObjectPrevPosition;
		this.ObjectPrevPosition = this.Root.position;
		int num = 1;
		if (this.UpdateRate > 0f)
		{
			float num2 = 1f / this.UpdateRate;
			this.UpdateTime += _deltaTime;
			num = 0;
			while (this.UpdateTime >= num2)
			{
				this.UpdateTime -= num2;
				if (++num >= this.HeavyLoopMaxCount)
				{
					this.UpdateTime = 0f;
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
				this.ObjectMove = Vector3.zero;
			}
		}
		else
		{
			this.SkipUpdateParticles();
		}
		this.ApplyParticlesToTransforms();
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0003C418 File Offset: 0x0003A818
	private void InitNodeParticle()
	{
		if (this.Patterns == null)
		{
			return;
		}
		foreach (DynamicBone_Ver02.BonePtn bonePtn in this.Patterns)
		{
			if (bonePtn.ParticlePtns != null)
			{
				bonePtn.ParticlePtns.Clear();
			}
			else
			{
				bonePtn.ParticlePtns = new List<DynamicBone_Ver02.ParticlePtn>();
			}
			if (bonePtn.Params.Count == this.Bones.Count)
			{
				foreach (var <>__AnonType in this.Bones.Select((Transform value, int idx) => new
				{
					value,
					idx
				}))
				{
					bonePtn.ParticlePtns.Add(this.AppendParticlePtn(bonePtn.Params[<>__AnonType.idx], Vector3.zero));
				}
				DynamicBone_Ver02.BoneParameter boneParameter = new DynamicBone_Ver02.BoneParameter();
				boneParameter.Damping = bonePtn.EndOffsetDamping;
				boneParameter.Elasticity = bonePtn.EndOffsetElasticity;
				boneParameter.Stiffness = bonePtn.EndOffsetStiffness;
				boneParameter.Inert = bonePtn.EndOffsetInert;
				bonePtn.ParticlePtns.Add(this.AppendParticlePtn(boneParameter, bonePtn.EndOffset));
			}
		}
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x0003C5B8 File Offset: 0x0003A9B8
	private void SetupParticles()
	{
		this.Particles.Clear();
		if (this.Root == null && this.Bones.Count > 0)
		{
			this.Root = this.Bones[0];
		}
		if (this.Root == null)
		{
			return;
		}
		if (this.Bones == null || this.Patterns == null)
		{
			return;
		}
		if (this.Bones.Count == 0 || this.Patterns.Count == 0)
		{
			return;
		}
		if (this.Bones.Count != this.Patterns[0].Params.Count)
		{
			return;
		}
		this.ObjectScale = this.Root.lossyScale.x;
		this.ObjectPrevPosition = this.Root.position;
		this.ObjectMove = Vector3.zero;
		int num = -1;
		foreach (var <>__AnonType in this.Bones.Select((Transform value, int idx) => new
		{
			value,
			idx
		}))
		{
			this.AppendParticles(<>__AnonType.value, this.Patterns[0].Params[<>__AnonType.idx], Vector3.zero, num);
			num++;
		}
		this.AppendParticles(null, new DynamicBone_Ver02.BoneParameter(), this.Patterns[0].EndOffset, num);
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x0003C768 File Offset: 0x0003AB68
	private DynamicBone_Ver02.ParticlePtn AppendParticlePtn(DynamicBone_Ver02.BoneParameter _parameter, Vector3 _endOffset)
	{
		DynamicBone_Ver02.ParticlePtn particlePtn = new DynamicBone_Ver02.ParticlePtn();
		particlePtn.IsRotationCalc = _parameter.IsRotationCalc;
		particlePtn.Damping = _parameter.Damping;
		particlePtn.Elasticity = _parameter.Elasticity;
		particlePtn.Stiffness = _parameter.Stiffness;
		particlePtn.Inert = _parameter.Inert;
		particlePtn.ScaleNextBoneLength = _parameter.NextBoneLength;
		particlePtn.Radius = _parameter.CollisionRadius;
		particlePtn.IsMoveLimit = _parameter.IsMoveLimit;
		particlePtn.MoveLimitMin = _parameter.MoveLimitMin;
		particlePtn.MoveLimitMax = _parameter.MoveLimitMax;
		particlePtn.KeepLengthLimitMin = _parameter.KeepLengthLimitMin;
		particlePtn.KeepLengthLimitMax = _parameter.KeepLengthLimitMax;
		particlePtn.IsCrush = _parameter.IsCrush;
		particlePtn.CrushMoveAreaMin = _parameter.CrushMoveAreaMin;
		particlePtn.CrushMoveAreaMax = _parameter.CrushMoveAreaMax;
		particlePtn.CrushAddXYMin = _parameter.CrushAddXYMin;
		particlePtn.CrushAddXYMax = _parameter.CrushAddXYMax;
		particlePtn.Damping = Mathf.Clamp01(particlePtn.Damping);
		particlePtn.Elasticity = Mathf.Clamp01(particlePtn.Elasticity);
		particlePtn.Stiffness = Mathf.Clamp01(particlePtn.Stiffness);
		particlePtn.Inert = Mathf.Clamp01(particlePtn.Inert);
		particlePtn.ScaleNextBoneLength = Mathf.Max(particlePtn.ScaleNextBoneLength, 0f);
		particlePtn.Radius = Mathf.Max(particlePtn.Radius, 0f);
		if (_parameter.RefTransform != null)
		{
			particlePtn.InitLocalPosition = _parameter.RefTransform.localPosition;
			particlePtn.InitLocalRotation = _parameter.RefTransform.localRotation;
			particlePtn.InitLocalScale = _parameter.RefTransform.localScale;
			particlePtn.refTrans = _parameter.RefTransform;
		}
		else
		{
			particlePtn.EndOffset = _endOffset;
		}
		return particlePtn;
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x0003C914 File Offset: 0x0003AD14
	private DynamicBone_Ver02.Particle AppendParticles(Transform _transform, DynamicBone_Ver02.BoneParameter _parameter, Vector3 _endOffset, int _parentIndex)
	{
		DynamicBone_Ver02.Particle particle = new DynamicBone_Ver02.Particle();
		particle.Transform = _transform;
		particle.IsRotationCalc = _parameter.IsRotationCalc;
		particle.Damping = _parameter.Damping;
		particle.Elasticity = _parameter.Elasticity;
		particle.Stiffness = _parameter.Stiffness;
		particle.Inert = _parameter.Inert;
		particle.ScaleNextBoneLength = _parameter.NextBoneLength;
		particle.Radius = _parameter.CollisionRadius;
		particle.IsMoveLimit = _parameter.IsMoveLimit;
		particle.MoveLimitMin = _parameter.MoveLimitMin;
		particle.MoveLimitMax = _parameter.MoveLimitMax;
		particle.KeepLengthLimitMin = _parameter.KeepLengthLimitMin;
		particle.KeepLengthLimitMax = _parameter.KeepLengthLimitMax;
		particle.IsCrush = _parameter.IsCrush;
		particle.CrushMoveAreaMin = _parameter.CrushMoveAreaMin;
		particle.CrushMoveAreaMax = _parameter.CrushMoveAreaMax;
		particle.CrushAddXYMin = _parameter.CrushAddXYMin;
		particle.CrushAddXYMax = _parameter.CrushAddXYMax;
		particle.ParentIndex = _parentIndex;
		particle.Damping = Mathf.Clamp01(particle.Damping);
		particle.Elasticity = Mathf.Clamp01(particle.Elasticity);
		particle.Stiffness = Mathf.Clamp01(particle.Stiffness);
		particle.Inert = Mathf.Clamp01(particle.Inert);
		particle.ScaleNextBoneLength = Mathf.Max(particle.ScaleNextBoneLength, 0f);
		particle.Radius = Mathf.Max(particle.Radius, 0f);
		if (_transform != null)
		{
			particle.Position = (particle.PrevPosition = _transform.position);
			particle.InitLocalPosition = _transform.localPosition;
			particle.InitLocalRotation = _transform.localRotation;
			particle.refTrans = _transform;
			if (_parentIndex >= 0)
			{
				this.CalcLocalPosition(particle, this.Particles[_parentIndex]);
			}
		}
		else
		{
			Transform transform = this.Particles[_parentIndex].Transform;
			particle.EndOffset = _endOffset;
			particle.Position = (particle.PrevPosition = transform.TransformPoint(particle.EndOffset));
		}
		this.Particles.Add(particle);
		return particle;
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x0003CB18 File Offset: 0x0003AF18
	private void InitTransforms()
	{
		int count = this.Particles.Count;
		for (int i = 0; i < count; i++)
		{
			DynamicBone_Ver02.Particle particle = this.Particles[i];
			if (!(particle.Transform == null))
			{
				if (particle.refTrans != null)
				{
					particle.Transform.localPosition = particle.refTrans.localPosition;
					particle.Transform.localRotation = particle.refTrans.localRotation;
					particle.Transform.localScale = particle.refTrans.localScale;
				}
				else
				{
					particle.Transform.localPosition = particle.InitLocalPosition;
					particle.Transform.localRotation = particle.InitLocalRotation;
					particle.Transform.localScale = particle.InitLocalScale;
				}
			}
		}
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0003CBF4 File Offset: 0x0003AFF4
	private void UpdateParticles1()
	{
		if (this.Patterns == null || (this.Patterns != null && this.Patterns.Count == 0))
		{
			return;
		}
		Vector3 b = (this.Gravity + this.Force) * this.ObjectScale;
		for (int i = 0; i < this.Particles.Count; i++)
		{
			DynamicBone_Ver02.Particle particle = this.Particles[i];
			if (particle.ParentIndex >= 0)
			{
				Vector3 a = (particle.Position - particle.PrevPosition) * this.ReflectSpeed;
				Vector3 b2 = this.ObjectMove * particle.Inert;
				particle.PrevPosition = particle.Position + b2;
				particle.Position += a * (1f - particle.Damping) + b + b2;
			}
			else
			{
				particle.PrevPosition = particle.Position;
				particle.Position = particle.Transform.position;
			}
		}
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0003CD10 File Offset: 0x0003B110
	private void UpdateParticles2()
	{
		for (int i = 1; i < this.Particles.Count; i++)
		{
			DynamicBone_Ver02.Particle particle = this.Particles[i];
			DynamicBone_Ver02.Particle particle2 = this.Particles[particle.ParentIndex];
			float num;
			if (particle.Transform != null)
			{
				num = (particle2.Transform.position - particle.Transform.position).magnitude;
			}
			else
			{
				num = particle.EndOffset.magnitude * this.ObjectScale;
			}
			float num2 = Mathf.Lerp(1f, particle.Stiffness, this.Weight);
			if (num2 > 0f || particle.Elasticity > 0f)
			{
				Matrix4x4 localToWorldMatrix = particle2.Transform.localToWorldMatrix;
				localToWorldMatrix.SetColumn(3, particle2.Position);
				Vector3 a;
				if (particle.Transform != null)
				{
					a = localToWorldMatrix.MultiplyPoint3x4(particle.LocalPosition);
				}
				else
				{
					a = localToWorldMatrix.MultiplyPoint3x4(particle.EndOffset);
				}
				Vector3 a2 = a - particle.Position;
				particle.Position += a2 * particle.Elasticity;
				if (num2 > 0f)
				{
					a2 = a - particle.Position;
					float magnitude = a2.magnitude;
					float num3 = num * (1f - num2) * 2f;
					if (magnitude > num3)
					{
						particle.Position += a2 * ((magnitude - num3) / magnitude);
					}
				}
			}
			float particleRadius = particle.Radius * this.ObjectScale;
			foreach (DynamicBoneCollider dynamicBoneCollider in this.Colliders)
			{
				if (dynamicBoneCollider != null && dynamicBoneCollider.enabled)
				{
					dynamicBoneCollider.Collide(ref particle.Position, particleRadius);
				}
			}
			Vector3 a3 = particle2.Position - particle.Position;
			float magnitude2 = a3.magnitude;
			if (magnitude2 > 0f)
			{
				float num4 = (magnitude2 - num) / magnitude2;
				if (particle.KeepLengthLimitMin >= num4)
				{
					particle.Position += a3 * (num4 - particle.KeepLengthLimitMin);
				}
				else if (num4 >= particle.KeepLengthLimitMax)
				{
					particle.Position += a3 * (num4 - particle.KeepLengthLimitMax);
				}
			}
		}
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0003CFD0 File Offset: 0x0003B3D0
	private void SkipUpdateParticles()
	{
		for (int i = 0; i < this.Particles.Count; i++)
		{
			DynamicBone_Ver02.Particle particle = this.Particles[i];
			if (particle.ParentIndex >= 0)
			{
				Vector3 b = this.ObjectMove * particle.Inert;
				particle.PrevPosition += b;
				particle.Position += b;
				DynamicBone_Ver02.Particle particle2 = this.Particles[particle.ParentIndex];
				float num;
				if (particle.Transform != null)
				{
					num = (particle2.Transform.position - particle.Transform.position).magnitude;
				}
				else
				{
					num = particle.EndOffset.magnitude * this.ObjectScale;
				}
				float num2 = Mathf.Lerp(1f, particle.Stiffness, this.Weight);
				if (num2 > 0f)
				{
					Matrix4x4 localToWorldMatrix = particle2.Transform.localToWorldMatrix;
					localToWorldMatrix.SetColumn(3, particle2.Position);
					Vector3 a;
					if (particle.Transform != null)
					{
						a = localToWorldMatrix.MultiplyPoint3x4(particle.LocalPosition);
					}
					else
					{
						a = localToWorldMatrix.MultiplyPoint3x4(particle.EndOffset);
					}
					Vector3 a2 = a - particle.Position;
					float magnitude = a2.magnitude;
					float num3 = num * (1f - num2) * 2f;
					if (magnitude > num3)
					{
						particle.Position += a2 * ((magnitude - num3) / magnitude);
					}
				}
				Vector3 a3 = particle2.Position - particle.Position;
				float magnitude2 = a3.magnitude;
				if (magnitude2 > 0f)
				{
					float num4 = (magnitude2 - num) / magnitude2;
					if (particle.KeepLengthLimitMin >= num4)
					{
						particle.Position += a3 * (num4 - particle.KeepLengthLimitMin);
					}
					else if (num4 >= particle.KeepLengthLimitMax)
					{
						particle.Position += a3 * (num4 - particle.KeepLengthLimitMax);
					}
				}
			}
			else
			{
				particle.PrevPosition = particle.Position;
				particle.Position = particle.Transform.position;
			}
		}
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x0003D22C File Offset: 0x0003B62C
	private void ApplyParticlesToTransforms()
	{
		for (int i = 1; i < this.Particles.Count; i++)
		{
			DynamicBone_Ver02.Particle particle = this.Particles[i];
			DynamicBone_Ver02.Particle particle2 = this.Particles[particle.ParentIndex];
			if (particle2.IsRotationCalc)
			{
				Vector3 direction;
				if (particle.Transform != null)
				{
					direction = particle.LocalPosition;
				}
				else
				{
					direction = particle.EndOffset;
				}
				Vector3 vector = particle2.Transform.TransformDirection(direction);
				Vector3 toDirection = particle.Position - particle2.Position;
				float magnitude = direction.magnitude;
				if (magnitude != 0f)
				{
					toDirection = particle.Position + vector * -1f * (1f - particle2.ScaleNextBoneLength) - particle2.Position;
				}
				Quaternion lhs = Quaternion.FromToRotation(vector, toDirection);
				particle2.Transform.rotation = lhs * particle2.Transform.rotation;
			}
			if (particle.Transform)
			{
				Matrix4x4 localToWorldMatrix = particle.Transform.localToWorldMatrix;
				Vector3 point = localToWorldMatrix.inverse.MultiplyPoint3x4(particle.Position);
				if (particle.IsCrush)
				{
					float num2;
					if (point.z <= 0f)
					{
						float num = Mathf.Clamp01(Mathf.InverseLerp(particle.CrushMoveAreaMin, 0f, point.z));
						num2 = particle.CrushAddXYMin * (1f - num);
					}
					else
					{
						float num3 = Mathf.Clamp01(Mathf.InverseLerp(0f, particle.CrushMoveAreaMax, point.z));
						num2 = particle.CrushAddXYMax * num3;
					}
					particle.Transform.localScale = particle.InitLocalScale + new Vector3(num2, num2, 0f);
				}
				if (particle.IsMoveLimit)
				{
					point.x = Mathf.Clamp(point.x, particle.MoveLimitMin.x, particle.MoveLimitMax.x);
					point.y = Mathf.Clamp(point.y, particle.MoveLimitMin.y, particle.MoveLimitMax.y);
					point.z = Mathf.Clamp(point.z, particle.MoveLimitMin.z, particle.MoveLimitMax.z);
					particle.Transform.position = localToWorldMatrix.MultiplyPoint3x4(point);
				}
				else
				{
					particle.Transform.position = particle.Position;
				}
			}
		}
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x0003D4BF File Offset: 0x0003B8BF
	private void CalcLocalPosition(DynamicBone_Ver02.Particle _particle, DynamicBone_Ver02.Particle _parent)
	{
		_particle.LocalPosition = _parent.Transform.InverseTransformPoint(_particle.Position);
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0003D4D8 File Offset: 0x0003B8D8
	private Vector3 CalcLocalPosition(Vector3 _particle, Transform _parent)
	{
		return _parent.InverseTransformPoint(_particle);
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x0003D4E4 File Offset: 0x0003B8E4
	private bool IsRefTransform()
	{
		if (this.Patterns == null)
		{
			return false;
		}
		foreach (DynamicBone_Ver02.BonePtn bonePtn in this.Patterns)
		{
			if (bonePtn.Params == null)
			{
				return false;
			}
			foreach (DynamicBone_Ver02.BoneParameter boneParameter in bonePtn.Params)
			{
				if (boneParameter.RefTransform == null)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x0003D5B8 File Offset: 0x0003B9B8
	private Transform FindLoop(Transform transform, string name)
	{
		if (string.Compare(name, transform.name) == 0)
		{
			return transform;
		}
		IEnumerator enumerator = transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform2 = (Transform)obj;
				Transform transform3 = this.FindLoop(transform2, name);
				if (null != transform3)
				{
					return transform3;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		return null;
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x0003D648 File Offset: 0x0003BA48
	private bool LoadText(DynamicBone_Ver02.LoadInfo _info, List<string> _list, ref int _index)
	{
		string[] array = _list[_index].Split(new char[]
		{
			'\t'
		});
		int num = array.Length;
		if (num == 0)
		{
			return false;
		}
		string text = array[0].Substring(0, 2);
		if (text.Equals("//"))
		{
			_index++;
			return true;
		}
		string text2 = array[0];
		if (text2 != null)
		{
			if (!(text2 == "#Comment"))
			{
				if (!(text2 == "#ReflectSpeed"))
				{
					if (!(text2 == "#HeavyLoopMaxCount"))
					{
						if (!(text2 == "#Colliders name"))
						{
							if (!(text2 == "#Bone name"))
							{
								if (!(text2 == "#PtnClassMember"))
								{
									return false;
								}
								DynamicBone_Ver02.BonePtn bonePtn = new DynamicBone_Ver02.BonePtn();
								if (!this.LoadPtnClassMember(bonePtn, array, _index))
								{
									return false;
								}
								_index++;
								if (!this.LoadParamClassMember(bonePtn, _list, ref _index))
								{
									return false;
								}
								_info.Patterns.Add(bonePtn);
								return true;
							}
							else
							{
								for (int i = 1; i < num; i++)
								{
									if (array[i] == string.Empty || array[i] == " ")
									{
										break;
									}
									Transform transform = this.FindLoop(base.transform, array[i]);
									if (transform == null)
									{
										return false;
									}
									_info.Bones.Add(transform);
								}
							}
						}
						else
						{
							for (int j = 1; j < num; j++)
							{
								if (array[j] == string.Empty || array[j] == " ")
								{
									break;
								}
								Transform transform2 = this.FindLoop(base.transform, array[j]);
								if (transform2 == null)
								{
									return false;
								}
								DynamicBoneCollider component = transform2.GetComponent<DynamicBoneCollider>();
								if (component == null)
								{
									return false;
								}
								_info.Colliders.Add(component);
							}
						}
					}
					else
					{
						int heavyLoopMaxCount;
						if (!int.TryParse(array[1], out heavyLoopMaxCount))
						{
							return false;
						}
						_info.HeavyLoopMaxCount = heavyLoopMaxCount;
					}
				}
				else
				{
					float reflectSpeed;
					if (!float.TryParse(array[1], out reflectSpeed))
					{
						return false;
					}
					_info.ReflectSpeed = reflectSpeed;
				}
			}
			else
			{
				_info.Comment = array[1];
			}
			_index++;
			return true;
		}
		return false;
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x0003D8A0 File Offset: 0x0003BCA0
	private bool LoadPtnClassMember(DynamicBone_Ver02.BonePtn _ptn, string[] _str, int _index)
	{
		int length = _str.Length;
		int num = 0;
		if (!this.ChekcLength(length, ref num, _index, "[PtnClassMember] 表示する名前", string.Empty))
		{
			return false;
		}
		_ptn.Name = _str[num];
		float num2;
		if (!this.GetMemberFloat(length, _str, ref num, _index, out num2, "[PtnClassMember] 重力X", string.Empty))
		{
			return false;
		}
		Vector3 vector = _ptn.Gravity;
		vector.x = num2;
		_ptn.Gravity = vector;
		if (!this.GetMemberFloat(length, _str, ref num, _index, out num2, "[PtnClassMember] 重力Y", string.Empty))
		{
			return false;
		}
		vector = _ptn.Gravity;
		vector.y = num2;
		_ptn.Gravity = vector;
		if (!this.GetMemberFloat(length, _str, ref num, _index, out num2, "[PtnClassMember] 重力Z", string.Empty))
		{
			return false;
		}
		vector = _ptn.Gravity;
		vector.z = num2;
		_ptn.Gravity = vector;
		if (!this.GetMemberFloat(length, _str, ref num, _index, out num2, "[PtnClassMember] EndOffsetX", string.Empty))
		{
			return false;
		}
		vector = _ptn.EndOffset;
		vector.x = num2;
		_ptn.EndOffset = vector;
		if (!this.GetMemberFloat(length, _str, ref num, _index, out num2, "[PtnClassMember] EndOffsetY", string.Empty))
		{
			return false;
		}
		vector = _ptn.EndOffset;
		vector.y = num2;
		_ptn.EndOffset = vector;
		if (!this.GetMemberFloat(length, _str, ref num, _index, out num2, "[PtnClassMember] EndOffsetZ", string.Empty))
		{
			return false;
		}
		vector = _ptn.EndOffset;
		vector.z = num2;
		_ptn.EndOffset = vector;
		if (!this.GetMemberFloat(length, _str, ref num, _index, out num2, "[PtnClassMember] EndOffsetの空気抵抗", string.Empty))
		{
			return false;
		}
		_ptn.EndOffsetDamping = num2;
		if (!this.GetMemberFloat(length, _str, ref num, _index, out num2, "[PtnClassMember] EndOffsetの弾力", string.Empty))
		{
			return false;
		}
		_ptn.EndOffsetElasticity = num2;
		if (!this.GetMemberFloat(length, _str, ref num, _index, out num2, "[PtnClassMember] EndOffsetの硬さ", string.Empty))
		{
			return false;
		}
		_ptn.EndOffsetStiffness = num2;
		if (!this.GetMemberFloat(length, _str, ref num, _index, out num2, "[PtnClassMember] EndOffsetの惰性", string.Empty))
		{
			return false;
		}
		_ptn.EndOffsetInert = num2;
		return true;
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x0003DAA4 File Offset: 0x0003BEA4
	private bool LoadParamClassMember(DynamicBone_Ver02.BonePtn _ptn, List<string> _list, ref int _index)
	{
		while (_list.Count > _index)
		{
			string[] array = _list[_index].Split(new char[]
			{
				'\t'
			});
			int num = array.Length;
			int num2 = 0;
			float num3 = 0f;
			bool flag = false;
			if (num <= num2)
			{
				return false;
			}
			string text = array[num2].Substring(0, 2);
			if (text.Equals("//"))
			{
				_index++;
			}
			else
			{
				if (array[num2] != "#ParamClassMember")
				{
					break;
				}
				DynamicBone_Ver02.BoneParameter boneParameter = new DynamicBone_Ver02.BoneParameter();
				if (!this.ChekcLength(num, ref num2, _index, "[ParamClassMember] 表示する名前", string.Empty))
				{
					return false;
				}
				boneParameter.Name = array[num2];
				if (!this.ChekcLength(num, ref num2, _index, "[ParamClassMember] 参照するフレーム", string.Empty))
				{
					return false;
				}
				Transform transform = this.FindLoop(base.transform, array[num2]);
				if (transform == null)
				{
					return false;
				}
				boneParameter.RefTransform = transform;
				if (!this.GetMemberBool(num, array, ref num2, _index, out flag, "[ParamClassMember] 回転するか ", string.Empty))
				{
					return false;
				}
				boneParameter.IsRotationCalc = flag;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 空気抵抗", string.Empty))
				{
					return false;
				}
				boneParameter.Damping = num3;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 弾力", string.Empty))
				{
					return false;
				}
				boneParameter.Elasticity = num3;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 硬さ", string.Empty))
				{
					return false;
				}
				boneParameter.Stiffness = num3;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 惰性", string.Empty))
				{
					return false;
				}
				boneParameter.Inert = num3;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 次の骨までの距離補正", string.Empty))
				{
					return false;
				}
				boneParameter.NextBoneLength = num3;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 当たり判定の半径", string.Empty))
				{
					return false;
				}
				boneParameter.CollisionRadius = num3;
				if (!this.GetMemberBool(num, array, ref num2, _index, out flag, "[ParamClassMember] 移動制限するか ", string.Empty))
				{
					return false;
				}
				boneParameter.IsMoveLimit = flag;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 移動制限最小X", string.Empty))
				{
					return false;
				}
				Vector3 vector = boneParameter.MoveLimitMin;
				vector.x = num3;
				boneParameter.MoveLimitMin = vector;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 移動制限最小Y", string.Empty))
				{
					return false;
				}
				vector = boneParameter.MoveLimitMin;
				vector.y = num3;
				boneParameter.MoveLimitMin = vector;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 移動制限最小Z", string.Empty))
				{
					return false;
				}
				vector = boneParameter.MoveLimitMin;
				vector.z = num3;
				boneParameter.MoveLimitMin = vector;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 移動制限最大X", string.Empty))
				{
					return false;
				}
				vector = boneParameter.MoveLimitMax;
				vector.x = num3;
				boneParameter.MoveLimitMax = vector;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 移動制限最大Y", string.Empty))
				{
					return false;
				}
				vector = boneParameter.MoveLimitMax;
				vector.y = num3;
				boneParameter.MoveLimitMax = vector;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 移動制限最大Z", string.Empty))
				{
					return false;
				}
				vector = boneParameter.MoveLimitMax;
				vector.z = num3;
				boneParameter.MoveLimitMax = vector;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 親からの長さの補正しない範囲最小", string.Empty))
				{
					return false;
				}
				boneParameter.KeepLengthLimitMin = num3;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 親からの長さの補正しない範囲最大", string.Empty))
				{
					return false;
				}
				boneParameter.KeepLengthLimitMax = num3;
				if (!this.GetMemberBool(num, array, ref num2, _index, out flag, "[ParamClassMember] 潰すか ", string.Empty))
				{
					return false;
				}
				boneParameter.IsCrush = flag;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 潰す移動判断範囲最小", string.Empty))
				{
					return false;
				}
				boneParameter.CrushMoveAreaMin = num3;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 潰す移動判断範囲最大", string.Empty))
				{
					return false;
				}
				boneParameter.CrushMoveAreaMax = num3;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 潰れた時に加算するXYスケール", string.Empty))
				{
					return false;
				}
				boneParameter.CrushAddXYMin = num3;
				if (!this.GetMemberFloat(num, array, ref num2, _index, out num3, "[ParamClassMember] 伸びた時に加算するXYスケール", string.Empty))
				{
					return false;
				}
				boneParameter.CrushAddXYMax = num3;
				_ptn.Params.Add(boneParameter);
				_index++;
			}
		}
		return true;
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x0003DF5C File Offset: 0x0003C35C
	private bool ChekcLength(int _length, ref int _index, int _line, string _warning = "", string _warning1 = "")
	{
		return _length > ++_index;
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x0003DF7B File Offset: 0x0003C37B
	private bool GetMemberFloat(int _length, string[] _str, ref int _index, int _line, out float _param, string _warning = "", string _warning1 = "")
	{
		_param = 0f;
		return this.ChekcLength(_length, ref _index, _line, _warning, string.Empty) && float.TryParse(_str[_index], out _param);
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0003DFB0 File Offset: 0x0003C3B0
	private bool GetMemberInt(int _length, string[] _str, ref int _index, int _line, out int _param, string _warning = "", string _warning1 = "")
	{
		_param = 0;
		return this.ChekcLength(_length, ref _index, _line, _warning, string.Empty) && int.TryParse(_str[_index], out _param);
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x0003DFE4 File Offset: 0x0003C3E4
	private bool GetMemberBool(int _length, string[] _str, ref int _index, int _line, out bool _param, string _warning = "", string _warning1 = "")
	{
		_param = false;
		if (!this.ChekcLength(_length, ref _index, _line, _warning, string.Empty))
		{
			return false;
		}
		if (_str[_index] == "false" || _str[_index] == "FALSE" || _str[_index] == "False")
		{
			_param = false;
			return true;
		}
		if (_str[_index] == "true" || _str[_index] == "TRUE" || _str[_index] == "True")
		{
			_param = true;
			return true;
		}
		return false;
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x0003E08C File Offset: 0x0003C48C
	private void SaveText(StreamWriter _writer)
	{
		_writer.Write("//コメント\n");
		_writer.Write("#Comment\t" + this.Comment + "\n");
		_writer.Write("//粒子のスピード\n");
		_writer.Write("#ReflectSpeed\t" + this.ReflectSpeed.ToString() + "\n");
		_writer.Write("//重い時に何回まで回すか\u3000回数多いと正確になるけど更に重くなるよ\n");
		_writer.Write("#HeavyLoopMaxCount\t" + this.HeavyLoopMaxCount.ToString() + "\n");
		_writer.Write("//登録する当たり判定の名前\n");
		_writer.Write("#Colliders name\t");
		foreach (DynamicBoneCollider dynamicBoneCollider in this.Colliders)
		{
			_writer.Write(dynamicBoneCollider.gameObject.name + "\t");
		}
		_writer.Write("\n");
		_writer.Write("//登録する骨の名前\n");
		_writer.Write("#Bone name\t");
		foreach (Transform transform in this.Bones)
		{
			_writer.Write(transform.name + "\t");
		}
		_writer.Write("\n");
		foreach (DynamicBone_Ver02.BonePtn bonePtn in this.Patterns)
		{
			_writer.Write("//パターンの設定\n");
			_writer.Write("//PtnClass\t");
			_writer.Write("表示する名前\t");
			_writer.Write("重力 X\t");
			_writer.Write("重力 Y\t");
			_writer.Write("重力 Z\t");
			_writer.Write("EndOffset x\t");
			_writer.Write("EndOffset y\t");
			_writer.Write("EndOffset z\t");
			_writer.Write("EndOffsetの空気抵抗\t");
			_writer.Write("EndOffsetの弾力\t");
			_writer.Write("EndOffsetの硬さ\t");
			_writer.Write("EndOffsetの惰性\t");
			_writer.Write("\n");
			_writer.Write("#PtnClassMember\t");
			_writer.Write(bonePtn.Name + "\t");
			_writer.Write(bonePtn.Gravity.x.ToString() + "\t");
			_writer.Write(bonePtn.Gravity.y.ToString() + "\t");
			_writer.Write(bonePtn.Gravity.z.ToString() + "\t");
			_writer.Write(bonePtn.EndOffset.x.ToString() + "\t");
			_writer.Write(bonePtn.EndOffset.y.ToString() + "\t");
			_writer.Write(bonePtn.EndOffset.z.ToString() + "\t");
			_writer.Write(bonePtn.EndOffsetDamping.ToString() + "\t");
			_writer.Write(bonePtn.EndOffsetElasticity.ToString() + "\t");
			_writer.Write(bonePtn.EndOffsetStiffness.ToString() + "\t");
			_writer.Write(bonePtn.EndOffsetInert.ToString() + "\t");
			_writer.Write("\n");
			_writer.Write("//そのパターンの骨に対するパラメーター\n");
			_writer.Write("//ParamClass\t");
			_writer.Write("表示する名前\t");
			_writer.Write("参照するフレーム名\t");
			_writer.Write("回転する？\t");
			_writer.Write("空気抵抗\t");
			_writer.Write("弾力\t");
			_writer.Write("硬さ\t");
			_writer.Write("惰性\t");
			_writer.Write("次の骨までの距離補正\t");
			_writer.Write("当たり判定の半径\t");
			_writer.Write("移動制限する？\t");
			_writer.Write("移動制限最小X\t");
			_writer.Write("移動制限最小Y\t");
			_writer.Write("移動制限最小Z\t");
			_writer.Write("移動制限最大X\t");
			_writer.Write("移動制限最大Y\t");
			_writer.Write("移動制限最大Z\t");
			_writer.Write("親からの長さを補正しない範囲最小値\t");
			_writer.Write("親からの長さを補正しない範囲最大値\t");
			_writer.Write("潰す？\t");
			_writer.Write("潰す移動判断範囲最小\t");
			_writer.Write("潰す移動判断範囲最大\t");
			_writer.Write("潰れた時に加算するXYスケール\t");
			_writer.Write("伸びた時に加算するXYスケール\t");
			_writer.Write("\n");
			foreach (DynamicBone_Ver02.BoneParameter boneParameter in bonePtn.Params)
			{
				_writer.Write("#ParamClassMember\t");
				_writer.Write(boneParameter.Name + "\t");
				string str = string.Empty;
				if (boneParameter.RefTransform != null)
				{
					str = boneParameter.RefTransform.name;
				}
				_writer.Write(str + "\t");
				_writer.Write(boneParameter.IsRotationCalc.ToString() + "\t");
				_writer.Write(boneParameter.Damping.ToString() + "\t");
				_writer.Write(boneParameter.Elasticity.ToString() + "\t");
				_writer.Write(boneParameter.Stiffness.ToString() + "\t");
				_writer.Write(boneParameter.Inert.ToString() + "\t");
				_writer.Write(boneParameter.NextBoneLength.ToString() + "\t");
				_writer.Write(boneParameter.CollisionRadius.ToString() + "\t");
				_writer.Write(boneParameter.IsMoveLimit.ToString() + "\t");
				_writer.Write(boneParameter.MoveLimitMin.x.ToString() + "\t");
				_writer.Write(boneParameter.MoveLimitMin.y.ToString() + "\t");
				_writer.Write(boneParameter.MoveLimitMin.z.ToString() + "\t");
				_writer.Write(boneParameter.MoveLimitMax.x.ToString() + "\t");
				_writer.Write(boneParameter.MoveLimitMax.y.ToString() + "\t");
				_writer.Write(boneParameter.MoveLimitMax.z.ToString() + "\t");
				_writer.Write(boneParameter.KeepLengthLimitMin.ToString() + "\t");
				_writer.Write(boneParameter.KeepLengthLimitMax.ToString() + "\t");
				_writer.Write(boneParameter.IsCrush.ToString() + "\t");
				_writer.Write(boneParameter.CrushMoveAreaMin.ToString() + "\t");
				_writer.Write(boneParameter.CrushMoveAreaMin.ToString() + "\t");
				_writer.Write(boneParameter.CrushAddXYMin.ToString() + "\t");
				_writer.Write(boneParameter.CrushAddXYMax.ToString() + "\t");
				_writer.Write("\n");
			}
		}
	}

	// Token: 0x04000CCC RID: 3276
	public string Comment = string.Empty;

	// Token: 0x04000CCD RID: 3277
	public Transform Root;

	// Token: 0x04000CCE RID: 3278
	public float UpdateRate = 60f;

	// Token: 0x04000CCF RID: 3279
	[Range(0f, 100f)]
	[Tooltip("速度UP")]
	public float ReflectSpeed = 1f;

	// Token: 0x04000CD0 RID: 3280
	[Range(0f, 10f)]
	[Tooltip("重い時に何回まで回す？正確になるけどその分重くなる")]
	public int HeavyLoopMaxCount = 3;

	// Token: 0x04000CD1 RID: 3281
	public Vector3 Gravity = Vector3.zero;

	// Token: 0x04000CD2 RID: 3282
	public Vector3 Force = Vector3.zero;

	// Token: 0x04000CD3 RID: 3283
	public List<DynamicBoneCollider> Colliders;

	// Token: 0x04000CD4 RID: 3284
	public List<Transform> Bones;

	// Token: 0x04000CD5 RID: 3285
	public List<DynamicBone_Ver02.BonePtn> Patterns;

	// Token: 0x04000CD6 RID: 3286
	private Vector3 ObjectMove = Vector3.zero;

	// Token: 0x04000CD7 RID: 3287
	private Vector3 ObjectPrevPosition = Vector3.zero;

	// Token: 0x04000CD8 RID: 3288
	private float ObjectScale = 1f;

	// Token: 0x04000CD9 RID: 3289
	private float UpdateTime;

	// Token: 0x04000CDA RID: 3290
	private float Weight = 1f;

	// Token: 0x04000CDB RID: 3291
	private List<DynamicBone_Ver02.Particle> Particles = new List<DynamicBone_Ver02.Particle>();

	// Token: 0x04000CDC RID: 3292
	public int PtnNo;

	// Token: 0x04000CDD RID: 3293
	public string DragAndDrop = string.Empty;

	// Token: 0x02000303 RID: 771
	[Serializable]
	public class BoneParameter
	{
		// Token: 0x06000D91 RID: 3473 RVA: 0x0003E998 File Offset: 0x0003CD98
		public BoneParameter()
		{
			this.Name = string.Empty;
			this.IsRotationCalc = false;
			this.Damping = 0f;
			this.Elasticity = 0f;
			this.Stiffness = 0f;
			this.Inert = 0f;
			this.NextBoneLength = 1f;
			this.CollisionRadius = 0f;
			this.IsMoveLimit = false;
			this.MoveLimitMin = Vector3.zero;
			this.MoveLimitMax = Vector3.zero;
			this.KeepLengthLimitMin = 0f;
			this.KeepLengthLimitMax = 0f;
			this.IsCrush = false;
			this.CrushMoveAreaMin = 0f;
			this.CrushMoveAreaMax = 0f;
			this.CrushAddXYMin = 0f;
			this.CrushAddXYMax = 0f;
		}

		// Token: 0x04000CE0 RID: 3296
		public string Name = string.Empty;

		// Token: 0x04000CE1 RID: 3297
		[Tooltip("参照骨")]
		public Transform RefTransform;

		// Token: 0x04000CE2 RID: 3298
		[Tooltip("回転させる？")]
		public bool IsRotationCalc;

		// Token: 0x04000CE3 RID: 3299
		[Range(0f, 1f)]
		[Tooltip("空気抵抗")]
		public float Damping;

		// Token: 0x04000CE4 RID: 3300
		[Range(0f, 1f)]
		[Tooltip("弾力(元の位置に戻ろうとする力)")]
		public float Elasticity;

		// Token: 0x04000CE5 RID: 3301
		[Range(0f, 1f)]
		[Tooltip("硬さ(要は移動のリミット：移動制限)")]
		public float Stiffness;

		// Token: 0x04000CE6 RID: 3302
		[Range(0f, 1f)]
		[Tooltip("惰性(ルートが動いた分を加算するか 加算すると親子付されてる感じになる？)")]
		public float Inert;

		// Token: 0x04000CE7 RID: 3303
		[Range(0f, 100f)]
		[Tooltip("次の骨までの長さの制御(回転に影響する：短いと回りやすい(角度が出やすい)\u3000長いと回りにくい(角度が出にくい))")]
		public float NextBoneLength = 1f;

		// Token: 0x04000CE8 RID: 3304
		[Tooltip("コリジョンの大きさ")]
		public float CollisionRadius;

		// Token: 0x04000CE9 RID: 3305
		[Tooltip("移動制限")]
		public bool IsMoveLimit;

		// Token: 0x04000CEA RID: 3306
		[Tooltip("ローカル移動制限最小")]
		public Vector3 MoveLimitMin = Vector3.zero;

		// Token: 0x04000CEB RID: 3307
		[Tooltip("ローカル移動制限最大")]
		public Vector3 MoveLimitMax = Vector3.zero;

		// Token: 0x04000CEC RID: 3308
		[Tooltip("骨の長さを留める制限最小")]
		public float KeepLengthLimitMin;

		// Token: 0x04000CED RID: 3309
		[Tooltip("骨の長さを留める制限最大")]
		public float KeepLengthLimitMax;

		// Token: 0x04000CEE RID: 3310
		[Tooltip("潰れ制御")]
		public bool IsCrush;

		// Token: 0x04000CEF RID: 3311
		[Tooltip("潰れ範囲最小 この間で設定されたスケール値を足す 判定はローカル位置のZ値")]
		public float CrushMoveAreaMin;

		// Token: 0x04000CF0 RID: 3312
		[Tooltip("潰れ範囲最大 この間で設定されたスケール値を足す 判定はローカル位置のZ値")]
		public float CrushMoveAreaMax;

		// Token: 0x04000CF1 RID: 3313
		[Tooltip("潰れた時に加算するXYスケール")]
		public float CrushAddXYMin;

		// Token: 0x04000CF2 RID: 3314
		[Tooltip("伸びた時に加算するXYスケール")]
		public float CrushAddXYMax;
	}

	// Token: 0x02000304 RID: 772
	public class ParticlePtn
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x0003EA94 File Offset: 0x0003CE94
		public ParticlePtn()
		{
			this.Damping = 0f;
			this.Elasticity = 0f;
			this.Stiffness = 0f;
			this.Inert = 0f;
			this.Radius = 0f;
			this.IsRotationCalc = true;
			this.ScaleNextBoneLength = 1f;
			this.IsMoveLimit = false;
			this.MoveLimitMin = Vector3.zero;
			this.MoveLimitMax = Vector3.zero;
			this.KeepLengthLimitMin = 0f;
			this.KeepLengthLimitMax = 0f;
			this.IsCrush = false;
			this.CrushMoveAreaMin = 0f;
			this.CrushMoveAreaMax = 0f;
			this.CrushAddXYMin = 0f;
			this.CrushAddXYMax = 0f;
			this.EndOffset = Vector3.zero;
			this.InitLocalPosition = Vector3.zero;
			this.InitLocalRotation = Quaternion.identity;
			this.InitLocalScale = Vector3.zero;
			this.refTrans = null;
			this.LocalPosition = Vector3.zero;
		}

		// Token: 0x04000CF3 RID: 3315
		public float Damping;

		// Token: 0x04000CF4 RID: 3316
		public float Elasticity;

		// Token: 0x04000CF5 RID: 3317
		public float Stiffness;

		// Token: 0x04000CF6 RID: 3318
		public float Inert;

		// Token: 0x04000CF7 RID: 3319
		public float Radius;

		// Token: 0x04000CF8 RID: 3320
		public bool IsRotationCalc = true;

		// Token: 0x04000CF9 RID: 3321
		public float ScaleNextBoneLength = 1f;

		// Token: 0x04000CFA RID: 3322
		public bool IsMoveLimit;

		// Token: 0x04000CFB RID: 3323
		public Vector3 MoveLimitMin = Vector3.zero;

		// Token: 0x04000CFC RID: 3324
		public Vector3 MoveLimitMax = Vector3.zero;

		// Token: 0x04000CFD RID: 3325
		public float KeepLengthLimitMin;

		// Token: 0x04000CFE RID: 3326
		public float KeepLengthLimitMax;

		// Token: 0x04000CFF RID: 3327
		public bool IsCrush;

		// Token: 0x04000D00 RID: 3328
		public float CrushMoveAreaMin;

		// Token: 0x04000D01 RID: 3329
		public float CrushMoveAreaMax;

		// Token: 0x04000D02 RID: 3330
		public float CrushAddXYMin;

		// Token: 0x04000D03 RID: 3331
		public float CrushAddXYMax;

		// Token: 0x04000D04 RID: 3332
		public Vector3 EndOffset = Vector3.zero;

		// Token: 0x04000D05 RID: 3333
		public Vector3 InitLocalPosition = Vector3.zero;

		// Token: 0x04000D06 RID: 3334
		public Quaternion InitLocalRotation = Quaternion.identity;

		// Token: 0x04000D07 RID: 3335
		public Vector3 InitLocalScale = Vector3.zero;

		// Token: 0x04000D08 RID: 3336
		public Transform refTrans;

		// Token: 0x04000D09 RID: 3337
		public Vector3 LocalPosition = Vector3.zero;
	}

	// Token: 0x02000305 RID: 773
	[Serializable]
	public class BonePtn
	{
		// Token: 0x04000D0A RID: 3338
		public string Name = string.Empty;

		// Token: 0x04000D0B RID: 3339
		[Tooltip("重力")]
		public Vector3 Gravity = Vector3.zero;

		// Token: 0x04000D0C RID: 3340
		[Tooltip("最後の骨を回すために必要")]
		public Vector3 EndOffset = Vector3.zero;

		// Token: 0x04000D0D RID: 3341
		[Range(0f, 1f)]
		[Tooltip("空気抵抗")]
		public float EndOffsetDamping;

		// Token: 0x04000D0E RID: 3342
		[Range(0f, 1f)]
		[Tooltip("弾力(元の位置に戻ろうとする力)")]
		public float EndOffsetElasticity;

		// Token: 0x04000D0F RID: 3343
		[Range(0f, 1f)]
		[Tooltip("硬さ(要は移動のリミット：移動制限)")]
		public float EndOffsetStiffness;

		// Token: 0x04000D10 RID: 3344
		[Range(0f, 1f)]
		[Tooltip("惰性(ルートが動いた分を加算するか 加算すると親子付されてる感じになる？)")]
		public float EndOffsetInert;

		// Token: 0x04000D11 RID: 3345
		public List<DynamicBone_Ver02.BoneParameter> Params = new List<DynamicBone_Ver02.BoneParameter>();

		// Token: 0x04000D12 RID: 3346
		public List<DynamicBone_Ver02.ParticlePtn> ParticlePtns = new List<DynamicBone_Ver02.ParticlePtn>();
	}

	// Token: 0x02000306 RID: 774
	public class Particle
	{
		// Token: 0x04000D13 RID: 3347
		public Transform Transform;

		// Token: 0x04000D14 RID: 3348
		public int ParentIndex = -1;

		// Token: 0x04000D15 RID: 3349
		public float Damping;

		// Token: 0x04000D16 RID: 3350
		public float Elasticity;

		// Token: 0x04000D17 RID: 3351
		public float Stiffness;

		// Token: 0x04000D18 RID: 3352
		public float Inert;

		// Token: 0x04000D19 RID: 3353
		public float Radius;

		// Token: 0x04000D1A RID: 3354
		public bool IsRotationCalc = true;

		// Token: 0x04000D1B RID: 3355
		public float ScaleNextBoneLength = 1f;

		// Token: 0x04000D1C RID: 3356
		public bool IsMoveLimit;

		// Token: 0x04000D1D RID: 3357
		public Vector3 MoveLimitMin = Vector3.zero;

		// Token: 0x04000D1E RID: 3358
		public Vector3 MoveLimitMax = Vector3.zero;

		// Token: 0x04000D1F RID: 3359
		public float KeepLengthLimitMin;

		// Token: 0x04000D20 RID: 3360
		public float KeepLengthLimitMax;

		// Token: 0x04000D21 RID: 3361
		public bool IsCrush;

		// Token: 0x04000D22 RID: 3362
		public float CrushMoveAreaMin;

		// Token: 0x04000D23 RID: 3363
		public float CrushMoveAreaMax;

		// Token: 0x04000D24 RID: 3364
		public float CrushAddXYMin;

		// Token: 0x04000D25 RID: 3365
		public float CrushAddXYMax;

		// Token: 0x04000D26 RID: 3366
		public Vector3 Position = Vector3.zero;

		// Token: 0x04000D27 RID: 3367
		public Vector3 PrevPosition = Vector3.zero;

		// Token: 0x04000D28 RID: 3368
		public Vector3 EndOffset = Vector3.zero;

		// Token: 0x04000D29 RID: 3369
		public Vector3 InitLocalPosition = Vector3.zero;

		// Token: 0x04000D2A RID: 3370
		public Quaternion InitLocalRotation = Quaternion.identity;

		// Token: 0x04000D2B RID: 3371
		public Vector3 InitLocalScale = Vector3.zero;

		// Token: 0x04000D2C RID: 3372
		public Transform refTrans;

		// Token: 0x04000D2D RID: 3373
		public Vector3 LocalPosition = Vector3.zero;
	}

	// Token: 0x02000307 RID: 775
	public class LoadInfo
	{
		// Token: 0x04000D2E RID: 3374
		public string Comment;

		// Token: 0x04000D2F RID: 3375
		public float ReflectSpeed;

		// Token: 0x04000D30 RID: 3376
		public int HeavyLoopMaxCount;

		// Token: 0x04000D31 RID: 3377
		public List<DynamicBoneCollider> Colliders = new List<DynamicBoneCollider>();

		// Token: 0x04000D32 RID: 3378
		public List<Transform> Bones = new List<Transform>();

		// Token: 0x04000D33 RID: 3379
		public List<DynamicBone_Ver02.BonePtn> Patterns = new List<DynamicBone_Ver02.BonePtn>();
	}

	// Token: 0x02000308 RID: 776
	private class TransformParam
	{
		// Token: 0x04000D34 RID: 3380
		public Vector3 pos;

		// Token: 0x04000D35 RID: 3381
		public Quaternion rot;

		// Token: 0x04000D36 RID: 3382
		public Vector3 scale;
	}
}
