using System;
using System.IO;
using UnityEngine;

// Token: 0x020011A8 RID: 4520
public class FileData
{
	// Token: 0x0600949D RID: 38045 RVA: 0x003D4DD3 File Offset: 0x003D31D3
	public FileData(string rootName = "")
	{
		this.rootName = rootName;
	}

	// Token: 0x0600949E RID: 38046 RVA: 0x003D4DF0 File Offset: 0x003D31F0
	public string Create(string name)
	{
		string text = this.Path + name;
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return text + '/';
	}

	// Token: 0x17001F88 RID: 8072
	// (get) Token: 0x0600949F RID: 38047 RVA: 0x003D4E2C File Offset: 0x003D322C
	public string Path
	{
		get
		{
			string text;
			if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
			{
				text = Application.dataPath + "/../";
			}
			else
			{
				text = Application.persistentDataPath + "/";
			}
			if (this.rootName != string.Empty)
			{
				text = text + this.rootName + '/';
			}
			return text;
		}
	}

	// Token: 0x0400778F RID: 30607
	private string rootName = string.Empty;
}
