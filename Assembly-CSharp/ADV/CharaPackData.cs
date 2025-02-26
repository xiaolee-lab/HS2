using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Manager;

namespace ADV
{
	// Token: 0x020006BF RID: 1727
	public class CharaPackData : IPack
	{
		// Token: 0x060028D4 RID: 10452 RVA: 0x000F1722 File Offset: 0x000EFB22
		public void SetCommandData(params ICommandData[] commandData)
		{
			this.commandData = (from p in commandData
			where p != null
			select p).ToArray<ICommandData>();
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000F1752 File Offset: 0x000EFB52
		public void SetParam(params IParams[] param)
		{
			this.param = (from p in param
			where p != null
			select p).ToArray<IParams>();
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000F1782 File Offset: 0x000EFB82
		public virtual void Init()
		{
			if (Singleton<SoundPlayer>.IsInstance())
			{
				Singleton<SoundPlayer>.Instance.BGMPlayActive = false;
			}
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000F1799 File Offset: 0x000EFB99
		public virtual void Release()
		{
			if (Singleton<SoundPlayer>.IsInstance())
			{
				Singleton<SoundPlayer>.Instance.ActivateMapBGM();
			}
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000F17B0 File Offset: 0x000EFBB0
		public virtual List<Program.Transfer> Create()
		{
			this.Vars = null;
			this._commandList.Clear();
			List<Program.Transfer> list = Program.Transfer.NewList(true, false);
			if (this.commandData != null)
			{
				foreach (ICommandData self in this.commandData)
				{
					self.AddList(this._commandList, "G_");
				}
				CommandData.CreateCommand(list, this._commandList);
			}
			if (this.param != null)
			{
				foreach (IParams @params in this.param)
				{
					@params.param.Reset(null);
					@params.param.CreateCommand(list);
					this._commandList.AddRange(@params.param.list);
				}
			}
			return list;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000F1884 File Offset: 0x000EFC84
		public virtual void Receive(TextScenario scenario)
		{
			foreach (CommandData commandData in this._commandList)
			{
				commandData.ReceiveADV(scenario);
			}
			this.Vars = scenario.Vars;
			Action onComplete = this.onComplete;
			if (onComplete != null)
			{
				onComplete();
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060028DA RID: 10458 RVA: 0x000F1904 File Offset: 0x000EFD04
		// (set) Token: 0x060028DB RID: 10459 RVA: 0x000F190C File Offset: 0x000EFD0C
		public Action onComplete { get; set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060028DC RID: 10460 RVA: 0x000F1915 File Offset: 0x000EFD15
		// (set) Token: 0x060028DD RID: 10461 RVA: 0x000F191D File Offset: 0x000EFD1D
		public Dictionary<string, ValData> Vars { get; set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060028DE RID: 10462 RVA: 0x000F1926 File Offset: 0x000EFD26
		// (set) Token: 0x060028DF RID: 10463 RVA: 0x000F192E File Offset: 0x000EFD2E
		public ICommandData[] commandData { get; private set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x000F1937 File Offset: 0x000EFD37
		// (set) Token: 0x060028E1 RID: 10465 RVA: 0x000F193F File Offset: 0x000EFD3F
		public IParams[] param { get; private set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x000F1948 File Offset: 0x000EFD48
		public IReadOnlyCollection<CommandData> commandList
		{
			[CompilerGenerated]
			get
			{
				return this._commandList;
			}
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000F1950 File Offset: 0x000EFD50
		public void CommandListVisibleEnabled(bool enabled)
		{
			this.isCommandListVisibleEnabled = enabled;
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060028E4 RID: 10468 RVA: 0x000F1959 File Offset: 0x000EFD59
		// (set) Token: 0x060028E5 RID: 10469 RVA: 0x000F1961 File Offset: 0x000EFD61
		private bool isCommandListVisibleEnabled { get; set; }

		// Token: 0x060028E6 RID: 10470 RVA: 0x000F196A File Offset: 0x000EFD6A
		void IPack.CommandListVisibleEnabledDefault()
		{
			this.isCommandListVisibleEnabled = true;
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000F1973 File Offset: 0x000EFD73
		bool IPack.isCommandListVisibleEnabled
		{
			[CompilerGenerated]
			get
			{
				return this.isCommandListVisibleEnabled;
			}
		}

		// Token: 0x04002A51 RID: 10833
		private List<CommandData> _commandList = new List<CommandData>();
	}
}
