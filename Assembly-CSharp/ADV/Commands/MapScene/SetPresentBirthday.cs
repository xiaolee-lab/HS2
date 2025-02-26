using System;
using AIChara;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x02000753 RID: 1875
	public class SetPresentBirthday : PresentBase
	{
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x000FE949 File Offset: 0x000FCD49
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

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002C39 RID: 11321 RVA: 0x000FE959 File Offset: 0x000FCD59
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

		// Token: 0x06002C3A RID: 11322 RVA: 0x000FE969 File Offset: 0x000FCD69
		protected override Resources.GameInfoTables.AdvPresentItemInfo GetAdvPresentItemInfo(ChaControl chaCtrl)
		{
			return Singleton<Resources>.Instance.GameInfo.GetAdvPresentBirthdayInfo(chaCtrl);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000FE97C File Offset: 0x000FCD7C
		public override void Do()
		{
			base.Do();
			int num = 0;
			base.SetPresentInfo(ref num);
		}
	}
}
