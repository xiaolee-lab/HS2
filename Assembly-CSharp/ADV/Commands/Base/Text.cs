using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using AIProject;
using Manager;

namespace ADV.Commands.Base
{
	// Token: 0x020006FC RID: 1788
	public class Text : CommandBase
	{
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x000F6F42 File Offset: 0x000F5342
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Name",
					"Text"
				};
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x000F6F5A File Offset: 0x000F535A
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000F6F60 File Offset: 0x000F5360
		public override void Convert(string fileName, ref string[] args)
		{
			int index = 1;
			string text = args.SafeGet(index);
			if (text.IsNullOrEmpty())
			{
				return;
			}
			args[index++] = string.Join("\n", text.Split(new string[]
			{
				"@br"
			}, StringSplitOptions.None));
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000F6FAC File Offset: 0x000F53AC
		public override void Do()
		{
			base.Do();
			Text.Data data = new Text.Data(this.args);
			base.scenario.fontColorKey = data.name;
			if (data.name != string.Empty)
			{
				data.name = base.scenario.ReplaceText(data.name);
			}
			if (data.text != string.Empty)
			{
				data.text = base.scenario.ReplaceText(data.text);
			}
			TextScenario.CurrentCharaData currentCharaData = base.scenario.currentCharaData;
			if (currentCharaData.isSkip || Config.GameData.NextVoiceStop)
			{
				base.scenario.VoicePlay(null, null, null);
			}
			currentCharaData.isSkip = false;
			base.scenario.captionSystem.SetName(data.name);
			base.scenario.captionSystem.SetText(data.text, false);
			string text = null;
			string colorKey = data.colorKey;
			if (colorKey != null)
			{
				if (!(colorKey == "[H]"))
				{
					if (!(colorKey == "[P]"))
					{
						if (colorKey == "[M]")
						{
							CharaData currentChara = base.scenario.currentChara;
							Actor actor;
							if (currentChara == null)
							{
								actor = null;
							}
							else
							{
								TextScenario.ParamData data2 = currentChara.data;
								actor = ((data2 != null) ? data2.merchantActor : null);
							}
							Actor actor2 = actor;
							if (actor2 == null)
							{
								List<TextScenario.ParamData> heroineList = base.scenario.heroineList;
								Actor actor3;
								if (heroineList == null)
								{
									actor3 = null;
								}
								else
								{
									TextScenario.ParamData paramData = heroineList.FirstOrDefault((TextScenario.ParamData p) => p.merchantActor != null);
									actor3 = ((paramData != null) ? paramData.actor : null);
								}
								actor2 = actor3;
							}
							text = MapUIContainer.CharaNameColor(actor2);
						}
					}
					else
					{
						TextScenario.ParamData player = base.scenario.player;
						text = MapUIContainer.CharaNameColor((player != null) ? player.actor : null);
					}
				}
				else
				{
					CharaData currentChara2 = base.scenario.currentChara;
					Actor actor4;
					if (currentChara2 == null)
					{
						actor4 = null;
					}
					else
					{
						TextScenario.ParamData data3 = currentChara2.data;
						actor4 = ((data3 != null) ? data3.actor : null);
					}
					text = MapUIContainer.CharaNameColor(actor4);
				}
			}
			if (text != null)
			{
				data.name = text;
			}
			base.scenario.TextLogCall(data, currentCharaData.voiceList);
			this.next = new Text.Next(base.scenario);
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000F71FB File Offset: 0x000F55FB
		public override bool Process()
		{
			base.Process();
			return this.next.Process();
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000F720F File Offset: 0x000F560F
		public override void Result(bool processEnd)
		{
			base.Result(processEnd);
			this.next.Result();
		}

		// Token: 0x04002AEA RID: 10986
		private Text.Next next;

		// Token: 0x020006FD RID: 1789
		public class Data
		{
			// Token: 0x06002A93 RID: 10899 RVA: 0x000F7234 File Offset: 0x000F5634
			public Data(params string[] args)
			{
				int num = 1;
				int index = num;
				this.colorKey = (this.name = (args.SafeGet(0) ?? string.Empty));
				string text;
				if ((text = args.SafeGet(index)) == null)
				{
					text = (args.SafeGet(num) ?? string.Empty);
				}
				this.text = text;
			}

			// Token: 0x1700068E RID: 1678
			// (get) Token: 0x06002A94 RID: 10900 RVA: 0x000F72AA File Offset: 0x000F56AA
			// (set) Token: 0x06002A95 RID: 10901 RVA: 0x000F72B2 File Offset: 0x000F56B2
			public string colorKey { get; private set; }

			// Token: 0x04002AEC RID: 10988
			public string name = string.Empty;

			// Token: 0x04002AED RID: 10989
			public string text = string.Empty;
		}

		// Token: 0x020006FE RID: 1790
		private class Next
		{
			// Token: 0x06002A96 RID: 10902 RVA: 0x000F72BC File Offset: 0x000F56BC
			public Next(TextScenario scenario)
			{
				Text.Next $this = this;
				this.scenario = scenario;
				TextScenario.CurrentCharaData currentCharaData = scenario.currentCharaData;
				List<TextScenario.IMotion[]> motionList = currentCharaData.motionList;
				List<TextScenario.IExpression[]> expressionList = currentCharaData.expressionList;
				int cnt = 0;
				Func<bool> func = () => !motionList.IsNullOrEmpty<TextScenario.IMotion[]>() && motionList.SafeGet(cnt) != null;
				Func<bool> func2 = () => !expressionList.IsNullOrEmpty<TextScenario.IExpression[]>() && expressionList.SafeGet(cnt) != null;
				while (func() || func2())
				{
					TextScenario.IMotion[] motion = func() ? motionList[cnt] : null;
					TextScenario.IExpression[] expression = func2() ? expressionList[cnt] : null;
					this.playList.Add(new List<Action>
					{
						delegate()
						{
							$this.Play(expression);
						},
						delegate()
						{
							$this.Play(motion);
						}
					});
					cnt++;
				}
				if (!currentCharaData.voiceList.IsNullOrEmpty<TextScenario.IVoice[]>())
				{
					this.onChange = delegate()
					{
						$this.Play();
					};
					scenario.VoicePlay(currentCharaData.voiceList, this.onChange, delegate
					{
						$this.voicePlayEnd = true;
					});
				}
			}

			// Token: 0x06002A97 RID: 10903 RVA: 0x000F7434 File Offset: 0x000F5834
			private bool Play()
			{
				return this.playList.SafeProc(this.currentNo++, delegate(List<Action> p)
				{
					p.ForEach(delegate(Action proc)
					{
						proc();
					});
				});
			}

			// Token: 0x06002A98 RID: 10904 RVA: 0x000F747C File Offset: 0x000F587C
			public bool Process()
			{
				if (this.scenario.currentCharaData.isSkip)
				{
					return true;
				}
				if (this.playList.Count <= this.currentNo && this.voicePlayEnd)
				{
					return true;
				}
				if (this.onChange == null)
				{
					List<TextScenario.IMotion[]> motionList = this.scenario.currentCharaData.motionList;
					bool flag;
					if (this.currentNo == 0)
					{
						flag = true;
					}
					else
					{
						bool flag2 = false;
						if (!motionList.IsNullOrEmpty<TextScenario.IMotion[]>() && this.currentNo < motionList.Count)
						{
							flag2 = !motionList[this.currentNo].IsNullOrEmpty<TextScenario.IMotion>();
						}
						TextScenario.IMotion[] array = (!flag2) ? null : motionList[this.currentNo - 1];
						flag = (array.IsNullOrEmpty<TextScenario.IMotion>() || this.MotionEndCheck(array));
					}
					if (flag && !this.Play())
					{
						bool lastMotionEnd = true;
						bool flag3 = !motionList.IsNullOrEmpty<TextScenario.IMotion[]>();
						if (flag3)
						{
							flag3 = motionList.LastOrDefault<TextScenario.IMotion[]>().SafeProc(delegate(TextScenario.IMotion[] last)
							{
								lastMotionEnd = this.MotionEndCheck(last);
							});
						}
						return !flag3 || lastMotionEnd;
					}
				}
				return false;
			}

			// Token: 0x06002A99 RID: 10905 RVA: 0x000F75C3 File Offset: 0x000F59C3
			public void Result()
			{
				while (this.currentNo < this.playList.Count)
				{
					this.Play();
				}
			}

			// Token: 0x06002A9A RID: 10906 RVA: 0x000F75E8 File Offset: 0x000F59E8
			private void Play(TextScenario.IMotion[] motionList)
			{
				if (motionList.IsNullOrEmpty<TextScenario.IMotion>())
				{
					return;
				}
				this.scenario.CrossFadeStart();
				foreach (TextScenario.IMotion motion in motionList)
				{
					motion.Play(this.scenario);
				}
			}

			// Token: 0x06002A9B RID: 10907 RVA: 0x000F7634 File Offset: 0x000F5A34
			private void Play(TextScenario.IExpression[] expressionList)
			{
				if (expressionList.IsNullOrEmpty<TextScenario.IExpression>())
				{
					return;
				}
				foreach (TextScenario.IExpression expression in expressionList)
				{
					expression.Play(this.scenario);
				}
			}

			// Token: 0x06002A9C RID: 10908 RVA: 0x000F7674 File Offset: 0x000F5A74
			private bool MotionEndCheck(TextScenario.IMotion[] list)
			{
				Func<ChaControl, bool> endCheck = (ChaControl chaCtrl) => chaCtrl.getAnimatorStateInfo(0).normalizedTime >= 1f;
				return list.All((TextScenario.IMotion motion) => endCheck(motion.GetChara(this.scenario).chaCtrl));
			}

			// Token: 0x04002AEF RID: 10991
			private int currentNo;

			// Token: 0x04002AF0 RID: 10992
			private List<List<Action>> playList = new List<List<Action>>();

			// Token: 0x04002AF1 RID: 10993
			private TextScenario scenario;

			// Token: 0x04002AF2 RID: 10994
			private Action onChange;

			// Token: 0x04002AF3 RID: 10995
			private bool voicePlayEnd;
		}
	}
}
