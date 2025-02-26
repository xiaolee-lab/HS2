using System;
using System.Collections.Generic;
using AIChara;
using Manager;

namespace ADV.Commands.Base
{
	// Token: 0x02000703 RID: 1795
	public class Expression : CommandBase
	{
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x000F84D4 File Offset: 0x000F68D4
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"眉",
					"目",
					"口",
					"眉開き",
					"目開き",
					"口開き",
					"視線",
					"頬赤",
					"ハイライト",
					"涙",
					"瞬き"
				};
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x000F854C File Offset: 0x000F694C
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString()
				};
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000F8578 File Offset: 0x000F6978
		public static List<Expression.Data> Convert(ref string[] args, TextScenario scenario)
		{
			List<Expression.Data> list = new List<Expression.Data>();
			if (args.Length > 1)
			{
				int num = 0;
				while (!args.IsNullOrEmpty(num))
				{
					string check = null;
					args.SafeProc(num + 1, delegate(string s)
					{
						check = s;
					});
					if (check != null)
					{
						int no = int.Parse(args[num]);
						Game.Expression expression = Game.GetExpression(scenario.commandController.expDic, check);
						if (expression != null)
						{
							list.Add(new Expression.Data(no, expression));
							num += 2;
							continue;
						}
						CharaData chara = scenario.commandController.GetChara(no);
						ChaControl chaCtrl = chara.chaCtrl;
						int personality = 0;
						if (chara.data != null)
						{
							VoiceInfo.Param param3;
							if (chara.data.agentData != null)
							{
								VoiceInfo.Param param;
								if (Singleton<Voice>.Instance.voiceInfoDic.TryGetValue(chara.data.chaCtrl.fileParam.personality, out param))
								{
									personality = param.No;
								}
							}
							else if (chara.data.merchantData != null)
							{
								VoiceInfo.Param param2;
								if (Singleton<Voice>.Instance.voiceInfoDic.TryGetValue(-90, out param2))
								{
									personality = param2.No;
								}
							}
							else if (chara.data.playerData != null && Singleton<Voice>.Instance.voiceInfoDic.TryGetValue(-99, out param3))
							{
								personality = param3.No;
							}
						}
						if (Singleton<Game>.IsInstance())
						{
							expression = Singleton<Game>.Instance.GetExpression(personality, check);
							if (expression != null)
							{
								list.Add(new Expression.Data(no, expression));
								num += 2;
								continue;
							}
						}
					}
					list.Add(new Expression.Data(args, ref num)
					{
						IsChangeSkip = true
					});
				}
			}
			return list;
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x000F874C File Offset: 0x000F6B4C
		public override void Do()
		{
			base.Do();
			base.scenario.currentCharaData.CreateExpressionList();
			base.scenario.currentCharaData.expressionList.Add(Expression.Convert(ref this.args, base.scenario).ToArray());
		}

		// Token: 0x02000704 RID: 1796
		public class Data : Game.Expression, TextScenario.IExpression, TextScenario.IChara
		{
			// Token: 0x06002AEC RID: 10988 RVA: 0x000F903A File Offset: 0x000F743A
			public Data(string[] args, ref int cnt) : base(args, ref cnt)
			{
			}

			// Token: 0x06002AED RID: 10989 RVA: 0x000F9044 File Offset: 0x000F7444
			public Data(string[] args) : base(args)
			{
			}

			// Token: 0x06002AEE RID: 10990 RVA: 0x000F904D File Offset: 0x000F744D
			public Data(int no, Game.Expression src)
			{
				this.no = no;
				src.Copy(this);
			}

			// Token: 0x170006AE RID: 1710
			// (get) Token: 0x06002AEF RID: 10991 RVA: 0x000F9063 File Offset: 0x000F7463
			// (set) Token: 0x06002AF0 RID: 10992 RVA: 0x000F906B File Offset: 0x000F746B
			public int no { get; private set; }

			// Token: 0x06002AF1 RID: 10993 RVA: 0x000F9074 File Offset: 0x000F7474
			public override void Initialize(string[] args, ref int cnt, bool isThrow = false)
			{
				try
				{
					this.no = int.Parse(args[cnt++]);
					base.Initialize(args, ref cnt, true);
				}
				catch (Exception)
				{
					if (isThrow)
					{
						throw new Exception(string.Join(",", args));
					}
				}
			}

			// Token: 0x06002AF2 RID: 10994 RVA: 0x000F90D4 File Offset: 0x000F74D4
			public void Play(TextScenario scenario)
			{
				base.Change(this.GetChara(scenario).chaCtrl);
			}

			// Token: 0x06002AF3 RID: 10995 RVA: 0x000F90E8 File Offset: 0x000F74E8
			public CharaData GetChara(TextScenario scenario)
			{
				return scenario.commandController.GetChara(this.no);
			}
		}
	}
}
