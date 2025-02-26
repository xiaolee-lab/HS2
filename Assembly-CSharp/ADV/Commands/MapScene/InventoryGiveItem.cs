using System;
using AIProject;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200075B RID: 1883
	public class InventoryGiveItem : InventoryBase
	{
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x000FEF27 File Offset: 0x000FD327
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"MyNo",
					"TargetNo"
				};
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x000FEF3F File Offset: 0x000FD33F
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"-1",
					string.Empty
				};
			}
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000FEF58 File Offset: 0x000FD358
		public override void Do()
		{
			base.Do();
			int num = 0;
			this.myChara = this.GetChara(ref num);
			if (this.myChara == null)
			{
				return;
			}
			this.targetChara = this.GetChara(ref num);
			base.OpenInventory(num++, this.myChara.data.characterInfo.ItemList);
			base.scenario.regulate.AddRegulate(Regulate.Control.Next);
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000FEFC6 File Offset: 0x000FD3C6
		public override bool Process()
		{
			base.Process();
			return base.isClose;
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000FEFD8 File Offset: 0x000FD3D8
		public override void Result(bool processEnd)
		{
			base.Result(processEnd);
			base.scenario.regulate.SubRegulate(Regulate.Control.Next);
			this.isGive = base.scenario.AddItemVars(base.Item);
			base.scenario.Vars["isGive"] = new ValData(this.isGive);
			if (this.isGive && this.targetChara != null)
			{
				this.targetChara.data.characterInfo.ItemList.AddItem(base.Item);
			}
		}

		// Token: 0x04002B51 RID: 11089
		private bool isGive;

		// Token: 0x04002B52 RID: 11090
		private CharaData myChara;

		// Token: 0x04002B53 RID: 11091
		private CharaData targetChara;
	}
}
