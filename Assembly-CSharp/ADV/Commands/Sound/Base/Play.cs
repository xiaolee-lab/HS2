using System;
using Illusion.Game;
using Manager;
using UnityEngine;

namespace ADV.Commands.Sound.Base
{
	// Token: 0x0200076F RID: 1903
	public abstract class Play : CommandBase
	{
		// Token: 0x06002C9B RID: 11419 RVA: 0x000FFDBA File Offset: 0x000FE1BA
		public Play(Sound.Type type)
		{
			this.type = type;
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x000FFDCC File Offset: 0x000FE1CC
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
					"isName",
					"isAsync",
					"SettingNo",
					"isWait",
					"isStop",
					"isLoop",
					"Pitch",
					"PanStereo",
					"SpatialBlend",
					"Time",
					"Volume",
					"Pos",
					"Move",
					"Stop"
				};
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000FFE7C File Offset: 0x000FE27C
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty,
					"0",
					"0",
					bool.TrueString,
					bool.TrueString,
					"-1",
					bool.FalseString,
					bool.FalseString,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000FFF2C File Offset: 0x000FE32C
		public override void Do()
		{
			base.Do();
			Illusion.Game.Utils.Sound.Setting setting = new Illusion.Game.Utils.Sound.Setting(this.type);
			int num = 0;
			setting.assetBundleName = this.args[num++];
			setting.assetName = this.args[num++];
			this.delayTime = (setting.delayTime = float.Parse(this.args[num++]));
			setting.fadeTime = float.Parse(this.args[num++]);
			setting.isAssetEqualPlay = bool.Parse(this.args[num++]);
			setting.isAsync = bool.Parse(this.args[num++]);
			setting.settingNo = int.Parse(this.args[num++]);
			this.isWait = bool.Parse(this.args[num++]);
			this.isStop = bool.Parse(this.args[num++]);
			this.transform = Illusion.Game.Utils.Sound.Play(setting);
			AudioSource audioSource = this.transform.GetComponent<AudioSource>();
			this.args.SafeProc(num++, delegate(string s)
			{
				audioSource.loop = bool.Parse(s);
			});
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
				audioSource.spatialBlend = float.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				audioSource.time = float.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				audioSource.volume = float.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				Vector3 position;
				if (!this.scenario.commandController.V3Dic.TryGetValue(s, out position))
				{
					int num2 = 0;
					CommandBase.CountAddV3(s.Split(new char[]
					{
						','
					}), ref num2, ref position);
				}
				this.transform.position = position;
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				Vector3 value;
				if (!this.scenario.commandController.V3Dic.TryGetValue(s, out value))
				{
					int num2 = 0;
					CommandBase.CountAddV3(s.Split(new char[]
					{
						','
					}), ref num2, ref value);
				}
				this.move = new Vector3?(value);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				this.stopTime = new float?(float.Parse(s));
			});
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x0010014C File Offset: 0x000FE54C
		public override bool Process()
		{
			base.Process();
			if (!this.isWait)
			{
				return true;
			}
			if (this.timer >= this.delayTime)
			{
				if (!Singleton<Sound>.Instance.IsPlay(this.type, this.transform))
				{
					return true;
				}
			}
			else
			{
				this.timer += Time.deltaTime;
			}
			if (this.move != null)
			{
				this.transform.Translate(this.move.Value * Time.deltaTime);
			}
			return this.stopTime != null && this.timer >= this.stopTime.Value;
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x0010020B File Offset: 0x000FE60B
		public override void Result(bool processEnd)
		{
			base.Result(processEnd);
			if (this.isStop)
			{
				Singleton<Sound>.Instance.Stop(this.transform);
			}
		}

		// Token: 0x04002B6A RID: 11114
		private Sound.Type type;

		// Token: 0x04002B6B RID: 11115
		private float delayTime;

		// Token: 0x04002B6C RID: 11116
		private bool isWait;

		// Token: 0x04002B6D RID: 11117
		private bool isStop;

		// Token: 0x04002B6E RID: 11118
		private Transform transform;

		// Token: 0x04002B6F RID: 11119
		private float timer;

		// Token: 0x04002B70 RID: 11120
		private Vector3? move;

		// Token: 0x04002B71 RID: 11121
		private float? stopTime;
	}
}
