using System;
using UnityEngine;

// Token: 0x0200045E RID: 1118
public class FadeInOutScale : MonoBehaviour
{
	// Token: 0x06001479 RID: 5241 RVA: 0x0008042C File Offset: 0x0007E82C
	private void Start()
	{
		this.t = base.transform;
		this.oldScale = this.t.localScale;
		this.isInitialized = true;
		this.GetEffectSettingsComponent(base.transform);
		if (this.effectSettings != null)
		{
			this.effectSettings.CollisionEnter += this.prefabSettings_CollisionEnter;
		}
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x00080494 File Offset: 0x0007E894
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

	// Token: 0x0600147B RID: 5243 RVA: 0x000804E0 File Offset: 0x0007E8E0
	public void InitDefaultVariables()
	{
		if (this.FadeInOutStatus == FadeInOutStatus.OutAfterCollision)
		{
			this.t.localScale = this.oldScale;
			this.canUpdate = false;
		}
		else
		{
			this.t.localScale = Vector3.zero;
			this.canUpdate = true;
		}
		this.updateTime = true;
		this.time = 0f;
		this.oldSin = 0f;
		this.isCollisionEnter = false;
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x00080551 File Offset: 0x0007E951
	private void prefabSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		this.isCollisionEnter = true;
		this.canUpdate = true;
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x00080561 File Offset: 0x0007E961
	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.InitDefaultVariables();
		}
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x00080574 File Offset: 0x0007E974
	private void Update()
	{
		if (!this.canUpdate)
		{
			return;
		}
		if (this.updateTime)
		{
			this.time = Time.time;
			this.updateTime = false;
		}
		float num = Mathf.Sin((Time.time - this.time) / this.Speed);
		float num2;
		if (this.oldSin > num)
		{
			this.canUpdate = false;
			num2 = this.MaxScale;
		}
		else
		{
			num2 = num * this.MaxScale;
		}
		if (this.FadeInOutStatus == FadeInOutStatus.In)
		{
			if (num2 < this.MaxScale)
			{
				this.t.localScale = new Vector3(this.oldScale.x * num2, this.oldScale.y * num2, this.oldScale.z * num2);
			}
			else
			{
				this.t.localScale = new Vector3(this.MaxScale, this.MaxScale, this.MaxScale);
			}
		}
		if (this.FadeInOutStatus == FadeInOutStatus.Out)
		{
			if (num2 > 0f)
			{
				this.t.localScale = new Vector3(this.MaxScale * this.oldScale.x - this.oldScale.x * num2, this.MaxScale * this.oldScale.y - this.oldScale.y * num2, this.MaxScale * this.oldScale.z - this.oldScale.z * num2);
			}
			else
			{
				this.t.localScale = Vector3.zero;
			}
		}
		if (this.FadeInOutStatus == FadeInOutStatus.OutAfterCollision && this.isCollisionEnter)
		{
			if (num2 > 0f)
			{
				this.t.localScale = new Vector3(this.MaxScale * this.oldScale.x - this.oldScale.x * num2, this.MaxScale * this.oldScale.y - this.oldScale.y * num2, this.MaxScale * this.oldScale.z - this.oldScale.z * num2);
			}
			else
			{
				this.t.localScale = Vector3.zero;
			}
		}
		this.oldSin = num;
	}

	// Token: 0x04001770 RID: 6000
	public FadeInOutStatus FadeInOutStatus;

	// Token: 0x04001771 RID: 6001
	public float Speed = 1f;

	// Token: 0x04001772 RID: 6002
	public float MaxScale = 2f;

	// Token: 0x04001773 RID: 6003
	private Vector3 oldScale;

	// Token: 0x04001774 RID: 6004
	private float time;

	// Token: 0x04001775 RID: 6005
	private float oldSin;

	// Token: 0x04001776 RID: 6006
	private bool updateTime = true;

	// Token: 0x04001777 RID: 6007
	private bool canUpdate = true;

	// Token: 0x04001778 RID: 6008
	private Transform t;

	// Token: 0x04001779 RID: 6009
	private EffectSettings effectSettings;

	// Token: 0x0400177A RID: 6010
	private bool isInitialized;

	// Token: 0x0400177B RID: 6011
	private bool isCollisionEnter;
}
