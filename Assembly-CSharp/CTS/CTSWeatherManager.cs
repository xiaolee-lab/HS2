using System;
using UnityEngine;

namespace CTS
{
	// Token: 0x0200069F RID: 1695
	[Serializable]
	public class CTSWeatherManager : MonoBehaviour
	{
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x000EE78B File Offset: 0x000ECB8B
		// (set) Token: 0x06002823 RID: 10275 RVA: 0x000EE793 File Offset: 0x000ECB93
		public float SnowPower
		{
			get
			{
				return this.m_snowPower;
			}
			set
			{
				if (this.m_snowPower != value)
				{
					this.m_snowPower = Mathf.Clamp01(value);
					this.m_somethingChanged = true;
					if (!Application.isPlaying)
					{
						this.BroadcastUpdates();
					}
				}
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x000EE7C4 File Offset: 0x000ECBC4
		// (set) Token: 0x06002825 RID: 10277 RVA: 0x000EE7CC File Offset: 0x000ECBCC
		public float SnowMinHeight
		{
			get
			{
				return this.m_snowMinHeight;
			}
			set
			{
				if (this.m_snowMinHeight != value)
				{
					this.m_snowMinHeight = value;
					if (this.m_snowMinHeight < 0f)
					{
						this.m_snowMinHeight = 0f;
					}
					this.m_somethingChanged = true;
					if (!Application.isPlaying)
					{
						this.BroadcastUpdates();
					}
				}
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x000EE81E File Offset: 0x000ECC1E
		// (set) Token: 0x06002827 RID: 10279 RVA: 0x000EE826 File Offset: 0x000ECC26
		public float RainPower
		{
			get
			{
				return this.m_rainPower;
			}
			set
			{
				if (this.m_rainPower != value)
				{
					this.m_rainPower = Mathf.Clamp01(value);
					this.m_somethingChanged = true;
					if (!Application.isPlaying)
					{
						this.BroadcastUpdates();
					}
				}
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x000EE857 File Offset: 0x000ECC57
		// (set) Token: 0x06002829 RID: 10281 RVA: 0x000EE85F File Offset: 0x000ECC5F
		public float MaxRainSmoothness
		{
			get
			{
				return this.m_maxRainSmoothness;
			}
			set
			{
				if (this.m_maxRainSmoothness != value)
				{
					this.m_maxRainSmoothness = Mathf.Clamp(value, 0f, 30f);
					this.m_somethingChanged = true;
					if (!Application.isPlaying)
					{
						this.BroadcastUpdates();
					}
				}
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x000EE89A File Offset: 0x000ECC9A
		// (set) Token: 0x0600282B RID: 10283 RVA: 0x000EE8A2 File Offset: 0x000ECCA2
		public float Season
		{
			get
			{
				return this.m_season;
			}
			set
			{
				if (this.m_season != value)
				{
					this.m_season = Mathf.Clamp(value, 0f, 3.9999f);
					this.m_somethingChanged = true;
					if (!Application.isPlaying)
					{
						this.BroadcastUpdates();
					}
				}
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x000EE8DD File Offset: 0x000ECCDD
		// (set) Token: 0x0600282D RID: 10285 RVA: 0x000EE8E5 File Offset: 0x000ECCE5
		public Color WinterTint
		{
			get
			{
				return this.m_winterTint;
			}
			set
			{
				if (this.m_winterTint != value)
				{
					this.m_winterTint = value;
					this.m_somethingChanged = true;
					if (!Application.isPlaying)
					{
						this.BroadcastUpdates();
					}
				}
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x000EE916 File Offset: 0x000ECD16
		// (set) Token: 0x0600282F RID: 10287 RVA: 0x000EE91E File Offset: 0x000ECD1E
		public Color SpringTint
		{
			get
			{
				return this.m_springTint;
			}
			set
			{
				if (this.m_springTint != value)
				{
					this.m_springTint = value;
					this.m_somethingChanged = true;
					if (!Application.isPlaying)
					{
						this.BroadcastUpdates();
					}
				}
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x000EE94F File Offset: 0x000ECD4F
		// (set) Token: 0x06002831 RID: 10289 RVA: 0x000EE957 File Offset: 0x000ECD57
		public Color SummerTint
		{
			get
			{
				return this.m_summerTint;
			}
			set
			{
				if (this.m_summerTint != value)
				{
					this.m_summerTint = value;
					this.m_somethingChanged = true;
					if (!Application.isPlaying)
					{
						this.BroadcastUpdates();
					}
				}
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x000EE988 File Offset: 0x000ECD88
		// (set) Token: 0x06002833 RID: 10291 RVA: 0x000EE990 File Offset: 0x000ECD90
		public Color AutumnTint
		{
			get
			{
				return this.m_autumnTint;
			}
			set
			{
				if (this.m_autumnTint != value)
				{
					this.m_autumnTint = value;
					this.m_somethingChanged = true;
					if (!Application.isPlaying)
					{
						this.BroadcastUpdates();
					}
				}
			}
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000EE9C1 File Offset: 0x000ECDC1
		private void Start()
		{
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000EE9C3 File Offset: 0x000ECDC3
		private void LateUpdate()
		{
			this.BroadcastUpdates();
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000EE9CB File Offset: 0x000ECDCB
		private void BroadcastUpdates()
		{
			if (this.m_somethingChanged)
			{
				CTSSingleton<CTSTerrainManager>.Instance.BroadcastWeatherUpdate(this);
				this.m_somethingChanged = false;
			}
		}

		// Token: 0x0400292C RID: 10540
		[SerializeField]
		private float m_snowPower;

		// Token: 0x0400292D RID: 10541
		[SerializeField]
		private float m_snowMinHeight;

		// Token: 0x0400292E RID: 10542
		[SerializeField]
		private float m_rainPower;

		// Token: 0x0400292F RID: 10543
		[SerializeField]
		private float m_maxRainSmoothness = 15f;

		// Token: 0x04002930 RID: 10544
		[SerializeField]
		private float m_season;

		// Token: 0x04002931 RID: 10545
		[SerializeField]
		private Color m_winterTint = Color.white;

		// Token: 0x04002932 RID: 10546
		[SerializeField]
		private Color m_springTint = new Color(0.7372549f, 1f, 0.5882353f);

		// Token: 0x04002933 RID: 10547
		[SerializeField]
		private Color m_summerTint = new Color(1f, 0.7254902f, 0.3764706f);

		// Token: 0x04002934 RID: 10548
		[SerializeField]
		private Color m_autumnTint = Color.white;

		// Token: 0x04002935 RID: 10549
		private bool m_somethingChanged = true;
	}
}
