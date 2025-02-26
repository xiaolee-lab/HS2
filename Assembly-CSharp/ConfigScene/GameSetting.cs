using System;
using System.Threading;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x02000860 RID: 2144
	public class GameSetting : BaseSetting
	{
		// Token: 0x060036B5 RID: 14005 RVA: 0x00143666 File Offset: 0x00141A66
		private void OnDestroy()
		{
			this.Release();
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x0014366E File Offset: 0x00141A6E
		private void Release()
		{
			if (this.cancel != null)
			{
				this.cancel.Dispose();
			}
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x00143688 File Offset: 0x00141A88
		public override void Init()
		{
			GameConfigSystem data = Config.GameData;
			base.LinkToggleArray(this.readSkipToggles, delegate(int i)
			{
				data.ReadSkip = (i == 0);
			});
			base.LinkToggleArray(this.nextVoiceStopToggles, delegate(int i)
			{
				data.NextVoiceStop = (i == 0);
			});
			base.LinkToggleArray(this.choiceSkipToggles, delegate(int i)
			{
				data.ChoicesSkip = (i == 0);
			});
			base.LinkToggleArray(this.choiceAutoToggles, delegate(int i)
			{
				data.ChoicesAuto = (i == 0);
			});
			base.LinkToggleArray(this.optionToggles, delegate(int i)
			{
				data.TextWindowOption = (i == 0);
			});
			base.LinkToggleArray(this.guidToggles, delegate(int i)
			{
				data.ActionGuide = (i == 0);
			});
			base.LinkToggleArray(this.helpToggles, delegate(int i)
			{
				data.StoryHelp = (i == 0);
			});
			base.LinkToggleArray(this.minimapToggles, delegate(int i)
			{
				data.MiniMap = (i == 0);
			});
			base.LinkToggleArray(this.pointerToggles, delegate(int i)
			{
				data.CenterPointer = (i == 0);
			});
			base.LinkToggleArray(this.lockToggles, delegate(int i)
			{
				data.ParameterLock = (i == 0);
			});
			base.LinkSlider(this.fontSpeedSlider, delegate(float value)
			{
				data.FontSpeed = (int)value;
			});
			(from value in this.fontSpeedSlider.OnValueChangedAsObservable()
			select (int)value).Subscribe(delegate(int value)
			{
				foreach (TypefaceAnimatorEx typefaceAnimatorEx in this.ta)
				{
					typefaceAnimatorEx.isNoWait = (value == 100);
					if (!typefaceAnimatorEx.isNoWait)
					{
						typefaceAnimatorEx.timeMode = TypefaceAnimatorEx.TimeMode.Speed;
						typefaceAnimatorEx.speed = (float)value;
					}
				}
			});
			base.LinkSlider(this.autoWaitTimeSlider, delegate(float value)
			{
				data.AutoWaitTime = value;
			});
			this.autoWaitTimeSlider.OnValueChangedAsObservable().Subscribe(delegate(float value)
			{
				if (this.cancel != null)
				{
					this.cancel.Dispose();
				}
				foreach (TypefaceAnimatorEx typefaceAnimatorEx in this.ta)
				{
					typefaceAnimatorEx.Play();
				}
			});
			(from isPlaying in (from _ in this.UpdateAsObservable()
			select this.ta[0].isPlaying).DistinctUntilChanged<bool>()
			where !isPlaying
			select isPlaying).Subscribe(delegate(bool _)
			{
				if (this.cancel != null)
				{
					this.cancel.Dispose();
				}
				float autoWaitTimer = 0f;
				this.cancel = Observable.FromCoroutine((CancellationToken __) => new WaitWhile(delegate()
				{
					float autoWaitTime = data.AutoWaitTime;
					autoWaitTimer = Mathf.Min(autoWaitTimer + Time.unscaledDeltaTime, autoWaitTime);
					return autoWaitTimer < autoWaitTime;
				}), false).Subscribe(delegate(Unit __)
				{
					foreach (TypefaceAnimatorEx typefaceAnimatorEx in <Init>c__AnonStorey.ta)
					{
						typefaceAnimatorEx.Play();
					}
				});
			});
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x0014387C File Offset: 0x00141C7C
		protected override void ValueToUI()
		{
			GameConfigSystem data = Config.GameData;
			base.SetToggleUIArray(this.readSkipToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.ReadSkip) : data.ReadSkip);
			});
			base.SetToggleUIArray(this.nextVoiceStopToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.NextVoiceStop) : data.NextVoiceStop);
			});
			base.SetToggleUIArray(this.choiceSkipToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.ChoicesSkip) : data.ChoicesSkip);
			});
			base.SetToggleUIArray(this.choiceAutoToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.ChoicesAuto) : data.ChoicesAuto);
			});
			base.SetToggleUIArray(this.optionToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.TextWindowOption) : data.TextWindowOption);
			});
			this.fontSpeedSlider.value = (float)data.FontSpeed;
			this.autoWaitTimeSlider.value = data.AutoWaitTime;
			base.SetToggleUIArray(this.guidToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.ActionGuide) : data.ActionGuide);
			});
			base.SetToggleUIArray(this.helpToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.StoryHelp) : data.StoryHelp);
			});
			base.SetToggleUIArray(this.minimapToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.MiniMap) : data.MiniMap);
			});
			base.SetToggleUIArray(this.pointerToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.CenterPointer) : data.CenterPointer);
			});
			base.SetToggleUIArray(this.lockToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.ParameterLock) : data.ParameterLock);
			});
		}

		// Token: 0x04003722 RID: 14114
		[Header("既読スキップ")]
		[SerializeField]
		private Toggle[] readSkipToggles;

		// Token: 0x04003723 RID: 14115
		[Header("次のテキスト表示時に音声を停止")]
		[SerializeField]
		private Toggle[] nextVoiceStopToggles;

		// Token: 0x04003724 RID: 14116
		[Header("選択肢でもスキップ継続")]
		[SerializeField]
		private Toggle[] choiceSkipToggles;

		// Token: 0x04003725 RID: 14117
		[Header("選択肢でもオート継続")]
		[SerializeField]
		private Toggle[] choiceAutoToggles;

		// Token: 0x04003726 RID: 14118
		[Header("テキストウィンドウオプション")]
		[SerializeField]
		private Toggle[] optionToggles;

		// Token: 0x04003727 RID: 14119
		[Header("文字表示速度")]
		[SerializeField]
		private Slider fontSpeedSlider;

		// Token: 0x04003728 RID: 14120
		[Header("自動送り待ち時間")]
		[SerializeField]
		private Slider autoWaitTimeSlider;

		// Token: 0x04003729 RID: 14121
		[Header("文字表示サンプル")]
		[SerializeField]
		private TypefaceAnimatorEx[] ta;

		// Token: 0x0400372A RID: 14122
		[Header("操作ガイド")]
		[SerializeField]
		private Toggle[] guidToggles;

		// Token: 0x0400372B RID: 14123
		[Header("ストーリーヘルプ")]
		[SerializeField]
		private Toggle[] helpToggles;

		// Token: 0x0400372C RID: 14124
		[Header("ミニマップ")]
		[SerializeField]
		private Toggle[] minimapToggles;

		// Token: 0x0400372D RID: 14125
		[Header("中央ポインター")]
		[SerializeField]
		private Toggle[] pointerToggles;

		// Token: 0x0400372E RID: 14126
		[Header("女の子のパラメーターロック")]
		[SerializeField]
		private Toggle[] lockToggles;

		// Token: 0x0400372F RID: 14127
		private IDisposable cancel;
	}
}
