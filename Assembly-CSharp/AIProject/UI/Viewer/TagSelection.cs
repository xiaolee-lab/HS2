using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EC2 RID: 3778
	public static class TagSelection
	{
		// Token: 0x06007BCA RID: 31690 RVA: 0x00344646 File Offset: 0x00342A46
		public static IDisposable BindToGroup(this IEnumerable<Toggle> toggles, Action<int> action)
		{
			return TagSelection.BindToGroup(action, toggles.ToArray<Toggle>());
		}

		// Token: 0x06007BCB RID: 31691 RVA: 0x00344654 File Offset: 0x00342A54
		public static IDisposable BindToGroup(Action<int> action, params Toggle[] toggles)
		{
			if (TagSelection.<>f__mg$cache0 == null)
			{
				TagSelection.<>f__mg$cache0 = new Func<Toggle, IObservable<bool>>(UnityUIComponentExtensions.OnValueChangedAsObservable);
			}
			return (from list in toggles.Select(TagSelection.<>f__mg$cache0).CombineLatest<bool>()
			select list.IndexOf(true) into sel
			where sel >= 0
			select sel).Subscribe(delegate(int sel)
			{
				Action<int> action2 = action;
				if (action2 != null)
				{
					action2(sel);
				}
			});
		}

		// Token: 0x06007BCC RID: 31692 RVA: 0x003446EB File Offset: 0x00342AEB
		public static CompositeDisposable BindToEnter(this IEnumerable<Selectable> selectables, bool isExit, Image cursor)
		{
			return TagSelection.BindToEnter(isExit, cursor, selectables.ToArray<Selectable>());
		}

		// Token: 0x06007BCD RID: 31693 RVA: 0x003446FC File Offset: 0x00342AFC
		public static CompositeDisposable BindToEnter(bool isExit, Image cursor, params Selectable[] selectables)
		{
			CompositeDisposable compositeDisposable = new CompositeDisposable();
			for (int i = 0; i < selectables.Length; i++)
			{
				Selectable item = selectables[i];
				(from _ in item.OnPointerEnterAsObservable().TakeUntilDestroy(cursor)
				where item.IsInteractable()
				select _).Subscribe(delegate(PointerEventData _)
				{
					cursor.enabled = true;
					CursorFrame.Set(cursor.rectTransform, item.GetComponent<RectTransform>(), null);
				}).AddTo(compositeDisposable);
				if (isExit)
				{
					item.OnPointerExitAsObservable().TakeUntilDestroy(cursor).Subscribe(delegate(PointerEventData _)
					{
						cursor.enabled = false;
					}).AddTo(compositeDisposable);
				}
			}
			return compositeDisposable;
		}

		// Token: 0x06007BCE RID: 31694 RVA: 0x003447C4 File Offset: 0x00342BC4
		public static CompositeDisposable BindToEnter(this IEnumerable<TagSelection.ICursorTagElement> elements, bool isExit)
		{
			return TagSelection.BindToEnter(isExit, elements.ToArray<TagSelection.ICursorTagElement>());
		}

		// Token: 0x06007BCF RID: 31695 RVA: 0x003447D4 File Offset: 0x00342BD4
		public static CompositeDisposable BindToEnter(bool isExit, params TagSelection.ICursorTagElement[] elements)
		{
			CompositeDisposable compositeDisposable = new CompositeDisposable();
			TagSelection.ICursorTagElement[] elements2 = elements;
			for (int i = 0; i < elements2.Length; i++)
			{
				TagSelection.ICursorTagElement item = elements2[i];
				CursorFrame.Set(item.cursor.rectTransform, item.selectable.GetComponent<RectTransform>(), null);
				(from _ in item.selectable.OnPointerEnterAsObservable().TakeUntilDestroy(item.cursor)
				select item.cursor).Subscribe(delegate(Image cursor)
				{
					cursor.enabled = true;
					if (!isExit)
					{
						foreach (TagSelection.ICursorTagElement cursorTagElement in from x in elements
						where x.cursor != cursor
						select x)
						{
							cursorTagElement.cursor.enabled = false;
						}
					}
				}).AddTo(compositeDisposable);
				if (isExit)
				{
					item.selectable.OnPointerExitAsObservable().TakeUntilDestroy(item.cursor).Subscribe(delegate(PointerEventData _)
					{
						item.cursor.enabled = false;
					}).AddTo(compositeDisposable);
				}
			}
			return compositeDisposable;
		}

		// Token: 0x04006364 RID: 25444
		[CompilerGenerated]
		private static Func<Toggle, IObservable<bool>> <>f__mg$cache0;

		// Token: 0x02000EC3 RID: 3779
		public interface ICursorTagElement
		{
			// Token: 0x1700186B RID: 6251
			// (get) Token: 0x06007BD2 RID: 31698
			Image cursor { get; }

			// Token: 0x1700186C RID: 6252
			// (get) Token: 0x06007BD3 RID: 31699
			Selectable selectable { get; }
		}
	}
}
