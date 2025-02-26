using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200056B RID: 1387
	[AddComponentMenu("")]
	[RequireComponent(typeof(CanvasGroup))]
	public class Window : MonoBehaviour
	{
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x0009CA4B File Offset: 0x0009AE4B
		public bool hasFocus
		{
			get
			{
				return this._isFocusedCallback != null && this._isFocusedCallback(this._id);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x0009CA6F File Offset: 0x0009AE6F
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x0009CA77 File Offset: 0x0009AE77
		public RectTransform rectTransform
		{
			get
			{
				if (this._rectTransform == null)
				{
					this._rectTransform = base.gameObject.GetComponent<RectTransform>();
				}
				return this._rectTransform;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x0009CAA1 File Offset: 0x0009AEA1
		public Text titleText
		{
			get
			{
				return this._titleText;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0009CAA9 File Offset: 0x0009AEA9
		public List<Text> contentText
		{
			get
			{
				return this._contentText;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x0009CAB1 File Offset: 0x0009AEB1
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x0009CAB9 File Offset: 0x0009AEB9
		public GameObject defaultUIElement
		{
			get
			{
				return this._defaultUIElement;
			}
			set
			{
				this._defaultUIElement = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x0009CAC2 File Offset: 0x0009AEC2
		// (set) Token: 0x06001D35 RID: 7477 RVA: 0x0009CACA File Offset: 0x0009AECA
		public Action<int> updateCallback
		{
			get
			{
				return this._updateCallback;
			}
			set
			{
				this._updateCallback = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x0009CAD3 File Offset: 0x0009AED3
		public Window.Timer timer
		{
			get
			{
				return this._timer;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0009CADC File Offset: 0x0009AEDC
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x0009CB00 File Offset: 0x0009AF00
		public int width
		{
			get
			{
				return (int)this.rectTransform.sizeDelta.x;
			}
			set
			{
				Vector2 sizeDelta = this.rectTransform.sizeDelta;
				sizeDelta.x = (float)value;
				this.rectTransform.sizeDelta = sizeDelta;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0009CB30 File Offset: 0x0009AF30
		// (set) Token: 0x06001D3A RID: 7482 RVA: 0x0009CB54 File Offset: 0x0009AF54
		public int height
		{
			get
			{
				return (int)this.rectTransform.sizeDelta.y;
			}
			set
			{
				Vector2 sizeDelta = this.rectTransform.sizeDelta;
				sizeDelta.y = (float)value;
				this.rectTransform.sizeDelta = sizeDelta;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0009CB82 File Offset: 0x0009AF82
		protected bool initialized
		{
			get
			{
				return this._initialized;
			}
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x0009CB8A File Offset: 0x0009AF8A
		private void OnEnable()
		{
			base.StartCoroutine("OnEnableAsync");
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0009CB98 File Offset: 0x0009AF98
		protected virtual void Update()
		{
			if (!this._initialized)
			{
				return;
			}
			if (!this.hasFocus)
			{
				return;
			}
			this.CheckUISelection();
			if (this._updateCallback != null)
			{
				this._updateCallback(this._id);
			}
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0009CBD4 File Offset: 0x0009AFD4
		public virtual void Initialize(int id, Func<int, bool> isFocusedCallback)
		{
			if (this._initialized)
			{
				UnityEngine.Debug.LogError("Window is already initialized!");
				return;
			}
			this._id = id;
			this._isFocusedCallback = isFocusedCallback;
			this._timer = new Window.Timer();
			this._contentText = new List<Text>();
			this._canvasGroup = base.GetComponent<CanvasGroup>();
			this._initialized = true;
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0009CC2E File Offset: 0x0009B02E
		public void SetSize(int width, int height)
		{
			this.rectTransform.sizeDelta = new Vector2((float)width, (float)height);
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0009CC44 File Offset: 0x0009B044
		public void CreateTitleText(GameObject prefab, Vector2 offset)
		{
			this.CreateText(prefab, ref this._titleText, "Title Text", UIPivot.TopCenter, UIAnchor.TopHStretch, offset);
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0009CC63 File Offset: 0x0009B063
		public void CreateTitleText(GameObject prefab, Vector2 offset, string text)
		{
			this.CreateTitleText(prefab, offset);
			this.SetTitleText(text);
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0009CC74 File Offset: 0x0009B074
		public void AddContentText(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			Text item = null;
			this.CreateText(prefab, ref item, "Content Text", pivot, anchor, offset);
			this._contentText.Add(item);
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0009CCA1 File Offset: 0x0009B0A1
		public void AddContentText(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset, string text)
		{
			this.AddContentText(prefab, pivot, anchor, offset);
			this.SetContentText(text, this._contentText.Count - 1);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0009CCC3 File Offset: 0x0009B0C3
		public void AddContentImage(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			this.CreateImage(prefab, "Image", pivot, anchor, offset);
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0009CCD5 File Offset: 0x0009B0D5
		public void AddContentImage(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset, string text)
		{
			this.AddContentImage(prefab, pivot, anchor, offset);
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0009CCE4 File Offset: 0x0009B0E4
		public void CreateButton(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset, string buttonText, UnityAction confirmCallback, UnityAction cancelCallback, bool setDefault)
		{
			if (prefab == null)
			{
				return;
			}
			ButtonInfo buttonInfo;
			GameObject gameObject = this.CreateButton(prefab, "Button", anchor, pivot, offset, out buttonInfo);
			if (gameObject == null)
			{
				return;
			}
			Button component = gameObject.GetComponent<Button>();
			if (confirmCallback != null)
			{
				component.onClick.AddListener(confirmCallback);
			}
			CustomButton customButton = component as CustomButton;
			if (cancelCallback != null && customButton != null)
			{
				customButton.CancelEvent += cancelCallback;
			}
			if (buttonInfo.text != null)
			{
				buttonInfo.text.text = buttonText;
			}
			if (setDefault)
			{
				this._defaultUIElement = gameObject;
			}
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0009CD87 File Offset: 0x0009B187
		public string GetTitleText(string text)
		{
			if (this._titleText == null)
			{
				return string.Empty;
			}
			return this._titleText.text;
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0009CDAB File Offset: 0x0009B1AB
		public void SetTitleText(string text)
		{
			if (this._titleText == null)
			{
				return;
			}
			this._titleText.text = text;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0009CDCC File Offset: 0x0009B1CC
		public string GetContentText(int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return string.Empty;
			}
			return this._contentText[index].text;
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0009CE24 File Offset: 0x0009B224
		public float GetContentTextHeight(int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return 0f;
			}
			return this._contentText[index].rectTransform.sizeDelta.y;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0009CE88 File Offset: 0x0009B288
		public void SetContentText(string text, int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return;
			}
			this._contentText[index].text = text;
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0009CEDB File Offset: 0x0009B2DB
		public void SetUpdateCallback(Action<int> callback)
		{
			this.updateCallback = callback;
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0009CEE4 File Offset: 0x0009B2E4
		public virtual void TakeInputFocus()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(this._defaultUIElement);
			this.Enable();
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0009CF0D File Offset: 0x0009B30D
		public virtual void Enable()
		{
			this._canvasGroup.interactable = true;
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0009CF1B File Offset: 0x0009B31B
		public virtual void Disable()
		{
			this._canvasGroup.interactable = false;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0009CF29 File Offset: 0x0009B329
		public virtual void Cancel()
		{
			if (!this.initialized)
			{
				return;
			}
			if (this.cancelCallback != null)
			{
				this.cancelCallback();
			}
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0009CF50 File Offset: 0x0009B350
		private void CreateText(GameObject prefab, ref Text textComponent, string name, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			if (prefab == null || this.content == null)
			{
				return;
			}
			if (textComponent != null)
			{
				UnityEngine.Debug.LogError("Window already has " + name + "!");
				return;
			}
			GameObject gameObject = UITools.InstantiateGUIObject<Text>(prefab, this.content.transform, name, pivot, anchor.min, anchor.max, offset);
			if (gameObject == null)
			{
				return;
			}
			textComponent = gameObject.GetComponent<Text>();
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0009CFE0 File Offset: 0x0009B3E0
		private void CreateImage(GameObject prefab, string name, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			if (prefab == null || this.content == null)
			{
				return;
			}
			UITools.InstantiateGUIObject<Image>(prefab, this.content.transform, name, pivot, anchor.min, anchor.max, offset);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0009D034 File Offset: 0x0009B434
		private GameObject CreateButton(GameObject prefab, string name, UIAnchor anchor, UIPivot pivot, Vector2 offset, out ButtonInfo buttonInfo)
		{
			buttonInfo = null;
			if (prefab == null)
			{
				return null;
			}
			GameObject gameObject = UITools.InstantiateGUIObject<ButtonInfo>(prefab, this.content.transform, name, pivot, anchor.min, anchor.max, offset);
			if (gameObject == null)
			{
				return null;
			}
			buttonInfo = gameObject.GetComponent<ButtonInfo>();
			Button component = gameObject.GetComponent<Button>();
			if (component == null)
			{
				UnityEngine.Debug.Log("Button prefab is missing Button component!");
				return null;
			}
			if (buttonInfo == null)
			{
				UnityEngine.Debug.Log("Button prefab is missing ButtonInfo component!");
				return null;
			}
			return gameObject;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0009D0D0 File Offset: 0x0009B4D0
		private IEnumerator OnEnableAsync()
		{
			yield return 1;
			if (EventSystem.current == null)
			{
				yield break;
			}
			if (this.defaultUIElement != null)
			{
				EventSystem.current.SetSelectedGameObject(this.defaultUIElement);
			}
			else
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			yield break;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0009D0EC File Offset: 0x0009B4EC
		private void CheckUISelection()
		{
			if (!this.hasFocus)
			{
				return;
			}
			if (EventSystem.current == null)
			{
				return;
			}
			if (EventSystem.current.currentSelectedGameObject == null)
			{
				this.RestoreDefaultOrLastUISelection();
			}
			this.lastUISelection = EventSystem.current.currentSelectedGameObject;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0009D144 File Offset: 0x0009B544
		private void RestoreDefaultOrLastUISelection()
		{
			if (!this.hasFocus)
			{
				return;
			}
			if (this.lastUISelection == null || !this.lastUISelection.activeInHierarchy)
			{
				this.SetUISelection(this._defaultUIElement);
				return;
			}
			this.SetUISelection(this.lastUISelection);
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0009D197 File Offset: 0x0009B597
		private void SetUISelection(GameObject selection)
		{
			if (EventSystem.current == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(selection);
		}

		// Token: 0x04001E37 RID: 7735
		public Image backgroundImage;

		// Token: 0x04001E38 RID: 7736
		public GameObject content;

		// Token: 0x04001E39 RID: 7737
		private bool _initialized;

		// Token: 0x04001E3A RID: 7738
		private int _id = -1;

		// Token: 0x04001E3B RID: 7739
		private RectTransform _rectTransform;

		// Token: 0x04001E3C RID: 7740
		private Text _titleText;

		// Token: 0x04001E3D RID: 7741
		private List<Text> _contentText;

		// Token: 0x04001E3E RID: 7742
		private GameObject _defaultUIElement;

		// Token: 0x04001E3F RID: 7743
		private Action<int> _updateCallback;

		// Token: 0x04001E40 RID: 7744
		private Func<int, bool> _isFocusedCallback;

		// Token: 0x04001E41 RID: 7745
		private Window.Timer _timer;

		// Token: 0x04001E42 RID: 7746
		private CanvasGroup _canvasGroup;

		// Token: 0x04001E43 RID: 7747
		public UnityAction cancelCallback;

		// Token: 0x04001E44 RID: 7748
		private GameObject lastUISelection;

		// Token: 0x0200056C RID: 1388
		public class Timer
		{
			// Token: 0x170002BE RID: 702
			// (get) Token: 0x06001D59 RID: 7513 RVA: 0x0009D1BD File Offset: 0x0009B5BD
			public bool started
			{
				get
				{
					return this._started;
				}
			}

			// Token: 0x170002BF RID: 703
			// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0009D1C5 File Offset: 0x0009B5C5
			public bool finished
			{
				get
				{
					if (!this.started)
					{
						return false;
					}
					if (Time.realtimeSinceStartup < this.end)
					{
						return false;
					}
					this._started = false;
					return true;
				}
			}

			// Token: 0x170002C0 RID: 704
			// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0009D1EE File Offset: 0x0009B5EE
			public float remaining
			{
				get
				{
					if (!this._started)
					{
						return 0f;
					}
					return this.end - Time.realtimeSinceStartup;
				}
			}

			// Token: 0x06001D5C RID: 7516 RVA: 0x0009D20D File Offset: 0x0009B60D
			public void Start(float length)
			{
				this.end = Time.realtimeSinceStartup + length;
				this._started = true;
			}

			// Token: 0x06001D5D RID: 7517 RVA: 0x0009D223 File Offset: 0x0009B623
			public void Stop()
			{
				this._started = false;
			}

			// Token: 0x04001E45 RID: 7749
			private bool _started;

			// Token: 0x04001E46 RID: 7750
			private float end;
		}
	}
}
