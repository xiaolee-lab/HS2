using System;
using UnityEngine;

namespace ADV.Commands.Object
{
	// Token: 0x02000761 RID: 1889
	public class Load : CommandBase
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x000FF393 File Offset: 0x000FD793
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Name",
					"Bundle",
					"Asset",
					"Manifest"
				};
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000FF3BB File Offset: 0x000FD7BB
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000FF3E4 File Offset: 0x000FD7E4
		public override void Do()
		{
			base.Do();
			int num = 0;
			string text = this.args[num++];
			string assetBundleName = this.args[num++];
			string assetName = this.args[num++];
			string manifestAssetBundleName = this.args[num++];
			GameObject asset = AssetBundleManager.LoadAsset(assetBundleName, assetName, typeof(GameObject), manifestAssetBundleName).GetAsset<GameObject>();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(asset);
			if (!text.IsNullOrEmpty())
			{
				gameObject.name = text;
			}
			else
			{
				gameObject.name = asset.name;
			}
			AssetBundleManager.UnloadAssetBundle(assetBundleName, false, manifestAssetBundleName, false);
			base.scenario.commandController.SetObject(gameObject);
		}
	}
}
