using System;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x0200086C RID: 2156
	public class VoiceUI : MonoBehaviour
	{
		// Token: 0x060036F4 RID: 14068 RVA: 0x00145C4C File Offset: 0x0014404C
		public void Refresh()
		{
			if (!Singleton<Voice>.Instance._Config.chara.ContainsKey(this.index))
			{
				return;
			}
			this.toggle.isOn = Singleton<Voice>.Instance._Config.chara[this.index].sound.Mute;
			this.slider.value = (float)Singleton<Voice>.Instance._Config.chara[this.index].sound.Volume;
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x00145CD8 File Offset: 0x001440D8
		protected void OnValueChangeToggle(bool _value)
		{
			Singleton<Voice>.Instance._Config.chara[this.index].sound.Mute = _value;
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x00145CFF File Offset: 0x001440FF
		protected void OnValueChangeSlider(float _value)
		{
			Singleton<Voice>.Instance._Config.chara[this.index].sound.Volume = Mathf.FloorToInt(_value);
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x00145D2B File Offset: 0x0014412B
		protected void Reset()
		{
			if (this.toggle == null)
			{
				this.toggle = base.GetComponentInChildren<Toggle>();
			}
			if (this.slider == null)
			{
				this.slider = base.GetComponentInChildren<Slider>();
			}
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x00145D68 File Offset: 0x00144168
		protected void Start()
		{
			if (!Singleton<Voice>.Instance._Config.chara.ContainsKey(this.index))
			{
				return;
			}
			this.Refresh();
			this.toggle.onValueChanged.AddListener(delegate(bool value)
			{
				this.OnValueChangeToggle(value);
			});
			this.slider.onValueChanged.AddListener(delegate(float value)
			{
				this.OnValueChangeSlider(value);
			});
		}

		// Token: 0x0400377E RID: 14206
		[SerializeField]
		protected int index;

		// Token: 0x0400377F RID: 14207
		[SerializeField]
		protected Toggle toggle;

		// Token: 0x04003780 RID: 14208
		[SerializeField]
		protected Slider slider;
	}
}
