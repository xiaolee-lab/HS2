using System;
using UnityEngine;

namespace ADV
{
	// Token: 0x0200077B RID: 1915
	[Serializable]
	public class Info
	{
		// Token: 0x04002B80 RID: 11136
		public Info.Audio audio = new Info.Audio();

		// Token: 0x04002B81 RID: 11137
		public Info.Anime anime = new Info.Anime();

		// Token: 0x0200077C RID: 1916
		[Serializable]
		public class Audio
		{
			// Token: 0x04002B82 RID: 11138
			public bool is2D;

			// Token: 0x04002B83 RID: 11139
			public bool isNotMoveMouth;

			// Token: 0x04002B84 RID: 11140
			public Info.Audio.Eco eco = new Info.Audio.Eco();

			// Token: 0x0200077D RID: 1917
			[Serializable]
			public class Eco
			{
				// Token: 0x1700076F RID: 1903
				// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x00100DC1 File Offset: 0x000FF1C1
				// (set) Token: 0x06002CD3 RID: 11475 RVA: 0x00100DC9 File Offset: 0x000FF1C9
				public bool use
				{
					get
					{
						return this._use;
					}
					set
					{
						this._use = value;
					}
				}

				// Token: 0x17000770 RID: 1904
				// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x00100DD2 File Offset: 0x000FF1D2
				// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x00100DDA File Offset: 0x000FF1DA
				public float delay
				{
					get
					{
						return this._delay;
					}
					set
					{
						this._delay = value;
					}
				}

				// Token: 0x17000771 RID: 1905
				// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x00100DE3 File Offset: 0x000FF1E3
				// (set) Token: 0x06002CD7 RID: 11479 RVA: 0x00100DEB File Offset: 0x000FF1EB
				public float decayRatio
				{
					get
					{
						return this._decayRatio;
					}
					set
					{
						this._decayRatio = value;
					}
				}

				// Token: 0x17000772 RID: 1906
				// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x00100DF4 File Offset: 0x000FF1F4
				// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x00100DFC File Offset: 0x000FF1FC
				public float wetMix
				{
					get
					{
						return this._wetMix;
					}
					set
					{
						this._wetMix = value;
					}
				}

				// Token: 0x17000773 RID: 1907
				// (get) Token: 0x06002CDA RID: 11482 RVA: 0x00100E05 File Offset: 0x000FF205
				// (set) Token: 0x06002CDB RID: 11483 RVA: 0x00100E0D File Offset: 0x000FF20D
				public float dryMix
				{
					get
					{
						return this._dryMix;
					}
					set
					{
						this._dryMix = value;
					}
				}

				// Token: 0x04002B85 RID: 11141
				[SerializeField]
				private bool _use;

				// Token: 0x04002B86 RID: 11142
				[SerializeField]
				[Range(10f, 5000f)]
				private float _delay = 50f;

				// Token: 0x04002B87 RID: 11143
				[SerializeField]
				[Range(0f, 1f)]
				private float _decayRatio = 0.5f;

				// Token: 0x04002B88 RID: 11144
				[SerializeField]
				[Range(0f, 1f)]
				private float _wetMix = 1f;

				// Token: 0x04002B89 RID: 11145
				[SerializeField]
				[Range(0f, 1f)]
				private float _dryMix = 1f;
			}
		}

		// Token: 0x0200077E RID: 1918
		[Serializable]
		public class Anime
		{
			// Token: 0x04002B8A RID: 11146
			public Info.Anime.Play play = new Info.Anime.Play();

			// Token: 0x0200077F RID: 1919
			[Serializable]
			public class Play
			{
				// Token: 0x17000774 RID: 1908
				// (get) Token: 0x06002CDE RID: 11486 RVA: 0x00100E47 File Offset: 0x000FF247
				// (set) Token: 0x06002CDF RID: 11487 RVA: 0x00100E4F File Offset: 0x000FF24F
				public float crossFadeTime
				{
					get
					{
						return this._crossFadeTime;
					}
					set
					{
						this._crossFadeTime = value;
					}
				}

				// Token: 0x17000775 RID: 1909
				// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x00100E58 File Offset: 0x000FF258
				// (set) Token: 0x06002CE1 RID: 11489 RVA: 0x00100E60 File Offset: 0x000FF260
				public bool isCrossFade
				{
					get
					{
						return this._isCrossFade;
					}
					set
					{
						this._isCrossFade = value;
					}
				}

				// Token: 0x17000776 RID: 1910
				// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x00100E69 File Offset: 0x000FF269
				// (set) Token: 0x06002CE3 RID: 11491 RVA: 0x00100E71 File Offset: 0x000FF271
				public int layerNo
				{
					get
					{
						return this._layerNo;
					}
					set
					{
						this._layerNo = value;
					}
				}

				// Token: 0x17000777 RID: 1911
				// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x00100E7A File Offset: 0x000FF27A
				// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x00100E82 File Offset: 0x000FF282
				public float transitionDuration
				{
					get
					{
						return this._transitionDuration;
					}
					set
					{
						this._transitionDuration = value;
					}
				}

				// Token: 0x17000778 RID: 1912
				// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x00100E8B File Offset: 0x000FF28B
				// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x00100E93 File Offset: 0x000FF293
				public float normalizedTime
				{
					get
					{
						return this._normalizedTime;
					}
					set
					{
						this._normalizedTime = value;
					}
				}

				// Token: 0x04002B8B RID: 11147
				[Header("Effect")]
				[SerializeField]
				[Range(0f, 10f)]
				private float _crossFadeTime = 0.8f;

				// Token: 0x04002B8C RID: 11148
				[Header("Animation")]
				[SerializeField]
				private bool _isCrossFade;

				// Token: 0x04002B8D RID: 11149
				[SerializeField]
				private int _layerNo;

				// Token: 0x04002B8E RID: 11150
				[SerializeField]
				[Range(0.001f, 3f)]
				private float _transitionDuration = 0.3f;

				// Token: 0x04002B8F RID: 11151
				[SerializeField]
				[Range(0f, 1f)]
				private float _normalizedTime;
			}
		}
	}
}
