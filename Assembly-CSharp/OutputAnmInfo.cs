using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020010CF RID: 4303
public class OutputAnmInfo : MonoBehaviour
{
	// Token: 0x06008F3C RID: 36668 RVA: 0x003B8784 File Offset: 0x003B6B84
	private void Start()
	{
		if (string.Empty == this.outputFile)
		{
			return;
		}
		if (this.UseInfoFlag)
		{
			this.LoadUseNameList();
			this.arrUseName = this.lstUseName.ToArray();
		}
		AnimationKeyInfo animationKeyInfo = new AnimationKeyInfo();
		if (animationKeyInfo.CreateInfo(this.start, this.end, this.objAnm, this.arrUseName))
		{
			string text = Application.dataPath + "/_CustomShapeOutput";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string text2 = text + "/" + this.outputFile + ".bytes";
			animationKeyInfo.SaveInfo(text2);
			if (this.outputDebugText)
			{
				string outputPath = text2.Replace(".bytes", ".txt");
				animationKeyInfo.OutputText(outputPath);
			}
		}
		else
		{
			this.msg = this.outputFile + " の作成に失敗";
		}
	}

	// Token: 0x06008F3D RID: 36669 RVA: 0x003B8870 File Offset: 0x003B6C70
	public void LoadUseNameList()
	{
		if (null == this.taUseInfo)
		{
			return;
		}
		string[,] array;
		YS_Assist.GetListString(this.taUseInfo.text, out array);
		this.lstUseName.Clear();
		int length = array.GetLength(0);
		for (int i = 0; i < length; i++)
		{
			string item = array[i, 0].Replace("\r", string.Empty).Replace("\n", string.Empty);
			this.lstUseName.Add(item);
		}
	}

	// Token: 0x06008F3E RID: 36670 RVA: 0x003B88FC File Offset: 0x003B6CFC
	private void OnGUI()
	{
		GUI.color = Color.white;
		GUILayout.BeginArea(new Rect(10f, (float)(10 + this.msgCnt * 25), 400f, 20f));
		GUILayout.Label(this.msg, Array.Empty<GUILayoutOption>());
		GUILayout.EndArea();
	}

	// Token: 0x06008F3F RID: 36671 RVA: 0x003B894E File Offset: 0x003B6D4E
	private void Update()
	{
	}

	// Token: 0x040073C1 RID: 29633
	public int msgCnt;

	// Token: 0x040073C2 RID: 29634
	public GameObject objAnm;

	// Token: 0x040073C3 RID: 29635
	public int start;

	// Token: 0x040073C4 RID: 29636
	public int end;

	// Token: 0x040073C5 RID: 29637
	public string outputFile = string.Empty;

	// Token: 0x040073C6 RID: 29638
	public bool outputDebugText;

	// Token: 0x040073C7 RID: 29639
	[Header("--------<ExtraOption>---------------------------------------------------------------")]
	public bool UseInfoFlag;

	// Token: 0x040073C8 RID: 29640
	public TextAsset taUseInfo;

	// Token: 0x040073C9 RID: 29641
	private List<string> lstUseName = new List<string>();

	// Token: 0x040073CA RID: 29642
	private string[] arrUseName;

	// Token: 0x040073CB RID: 29643
	private string msg = "アニメキー情報作成終了";
}
