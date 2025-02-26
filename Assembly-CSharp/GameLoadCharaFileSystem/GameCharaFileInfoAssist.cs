using System;
using System.Collections.Generic;
using System.IO;
using AIChara;
using AIProject.SaveData;
using AIProject.Scene;
using Manager;

namespace GameLoadCharaFileSystem
{
	// Token: 0x02000875 RID: 2165
	public class GameCharaFileInfoAssist
	{
		// Token: 0x06003722 RID: 14114 RVA: 0x00146414 File Offset: 0x00144814
		private static void AddList(List<GameCharaFileInfo> _list, string path, byte sex, bool useMyData, bool useDownload, bool preset, bool _isFindSaveData, bool firstEmpty, ref int idx)
		{
			string[] searchPattern = new string[]
			{
				"*.png"
			};
			HashSet<string> hashSet = new HashSet<string>();
			if (_isFindSaveData)
			{
				WorldData autoData = Singleton<Game>.Instance.Data.AutoData;
				if (autoData != null)
				{
					hashSet.Add(autoData.PlayerData.CharaFileName);
					foreach (KeyValuePair<int, AgentData> keyValuePair in autoData.AgentTable)
					{
						hashSet.Add(keyValuePair.Value.CharaFileName);
					}
				}
				foreach (KeyValuePair<int, WorldData> keyValuePair2 in Singleton<Game>.Instance.Data.WorldList)
				{
					if (Singleton<Game>.Instance.WorldData == null || !Singleton<MapScene>.IsInstance() || keyValuePair2.Value.WorldID != Singleton<Game>.Instance.WorldData.WorldID)
					{
						hashSet.Add(keyValuePair2.Value.PlayerData.CharaFileName);
						foreach (KeyValuePair<int, AgentData> keyValuePair3 in keyValuePair2.Value.AgentTable)
						{
							hashSet.Add(keyValuePair3.Value.CharaFileName);
						}
					}
				}
				if (Singleton<Game>.Instance.WorldData != null)
				{
					hashSet.Add(Singleton<Game>.Instance.WorldData.PlayerData.CharaFileName);
					foreach (KeyValuePair<int, AgentData> keyValuePair4 in Singleton<Game>.Instance.WorldData.AgentTable)
					{
						hashSet.Add(keyValuePair4.Value.CharaFileName);
					}
				}
			}
			string userUUID = Singleton<GameSystem>.Instance.UserUUID;
			CategoryKind categoryKind = (sex != 0) ? CategoryKind.Female : CategoryKind.Male;
			if (preset)
			{
				categoryKind |= CategoryKind.Preset;
			}
			FolderAssist folderAssist = new FolderAssist();
			folderAssist.CreateFolderInfoEx(path, searchPattern, true);
			int fileCount = folderAssist.GetFileCount();
			if (firstEmpty)
			{
				_list.Add(new GameCharaFileInfo
				{
					index = idx++,
					name = null,
					personality = "不明",
					voice = 0,
					hair = 0,
					birthMonth = 0,
					birthDay = 0,
					strBirthDay = string.Empty,
					sex = 1,
					FullPath = null,
					FileName = null,
					time = DateTime.MinValue,
					gameRegistration = false,
					flavorState = null,
					phase = 0,
					normalSkill = null,
					hSkill = null,
					favoritePlace = 0,
					futanari = false,
					lifeStyle = -1,
					cateKind = categoryKind,
					data_uuid = string.Empty,
					isInSaveData = false
				});
			}
			for (int i = 0; i < fileCount; i++)
			{
				ChaFileControl chaFileControl = new ChaFileControl();
				if (!chaFileControl.LoadCharaFile(folderAssist.lstFile[i].FullPath, 255, false, true))
				{
					int lastErrorCode = chaFileControl.GetLastErrorCode();
				}
				else if (chaFileControl.parameter.sex == sex)
				{
					CategoryKind categoryKind2 = (CategoryKind)0;
					if (!preset)
					{
						if (userUUID == chaFileControl.userID || chaFileControl.userID == "illusion-2019-1025-xxxx-aisyoujyocha")
						{
							if (!useMyData)
							{
								goto IL_608;
							}
							categoryKind2 = CategoryKind.MyData;
						}
						else
						{
							if (!useDownload)
							{
								goto IL_608;
							}
							categoryKind2 = CategoryKind.Download;
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
					_list.Add(new GameCharaFileInfo
					{
						index = idx++,
						name = chaFileControl.parameter.fullname,
						personality = personality,
						voice = chaFileControl.parameter.personality,
						hair = chaFileControl.custom.hair.kind,
						birthMonth = (int)chaFileControl.parameter.birthMonth,
						birthDay = (int)chaFileControl.parameter.birthDay,
						strBirthDay = chaFileControl.parameter.strBirthDay,
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
						lifeStyle = chaFileControl.gameinfo.lifestyle,
						cateKind = (categoryKind | categoryKind2),
						data_uuid = chaFileControl.dataID,
						isInSaveData = hashSet.Contains(Path.GetFileNameWithoutExtension(chaFileControl.charaFileName))
					});
				}
				IL_608:;
			}
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x00146A6C File Offset: 0x00144E6C
		public static List<GameCharaFileInfo> CreateCharaFileInfoList(bool useMale, bool useFemale, bool useMyData = true, bool useDownload = true, bool firstEmpty = false)
		{
			List<GameCharaFileInfo> list = new List<GameCharaFileInfo>();
			int num = 0;
			if (useMale)
			{
				string path = UserData.Path + "chara/male/";
				GameCharaFileInfoAssist.AddList(list, path, 0, useMyData, useDownload, false, false, firstEmpty, ref num);
			}
			if (useFemale)
			{
				string path2 = UserData.Path + "chara/female/";
				GameCharaFileInfoAssist.AddList(list, path2, 1, useMyData, useDownload, false, true, firstEmpty, ref num);
			}
			return list;
		}
	}
}
