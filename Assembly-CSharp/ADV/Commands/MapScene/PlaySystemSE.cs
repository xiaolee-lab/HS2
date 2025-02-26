using System;
using AIProject;
using Illusion;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200074D RID: 1869
	public class PlaySystemSE : CommandBase
	{
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x000FE0F8 File Offset: 0x000FC4F8
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Name"
				};
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x000FE108 File Offset: 0x000FC508
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

		// Token: 0x06002C20 RID: 11296 RVA: 0x000FE118 File Offset: 0x000FC518
		public override void Do()
		{
			base.Do();
			string text = this.args[0];
			int se;
			if (!int.TryParse(text, out se))
			{
				se = Illusion.Utils.Enum<SoundPack.SystemSE>.FindIndex(text, true);
			}
			Singleton<Resources>.Instance.SoundPack.Play((SoundPack.SystemSE)se);
		}
	}
}
