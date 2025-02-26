using System;
using UnityEngine;

// Token: 0x02000450 RID: 1104
public class LineRendererBehaviour : MonoBehaviour
{
	// Token: 0x06001439 RID: 5177 RVA: 0x0007E77C File Offset: 0x0007CB7C
	private void GetEffectSettingsComponent(Transform tr)
	{
		Transform parent = tr.parent;
		if (parent != null)
		{
			this.effectSettings = parent.GetComponentInChildren<EffectSettings>();
			if (this.effectSettings == null)
			{
				this.GetEffectSettingsComponent(parent.transform);
			}
		}
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x0007E7C8 File Offset: 0x0007CBC8
	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
		if (this.effectSettings == null)
		{
		}
		this.tRoot = this.effectSettings.transform;
		this.line = base.GetComponent<LineRenderer>();
		this.InitializeDefault();
		this.isInitializedOnStart = true;
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x0007E81C File Offset: 0x0007CC1C
	private void InitializeDefault()
	{
		base.GetComponent<Renderer>().material.SetFloat("_Chanel", (float)this.currentShaderIndex);
		this.currentShaderIndex++;
		if (this.currentShaderIndex == 3)
		{
			this.currentShaderIndex = 0;
		}
		this.line.SetPosition(0, this.tRoot.position);
		if (this.IsVertical)
		{
			if (Physics.Raycast(this.tRoot.position, Vector3.down, out this.hit))
			{
				this.line.SetPosition(1, this.hit.point);
				if (this.StartGlow != null)
				{
					this.StartGlow.transform.position = this.tRoot.position;
				}
				if (this.HitGlow != null)
				{
					this.HitGlow.transform.position = this.hit.point;
				}
				if (this.GoLight != null)
				{
					this.GoLight.transform.position = this.hit.point + new Vector3(0f, this.LightHeightOffset, 0f);
				}
				if (this.Particles != null)
				{
					this.Particles.transform.position = this.hit.point + new Vector3(0f, this.ParticlesHeightOffset, 0f);
				}
				if (this.Explosion != null)
				{
					this.Explosion.transform.position = this.hit.point + new Vector3(0f, this.ParticlesHeightOffset, 0f);
				}
			}
		}
		else
		{
			if (this.effectSettings.Target != null)
			{
				this.tTarget = this.effectSettings.Target.transform;
			}
			else if (!this.effectSettings.UseMoveVector)
			{
			}
			Vector3 vector;
			if (!this.effectSettings.UseMoveVector)
			{
				vector = (this.tTarget.position - this.tRoot.position).normalized;
			}
			else
			{
				vector = this.tRoot.position + this.effectSettings.MoveVector * this.effectSettings.MoveDistance;
			}
			Vector3 a = this.tRoot.position + vector * this.effectSettings.MoveDistance;
			if (Physics.Raycast(this.tRoot.position, vector, out this.hit, this.effectSettings.MoveDistance + 1f, this.effectSettings.LayerMask))
			{
				a = (this.tRoot.position + Vector3.Normalize(this.hit.point - this.tRoot.position) * (this.effectSettings.MoveDistance + 1f)).normalized;
			}
			this.line.SetPosition(1, this.hit.point - this.effectSettings.ColliderRadius * a);
			Vector3 vector2 = this.hit.point - a * this.ParticlesHeightOffset;
			if (this.StartGlow != null)
			{
				this.StartGlow.transform.position = this.tRoot.position;
			}
			if (this.HitGlow != null)
			{
				this.HitGlow.transform.position = vector2;
			}
			if (this.GoLight != null)
			{
				this.GoLight.transform.position = this.hit.point - a * this.LightHeightOffset;
			}
			if (this.Particles != null)
			{
				this.Particles.transform.position = vector2;
			}
			if (this.Explosion != null)
			{
				this.Explosion.transform.position = vector2;
				this.Explosion.transform.LookAt(vector2 + this.hit.normal);
			}
		}
		CollisionInfo e = new CollisionInfo
		{
			Hit = this.hit
		};
		this.effectSettings.OnCollisionHandler(e);
		if (this.hit.transform != null)
		{
			ShieldCollisionBehaviour component = this.hit.transform.GetComponent<ShieldCollisionBehaviour>();
			if (component != null)
			{
				component.ShieldCollisionEnter(e);
			}
		}
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x0007ECE4 File Offset: 0x0007D0E4
	private void OnEnable()
	{
		if (this.isInitializedOnStart)
		{
			this.InitializeDefault();
		}
	}

	// Token: 0x040016F8 RID: 5880
	public bool IsVertical;

	// Token: 0x040016F9 RID: 5881
	public float LightHeightOffset = 0.3f;

	// Token: 0x040016FA RID: 5882
	public float ParticlesHeightOffset = 0.2f;

	// Token: 0x040016FB RID: 5883
	public float TimeDestroyLightAfterCollision = 4f;

	// Token: 0x040016FC RID: 5884
	public float TimeDestroyThisAfterCollision = 4f;

	// Token: 0x040016FD RID: 5885
	public float TimeDestroyRootAfterCollision = 4f;

	// Token: 0x040016FE RID: 5886
	public GameObject EffectOnHitObject;

	// Token: 0x040016FF RID: 5887
	public GameObject Explosion;

	// Token: 0x04001700 RID: 5888
	public GameObject StartGlow;

	// Token: 0x04001701 RID: 5889
	public GameObject HitGlow;

	// Token: 0x04001702 RID: 5890
	public GameObject Particles;

	// Token: 0x04001703 RID: 5891
	public GameObject GoLight;

	// Token: 0x04001704 RID: 5892
	private EffectSettings effectSettings;

	// Token: 0x04001705 RID: 5893
	private Transform tRoot;

	// Token: 0x04001706 RID: 5894
	private Transform tTarget;

	// Token: 0x04001707 RID: 5895
	private bool isInitializedOnStart;

	// Token: 0x04001708 RID: 5896
	private LineRenderer line;

	// Token: 0x04001709 RID: 5897
	private int currentShaderIndex;

	// Token: 0x0400170A RID: 5898
	private RaycastHit hit;
}
