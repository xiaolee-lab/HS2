using System;
using Illusion;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x02000724 RID: 1828
	public class MotionSetParam : CommandBase
	{
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x000FBAC5 File Offset: 0x000F9EC5
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Name",
					"Value"
				};
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x000FBAE8 File Offset: 0x000F9EE8
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000FBB24 File Offset: 0x000F9F24
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			string name = this.args[num++];
			string text = this.args[num++];
			Animator animBody = base.scenario.commandController.GetChara(no).chaCtrl.animBody;
			AnimatorControllerParameter animeParam = Illusion.Utils.Animator.GetAnimeParam(name, animBody);
			AnimatorControllerParameterType type = animeParam.type;
			if (type != AnimatorControllerParameterType.Float)
			{
				if (type != AnimatorControllerParameterType.Int)
				{
					if (type == AnimatorControllerParameterType.Bool)
					{
						animBody.SetBool(name, bool.Parse(text));
					}
				}
				else
				{
					animBody.SetInteger(name, int.Parse(text));
				}
			}
			else
			{
				animBody.SetFloat(name, float.Parse(text));
			}
		}
	}
}
