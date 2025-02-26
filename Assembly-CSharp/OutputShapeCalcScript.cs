using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x020010D0 RID: 4304
public class OutputShapeCalcScript : MonoBehaviour
{
	// Token: 0x06008F41 RID: 36673 RVA: 0x003B8970 File Offset: 0x003B6D70
	private void Start()
	{
		this.dictBone.Clear();
		if (null != this.text)
		{
			string[,] array = null;
			YS_Assist.GetListString(this.text.text, out array);
			OutputShapeCalcScript.Info info = null;
			int length = array.GetLength(0);
			int length2 = array.GetLength(1);
			if (length != 0 && length2 != 0)
			{
				for (int i = 0; i < length; i++)
				{
					if (!this.dictBone.TryGetValue(array[i, 0], out info))
					{
						info = new OutputShapeCalcScript.Info();
						this.dictBone[array[i, 0]] = info;
					}
					if ("v" == array[i, 2])
					{
						info.lstPosX.Add(array[i, 1]);
					}
					if ("v" == array[i, 3])
					{
						info.lstPosY.Add(array[i, 1]);
					}
					if ("v" == array[i, 4])
					{
						info.lstPosZ.Add(array[i, 1]);
					}
					if ("v" == array[i, 5])
					{
						info.lstRotX.Add(array[i, 1]);
					}
					if ("v" == array[i, 6])
					{
						info.lstRotY.Add(array[i, 1]);
					}
					if ("v" == array[i, 7])
					{
						info.lstRotZ.Add(array[i, 1]);
					}
					if ("v" == array[i, 8])
					{
						info.lstSclX.Add(array[i, 1]);
					}
					if ("v" == array[i, 9])
					{
						info.lstSclY.Add(array[i, 1]);
					}
					if ("v" == array[i, 10])
					{
						info.lstSclZ.Add(array[i, 1]);
					}
					if (!this.lstSrc.Contains(array[i, 1]))
					{
						this.lstSrc.Add(array[i, 1]);
					}
				}
			}
			string outputPath = Application.dataPath + "/shapecalc.txt";
			this.OutputText(outputPath);
		}
	}

	// Token: 0x06008F42 RID: 36674 RVA: 0x003B8BEC File Offset: 0x003B6FEC
	public void OutputText(string outputPath)
	{
		StringBuilder stringBuilder = new StringBuilder(2048);
		stringBuilder.Length = 0;
		stringBuilder.Append("=== 計算式 ===================================================================\n");
		foreach (KeyValuePair<string, OutputShapeCalcScript.Info> keyValuePair in this.dictBone)
		{
			if (keyValuePair.Value.lstPosX.Count != 0)
			{
				stringBuilder.Append("dictDstBoneInfo[(int)DstBoneName.").Append(keyValuePair.Key).Append("].trfBone.SetLocalPositionX(");
				for (int i = 0; i < keyValuePair.Value.lstPosX.Count; i++)
				{
					stringBuilder.Append("dictSrcBoneInfo[(int)SrcBoneName.").Append(keyValuePair.Value.lstPosX[i]).Append("].vctPos.x");
					if (i + 1 < keyValuePair.Value.lstPosX.Count)
					{
						stringBuilder.Append(" + ");
					}
					else
					{
						stringBuilder.Append(");\n");
					}
				}
			}
			if (keyValuePair.Value.lstPosY.Count != 0)
			{
				stringBuilder.Append("dictDstBoneInfo[(int)DstBoneName.").Append(keyValuePair.Key).Append("].trfBone.SetLocalPositionY(");
				for (int j = 0; j < keyValuePair.Value.lstPosY.Count; j++)
				{
					stringBuilder.Append("dictSrcBoneInfo[(int)SrcBoneName.").Append(keyValuePair.Value.lstPosY[j]).Append("].vctPos.y");
					if (j + 1 < keyValuePair.Value.lstPosY.Count)
					{
						stringBuilder.Append(" + ");
					}
					else
					{
						stringBuilder.Append(");\n");
					}
				}
			}
			if (keyValuePair.Value.lstPosZ.Count != 0)
			{
				stringBuilder.Append("dictDstBoneInfo[(int)DstBoneName.").Append(keyValuePair.Key).Append("].trfBone.SetLocalPositionZ(");
				for (int k = 0; k < keyValuePair.Value.lstPosZ.Count; k++)
				{
					stringBuilder.Append("dictSrcBoneInfo[(int)SrcBoneName.").Append(keyValuePair.Value.lstPosZ[k]).Append("].vctPos.z");
					if (k + 1 < keyValuePair.Value.lstPosZ.Count)
					{
						stringBuilder.Append(" + ");
					}
					else
					{
						stringBuilder.Append(");\n");
					}
				}
			}
			if (keyValuePair.Value.lstRotX.Count != 0 || keyValuePair.Value.lstRotY.Count != 0 || keyValuePair.Value.lstRotZ.Count != 0)
			{
				stringBuilder.Append("dictDstBoneInfo[(int)DstBoneName.").Append(keyValuePair.Key).Append("].trfBone.SetLocalRotation(\n");
				stringBuilder.Append("\t");
				if (keyValuePair.Value.lstRotX.Count != 0)
				{
					for (int l = 0; l < keyValuePair.Value.lstRotX.Count; l++)
					{
						stringBuilder.Append("dictSrcBoneInfo[(int)SrcBoneName.").Append(keyValuePair.Value.lstRotX[l]).Append("].vctRot.x");
						if (l + 1 < keyValuePair.Value.lstRotX.Count)
						{
							stringBuilder.Append(" + ");
						}
						else
						{
							stringBuilder.Append(",\n");
						}
					}
				}
				else
				{
					stringBuilder.Append("0.0f,\n");
				}
				stringBuilder.Append("\t");
				if (keyValuePair.Value.lstRotY.Count != 0)
				{
					for (int m = 0; m < keyValuePair.Value.lstRotY.Count; m++)
					{
						stringBuilder.Append("dictSrcBoneInfo[(int)SrcBoneName.").Append(keyValuePair.Value.lstRotY[m]).Append("].vctRot.y");
						if (m + 1 < keyValuePair.Value.lstRotY.Count)
						{
							stringBuilder.Append(" + ");
						}
						else
						{
							stringBuilder.Append(",\n");
						}
					}
				}
				else
				{
					stringBuilder.Append("0.0f,\n");
				}
				stringBuilder.Append("\t");
				if (keyValuePair.Value.lstRotZ.Count != 0)
				{
					for (int n = 0; n < keyValuePair.Value.lstRotZ.Count; n++)
					{
						stringBuilder.Append("dictSrcBoneInfo[(int)SrcBoneName.").Append(keyValuePair.Value.lstRotZ[n]).Append("].vctRot.z");
						if (n + 1 < keyValuePair.Value.lstRotZ.Count)
						{
							stringBuilder.Append(" + ");
						}
						else
						{
							stringBuilder.Append(");\n");
						}
					}
				}
				else
				{
					stringBuilder.Append("0.0f);\n");
				}
			}
			if (keyValuePair.Value.lstSclX.Count != 0 || keyValuePair.Value.lstSclY.Count != 0 || keyValuePair.Value.lstSclZ.Count != 0)
			{
				stringBuilder.Append("dictDstBoneInfo[(int)DstBoneName.").Append(keyValuePair.Key).Append("].trfBone.SetLocalScale(\n");
				stringBuilder.Append("\t");
				if (keyValuePair.Value.lstSclX.Count != 0)
				{
					for (int num = 0; num < keyValuePair.Value.lstSclX.Count; num++)
					{
						stringBuilder.Append("dictSrcBoneInfo[(int)SrcBoneName.").Append(keyValuePair.Value.lstSclX[num]).Append("].vctScl.x");
						if (num + 1 < keyValuePair.Value.lstSclX.Count)
						{
							stringBuilder.Append(" + ");
						}
						else
						{
							stringBuilder.Append(",\n");
						}
					}
				}
				else
				{
					stringBuilder.Append("1.0f,\n");
				}
				stringBuilder.Append("\t");
				if (keyValuePair.Value.lstSclY.Count != 0)
				{
					for (int num2 = 0; num2 < keyValuePair.Value.lstSclY.Count; num2++)
					{
						stringBuilder.Append("dictSrcBoneInfo[(int)SrcBoneName.").Append(keyValuePair.Value.lstSclY[num2]).Append("].vctScl.y");
						if (num2 + 1 < keyValuePair.Value.lstSclY.Count)
						{
							stringBuilder.Append(" + ");
						}
						else
						{
							stringBuilder.Append(",\n");
						}
					}
				}
				else
				{
					stringBuilder.Append("1.0f,\n");
				}
				stringBuilder.Append("\t");
				if (keyValuePair.Value.lstSclZ.Count != 0)
				{
					for (int num3 = 0; num3 < keyValuePair.Value.lstSclZ.Count; num3++)
					{
						stringBuilder.Append("dictSrcBoneInfo[(int)SrcBoneName.").Append(keyValuePair.Value.lstSclZ[num3]).Append("].vctScl.z");
						if (num3 + 1 < keyValuePair.Value.lstSclZ.Count)
						{
							stringBuilder.Append(" + ");
						}
						else
						{
							stringBuilder.Append(");\n");
						}
					}
				}
				else
				{
					stringBuilder.Append("1.0f);\n");
				}
			}
			stringBuilder.Append("\n");
		}
		for (int num4 = 0; num4 < 10; num4++)
		{
			stringBuilder.Append("\n");
		}
		stringBuilder.Append("=== 転送先 ===================================================================\n");
		stringBuilder.Append("public enum DstBoneName\n").Append("{\n");
		int num5 = 0;
		foreach (KeyValuePair<string, OutputShapeCalcScript.Info> keyValuePair2 in this.dictBone)
		{
			stringBuilder.Append("\t").Append(keyValuePair2.Key).Append(",");
			num5++;
			if (num5 == 4)
			{
				stringBuilder.Append("\n");
				num5 = 0;
			}
			else
			{
				stringBuilder.Append("\t\t\t");
			}
		}
		stringBuilder.Append("\n};");
		for (int num6 = 0; num6 < 10; num6++)
		{
			stringBuilder.Append("\n");
		}
		stringBuilder.Append("=== 転送元 ===================================================================\n");
		stringBuilder.Append("public enum SrcBoneName\n").Append("{\n");
		num5 = 0;
		for (int num7 = 0; num7 < this.lstSrc.Count; num7++)
		{
			stringBuilder.Append("\t").Append(this.lstSrc[num7]).Append(",");
			num5++;
			if (num5 == 4)
			{
				stringBuilder.Append("\n");
				num5 = 0;
			}
			else
			{
				stringBuilder.Append("\t\t\t");
			}
		}
		stringBuilder.Append("\n};");
		using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
		{
			using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
			{
				streamWriter.Write(stringBuilder.ToString());
				streamWriter.Write("\n");
			}
		}
	}

	// Token: 0x06008F43 RID: 36675 RVA: 0x003B9640 File Offset: 0x003B7A40
	private void OnGUI()
	{
		GUI.color = Color.white;
		GUILayout.BeginArea(new Rect(10f, 10f, 400f, 20f));
		GUILayout.Label("シェイプ計算スクリプト補助データ作成終了", Array.Empty<GUILayoutOption>());
		GUILayout.EndArea();
	}

	// Token: 0x06008F44 RID: 36676 RVA: 0x003B967E File Offset: 0x003B7A7E
	private void Update()
	{
	}

	// Token: 0x040073CC RID: 29644
	public TextAsset text;

	// Token: 0x040073CD RID: 29645
	private Dictionary<string, OutputShapeCalcScript.Info> dictBone = new Dictionary<string, OutputShapeCalcScript.Info>();

	// Token: 0x040073CE RID: 29646
	private List<string> lstSrc = new List<string>();

	// Token: 0x020010D1 RID: 4305
	public class Info
	{
		// Token: 0x040073CF RID: 29647
		public List<string> lstPosX = new List<string>();

		// Token: 0x040073D0 RID: 29648
		public List<string> lstPosY = new List<string>();

		// Token: 0x040073D1 RID: 29649
		public List<string> lstPosZ = new List<string>();

		// Token: 0x040073D2 RID: 29650
		public List<string> lstRotX = new List<string>();

		// Token: 0x040073D3 RID: 29651
		public List<string> lstRotY = new List<string>();

		// Token: 0x040073D4 RID: 29652
		public List<string> lstRotZ = new List<string>();

		// Token: 0x040073D5 RID: 29653
		public List<string> lstSclX = new List<string>();

		// Token: 0x040073D6 RID: 29654
		public List<string> lstSclY = new List<string>();

		// Token: 0x040073D7 RID: 29655
		public List<string> lstSclZ = new List<string>();
	}
}
