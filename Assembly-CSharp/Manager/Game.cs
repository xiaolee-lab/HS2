using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using ADV;
using AIChara;
using AIProject;
using AIProject.Definitions;
using AIProject.SaveData;
using AIProject.Scene;
using ConfigScene;
using UniRx;
using UnityEngine;
using UnityEx;

namespace Manager
{
	// Token: 0x020008DE RID: 2270
	public sealed class Game : Singleton<Game>
	{
		// Token: 0x06003C41 RID: 15425 RVA: 0x00161F82 File Offset: 0x00160382
		private Game()
		{
			this.ReserveSceneName = string.Empty;
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x00161FB6 File Offset: 0x001603B6
		public void SetCustomSceneInfo(string prev, byte type, byte sex, string fileName)
		{
			this.customSceneInfo.previous = prev;
			this.customSceneInfo.type = type;
			this.customSceneInfo.sex = sex;
			this.customSceneInfo.fileName = fileName;
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x00161FEC File Offset: 0x001603EC
		public static bool isAdd01
		{
			get
			{
				bool? isAdd = Game._isAdd01;
				bool value;
				if (isAdd != null)
				{
					value = isAdd.Value;
				}
				else
				{
					bool? flag = Game._isAdd01 = new bool?(AssetBundleCheck.IsManifest("add01"));
					value = flag.Value;
				}
				return value;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06003C44 RID: 15428 RVA: 0x00162034 File Offset: 0x00160434
		public static bool isAdd30
		{
			get
			{
				bool? isAdd = Game._isAdd30;
				bool value;
				if (isAdd != null)
				{
					value = isAdd.Value;
				}
				else
				{
					bool? flag = Game._isAdd30 = new bool?(AssetBundleCheck.IsManifest("add30"));
					value = flag.Value;
				}
				return value;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x0016207C File Offset: 0x0016047C
		public static bool isAdd50
		{
			get
			{
				bool? isAdd = Game._isAdd50;
				bool value;
				if (isAdd != null)
				{
					value = isAdd.Value;
				}
				else
				{
					bool? flag = Game._isAdd50 = new bool?(AssetBundleCheck.IsManifest("add50"));
					value = flag.Value;
				}
				return value;
			}
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x001620C4 File Offset: 0x001604C4
		public static bool IsRestrictedOver50(string filepath)
		{
			return Game.IsRestrictedOver50(filepath, 50);
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x001620D0 File Offset: 0x001604D0
		public static bool IsRestrictedOver50(string filepath, int numBundle)
		{
			string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(filepath);
			int num;
			return !int.TryParse(fileNameWithoutExtension, out num) || (num >= numBundle && !Game.isAdd50);
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x00162107 File Offset: 0x00160507
		// (set) Token: 0x06003C49 RID: 15433 RVA: 0x0016210F File Offset: 0x0016050F
		public bool IsDebug { get; set; }

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06003C4A RID: 15434 RVA: 0x00162118 File Offset: 0x00160518
		// (set) Token: 0x06003C4B RID: 15435 RVA: 0x00162120 File Offset: 0x00160520
		public byte UploaderType { get; set; }

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06003C4C RID: 15436 RVA: 0x00162129 File Offset: 0x00160529
		// (set) Token: 0x06003C4D RID: 15437 RVA: 0x00162131 File Offset: 0x00160531
		public string ReserveSceneName { get; set; }

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06003C4E RID: 15438 RVA: 0x0016213A File Offset: 0x0016053A
		// (set) Token: 0x06003C4F RID: 15439 RVA: 0x00162142 File Offset: 0x00160542
		public GlobalSaveData GlobalData { get; private set; }

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06003C50 RID: 15440 RVA: 0x0016214B File Offset: 0x0016054B
		// (set) Token: 0x06003C51 RID: 15441 RVA: 0x00162153 File Offset: 0x00160553
		public SaveData Data { get; private set; }

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06003C52 RID: 15442 RVA: 0x0016215C File Offset: 0x0016055C
		// (set) Token: 0x06003C53 RID: 15443 RVA: 0x00162164 File Offset: 0x00160564
		public WorldData WorldData { get; set; }

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06003C54 RID: 15444 RVA: 0x0016216D File Offset: 0x0016056D
		// (set) Token: 0x06003C55 RID: 15445 RVA: 0x00162175 File Offset: 0x00160575
		public bool IsAuto { get; set; }

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06003C56 RID: 15446 RVA: 0x0016217E File Offset: 0x0016057E
		public AIProject.SaveData.Environment Environment
		{
			[CompilerGenerated]
			get
			{
				WorldData worldData = this.WorldData;
				return (worldData != null) ? worldData.Environment : null;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06003C57 RID: 15447 RVA: 0x00162195 File Offset: 0x00160595
		// (set) Token: 0x06003C58 RID: 15448 RVA: 0x0016219C File Offset: 0x0016059C
		public static string PrevPlayerStateFromCharaCreate { get; set; }

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x001621A4 File Offset: 0x001605A4
		// (set) Token: 0x06003C5A RID: 15450 RVA: 0x001621AB File Offset: 0x001605AB
		public static int PrevAccessDeviceID { get; set; }

		// Token: 0x06003C5B RID: 15451 RVA: 0x001621B4 File Offset: 0x001605B4
		public bool ExistsBackup()
		{
			if (this._bakData == null || (this.WorldData == null && !this.IsAuto))
			{
				return false;
			}
			if (this.IsAuto)
			{
				return this._bakData.AutoData != null;
			}
			return this._bakData.WorldList.ContainsKey(this.WorldData.WorldID);
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x0016221C File Offset: 0x0016061C
		public bool ExistsBackup(int id)
		{
			if (this._bakData == null)
			{
				return false;
			}
			if (this.IsAuto)
			{
				return this._bakData.AutoData != null;
			}
			return this._bakData.WorldList.ContainsKey(id);
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x00162259 File Offset: 0x00160659
		// (set) Token: 0x06003C5E RID: 15454 RVA: 0x00162261 File Offset: 0x00160661
		public List<UnityEx.ValueTuple<string, string>> AssetBundlePaths { get; set; } = new List<UnityEx.ValueTuple<string, string>>();

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x0016226A File Offset: 0x0016066A
		// (set) Token: 0x06003C60 RID: 15456 RVA: 0x00162272 File Offset: 0x00160672
		public Dictionary<int, AssetBundleInfo> LoadingSpriteABList { get; private set; } = new Dictionary<int, AssetBundleInfo>();

		// Token: 0x06003C61 RID: 15457 RVA: 0x0016227C File Offset: 0x0016067C
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			string path = "scene/common/";
			string[] array = (from bundle in CommonLib.GetAssetBundleNameListFromPath(path, false)
			orderby bundle descending
			select bundle).ToArray<string>();
			foreach (string text in array)
			{
				string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
				string text2 = (!(fileNameWithoutExtension == "00")) ? string.Format("add{0}", fileNameWithoutExtension) : "abdata";
				GameObject x2 = null;
				foreach (GameObject gameObject in AssetBundleManager.LoadAllAsset(text, typeof(GameObject), text2).GetAllAssets<GameObject>())
				{
					if (gameObject.name == "resrcmanager")
					{
						x2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, base.transform);
						break;
					}
					this.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(text, text2));
				}
				if (x2 != null)
				{
					break;
				}
			}
			DefinePack definePack = Singleton<Resources>.Instance.DefinePack;
			List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ExpList, true);
			assetBundleNameListFromPath.Sort();
			using (List<string>.Enumerator enumerator = assetBundleNameListFromPath.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string file = enumerator.Current;
					foreach (ExcelData excelData in AssetBundleManager.LoadAllAsset(file, typeof(ExcelData), null).GetAllAssets<ExcelData>())
					{
						int key = int.Parse(excelData.name.Replace("c", string.Empty));
						Dictionary<string, Game.Expression> dic;
						if (!this.CharaExpTable.TryGetValue(key, out dic))
						{
							Dictionary<string, Game.Expression> dictionary = new Dictionary<string, Game.Expression>();
							this.CharaExpTable[key] = dictionary;
							dic = dictionary;
						}
						Game.LoadExpExcelData(dic, excelData);
						if (!this.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == file))
						{
							this.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(file, string.Empty));
						}
					}
				}
			}
			List<string> assetBundleNameListFromPath2 = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.LoadingSpriteList, false);
			assetBundleNameListFromPath2.Sort();
			using (List<string>.Enumerator enumerator2 = assetBundleNameListFromPath2.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					string file = enumerator2.Current;
					foreach (LoadingImageData loadingImageData in AssetBundleManager.LoadAllAsset(file, typeof(LoadingImageData), null).GetAllAssets<LoadingImageData>())
					{
						foreach (LoadingImageData.Param param in loadingImageData.param)
						{
							this.LoadingSpriteABList[param.ID] = new AssetBundleInfo(param.Name, param.Bundle, param.Asset, param.Manifest);
						}
						if (!this.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == file))
						{
							this.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(file, string.Empty));
						}
					}
				}
			}
			Observable.TimerFrame(2, FrameCountType.Update).Subscribe(delegate(long _)
			{
				foreach (UnityEx.ValueTuple<string, string> valueTuple in this.AssetBundlePaths)
				{
					AssetBundleManager.UnloadAssetBundle(valueTuple.Item1, true, valueTuple.Item2, false);
				}
			});
			TextScenario.LoadReadInfo();
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x00162688 File Offset: 0x00160A88
		private void OnApplicationQuit()
		{
			if (this.IsDebug)
			{
				return;
			}
			TextScenario.SaveReadInfo();
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06003C63 RID: 15459 RVA: 0x0016269B File Offset: 0x00160A9B
		// (set) Token: 0x06003C64 RID: 15460 RVA: 0x001626A3 File Offset: 0x00160AA3
		public ConfigWindow Config { get; set; }

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06003C65 RID: 15461 RVA: 0x001626AC File Offset: 0x00160AAC
		// (set) Token: 0x06003C66 RID: 15462 RVA: 0x001626B4 File Offset: 0x00160AB4
		public ConfirmScene Dialog { get; set; }

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06003C67 RID: 15463 RVA: 0x001626BD File Offset: 0x00160ABD
		// (set) Token: 0x06003C68 RID: 15464 RVA: 0x001626C5 File Offset: 0x00160AC5
		public ExitScene ExitScene { get; set; }

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06003C69 RID: 15465 RVA: 0x001626CE File Offset: 0x00160ACE
		// (set) Token: 0x06003C6A RID: 15466 RVA: 0x001626D6 File Offset: 0x00160AD6
		public MapShortcutUI MapShortcutUI { get; set; }

		// Token: 0x06003C6B RID: 15467 RVA: 0x001626E0 File Offset: 0x00160AE0
		public void LoadShortcut()
		{
			MapShortcutUI.ImageIndex = 0;
			MapShortcutUI.ClosedEvent = null;
			GameObject original = CommonLib.LoadAsset<GameObject>("scene/common/00.unity3d", Singleton<Resources>.Instance.DefinePack.SceneNames.MapShortcutScene, false, string.Empty);
			UnityEngine.Object.Instantiate<GameObject>(original, base.transform, false);
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x0016272C File Offset: 0x00160B2C
		public void LoadShortcut(int index, System.Action closedEvent = null)
		{
			MapShortcutUI.ImageIndex = index;
			MapShortcutUI.ClosedEvent = closedEvent;
			GameObject original = CommonLib.LoadAsset<GameObject>("scene/common/00.unity3d", Singleton<Resources>.Instance.DefinePack.SceneNames.MapShortcutScene, false, string.Empty);
			UnityEngine.Object.Instantiate<GameObject>(original, base.transform, false);
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x00162778 File Offset: 0x00160B78
		public void LoadConfig()
		{
			string text = (!Game.isAdd50) ? "scene/common/00.unity3d" : "scene/common/50.unity3d";
			string text2 = (!Game.isAdd50) ? string.Empty : "add50";
			string assetBundleName = text;
			string configScene = Singleton<Resources>.Instance.DefinePack.SceneNames.ConfigScene;
			string manifestName = text2;
			GameObject original = CommonLib.LoadAsset<GameObject>(assetBundleName, configScene, false, manifestName);
			UnityEngine.Object.Instantiate<GameObject>(original, base.transform, false);
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x001627F0 File Offset: 0x00160BF0
		public void LoadDialog()
		{
			GameObject original = CommonLib.LoadAsset<GameObject>("scene/common/00.unity3d", Singleton<Resources>.Instance.DefinePack.SceneNames.DialogScene, false, string.Empty);
			UnityEngine.Object.Instantiate<GameObject>(original, base.transform, false);
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x00162830 File Offset: 0x00160C30
		public void DestroyDialog()
		{
			UnityEngine.Object.Destroy(this.Dialog.gameObject);
			this.Dialog = null;
			GC.Collect();
			Resources.UnloadUnusedAssets();
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x00162854 File Offset: 0x00160C54
		public void LoadExit()
		{
			GameObject original = CommonLib.LoadAsset<GameObject>("scene/common/00.unity3d", Singleton<Resources>.Instance.DefinePack.SceneNames.ExitScene, false, string.Empty);
			UnityEngine.Object.Instantiate<GameObject>(original, base.transform, false);
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x00162894 File Offset: 0x00160C94
		public void SaveGlobalData()
		{
			if (this.GlobalData == null)
			{
				return;
			}
			string globalSaveDataFile = AIProject.Definitions.Path.GlobalSaveDataFile;
			if (!Directory.Exists(AIProject.Definitions.Path.SaveDataDirectory))
			{
				Directory.CreateDirectory(AIProject.Definitions.Path.SaveDataDirectory);
			}
			this.GlobalData.SaveFile(globalSaveDataFile);
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x001628DC File Offset: 0x00160CDC
		public void LoadGlobalData()
		{
			string globalSaveDataFile = AIProject.Definitions.Path.GlobalSaveDataFile;
			this.GlobalData = GlobalSaveData.LoadFile(globalSaveDataFile);
			if (this.GlobalData == null)
			{
				this.GlobalData = new GlobalSaveData();
				this.SaveGlobalData();
			}
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x00162918 File Offset: 0x00160D18
		public void SaveProfile(string path)
		{
			if (!Directory.Exists(AIProject.Definitions.Path.SaveDataDirectory))
			{
				Directory.CreateDirectory(AIProject.Definitions.Path.SaveDataDirectory);
			}
			this.Data.SaveFile(path);
			if (this._bakData == null)
			{
				this._bakData = new SaveData();
				this._bakData.Copy(this.Data);
				return;
			}
			path += ".bak";
			this._bakData.SaveFile(path);
			this._bakData.Copy(this.Data);
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x001629A0 File Offset: 0x00160DA0
		public void LoadProfile(string path)
		{
			this.Data = SaveData.LoadFile(path);
			if (this.Data == null)
			{
				this.Data = new SaveData();
			}
			else
			{
				this._bakData = new SaveData();
				this._bakData.Copy(this.Data);
			}
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x001629F0 File Offset: 0x00160DF0
		public static WorldData CreateInitData(int id, bool cleared = false)
		{
			WorldData worldData = new WorldData();
			worldData.WorldID = id;
			AIProject.SaveData.Environment environment = worldData.Environment;
			environment.Time = new AIProject.SaveData.Environment.SerializableDateTime(1, 1, 1, 10, 0, 0);
			environment.Weather = Weather.Cloud1;
			environment.Temperature = Temperature.Normal;
			if (cleared)
			{
				environment.TutorialProgress = 29;
				Dictionary<int, bool> dictionary;
				if (!environment.BasePointOpenState.TryGetValue(0, out dictionary))
				{
					Dictionary<int, bool> dictionary2 = new Dictionary<int, bool>();
					environment.BasePointOpenState[0] = dictionary2;
					dictionary = dictionary2;
				}
				dictionary[-1] = true;
			}
			worldData.PlayerData.InventorySlotMax = Singleton<Resources>.Instance.PlayerProfile.DefaultInventoryMax;
			int agentMax = Singleton<Resources>.Instance.DefinePack.MapDefines.AgentMax;
			int agentDefaultNum = Singleton<Resources>.Instance.DefinePack.MapDefines.AgentDefaultNum;
			for (int i = 0; i < agentMax; i++)
			{
				AgentData agentData = new AgentData();
				worldData.AgentTable[i] = agentData;
				AgentData agentData2 = agentData;
				agentData2.OpenState = (i < 1);
				agentData2.PlayEnterScene = (i < 1);
			}
			return worldData;
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x00162AFF File Offset: 0x00160EFF
		public static bool IsFirstGame
		{
			get
			{
				return Singleton<Game>.IsInstance() && !Singleton<Game>.Instance.ExistsBackup();
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06003C77 RID: 15479 RVA: 0x00162B1C File Offset: 0x00160F1C
		public static bool IsFreeMode
		{
			get
			{
				if (!Singleton<Game>.IsInstance())
				{
					return false;
				}
				Game instance = Singleton<Game>.Instance;
				return instance.WorldData != null && instance.WorldData.FreeMode;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x00162B53 File Offset: 0x00160F53
		// (set) Token: 0x06003C79 RID: 15481 RVA: 0x00162B5B File Offset: 0x00160F5B
		public Dictionary<int, Dictionary<string, Game.Expression>> CharaExpTable { get; private set; } = new Dictionary<int, Dictionary<string, Game.Expression>>();

		// Token: 0x06003C7A RID: 15482 RVA: 0x00162B64 File Offset: 0x00160F64
		public static void LoadExpExcelData(Dictionary<string, Game.Expression> dic, ExcelData excelData)
		{
			foreach (ExcelData.Param param in excelData.list)
			{
				if (!param.list.IsNullOrEmpty<string>())
				{
					Game.Expression value = new Game.Expression(param.list.Skip(1).ToArray<string>())
					{
						IsChangeSkip = true
					};
					dic[param.list[0]] = value;
				}
			}
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x00162C04 File Offset: 0x00161004
		public static Game.Expression GetExpression(Dictionary<string, Game.Expression> dic, string key)
		{
			Game.Expression result;
			dic.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x00162C1C File Offset: 0x0016101C
		public Game.Expression GetExpression(int personality, string key)
		{
			Dictionary<string, Game.Expression> dic;
			if (!this.CharaExpTable.TryGetValue(personality, out dic))
			{
				return null;
			}
			return Game.GetExpression(dic, key);
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x00162C48 File Offset: 0x00161048
		public void RemoveWorldData(int id)
		{
			bool flag = false;
			if (this.Data != null && !this.Data.WorldList.IsNullOrEmpty<int, WorldData>() && !this.Data.WorldList.IsNullOrEmpty<int, WorldData>())
			{
				flag |= this.Data.WorldList.Remove(id);
			}
			if (this._bakData != null && !this._bakData.WorldList.IsNullOrEmpty<int, WorldData>())
			{
				flag |= this._bakData.WorldList.Remove(id);
			}
			if (flag)
			{
				this.SaveProfile(AIProject.Definitions.Path.WorldSaveDataFile);
			}
		}

		// Token: 0x04003AA8 RID: 15016
		public static readonly System.Version Version = new System.Version(1, 0);

		// Token: 0x04003AA9 RID: 15017
		public Game.CustomSceneInfo customSceneInfo;

		// Token: 0x04003AAA RID: 15018
		private static bool? _isAdd01 = null;

		// Token: 0x04003AAB RID: 15019
		private static bool? _isAdd30 = null;

		// Token: 0x04003AAC RID: 15020
		private static bool? _isAdd50 = null;

		// Token: 0x04003AB2 RID: 15026
		private SaveData _bakData;

		// Token: 0x020008DF RID: 2271
		public class Expression
		{
			// Token: 0x06003C81 RID: 15489 RVA: 0x000F879A File Offset: 0x000F6B9A
			public Expression()
			{
			}

			// Token: 0x06003C82 RID: 15490 RVA: 0x000F87D1 File Offset: 0x000F6BD1
			public Expression(Game.Expression other)
			{
				Game.Expression.Copy(other, this);
			}

			// Token: 0x06003C83 RID: 15491 RVA: 0x000F880F File Offset: 0x000F6C0F
			public Expression(string[] args, ref int index)
			{
				this.Initialize(args, ref index, false);
			}

			// Token: 0x06003C84 RID: 15492 RVA: 0x000F8850 File Offset: 0x000F6C50
			public Expression(string[] args)
			{
				int num = 0;
				this.Initialize(args, ref num, false);
			}

			// Token: 0x17000B0F RID: 2831
			// (get) Token: 0x06003C85 RID: 15493 RVA: 0x000F889E File Offset: 0x000F6C9E
			// (set) Token: 0x06003C86 RID: 15494 RVA: 0x000F88A6 File Offset: 0x000F6CA6
			public bool IsChangeSkip { private get; set; }

			// Token: 0x17000B10 RID: 2832
			// (get) Token: 0x06003C87 RID: 15495 RVA: 0x000F88AF File Offset: 0x000F6CAF
			// (set) Token: 0x06003C88 RID: 15496 RVA: 0x000F88B7 File Offset: 0x000F6CB7
			public Game.Expression.Pattern Eyebrow { get; private set; }

			// Token: 0x17000B11 RID: 2833
			// (get) Token: 0x06003C89 RID: 15497 RVA: 0x000F88C0 File Offset: 0x000F6CC0
			// (set) Token: 0x06003C8A RID: 15498 RVA: 0x000F88C8 File Offset: 0x000F6CC8
			public Game.Expression.Pattern Eyes { get; private set; }

			// Token: 0x17000B12 RID: 2834
			// (get) Token: 0x06003C8B RID: 15499 RVA: 0x000F88D1 File Offset: 0x000F6CD1
			// (set) Token: 0x06003C8C RID: 15500 RVA: 0x000F88D9 File Offset: 0x000F6CD9
			public Game.Expression.Pattern Mouth { get; private set; }

			// Token: 0x17000B13 RID: 2835
			// (get) Token: 0x06003C8D RID: 15501 RVA: 0x000F88E2 File Offset: 0x000F6CE2
			// (set) Token: 0x06003C8E RID: 15502 RVA: 0x000F88EA File Offset: 0x000F6CEA
			public float EyebrowOpen { get; private set; } = 1f;

			// Token: 0x17000B14 RID: 2836
			// (get) Token: 0x06003C8F RID: 15503 RVA: 0x000F88F3 File Offset: 0x000F6CF3
			// (set) Token: 0x06003C90 RID: 15504 RVA: 0x000F88FB File Offset: 0x000F6CFB
			public float EyesOpen { get; private set; } = 1f;

			// Token: 0x17000B15 RID: 2837
			// (get) Token: 0x06003C91 RID: 15505 RVA: 0x000F8904 File Offset: 0x000F6D04
			// (set) Token: 0x06003C92 RID: 15506 RVA: 0x000F890C File Offset: 0x000F6D0C
			public float MouthOpen { get; private set; } = 1f;

			// Token: 0x17000B16 RID: 2838
			// (get) Token: 0x06003C93 RID: 15507 RVA: 0x000F8915 File Offset: 0x000F6D15
			// (set) Token: 0x06003C94 RID: 15508 RVA: 0x000F891D File Offset: 0x000F6D1D
			public int EyesLook { get; private set; }

			// Token: 0x17000B17 RID: 2839
			// (get) Token: 0x06003C95 RID: 15509 RVA: 0x000F8926 File Offset: 0x000F6D26
			// (set) Token: 0x06003C96 RID: 15510 RVA: 0x000F892E File Offset: 0x000F6D2E
			public float HohoAkaRate { get; private set; }

			// Token: 0x17000B18 RID: 2840
			// (get) Token: 0x06003C97 RID: 15511 RVA: 0x000F8937 File Offset: 0x000F6D37
			// (set) Token: 0x06003C98 RID: 15512 RVA: 0x000F893F File Offset: 0x000F6D3F
			public bool IsHighlight { get; private set; } = true;

			// Token: 0x17000B19 RID: 2841
			// (get) Token: 0x06003C99 RID: 15513 RVA: 0x000F8948 File Offset: 0x000F6D48
			// (set) Token: 0x06003C9A RID: 15514 RVA: 0x000F8950 File Offset: 0x000F6D50
			public float TearsRate { get; private set; }

			// Token: 0x17000B1A RID: 2842
			// (get) Token: 0x06003C9B RID: 15515 RVA: 0x000F8959 File Offset: 0x000F6D59
			// (set) Token: 0x06003C9C RID: 15516 RVA: 0x000F8961 File Offset: 0x000F6D61
			public bool IsBlink { get; private set; } = true;

			// Token: 0x06003C9D RID: 15517 RVA: 0x000F896C File Offset: 0x000F6D6C
			public virtual void Initialize(string[] args, ref int index, bool isThrow = false)
			{
				try
				{
					string element = args.GetElement(index++);
					this._useEyebrow = !element.IsNullOrEmpty();
					if (this._useEyebrow)
					{
						this.Eyebrow = new Game.Expression.Pattern(element, true);
					}
					string element2 = args.GetElement(index++);
					this._useEyes = !element2.IsNullOrEmpty();
					if (this._useEyes)
					{
						this.Eyes = new Game.Expression.Pattern(element2, true);
					}
					string element3 = args.GetElement(index++);
					this._useMouth = !element3.IsNullOrEmpty();
					if (this._useMouth)
					{
						this.Mouth = new Game.Expression.Pattern(element3, true);
					}
					string element4 = args.GetElement(index++);
					this._useEyebrowOpen = !element4.IsNullOrEmpty();
					if (this._useEyebrowOpen)
					{
						this.EyebrowOpen = float.Parse(element4);
					}
					string element5 = args.GetElement(index++);
					this._useEyesOpen = !element5.IsNullOrEmpty();
					if (this._useEyesOpen)
					{
						this.EyesOpen = float.Parse(element5);
					}
					string element6 = args.GetElement(index++);
					this._useMouthOpen = !element6.IsNullOrEmpty();
					if (this._useMouthOpen)
					{
						this.MouthOpen = float.Parse(element6);
					}
					string element7 = args.GetElement(index++);
					this._useEyesLook = !element7.IsNullOrEmpty();
					if (this._useEyesLook)
					{
						this.EyesLook = int.Parse(element7);
					}
					string element8 = args.GetElement(index++);
					this._useHohoAkaRate = !element8.IsNullOrEmpty();
					if (this._useHohoAkaRate)
					{
						this.HohoAkaRate = float.Parse(element8);
					}
					string element9 = args.GetElement(index++);
					this._useHighlight = !element9.IsNullOrEmpty();
					if (this._useHighlight)
					{
						this.IsHighlight = bool.Parse(element9);
					}
					string element10 = args.GetElement(index++);
					this._useTearsLv = !element10.IsNullOrEmpty();
					if (this._useTearsLv)
					{
						this.TearsRate = float.Parse(element10);
					}
					string element11 = args.GetElement(index++);
					this._useBlink = !element11.IsNullOrEmpty();
					if (this._useBlink)
					{
						this.IsBlink = bool.Parse(element11);
					}
				}
				catch (Exception)
				{
					if (isThrow)
					{
						throw new Exception(string.Format("Expression:{0}", string.Join(",", args)));
					}
				}
			}

			// Token: 0x06003C9E RID: 15518 RVA: 0x000F8C30 File Offset: 0x000F7030
			public static void Copy(Game.Expression source, Game.Expression destination)
			{
				destination.Eyebrow = ((((source != null) ? source.Eyebrow.Clone() : null) as Game.Expression.Pattern) ?? new Game.Expression.Pattern());
				Game.Expression.Pattern eyes = source.Eyes;
				destination.Eyes = ((((eyes != null) ? eyes.Clone() : null) as Game.Expression.Pattern) ?? new Game.Expression.Pattern());
				Game.Expression.Pattern mouth = source.Mouth;
				destination.Mouth = ((((mouth != null) ? mouth.Clone() : null) as Game.Expression.Pattern) ?? new Game.Expression.Pattern());
				destination.EyebrowOpen = source.EyebrowOpen;
				destination.EyesOpen = source.EyesOpen;
				destination.MouthOpen = source.MouthOpen;
				destination.EyesLook = source.EyesLook;
				destination.HohoAkaRate = source.HohoAkaRate;
				destination.TearsRate = source.TearsRate;
				destination.IsBlink = source.IsBlink;
				destination.IsChangeSkip = source.IsChangeSkip;
				destination._useEyebrow = source._useEyebrow;
				destination._useEyes = source._useEyes;
				destination._useMouth = source._useMouth;
				destination._useEyebrowOpen = source._useEyebrowOpen;
				destination._useEyesOpen = source._useEyesOpen;
				destination._useMouthOpen = source._useMouthOpen;
				destination._useEyesLook = source._useEyesLook;
				destination._useHohoAkaRate = source._useHohoAkaRate;
				destination._useTearsLv = source._useTearsLv;
				destination._useBlink = source._useBlink;
			}

			// Token: 0x06003C9F RID: 15519 RVA: 0x000F8D98 File Offset: 0x000F7198
			public void Copy(Game.Expression destination)
			{
				Game.Expression.Copy(this, destination);
			}

			// Token: 0x06003CA0 RID: 15520 RVA: 0x000F8DA4 File Offset: 0x000F71A4
			public void Change(ChaControl charInfo)
			{
				bool isChangeSkip = this.IsChangeSkip;
				if (!isChangeSkip || this._useEyebrow)
				{
					charInfo.ChangeEyebrowPtn(this.Eyebrow.Ptn, this.Eyebrow.Blend);
				}
				if (!isChangeSkip || this._useEyes)
				{
					charInfo.ChangeEyesPtn(this.Eyes.Ptn, this.Eyes.Blend);
				}
				if (!isChangeSkip || this._useMouth)
				{
					charInfo.ChangeMouthPtn(this.Mouth.Ptn, this.Mouth.Blend);
				}
				if (!isChangeSkip || this._useEyebrowOpen)
				{
					charInfo.ChangeEyebrowOpenMax(this.EyebrowOpen);
				}
				if (!isChangeSkip || this._useEyesOpen)
				{
					charInfo.ChangeEyesOpenMax(this.EyesOpen);
				}
				if (!isChangeSkip || this._useMouthOpen)
				{
					charInfo.ChangeMouthOpenMax(this.MouthOpen);
				}
				if ((!isChangeSkip || this._useEyesLook) && this.EyesLook != -1)
				{
					charInfo.ChangeLookEyesPtn(this.EyesLook);
				}
				if (!isChangeSkip || this._useHohoAkaRate)
				{
					charInfo.ChangeHohoAkaRate(this.HohoAkaRate);
				}
				if (!isChangeSkip || this._useHighlight)
				{
					charInfo.HideEyeHighlight(!this.IsHighlight);
				}
				if (!isChangeSkip || this._useTearsLv)
				{
					charInfo.ChangeTearsRate(this.TearsRate);
				}
				if (!this.IsChangeSkip || this._useBlink)
				{
					charInfo.ChangeEyesBlinkFlag(this._useBlink);
				}
			}

			// Token: 0x04003ACB RID: 15051
			protected bool _useEyebrow;

			// Token: 0x04003ACC RID: 15052
			protected bool _useEyes;

			// Token: 0x04003ACD RID: 15053
			protected bool _useMouth;

			// Token: 0x04003ACE RID: 15054
			protected bool _useEyebrowOpen;

			// Token: 0x04003ACF RID: 15055
			protected bool _useEyesOpen;

			// Token: 0x04003AD0 RID: 15056
			protected bool _useMouthOpen;

			// Token: 0x04003AD1 RID: 15057
			protected bool _useEyesLook;

			// Token: 0x04003AD2 RID: 15058
			protected bool _useHohoAkaRate;

			// Token: 0x04003AD3 RID: 15059
			protected bool _useHighlight;

			// Token: 0x04003AD4 RID: 15060
			protected bool _useTearsLv;

			// Token: 0x04003AD5 RID: 15061
			protected bool _useBlink;

			// Token: 0x020008E0 RID: 2272
			public class Pattern : ICloneable
			{
				// Token: 0x06003CA1 RID: 15521 RVA: 0x000F8F3B File Offset: 0x000F733B
				public Pattern()
				{
					this.Initialize();
				}

				// Token: 0x06003CA2 RID: 15522 RVA: 0x000F8F4C File Offset: 0x000F734C
				public Pattern(string arg, bool isThrow = false)
				{
					this.Initialize();
					if (arg.IsNullOrEmpty())
					{
						return;
					}
					string[] array = arg.Split(new char[]
					{
						','
					});
					int num = 0;
					try
					{
						string element = array.GetElement(num++);
						if (element != null)
						{
							this.Ptn = int.Parse(element);
						}
						element = array.GetElement(num++);
						if (element != null)
						{
							this.Blend = bool.Parse(element);
						}
					}
					catch (Exception)
					{
						if (isThrow)
						{
							throw new Exception(string.Format("Expression Pattern:{0}", string.Join(",", array)));
						}
					}
				}

				// Token: 0x17000B1B RID: 2843
				// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x000F9000 File Offset: 0x000F7400
				// (set) Token: 0x06003CA4 RID: 15524 RVA: 0x000F9008 File Offset: 0x000F7408
				public int Ptn { get; set; }

				// Token: 0x17000B1C RID: 2844
				// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x000F9011 File Offset: 0x000F7411
				// (set) Token: 0x06003CA6 RID: 15526 RVA: 0x000F9019 File Offset: 0x000F7419
				public bool Blend { get; set; }

				// Token: 0x06003CA7 RID: 15527 RVA: 0x000F9022 File Offset: 0x000F7422
				public void Initialize()
				{
					this.Ptn = 0;
					this.Blend = true;
				}

				// Token: 0x06003CA8 RID: 15528 RVA: 0x000F9032 File Offset: 0x000F7432
				public object Clone()
				{
					return base.MemberwiseClone();
				}
			}
		}

		// Token: 0x020008E1 RID: 2273
		public struct CustomSceneInfo
		{
			// Token: 0x04003AD8 RID: 15064
			public string previous;

			// Token: 0x04003AD9 RID: 15065
			public byte type;

			// Token: 0x04003ADA RID: 15066
			public byte sex;

			// Token: 0x04003ADB RID: 15067
			public bool isFemale;

			// Token: 0x04003ADC RID: 15068
			public string fileName;
		}
	}
}
