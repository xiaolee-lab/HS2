using System;
using System.Collections.Generic;
using AIProject;
using FBSAssist;
using UnityEngine;

// Token: 0x020010F1 RID: 4337
[Serializable]
public class FBSBase
{
	// Token: 0x06008FE0 RID: 36832 RVA: 0x003BF3D0 File Offset: 0x003BD7D0
	public bool Init()
	{
		this.blendTimeCtrl = new TimeProgressCtrl(0.15f);
		this.blendTimeCtrl.End();
		for (int i = 0; i < this.FBSTarget.Length; i++)
		{
			this.FBSTarget[i].SetSkinnedMeshRenderer();
		}
		this.dictBackFace.Clear();
		this.dictBackFace[0] = 1f;
		this.dictNowFace.Clear();
		this.dictNowFace[0] = 1f;
		return true;
	}

	// Token: 0x06008FE1 RID: 36833 RVA: 0x003BF457 File Offset: 0x003BD857
	public void SetOpenRateForce(float rate)
	{
		this.openRate = rate;
	}

	// Token: 0x06008FE2 RID: 36834 RVA: 0x003BF460 File Offset: 0x003BD860
	public int GetMaxPtn()
	{
		if (this.FBSTarget.Length == 0)
		{
			return 0;
		}
		return this.FBSTarget[0].PtnSet.Length;
	}

	// Token: 0x06008FE3 RID: 36835 RVA: 0x003BF480 File Offset: 0x003BD880
	public void ChangePtn(int ptn, bool blend)
	{
		if (this.GetMaxPtn() <= ptn)
		{
			return;
		}
		if (this.dictNowFace.Count == 1 && this.dictNowFace.ContainsKey(ptn) && this.dictNowFace[ptn] == 1f)
		{
			return;
		}
		Dictionary<int, float> dictionary = new Dictionary<int, float>();
		dictionary[ptn] = 1f;
		this.ChangeFace(dictionary, blend);
	}

	// Token: 0x06008FE4 RID: 36836 RVA: 0x003BF4F0 File Offset: 0x003BD8F0
	public void ChangeFace(Dictionary<int, float> dictFace, bool blend)
	{
		bool flag = false;
		byte b = 0;
		float num = 0f;
		foreach (FBSTargetInfo fbstargetInfo in this.FBSTarget)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = fbstargetInfo.GetSkinnedMeshRenderer();
			foreach (int num2 in dictFace.Keys)
			{
				if (skinnedMeshRenderer.sharedMesh.blendShapeCount <= fbstargetInfo.PtnSet[num2].Close)
				{
					b = 1;
					break;
				}
				if (skinnedMeshRenderer.sharedMesh.blendShapeCount <= fbstargetInfo.PtnSet[num2].Open)
				{
					b = 1;
					break;
				}
				num += dictFace[num2];
			}
			if (b != 0)
			{
				break;
			}
			if (!flag && num > 1f)
			{
				b = 2;
				break;
			}
			flag = true;
		}
		if (b == 1)
		{
			return;
		}
		if (b == 2)
		{
			return;
		}
		this.dictBackFace.Clear();
		foreach (int key in this.dictNowFace.Keys)
		{
			this.dictBackFace[key] = this.dictNowFace[key];
		}
		this.dictNowFace.Clear();
		foreach (int key2 in dictFace.Keys)
		{
			this.dictNowFace[key2] = dictFace[key2];
		}
		if (!blend)
		{
			this.blendTimeCtrl.End();
		}
		else
		{
			this.blendTimeCtrl.Start();
		}
	}

	// Token: 0x06008FE5 RID: 36837 RVA: 0x003BF704 File Offset: 0x003BDB04
	public void SetFixedRate(float value)
	{
		this.FixedRate = value;
	}

	// Token: 0x06008FE6 RID: 36838 RVA: 0x003BF70D File Offset: 0x003BDB0D
	public void SetCorrectOpenMax(float value)
	{
		this.correctOpenMax = value;
	}

	// Token: 0x06008FE7 RID: 36839 RVA: 0x003BF718 File Offset: 0x003BDB18
	public void CalculateBlendShape()
	{
		if (this.FBSTarget.Length == 0)
		{
			return;
		}
		float b = (this.correctOpenMax >= 0f) ? this.correctOpenMax : this.OpenMax;
		float num = Mathf.Lerp(this.OpenMin, b, this.openRate);
		if (0f <= this.FixedRate)
		{
			num = this.FixedRate;
		}
		float num2 = 0f;
		if (this.blendTimeCtrl != null)
		{
			num2 = this.blendTimeCtrl.Calculate();
		}
		foreach (FBSTargetInfo fbstargetInfo in this.FBSTarget)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = fbstargetInfo.GetSkinnedMeshRenderer();
			Dictionary<int, float> dictionary = DictionaryPool<int, float>.Get();
			for (int j = 0; j < fbstargetInfo.PtnSet.Length; j++)
			{
				dictionary[fbstargetInfo.PtnSet[j].Close] = 0f;
				dictionary[fbstargetInfo.PtnSet[j].Open] = 0f;
			}
			int num3 = (int)Mathf.Clamp(num * 100f, 0f, 100f);
			if (num2 != 1f)
			{
				foreach (int num4 in this.dictBackFace.Keys)
				{
					dictionary[fbstargetInfo.PtnSet[num4].Close] = dictionary[fbstargetInfo.PtnSet[num4].Close] + this.dictBackFace[num4] * (float)(100 - num3) * (1f - num2);
					dictionary[fbstargetInfo.PtnSet[num4].Open] = dictionary[fbstargetInfo.PtnSet[num4].Open] + this.dictBackFace[num4] * (float)num3 * (1f - num2);
				}
			}
			foreach (int num5 in this.dictNowFace.Keys)
			{
				dictionary[fbstargetInfo.PtnSet[num5].Close] = dictionary[fbstargetInfo.PtnSet[num5].Close] + this.dictNowFace[num5] * (float)(100 - num3) * num2;
				dictionary[fbstargetInfo.PtnSet[num5].Open] = dictionary[fbstargetInfo.PtnSet[num5].Open] + this.dictNowFace[num5] * (float)num3 * num2;
			}
			foreach (KeyValuePair<int, float> keyValuePair in dictionary)
			{
				if (keyValuePair.Key != -1)
				{
					skinnedMeshRenderer.SetBlendShapeWeight(keyValuePair.Key, keyValuePair.Value);
				}
			}
			DictionaryPool<int, float>.Release(dictionary);
		}
	}

	// Token: 0x040074AC RID: 29868
	public FBSTargetInfo[] FBSTarget;

	// Token: 0x040074AD RID: 29869
	protected Dictionary<int, float> dictBackFace = new Dictionary<int, float>();

	// Token: 0x040074AE RID: 29870
	protected Dictionary<int, float> dictNowFace = new Dictionary<int, float>();

	// Token: 0x040074AF RID: 29871
	protected float openRate;

	// Token: 0x040074B0 RID: 29872
	[Range(0f, 1f)]
	public float OpenMin;

	// Token: 0x040074B1 RID: 29873
	[Range(0f, 1f)]
	public float OpenMax = 1f;

	// Token: 0x040074B2 RID: 29874
	[Range(-0.1f, 1f)]
	public float FixedRate = -0.1f;

	// Token: 0x040074B3 RID: 29875
	private float correctOpenMax = -1f;

	// Token: 0x040074B4 RID: 29876
	protected TimeProgressCtrl blendTimeCtrl;
}
