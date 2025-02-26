using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject;

namespace ADV
{
	// Token: 0x020006BD RID: 1725
	public class CharaParams : Params
	{
		// Token: 0x060028B4 RID: 10420 RVA: 0x000F14D9 File Offset: 0x000EF8D9
		public CharaParams(ICommandData commandData, string HEADER) : base(commandData)
		{
			this.HEADER = HEADER;
			this.Initialize(this.HEADER_PARAM);
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x000F1500 File Offset: 0x000EF900
		public string HEADER { get; } = string.Empty;

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x000F1508 File Offset: 0x000EF908
		public string HEADER_PARAM
		{
			get
			{
				return this.HEADER + "_";
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060028B7 RID: 10423 RVA: 0x000F151C File Offset: 0x000EF91C
		public override List<CommandData> list
		{
			[CompilerGenerated]
			get
			{
				List<CommandData> result;
				if ((result = this.cachedList) == null)
				{
					result = (this.cachedList = base.list.Concat(this.addList).ToList<CommandData>());
				}
				return result;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x000F1558 File Offset: 0x000EF958
		public List<CommandData> addList
		{
			get
			{
				if (this._addList != null)
				{
					return this._addList;
				}
				this._addList = new List<CommandData>(((ICommandData)base.data).CreateCommandData(this.HEADER_PARAM));
				this.CreateCommandData_Actor(this._addList);
				return this._addList;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060028B9 RID: 10425 RVA: 0x000F15AC File Offset: 0x000EF9AC
		public int charaID
		{
			get
			{
				if (this.actor == null)
				{
					return 0;
				}
				if (this.actor is AgentActor && this.actor != null && this.actor.ChaControl != null && this.actor.ChaControl.chaFile != null && this.actor.ChaControl.chaFile.parameter != null)
				{
					return this.actor.ChaControl.chaFile.parameter.personality;
				}
				return this.actor.ID;
			}
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000F1658 File Offset: 0x000EFA58
		public void Bind(Actor actor)
		{
			this.actor = actor;
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x000F1661 File Offset: 0x000EFA61
		// (set) Token: 0x060028BC RID: 10428 RVA: 0x000F1669 File Offset: 0x000EFA69
		public Actor actor { get; private set; }

		// Token: 0x060028BD RID: 10429 RVA: 0x000F1674 File Offset: 0x000EFA74
		public void CreateCommandData_Actor(List<CommandData> list)
		{
			if (this.actor == null)
			{
				return;
			}
			list.Add(new CommandData(CommandData.Command.String, this.HEADER_PARAM + string.Format("[{0}]", "CharaName"), () => this.actor.CharaName, null));
			list.Add(new CommandData(CommandData.Command.Replace, this.HEADER, () => this.actor.CharaName, null));
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000F16E5 File Offset: 0x000EFAE5
		public override void Reset(string header = null)
		{
			this._addList = null;
			this.cachedList = null;
		}

		// Token: 0x04002A42 RID: 10818
		private List<CommandData> cachedList;

		// Token: 0x04002A43 RID: 10819
		private List<CommandData> _addList;
	}
}
