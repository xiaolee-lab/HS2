using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200126A RID: 4714
	public class Info : Singleton<Info>
	{
		// Token: 0x1700216B RID: 8555
		// (get) Token: 0x06009C1D RID: 39965 RVA: 0x003FD058 File Offset: 0x003FB458
		// (set) Token: 0x06009C1E RID: 39966 RVA: 0x003FD060 File Offset: 0x003FB460
		public int AccessoryPointNum { get; private set; }

		// Token: 0x1700216C RID: 8556
		// (get) Token: 0x06009C1F RID: 39967 RVA: 0x003FD06C File Offset: 0x003FB46C
		public int[] AccessoryPointsIndex
		{
			get
			{
				HashSet<int> hashSet = new HashSet<int>();
				foreach (KeyValuePair<int, Info.AccessoryGroupInfo> keyValuePair in this.dicAccessoryGroup)
				{
					hashSet.UnionWith(Enumerable.Range(keyValuePair.Value.Targets[0], keyValuePair.Value.Targets[1] - keyValuePair.Value.Targets[0] + 1));
				}
				return hashSet.ToArray<int>();
			}
		}

		// Token: 0x1700216D RID: 8557
		// (get) Token: 0x06009C20 RID: 39968 RVA: 0x003FD108 File Offset: 0x003FB508
		public ExcelData accessoryPointGroup
		{
			get
			{
				return this.m_AccessoryPointGroup;
			}
		}

		// Token: 0x1700216E RID: 8558
		// (get) Token: 0x06009C21 RID: 39969 RVA: 0x003FD110 File Offset: 0x003FB510
		// (set) Token: 0x06009C22 RID: 39970 RVA: 0x003FD118 File Offset: 0x003FB518
		public bool isLoadList { get; private set; }

		// Token: 0x1700216F RID: 8559
		// (get) Token: 0x06009C23 RID: 39971 RVA: 0x003FD121 File Offset: 0x003FB521
		// (set) Token: 0x06009C24 RID: 39972 RVA: 0x003FD129 File Offset: 0x003FB529
		public bool IsInstallAdd { get; private set; }

		// Token: 0x06009C25 RID: 39973 RVA: 0x003FD132 File Offset: 0x003FB532
		private void CheckIsAdd()
		{
			this.IsInstallAdd = AssetBundleCheck.IsManifest("add50");
		}

		// Token: 0x06009C26 RID: 39974 RVA: 0x003FD144 File Offset: 0x003FB544
		public IEnumerator LoadExcelDataCoroutine()
		{
			if (this.isLoadList)
			{
				yield break;
			}
			this.fileCheck = new Info.FileCheck();
			this.waitTime = new Info.WaitTime();
			this.dicBoneInfo.Clear();
			this.dicItemGroupCategory.Clear();
			this.dicItemLoadInfo.Clear();
			this.dicLightLoadInfo.Clear();
			this.dicAGroupCategory.Clear();
			this.dicAnimeLoadInfo.Clear();
			this.dicVoiceGroupCategory.Clear();
			this.dicVoiceLoadInfo.Clear();
			this.dicColorGradingLoadInfo.Clear();
			this.dicReflectionProbeLoadInfo.Clear();
			if (this.waitTime.isOver)
			{
				yield return null;
				this.waitTime.Next();
			}
			List<string> pathList = CommonLib.GetAssetBundleNameListFromPath("studio/info/", true);
			pathList.Sort();
			if (this.waitTime.isOver)
			{
				yield return null;
				this.waitTime.Next();
			}
			Info.FileListInfo fli = new Info.FileListInfo(pathList);
			for (int i = 0; i < pathList.Count; i++)
			{
				string bundlePath = pathList[i];
				string fileName = Path.GetFileNameWithoutExtension(bundlePath);
				int v = 0;
				if (!int.TryParse(fileName, out v) || v < 50 || this.IsInstallAdd)
				{
					if (fli.Check(bundlePath, "AccessoryPointGroup_" + fileName))
					{
						this.LoadAccessoryGroupInfo(this.LoadExcelData(bundlePath, "AccessoryPointGroup_" + fileName));
					}
					if (fli.Check(bundlePath, "Bone_" + fileName))
					{
						this.LoadBoneInfo(this.LoadExcelData(bundlePath, "Bone_" + fileName), this.dicBoneInfo);
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					if (fli.Check(bundlePath, "ItemGroup_" + fileName))
					{
						this.LoadAnimeGroupInfo(this.LoadExcelData(bundlePath, "ItemGroup_" + fileName), this.dicItemGroupCategory);
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					this.LoadAnimeCategoryInfo(bundlePath, "ItemCategory_(\\d*)_(\\d*)", this.dicItemGroupCategory);
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					yield return this.LoadItemLoadInfoCoroutine(bundlePath, "ItemList_(\\d*)_(\\d*)_(\\d*)");
					this.LoadItemBoneInfo(bundlePath, "ItemBoneList_(\\d*)_(\\d*)");
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					if (fli.Check(bundlePath, "ItemData_" + fileName))
					{
						this.LoadItemColorData(bundlePath, "ItemData_" + fileName);
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					if (fli.Check(bundlePath, "Light_" + fileName))
					{
						this.LoadLightLoadInfo(this.LoadExcelData(bundlePath, "Light_" + fileName));
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					if (fli.Check(bundlePath, "AnimeGroup_" + fileName))
					{
						this.LoadAnimeGroupInfo(this.LoadExcelData(bundlePath, "AnimeGroup_" + fileName), this.dicAGroupCategory);
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					this.LoadAnimeCategoryInfo(bundlePath, "AnimeCategory_(\\d*)_(\\d*)", this.dicAGroupCategory);
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					yield return this.LoadAnimeLoadInfoCoroutine(bundlePath, "Anime_(\\d*)_(\\d*)_(\\d*)", this.dicAnimeLoadInfo, new Info.LoadAnimeInfoCoroutineFunc(this.LoadAnimeLoadInfo));
					yield return this.LoadAnimeLoadInfoCoroutine(bundlePath, "HAnime_(\\d*)_(\\d*)_(\\d*)", this.dicAnimeLoadInfo, new Info.LoadAnimeInfoCoroutineFunc(this.LoadHAnimeLoadInfo));
					if (fli.Check(bundlePath, "VoiceGroup_" + fileName))
					{
						this.LoadAnimeGroupInfo(this.LoadExcelData(bundlePath, "VoiceGroup_" + fileName), this.dicVoiceGroupCategory);
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					this.LoadAnimeCategoryInfo(bundlePath, "VoiceCategory_(\\d*)_(\\d*)", this.dicVoiceGroupCategory);
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					yield return this.LoadVoiceLoadInfoCoroutine(bundlePath, "Voice_(\\d*)_(\\d*)_(\\d*)");
					if (fli.Check(bundlePath, "BGM_" + fileName))
					{
						this.LoadSoundLoadInfo(this.LoadExcelData(bundlePath, "BGM_" + fileName), this.dicBGMLoadInfo);
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					if (fli.Check(bundlePath, "Map_" + fileName))
					{
						this.LoadMapLoadInfo(this.LoadExcelData(bundlePath, "Map_" + fileName), this.dicMapLoadInfo);
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					if (fli.Check(bundlePath, "Filter_" + fileName))
					{
						this.LoadSoundLoadInfo(this.LoadExcelData(bundlePath, "Filter_" + fileName), this.dicColorGradingLoadInfo);
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					if (fli.Check(bundlePath, "Probe_" + fileName))
					{
						this.LoadSoundLoadInfo(this.LoadExcelData(bundlePath, "Probe_" + fileName), this.dicReflectionProbeLoadInfo);
					}
					if (this.waitTime.isOver)
					{
						yield return null;
						this.waitTime.Next();
					}
					AssetBundleManager.UnloadAssetBundle(bundlePath, true, null, false);
				}
			}
			this.fileCheck = null;
			this.waitTime = null;
			this.isLoadList = true;
			yield break;
		}

		// Token: 0x06009C27 RID: 39975 RVA: 0x003FD160 File Offset: 0x003FB560
		public Info.LoadCommonInfo GetVoiceInfo(int _group, int _category, int _no)
		{
			Dictionary<int, Dictionary<int, Info.LoadCommonInfo>> dictionary = null;
			if (!this.dicVoiceLoadInfo.TryGetValue(_group, out dictionary))
			{
				return null;
			}
			Dictionary<int, Info.LoadCommonInfo> dictionary2 = null;
			if (!dictionary.TryGetValue(_category, out dictionary2))
			{
				return null;
			}
			Info.LoadCommonInfo loadCommonInfo = null;
			return (!dictionary2.TryGetValue(_no, out loadCommonInfo)) ? null : loadCommonInfo;
		}

		// Token: 0x06009C28 RID: 39976 RVA: 0x003FD1B0 File Offset: 0x003FB5B0
		public ItemColorData.ColorData SafeGetItemColorData(int _group, int _category, int _id)
		{
			Dictionary<int, Dictionary<int, ItemColorData.ColorData>> dictionary = null;
			if (!this.dicItemColorData.TryGetValue(_group, out dictionary))
			{
				return null;
			}
			Dictionary<int, ItemColorData.ColorData> dictionary2 = null;
			if (!dictionary.TryGetValue(_category, out dictionary2))
			{
				return null;
			}
			ItemColorData.ColorData colorData = null;
			return (!dictionary2.TryGetValue(_id, out colorData)) ? null : colorData;
		}

		// Token: 0x06009C29 RID: 39977 RVA: 0x003FD200 File Offset: 0x003FB600
		public bool ExistItemGroup(int _group)
		{
			Dictionary<int, Dictionary<int, Info.ItemLoadInfo>> source = null;
			if (!this.dicItemLoadInfo.TryGetValue(_group, out source))
			{
				return false;
			}
			return source.Sum((KeyValuePair<int, Dictionary<int, Info.ItemLoadInfo>> _v) => _v.Value.Count) != 0;
		}

		// Token: 0x06009C2A RID: 39978 RVA: 0x003FD250 File Offset: 0x003FB650
		public bool ExistItemCategory(int _group, int _category)
		{
			Dictionary<int, Dictionary<int, Info.ItemLoadInfo>> dictionary = null;
			if (!this.dicItemLoadInfo.TryGetValue(_group, out dictionary))
			{
				return false;
			}
			Dictionary<int, Info.ItemLoadInfo> dictionary2 = null;
			return dictionary.TryGetValue(_category, out dictionary2) && dictionary2.Count != 0;
		}

		// Token: 0x06009C2B RID: 39979 RVA: 0x003FD294 File Offset: 0x003FB694
		public bool ExistAnimeGroup(int _group)
		{
			Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>> source = null;
			if (!this.dicAnimeLoadInfo.TryGetValue(_group, out source))
			{
				return false;
			}
			return source.Sum((KeyValuePair<int, Dictionary<int, Info.AnimeLoadInfo>> _v) => _v.Value.Count) != 0;
		}

		// Token: 0x06009C2C RID: 39980 RVA: 0x003FD2E4 File Offset: 0x003FB6E4
		public bool ExistAnimeCategory(int _group, int _category)
		{
			Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>> dictionary = null;
			if (!this.dicAnimeLoadInfo.TryGetValue(_group, out dictionary))
			{
				return false;
			}
			Dictionary<int, Info.AnimeLoadInfo> dictionary2 = null;
			return dictionary.TryGetValue(_category, out dictionary2) && dictionary2.Count != 0;
		}

		// Token: 0x06009C2D RID: 39981 RVA: 0x003FD328 File Offset: 0x003FB728
		private ExcelData LoadExcelData(string _bundlePath, string _fileName)
		{
			string text = string.Empty;
			if (AssetBundleCheck.IsSimulation)
			{
				if (!AssetBundleCheck.FindFile(_bundlePath, _fileName, false))
				{
					return null;
				}
			}
			else
			{
				bool flag = false;
				foreach (KeyValuePair<string, AssetBundleManager.BundlePack> keyValuePair in from v in AssetBundleManager.ManifestBundlePack
				where Regex.Match(v.Key, "studio(\\d*)").Success
				select v)
				{
					flag |= (keyValuePair.Value.AssetBundleManifest.GetAllAssetBundles().ToList<string>().FindIndex((string s) => s == _bundlePath) != -1);
					if (flag)
					{
						text = keyValuePair.Key;
						break;
					}
				}
				if (!flag)
				{
					return null;
				}
			}
			string bundlePath = _bundlePath;
			string manifestName = text;
			ExcelData excelData = CommonLib.LoadAsset<ExcelData>(bundlePath, _fileName, false, manifestName);
			if (null == excelData)
			{
				return null;
			}
			return excelData;
		}

		// Token: 0x06009C2E RID: 39982 RVA: 0x003FD450 File Offset: 0x003FB850
		private string[] FindAllAssetName(string _bundlePath, string _regex)
		{
			string[] result = null;
			if (AssetBundleCheck.IsSimulation)
			{
				result = AssetBundleCheck.FindAllAssetName(_bundlePath, _regex, false, RegexOptions.IgnoreCase);
			}
			else
			{
				foreach (KeyValuePair<string, AssetBundleManager.BundlePack> keyValuePair in from v in AssetBundleManager.ManifestBundlePack
				where Regex.Match(v.Key, "studio(\\d*)").Success
				select v)
				{
					if (keyValuePair.Value.AssetBundleManifest.GetAllAssetBundles().ToList<string>().FindIndex((string s) => s == _bundlePath) != -1)
					{
						LoadedAssetBundle loadedAssetBundle = null;
						if (!keyValuePair.Value.LoadedAssetBundles.TryGetValue(_bundlePath, out loadedAssetBundle))
						{
							loadedAssetBundle = AssetBundleManager.LoadAssetBundle(_bundlePath, false, keyValuePair.Key);
							if (loadedAssetBundle == null)
							{
								break;
							}
						}
						IEnumerable<string> allAssetNames = loadedAssetBundle.m_AssetBundle.GetAllAssetNames();
						if (Info.<>f__mg$cache0 == null)
						{
							Info.<>f__mg$cache0 = new Func<string, string>(Path.GetFileNameWithoutExtension);
						}
						result = allAssetNames.Select(Info.<>f__mg$cache0).Where((string s) => Regex.Match(s, _regex, RegexOptions.IgnoreCase).Success).ToArray<string>();
						loadedAssetBundle = null;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06009C2F RID: 39983 RVA: 0x003FD5C0 File Offset: 0x003FB9C0
		private void LoadAccessoryGroupInfo(ExcelData _ed)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(1)
			select v.list)
			{
				int key = -1;
				if (int.TryParse(list.SafeGet(0), out key))
				{
					string text = list.SafeGet(1);
					if (!text.IsNullOrEmpty())
					{
						this.dicAccessoryGroup[key] = new Info.AccessoryGroupInfo(text, list.SafeGet(2));
					}
				}
			}
			this.m_AccessoryPointGroup = _ed;
			int num = 0;
			foreach (KeyValuePair<int, Info.AccessoryGroupInfo> keyValuePair in this.dicAccessoryGroup)
			{
				int num2 = keyValuePair.Value.Targets.SafeGet(0);
				int num3 = keyValuePair.Value.Targets.SafeGet(1);
				for (int i = num2; i <= num3; i++)
				{
					num++;
				}
			}
			this.AccessoryPointNum = num;
		}

		// Token: 0x06009C30 RID: 39984 RVA: 0x003FD730 File Offset: 0x003FBB30
		private void LoadBoneInfo(ExcelData _ed, Dictionary<int, Info.BoneInfo> _dic)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(1)
			select v.list)
			{
				int num = 0;
				int num2 = -1;
				if (int.TryParse(list[num++], out num2))
				{
					string text = list[num++];
					if (!text.IsNullOrEmpty())
					{
						_dic[num2] = new Info.BoneInfo(num2, text, list);
					}
				}
			}
		}

		// Token: 0x06009C31 RID: 39985 RVA: 0x003FD808 File Offset: 0x003FBC08
		private IEnumerator LoadItemLoadInfoCoroutine(string _bundlePath, string _regex)
		{
			string[] files = this.FindAllAssetName(_bundlePath, _regex);
			if (files.IsNullOrEmpty<string>())
			{
				yield break;
			}
			string checkKey = _regex.Split(new char[]
			{
				'_'
			})[0].ToLower();
			SortedDictionary<int, SortedDictionary<int, SortedDictionary<int, string>>> sortDic = new SortedDictionary<int, SortedDictionary<int, SortedDictionary<int, string>>>();
			for (int i = 0; i < files.Length; i++)
			{
				if (files[i].Split(new char[]
				{
					'_'
				})[0].ToLower().CompareTo(checkKey) == 0)
				{
					Match match = Regex.Match(files[i], _regex, RegexOptions.IgnoreCase);
					int num = int.Parse(match.Groups[1].Value);
					int key = int.Parse(match.Groups[2].Value);
					int key2 = int.Parse(match.Groups[3].Value);
					if (num == 50)
					{
						if (!this.IsInstallAdd)
						{
							goto IL_1BC;
						}
					}
					if (!sortDic.ContainsKey(key))
					{
						sortDic.Add(key, new SortedDictionary<int, SortedDictionary<int, string>>());
					}
					if (!sortDic[key].ContainsKey(key2))
					{
						sortDic[key].Add(key2, new SortedDictionary<int, string>());
					}
					sortDic[key][key2].Add(num, files[i]);
				}
				IL_1BC:;
			}
			foreach (KeyValuePair<int, SortedDictionary<int, SortedDictionary<int, string>>> g in sortDic)
			{
				foreach (KeyValuePair<int, SortedDictionary<int, string>> c in g.Value)
				{
					foreach (KeyValuePair<int, string> v in c.Value)
					{
						this.LoadItemLoadInfo(this.LoadExcelData(_bundlePath, v.Value));
						if (this.waitTime.isOver)
						{
							yield return null;
							this.waitTime.Next();
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06009C32 RID: 39986 RVA: 0x003FD834 File Offset: 0x003FBC34
		private void SortDictionary(string[] files, string _regex, SortedDictionary<int, SortedDictionary<int, string>> _sortDic)
		{
			string strB = _regex.Split(new char[]
			{
				'_'
			})[0].ToLower();
			for (int i = 0; i < files.Length; i++)
			{
				if (files[i].Split(new char[]
				{
					'_'
				})[0].ToLower().CompareTo(strB) == 0)
				{
					Match match = Regex.Match(files[i], _regex, RegexOptions.IgnoreCase);
					int key = int.Parse(match.Groups[1].Value);
					int key2 = int.Parse(match.Groups[2].Value);
					if (!_sortDic.ContainsKey(key))
					{
						_sortDic.Add(key, new SortedDictionary<int, string>());
					}
					_sortDic[key].Add(key2, files[i]);
				}
			}
		}

		// Token: 0x06009C33 RID: 39987 RVA: 0x003FD8FC File Offset: 0x003FBCFC
		private void LoadItemLoadInfo(ExcelData _ed)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(1)
			select v.list)
			{
				int key = -1;
				if (!int.TryParse(list.SafeGet(0), out key))
				{
					break;
				}
				int key2 = int.Parse(list[1]);
				int key3 = int.Parse(list[2]);
				if (!this.dicItemLoadInfo.ContainsKey(key2))
				{
					this.dicItemLoadInfo[key2] = new Dictionary<int, Dictionary<int, Info.ItemLoadInfo>>();
				}
				if (!this.dicItemLoadInfo[key2].ContainsKey(key3))
				{
					this.dicItemLoadInfo[key2][key3] = new Dictionary<int, Info.ItemLoadInfo>();
				}
				this.dicItemLoadInfo[key2][key3][key] = new Info.ItemLoadInfo(list);
			}
		}

		// Token: 0x06009C34 RID: 39988 RVA: 0x003FDA2C File Offset: 0x003FBE2C
		private void LoadItemBoneInfo(string _bundlePath, string _regex)
		{
			string[] array = this.FindAllAssetName(_bundlePath, _regex);
			if (array.IsNullOrEmpty<string>())
			{
				return;
			}
			SortedDictionary<int, SortedDictionary<int, string>> sortedDictionary = new SortedDictionary<int, SortedDictionary<int, string>>();
			this.SortDictionary(array, _regex, sortedDictionary);
			foreach (KeyValuePair<int, SortedDictionary<int, string>> keyValuePair in sortedDictionary)
			{
				foreach (KeyValuePair<int, string> keyValuePair2 in keyValuePair.Value)
				{
					this.LoadItemBoneInfo(this.LoadExcelData(_bundlePath, keyValuePair2.Value), keyValuePair.Key, keyValuePair2.Key);
				}
			}
		}

		// Token: 0x06009C35 RID: 39989 RVA: 0x003FDB0C File Offset: 0x003FBF0C
		private void LoadItemColorData(string _bundlePath, string _file)
		{
			string text = string.Empty;
			if (AssetBundleCheck.IsSimulation)
			{
				if (!AssetBundleCheck.FindFile(_bundlePath, _file, false))
				{
					return;
				}
			}
			else
			{
				bool flag = false;
				foreach (KeyValuePair<string, AssetBundleManager.BundlePack> keyValuePair in from v in AssetBundleManager.ManifestBundlePack
				where Regex.Match(v.Key, "studio(\\d*)").Success
				select v)
				{
					flag |= (keyValuePair.Value.AssetBundleManifest.GetAllAssetBundles().ToList<string>().FindIndex((string s) => s == _bundlePath) != -1);
					if (flag)
					{
						text = keyValuePair.Key;
						break;
					}
				}
				if (!flag)
				{
					return;
				}
			}
			string bundlePath = _bundlePath;
			string manifestName = text;
			ItemColorData itemColorData = CommonLib.LoadAsset<ItemColorData>(bundlePath, _file, false, manifestName);
			if (itemColorData == null)
			{
				return;
			}
			foreach (KeyValuePair<int, Dictionary<int, Dictionary<int, ItemColorData.ColorData>>> keyValuePair2 in itemColorData.ColorDatas)
			{
				Dictionary<int, Dictionary<int, ItemColorData.ColorData>> dictionary = null;
				if (!this.dicItemColorData.TryGetValue(keyValuePair2.Key, out dictionary))
				{
					dictionary = new Dictionary<int, Dictionary<int, ItemColorData.ColorData>>();
					this.dicItemColorData.Add(keyValuePair2.Key, dictionary);
				}
				foreach (KeyValuePair<int, Dictionary<int, ItemColorData.ColorData>> keyValuePair3 in keyValuePair2.Value)
				{
					Dictionary<int, ItemColorData.ColorData> dictionary2 = null;
					if (!dictionary.TryGetValue(keyValuePair3.Key, out dictionary2))
					{
						dictionary2 = new Dictionary<int, ItemColorData.ColorData>();
						dictionary.Add(keyValuePair3.Key, dictionary2);
					}
					foreach (KeyValuePair<int, ItemColorData.ColorData> keyValuePair4 in keyValuePair3.Value)
					{
						dictionary2[keyValuePair4.Key] = new ItemColorData.ColorData(keyValuePair4.Value);
					}
				}
			}
		}

		// Token: 0x06009C36 RID: 39990 RVA: 0x003FDDB4 File Offset: 0x003FC1B4
		private void LoadItemBoneInfo(ExcelData _ed, int _group, int _category)
		{
			if (_ed == null)
			{
				return;
			}
			Dictionary<int, Dictionary<int, Info.ItemLoadInfo>> dictionary = null;
			if (!this.dicItemLoadInfo.TryGetValue(_group, out dictionary))
			{
				return;
			}
			Dictionary<int, Info.ItemLoadInfo> dictionary2 = null;
			if (!dictionary.TryGetValue(_category, out dictionary2))
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(1)
			select v.list)
			{
				Info.ItemLoadInfo itemLoadInfo = null;
				int key = -1;
				if (int.TryParse(list.SafeGet(0), out key))
				{
					if (dictionary2.TryGetValue(key, out itemLoadInfo))
					{
						itemLoadInfo.bones = list.Skip(1).Where((string s) => !s.IsNullOrEmpty()).ToList<string>();
					}
				}
			}
		}

		// Token: 0x06009C37 RID: 39991 RVA: 0x003FDECC File Offset: 0x003FC2CC
		private void LoadLightLoadInfo(ExcelData _ed)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(1)
			select v.list)
			{
				int num = 0;
				Info.LightLoadInfo lightLoadInfo = new Info.LightLoadInfo();
				lightLoadInfo.no = int.Parse(list[num++]);
				lightLoadInfo.name = list[num++];
				lightLoadInfo.manifest = list[num++];
				lightLoadInfo.bundlePath = list[num++];
				lightLoadInfo.fileName = list[num++];
				lightLoadInfo.target = (Info.LightLoadInfo.Target)int.Parse(list[num++]);
				this.dicLightLoadInfo[lightLoadInfo.no] = lightLoadInfo;
			}
		}

		// Token: 0x06009C38 RID: 39992 RVA: 0x003FDFE0 File Offset: 0x003FC3E0
		private void LoadAnimeGroupInfo(ExcelData _ed, Dictionary<int, Info.GroupInfo> _dic)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(1)
			select v.list)
			{
				int num = 0;
				int sort = 0;
				if (int.TryParse(list.SafeGet(num++), out sort))
				{
					int key = -1;
					if (int.TryParse(list.SafeGet(num++), out key))
					{
						string name = list[num++];
						Info.GroupInfo groupInfo = null;
						if (_dic.TryGetValue(key, out groupInfo))
						{
							groupInfo.sort = sort;
							groupInfo.name = name;
						}
						else
						{
							groupInfo = new Info.GroupInfo
							{
								sort = sort,
								name = name
							};
							groupInfo.name = name;
							_dic.Add(key, groupInfo);
						}
					}
				}
			}
		}

		// Token: 0x06009C39 RID: 39993 RVA: 0x003FE10C File Offset: 0x003FC50C
		private void LoadAnimeCategoryInfo(string _bundlePath, string _regex, Dictionary<int, Info.GroupInfo> _dic)
		{
			string[] array = this.FindAllAssetName(_bundlePath, _regex);
			if (array.IsNullOrEmpty<string>())
			{
				return;
			}
			string strB = _regex.Split(new char[]
			{
				'_'
			})[0].ToLower();
			SortedDictionary<int, SortedDictionary<int, string>> sortedDictionary = new SortedDictionary<int, SortedDictionary<int, string>>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Split(new char[]
				{
					'_'
				})[0].ToLower().CompareTo(strB) == 0)
				{
					Match match = Regex.Match(array[i], _regex, RegexOptions.IgnoreCase);
					int key = int.Parse(match.Groups[1].Value);
					int key2 = int.Parse(match.Groups[2].Value);
					if (!sortedDictionary.ContainsKey(key2))
					{
						sortedDictionary.Add(key2, new SortedDictionary<int, string>());
					}
					sortedDictionary[key2].Add(key, array[i]);
				}
			}
			foreach (KeyValuePair<int, SortedDictionary<int, string>> keyValuePair in sortedDictionary)
			{
				if (_dic.ContainsKey(keyValuePair.Key))
				{
					foreach (KeyValuePair<int, string> keyValuePair2 in keyValuePair.Value)
					{
						this.LoadAnimeCategoryInfo(this.LoadExcelData(_bundlePath, keyValuePair2.Value), _dic[keyValuePair.Key]);
					}
				}
			}
		}

		// Token: 0x06009C3A RID: 39994 RVA: 0x003FE2BC File Offset: 0x003FC6BC
		private void LoadAnimeCategoryInfo(ExcelData _ed, Info.GroupInfo _info)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(1)
			select v.list)
			{
				int num = 0;
				int sort = 0;
				if (int.TryParse(list.SafeGet(num++), out sort))
				{
					int key = -1;
					if (int.TryParse(list.SafeGet(num++), out key))
					{
						_info.dicCategory[key] = new Info.CategoryInfo
						{
							sort = sort,
							name = list.SafeGet(num++)
						};
					}
				}
			}
		}

		// Token: 0x06009C3B RID: 39995 RVA: 0x003FE3B4 File Offset: 0x003FC7B4
		private IEnumerator LoadAnimeLoadInfoCoroutine(string _bundlePath, string _regex, Dictionary<int, Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>>> _dic, Info.LoadAnimeInfoCoroutineFunc _func)
		{
			string[] files = this.FindAllAssetName(_bundlePath, _regex);
			if (files.IsNullOrEmpty<string>())
			{
				yield break;
			}
			string checkKey = _regex.Split(new char[]
			{
				'_'
			})[0].ToLower();
			SortedDictionary<int, SortedDictionary<int, SortedDictionary<int, string>>> sortDic = new SortedDictionary<int, SortedDictionary<int, SortedDictionary<int, string>>>();
			for (int i = 0; i < files.Length; i++)
			{
				if (files[i].Split(new char[]
				{
					'_'
				})[0].ToLower().CompareTo(checkKey) == 0)
				{
					Match match = Regex.Match(files[i], _regex, RegexOptions.IgnoreCase);
					int num = int.Parse(match.Groups[1].Value);
					int key = int.Parse(match.Groups[2].Value);
					int key2 = int.Parse(match.Groups[3].Value);
					if (num == 50)
					{
						if (!this.IsInstallAdd)
						{
							goto IL_1BC;
						}
					}
					if (!sortDic.ContainsKey(key))
					{
						sortDic.Add(key, new SortedDictionary<int, SortedDictionary<int, string>>());
					}
					if (!sortDic[key].ContainsKey(key2))
					{
						sortDic[key].Add(key2, new SortedDictionary<int, string>());
					}
					sortDic[key][key2].Add(num, files[i]);
				}
				IL_1BC:;
			}
			foreach (KeyValuePair<int, SortedDictionary<int, SortedDictionary<int, string>>> g in sortDic)
			{
				foreach (KeyValuePair<int, SortedDictionary<int, string>> c in g.Value)
				{
					foreach (KeyValuePair<int, string> v in c.Value)
					{
						_func(this.LoadExcelData(_bundlePath, v.Value), _dic);
						if (this.waitTime.isOver)
						{
							yield return null;
							this.waitTime.Next();
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06009C3C RID: 39996 RVA: 0x003FE3EC File Offset: 0x003FC7EC
		private void LoadAnimeLoadInfo(ExcelData _ed, Dictionary<int, Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>>> _dic)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(2)
			select v.list)
			{
				if (this.fileCheck.Check(list.SafeGet(5)))
				{
					int num = 0;
					int sort = 0;
					if (int.TryParse(list.SafeGet(num++), out sort))
					{
						int key = -1;
						if (int.TryParse(list.SafeGet(num++), out key))
						{
							int key2 = int.Parse(list.SafeGet(num++));
							int key3 = int.Parse(list.SafeGet(num++));
							if (!_dic.ContainsKey(key2))
							{
								_dic.Add(key2, new Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>>());
							}
							if (!_dic[key2].ContainsKey(key3))
							{
								_dic[key2].Add(key3, new Dictionary<int, Info.AnimeLoadInfo>());
							}
							_dic[key2][key3][key] = new Info.AnimeLoadInfo
							{
								sort = sort,
								name = list.SafeGet(num++),
								bundlePath = list.SafeGet(num++),
								fileName = list.SafeGet(num++),
								clip = list.SafeGet(num++),
								option = Info.AnimeLoadInfo.LoadOption(list, num++, true)
							};
						}
					}
				}
			}
		}

		// Token: 0x06009C3D RID: 39997 RVA: 0x003FE5C4 File Offset: 0x003FC9C4
		private void LoadHAnimeLoadInfo(ExcelData _ed, Dictionary<int, Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>>> _dic)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(2)
			select v.list)
			{
				if (this.fileCheck.Check(list.SafeGet(5)))
				{
					int num = 0;
					if (int.TryParse(list.SafeGet(0), out num))
					{
						int key = 0;
						if (int.TryParse(list.SafeGet(1), out key))
						{
							int key2 = int.Parse(list.SafeGet(2));
							int key3 = int.Parse(list.SafeGet(3));
							if (!_dic.ContainsKey(key2))
							{
								_dic.Add(key2, new Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>>());
							}
							if (!_dic[key2].ContainsKey(key3))
							{
								_dic[key2].Add(key3, new Dictionary<int, Info.AnimeLoadInfo>());
							}
							_dic[key2][key3][key] = new Info.HAnimeLoadInfo(4, list);
						}
					}
				}
			}
		}

		// Token: 0x06009C3E RID: 39998 RVA: 0x003FE718 File Offset: 0x003FCB18
		private IEnumerator LoadVoiceLoadInfoCoroutine(string _bundlePath, string _regex)
		{
			string[] files = this.FindAllAssetName(_bundlePath, _regex);
			if (files.IsNullOrEmpty<string>())
			{
				yield break;
			}
			string checkKey = _regex.Split(new char[]
			{
				'_'
			})[0].ToLower();
			SortedDictionary<int, SortedDictionary<int, SortedDictionary<int, string>>> sortDic = new SortedDictionary<int, SortedDictionary<int, SortedDictionary<int, string>>>();
			for (int i = 0; i < files.Length; i++)
			{
				if (files[i].Split(new char[]
				{
					'_'
				})[0].ToLower().CompareTo(checkKey) == 0)
				{
					Match match = Regex.Match(files[i], _regex, RegexOptions.IgnoreCase);
					int key = int.Parse(match.Groups[1].Value);
					int key2 = int.Parse(match.Groups[2].Value);
					int key3 = int.Parse(match.Groups[3].Value);
					if (!sortDic.ContainsKey(key))
					{
						sortDic.Add(key, new SortedDictionary<int, SortedDictionary<int, string>>());
					}
					if (!sortDic[key].ContainsKey(key2))
					{
						sortDic[key].Add(key2, new SortedDictionary<int, string>());
					}
					sortDic[key][key2].Add(key3, files[i]);
				}
			}
			foreach (KeyValuePair<int, SortedDictionary<int, SortedDictionary<int, string>>> g in sortDic)
			{
				foreach (KeyValuePair<int, SortedDictionary<int, string>> c in g.Value)
				{
					foreach (KeyValuePair<int, string> v in c.Value)
					{
						this.LoadVoiceLoadInfo(this.LoadExcelData(_bundlePath, v.Value));
						if (this.waitTime.isOver)
						{
							yield return null;
							this.waitTime.Next();
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06009C3F RID: 39999 RVA: 0x003FE744 File Offset: 0x003FCB44
		private void LoadVoiceLoadInfo(ExcelData _ed)
		{
			foreach (List<string> list in from v in _ed.list.Skip(2)
			select v.list)
			{
				this.LoadVoiceLoadInfo(list);
			}
		}

		// Token: 0x06009C40 RID: 40000 RVA: 0x003FE7C8 File Offset: 0x003FCBC8
		private Info.LoadCommonInfo LoadVoiceLoadInfo(List<string> _list)
		{
			int num = 0;
			Info.LoadCommonInfo loadCommonInfo = new Info.LoadCommonInfo();
			int key = -1;
			if (!int.TryParse(_list[num++], out key))
			{
				return null;
			}
			int key2 = -1;
			if (!int.TryParse(_list[num++], out key2))
			{
				return null;
			}
			int key3 = -1;
			if (!int.TryParse(_list[num++], out key3))
			{
				return null;
			}
			loadCommonInfo.name = _list[num++];
			loadCommonInfo.bundlePath = _list[num++];
			loadCommonInfo.fileName = _list[num++];
			if (!this.dicVoiceLoadInfo.ContainsKey(key2))
			{
				this.dicVoiceLoadInfo.Add(key2, new Dictionary<int, Dictionary<int, Info.LoadCommonInfo>>());
			}
			if (!this.dicVoiceLoadInfo[key2].ContainsKey(key3))
			{
				this.dicVoiceLoadInfo[key2].Add(key3, new Dictionary<int, Info.LoadCommonInfo>());
			}
			this.dicVoiceLoadInfo[key2][key3][key] = loadCommonInfo;
			return loadCommonInfo;
		}

		// Token: 0x06009C41 RID: 40001 RVA: 0x003FE8D0 File Offset: 0x003FCCD0
		private void LoadSoundLoadInfo(ExcelData _ed, Dictionary<int, Info.LoadCommonInfo> _dic)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(1)
			select v.list)
			{
				int num = 0;
				int key = -1;
				if (int.TryParse(list[num++], out key))
				{
					_dic[key] = new Info.LoadCommonInfo
					{
						name = list[num++],
						bundlePath = list[num++],
						fileName = list[num++]
					};
				}
			}
		}

		// Token: 0x06009C42 RID: 40002 RVA: 0x003FE9C0 File Offset: 0x003FCDC0
		private void LoadMapLoadInfo(ExcelData _ed, Dictionary<int, Info.MapLoadInfo> _dic)
		{
			if (_ed == null)
			{
				return;
			}
			foreach (List<string> list in from v in _ed.list.Skip(2)
			select v.list)
			{
				int num = 0;
				int key = -1;
				if (int.TryParse(list[num++], out key))
				{
					_dic[key] = new Info.MapLoadInfo(list);
				}
			}
		}

		// Token: 0x06009C43 RID: 40003 RVA: 0x003FEA74 File Offset: 0x003FCE74
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.CheckIsAdd();
			this.isLoadList = false;
		}

		// Token: 0x04007C4D RID: 31821
		public Dictionary<int, Info.BoneInfo> dicBoneInfo = new Dictionary<int, Info.BoneInfo>();

		// Token: 0x04007C4E RID: 31822
		public Dictionary<int, Info.GroupInfo> dicItemGroupCategory = new Dictionary<int, Info.GroupInfo>();

		// Token: 0x04007C4F RID: 31823
		public Dictionary<int, Dictionary<int, Dictionary<int, Info.ItemLoadInfo>>> dicItemLoadInfo = new Dictionary<int, Dictionary<int, Dictionary<int, Info.ItemLoadInfo>>>();

		// Token: 0x04007C50 RID: 31824
		public Dictionary<int, Dictionary<int, Dictionary<int, ItemColorData.ColorData>>> dicItemColorData = new Dictionary<int, Dictionary<int, Dictionary<int, ItemColorData.ColorData>>>();

		// Token: 0x04007C51 RID: 31825
		public Dictionary<int, Info.AccessoryGroupInfo> dicAccessoryGroup = new Dictionary<int, Info.AccessoryGroupInfo>();

		// Token: 0x04007C53 RID: 31827
		private ExcelData m_AccessoryPointGroup;

		// Token: 0x04007C54 RID: 31828
		public Dictionary<int, Info.LightLoadInfo> dicLightLoadInfo = new Dictionary<int, Info.LightLoadInfo>();

		// Token: 0x04007C55 RID: 31829
		public Dictionary<int, Info.GroupInfo> dicAGroupCategory = new Dictionary<int, Info.GroupInfo>();

		// Token: 0x04007C56 RID: 31830
		public Dictionary<int, Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>>> dicAnimeLoadInfo = new Dictionary<int, Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>>>();

		// Token: 0x04007C57 RID: 31831
		public Dictionary<int, Info.HandAnimeInfo>[] dicHandAnime = new Dictionary<int, Info.HandAnimeInfo>[]
		{
			new Dictionary<int, Info.HandAnimeInfo>(),
			new Dictionary<int, Info.HandAnimeInfo>()
		};

		// Token: 0x04007C58 RID: 31832
		public Dictionary<int, Info.GroupInfo> dicVoiceGroupCategory = new Dictionary<int, Info.GroupInfo>();

		// Token: 0x04007C59 RID: 31833
		public Dictionary<int, Dictionary<int, Dictionary<int, Info.LoadCommonInfo>>> dicVoiceLoadInfo = new Dictionary<int, Dictionary<int, Dictionary<int, Info.LoadCommonInfo>>>();

		// Token: 0x04007C5A RID: 31834
		public Dictionary<int, Info.LoadCommonInfo> dicBGMLoadInfo = new Dictionary<int, Info.LoadCommonInfo>();

		// Token: 0x04007C5B RID: 31835
		public Dictionary<int, Info.LoadCommonInfo> dicENVLoadInfo = new Dictionary<int, Info.LoadCommonInfo>();

		// Token: 0x04007C5C RID: 31836
		public Dictionary<int, Info.MapLoadInfo> dicMapLoadInfo = new Dictionary<int, Info.MapLoadInfo>();

		// Token: 0x04007C5D RID: 31837
		public Dictionary<int, Info.LoadCommonInfo> dicColorGradingLoadInfo = new Dictionary<int, Info.LoadCommonInfo>();

		// Token: 0x04007C5E RID: 31838
		public Dictionary<int, Info.LoadCommonInfo> dicReflectionProbeLoadInfo = new Dictionary<int, Info.LoadCommonInfo>();

		// Token: 0x04007C5F RID: 31839
		private Info.FileCheck fileCheck;

		// Token: 0x04007C61 RID: 31841
		private Info.WaitTime waitTime;

		// Token: 0x04007C66 RID: 31846
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache0;

		// Token: 0x0200126B RID: 4715
		public class FileInfo
		{
			// Token: 0x17002170 RID: 8560
			// (get) Token: 0x06009C57 RID: 40023 RVA: 0x003FEB92 File Offset: 0x003FCF92
			public bool Check
			{
				get
				{
					return !this.bundlePath.IsNullOrEmpty() & !this.fileName.IsNullOrEmpty();
				}
			}

			// Token: 0x06009C58 RID: 40024 RVA: 0x003FEBB1 File Offset: 0x003FCFB1
			public void Clear()
			{
				this.manifest = string.Empty;
				this.bundlePath = string.Empty;
				this.fileName = string.Empty;
			}

			// Token: 0x04007C76 RID: 31862
			public string manifest = string.Empty;

			// Token: 0x04007C77 RID: 31863
			public string bundlePath = string.Empty;

			// Token: 0x04007C78 RID: 31864
			public string fileName = string.Empty;
		}

		// Token: 0x0200126C RID: 4716
		public class LoadCommonInfo : Info.FileInfo
		{
			// Token: 0x04007C79 RID: 31865
			public string name = string.Empty;
		}

		// Token: 0x0200126D RID: 4717
		public class CategoryInfo
		{
			// Token: 0x04007C7A RID: 31866
			public int sort;

			// Token: 0x04007C7B RID: 31867
			public string name = string.Empty;
		}

		// Token: 0x0200126E RID: 4718
		public class GroupInfo
		{
			// Token: 0x04007C7C RID: 31868
			public int sort;

			// Token: 0x04007C7D RID: 31869
			public string name = string.Empty;

			// Token: 0x04007C7E RID: 31870
			public Dictionary<int, Info.CategoryInfo> dicCategory = new Dictionary<int, Info.CategoryInfo>();
		}

		// Token: 0x0200126F RID: 4719
		public class BoneInfo
		{
			// Token: 0x06009C5C RID: 40028 RVA: 0x003FEC18 File Offset: 0x003FD018
			public BoneInfo(int _no, string _bone, List<string> _lst)
			{
				this.no = _no;
				this.bone = _bone;
				int num = 2;
				this.name = _lst.SafeGet(num++);
				if (!int.TryParse(_lst.SafeGet(num++), out this.group))
				{
					this.group = 0;
				}
				if (!int.TryParse(_lst.SafeGet(num++), out this.level))
				{
					this.level = 0;
				}
				if (!int.TryParse(_lst.SafeGet(num++), out this.sync))
				{
					this.sync = -1;
				}
			}

			// Token: 0x04007C7F RID: 31871
			public int no;

			// Token: 0x04007C80 RID: 31872
			public string bone = string.Empty;

			// Token: 0x04007C81 RID: 31873
			public string name = string.Empty;

			// Token: 0x04007C82 RID: 31874
			public int group = -1;

			// Token: 0x04007C83 RID: 31875
			public int level;

			// Token: 0x04007C84 RID: 31876
			public int sync = -1;
		}

		// Token: 0x02001270 RID: 4720
		public class ItemLoadInfo : Info.LoadCommonInfo
		{
			// Token: 0x06009C5D RID: 40029 RVA: 0x003FECD6 File Offset: 0x003FD0D6
			public ItemLoadInfo(List<string> _lst)
			{
				this.name = _lst[3];
				this.manifest = _lst[4];
				this.bundlePath = _lst[5];
				this.fileName = _lst[6];
			}

			// Token: 0x04007C85 RID: 31877
			public List<string> bones;
		}

		// Token: 0x02001271 RID: 4721
		public class AccessoryGroupInfo
		{
			// Token: 0x06009C5E RID: 40030 RVA: 0x003FED14 File Offset: 0x003FD114
			public AccessoryGroupInfo(string _name, string _targets)
			{
				this.Name = _name;
				IEnumerable<string> source = _targets.Split(new char[]
				{
					'-'
				});
				if (Info.AccessoryGroupInfo.<>f__mg$cache0 == null)
				{
					Info.AccessoryGroupInfo.<>f__mg$cache0 = new Func<string, int>(int.Parse);
				}
				this.Targets = source.Select(Info.AccessoryGroupInfo.<>f__mg$cache0).ToArray<int>();
			}

			// Token: 0x17002171 RID: 8561
			// (get) Token: 0x06009C5F RID: 40031 RVA: 0x003FED77 File Offset: 0x003FD177
			// (set) Token: 0x06009C60 RID: 40032 RVA: 0x003FED7F File Offset: 0x003FD17F
			public string Name { get; private set; } = string.Empty;

			// Token: 0x17002172 RID: 8562
			// (get) Token: 0x06009C61 RID: 40033 RVA: 0x003FED88 File Offset: 0x003FD188
			// (set) Token: 0x06009C62 RID: 40034 RVA: 0x003FED90 File Offset: 0x003FD190
			public int[] Targets { get; private set; }

			// Token: 0x04007C88 RID: 31880
			[CompilerGenerated]
			private static Func<string, int> <>f__mg$cache0;
		}

		// Token: 0x02001272 RID: 4722
		public class LightLoadInfo : Info.LoadCommonInfo
		{
			// Token: 0x04007C89 RID: 31881
			public int no;

			// Token: 0x04007C8A RID: 31882
			public Info.LightLoadInfo.Target target;

			// Token: 0x02001273 RID: 4723
			public enum Target
			{
				// Token: 0x04007C8C RID: 31884
				All,
				// Token: 0x04007C8D RID: 31885
				Chara,
				// Token: 0x04007C8E RID: 31886
				Map
			}
		}

		// Token: 0x02001274 RID: 4724
		public class ParentageInfo
		{
			// Token: 0x04007C8F RID: 31887
			public string parent = string.Empty;

			// Token: 0x04007C90 RID: 31888
			public string child = string.Empty;
		}

		// Token: 0x02001275 RID: 4725
		public class OptionItemInfo : Info.FileInfo
		{
			// Token: 0x04007C91 RID: 31889
			public Info.FileInfo anmInfo;

			// Token: 0x04007C92 RID: 31890
			public Info.FileInfo anmOveride;

			// Token: 0x04007C93 RID: 31891
			public Info.ParentageInfo[] parentageInfo;

			// Token: 0x04007C94 RID: 31892
			public bool counterScale;

			// Token: 0x04007C95 RID: 31893
			public bool isAnimeSync = true;
		}

		// Token: 0x02001276 RID: 4726
		public class AnimeLoadInfo : Info.LoadCommonInfo
		{
			// Token: 0x06009C67 RID: 40039 RVA: 0x003FEDE4 File Offset: 0x003FD1E4
			public static List<Info.OptionItemInfo> LoadOption(List<string> _list, int _start, bool _animeSync)
			{
				List<Info.OptionItemInfo> list = new List<Info.OptionItemInfo>();
				int num = _start;
				for (;;)
				{
					Info.OptionItemInfo info = new Info.OptionItemInfo();
					if (!_list.SafeProc(num++, delegate(string _s)
					{
						info.bundlePath = _s;
					}))
					{
						break;
					}
					if (!_list.SafeProc(num++, delegate(string _s)
					{
						info.fileName = _s;
					}))
					{
						break;
					}
					if (!_list.SafeProc(num++, delegate(string _s)
					{
						info.manifest = _s;
					}))
					{
						break;
					}
					info.anmInfo = new Info.FileInfo();
					info.anmInfo.bundlePath = _list.SafeGet(num++);
					info.anmInfo.fileName = _list.SafeGet(num++);
					info.anmOveride = new Info.FileInfo();
					info.anmOveride.bundlePath = _list.SafeGet(num++);
					info.anmOveride.fileName = _list.SafeGet(num++);
					info.parentageInfo = Info.AnimeLoadInfo.AnalysisParentageInfo(_list.SafeGet(num++));
					bool.TryParse(_list.SafeGet(num++), out info.counterScale);
					if (_animeSync && !bool.TryParse(_list.SafeGet(num++), out info.isAnimeSync))
					{
						info.isAnimeSync = true;
					}
					list.Add(info);
				}
				return list;
			}

			// Token: 0x06009C68 RID: 40040 RVA: 0x003FEF74 File Offset: 0x003FD374
			private static Info.ParentageInfo[] AnalysisParentageInfo(string _str)
			{
				if (_str.IsNullOrEmpty())
				{
					return null;
				}
				string[] array = _str.Split(new char[]
				{
					','
				});
				List<Info.ParentageInfo> list = new List<Info.ParentageInfo>();
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						'/'
					});
					Info.ParentageInfo parentageInfo = new Info.ParentageInfo();
					parentageInfo.parent = array2[0];
					if (array2.Length > 1)
					{
						parentageInfo.child = array2[1];
					}
					list.Add(parentageInfo);
				}
				return list.ToArray();
			}

			// Token: 0x04007C96 RID: 31894
			public int sort;

			// Token: 0x04007C97 RID: 31895
			public string clip = string.Empty;

			// Token: 0x04007C98 RID: 31896
			public List<Info.OptionItemInfo> option;
		}

		// Token: 0x02001277 RID: 4727
		public class HAnimeLoadInfo : Info.AnimeLoadInfo
		{
			// Token: 0x06009C69 RID: 40041 RVA: 0x003FF034 File Offset: 0x003FD434
			public HAnimeLoadInfo(int _startIdx, List<string> _list)
			{
				int.TryParse(_list.SafeGet(0), out this.sort);
				int num = _startIdx + 1;
				this.name = _list.SafeGet(_startIdx);
				this.bundlePath = _list.SafeGet(num++);
				this.fileName = _list.SafeGet(num++);
				this.overrideFile = new Info.FileInfo
				{
					bundlePath = _list.SafeGet(num++),
					fileName = _list.SafeGet(num++)
				};
				this.clip = _list.SafeGet(num++);
				int.TryParse(_list.SafeGet(num++), out this.breastLayer);
				this.yureFile = new Info.FileInfo
				{
					bundlePath = _list.SafeGet(num++),
					fileName = _list.SafeGet(num++)
				};
				int.TryParse(_list.SafeGet(num++), out this.motionID);
				int.TryParse(_list.SafeGet(num++), out this.num);
				this.isMotion = bool.Parse(_list.SafeGet(num++));
				this.pv = Enumerable.Repeat<bool>(true, 8).ToArray<bool>();
				for (int i = 0; i < 8; i++)
				{
					bool flag = true;
					if (bool.TryParse(_list.SafeGet(num++), out flag))
					{
						this.pv[i] = flag;
					}
				}
				this.option = Info.AnimeLoadInfo.LoadOption(_list, num++, false);
			}

			// Token: 0x17002173 RID: 8563
			// (get) Token: 0x06009C6A RID: 40042 RVA: 0x003FF1B8 File Offset: 0x003FD5B8
			public bool isBreastLayer
			{
				get
				{
					return this.breastLayer != -1;
				}
			}

			// Token: 0x04007C99 RID: 31897
			public Info.FileInfo overrideFile;

			// Token: 0x04007C9A RID: 31898
			public int breastLayer = -1;

			// Token: 0x04007C9B RID: 31899
			public Info.FileInfo yureFile;

			// Token: 0x04007C9C RID: 31900
			public int motionID;

			// Token: 0x04007C9D RID: 31901
			public int num;

			// Token: 0x04007C9E RID: 31902
			public bool isMotion;

			// Token: 0x04007C9F RID: 31903
			public bool[] pv;
		}

		// Token: 0x02001278 RID: 4728
		public class HandAnimeInfo : Info.LoadCommonInfo
		{
			// Token: 0x04007CA0 RID: 31904
			public string clip = string.Empty;
		}

		// Token: 0x02001279 RID: 4729
		public class MapLoadInfo : Info.LoadCommonInfo
		{
			// Token: 0x06009C6C RID: 40044 RVA: 0x003FF1DC File Offset: 0x003FD5DC
			public MapLoadInfo(List<string> _list)
			{
				this.name = _list[1];
				this.bundlePath = _list[2];
				this.fileName = _list[3];
				this.manifest = _list.SafeGet(4);
				this.vanish = new Info.FileInfo();
				this.vanish.bundlePath = _list.SafeGet(5);
				this.vanish.fileName = _list.SafeGet(6);
			}

			// Token: 0x04007CA1 RID: 31905
			public Info.FileInfo vanish;
		}

		// Token: 0x0200127A RID: 4730
		private class FileCheck
		{
			// Token: 0x06009C6D RID: 40045 RVA: 0x003FF252 File Offset: 0x003FD652
			public FileCheck()
			{
				this.dicConfirmed = new Dictionary<string, bool>();
			}

			// Token: 0x06009C6E RID: 40046 RVA: 0x003FF268 File Offset: 0x003FD668
			public bool Check(string _path)
			{
				if (_path.IsNullOrEmpty())
				{
					return false;
				}
				bool flag = false;
				if (this.dicConfirmed.TryGetValue(_path, out flag))
				{
					return flag;
				}
				flag = (!AssetBundleCheck.IsSimulation && File.Exists(AssetBundleManager.BaseDownloadingURL + _path));
				this.dicConfirmed.Add(_path, flag);
				return flag;
			}

			// Token: 0x04007CA2 RID: 31906
			private Dictionary<string, bool> dicConfirmed;
		}

		// Token: 0x0200127B RID: 4731
		public class WaitTime
		{
			// Token: 0x06009C6F RID: 40047 RVA: 0x003FF2C9 File Offset: 0x003FD6C9
			public WaitTime()
			{
				this.Next();
			}

			// Token: 0x17002174 RID: 8564
			// (get) Token: 0x06009C70 RID: 40048 RVA: 0x003FF2D7 File Offset: 0x003FD6D7
			public bool isOver
			{
				get
				{
					return Time.realtimeSinceStartup >= this.nextFrameTime;
				}
			}

			// Token: 0x06009C71 RID: 40049 RVA: 0x003FF2E9 File Offset: 0x003FD6E9
			public void Next()
			{
				this.nextFrameTime = Time.realtimeSinceStartup + 0.03f;
			}

			// Token: 0x04007CA3 RID: 31907
			private const float intervalTime = 0.03f;

			// Token: 0x04007CA4 RID: 31908
			private float nextFrameTime;
		}

		// Token: 0x0200127C RID: 4732
		private class FileListInfo
		{
			// Token: 0x06009C72 RID: 40050 RVA: 0x003FF2FC File Offset: 0x003FD6FC
			public FileListInfo(List<string> _list)
			{
				this.dicFile = new Dictionary<string, string[]>();
				foreach (string text in _list)
				{
					this.dicFile.Add(text, AssetBundleCheck.GetAllFileName(text, false));
				}
			}

			// Token: 0x06009C73 RID: 40051 RVA: 0x003FF370 File Offset: 0x003FD770
			public bool Check(string _path, string _file)
			{
				string[] source = null;
				if (!AssetBundleCheck.IsSimulation)
				{
					_file = _file.ToLower();
				}
				return this.dicFile.TryGetValue(_path, out source) && source.Contains(_file);
			}

			// Token: 0x04007CA5 RID: 31909
			private Dictionary<string, string[]> dicFile;
		}

		// Token: 0x0200127D RID: 4733
		// (Invoke) Token: 0x06009C75 RID: 40053
		private delegate void LoadAnimeInfoCoroutineFunc(ExcelData _ed, Dictionary<int, Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>>> _dic);
	}
}
