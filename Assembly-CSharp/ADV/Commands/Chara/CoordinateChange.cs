using System;
using AIChara;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x0200070C RID: 1804
	public class CoordinateChange : CommandBase
	{
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x000F9EA9 File Offset: 0x000F82A9
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Bundle",
					"Asset"
				};
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002B1E RID: 11038 RVA: 0x000F9EC9 File Offset: 0x000F82C9
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000F9EEC File Offset: 0x000F82EC
		public override void Do()
		{
			base.Do();
			int num = 0;
			this.no = int.Parse(this.args[num++]);
			string assetBundleName = this.args[num++];
			string assetName = this.args[num++];
			TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(assetBundleName, assetName, false, string.Empty);
			if (textAsset != null)
			{
				ChaControl chaCtrl = base.scenario.commandController.GetChara(this.no).chaCtrl;
				chaCtrl.nowCoordinate.LoadFile(textAsset);
				chaCtrl.Reload(false, true, true, true, true);
			}
			AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
		}

		// Token: 0x04002B1C RID: 11036
		private int no;
	}
}
