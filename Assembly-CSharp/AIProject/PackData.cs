using System;
using System.Collections.Generic;
using ADV;
using AIProject.UI;

namespace AIProject
{
	// Token: 0x02000D21 RID: 3361
	internal class PackData : CharaPackData
	{
		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x06006B79 RID: 27513 RVA: 0x002E1A27 File Offset: 0x002DFE27
		// (set) Token: 0x06006B7A RID: 27514 RVA: 0x002E1A2F File Offset: 0x002DFE2F
		public CommCommandList.CommandInfo[] restoreCommands { get; set; }

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x06006B7B RID: 27515 RVA: 0x002E1A38 File Offset: 0x002DFE38
		public bool isSuccessH
		{
			get
			{
				ValData valData;
				return base.Vars != null && base.Vars.TryGetValue("isSuccessH", out valData) && (bool)valData.o;
			}
		}

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x06006B7C RID: 27516 RVA: 0x002E1A74 File Offset: 0x002DFE74
		// (set) Token: 0x06006B7D RID: 27517 RVA: 0x002E1A7C File Offset: 0x002DFE7C
		public bool isBirthday { get; set; }

		// Token: 0x06006B7E RID: 27518 RVA: 0x002E1A88 File Offset: 0x002DFE88
		public override List<Program.Transfer> Create()
		{
			List<Program.Transfer> list = base.Create();
			list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
			{
				"bool",
				"isBirthday",
				this.isBirthday.ToString()
			}));
			return list;
		}

		// Token: 0x06006B7F RID: 27519 RVA: 0x002E1AD8 File Offset: 0x002DFED8
		public override void Receive(TextScenario scenario)
		{
			base.Receive(scenario);
			if (this.restoreCommands != null)
			{
				CommCommandList commandList = MapUIContainer.CommandList;
				commandList.Refresh(this.restoreCommands, commandList.CanvasGroup, null);
			}
		}
	}
}
