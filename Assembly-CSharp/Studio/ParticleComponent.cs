using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001264 RID: 4708
	public class ParticleComponent : MonoBehaviour
	{
		// Token: 0x17002159 RID: 8537
		// (get) Token: 0x06009BCF RID: 39887 RVA: 0x003FBB3A File Offset: 0x003F9F3A
		public bool[] UseColor
		{
			[CompilerGenerated]
			get
			{
				bool[] array = new bool[3];
				array[0] = this.UseColor1;
				return array;
			}
		}

		// Token: 0x1700215A RID: 8538
		// (get) Token: 0x06009BD0 RID: 39888 RVA: 0x003FBB4B File Offset: 0x003F9F4B
		public bool UseColor1
		{
			[CompilerGenerated]
			get
			{
				return !this.particleColor1.IsNullOrEmpty<ParticleSystem>();
			}
		}

		// Token: 0x1700215B RID: 8539
		// (get) Token: 0x06009BD1 RID: 39889 RVA: 0x003FBB5B File Offset: 0x003F9F5B
		public bool check
		{
			get
			{
				return !this.particleColor1.IsNullOrEmpty<ParticleSystem>();
			}
		}

		// Token: 0x06009BD2 RID: 39890 RVA: 0x003FBB6C File Offset: 0x003F9F6C
		public void UpdateColor(OIItemInfo _info)
		{
			if (!this.particleColor1.IsNullOrEmpty<ParticleSystem>())
			{
				foreach (ParticleSystem particleSystem in this.particleColor1)
				{
					particleSystem.main.startColor = _info.colors[0].mainColor;
				}
			}
		}

		// Token: 0x06009BD3 RID: 39891 RVA: 0x003FBBC8 File Offset: 0x003F9FC8
		public void PlayOnLoad()
		{
			if (!this.playOnLoad || this.particlesPlay.IsNullOrEmpty<ParticleSystem>())
			{
				return;
			}
			foreach (ParticleSystem particleSystem in from v in this.particlesPlay
			where v != null
			select v)
			{
				particleSystem.Play();
			}
		}

		// Token: 0x06009BD4 RID: 39892 RVA: 0x003FBC60 File Offset: 0x003FA060
		public void SetColor()
		{
			if (!this.particleColor1.IsNullOrEmpty<ParticleSystem>())
			{
				this.defColor01 = this.particleColor1[0].main.startColor.color;
			}
		}

		// Token: 0x04007C2C RID: 31788
		[Header("カラー１反映対象")]
		public ParticleSystem[] particleColor1;

		// Token: 0x04007C2D RID: 31789
		public Color defColor01 = Color.white;

		// Token: 0x04007C2E RID: 31790
		[Header("読み込まれたタイミングで再生する")]
		public bool playOnLoad;

		// Token: 0x04007C2F RID: 31791
		public ParticleSystem[] particlesPlay;
	}
}
