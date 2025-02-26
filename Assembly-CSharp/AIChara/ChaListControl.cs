using System;
using System.Collections.Generic;
using System.IO;
using Manager;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x0200080D RID: 2061
	public class ChaListControl
	{
		// Token: 0x06003441 RID: 13377 RVA: 0x00133D54 File Offset: 0x00132154
		public ChaListControl()
		{
			ChaListDefine.CategoryNo[] array = (ChaListDefine.CategoryNo[])Enum.GetValues(typeof(ChaListDefine.CategoryNo));
			foreach (ChaListDefine.CategoryNo key in array)
			{
				this.dictListInfo[(int)key] = new Dictionary<int, ListInfoBase>();
			}
			this.itemIDInfo = this._itemIDInfo;
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x00133DDF File Offset: 0x001321DF
		// (set) Token: 0x06003443 RID: 13379 RVA: 0x00133DE7 File Offset: 0x001321E7
		public Dictionary<int, byte> _itemIDInfo { get; set; } = new Dictionary<int, byte>();

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x00133DF0 File Offset: 0x001321F0
		public IReadOnlyDictionary<int, byte> itemIDInfo { get; }

		// Token: 0x06003445 RID: 13381 RVA: 0x00133DF8 File Offset: 0x001321F8
		public Dictionary<int, ListInfoBase> GetCategoryInfo(ChaListDefine.CategoryNo type)
		{
			Dictionary<int, ListInfoBase> dictionary = null;
			if (!this.dictListInfo.TryGetValue((int)type, out dictionary))
			{
				return null;
			}
			return new Dictionary<int, ListInfoBase>(dictionary);
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x00133E24 File Offset: 0x00132224
		public ListInfoBase GetListInfo(ChaListDefine.CategoryNo type, int id)
		{
			Dictionary<int, ListInfoBase> dictionary = null;
			if (!this.dictListInfo.TryGetValue((int)type, out dictionary))
			{
				return null;
			}
			ListInfoBase result = null;
			if (!dictionary.TryGetValue(id, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x00133E5C File Offset: 0x0013225C
		public bool LoadListInfoAll()
		{
			ChaListDefine.CategoryNo[] array = (ChaListDefine.CategoryNo[])Enum.GetValues(typeof(ChaListDefine.CategoryNo));
			Dictionary<int, ListInfoBase> dictData = null;
			List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/characustom/", false);
			for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
			{
				AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAllAsset(assetBundleNameListFromPath[i], typeof(TextAsset), null);
				if (assetBundleLoadAssetOperation == null)
				{
					string text = "読み込みエラー\r\nassetBundleName：" + assetBundleNameListFromPath[i];
				}
				else if (assetBundleLoadAssetOperation.IsEmpty())
				{
					AssetBundleManager.UnloadAssetBundle(assetBundleNameListFromPath[i], true, null, false);
				}
				else
				{
					TextAsset[] allAssets = assetBundleLoadAssetOperation.GetAllAssets<TextAsset>();
					if (allAssets == null || allAssets.Length == 0)
					{
						AssetBundleManager.UnloadAssetBundle(assetBundleNameListFromPath[i], true, null, false);
					}
					else
					{
						foreach (ChaListDefine.CategoryNo key in array)
						{
							if (this.dictListInfo.TryGetValue((int)key, out dictData))
							{
								foreach (TextAsset textAsset in allAssets)
								{
									string removeStringRight = YS_Assist.GetRemoveStringRight(textAsset.name, "_", false);
									if (!(removeStringRight != key.ToString() + "_"))
									{
										this.LoadListInfo(dictData, textAsset);
									}
								}
							}
						}
						AssetBundleManager.UnloadAssetBundle(assetBundleNameListFromPath[i], true, null, false);
					}
				}
			}
			this.EntryClothesIsInit();
			this.LoadItemID();
			UnityEngine.Resources.UnloadUnusedAssets();
			GC.Collect();
			return true;
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x00133FF8 File Offset: 0x001323F8
		private bool LoadListInfo(Dictionary<int, ListInfoBase> dictData, string assetBundleName, string assetName)
		{
			TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(assetBundleName, assetName, false, string.Empty);
			if (null == textAsset)
			{
				return false;
			}
			bool result = this.LoadListInfo(dictData, textAsset);
			AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
			return result;
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x00134034 File Offset: 0x00132434
		private bool LoadListInfo(Dictionary<int, ListInfoBase> dictData, TextAsset ta)
		{
			if (null == ta)
			{
				return false;
			}
			ChaListData chaListData = MessagePackSerializer.Deserialize<ChaListData>(ta.bytes);
			if (chaListData == null)
			{
				return false;
			}
			foreach (KeyValuePair<int, List<string>> keyValuePair in chaListData.dictList)
			{
				int count = dictData.Count;
				ListInfoBase listInfoBase = new ListInfoBase();
				if (listInfoBase.Set(count, chaListData.categoryNo, chaListData.distributionNo, chaListData.lstKey, keyValuePair.Value))
				{
					if (Game.isAdd50 || listInfoBase.Distribution < 50)
					{
						if (!dictData.ContainsKey(listInfoBase.Id))
						{
							dictData[listInfoBase.Id] = listInfoBase;
							int infoInt = listInfoBase.GetInfoInt(ChaListDefine.KeyType.Possess);
							int item = listInfoBase.Category * 1000 + listInfoBase.Id;
							if (infoInt == 1)
							{
								this.lstItemIsInit.Add(item);
							}
							else if (infoInt == 2)
							{
								this.lstItemIsNew.Add(item);
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x00134174 File Offset: 0x00132574
		public static List<ExcelData.Param> LoadExcelData(string assetBunndlePath, string assetName, int cellS, int rowS)
		{
			if (!AssetBundleCheck.IsFile(assetBunndlePath, assetName))
			{
				return null;
			}
			AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(assetBunndlePath, assetName, typeof(ExcelData), null);
			AssetBundleManager.UnloadAssetBundle(assetBunndlePath, true, null, false);
			if (assetBundleLoadAssetOperation.IsEmpty())
			{
				return null;
			}
			ExcelData asset = assetBundleLoadAssetOperation.GetAsset<ExcelData>();
			int num = asset.MaxCell - 1;
			int row = asset.list[num].list.Count - 1;
			return asset.Get(new ExcelData.Specify(cellS, rowS), new ExcelData.Specify(num, row));
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x001341F8 File Offset: 0x001325F8
		public void EntryClothesIsInit()
		{
			for (int i = 0; i < this.lstItemIsInit.Count; i++)
			{
				this.AddItemID(this.lstItemIsInit[i], 2);
			}
			for (int j = 0; j < this.lstItemIsNew.Count; j++)
			{
				this.AddItemID(this.lstItemIsNew[j], 1);
			}
			this.lstItemIsInit.Clear();
			this.lstItemIsInit.TrimExcess();
			this.lstItemIsNew.Clear();
			this.lstItemIsNew.TrimExcess();
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x00134290 File Offset: 0x00132690
		public void SaveItemID()
		{
			string path = UserData.Path + ChaListDefine.CheckItemFile;
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					binaryWriter.Write(ChaListDefine.CheckItemVersion.ToString());
					binaryWriter.Write(this._itemIDInfo.Count);
					foreach (KeyValuePair<int, byte> keyValuePair in this._itemIDInfo)
					{
						binaryWriter.Write(keyValuePair.Key);
						binaryWriter.Write(keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x00134398 File Offset: 0x00132798
		public void LoadItemID()
		{
			string path = UserData.Path + ChaListDefine.CheckItemFile;
			if (!File.Exists(path))
			{
				return;
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					string text = binaryReader.ReadString();
					int num = binaryReader.ReadInt32();
					for (int i = 0; i < num; i++)
					{
						int key = binaryReader.ReadInt32();
						byte b = binaryReader.ReadByte();
						byte b2 = 0;
						if (this._itemIDInfo.TryGetValue(key, out b2))
						{
							if (b2 < b)
							{
								this._itemIDInfo[key] = b;
							}
						}
						else
						{
							this._itemIDInfo.Add(key, b);
						}
					}
				}
			}
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x0013448C File Offset: 0x0013288C
		public void AddItemID(string IDStr, byte flags = 1)
		{
			string[] array = IDStr.Split(new char[]
			{
				'/'
			});
			foreach (string s in array)
			{
				int key = int.Parse(s);
				byte b = 0;
				if (this._itemIDInfo.TryGetValue(key, out b))
				{
					if (b < flags)
					{
						this._itemIDInfo[key] = flags;
					}
				}
				else
				{
					this._itemIDInfo.Add(key, flags);
				}
			}
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x00134510 File Offset: 0x00132910
		public void AddItemID(int pid, byte flags = 1)
		{
			byte b = 0;
			if (this._itemIDInfo.TryGetValue(pid, out b))
			{
				if (b < flags)
				{
					this._itemIDInfo[pid] = flags;
				}
			}
			else
			{
				this._itemIDInfo.Add(pid, flags);
			}
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x00134558 File Offset: 0x00132958
		public void AddItemID(int category, int id, byte flags)
		{
			int pid = category * 1000 + id;
			this.AddItemID(pid, flags);
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x00134578 File Offset: 0x00132978
		public byte CheckItemID(int pid)
		{
			byte result = 0;
			if (this._itemIDInfo.TryGetValue(pid, out result))
			{
				return result;
			}
			return result;
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x001345A0 File Offset: 0x001329A0
		public byte CheckItemID(int category, int id)
		{
			int pid = category * 1000 + id;
			return this.CheckItemID(pid);
		}

		// Token: 0x04003429 RID: 13353
		private Dictionary<int, Dictionary<int, ListInfoBase>> dictListInfo = new Dictionary<int, Dictionary<int, ListInfoBase>>();

		// Token: 0x0400342C RID: 13356
		private List<int> lstItemIsInit = new List<int>();

		// Token: 0x0400342D RID: 13357
		private List<int> lstItemIsNew = new List<int>();
	}
}
