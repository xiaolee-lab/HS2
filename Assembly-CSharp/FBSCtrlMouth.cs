using System;
using FBSAssist;
using UnityEngine;

// Token: 0x020010F7 RID: 4343
[Serializable]
public class FBSCtrlMouth : FBSBase
{
	// Token: 0x06008FFB RID: 36859 RVA: 0x003BFE36 File Offset: 0x003BE236
	public float GetAdjustWidthScale()
	{
		return this.adjustWidthScale;
	}

	// Token: 0x06008FFC RID: 36860 RVA: 0x003BFE3E File Offset: 0x003BE23E
	public new void Init()
	{
		base.Init();
		this.tpcRand = new TimeProgressCtrlRandom();
		this.tpcRand.Init(this.randTimeMin, this.randTimeMax);
	}

	// Token: 0x06008FFD RID: 36861 RVA: 0x003BFE69 File Offset: 0x003BE269
	public void CalcBlend(float openValue)
	{
		this.openRate = openValue;
		base.CalculateBlendShape();
		if (this.useAjustWidthScale)
		{
			this.AdjustWidthScale();
		}
	}

	// Token: 0x06008FFE RID: 36862 RVA: 0x003BFE8A File Offset: 0x003BE28A
	public void UseAdjustWidthScale(bool useFlags)
	{
		this.useAjustWidthScale = useFlags;
	}

	// Token: 0x06008FFF RID: 36863 RVA: 0x003BFE94 File Offset: 0x003BE294
	public bool AdjustWidthScale()
	{
		this.adjustWidthScale = 1f;
		bool flag = false;
		float num = this.tpcRand.Calculate(this.randTimeMin, this.randTimeMax);
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
			this.adjustWidthScale = this.sclNow;
		}
		if (null != this.objAdjustWidthScale)
		{
			this.objAdjustWidthScale.transform.localScale = new Vector3(this.adjustWidthScale, 1f, 1f);
		}
		return true;
	}

	// Token: 0x040074C3 RID: 29891
	public bool useAjustWidthScale;

	// Token: 0x040074C4 RID: 29892
	private TimeProgressCtrlRandom tpcRand;

	// Token: 0x040074C5 RID: 29893
	public GameObject objAdjustWidthScale;

	// Token: 0x040074C6 RID: 29894
	[Range(0.01f, 1f)]
	public float randTimeMin = 0.1f;

	// Token: 0x040074C7 RID: 29895
	[Range(0.01f, 1f)]
	public float randTimeMax = 0.2f;

	// Token: 0x040074C8 RID: 29896
	[Range(0.1f, 2f)]
	public float randScaleMin = 0.65f;

	// Token: 0x040074C9 RID: 29897
	[Range(0.1f, 2f)]
	public float randScaleMax = 1f;

	// Token: 0x040074CA RID: 29898
	[Range(0f, 1f)]
	public float openRefValue = 0.2f;

	// Token: 0x040074CB RID: 29899
	private float sclNow = 1f;

	// Token: 0x040074CC RID: 29900
	private float sclStart = 1f;

	// Token: 0x040074CD RID: 29901
	private float sclEnd = 1f;

	// Token: 0x040074CE RID: 29902
	private float adjustWidthScale = 1f;
}
