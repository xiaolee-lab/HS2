using System;
using System.Collections.Generic;
using AIChara;
using Illusion.Extensions;
using Illusion.Game;
using Manager;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006FF RID: 1791
	public class Voice : CommandBase
	{
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x000F781F File Offset: 0x000F5C1F
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Bundle",
					"Asset",
					"Personality",
					"Pitch"
				};
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x000F7850 File Offset: 0x000F5C50
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString()
				};
			}
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000F787C File Offset: 0x000F5C7C
		public override void Do()
		{
			base.Do();
			TextScenario.CurrentCharaData currentCharaData = base.scenario.currentCharaData;
			currentCharaData.CreateVoiceList();
			List<Voice.Data> list = new List<Voice.Data>();
			if (this.args.Length > 1)
			{
				int index = 0;
				while (!this.args.IsNullOrEmpty(index))
				{
					Voice.Data data = new Voice.Data(this.args, ref index);
					CharaData chara = base.scenario.commandController.GetChara(data.no);
					if (chara != null)
					{
						data.transform = chara.voiceTrans;
						data.chaCtrl = chara.chaCtrl;
						if (!data.usePersonality)
						{
							data.personality = chara.voiceNo;
						}
						if (!data.usePitch)
						{
							data.pitch = chara.voicePitch;
						}
					}
					data.is2D = base.scenario.info.audio.is2D;
					data.isNotMoveMouth = base.scenario.info.audio.isNotMoveMouth;
					if (base.scenario.info.audio.eco.use)
					{
						data.eco = base.scenario.info.audio.eco.DeepCopy<Info.Audio.Eco>();
					}
					list.Add(data);
				}
			}
			currentCharaData.voiceList.Add(list.ToArray());
			foreach (Voice.Data data2 in list)
			{
				if (data2.bundle.IsNullOrEmpty())
				{
					string bundle;
					if (base.scenario.currentCharaData.bundleVoices.TryGetValue(data2.personality, out bundle))
					{
						data2.bundle = bundle;
					}
				}
				else
				{
					base.scenario.currentCharaData.bundleVoices[data2.personality] = data2.bundle;
				}
			}
		}

		// Token: 0x02000700 RID: 1792
		public class Data : TextScenario.IVoice
		{
			// Token: 0x06002AA4 RID: 10916 RVA: 0x000F7A7C File Offset: 0x000F5E7C
			public Data(string[] args, ref int cnt)
			{
				this.pitch = 1f;
				try
				{
					this.no = int.Parse(args[cnt++]);
					this.bundle = args.SafeGet(cnt++);
					this.asset = args.SafeGet(cnt++);
					this.usePersonality = args.SafeProc(cnt++, delegate(string s)
					{
						this.personality = int.Parse(s);
					});
					this.usePitch = args.SafeProc(cnt++, delegate(string s)
					{
						this.pitch = float.Parse(s);
					});
				}
				catch (Exception)
				{
				}
			}

			// Token: 0x06002AA5 RID: 10917 RVA: 0x000F7B38 File Offset: 0x000F5F38
			public void Convert2D()
			{
				this.transform = null;
			}

			// Token: 0x17000691 RID: 1681
			// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x000F7B41 File Offset: 0x000F5F41
			// (set) Token: 0x06002AA7 RID: 10919 RVA: 0x000F7B49 File Offset: 0x000F5F49
			public int no { get; private set; }

			// Token: 0x17000692 RID: 1682
			// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000F7B52 File Offset: 0x000F5F52
			// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x000F7B5A File Offset: 0x000F5F5A
			public string bundle { get; set; }

			// Token: 0x17000693 RID: 1683
			// (get) Token: 0x06002AAA RID: 10922 RVA: 0x000F7B63 File Offset: 0x000F5F63
			// (set) Token: 0x06002AAB RID: 10923 RVA: 0x000F7B6B File Offset: 0x000F5F6B
			public string asset { get; private set; }

			// Token: 0x17000694 RID: 1684
			// (get) Token: 0x06002AAC RID: 10924 RVA: 0x000F7B74 File Offset: 0x000F5F74
			// (set) Token: 0x06002AAD RID: 10925 RVA: 0x000F7B7C File Offset: 0x000F5F7C
			public int personality { get; set; }

			// Token: 0x17000695 RID: 1685
			// (get) Token: 0x06002AAE RID: 10926 RVA: 0x000F7B85 File Offset: 0x000F5F85
			// (set) Token: 0x06002AAF RID: 10927 RVA: 0x000F7B8D File Offset: 0x000F5F8D
			public float pitch { get; set; }

			// Token: 0x17000696 RID: 1686
			// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x000F7B96 File Offset: 0x000F5F96
			// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x000F7B9E File Offset: 0x000F5F9E
			public ChaControl chaCtrl { get; set; }

			// Token: 0x17000697 RID: 1687
			// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x000F7BA7 File Offset: 0x000F5FA7
			// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x000F7BAF File Offset: 0x000F5FAF
			public Transform transform { get; set; }

			// Token: 0x17000698 RID: 1688
			// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x000F7BB8 File Offset: 0x000F5FB8
			// (set) Token: 0x06002AB5 RID: 10933 RVA: 0x000F7BC0 File Offset: 0x000F5FC0
			public Info.Audio.Eco eco { get; set; }

			// Token: 0x17000699 RID: 1689
			// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x000F7BC9 File Offset: 0x000F5FC9
			// (set) Token: 0x06002AB7 RID: 10935 RVA: 0x000F7BD1 File Offset: 0x000F5FD1
			public bool is2D { get; set; }

			// Token: 0x1700069A RID: 1690
			// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x000F7BDA File Offset: 0x000F5FDA
			// (set) Token: 0x06002AB9 RID: 10937 RVA: 0x000F7BE2 File Offset: 0x000F5FE2
			public bool isNotMoveMouth { get; set; }

			// Token: 0x1700069B RID: 1691
			// (get) Token: 0x06002ABA RID: 10938 RVA: 0x000F7BEB File Offset: 0x000F5FEB
			// (set) Token: 0x06002ABB RID: 10939 RVA: 0x000F7BF3 File Offset: 0x000F5FF3
			public bool usePersonality { get; private set; }

			// Token: 0x1700069C RID: 1692
			// (get) Token: 0x06002ABC RID: 10940 RVA: 0x000F7BFC File Offset: 0x000F5FFC
			// (set) Token: 0x06002ABD RID: 10941 RVA: 0x000F7C04 File Offset: 0x000F6004
			public bool usePitch { get; private set; }

			// Token: 0x1700069D RID: 1693
			// (get) Token: 0x06002ABE RID: 10942 RVA: 0x000F7C0D File Offset: 0x000F600D
			// (set) Token: 0x06002ABF RID: 10943 RVA: 0x000F7C15 File Offset: 0x000F6015
			public AudioSource audio { get; private set; }

			// Token: 0x06002AC0 RID: 10944 RVA: 0x000F7C20 File Offset: 0x000F6020
			public AudioSource Play()
			{
				Illusion.Game.Utils.Voice.Setting s = new Illusion.Game.Utils.Voice.Setting
				{
					no = this.personality,
					assetBundleName = this.bundle,
					assetName = this.asset,
					voiceTrans = this.transform,
					isAsync = false,
					pitch = this.pitch,
					is2D = this.is2D
				};
				Transform transform = Illusion.Game.Utils.Voice.Play(s);
				if (this.eco != null)
				{
					AudioEchoFilter audioEchoFilter = (transform != null) ? transform.gameObject.AddComponent<AudioEchoFilter>() : null;
					audioEchoFilter.delay = this.eco.delay;
					audioEchoFilter.decayRatio = this.eco.decayRatio;
					audioEchoFilter.wetMix = this.eco.wetMix;
					audioEchoFilter.dryMix = this.eco.dryMix;
				}
				if (this.chaCtrl != null && this.transform != null)
				{
					this.chaCtrl.SetVoiceTransform((!this.isNotMoveMouth) ? transform : null);
				}
				this.audio = ((transform != null) ? transform.GetComponent<AudioSource>() : null);
				if (this.audio != null && this.transform != null)
				{
					Singleton<Sound>.Instance.AudioSettingData3DOnly(this.audio, 1);
				}
				return this.audio;
			}

			// Token: 0x06002AC1 RID: 10945 RVA: 0x000F7D7D File Offset: 0x000F617D
			public bool Wait()
			{
				return Singleton<Voice>.Instance.IsVoiceCheck(this.personality, this.transform, false);
			}
		}
	}
}
