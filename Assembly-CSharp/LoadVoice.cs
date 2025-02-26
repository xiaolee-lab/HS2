using System;
using System.Collections;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

// Token: 0x02001134 RID: 4404
public class LoadVoice : LoadAudioBase
{
	// Token: 0x060091CD RID: 37325 RVA: 0x003C9218 File Offset: 0x003C7618
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
		if (base.clip == null)
		{
			yield break;
		}
		Singleton<Voice>.Instance.Bind(this);
		base.name = "Voice LoadEnd";
		if (base.audioSource == null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			yield break;
		}
		if (this.settingNo < 0)
		{
			base.audioSource.loop = !this.isPlayEndDelete;
			if (!this.is2D)
			{
				base.audioSource.spatialBlend = (float)((!(this.voiceTrans != null)) ? 0 : 1);
			}
			else
			{
				base.audioSource.spatialBlend = 0f;
			}
		}
		base.audioSource.pitch = this.pitch;
		yield break;
	}

	// Token: 0x060091CE RID: 37326 RVA: 0x003C9234 File Offset: 0x003C7634
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
		base.StartCoroutine(base.Play(null));
		this.UpdateAsObservable().TakeUntilDestroy(base.audioSource).Subscribe(delegate(Unit _)
		{
			this.audioSource.volume = Singleton<Voice>.Instance.GetVolume(this.no);
		});
		if (this.voiceTrans == null)
		{
			yield break;
		}
		Transform audioTrans = base.audioSource.transform;
		this.UpdateAsObservable().TakeUntilDestroy(this.voiceTrans).TakeUntilDestroy(base.audioSource).Subscribe(delegate(Unit _)
		{
			audioTrans.SetPositionAndRotation(this.voiceTrans.position, this.voiceTrans.rotation);
		});
		base.enabled = true;
		yield break;
	}

	// Token: 0x04007609 RID: 30217
	public int no;

	// Token: 0x0400760A RID: 30218
	public Transform voiceTrans;

	// Token: 0x0400760B RID: 30219
	public Voice.Type type;

	// Token: 0x0400760C RID: 30220
	public bool isPlayEndDelete = true;

	// Token: 0x0400760D RID: 30221
	public float pitch = 1f;

	// Token: 0x0400760E RID: 30222
	public bool is2D;
}
