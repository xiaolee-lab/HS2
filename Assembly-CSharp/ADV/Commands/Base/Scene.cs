using System;
using Manager;

namespace ADV.Commands.Base
{
	// Token: 0x020006F6 RID: 1782
	public class Scene : CommandBase
	{
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002A6E RID: 10862 RVA: 0x000F6B7B File Offset: 0x000F4F7B
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Bundle",
					"Asset",
					"isLoad",
					"isAsync",
					"isFade",
					"isOverlap",
					"isImageDraw"
				};
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002A6F RID: 10863 RVA: 0x000F6BBB File Offset: 0x000F4FBB
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty,
					bool.FalseString,
					bool.TrueString,
					bool.TrueString,
					bool.FalseString,
					bool.FalseString
				};
			}
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000F6BFC File Offset: 0x000F4FFC
		public override void Do()
		{
			base.Do();
			int num = 0;
			string assetBundleName = this.args[num++];
			string levelName = this.args[num++];
			bool flag = bool.Parse(this.args[num++]);
			bool isAsync = bool.Parse(this.args[num++]);
			bool isFade = bool.Parse(this.args[num++]);
			bool isOverlap = bool.Parse(this.args[num++]);
			bool isLoadingImageDraw = bool.Parse(this.args[num++]);
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				assetBundleName = assetBundleName,
				levelName = levelName,
				isAdd = !flag,
				isAsync = isAsync,
				isFade = isFade,
				isOverlap = isOverlap
			}, isLoadingImageDraw);
		}
	}
}
