using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x020010D2 RID: 4306
public class AnimationKeyInfo
{
	// Token: 0x06008F47 RID: 36679 RVA: 0x003B970C File Offset: 0x003B7B0C
	public int GetKeyCount()
	{
		if (this.dictInfo == null)
		{
			return 0;
		}
		if (this.dictInfo.Count == 0)
		{
			return 0;
		}
		List<AnimationKeyInfo.AnmKeyInfo> list = this.dictInfo.Values.ToList<List<AnimationKeyInfo.AnmKeyInfo>>()[0];
		return list.Count;
	}

	// Token: 0x06008F48 RID: 36680 RVA: 0x003B9758 File Offset: 0x003B7B58
	public bool CreateInfo(int start, int end, GameObject obj, string[] usename)
	{
		if (null == obj)
		{
			return false;
		}
		Animator component = obj.GetComponent<Animator>();
		if (null == component)
		{
			return false;
		}
		this.dictInfo.Clear();
		float num = 1f / (float)(end - start);
		int num2 = end - start + 1;
		for (int i = 0; i < num2; i++)
		{
			float normalizedTime = num * (float)i;
			component.Play(component.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, normalizedTime);
			component.Update(0f);
			this.CreateInfoLoop(i, obj.transform, usename);
		}
		return true;
	}

	// Token: 0x06008F49 RID: 36681 RVA: 0x003B97F0 File Offset: 0x003B7BF0
	private void CreateInfoLoop(int no, Transform tf, string[] usename)
	{
		if (null == tf)
		{
			return;
		}
		List<AnimationKeyInfo.AnmKeyInfo> list = null;
		bool flag = false;
		if (usename != null && Array.IndexOf<string>(usename, tf.name) == -1)
		{
			flag = true;
		}
		if (!flag)
		{
			if (!this.dictInfo.TryGetValue(tf.name, out list))
			{
				list = new List<AnimationKeyInfo.AnmKeyInfo>();
				this.dictInfo[tf.name] = list;
			}
			AnimationKeyInfo.AnmKeyInfo anmKeyInfo = new AnimationKeyInfo.AnmKeyInfo();
			anmKeyInfo.Set(no, tf.localPosition, tf.localEulerAngles, tf.localScale);
			list.Add(anmKeyInfo);
		}
		for (int i = 0; i < tf.childCount; i++)
		{
			Transform child = tf.GetChild(i);
			this.CreateInfoLoop(no, child, usename);
		}
	}

	// Token: 0x06008F4A RID: 36682 RVA: 0x003B98B0 File Offset: 0x003B7CB0
	public bool GetInfo(string name, float rate, ref Vector3 value, byte type)
	{
		List<AnimationKeyInfo.AnmKeyInfo> list = null;
		if (!this.dictInfo.TryGetValue(name, out list))
		{
			return false;
		}
		if (type == 0)
		{
			if (rate == 0f)
			{
				value = list[0].pos;
			}
			else if (rate == 1f)
			{
				value = list[list.Count - 1].pos;
			}
			else
			{
				float num = (float)(list.Count - 1) * rate;
				int num2 = Mathf.FloorToInt(num);
				float t = num - (float)num2;
				value = Vector3.Lerp(list[num2].pos, list[num2 + 1].pos, t);
			}
		}
		else if (type == 1)
		{
			if (rate == 0f)
			{
				value = list[0].rot;
			}
			else if (rate == 1f)
			{
				value = list[list.Count - 1].rot;
			}
			else
			{
				float num3 = (float)(list.Count - 1) * rate;
				int num4 = Mathf.FloorToInt(num3);
				float t2 = num3 - (float)num4;
				value.x = Mathf.LerpAngle(list[num4].rot.x, list[num4 + 1].rot.x, t2);
				value.y = Mathf.LerpAngle(list[num4].rot.y, list[num4 + 1].rot.y, t2);
				value.z = Mathf.LerpAngle(list[num4].rot.z, list[num4 + 1].rot.z, t2);
			}
		}
		else if (rate == 0f)
		{
			value = list[0].scl;
		}
		else if (rate == 1f)
		{
			value = list[list.Count - 1].scl;
		}
		else
		{
			float num5 = (float)(list.Count - 1) * rate;
			int num6 = Mathf.FloorToInt(num5);
			float t3 = num5 - (float)num6;
			value = Vector3.Lerp(list[num6].scl, list[num6 + 1].scl, t3);
		}
		return true;
	}

	// Token: 0x06008F4B RID: 36683 RVA: 0x003B9B0C File Offset: 0x003B7F0C
	public bool GetInfo(string name, int key, ref Vector3 value, byte type)
	{
		List<AnimationKeyInfo.AnmKeyInfo> list = null;
		if (!this.dictInfo.TryGetValue(name, out list))
		{
			return false;
		}
		if (list.Count <= key)
		{
			return false;
		}
		if (type == 0)
		{
			value = list[key].pos;
		}
		else if (type == 1)
		{
			value = list[key].rot;
		}
		else
		{
			value = list[key].scl;
		}
		return true;
	}

	// Token: 0x06008F4C RID: 36684 RVA: 0x003B9B90 File Offset: 0x003B7F90
	public bool GetInfo(string name, float rate, ref Vector3[] value, bool[] flag)
	{
		if (value.Length != 3 || flag.Length != 3)
		{
			return false;
		}
		List<AnimationKeyInfo.AnmKeyInfo> list = null;
		if (!this.dictInfo.TryGetValue(name, out list))
		{
			return false;
		}
		if (flag[0])
		{
			if (rate == 0f)
			{
				value[0] = list[0].pos;
			}
			else if (rate == 1f)
			{
				value[0] = list[list.Count - 1].pos;
			}
			else
			{
				float num = (float)(list.Count - 1) * rate;
				int num2 = Mathf.FloorToInt(num);
				float t = num - (float)num2;
				value[0] = Vector3.Lerp(list[num2].pos, list[num2 + 1].pos, t);
			}
		}
		if (flag[1])
		{
			if (rate == 0f)
			{
				value[1] = list[0].rot;
			}
			else if (rate == 1f)
			{
				value[1] = list[list.Count - 1].rot;
			}
			else
			{
				float num3 = (float)(list.Count - 1) * rate;
				int num4 = Mathf.FloorToInt(num3);
				float t2 = num3 - (float)num4;
				value[1].x = Mathf.LerpAngle(list[num4].rot.x, list[num4 + 1].rot.x, t2);
				value[1].y = Mathf.LerpAngle(list[num4].rot.y, list[num4 + 1].rot.y, t2);
				value[1].z = Mathf.LerpAngle(list[num4].rot.z, list[num4 + 1].rot.z, t2);
			}
		}
		if (flag[2])
		{
			if (rate == 0f)
			{
				value[2] = list[0].scl;
			}
			else if (rate == 1f)
			{
				value[2] = list[list.Count - 1].scl;
			}
			else
			{
				float num5 = (float)(list.Count - 1) * rate;
				int num6 = Mathf.FloorToInt(num5);
				float t3 = num5 - (float)num6;
				value[2] = Vector3.Lerp(list[num6].scl, list[num6 + 1].scl, t3);
			}
		}
		return true;
	}

	// Token: 0x06008F4D RID: 36685 RVA: 0x003B9E50 File Offset: 0x003B8250
	public bool GetInfo(string name, int key, ref Vector3[] value, bool[] flag)
	{
		if (value.Length != 3 || flag.Length != 3)
		{
			return false;
		}
		List<AnimationKeyInfo.AnmKeyInfo> list = null;
		if (!this.dictInfo.TryGetValue(name, out list))
		{
			return false;
		}
		if (list.Count <= key)
		{
			return false;
		}
		if (flag[0])
		{
			value[0] = list[key].pos;
		}
		if (flag[1])
		{
			value[1] = list[key].rot;
		}
		if (flag[2])
		{
			value[2] = list[key].scl;
		}
		return true;
	}

	// Token: 0x06008F4E RID: 36686 RVA: 0x003B9F00 File Offset: 0x003B8300
	public void SaveInfo(string filepath)
	{
		using (FileStream fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write))
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
			{
				int count = this.dictInfo.Count;
				binaryWriter.Write(count);
				foreach (KeyValuePair<string, List<AnimationKeyInfo.AnmKeyInfo>> keyValuePair in this.dictInfo)
				{
					binaryWriter.Write(keyValuePair.Key);
					binaryWriter.Write(keyValuePair.Value.Count);
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						binaryWriter.Write(keyValuePair.Value[i].no);
						binaryWriter.Write(keyValuePair.Value[i].pos.x);
						binaryWriter.Write(keyValuePair.Value[i].pos.y);
						binaryWriter.Write(keyValuePair.Value[i].pos.z);
						binaryWriter.Write(keyValuePair.Value[i].rot.x);
						binaryWriter.Write(keyValuePair.Value[i].rot.y);
						binaryWriter.Write(keyValuePair.Value[i].rot.z);
						binaryWriter.Write(keyValuePair.Value[i].scl.x);
						binaryWriter.Write(keyValuePair.Value[i].scl.y);
						binaryWriter.Write(keyValuePair.Value[i].scl.z);
					}
				}
			}
		}
	}

	// Token: 0x06008F4F RID: 36687 RVA: 0x003BA140 File Offset: 0x003B8540
	public void LoadInfo(string filePath)
	{
		using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
		{
			this.LoadInfo(fileStream);
		}
	}

	// Token: 0x06008F50 RID: 36688 RVA: 0x003BA180 File Offset: 0x003B8580
	public void LoadInfo(string manifest, string assetBundleName, string assetName, Action<string, string> funcAssetBundleEntry = null)
	{
		if (AssetBundleCheck.IsSimulation)
		{
			manifest = string.Empty;
		}
		if (!AssetBundleCheck.IsFile(assetBundleName, assetName))
		{
			string text = "読み込みエラー\r\nassetBundleName：" + assetBundleName + "\tassetName：" + assetName;
			return;
		}
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(assetBundleName, assetName, typeof(TextAsset), manifest);
		if (assetBundleLoadAssetOperation == null)
		{
			string text2 = "読み込みエラー\r\nassetName：" + assetName;
			return;
		}
		TextAsset asset = assetBundleLoadAssetOperation.GetAsset<TextAsset>();
		if (null == asset)
		{
			return;
		}
		using (MemoryStream memoryStream = new MemoryStream())
		{
			memoryStream.Write(asset.bytes, 0, asset.bytes.Length);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			this.LoadInfo(memoryStream);
		}
		if (funcAssetBundleEntry == null)
		{
			AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
		}
		else
		{
			funcAssetBundleEntry(assetBundleName, string.Empty);
		}
	}

	// Token: 0x06008F51 RID: 36689 RVA: 0x003BA270 File Offset: 0x003B8670
	public void LoadInfo(Stream st)
	{
		using (BinaryReader binaryReader = new BinaryReader(st))
		{
			int num = binaryReader.ReadInt32();
			this.dictInfo.Clear();
			for (int i = 0; i < num; i++)
			{
				List<AnimationKeyInfo.AnmKeyInfo> list = new List<AnimationKeyInfo.AnmKeyInfo>();
				string key = binaryReader.ReadString();
				this.dictInfo[key] = list;
				int num2 = binaryReader.ReadInt32();
				for (int j = 0; j < num2; j++)
				{
					AnimationKeyInfo.AnmKeyInfo anmKeyInfo = new AnimationKeyInfo.AnmKeyInfo();
					anmKeyInfo.no = binaryReader.ReadInt32();
					anmKeyInfo.pos.x = binaryReader.ReadSingle();
					anmKeyInfo.pos.y = binaryReader.ReadSingle();
					anmKeyInfo.pos.z = binaryReader.ReadSingle();
					anmKeyInfo.rot.x = binaryReader.ReadSingle();
					anmKeyInfo.rot.y = binaryReader.ReadSingle();
					anmKeyInfo.rot.z = binaryReader.ReadSingle();
					anmKeyInfo.scl.x = binaryReader.ReadSingle();
					anmKeyInfo.scl.y = binaryReader.ReadSingle();
					anmKeyInfo.scl.z = binaryReader.ReadSingle();
					list.Add(anmKeyInfo);
				}
			}
		}
	}

	// Token: 0x06008F52 RID: 36690 RVA: 0x003BA3D0 File Offset: 0x003B87D0
	public void OutputText(string outputPath)
	{
		StringBuilder stringBuilder = new StringBuilder(2048);
		stringBuilder.Length = 0;
		foreach (KeyValuePair<string, List<AnimationKeyInfo.AnmKeyInfo>> keyValuePair in this.dictInfo)
		{
			for (int i = 0; i < keyValuePair.Value.Count; i++)
			{
				stringBuilder.Append(keyValuePair.Key).Append("\t");
				stringBuilder.Append(keyValuePair.Value[i].GetInfoStr());
				stringBuilder.Append("\n");
			}
		}
		using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
		{
			using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
			{
				streamWriter.Write(stringBuilder.ToString());
				streamWriter.Write("\n");
			}
		}
	}

	// Token: 0x040073D8 RID: 29656
	private Dictionary<string, List<AnimationKeyInfo.AnmKeyInfo>> dictInfo = new Dictionary<string, List<AnimationKeyInfo.AnmKeyInfo>>();

	// Token: 0x020010D3 RID: 4307
	public class AnmKeyInfo
	{
		// Token: 0x06008F54 RID: 36692 RVA: 0x003BA544 File Offset: 0x003B8944
		public void Set(int _no, Vector3 _pos, Vector3 _rot, Vector3 _scl)
		{
			this.no = _no;
			this.pos = _pos;
			this.rot = _rot;
			this.scl = _scl;
		}

		// Token: 0x06008F55 RID: 36693 RVA: 0x003BA564 File Offset: 0x003B8964
		public string GetInfoStr()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(this.no.ToString()).Append("\t");
			stringBuilder.Append(this.pos.ToString("f7")).Append("\t");
			stringBuilder.Append(this.rot.ToString("f7")).Append("\t");
			stringBuilder.Append(this.scl.ToString("f7"));
			return stringBuilder.ToString();
		}

		// Token: 0x040073D9 RID: 29657
		public int no;

		// Token: 0x040073DA RID: 29658
		public Vector3 pos = default(Vector3);

		// Token: 0x040073DB RID: 29659
		public Vector3 rot = default(Vector3);

		// Token: 0x040073DC RID: 29660
		public Vector3 scl = default(Vector3);
	}
}
