using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x020008FB RID: 2299
	[Serializable]
	public class PlayState
	{
		// Token: 0x06003FB3 RID: 16307 RVA: 0x0019E1D6 File Offset: 0x0019C5D6
		public PlayState()
		{
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x0019E200 File Offset: 0x0019C600
		public PlayState(int layer, string[] inStateNames, string[] outStateNames)
		{
			this.Layer = layer;
			this.MainStateInfo.InStateInfo = new PlayState.AnimStateInfo();
			if (!inStateNames.IsNullOrEmpty<string>())
			{
				PlayState.Info[] array = new PlayState.Info[inStateNames.Length];
				this.MainStateInfo.InStateInfo.StateInfos = array;
				PlayState.Info[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = new PlayState.Info(inStateNames[i], layer);
				}
			}
			this.MainStateInfo.OutStateInfo = new PlayState.AnimStateInfo();
			if (!outStateNames.IsNullOrEmpty<string>())
			{
				PlayState.Info[] array = new PlayState.Info[outStateNames.Length];
				this.MainStateInfo.OutStateInfo.StateInfos = array;
				PlayState.Info[] array3 = array;
				for (int j = 0; j < array3.Length; j++)
				{
					array3[j] = new PlayState.Info(outStateNames[j], layer);
				}
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x0019E301 File Offset: 0x0019C701
		// (set) Token: 0x06003FB6 RID: 16310 RVA: 0x0019E309 File Offset: 0x0019C709
		public int Layer { get; set; }

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06003FB7 RID: 16311 RVA: 0x0019E312 File Offset: 0x0019C712
		// (set) Token: 0x06003FB8 RID: 16312 RVA: 0x0019E31A File Offset: 0x0019C71A
		public int DirectionType { get; set; }

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x0019E323 File Offset: 0x0019C723
		// (set) Token: 0x06003FBA RID: 16314 RVA: 0x0019E32B File Offset: 0x0019C72B
		public PlayState.PlayStateInfo MainStateInfo { get; private set; } = new PlayState.PlayStateInfo();

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06003FBB RID: 16315 RVA: 0x0019E334 File Offset: 0x0019C734
		// (set) Token: 0x06003FBC RID: 16316 RVA: 0x0019E33C File Offset: 0x0019C73C
		public List<PlayState.PlayStateInfo> SubStateInfos { get; private set; } = new List<PlayState.PlayStateInfo>();

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06003FBD RID: 16317 RVA: 0x0019E345 File Offset: 0x0019C745
		// (set) Token: 0x06003FBE RID: 16318 RVA: 0x0019E34D File Offset: 0x0019C74D
		public ActionInfo ActionInfo { get; set; }

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06003FBF RID: 16319 RVA: 0x0019E356 File Offset: 0x0019C756
		// (set) Token: 0x06003FC0 RID: 16320 RVA: 0x0019E35E File Offset: 0x0019C75E
		public PlayState.Info MaskStateInfo { get; set; }

		// Token: 0x06003FC1 RID: 16321 RVA: 0x0019E368 File Offset: 0x0019C768
		public PlayState.ItemInfo GetItemInfo(int index)
		{
			PlayState.ItemInfo? itemInfo = (this._itemInfos != null) ? new PlayState.ItemInfo?(this._itemInfos.GetElement(index)) : null;
			return (itemInfo == null) ? default(PlayState.ItemInfo) : itemInfo.Value;
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x0019E3C0 File Offset: 0x0019C7C0
		public int ItemInfoCount
		{
			[CompilerGenerated]
			get
			{
				int? num = (this._itemInfos != null) ? new int?(this._itemInfos.Count) : null;
				return (num == null) ? 0 : num.Value;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06003FC3 RID: 16323 RVA: 0x0019E40D File Offset: 0x0019C80D
		// (set) Token: 0x06003FC4 RID: 16324 RVA: 0x0019E415 File Offset: 0x0019C815
		public bool EndEnableBlend { get; set; }

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06003FC5 RID: 16325 RVA: 0x0019E41E File Offset: 0x0019C81E
		// (set) Token: 0x06003FC6 RID: 16326 RVA: 0x0019E426 File Offset: 0x0019C826
		public float EndBlendRate { get; set; }

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06003FC7 RID: 16327 RVA: 0x0019E42F File Offset: 0x0019C82F
		// (set) Token: 0x06003FC8 RID: 16328 RVA: 0x0019E437 File Offset: 0x0019C837
		public bool UseNeckLook { get; set; }

		// Token: 0x06003FC9 RID: 16329 RVA: 0x0019E440 File Offset: 0x0019C840
		~PlayState()
		{
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x0019E46C File Offset: 0x0019C86C
		public void AddItemInfo(PlayState.ItemInfo itemInfo)
		{
			this._itemInfos.Add(itemInfo);
		}

		// Token: 0x04003C29 RID: 15401
		private List<PlayState.ItemInfo> _itemInfos = new List<PlayState.ItemInfo>();

		// Token: 0x020008FC RID: 2300
		[Serializable]
		public class PlayStateInfo
		{
			// Token: 0x17000BE7 RID: 3047
			// (get) Token: 0x06003FCC RID: 16332 RVA: 0x0019E48D File Offset: 0x0019C88D
			// (set) Token: 0x06003FCD RID: 16333 RVA: 0x0019E495 File Offset: 0x0019C895
			public AssetBundleInfo AssetBundleInfo { get; set; }

			// Token: 0x17000BE8 RID: 3048
			// (get) Token: 0x06003FCE RID: 16334 RVA: 0x0019E49E File Offset: 0x0019C89E
			// (set) Token: 0x06003FCF RID: 16335 RVA: 0x0019E4A6 File Offset: 0x0019C8A6
			public PlayState.AnimStateInfo InStateInfo { get; set; }

			// Token: 0x17000BE9 RID: 3049
			// (get) Token: 0x06003FD0 RID: 16336 RVA: 0x0019E4AF File Offset: 0x0019C8AF
			// (set) Token: 0x06003FD1 RID: 16337 RVA: 0x0019E4B7 File Offset: 0x0019C8B7
			public PlayState.AnimStateInfo OutStateInfo { get; set; }

			// Token: 0x17000BEA RID: 3050
			// (get) Token: 0x06003FD2 RID: 16338 RVA: 0x0019E4C0 File Offset: 0x0019C8C0
			// (set) Token: 0x06003FD3 RID: 16339 RVA: 0x0019E4C8 File Offset: 0x0019C8C8
			public float FadeOutTime { get; set; } = 1f;

			// Token: 0x17000BEB RID: 3051
			// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x0019E4D1 File Offset: 0x0019C8D1
			// (set) Token: 0x06003FD5 RID: 16341 RVA: 0x0019E4D9 File Offset: 0x0019C8D9
			public bool IsLoop { get; set; }

			// Token: 0x17000BEC RID: 3052
			// (get) Token: 0x06003FD6 RID: 16342 RVA: 0x0019E4E2 File Offset: 0x0019C8E2
			// (set) Token: 0x06003FD7 RID: 16343 RVA: 0x0019E4EA File Offset: 0x0019C8EA
			public int LoopMin { get; set; }

			// Token: 0x17000BED RID: 3053
			// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x0019E4F3 File Offset: 0x0019C8F3
			// (set) Token: 0x06003FD9 RID: 16345 RVA: 0x0019E4FB File Offset: 0x0019C8FB
			public int LoopMax { get; set; }
		}

		// Token: 0x020008FD RID: 2301
		[Serializable]
		public class AnimStateInfo
		{
			// Token: 0x17000BEE RID: 3054
			// (get) Token: 0x06003FDB RID: 16347 RVA: 0x0019E50C File Offset: 0x0019C90C
			// (set) Token: 0x06003FDC RID: 16348 RVA: 0x0019E514 File Offset: 0x0019C914
			public PlayState.Info[] StateInfos
			{
				get
				{
					return this._stateInfos;
				}
				set
				{
					this._stateInfos = value;
				}
			}

			// Token: 0x17000BEF RID: 3055
			// (get) Token: 0x06003FDD RID: 16349 RVA: 0x0019E51D File Offset: 0x0019C91D
			// (set) Token: 0x06003FDE RID: 16350 RVA: 0x0019E525 File Offset: 0x0019C925
			public bool EnableFade
			{
				get
				{
					return this._enableFade;
				}
				set
				{
					this._enableFade = value;
				}
			}

			// Token: 0x17000BF0 RID: 3056
			// (get) Token: 0x06003FDF RID: 16351 RVA: 0x0019E52E File Offset: 0x0019C92E
			// (set) Token: 0x06003FE0 RID: 16352 RVA: 0x0019E536 File Offset: 0x0019C936
			public float FadeSecond
			{
				get
				{
					return this._fadeTime;
				}
				set
				{
					this._fadeTime = value;
				}
			}

			// Token: 0x04003C36 RID: 15414
			[SerializeField]
			private PlayState.Info[] _stateInfos;

			// Token: 0x04003C37 RID: 15415
			[SerializeField]
			private bool _enableFade;

			// Token: 0x04003C38 RID: 15416
			[SerializeField]
			private float _fadeTime;
		}

		// Token: 0x020008FE RID: 2302
		[Serializable]
		public struct Info
		{
			// Token: 0x06003FE1 RID: 16353 RVA: 0x0019E53F File Offset: 0x0019C93F
			public Info(string name_, int layer_)
			{
				this.stateName = name_;
				this.layer = layer_;
			}

			// Token: 0x17000BF1 RID: 3057
			// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x0019E54F File Offset: 0x0019C94F
			public int ShortNameStateHash
			{
				[CompilerGenerated]
				get
				{
					return Animator.StringToHash(this.stateName);
				}
			}

			// Token: 0x04003C39 RID: 15417
			public string stateName;

			// Token: 0x04003C3A RID: 15418
			public int layer;
		}

		// Token: 0x020008FF RID: 2303
		public struct ItemInfo
		{
			// Token: 0x04003C3B RID: 15419
			public string parentName;

			// Token: 0x04003C3C RID: 15420
			public bool fromEquipedItem;

			// Token: 0x04003C3D RID: 15421
			public int itemID;

			// Token: 0x04003C3E RID: 15422
			public bool isSync;
		}
	}
}
