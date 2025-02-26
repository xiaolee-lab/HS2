using System;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x02000863 RID: 2147
	public class SoundSetting : BaseSetting
	{
		// Token: 0x060036C7 RID: 14023 RVA: 0x001451DB File Offset: 0x001435DB
		private void InitSet(SoundSetting.SoundGroup sg, SoundData sd)
		{
			sg.toggle.isOn = sd.Mute;
			sg.slider.value = (float)sd.Volume;
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x00145200 File Offset: 0x00143600
		private void InitLink(SoundSetting.SoundGroup sg, SoundData sd, bool isSliderEvent)
		{
			base.LinkToggle(sg.toggle, delegate(bool isOn)
			{
				sd.Mute = isOn;
			});
			sg.toggle.onValueChanged.AsObservable<bool>().Subscribe(delegate(bool isOn)
			{
				sg.image.enabled = !isOn;
			});
			(from b in sg.toggle.OnValueChangedAsObservable()
			select !b).SubscribeToInteractable(sg.slider);
			if (isSliderEvent)
			{
				base.LinkSlider(sg.slider, delegate(float value)
				{
					sd.Volume = (int)value;
				});
			}
			else
			{
				(from _ in sg.slider.OnPointerDownAsObservable()
				where UnityEngine.Input.GetMouseButtonDown(0)
				select _).Subscribe(delegate(PointerEventData _)
				{
					this.EnterSE();
				});
			}
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x0014531C File Offset: 0x0014371C
		public override void Init()
		{
			SoundSystem soundData = Config.SoundData;
			this.InitLink(this.Master, soundData.Master, true);
			this.InitLink(this.ENV, soundData.ENV, true);
			this.InitLink(this.SystemSE, soundData.SystemSE, true);
			this.InitLink(this.GameSE, soundData.GameSE, true);
			this.InitLink(this.BGM, soundData.BGM, true);
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x00145390 File Offset: 0x00143790
		protected override void ValueToUI()
		{
			SoundSystem soundData = Config.SoundData;
			this.InitSet(this.Master, soundData.Master);
			this.InitSet(this.BGM, soundData.BGM);
			this.InitSet(this.ENV, soundData.ENV);
			this.InitSet(this.SystemSE, soundData.SystemSE);
			this.InitSet(this.GameSE, soundData.GameSE);
		}

		// Token: 0x04003761 RID: 14177
		[SerializeField]
		private SoundSetting.SoundGroup Master;

		// Token: 0x04003762 RID: 14178
		[SerializeField]
		private SoundSetting.SoundGroup BGM;

		// Token: 0x04003763 RID: 14179
		[SerializeField]
		private SoundSetting.SoundGroup ENV;

		// Token: 0x04003764 RID: 14180
		[SerializeField]
		private SoundSetting.SoundGroup SystemSE;

		// Token: 0x04003765 RID: 14181
		[SerializeField]
		private SoundSetting.SoundGroup GameSE;

		// Token: 0x02000864 RID: 2148
		[Serializable]
		private class SoundGroup
		{
			// Token: 0x04003768 RID: 14184
			public Toggle toggle;

			// Token: 0x04003769 RID: 14185
			public Slider slider;

			// Token: 0x0400376A RID: 14186
			public Image image;
		}
	}
}
