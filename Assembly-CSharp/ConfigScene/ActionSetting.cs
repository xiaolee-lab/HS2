using System;
using AIProject;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x0200085E RID: 2142
	public class ActionSetting : BaseSetting
	{
		// Token: 0x060036A0 RID: 13984 RVA: 0x00143220 File Offset: 0x00141620
		public override void Init()
		{
			ActionSystem actData = Config.ActData;
			base.LinkToggleArray(this.lookToggles, delegate(int i)
			{
				actData.Look = (i == 0);
			});
			base.LinkSlider(this.tpsSensitivityXSlider, delegate(float value)
			{
				actData.TPSSensitivityY = (int)value;
			});
			base.LinkSlider(this.tpsSensitivityYSlider, delegate(float value)
			{
				actData.TPSSensitivityX = (int)value;
			});
			base.LinkSlider(this.fpsSensitivityXSlider, delegate(float value)
			{
				actData.FPSSensitivityY = (int)value;
			});
			base.LinkSlider(this.fpsSensitivityYSlider, delegate(float value)
			{
				actData.FPSSensitivityX = (int)value;
			});
			base.LinkToggleArray(this.invertMoveXToggles, delegate(int i)
			{
				actData.InvertMoveY = (i == 1);
			});
			base.LinkToggleArray(this.invertMoveYToggles, delegate(int i)
			{
				actData.InvertMoveX = (i == 1);
			});
			this.tpsSensitivityXResetButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.tpsSensitivityXSlider.value = 0f;
			});
			this.tpsSensitivityYResetButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.tpsSensitivityYSlider.value = 0f;
			});
			this.fpsSensitivityXResetButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.fpsSensitivityXSlider.value = 0f;
			});
			this.fpsSensitivityYResetButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.fpsSensitivityYSlider.value = 0f;
			});
			Observable.Merge<Unit>(new IObservable<Unit>[]
			{
				this.tpsSensitivityXResetButton.OnClickAsObservable(),
				this.tpsSensitivityYResetButton.OnClickAsObservable(),
				this.fpsSensitivityXResetButton.OnClickAsObservable(),
				this.fpsSensitivityYResetButton.OnClickAsObservable()
			}).Subscribe(delegate(Unit _)
			{
				this.EnterSE();
			});
			Observable.Merge<PointerEventData>(new IObservable<PointerEventData>[]
			{
				this.tpsSensitivityXResetButton.OnPointerEnterAsObservable(),
				this.tpsSensitivityYResetButton.OnPointerEnterAsObservable(),
				this.fpsSensitivityXResetButton.OnPointerEnterAsObservable(),
				this.fpsSensitivityYResetButton.OnPointerEnterAsObservable()
			}).Subscribe(delegate(PointerEventData _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x0014341C File Offset: 0x0014181C
		protected override void ValueToUI()
		{
			ActionSystem actData = Config.ActData;
			base.SetToggleUIArray(this.lookToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!actData.Look) : actData.Look);
			});
			this.tpsSensitivityXSlider.value = (float)actData.TPSSensitivityY;
			this.tpsSensitivityYSlider.value = (float)actData.TPSSensitivityX;
			this.fpsSensitivityXSlider.value = (float)actData.FPSSensitivityY;
			this.fpsSensitivityYSlider.value = (float)actData.FPSSensitivityX;
			base.SetToggleUIArray(this.invertMoveXToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 1) ? (!actData.InvertMoveY) : actData.InvertMoveY);
			});
			base.SetToggleUIArray(this.invertMoveYToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 1) ? (!actData.InvertMoveX) : actData.InvertMoveX);
			});
		}

		// Token: 0x0400370F RID: 14095
		[Header("注視点の表示")]
		[SerializeField]
		private Toggle[] lookToggles;

		// Token: 0x04003710 RID: 14096
		[Header("TPS時のマウス感度X")]
		[SerializeField]
		private Slider tpsSensitivityXSlider;

		// Token: 0x04003711 RID: 14097
		[Header("TPS時のマウス感度Y")]
		[SerializeField]
		private Slider tpsSensitivityYSlider;

		// Token: 0x04003712 RID: 14098
		[Header("FPS時のマウス感度X")]
		[SerializeField]
		private Slider fpsSensitivityXSlider;

		// Token: 0x04003713 RID: 14099
		[Header("FPS時のマウス感度Y")]
		[SerializeField]
		private Slider fpsSensitivityYSlider;

		// Token: 0x04003714 RID: 14100
		[Header("カメラ移動Xの反転")]
		[SerializeField]
		private Toggle[] invertMoveXToggles;

		// Token: 0x04003715 RID: 14101
		[Header("カメラ移動Yの反転")]
		[SerializeField]
		private Toggle[] invertMoveYToggles;

		// Token: 0x04003716 RID: 14102
		[Header("TPS時のマウス感度Xリセット")]
		[SerializeField]
		private Button tpsSensitivityXResetButton;

		// Token: 0x04003717 RID: 14103
		[Header("TPS時のマウス感度Yリセット")]
		[SerializeField]
		private Button tpsSensitivityYResetButton;

		// Token: 0x04003718 RID: 14104
		[Header("FPS時のマウス感度Xリセット")]
		[SerializeField]
		private Button fpsSensitivityXResetButton;

		// Token: 0x04003719 RID: 14105
		[Header("FPS時のマウス感度Yリセット")]
		[SerializeField]
		private Button fpsSensitivityYResetButton;
	}
}
