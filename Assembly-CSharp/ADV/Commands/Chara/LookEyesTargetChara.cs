using System;
using AIChara;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x0200071C RID: 1820
	public class LookEyesTargetChara : CommandBase
	{
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x000FB21B File Offset: 0x000F961B
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

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002B5B RID: 11099 RVA: 0x000FB23C File Offset: 0x000F963C
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

		// Token: 0x06002B5C RID: 11100 RVA: 0x000FB278 File Offset: 0x000F9678
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
			chara.chaCtrl.ChangeLookEyesTarget(-1, referenceInfo.transform, 0.5f, 0f, 1f, 2f);
		}
	}
}
