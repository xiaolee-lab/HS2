using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject;
using Manager;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x0200085F RID: 2143
	public abstract class BaseSetting : MonoBehaviour
	{
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x00142E9E File Offset: 0x0014129E
		// (set) Token: 0x060036A5 RID: 13989 RVA: 0x00142EA6 File Offset: 0x001412A6
		public bool isPlaySE
		{
			get
			{
				return this._isPlaySE;
			}
			set
			{
				this._isPlaySE = value;
			}
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x00142EAF File Offset: 0x001412AF
		protected void EnterSE()
		{
			if (!this._isPlaySE)
			{
				return;
			}
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x00142ED0 File Offset: 0x001412D0
		protected void LinkToggle(Toggle toggle, Action<bool> act)
		{
			toggle.onValueChanged.AsObservable<bool>().Subscribe(delegate(bool isOn)
			{
				this.EnterSE();
				act(isOn);
			});
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x00142F10 File Offset: 0x00141310
		protected void LinkToggleArray(Toggle[] _tgls, Action<int> _action)
		{
			if (BaseSetting.<>f__mg$cache0 == null)
			{
				BaseSetting.<>f__mg$cache0 = new Func<Toggle, IObservable<bool>>(UnityUIComponentExtensions.OnValueChangedAsObservable);
			}
			ReadOnlyReactiveProperty<int> source = (from list in _tgls.Select(BaseSetting.<>f__mg$cache0).CombineLatest<bool>()
			select list.IndexOf(true) into i
			where i >= 0
			select i).ToReadOnlyReactiveProperty<int>();
			source.Skip(1).Subscribe(delegate(int i)
			{
				Action<int> action = _action;
				if (action != null)
				{
					action(i);
				}
				this.EnterSE();
			});
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x00142FBC File Offset: 0x001413BC
		protected void LinkSlider(Slider slider, Action<float> act)
		{
			slider.onValueChanged.AsObservable<float>().Subscribe(delegate(float value)
			{
				act(value);
			});
			(from _ in slider.OnPointerDownAsObservable()
			where UnityEngine.Input.GetMouseButtonDown(0)
			select _).Subscribe(delegate(PointerEventData _)
			{
				this.EnterSE();
			});
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x00143034 File Offset: 0x00141434
		protected void LinkTmpDropdown(TMP_Dropdown dropdown, Action<float> act)
		{
			dropdown.onValueChanged.AsObservable<int>().Subscribe(delegate(int value)
			{
				act((float)value);
			});
			(from _ in dropdown.OnPointerDownAsObservable()
			where UnityEngine.Input.GetMouseButtonDown(0)
			select _).Subscribe(delegate(PointerEventData _)
			{
				this.EnterSE();
			});
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x001430AC File Offset: 0x001414AC
		protected void SetToggleUIArray(Toggle[] _toggles, Action<Toggle, int> _action)
		{
			foreach (var <>__AnonType in _toggles.Select((Toggle tgl, int index) => new
			{
				tgl,
				index
			}))
			{
				_action(<>__AnonType.tgl, <>__AnonType.index);
			}
		}

		// Token: 0x060036AC RID: 13996
		public abstract void Init();

		// Token: 0x060036AD RID: 13997
		protected abstract void ValueToUI();

		// Token: 0x060036AE RID: 13998 RVA: 0x00143130 File Offset: 0x00141530
		public void UIPresenter()
		{
			bool isPlaySE = this._isPlaySE;
			this._isPlaySE = false;
			this.ValueToUI();
			this._isPlaySE = isPlaySE;
		}

		// Token: 0x0400371B RID: 14107
		private bool _isPlaySE = true;

		// Token: 0x0400371C RID: 14108
		[CompilerGenerated]
		private static Func<Toggle, IObservable<bool>> <>f__mg$cache0;
	}
}
