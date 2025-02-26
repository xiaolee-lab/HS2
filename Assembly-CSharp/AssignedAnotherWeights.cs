using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IllusionUtility.GetUtility;
using UnityEngine;

// Token: 0x02001167 RID: 4455
public class AssignedAnotherWeights
{
	// Token: 0x06009318 RID: 37656 RVA: 0x003CFDAD File Offset: 0x003CE1AD
	public AssignedAnotherWeights()
	{
		this.dictBone = new Dictionary<string, GameObject>();
	}

	// Token: 0x17001F79 RID: 8057
	// (get) Token: 0x06009319 RID: 37657 RVA: 0x003CFDC0 File Offset: 0x003CE1C0
	// (set) Token: 0x0600931A RID: 37658 RVA: 0x003CFDC8 File Offset: 0x003CE1C8
	public Dictionary<string, GameObject> dictBone { get; private set; }

	// Token: 0x0600931B RID: 37659 RVA: 0x003CFDD1 File Offset: 0x003CE1D1
	public void Release()
	{
		this.dictBone.Clear();
	}

	// Token: 0x0600931C RID: 37660 RVA: 0x003CFDDE File Offset: 0x003CE1DE
	public void CreateBoneList(GameObject obj, string name)
	{
		this.dictBone.Clear();
		this.CreateBoneListLoop(obj, name);
	}

	// Token: 0x0600931D RID: 37661 RVA: 0x003CFDF4 File Offset: 0x003CE1F4
	public void CreateBoneListMultiple(GameObject obj, params string[] names)
	{
		this.dictBone.Clear();
		foreach (string name in names)
		{
			this.CreateBoneListLoop(obj, name);
		}
	}

	// Token: 0x0600931E RID: 37662 RVA: 0x003CFE30 File Offset: 0x003CE230
	public void CreateBoneListLoop(GameObject obj, string name)
	{
		if ((string.Compare(obj.name, 0, name, 0, name.Length) == 0 || string.Empty == name) && !this.dictBone.ContainsKey(obj.name))
		{
			this.dictBone[obj.name] = obj;
		}
		for (int i = 0; i < obj.transform.childCount; i++)
		{
			this.CreateBoneListLoop(obj.transform.GetChild(i).gameObject, name);
		}
	}

	// Token: 0x0600931F RID: 37663 RVA: 0x003CFEC4 File Offset: 0x003CE2C4
	public void CreateBoneList(GameObject obj, string assetBundleName, string assetName)
	{
		this.dictBone.Clear();
		if (!AssetBundleCheck.IsFile(assetBundleName, assetName))
		{
			string text = "読み込みエラー\r\nassetBundleName：" + assetBundleName + "\tassetName：" + assetName;
			return;
		}
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(assetBundleName, assetName, typeof(TextAsset), null);
		if (assetBundleLoadAssetOperation.IsEmpty())
		{
			string text2 = "読み込みエラー\r\nassetName：" + assetName;
			return;
		}
		TextAsset asset = assetBundleLoadAssetOperation.GetAsset<TextAsset>();
		string[,] array;
		YS_Assist.GetListString(asset.text, out array);
		int length = array.GetLength(0);
		int length2 = array.GetLength(1);
		if (length != 0 && length2 != 0)
		{
			for (int i = 0; i < length; i++)
			{
				GameObject gameObject = obj.transform.FindLoop(array[i, 0]);
				if (gameObject)
				{
					this.dictBone[array[i, 0]] = gameObject.gameObject;
				}
			}
		}
		AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
	}

	// Token: 0x06009320 RID: 37664 RVA: 0x003CFFBC File Offset: 0x003CE3BC
	public void CreateBoneList(GameObject obj, string[] bonename)
	{
		this.dictBone.Clear();
		for (int i = 0; i < bonename.Length; i++)
		{
			GameObject gameObject = obj.transform.FindLoop(bonename[i]);
			if (gameObject)
			{
				this.dictBone[bonename[i]] = gameObject.gameObject;
			}
		}
	}

	// Token: 0x06009321 RID: 37665 RVA: 0x003D0018 File Offset: 0x003CE418
	public void AssignedWeights(GameObject obj, string delTopName, Transform rootBone = null)
	{
		if (this.dictBone == null || this.dictBone.Count == 0)
		{
			return;
		}
		if (null == obj)
		{
			return;
		}
		this.AssignedWeightsLoop(obj.transform, rootBone);
		GameObject gameObject = obj.transform.FindLoop(delTopName);
		if (gameObject)
		{
			gameObject.transform.SetParent(null);
			UnityEngine.Object.Destroy(gameObject.gameObject);
		}
	}

	// Token: 0x06009322 RID: 37666 RVA: 0x003D008C File Offset: 0x003CE48C
	private void AssignedWeightsLoop(Transform t, Transform rootBone = null)
	{
		SkinnedMeshRenderer smr = t.GetComponent<SkinnedMeshRenderer>();
		if (smr)
		{
			int num = smr.bones.Length;
			Transform[] array = new Transform[num];
			GameObject gameObject = null;
			bool flag = false;
			for (int j = 0; j < num; j++)
			{
				if (this.dictBone.TryGetValue(smr.bones[j].name, out gameObject))
				{
					array[j] = gameObject.transform;
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				array = (from x in array
				where null != x
				select x).ToArray<Transform>();
			}
			if (array.Length != smr.bones.Length)
			{
				List<Transform> list = new List<Transform>();
				int i;
				for (i = 0; i < smr.bones.Length; i++)
				{
					if (null == array.FirstOrDefault((Transform x) => x.name == smr.bones[i].name))
					{
						Array.Resize<Transform>(ref array, array.Length + 1);
						array[array.Length - 1] = smr.bones[i];
					}
				}
			}
			smr.bones = array;
			if (rootBone)
			{
				smr.rootBone = rootBone;
			}
			else if (smr.rootBone && this.dictBone.TryGetValue(smr.rootBone.name, out gameObject))
			{
				smr.rootBone = gameObject.transform;
			}
		}
		IEnumerator enumerator = t.gameObject.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform t2 = (Transform)obj;
				this.AssignedWeightsLoop(t2, rootBone);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06009323 RID: 37667 RVA: 0x003D02D0 File Offset: 0x003CE6D0
	public void AssignedWeightsAndSetBounds(GameObject obj, string delTopName, Bounds bounds, Transform rootBone = null)
	{
		if (this.dictBone == null || this.dictBone.Count == 0)
		{
			return;
		}
		if (null == obj)
		{
			return;
		}
		this.AssignedWeightsAndSetBoundsLoop(obj.transform, bounds, rootBone);
		GameObject gameObject = obj.transform.FindLoop(delTopName);
		if (gameObject)
		{
			gameObject.transform.SetParent(null);
			UnityEngine.Object.Destroy(gameObject.gameObject);
		}
	}

	// Token: 0x06009324 RID: 37668 RVA: 0x003D0344 File Offset: 0x003CE744
	private void AssignedWeightsAndSetBoundsLoop(Transform t, Bounds bounds, Transform rootBone = null)
	{
		SkinnedMeshRenderer smr = t.GetComponent<SkinnedMeshRenderer>();
		if (smr)
		{
			int num = smr.bones.Length;
			Transform[] array = new Transform[num];
			GameObject gameObject = null;
			bool flag = false;
			for (int j = 0; j < num; j++)
			{
				if (this.dictBone.TryGetValue(smr.bones[j].name, out gameObject))
				{
					array[j] = gameObject.transform;
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				array = (from x in array
				where null != x
				select x).ToArray<Transform>();
			}
			if (array.Length != smr.bones.Length)
			{
				List<Transform> list = new List<Transform>();
				int i;
				for (i = 0; i < smr.bones.Length; i++)
				{
					if (null == array.FirstOrDefault((Transform x) => x.name == smr.bones[i].name))
					{
						Array.Resize<Transform>(ref array, array.Length + 1);
						array[array.Length - 1] = smr.bones[i];
					}
				}
			}
			smr.bones = array;
			smr.localBounds = bounds;
			Cloth component = smr.gameObject.GetComponent<Cloth>();
			if (rootBone && null == component)
			{
				smr.rootBone = rootBone;
			}
			else if (smr.rootBone && this.dictBone.TryGetValue(smr.rootBone.name, out gameObject))
			{
				smr.rootBone = gameObject.transform;
			}
		}
		IEnumerator enumerator = t.gameObject.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform t2 = (Transform)obj;
				this.AssignedWeightsAndSetBoundsLoop(t2, bounds, rootBone);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}
}
