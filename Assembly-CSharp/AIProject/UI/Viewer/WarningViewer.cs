using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using AIProject.Scene;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000E71 RID: 3697
	public class WarningViewer : MonoBehaviour
	{
		// Token: 0x060075A4 RID: 30116 RVA: 0x0031DA08 File Offset: 0x0031BE08
		public static IEnumerator Load(Transform viewerParent, Action<WarningViewer> onComplete)
		{
			while (!Singleton<Manager.Resources>.IsInstance())
			{
				yield return null;
			}
			string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
			GameObject asset = CommonLib.LoadAsset<GameObject>(bundle, "WarningViewer", false, string.Empty);
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
			}
			onComplete.Call(UnityEngine.Object.Instantiate<GameObject>(asset, viewerParent, false).GetComponent<WarningViewer>());
			yield break;
		}

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x060075A5 RID: 30117 RVA: 0x0031DA2A File Offset: 0x0031BE2A
		// (set) Token: 0x060075A6 RID: 30118 RVA: 0x0031DA32 File Offset: 0x0031BE32
		public bool initialized { get; private set; }

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x060075A7 RID: 30119 RVA: 0x0031DA3B File Offset: 0x0031BE3B
		// (set) Token: 0x060075A8 RID: 30120 RVA: 0x0031DA48 File Offset: 0x0031BE48
		public int msgID
		{
			get
			{
				return this._msgID.Value;
			}
			set
			{
				this._msgID.Value = value;
			}
		}

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x060075A9 RID: 30121 RVA: 0x0031DA56 File Offset: 0x0031BE56
		// (set) Token: 0x060075AA RID: 30122 RVA: 0x0031DA63 File Offset: 0x0031BE63
		public bool visible
		{
			get
			{
				return this._visible.Value;
			}
			set
			{
				this._visible.Value = value;
			}
		}

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x060075AB RID: 30123 RVA: 0x0031DA71 File Offset: 0x0031BE71
		// (set) Token: 0x060075AC RID: 30124 RVA: 0x0031DA7E File Offset: 0x0031BE7E
		public int langage
		{
			get
			{
				return this._language.Value;
			}
			set
			{
				this._language.Value = value;
			}
		}

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x060075AD RID: 30125 RVA: 0x0031DA8C File Offset: 0x0031BE8C
		// (set) Token: 0x060075AE RID: 30126 RVA: 0x0031DA99 File Offset: 0x0031BE99
		public string message
		{
			get
			{
				return this._message.Value;
			}
			set
			{
				this._message.Value = value;
			}
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x060075AF RID: 30127 RVA: 0x0031DAA7 File Offset: 0x0031BEA7
		private IntReactiveProperty _msgID { get; } = new IntReactiveProperty(-1);

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x060075B0 RID: 30128 RVA: 0x0031DAAF File Offset: 0x0031BEAF
		private BoolReactiveProperty _visible { get; } = new BoolReactiveProperty(false);

		// Token: 0x060075B1 RID: 30129 RVA: 0x0031DAB8 File Offset: 0x0031BEB8
		private IEnumerator Start()
		{
			(from s in this._message
			where !s.IsNullOrEmpty()
			select s).SubscribeToText(this._text);
			this._visible.Subscribe(delegate(bool isOn)
			{
				float startAlpha = this._canvasGroup.alpha;
				IObservable<<>__AnonType19<int, float>> fadeStream = (from x in ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true)
				select new
				{
					targetAlpha = ((!isOn) ? 0 : 1),
					Value = x.Value
				}).Do(delegate(x)
				{
					this._canvasGroup.alpha = Mathf.Lerp(startAlpha, (float)x.targetAlpha, x.Value);
				});
				if (this.fadeDisposable != null)
				{
					this.fadeDisposable.Dispose();
				}
				this.fadeDisposable = Observable.FromCoroutine((CancellationToken _) => fadeStream.ToYieldInstruction(), false).Subscribe<Unit>();
			});
			while (!Singleton<Manager.Resources>.IsInstance() || !Singleton<Manager.Resources>.Instance.PopupInfo.WarningTable.Any<KeyValuePair<int, string[]>>())
			{
				yield return null;
			}
			ReadOnlyDictionary<int, string[]> warningTable = Singleton<Manager.Resources>.Instance.PopupInfo.WarningTable;
			string[] messageTable = null;
			Func<string> convertLanguageMessage = delegate()
			{
				string[] messageTable = messageTable;
				return (messageTable != null) ? messageTable.GetElement(this._language.Value) : null;
			};
			this._language.Subscribe(delegate(int lang)
			{
				this._message.Value = convertLanguageMessage();
			});
			this._msgID.Select(delegate(int id)
			{
				warningTable.TryGetValue(id, out messageTable);
				return convertLanguageMessage();
			}).Subscribe(delegate(string text)
			{
				this._message.Value = text;
			});
			this.initialized = true;
			yield break;
		}

		// Token: 0x060075B2 RID: 30130 RVA: 0x0031DAD3 File Offset: 0x0031BED3
		private void OnDestroy()
		{
			if (this.fadeDisposable != null)
			{
				this.fadeDisposable.Dispose();
			}
			this.fadeDisposable = null;
		}

		// Token: 0x04005FE5 RID: 24549
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005FE6 RID: 24550
		[SerializeField]
		private Text _text;

		// Token: 0x04005FE7 RID: 24551
		[SerializeField]
		private IntReactiveProperty _language = new IntReactiveProperty(0);

		// Token: 0x04005FE8 RID: 24552
		[Header("not used msgID")]
		[SerializeField]
		private StringReactiveProperty _message = new StringReactiveProperty();

		// Token: 0x04005FEB RID: 24555
		private IDisposable fadeDisposable;
	}
}
