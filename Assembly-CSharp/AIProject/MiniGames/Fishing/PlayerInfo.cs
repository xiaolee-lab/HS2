using System;
using System.Runtime.CompilerServices;
using AIProject.Player;
using UnityEngine;

namespace AIProject.MiniGames.Fishing
{
	// Token: 0x02000F31 RID: 3889
	public class PlayerInfo
	{
		// Token: 0x0600806D RID: 32877 RVA: 0x00367386 File Offset: 0x00365786
		public PlayerInfo()
		{
			this.root = null;
			this.hand = null;
			this.fishingRod = null;
		}

		// Token: 0x170019C6 RID: 6598
		// (get) Token: 0x0600806E RID: 32878 RVA: 0x003673A3 File Offset: 0x003657A3
		public bool ActivePlayer
		{
			[CompilerGenerated]
			get
			{
				return this.root != null;
			}
		}

		// Token: 0x170019C7 RID: 6599
		// (get) Token: 0x0600806F RID: 32879 RVA: 0x003673B1 File Offset: 0x003657B1
		public bool ActiveHand
		{
			[CompilerGenerated]
			get
			{
				return this.hand != null;
			}
		}

		// Token: 0x170019C8 RID: 6600
		// (get) Token: 0x06008070 RID: 32880 RVA: 0x003673BF File Offset: 0x003657BF
		public bool ActiveFishingRod
		{
			[CompilerGenerated]
			get
			{
				return this.fishingRod != null;
			}
		}

		// Token: 0x170019C9 RID: 6601
		// (get) Token: 0x06008071 RID: 32881 RVA: 0x003673CD File Offset: 0x003657CD
		public bool ActiveLurePos
		{
			[CompilerGenerated]
			get
			{
				return this.lurePos != null;
			}
		}

		// Token: 0x170019CA RID: 6602
		// (get) Token: 0x06008072 RID: 32882 RVA: 0x003673DB File Offset: 0x003657DB
		public bool ActiveFishingRodAnimController
		{
			[CompilerGenerated]
			get
			{
				return this.fishingRodAnimController != null;
			}
		}

		// Token: 0x170019CB RID: 6603
		// (get) Token: 0x06008073 RID: 32883 RVA: 0x003673E9 File Offset: 0x003657E9
		public bool ActiveFishingRodAnimator
		{
			[CompilerGenerated]
			get
			{
				return this.fishingRodAnimator != null;
			}
		}

		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x06008074 RID: 32884 RVA: 0x003673F7 File Offset: 0x003657F7
		public bool ActiveFishingRodInfo
		{
			[CompilerGenerated]
			get
			{
				return this.ActiveFishingRod && this.ActiveLurePos && this.ActiveFishingRodAnimController && this.ActiveFishingRodAnimator;
			}
		}

		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x06008075 RID: 32885 RVA: 0x00367423 File Offset: 0x00365823
		public bool AllActive
		{
			[CompilerGenerated]
			get
			{
				return this.ActivePlayer && this.ActiveHand && this.ActiveFishingRodInfo;
			}
		}

		// Token: 0x06008076 RID: 32886 RVA: 0x00367444 File Offset: 0x00365844
		public void Set(Fishing _playerFishing)
		{
			this.root = _playerFishing.player;
			this.hand = _playerFishing.hand;
		}

		// Token: 0x06008077 RID: 32887 RVA: 0x0036745E File Offset: 0x0036585E
		public bool EqualPlayer(PlayerActor _playerActor)
		{
			return this.root == _playerActor;
		}

		// Token: 0x06008078 RID: 32888 RVA: 0x0036746C File Offset: 0x0036586C
		public bool EqualHand(GameObject _hand)
		{
			return this.hand == _hand;
		}

		// Token: 0x06008079 RID: 32889 RVA: 0x0036747A File Offset: 0x0036587A
		public bool EqualFishingRod(GameObject _fishingRod)
		{
			return this.fishingRod == _fishingRod;
		}

		// Token: 0x0600807A RID: 32890 RVA: 0x00367488 File Offset: 0x00365888
		public bool EqualLurePos(GameObject _lurePos)
		{
			return this.lurePos == _lurePos;
		}

		// Token: 0x0600807B RID: 32891 RVA: 0x00367496 File Offset: 0x00365896
		public bool EqualFishingAnimController(RuntimeAnimatorController _con)
		{
			return this.fishingRodAnimController == _con;
		}

		// Token: 0x0400674E RID: 26446
		public PlayerActor root;

		// Token: 0x0400674F RID: 26447
		public GameObject hand;

		// Token: 0x04006750 RID: 26448
		public GameObject fishingRod;

		// Token: 0x04006751 RID: 26449
		public GameObject lurePos;

		// Token: 0x04006752 RID: 26450
		public RuntimeAnimatorController fishingRodAnimController;

		// Token: 0x04006753 RID: 26451
		public Animator fishingRodAnimator;
	}
}
