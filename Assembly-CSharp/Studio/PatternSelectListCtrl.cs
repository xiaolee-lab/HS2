using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UGUI_AssistLibrary;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001337 RID: 4919
	public class PatternSelectListCtrl : MonoBehaviour
	{
		// Token: 0x17002281 RID: 8833
		// (get) Token: 0x0600A4A1 RID: 42145 RVA: 0x004328C6 File Offset: 0x00430CC6
		public List<PatternSelectInfo> lstSelectInfo
		{
			get
			{
				return this._lstSelectInfo;
			}
		}

		// Token: 0x0600A4A2 RID: 42146 RVA: 0x004328CE File Offset: 0x00430CCE
		public void ClearList()
		{
			this._lstSelectInfo.Clear();
		}

		// Token: 0x0600A4A3 RID: 42147 RVA: 0x004328DC File Offset: 0x00430CDC
		public void AddList(int index, string name, string assetBundle, string assetName)
		{
			PatternSelectInfo patternSelectInfo = new PatternSelectInfo();
			patternSelectInfo.index = index;
			patternSelectInfo.name = name;
			patternSelectInfo.assetBundle = assetBundle;
			patternSelectInfo.assetName = assetName;
			this._lstSelectInfo.Add(patternSelectInfo);
		}

		// Token: 0x0600A4A4 RID: 42148 RVA: 0x00432918 File Offset: 0x00430D18
		public void AddOutside(int _start)
		{
			IEnumerable<string> files = Directory.GetFiles(UserData.Create("pattern_thumb"), "*.png");
			if (PatternSelectListCtrl.<>f__mg$cache0 == null)
			{
				PatternSelectListCtrl.<>f__mg$cache0 = new Func<string, string>(Path.GetFileName);
			}
			List<string> list = files.Select(PatternSelectListCtrl.<>f__mg$cache0).ToList<string>();
			for (int i = 0; i < list.Count; i++)
			{
				this.AddList(_start + i, Path.GetFileNameWithoutExtension(list[i]), string.Empty, list[i]);
			}
		}

		// Token: 0x0600A4A5 RID: 42149 RVA: 0x0043299C File Offset: 0x00430D9C
		public void Create(PatternSelectListCtrl.OnChangeItemFunc _onChangeItemFunc)
		{
			this.onChangeItemFunc = _onChangeItemFunc;
			for (int i = this.objContent.transform.childCount - 1; i >= 0; i--)
			{
				Transform child = this.objContent.transform.GetChild(i);
				UnityEngine.Object.Destroy(child.gameObject);
			}
			ToggleGroup component = this.objContent.GetComponent<ToggleGroup>();
			int num = 0;
			for (int j = 0; j < this._lstSelectInfo.Count; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objTemp);
				PatternSelectInfoComponent component2 = gameObject.GetComponent<PatternSelectInfoComponent>();
				component2.info = this._lstSelectInfo[j];
				component2.info.sic = component2;
				component2.tgl.group = component;
				gameObject.transform.SetParent(this.objContent.transform, false);
				this.SetToggleHandler(gameObject, component2);
				component2.img = gameObject.GetComponent<Image>();
				if (component2.img)
				{
					Texture2D texture2D;
					if (this._lstSelectInfo[j].assetBundle.IsNullOrEmpty())
					{
						string path = UserData.Path + "pattern_thumb/" + this._lstSelectInfo[j].assetName;
						texture2D = PngAssist.LoadTexture(path);
					}
					else
					{
						texture2D = CommonLib.LoadAsset<Texture2D>(this._lstSelectInfo[j].assetBundle, this._lstSelectInfo[j].assetName, false, string.Empty);
					}
					if (texture2D)
					{
						component2.img.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
					}
				}
				component2.Disvisible(this._lstSelectInfo[j].disvisible);
				component2.Disable(this._lstSelectInfo[j].disable);
				num++;
			}
			this.ToggleAllOff();
		}

		// Token: 0x0600A4A6 RID: 42150 RVA: 0x00432BB0 File Offset: 0x00430FB0
		public PatternSelectInfo GetSelectInfoFromIndex(int index)
		{
			return this._lstSelectInfo.Find((PatternSelectInfo item) => item.index == index);
		}

		// Token: 0x0600A4A7 RID: 42151 RVA: 0x00432BE4 File Offset: 0x00430FE4
		public PatternSelectInfo GetSelectInfoFromName(string name)
		{
			return this._lstSelectInfo.Find((PatternSelectInfo item) => item.name == name);
		}

		// Token: 0x0600A4A8 RID: 42152 RVA: 0x00432C18 File Offset: 0x00431018
		public string GetNameFormIndex(int index)
		{
			PatternSelectInfo patternSelectInfo = this._lstSelectInfo.Find((PatternSelectInfo item) => item.index == index);
			return (patternSelectInfo == null) ? string.Empty : patternSelectInfo.name;
		}

		// Token: 0x0600A4A9 RID: 42153 RVA: 0x00432C60 File Offset: 0x00431060
		public int GetIndexFromName(string name)
		{
			PatternSelectInfo patternSelectInfo = this._lstSelectInfo.Find((PatternSelectInfo item) => item.name == name);
			return (patternSelectInfo == null) ? -1 : patternSelectInfo.index;
		}

		// Token: 0x0600A4AA RID: 42154 RVA: 0x00432CA4 File Offset: 0x004310A4
		public int GetSelectIndex()
		{
			PatternSelectInfo patternSelectInfo = this._lstSelectInfo.Find((PatternSelectInfo psi) => psi.interactable & psi.activeSelf & psi.isOn);
			return (patternSelectInfo == null) ? -1 : patternSelectInfo.index;
		}

		// Token: 0x0600A4AB RID: 42155 RVA: 0x00432CEC File Offset: 0x004310EC
		public PatternSelectInfoComponent GetSelectTopItem()
		{
			int selectIndex = this.GetSelectIndex();
			if (selectIndex == -1)
			{
				return null;
			}
			PatternSelectInfo selectInfoFromIndex = this.GetSelectInfoFromIndex(selectIndex);
			if (selectInfoFromIndex != null)
			{
				return selectInfoFromIndex.sic;
			}
			return null;
		}

		// Token: 0x0600A4AC RID: 42156 RVA: 0x00432D20 File Offset: 0x00431120
		public PatternSelectInfoComponent GetSelectableTopItem()
		{
			SortedDictionary<int, PatternSelectInfoComponent> sortedDictionary = new SortedDictionary<int, PatternSelectInfoComponent>();
			for (int i = 0; i < this._lstSelectInfo.Count; i++)
			{
				if (this._lstSelectInfo[i].sic.tgl.interactable)
				{
					if (this._lstSelectInfo[i].sic.gameObject.activeSelf)
					{
						sortedDictionary[this._lstSelectInfo[i].sic.gameObject.transform.GetSiblingIndex()] = this._lstSelectInfo[i].sic;
					}
				}
			}
			PatternSelectInfoComponent result = null;
			if (sortedDictionary.Count != 0)
			{
				result = sortedDictionary.First<KeyValuePair<int, PatternSelectInfoComponent>>().Value;
			}
			return result;
		}

		// Token: 0x0600A4AD RID: 42157 RVA: 0x00432DF0 File Offset: 0x004311F0
		public int GetDrawOrderFromIndex(int index)
		{
			SortedDictionary<int, PatternSelectInfoComponent> sortedDictionary = new SortedDictionary<int, PatternSelectInfoComponent>();
			for (int i = 0; i < this._lstSelectInfo.Count; i++)
			{
				if (this._lstSelectInfo[i].sic.gameObject.activeSelf)
				{
					sortedDictionary[this._lstSelectInfo[i].sic.gameObject.transform.GetSiblingIndex()] = this._lstSelectInfo[i].sic;
				}
			}
			foreach (var <>__AnonType in sortedDictionary.Select((KeyValuePair<int, PatternSelectInfoComponent> val, int idx) => new
			{
				val,
				idx
			}))
			{
				if (<>__AnonType.val.Value.info.index == index)
				{
					return <>__AnonType.idx;
				}
			}
			return -1;
		}

		// Token: 0x0600A4AE RID: 42158 RVA: 0x00432F10 File Offset: 0x00431310
		public int GetInclusiveCount()
		{
			return this._lstSelectInfo.Count((PatternSelectInfo _psi) => _psi.activeSelf);
		}

		// Token: 0x0600A4AF RID: 42159 RVA: 0x00432F3C File Offset: 0x0043133C
		public void SelectPrevItem()
		{
			List<PatternSelectInfo> list = (from lst in this._lstSelectInfo
			where lst.sic.tgl.interactable && lst.sic.gameObject.activeSelf
			select lst).ToList<PatternSelectInfo>();
			int num = list.FindIndex((PatternSelectInfo lst) => lst.sic.tgl.isOn);
			if (num == -1)
			{
				return;
			}
			int count = list.Count;
			int index = (num + count - 1) % count;
			this.SelectItem(list[index].index);
		}

		// Token: 0x0600A4B0 RID: 42160 RVA: 0x00432FC4 File Offset: 0x004313C4
		public void SelectNextItem()
		{
			List<PatternSelectInfo> list = (from lst in this._lstSelectInfo
			where lst.sic.tgl.interactable && lst.sic.gameObject.activeSelf
			select lst).ToList<PatternSelectInfo>();
			int num = list.FindIndex((PatternSelectInfo lst) => lst.sic.tgl.isOn);
			if (num == -1)
			{
				return;
			}
			int count = list.Count;
			int index = (num + 1) % count;
			this.SelectItem(list[index].index);
		}

		// Token: 0x0600A4B1 RID: 42161 RVA: 0x0043304C File Offset: 0x0043144C
		public void SelectItem(int index)
		{
			PatternSelectInfo patternSelectInfo = this._lstSelectInfo.Find((PatternSelectInfo item) => item.index == index);
			if (patternSelectInfo == null)
			{
				return;
			}
			patternSelectInfo.sic.tgl.isOn = true;
			this.ChangeItem(patternSelectInfo.sic);
			this.UpdateScrollPosition();
		}

		// Token: 0x0600A4B2 RID: 42162 RVA: 0x004330A8 File Offset: 0x004314A8
		public void SelectItem(string name)
		{
			PatternSelectInfo patternSelectInfo = this._lstSelectInfo.Find((PatternSelectInfo item) => item.name == name);
			if (patternSelectInfo == null)
			{
				return;
			}
			patternSelectInfo.sic.tgl.isOn = true;
			this.ChangeItem(patternSelectInfo.sic);
			this.UpdateScrollPosition();
		}

		// Token: 0x0600A4B3 RID: 42163 RVA: 0x00433104 File Offset: 0x00431504
		public void UpdateScrollPosition()
		{
		}

		// Token: 0x0600A4B4 RID: 42164 RVA: 0x00433106 File Offset: 0x00431506
		public void OnPointerClick(PatternSelectInfoComponent _psic)
		{
			if (null == _psic)
			{
				return;
			}
			if (!_psic.tgl.interactable)
			{
				return;
			}
			this.ChangeItem(_psic);
		}

		// Token: 0x0600A4B5 RID: 42165 RVA: 0x00433130 File Offset: 0x00431530
		public void OnPointerEnter(PatternSelectInfoComponent _psic)
		{
			if (null == _psic)
			{
				return;
			}
			if (!_psic.tgl.interactable)
			{
				return;
			}
			if (this.textDrawName)
			{
				this.textDrawName.text = _psic.info.name;
			}
		}

		// Token: 0x0600A4B6 RID: 42166 RVA: 0x00433184 File Offset: 0x00431584
		public void OnPointerExit(PatternSelectInfoComponent _psic)
		{
			if (null == _psic)
			{
				return;
			}
			if (!_psic.tgl.interactable)
			{
				return;
			}
			if (this.textDrawName)
			{
				this.textDrawName.text = this.selectDrawName;
			}
		}

		// Token: 0x0600A4B7 RID: 42167 RVA: 0x004331D0 File Offset: 0x004315D0
		public void ChangeItem(PatternSelectInfoComponent _psic)
		{
			if (this.onChangeItemFunc != null)
			{
				this.onChangeItemFunc(_psic.info.index);
			}
			this.selectDrawName = _psic.info.name;
			if (this.textDrawName)
			{
				this.textDrawName.text = this.selectDrawName;
			}
		}

		// Token: 0x0600A4B8 RID: 42168 RVA: 0x00433230 File Offset: 0x00431630
		public void ToggleAllOff()
		{
			for (int i = 0; i < this._lstSelectInfo.Count; i++)
			{
				this._lstSelectInfo[i].sic.tgl.isOn = false;
			}
		}

		// Token: 0x0600A4B9 RID: 42169 RVA: 0x00433278 File Offset: 0x00431678
		public void DisableItem(int index, bool _disable)
		{
			this._lstSelectInfo.Find((PatternSelectInfo item) => item.index == index).SafeProc(delegate(PatternSelectInfo psi)
			{
				psi.disable = _disable;
				psi.sic.Disable(_disable);
			});
		}

		// Token: 0x0600A4BA RID: 42170 RVA: 0x004332C4 File Offset: 0x004316C4
		public void DisableItem(string name, bool _disable)
		{
			this._lstSelectInfo.Find((PatternSelectInfo item) => item.name == name).SafeProc(delegate(PatternSelectInfo psi)
			{
				psi.disable = _disable;
				psi.sic.Disable(_disable);
			});
		}

		// Token: 0x0600A4BB RID: 42171 RVA: 0x00433310 File Offset: 0x00431710
		public void DisvisibleItem(int index, bool _disvisible)
		{
			this._lstSelectInfo.Find((PatternSelectInfo item) => item.index == index).SafeProc(delegate(PatternSelectInfo psi)
			{
				psi.disvisible = _disvisible;
				psi.sic.Disvisible(_disvisible);
			});
		}

		// Token: 0x0600A4BC RID: 42172 RVA: 0x0043335C File Offset: 0x0043175C
		public void DisvisibleItem(string name, bool _disvisible)
		{
			this._lstSelectInfo.Find((PatternSelectInfo item) => item.name == name).SafeProc(delegate(PatternSelectInfo psi)
			{
				psi.disvisible = _disvisible;
				psi.sic.Disvisible(_disvisible);
			});
		}

		// Token: 0x0600A4BD RID: 42173 RVA: 0x004333A8 File Offset: 0x004317A8
		private void SetToggleHandler(GameObject obj, PatternSelectInfoComponent _psic)
		{
			UIAL_EventTrigger uial_EventTrigger = obj.AddComponent<UIAL_EventTrigger>();
			uial_EventTrigger.triggers = new List<UIAL_EventTrigger.Entry>();
			UIAL_EventTrigger.Entry entry = new UIAL_EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.buttonType = UIAL_EventTrigger.ButtonType.Left;
			entry.callback.AddListener(delegate(BaseEventData value)
			{
				this.OnPointerClick(_psic);
			});
			uial_EventTrigger.triggers.Add(entry);
			if (this.textDrawName)
			{
				entry = new UIAL_EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerEnter;
				entry.callback.AddListener(delegate(BaseEventData value)
				{
					this.OnPointerEnter(_psic);
				});
				uial_EventTrigger.triggers.Add(entry);
				entry = new UIAL_EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerExit;
				entry.callback.AddListener(delegate(BaseEventData value)
				{
					this.OnPointerExit(_psic);
				});
				uial_EventTrigger.triggers.Add(entry);
			}
		}

		// Token: 0x0600A4BE RID: 42174 RVA: 0x00433482 File Offset: 0x00431882
		private void Start()
		{
			this.btnPrev.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.SelectPrevItem();
			});
			this.btnNext.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.SelectNextItem();
			});
		}

		// Token: 0x040081A9 RID: 33193
		[SerializeField]
		private TextMeshProUGUI textDrawName;

		// Token: 0x040081AA RID: 33194
		[SerializeField]
		private RectTransform rtfScrollRect;

		// Token: 0x040081AB RID: 33195
		[SerializeField]
		private RectTransform rtfContant;

		// Token: 0x040081AC RID: 33196
		[SerializeField]
		private GameObject objContent;

		// Token: 0x040081AD RID: 33197
		[SerializeField]
		private GameObject objTemp;

		// Token: 0x040081AE RID: 33198
		[SerializeField]
		private Button btnPrev;

		// Token: 0x040081AF RID: 33199
		[SerializeField]
		private Button btnNext;

		// Token: 0x040081B0 RID: 33200
		private string selectDrawName = string.Empty;

		// Token: 0x040081B1 RID: 33201
		private List<PatternSelectInfo> _lstSelectInfo = new List<PatternSelectInfo>();

		// Token: 0x040081B2 RID: 33202
		public PatternSelectListCtrl.OnChangeItemFunc onChangeItemFunc;

		// Token: 0x040081B3 RID: 33203
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache0;

		// Token: 0x02001338 RID: 4920
		// (Invoke) Token: 0x0600A4C9 RID: 42185
		public delegate void OnChangeItemFunc(int index);
	}
}
