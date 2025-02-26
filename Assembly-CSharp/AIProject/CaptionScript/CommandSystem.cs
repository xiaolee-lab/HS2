using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E3D RID: 3645
	public class CommandSystem : MonoBehaviour
	{
		// Token: 0x0600720F RID: 29199 RVA: 0x00307CEC File Offset: 0x003060EC
		public CommandSystem()
		{
			Dictionary<string, IScriptCommand> dictionary = new Dictionary<string, IScriptCommand>();
			dictionary[TextCommand2.DefaultTag] = new TextCommand2();
			dictionary[DisplayOptionsCommand2.DefaultTag] = new DisplayOptionsCommand2();
			dictionary[RandomGotoTagCommand.DefaultTag] = new RandomGotoTagCommand();
			dictionary[EndADVCommand2.DefaultTag] = new EndADVCommand2();
			dictionary[ConditionSpendMoneyCommand.DefaultTag] = new ConditionSpendMoneyCommand();
			dictionary[ConditionOpenAreaIDCommand.DefaultTag] = new ConditionOpenAreaIDCommand();
			dictionary[ConditionChunkIDCommand.DefaultTag] = new ConditionChunkIDCommand();
			dictionary[FuckCommand2.DefaultTag] = new FuckCommand2();
			dictionary[OpenShopCommand.DefaultTag] = new OpenShopCommand();
			this.MerchantCommandTable = dictionary;
			this._scenarioTableList = new List<Dictionary<string, string>>();
			this._currentLine = -1;
			base..ctor();
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06007210 RID: 29200 RVA: 0x00307DB9 File Offset: 0x003061B9
		// (set) Token: 0x06007211 RID: 29201 RVA: 0x00307DC1 File Offset: 0x003061C1
		public Dictionary<string, IScriptCommand> ActorCommandTable { get; set; }

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06007212 RID: 29202 RVA: 0x00307DCA File Offset: 0x003061CA
		// (set) Token: 0x06007213 RID: 29203 RVA: 0x00307DD2 File Offset: 0x003061D2
		public bool CompletedCommand { get; set; }

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06007214 RID: 29204 RVA: 0x00307DDB File Offset: 0x003061DB
		public bool CompletedScenario
		{
			[CompilerGenerated]
			get
			{
				return this._currentLine >= this._scenarioTableList.Count && this.CompletedCommand;
			}
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06007215 RID: 29205 RVA: 0x00307DFC File Offset: 0x003061FC
		// (set) Token: 0x06007216 RID: 29206 RVA: 0x00307E04 File Offset: 0x00306204
		public RawImage RawImage { get; set; }

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06007217 RID: 29207 RVA: 0x00307E0D File Offset: 0x0030620D
		// (set) Token: 0x06007218 RID: 29208 RVA: 0x00307E15 File Offset: 0x00306215
		public ActorCameraControl CameraSystem { get; set; }

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06007219 RID: 29209 RVA: 0x00307E1E File Offset: 0x0030621E
		// (set) Token: 0x0600721A RID: 29210 RVA: 0x00307E26 File Offset: 0x00306226
		public GameObject ObjBack { get; private set; }

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x0600721B RID: 29211 RVA: 0x00307E2F File Offset: 0x0030622F
		// (set) Token: 0x0600721C RID: 29212 RVA: 0x00307E37 File Offset: 0x00306237
		public Transform TransformDepth { get; private set; }

		// Token: 0x0600721D RID: 29213 RVA: 0x00307E40 File Offset: 0x00306240
		private void Awake()
		{
			this.CompletedCommand = false;
		}

		// Token: 0x0600721E RID: 29214 RVA: 0x00307E49 File Offset: 0x00306249
		private void Start()
		{
			(from _ in Observable.EveryUpdate()
			where !this.CompletedScenario
			where this.CompletedCommand
			select _).Subscribe(delegate(long _)
			{
				this.ExecuteNextLine();
			});
		}

		// Token: 0x0600721F RID: 29215 RVA: 0x00307E84 File Offset: 0x00306284
		public bool LoadScenario(string assetbundle, string file)
		{
			this._scenarioTableList.Clear();
			this._currentLine = -1;
			ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundle, file, string.Empty);
			if (excelData == null)
			{
				return false;
			}
			int maxCell = excelData.MaxCell;
			for (int i = 0; i < maxCell; i++)
			{
				ExcelData.Param param = excelData.list[i];
				if (param.list.Count != 0)
				{
					string text = param.list[0];
					if (text.Length != 0)
					{
						int n = text.IndexOf("@");
						if (MathfEx.RangeEqualOn<int>(0, n, 1))
						{
							string key = param.list[0].Replace("'@", string.Empty).Replace("@", string.Empty);
							IScriptCommand scriptCommand;
							if (this.ActorCommandTable.TryGetValue(key, out scriptCommand))
							{
								if (scriptCommand.IsBefore)
								{
									scriptCommand.Execute(scriptCommand.Analysis(param.list), -1);
								}
								else
								{
									this._scenarioTableList.Add(scriptCommand.Analysis(param.list));
								}
							}
						}
					}
				}
			}
			this._currentLine = -1;
			return this._scenarioTableList.Count != 0;
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x00307FD8 File Offset: 0x003063D8
		public void GotoTag(string tag)
		{
			int num = this._scenarioTableList.FindIndex(delegate(Dictionary<string, string> d)
			{
				string a;
				string a2;
				return d.TryGetValue("Tag", out a) && d.TryGetValue("name", out a2) && a == "tag" && a2 == tag;
			});
			if (num >= 0)
			{
				this._currentLine = num;
			}
			else
			{
				this._scenarioTableList.Clear();
			}
			this.CompletedCommand = true;
		}

		// Token: 0x06007221 RID: 29217 RVA: 0x00308030 File Offset: 0x00306430
		public void ExecuteNextLine()
		{
			bool flag = true;
			while (flag)
			{
				this._currentLine++;
				if (this._currentLine >= this._scenarioTableList.Count)
				{
					break;
				}
				string key;
				IScriptCommand scriptCommand;
				if (this._scenarioTableList[this._currentLine].TryGetValue("Tag", out key) && this.ActorCommandTable.TryGetValue(key, out scriptCommand))
				{
					flag = scriptCommand.Execute(this._scenarioTableList[this._currentLine], this._currentLine);
				}
				if (!flag)
				{
					break;
				}
			}
			if (this._scenarioTableList.Count == 0)
			{
				return;
			}
			this.CompletedCommand = false;
		}

		// Token: 0x06007222 RID: 29218 RVA: 0x003080E9 File Offset: 0x003064E9
		public void Clear()
		{
			this._scenarioTableList.Clear();
			this._currentLine = -1;
			this.CompletedCommand = true;
		}

		// Token: 0x06007223 RID: 29219 RVA: 0x00308104 File Offset: 0x00306504
		public void WaitForFading()
		{
			Observable.FromCoroutine(() => this.WaitWhileFading(), false).TakeUntilDestroy(base.gameObject).Subscribe(delegate(Unit _)
			{
			});
		}

		// Token: 0x06007224 RID: 29220 RVA: 0x00308154 File Offset: 0x00306554
		private IEnumerator WaitWhileFading()
		{
			yield return null;
			while (!Singleton<Scene>.Instance.sceneFade.IsEnd)
			{
				yield return null;
			}
			this.CompletedCommand = true;
			yield return null;
			yield break;
		}

		// Token: 0x06007225 RID: 29221 RVA: 0x00308170 File Offset: 0x00306570
		public void WaitSeconds(float duration, bool acceptInput = false)
		{
			Observable.FromCoroutine(() => this.WaitSecondsCoroutine(duration, acceptInput), false).TakeUntilDestroy(base.gameObject).Subscribe(delegate(Unit _)
			{
				this.CompletedCommand = true;
			});
		}

		// Token: 0x06007226 RID: 29222 RVA: 0x003081C8 File Offset: 0x003065C8
		private IEnumerator WaitSecondsCoroutine(float duration, bool acceptInput)
		{
			float elapsedTime = 0f;
			while (duration > elapsedTime)
			{
				yield return null;
				elapsedTime += Time.deltaTime;
				if (acceptInput)
				{
					Manager.Input input = Singleton<Manager.Input>.Instance;
					bool enter = input.IsPressedSubmit();
					bool ctrlSkip = input.IsDown(KeyCode.LeftControl) || input.IsDown(KeyCode.RightControl);
					if (enter || ctrlSkip)
					{
						break;
					}
				}
			}
			yield break;
		}

		// Token: 0x04005D41 RID: 23873
		public const string TagKey = "Tag";

		// Token: 0x04005D42 RID: 23874
		public const string NameKey = "name";

		// Token: 0x04005D43 RID: 23875
		public const string AssetKey = "asset";

		// Token: 0x04005D44 RID: 23876
		public const string FileKey = "file";

		// Token: 0x04005D45 RID: 23877
		public const string NumKey = "num";

		// Token: 0x04005D46 RID: 23878
		public const string AnimationNameKey = "AnimeName";

		// Token: 0x04005D47 RID: 23879
		public const string CrossFadeKey = "Cross Fade";

		// Token: 0x04005D48 RID: 23880
		public const string ConditionsKey = "conditions";

		// Token: 0x04005D49 RID: 23881
		public const string TrueKey = "true";

		// Token: 0x04005D4A RID: 23882
		public const string FalseKey = "false";

		// Token: 0x04005D4B RID: 23883
		public static readonly string[] ReplaceStrings = new string[]
		{
			"'@",
			"@"
		};

		// Token: 0x04005D4D RID: 23885
		public readonly Dictionary<string, IScriptCommand> CommandTable = new Dictionary<string, IScriptCommand>();

		// Token: 0x04005D4E RID: 23886
		public readonly Dictionary<string, IScriptCommand> MerchantCommandTable;

		// Token: 0x04005D54 RID: 23892
		private List<Dictionary<string, string>> _scenarioTableList;

		// Token: 0x04005D55 RID: 23893
		private int _currentLine;
	}
}
