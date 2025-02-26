using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UploaderSystem
{
	// Token: 0x02001014 RID: 4116
	public class NetCacheControl : MonoBehaviour
	{
		// Token: 0x17001E2C RID: 7724
		// (get) Token: 0x06008A32 RID: 35378 RVA: 0x003A2557 File Offset: 0x003A0957
		private NetworkInfo netInfo
		{
			get
			{
				return Singleton<NetworkInfo>.Instance;
			}
		}

		// Token: 0x06008A33 RID: 35379 RVA: 0x003A2560 File Offset: 0x003A0960
		private Dictionary<int, string> GetCacheFileList(DataType type)
		{
			string[] array = new string[]
			{
				"cache/chara/",
				"cache/housing/"
			};
			string str = UserData.Path + array[(int)type];
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			string text = string.Empty;
			for (int i = 0; i < 50; i++)
			{
				text = str + i.ToString("00") + ".dat";
				if (File.Exists(text))
				{
					dictionary[i] = text;
				}
			}
			if (dictionary.Count >= 50)
			{
				var list = (from x in dictionary
				select new
				{
					k = x.Key,
					v = new FileInfo(x.Value)
				} into x
				orderby x.v.LastAccessTime
				select x).ToList();
				dictionary.Remove(list[0].k);
				File.Delete(list[0].v.FullName);
			}
			return dictionary;
		}

		// Token: 0x06008A34 RID: 35380 RVA: 0x003A2668 File Offset: 0x003A0A68
		public void UpdateCacheHeaderInfo(DataType type)
		{
			if (this.dictCacheHeaderInfo[(int)type] == null)
			{
				this.dictCacheHeaderInfo[(int)type] = new Dictionary<string, List<NetCacheControl.CacheHeader>>();
			}
			else
			{
				this.dictCacheHeaderInfo[(int)type].Clear();
			}
			Dictionary<int, string> cacheFileList = this.GetCacheFileList(type);
			foreach (KeyValuePair<int, string> keyValuePair in cacheFileList)
			{
				using (FileStream fileStream = new FileStream(keyValuePair.Value, FileMode.Open, FileAccess.Read))
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						string text = binaryReader.ReadString();
						int num = binaryReader.ReadInt32();
						int num2 = binaryReader.ReadInt32();
						List<NetCacheControl.CacheHeader> list = new List<NetCacheControl.CacheHeader>();
						for (int i = 0; i < num2; i++)
						{
							list.Add(new NetCacheControl.CacheHeader
							{
								idx = binaryReader.ReadInt32(),
								update_idx = binaryReader.ReadInt32(),
								pos = binaryReader.ReadInt64(),
								size = binaryReader.ReadInt32()
							});
						}
						this.dictCacheHeaderInfo[(int)type][keyValuePair.Value] = list;
					}
				}
			}
		}

		// Token: 0x06008A35 RID: 35381 RVA: 0x003A27D8 File Offset: 0x003A0BD8
		public string GetCacheHeader(DataType type, int idx, out NetCacheControl.CacheHeader ch)
		{
			ch = null;
			foreach (KeyValuePair<string, List<NetCacheControl.CacheHeader>> keyValuePair in this.dictCacheHeaderInfo[(int)type])
			{
				foreach (NetCacheControl.CacheHeader cacheHeader in keyValuePair.Value)
				{
					if (cacheHeader.idx == idx)
					{
						ch = new NetCacheControl.CacheHeader();
						ch.idx = cacheHeader.idx;
						ch.update_idx = cacheHeader.update_idx;
						ch.pos = cacheHeader.pos;
						ch.size = cacheHeader.size;
						return keyValuePair.Key;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06008A36 RID: 35382 RVA: 0x003A28D4 File Offset: 0x003A0CD4
		public void DeleteCache(DataType type)
		{
			Dictionary<int, string> cacheFileList = this.GetCacheFileList(type);
			foreach (KeyValuePair<int, string> keyValuePair in cacheFileList)
			{
				if (File.Exists(keyValuePair.Value))
				{
					File.Delete(keyValuePair.Value);
				}
			}
			this.UpdateCacheHeaderInfo(type);
		}

		// Token: 0x06008A37 RID: 35383 RVA: 0x003A2950 File Offset: 0x003A0D50
		public bool CreateCache(DataType type, Dictionary<int, Tuple<int, byte[]>> dictGet)
		{
			if (dictGet.Count == 0)
			{
				return false;
			}
			string[] array = new string[]
			{
				"cache/chara/",
				"cache/housing/"
			};
			string str = UserData.Path + array[(int)type];
			Dictionary<int, Tuple<int, byte[]>> dictionary = null;
			string text = string.Empty;
			int i = 0;
			while (i < 50)
			{
				text = str + i.ToString("00") + ".dat";
				if (File.Exists(text))
				{
					List<NetCacheControl.CacheHeader> list = null;
					if (this.dictCacheHeaderInfo[(int)type].TryGetValue(text, out list) && list.Count >= 1000)
					{
						i++;
						continue;
					}
					dictionary = this.LoadCacheFile(text);
				}
				else
				{
					dictionary = new Dictionary<int, Tuple<int, byte[]>>();
				}
				break;
			}
			foreach (KeyValuePair<int, Tuple<int, byte[]>> keyValuePair in dictGet)
			{
				if (keyValuePair.Value.Item2 != null)
				{
					dictionary[keyValuePair.Key] = new Tuple<int, byte[]>(keyValuePair.Value.Item1, keyValuePair.Value.Item2);
				}
			}
			this.SaveCacheFile(text, dictionary);
			return true;
		}

		// Token: 0x06008A38 RID: 35384 RVA: 0x003A2AA8 File Offset: 0x003A0EA8
		public void SaveCacheFile(string path, Dictionary<int, Tuple<int, byte[]>> dictPNG)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			int[] array = dictPNG.Keys.ToArray<int>();
			Dictionary<int, long> dictionary = new Dictionary<int, long>();
			byte[] buffer = null;
			int num = 20;
			long num2 = (long)(Encoding.UTF8.GetByteCount("【CacheFile】") + 4 + 4 + num * array.Length + 1);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					foreach (int key in array)
					{
						dictionary[key] = num2;
						num2 += (long)dictPNG[key].Item2.Length;
						binaryWriter.Write(dictPNG[key].Item2);
					}
					buffer = memoryStream.ToArray();
				}
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter2 = new BinaryWriter(fileStream))
				{
					binaryWriter2.Write("【CacheFile】");
					binaryWriter2.Write(100);
					binaryWriter2.Write(array.Length);
					foreach (int num3 in array)
					{
						binaryWriter2.Write(num3);
						binaryWriter2.Write(dictPNG[num3].Item1);
						binaryWriter2.Write(dictionary[num3]);
						binaryWriter2.Write(dictPNG[num3].Item2.Length);
					}
					binaryWriter2.Write(buffer);
				}
			}
		}

		// Token: 0x06008A39 RID: 35385 RVA: 0x003A2C8C File Offset: 0x003A108C
		public Dictionary<int, Tuple<int, byte[]>> LoadCacheFile(string path)
		{
			Dictionary<int, Tuple<int, byte[]>> dictionary = new Dictionary<int, Tuple<int, byte[]>>();
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					string text = binaryReader.ReadString();
					int num = binaryReader.ReadInt32();
					List<NetCacheControl.CacheHeader> list = new List<NetCacheControl.CacheHeader>();
					int num2 = binaryReader.ReadInt32();
					for (int i = 0; i < num2; i++)
					{
						list.Add(new NetCacheControl.CacheHeader
						{
							idx = binaryReader.ReadInt32(),
							update_idx = binaryReader.ReadInt32(),
							pos = binaryReader.ReadInt64(),
							size = binaryReader.ReadInt32()
						});
					}
					int count = list.Count;
					for (int j = 0; j < count; j++)
					{
						fileStream.Seek(list[j].pos, SeekOrigin.Begin);
						byte[] item = binaryReader.ReadBytes(list[j].size);
						dictionary[list[j].idx] = new Tuple<int, byte[]>(list[j].update_idx, item);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06008A3A RID: 35386 RVA: 0x003A2DF8 File Offset: 0x003A11F8
		public byte[] LoadCache(string path, long pos, int size)
		{
			byte[] result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				fileStream.Seek(pos, SeekOrigin.Begin);
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					result = binaryReader.ReadBytes(size);
				}
			}
			return result;
		}

		// Token: 0x06008A3B RID: 35387 RVA: 0x003A2E64 File Offset: 0x003A1264
		public void GetCache(DataType type, Dictionary<int, Tuple<int, byte[]>> dictPNG)
		{
			int[] array = dictPNG.Keys.ToArray<int>();
			for (int i = 0; i < array.Length; i++)
			{
				NetCacheControl.CacheHeader cacheHeader = null;
				string cacheHeader2 = this.GetCacheHeader(type, array[i], out cacheHeader);
				if (cacheHeader != null)
				{
					dictPNG[cacheHeader.idx] = new Tuple<int, byte[]>(cacheHeader.update_idx, this.LoadCache(cacheHeader2, cacheHeader.pos, cacheHeader.size));
				}
			}
		}

		// Token: 0x06008A3C RID: 35388 RVA: 0x003A2ED0 File Offset: 0x003A12D0
		private void Start()
		{
			this.UpdateCacheHeaderInfo(DataType.Chara);
			this.UpdateCacheHeaderInfo(DataType.Housing);
		}

		// Token: 0x06008A3D RID: 35389 RVA: 0x003A2EE0 File Offset: 0x003A12E0
		private void Update()
		{
		}

		// Token: 0x04007096 RID: 28822
		private const int cacheFileMax = 50;

		// Token: 0x04007097 RID: 28823
		public bool enableCache = true;

		// Token: 0x04007098 RID: 28824
		private Dictionary<string, List<NetCacheControl.CacheHeader>>[] dictCacheHeaderInfo = new Dictionary<string, List<NetCacheControl.CacheHeader>>[Enum.GetNames(typeof(DataType)).Length];

		// Token: 0x02001015 RID: 4117
		public class CacheHeader
		{
			// Token: 0x0400709B RID: 28827
			public int idx;

			// Token: 0x0400709C RID: 28828
			public int update_idx;

			// Token: 0x0400709D RID: 28829
			public long pos;

			// Token: 0x0400709E RID: 28830
			public int size;
		}
	}
}
