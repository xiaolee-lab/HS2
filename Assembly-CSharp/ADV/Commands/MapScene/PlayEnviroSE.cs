using System;
using Manager;
using UnityEngine;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200074F RID: 1871
	public class PlayEnviroSE : CommandBase
	{
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x000FE300 File Offset: 0x000FC700
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"ID",
					"FadeTime",
					"Index",
					"Pos"
				};
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x000FE328 File Offset: 0x000FC728
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					"0",
					"-1",
					string.Empty
				};
			}
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000FE350 File Offset: 0x000FC750
		public override void Do()
		{
			base.Do();
			int num = 0;
			int clipID = int.Parse(this.args[num++]);
			float fadeTime = float.Parse(this.args[num++]);
			int num2 = int.Parse(this.args[num++]);
			string posStr = string.Empty;
			this.args.SafeProc(num++, delegate(string s)
			{
				posStr = s;
			});
			AudioSource audioSource;
			if (num2 < 0)
			{
				audioSource = Singleton<Manager.Resources>.Instance.SoundPack.PlayEnviroSE(clipID, out num2, fadeTime);
			}
			else
			{
				audioSource = Singleton<Manager.Resources>.Instance.SoundPack.PlayEnviroSE(clipID, num2, fadeTime);
			}
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
