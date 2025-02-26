using System;
using UnityEngine;

// Token: 0x02001119 RID: 4377
[Serializable]
public class MorphBlinkControl
{
	// Token: 0x06009108 RID: 37128 RVA: 0x003C65A5 File Offset: 0x003C49A5
	public void SetFixedFlags(byte flags)
	{
		this.fixedFlags = flags;
	}

	// Token: 0x06009109 RID: 37129 RVA: 0x003C65AE File Offset: 0x003C49AE
	public byte GetFixedFlags()
	{
		return this.fixedFlags;
	}

	// Token: 0x0600910A RID: 37130 RVA: 0x003C65B8 File Offset: 0x003C49B8
	public void SetFrequency(byte value)
	{
		this.BlinkFrequency = value;
		if ((int)this.blinkMode == 0)
		{
			int num = UnityEngine.Random.Range(0, (int)this.BlinkFrequency);
			float num2 = Mathf.InverseLerp(0f, (float)this.BlinkFrequency, (float)num);
			num2 = Mathf.Lerp(0f, (float)this.BlinkFrequency, num2);
			this.blinkTime = Time.time + 0.2f * num2;
		}
	}

	// Token: 0x0600910B RID: 37131 RVA: 0x003C661F File Offset: 0x003C4A1F
	public void SetSpeed(float value)
	{
		this.BaseSpeed = Mathf.Max(1f, value);
	}

	// Token: 0x0600910C RID: 37132 RVA: 0x003C6632 File Offset: 0x003C4A32
	public void SetForceOpen()
	{
		this.calcSpeed = this.BaseSpeed + UnityEngine.Random.Range(0f, 0.05f);
		this.blinkTime = Time.time + this.calcSpeed;
		this.blinkMode = -1;
	}

	// Token: 0x0600910D RID: 37133 RVA: 0x003C666C File Offset: 0x003C4A6C
	public void SetForceClose()
	{
		this.calcSpeed = this.BaseSpeed + UnityEngine.Random.Range(0f, 0.05f);
		this.blinkTime = Time.time + this.calcSpeed;
		this.count = UnityEngine.Random.Range(0, 3) + 1;
		this.blinkMode = 1;
	}

	// Token: 0x0600910E RID: 37134 RVA: 0x003C66C0 File Offset: 0x003C4AC0
	public void CalcBlink()
	{
		float num = Mathf.Max(0f, this.blinkTime - Time.time);
		sbyte b = this.blinkMode;
		float num2;
		switch (b + 1)
		{
		case 1:
			num2 = 1f;
			goto IL_88;
		case 2:
			num2 = Mathf.Clamp(num / this.calcSpeed, 0f, 1f);
			goto IL_88;
		}
		num2 = Mathf.Clamp(1f - num / this.calcSpeed, 0f, 1f);
		IL_88:
		if (this.fixedFlags == 0)
		{
			this.openRate = num2;
		}
		if (this.fixedFlags != 0)
		{
			return;
		}
		if (Time.time <= this.blinkTime)
		{
			return;
		}
		sbyte b2 = this.blinkMode;
		switch (b2 + 1)
		{
		case 0:
		{
			int num3 = UnityEngine.Random.Range(0, (int)this.BlinkFrequency);
			float num4 = Mathf.InverseLerp(0f, (float)this.BlinkFrequency, (float)num3);
			num4 = Mathf.Lerp(0f, (float)this.BlinkFrequency, num4);
			this.blinkTime = Time.time + 0.2f * num4;
			this.blinkMode = 0;
			break;
		}
		case 1:
			this.SetForceClose();
			break;
		case 2:
			this.count--;
			if (0 >= this.count)
			{
				this.SetForceOpen();
			}
			break;
		}
	}

	// Token: 0x0600910F RID: 37135 RVA: 0x003C6832 File Offset: 0x003C4C32
	public float GetOpenRate()
	{
		return this.openRate;
	}

	// Token: 0x0400759A RID: 30106
	private byte fixedFlags;

	// Token: 0x0400759B RID: 30107
	[Range(0f, 255f)]
	public byte BlinkFrequency = 30;

	// Token: 0x0400759C RID: 30108
	private sbyte blinkMode;

	// Token: 0x0400759D RID: 30109
	[Range(0f, 0.5f)]
	public float BaseSpeed = 0.15f;

	// Token: 0x0400759E RID: 30110
	private float calcSpeed;

	// Token: 0x0400759F RID: 30111
	private float blinkTime;

	// Token: 0x040075A0 RID: 30112
	private int count;

	// Token: 0x040075A1 RID: 30113
	private float openRate = 1f;
}
