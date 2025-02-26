using System;
using System.Collections.Generic;
using System.Linq;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x02000865 RID: 2149
	public class VoiceSetting : BaseSetting
	{
		// Token: 0x060036CF RID: 14031 RVA: 0x00145470 File Offset: 0x00143870
		public override void Init()
		{
			Dictionary<int, VoiceSystem.Voice> chara = Singleton<Voice>.Instance._Config.chara;
			this.Add(this.rtcPcm);
			for (int i = 0; i < this.node.transform.childCount; i++)
			{
				RectTransform trans = this.node.transform.GetChild(i) as RectTransform;
				this.Add(trans);
			}
			int num = this.node.transform.childCount + ((this.node.transform.childCount % 2 != 0) ? 1 : 0);
			foreach (KeyValuePair<int, string> keyValuePair in Singleton<Voice>.Instance.voiceInfoList.ToDictionary((VoiceInfo.Param v) => v.No, (VoiceInfo.Param v) => v.Personality))
			{
				if (chara.ContainsKey(keyValuePair.Key))
				{
					this.Create(num++, keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x001455C4 File Offset: 0x001439C4
		protected override void ValueToUI()
		{
			foreach (VoiceSetting.SetData setData in this.dic.Values)
			{
				setData.toggle.isOn = setData.sd.Mute;
				setData.slider.value = (float)setData.sd.Volume;
			}
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x0014564C File Offset: 0x00143A4C
		private void Create(int num, int key, string name)
		{
			RectTransform component = UnityEngine.Object.Instantiate<GameObject>(this.prefab, this.node.transform).GetComponent<RectTransform>();
			component.name = key.ToString();
			component.GetComponentInChildren<Text>().text = name;
			this.Add(key, component);
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x001456A0 File Offset: 0x00143AA0
		private bool Add(Transform trans)
		{
			if (this.dic.ContainsKey(trans))
			{
				return false;
			}
			VoiceSetting.SetData setData = new VoiceSetting.SetData
			{
				sd = Singleton<Voice>.Instance._Config.PCM,
				slider = trans.GetComponentInChildren<Slider>(),
				toggle = trans.GetComponentInChildren<Toggle>(),
				image = trans.GetComponentInChildren<Image>()
			};
			this.AddEvent(setData);
			this.dic.Add(trans, setData);
			return true;
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x00145718 File Offset: 0x00143B18
		private bool Add(int key, Transform trans)
		{
			if (this.dic.ContainsKey(trans))
			{
				return false;
			}
			VoiceSetting.SetData setData = new VoiceSetting.SetData
			{
				sd = Singleton<Voice>.Instance._Config.chara[key].sound,
				slider = trans.GetComponentInChildren<Slider>(),
				toggle = trans.GetComponentInChildren<Toggle>(),
				image = trans.GetComponentInChildren<Image>()
			};
			this.AddEvent(setData);
			this.dic.Add(trans, setData);
			return true;
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x0014579C File Offset: 0x00143B9C
		private void AddEvent(VoiceSetting.SetData data)
		{
			data.toggle.onValueChanged.AsObservable<bool>().Subscribe(delegate(bool isOn)
			{
				data.sd.Mute = isOn;
				data.image.enabled = !isOn;
				this.EnterSE();
			});
			(from b in data.toggle.OnValueChangedAsObservable()
			select !b).SubscribeToInteractable(data.slider);
			(from value in data.slider.onValueChanged.AsObservable<float>()
			select (int)value).Subscribe(delegate(int value)
			{
				data.sd.Volume = value;
			});
			(from _ in data.slider.OnPointerDownAsObservable()
			where UnityEngine.Input.GetMouseButtonDown(0)
			select _).Subscribe(delegate(PointerEventData _)
			{
				this.EnterSE();
			});
		}

		// Token: 0x0400376B RID: 14187
		[SerializeField]
		private GameObject prefab;

		// Token: 0x0400376C RID: 14188
		[SerializeField]
		private RectTransform rtcPcm;

		// Token: 0x0400376D RID: 14189
		[SerializeField]
		private RectTransform node;

		// Token: 0x0400376E RID: 14190
		private Dictionary<Transform, VoiceSetting.SetData> dic = new Dictionary<Transform, VoiceSetting.SetData>();

		// Token: 0x02000866 RID: 2150
		private class SetData
		{
			// Token: 0x04003774 RID: 14196
			public SoundData sd;

			// Token: 0x04003775 RID: 14197
			public Toggle toggle;

			// Token: 0x04003776 RID: 14198
			public Slider slider;

			// Token: 0x04003777 RID: 14199
			public Image image;
		}
	}
}
