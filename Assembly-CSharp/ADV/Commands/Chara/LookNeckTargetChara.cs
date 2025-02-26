using System;
using AIChara;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x0200071F RID: 1823
	public class LookNeckTargetChara : CommandBase
	{
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000FB69F File Offset: 0x000F9A9F
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"TargetNo",
					"Key"
				};
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000FB6C0 File Offset: 0x000F9AC0
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					"0",
					string.Empty
				};
			}
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000FB6FC File Offset: 0x000F9AFC
		public override void Do()
		{
			base.Do();
			int num = 0;
			CharaData chara = base.scenario.commandController.GetChara(int.Parse(this.args[num++]));
			CharaData chara2 = base.scenario.commandController.GetChara(int.Parse(this.args[num++]));
			string text = this.args[num++];
			int key;
			if (!int.TryParse(text, out key))
			{
				key = text.Check(true, Enum.GetNames(typeof(ChaReference.RefObjKey)));
			}
			GameObject referenceInfo = chara2.chaCtrl.GetReferenceInfo((ChaReference.RefObjKey)key);
			chara.chaCtrl.ChangeLookNeckTarget(-1, referenceInfo.transform, 0.5f, 0f, 1f, 0.8f);
		}
	}
}
