using System;
using System.Collections.Generic;
using System.Linq;
using MorphAssist;
using UnityEngine;

// Token: 0x02001116 RID: 4374
[Serializable]
public class MorphFaceBase : MorphBase
{
	// Token: 0x060090FB RID: 37115 RVA: 0x003C5EBF File Offset: 0x003C42BF
	public bool Create(GameObject o)
	{
		if (!base.CreateCalcInfo(o))
		{
			return false;
		}
		this.blendTimeCtrl = new TimeProgressCtrl();
		return true;
	}

	// Token: 0x060090FC RID: 37116 RVA: 0x003C5EDB File Offset: 0x003C42DB
	public bool Init(List<MorphingTargetInfo> MorphTargetList)
	{
		base.ChangeRefTargetMesh(MorphTargetList);
		this.blendTimeCtrl = new TimeProgressCtrl();
		return true;
	}

	// Token: 0x060090FD RID: 37117 RVA: 0x003C5EF4 File Offset: 0x003C42F4
	public void ChangePtn(int ptn, bool blend)
	{
		if (this.NowPtn == ptn)
		{
			return;
		}
		this.backPtn = this.NowPtn;
		this.NowPtn = ptn;
		if (!blend)
		{
			this.blendTimeCtrl.End();
		}
		else
		{
			this.blendTimeCtrl.Start();
		}
	}

	// Token: 0x060090FE RID: 37118 RVA: 0x003C5F42 File Offset: 0x003C4342
	public void SetFixedRate(float value)
	{
		this.FixedRate = value;
	}

	// Token: 0x060090FF RID: 37119 RVA: 0x003C5F4B File Offset: 0x003C434B
	public void SetCorrectOpenMax(float value)
	{
		this.correctOpenMax = value;
	}

	// Token: 0x06009100 RID: 37120 RVA: 0x003C5F54 File Offset: 0x003C4354
	public void CalculateBlendVertex()
	{
		if (this.CalcInfo == null)
		{
			return;
		}
		float b = (this.correctOpenMax >= 0f) ? this.correctOpenMax : this.OpenMax;
		float t = Mathf.Lerp(this.OpenMin, b, this.openRate);
		if (0f <= this.FixedRate)
		{
			t = this.FixedRate;
		}
		float num = 0f;
		if (this.blendTimeCtrl != null)
		{
			num = this.blendTimeCtrl.Calculate();
		}
		if (num == 1f)
		{
			foreach (MorphCalcInfo morphCalcInfo in this.CalcInfo)
			{
				if (!(null == morphCalcInfo.TargetMesh))
				{
					if (this.NowPtn * 2 + 1 < morphCalcInfo.UpdateInfo.Length)
					{
						Vector3[] vertices = morphCalcInfo.TargetMesh.vertices;
						foreach (var <>__AnonType in morphCalcInfo.UpdateIndex.Select((int value, int index) => new
						{
							value,
							index
						}))
						{
							vertices[<>__AnonType.value] = Vector3.Lerp(morphCalcInfo.UpdateInfo[this.NowPtn * 2].Pos[<>__AnonType.index], morphCalcInfo.UpdateInfo[this.NowPtn * 2 + 1].Pos[<>__AnonType.index], t);
						}
						morphCalcInfo.TargetMesh.vertices = vertices;
						if (this.BlendNormals)
						{
							Vector3[] normals = morphCalcInfo.TargetMesh.normals;
							foreach (var <>__AnonType2 in morphCalcInfo.UpdateIndex.Select((int value, int index) => new
							{
								value,
								index
							}))
							{
								normals[<>__AnonType2.value] = Vector3.Lerp(morphCalcInfo.UpdateInfo[this.NowPtn * 2].Normmal[<>__AnonType2.index], morphCalcInfo.UpdateInfo[this.NowPtn * 2 + 1].Normmal[<>__AnonType2.index], t);
							}
							morphCalcInfo.TargetMesh.normals = normals;
						}
					}
				}
			}
		}
		else
		{
			foreach (MorphCalcInfo morphCalcInfo2 in this.CalcInfo)
			{
				if (!(null == morphCalcInfo2.TargetMesh))
				{
					if (this.NowPtn * 2 + 1 < morphCalcInfo2.UpdateInfo.Length)
					{
						if (this.backPtn * 2 + 1 < morphCalcInfo2.UpdateInfo.Length)
						{
							Vector3[] vertices2 = morphCalcInfo2.TargetMesh.vertices;
							foreach (var <>__AnonType3 in morphCalcInfo2.UpdateIndex.Select((int value, int index) => new
							{
								value,
								index
							}))
							{
								Vector3 a = Vector3.Lerp(morphCalcInfo2.UpdateInfo[this.backPtn * 2].Pos[<>__AnonType3.index], morphCalcInfo2.UpdateInfo[this.backPtn * 2 + 1].Pos[<>__AnonType3.index], t);
								Vector3 b2 = Vector3.Lerp(morphCalcInfo2.UpdateInfo[this.NowPtn * 2].Pos[<>__AnonType3.index], morphCalcInfo2.UpdateInfo[this.NowPtn * 2 + 1].Pos[<>__AnonType3.index], t);
								vertices2[<>__AnonType3.value] = Vector3.Lerp(a, b2, num);
							}
							morphCalcInfo2.TargetMesh.vertices = vertices2;
							if (this.BlendNormals)
							{
								Vector3[] normals2 = morphCalcInfo2.TargetMesh.normals;
								foreach (var <>__AnonType4 in morphCalcInfo2.UpdateIndex.Select((int value, int index) => new
								{
									value,
									index
								}))
								{
									Vector3 a2 = Vector3.Lerp(morphCalcInfo2.UpdateInfo[this.backPtn * 2].Normmal[<>__AnonType4.index], morphCalcInfo2.UpdateInfo[this.backPtn * 2 + 1].Normmal[<>__AnonType4.index], t);
									Vector3 b3 = Vector3.Lerp(morphCalcInfo2.UpdateInfo[this.NowPtn * 2].Normmal[<>__AnonType4.index], morphCalcInfo2.UpdateInfo[this.NowPtn * 2 + 1].Normmal[<>__AnonType4.index], t);
									normals2[<>__AnonType4.value] = Vector3.Lerp(a2, b3, num);
								}
								morphCalcInfo2.TargetMesh.normals = normals2;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x04007588 RID: 30088
	protected int backPtn;

	// Token: 0x04007589 RID: 30089
	[Range(0f, 255f)]
	public int NowPtn;

	// Token: 0x0400758A RID: 30090
	[Range(0f, 1f)]
	protected float openRate;

	// Token: 0x0400758B RID: 30091
	[Range(0f, 1f)]
	public float OpenMin;

	// Token: 0x0400758C RID: 30092
	[Range(0f, 1f)]
	public float OpenMax = 1f;

	// Token: 0x0400758D RID: 30093
	[Range(-0.1f, 1f)]
	public float FixedRate = -0.1f;

	// Token: 0x0400758E RID: 30094
	private float correctOpenMax = -1f;

	// Token: 0x0400758F RID: 30095
	public bool BlendNormals;

	// Token: 0x04007590 RID: 30096
	protected TimeProgressCtrl blendTimeCtrl;
}
