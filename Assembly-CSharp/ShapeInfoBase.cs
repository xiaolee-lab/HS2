using System;
using System.Collections.Generic;
using IllusionUtility.GetUtility;
using UnityEngine;

// Token: 0x020010D4 RID: 4308
public abstract class ShapeInfoBase
{
	// Token: 0x06008F56 RID: 36694 RVA: 0x00126DB3 File Offset: 0x001251B3
	public ShapeInfoBase()
	{
		this.InitEnd = false;
		this.dictCategory = new Dictionary<int, List<ShapeInfoBase.CategoryInfo>>();
		this.dictDst = new Dictionary<int, ShapeInfoBase.BoneInfo>();
		this.dictSrc = new Dictionary<int, ShapeInfoBase.BoneInfo>();
	}

	// Token: 0x17001F09 RID: 7945
	// (get) Token: 0x06008F57 RID: 36695 RVA: 0x00126DEE File Offset: 0x001251EE
	// (set) Token: 0x06008F58 RID: 36696 RVA: 0x00126DF6 File Offset: 0x001251F6
	public bool InitEnd { get; protected set; }

	// Token: 0x06008F59 RID: 36697 RVA: 0x00126DFF File Offset: 0x001251FF
	public int GetKeyCount()
	{
		return (this.anmKeyInfo != null) ? this.anmKeyInfo.GetKeyCount() : 0;
	}

	// Token: 0x06008F5A RID: 36698
	public abstract void InitShapeInfo(string manifest, string assetBundleAnmKey, string assetBundleCategory, string anmKeyInfoName, string cateInfoName, Transform trfObj);

	// Token: 0x06008F5B RID: 36699 RVA: 0x00126E1D File Offset: 0x0012521D
	protected void InitShapeInfoBase(string manifest, string assetBundleAnmKey, string assetBundleCategory, string anmKeyInfoName, string cateInfoName, Transform trfObj, Dictionary<string, int> dictEnumDst, Dictionary<string, int> dictEnumSrc, Action<string, string> funcAssetBundleEntry = null)
	{
		this.anmKeyInfo.LoadInfo(manifest, assetBundleAnmKey, anmKeyInfoName, funcAssetBundleEntry);
		this.LoadCategoryInfoList(assetBundleCategory, cateInfoName, dictEnumSrc);
		this.GetDstBoneInfo(trfObj, dictEnumDst);
		this.GetSrcBoneInfo();
	}

	// Token: 0x06008F5C RID: 36700 RVA: 0x00126E4D File Offset: 0x0012524D
	public void ReleaseShapeInfo()
	{
		this.InitEnd = false;
		this.dictCategory.Clear();
		this.dictDst.Clear();
		this.dictSrc.Clear();
	}

	// Token: 0x06008F5D RID: 36701 RVA: 0x00126E78 File Offset: 0x00125278
	private bool LoadCategoryInfoList(string assetBundleName, string assetName, Dictionary<string, int> dictEnumSrc)
	{
		if (!AssetBundleCheck.IsFile(assetBundleName, assetName))
		{
			string text = "読み込みエラー\r\nassetBundleName：" + assetBundleName + "\tassetName：" + assetName;
			return false;
		}
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(assetBundleName, assetName, typeof(TextAsset), null);
		if (assetBundleLoadAssetOperation == null)
		{
			string text2 = "読み込みエラー\r\nassetName：" + assetName;
			return false;
		}
		TextAsset asset = assetBundleLoadAssetOperation.GetAsset<TextAsset>();
		if (null == asset)
		{
			return false;
		}
		string[,] array;
		YS_Assist.GetListString(asset.text, out array);
		int length = array.GetLength(0);
		int length2 = array.GetLength(1);
		this.dictCategory.Clear();
		if (length != 0 && length2 != 0)
		{
			for (int i = 0; i < length; i++)
			{
				ShapeInfoBase.CategoryInfo categoryInfo = new ShapeInfoBase.CategoryInfo();
				categoryInfo.Initialize();
				int key = int.Parse(array[i, 0]);
				categoryInfo.name = array[i, 1];
				int id = 0;
				if (!dictEnumSrc.TryGetValue(categoryInfo.name, out id))
				{
					string text3 = "SrcBone【" + categoryInfo.name + "】のIDが見つかりません";
				}
				else
				{
					categoryInfo.id = id;
					categoryInfo.use[0][0] = ((!(array[i, 2] == "0")) ? true : false);
					categoryInfo.use[0][1] = ((!(array[i, 3] == "0")) ? true : false);
					categoryInfo.use[0][2] = ((!(array[i, 4] == "0")) ? true : false);
					if (categoryInfo.use[0][0] || categoryInfo.use[0][1] || categoryInfo.use[0][2])
					{
						categoryInfo.getflag[0] = true;
					}
					categoryInfo.use[1][0] = ((!(array[i, 5] == "0")) ? true : false);
					categoryInfo.use[1][1] = ((!(array[i, 6] == "0")) ? true : false);
					categoryInfo.use[1][2] = ((!(array[i, 7] == "0")) ? true : false);
					if (categoryInfo.use[1][0] || categoryInfo.use[1][1] || categoryInfo.use[1][2])
					{
						categoryInfo.getflag[1] = true;
					}
					categoryInfo.use[2][0] = ((!(array[i, 8] == "0")) ? true : false);
					categoryInfo.use[2][1] = ((!(array[i, 9] == "0")) ? true : false);
					categoryInfo.use[2][2] = ((!(array[i, 10] == "0")) ? true : false);
					if (categoryInfo.use[2][0] || categoryInfo.use[2][1] || categoryInfo.use[2][2])
					{
						categoryInfo.getflag[2] = true;
					}
					List<ShapeInfoBase.CategoryInfo> list = null;
					if (!this.dictCategory.TryGetValue(key, out list))
					{
						list = new List<ShapeInfoBase.CategoryInfo>();
						this.dictCategory[key] = list;
					}
					list.Add(categoryInfo);
				}
			}
		}
		AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
		return true;
	}

	// Token: 0x06008F5E RID: 36702 RVA: 0x00127218 File Offset: 0x00125618
	private bool GetDstBoneInfo(Transform trfBone, Dictionary<string, int> dictEnumDst)
	{
		this.dictDst.Clear();
		foreach (KeyValuePair<string, int> keyValuePair in dictEnumDst)
		{
			GameObject gameObject = trfBone.FindLoop(keyValuePair.Key);
			if (null != gameObject)
			{
				ShapeInfoBase.BoneInfo boneInfo = new ShapeInfoBase.BoneInfo();
				boneInfo.trfBone = gameObject.transform;
				this.dictDst[keyValuePair.Value] = boneInfo;
			}
		}
		return true;
	}

	// Token: 0x06008F5F RID: 36703 RVA: 0x001272B4 File Offset: 0x001256B4
	private void GetSrcBoneInfo()
	{
		this.dictSrc.Clear();
		foreach (KeyValuePair<int, List<ShapeInfoBase.CategoryInfo>> keyValuePair in this.dictCategory)
		{
			int count = keyValuePair.Value.Count;
			for (int i = 0; i < count; i++)
			{
				ShapeInfoBase.BoneInfo value = null;
				if (!this.dictSrc.TryGetValue(keyValuePair.Value[i].id, out value))
				{
					value = new ShapeInfoBase.BoneInfo();
					this.dictSrc[keyValuePair.Value[i].id] = value;
				}
			}
		}
	}

	// Token: 0x06008F60 RID: 36704 RVA: 0x00127380 File Offset: 0x00125780
	public bool ChangeValue(int category, float value)
	{
		if (this.anmKeyInfo == null)
		{
			return false;
		}
		List<ShapeInfoBase.CategoryInfo> list;
		if (!this.dictCategory.TryGetValue(category, out list))
		{
			return false;
		}
		int count = list.Count;
		string name = string.Empty;
		for (int i = 0; i < count; i++)
		{
			ShapeInfoBase.BoneInfo boneInfo = null;
			int id = list[i].id;
			name = list[i].name;
			if (this.dictSrc.TryGetValue(id, out boneInfo))
			{
				Vector3[] array = new Vector3[3];
				for (int j = 0; j < 3; j++)
				{
					array[j] = Vector3.zero;
				}
				bool[] array2 = new bool[3];
				for (int k = 0; k < 3; k++)
				{
					array2[k] = list[i].getflag[k];
				}
				this.anmKeyInfo.GetInfo(name, value, ref array, array2);
				if (list[i].use[0][0])
				{
					boneInfo.vctPos.x = array[0].x;
				}
				if (list[i].use[0][1])
				{
					boneInfo.vctPos.y = array[0].y;
				}
				if (list[i].use[0][2])
				{
					boneInfo.vctPos.z = array[0].z;
				}
				if (list[i].use[1][0])
				{
					boneInfo.vctRot.x = array[1].x;
				}
				if (list[i].use[1][1])
				{
					boneInfo.vctRot.y = array[1].y;
				}
				if (list[i].use[1][2])
				{
					boneInfo.vctRot.z = array[1].z;
				}
				if (list[i].use[2][0])
				{
					boneInfo.vctScl.x = array[2].x;
				}
				if (list[i].use[2][1])
				{
					boneInfo.vctScl.y = array[2].y;
				}
				if (list[i].use[2][2])
				{
					boneInfo.vctScl.z = array[2].z;
				}
			}
		}
		return true;
	}

	// Token: 0x06008F61 RID: 36705 RVA: 0x00127624 File Offset: 0x00125A24
	public bool ChangeValue(int category, int key01, int key02, float blend)
	{
		if (this.anmKeyInfo == null)
		{
			return false;
		}
		List<ShapeInfoBase.CategoryInfo> list;
		if (!this.dictCategory.TryGetValue(category, out list))
		{
			return false;
		}
		int count = list.Count;
		string name = string.Empty;
		for (int i = 0; i < count; i++)
		{
			ShapeInfoBase.BoneInfo boneInfo = null;
			int id = list[i].id;
			name = list[i].name;
			if (this.dictSrc.TryGetValue(id, out boneInfo))
			{
				Vector3[] array = new Vector3[3];
				for (int j = 0; j < 3; j++)
				{
					array[j] = Vector3.zero;
				}
				Vector3[] array2 = new Vector3[3];
				for (int k = 0; k < 3; k++)
				{
					array2[k] = Vector3.zero;
				}
				bool[] array3 = new bool[3];
				for (int l = 0; l < 3; l++)
				{
					array3[l] = list[i].getflag[l];
				}
				if (!this.anmKeyInfo.GetInfo(name, key01, ref array, array3))
				{
					return false;
				}
				if (!this.anmKeyInfo.GetInfo(name, key02, ref array2, array3))
				{
					return false;
				}
				Vector3 vector = Vector3.Lerp(array[0], array2[0], blend);
				if (list[i].use[0][0])
				{
					boneInfo.vctPos.x = vector.x;
				}
				if (list[i].use[0][1])
				{
					boneInfo.vctPos.y = vector.y;
				}
				if (list[i].use[0][2])
				{
					boneInfo.vctPos.z = vector.z;
				}
				vector.x = Mathf.LerpAngle(array[1].x, array2[1].x, blend);
				vector.y = Mathf.LerpAngle(array[1].y, array2[1].y, blend);
				vector.z = Mathf.LerpAngle(array[1].z, array2[1].z, blend);
				if (list[i].use[1][0])
				{
					boneInfo.vctRot.x = vector.x;
				}
				if (list[i].use[1][1])
				{
					boneInfo.vctRot.y = vector.y;
				}
				if (list[i].use[1][2])
				{
					boneInfo.vctRot.z = vector.z;
				}
				vector = Vector3.Lerp(array[2], array2[2], blend);
				if (list[i].use[2][0])
				{
					boneInfo.vctScl.x = vector.x;
				}
				if (list[i].use[2][1])
				{
					boneInfo.vctScl.y = vector.y;
				}
				if (list[i].use[2][2])
				{
					boneInfo.vctScl.z = vector.z;
				}
			}
		}
		return true;
	}

	// Token: 0x06008F62 RID: 36706
	public abstract void ForceUpdate();

	// Token: 0x06008F63 RID: 36707
	public abstract void Update();

	// Token: 0x06008F64 RID: 36708
	public abstract void UpdateAlways();

	// Token: 0x040073DE RID: 29662
	private Dictionary<int, List<ShapeInfoBase.CategoryInfo>> dictCategory;

	// Token: 0x040073DF RID: 29663
	protected Dictionary<int, ShapeInfoBase.BoneInfo> dictDst;

	// Token: 0x040073E0 RID: 29664
	protected Dictionary<int, ShapeInfoBase.BoneInfo> dictSrc;

	// Token: 0x040073E1 RID: 29665
	private AnimationKeyInfo anmKeyInfo = new AnimationKeyInfo();

	// Token: 0x020010D5 RID: 4309
	public class CategoryInfo
	{
		// Token: 0x06008F66 RID: 36710 RVA: 0x001279CC File Offset: 0x00125DCC
		public void Initialize()
		{
			for (int i = 0; i < 3; i++)
			{
				this.use[i] = new bool[3];
				this.getflag[i] = false;
			}
		}

		// Token: 0x040073E2 RID: 29666
		public int id;

		// Token: 0x040073E3 RID: 29667
		public string name = string.Empty;

		// Token: 0x040073E4 RID: 29668
		public bool[][] use = new bool[3][];

		// Token: 0x040073E5 RID: 29669
		public bool[] getflag = new bool[3];
	}

	// Token: 0x020010D6 RID: 4310
	public class BoneInfo
	{
		// Token: 0x040073E6 RID: 29670
		public Transform trfBone;

		// Token: 0x040073E7 RID: 29671
		public Vector3 vctPos = Vector3.zero;

		// Token: 0x040073E8 RID: 29672
		public Vector3 vctRot = Vector3.zero;

		// Token: 0x040073E9 RID: 29673
		public Vector3 vctScl = Vector3.one;
	}
}
