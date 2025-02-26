using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C0D RID: 3085
	public class HarborDoorAnimData : MonoBehaviour
	{
		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06005F37 RID: 24375 RVA: 0x002764E3 File Offset: 0x002748E3
		public int LinkID
		{
			[CompilerGenerated]
			get
			{
				return this._linkID;
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06005F38 RID: 24376 RVA: 0x002764EB File Offset: 0x002748EB
		public Animator DoorAnimator
		{
			[CompilerGenerated]
			get
			{
				return this._doorAnimator;
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06005F39 RID: 24377 RVA: 0x002764F3 File Offset: 0x002748F3
		public int AnimatorID
		{
			[CompilerGenerated]
			get
			{
				return this._animatorID;
			}
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06005F3A RID: 24378 RVA: 0x002764FB File Offset: 0x002748FB
		private string OpenIdleStateName
		{
			[CompilerGenerated]
			get
			{
				return this._openIdleStateName;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06005F3B RID: 24379 RVA: 0x00276503 File Offset: 0x00274903
		public string CloseIdleStateName
		{
			[CompilerGenerated]
			get
			{
				return this._closeIdleStateName;
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06005F3C RID: 24380 RVA: 0x0027650B File Offset: 0x0027490B
		public string ToOpenStateName
		{
			[CompilerGenerated]
			get
			{
				return this._toOpenStateName;
			}
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06005F3D RID: 24381 RVA: 0x00276513 File Offset: 0x00274913
		public string ToCloseStateName
		{
			[CompilerGenerated]
			get
			{
				return this._toCloseStateName;
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06005F3E RID: 24382 RVA: 0x0027651B File Offset: 0x0027491B
		public bool ActiveAnimator
		{
			[CompilerGenerated]
			get
			{
				return this._doorAnimator != null && this._doorAnimator.isActiveAndEnabled && this._doorAnimator.runtimeAnimatorController != null;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06005F3F RID: 24383 RVA: 0x00276552 File Offset: 0x00274952
		public bool PlayingAnimation
		{
			[CompilerGenerated]
			get
			{
				return this._animationDisposable != null;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06005F40 RID: 24384 RVA: 0x00276560 File Offset: 0x00274960
		// (set) Token: 0x06005F41 RID: 24385 RVA: 0x00276568 File Offset: 0x00274968
		public Action AnimEndAction { get; set; }

		// Token: 0x06005F42 RID: 24386 RVA: 0x00276574 File Offset: 0x00274974
		protected virtual void Awake()
		{
			if (HarborDoorAnimData.Table == null)
			{
				HarborDoorAnimData.Table = new ReadOnlyDictionary<int, HarborDoorAnimData>(HarborDoorAnimData._table);
			}
			if (this._doorAnimator == null)
			{
				this._doorAnimator = base.GetComponent<Animator>();
			}
			if (this._doorAnimator == null)
			{
				return;
			}
			this._doorAnimator.runtimeAnimatorController = this.GetAnimatorController();
			HarborDoorAnimData._table[this._linkID] = this;
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x002765EB File Offset: 0x002749EB
		private void OnDestroy()
		{
			if (HarborDoorAnimData._table.ContainsKey(this._linkID))
			{
				HarborDoorAnimData._table.Remove(this._linkID);
			}
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x00276613 File Offset: 0x00274A13
		public RuntimeAnimatorController GetAnimatorController()
		{
			RuntimeAnimatorController result;
			if (Singleton<Manager.Resources>.IsInstance())
			{
				Manager.Resources.AnimationTables animation = Singleton<Manager.Resources>.Instance.Animation;
				result = ((animation != null) ? animation.GetItemAnimator(this._animatorID) : null);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x00276644 File Offset: 0x00274A44
		public void SetState(string stateName)
		{
			this._queue.Clear();
			if (!stateName.IsNullOrEmpty())
			{
				this._queue.Enqueue(stateName);
			}
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x00276668 File Offset: 0x00274A68
		public void SetState(string[] stateNames)
		{
			this._queue.Clear();
			if (!stateNames.IsNullOrEmpty<string>())
			{
				foreach (string text in stateNames)
				{
					if (!text.IsNullOrEmpty())
					{
						this._queue.Enqueue(text);
					}
				}
			}
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x002766BC File Offset: 0x00274ABC
		public void SetState(List<string> stateNames)
		{
			this._queue.Clear();
			if (!stateNames.IsNullOrEmpty<string>())
			{
				foreach (string text in stateNames)
				{
					if (!text.IsNullOrEmpty())
					{
						this._queue.Enqueue(text);
					}
				}
			}
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x0027673C File Offset: 0x00274B3C
		public void PlayOpenIdleAnimation(bool enableFade, float fadeTime, float fadeOutTime, int layer)
		{
			this.SetState(this._openIdleStateName);
			this.PlayAnimation(enableFade, fadeTime, fadeOutTime, layer);
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x00276755 File Offset: 0x00274B55
		public void PlayCloseIdleAnimation(bool enableFade, float fadeTime, float fadeOutTime, int layer)
		{
			this.SetState(this._closeIdleStateName);
			this.PlayAnimation(enableFade, fadeTime, fadeOutTime, layer);
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x0027676E File Offset: 0x00274B6E
		public void PlayToOpenAnimation(bool enableFade, float fadeTime, float fadeOutTime, int layer)
		{
			this.SetState(this._toOpenStateName);
			this.PlayAnimation(enableFade, fadeTime, fadeOutTime, layer);
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x00276787 File Offset: 0x00274B87
		public void PlayToCloseAnimation(bool enableFade, float fadeTime, float fadeOutTime, int layer)
		{
			this.SetState(this._toCloseStateName);
			this.PlayAnimation(enableFade, fadeTime, fadeOutTime, layer);
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x002767A0 File Offset: 0x00274BA0
		public void PlayAnimation(bool enableFade, float fadeTime, float fadeOutTime, int layer)
		{
			if (this._queue.IsNullOrEmpty<string>())
			{
				return;
			}
			this.StopAnimation();
			IEnumerator coroutine = this.PlayAnimCoroutine(enableFade, fadeTime, fadeOutTime, layer);
			this._animationDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x06005F4D RID: 24397 RVA: 0x002767F8 File Offset: 0x00274BF8
		public void StopAnimation()
		{
			if (this._animationDisposable != null)
			{
				this._animationDisposable.Dispose();
				this._animationDisposable = null;
			}
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x00276818 File Offset: 0x00274C18
		private IEnumerator PlayAnimCoroutine(bool enableFade, float fadeTime, float fadeOutTime, int layer)
		{
			while (0 < this._queue.Count)
			{
				string stateName = this._queue.Dequeue();
				if (enableFade)
				{
					this.CrossFadeAnimation(stateName, fadeTime, layer, 0f);
					IConnectableObservable<long> waiter = Observable.Timer(TimeSpan.FromSeconds((double)fadeTime)).Publish<long>();
					waiter.Connect();
					yield return waiter.ToYieldInstruction<long>();
				}
				else
				{
					this.PlayAnimation(stateName, layer, 0f);
					yield return null;
				}
				yield return null;
				AnimatorStateInfo stateInfo = this.DoorAnimator.GetCurrentAnimatorStateInfo(layer);
				bool isInTransition = this.DoorAnimator.IsInTransition(layer);
				while (isInTransition || (stateInfo.IsName(stateName) && stateInfo.normalizedTime < fadeOutTime))
				{
					stateInfo = this.DoorAnimator.GetCurrentAnimatorStateInfo(layer);
					isInTransition = this.DoorAnimator.IsInTransition(layer);
					yield return null;
				}
				yield return null;
			}
			yield return null;
			this._animationDisposable = null;
			if (this.AnimEndAction != null)
			{
				Action animEndAction = this.AnimEndAction;
				this.AnimEndAction = null;
				animEndAction();
			}
			yield break;
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x00276850 File Offset: 0x00274C50
		private void PlayAnimation(string stateName, int layer, float normalizedTime)
		{
			if (global::Debug.isDebugBuild)
			{
			}
			this.DoorAnimator.Play(stateName, layer, normalizedTime);
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x0027686A File Offset: 0x00274C6A
		private void CrossFadeAnimation(string stateName, float fadeTime, int layer, float fixedTimeOffset)
		{
			if (global::Debug.isDebugBuild)
			{
			}
			this.DoorAnimator.CrossFadeInFixedTime(stateName, fadeTime, layer, fixedTimeOffset);
		}

		// Token: 0x040054A2 RID: 21666
		private static Dictionary<int, HarborDoorAnimData> _table = new Dictionary<int, HarborDoorAnimData>();

		// Token: 0x040054A3 RID: 21667
		public static ReadOnlyDictionary<int, HarborDoorAnimData> Table = null;

		// Token: 0x040054A4 RID: 21668
		[SerializeField]
		private int _linkID;

		// Token: 0x040054A5 RID: 21669
		[SerializeField]
		private Animator _doorAnimator;

		// Token: 0x040054A6 RID: 21670
		[SerializeField]
		private int _animatorID;

		// Token: 0x040054A7 RID: 21671
		[SerializeField]
		private string _openIdleStateName = string.Empty;

		// Token: 0x040054A8 RID: 21672
		[SerializeField]
		private string _closeIdleStateName = string.Empty;

		// Token: 0x040054A9 RID: 21673
		[SerializeField]
		private string _toOpenStateName = string.Empty;

		// Token: 0x040054AA RID: 21674
		[SerializeField]
		private string _toCloseStateName = string.Empty;

		// Token: 0x040054AB RID: 21675
		private Queue<string> _queue = new Queue<string>();

		// Token: 0x040054AC RID: 21676
		private IDisposable _animationDisposable;
	}
}
