using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020010AE RID: 4270
public class AnimeReserv
{
	// Token: 0x06008E8E RID: 36494 RVA: 0x003B4D21 File Offset: 0x003B3121
	public AnimeReserv(Animation _animation)
	{
		this.animation = _animation;
		this.animeQueue = new Queue<AnimeReserv.AnimeData>();
	}

	// Token: 0x06008E8F RID: 36495 RVA: 0x003B4D3B File Offset: 0x003B313B
	public void ReservAdd(string name, float fadeSpeed = 0f)
	{
		this.animeQueue.Enqueue(new AnimeReserv.AnimeData(name, fadeSpeed));
	}

	// Token: 0x06008E90 RID: 36496 RVA: 0x003B4D50 File Offset: 0x003B3150
	public void ReservEXE()
	{
		while (this.animeQueue.Count > 0)
		{
			AnimeReserv.AnimeData animeData = this.animeQueue.Dequeue();
			if (animeData.FadeSpeed == 0f)
			{
				this.animation.PlayQueued(animeData.Name);
			}
			else
			{
				this.animation.CrossFadeQueued(animeData.Name, animeData.FadeSpeed);
			}
		}
	}

	// Token: 0x04007344 RID: 29508
	protected Queue<AnimeReserv.AnimeData> animeQueue;

	// Token: 0x04007345 RID: 29509
	protected Animation animation;

	// Token: 0x020010AF RID: 4271
	protected class AnimeData
	{
		// Token: 0x06008E91 RID: 36497 RVA: 0x003B4DBE File Offset: 0x003B31BE
		public AnimeData(string _name, float _fadeSpeed = 0f)
		{
			this.name = _name;
			this.fadeSpeed = _fadeSpeed;
		}

		// Token: 0x17001EED RID: 7917
		// (get) Token: 0x06008E92 RID: 36498 RVA: 0x003B4DD4 File Offset: 0x003B31D4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001EEE RID: 7918
		// (get) Token: 0x06008E93 RID: 36499 RVA: 0x003B4DDC File Offset: 0x003B31DC
		public float FadeSpeed
		{
			get
			{
				return this.fadeSpeed;
			}
		}

		// Token: 0x04007346 RID: 29510
		private string name;

		// Token: 0x04007347 RID: 29511
		private float fadeSpeed;
	}
}
