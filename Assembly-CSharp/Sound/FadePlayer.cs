using System;
using UnityEngine;

namespace Sound
{
	// Token: 0x0200113B RID: 4411
	[RequireComponent(typeof(AudioSource))]
	public class FadePlayer : MonoBehaviour
	{
		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x06009215 RID: 37397 RVA: 0x003CA083 File Offset: 0x003C8483
		// (set) Token: 0x06009216 RID: 37398 RVA: 0x003CA08B File Offset: 0x003C848B
		public FadePlayer.State nowState { get; private set; }

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x06009217 RID: 37399 RVA: 0x003CA094 File Offset: 0x003C8494
		// (set) Token: 0x06009218 RID: 37400 RVA: 0x003CA09C File Offset: 0x003C849C
		public AudioSource audioSource { get; private set; }

		// Token: 0x06009219 RID: 37401 RVA: 0x003CA0A5 File Offset: 0x003C84A5
		private void Awake()
		{
			this.audioSource = base.GetComponent<AudioSource>();
			this.nowState = new FadePlayer.Wait(this);
		}

		// Token: 0x0600921A RID: 37402 RVA: 0x003CA0BF File Offset: 0x003C84BF
		private void Update()
		{
			this.nowState.Update();
			if (this.nowState is FadePlayer.Wait && !this.audioSource.isPlaying)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x0600921B RID: 37403 RVA: 0x003CA0F8 File Offset: 0x003C84F8
		public void Play(float fadeTime = 0f)
		{
			this.fadeInTime = fadeTime;
			this.nowState.Play();
		}

		// Token: 0x0600921C RID: 37404 RVA: 0x003CA10C File Offset: 0x003C850C
		public void Pause()
		{
			this.nowState.Pause();
		}

		// Token: 0x0600921D RID: 37405 RVA: 0x003CA119 File Offset: 0x003C8519
		public void Stop(float fadeTime)
		{
			this.fadeOutTime = fadeTime;
			this.nowState.Stop();
		}

		// Token: 0x0400763D RID: 30269
		public float currentVolume = 1f;

		// Token: 0x04007640 RID: 30272
		private float fadeInTime;

		// Token: 0x04007641 RID: 30273
		private float fadeOutTime;

		// Token: 0x0200113C RID: 4412
		public abstract class State
		{
			// Token: 0x0600921E RID: 37406 RVA: 0x003CA12D File Offset: 0x003C852D
			public State(FadePlayer player)
			{
				this.player = player;
			}

			// Token: 0x0600921F RID: 37407 RVA: 0x003CA13C File Offset: 0x003C853C
			public virtual void Play()
			{
			}

			// Token: 0x06009220 RID: 37408 RVA: 0x003CA13E File Offset: 0x003C853E
			public virtual void Pause()
			{
			}

			// Token: 0x06009221 RID: 37409 RVA: 0x003CA140 File Offset: 0x003C8540
			public virtual void Stop()
			{
			}

			// Token: 0x06009222 RID: 37410 RVA: 0x003CA142 File Offset: 0x003C8542
			public virtual bool Update()
			{
				return true;
			}

			// Token: 0x04007642 RID: 30274
			protected FadePlayer player;
		}

		// Token: 0x0200113D RID: 4413
		public class Wait : FadePlayer.State
		{
			// Token: 0x06009223 RID: 37411 RVA: 0x003CA145 File Offset: 0x003C8545
			public Wait(FadePlayer player) : base(player)
			{
			}

			// Token: 0x06009224 RID: 37412 RVA: 0x003CA150 File Offset: 0x003C8550
			public override void Play()
			{
				if (this.player.fadeInTime > 0f)
				{
					this.player.nowState = new FadePlayer.FadeIn(this.player);
				}
				else
				{
					this.player.nowState = new FadePlayer.Playing(this.player);
				}
			}
		}

		// Token: 0x0200113E RID: 4414
		public class FadeIn : FadePlayer.State
		{
			// Token: 0x06009225 RID: 37413 RVA: 0x003CA1A3 File Offset: 0x003C85A3
			public FadeIn(FadePlayer player) : base(player)
			{
				player.audioSource.Play();
				player.audioSource.volume = 0f;
			}

			// Token: 0x06009226 RID: 37414 RVA: 0x003CA1C7 File Offset: 0x003C85C7
			public override void Pause()
			{
				this.player.nowState = new FadePlayer.Paused(this.player);
			}

			// Token: 0x06009227 RID: 37415 RVA: 0x003CA1DF File Offset: 0x003C85DF
			public override void Stop()
			{
				this.player.nowState = new FadePlayer.FadeOut(this.player);
			}

			// Token: 0x06009228 RID: 37416 RVA: 0x003CA1F8 File Offset: 0x003C85F8
			public override bool Update()
			{
				this.t += Time.deltaTime;
				this.player.audioSource.volume = Mathf.Lerp(0f, this.player.currentVolume, this.t / this.player.fadeInTime);
				if (this.t >= this.player.fadeInTime)
				{
					this.player.nowState = new FadePlayer.Playing(this.player);
					return true;
				}
				return false;
			}

			// Token: 0x04007643 RID: 30275
			private float t;
		}

		// Token: 0x0200113F RID: 4415
		public class Playing : FadePlayer.State
		{
			// Token: 0x06009229 RID: 37417 RVA: 0x003CA27D File Offset: 0x003C867D
			public Playing(FadePlayer player) : base(player)
			{
				if (!player.audioSource.isPlaying)
				{
					player.audioSource.Play();
				}
			}

			// Token: 0x0600922A RID: 37418 RVA: 0x003CA2A1 File Offset: 0x003C86A1
			public override void Pause()
			{
				this.player.nowState = new FadePlayer.Paused(this.player);
			}

			// Token: 0x0600922B RID: 37419 RVA: 0x003CA2B9 File Offset: 0x003C86B9
			public override void Stop()
			{
				this.player.nowState = new FadePlayer.FadeOut(this.player);
			}

			// Token: 0x0600922C RID: 37420 RVA: 0x003CA2D1 File Offset: 0x003C86D1
			public override bool Update()
			{
				this.player.audioSource.volume = this.player.currentVolume;
				return false;
			}
		}

		// Token: 0x02001140 RID: 4416
		public class Paused : FadePlayer.State
		{
			// Token: 0x0600922D RID: 37421 RVA: 0x003CA2EF File Offset: 0x003C86EF
			public Paused(FadePlayer player) : base(player)
			{
				this.preState = player.nowState;
				player.audioSource.Pause();
			}

			// Token: 0x0600922E RID: 37422 RVA: 0x003CA30F File Offset: 0x003C870F
			public override void Stop()
			{
				this.player.audioSource.Stop();
				this.player.nowState = new FadePlayer.Wait(this.player);
			}

			// Token: 0x0600922F RID: 37423 RVA: 0x003CA337 File Offset: 0x003C8737
			public override void Play()
			{
				this.player.nowState = this.preState;
				this.player.audioSource.Play();
			}

			// Token: 0x04007644 RID: 30276
			private FadePlayer.State preState;
		}

		// Token: 0x02001141 RID: 4417
		public class FadeOut : FadePlayer.State
		{
			// Token: 0x06009230 RID: 37424 RVA: 0x003CA35A File Offset: 0x003C875A
			public FadeOut(FadePlayer player) : base(player)
			{
				player.currentVolume = player.audioSource.volume;
			}

			// Token: 0x06009231 RID: 37425 RVA: 0x003CA374 File Offset: 0x003C8774
			public override void Pause()
			{
				this.player.nowState = new FadePlayer.Paused(this.player);
			}

			// Token: 0x06009232 RID: 37426 RVA: 0x003CA38C File Offset: 0x003C878C
			public override bool Update()
			{
				this.t += Time.deltaTime;
				this.player.audioSource.volume = this.player.currentVolume * (1f - this.t / this.player.fadeOutTime);
				if (this.t >= this.player.fadeOutTime)
				{
					this.player.audioSource.volume = 0f;
					this.player.audioSource.Stop();
					this.player.nowState = new FadePlayer.Wait(this.player);
					return true;
				}
				return false;
			}

			// Token: 0x04007645 RID: 30277
			private float t;
		}
	}
}
