using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012E2 RID: 4834
	public class AnimeControl : MonoBehaviour
	{
		// Token: 0x170021FE RID: 8702
		// (get) Token: 0x0600A132 RID: 41266 RVA: 0x00422FF1 File Offset: 0x004213F1
		// (set) Token: 0x0600A133 RID: 41267 RVA: 0x00422FF9 File Offset: 0x004213F9
		public ObjectCtrlInfo objectCtrlInfo
		{
			get
			{
				return this.m_ObjectCtrlInfo;
			}
			set
			{
				this.m_ObjectCtrlInfo = value;
				if (this.m_ObjectCtrlInfo != null)
				{
					this.UpdateInfo();
				}
			}
		}

		// Token: 0x170021FF RID: 8703
		// (set) Token: 0x0600A134 RID: 41268 RVA: 0x00423013 File Offset: 0x00421413
		public bool active
		{
			set
			{
				if (base.gameObject.activeSelf != value)
				{
					base.gameObject.SetActive(value);
				}
			}
		}

		// Token: 0x0600A135 RID: 41269 RVA: 0x00423034 File Offset: 0x00421434
		private void OnValueChangedSpeed(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			if (this.arrayTarget.IsNullOrEmpty<ObjectCtrlInfo>())
			{
				this.OnPointerDownSpeed(null);
			}
			for (int i = 0; i < this.num; i++)
			{
				this.arrayTarget[i].animeSpeed = _value;
			}
			this.inputSpeed.text = _value.ToString("0.00");
		}

		// Token: 0x0600A136 RID: 41270 RVA: 0x004230A0 File Offset: 0x004214A0
		private void OnPointerDownSpeed(BaseEventData _data)
		{
			if (!this.arrayTarget.IsNullOrEmpty<ObjectCtrlInfo>())
			{
				return;
			}
			this.arrayTarget = (from v in Singleton<Studio>.Instance.treeNodeCtrl.selectObjectCtrl
			where v.kind == 0 || v.kind == 1
			select v).ToArray<ObjectCtrlInfo>();
			this.num = this.arrayTarget.Length;
			this.oldValue = (from v in this.arrayTarget
			select v.animeSpeed).ToArray<float>();
		}

		// Token: 0x0600A137 RID: 41271 RVA: 0x0042313C File Offset: 0x0042153C
		private void OnPointerUpSpeed(BaseEventData _data)
		{
			if (this.arrayTarget.Length == 0)
			{
				return;
			}
			Singleton<UndoRedoManager>.Instance.Do(new AnimeCommand.SpeedCommand((from v in this.arrayTarget
			select v.objectInfo.dicKey).ToArray<int>(), this.sliderSpeed.value, this.oldValue));
			this.arrayTarget = null;
			this.num = 0;
		}

		// Token: 0x0600A138 RID: 41272 RVA: 0x004231B4 File Offset: 0x004215B4
		private void OnEndEditSpeed(string _text)
		{
			float num = Mathf.Clamp(Utility.StringToFloat(_text), 0f, 3f);
			this.arrayTarget = (from v in Singleton<Studio>.Instance.treeNodeCtrl.selectObjectCtrl
			where v.kind == 0 || v.kind == 1
			select v).ToArray<ObjectCtrlInfo>();
			this.num = this.arrayTarget.Length;
			this.oldValue = (from v in this.arrayTarget
			select v.animeSpeed).ToArray<float>();
			Singleton<UndoRedoManager>.Instance.Do(new AnimeCommand.SpeedCommand((from v in this.arrayTarget
			select v.objectInfo.dicKey).ToArray<int>(), num, this.oldValue));
			this.isUpdateInfo = true;
			this.sliderSpeed.value = num;
			this.inputSpeed.text = num.ToString("0.00");
			this.isUpdateInfo = false;
			this.arrayTarget = null;
			this.num = 0;
		}

		// Token: 0x0600A139 RID: 41273 RVA: 0x004232D8 File Offset: 0x004216D8
		private void OnValueChangedPattern(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			if (this.arrayTarget.IsNullOrEmpty<ObjectCtrlInfo>())
			{
				this.OnPointerDownPattern(null);
			}
			float animePattern = Mathf.Lerp(0f, 1f, _value);
			for (int i = 0; i < this.num; i++)
			{
				(this.arrayTarget[i] as OCIChar).animePattern = animePattern;
			}
			this.inputPattern.text = animePattern.ToString("0.00");
		}

		// Token: 0x0600A13A RID: 41274 RVA: 0x0042335C File Offset: 0x0042175C
		private void OnPointerDownPattern(BaseEventData _data)
		{
			if (!this.arrayTarget.IsNullOrEmpty<ObjectCtrlInfo>())
			{
				return;
			}
			this.arrayTarget = (from v in Singleton<Studio>.Instance.treeNodeCtrl.selectObjectCtrl
			where v.kind == 0
			select v).ToArray<ObjectCtrlInfo>();
			this.num = this.arrayTarget.Length;
			this.oldValue = (from v in this.arrayTarget
			select (v as OCIChar).animePattern).ToArray<float>();
		}

		// Token: 0x0600A13B RID: 41275 RVA: 0x004233F8 File Offset: 0x004217F8
		private void OnPointerUpPattern(BaseEventData _data)
		{
			if (this.arrayTarget.Length == 0)
			{
				return;
			}
			float value = Mathf.Lerp(0f, 1f, this.sliderPattern.value);
			Singleton<UndoRedoManager>.Instance.Do(new AnimeCommand.PatternCommand((from v in this.arrayTarget
			select v.objectInfo.dicKey).ToArray<int>(), value, this.oldValue));
		}

		// Token: 0x0600A13C RID: 41276 RVA: 0x00423474 File Offset: 0x00421874
		private void OnEndEditPattern(string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), 0f, 1f);
			this.arrayTarget = (from v in Singleton<Studio>.Instance.treeNodeCtrl.selectObjectCtrl
			where v.kind == 0
			select v).ToArray<ObjectCtrlInfo>();
			this.num = this.arrayTarget.Length;
			this.oldValue = (from v in this.arrayTarget
			select (v as OCIChar).animePattern).ToArray<float>();
			Singleton<UndoRedoManager>.Instance.Do(new AnimeCommand.PatternCommand((from v in this.arrayTarget
			select v.objectInfo.dicKey).ToArray<int>(), value, this.oldValue));
			this.isUpdateInfo = true;
			this.sliderPattern.value = Mathf.InverseLerp(0f, 1f, value);
			this.inputPattern.text = value.ToString("0.00");
			this.isUpdateInfo = false;
			this.arrayTarget = null;
			this.num = 0;
		}

		// Token: 0x0600A13D RID: 41277 RVA: 0x004235A8 File Offset: 0x004219A8
		private void ChangedOptionParam(float _value, int _kind)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			if (this.arrayTarget.IsNullOrEmpty<ObjectCtrlInfo>())
			{
				this.DownOptionParam(null, _kind);
			}
			float num = Mathf.Lerp(0f, 1f, _value);
			for (int i = 0; i < this.num; i++)
			{
				OCIChar ocichar = this.arrayTarget[i] as OCIChar;
				if (_kind == 0)
				{
					ocichar.animeOptionParam1 = num;
				}
				else
				{
					ocichar.animeOptionParam2 = num;
				}
			}
			this.inputOptionParam[_kind].text = num.ToString("0.00");
		}

		// Token: 0x0600A13E RID: 41278 RVA: 0x00423644 File Offset: 0x00421A44
		private void DownOptionParam(BaseEventData _data, int _kind)
		{
			if (!this.arrayTarget.IsNullOrEmpty<ObjectCtrlInfo>())
			{
				return;
			}
			this.arrayTarget = (from v in Singleton<Studio>.Instance.treeNodeCtrl.selectObjectCtrl
			where v.kind == 0
			select v).ToArray<ObjectCtrlInfo>();
			this.num = this.arrayTarget.Length;
			this.oldValue = (from v in this.arrayTarget
			select (v as OCIChar).animeOptionParam[_kind]).ToArray<float>();
		}

		// Token: 0x0600A13F RID: 41279 RVA: 0x004236DC File Offset: 0x00421ADC
		private void UpOptionParam(BaseEventData _data, int _kind)
		{
			if (this.arrayTarget.Length == 0)
			{
				return;
			}
			float value = Mathf.Lerp(0f, 1f, this.sliderOptionParam[_kind].value);
			Singleton<UndoRedoManager>.Instance.Do(new AnimeCommand.OptionParamCommand((from v in this.arrayTarget
			select v.objectInfo.dicKey).ToArray<int>(), value, this.oldValue, _kind));
		}

		// Token: 0x0600A140 RID: 41280 RVA: 0x00423758 File Offset: 0x00421B58
		private void EndEditOptionParam(string _text, int _kind)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), 0f, 1f);
			this.arrayTarget = (from v in Singleton<Studio>.Instance.treeNodeCtrl.selectObjectCtrl
			where v.kind == 0
			select v).ToArray<ObjectCtrlInfo>();
			this.num = this.arrayTarget.Length;
			this.oldValue = (from v in this.arrayTarget
			select (v as OCIChar).animeOptionParam[_kind]).ToArray<float>();
			Singleton<UndoRedoManager>.Instance.Do(new AnimeCommand.OptionParamCommand((from v in this.arrayTarget
			select v.objectInfo.dicKey).ToArray<int>(), value, this.oldValue, _kind));
			this.isUpdateInfo = true;
			this.sliderOptionParam[_kind].value = value;
			this.inputOptionParam[_kind].text = value.ToString("0.00");
			this.isUpdateInfo = false;
			this.arrayTarget = null;
			this.num = 0;
		}

		// Token: 0x0600A141 RID: 41281 RVA: 0x0042388C File Offset: 0x00421C8C
		private void OnValueChangedOption(bool _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			OCIChar ocichar = this.m_ObjectCtrlInfo as OCIChar;
			if (ocichar != null)
			{
				ocichar.optionItemCtrl.visible = _value;
			}
		}

		// Token: 0x0600A142 RID: 41282 RVA: 0x004238C4 File Offset: 0x00421CC4
		private void OnValueChangedLoop(bool _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			OCIChar ocichar = this.m_ObjectCtrlInfo as OCIChar;
			if (ocichar != null)
			{
				ocichar.charAnimeCtrl.isForceLoop = _value;
			}
		}

		// Token: 0x0600A143 RID: 41283 RVA: 0x004238FC File Offset: 0x00421CFC
		private void OnClickRestart()
		{
			OCIChar[] array = (from v in Singleton<Studio>.Instance.treeNodeCtrl.selectObjectCtrl
			select v as OCIChar into v
			where v != null
			select v).ToArray<OCIChar>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].RestartAnime();
			}
			OCIItem[] array2 = (from v in Singleton<Studio>.Instance.treeNodeCtrl.selectObjectCtrl
			select v as OCIItem into v
			where v != null
			select v).ToArray<OCIItem>();
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].RestartAnime();
			}
		}

		// Token: 0x0600A144 RID: 41284 RVA: 0x004239F4 File Offset: 0x00421DF4
		private void OnClickAllRestart()
		{
			OCIChar[] array = (from v in Singleton<Studio>.Instance.dicObjectCtrl
			where v.Value.kind == 0
			select v.Value as OCIChar).ToArray<OCIChar>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].RestartAnime();
			}
			OCIItem[] array2 = (from v in Singleton<Studio>.Instance.dicObjectCtrl
			where v.Value.kind == 1
			select v.Value as OCIItem).ToArray<OCIItem>();
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].RestartAnime();
			}
		}

		// Token: 0x0600A145 RID: 41285 RVA: 0x00423AE4 File Offset: 0x00421EE4
		private void OnClickCopy()
		{
			OCIChar[] array = (from v in Singleton<Studio>.Instance.treeNodeCtrl.selectObjectCtrl
			select v as OCIChar into v
			where v != null
			select v).ToArray<OCIChar>();
			if (array.Length == 0)
			{
				return;
			}
			this.animeInfo.Copy(array[0].oiCharInfo.animeInfo);
			this.sex = array[0].oiCharInfo.sex;
		}

		// Token: 0x0600A146 RID: 41286 RVA: 0x00423B80 File Offset: 0x00421F80
		private void OnClickPaste()
		{
			if (this.sex == -1 || !this.animeInfo.exist)
			{
				return;
			}
			OCIChar[] array = (from v in Singleton<Studio>.Instance.treeNodeCtrl.selectObjectCtrl
			select v as OCIChar into v
			where v != null
			select v).ToArray<OCIChar>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].LoadAnime(this.animeInfo.group, this.animeInfo.category, this.animeInfo.no, 0f);
			}
		}

		// Token: 0x0600A147 RID: 41287 RVA: 0x00423C48 File Offset: 0x00422048
		private void Init()
		{
			this.sliderSpeed.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedSpeed));
			this.AddEventTrigger(this.eventSpeed, EventTriggerType.PointerDown, new UnityAction<BaseEventData>(this.OnPointerDownSpeed));
			this.AddEventTrigger(this.eventSpeed, EventTriggerType.PointerUp, new UnityAction<BaseEventData>(this.OnPointerUpSpeed));
			this.inputSpeed.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSpeed));
			this.sliderPattern.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedPattern));
			this.AddEventTrigger(this.eventPattern, EventTriggerType.PointerDown, new UnityAction<BaseEventData>(this.OnPointerDownPattern));
			this.AddEventTrigger(this.eventPattern, EventTriggerType.PointerUp, new UnityAction<BaseEventData>(this.OnPointerUpPattern));
			this.inputPattern.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditPattern));
			for (int i = 0; i < 2; i++)
			{
				int kind = i;
				this.sliderOptionParam[i].onValueChanged.AddListener(delegate(float t)
				{
					this.ChangedOptionParam(t, kind);
				});
				this.AddEventTrigger(this.eventOptionParam[i], EventTriggerType.PointerDown, delegate(BaseEventData d)
				{
					this.DownOptionParam(d, kind);
				});
				this.AddEventTrigger(this.eventOptionParam[i], EventTriggerType.PointerUp, delegate(BaseEventData d)
				{
					this.UpOptionParam(d, kind);
				});
				this.inputOptionParam[i].onEndEdit.AddListener(delegate(string s)
				{
					this.EndEditOptionParam(s, kind);
				});
			}
			this.toggleOption.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedOption));
			this.toggleLoop.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedLoop));
			this.buttons[0].onClick.AddListener(new UnityAction(this.OnClickRestart));
			this.buttons[1].onClick.AddListener(new UnityAction(this.OnClickAllRestart));
			this.buttons[2].onClick.AddListener(new UnityAction(this.OnClickCopy));
			this.buttons[3].onClick.AddListener(new UnityAction(this.OnClickPaste));
		}

		// Token: 0x0600A148 RID: 41288 RVA: 0x00423E74 File Offset: 0x00422274
		public void UpdateInfo()
		{
			this.isUpdateInfo = true;
			this.arrayTarget = null;
			bool flag = this.m_ObjectCtrlInfo.kind == 0;
			OCIChar ocichar = this.m_ObjectCtrlInfo as OCIChar;
			bool flag2 = flag && ocichar.isHAnime;
			this.sliderSpeed.value = this.m_ObjectCtrlInfo.animeSpeed;
			this.inputSpeed.text = this.m_ObjectCtrlInfo.animeSpeed.ToString("0.00");
			this.sliderPattern.interactable = flag;
			this.inputPattern.interactable = flag;
			this.sliderPattern.value = ((!flag) ? 0.5f : Mathf.InverseLerp(0f, 1f, ocichar.animePattern));
			this.inputPattern.text = ((!flag) ? "-" : ocichar.animePattern.ToString("0.00"));
			for (int i = 0; i < 2; i++)
			{
				this.sliderOptionParam[i].interactable = flag2;
				this.inputOptionParam[i].interactable = flag2;
				this.sliderOptionParam[i].value = ((!flag2) ? 0.5f : ocichar.animeOptionParam[i]);
				this.inputOptionParam[i].text = ((!flag2) ? "-" : ocichar.animeOptionParam[i].ToString("0.00"));
			}
			this.toggleOption.interactable = flag;
			this.toggleOption.isOn = (flag && ocichar.optionItemCtrl.visible);
			this.toggleLoop.interactable = flag;
			this.toggleLoop.isOn = (flag && ocichar.charAnimeCtrl.isForceLoop);
			this.buttons[2].interactable = flag;
			this.buttons[3].interactable = flag;
			this.isUpdateInfo = false;
		}

		// Token: 0x0600A149 RID: 41289 RVA: 0x0042407C File Offset: 0x0042247C
		private void AddEventTrigger(EventTrigger _event, EventTriggerType _type, UnityAction<BaseEventData> _action)
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = _type;
			entry.callback.AddListener(_action);
			_event.triggers.Add(entry);
		}

		// Token: 0x0600A14A RID: 41290 RVA: 0x004240AE File Offset: 0x004224AE
		private void Awake()
		{
			this.arrayTarget = null;
			this.num = 0;
			this.Init();
			base.gameObject.SetActive(false);
		}

		// Token: 0x04007F5C RID: 32604
		[SerializeField]
		private Slider sliderSpeed;

		// Token: 0x04007F5D RID: 32605
		[SerializeField]
		private EventTrigger eventSpeed;

		// Token: 0x04007F5E RID: 32606
		[SerializeField]
		private InputField inputSpeed;

		// Token: 0x04007F5F RID: 32607
		[SerializeField]
		private Slider sliderPattern;

		// Token: 0x04007F60 RID: 32608
		[SerializeField]
		private EventTrigger eventPattern;

		// Token: 0x04007F61 RID: 32609
		[SerializeField]
		private InputField inputPattern;

		// Token: 0x04007F62 RID: 32610
		[SerializeField]
		private Slider[] sliderOptionParam;

		// Token: 0x04007F63 RID: 32611
		[SerializeField]
		private EventTrigger[] eventOptionParam;

		// Token: 0x04007F64 RID: 32612
		[SerializeField]
		private InputField[] inputOptionParam;

		// Token: 0x04007F65 RID: 32613
		[SerializeField]
		private Toggle toggleOption;

		// Token: 0x04007F66 RID: 32614
		[SerializeField]
		private Toggle toggleLoop;

		// Token: 0x04007F67 RID: 32615
		[SerializeField]
		private Button[] buttons;

		// Token: 0x04007F68 RID: 32616
		private ObjectCtrlInfo m_ObjectCtrlInfo;

		// Token: 0x04007F69 RID: 32617
		private bool isUpdateInfo;

		// Token: 0x04007F6A RID: 32618
		private ObjectCtrlInfo[] arrayTarget;

		// Token: 0x04007F6B RID: 32619
		private int num;

		// Token: 0x04007F6C RID: 32620
		private float[] oldValue;

		// Token: 0x04007F6D RID: 32621
		private int sex = -1;

		// Token: 0x04007F6E RID: 32622
		private OICharInfo.AnimeInfo animeInfo = new OICharInfo.AnimeInfo();
	}
}
