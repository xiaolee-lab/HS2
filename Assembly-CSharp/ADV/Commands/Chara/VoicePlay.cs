using System;
using AIChara;
using Illusion;
using Illusion.Extensions;
using Illusion.Game;
using Manager;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x0200072D RID: 1837
	public class VoicePlay : CommandBase
	{
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x000FC608 File Offset: 0x000FAA08
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Type",
					"Bundle",
					"Asset",
					"Delay",
					"Fade",
					"isLoop",
					"isAsync",
					"VoiceNo",
					"Pitch",
					"is2D",
					"useADV"
				};
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002B9D RID: 11165 RVA: 0x000FC680 File Offset: 0x000FAA80
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					VoicePlay.Type.Normal.ToString(),
					string.Empty,
					string.Empty,
					"0",
					"0",
					bool.FalseString,
					bool.TrueString,
					string.Empty,
					string.Empty,
					bool.FalseString,
					bool.TrueString
				};
			}
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000FC710 File Offset: 0x000FAB10
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			string self = this.args[num++];
			int num2 = self.Check(true, Illusion.Utils.Enum<VoicePlay.Type>.Names);
			CharaData chara = base.scenario.commandController.GetChara(no);
			string assetBundleName = this.args[num++];
			string assetName = this.args[num++];
			float delayTime = float.Parse(this.args[num++]);
			float fadeTime = float.Parse(this.args[num++]);
			bool flag = bool.Parse(this.args[num++]);
			bool isAsync = bool.Parse(this.args[num++]);
			int voiceNo = 0;
			bool flag2 = this.args.SafeProc(num++, delegate(string s)
			{
				voiceNo = int.Parse(s);
			});
			float pitch = 1f;
			bool flag3 = this.args.SafeProc(num++, delegate(string s)
			{
				pitch = float.Parse(s);
			});
			bool is2D = bool.Parse(this.args[num++]);
			bool flag4 = bool.Parse(this.args[num++]);
			Illusion.Game.Utils.Voice.Setting setting = new Illusion.Game.Utils.Voice.Setting
			{
				no = voiceNo,
				assetBundleName = assetBundleName,
				assetName = assetName,
				delayTime = delayTime,
				fadeTime = fadeTime,
				isPlayEndDelete = !flag,
				isAsync = isAsync,
				pitch = pitch,
				is2D = is2D
			};
			ChaControl chaControl = null;
			if (chara != null)
			{
				chaControl = chara.chaCtrl;
				if (!flag2)
				{
					setting.no = chara.voiceNo;
				}
				if (!flag3)
				{
					setting.pitch = chara.voicePitch;
				}
				setting.voiceTrans = chara.voiceTrans;
			}
			Transform transform = null;
			VoicePlay.Type type = (VoicePlay.Type)num2;
			if (type != VoicePlay.Type.Normal)
			{
				if (type != VoicePlay.Type.Onece)
				{
					if (type == VoicePlay.Type.Overlap)
					{
						transform = Illusion.Game.Utils.Voice.Play(setting);
					}
				}
				else
				{
					transform = Illusion.Game.Utils.Voice.OnecePlay(setting);
				}
			}
			else
			{
				transform = Illusion.Game.Utils.Voice.OnecePlayChara(setting);
			}
			if (chaControl != null)
			{
				chaControl.SetVoiceTransform(transform);
			}
			if (transform != null)
			{
				AudioSource component = transform.GetComponent<AudioSource>();
				if (component != null)
				{
					if (flag4)
					{
						Singleton<Sound>.Instance.AudioSettingData3DOnly(component, 1);
					}
					if (flag)
					{
						component.loop = flag;
						base.scenario.loopVoiceList.Add(new TextScenario.LoopVoicePack(setting.no, chaControl, component));
					}
				}
			}
		}

		// Token: 0x0200072E RID: 1838
		private enum Type
		{
			// Token: 0x04002B29 RID: 11049
			Normal,
			// Token: 0x04002B2A RID: 11050
			Onece,
			// Token: 0x04002B2B RID: 11051
			Overlap
		}
	}
}
