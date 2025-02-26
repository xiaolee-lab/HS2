using System;
using System.Linq;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x02000719 RID: 1817
	public class ItemFind : CommandBase
	{
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002B4E RID: 11086 RVA: 0x000FADF0 File Offset: 0x000F91F0
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"ItemNo",
					"Name"
				};
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x000FAE10 File Offset: 0x000F9210
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					"0",
					"Find"
				};
			}
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000FAE4C File Offset: 0x000F924C
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			int key = int.Parse(this.args[num++]);
			string findName = this.args[num++];
			CharaData chara = base.scenario.commandController.GetChara(no);
			Transform transform = chara.chaCtrl.GetComponentsInChildren<Transform>(true).FirstOrDefault((Transform p) => p.name == findName);
			if (transform != null)
			{
				chara.itemDic[key] = new CharaData.CharaItem(transform.gameObject);
			}
		}
	}
}
