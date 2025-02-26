using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Manager;
using UnityEngine;

namespace ADV
{
	// Token: 0x02000785 RID: 1925
	public static class Program
	{
		// Token: 0x06002D03 RID: 11523 RVA: 0x001014A7 File Offset: 0x000FF8A7
		public static string ScenarioBundle(string file)
		{
			return "scenario/" + file + ".unity3d";
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x001014BC File Offset: 0x000FF8BC
		public static string FindADVBundleFilePath(string path, string asset = null)
		{
			ScenarioData scenarioData;
			return Program.FindADVBundleFilePath(path, asset, out scenarioData);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x001014D4 File Offset: 0x000FF8D4
		public static string FindADVBundleFilePath(string path, string asset, out ScenarioData data)
		{
			data = null;
			string text = null;
			foreach (string text2 in from bundle in CommonLib.GetAssetBundleNameListFromPath(string.Format("adv/scenario/{0}/ ", path), true)
			orderby bundle descending
			select bundle)
			{
				if (asset == null)
				{
					text = text2;
				}
				else
				{
					foreach (ScenarioData scenarioData in AssetBundleManager.LoadAllAsset(text2, typeof(ScenarioData), null).GetAllAssets<ScenarioData>())
					{
						if (scenarioData.name == asset)
						{
							text = text2;
							data = scenarioData;
							break;
						}
					}
					AssetBundleManager.UnloadAssetBundle(text2, false, null, false);
				}
				if (text != null)
				{
					break;
				}
			}
			return text;
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x001015CC File Offset: 0x000FF9CC
		public static string FindADVBundleFilePath(int id, int category, string asset = null)
		{
			ScenarioData scenarioData;
			return Program.FindADVBundleFilePath(id, category, asset, out scenarioData);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x001015E4 File Offset: 0x000FF9E4
		public static string FindADVBundleFilePath(int id, int category, string asset, out ScenarioData data)
		{
			data = null;
			string text = null;
			string b = string.Format("{0:00}", category);
			foreach (string text2 in from bundle in CommonLib.GetAssetBundleNameListFromPath(string.Format("adv/scenario/c{0:00}/", id), true)
			orderby bundle descending
			select bundle)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text2);
				if (!(fileNameWithoutExtension != b))
				{
					if (asset == null)
					{
						text = text2;
					}
					else
					{
						foreach (ScenarioData scenarioData in AssetBundleManager.LoadAllAsset(text2, typeof(ScenarioData), null).GetAllAssets<ScenarioData>())
						{
							if (scenarioData.name == asset)
							{
								text = text2;
								data = scenarioData;
								break;
							}
						}
						AssetBundleManager.UnloadAssetBundle(text2, false, null, false);
					}
					if (text != null)
					{
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x00101710 File Offset: 0x000FFB10
		public static string FindMessageADVBundleFilePath(string path, string asset = null)
		{
			ScenarioData scenarioData;
			return Program.FindMessageADVBundleFilePath(path, asset, out scenarioData);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x00101728 File Offset: 0x000FFB28
		public static string FindMessageADVBundleFilePath(string path, string asset, out ScenarioData data)
		{
			data = null;
			string text = null;
			foreach (string text2 in from bundle in CommonLib.GetAssetBundleNameListFromPath(string.Format("adv/message/{0}/", path), true)
			orderby bundle descending
			select bundle)
			{
				if (asset == null)
				{
					text = text2;
				}
				else
				{
					foreach (ScenarioData scenarioData in AssetBundleManager.LoadAllAsset(text2, typeof(ScenarioData), null).GetAllAssets<ScenarioData>())
					{
						if (scenarioData.name == asset)
						{
							text = text2;
							data = scenarioData;
							break;
						}
					}
					AssetBundleManager.UnloadAssetBundle(text2, false, null, false);
				}
				if (text != null)
				{
					break;
				}
			}
			return text;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x00101820 File Offset: 0x000FFC20
		public static IEnumerator Open(IData scene, IData openData, Program.OpenDataProc proc = null)
		{
			if (scene != null)
			{
				yield break;
			}
			yield break;
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x0010183C File Offset: 0x000FFC3C
		public static bool isADVActionActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x0010184C File Offset: 0x000FFC4C
		public static bool isADVScene
		{
			get
			{
				return Singleton<Scene>.Instance.NowSceneNames.Contains("ADV");
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x00101862 File Offset: 0x000FFC62
		public static bool isADVProcessing
		{
			get
			{
				return Program.isADVActionActive || Program.isADVScene;
			}
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x00101878 File Offset: 0x000FFC78
		public static IEnumerator ADVProcessingCheck()
		{
			if (Program.isADVActionActive)
			{
				if (Program.<>f__mg$cache0 == null)
				{
					Program.<>f__mg$cache0 = new Func<bool>(Program.get_isADVActionActive);
				}
				yield return new WaitWhile(Program.<>f__mg$cache0);
			}
			else
			{
				if (Program.<>f__mg$cache1 == null)
				{
					Program.<>f__mg$cache1 = new Func<bool>(Program.get_isADVScene);
				}
				yield return new WaitWhile(Program.<>f__mg$cache1);
			}
			yield break;
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x0010188C File Offset: 0x000FFC8C
		public static IEnumerator Wait(string addSceneName)
		{
			if (!Singleton<Scene>.IsInstance())
			{
				yield break;
			}
			yield return new WaitWhile(() => Singleton<Scene>.Instance.AddSceneName != addSceneName);
			yield return Program.ADVProcessingCheck();
			yield break;
		}

		// Token: 0x04002B9F RID: 11167
		public const string BASE_FOV = "23";

		// Token: 0x04002BA0 RID: 11168
		public const string AdvScenarioPath = "scenario/";

		// Token: 0x04002BA4 RID: 11172
		[CompilerGenerated]
		private static Func<bool> <>f__mg$cache0;

		// Token: 0x04002BA5 RID: 11173
		[CompilerGenerated]
		private static Func<bool> <>f__mg$cache1;

		// Token: 0x02000786 RID: 1926
		[Serializable]
		public class Transfer
		{
			// Token: 0x06002D13 RID: 11539 RVA: 0x001018B0 File Offset: 0x000FFCB0
			public Transfer(ScenarioData.Param param)
			{
				this.line = -1;
				this.param = param;
			}

			// Token: 0x1700077F RID: 1919
			// (get) Token: 0x06002D14 RID: 11540 RVA: 0x001018C6 File Offset: 0x000FFCC6
			// (set) Token: 0x06002D15 RID: 11541 RVA: 0x001018CE File Offset: 0x000FFCCE
			public int line { get; private set; }

			// Token: 0x17000780 RID: 1920
			// (get) Token: 0x06002D16 RID: 11542 RVA: 0x001018D7 File Offset: 0x000FFCD7
			// (set) Token: 0x06002D17 RID: 11543 RVA: 0x001018DF File Offset: 0x000FFCDF
			public ScenarioData.Param param { get; private set; }

			// Token: 0x06002D18 RID: 11544 RVA: 0x001018E8 File Offset: 0x000FFCE8
			public static List<Program.Transfer> NewList(bool multi = true, bool isSceneRegulate = false)
			{
				return new List<Program.Transfer>
				{
					Program.Transfer.Create(multi, Command.SceneFadeRegulate, new string[]
					{
						isSceneRegulate.ToString()
					})
				};
			}

			// Token: 0x06002D19 RID: 11545 RVA: 0x00101920 File Offset: 0x000FFD20
			public static Program.Transfer Create(bool multi, Command command, params string[] args)
			{
				return new Program.Transfer(new ScenarioData.Param(multi, command, args));
			}

			// Token: 0x06002D1A RID: 11546 RVA: 0x0010192F File Offset: 0x000FFD2F
			public static Program.Transfer VAR(params string[] args)
			{
				return Program.Transfer.Create(true, Command.VAR, args);
			}

			// Token: 0x06002D1B RID: 11547 RVA: 0x00101939 File Offset: 0x000FFD39
			public static Program.Transfer Open(params string[] args)
			{
				return Program.Transfer.Create(false, Command.Open, args);
			}

			// Token: 0x06002D1C RID: 11548 RVA: 0x00101944 File Offset: 0x000FFD44
			public static Program.Transfer Close()
			{
				return Program.Transfer.Create(false, Command.Close, null);
			}

			// Token: 0x06002D1D RID: 11549 RVA: 0x0010194F File Offset: 0x000FFD4F
			public static Program.Transfer Text(params string[] args)
			{
				return Program.Transfer.Create(false, Command.Text, args);
			}

			// Token: 0x06002D1E RID: 11550 RVA: 0x0010195A File Offset: 0x000FFD5A
			public static Program.Transfer Voice(params string[] args)
			{
				return Program.Transfer.Create(true, Command.Voice, args);
			}

			// Token: 0x06002D1F RID: 11551 RVA: 0x00101965 File Offset: 0x000FFD65
			public static Program.Transfer Motion(params string[] args)
			{
				return Program.Transfer.Create(true, Command.Motion, args);
			}

			// Token: 0x06002D20 RID: 11552 RVA: 0x00101970 File Offset: 0x000FFD70
			public static Program.Transfer Expression(params string[] args)
			{
				return Program.Transfer.Create(true, Command.Expression, args);
			}

			// Token: 0x06002D21 RID: 11553 RVA: 0x0010197B File Offset: 0x000FFD7B
			public static Program.Transfer ExpressionIcon(params string[] args)
			{
				return Program.Transfer.Create(true, Command.ExpressionIcon, args);
			}
		}

		// Token: 0x02000787 RID: 1927
		public class OpenDataProc
		{
			// Token: 0x17000781 RID: 1921
			// (get) Token: 0x06002D23 RID: 11555 RVA: 0x0010198E File Offset: 0x000FFD8E
			// (set) Token: 0x06002D24 RID: 11556 RVA: 0x00101996 File Offset: 0x000FFD96
			public Action onLoad { get; set; }

			// Token: 0x17000782 RID: 1922
			// (get) Token: 0x06002D25 RID: 11557 RVA: 0x0010199F File Offset: 0x000FFD9F
			// (set) Token: 0x06002D26 RID: 11558 RVA: 0x001019A7 File Offset: 0x000FFDA7
			public Func<IEnumerator> onFadeIn { get; set; }

			// Token: 0x17000783 RID: 1923
			// (get) Token: 0x06002D27 RID: 11559 RVA: 0x001019B0 File Offset: 0x000FFDB0
			// (set) Token: 0x06002D28 RID: 11560 RVA: 0x001019B8 File Offset: 0x000FFDB8
			public Func<IEnumerator> onFadeOut { get; set; }

			// Token: 0x17000784 RID: 1924
			// (get) Token: 0x06002D29 RID: 11561 RVA: 0x001019C1 File Offset: 0x000FFDC1
			// (set) Token: 0x06002D2A RID: 11562 RVA: 0x001019C9 File Offset: 0x000FFDC9
			public Scene.Data.FadeType fadeType { get; set; }
		}
	}
}
