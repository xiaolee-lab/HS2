using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Illusion.Extensions;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Illusion.Game.Extensions
{
	// Token: 0x020007A9 RID: 1961
	public static class SelectableExtensions
	{
		// Token: 0x06002E70 RID: 11888 RVA: 0x00106688 File Offset: 0x00104A88
		public static IObservable<IList<double>> DoubleInterval<T>(this IObservable<T> source, float interval, bool isHot = true)
		{
			return isHot ? SelectableExtensions.CreateDoubleIntervalStream<T>(source, interval).Share<IList<double>>() : SelectableExtensions.CreateDoubleIntervalStream<T>(source, interval);
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x001066A8 File Offset: 0x00104AA8
		public static IConnectableObservable<IList<double>> DoubleIntervalPublish<T>(this IObservable<T> source, float interval)
		{
			return SelectableExtensions.CreateDoubleIntervalStream<T>(source, interval).Publish<IList<double>>();
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x001066B8 File Offset: 0x00104AB8
		private static IObservable<IList<double>> CreateDoubleIntervalStream<T>(IObservable<T> source, float interval)
		{
			return from list in (from t in source.TimeInterval<T>()
			select t.Interval.TotalMilliseconds).Buffer(2, 1)
			where list[0] > (double)interval
			where list[1] <= (double)interval
			select list;
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x00106712 File Offset: 0x00104B12
		public static IDisposable SubscribeToText(this IObservable<string> source, TextMeshProUGUI text)
		{
			return source.SubscribeWithState(text, delegate(string x, TextMeshProUGUI t)
			{
				t.text = x;
			});
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x00106738 File Offset: 0x00104B38
		public static List<Tuple<Selectable, SelectUI>> SelectSEAdd<T>(this T _, params Selectable[] selectables)
		{
			if (SelectableExtensions.<>f__mg$cache0 == null)
			{
				SelectableExtensions.<>f__mg$cache0 = new Func<Selectable, Tuple<Selectable, SelectUI>>(SelectableExtensions.SelectSE);
			}
			return selectables.Select(SelectableExtensions.<>f__mg$cache0).ToList<Tuple<Selectable, SelectUI>>();
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x00106764 File Offset: 0x00104B64
		public static Tuple<Selectable, SelectUI> SelectSE(this Selectable selectable)
		{
			SelectUI selectUI = selectable.GetOrAddComponent<SelectUI>();
			(from sel in selectable.ObserveEveryValueChanged((Selectable _) => selectUI.IsSelect && selectable.IsInteractable(), FrameCountType.Update, false).TakeUntilDestroy(selectable)
			where sel
			select sel).Subscribe(delegate(bool _)
			{
				Utils.Sound.Play(SystemSE.sel);
			});
			return Tuple.Create<Selectable, SelectUI>(selectable, selectUI);
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x0010680C File Offset: 0x00104C0C
		public static void FocusAdd(this Component component, bool isTabKey, Func<bool> isFocus, Func<int> next, Func<Tuple<bool, int>> direct, int firstIndex, params Selectable[] sels)
		{
			if (sels.IsNullOrEmpty<Selectable>())
			{
				return;
			}
			Selectable lastCurrent = sels.SafeGet(firstIndex);
			(from go in component.ObserveEveryValueChanged((Component _) => EventSystem.current.currentSelectedGameObject, FrameCountType.Update, false)
			select (!(go != null)) ? null : go.GetComponent<Selectable>()).Subscribe(delegate(Selectable sel)
			{
				if (sels.Contains(sel))
				{
					lastCurrent = sel;
				}
				else if (isFocus() && lastCurrent != null)
				{
					lastCurrent.Select();
				}
			});
			Action<int, bool> focus = delegate(int index, bool isDirect)
			{
				bool flag = index >= 0;
				if (!isDirect)
				{
					index += sels.Check((Selectable v) => v == lastCurrent);
				}
				MathfEx.LoopValue(ref index, 0, sels.Length - 1);
				if (!sels[index].IsInteractable())
				{
					if (sels.Any((Selectable p) => p.IsInteractable()))
					{
						if (!flag)
						{
							index = Mathf.Min(sels.Length, index + 1);
						}
						IEnumerable<int> enumerable = Enumerable.Range(index, sels.Length - index);
						IEnumerable<int> enumerable2 = Enumerable.Range(0, index);
						index = ((!flag) ? enumerable2.Reverse<int>().Concat(enumerable.Reverse<int>()) : enumerable.Concat(enumerable2)).FirstOrDefault((int i) => sels[i].IsInteractable());
					}
				}
				sels[index].Select();
			};
			if (isTabKey)
			{
				(from _ in component.UpdateAsObservable()
				where isFocus()
				where Input.GetKeyDown(KeyCode.Tab)
				select _).Subscribe(delegate(Unit _)
				{
					focus((!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) ? 1 : -1, false);
				});
			}
			if (!next.IsNullOrEmpty())
			{
				(from _ in component.UpdateAsObservable()
				where isFocus()
				select _).Subscribe(delegate(Unit _)
				{
					int num = next();
					if (num != 0)
					{
						focus(num, false);
					}
				});
			}
			if (!direct.IsNullOrEmpty())
			{
				(from _ in component.UpdateAsObservable()
				where isFocus()
				select _).Subscribe(delegate(Unit _)
				{
					Tuple<bool, int> tuple = direct();
					if (tuple.Item1)
					{
						focus(tuple.Item2, true);
					}
				});
			}
		}

		// Token: 0x04002D44 RID: 11588
		[CompilerGenerated]
		private static Func<Selectable, Tuple<Selectable, SelectUI>> <>f__mg$cache0;
	}
}
