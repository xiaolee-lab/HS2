using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Scene;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EC0 RID: 3776
	public class CountViewer : MonoBehaviour
	{
		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x06007BB3 RID: 31667 RVA: 0x00344011 File Offset: 0x00342411
		public IObservable<int> Counter
		{
			[CompilerGenerated]
			get
			{
				return this._count;
			}
		}

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x06007BB4 RID: 31668 RVA: 0x00344019 File Offset: 0x00342419
		// (set) Token: 0x06007BB5 RID: 31669 RVA: 0x00344026 File Offset: 0x00342426
		public int Count
		{
			get
			{
				return this._count.Value;
			}
			set
			{
				this._count.Value = value;
			}
		}

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x06007BB6 RID: 31670 RVA: 0x00344034 File Offset: 0x00342434
		// (set) Token: 0x06007BB7 RID: 31671 RVA: 0x0034403C File Offset: 0x0034243C
		public int MaxCount { get; set; }

		// Token: 0x1700186A RID: 6250
		// (set) Token: 0x06007BB8 RID: 31672 RVA: 0x00344045 File Offset: 0x00342445
		public int ForceCount
		{
			set
			{
				this._count.SetValueAndForceNotify(value);
			}
		}

		// Token: 0x06007BB9 RID: 31673 RVA: 0x00344054 File Offset: 0x00342454
		public static IEnumerator Load(Transform viewerParent, Action<CountViewer> onComplete)
		{
			while (!Singleton<Manager.Resources>.IsInstance())
			{
				yield return null;
			}
			GameObject asset = CommonLib.LoadAsset<GameObject>(Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab, "CountViewer", false, string.Empty);
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab, string.Empty));
			}
			onComplete.Call(UnityEngine.Object.Instantiate<GameObject>(asset, viewerParent, false).GetComponent<CountViewer>());
			yield break;
		}

		// Token: 0x06007BBA RID: 31674 RVA: 0x00344078 File Offset: 0x00342478
		private void Start()
		{
			ReadOnlyReactiveProperty<bool> source = (from i in this._count
			select i > 1).ToReadOnlyReactiveProperty<bool>();
			ReadOnlyReactiveProperty<bool> source2 = (from i in this._count
			select i < this.MaxCount).ToReadOnlyReactiveProperty<bool>();
			List<IObservable<int>> list = new List<IObservable<int>>();
			using (var enumerator = (from bt in this._addButtons.Select((Button bt, int index) => new
			{
				bt = bt,
				add = ((index != 0) ? (index * 10) : 1)
			})
			where bt != null
			select bt).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					<>__AnonType27<Button, int> item = enumerator.Current;
					source2.SubscribeToInteractable(item.bt);
					list.Add(from _ in item.bt.OnClickAsObservable()
					select item.add);
				}
			}
			using (var enumerator2 = (from bt in this._subButtons.Select((Button bt, int index) => new
			{
				bt = bt,
				add = ((index != 0) ? (index * 10) : 1)
			})
			where bt != null
			select bt).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					<>__AnonType27<Button, int> item = enumerator2.Current;
					source.SubscribeToInteractable(item.bt);
					list.Add(from _ in item.bt.OnClickAsObservable()
					select -item.add);
				}
			}
			if (list.Any<IObservable<int>>())
			{
				(from value in list.Merge<int>()
				select Mathf.Clamp(this._count.Value + value, 1, this.MaxCount)).Subscribe(delegate(int value)
				{
					this._count.Value = value;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				});
			}
			if (this._inputCounter != null)
			{
				this._count.Subscribe(delegate(int i)
				{
					this._inputCounter.text = i.ToString();
				});
				this._inputCounter.OnSelectAsObservable().Take(1).Subscribe(delegate(BaseEventData _)
				{
					Transform transform = this._inputCounter.transform.Find(this._inputCounter.name + " Input Caret");
					if (transform != null)
					{
						RectTransform component = transform.GetComponent<RectTransform>();
						RectTransform rectTransform = this._countText.rectTransform;
						component.anchoredPosition = rectTransform.anchoredPosition;
						component.sizeDelta = rectTransform.sizeDelta;
					}
				});
				this._inputCounter.OnEndEditAsObservable().Select(delegate(string text)
				{
					int value;
					int.TryParse(text, out value);
					return Mathf.Clamp(value, 1, this.MaxCount);
				}).Subscribe(delegate(int n)
				{
					this._count.SetValueAndForceNotify(n);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				});
			}
		}

		// Token: 0x0400635A RID: 25434
		[SerializeField]
		private Text _countText;

		// Token: 0x0400635B RID: 25435
		[SerializeField]
		private InputField _inputCounter;

		// Token: 0x0400635C RID: 25436
		[SerializeField]
		[Header("Addition")]
		private Button[] _addButtons = new Button[0];

		// Token: 0x0400635D RID: 25437
		[SerializeField]
		[Header("Subtract")]
		private Button[] _subButtons = new Button[0];

		// Token: 0x0400635E RID: 25438
		private IntReactiveProperty _count = new IntReactiveProperty(1);
	}
}
