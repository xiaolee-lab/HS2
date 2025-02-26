using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ADV
{
	// Token: 0x0200079D RID: 1949
	[RequireComponent(typeof(Toggle))]
	public class ToggleText : MonoBehaviour
	{
		// Token: 0x06002E33 RID: 11827 RVA: 0x001051EC File Offset: 0x001035EC
		private void SetColor(Color color)
		{
			if (this._tmpText != null)
			{
				this._tmpText.color = color;
				return;
			}
			if (this._unityText != null)
			{
				this._unityText.color = color;
				return;
			}
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x0010522C File Offset: 0x0010362C
		private void Start()
		{
			if (this._toggle == null)
			{
				this._toggle = base.GetComponent<Toggle>();
				if (this._toggle == null)
				{
					UnityEngine.Object.Destroy(this);
					return;
				}
			}
			if (this._tmpText == null && this._unityText == null)
			{
				if (this._tmpText == null)
				{
					this._tmpText = base.GetComponentInChildren<TextMeshProUGUI>();
				}
				if (this._unityText == null)
				{
					this._unityText = base.GetComponentInChildren<Text>();
				}
				if (this._tmpText == null && this._unityText == null)
				{
					UnityEngine.Object.Destroy(this);
					return;
				}
			}
			ColorBlock colors = this._toggle.colors;
			ReactiveProperty<bool> isOnUI = new ReactiveProperty<bool>(false);
			ReactiveProperty<bool> isPressUI = new ReactiveProperty<bool>(false);
			(from _ in isOnUI
			where !this._toggle.isOn
			select _).Subscribe(delegate(bool isOn)
			{
				if (isOn)
				{
					if (!isPressUI.Value)
					{
						this.SetColor(colors.highlightedColor);
					}
				}
				else if (isPressUI.Value)
				{
					this.SetColor(colors.highlightedColor);
				}
			});
			(from _ in isPressUI
			where !this._toggle.isOn
			select _).Subscribe(delegate(bool isOn)
			{
				if (!isOn && isOnUI.Value)
				{
					this.SetColor(colors.highlightedColor);
				}
			});
			(from isOn in isPressUI.CombineLatest(isOnUI, (bool x, bool y) => x && y)
			where isOn
			select isOn).Subscribe(delegate(bool _)
			{
				if (!this._toggle.isOn)
				{
					this.SetColor(colors.pressedColor);
				}
			});
			(from isOut in isPressUI.CombineLatest(isOnUI, (bool x, bool y) => !x && !y)
			where isOut
			select isOut).Subscribe(delegate(bool _)
			{
				if (this._toggle.isOn)
				{
					this.SetColor(colors.pressedColor);
				}
				else
				{
					this.SetColor(colors.normalColor);
				}
			});
			this._toggle.OnPointerDownAsObservable().Subscribe(delegate(PointerEventData _)
			{
				isPressUI.Value = true;
			});
			this._toggle.OnPointerUpAsObservable().Subscribe(delegate(PointerEventData _)
			{
				isPressUI.Value = false;
			});
			this._toggle.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
			{
				isOnUI.Value = true;
			});
			this._toggle.OnPointerExitAsObservable().Subscribe(delegate(PointerEventData _)
			{
				isOnUI.Value = false;
			});
			this._toggle.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
			{
				if (isOn)
				{
					this.SetColor(colors.pressedColor);
				}
				else
				{
					this.SetColor(colors.normalColor);
				}
			});
		}

		// Token: 0x04002D18 RID: 11544
		[SerializeField]
		private Toggle _toggle;

		// Token: 0x04002D19 RID: 11545
		[SerializeField]
		private TextMeshProUGUI _tmpText;

		// Token: 0x04002D1A RID: 11546
		[SerializeField]
		private Text _unityText;
	}
}
