using System;
using UnityEngine;

// Token: 0x02000444 RID: 1092
public class LineProjectileCollisionBehaviour : MonoBehaviour
{
	// Token: 0x06001400 RID: 5120 RVA: 0x0007CCDC File Offset: 0x0007B0DC
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

	// Token: 0x06001401 RID: 5121 RVA: 0x0007CD28 File Offset: 0x0007B128
	private void Start()
	{
		this.t = base.transform;
		if (this.EffectOnHit != null)
		{
			this.tEffectOnHit = this.EffectOnHit.transform;
			this.effectOnHitParticles = this.EffectOnHit.GetComponentsInChildren<ParticleSystem>();
		}
		if (this.ParticlesScale != null)
		{
			this.tParticleScale = this.ParticlesScale.transform;
		}
		this.GetEffectSettingsComponent(this.t);
		if (this.effectSettings == null)
		{
		}
		if (this.GoLight != null)
		{
			this.tLight = this.GoLight.transform;
		}
		this.InitializeDefault();
		this.isInitializedOnStart = true;
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x0007CDE2 File Offset: 0x0007B1E2
	private void OnEnable()
	{
		if (this.isInitializedOnStart)
		{
			this.InitializeDefault();
		}
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x0007CDF5 File Offset: 0x0007B1F5
	private void OnDisable()
	{
		this.CollisionLeave();
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x0007CE00 File Offset: 0x0007B200
	private void InitializeDefault()
	{
		this.hit = default(RaycastHit);
		this.frameDroped = false;
	}

	// Token: 0x06001405 RID: 5125 RVA: 0x0007CE24 File Offset: 0x0007B224
	private void Update()
	{
		if (!this.frameDroped)
		{
			this.frameDroped = true;
			return;
		}
		Vector3 vector = this.t.position + this.t.forward * this.effectSettings.MoveDistance;
		RaycastHit raycastHit;
		if (Physics.Raycast(this.t.position, this.t.forward, out raycastHit, this.effectSettings.MoveDistance + 1f, this.effectSettings.LayerMask))
		{
			this.hit = raycastHit;
			vector = raycastHit.point;
			if (this.oldRaycastHit.collider != this.hit.collider)
			{
				this.CollisionLeave();
				this.oldRaycastHit = this.hit;
				this.CollisionEnter();
				if (this.EffectOnHit != null)
				{
					foreach (ParticleSystem particleSystem in this.effectOnHitParticles)
					{
						particleSystem.Play();
					}
				}
			}
			if (this.EffectOnHit != null)
			{
				this.tEffectOnHit.position = this.hit.point - this.t.forward * this.effectSettings.ColliderRadius;
			}
		}
		else if (this.EffectOnHit != null)
		{
			foreach (ParticleSystem particleSystem2 in this.effectOnHitParticles)
			{
				particleSystem2.Stop();
			}
		}
		if (this.EffectOnHit != null)
		{
			this.tEffectOnHit.LookAt(this.hit.point + this.hit.normal);
		}
		if (this.IsCenterLightPosition && this.GoLight != null)
		{
			this.tLight.position = (this.t.position + vector) / 2f;
		}
		foreach (LineRenderer lineRenderer in this.LineRenderers)
		{
			lineRenderer.SetPosition(0, vector);
			lineRenderer.SetPosition(1, this.t.position);
		}
		if (this.ParticlesScale != null)
		{
			float x = Vector3.Distance(this.t.position, vector) / 2f;
			this.tParticleScale.localScale = new Vector3(x, 1f, 1f);
		}
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x0007D0C4 File Offset: 0x0007B4C4
	private void CollisionEnter()
	{
		if (this.EffectOnHitObject != null && this.hit.transform != null)
		{
			AddMaterialOnHit componentInChildren = this.hit.transform.GetComponentInChildren<AddMaterialOnHit>();
			this.effectSettingsInstance = null;
			if (componentInChildren != null)
			{
				this.effectSettingsInstance = componentInChildren.gameObject.GetComponent<EffectSettings>();
			}
			if (this.effectSettingsInstance != null)
			{
				this.effectSettingsInstance.IsVisible = true;
			}
			else
			{
				Transform transform = this.hit.transform;
				Renderer componentInChildren2 = transform.GetComponentInChildren<Renderer>();
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EffectOnHitObject);
				gameObject.transform.parent = componentInChildren2.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.GetComponent<AddMaterialOnHit>().UpdateMaterial(this.hit);
				this.effectSettingsInstance = gameObject.GetComponent<EffectSettings>();
			}
		}
		this.effectSettings.OnCollisionHandler(new CollisionInfo
		{
			Hit = this.hit
		});
	}

	// Token: 0x06001407 RID: 5127 RVA: 0x0007D1CD File Offset: 0x0007B5CD
	private void CollisionLeave()
	{
		if (this.effectSettingsInstance != null)
		{
			this.effectSettingsInstance.IsVisible = false;
		}
	}

	// Token: 0x04001693 RID: 5779
	public GameObject EffectOnHit;

	// Token: 0x04001694 RID: 5780
	public GameObject EffectOnHitObject;

	// Token: 0x04001695 RID: 5781
	public GameObject ParticlesScale;

	// Token: 0x04001696 RID: 5782
	public GameObject GoLight;

	// Token: 0x04001697 RID: 5783
	public bool IsCenterLightPosition;

	// Token: 0x04001698 RID: 5784
	public LineRenderer[] LineRenderers;

	// Token: 0x04001699 RID: 5785
	private EffectSettings effectSettings;

	// Token: 0x0400169A RID: 5786
	private Transform t;

	// Token: 0x0400169B RID: 5787
	private Transform tLight;

	// Token: 0x0400169C RID: 5788
	private Transform tEffectOnHit;

	// Token: 0x0400169D RID: 5789
	private Transform tParticleScale;

	// Token: 0x0400169E RID: 5790
	private RaycastHit hit;

	// Token: 0x0400169F RID: 5791
	private RaycastHit oldRaycastHit;

	// Token: 0x040016A0 RID: 5792
	private bool isInitializedOnStart;

	// Token: 0x040016A1 RID: 5793
	private bool frameDroped;

	// Token: 0x040016A2 RID: 5794
	private ParticleSystem[] effectOnHitParticles;

	// Token: 0x040016A3 RID: 5795
	private EffectSettings effectSettingsInstance;
}
