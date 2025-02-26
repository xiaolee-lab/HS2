using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001325 RID: 4901
	public class MPRouteCtrl : MonoBehaviour
	{
		// Token: 0x17002260 RID: 8800
		// (get) Token: 0x0600A3D3 RID: 41939 RVA: 0x0042F300 File Offset: 0x0042D700
		// (set) Token: 0x0600A3D4 RID: 41940 RVA: 0x0042F308 File Offset: 0x0042D708
		public OCIRoute ociRoute
		{
			get
			{
				return this.m_OCIRoute;
			}
			set
			{
				this.m_OCIRoute = value;
				this.UpdateInfo();
			}
		}

		// Token: 0x17002261 RID: 8801
		// (get) Token: 0x0600A3D5 RID: 41941 RVA: 0x0042F317 File Offset: 0x0042D717
		// (set) Token: 0x0600A3D6 RID: 41942 RVA: 0x0042F320 File Offset: 0x0042D720
		public bool active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
				base.gameObject.SetActive(this.m_Active && this.m_OCIRoute != null);
				this.routeControl.visible = value;
				if (this.isColorFunc && !value)
				{
					Singleton<Studio>.Instance.colorPalette.visible = false;
				}
			}
		}

		// Token: 0x0600A3D7 RID: 41943 RVA: 0x0042F389 File Offset: 0x0042D789
		public bool Deselect(OCIRoute _ociRoute)
		{
			if (this.m_OCIRoute != _ociRoute)
			{
				return false;
			}
			this.ociRoute = null;
			this.active = false;
			return true;
		}

		// Token: 0x0600A3D8 RID: 41944 RVA: 0x0042F3A8 File Offset: 0x0042D7A8
		public void UpdateInteractable(OCIRoute _route)
		{
			if (this.m_OCIRoute != _route)
			{
				return;
			}
			bool interactable = !this.m_OCIRoute.isPlay;
			this.buttonAddPoint.interactable = interactable;
			this.toggleOrient.interactable = interactable;
			this.toggleLoop.interactable = interactable;
		}

		// Token: 0x0600A3D9 RID: 41945 RVA: 0x0042F3F8 File Offset: 0x0042D7F8
		private void UpdateInfo()
		{
			if (this.m_OCIRoute == null)
			{
				return;
			}
			this.isUpdateInfo = true;
			this.inputName.text = this.m_OCIRoute.name;
			this.toggleLoop.isOn = this.m_OCIRoute.routeInfo.loop;
			this.toggleLine.isOn = this.m_OCIRoute.visibleLine;
			this.toggleOrient.isOn = (int)this.m_OCIRoute.routeInfo.orient;
			this.buttonColor.image.color = this.m_OCIRoute.routeInfo.color;
			this.isUpdateInfo = false;
			this.UpdateInteractable(this.m_OCIRoute);
		}

		// Token: 0x0600A3DA RID: 41946 RVA: 0x0042F4AD File Offset: 0x0042D8AD
		private void OnEndEditName(string _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCIRoute.name = _value;
		}

		// Token: 0x0600A3DB RID: 41947 RVA: 0x0042F4C8 File Offset: 0x0042D8C8
		private void OnClickAddPoint()
		{
			if (this.m_OCIRoute == null)
			{
				return;
			}
			OCIRoutePoint ociroutePoint = this.m_OCIRoute.AddPoint();
			if (Studio.optionSystem.autoSelect && ociroutePoint != null)
			{
				Singleton<Studio>.Instance.treeNodeCtrl.SelectSingle(ociroutePoint.treeNodeObject, true);
			}
		}

		// Token: 0x0600A3DC RID: 41948 RVA: 0x0042F518 File Offset: 0x0042D918
		private void OnValueChangedPlay(bool _value)
		{
			if (this.m_OCIRoute == null)
			{
				return;
			}
			if (_value)
			{
				this.m_OCIRoute.Play();
			}
			else
			{
				this.m_OCIRoute.Stop(true);
			}
		}

		// Token: 0x0600A3DD RID: 41949 RVA: 0x0042F54C File Offset: 0x0042D94C
		private void OnValueChangedLoop(bool _value)
		{
			List<OCIRoute> list = (from v in Studio.GetSelectObjectCtrl()
			where v.kind == 4
			select v as OCIRoute).ToList<OCIRoute>();
			list.Add(this.m_OCIRoute);
			HashSet<OCIRoute> hashSet = new HashSet<OCIRoute>();
			foreach (OCIRoute ociroute in list)
			{
				ociroute.routeInfo.loop = _value;
				hashSet.Add(ociroute);
			}
			foreach (OCIRoute ociroute2 in hashSet)
			{
				ociroute2.ForceUpdateLine();
			}
		}

		// Token: 0x0600A3DE RID: 41950 RVA: 0x0042F65C File Offset: 0x0042DA5C
		private void OnValueChangedLine(bool _value)
		{
			List<OCIRoute> list = (from v in Studio.GetSelectObjectCtrl()
			where v.kind == 4
			select v as OCIRoute).ToList<OCIRoute>();
			list.Add(this.m_OCIRoute);
			foreach (OCIRoute ociroute in list)
			{
				ociroute.visibleLine = _value;
			}
		}

		// Token: 0x0600A3DF RID: 41951 RVA: 0x0042F710 File Offset: 0x0042DB10
		private void OnValueChangedOrient(bool _value, int _idx)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			if (!_value)
			{
				return;
			}
			List<OCIRoute> list = (from v in Studio.GetSelectObjectCtrl()
			where v.kind == 4
			select v as OCIRoute).ToList<OCIRoute>();
			list.Add(this.m_OCIRoute);
			foreach (OCIRoute ociroute in list)
			{
				ociroute.routeInfo.orient = (OIRouteInfo.Orient)_idx;
			}
			this.toggleOrient.isOn = _idx;
		}

		// Token: 0x0600A3E0 RID: 41952 RVA: 0x0042F7E8 File Offset: 0x0042DBE8
		private void OnClickColor()
		{
			if (Singleton<Studio>.Instance.colorPalette.Check("ルートのラインカラー"))
			{
				this.isColorFunc = false;
				Singleton<Studio>.Instance.colorPalette.visible = false;
				return;
			}
			List<OCIRoute> array = (from v in Studio.GetSelectObjectCtrl()
			where v.kind == 4
			select v as OCIRoute).ToList<OCIRoute>();
			array.Add(this.m_OCIRoute);
			Singleton<Studio>.Instance.colorPalette.Setup("ルートのラインカラー", this.m_OCIRoute.routeInfo.color, delegate(Color _c)
			{
				foreach (OCIRoute ociroute in array)
				{
					ociroute.routeInfo.color = _c;
					ociroute.SetLineColor(_c);
				}
				this.buttonColor.image.color = _c;
			}, false);
			this.isColorFunc = true;
		}

		// Token: 0x0600A3E1 RID: 41953 RVA: 0x0042F8D0 File Offset: 0x0042DCD0
		private void Start()
		{
			this.inputName.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditName));
			this.buttonAddPoint.onClick.AddListener(new UnityAction(this.OnClickAddPoint));
			this.toggleLoop.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedLoop));
			this.toggleLine.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedLine));
			this.toggleOrient.action = new Action<bool, int>(this.OnValueChangedOrient);
			this.buttonColor.onClick.AddListener(new UnityAction(this.OnClickColor));
		}

		// Token: 0x0400812C RID: 33068
		[SerializeField]
		private TMP_InputField inputName;

		// Token: 0x0400812D RID: 33069
		[SerializeField]
		private Button buttonAddPoint;

		// Token: 0x0400812E RID: 33070
		[SerializeField]
		private MPRouteCtrl.ToggleGroup toggleOrient;

		// Token: 0x0400812F RID: 33071
		[SerializeField]
		private Toggle toggleLoop;

		// Token: 0x04008130 RID: 33072
		[SerializeField]
		private Toggle toggleLine;

		// Token: 0x04008131 RID: 33073
		[SerializeField]
		private Button buttonColor;

		// Token: 0x04008132 RID: 33074
		[SerializeField]
		private RouteControl routeControl;

		// Token: 0x04008133 RID: 33075
		private OCIRoute m_OCIRoute;

		// Token: 0x04008134 RID: 33076
		private bool m_Active;

		// Token: 0x04008135 RID: 33077
		private bool isUpdateInfo;

		// Token: 0x04008136 RID: 33078
		private bool isColorFunc;

		// Token: 0x02001326 RID: 4902
		[Serializable]
		private class ToggleGroup
		{
			// Token: 0x17002262 RID: 8802
			// (get) Token: 0x0600A3EB RID: 41963 RVA: 0x0042F9D4 File Offset: 0x0042DDD4
			// (set) Token: 0x0600A3EC RID: 41964 RVA: 0x0042FA00 File Offset: 0x0042DE00
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

			// Token: 0x17002263 RID: 8803
			// (set) Token: 0x0600A3ED RID: 41965 RVA: 0x0042FA38 File Offset: 0x0042DE38
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

			// Token: 0x17002264 RID: 8804
			public Toggle this[int _idx]
			{
				get
				{
					return this.toggle[_idx];
				}
			}

			// Token: 0x17002265 RID: 8805
			// (set) Token: 0x0600A3EF RID: 41967 RVA: 0x0042FA78 File Offset: 0x0042DE78
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

			// Token: 0x0400813F RID: 33087
			[SerializeField]
			private Toggle[] toggle;
		}
	}
}
