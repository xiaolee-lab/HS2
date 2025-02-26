using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012C9 RID: 4809
	public class CameraLightCtrl : MonoBehaviour
	{
		// Token: 0x0600A06F RID: 41071 RVA: 0x0041F31C File Offset: 0x0041D71C
		public void Init()
		{
			this.lightChara.Init();
		}

		// Token: 0x0600A070 RID: 41072 RVA: 0x0041F329 File Offset: 0x0041D729
		public void Reflect()
		{
			this.lightChara.Reflect();
		}

		// Token: 0x0600A071 RID: 41073 RVA: 0x0041F336 File Offset: 0x0041D736
		private void OnEnable()
		{
			this.lightChara.UpdateUI();
		}

		// Token: 0x0600A072 RID: 41074 RVA: 0x0041F343 File Offset: 0x0041D743
		private void Start()
		{
			this.Init();
		}

		// Token: 0x04007EC5 RID: 32453
		[SerializeField]
		private CameraLightCtrl.LightCalc lightChara = new CameraLightCtrl.LightCalc();

		// Token: 0x020012CA RID: 4810
		public class LightInfo
		{
			// Token: 0x0600A074 RID: 41076 RVA: 0x0041F37C File Offset: 0x0041D77C
			public virtual void Init()
			{
				this.color = Utility.ConvertColor(255, 255, 255);
				this.intensity = 1f;
				this.rot[0] = 0f;
				this.rot[1] = 0f;
				this.shadow = true;
			}

			// Token: 0x0600A075 RID: 41077 RVA: 0x0041F3D0 File Offset: 0x0041D7D0
			public virtual void Save(BinaryWriter _writer, Version _version)
			{
				_writer.Write(JsonUtility.ToJson(this.color));
				_writer.Write(this.intensity);
				_writer.Write(this.rot[0]);
				_writer.Write(this.rot[1]);
				_writer.Write(this.shadow);
			}

			// Token: 0x0600A076 RID: 41078 RVA: 0x0041F428 File Offset: 0x0041D828
			public virtual void Load(BinaryReader _reader, Version _version)
			{
				this.color = JsonUtility.FromJson<Color>(_reader.ReadString());
				this.intensity = _reader.ReadSingle();
				this.rot[0] = _reader.ReadSingle();
				this.rot[1] = _reader.ReadSingle();
				this.shadow = _reader.ReadBoolean();
			}

			// Token: 0x04007EC6 RID: 32454
			public Color color = Color.white;

			// Token: 0x04007EC7 RID: 32455
			public float intensity = 1f;

			// Token: 0x04007EC8 RID: 32456
			public float[] rot = new float[2];

			// Token: 0x04007EC9 RID: 32457
			public bool shadow = true;
		}

		// Token: 0x020012CB RID: 4811
		public class MapLightInfo : CameraLightCtrl.LightInfo
		{
			// Token: 0x0600A078 RID: 41080 RVA: 0x0041F489 File Offset: 0x0041D889
			public override void Init()
			{
				base.Init();
				this.type = LightType.Directional;
			}

			// Token: 0x0600A079 RID: 41081 RVA: 0x0041F498 File Offset: 0x0041D898
			public override void Save(BinaryWriter _writer, Version _version)
			{
				base.Save(_writer, _version);
				_writer.Write((int)this.type);
			}

			// Token: 0x0600A07A RID: 41082 RVA: 0x0041F4AE File Offset: 0x0041D8AE
			public override void Load(BinaryReader _reader, Version _version)
			{
				base.Load(_reader, _version);
				this.type = (LightType)_reader.ReadInt32();
			}

			// Token: 0x04007ECA RID: 32458
			public LightType type = LightType.Directional;
		}

		// Token: 0x020012CC RID: 4812
		[Serializable]
		private class LightCalc
		{
			// Token: 0x170021EA RID: 8682
			// (get) Token: 0x0600A07C RID: 41084 RVA: 0x0041F4CC File Offset: 0x0041D8CC
			// (set) Token: 0x0600A07D RID: 41085 RVA: 0x0041F4D4 File Offset: 0x0041D8D4
			public bool isInit { get; private set; }

			// Token: 0x0600A07E RID: 41086 RVA: 0x0041F4E0 File Offset: 0x0041D8E0
			public void Init()
			{
				if (this.isInit)
				{
					return;
				}
				this.buttonColor.onClick.AddListener(new UnityAction(this.OnClickColor));
				this.toggleShadow.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangeShadow));
				this.sliderIntensity.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangeIntensity));
				this.inputIntensity.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditIntensity));
				this.buttonIntensity.onClick.AddListener(new UnityAction(this.OnClickIntensity));
				for (int i = 0; i < 2; i++)
				{
					int axis = i;
					this.sliderAxis[axis].onValueChanged.AddListener(delegate(float f)
					{
						this.OnValueChangeAxis(f, axis);
					});
					this.inputAxis[axis].onEndEdit.AddListener(delegate(string s)
					{
						this.OnEndEditAxis(s, axis);
					});
					this.buttonAxis[axis].onClick.AddListener(delegate()
					{
						this.OnClickAxis(axis);
					});
				}
				this.Reflect();
				this.isInit = true;
			}

			// Token: 0x0600A07F RID: 41087 RVA: 0x0041F624 File Offset: 0x0041DA24
			public void UpdateUI()
			{
				this.isUpdateInfo = true;
				this.buttonColor.image.color = Singleton<Studio>.Instance.sceneInfo.charaLight.color;
				this.sliderIntensity.value = Singleton<Studio>.Instance.sceneInfo.charaLight.intensity;
				this.inputIntensity.text = Singleton<Studio>.Instance.sceneInfo.charaLight.intensity.ToString("0.00");
				for (int i = 0; i < 2; i++)
				{
					this.sliderAxis[i].value = Singleton<Studio>.Instance.sceneInfo.charaLight.rot[i];
					this.inputAxis[i].text = Singleton<Studio>.Instance.sceneInfo.charaLight.rot[i].ToString("000");
				}
				this.toggleShadow.isOn = Singleton<Studio>.Instance.sceneInfo.charaLight.shadow;
				this.isUpdateInfo = false;
			}

			// Token: 0x0600A080 RID: 41088 RVA: 0x0041F730 File Offset: 0x0041DB30
			public void Reflect()
			{
				this.light.color = Singleton<Studio>.Instance.sceneInfo.charaLight.color;
				this.light.intensity = Singleton<Studio>.Instance.sceneInfo.charaLight.intensity;
				this.transRoot.localRotation = Quaternion.Euler(Singleton<Studio>.Instance.sceneInfo.charaLight.rot[0], Singleton<Studio>.Instance.sceneInfo.charaLight.rot[1], 0f);
				this.light.shadows = ((!Singleton<Studio>.Instance.sceneInfo.charaLight.shadow) ? LightShadows.None : LightShadows.Soft);
			}

			// Token: 0x0600A081 RID: 41089 RVA: 0x0041F7E8 File Offset: 0x0041DBE8
			private void OnClickColor()
			{
				Singleton<Studio>.Instance.colorPalette.Setup("キャラライト", Singleton<Studio>.Instance.sceneInfo.charaLight.color, new Action<Color>(this.OnValueChangeColor), false);
				Singleton<Studio>.Instance.colorPalette.visible = true;
			}

			// Token: 0x0600A082 RID: 41090 RVA: 0x0041F83A File Offset: 0x0041DC3A
			private void OnValueChangeColor(Color _color)
			{
				this.buttonColor.image.color = _color;
				Singleton<Studio>.Instance.sceneInfo.charaLight.color = _color;
				this.Reflect();
			}

			// Token: 0x0600A083 RID: 41091 RVA: 0x0041F868 File Offset: 0x0041DC68
			private void OnValueChangeShadow(bool _value)
			{
				if (this.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.charaLight.shadow = _value;
				this.Reflect();
			}

			// Token: 0x0600A084 RID: 41092 RVA: 0x0041F891 File Offset: 0x0041DC91
			private void OnValueChangeIntensity(float _value)
			{
				if (this.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.charaLight.intensity = _value;
				this.inputIntensity.text = _value.ToString("0.00");
				this.Reflect();
			}

			// Token: 0x0600A085 RID: 41093 RVA: 0x0041F8D4 File Offset: 0x0041DCD4
			private void OnEndEditIntensity(string _text)
			{
				float num = Mathf.Clamp(Utility.StringToFloat(_text), 0.1f, 2f);
				Singleton<Studio>.Instance.sceneInfo.charaLight.intensity = num;
				this.sliderIntensity.value = num;
				this.Reflect();
			}

			// Token: 0x0600A086 RID: 41094 RVA: 0x0041F920 File Offset: 0x0041DD20
			private void OnClickIntensity()
			{
				Singleton<Studio>.Instance.sceneInfo.charaLight.intensity = 1f;
				this.sliderIntensity.value = 1f;
				this.inputIntensity.text = Singleton<Studio>.Instance.sceneInfo.charaLight.intensity.ToString("0.00");
				this.Reflect();
			}

			// Token: 0x0600A087 RID: 41095 RVA: 0x0041F988 File Offset: 0x0041DD88
			private void OnValueChangeAxis(float _value, int _axis)
			{
				if (this.isUpdateInfo)
				{
					return;
				}
				Singleton<Studio>.Instance.sceneInfo.charaLight.rot[_axis] = _value;
				this.inputAxis[_axis].text = _value.ToString("000");
				this.Reflect();
			}

			// Token: 0x0600A088 RID: 41096 RVA: 0x0041F9D8 File Offset: 0x0041DDD8
			private void OnEndEditAxis(string _text, int _axis)
			{
				float num = Mathf.Clamp(Utility.StringToFloat(_text), 0f, 359f);
				Singleton<Studio>.Instance.sceneInfo.charaLight.rot[_axis] = num;
				this.sliderAxis[_axis].value = num;
				this.Reflect();
			}

			// Token: 0x0600A089 RID: 41097 RVA: 0x0041FA28 File Offset: 0x0041DE28
			private void OnClickAxis(int _axis)
			{
				Singleton<Studio>.Instance.sceneInfo.charaLight.rot[_axis] = 0f;
				this.sliderAxis[_axis].value = 0f;
				this.inputAxis[_axis].text = Singleton<Studio>.Instance.sceneInfo.charaLight.rot[_axis].ToString("000");
				this.Reflect();
			}

			// Token: 0x04007ECB RID: 32459
			public Light light;

			// Token: 0x04007ECC RID: 32460
			public Transform transRoot;

			// Token: 0x04007ECD RID: 32461
			public Button buttonColor;

			// Token: 0x04007ECE RID: 32462
			public Toggle toggleShadow;

			// Token: 0x04007ECF RID: 32463
			public Slider sliderIntensity;

			// Token: 0x04007ED0 RID: 32464
			public InputField inputIntensity;

			// Token: 0x04007ED1 RID: 32465
			public Button buttonIntensity;

			// Token: 0x04007ED2 RID: 32466
			public Slider[] sliderAxis;

			// Token: 0x04007ED3 RID: 32467
			public InputField[] inputAxis;

			// Token: 0x04007ED4 RID: 32468
			public Button[] buttonAxis;

			// Token: 0x04007ED5 RID: 32469
			private bool isUpdateInfo;
		}
	}
}
