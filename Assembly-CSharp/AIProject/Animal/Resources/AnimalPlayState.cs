using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEx;

namespace AIProject.Animal.Resources
{
	// Token: 0x02000B8B RID: 2955
	public class AnimalPlayState
	{
		// Token: 0x06005808 RID: 22536 RVA: 0x0025ED7C File Offset: 0x0025D17C
		public AnimalPlayState(int _layer, string[] _inStateNames, string[] _outStateNames)
		{
			this.Layer = _layer;
			if (!_inStateNames.IsNullOrEmpty<string>())
			{
				this.MainStateInfo.InStateInfos = new AnimalPlayState.StateInfo[_inStateNames.Length];
				for (int i = 0; i < _inStateNames.Length; i++)
				{
					this.MainStateInfo.InStateInfos[i] = new AnimalPlayState.StateInfo(_inStateNames[i], _layer);
				}
			}
			if (!_outStateNames.IsNullOrEmpty<string>())
			{
				this.MainStateInfo.OutStateInfos = new AnimalPlayState.StateInfo[_outStateNames.Length];
				for (int j = 0; j < _outStateNames.Length; j++)
				{
					this.MainStateInfo.OutStateInfos[j] = new AnimalPlayState.StateInfo(_outStateNames[j], _layer);
				}
			}
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x0025EE5C File Offset: 0x0025D25C
		public AnimalPlayState(int _layer, int _stateID, string[] _inStateNames, string[] _outStateNames)
		{
			this.Layer = _layer;
			this.StateID = _stateID;
			if (!_inStateNames.IsNullOrEmpty<string>())
			{
				this.MainStateInfo.InStateInfos = new AnimalPlayState.StateInfo[_inStateNames.Length];
				for (int i = 0; i < _inStateNames.Length; i++)
				{
					this.MainStateInfo.InStateInfos[i] = new AnimalPlayState.StateInfo(_inStateNames[i], _layer);
				}
			}
			if (!_outStateNames.IsNullOrEmpty<string>())
			{
				this.MainStateInfo.OutStateInfos = new AnimalPlayState.StateInfo[_outStateNames.Length];
				for (int j = 0; j < _outStateNames.Length; j++)
				{
					this.MainStateInfo.OutStateInfos[j] = new AnimalPlayState.StateInfo(_outStateNames[j], _layer);
				}
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x0600580A RID: 22538 RVA: 0x0025EF45 File Offset: 0x0025D345
		// (set) Token: 0x0600580B RID: 22539 RVA: 0x0025EF4D File Offset: 0x0025D34D
		public int StateID { get; set; }

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x0600580C RID: 22540 RVA: 0x0025EF56 File Offset: 0x0025D356
		// (set) Token: 0x0600580D RID: 22541 RVA: 0x0025EF5E File Offset: 0x0025D35E
		public int Layer { get; set; }

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x0600580E RID: 22542 RVA: 0x0025EF67 File Offset: 0x0025D367
		// (set) Token: 0x0600580F RID: 22543 RVA: 0x0025EF6F File Offset: 0x0025D36F
		public AnimalPlayState.PlayStateInfo MainStateInfo { get; private set; } = new AnimalPlayState.PlayStateInfo();

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06005810 RID: 22544 RVA: 0x0025EF78 File Offset: 0x0025D378
		// (set) Token: 0x06005811 RID: 22545 RVA: 0x0025EF80 File Offset: 0x0025D380
		public List<AnimalPlayState.PlayStateInfo> SubStateInfos { get; private set; } = new List<AnimalPlayState.PlayStateInfo>();

		// Token: 0x06005812 RID: 22546 RVA: 0x0025EF8C File Offset: 0x0025D38C
		public void RemakeAnimator()
		{
			AnimalPlayState.PlayStateInfo mainStateInfo = this.MainStateInfo;
			if (mainStateInfo != null)
			{
				mainStateInfo.RemakeAnimator();
			}
			if (!this.SubStateInfos.IsNullOrEmpty<AnimalPlayState.PlayStateInfo>())
			{
				foreach (AnimalPlayState.PlayStateInfo playStateInfo in this.SubStateInfos)
				{
					if (playStateInfo != null)
					{
						playStateInfo.RemakeAnimator();
					}
				}
			}
		}

		// Token: 0x040050E1 RID: 20705
		public float[] FloatList = new float[0];

		// Token: 0x02000B8C RID: 2956
		public class PlayStateInfo
		{
			// Token: 0x17001046 RID: 4166
			// (get) Token: 0x06005814 RID: 22548 RVA: 0x0025F032 File Offset: 0x0025D432
			// (set) Token: 0x06005815 RID: 22549 RVA: 0x0025F03A File Offset: 0x0025D43A
			public AssetBundleInfo AssetBundleInfo { get; set; }

			// Token: 0x17001047 RID: 4167
			// (get) Token: 0x06005816 RID: 22550 RVA: 0x0025F043 File Offset: 0x0025D443
			// (set) Token: 0x06005817 RID: 22551 RVA: 0x0025F04B File Offset: 0x0025D44B
			public RuntimeAnimatorController Controller { get; set; }

			// Token: 0x17001048 RID: 4168
			// (get) Token: 0x06005818 RID: 22552 RVA: 0x0025F054 File Offset: 0x0025D454
			// (set) Token: 0x06005819 RID: 22553 RVA: 0x0025F05C File Offset: 0x0025D45C
			public AnimalPlayState.StateInfo[] InStateInfos { get; set; }

			// Token: 0x17001049 RID: 4169
			// (get) Token: 0x0600581A RID: 22554 RVA: 0x0025F065 File Offset: 0x0025D465
			// (set) Token: 0x0600581B RID: 22555 RVA: 0x0025F06D File Offset: 0x0025D46D
			public AnimalPlayState.StateInfo[] OutStateInfos { get; set; }

			// Token: 0x1700104A RID: 4170
			// (get) Token: 0x0600581C RID: 22556 RVA: 0x0025F076 File Offset: 0x0025D476
			public bool ActiveInState
			{
				[CompilerGenerated]
				get
				{
					return !this.InStateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>() && this.Controller != null;
				}
			}

			// Token: 0x1700104B RID: 4171
			// (get) Token: 0x0600581D RID: 22557 RVA: 0x0025F097 File Offset: 0x0025D497
			public bool ActiveOutState
			{
				[CompilerGenerated]
				get
				{
					return !this.OutStateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>() && this.Controller != null;
				}
			}

			// Token: 0x1700104C RID: 4172
			// (get) Token: 0x0600581E RID: 22558 RVA: 0x0025F0B8 File Offset: 0x0025D4B8
			// (set) Token: 0x0600581F RID: 22559 RVA: 0x0025F0C0 File Offset: 0x0025D4C0
			public bool InFadeEnable { get; set; }

			// Token: 0x1700104D RID: 4173
			// (get) Token: 0x06005820 RID: 22560 RVA: 0x0025F0C9 File Offset: 0x0025D4C9
			// (set) Token: 0x06005821 RID: 22561 RVA: 0x0025F0D1 File Offset: 0x0025D4D1
			public float InFadeSecond { get; set; } = 0.1f;

			// Token: 0x1700104E RID: 4174
			// (get) Token: 0x06005822 RID: 22562 RVA: 0x0025F0DA File Offset: 0x0025D4DA
			// (set) Token: 0x06005823 RID: 22563 RVA: 0x0025F0E2 File Offset: 0x0025D4E2
			public bool OutFadeEnable { get; set; }

			// Token: 0x1700104F RID: 4175
			// (get) Token: 0x06005824 RID: 22564 RVA: 0x0025F0EB File Offset: 0x0025D4EB
			// (set) Token: 0x06005825 RID: 22565 RVA: 0x0025F0F3 File Offset: 0x0025D4F3
			public float OutFadeSecond { get; set; } = 0.1f;

			// Token: 0x17001050 RID: 4176
			// (get) Token: 0x06005826 RID: 22566 RVA: 0x0025F0FC File Offset: 0x0025D4FC
			// (set) Token: 0x06005827 RID: 22567 RVA: 0x0025F104 File Offset: 0x0025D504
			public bool IsLoop { get; set; }

			// Token: 0x17001051 RID: 4177
			// (get) Token: 0x06005828 RID: 22568 RVA: 0x0025F10D File Offset: 0x0025D50D
			// (set) Token: 0x06005829 RID: 22569 RVA: 0x0025F115 File Offset: 0x0025D515
			public int LoopMin { get; set; }

			// Token: 0x17001052 RID: 4178
			// (get) Token: 0x0600582A RID: 22570 RVA: 0x0025F11E File Offset: 0x0025D51E
			// (set) Token: 0x0600582B RID: 22571 RVA: 0x0025F126 File Offset: 0x0025D526
			public int LoopMax { get; set; }

			// Token: 0x0600582C RID: 22572 RVA: 0x0025F12F File Offset: 0x0025D52F
			public void RemakeAnimator()
			{
				if (this.Controller)
				{
				}
			}
		}

		// Token: 0x02000B8D RID: 2957
		public struct StateInfo
		{
			// Token: 0x0600582D RID: 22573 RVA: 0x0025F141 File Offset: 0x0025D541
			public StateInfo(string _animName, int _layer)
			{
				this.animName = _animName;
				this.layer = _layer;
				this.animCode = Animator.StringToHash(_animName);
			}

			// Token: 0x040050ED RID: 20717
			public int layer;

			// Token: 0x040050EE RID: 20718
			public string animName;

			// Token: 0x040050EF RID: 20719
			public int animCode;
		}

		// Token: 0x02000B8E RID: 2958
		public struct ItemInfo
		{
			// Token: 0x040050F0 RID: 20720
			public string parentName;

			// Token: 0x040050F1 RID: 20721
			public AssetBundleInfo itemABInfo;

			// Token: 0x040050F2 RID: 20722
			public AssetBundleInfo animatorABInfo;

			// Token: 0x040050F3 RID: 20723
			public bool isSync;
		}
	}
}
