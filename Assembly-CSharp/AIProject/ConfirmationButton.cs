using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000A32 RID: 2610
	public class ConfirmationButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06004D7E RID: 19838 RVA: 0x001DBF1F File Offset: 0x001DA31F
		// (set) Token: 0x06004D7F RID: 19839 RVA: 0x001DBF2C File Offset: 0x001DA32C
		public bool IsActiveSelectedFrame
		{
			get
			{
				return this._activeSelectedFrame.Value;
			}
			set
			{
				this._activeSelectedFrame.Value = value;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06004D80 RID: 19840 RVA: 0x001DBF3A File Offset: 0x001DA33A
		// (set) Token: 0x06004D81 RID: 19841 RVA: 0x001DBF42 File Offset: 0x001DA342
		public UnityEvent OnPointerDownEvent { get; private set; } = new UnityEvent();

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06004D82 RID: 19842 RVA: 0x001DBF4B File Offset: 0x001DA34B
		// (set) Token: 0x06004D83 RID: 19843 RVA: 0x001DBF53 File Offset: 0x001DA353
		public UnityEvent OnPointerUpEvent { get; private set; } = new UnityEvent();

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06004D84 RID: 19844 RVA: 0x001DBF5C File Offset: 0x001DA35C
		// (set) Token: 0x06004D85 RID: 19845 RVA: 0x001DBF64 File Offset: 0x001DA364
		public UnityEvent OnPointerEnterEvent { get; private set; } = new UnityEvent();

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06004D86 RID: 19846 RVA: 0x001DBF6D File Offset: 0x001DA36D
		// (set) Token: 0x06004D87 RID: 19847 RVA: 0x001DBF75 File Offset: 0x001DA375
		public UnityEvent OnPointerExitEvent { get; private set; } = new UnityEvent();

		// Token: 0x06004D88 RID: 19848 RVA: 0x001DBF80 File Offset: 0x001DA380
		private void SetCoverEnabled(bool active)
		{
			if (this._image == null)
			{
				Color color = this._image.color;
				color.a = (float)((!active) ? 0 : 1);
				this._image.color = color;
			}
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x001DBFCC File Offset: 0x001DA3CC
		public void AddListener(Action act)
		{
			if (this._button.IsInteractable())
			{
				if (this._button != null)
				{
					this._button.onClick.AddListener(delegate()
					{
						Action act2 = act;
						if (act2 != null)
						{
							act2();
						}
					});
				}
			}
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x001DC01F File Offset: 0x001DA41F
		public void Invoke()
		{
			Button.ButtonClickedEvent onClick = this._button.onClick;
			if (onClick != null)
			{
				onClick.Invoke();
			}
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x001DC03A File Offset: 0x001DA43A
		public void DisableRaycast()
		{
			this._image.raycastTarget = false;
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x001DC048 File Offset: 0x001DA448
		private void Reset()
		{
			this._button = base.GetComponent<Button>();
			this._image = base.GetComponent<Image>();
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x001DC062 File Offset: 0x001DA462
		private void Awake()
		{
			this.OnPointerEnterEvent.AddListener(delegate()
			{
				this.SetCoverEnabled(true);
			});
			this.OnPointerExitEvent.AddListener(delegate()
			{
				this.SetCoverEnabled(false);
			});
			this.SetCoverEnabled(false);
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x001DC099 File Offset: 0x001DA499
		private void Start()
		{
			this._activeSelectedFrame.Subscribe(delegate(bool x)
			{
				if (x)
				{
					this.VisibleSelectedFrame();
				}
				else
				{
					this.ImvisibleSelectedFrame();
				}
			});
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x001DC0B3 File Offset: 0x001DA4B3
		private void VisibleSelectedFrame()
		{
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x001DC0B5 File Offset: 0x001DA4B5
		private void ImvisibleSelectedFrame()
		{
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x001DC0B7 File Offset: 0x001DA4B7
		public void OnPointerDown(PointerEventData eventData)
		{
			UnityEvent onPointerDownEvent = this.OnPointerDownEvent;
			if (onPointerDownEvent != null)
			{
				onPointerDownEvent.Invoke();
			}
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x001DC0CD File Offset: 0x001DA4CD
		public void OnPointerEnter(PointerEventData eventData)
		{
			UnityEvent onPointerEnterEvent = this.OnPointerEnterEvent;
			if (onPointerEnterEvent != null)
			{
				onPointerEnterEvent.Invoke();
			}
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x001DC0E3 File Offset: 0x001DA4E3
		public void OnPointerExit(PointerEventData eventData)
		{
			UnityEvent onPointerExitEvent = this.OnPointerExitEvent;
			if (onPointerExitEvent != null)
			{
				onPointerExitEvent.Invoke();
			}
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x001DC0F9 File Offset: 0x001DA4F9
		public void OnPointerUp(PointerEventData eventData)
		{
			UnityEvent onPointerUpEvent = this.OnPointerUpEvent;
			if (onPointerUpEvent != null)
			{
				onPointerUpEvent.Invoke();
			}
		}

		// Token: 0x040046DA RID: 18138
		[SerializeField]
		private Button _button;

		// Token: 0x040046DB RID: 18139
		[SerializeField]
		private Image _image;

		// Token: 0x040046DC RID: 18140
		[SerializeField]
		private Image _selectedFrame;

		// Token: 0x040046DD RID: 18141
		private BoolReactiveProperty _activeSelectedFrame = new BoolReactiveProperty(false);
	}
}
