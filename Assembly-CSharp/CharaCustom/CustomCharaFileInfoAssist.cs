using System;
using System.Collections.Generic;
using System.IO;
using AIChara;
using AIProject.SaveData;
using Manager;

namespace CharaCustom
{
	// Token: 0x020009A5 RID: 2469
	public class CustomCharaFileInfoAssist
	{
		// Token: 0x06004709 RID: 18185 RVA: 0x001B5544 File Offset: 0x001B3944
		private static void AddList(List<CustomCharaFileInfo> _list, string path, byte sex, bool useMyData, bool useDownload, bool preset, bool _isFindSaveData, ref int idx)
		{
			string[] searchPattern = new string[]
			{
				"*.png"
			};
			List<string> list = new List<string>();
			if (_isFindSaveData && Singleton<Game>.Instance.Data != null)
			{
				WorldData autoData = Singleton<Game>.Instance.Data.AutoData;
				if (autoData != null)
				{
					list.Add(autoData.PlayerData.CharaFileName);
					foreach (KeyValuePair<int, AgentData> keyValuePair in autoData.AgentTable)
					{
						list.Add(keyValuePair.Value.CharaFileName);
					}
				}
				foreach (KeyValuePair<int, WorldData> keyValuePair2 in Singleton<Game>.Instance.Data.WorldList)
				{
					list.Add(keyValuePair2.Value.PlayerData.CharaFileName);
					foreach (KeyValuePair<int, AgentData> keyValuePair3 in keyValuePair2.Value.AgentTable)
					{
						list.Add(keyValuePair3.Value.CharaFileName);
					}
				}
			}
			string userUUID = Singleton<GameSystem>.Instance.UserUUID;
			CharaCategoryKind charaCategoryKind = (sex != 0) ? CharaCategoryKind.Female : CharaCategoryKind.Male;
			if (preset)
			{
				charaCategoryKind |= CharaCategoryKind.Preset;
			}
			FolderAssist folderAssist = new FolderAssist();
			folderAssist.CreateFolderInfoEx(path, searchPattern, true);
			int fileCount = folderAssist.GetFileCount();
			for (int i = 0; i < fileCount; i++)
			{
				ChaFileControl chaFileControl = new ChaFileControl();
				if (!chaFileControl.LoadCharaFile(folderAssist.lstFile[i].FullPath, 255, false, true))
				{
					int lastErrorCode = chaFileControl.GetLastErrorCode();
				}
				else if (chaFileControl.parameter.sex == sex)
				{
					CharaCategoryKind charaCategoryKind2 = (CharaCategoryKind)0;
					if (!preset)
					{
						if (userUUID == chaFileControl.userID)
						{
							if (!useMyData)
							{
								goto IL_675;
							}
							charaCategoryKind2 = CharaCategoryKind.MyData;
						}
						else
						{
							if (!useDownload)
							{
								goto IL_675;
							}
							charaCategoryKind2 = CharaCategoryKind.Download;
						}
					}
					string personality = string.Empty;
					if (sex != 0)
					{
						VoiceInfo.Param param;
						if (!Singleton<Voice>.Instance.voiceInfoDic.TryGetValue(chaFileControl.parameter.personality, out param))
						{
							personality = "不明";
						}
						else
						{
							personality = param.Personality;
						}
					}
					else
					{
						personality = string.Empty;
					}
					_list.Add(new CustomCharaFileInfo
					{
						index = idx++,
						name = chaFileControl.parameter.fullname,
						personality = personality,
						type = chaFileControl.parameter.personality,
						height = chaFileControl.custom.GetHeightKind(),
						bustSize = chaFileControl.custom.GetBustSizeKind(),
						hair = chaFileControl.custom.hair.kind,
						birthMonth = (int)chaFileControl.parameter.birthMonth,
						birthDay = (int)chaFileControl.parameter.birthDay,
						strBirthDay = chaFileControl.parameter.strBirthDay,
						lifestyle = chaFileControl.gameinfo.lifestyle,
						pheromone = chaFileControl.gameinfo.flavorState[0],
						reliability = chaFileControl.gameinfo.flavorState[1],
						reason = chaFileControl.gameinfo.flavorState[2],
						instinct = chaFileControl.gameinfo.flavorState[3],
						dirty = chaFileControl.gameinfo.flavorState[4],
						wariness = chaFileControl.gameinfo.flavorState[5],
						darkness = chaFileControl.gameinfo.flavorState[6],
						sociability = chaFileControl.gameinfo.flavorState[7],
						skill_n01 = chaFileControl.gameinfo.normalSkill[0],
						skill_n02 = chaFileControl.gameinfo.normalSkill[1],
						skill_n03 = chaFileControl.gameinfo.normalSkill[2],
						skill_n04 = chaFileControl.gameinfo.normalSkill[3],
						skill_n05 = chaFileControl.gameinfo.normalSkill[4],
						skill_h01 = chaFileControl.gameinfo.hSkill[0],
						skill_h02 = chaFileControl.gameinfo.hSkill[1],
						skill_h03 = chaFileControl.gameinfo.hSkill[2],
						skill_h04 = chaFileControl.gameinfo.hSkill[3],
						skill_h05 = chaFileControl.gameinfo.hSkill[4],
						wish_01 = chaFileControl.parameter.wish01,
						wish_02 = chaFileControl.parameter.wish02,
						wish_03 = chaFileControl.parameter.wish03,
						sex = (int)chaFileControl.parameter.sex,
						FullPath = folderAssist.lstFile[i].FullPath,
						FileName = folderAssist.lstFile[i].FileName,
						time = folderAssist.lstFile[i].time,
						gameRegistration = chaFileControl.gameinfo.gameRegistration,
						flavorState = new Dictionary<int, int>(chaFileControl.gameinfo.flavorState),
						phase = chaFileControl.gameinfo.phase,
						normalSkill = new Dictionary<int, int>(chaFileControl.gameinfo.normalSkill),
						hSkill = new Dictionary<int, int>(chaFileControl.gameinfo.hSkill),
						favoritePlace = chaFileControl.gameinfo.favoritePlace,
						futanari = chaFileControl.parameter.futanari,
						cateKind = (charaCategoryKind | charaCategoryKind2),
						data_uuid = chaFileControl.dataID,
						isInSaveData = list.Contains(Path.GetFileNameWithoutExtension(chaFileControl.charaFileName))
					});
				}
				IL_675:;
			}
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x001B5C00 File Offset: 0x001B4000
		public static List<CustomCharaFileInfo> CreateCharaFileInfoList(bool useMale, bool useFemale, bool useMyData = true, bool useDownload = true, bool usePreset = true, bool _isFindSaveData = true)
		{
			List<CustomCharaFileInfo> list = new List<CustomCharaFileInfo>();
			int num = 0;
			if (usePreset)
			{
				if (useMale)
				{
					string path = DefaultData.Path + "chara/male/";
					CustomCharaFileInfoAssist.AddList(list, path, 0, false, false, true, false, ref num);
				}
				if (useFemale)
				{
					string path2 = DefaultData.Path + "chara/female/";
					CustomCharaFileInfoAssist.AddList(list, path2, 1, false, false, true, false, ref num);
				}
			}
			if (useMyData || useDownload)
			{
				if (useMale)
				{
					string path3 = UserData.Path + "chara/male/";
					CustomCharaFileInfoAssist.AddList(list, path3, 0, useMyData, useDownload, false, _isFindSaveData, ref num);
				}
				if (useFemale)
				{
					string path4 = UserData.Path + "chara/female/";
					CustomCharaFileInfoAssist.AddList(list, path4, 1, useMyData, useDownload, false, _isFindSaveData, ref num);
				}
			}
			return list;
		}
	}
}
