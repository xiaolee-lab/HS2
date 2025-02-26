using System;
using System.Linq;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x02000716 RID: 1814
	public class ItemCreate : CommandBase
	{
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002B42 RID: 11074 RVA: 0x000FA914 File Offset: 0x000F8D14
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"ItemNo",
					"Bundle",
					"Asset",
					"Root",
					"isWorldPositionStays",
					"Manifest",
					"Pos",
					"Ang"
				};
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x000FA970 File Offset: 0x000F8D70
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					"0",
					string.Empty,
					string.Empty,
					string.Empty,
					bool.FalseString,
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000FA9DC File Offset: 0x000F8DDC
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			int key = int.Parse(this.args[num++]);
			string bundle = this.args[num++];
			string asset = this.args[num++];
			string root = this.args[num++];
			bool worldPositionStays = bool.Parse(this.args[num++]);
			string manifest = this.args.SafeGet(num++);
			string text = this.args.SafeGet(num++);
			string text2 = this.args.SafeGet(num++);
			CharaData chara = base.scenario.commandController.GetChara(no);
			Transform transform = null;
			if (!root.IsNullOrEmpty() && chara.chaCtrl != null)
			{
				transform = chara.chaCtrl.transform.GetComponentsInChildren<Transform>(true).FirstOrDefault((Transform p) => p.name == root);
			}
			if (transform == null)
			{
				transform = base.scenario.advScene.transform;
			}
			CharaData.CharaItem charaItem;
			if (chara.itemDic.TryGetValue(key, out charaItem))
			{
				charaItem.Delete();
			}
			charaItem = new CharaData.CharaItem();
			charaItem.LoadObject(bundle, asset, transform, worldPositionStays, manifest);
			Vector3 b;
			if (!base.scenario.commandController.GetV3Dic(text, out b))
			{
				int num2 = 0;
				CommandBase.CountAddV3(text.Split(new char[]
				{
					','
				}), ref num2, ref b);
			}
			charaItem.item.transform.localPosition += b;
			if (!base.scenario.commandController.GetV3Dic(text2, out b))
			{
				int num3 = 0;
				CommandBase.CountAddV3(text2.Split(new char[]
				{
					','
				}), ref num3, ref b);
			}
			charaItem.item.transform.localEulerAngles += b;
			chara.itemDic[key] = charaItem;
		}
	}
}
