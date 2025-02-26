using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02001115 RID: 4373
[Serializable]
public class MorphBase
{
	// Token: 0x060090F5 RID: 37109 RVA: 0x003C56CC File Offset: 0x003C3ACC
	public int GetMaxPtn()
	{
		if (this.CalcInfo.Length == 0)
		{
			return 0;
		}
		return this.CalcInfo[0].UpdateInfo.Length / 2;
	}

	// Token: 0x060090F6 RID: 37110 RVA: 0x003C56F0 File Offset: 0x003C3AF0
	protected bool CreateCalcInfo(GameObject obj)
	{
		if (null == obj)
		{
			return false;
		}
		MorphSetting morphSetting = (MorphSetting)obj.GetComponent("MorphSetting");
		if (null == morphSetting)
		{
			return false;
		}
		this.CalcInfo = null;
		GC.Collect();
		this.CalcInfo = new MorphCalcInfo[morphSetting.MorphDataList.Count];
		int num = 0;
		foreach (MorphData morphData in morphSetting.MorphDataList)
		{
			if (!(null == morphData.TargetObj))
			{
				this.CalcInfo[num] = new MorphCalcInfo();
				this.CalcInfo[num].TargetObj = morphData.TargetObj;
				MeshFilter meshFilter = new MeshFilter();
				meshFilter = (morphData.TargetObj.GetComponent(typeof(MeshFilter)) as MeshFilter);
				if (meshFilter)
				{
					this.CalcInfo[num].OriginalMesh = meshFilter.sharedMesh;
					this.CalcInfo[num].OriginalPos = meshFilter.sharedMesh.vertices;
					this.CalcInfo[num].OriginalNormal = meshFilter.sharedMesh.normals;
					this.CalcInfo[num].WeightFlags = false;
				}
				else
				{
					SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
					skinnedMeshRenderer = (morphData.TargetObj.GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer);
					this.CalcInfo[num].OriginalMesh = skinnedMeshRenderer.sharedMesh;
					this.CalcInfo[num].OriginalPos = skinnedMeshRenderer.sharedMesh.vertices;
					this.CalcInfo[num].OriginalNormal = skinnedMeshRenderer.sharedMesh.normals;
					this.CalcInfo[num].WeightFlags = true;
				}
				int num2;
				if (null == morphData.MorphArea)
				{
					num2 = this.CalcInfo[num].OriginalMesh.vertices.Length;
					this.CalcInfo[num].UpdateIndex = new int[num2];
					for (int i = 0; i < num2; i++)
					{
						this.CalcInfo[num].UpdateIndex[i] = i;
					}
				}
				else if (morphData.MorphArea.colors.Length != 0)
				{
					List<int> list = new List<int>();
					foreach (var <>__AnonType in morphData.MorphArea.colors.Select((Color value, int index) => new
					{
						value,
						index
					}))
					{
						if (<>__AnonType.value == morphData.AreaColor)
						{
							list.Add(<>__AnonType.index);
						}
					}
					this.CalcInfo[num].UpdateIndex = new int[list.Count];
					foreach (var <>__AnonType2 in list.Select((int value, int index) => new
					{
						value,
						index
					}))
					{
						this.CalcInfo[num].UpdateIndex[<>__AnonType2.index] = <>__AnonType2.value;
					}
					num2 = list.Count;
				}
				else
				{
					num2 = this.CalcInfo[num].OriginalMesh.vertices.Length;
					this.CalcInfo[num].UpdateIndex = new int[num2];
					for (int j = 0; j < num2; j++)
					{
						this.CalcInfo[num].UpdateIndex[j] = j;
					}
				}
				int num3 = morphData.MorphMesh.Length;
				this.CalcInfo[num].UpdateInfo = new MorphUpdateInfo[num3];
				for (int k = 0; k < num3; k++)
				{
					this.CalcInfo[num].UpdateInfo[k] = new MorphUpdateInfo();
					this.CalcInfo[num].UpdateInfo[k].Pos = new Vector3[num2];
					this.CalcInfo[num].UpdateInfo[k].Normmal = new Vector3[num2];
					if (null == morphData.MorphMesh[k])
					{
						for (int l = 0; l < num2; l++)
						{
							this.CalcInfo[num].UpdateInfo[k].Pos[l] = this.CalcInfo[num].OriginalMesh.vertices[this.CalcInfo[num].UpdateIndex[l]];
							this.CalcInfo[num].UpdateInfo[k].Normmal[l] = this.CalcInfo[num].OriginalMesh.normals[this.CalcInfo[num].UpdateIndex[l]];
						}
					}
					else
					{
						for (int m = 0; m < num2; m++)
						{
							this.CalcInfo[num].UpdateInfo[k].Pos[m] = morphData.MorphMesh[k].vertices[this.CalcInfo[num].UpdateIndex[m]];
							this.CalcInfo[num].UpdateInfo[k].Normmal[m] = morphData.MorphMesh[k].normals[this.CalcInfo[num].UpdateIndex[m]];
						}
					}
				}
				num++;
			}
		}
		return true;
	}

	// Token: 0x060090F7 RID: 37111 RVA: 0x003C5CFC File Offset: 0x003C40FC
	protected bool ChangeRefTargetMesh(List<MorphingTargetInfo> MorphTargetList)
	{
		foreach (MorphCalcInfo morphCalcInfo in this.CalcInfo)
		{
			if (!(null == morphCalcInfo.OriginalMesh))
			{
				Mesh mesh = null;
				foreach (MorphingTargetInfo morphingTargetInfo in MorphTargetList)
				{
					if (morphingTargetInfo.TargetObj == morphCalcInfo.TargetObj)
					{
						mesh = morphingTargetInfo.TargetMesh;
						break;
					}
				}
				if (mesh)
				{
					morphCalcInfo.TargetMesh = mesh;
				}
				else
				{
					MorphCloneMesh.Clone(out morphCalcInfo.TargetMesh, morphCalcInfo.OriginalMesh);
					morphCalcInfo.TargetMesh.name = morphCalcInfo.OriginalMesh.name;
					MorphTargetList.Add(new MorphingTargetInfo
					{
						TargetMesh = morphCalcInfo.TargetMesh,
						TargetObj = morphCalcInfo.TargetObj
					});
				}
				if (morphCalcInfo.WeightFlags)
				{
					SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
					skinnedMeshRenderer = (morphCalcInfo.TargetObj.GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer);
					skinnedMeshRenderer.sharedMesh = morphCalcInfo.TargetMesh;
				}
				else
				{
					MeshFilter meshFilter = new MeshFilter();
					meshFilter = (morphCalcInfo.TargetObj.GetComponent(typeof(MeshFilter)) as MeshFilter);
					meshFilter.sharedMesh = morphCalcInfo.TargetMesh;
				}
			}
		}
		return true;
	}

	// Token: 0x04007584 RID: 30084
	public const int morphFilesVersion = 100;

	// Token: 0x04007585 RID: 30085
	public MorphCalcInfo[] CalcInfo;
}
