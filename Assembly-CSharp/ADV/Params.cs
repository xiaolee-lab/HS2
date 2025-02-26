using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Illusion;

namespace ADV
{
	// Token: 0x020006BE RID: 1726
	public class Params
	{
		// Token: 0x060028C1 RID: 10433 RVA: 0x000F0F3C File Offset: 0x000EF33C
		public Params(object data)
		{
			this.data = data;
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x000F0F56 File Offset: 0x000EF356
		public Params(object data, string header) : this(data)
		{
			this.Initialize(header);
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060028C3 RID: 10435 RVA: 0x000F0F66 File Offset: 0x000EF366
		public virtual List<CommandData> list
		{
			[CompilerGenerated]
			get
			{
				return this._list;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x000F0F6E File Offset: 0x000EF36E
		protected object data { get; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x000F0F76 File Offset: 0x000EF376
		private List<CommandData> _list { get; } = new List<CommandData>();

		// Token: 0x060028C6 RID: 10438 RVA: 0x000F0F80 File Offset: 0x000EF380
		protected virtual void Initialize(string header)
		{
			Params.<Initialize>c__AnonStorey0 <Initialize>c__AnonStorey = new Params.<Initialize>c__AnonStorey0();
			<Initialize>c__AnonStorey.header = header;
			<Initialize>c__AnonStorey.$this = this;
			this._list.AddRange(from info in Illusion.Utils.Type.GetPublicFields(this.data.GetType())
			select new
			{
				info = info,
				command = CommandData.Cast(info.FieldType)
			} into p
			where p.command != CommandData.Command.None
			select new CommandData(p.command, <Initialize>c__AnonStorey.header + p.info.Name, () => p.info.GetValue(<Initialize>c__AnonStorey.data), delegate(object o)
			{
				p.info.SetValue(<Initialize>c__AnonStorey.data, Convert.ChangeType(o, p.info.FieldType));
			}));
			this._list.AddRange((from info in Illusion.Utils.Type.GetPublicProperties(this.data.GetType())
			select new
			{
				info = info,
				command = CommandData.Cast(info.PropertyType)
			} into p
			where p.command != CommandData.Command.None
			select p).Select(delegate(p)
			{
				Func<object> get = null;
				Action<object> set = null;
				if (p.info.CanRead)
				{
					get = (() => p.info.GetValue(<Initialize>c__AnonStorey.data, null));
				}
				if (p.info.CanWrite)
				{
					set = delegate(object o)
					{
						p.info.SetValue(<Initialize>c__AnonStorey.data, o);
					};
				}
				return new CommandData(p.command, <Initialize>c__AnonStorey.header + p.info.Name, get, set);
			}));
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000F1081 File Offset: 0x000EF481
		public virtual void CreateCommand(List<Program.Transfer> transfers)
		{
			CommandData.CreateCommand(transfers, this.list);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000F108F File Offset: 0x000EF48F
		public virtual void Reset(string header = null)
		{
			this._list.Clear();
			this.Initialize(header);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000F10A4 File Offset: 0x000EF4A4
		public virtual void SetParamSync(TextScenario scenario, string key, object value)
		{
			CommandData commandData = this.list.FirstOrDefault((CommandData p) => p.key == key);
			if (commandData == null)
			{
				return;
			}
			ValData valData = new ValData(ValData.Convert(value, commandData.value.GetType()));
			commandData.value = valData.o;
			scenario.Vars[commandData.key] = valData;
			this.UpdateReplaceADV(scenario);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000F111C File Offset: 0x000EF51C
		public virtual void SetADV(TextScenario scenario)
		{
			foreach (CommandData commandData in from p in this.list
			where p.isVar
			select p)
			{
				scenario.Vars[commandData.key] = new ValData(ValData.Convert(commandData.value, commandData.value.GetType()));
			}
			this.UpdateReplaceADV(scenario);
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000F11C4 File Offset: 0x000EF5C4
		public virtual void UpdateReplaceADV(TextScenario scenario)
		{
			foreach (CommandData commandData in from p in this.list
			where p.command == CommandData.Command.Replace
			select p)
			{
				scenario.Replaces[commandData.key] = (string)commandData.value;
			}
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000F1254 File Offset: 0x000EF654
		public virtual void ReceiveADV(TextScenario scenario)
		{
			foreach (CommandData commandData in this.list)
			{
				commandData.ReceiveADV(scenario);
			}
		}
	}
}
