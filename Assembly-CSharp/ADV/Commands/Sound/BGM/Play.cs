using System;
using Illusion.Game;
using Manager;
using UnityEngine;

namespace ADV.Commands.Sound.BGM
{
	// Token: 0x02000771 RID: 1905
	public class Play : CommandBase
	{
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x0010041C File Offset: 0x000FE81C
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Bundle",
					"Asset",
					"Delay",
					"Fade",
					"isAsync",
					"Pitch",
					"PanStereo",
					"Time",
					"Volume"
				};
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x00100478 File Offset: 0x000FE878
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"sound/data/bgm/00.unity3d",
					string.Empty,
					"0",
					string.Empty,
					bool.TrueString,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x001004D4 File Offset: 0x000FE8D4
		public override void Do()
		{
			base.Do();
			int num = 0;
			string assetBundleName = this.args[num++];
			Illusion.Game.Utils.Sound.SettingBGM setting = new Illusion.Game.Utils.Sound.SettingBGM(assetBundleName);
			this.args.SafeProc(num++, delegate(string s)
			{
				setting.assetName = s;
			});
			setting.delayTime = float.Parse(this.args[num++]);
			this.args.SafeProc(num++, delegate(string s)
			{
				setting.fadeTime = float.Parse(s);
			});
			setting.isAsync = bool.Parse(this.args[num++]);
			if (Singleton<SoundPlayer>.IsInstance())
			{
				Singleton<SoundPlayer>.Instance.MuteHousingAreaBGM(setting.fadeTime, false);
			}
			AudioSource audioSource = Illusion.Game.Utils.Sound.Play(setting).GetComponent<AudioSource>();
			this.args.SafeProc(num++, delegate(string s)
			{
				audioSource.pitch = float.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				audioSource.panStereo = float.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				audioSource.time = float.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				audioSource.volume = float.Parse(s);
			});
		}
	}
}
