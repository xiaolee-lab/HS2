using System;
using UnityEngine;

// Token: 0x02000466 RID: 1126
public class SetPositionOnHit : MonoBehaviour
{
	// Token: 0x060014B4 RID: 5300 RVA: 0x00081A8C File Offset: 0x0007FE8C
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

	// Token: 0x060014B5 RID: 5301 RVA: 0x00081AD5 File Offset: 0x0007FED5
	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
		if (this.effectSettings == null)
		{
		}
		this.tRoot = this.effectSettings.transform;
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x00081B08 File Offset: 0x0007FF08
	private void effectSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		Vector3 normalized = (this.tRoot.position + Vector3.Normalize(e.Hit.point - this.tRoot.position) * (this.effectSettings.MoveDistance + 1f)).normalized;
		base.transform.position = e.Hit.point - normalized * this.OffsetPosition;
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x00081B8B File Offset: 0x0007FF8B
	private void Update()
	{
		if (!this.isInitialized)
		{
			this.isInitialized = true;
			this.effectSettings.CollisionEnter += this.effectSettings_CollisionEnter;
		}
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x00081BB6 File Offset: 0x0007FFB6
	private void OnDisable()
	{
		base.transform.position = Vector3.zero;
	}

	// Token: 0x040017D4 RID: 6100
	public float OffsetPosition;

	// Token: 0x040017D5 RID: 6101
	private EffectSettings effectSettings;

	// Token: 0x040017D6 RID: 6102
	private Transform tRoot;

	// Token: 0x040017D7 RID: 6103
	private bool isInitialized;
}
