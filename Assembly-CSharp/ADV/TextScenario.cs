using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using ADV.Commands.Base;
using AIChara;
using AIProject;
using AIProject.CaptionScript;
using AIProject.SaveData;
using AIProject.UI;
using Cinemachine;
using Illusion;
using Illusion.Game.Elements;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ADV
{
	// Token: 0x02000788 RID: 1928
	[RequireComponent(typeof(CommandController))]
	public class TextScenario : MonoBehaviour
	{
		// Token: 0x06002D2C RID: 11564 RVA: 0x00101CBB File Offset: 0x001000BB
		public static void LoadReadInfo()
		{
			if (TextScenario.readInfo != null)
			{
				return;
			}
			TextScenario.readInfo = new TextScenario.AlreadyReadInfo();
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x00101CD2 File Offset: 0x001000D2
		public static void SaveReadInfo()
		{
			if (TextScenario.readInfo == null)
			{
				return;
			}
			TextScenario.readInfo.Save();
		}

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x06002D2E RID: 11566 RVA: 0x00101CEC File Offset: 0x001000EC
		// (remove) Token: 0x06002D2F RID: 11567 RVA: 0x00101D24 File Offset: 0x00100124
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<string, string, IReadOnlyCollection<TextScenario.IVoice[]>> TextLog;

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06002D30 RID: 11568 RVA: 0x00101D5C File Offset: 0x0010015C
		// (remove) Token: 0x06002D31 RID: 11569 RVA: 0x00101D94 File Offset: 0x00100194
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action VisibleLog;

		// Token: 0x06002D32 RID: 11570 RVA: 0x00101DCA File Offset: 0x001001CA
		public void TextLogCall(ADV.Commands.Base.Text.Data data, IReadOnlyCollection<TextScenario.IVoice[]> voices)
		{
			if (this.TextLog != null)
			{
				this.TextLog(data.name, data.text, voices);
			}
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x00101DF1 File Offset: 0x001001F1
		public void FadeIn(float duration, bool ignoreTimeScale = true)
		{
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, duration, ignoreTimeScale);
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x00101E02 File Offset: 0x00100202
		public void FadeOut(float duration, bool ignoreTimeScale = true)
		{
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, duration, ignoreTimeScale);
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x00101E13 File Offset: 0x00100213
		public bool Fading
		{
			[CompilerGenerated]
			get
			{
				return MapUIContainer.FadeCanvas.IsFading;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x00101E1F File Offset: 0x0010021F
		public CaptionSystem captionSystem
		{
			[CompilerGenerated]
			get
			{
				return this.captions.CaptionSystem;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x00101E2C File Offset: 0x0010022C
		public Captions captions
		{
			[CompilerGenerated]
			get
			{
				Captions result;
				if ((result = this._captions) == null)
				{
					result = (this._captions = Singleton<ADV>.Instance.Captions);
				}
				return result;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x00101E59 File Offset: 0x00100259
		private Manager.Input input
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Input>.Instance;
			}
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x00101E60 File Offset: 0x00100260
		public string ChoiceON(string title, CommCommandList.CommandInfo[] options)
		{
			string text = MapUIContainer.ChoiceUI.Label.text;
			MapUIContainer.SetActiveChoiceUI(true, title);
			MapUIContainer.ChoiceUI.Refresh(options, MapUIContainer.ChoiceUI.CanvasGroup, null);
			return text;
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x00101E9C File Offset: 0x0010029C
		public void ChoiceOFF(string label)
		{
			if (this._choiceCommandDis != null)
			{
				this._choiceCommandDis.Dispose();
			}
			this._choiceCommandDis = null;
			if (!label.IsNullOrEmpty())
			{
				this._choiceCommandDis = MapUIContainer.ChoiceUI.OnCompletedStopAsObservable().Take(1).Subscribe(delegate(Unit _)
				{
					MapUIContainer.ChoiceUI.Label.text = label;
				});
			}
			MapUIContainer.ChoiceUI.Visibled = false;
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x00101F17 File Offset: 0x00100317
		public ADVUI advUI
		{
			[CompilerGenerated]
			get
			{
				return this._advUI;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x00101F1F File Offset: 0x0010031F
		public Regulate regulate
		{
			[CompilerGenerated]
			get
			{
				return this._regulate;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x00101F27 File Offset: 0x00100327
		public Info info
		{
			[CompilerGenerated]
			get
			{
				return this._info;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002D3E RID: 11582 RVA: 0x00101F2F File Offset: 0x0010032F
		// (set) Token: 0x06002D3F RID: 11583 RVA: 0x00101F37 File Offset: 0x00100337
		public Camera AdvCamera
		{
			get
			{
				return this._AdvCamera;
			}
			set
			{
				this._AdvCamera = value;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x00101F40 File Offset: 0x00100340
		// (set) Token: 0x06002D41 RID: 11585 RVA: 0x00101F48 File Offset: 0x00100348
		public CinemachineVirtualCamera virtualCamera
		{
			get
			{
				return this._virtualCamera;
			}
			set
			{
				this._virtualCamera = value;
			}
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x00101F51 File Offset: 0x00100351
		public void CrossFadeStart()
		{
			if (this._crossFade == null)
			{
				return;
			}
			if (!this.isFadeAllEnd)
			{
				return;
			}
			this._crossFade.FadeStart(this.info.anime.play.crossFadeTime);
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x00101F91 File Offset: 0x00100391
		public CrossFade crossFade
		{
			[CompilerGenerated]
			get
			{
				return this._crossFade;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002D44 RID: 11588 RVA: 0x00101F99 File Offset: 0x00100399
		// (set) Token: 0x06002D45 RID: 11589 RVA: 0x00101FA1 File Offset: 0x001003A1
		public List<Program.Transfer> transferList
		{
			get
			{
				return this._transferList;
			}
			set
			{
				this._transferList = value;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002D46 RID: 11590 RVA: 0x00101FAA File Offset: 0x001003AA
		// (set) Token: 0x06002D47 RID: 11591 RVA: 0x00101FB2 File Offset: 0x001003B2
		private List<Program.Transfer> _transferList { get; set; }

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002D48 RID: 11592 RVA: 0x00101FBB File Offset: 0x001003BB
		// (set) Token: 0x06002D49 RID: 11593 RVA: 0x00101FC3 File Offset: 0x001003C3
		public string fontColorKey { get; set; }

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x00101FCC File Offset: 0x001003CC
		// (set) Token: 0x06002D4B RID: 11595 RVA: 0x00101FD9 File Offset: 0x001003D9
		public bool isSkip
		{
			get
			{
				return this._isSkip.Value;
			}
			set
			{
				this._isSkip.Value = value;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x00101FE7 File Offset: 0x001003E7
		// (set) Token: 0x06002D4D RID: 11597 RVA: 0x00101FF4 File Offset: 0x001003F4
		public bool isAuto
		{
			get
			{
				return this._isAuto.Value;
			}
			set
			{
				this._isAuto.Value = value;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002D4E RID: 11598 RVA: 0x00102002 File Offset: 0x00100402
		// (set) Token: 0x06002D4F RID: 11599 RVA: 0x0010200A File Offset: 0x0010040A
		public bool isWait
		{
			get
			{
				return this._isWait;
			}
			set
			{
				this._isWait = value;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x00102013 File Offset: 0x00100413
		// (set) Token: 0x06002D51 RID: 11601 RVA: 0x0010201D File Offset: 0x0010041D
		public int CurrentLine
		{
			get
			{
				return this.currentLine - 1;
			}
			set
			{
				this.currentLine = value;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x00102026 File Offset: 0x00100426
		public OpenData openData
		{
			[CompilerGenerated]
			get
			{
				return this._openData;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x0010202E File Offset: 0x0010042E
		// (set) Token: 0x06002D54 RID: 11604 RVA: 0x0010203B File Offset: 0x0010043B
		public string LoadBundleName
		{
			get
			{
				return this._openData.bundle;
			}
			set
			{
				this._openData.bundle = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002D55 RID: 11605 RVA: 0x00102049 File Offset: 0x00100449
		// (set) Token: 0x06002D56 RID: 11606 RVA: 0x00102056 File Offset: 0x00100456
		public string LoadAssetName
		{
			get
			{
				return this._openData.asset;
			}
			set
			{
				this._openData.asset = value;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x00102064 File Offset: 0x00100464
		// (set) Token: 0x06002D58 RID: 11608 RVA: 0x0010206C File Offset: 0x0010046C
		public bool isSceneFadeRegulate
		{
			get
			{
				return this._isSceneFadeRegulate;
			}
			set
			{
				this._isSceneFadeRegulate = value;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x00102075 File Offset: 0x00100475
		public bool isCameraLock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x00102078 File Offset: 0x00100478
		// (set) Token: 0x06002D5B RID: 11611 RVA: 0x00102080 File Offset: 0x00100480
		public bool isBackGroundCommanding { get; set; }

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x00102089 File Offset: 0x00100489
		public CommandController commandController
		{
			get
			{
				return this.GetComponentCache(ref this._commandController);
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x00102097 File Offset: 0x00100497
		public ADVScene advScene
		{
			get
			{
				return this.GetComponentCache(ref this._advScene);
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x001020A5 File Offset: 0x001004A5
		public List<ScenarioData.Param> CommandPacks
		{
			get
			{
				return this.commandPacks;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x001020AD File Offset: 0x001004AD
		public bool isBackGroundCommandProcessing
		{
			get
			{
				return this.backCommandList.Count > 0;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x001020BD File Offset: 0x001004BD
		public Image FilterImage
		{
			get
			{
				return this.filterImage;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x001020C8 File Offset: 0x001004C8
		public bool isFadeAllEnd
		{
			get
			{
				return !Singleton<Manager.Scene>.IsInstance() || this.advScene == null || this.advScene.AdvFade == null || this._crossFade == null || (!Singleton<Manager.Scene>.Instance.IsFadeNow && this.advScene.AdvFade.IsEnd && this._crossFade.isEnd);
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x0010214B File Offset: 0x0010054B
		protected List<ScenarioData.Param> commandPacks { get; } = new List<ScenarioData.Param>();

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x00102153 File Offset: 0x00100553
		private TextScenario.FileOpen fileOpenData { get; } = new TextScenario.FileOpen();

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x0010215B File Offset: 0x0010055B
		// (set) Token: 0x06002D65 RID: 11621 RVA: 0x00102163 File Offset: 0x00100563
		protected bool isRequestLine { get; set; }

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x0010216C File Offset: 0x0010056C
		protected HashSet<int> textHash { get; } = new HashSet<int>();

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x00102174 File Offset: 0x00100574
		public Dictionary<string, ValData> Vars
		{
			[CompilerGenerated]
			get
			{
				return this.vars;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x0010217C File Offset: 0x0010057C
		private Dictionary<string, ValData> vars { get; } = new Dictionary<string, ValData>();

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x00102184 File Offset: 0x00100584
		public Dictionary<string, string> Replaces
		{
			[CompilerGenerated]
			get
			{
				return this.replaces;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x0010218C File Offset: 0x0010058C
		private Dictionary<string, string> replaces { get; } = new Dictionary<string, string>();

		// Token: 0x06002D6B RID: 11627 RVA: 0x00102194 File Offset: 0x00100594
		public void SetPackage(IPack package)
		{
			this.package = package;
			this.SetCharacters((package != null) ? package.param : null);
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x001021B2 File Offset: 0x001005B2
		// (set) Token: 0x06002D6D RID: 11629 RVA: 0x001021BA File Offset: 0x001005BA
		public IPack package { get; private set; }

		// Token: 0x06002D6E RID: 11630 RVA: 0x001021C4 File Offset: 0x001005C4
		public void SetCharacters(IParams[] param)
		{
			if (param == null)
			{
				return;
			}
			TextScenario.ParamData[] source = (from p in param
			select new TextScenario.ParamData(p)).ToArray<TextScenario.ParamData>();
			this.heroineList = (from p in source
			where p.isHeroine
			select p).ToList<TextScenario.ParamData>();
			this.player = source.FirstOrDefault((TextScenario.ParamData p) => p.isPlayer);
			CharaData.MotionReserver motionReserver = null;
			foreach (var <>__AnonType in this.heroineList.Select((TextScenario.ParamData data, int index) => new
			{
				data,
				index
			}))
			{
				this.commandController.AddChara(<>__AnonType.index, new CharaData(<>__AnonType.data, this, motionReserver));
			}
			if (this.player != null)
			{
				this.commandController.AddChara(-1, new CharaData(this.player, this, motionReserver));
			}
			if (this.commandController.Characters.Any<KeyValuePair<int, CharaData>>())
			{
				this.ChangeCurrentChara(this.commandController.Characters.First<KeyValuePair<int, CharaData>>().Key);
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x00102338 File Offset: 0x00100738
		// (set) Token: 0x06002D70 RID: 11632 RVA: 0x00102340 File Offset: 0x00100740
		public TextScenario.ParamData player { get; private set; }

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x00102349 File Offset: 0x00100749
		// (set) Token: 0x06002D72 RID: 11634 RVA: 0x00102351 File Offset: 0x00100751
		public List<TextScenario.ParamData> heroineList { get; private set; }

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x0010235A File Offset: 0x0010075A
		// (set) Token: 0x06002D74 RID: 11636 RVA: 0x00102362 File Offset: 0x00100762
		public CharaData currentChara
		{
			get
			{
				return this._currentChara;
			}
			set
			{
				this._currentChara = value;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x0010236B File Offset: 0x0010076B
		// (set) Token: 0x06002D76 RID: 11638 RVA: 0x00102373 File Offset: 0x00100773
		private CharaData _currentChara { get; set; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x0010237C File Offset: 0x0010077C
		// (set) Token: 0x06002D78 RID: 11640 RVA: 0x00102384 File Offset: 0x00100784
		private BackupPosRot backCameraBackup { get; set; }

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x0010238D File Offset: 0x0010078D
		private CommandList nowCommandList
		{
			get
			{
				return this._commandController.NowCommandList;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x0010239A File Offset: 0x0010079A
		private CommandList backCommandList
		{
			get
			{
				return this._commandController.BackGroundCommandList;
			}
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x001023A7 File Offset: 0x001007A7
		public bool ChangeCurrentChara(int no)
		{
			this.currentChara = this.commandController.GetChara(no);
			return this.currentChara != null;
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x001023C8 File Offset: 0x001007C8
		public string ReplaceVars(string arg)
		{
			ValData valData;
			return this.Vars.TryGetValue(arg, out valData) ? valData.o.ToString() : arg;
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x001023FC File Offset: 0x001007FC
		public string ReplaceText(string text)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			try
			{
				IEnumerator enumerator = Regex.Matches(text, "\\[.*?\\]").GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Match match = (Match)obj;
						if (match.Success)
						{
							string key = string.Empty;
							try
							{
								key = Regex.Replace(match.Value, "\\[|\\]", string.Empty);
							}
							catch (Exception)
							{
							}
							string text2;
							if (this.Replaces.TryGetValue(key, out text2))
							{
								if (!text2.IsNullOrEmpty())
								{
									text2 = this.ReplaceText(text2);
								}
								if (text2 == null)
								{
									text2 = match.Value;
								}
							}
							else
							{
								text2 = match.Value;
							}
							stringBuilder.Append(text.Substring(num, match.Index - num));
							stringBuilder.Append(text2);
							num = match.Index + match.Length;
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			catch (Exception)
			{
			}
			stringBuilder.Append(text.Substring(num, text.Length - num));
			return stringBuilder.ToString();
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x00102554 File Offset: 0x00100954
		public IObservable<Unit> OnInitializedAsync
		{
			[CompilerGenerated]
			get
			{
				return this._single;
			}
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x0010255C File Offset: 0x0010095C
		public virtual void Initialize()
		{
			this.commandPacks.Clear();
			this.fileOpenData.Clear();
			this._regulate.SetRegulate((Regulate.Control)0);
			if (this.backCameraBackup != null && this.backCamera != null)
			{
				this.backCameraBackup.Set(this.backCamera.transform);
			}
			this.currentLine = 0;
			this.autoWaitTimer = 0f;
			this.commandController.Initialize();
			this.captionSystem.Clear();
			this.vars.Clear();
			this.replaces.Clear();
			this.advFade.SafeProcObject(delegate(ADVFade p)
			{
				p.Initialize();
			});
			this._single.Done();
			TextScenario.LoadReadInfo();
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x00102634 File Offset: 0x00100A34
		protected void MemberInit()
		{
			this.isSkip = false;
			this.isAuto = false;
			this._isWait = false;
			this._isSceneFadeRegulate = true;
			this._isStartRun = false;
			this.textHash.Clear();
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x00102664 File Offset: 0x00100A64
		public virtual void Release()
		{
			if (this.voicePlayDis != null)
			{
				this.voicePlayDis.Dispose();
			}
			this.voicePlayDis = null;
			if (this._choiceCommandDis != null)
			{
				this._choiceCommandDis.Dispose();
			}
			this._choiceCommandDis = null;
			this.loopVoiceList.ForEach(delegate(TextScenario.LoopVoicePack p)
			{
				if (p.audio != null)
				{
					UnityEngine.Object.Destroy(p.audio.gameObject);
				}
			});
			this.loopVoiceList.Clear();
			this._single = new Illusion.Game.Elements.Single();
			this.commandController.Release();
			this.AdvCamera = null;
			this.info.audio.is2D = false;
			this.info.audio.isNotMoveMouth = false;
			if (!Singleton<Game>.IsInstance())
			{
				TextScenario.SaveReadInfo();
			}
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x00102730 File Offset: 0x00100B30
		public void CommandAdd(bool isNext, int line, bool multi, Command command, string[] args)
		{
			List<string> list = new List<string>();
			list.Add("0");
			list.Add(multi.ToString());
			list.Add(command.ToString());
			List<string> list2 = list;
			string[] collection = args;
			if (args == null)
			{
				(collection = new string[1])[0] = string.Empty;
			}
			list2.AddRange(collection);
			ScenarioData.Param item = new ScenarioData.Param(list.ToArray());
			if (this.commandPacks.Count == line)
			{
				this.commandPacks.Add(item);
			}
			else
			{
				this.commandPacks.Insert(line, item);
			}
			if (isNext)
			{
				this.RequestNextLine();
			}
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x001027D8 File Offset: 0x00100BD8
		public virtual bool LoadFile(string bundle, string asset, bool isClear = true, bool isClearCheck = true, bool isNext = true)
		{
			if (isClear)
			{
				this.captionSystem.Clear();
				this.fileOpenData.Clear();
			}
			if (bundle.IsNullOrEmpty())
			{
				bundle = this.LoadBundleName;
			}
			if (!isClear && isClearCheck && this.fileOpenData.FileList.Any((RootData p) => p.bundleName == bundle && p.fileName == asset))
			{
				return false;
			}
			this.openData.Load(bundle, asset);
			if (!isClear)
			{
				this.commandPacks.InsertRange(this.currentLine, this.openData.data.list);
				if (!this.fileOpenData.FileList.Any((RootData p) => p.bundleName == bundle && p.fileName == asset && p.line == this.currentLine))
				{
					this.fileOpenData.FileList.Add(new RootData
					{
						bundleName = bundle,
						fileName = asset,
						line = this.CurrentLine
					});
				}
			}
			else
			{
				this.LoadBundleName = bundle;
				this.LoadAssetName = asset;
				string[] args = new string[]
				{
					bundle,
					asset,
					bool.FalseString,
					bool.TrueString,
					bool.TrueString
				};
				this.currentLine = 0;
				this.commandPacks.Clear();
				this.CommandAdd(false, this.currentLine++, false, Command.Open, args);
				this.commandPacks.AddRange(this.openData.data.list);
			}
			if (isNext)
			{
				this.RequestNextLine();
			}
			this.openData.ClearData();
			return true;
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x001029B4 File Offset: 0x00100DB4
		public bool SearchTagJumpOrOpenFile(string jump, int localLine)
		{
			string[] array = jump.Split(new char[]
			{
				':'
			});
			if (array.Length != 1)
			{
				Open open = new Open();
				open.Set(Command.Open);
				string[] argsDefault = open.ArgsDefault;
				int num = 0;
				while (num < array.Length && num < argsDefault.Length)
				{
					argsDefault[num] = this.ReplaceVars(array[num]);
					num++;
				}
				this.CommandAdd(false, localLine + 1, false, open.command, argsDefault);
				return true;
			}
			int n;
			if (this.SearchTag(jump, out n))
			{
				this.Jump(n);
				return true;
			}
			return false;
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x00102A54 File Offset: 0x00100E54
		public bool SearchTag(string tagName, out int n)
		{
			n = this.commandPacks.TakeWhile((ScenarioData.Param p) => p.Command != Command.Tag || this.ReplaceVars(p.Args[0]) != tagName).Count<ScenarioData.Param>();
			return n < this.commandPacks.Count;
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x00102AA2 File Offset: 0x00100EA2
		public void Jump(int n)
		{
			this.currentLine = n;
			this.RequestNextLine();
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x00102AB1 File Offset: 0x00100EB1
		public virtual void ConfigProc()
		{
			if (!Config.initialized)
			{
				return;
			}
			this.autoWaitTime = Config.GameData.AutoWaitTime;
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x00102ACE File Offset: 0x00100ECE
		public void BackGroundCommandProcessEnd()
		{
			this.backCommandList.ProcessEnd();
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x00102ADB File Offset: 0x00100EDB
		public List<TextScenario.LoopVoicePack> loopVoiceList
		{
			get
			{
				return this._loopVoiceList;
			}
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x00102AE4 File Offset: 0x00100EE4
		public void VoicePlay(List<TextScenario.IVoice[]> voices, Action onChange, Action onEnd)
		{
			if (this.voicePlayDis != null)
			{
				this.voicePlayDis.Dispose();
			}
			this.voicePlayDis = null;
			Singleton<Manager.Voice>.Instance.StopAll(false);
			if (voices == null)
			{
				return;
			}
			if (this._loopVoiceList.Any<TextScenario.LoopVoicePack>())
			{
				HashSet<int> hashSet = new HashSet<int>();
				foreach (TextScenario.IVoice[] array in voices)
				{
					foreach (TextScenario.IVoice voice in array)
					{
						hashSet.Add(voice.personality);
					}
				}
				foreach (int num in hashSet)
				{
					foreach (TextScenario.LoopVoicePack loopVoicePack in this._loopVoiceList)
					{
						if (loopVoicePack.voiceNo == num && loopVoicePack.audio != null)
						{
							loopVoicePack.audio.Pause();
						}
					}
				}
			}
			this.voicePlayDis = Observable.FromCoroutine<Unit>((IObserver<Unit> observer) => this.VoicePlayCoroutine(observer, voices)).Subscribe(delegate(Unit _)
			{
				onChange.Call();
			}, delegate()
			{
				List<TextScenario.LoopVoicePack> list = new List<TextScenario.LoopVoicePack>();
				foreach (TextScenario.LoopVoicePack loopVoicePack2 in this._loopVoiceList)
				{
					if (!loopVoicePack2.Set() || loopVoicePack2.audio == null)
					{
						list.Add(loopVoicePack2);
					}
					else
					{
						loopVoicePack2.audio.Play();
					}
				}
				list.ForEach(delegate(TextScenario.LoopVoicePack item)
				{
					this._loopVoiceList.Remove(item);
				});
				onEnd.Call();
			});
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x00102CC0 File Offset: 0x001010C0
		private IEnumerator VoicePlayCoroutine(IObserver<Unit> observer, List<TextScenario.IVoice[]> voiceList)
		{
			foreach (TextScenario.IVoice[] voice in voiceList)
			{
				foreach (TextScenario.IVoice voice2 in voice)
				{
					voice2.Play();
				}
				observer.OnNext(Unit.Default);
				for (;;)
				{
					if (!voice.Any((TextScenario.IVoice p) => p.Wait()))
					{
						break;
					}
					yield return null;
				}
			}
			observer.OnCompleted();
			yield break;
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x00102CE4 File Offset: 0x001010E4
		protected virtual bool StartRun()
		{
			if (this._isStartRun)
			{
				return false;
			}
			this.ADVCameraSetting();
			this._isStartRun = true;
			int num = 0;
			if (!this._transferList.IsNullOrEmpty<Program.Transfer>())
			{
				Program.Transfer transfer = this._transferList[0];
				if (transfer.param.Command == Command.SceneFadeRegulate)
				{
					this._isSceneFadeRegulate = bool.Parse(transfer.param.Args[0]);
				}
				foreach (Program.Transfer transfer2 in this._transferList)
				{
					int line = (transfer2.line != -1) ? transfer2.line : num;
					this.CommandAdd(false, line, transfer2.param.Multi, transfer2.param.Command, transfer2.param.Args);
					num++;
				}
			}
			if (!this.LoadBundleName.IsNullOrEmpty() && !this.LoadAssetName.IsNullOrEmpty())
			{
				string[] args = new string[]
				{
					this.LoadBundleName,
					this.LoadAssetName,
					bool.FalseString,
					bool.TrueString,
					bool.TrueString
				};
				this.CommandAdd(true, num, false, Command.Open, args);
			}
			else if (this._openData.HasData)
			{
				this.commandPacks.AddRange(this.openData.data.list);
				this.RequestNextLine();
				this.openData.ClearData();
			}
			return true;
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x00102E88 File Offset: 0x00101288
		protected void RequestNextLine()
		{
			base.StartCoroutine(this._RequestNextLine());
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x00102E98 File Offset: 0x00101298
		protected virtual IEnumerator _RequestNextLine()
		{
			this.isRequestLine = true;
			this.nowCommandList.ProcessEnd();
			bool isMulti = false;
			ScenarioData.Param pack = null;
			this.textHash.Clear();
			this.autoWaitTimer = 0f;
			do
			{
				while (this.commandController.LoadingCharaList.Any<CharaData>())
				{
					yield return null;
				}
				isMulti = false;
				if (this.currentLine < this.commandPacks.Count)
				{
					pack = this.commandPacks[this.currentLine++];
					if (pack.Command == Command.Log)
					{
						isMulti = true;
					}
					else
					{
						if (!this.isBackGroundCommanding || pack.Command == Command.Task)
						{
							isMulti = this.nowCommandList.Add(pack, this.CurrentLine);
						}
						else
						{
							isMulti = this.backCommandList.Add(pack, this.CurrentLine);
						}
						Command command = pack.Command;
						if (command == Command.Text)
						{
							this.textHash.Add(pack.Hash);
						}
					}
				}
			}
			while (isMulti);
			this.isRequestLine = false;
			foreach (int i in this.textHash)
			{
				if (TextScenario.readInfo.Add(i) && this.isSkip && Config.GameData.ReadSkip)
				{
					this.isSkip = false;
				}
			}
			yield break;
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x00102EB3 File Offset: 0x001012B3
		protected void ADVCameraSetting()
		{
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x00102EB8 File Offset: 0x001012B8
		protected bool MessageWindowProc(TextScenario.NextInfo nextInfo)
		{
			if (this.isRequestLine)
			{
				return false;
			}
			this.backCommandList.Process();
			if (this.nowCommandList.Process())
			{
				return false;
			}
			if (this.commandPacks.Count == 0)
			{
				return false;
			}
			if (this.regulate.control.HasFlag(Regulate.Control.ClickNext))
			{
				nextInfo.isNext = false;
			}
			if (this.regulate.control.HasFlag(Regulate.Control.Skip))
			{
				nextInfo.isSkip = false;
				this.isSkip = false;
			}
			if (this.regulate.control.HasFlag(Regulate.Control.Auto))
			{
				this.isAuto = false;
			}
			if (this.regulate.control.HasFlag(Regulate.Control.AutoForce))
			{
				this.isAuto = true;
			}
			bool isCompleteDisplayText = nextInfo.isCompleteDisplayText;
			bool isNext = nextInfo.isNext;
			bool isSkip = nextInfo.isSkip;
			if (!isCompleteDisplayText)
			{
				if (isNext || this.isSkip || isSkip)
				{
					this.captionSystem.ForceCompleteDisplayText();
					this.nowCommandList.ProcessEnd();
				}
				return false;
			}
			this.autoWaitTimer = Mathf.Min(this.autoWaitTimer + Time.deltaTime, this.autoWaitTime);
			bool flag = this.nowCommandList.Count > 0;
			bool flag2 = this.textHash.Count > 0;
			bool flag3 = false;
			if (isSkip || this.isSkip)
			{
				flag3 |= true;
			}
			else if (this.isAuto && flag2)
			{
				flag3 |= (this.autoWaitTimer >= this.autoWaitTime && !flag);
			}
			flag3 = (flag3 || isNext);
			if (this.regulate.control.HasFlag(Regulate.Control.Next))
			{
				flag3 = false;
			}
			flag3 |= (!flag2 && !flag);
			if (flag3)
			{
				this.currentCharaData.Clear();
				this.RequestNextLine();
			}
			return flag3;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x001030D5 File Offset: 0x001014D5
		protected virtual void UpdateBefore()
		{
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x001030D8 File Offset: 0x001014D8
		protected virtual bool UpdateRegulate()
		{
			if (Manager.Scene.isReturnTitle || Manager.Scene.isGameEnd)
			{
				return true;
			}
			if (!Singleton<Manager.Scene>.IsInstance() || !Singleton<Game>.IsInstance())
			{
				return true;
			}
			if (Singleton<Manager.Scene>.Instance.IsNowLoading)
			{
				return true;
			}
			if (this._isWait)
			{
				return true;
			}
			this.StartRun();
			foreach (CharaData charaData in this.commandController.Characters.Values)
			{
				if (!charaData.initialized)
				{
					return true;
				}
			}
			return (this._isSceneFadeRegulate && Singleton<Manager.Scene>.Instance.sceneFade.IsFadeNow) || Singleton<Manager.Scene>.Instance.IsOverlap || Mathf.Max(0, Singleton<Manager.Scene>.Instance.NowSceneNames.IndexOf("ADV")) > 0 || (Singleton<Game>.Instance.Config != null || Singleton<Game>.Instance.Dialog != null || Singleton<Game>.Instance.ExitScene != null || Singleton<Game>.Instance.MapShortcutUI != null || this.Fading) || !this.captionSystem.Visible;
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x00103264 File Offset: 0x00101664
		protected virtual void Awake()
		{
			this._regulate = new Regulate(this);
			if (this.backCamera != null)
			{
				this.backCameraBackup = new BackupPosRot(this.backCamera.transform);
			}
			(from _ in this.captionSystem.RaycasterImage.OnPointerClickAsObservable()
			where base.isActiveAndEnabled
			select _ into ped
			where ped != null
			select ped).Subscribe(delegate(PointerEventData ped)
			{
				bool flag = false;
				bool flag2 = false;
				switch (ped.button)
				{
				case PointerEventData.InputButton.Left:
					flag = true;
					break;
				case PointerEventData.InputButton.Right:
					flag2 = true;
					break;
				}
				if ((flag || flag2) && !this.captionSystem.Visible)
				{
					this.captionSystem.Visible = true;
					return;
				}
				if (flag)
				{
					this._clicked = true;
				}
			});
			if (this.advUI != null)
			{
				if (this.advUI.skip != null)
				{
					(from isOn in this._isSkip
					where isOn != this.advUI.skip.isOn
					select isOn).Subscribe(delegate(bool isOn)
					{
						this.advUI.useSE = false;
						this.advUI.skip.isOn = isOn;
						this.advUI.useSE = true;
					});
					(from isOn in this.advUI.skip.OnValueChangedAsObservable()
					where isOn != this.isSkip
					select isOn).Subscribe(delegate(bool isOn)
					{
						this.isSkip = isOn;
						this.advUI.PlaySE(SoundPack.SystemSE.OK_S);
					});
				}
				if (this.advUI.auto != null)
				{
					(from isOn in this._isAuto
					where isOn != this.advUI.auto.isOn
					select isOn).Subscribe(delegate(bool isOn)
					{
						this.advUI.useSE = false;
						this.advUI.auto.isOn = isOn;
						this.advUI.useSE = true;
					});
					(from isOn in this.advUI.auto.OnValueChangedAsObservable()
					where isOn != this.isAuto
					select isOn).Subscribe(delegate(bool isOn)
					{
						this.isAuto = isOn;
						this.advUI.PlaySE(SoundPack.SystemSE.OK_S);
					});
				}
				if (this.advUI.log != null)
				{
					this.advUI.log.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (this.VisibleLog != null)
						{
							this.VisibleLog();
						}
						this.advUI.PlaySE(SoundPack.SystemSE.OK_S);
					});
				}
				if (this.advUI.close != null)
				{
					(from _ in this.advUI.close.OnClickAsObservable()
					where this.captionSystem.Visible
					select _).Subscribe(delegate(Unit _)
					{
						this.captionSystem.Visible = false;
						this.advUI.PlaySE(SoundPack.SystemSE.Cancel);
					});
				}
			}
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x0010346F File Offset: 0x0010186F
		protected virtual void OnEnable()
		{
			this.Initialize();
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x00103477 File Offset: 0x00101877
		protected virtual void OnDisable()
		{
			if (Manager.Scene.isGameEnd)
			{
				return;
			}
			this.Release();
			this.MemberInit();
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x00103490 File Offset: 0x00101890
		protected virtual void Update()
		{
			this.UpdateBefore();
			if (this.UpdateRegulate())
			{
				return;
			}
			bool isNext = this.input.IsPressedSubmit() || this._clicked;
			bool isSkip = this.input.IsDown(KeyCode.LeftControl) || this.input.IsDown(KeyCode.RightControl);
			this._clicked = false;
			this.MessageWindowProc(new TextScenario.NextInfo(this.captionSystem.IsCompleteDisplayText, isNext, isSkip));
		}

		// Token: 0x04002BAC RID: 11180
		private static TextScenario.AlreadyReadInfo readInfo;

		// Token: 0x04002BAF RID: 11183
		[SerializeField]
		private Captions _captions;

		// Token: 0x04002BB0 RID: 11184
		private bool _clicked;

		// Token: 0x04002BB1 RID: 11185
		private IDisposable _choiceCommandDis;

		// Token: 0x04002BB2 RID: 11186
		[SerializeField]
		private ADVUI _advUI;

		// Token: 0x04002BB3 RID: 11187
		[SerializeField]
		private Regulate _regulate;

		// Token: 0x04002BB4 RID: 11188
		[SerializeField]
		private Info _info = new Info();

		// Token: 0x04002BB5 RID: 11189
		[SerializeField]
		private Camera _AdvCamera;

		// Token: 0x04002BB6 RID: 11190
		private CinemachineVirtualCamera _virtualCamera;

		// Token: 0x04002BB7 RID: 11191
		private CrossFade _crossFade;

		// Token: 0x04002BB8 RID: 11192
		public TextScenario.CurrentCharaData currentCharaData = new TextScenario.CurrentCharaData();

		// Token: 0x04002BBB RID: 11195
		[SerializeField]
		private OpenData _openData = new OpenData();

		// Token: 0x04002BBD RID: 11197
		private CommandController _commandController;

		// Token: 0x04002BBE RID: 11198
		private ADVScene _advScene;

		// Token: 0x04002BBF RID: 11199
		[SerializeField]
		private ADVFade advFade;

		// Token: 0x04002BC0 RID: 11200
		[SerializeField]
		private Camera backCamera;

		// Token: 0x04002BC1 RID: 11201
		[SerializeField]
		private Transform characters;

		// Token: 0x04002BC2 RID: 11202
		[SerializeField]
		private Image filterImage;

		// Token: 0x04002BC3 RID: 11203
		[SerializeField]
		protected BoolReactiveProperty _isSkip = new BoolReactiveProperty(false);

		// Token: 0x04002BC4 RID: 11204
		[SerializeField]
		protected BoolReactiveProperty _isAuto = new BoolReactiveProperty(false);

		// Token: 0x04002BC5 RID: 11205
		[SerializeField]
		protected bool _isWait;

		// Token: 0x04002BC6 RID: 11206
		[Header("Debug表示")]
		[SerializeField]
		protected int currentLine;

		// Token: 0x04002BC7 RID: 11207
		[SerializeField]
		protected float autoWaitTimer;

		// Token: 0x04002BC8 RID: 11208
		[SerializeField]
		protected float autoWaitTime = 3f;

		// Token: 0x04002BC9 RID: 11209
		[SerializeField]
		protected bool _isText;

		// Token: 0x04002BCA RID: 11210
		[SerializeField]
		private bool _isSceneFadeRegulate = true;

		// Token: 0x04002BCB RID: 11211
		[SerializeField]
		private bool _isStartRun;

		// Token: 0x04002BD7 RID: 11223
		private Illusion.Game.Elements.Single _single = new Illusion.Game.Elements.Single();

		// Token: 0x04002BD8 RID: 11224
		private IDisposable voicePlayDis;

		// Token: 0x04002BD9 RID: 11225
		private List<TextScenario.LoopVoicePack> _loopVoiceList = new List<TextScenario.LoopVoicePack>();

		// Token: 0x04002BDA RID: 11226
		public const int VOICE_SET_NO = 1;

		// Token: 0x02000789 RID: 1929
		private class AlreadyReadInfo
		{
			// Token: 0x06002DAC RID: 11692 RVA: 0x00103705 File Offset: 0x00101B05
			public AlreadyReadInfo()
			{
				this.read.Add(0);
				this.Load();
			}

			// Token: 0x170007B4 RID: 1972
			// (get) Token: 0x06002DAD RID: 11693 RVA: 0x0010372C File Offset: 0x00101B2C
			private HashSet<int> read { get; } = new HashSet<int>();

			// Token: 0x06002DAE RID: 11694 RVA: 0x00103734 File Offset: 0x00101B34
			public bool Add(int i)
			{
				return this.read.Add(i);
			}

			// Token: 0x06002DAF RID: 11695 RVA: 0x00103742 File Offset: 0x00101B42
			public void Save()
			{
				Illusion.Utils.File.OpenWrite(UserData.Create("save") + "read.dat", false, delegate(FileStream f)
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(f))
					{
						binaryWriter.Write(this.read.Count);
						foreach (int value in this.read)
						{
							binaryWriter.Write(value);
						}
					}
				});
			}

			// Token: 0x06002DB0 RID: 11696 RVA: 0x0010376A File Offset: 0x00101B6A
			public bool Load()
			{
				return Illusion.Utils.File.OpenRead(string.Concat(new object[]
				{
					UserData.Path,
					"save",
					'/',
					"read.dat"
				}), delegate(FileStream f)
				{
					using (BinaryReader binaryReader = new BinaryReader(f))
					{
						int num = binaryReader.ReadInt32();
						for (int i = 0; i < num; i++)
						{
							this.read.Add(binaryReader.ReadInt32());
						}
					}
				});
			}

			// Token: 0x04002BE2 RID: 11234
			private const string Path = "save";

			// Token: 0x04002BE3 RID: 11235
			private const string FileName = "read.dat";
		}

		// Token: 0x0200078A RID: 1930
		public interface IVoice
		{
			// Token: 0x170007B5 RID: 1973
			// (get) Token: 0x06002DB3 RID: 11699
			int personality { get; }

			// Token: 0x170007B6 RID: 1974
			// (get) Token: 0x06002DB4 RID: 11700
			string bundle { get; }

			// Token: 0x170007B7 RID: 1975
			// (get) Token: 0x06002DB5 RID: 11701
			string asset { get; }

			// Token: 0x06002DB6 RID: 11702
			void Convert2D();

			// Token: 0x06002DB7 RID: 11703
			AudioSource Play();

			// Token: 0x06002DB8 RID: 11704
			bool Wait();
		}

		// Token: 0x0200078B RID: 1931
		public interface IChara
		{
			// Token: 0x170007B8 RID: 1976
			// (get) Token: 0x06002DB9 RID: 11705
			int no { get; }

			// Token: 0x06002DBA RID: 11706
			void Play(TextScenario scenario);

			// Token: 0x06002DBB RID: 11707
			CharaData GetChara(TextScenario scenario);
		}

		// Token: 0x0200078C RID: 1932
		public interface IMotion : TextScenario.IChara
		{
		}

		// Token: 0x0200078D RID: 1933
		public interface IExpression : TextScenario.IChara
		{
		}

		// Token: 0x0200078E RID: 1934
		public interface IExpressionIcon : TextScenario.IChara
		{
		}

		// Token: 0x0200078F RID: 1935
		public class CurrentCharaData
		{
			// Token: 0x06002DBC RID: 11708 RVA: 0x001038A4 File Offset: 0x00101CA4
			public CurrentCharaData()
			{
				this._bundleVoices = new Dictionary<int, string>();
			}

			// Token: 0x170007B9 RID: 1977
			// (get) Token: 0x06002DBD RID: 11709 RVA: 0x001038B7 File Offset: 0x00101CB7
			public Dictionary<int, string> bundleVoices
			{
				[CompilerGenerated]
				get
				{
					return this._bundleVoices;
				}
			}

			// Token: 0x170007BA RID: 1978
			// (get) Token: 0x06002DBE RID: 11710 RVA: 0x001038BF File Offset: 0x00101CBF
			// (set) Token: 0x06002DBF RID: 11711 RVA: 0x001038C7 File Offset: 0x00101CC7
			public bool isSkip { get; set; }

			// Token: 0x170007BB RID: 1979
			// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x001038D0 File Offset: 0x00101CD0
			public List<TextScenario.IVoice[]> voiceList
			{
				[CompilerGenerated]
				get
				{
					return this._voiceList;
				}
			}

			// Token: 0x170007BC RID: 1980
			// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x001038D8 File Offset: 0x00101CD8
			public List<TextScenario.IMotion[]> motionList
			{
				[CompilerGenerated]
				get
				{
					return this._motionList;
				}
			}

			// Token: 0x170007BD RID: 1981
			// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x001038E0 File Offset: 0x00101CE0
			public List<TextScenario.IExpression[]> expressionList
			{
				[CompilerGenerated]
				get
				{
					return this._expressionList;
				}
			}

			// Token: 0x06002DC3 RID: 11715 RVA: 0x001038E8 File Offset: 0x00101CE8
			public void CreateVoiceList()
			{
				if (this.voiceList == null)
				{
					this._voiceList = new List<TextScenario.IVoice[]>();
				}
			}

			// Token: 0x06002DC4 RID: 11716 RVA: 0x00103900 File Offset: 0x00101D00
			public void CreateMotionList()
			{
				if (this.motionList == null)
				{
					this._motionList = new List<TextScenario.IMotion[]>();
				}
			}

			// Token: 0x06002DC5 RID: 11717 RVA: 0x00103918 File Offset: 0x00101D18
			public void CreateExpressionList()
			{
				if (this.expressionList == null)
				{
					this._expressionList = new List<TextScenario.IExpression[]>();
				}
			}

			// Token: 0x06002DC6 RID: 11718 RVA: 0x00103930 File Offset: 0x00101D30
			public void Clear()
			{
				this._voiceList = null;
				this._motionList = null;
				this._expressionList = null;
			}

			// Token: 0x04002BE6 RID: 11238
			private List<TextScenario.IVoice[]> _voiceList;

			// Token: 0x04002BE7 RID: 11239
			private List<TextScenario.IMotion[]> _motionList;

			// Token: 0x04002BE8 RID: 11240
			private List<TextScenario.IExpression[]> _expressionList;

			// Token: 0x04002BE9 RID: 11241
			private Dictionary<int, string> _bundleVoices;
		}

		// Token: 0x02000790 RID: 1936
		public class ParamData
		{
			// Token: 0x06002DC7 RID: 11719 RVA: 0x00103948 File Offset: 0x00101D48
			public ParamData(IParams param)
			{
				this.param = param;
				int num = -1;
				if (param is PlayerData)
				{
					num = 0;
				}
				else if (param is AgentData)
				{
					num = 1;
				}
				else if (param is MerchantData)
				{
					num = 2;
				}
				this.type = num;
				this.charaParam = (((param != null) ? param.param : null) as CharaParams);
				CharaParams charaParam = this.charaParam;
				int? num2 = (charaParam != null) ? new int?(charaParam.charaID) : null;
				this.voiceNo = ((num2 == null) ? 0 : num2.Value);
				CharaParams charaParam2 = this.charaParam;
				this.actor = ((charaParam2 != null) ? charaParam2.actor : null);
				if (this.actor != null)
				{
					this.chaCtrl = this.actor.ChaControl;
					this.transform = this.actor.Animation.Character.transform;
				}
				if (this.chaCtrl != null)
				{
					this.voicePitch = this.chaCtrl.fileParam.voicePitch;
				}
				switch (num)
				{
				case 0:
					this.playerData = (param as PlayerData);
					this.characterInfo = this.playerData;
					this.playerActor = (this.actor as PlayerActor);
					this.sex = this.playerData.Sex;
					break;
				case 1:
					this.agentData = (param as AgentData);
					this.characterInfo = this.agentData;
					this.agentActor = (this.actor as AgentActor);
					break;
				case 2:
					this.merchantData = (param as MerchantData);
					this.characterInfo = this.merchantData;
					this.merchantActor = (this.actor as MerchantActor);
					break;
				}
			}

			// Token: 0x170007BE RID: 1982
			// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x00103B40 File Offset: 0x00101F40
			public bool isPlayer
			{
				[CompilerGenerated]
				get
				{
					return this.type == 0;
				}
			}

			// Token: 0x170007BF RID: 1983
			// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x00103B4B File Offset: 0x00101F4B
			public bool isHeroine
			{
				[CompilerGenerated]
				get
				{
					return MathfEx.IsRange<int>(1, this.type, 2, true);
				}
			}

			// Token: 0x170007C0 RID: 1984
			// (get) Token: 0x06002DCA RID: 11722 RVA: 0x00103B5B File Offset: 0x00101F5B
			public int type { get; }

			// Token: 0x170007C1 RID: 1985
			// (get) Token: 0x06002DCB RID: 11723 RVA: 0x00103B63 File Offset: 0x00101F63
			public IParams param { get; }

			// Token: 0x170007C2 RID: 1986
			// (get) Token: 0x06002DCC RID: 11724 RVA: 0x00103B6B File Offset: 0x00101F6B
			public CharaParams charaParam { get; }

			// Token: 0x170007C3 RID: 1987
			// (get) Token: 0x06002DCD RID: 11725 RVA: 0x00103B73 File Offset: 0x00101F73
			public Actor actor { get; }

			// Token: 0x170007C4 RID: 1988
			// (get) Token: 0x06002DCE RID: 11726 RVA: 0x00103B7B File Offset: 0x00101F7B
			public PlayerActor playerActor { get; }

			// Token: 0x170007C5 RID: 1989
			// (get) Token: 0x06002DCF RID: 11727 RVA: 0x00103B83 File Offset: 0x00101F83
			public AgentActor agentActor { get; }

			// Token: 0x170007C6 RID: 1990
			// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x00103B8B File Offset: 0x00101F8B
			public MerchantActor merchantActor { get; }

			// Token: 0x170007C7 RID: 1991
			// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x00103B93 File Offset: 0x00101F93
			public ICharacterInfo characterInfo { get; }

			// Token: 0x170007C8 RID: 1992
			// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x00103B9B File Offset: 0x00101F9B
			public PlayerData playerData { get; }

			// Token: 0x170007C9 RID: 1993
			// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x00103BA3 File Offset: 0x00101FA3
			public AgentData agentData { get; }

			// Token: 0x170007CA RID: 1994
			// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x00103BAB File Offset: 0x00101FAB
			public MerchantData merchantData { get; }

			// Token: 0x170007CB RID: 1995
			// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x00103BB4 File Offset: 0x00101FB4
			// (set) Token: 0x06002DD6 RID: 11734 RVA: 0x00103C08 File Offset: 0x00102008
			public bool isVisible
			{
				get
				{
					if (this.actor != null && this.type > 0)
					{
						return this.actor.IsVisible;
					}
					return this.chaCtrl != null && this.chaCtrl.visibleAll;
				}
				set
				{
					if (this.actor != null && this.type > 0)
					{
						this.actor.IsVisible = value;
						return;
					}
					if (this.chaCtrl != null)
					{
						this.chaCtrl.visibleAll = value;
						return;
					}
				}
			}

			// Token: 0x170007CC RID: 1996
			// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x00103C5D File Offset: 0x0010205D
			public int sex { get; } = 1;

			// Token: 0x170007CD RID: 1997
			// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x00103C65 File Offset: 0x00102065
			public int voiceNo { get; }

			// Token: 0x170007CE RID: 1998
			// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x00103C6D File Offset: 0x0010206D
			public float voicePitch { get; } = 1f;

			// Token: 0x170007CF RID: 1999
			// (get) Token: 0x06002DDA RID: 11738 RVA: 0x00103C75 File Offset: 0x00102075
			public ChaControl chaCtrl { get; }

			// Token: 0x170007D0 RID: 2000
			// (get) Token: 0x06002DDB RID: 11739 RVA: 0x00103C7D File Offset: 0x0010207D
			public Transform transform { get; }

			// Token: 0x06002DDC RID: 11740 RVA: 0x00103C88 File Offset: 0x00102088
			public ChaControl Create()
			{
				if (this.actor == null)
				{
					return null;
				}
				int type = this.type;
				if (type == 0)
				{
					return (this.sex != 0) ? Singleton<Character>.Instance.CreateChara(1, this.actor.gameObject, 0, null) : Singleton<Character>.Instance.CreateChara(0, this.actor.gameObject, 0, null);
				}
				if (type == 1)
				{
					return Singleton<Character>.Instance.CreateChara(1, this.actor.gameObject, 0, null);
				}
				if (type != 2)
				{
					return null;
				}
				return Singleton<Character>.Instance.CreateChara(1, this.actor.gameObject, 0, null);
			}
		}

		// Token: 0x02000791 RID: 1937
		public class LoopVoicePack
		{
			// Token: 0x06002DDD RID: 11741 RVA: 0x00103D3C File Offset: 0x0010213C
			public LoopVoicePack(int voiceNo, ChaControl chaCtrl, AudioSource audio)
			{
				this.voiceNo = voiceNo;
				this.chaCtrl = chaCtrl;
				this.audio = audio;
			}

			// Token: 0x170007D1 RID: 2001
			// (get) Token: 0x06002DDE RID: 11742 RVA: 0x00103D59 File Offset: 0x00102159
			// (set) Token: 0x06002DDF RID: 11743 RVA: 0x00103D61 File Offset: 0x00102161
			public int voiceNo { get; private set; }

			// Token: 0x170007D2 RID: 2002
			// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x00103D6A File Offset: 0x0010216A
			// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x00103D72 File Offset: 0x00102172
			public ChaControl chaCtrl { get; private set; }

			// Token: 0x170007D3 RID: 2003
			// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x00103D7B File Offset: 0x0010217B
			// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x00103D83 File Offset: 0x00102183
			public AudioSource audio { get; private set; }

			// Token: 0x06002DE4 RID: 11748 RVA: 0x00103D8C File Offset: 0x0010218C
			public bool Set()
			{
				return !(this.chaCtrl == null) && !(this.audio == null);
			}
		}

		// Token: 0x02000792 RID: 1938
		protected class NextInfo
		{
			// Token: 0x06002DE5 RID: 11749 RVA: 0x00103DB3 File Offset: 0x001021B3
			public NextInfo(bool isCompleteDisplayText, bool isNext, bool isSkip)
			{
				this.isCompleteDisplayText = isCompleteDisplayText;
				this.isNext = isNext;
				this.isSkip = isSkip;
			}

			// Token: 0x04002BFD RID: 11261
			public bool isCompleteDisplayText;

			// Token: 0x04002BFE RID: 11262
			public bool isNext;

			// Token: 0x04002BFF RID: 11263
			public bool isSkip;
		}

		// Token: 0x02000793 RID: 1939
		[Serializable]
		private sealed class FileOpen
		{
			// Token: 0x170007D4 RID: 2004
			// (get) Token: 0x06002DE7 RID: 11751 RVA: 0x00103DEE File Offset: 0x001021EE
			public List<RootData> FileList
			{
				[CompilerGenerated]
				get
				{
					return this.fileList;
				}
			}

			// Token: 0x170007D5 RID: 2005
			// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x00103DF6 File Offset: 0x001021F6
			public List<RootData> RootList
			{
				[CompilerGenerated]
				get
				{
					return this.rootList;
				}
			}

			// Token: 0x06002DE9 RID: 11753 RVA: 0x00103DFE File Offset: 0x001021FE
			public void Clear()
			{
				this.fileList.Clear();
				this.rootList.Clear();
			}

			// Token: 0x04002C00 RID: 11264
			[SerializeField]
			private List<RootData> fileList = new List<RootData>();

			// Token: 0x04002C01 RID: 11265
			[SerializeField]
			private List<RootData> rootList = new List<RootData>();
		}
	}
}
