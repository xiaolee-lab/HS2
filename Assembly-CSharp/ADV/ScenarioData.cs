using System;
using System.Collections.Generic;
using System.Linq;
using Illusion.Extensions;
using UnityEngine;

namespace ADV
{
	// Token: 0x02000795 RID: 1941
	public class ScenarioData : ScriptableObject
	{
		// Token: 0x06002DEB RID: 11755 RVA: 0x001044A8 File Offset: 0x001028A8
		private static bool MultiForce(Command command)
		{
			switch (command)
			{
			case Command.Voice:
			case Command.Motion:
			case Command.Expression:
			case Command.ExpressionIcon:
				break;
			default:
				switch (command)
				{
				case Command.VAR:
				case Command.RandomVar:
				case Command.Calc:
					break;
				default:
					if (command != Command.FormatVAR && command != Command.CharaKaraokePlay && command != Command.Format)
					{
						return false;
					}
					break;
				}
				break;
			}
			return true;
		}

		// Token: 0x04002CF6 RID: 11510
		[SerializeField]
		public List<ScenarioData.Param> list = new List<ScenarioData.Param>();

		// Token: 0x02000796 RID: 1942
		[Serializable]
		public class Param
		{
			// Token: 0x06002DEC RID: 11756 RVA: 0x00104508 File Offset: 0x00102908
			public Param(bool multi, Command command, params string[] args)
			{
				this._multi = multi;
				this._command = command;
				this._args = args;
			}

			// Token: 0x06002DED RID: 11757 RVA: 0x00104525 File Offset: 0x00102925
			public Param(params string[] args)
			{
				this.Initialize(args);
			}

			// Token: 0x170007D6 RID: 2006
			// (get) Token: 0x06002DEE RID: 11758 RVA: 0x00104534 File Offset: 0x00102934
			public int Hash
			{
				get
				{
					return this._hash;
				}
			}

			// Token: 0x170007D7 RID: 2007
			// (get) Token: 0x06002DEF RID: 11759 RVA: 0x0010453C File Offset: 0x0010293C
			public int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x170007D8 RID: 2008
			// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x00104544 File Offset: 0x00102944
			public bool Multi
			{
				get
				{
					return this._multi;
				}
			}

			// Token: 0x170007D9 RID: 2009
			// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x0010454C File Offset: 0x0010294C
			public Command Command
			{
				get
				{
					return this._command;
				}
			}

			// Token: 0x170007DA RID: 2010
			// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x00104554 File Offset: 0x00102954
			public string[] Args
			{
				get
				{
					return this._args;
				}
			}

			// Token: 0x06002DF3 RID: 11763 RVA: 0x0010455C File Offset: 0x0010295C
			public void SetHash(int hash)
			{
				this._hash = hash;
			}

			// Token: 0x06002DF4 RID: 11764 RVA: 0x00104568 File Offset: 0x00102968
			public IEnumerable<string> Output()
			{
				return new string[]
				{
					this._hash.ToString(),
					this._version.ToString(),
					this._multi.ToString(),
					this._command.ToString()
				}.Concat(this._args);
			}

			// Token: 0x06002DF5 RID: 11765 RVA: 0x001045D8 File Offset: 0x001029D8
			private void Initialize(params string[] args)
			{
				int count = 1;
				bool flag = bool.TryParse(args[count++], out this._multi);
				string self = args.SafeGet(count++);
				try
				{
					this._command = (Command)Enum.ToObject(typeof(Command), self.Check(true, Enum.GetNames(typeof(Command))));
				}
				catch (Exception)
				{
					throw new Exception("CommandError:" + string.Join(",", (from s in args
					select (!s.IsNullOrEmpty()) ? s : "(null)").ToArray<string>()));
				}
				if (!flag)
				{
					this._multi |= ScenarioData.MultiForce(this._command);
				}
				this._args = ScenarioData.Param.ConvertAnalyze(this._command, args.Skip(count).ToArray<string>().LastStringEmptySpaceRemove(), null);
			}

			// Token: 0x06002DF6 RID: 11766 RVA: 0x001046D0 File Offset: 0x00102AD0
			private static string[] ConvertAnalyze(Command command, string[] args, string fileName)
			{
				CommandBase commandBase = CommandList.CommandGet(command);
				if (commandBase != null)
				{
					commandBase.Convert(fileName, ref args);
				}
				return args.LastStringEmptySpaceRemove();
			}

			// Token: 0x04002CF7 RID: 11511
			[SerializeField]
			private int _hash;

			// Token: 0x04002CF8 RID: 11512
			[SerializeField]
			private int _version;

			// Token: 0x04002CF9 RID: 11513
			[SerializeField]
			private bool _multi;

			// Token: 0x04002CFA RID: 11514
			[SerializeField]
			private Command _command;

			// Token: 0x04002CFB RID: 11515
			[SerializeField]
			private string[] _args;
		}
	}
}
