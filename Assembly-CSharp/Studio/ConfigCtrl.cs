using System;
using ConfigScene;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012D6 RID: 4822
	public class ConfigCtrl : MonoBehaviour
	{
		// Token: 0x170021F0 RID: 8688
		// (get) Token: 0x0600A0D9 RID: 41177 RVA: 0x00421502 File Offset: 0x0041F902
		// (set) Token: 0x0600A0DA RID: 41178 RVA: 0x0042150A File Offset: 0x0041F90A
		private SoundData[] soundData { get; set; }

		// Token: 0x0600A0DB RID: 41179 RVA: 0x00421514 File Offset: 0x0041F914
		private void OnClickColor()
		{
			if (Singleton<Studio>.Instance.colorPalette.Check("背景色"))
			{
				Singleton<Studio>.Instance.colorPalette.visible = false;
				return;
			}
			Singleton<Studio>.Instance.colorPalette.Setup("背景色", Config.GraphicData.BackColor, new Action<Color>(this.OnValueChangeColor), false);
		}

		// Token: 0x0600A0DC RID: 41180 RVA: 0x00421576 File Offset: 0x0041F976
		private void OnValueChangeColor(Color _color)
		{
			Config.GraphicData.BackColor = _color;
			this.buttonColor.image.color = _color;
			Camera.main.backgroundColor = _color;
		}

		// Token: 0x0600A0DD RID: 41181 RVA: 0x0042159F File Offset: 0x0041F99F
		private void OnOnValueChangedTexture(int _no)
		{
		}

		// Token: 0x0600A0DE RID: 41182 RVA: 0x004215A1 File Offset: 0x0041F9A1
		private void OnValueChangedMute(bool _value, int _idx)
		{
			this.soundData[_idx].Mute = !_value;
			this.sliderSound[_idx].interactable = _value;
		}

		// Token: 0x0600A0DF RID: 41183 RVA: 0x004215C2 File Offset: 0x0041F9C2
		private void OnValueChangedVolume(float _value, int _idx)
		{
			this.soundData[_idx].Volume = Mathf.FloorToInt(_value);
		}

		// Token: 0x0600A0E0 RID: 41184 RVA: 0x004215D8 File Offset: 0x0041F9D8
		private void Start()
		{
			this.soundData = new SoundData[]
			{
				Config.SoundData.Master,
				Config.SoundData.BGM,
				Config.SoundData.GameSE,
				Config.SoundData.SystemSE,
				Config.SoundData.ENV,
				Singleton<Voice>.Instance._Config.PCM
			};
			this.buttonColor.image.color = Config.GraphicData.BackColor;
			this.toggleShield.isOn = Config.GraphicData.Shield;
			for (int i = 0; i < 6; i++)
			{
				this.toggleSound[i].isOn = !this.soundData[i].Mute;
				this.sliderSound[i].interactable = !this.soundData[i].Mute;
				this.sliderSound[i].value = (float)this.soundData[i].Volume;
			}
			this.buttonColor.onClick.AddListener(new UnityAction(this.OnClickColor));
			this.toggleShield.onValueChanged.AddListener(delegate(bool v)
			{
				Config.GraphicData.Shield = v;
				Singleton<Studio>.Instance.cameraCtrl.isConfigVanish = v;
			});
			for (int j = 0; j < 3; j++)
			{
				byte limit = (byte)j;
				this.togglesTexture[j].onValueChanged.AddListener(delegate(bool _b)
				{
					if (_b)
					{
						Config.GraphicData.MapGraphicQuality = limit;
						if (QualitySettings.masterTextureLimit != (int)limit)
						{
							QualitySettings.masterTextureLimit = (int)limit;
						}
					}
				});
			}
			for (int k = 0; k < 6; k++)
			{
				int no = k;
				this.toggleSound[k].onValueChanged.AddListener(delegate(bool v)
				{
					this.OnValueChangedMute(v, no);
				});
				this.sliderSound[k].onValueChanged.AddListener(delegate(float v)
				{
					this.OnValueChangedVolume(v, no);
				});
			}
		}

		// Token: 0x04007F0E RID: 32526
		[SerializeField]
		private Button buttonColor;

		// Token: 0x04007F0F RID: 32527
		[SerializeField]
		private Toggle toggleShield;

		// Token: 0x04007F10 RID: 32528
		[SerializeField]
		private Toggle[] togglesTexture;

		// Token: 0x04007F11 RID: 32529
		[SerializeField]
		private Toggle[] toggleSound;

		// Token: 0x04007F12 RID: 32530
		[SerializeField]
		private Slider[] sliderSound;
	}
}
