using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace ADV
{
	// Token: 0x020006D2 RID: 1746
	public abstract class CommandBase : ICommand
	{
		// Token: 0x060029AD RID: 10669 RVA: 0x000F4228 File Offset: 0x000F2628
		public void Set(Command command)
		{
			this.command = command;
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000F4231 File Offset: 0x000F2631
		public static string[] RemoveArgsEmpty(string[] args)
		{
			string[] result;
			if (args == null)
			{
				result = null;
			}
			else
			{
				result = (from s in args
				where !s.IsNullOrEmpty()
				select s).ToArray<string>();
			}
			return result;
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x000F4267 File Offset: 0x000F2667
		public string[] RemoveArgsEmpty()
		{
			return CommandBase.RemoveArgsEmpty(this.args);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000F4274 File Offset: 0x000F2674
		public static string[] GetArgToSplit(int cnt, params string[] args)
		{
			string[] ret = null;
			args.SafeProc(cnt, delegate(string s)
			{
				ret = s.Split(new char[]
				{
					','
				});
			});
			return ret;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000F42A8 File Offset: 0x000F26A8
		public string[] GetArgToSplit(int cnt)
		{
			return CommandBase.GetArgToSplit(cnt, this.args);
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000F42B8 File Offset: 0x000F26B8
		public static string[] GetArgToSplitLast(int cnt, params string[] args)
		{
			List<string> list = new List<string>();
			for (;;)
			{
				string[] argToSplit = CommandBase.GetArgToSplit(cnt++, args);
				if (argToSplit.IsNullOrEmpty<string>())
				{
					break;
				}
				list.AddRange(argToSplit);
			}
			return list.ToArray();
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000F42FA File Offset: 0x000F26FA
		public string[] GetArgToSplitLast(int cnt)
		{
			return CommandBase.GetArgToSplitLast(cnt, this.args);
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000F4308 File Offset: 0x000F2708
		public static string[][] GetArgToSplitLastTable(int cnt, params string[] args)
		{
			List<string[]> list = new List<string[]>();
			for (;;)
			{
				string[] argToSplit = CommandBase.GetArgToSplit(cnt++, args);
				if (argToSplit.IsNullOrEmpty<string>())
				{
					break;
				}
				list.Add(argToSplit);
			}
			return list.ToArray();
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000F434A File Offset: 0x000F274A
		public string[][] GetArgToSplitLastTable(int cnt)
		{
			return CommandBase.GetArgToSplitLastTable(cnt, this.args);
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x000F4358 File Offset: 0x000F2758
		// (set) Token: 0x060029B7 RID: 10679 RVA: 0x000F4360 File Offset: 0x000F2760
		public int localLine { get; set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060029B8 RID: 10680 RVA: 0x000F4369 File Offset: 0x000F2769
		// (set) Token: 0x060029B9 RID: 10681 RVA: 0x000F4371 File Offset: 0x000F2771
		public TextScenario scenario { get; private set; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x000F437A File Offset: 0x000F277A
		// (set) Token: 0x060029BB RID: 10683 RVA: 0x000F4382 File Offset: 0x000F2782
		public Command command { get; private set; }

		// Token: 0x060029BC RID: 10684 RVA: 0x000F438C File Offset: 0x000F278C
		public void Initialize(TextScenario scenario, Command command, string[] args)
		{
			this.scenario = scenario;
			this.command = command;
			string[] argsDefault = this.ArgsDefault;
			if (argsDefault != null)
			{
				int a = argsDefault.Length;
				int num = args.Length;
				int num2 = Mathf.Min(a, num);
				for (int i = 0; i < num2; i++)
				{
					if (!args[i].IsNullOrEmpty())
					{
						argsDefault[i] = args[i];
					}
				}
				List<string> list = new List<string>(argsDefault);
				for (int j = list.Count; j < num; j++)
				{
					list.Add(args[j]);
				}
				this.args = list.ToArray();
			}
			else
			{
				this.args = args.ToArray<string>();
			}
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000F443D File Offset: 0x000F283D
		public virtual void Convert(string fileName, ref string[] args)
		{
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000F443F File Offset: 0x000F283F
		public virtual void ConvertBeforeArgsProc()
		{
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060029BF RID: 10687
		public abstract string[] ArgsLabel { get; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060029C0 RID: 10688
		public abstract string[] ArgsDefault { get; }

		// Token: 0x060029C1 RID: 10689 RVA: 0x000F4444 File Offset: 0x000F2844
		protected static void CountAddV3(string[] args, ref int cnt, ref Vector3 v)
		{
			if (args == null)
			{
				return;
			}
			for (int i = 0; i < 3; i++)
			{
				float value;
				if (float.TryParse(args.SafeGet(cnt++), out value))
				{
					v[i] = value;
				}
			}
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000F448D File Offset: 0x000F288D
		protected static void CountAddV3(ref int cnt)
		{
			cnt += 3;
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000F4495 File Offset: 0x000F2895
		protected static Vector3 LerpV3(Vector3 start, Vector3 end, float t)
		{
			return Vector3.Lerp(start, end, t);
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000F44A0 File Offset: 0x000F28A0
		protected static Vector3 LerpAngleV3(Vector3 start, Vector3 end, float t)
		{
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < 3; i++)
			{
				zero[i] = Mathf.LerpAngle(start[i], end[i], t);
			}
			return zero;
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000F44E4 File Offset: 0x000F28E4
		public virtual void Do()
		{
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000F44E6 File Offset: 0x000F28E6
		public virtual bool Process()
		{
			return true;
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000F44E9 File Offset: 0x000F28E9
		public virtual void Result(bool processEnd)
		{
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000F44EB File Offset: 0x000F28EB
		[Conditional("ADV_DEBUG")]
		protected void ErrorCheckLog(bool isError, string message)
		{
			if (isError)
			{
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000F44F3 File Offset: 0x000F28F3
		[Conditional("ADV_DEBUG")]
		private void dbPrint(string procName, string[] command)
		{
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000F44F5 File Offset: 0x000F28F5
		[Conditional("__DEBUG_PROC__")]
		private void dbPrintDebug(string procName, string[] command)
		{
		}

		// Token: 0x04002AA6 RID: 10918
		public string[] args;

		// Token: 0x04002AA7 RID: 10919
		public const int currentCharaDefaultIndex = 2147483647;
	}
}
