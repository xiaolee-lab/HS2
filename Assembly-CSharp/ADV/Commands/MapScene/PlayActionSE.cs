using System;
using Illusion;
using Manager;
using UnityEngine;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200074E RID: 1870
	public class PlayActionSE : CommandBase
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000FE161 File Offset: 0x000FC561
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"ID",
					"FadeTime",
					"Type",
					"Pos"
				};
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x000FE18C File Offset: 0x000FC58C
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					"0",
					Sound.Type.GameSE3D.ToString(),
					string.Empty
				};
			}
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000FE1CC File Offset: 0x000FC5CC
		public override void Do()
		{
			base.Do();
			int num = 0;
			int key = int.Parse(this.args[num++]);
			float fadeTime = float.Parse(this.args[num++]);
			string text = this.args[num++];
			string posStr = string.Empty;
			this.args.SafeProc(num++, delegate(string s)
			{
				posStr = s;
			});
			int soundType;
			if (!int.TryParse(text, out soundType))
			{
				soundType = Illusion.Utils.Enum<Sound.Type>.FindIndex(text, true);
			}
			AudioSource audioSource = Singleton<Manager.Resources>.Instance.SoundPack.Play(key, (Sound.Type)soundType, fadeTime);
			if (audioSource != null)
			{
				Vector3 position;
				if (base.scenario.commandController.V3Dic.TryGetValue(posStr, out position))
				{
					audioSource.transform.position = position;
				}
				else if (base.scenario.AdvCamera != null)
				{
					audioSource.transform.position = base.scenario.AdvCamera.transform.position;
				}
			}
		}
	}
}
