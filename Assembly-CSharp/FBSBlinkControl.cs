using System;
using UnityEngine;

// Token: 0x020010F4 RID: 4340
[Serializable]
public class FBSBlinkControl
{
	// Token: 0x06008FEE RID: 36846 RVA: 0x003BFAD4 File Offset: 0x003BDED4
	public void SetFixedFlags(byte flags)
	{
		this.fixedFlags = flags;
	}

	// Token: 0x06008FEF RID: 36847 RVA: 0x003BFADD File Offset: 0x003BDEDD
	public byte GetFixedFlags()
	{
		return this.fixedFlags;
	}

	// Token: 0x06008FF0 RID: 36848 RVA: 0x003BFAE8 File Offset: 0x003BDEE8
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

	// Token: 0x06008FF1 RID: 36849 RVA: 0x003BFB4F File Offset: 0x003BDF4F
	public void SetSpeed(float value)
	{
		this.BaseSpeed = Mathf.Max(1f, value);
	}

	// Token: 0x06008FF2 RID: 36850 RVA: 0x003BFB62 File Offset: 0x003BDF62
	public void SetForceOpen()
	{
		this.calcSpeed = this.BaseSpeed + UnityEngine.Random.Range(0f, 0.05f);
		this.blinkTime = Time.time + this.calcSpeed;
		this.blinkMode = -1;
	}

	// Token: 0x06008FF3 RID: 36851 RVA: 0x003BFB9C File Offset: 0x003BDF9C
	public void SetForceClose()
	{
		this.calcSpeed = this.BaseSpeed + UnityEngine.Random.Range(0f, 0.05f);
		this.blinkTime = Time.time + this.calcSpeed;
		this.count = UnityEngine.Random.Range(0, 3) + 1;
		this.blinkMode = 1;
	}

	// Token: 0x06008FF4 RID: 36852 RVA: 0x003BFBF0 File Offset: 0x003BDFF0
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

	// Token: 0x06008FF5 RID: 36853 RVA: 0x003BFD62 File Offset: 0x003BE162
	public float GetOpenRate()
	{
		return this.openRate;
	}

	// Token: 0x040074BA RID: 29882
	private byte fixedFlags;

	// Token: 0x040074BB RID: 29883
	[Range(0f, 255f)]
	public byte BlinkFrequency = 30;

	// Token: 0x040074BC RID: 29884
	private sbyte blinkMode;

	// Token: 0x040074BD RID: 29885
	[Range(0f, 0.5f)]
	public float BaseSpeed = 0.15f;

	// Token: 0x040074BE RID: 29886
	private float calcSpeed;

	// Token: 0x040074BF RID: 29887
	private float blinkTime;

	// Token: 0x040074C0 RID: 29888
	private int count;

	// Token: 0x040074C1 RID: 29889
	private float openRate = 1f;
}
