using System;
using System.Collections.Generic;
using MorphAssist;
using UnityEngine;

// Token: 0x0200111C RID: 4380
[Serializable]
public class MorphCtrlMouth : MorphFaceBase
{
	// Token: 0x06009115 RID: 37141 RVA: 0x003C68FB File Offset: 0x003C4CFB
	public new void Init(List<MorphingTargetInfo> MorphTargetList)
	{
		base.Init(MorphTargetList);
		this.tpcRand = new TimeProgressCtrlRandom();
		this.tpcRand.Init(this.randTimeMin, this.randTimeMax);
	}

	// Token: 0x06009116 RID: 37142 RVA: 0x003C6927 File Offset: 0x003C4D27
	public void CalcBlend(float openValue)
	{
		this.openRate = openValue;
		base.CalculateBlendVertex();
		if (this.useAjustWidthScale)
		{
			this.AdjustWidthScale();
		}
	}

	// Token: 0x06009117 RID: 37143 RVA: 0x003C6948 File Offset: 0x003C4D48
	public void UseAdjustWidthScale(bool useFlags)
	{
		this.useAjustWidthScale = useFlags;
	}

	// Token: 0x06009118 RID: 37144 RVA: 0x003C6954 File Offset: 0x003C4D54
	public bool AdjustWidthScale()
	{
		if (null == this.objAdjustWidthScale)
		{
			return false;
		}
		bool flag = false;
		float num = this.tpcRand.Calculate();
		if (num == 1f)
		{
			this.sclStart = (this.sclNow = this.sclEnd);
			this.sclEnd = UnityEngine.Random.Range(this.randScaleMin, this.randScaleMax);
			flag = true;
		}
		if (flag)
		{
			num = 0f;
		}
		this.sclNow = Mathf.Lerp(this.sclStart, this.sclEnd, num);
		this.sclNow = Mathf.Max(0f, this.sclNow - this.openRefValue * this.openRate);
		if (0.2f < this.openRate)
		{
			this.objAdjustWidthScale.transform.localScale = new Vector3(this.sclNow, 1f, 1f);
		}
		return true;
	}

	// Token: 0x040075A3 RID: 30115
	public bool useAjustWidthScale;

	// Token: 0x040075A4 RID: 30116
	private TimeProgressCtrlRandom tpcRand;

	// Token: 0x040075A5 RID: 30117
	public GameObject objAdjustWidthScale;

	// Token: 0x040075A6 RID: 30118
	[Range(0.01f, 1f)]
	public float randTimeMin = 0.1f;

	// Token: 0x040075A7 RID: 30119
	[Range(0.01f, 1f)]
	public float randTimeMax = 0.2f;

	// Token: 0x040075A8 RID: 30120
	[Range(0.1f, 2f)]
	public float randScaleMin = 0.65f;

	// Token: 0x040075A9 RID: 30121
	[Range(0.1f, 2f)]
	public float randScaleMax = 1f;

	// Token: 0x040075AA RID: 30122
	[Range(0f, 1f)]
	public float openRefValue = 0.2f;

	// Token: 0x040075AB RID: 30123
	private float sclNow = 1f;

	// Token: 0x040075AC RID: 30124
	private float sclStart = 1f;

	// Token: 0x040075AD RID: 30125
	private float sclEnd = 1f;
}
