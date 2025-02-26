using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200044C RID: 1100
public class MoveOnGround : MonoBehaviour
{
	// Token: 0x14000052 RID: 82
	// (add) Token: 0x06001426 RID: 5158 RVA: 0x0007E280 File Offset: 0x0007C680
	// (remove) Token: 0x06001427 RID: 5159 RVA: 0x0007E2B8 File Offset: 0x0007C6B8
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EventHandler<CollisionInfo> OnCollision;

	// Token: 0x06001428 RID: 5160 RVA: 0x0007E2F0 File Offset: 0x0007C6F0
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

	// Token: 0x06001429 RID: 5161 RVA: 0x0007E339 File Offset: 0x0007C739
	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
		if (this.effectSettings == null)
		{
		}
		this.particles = this.effectSettings.GetComponentsInChildren<ParticleSystem>();
		this.InitDefaultVariables();
		this.isInitialized = true;
	}

	// Token: 0x0600142A RID: 5162 RVA: 0x0007E376 File Offset: 0x0007C776
	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.InitDefaultVariables();
		}
	}

	// Token: 0x0600142B RID: 5163 RVA: 0x0007E38C File Offset: 0x0007C78C
	private void InitDefaultVariables()
	{
		foreach (ParticleSystem particleSystem in this.particles)
		{
			particleSystem.Stop();
		}
		this.isFinished = false;
		this.tTarget = this.effectSettings.Target.transform;
		if (this.IsRootMove)
		{
			this.tRoot = this.effectSettings.transform;
		}
		else
		{
			this.tRoot = base.transform.parent;
			this.tRoot.localPosition = Vector3.zero;
		}
		this.targetPos = this.tRoot.position + Vector3.Normalize(this.tTarget.position - this.tRoot.position) * this.effectSettings.MoveDistance;
		RaycastHit raycastHit;
		Physics.Raycast(this.tRoot.position, Vector3.down, out raycastHit);
		this.tRoot.position = raycastHit.point;
		foreach (ParticleSystem particleSystem2 in this.particles)
		{
			particleSystem2.Play();
		}
	}

	// Token: 0x0600142C RID: 5164 RVA: 0x0007E4BC File Offset: 0x0007C8BC
	private void Update()
	{
		if (this.tTarget == null || this.isFinished)
		{
			return;
		}
		Vector3 position = this.tRoot.position;
		RaycastHit raycastHit;
		Physics.Raycast(new Vector3(position.x, 0.5f, position.z), Vector3.down, out raycastHit);
		this.tRoot.position = raycastHit.point;
		position = this.tRoot.position;
		Vector3 vector = (!this.effectSettings.IsHomingMove) ? this.targetPos : this.tTarget.position;
		Vector3 vector2 = new Vector3(vector.x, 0f, vector.z);
		if (Vector3.Distance(new Vector3(position.x, 0f, position.z), vector2) <= this.effectSettings.ColliderRadius)
		{
			this.effectSettings.OnCollisionHandler(new CollisionInfo());
			this.isFinished = true;
		}
		this.tRoot.position = Vector3.MoveTowards(position, vector2, this.effectSettings.MoveSpeed * Time.deltaTime);
	}

	// Token: 0x040016EB RID: 5867
	public bool IsRootMove = true;

	// Token: 0x040016EC RID: 5868
	private EffectSettings effectSettings;

	// Token: 0x040016ED RID: 5869
	private Transform tRoot;

	// Token: 0x040016EE RID: 5870
	private Transform tTarget;

	// Token: 0x040016EF RID: 5871
	private Vector3 targetPos;

	// Token: 0x040016F0 RID: 5872
	private bool isInitialized;

	// Token: 0x040016F1 RID: 5873
	private bool isFinished;

	// Token: 0x040016F2 RID: 5874
	private ParticleSystem[] particles;
}
