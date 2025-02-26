using System;
using System.Collections;
using Manager;
using UnityEngine;

// Token: 0x02001133 RID: 4403
public class LoadSound : LoadAudioBase
{
	// Token: 0x060091C9 RID: 37321 RVA: 0x003C8E20 File Offset: 0x003C7220
	public override IEnumerator _Init()
	{
		if (base.initialized)
		{
			yield break;
		}
		if (!this.isAssetEqualPlay && Singleton<Sound>.Instance.FindAsset(this.type, this.assetName, this.assetBundleName) != null)
		{
			Transform parent = base.transform.parent;
			if (parent != null)
			{
				if (parent.GetComponent<AudioSource>() != null)
				{
					this.isReleaseClip = false;
					UnityEngine.Object.Destroy(parent.gameObject);
					base.initialized = true;
					yield break;
				}
			}
			else if (base.transform.GetComponent<LoadAudioBase>() != null)
			{
				this.isReleaseClip = false;
				UnityEngine.Object.Destroy(base.gameObject);
				base.initialized = true;
				yield break;
			}
		}
		if (this.isAsync)
		{
			yield return base.StartCoroutine(base._Init());
		}
		else
		{
			base.StartCoroutine(base._Init());
		}
		if (base.clip == null)
		{
			yield break;
		}
		Singleton<Sound>.Instance.Bind(this);
		base.name = "Sound LoadEnd";
		yield break;
	}

	// Token: 0x060091CA RID: 37322 RVA: 0x003C8E3C File Offset: 0x003C723C
	protected override IEnumerator Start()
	{
		base.enabled = false;
		if (base.audioSource == null)
		{
			yield return base.StartCoroutine(this._Init());
		}
		while (!base.isLoadEnd)
		{
			yield return null;
		}
		if (base.clip == null)
		{
			yield break;
		}
		GameObject fadeOut = null;
		if (this.type == Sound.Type.BGM)
		{
			this.fadeTime = Mathf.Max(this.fadeTime, 0.01f);
			fadeOut = Singleton<Sound>.Instance.currentBGM;
			Singleton<Sound>.Instance.currentBGM = base.audioSource.gameObject;
		}
		base.StartCoroutine(base.Play(fadeOut));
		base.enabled = true;
		yield break;
	}

	// Token: 0x04007607 RID: 30215
	public Sound.Type type = Sound.Type.GameSE2D;

	// Token: 0x04007608 RID: 30216
	public bool isAssetEqualPlay = true;
}
