using System;
using AIChara;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x02000752 RID: 1874
	public class SetPresent : PresentBase
	{
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x000FE8EF File Offset: 0x000FCCEF
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No"
				};
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x000FE8FF File Offset: 0x000FCCFF
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0"
				};
			}
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000FE90F File Offset: 0x000FCD0F
		protected override Resources.GameInfoTables.AdvPresentItemInfo GetAdvPresentItemInfo(ChaControl chaCtrl)
		{
			return Singleton<Resources>.Instance.GameInfo.GetAdvPresentInfo(chaCtrl);
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000FE924 File Offset: 0x000FCD24
		public override void Do()
		{
			base.Do();
			int num = 0;
			base.SetPresentInfo(ref num);
		}
	}
}
