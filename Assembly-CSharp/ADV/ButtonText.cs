using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ADV
{
	// Token: 0x0200079B RID: 1947
	[RequireComponent(typeof(Button))]
	public class ButtonText : MonoBehaviour
	{
		// Token: 0x06002E2D RID: 11821 RVA: 0x00104E76 File Offset: 0x00103276
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

		// Token: 0x06002E2E RID: 11822 RVA: 0x00104EB4 File Offset: 0x001032B4
		private void Start()
		{
			if (this._button == null)
			{
				this._button = base.GetComponent<Button>();
				if (this._button == null)
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
			ColorBlock colors = this._button.colors;
			ReactiveProperty<bool> isOnUI = new ReactiveProperty<bool>(false);
			ReactiveProperty<bool> isPressUI = new ReactiveProperty<bool>(false);
			isOnUI.Subscribe(delegate(bool isOn)
			{
				if (isOn)
				{
					if (isPressUI.Value)
					{
						this.SetColor(colors.pressedColor);
					}
					else
					{
						this.SetColor(colors.highlightedColor);
					}
				}
				else if (isPressUI.Value)
				{
					this.SetColor(colors.highlightedColor);
				}
				else
				{
					this.SetColor(colors.normalColor);
				}
			});
			isPressUI.Subscribe(delegate(bool isOn)
			{
				if (isOn)
				{
					this.SetColor(colors.pressedColor);
				}
				else if (isOnUI.Value)
				{
					this.SetColor(colors.highlightedColor);
				}
				else
				{
					this.SetColor(colors.normalColor);
				}
			});
			this._button.OnPointerDownAsObservable().Subscribe(delegate(PointerEventData _)
			{
				isPressUI.Value = true;
			});
			this._button.OnPointerUpAsObservable().Subscribe(delegate(PointerEventData _)
			{
				isPressUI.Value = false;
			});
			this._button.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
			{
				isOnUI.Value = true;
			});
			this._button.OnPointerExitAsObservable().Subscribe(delegate(PointerEventData _)
			{
				isOnUI.Value = false;
			});
		}

		// Token: 0x04002D14 RID: 11540
		[SerializeField]
		private Button _button;

		// Token: 0x04002D15 RID: 11541
		[SerializeField]
		private TextMeshProUGUI _tmpText;

		// Token: 0x04002D16 RID: 11542
		[SerializeField]
		private Text _unityText;
	}
}
