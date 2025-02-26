using System;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using ReMotion;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000E9A RID: 3738
	public class ListOptionButton : FromToLocomotion2D, IUIFader
	{
		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x06007893 RID: 30867 RVA: 0x0032CFEE File Offset: 0x0032B3EE
		public Image Image
		{
			[CompilerGenerated]
			get
			{
				return this._image;
			}
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x06007894 RID: 30868 RVA: 0x0032CFF6 File Offset: 0x0032B3F6
		public StringReactiveProperty Text
		{
			[CompilerGenerated]
			get
			{
				return this._text;
			}
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x06007895 RID: 30869 RVA: 0x0032CFFE File Offset: 0x0032B3FE
		// (set) Token: 0x06007896 RID: 30870 RVA: 0x0032D00B File Offset: 0x0032B40B
		public Button.ButtonClickedEvent OnClicked
		{
			get
			{
				return this._button.onClick;
			}
			set
			{
				this._button.onClick = value;
			}
		}

		// Token: 0x06007897 RID: 30871 RVA: 0x0032D019 File Offset: 0x0032B419
		private void Start()
		{
			this._canvasGroup.alpha = 0f;
			(from x in this._text
			where this._label.text != x
			select x).Subscribe(delegate(string x)
			{
				this._label.text = x;
			});
		}

		// Token: 0x06007898 RID: 30872 RVA: 0x0032D054 File Offset: 0x0032B454
		public IObservable<TimeInterval<float>[]> Open()
		{
			Vector2 diff = this._source - this._destination;
			IConnectableObservable<TimeInterval<float>> connectableObservable = ObservableEasing.Create(Tween.MotionFunctionTable[this._motionTypes.@in], 0.3f, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			IConnectableObservable<TimeInterval<float>> connectableObservable2 = ObservableEasing.Create(Tween.MotionFunctionTable[this._alphaFadingTypes.@in], 0.3f, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			connectableObservable.Connect();
			connectableObservable2.Connect();
			connectableObservable.Subscribe(delegate(TimeInterval<float> x)
			{
				this.SetPosition(diff, 1f - x.Value);
			});
			connectableObservable2.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = x.Value;
			});
			return Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				connectableObservable,
				connectableObservable2
			});
		}

		// Token: 0x06007899 RID: 30873 RVA: 0x0032D124 File Offset: 0x0032B524
		public IObservable<TimeInterval<float>[]> Close()
		{
			Vector2 diff = this._source - this._destination;
			IConnectableObservable<TimeInterval<float>> connectableObservable = ObservableEasing.Create(Tween.MotionFunctionTable[this._motionTypes.@out], 0.3f, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			IConnectableObservable<TimeInterval<float>> connectableObservable2 = ObservableEasing.Create(Tween.MotionFunctionTable[this._alphaFadingTypes.@out], 0.3f, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			connectableObservable.Connect();
			connectableObservable2.Connect();
			connectableObservable.Subscribe(delegate(TimeInterval<float> x)
			{
				this.SetPosition(diff, x.Value);
			});
			connectableObservable2.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = 1f - x.Value;
			});
			return Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				connectableObservable,
				connectableObservable2
			});
		}

		// Token: 0x04006187 RID: 24967
		[SerializeField]
		private TextMeshProUGUI _label;

		// Token: 0x04006188 RID: 24968
		[SerializeField]
		private Image _image;

		// Token: 0x04006189 RID: 24969
		[SerializeField]
		private Button _button;

		// Token: 0x0400618A RID: 24970
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x0400618B RID: 24971
		[SerializeField]
		private StringReactiveProperty _text = new StringReactiveProperty(string.Empty);
	}
}
