using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001322 RID: 4898
	public class MPLightCtrl : MonoBehaviour
	{
		// Token: 0x1700225B RID: 8795
		// (get) Token: 0x0600A3B8 RID: 41912 RVA: 0x0042E87D File Offset: 0x0042CC7D
		// (set) Token: 0x0600A3B9 RID: 41913 RVA: 0x0042E885 File Offset: 0x0042CC85
		public OCILight ociLight
		{
			get
			{
				return this.m_OCILight;
			}
			set
			{
				this.m_OCILight = value;
				if (this.m_OCILight != null)
				{
					this.UpdateInfo();
				}
			}
		}

		// Token: 0x1700225C RID: 8796
		// (get) Token: 0x0600A3BA RID: 41914 RVA: 0x0042E89F File Offset: 0x0042CC9F
		// (set) Token: 0x0600A3BB RID: 41915 RVA: 0x0042E8AC File Offset: 0x0042CCAC
		public bool active
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				base.gameObject.SetActive(value);
				if (!base.gameObject.activeSelf)
				{
					if (this.isColorFunc)
					{
						Singleton<Studio>.Instance.colorPalette.Close();
					}
					this.isColorFunc = false;
				}
			}
		}

		// Token: 0x0600A3BC RID: 41916 RVA: 0x0042E8EB File Offset: 0x0042CCEB
		public bool Deselect(OCILight _ociLight)
		{
			if (this.m_OCILight != _ociLight)
			{
				return false;
			}
			this.ociLight = null;
			this.active = false;
			return true;
		}

		// Token: 0x0600A3BD RID: 41917 RVA: 0x0042E90C File Offset: 0x0042CD0C
		private void UpdateInfo()
		{
			this.isUpdateInfo = true;
			if (this.m_OCILight == null)
			{
				return;
			}
			LightType lightType = this.m_OCILight.lightType;
			if (lightType != LightType.Directional)
			{
				if (lightType != LightType.Point)
				{
					if (lightType == LightType.Spot)
					{
						this.viRange.slider.minValue = 0.5f;
						this.viRange.slider.maxValue = 100f;
					}
				}
				else
				{
					this.viRange.slider.minValue = 0.1f;
					this.viRange.slider.maxValue = 100f;
				}
			}
			this.toggleVisible.isOn = this.m_OCILight.lightInfo.enable;
			this.toggleTarget.isOn = this.m_OCILight.lightInfo.drawTarget;
			this.toggleShadow.isOn = this.m_OCILight.lightInfo.shadow;
			if (this.imageSample)
			{
				this.imageSample.color = this.m_OCILight.lightInfo.color;
			}
			this.viIntensity.slider.value = this.m_OCILight.lightInfo.intensity;
			this.viIntensity.inputField.text = this.m_OCILight.lightInfo.intensity.ToString("0.000");
			this.viRange.slider.value = this.m_OCILight.lightInfo.range;
			this.viRange.inputField.text = this.m_OCILight.lightInfo.range.ToString("0.000");
			this.viSpotAngle.slider.value = this.m_OCILight.lightInfo.spotAngle;
			this.viSpotAngle.inputField.text = this.m_OCILight.lightInfo.spotAngle.ToString("0.000");
			LightType lightType2 = this.m_OCILight.lightType;
			if (lightType2 != LightType.Directional)
			{
				if (lightType2 != LightType.Point)
				{
					if (lightType2 == LightType.Spot)
					{
						this.backgroundInfoDirectional.active = false;
						this.backgroundInfoPoint.active = false;
						this.backgroundInfoSpot.active = true;
						this.backgroundInfoSpot.target = this.m_OCILight.lightTarget;
						this.viRange.active = true;
						this.viSpotAngle.active = true;
					}
				}
				else
				{
					this.backgroundInfoDirectional.active = false;
					this.backgroundInfoPoint.active = true;
					this.backgroundInfoPoint.target = this.m_OCILight.lightTarget;
					this.backgroundInfoSpot.active = false;
					this.viRange.active = true;
					this.viSpotAngle.active = false;
				}
			}
			else
			{
				this.backgroundInfoDirectional.active = true;
				this.backgroundInfoDirectional.target = this.m_OCILight.lightTarget;
				this.backgroundInfoPoint.active = false;
				this.backgroundInfoSpot.active = false;
				this.viRange.active = false;
				this.viSpotAngle.active = false;
			}
			this.isUpdateInfo = false;
		}

		// Token: 0x0600A3BE RID: 41918 RVA: 0x0042EC40 File Offset: 0x0042D040
		private void OnClickColor()
		{
			Singleton<Studio>.Instance.colorPalette.Setup("ライト", this.m_OCILight.lightInfo.color, new Action<Color>(this.OnValueChangeColor), false);
			this.isColorFunc = true;
			Singleton<Studio>.Instance.colorPalette.visible = true;
		}

		// Token: 0x0600A3BF RID: 41919 RVA: 0x0042EC95 File Offset: 0x0042D095
		private void OnValueChangeColor(Color _color)
		{
			if (this.m_OCILight != null)
			{
				this.m_OCILight.SetColor(_color);
			}
			if (this.imageSample)
			{
				this.imageSample.color = _color;
			}
		}

		// Token: 0x0600A3C0 RID: 41920 RVA: 0x0042ECCA File Offset: 0x0042D0CA
		private void OnValueChangeEnable(bool _value)
		{
			this.m_OCILight.SetEnable(_value, false);
		}

		// Token: 0x0600A3C1 RID: 41921 RVA: 0x0042ECDA File Offset: 0x0042D0DA
		private void OnValueChangeDrawTarget(bool _value)
		{
			this.m_OCILight.SetDrawTarget(_value, false);
		}

		// Token: 0x0600A3C2 RID: 41922 RVA: 0x0042ECEA File Offset: 0x0042D0EA
		private void OnValueChangeShadow(bool _value)
		{
			this.m_OCILight.SetShadow(_value, false);
		}

		// Token: 0x0600A3C3 RID: 41923 RVA: 0x0042ECFC File Offset: 0x0042D0FC
		private void OnValueChangeIntensity(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			if (this.m_OCILight.SetIntensity(Mathf.Clamp(_value, 0.1f, 2f), false))
			{
				this.viIntensity.inputField.text = this.m_OCILight.lightInfo.intensity.ToString("0.00");
			}
		}

		// Token: 0x0600A3C4 RID: 41924 RVA: 0x0042ED60 File Offset: 0x0042D160
		private void OnEndEditIntensity(string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), 0.1f, 2f);
			this.m_OCILight.SetIntensity(value, false);
			this.viIntensity.inputField.text = this.m_OCILight.lightInfo.intensity.ToString("0.00");
			this.viIntensity.slider.value = this.m_OCILight.lightInfo.intensity;
		}

		// Token: 0x0600A3C5 RID: 41925 RVA: 0x0042EDDC File Offset: 0x0042D1DC
		private void OnClickIntensity()
		{
			if (this.m_OCILight.SetIntensity(1f, false))
			{
				this.viIntensity.inputField.text = this.m_OCILight.lightInfo.intensity.ToString("0.00");
				this.viIntensity.slider.value = this.m_OCILight.lightInfo.intensity;
			}
		}

		// Token: 0x0600A3C6 RID: 41926 RVA: 0x0042EE4C File Offset: 0x0042D24C
		private void OnValueChangeRange(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			if (this.m_OCILight.SetRange(_value, false))
			{
				this.viRange.inputField.text = this.m_OCILight.lightInfo.range.ToString("0.000");
			}
		}

		// Token: 0x0600A3C7 RID: 41927 RVA: 0x0042EEA4 File Offset: 0x0042D2A4
		private void OnEndEditRange(string _text)
		{
			float value = Mathf.Max((this.m_OCILight.lightType != LightType.Spot) ? 0.1f : 0.5f, Utility.StringToFloat(_text));
			this.m_OCILight.SetRange(value, false);
			this.viRange.inputField.text = this.m_OCILight.lightInfo.range.ToString("0.000");
			this.viRange.slider.value = this.m_OCILight.lightInfo.range;
		}

		// Token: 0x0600A3C8 RID: 41928 RVA: 0x0042EF34 File Offset: 0x0042D334
		private void OnClickRange()
		{
			if (this.m_OCILight.SetRange(30f, false))
			{
				this.viRange.inputField.text = this.m_OCILight.lightInfo.range.ToString("0.000");
				this.viRange.slider.value = this.m_OCILight.lightInfo.range;
			}
		}

		// Token: 0x0600A3C9 RID: 41929 RVA: 0x0042EFA4 File Offset: 0x0042D3A4
		private void OnValueChangeSpotAngle(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			if (this.m_OCILight.SetSpotAngle(_value, false))
			{
				this.viSpotAngle.inputField.text = this.m_OCILight.lightInfo.spotAngle.ToString("0.000");
			}
		}

		// Token: 0x0600A3CA RID: 41930 RVA: 0x0042EFFC File Offset: 0x0042D3FC
		private void OnEndEditSpotAngle(string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), 1f, 179f);
			this.m_OCILight.SetSpotAngle(value, false);
			this.viSpotAngle.inputField.text = this.m_OCILight.lightInfo.spotAngle.ToString("0.000");
			this.viSpotAngle.slider.value = this.m_OCILight.lightInfo.spotAngle;
		}

		// Token: 0x0600A3CB RID: 41931 RVA: 0x0042F078 File Offset: 0x0042D478
		private void OnClickSpotAngle()
		{
			if (this.m_OCILight.SetSpotAngle(30f, false))
			{
				this.viSpotAngle.inputField.text = this.m_OCILight.lightInfo.spotAngle.ToString("0.000");
				this.viSpotAngle.slider.value = this.m_OCILight.lightInfo.spotAngle;
			}
		}

		// Token: 0x0600A3CC RID: 41932 RVA: 0x0042F0E8 File Offset: 0x0042D4E8
		private void Awake()
		{
			this.buttonSample.onClick.AddListener(new UnityAction(this.OnClickColor));
			this.toggleVisible.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangeEnable));
			this.toggleTarget.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangeDrawTarget));
			this.toggleShadow.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangeShadow));
			this.viIntensity.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangeIntensity));
			this.viIntensity.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditIntensity));
			this.viIntensity.button.onClick.AddListener(new UnityAction(this.OnClickIntensity));
			this.viRange.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangeRange));
			this.viRange.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditRange));
			this.viRange.button.onClick.AddListener(new UnityAction(this.OnClickRange));
			this.viSpotAngle.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangeSpotAngle));
			this.viSpotAngle.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSpotAngle));
			this.viSpotAngle.button.onClick.AddListener(new UnityAction(this.OnClickSpotAngle));
			this.isUpdateInfo = false;
		}

		// Token: 0x04008117 RID: 33047
		[SerializeField]
		private MPLightCtrl.BackgroundInfo backgroundInfoDirectional = new MPLightCtrl.BackgroundInfo();

		// Token: 0x04008118 RID: 33048
		[SerializeField]
		private MPLightCtrl.BackgroundInfo backgroundInfoPoint = new MPLightCtrl.BackgroundInfo();

		// Token: 0x04008119 RID: 33049
		[SerializeField]
		private MPLightCtrl.BackgroundInfo backgroundInfoSpot = new MPLightCtrl.BackgroundInfo();

		// Token: 0x0400811A RID: 33050
		[SerializeField]
		private Image imageSample;

		// Token: 0x0400811B RID: 33051
		[SerializeField]
		private Button buttonSample;

		// Token: 0x0400811C RID: 33052
		[SerializeField]
		private Toggle toggleVisible;

		// Token: 0x0400811D RID: 33053
		[SerializeField]
		private Toggle toggleTarget;

		// Token: 0x0400811E RID: 33054
		[SerializeField]
		private Toggle toggleShadow;

		// Token: 0x0400811F RID: 33055
		[SerializeField]
		private MPLightCtrl.ValueInfo viIntensity;

		// Token: 0x04008120 RID: 33056
		[SerializeField]
		private MPLightCtrl.ValueInfo viRange;

		// Token: 0x04008121 RID: 33057
		[SerializeField]
		private MPLightCtrl.ValueInfo viSpotAngle;

		// Token: 0x04008122 RID: 33058
		private OCILight m_OCILight;

		// Token: 0x04008123 RID: 33059
		private bool isUpdateInfo;

		// Token: 0x04008124 RID: 33060
		private bool isColorFunc;

		// Token: 0x02001323 RID: 4899
		[Serializable]
		private class BackgroundInfo
		{
			// Token: 0x1700225D RID: 8797
			// (set) Token: 0x0600A3CE RID: 41934 RVA: 0x0042F29D File Offset: 0x0042D69D
			public bool active
			{
				set
				{
					if (this.obj.activeSelf != value)
					{
						this.obj.SetActive(value);
					}
				}
			}

			// Token: 0x1700225E RID: 8798
			// (set) Token: 0x0600A3CF RID: 41935 RVA: 0x0042F2BC File Offset: 0x0042D6BC
			public Info.LightLoadInfo.Target target
			{
				set
				{
					this.image.sprite = this.sprit[(int)value];
				}
			}

			// Token: 0x04008125 RID: 33061
			public GameObject obj;

			// Token: 0x04008126 RID: 33062
			public Sprite[] sprit;

			// Token: 0x04008127 RID: 33063
			public Image image;
		}

		// Token: 0x02001324 RID: 4900
		[Serializable]
		private class ValueInfo
		{
			// Token: 0x1700225F RID: 8799
			// (set) Token: 0x0600A3D1 RID: 41937 RVA: 0x0042F2D9 File Offset: 0x0042D6D9
			public bool active
			{
				set
				{
					if (this.obj.activeSelf != value)
					{
						this.obj.SetActive(value);
					}
				}
			}

			// Token: 0x04008128 RID: 33064
			public GameObject obj;

			// Token: 0x04008129 RID: 33065
			public Slider slider;

			// Token: 0x0400812A RID: 33066
			public InputField inputField;

			// Token: 0x0400812B RID: 33067
			public Button button;
		}
	}
}
