using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx.Misc;

namespace AIProject
{
	// Token: 0x02000FCA RID: 4042
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("YK/UI/Dropdown", 35)]
	public class OptimizedDropdown : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x0600864C RID: 34380 RVA: 0x003825A4 File Offset: 0x003809A4
		protected OptimizedDropdown()
		{
		}

		// Token: 0x17001D48 RID: 7496
		// (get) Token: 0x0600864D RID: 34381 RVA: 0x003825CD File Offset: 0x003809CD
		// (set) Token: 0x0600864E RID: 34382 RVA: 0x003825D5 File Offset: 0x003809D5
		public bool FocusedSelectedItem
		{
			get
			{
				return this._focusedSelectedItem;
			}
			set
			{
				this._focusedSelectedItem = value;
			}
		}

		// Token: 0x17001D49 RID: 7497
		// (get) Token: 0x0600864F RID: 34383 RVA: 0x003825DE File Offset: 0x003809DE
		public ScrollRect ScrollRect
		{
			[CompilerGenerated]
			get
			{
				return this._scrollRect;
			}
		}

		// Token: 0x17001D4A RID: 7498
		// (get) Token: 0x06008650 RID: 34384 RVA: 0x003825E6 File Offset: 0x003809E6
		// (set) Token: 0x06008651 RID: 34385 RVA: 0x003825EE File Offset: 0x003809EE
		public RectTransform Template
		{
			get
			{
				return this._template;
			}
			set
			{
				this._template = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17001D4B RID: 7499
		// (get) Token: 0x06008652 RID: 34386 RVA: 0x003825FD File Offset: 0x003809FD
		// (set) Token: 0x06008653 RID: 34387 RVA: 0x00382605 File Offset: 0x00380A05
		public Text CaptionText
		{
			get
			{
				return this._captionText;
			}
			set
			{
				this._captionText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17001D4C RID: 7500
		// (get) Token: 0x06008654 RID: 34388 RVA: 0x00382614 File Offset: 0x00380A14
		// (set) Token: 0x06008655 RID: 34389 RVA: 0x0038261C File Offset: 0x00380A1C
		public Image CaptionImage
		{
			get
			{
				return this._captionImage;
			}
			set
			{
				this._captionImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17001D4D RID: 7501
		// (get) Token: 0x06008656 RID: 34390 RVA: 0x0038262B File Offset: 0x00380A2B
		// (set) Token: 0x06008657 RID: 34391 RVA: 0x00382633 File Offset: 0x00380A33
		public Text ItemText
		{
			get
			{
				return this._itemText;
			}
			set
			{
				this._itemText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17001D4E RID: 7502
		// (get) Token: 0x06008658 RID: 34392 RVA: 0x00382642 File Offset: 0x00380A42
		// (set) Token: 0x06008659 RID: 34393 RVA: 0x0038264A File Offset: 0x00380A4A
		public Image ItemImage
		{
			get
			{
				return this._itemImage;
			}
			set
			{
				this._itemImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17001D4F RID: 7503
		// (get) Token: 0x0600865A RID: 34394 RVA: 0x00382659 File Offset: 0x00380A59
		// (set) Token: 0x0600865B RID: 34395 RVA: 0x00382666 File Offset: 0x00380A66
		public List<OptimizedDropdown.OptionData> Options
		{
			get
			{
				return this._options.Options;
			}
			set
			{
				this._options.Options = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17001D50 RID: 7504
		// (get) Token: 0x0600865C RID: 34396 RVA: 0x0038267A File Offset: 0x00380A7A
		// (set) Token: 0x0600865D RID: 34397 RVA: 0x00382682 File Offset: 0x00380A82
		public OptimizedDropdown.DropdownEvent OnValueChanged
		{
			get
			{
				return this._onValueChanged;
			}
			set
			{
				this._onValueChanged = value;
			}
		}

		// Token: 0x17001D51 RID: 7505
		// (get) Token: 0x0600865E RID: 34398 RVA: 0x0038268B File Offset: 0x00380A8B
		// (set) Token: 0x0600865F RID: 34399 RVA: 0x00382694 File Offset: 0x00380A94
		public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				bool flag = Application.isPlaying && (value == this._value || this.Options.Count == 0);
				if (flag)
				{
					return;
				}
				this._value = Mathf.Clamp(value, 0, this.Options.Count - 1);
				this.RefreshShownValue();
				this._onValueChanged.Invoke(this._value);
			}
		}

		// Token: 0x17001D52 RID: 7506
		// (get) Token: 0x06008660 RID: 34400 RVA: 0x00382704 File Offset: 0x00380B04
		public bool IsExpanded
		{
			[CompilerGenerated]
			get
			{
				return this._dropdown != null;
			}
		}

		// Token: 0x06008661 RID: 34401 RVA: 0x00382714 File Offset: 0x00380B14
		protected override void Awake()
		{
			Image captionImage = this._captionImage;
			if (captionImage)
			{
				this._captionImage.enabled = (this._captionImage.sprite != null);
			}
			RectTransform template = this._template;
			if (template)
			{
				this._template.gameObject.SetActive(false);
			}
		}

		// Token: 0x06008662 RID: 34402 RVA: 0x00382774 File Offset: 0x00380B74
		public void RefreshShownValue()
		{
			OptimizedDropdown.OptionData optionData = OptimizedDropdown._noOptionData;
			if (this.Options.Count > 0)
			{
				optionData = this.Options[Mathf.Clamp(this._value, 0, this.Options.Count - 1)];
			}
			if (this._captionText)
			{
				if (optionData != null && optionData.Text != null)
				{
					this._captionText.text = optionData.Text;
				}
				else
				{
					this._captionText.text = string.Empty;
				}
			}
			if (this._captionImage)
			{
				if (optionData != null)
				{
					this._captionImage.sprite = optionData.Sprite;
				}
				else
				{
					this._captionImage.sprite = null;
				}
				this._captionImage.enabled = (this._captionImage.sprite != null);
			}
		}

		// Token: 0x06008663 RID: 34403 RVA: 0x00382858 File Offset: 0x00380C58
		public void AddOptions(List<OptimizedDropdown.OptionData> options)
		{
			this.Options.AddRange(options);
			this.RefreshShownValue();
		}

		// Token: 0x06008664 RID: 34404 RVA: 0x0038286C File Offset: 0x00380C6C
		public void AddOptions(List<string> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.Options.Add(new OptimizedDropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x06008665 RID: 34405 RVA: 0x003828AD File Offset: 0x00380CAD
		public void ClearOptions()
		{
			this.Options.Clear();
			this.RefreshShownValue();
		}

		// Token: 0x06008666 RID: 34406 RVA: 0x003828C0 File Offset: 0x00380CC0
		private void SetupTemplate()
		{
			this._validTemplate = false;
			if (this._template)
			{
				GameObject gameObject = this._template.gameObject;
				gameObject.SetActive(true);
				Toggle componentInChildren = this._template.GetComponentInChildren<Toggle>();
				this._validTemplate = true;
				if (!componentInChildren || componentInChildren.transform == this.Template)
				{
					this._validTemplate = false;
				}
				else if (this.ItemText != null && !this.ItemText.transform.IsChildOf(componentInChildren.transform))
				{
					this._validTemplate = false;
				}
				else if (this.ItemImage != null && !this.ItemImage.transform.IsChildOf(componentInChildren.transform))
				{
					this._validTemplate = false;
				}
				if (!this._validTemplate)
				{
					gameObject.SetActive(false);
				}
				else
				{
					OptimizedDropdown.DropdownItem dropdownItem = componentInChildren.gameObject.AddComponent<OptimizedDropdown.DropdownItem>();
					dropdownItem.Text = this._itemText;
					dropdownItem.Image = this._itemImage;
					dropdownItem.Toggle = componentInChildren;
					dropdownItem.RectTransform = (componentInChildren.transform as RectTransform);
					Canvas orAddComponent = OptimizedDropdown.GetOrAddComponent<Canvas>(gameObject);
					orAddComponent.overrideSorting = true;
					orAddComponent.sortingOrder = 30000;
					OptimizedDropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
					OptimizedDropdown.GetOrAddComponent<CanvasGroup>(gameObject);
					gameObject.SetActive(false);
					this._validTemplate = true;
				}
				this._pool = new OptimizedDropdown.DropdownItemPool(null, null, componentInChildren.gameObject);
			}
		}

		// Token: 0x06008667 RID: 34407 RVA: 0x00382A44 File Offset: 0x00380E44
		private static T GetOrAddComponent<T>(GameObject go) where T : Component
		{
			T component = go.GetComponent<T>();
			return (!(component != null)) ? go.AddComponent<T>() : component;
		}

		// Token: 0x06008668 RID: 34408 RVA: 0x00382A75 File Offset: 0x00380E75
		public virtual void OnPointerClick(PointerEventData ped)
		{
			this.Show();
		}

		// Token: 0x06008669 RID: 34409 RVA: 0x00382A7D File Offset: 0x00380E7D
		public virtual void OnSubmit(BaseEventData bed)
		{
			this.Show();
		}

		// Token: 0x0600866A RID: 34410 RVA: 0x00382A85 File Offset: 0x00380E85
		public virtual void OnCancel(BaseEventData bed)
		{
			this.Hide();
		}

		// Token: 0x0600866B RID: 34411 RVA: 0x00382A90 File Offset: 0x00380E90
		public void Show()
		{
			if (!this.IsActive() || !this.IsInteractable() || (this._dropdown != null && this._dropdown.activeSelf))
			{
				return;
			}
			if (!this._validTemplate)
			{
				this.SetupTemplate();
				if (!this._validTemplate)
				{
					return;
				}
			}
			List<Canvas> list = ListPool<Canvas>.Get();
			base.GetComponentsInParent<Canvas>(false, list);
			if (list.Count > 0)
			{
				Canvas canvas = list[0];
				ListPool<Canvas>.Release(list);
				this._template.gameObject.SetActive(true);
				if (this._dropdown == null)
				{
					this._dropdown = UnityEngine.Object.Instantiate<GameObject>(this._template.gameObject);
					this._dropdown.name = "Dropdown List";
					OptimizedDropdown.DropdownItem componentInChildren = this._dropdown.GetComponentInChildren<OptimizedDropdown.DropdownItem>();
					componentInChildren.gameObject.SetActive(false);
					this._scrollRect = this._dropdown.GetComponent<ScrollRect>();
				}
				this._dropdown.SetActive(true);
				RectTransform rectTransform = this._dropdown.transform as RectTransform;
				rectTransform.SetParent(this._template.transform.parent, false);
				OptimizedDropdown.DropdownItem componentInChildren2 = this._template.GetComponentInChildren<OptimizedDropdown.DropdownItem>();
				GameObject gameObject = this._dropdown.GetComponentInChildren<OptimizedDropdown.DropdownItem>(true).transform.parent.gameObject;
				RectTransform rectTransform2 = gameObject.transform as RectTransform;
				componentInChildren2.RectTransform.gameObject.SetActive(true);
				if (this._rect == null)
				{
					this._rect = new Rect?(rectTransform2.rect);
				}
				Rect rect = componentInChildren2.RectTransform.rect;
				Vector2 vector = rect.min - this._rect.Value.min + componentInChildren2.RectTransform.localPosition;
				Vector2 vector2 = rect.max - this._rect.Value.max + componentInChildren2.RectTransform.localPosition;
				Vector2 size = rect.size;
				this._items.Clear();
				Toggle toggle = null;
				for (int i = 0; i < this.Options.Count; i++)
				{
					int index = i;
					OptimizedDropdown.OptionData data = this.Options[i];
					OptimizedDropdown.DropdownItem dropdownItem = this.AddItem(data, gameObject.transform, this._items);
					if (dropdownItem != null)
					{
						dropdownItem.Toggle.isOn = (this.Value == i);
						dropdownItem.Toggle.onValueChanged.AddListener(delegate(bool x)
						{
							if (!x)
							{
								return;
							}
							this.Value = index;
							this.Hide();
						});
						if (toggle != null)
						{
							Navigation navigation = toggle.navigation;
							Navigation navigation2 = dropdownItem.Toggle.navigation;
							navigation.mode = Navigation.Mode.Explicit;
							navigation2.mode = Navigation.Mode.Explicit;
							navigation.selectOnDown = dropdownItem.Toggle;
							navigation.selectOnRight = dropdownItem.Toggle;
							navigation2.selectOnLeft = toggle;
							navigation2.selectOnUp = toggle;
							toggle.navigation = navigation;
							dropdownItem.Toggle.navigation = navigation2;
						}
						toggle = dropdownItem.Toggle;
					}
				}
				Vector2 sizeDelta = rectTransform2.sizeDelta;
				sizeDelta.y = size.y * (float)this._items.Count + vector.y - vector2.y;
				rectTransform2.sizeDelta = sizeDelta;
				float num = rectTransform.rect.height - rectTransform2.rect.height;
				if (num > 0f)
				{
					rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - num);
				}
				Vector3[] array = new Vector3[4];
				rectTransform.GetWorldCorners(array);
				RectTransform rectTransform3 = canvas.transform as RectTransform;
				Rect rect2 = rectTransform3.rect;
				for (int j = 0; j < 2; j++)
				{
					bool flag = false;
					for (int k = 0; k < 4; k++)
					{
						Vector3 vector3 = rectTransform3.InverseTransformPoint(array[k]);
						bool flag2 = vector3[j] < rect2.min[j] || vector3[j] > rect2.max[j];
						if (flag2)
						{
							flag = true;
							break;
						}
					}
					bool flag3 = flag;
					if (flag3)
					{
						RectTransformUtility.FlipLayoutOnAxis(rectTransform, j, false, false);
					}
				}
				for (int l = 0; l < this._items.Count; l++)
				{
					RectTransform rectTransform4 = this._items[l].RectTransform;
					rectTransform4.anchorMin = new Vector2(rectTransform4.anchorMin.x, 0f);
					rectTransform4.anchorMax = new Vector2(rectTransform4.anchorMax.x, 0f);
					rectTransform4.anchoredPosition = new Vector2(rectTransform4.anchoredPosition.x, vector.y + size.y * (float)(this._items.Count - 1 - l) + size.y * rectTransform4.pivot.y);
					rectTransform4.sizeDelta = new Vector2(rectTransform4.sizeDelta.x, size.y);
				}
				this.AlphaFadeList(0.15f, 0f, 1f, delegate()
				{
				});
				this._template.gameObject.SetActive(false);
				this._blocker = this.CreateBlocker(canvas);
				if (this._focusedSelectedItem)
				{
					Toggle toggle2 = this._scrollRect.content.GetComponentsInChildren<Toggle>().FirstOrDefault((Toggle x) => x.isOn);
					RectTransform rectTransform5 = toggle2.transform as RectTransform;
					float num2 = this._scrollRect.content.rect.height - this._scrollRect.viewport.rect.height;
					float value = rectTransform5.anchoredPosition.y - rectTransform5.rect.height / 2f - this._scrollRect.viewport.rect.height / 2f;
					this._scrollRect.verticalScrollbar.value = Mathf.InverseLerp(0f, num2, Mathf.Clamp(value, 0f, num2));
				}
			}
		}

		// Token: 0x0600866C RID: 34412 RVA: 0x00383180 File Offset: 0x00381580
		protected virtual GameObject CreateBlocker(Canvas rootCanvas)
		{
			GameObject gameObject = new GameObject("Blocker");
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.SetParent(rootCanvas.transform, false);
			rectTransform.anchorMin = Vector3.zero;
			rectTransform.anchorMax = Vector3.one;
			rectTransform.sizeDelta = Vector2.zero;
			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.overrideSorting = true;
			Canvas component = this._dropdown.GetComponent<Canvas>();
			canvas.sortingLayerID = component.sortingLayerID;
			canvas.sortingOrder = component.sortingOrder - 1;
			gameObject.AddComponent<GraphicRaycaster>();
			Image image = gameObject.AddComponent<Image>();
			image.color = Color.clear;
			Button button = gameObject.AddComponent<Button>();
			button.onClick.AddListener(delegate()
			{
				this.Hide();
			});
			return gameObject;
		}

		// Token: 0x0600866D RID: 34413 RVA: 0x00383247 File Offset: 0x00381647
		protected virtual void DestroyBlocker(GameObject blocker)
		{
			UnityEngine.Object.Destroy(blocker);
		}

		// Token: 0x0600866E RID: 34414 RVA: 0x0038324F File Offset: 0x0038164F
		protected virtual OptimizedDropdown.DropdownItem CreateItem(OptimizedDropdown.DropdownItem itemTemplate)
		{
			return UnityEngine.Object.Instantiate<OptimizedDropdown.DropdownItem>(itemTemplate);
		}

		// Token: 0x0600866F RID: 34415 RVA: 0x00383258 File Offset: 0x00381658
		private OptimizedDropdown.DropdownItem AddItem(OptimizedDropdown.OptionData data, Transform parent, List<OptimizedDropdown.DropdownItem> items)
		{
			OptimizedDropdown.DropdownItem dropdownItem = this._pool.Get();
			dropdownItem.RectTransform.SetParent(parent, false);
			dropdownItem.gameObject.SetActive(true);
			dropdownItem.gameObject.name = "Item " + items.Count + ((data.Text == null) ? string.Empty : (": " + data.Text));
			dropdownItem.Toggle.onValueChanged.RemoveAllListeners();
			if (dropdownItem != null)
			{
				dropdownItem.Toggle.isOn = false;
			}
			if (dropdownItem.Text)
			{
				dropdownItem.Text.text = data.Text;
			}
			if (dropdownItem.Image)
			{
				dropdownItem.Image.sprite = data.Sprite;
				dropdownItem.Image.enabled = (dropdownItem.Image.sprite != null);
			}
			items.Add(dropdownItem);
			return dropdownItem;
		}

		// Token: 0x06008670 RID: 34416 RVA: 0x00383360 File Offset: 0x00381760
		private void AlphaFadeList(float duration, float alpha, Action onCompleted)
		{
			CanvasGroup component = this._dropdown.GetComponent<CanvasGroup>();
			this.AlphaFadeList(duration, component.alpha, alpha, onCompleted);
		}

		// Token: 0x06008671 RID: 34417 RVA: 0x00383388 File Offset: 0x00381788
		private void AlphaFadeList(float duration, float start, float end, Action onCompleted)
		{
			if (end.Equals(start))
			{
				return;
			}
			if (this._fadeSubscriber != null)
			{
				this._fadeSubscriber.Dispose();
			}
			this._fadeSubscriber = ObservableEasing.Linear(duration, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				this.Alpha = Mathf.Lerp(start, end, x.Value);
			}, delegate()
			{
				Action onCompleted2 = onCompleted;
				if (onCompleted2 != null)
				{
					onCompleted2();
				}
			});
		}

		// Token: 0x17001D53 RID: 7507
		// (set) Token: 0x06008672 RID: 34418 RVA: 0x00383418 File Offset: 0x00381818
		private float Alpha
		{
			set
			{
				if (this._dropdown == null)
				{
					return;
				}
				CanvasGroup component = this._dropdown.GetComponent<CanvasGroup>();
				component.alpha = value;
			}
		}

		// Token: 0x06008673 RID: 34419 RVA: 0x0038344C File Offset: 0x0038184C
		public void Hide()
		{
			if (this._dropdown != null)
			{
				this.AlphaFadeList(0.15f, 0f, delegate()
				{
					if (!this.IsActive())
					{
						return;
					}
					if (Time.timeScale != 0f)
					{
						Observable.Timer(TimeSpan.FromSeconds(0.15000000596046448)).Subscribe(delegate(long _)
						{
							Queue<OptimizedDropdown.DropdownItem> queue2 = this._items.ToQueue<OptimizedDropdown.DropdownItem>();
							while (queue2.Count > 0)
							{
								OptimizedDropdown.DropdownItem element2 = queue2.Dequeue();
								this._pool.Release(element2);
							}
							this._items.Clear();
							this._dropdown.SetActive(false);
						});
					}
					else
					{
						Queue<OptimizedDropdown.DropdownItem> queue = this._items.ToQueue<OptimizedDropdown.DropdownItem>();
						while (queue.Count > 0)
						{
							OptimizedDropdown.DropdownItem element = queue.Dequeue();
							this._pool.Release(element);
						}
						this._items.Clear();
						this._dropdown.SetActive(false);
					}
				});
			}
			if (this._blocker != null)
			{
				this.DestroyBlocker(this._blocker);
			}
			this._blocker = null;
			this.Select();
		}

		// Token: 0x04006D59 RID: 27993
		[SerializeField]
		private RectTransform _template;

		// Token: 0x04006D5A RID: 27994
		[SerializeField]
		private Text _captionText;

		// Token: 0x04006D5B RID: 27995
		[SerializeField]
		private Image _captionImage;

		// Token: 0x04006D5C RID: 27996
		[SerializeField]
		[Space]
		private Text _itemText;

		// Token: 0x04006D5D RID: 27997
		[SerializeField]
		private Image _itemImage;

		// Token: 0x04006D5E RID: 27998
		[SerializeField]
		private bool _focusedSelectedItem;

		// Token: 0x04006D5F RID: 27999
		[SerializeField]
		[Space]
		private int _value;

		// Token: 0x04006D60 RID: 28000
		[SerializeField]
		[Space]
		private OptimizedDropdown.OptionDataList _options = new OptimizedDropdown.OptionDataList();

		// Token: 0x04006D61 RID: 28001
		[SerializeField]
		[Space]
		private OptimizedDropdown.DropdownEvent _onValueChanged = new OptimizedDropdown.DropdownEvent();

		// Token: 0x04006D62 RID: 28002
		private ScrollRect _scrollRect;

		// Token: 0x04006D63 RID: 28003
		private GameObject _dropdown;

		// Token: 0x04006D64 RID: 28004
		private GameObject _blocker;

		// Token: 0x04006D65 RID: 28005
		private List<OptimizedDropdown.DropdownItem> _items = new List<OptimizedDropdown.DropdownItem>();

		// Token: 0x04006D66 RID: 28006
		private bool _validTemplate;

		// Token: 0x04006D67 RID: 28007
		private static OptimizedDropdown.OptionData _noOptionData = new OptimizedDropdown.OptionData();

		// Token: 0x04006D68 RID: 28008
		private OptimizedDropdown.DropdownItemPool _pool;

		// Token: 0x04006D69 RID: 28009
		private Rect? _rect;

		// Token: 0x04006D6A RID: 28010
		private IDisposable _fadeSubscriber;

		// Token: 0x02000FCB RID: 4043
		protected internal class DropdownItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, ICancelHandler
		{
			// Token: 0x17001D54 RID: 7508
			// (get) Token: 0x0600867B RID: 34427 RVA: 0x003835C8 File Offset: 0x003819C8
			// (set) Token: 0x0600867C RID: 34428 RVA: 0x003835D0 File Offset: 0x003819D0
			public Text Text
			{
				get
				{
					return this._text;
				}
				set
				{
					this._text = value;
				}
			}

			// Token: 0x17001D55 RID: 7509
			// (get) Token: 0x0600867D RID: 34429 RVA: 0x003835D9 File Offset: 0x003819D9
			// (set) Token: 0x0600867E RID: 34430 RVA: 0x003835E1 File Offset: 0x003819E1
			public Image Image
			{
				get
				{
					return this._image;
				}
				set
				{
					this._image = value;
				}
			}

			// Token: 0x17001D56 RID: 7510
			// (get) Token: 0x0600867F RID: 34431 RVA: 0x003835EA File Offset: 0x003819EA
			// (set) Token: 0x06008680 RID: 34432 RVA: 0x003835F2 File Offset: 0x003819F2
			public RectTransform RectTransform
			{
				get
				{
					return this._rectTransform;
				}
				set
				{
					this._rectTransform = value;
				}
			}

			// Token: 0x17001D57 RID: 7511
			// (get) Token: 0x06008681 RID: 34433 RVA: 0x003835FB File Offset: 0x003819FB
			// (set) Token: 0x06008682 RID: 34434 RVA: 0x00383603 File Offset: 0x00381A03
			public Toggle Toggle
			{
				get
				{
					return this._toggle;
				}
				set
				{
					this._toggle = value;
				}
			}

			// Token: 0x06008683 RID: 34435 RVA: 0x0038360C File Offset: 0x00381A0C
			public virtual void OnPointerEnter(PointerEventData ped)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}

			// Token: 0x06008684 RID: 34436 RVA: 0x00383620 File Offset: 0x00381A20
			public virtual void OnCancel(BaseEventData eventData)
			{
				Dropdown componentInParent = base.GetComponentInParent<Dropdown>();
				bool flag = componentInParent;
				if (flag)
				{
					componentInParent.Hide();
				}
			}

			// Token: 0x04006D6D RID: 28013
			[SerializeField]
			private Text _text;

			// Token: 0x04006D6E RID: 28014
			[SerializeField]
			private Image _image;

			// Token: 0x04006D6F RID: 28015
			[SerializeField]
			private RectTransform _rectTransform;

			// Token: 0x04006D70 RID: 28016
			[SerializeField]
			private Toggle _toggle;
		}

		// Token: 0x02000FCC RID: 4044
		protected internal class DropdownItemPool
		{
			// Token: 0x06008685 RID: 34437 RVA: 0x00383647 File Offset: 0x00381A47
			public DropdownItemPool(Action<OptimizedDropdown.DropdownItem> onGet, Action<OptimizedDropdown.DropdownItem> onReleased, GameObject template)
			{
				this._onGet = onGet;
				this._onReleased = onReleased;
				this._template = template;
			}

			// Token: 0x17001D58 RID: 7512
			// (get) Token: 0x06008686 RID: 34438 RVA: 0x0038366F File Offset: 0x00381A6F
			// (set) Token: 0x06008687 RID: 34439 RVA: 0x00383677 File Offset: 0x00381A77
			public int CountAll { get; private set; }

			// Token: 0x17001D59 RID: 7513
			// (get) Token: 0x06008688 RID: 34440 RVA: 0x00383680 File Offset: 0x00381A80
			public int CountActive
			{
				[CompilerGenerated]
				get
				{
					return this.CountAll - this.CountInactive;
				}
			}

			// Token: 0x17001D5A RID: 7514
			// (get) Token: 0x06008689 RID: 34441 RVA: 0x0038368F File Offset: 0x00381A8F
			public int CountInactive
			{
				[CompilerGenerated]
				get
				{
					return this._stack.Count;
				}
			}

			// Token: 0x0600868A RID: 34442 RVA: 0x0038369C File Offset: 0x00381A9C
			public OptimizedDropdown.DropdownItem Get()
			{
				if (this._template == null)
				{
					return null;
				}
				OptimizedDropdown.DropdownItem dropdownItem;
				if (this._stack.Count == 0)
				{
					dropdownItem = UnityEngine.Object.Instantiate<GameObject>(this._template).GetComponent<OptimizedDropdown.DropdownItem>();
					this.CountAll++;
				}
				else
				{
					dropdownItem = this._stack.Pop();
				}
				if (this._onGet != null)
				{
					this._onGet(dropdownItem);
				}
				return dropdownItem;
			}

			// Token: 0x0600868B RID: 34443 RVA: 0x00383718 File Offset: 0x00381B18
			public void Release(OptimizedDropdown.DropdownItem element)
			{
				if (this._stack.Count <= 0 || object.ReferenceEquals(this._stack.Peek(), element))
				{
				}
				if (this._onReleased != null)
				{
					this._onReleased(element);
				}
				this._stack.Push(element);
			}

			// Token: 0x04006D71 RID: 28017
			private readonly Stack<OptimizedDropdown.DropdownItem> _stack = new Stack<OptimizedDropdown.DropdownItem>();

			// Token: 0x04006D72 RID: 28018
			private readonly Action<OptimizedDropdown.DropdownItem> _onGet;

			// Token: 0x04006D73 RID: 28019
			private readonly Action<OptimizedDropdown.DropdownItem> _onReleased;

			// Token: 0x04006D74 RID: 28020
			private GameObject _template;
		}

		// Token: 0x02000FCD RID: 4045
		[Serializable]
		public class OptionData
		{
			// Token: 0x0600868C RID: 34444 RVA: 0x00383771 File Offset: 0x00381B71
			public OptionData()
			{
			}

			// Token: 0x0600868D RID: 34445 RVA: 0x00383784 File Offset: 0x00381B84
			public OptionData(string text)
			{
				this._text = text;
			}

			// Token: 0x0600868E RID: 34446 RVA: 0x0038379E File Offset: 0x00381B9E
			public OptionData(Sprite sprite)
			{
				this._sprite = sprite;
			}

			// Token: 0x0600868F RID: 34447 RVA: 0x003837B8 File Offset: 0x00381BB8
			public OptionData(string text, Sprite sprite)
			{
				this._text = text;
				this._sprite = sprite;
			}

			// Token: 0x17001D5B RID: 7515
			// (get) Token: 0x06008690 RID: 34448 RVA: 0x003837D9 File Offset: 0x00381BD9
			// (set) Token: 0x06008691 RID: 34449 RVA: 0x003837E1 File Offset: 0x00381BE1
			public string Text
			{
				get
				{
					return this._text;
				}
				set
				{
					this._text = value;
				}
			}

			// Token: 0x17001D5C RID: 7516
			// (get) Token: 0x06008692 RID: 34450 RVA: 0x003837EA File Offset: 0x00381BEA
			// (set) Token: 0x06008693 RID: 34451 RVA: 0x003837F2 File Offset: 0x00381BF2
			public Sprite Sprite
			{
				get
				{
					return this._sprite;
				}
				set
				{
					this._sprite = value;
				}
			}

			// Token: 0x04006D76 RID: 28022
			[SerializeField]
			private string _text = string.Empty;

			// Token: 0x04006D77 RID: 28023
			[SerializeField]
			private Sprite _sprite;
		}

		// Token: 0x02000FCE RID: 4046
		[Serializable]
		public class OptionDataList
		{
			// Token: 0x17001D5D RID: 7517
			// (get) Token: 0x06008695 RID: 34453 RVA: 0x0038380E File Offset: 0x00381C0E
			// (set) Token: 0x06008696 RID: 34454 RVA: 0x00383816 File Offset: 0x00381C16
			public List<OptimizedDropdown.OptionData> Options
			{
				get
				{
					return this._options;
				}
				set
				{
					this._options = value;
				}
			}

			// Token: 0x04006D78 RID: 28024
			[SerializeField]
			private List<OptimizedDropdown.OptionData> _options = new List<OptimizedDropdown.OptionData>();
		}

		// Token: 0x02000FCF RID: 4047
		[Serializable]
		public class DropdownEvent : UnityEvent<int>
		{
		}
	}
}
