using System;
using System.Collections;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AIProject
{
	// Token: 0x02000C33 RID: 3123
	public class CommandPointMarker : UIBehaviour
	{
		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x060060BB RID: 24763 RVA: 0x0028A1D0 File Offset: 0x002885D0
		// (set) Token: 0x060060BC RID: 24764 RVA: 0x0028A1DD File Offset: 0x002885DD
		public bool IsActivePanel
		{
			get
			{
				return this._isActivePanel.Value;
			}
			set
			{
				this._isActivePanel.Value = value;
			}
		}

		// Token: 0x060060BD RID: 24765 RVA: 0x0028A1EB File Offset: 0x002885EB
		protected override void Start()
		{
			this._isActivePanel.Subscribe(delegate(bool isOn)
			{
				this._locoAnimationTarget.gameObject.SetActive(isOn);
			});
			Observable.FromCoroutine(() => this.Locomotion(), false).ToYieldInstruction<Unit>();
		}

		// Token: 0x060060BE RID: 24766 RVA: 0x0028A220 File Offset: 0x00288620
		private IEnumerator Locomotion()
		{
			IObservable<TimeInterval<float>> stream = ObservableEasing.EaseInOutSine(2f, true).FrameTimeInterval(true);
			for (;;)
			{
				IConnectableObservable<TimeInterval<float>> connStream = stream.Publish<TimeInterval<float>>();
				connStream.Connect();
				connStream.Subscribe(delegate(TimeInterval<float> x)
				{
					this._locoAnimationTarget.anchoredPosition = Vector3.Lerp(this._from, this._to, x.Value);
				});
				yield return connStream.ToYieldInstruction<TimeInterval<float>>();
				connStream = stream.Publish<TimeInterval<float>>();
				connStream.Connect();
				connStream.Subscribe(delegate(TimeInterval<float> x)
				{
					this._locoAnimationTarget.anchoredPosition = Vector3.Lerp(this._to, this._from, x.Value);
				});
				yield return connStream.ToYieldInstruction<TimeInterval<float>>();
			}
			yield break;
		}

		// Token: 0x040055CB RID: 21963
		[SerializeField]
		private RectTransform _locoAnimationTarget;

		// Token: 0x040055CC RID: 21964
		[SerializeField]
		private Vector2 _from = Vector3.zero;

		// Token: 0x040055CD RID: 21965
		[SerializeField]
		private Vector2 _to = Vector3.zero;

		// Token: 0x040055CE RID: 21966
		private BoolReactiveProperty _isActivePanel = new BoolReactiveProperty(false);
	}
}
