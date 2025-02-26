using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001327 RID: 4903
	public class MPRoutePointCtrl : MonoBehaviour
	{
		// Token: 0x17002266 RID: 8806
		// (get) Token: 0x0600A3F2 RID: 41970 RVA: 0x0042FBB6 File Offset: 0x0042DFB6
		// (set) Token: 0x0600A3F3 RID: 41971 RVA: 0x0042FBBE File Offset: 0x0042DFBE
		public OCIRoutePoint ociRoutePoint
		{
			get
			{
				return this.m_OCIRoutePoint;
			}
			set
			{
				this.m_OCIRoutePoint = value;
				this.UpdateInfo();
			}
		}

		// Token: 0x17002267 RID: 8807
		// (get) Token: 0x0600A3F4 RID: 41972 RVA: 0x0042FBCD File Offset: 0x0042DFCD
		// (set) Token: 0x0600A3F5 RID: 41973 RVA: 0x0042FBD5 File Offset: 0x0042DFD5
		public bool active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
				base.gameObject.SetActive(this.m_Active && this.m_OCIRoutePoint != null);
			}
		}

		// Token: 0x0600A3F6 RID: 41974 RVA: 0x0042FC06 File Offset: 0x0042E006
		public bool Deselect(OCIRoutePoint _ociRoutePoint)
		{
			if (this.m_OCIRoutePoint != _ociRoutePoint)
			{
				return false;
			}
			this.ociRoutePoint = null;
			this.active = false;
			return true;
		}

		// Token: 0x0600A3F7 RID: 41975 RVA: 0x0042FC28 File Offset: 0x0042E028
		public void UpdateInteractable(OCIRoute _route)
		{
			if (_route == null)
			{
				return;
			}
			if (!_route.listPoint.Contains(this.m_OCIRoutePoint))
			{
				return;
			}
			bool interactable = !_route.isPlay;
			this.inputSpeed.interactable = interactable;
			this.dropdownEase.interactable = interactable;
			this.toggleConnection.interactable = interactable;
			this.toggleLink.interactable = interactable;
			if (this.m_OCIRoutePoint.connection == OIRoutePointInfo.Connection.Curve && this.m_OCIRoutePoint.link)
			{
				int index = _route.listPoint.FindIndex((OCIRoutePoint p) => p == this.m_OCIRoutePoint) - 1;
				OCIRoutePoint ociroutePoint = _route.listPoint.SafeGet(index);
				if (ociroutePoint != null && ociroutePoint.connection == OIRoutePointInfo.Connection.Curve)
				{
					this.inputSpeed.interactable = false;
					this.dropdownEase.interactable = false;
				}
			}
		}

		// Token: 0x0600A3F8 RID: 41976 RVA: 0x0042FD00 File Offset: 0x0042E100
		private void UpdateInfo()
		{
			if (this.m_OCIRoutePoint == null)
			{
				return;
			}
			this.isUpdateInfo = true;
			this.inputSpeed.value = this.m_OCIRoutePoint.routePointInfo.speed;
			int num = this.listEase.FindIndex((MPRoutePointCtrl.EaseInfo e) => e.ease == this.m_OCIRoutePoint.easeType);
			this.dropdownEase.value = ((num >= 0) ? num : 0);
			this.toggleConnection.isOn = (int)this.m_OCIRoutePoint.connection;
			this.toggleLink.isOn = this.m_OCIRoutePoint.link;
			this.isUpdateInfo = false;
			this.UpdateInteractable(this.m_OCIRoutePoint.route);
		}

		// Token: 0x0600A3F9 RID: 41977 RVA: 0x0042FDB0 File Offset: 0x0042E1B0
		private void OnValueChangeSpeed(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			IEnumerable<OCIRoutePoint> enumerable = from v in Studio.GetSelectObjectCtrl()
			where v.kind == 6
			select v as OCIRoutePoint;
			foreach (OCIRoutePoint ociroutePoint in enumerable)
			{
				ociroutePoint.speed = _value;
			}
			this.inputSpeed.value = _value;
		}

		// Token: 0x0600A3FA RID: 41978 RVA: 0x0042FE68 File Offset: 0x0042E268
		private void OnEndEditSpeed(string _text)
		{
			float num = Mathf.Clamp(Utility.StringToFloat(_text), this.inputSpeed.min, this.inputSpeed.max);
			IEnumerable<OCIRoutePoint> enumerable = from v in Studio.GetSelectObjectCtrl()
			where v.kind == 6
			select v as OCIRoutePoint;
			foreach (OCIRoutePoint ociroutePoint in enumerable)
			{
				ociroutePoint.speed = num;
			}
			this.inputSpeed.value = num;
		}

		// Token: 0x0600A3FB RID: 41979 RVA: 0x0042FF34 File Offset: 0x0042E334
		private void OnValueChangedEase(int _value)
		{
			if (this.m_OCIRoutePoint == null)
			{
				return;
			}
			IEnumerable<OCIRoutePoint> enumerable = from v in Studio.GetSelectObjectCtrl()
			where v.kind == 6
			select v as OCIRoutePoint;
			StudioTween.EaseType ease = this.listEase[_value].ease;
			foreach (OCIRoutePoint ociroutePoint in enumerable)
			{
				ociroutePoint.easeType = ease;
			}
		}

		// Token: 0x0600A3FC RID: 41980 RVA: 0x0042FFF0 File Offset: 0x0042E3F0
		private void OnValueChangedConnection(bool _value, int _idx)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			if (this.m_OCIRoutePoint == null || !_value)
			{
				return;
			}
			this.toggleConnection.isOn = _idx;
			IEnumerable<OCIRoutePoint> enumerable = from v in Studio.GetSelectObjectCtrl()
			where v.kind == 6
			select v as OCIRoutePoint;
			HashSet<OCIRoute> hashSet = new HashSet<OCIRoute>();
			foreach (OCIRoutePoint ociroutePoint in enumerable)
			{
				ociroutePoint.connection = (OIRoutePointInfo.Connection)_idx;
				hashSet.Add(ociroutePoint.route);
			}
			foreach (OCIRoute ociroute in hashSet)
			{
				ociroute.ForceUpdateLine();
			}
			this.UpdateInteractable(this.m_OCIRoutePoint.route);
		}

		// Token: 0x0600A3FD RID: 41981 RVA: 0x00430124 File Offset: 0x0042E524
		private void OnValueChangedLink(bool _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			IEnumerable<OCIRoutePoint> enumerable = from v in Studio.GetSelectObjectCtrl()
			where v.kind == 6
			select v as OCIRoutePoint;
			HashSet<OCIRoute> hashSet = new HashSet<OCIRoute>();
			foreach (OCIRoutePoint ociroutePoint in enumerable)
			{
				ociroutePoint.link = _value;
				hashSet.Add(ociroutePoint.route);
			}
			foreach (OCIRoute ociroute in hashSet)
			{
				ociroute.ForceUpdateLine();
			}
			this.UpdateInteractable(this.m_OCIRoutePoint.route);
		}

		// Token: 0x0600A3FE RID: 41982 RVA: 0x0043023C File Offset: 0x0042E63C
		private void Awake()
		{
			this.listEase = new List<MPRoutePointCtrl.EaseInfo>();
			this.listEase.Add(new MPRoutePointCtrl.EaseInfo("直線的", StudioTween.EaseType.linear));
			this.listEase.Add(new MPRoutePointCtrl.EaseInfo("徐々に早く", StudioTween.EaseType.easeInQuad));
			this.listEase.Add(new MPRoutePointCtrl.EaseInfo("徐々に遅く", StudioTween.EaseType.easeOutQuad));
			this.listEase.Add(new MPRoutePointCtrl.EaseInfo("急に早く", StudioTween.EaseType.easeInQuart));
			this.listEase.Add(new MPRoutePointCtrl.EaseInfo("急に遅く", StudioTween.EaseType.easeOutQuart));
			this.listEase.Add(new MPRoutePointCtrl.EaseInfo("バウンド", StudioTween.EaseType.easeOutBounce));
			this.dropdownEase.options = (from v in this.listEase
			select new Dropdown.OptionData(v.name)).ToList<Dropdown.OptionData>();
			this.inputSpeed.input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSpeed));
			this.inputSpeed.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangeSpeed));
			this.dropdownEase.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChangedEase));
			this.toggleConnection.action = new Action<bool, int>(this.OnValueChangedConnection);
			this.toggleLink.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedLink));
		}

		// Token: 0x04008141 RID: 33089
		[SerializeField]
		private MPRoutePointCtrl.InputCombination inputSpeed = new MPRoutePointCtrl.InputCombination();

		// Token: 0x04008142 RID: 33090
		[SerializeField]
		private Dropdown dropdownEase;

		// Token: 0x04008143 RID: 33091
		[SerializeField]
		private MPRoutePointCtrl.ToggleGroup toggleConnection = new MPRoutePointCtrl.ToggleGroup();

		// Token: 0x04008144 RID: 33092
		[SerializeField]
		private Toggle toggleLink;

		// Token: 0x04008145 RID: 33093
		private OCIRoutePoint m_OCIRoutePoint;

		// Token: 0x04008146 RID: 33094
		private bool m_Active;

		// Token: 0x04008147 RID: 33095
		private List<MPRoutePointCtrl.EaseInfo> listEase;

		// Token: 0x04008148 RID: 33096
		private bool isUpdateInfo;

		// Token: 0x02001328 RID: 4904
		[Serializable]
		private class InputCombination
		{
			// Token: 0x17002268 RID: 8808
			// (set) Token: 0x0600A40D RID: 41997 RVA: 0x00430437 File Offset: 0x0042E837
			public bool interactable
			{
				set
				{
					this.input.interactable = value;
					this.slider.interactable = value;
				}
			}

			// Token: 0x17002269 RID: 8809
			// (get) Token: 0x0600A40E RID: 41998 RVA: 0x00430451 File Offset: 0x0042E851
			// (set) Token: 0x0600A40F RID: 41999 RVA: 0x0043045E File Offset: 0x0042E85E
			public string text
			{
				get
				{
					return this.input.text;
				}
				set
				{
					this.input.text = value;
					this.slider.value = Utility.StringToFloat(value);
				}
			}

			// Token: 0x1700226A RID: 8810
			// (get) Token: 0x0600A410 RID: 42000 RVA: 0x0043047D File Offset: 0x0042E87D
			// (set) Token: 0x0600A411 RID: 42001 RVA: 0x0043048A File Offset: 0x0042E88A
			public float value
			{
				get
				{
					return this.slider.value;
				}
				set
				{
					this.slider.value = value;
					this.input.text = value.ToString("0.0");
				}
			}

			// Token: 0x1700226B RID: 8811
			// (get) Token: 0x0600A412 RID: 42002 RVA: 0x004304AF File Offset: 0x0042E8AF
			public float min
			{
				get
				{
					return this.slider.minValue;
				}
			}

			// Token: 0x1700226C RID: 8812
			// (get) Token: 0x0600A413 RID: 42003 RVA: 0x004304BC File Offset: 0x0042E8BC
			public float max
			{
				get
				{
					return this.slider.maxValue;
				}
			}

			// Token: 0x04008154 RID: 33108
			public TMP_InputField input;

			// Token: 0x04008155 RID: 33109
			public Slider slider;
		}

		// Token: 0x02001329 RID: 4905
		[Serializable]
		private class ToggleGroup
		{
			// Token: 0x1700226D RID: 8813
			// (get) Token: 0x0600A415 RID: 42005 RVA: 0x004304D1 File Offset: 0x0042E8D1
			// (set) Token: 0x0600A416 RID: 42006 RVA: 0x004304FC File Offset: 0x0042E8FC
			public int isOn
			{
				get
				{
					return Array.FindIndex<Toggle>(this.toggle, (Toggle _t) => _t.isOn);
				}
				set
				{
					for (int i = 0; i < this.toggle.Length; i++)
					{
						this.toggle[i].isOn = (i == value);
					}
				}
			}

			// Token: 0x1700226E RID: 8814
			// (set) Token: 0x0600A417 RID: 42007 RVA: 0x00430534 File Offset: 0x0042E934
			public bool interactable
			{
				set
				{
					foreach (Toggle toggle in this.toggle)
					{
						toggle.interactable = value;
					}
				}
			}

			// Token: 0x1700226F RID: 8815
			public Toggle this[int _idx]
			{
				get
				{
					return this.toggle[_idx];
				}
			}

			// Token: 0x17002270 RID: 8816
			// (set) Token: 0x0600A419 RID: 42009 RVA: 0x00430574 File Offset: 0x0042E974
			public Action<bool, int> action
			{
				set
				{
					for (int i = 0; i < this.toggle.Length; i++)
					{
						int no = i;
						this.toggle[i].onValueChanged.AddListener(delegate(bool _b)
						{
							value(_b, no);
						});
					}
				}
			}

			// Token: 0x04008156 RID: 33110
			[SerializeField]
			private Toggle[] toggle;
		}

		// Token: 0x0200132A RID: 4906
		private class EaseInfo
		{
			// Token: 0x0600A41B RID: 42011 RVA: 0x0043060A File Offset: 0x0042EA0A
			public EaseInfo(string _name, StudioTween.EaseType _ease)
			{
				this.name = _name;
				this.ease = _ease;
			}

			// Token: 0x17002271 RID: 8817
			// (get) Token: 0x0600A41C RID: 42012 RVA: 0x00430620 File Offset: 0x0042EA20
			// (set) Token: 0x0600A41D RID: 42013 RVA: 0x00430628 File Offset: 0x0042EA28
			public string name { get; private set; }

			// Token: 0x17002272 RID: 8818
			// (get) Token: 0x0600A41E RID: 42014 RVA: 0x00430631 File Offset: 0x0042EA31
			// (set) Token: 0x0600A41F RID: 42015 RVA: 0x00430639 File Offset: 0x0042EA39
			public StudioTween.EaseType ease { get; private set; }
		}
	}
}
