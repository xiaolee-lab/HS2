using System;
using UnityEngine;

// Token: 0x02000465 RID: 1125
public class RotateAround : MonoBehaviour
{
	// Token: 0x060014AE RID: 5294 RVA: 0x00081928 File Offset: 0x0007FD28
	private void Start()
	{
		if (this.UseCollision)
		{
			this.EffectSettings.CollisionEnter += this.EffectSettings_CollisionEnter;
		}
		if (this.TimeDelay > 0f)
		{
			base.Invoke("ChangeUpdate", this.TimeDelay);
		}
		else
		{
			this.canUpdate = true;
		}
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x00081984 File Offset: 0x0007FD84
	private void OnEnable()
	{
		this.canUpdate = true;
		this.allTime = 0f;
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x00081998 File Offset: 0x0007FD98
	private void EffectSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		this.canUpdate = false;
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x000819A1 File Offset: 0x0007FDA1
	private void ChangeUpdate()
	{
		this.canUpdate = true;
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x000819AC File Offset: 0x0007FDAC
	private void Update()
	{
		if (!this.canUpdate)
		{
			return;
		}
		this.allTime += Time.deltaTime;
		if (this.allTime >= this.LifeTime && this.LifeTime > 0.0001f)
		{
			return;
		}
		if (this.SpeedFadeInTime > 0.001f)
		{
			if (this.currentSpeedFadeIn < this.Speed)
			{
				this.currentSpeedFadeIn += Time.deltaTime / this.SpeedFadeInTime * this.Speed;
			}
			else
			{
				this.currentSpeedFadeIn = this.Speed;
			}
		}
		else
		{
			this.currentSpeedFadeIn = this.Speed;
		}
		base.transform.Rotate(Vector3.forward * Time.deltaTime * this.currentSpeedFadeIn);
	}

	// Token: 0x040017CB RID: 6091
	public float Speed = 1f;

	// Token: 0x040017CC RID: 6092
	public float LifeTime = 1f;

	// Token: 0x040017CD RID: 6093
	public float TimeDelay;

	// Token: 0x040017CE RID: 6094
	public float SpeedFadeInTime;

	// Token: 0x040017CF RID: 6095
	public bool UseCollision;

	// Token: 0x040017D0 RID: 6096
	public EffectSettings EffectSettings;

	// Token: 0x040017D1 RID: 6097
	private bool canUpdate;

	// Token: 0x040017D2 RID: 6098
	private float currentSpeedFadeIn;

	// Token: 0x040017D3 RID: 6099
	private float allTime;
}
