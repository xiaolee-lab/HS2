using System;
using UnityEngine;

// Token: 0x020010B0 RID: 4272
public class AnimeReservEX : AnimeReserv
{
	// Token: 0x06008E94 RID: 36500 RVA: 0x003B4DE4 File Offset: 0x003B31E4
	public AnimeReservEX(AnimationAssist _assist) : base(_assist.NowAnimation)
	{
		this.assist = _assist;
	}

	// Token: 0x06008E95 RID: 36501 RVA: 0x003B4DFC File Offset: 0x003B31FC
	public void Update()
	{
		if (this.animeQueue.Count > 0 && this.assist.IsAnimeEnd())
		{
			AnimeReserv.AnimeData animeData = this.animeQueue.Dequeue();
			this.assist.Play(animeData.Name, 0f, animeData.FadeSpeed, 0, WrapMode.Default);
		}
	}

	// Token: 0x04007348 RID: 29512
	private AnimationAssist assist;
}
