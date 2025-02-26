using System;
using System.Linq;
using ADV.EventCG;
using AIChara;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.EventCG
{
	// Token: 0x02000710 RID: 1808
	public class Setting : CommandBase
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x000FA221 File Offset: 0x000F8621
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Bundle",
					"Asset",
					"No"
				};
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000FA241 File Offset: 0x000F8641
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000FA264 File Offset: 0x000F8664
		public override void Do()
		{
			base.Do();
			Common.Release(base.scenario);
			int num = 0;
			string bundle = this.args[num++];
			string asset = this.args[num++];
			int? no = null;
			this.args.SafeProc(num++, delegate(string s)
			{
				no = new int?(int.Parse(s));
			});
			Action action = delegate()
			{
				GameObject asset = AssetBundleManager.LoadAsset(bundle, asset, typeof(GameObject), null).GetAsset<GameObject>();
				this.data = UnityEngine.Object.Instantiate<GameObject>(asset, this.scenario.commandController.EventCGRoot, false).GetComponent<Data>();
				if (no != null)
				{
					Transform transform = this.data.transform;
					Transform transform2 = this.scenario.commandController.GetChara(no.Value).backup.transform;
					transform.position += transform2.position;
					transform.rotation *= transform2.rotation;
				}
				this.data.name = asset.name;
				AssetBundleManager.UnloadAssetBundle(bundle, false, null, false);
			};
			if (!bundle.IsNullOrEmpty())
			{
				action();
			}
			else
			{
				foreach (string text in CommonLib.GetAssetBundleNameListFromPath("adv/scenario/eventcg/", true))
				{
					if (asset.Check(true, AssetBundleCheck.GetAllAssetName(text, false, null, false)) != -1)
					{
						bundle = text;
						action();
						break;
					}
				}
			}
			base.scenario.commandController.useCorrectCamera = false;
			if (base.scenario.virtualCamera != null)
			{
				this.data.camRoot = base.scenario.virtualCamera.transform;
				if (this.data.cameraData != null)
				{
					CharaData charaData = base.scenario.commandController.Characters.Values.FirstOrDefault((CharaData p) => p.data.isHeroine);
					if (charaData != null)
					{
						ChaControl chaCtrl = charaData.chaCtrl;
						if (!this.data.cameraData.chaCtrlList.Contains(chaCtrl))
						{
							this.data.cameraData.chaCtrlList.Add(chaCtrl);
						}
					}
				}
			}
			else
			{
				this.data.camRoot = base.scenario.AdvCamera.transform;
			}
			CommandController commandController = base.scenario.commandController;
			this.data.SetChaRoot(commandController.CharaRoot, commandController.Characters);
			this.data.Next(0, commandController.Characters);
		}

		// Token: 0x04002B1D RID: 11037
		private Data data;
	}
}
