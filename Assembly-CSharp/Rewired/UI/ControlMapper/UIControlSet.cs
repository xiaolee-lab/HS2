using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000563 RID: 1379
	[AddComponentMenu("")]
	public class UIControlSet : MonoBehaviour
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x000AB250 File Offset: 0x000A9650
		private Dictionary<int, UIControl> controls
		{
			get
			{
				Dictionary<int, UIControl> result;
				if ((result = this._controls) == null)
				{
					result = (this._controls = new Dictionary<int, UIControl>());
				}
				return result;
			}
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000AB278 File Offset: 0x000A9678
		public void SetTitle(string text)
		{
			if (this.title == null)
			{
				return;
			}
			this.title.text = text;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000AB298 File Offset: 0x000A9698
		public T GetControl<T>(int uniqueId) where T : UIControl
		{
			UIControl uicontrol;
			this.controls.TryGetValue(uniqueId, out uicontrol);
			return uicontrol as T;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000AB2C0 File Offset: 0x000A96C0
		public UISliderControl CreateSlider(GameObject prefab, Sprite icon, float minValue, float maxValue, Action<int, float> valueChangedCallback, Action<int> cancelCallback)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
			UISliderControl control = gameObject.GetComponent<UISliderControl>();
			if (control == null)
			{
				UnityEngine.Object.Destroy(gameObject);
				UnityEngine.Debug.LogError("Prefab missing UISliderControl component!");
				return null;
			}
			gameObject.transform.SetParent(base.transform, false);
			if (control.iconImage != null)
			{
				control.iconImage.sprite = icon;
			}
			if (control.slider != null)
			{
				control.slider.minValue = minValue;
				control.slider.maxValue = maxValue;
				if (valueChangedCallback != null)
				{
					control.slider.onValueChanged.AddListener(delegate(float value)
					{
						valueChangedCallback(control.id, value);
					});
				}
				if (cancelCallback != null)
				{
					control.SetCancelCallback(delegate
					{
						cancelCallback(control.id);
					});
				}
			}
			this.controls.Add(control.id, control);
			return control;
		}

		// Token: 0x04001E27 RID: 7719
		[SerializeField]
		private Text title;

		// Token: 0x04001E28 RID: 7720
		private Dictionary<int, UIControl> _controls;
	}
}
