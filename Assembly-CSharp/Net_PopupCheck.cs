using System;
using System.Collections;
using AIProject;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02001009 RID: 4105
public class Net_PopupCheck : MonoBehaviour
{
	// Token: 0x06008A02 RID: 35330 RVA: 0x003A1480 File Offset: 0x0039F880
	public IEnumerator CheckAnswerCor(IObserver<bool> observer, string msg)
	{
		if (this.textMsg)
		{
			this.textMsg.text = msg;
		}
		this.canvas.gameObject.SetActive(true);
		this.answer = null;
		for (;;)
		{
			bool? flag = this.answer;
			if (flag != null)
			{
				break;
			}
			yield return null;
		}
		bool? flag2 = this.answer;
		observer.OnNext(flag2 != null && flag2.Value);
		observer.OnCompleted();
		this.canvas.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x06008A03 RID: 35331 RVA: 0x003A14AC File Offset: 0x0039F8AC
	private void Start()
	{
		if (this.btnYes)
		{
			this.btnYes.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.answer = new bool?(true);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			}).AddTo(this);
		}
		if (this.btnNo)
		{
			this.btnNo.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.answer = new bool?(false);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			}).AddTo(this);
		}
	}

	// Token: 0x06008A04 RID: 35332 RVA: 0x003A151F File Offset: 0x0039F91F
	private void Update()
	{
		if (UnityEngine.Input.GetMouseButtonDown(1))
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			this.answer = new bool?(false);
		}
	}

	// Token: 0x0400705A RID: 28762
	[SerializeField]
	private Canvas canvas;

	// Token: 0x0400705B RID: 28763
	[SerializeField]
	private Button btnYes;

	// Token: 0x0400705C RID: 28764
	[SerializeField]
	private Button btnNo;

	// Token: 0x0400705D RID: 28765
	[SerializeField]
	private Text textMsg;

	// Token: 0x0400705E RID: 28766
	private bool? answer;
}
