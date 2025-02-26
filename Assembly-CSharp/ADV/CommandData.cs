using System;
using System.Collections.Generic;
using Illusion.Elements.Reference;

namespace ADV
{
	// Token: 0x020006BA RID: 1722
	public class CommandData : Pointer<object>
	{
		// Token: 0x060028A9 RID: 10409 RVA: 0x000F0C17 File Offset: 0x000EF017
		public CommandData(CommandData.Command command, string key, Func<object> get, Action<object> set = null) : base(get, set)
		{
			this.command = command;
			this.key = key;
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000F0C30 File Offset: 0x000EF030
		public bool ReceiveADV(TextScenario scenario)
		{
			if (!this.isVar)
			{
				return false;
			}
			ValData valData;
			if (!scenario.Vars.TryGetValue(this.key, out valData))
			{
				return false;
			}
			object obj = (valData != null) ? valData.o : null;
			if (obj == null)
			{
				return false;
			}
			base.value = obj;
			return true;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x000F0C84 File Offset: 0x000EF084
		public bool isVar
		{
			get
			{
				CommandData.Command command = this.command;
				return command != CommandData.Command.None && command != CommandData.Command.Replace;
			}
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000F0CB0 File Offset: 0x000EF0B0
		public static void CreateCommand(List<Program.Transfer> transfers, IReadOnlyCollection<CommandData> collection)
		{
			foreach (CommandData commandData in collection)
			{
				if (commandData.value != null)
				{
					switch (commandData.command)
					{
					case CommandData.Command.Replace:
						transfers.Add(Program.Transfer.Create(true, ADV.Command.Replace, new string[]
						{
							commandData.key,
							(string)commandData.value
						}));
						break;
					case CommandData.Command.Int:
					{
						bool multi = true;
						ADV.Command command = ADV.Command.VAR;
						string[] array = new string[3];
						array[0] = "int";
						array[1] = commandData.key;
						int num = 2;
						object value = commandData.value;
						array[num] = ((value != null) ? value.ToString() : null);
						transfers.Add(Program.Transfer.Create(multi, command, array));
						break;
					}
					case CommandData.Command.String:
					{
						bool multi2 = true;
						ADV.Command command2 = ADV.Command.VAR;
						string[] array2 = new string[3];
						array2[0] = "string";
						array2[1] = commandData.key;
						int num2 = 2;
						object value2 = commandData.value;
						array2[num2] = ((value2 != null) ? value2.ToString() : null);
						transfers.Add(Program.Transfer.Create(multi2, command2, array2));
						break;
					}
					case CommandData.Command.BOOL:
					{
						bool multi3 = true;
						ADV.Command command3 = ADV.Command.VAR;
						string[] array3 = new string[3];
						array3[0] = "bool";
						array3[1] = commandData.key;
						int num3 = 2;
						object value3 = commandData.value;
						array3[num3] = ((value3 != null) ? value3.ToString() : null);
						transfers.Add(Program.Transfer.Create(multi3, command3, array3));
						break;
					}
					case CommandData.Command.FLOAT:
					{
						bool multi4 = true;
						ADV.Command command4 = ADV.Command.VAR;
						string[] array4 = new string[3];
						array4[0] = "float";
						array4[1] = commandData.key;
						int num4 = 2;
						object value4 = commandData.value;
						array4[num4] = ((value4 != null) ? value4.ToString() : null);
						transfers.Add(Program.Transfer.Create(multi4, command4, array4));
						break;
					}
					}
				}
			}
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x000F0E78 File Offset: 0x000EF278
		public static CommandData.Command Cast(object o)
		{
			if (o is string)
			{
				return CommandData.Command.String;
			}
			if (o is int)
			{
				return CommandData.Command.Int;
			}
			if (o is bool)
			{
				return CommandData.Command.BOOL;
			}
			if (o is float)
			{
				return CommandData.Command.FLOAT;
			}
			return CommandData.Command.None;
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x000F0EB0 File Offset: 0x000EF2B0
		public static CommandData.Command Cast(Type type)
		{
			if (type == typeof(string))
			{
				return CommandData.Command.String;
			}
			if (type == typeof(int))
			{
				return CommandData.Command.Int;
			}
			if (type == typeof(bool))
			{
				return CommandData.Command.BOOL;
			}
			if (type == typeof(float))
			{
				return CommandData.Command.FLOAT;
			}
			return CommandData.Command.None;
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x000F0F1A File Offset: 0x000EF31A
		// (set) Token: 0x060028B0 RID: 10416 RVA: 0x000F0F22 File Offset: 0x000EF322
		public CommandData.Command command { get; private set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x000F0F2B File Offset: 0x000EF32B
		// (set) Token: 0x060028B2 RID: 10418 RVA: 0x000F0F33 File Offset: 0x000EF333
		public string key { get; private set; }

		// Token: 0x020006BB RID: 1723
		public enum Command
		{
			// Token: 0x04002A3B RID: 10811
			None,
			// Token: 0x04002A3C RID: 10812
			Replace,
			// Token: 0x04002A3D RID: 10813
			Int,
			// Token: 0x04002A3E RID: 10814
			String,
			// Token: 0x04002A3F RID: 10815
			BOOL,
			// Token: 0x04002A40 RID: 10816
			FLOAT
		}
	}
}
