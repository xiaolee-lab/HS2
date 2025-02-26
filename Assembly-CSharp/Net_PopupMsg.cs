using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200100A RID: 4106
public class Net_PopupMsg : MonoBehaviour
{
	// Token: 0x17001E27 RID: 7719
	// (get) Token: 0x06008A08 RID: 35336 RVA: 0x003A16DF File Offset: 0x0039FADF
	// (set) Token: 0x06008A09 RID: 35337 RVA: 0x003A16E7 File Offset: 0x0039FAE7
	public bool active { get; private set; }

	// Token: 0x06008A0A RID: 35338 RVA: 0x003A16F0 File Offset: 0x0039FAF0
	public void StartMessage(float st, float lt, float et, string msg, int mode)
	{
		if (null == this.cgrp)
		{
			return;
		}
		this.endMode = mode;
		this.looptime = lt;
		this.exitCommand = false;
		IObservable<float> source = (from _ in this.UpdateAsObservable()
		select Time.deltaTime).Scan((float acc, float current) => acc + current);
		IObservable<float> source2 = source.TakeWhile((float t) => t < st);
		IObservable<float> loopStream = source.TakeWhile((float t) => !this.CheckEnd(t));
		IObservable<float> endStream = source.TakeWhile((float t) => t < et);
		this.disposables.Clear();
		if (this.txt)
		{
			this.txt.text = msg;
		}
		this.cgrp.blocksRaycasts = true;
		this.active = true;
		source2.Subscribe(delegate(float t)
		{
			this.cgrp.alpha = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(0f, st, t));
		}, delegate()
		{
			this.cgrp.alpha = 1f;
			loopStream.Subscribe(delegate(float t)
			{
			}, delegate()
			{
				endStream.Subscribe(delegate(float t)
				{
					this.cgrp.alpha = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(0f, et, t));
				}, delegate()
				{
					this.cgrp.alpha = 0f;
					this.cgrp.blocksRaycasts = false;
					this.active = false;
				}).AddTo(this.disposables);
			}).AddTo(this.disposables);
		}).AddTo(this.disposables);
	}

	// Token: 0x06008A0B RID: 35339 RVA: 0x003A1833 File Offset: 0x0039FC33
	public void EndMessage()
	{
		this.exitCommand = true;
	}

	// Token: 0x06008A0C RID: 35340 RVA: 0x003A183C File Offset: 0x0039FC3C
	public bool CheckEnd(float time)
	{
		switch (this.endMode)
		{
		case 0:
			return time >= this.looptime || Input.anyKeyDown;
		case 1:
			return Input.anyKeyDown;
		case 2:
			return this.exitCommand;
		default:
			return false;
		}
	}

	// Token: 0x06008A0D RID: 35341 RVA: 0x003A188F File Offset: 0x0039FC8F
	private void Start()
	{
		this.cgrp.alpha = 0f;
		this.cgrp.blocksRaycasts = false;
		this.active = false;
		this.OnDestroyAsObservable().Subscribe(delegate(Unit _)
		{
			this.disposables.Clear();
			this.cgrp.alpha = 0f;
			this.cgrp.blocksRaycasts = false;
			this.active = false;
		});
	}

	// Token: 0x0400705F RID: 28767
	[SerializeField]
	private CanvasGroup cgrp;

	// Token: 0x04007060 RID: 28768
	[SerializeField]
	private Text txt;

	// Token: 0x04007061 RID: 28769
	private int endMode;

	// Token: 0x04007062 RID: 28770
	private float looptime;

	// Token: 0x04007063 RID: 28771
	private bool exitCommand;

	// Token: 0x04007065 RID: 28773
	private CompositeDisposable disposables = new CompositeDisposable();
}
