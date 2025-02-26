using System;
using System.Collections.Generic;
using System.Linq;
using AIProject;
using CharaCustom;
using Illusion.Extensions;
using UnityEngine;

namespace UploaderSystem
{
	// Token: 0x0200101C RID: 4124
	public class NetworkInfo : Singleton<NetworkInfo>
	{
		// Token: 0x06008A77 RID: 35447 RVA: 0x003A3BE8 File Offset: 0x003A1FE8
		public void DrawMessage(Color color, string msg)
		{
			if (null != this.logview && this.logview.IsActive)
			{
				this.logview.AddLog(color, msg, Array.Empty<object>());
			}
			else
			{
				this.popupMsg.StartMessage(0.2f, 2f, 0.2f, msg, (!this.noUserControl) ? 0 : 2);
			}
		}

		// Token: 0x06008A78 RID: 35448 RVA: 0x003A3C5A File Offset: 0x003A205A
		public void BlockUI()
		{
			if (this.objBlockUI)
			{
				this.objBlockUI.SetActiveIfDifferent(true);
			}
		}

		// Token: 0x06008A79 RID: 35449 RVA: 0x003A3C79 File Offset: 0x003A2079
		public void UnblockUI()
		{
			if (this.objBlockUI)
			{
				this.objBlockUI.SetActiveIfDifferent(false);
			}
		}

		// Token: 0x17001E38 RID: 7736
		// (get) Token: 0x06008A7A RID: 35450 RVA: 0x003A3C98 File Offset: 0x003A2098
		// (set) Token: 0x06008A7B RID: 35451 RVA: 0x003A3CA0 File Offset: 0x003A20A0
		public IReadOnlyDictionary<int, LifeStyleData.Param> AgentLifeStyleInfoTable { get; private set; }

		// Token: 0x06008A7C RID: 35452 RVA: 0x003A3CAC File Offset: 0x003A20AC
		private void LoadAgentLifeStyleInfoTable()
		{
			Dictionary<int, LifeStyleData.Param> dictionary = new Dictionary<int, LifeStyleData.Param>();
			List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/agent/lifestyle/", true);
			assetBundleNameListFromPath.Sort();
			foreach (string assetBundleName in assetBundleNameListFromPath)
			{
				foreach (LifeStyleData lifeStyleData in AssetBundleManager.LoadAllAsset(assetBundleName, typeof(LifeStyleData), null).GetAllAssets<LifeStyleData>())
				{
					foreach (LifeStyleData.Param param in lifeStyleData.param)
					{
						dictionary[param.ID] = param;
					}
				}
				AssetBundleManager.UnloadAssetBundle(assetBundleName, false, null, false);
			}
			this.AgentLifeStyleInfoTable = dictionary;
		}

		// Token: 0x06008A7D RID: 35453 RVA: 0x003A3DB4 File Offset: 0x003A21B4
		public string GetLifeStyleName(int id)
		{
			string result = "---------------";
			LifeStyleData.Param param;
			if (this.AgentLifeStyleInfoTable.TryGetValue(id, out param))
			{
				result = param.Name;
			}
			return result;
		}

		// Token: 0x06008A7E RID: 35454 RVA: 0x003A3DE4 File Offset: 0x003A21E4
		private void LoadItemList_Skill()
		{
			int[] source = new int[]
			{
				16,
				17
			};
			List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/gameitem/info/item/itemlist/", true);
			assetBundleNameListFromPath.Sort();
			Dictionary<int, Dictionary<int, ItemData.Param>> dictionary = new Dictionary<int, Dictionary<int, ItemData.Param>>();
			foreach (string assetBundleName in assetBundleNameListFromPath)
			{
				foreach (ItemData itemData in AssetBundleManager.LoadAllAsset(assetBundleName, typeof(ItemData), null).GetAllAssets<ItemData>())
				{
					int num;
					if (int.TryParse(itemData.name, out num))
					{
						if (source.Contains(num))
						{
							Dictionary<int, ItemData.Param> dictionary2;
							if (!dictionary.TryGetValue(num, out dictionary2))
							{
								dictionary2 = (dictionary[num] = new Dictionary<int, ItemData.Param>());
							}
							foreach (ItemData.Param param in itemData.param)
							{
								dictionary2[param.ID] = param;
							}
						}
					}
				}
				AssetBundleManager.UnloadAssetBundle(assetBundleName, false, null, false);
			}
			this.SkillTable = dictionary;
		}

		// Token: 0x06008A7F RID: 35455 RVA: 0x003A3F48 File Offset: 0x003A2348
		public string GetNormalSkillName(int id)
		{
			string result = "---------------";
			Dictionary<int, ItemData.Param> dictionary;
			ItemData.Param param;
			if (this.SkillTable.TryGetValue(16, out dictionary) && dictionary.TryGetValue(id, out param))
			{
				result = param.Name;
			}
			return result;
		}

		// Token: 0x06008A80 RID: 35456 RVA: 0x003A3F88 File Offset: 0x003A2388
		public string GetHSkillName(int id)
		{
			string result = "---------------";
			Dictionary<int, ItemData.Param> dictionary;
			ItemData.Param param;
			if (this.SkillTable.TryGetValue(17, out dictionary) && dictionary.TryGetValue(id, out param))
			{
				result = param.Name;
			}
			return result;
		}

		// Token: 0x06008A81 RID: 35457 RVA: 0x003A3FC8 File Offset: 0x003A23C8
		public void Start()
		{
			this.LoadAgentLifeStyleInfoTable();
			this.LoadItemList_Skill();
			for (int i = 0; i < this.dictUploaded.Length; i++)
			{
				this.dictUploaded[i] = new Dictionary<string, int>();
			}
		}

		// Token: 0x040070BE RID: 28862
		public NetworkInfo.Profile profile = new NetworkInfo.Profile();

		// Token: 0x040070BF RID: 28863
		public Dictionary<int, NetworkInfo.UserInfo> dictUserInfo = new Dictionary<int, NetworkInfo.UserInfo>();

		// Token: 0x040070C0 RID: 28864
		public List<NetworkInfo.CharaInfo> lstCharaInfo = new List<NetworkInfo.CharaInfo>();

		// Token: 0x040070C1 RID: 28865
		public List<NetworkInfo.HousingInfo> lstHousingInfo = new List<NetworkInfo.HousingInfo>();

		// Token: 0x040070C2 RID: 28866
		[SerializeField]
		public Net_PopupMsg popupMsg;

		// Token: 0x040070C3 RID: 28867
		[SerializeField]
		public LogView logview;

		// Token: 0x040070C4 RID: 28868
		[SerializeField]
		public Net_PopupCheck popupCheck;

		// Token: 0x040070C5 RID: 28869
		[SerializeField]
		public NetCacheControl cacheCtrl;

		// Token: 0x040070C6 RID: 28870
		[SerializeField]
		public NetSelectHNScrollController netSelectHN;

		// Token: 0x040070C7 RID: 28871
		[SerializeField]
		public CustomCharaWindow selectCharaFWindow;

		// Token: 0x040070C8 RID: 28872
		[SerializeField]
		public CustomCharaWindow selectCharaMWindow;

		// Token: 0x040070C9 RID: 28873
		[SerializeField]
		public HousingLoadWindow selectHousingWindow;

		// Token: 0x040070CA RID: 28874
		[SerializeField]
		private GameObject objBlockUI;

		// Token: 0x040070CB RID: 28875
		[HideInInspector]
		public bool changeCharaList;

		// Token: 0x040070CC RID: 28876
		[HideInInspector]
		public bool changeHosingList;

		// Token: 0x040070CD RID: 28877
		[HideInInspector]
		public Version newestVersion = new Version(0, 0, 0);

		// Token: 0x040070CE RID: 28878
		[HideInInspector]
		public bool updateProfile;

		// Token: 0x040070CF RID: 28879
		[HideInInspector]
		public bool updateVersion;

		// Token: 0x040070D0 RID: 28880
		public Dictionary<string, int>[] dictUploaded = new Dictionary<string, int>[Enum.GetNames(typeof(DataType)).Length];

		// Token: 0x040070D1 RID: 28881
		public bool noUserControl;

		// Token: 0x040070D3 RID: 28883
		public IReadOnlyDictionary<int, Dictionary<int, ItemData.Param>> SkillTable;

		// Token: 0x0200101D RID: 4125
		public class SelectHNInfo
		{
			// Token: 0x040070D4 RID: 28884
			public int userIdx = -1;

			// Token: 0x040070D5 RID: 28885
			public string handlename = string.Empty;

			// Token: 0x040070D6 RID: 28886
			public string drawname = string.Empty;
		}

		// Token: 0x0200101E RID: 4126
		public class Profile
		{
			// Token: 0x040070D7 RID: 28887
			public int userIdx = -1;
		}

		// Token: 0x0200101F RID: 4127
		public class UserInfo
		{
			// Token: 0x040070D8 RID: 28888
			public string handleName = string.Empty;
		}

		// Token: 0x02001020 RID: 4128
		public class BaseIndex
		{
			// Token: 0x040070D9 RID: 28889
			public int idx = -1;

			// Token: 0x040070DA RID: 28890
			public string data_uid = string.Empty;

			// Token: 0x040070DB RID: 28891
			public int update_idx;

			// Token: 0x040070DC RID: 28892
			public int user_idx = -1;

			// Token: 0x040070DD RID: 28893
			public string name = string.Empty;

			// Token: 0x040070DE RID: 28894
			public string comment = string.Empty;

			// Token: 0x040070DF RID: 28895
			public DateTime updateTime = default(DateTime);

			// Token: 0x040070E0 RID: 28896
			public int dlCount;

			// Token: 0x040070E1 RID: 28897
			public int weekCount;

			// Token: 0x040070E2 RID: 28898
			public int applause;

			// Token: 0x040070E3 RID: 28899
			public int rankingT = 999999;

			// Token: 0x040070E4 RID: 28900
			public int rankingW = 999999;

			// Token: 0x040070E5 RID: 28901
			public DateTime createTime = default(DateTime);
		}

		// Token: 0x02001021 RID: 4129
		public class CharaInfo : NetworkInfo.BaseIndex
		{
			// Token: 0x040070E6 RID: 28902
			public int type;

			// Token: 0x040070E7 RID: 28903
			public int birthmonth = 1;

			// Token: 0x040070E8 RID: 28904
			public int birthday = 1;

			// Token: 0x040070E9 RID: 28905
			public string strBirthDay = string.Empty;

			// Token: 0x040070EA RID: 28906
			public int sex;

			// Token: 0x040070EB RID: 28907
			public int height = 1;

			// Token: 0x040070EC RID: 28908
			public int bust = 1;

			// Token: 0x040070ED RID: 28909
			public int hair;

			// Token: 0x040070EE RID: 28910
			public int phase;

			// Token: 0x040070EF RID: 28911
			public int lifestyle = -1;

			// Token: 0x040070F0 RID: 28912
			public int pheromone;

			// Token: 0x040070F1 RID: 28913
			public int reliability;

			// Token: 0x040070F2 RID: 28914
			public int reason;

			// Token: 0x040070F3 RID: 28915
			public int instinct;

			// Token: 0x040070F4 RID: 28916
			public int dirty;

			// Token: 0x040070F5 RID: 28917
			public int wariness;

			// Token: 0x040070F6 RID: 28918
			public int sociability;

			// Token: 0x040070F7 RID: 28919
			public int darkness;

			// Token: 0x040070F8 RID: 28920
			public int skill_n01 = -1;

			// Token: 0x040070F9 RID: 28921
			public int skill_n02 = -1;

			// Token: 0x040070FA RID: 28922
			public int skill_n03 = -1;

			// Token: 0x040070FB RID: 28923
			public int skill_n04 = -1;

			// Token: 0x040070FC RID: 28924
			public int skill_n05 = -1;

			// Token: 0x040070FD RID: 28925
			public int skill_h01 = -1;

			// Token: 0x040070FE RID: 28926
			public int skill_h02 = -1;

			// Token: 0x040070FF RID: 28927
			public int skill_h03 = -1;

			// Token: 0x04007100 RID: 28928
			public int skill_h04 = -1;

			// Token: 0x04007101 RID: 28929
			public int skill_h05 = -1;

			// Token: 0x04007102 RID: 28930
			public int wish_01 = -1;

			// Token: 0x04007103 RID: 28931
			public int wish_02 = -1;

			// Token: 0x04007104 RID: 28932
			public int wish_03 = -1;

			// Token: 0x04007105 RID: 28933
			public int registration;
		}

		// Token: 0x02001022 RID: 4130
		public class HousingInfo : NetworkInfo.BaseIndex
		{
			// Token: 0x04007106 RID: 28934
			public int mapSize;
		}
	}
}
