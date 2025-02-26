using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// Token: 0x0200081B RID: 2075
public class CommonLib
{
	// Token: 0x060034E2 RID: 13538 RVA: 0x00137BD4 File Offset: 0x00135FD4
	public static List<string> GetAssetBundleNameListFromPath(string path, bool subdirCheck = false)
	{
		List<string> result = new List<string>();
		if (!AssetBundleCheck.IsSimulation)
		{
			string path2 = AssetBundleManager.BaseDownloadingURL + path;
			if (subdirCheck)
			{
				List<string> list = new List<string>();
				CommonLib.GetAllFiles(path2, "*.unity3d", list);
				result = (from s in list
				select s.Replace(AssetBundleManager.BaseDownloadingURL, string.Empty)).ToList<string>();
			}
			else
			{
				if (!Directory.Exists(path2))
				{
					return result;
				}
				result = (from s in Directory.GetFiles(path2, "*.unity3d")
				select s.Replace(AssetBundleManager.BaseDownloadingURL, string.Empty)).ToList<string>();
			}
		}
		return result;
	}

	// Token: 0x060034E3 RID: 13539 RVA: 0x00137C8C File Offset: 0x0013608C
	public static void GetAllFiles(string path, string searchPattern, List<string> lst)
	{
		if (!Directory.Exists(path))
		{
			return;
		}
		lst.AddRange(Directory.GetFiles(path, searchPattern));
		string[] directories = Directory.GetDirectories(path);
		for (int i = 0; i < directories.Length; i++)
		{
			CommonLib.GetAllFiles(directories[i], searchPattern, lst);
		}
	}

	// Token: 0x060034E4 RID: 13540 RVA: 0x00137CD8 File Offset: 0x001360D8
	public static void CopySameNameTransform(Transform trfDst, Transform trfSrc)
	{
		FindAssist findAssist = new FindAssist();
		findAssist.Initialize(trfDst);
		Dictionary<string, GameObject> dictObjName = findAssist.dictObjName;
		FindAssist findAssist2 = new FindAssist();
		findAssist2.Initialize(trfSrc);
		Dictionary<string, GameObject> dictObjName2 = findAssist2.dictObjName;
		GameObject gameObject = null;
		foreach (KeyValuePair<string, GameObject> keyValuePair in dictObjName)
		{
			if (dictObjName2.TryGetValue(keyValuePair.Key, out gameObject))
			{
				keyValuePair.Value.transform.localPosition = gameObject.transform.localPosition;
				keyValuePair.Value.transform.localRotation = gameObject.transform.localRotation;
				keyValuePair.Value.transform.localScale = gameObject.transform.localScale;
			}
		}
	}

	// Token: 0x060034E5 RID: 13541 RVA: 0x00137DC0 File Offset: 0x001361C0
	public static T LoadAsset<T>(string assetBundleName, string assetName, bool clone = false, string manifestName = "") where T : UnityEngine.Object
	{
		if (AssetBundleCheck.IsSimulation)
		{
			manifestName = string.Empty;
		}
		if (!AssetBundleCheck.IsFile(assetBundleName, assetName))
		{
			string text = "読み込みエラー\r\nassetBundleName：" + assetBundleName + "\tassetName：" + assetName;
			return (T)((object)null);
		}
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(assetBundleName, assetName, typeof(T), (!manifestName.IsNullOrEmpty()) ? manifestName : null);
		if (assetBundleLoadAssetOperation.IsEmpty())
		{
			string text2 = "読み込みエラー\r\nassetName：" + assetName;
			return (T)((object)null);
		}
		T t = assetBundleLoadAssetOperation.GetAsset<T>();
		if (null != t && clone)
		{
			T t2 = UnityEngine.Object.Instantiate<T>(t);
			t2.name = t.name;
			t = t2;
		}
		return t;
	}
}
