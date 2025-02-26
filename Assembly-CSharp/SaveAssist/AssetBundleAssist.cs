using System;
using System.IO;
using UnityEngine;

namespace SaveAssist
{
	// Token: 0x02000840 RID: 2112
	public abstract class AssetBundleAssist
	{
		// Token: 0x060035F5 RID: 13813 RVA: 0x0013E072 File Offset: 0x0013C472
		public AssetBundleAssist(string _savePath, string _assetBundleName, string _assetName)
		{
			this.savePath = _savePath;
			this.assetBundleName = _assetBundleName;
			this.assetName = _assetName;
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x0013E0B0 File Offset: 0x0013C4B0
		public void Save()
		{
			using (FileStream fileStream = new FileStream(this.savePath, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					this.SaveFunc(binaryWriter);
				}
			}
		}

		// Token: 0x060035F7 RID: 13815
		public abstract void SaveFunc(BinaryWriter bw);

		// Token: 0x060035F8 RID: 13816 RVA: 0x0013E11C File Offset: 0x0013C51C
		public void Load()
		{
			TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(this.assetBundleName, this.assetName, false, string.Empty);
			if (null != textAsset)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					memoryStream.Write(textAsset.bytes, 0, textAsset.bytes.Length);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						this.LoadFunc(binaryReader);
					}
				}
			}
			AssetBundleManager.UnloadAssetBundle(this.assetBundleName, true, null, true);
		}

		// Token: 0x060035F9 RID: 13817
		public abstract void LoadFunc(BinaryReader br);

		// Token: 0x0400363E RID: 13886
		protected string savePath = string.Empty;

		// Token: 0x0400363F RID: 13887
		protected string assetBundleName = string.Empty;

		// Token: 0x04003640 RID: 13888
		protected string assetName = string.Empty;
	}
}
