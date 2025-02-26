using System;
using System.Collections;
using System.Diagnostics;
using Manager;
using Sound;
using UnityEngine;

// Token: 0x02001132 RID: 4402
public abstract class LoadAudioBase : AssetLoader
{
	// Token: 0x17001F53 RID: 8019
	// (get) Token: 0x060091BE RID: 37310 RVA: 0x003C895A File Offset: 0x003C6D5A
	// (set) Token: 0x060091BF RID: 37311 RVA: 0x003C8962 File Offset: 0x003C6D62
	public AudioClip clip { get; private set; }

	// Token: 0x17001F54 RID: 8020
	// (get) Token: 0x060091C0 RID: 37312 RVA: 0x003C896B File Offset: 0x003C6D6B
	// (set) Token: 0x060091C1 RID: 37313 RVA: 0x003C8973 File Offset: 0x003C6D73
	public AudioSource audioSource { get; private set; }

	// Token: 0x060091C2 RID: 37314 RVA: 0x003C897C File Offset: 0x003C6D7C
	public void Init(AudioSource audioSource)
	{
		this.audioSource = audioSource;
		base.transform.SetParent(audioSource.transform, false);
		base.Init();
	}

	// Token: 0x060091C3 RID: 37315 RVA: 0x003C89A0 File Offset: 0x003C6DA0
	public override IEnumerator _Init()
	{
		if (base.initialized)
		{
			yield break;
		}
		if (this.isAsync)
		{
			yield return base.StartCoroutine(base._Init());
		}
		else
		{
			base.StartCoroutine(base._Init());
		}
		this.clip = (base.loadObject as AudioClip);
		if (this.clip == null)
		{
			if (this.audioSource != null)
			{
				UnityEngine.Object.Destroy(this.audioSource.gameObject);
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		yield break;
	}

	// Token: 0x060091C4 RID: 37316 RVA: 0x003C89BC File Offset: 0x003C6DBC
	protected IEnumerator Play(GameObject fadeOut)
	{
		if (this.audioSource.playOnAwake)
		{
			yield break;
		}
		yield return new WaitUntil(() => this.audioSource.clip.loadState == AudioDataLoadState.Loaded);
		if (this.delayTime > 0f)
		{
			yield return new global::WaitForSecondsRealtime(this.delayTime);
		}
		while (!this.audioSource.isActiveAndEnabled)
		{
			yield return null;
		}
		if (this.fadeTime > 0f)
		{
			Sound.PlayFade(fadeOut, this.audioSource, this.fadeTime);
		}
		else
		{
			this.audioSource.Play();
		}
		if (this.audioSource.loop)
		{
			yield break;
		}
		float pitch = this.audioSource.pitch;
		if (pitch == 0f)
		{
			pitch = 1f;
		}
		float endTime = (this.audioSource.clip.length - this.audioSource.time) * (1f / pitch);
		if (endTime > 0f)
		{
			yield return new global::WaitForSecondsRealtime(endTime);
		}
		FadePlayer fadePlay = this.audioSource.GetComponent<FadePlayer>();
		if (fadePlay != null)
		{
			fadePlay.Stop(this.fadeTime);
		}
		else
		{
			UnityEngine.Object.Destroy(this.audioSource.gameObject);
		}
		yield break;
	}

	// Token: 0x060091C5 RID: 37317 RVA: 0x003C89DE File Offset: 0x003C6DDE
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this.isReleaseClip && Singleton<Sound>.IsInstance())
		{
			Singleton<Sound>.Instance.Remove(this.clip);
		}
	}

	// Token: 0x060091C6 RID: 37318 RVA: 0x003C8A0B File Offset: 0x003C6E0B
	[Conditional("BASE_LOADER_LOG")]
	private void LogError(string str)
	{
	}

	// Token: 0x04007601 RID: 30209
	public float delayTime;

	// Token: 0x04007602 RID: 30210
	public float fadeTime;

	// Token: 0x04007603 RID: 30211
	public int settingNo = -1;

	// Token: 0x04007606 RID: 30214
	protected bool isReleaseClip = true;
}
