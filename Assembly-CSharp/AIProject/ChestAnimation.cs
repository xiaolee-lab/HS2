using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BD7 RID: 3031
	[RequireComponent(typeof(ActionPoint))]
	public class ChestAnimation : ActionPointAnimation
	{
		// Token: 0x06005CD2 RID: 23762 RVA: 0x00274AA4 File Offset: 0x00272EA4
		protected override void OnStart()
		{
			this._animator = ChestAnimData.Table[this._id];
			if (this._animator != null)
			{
				int chestAnimatorID = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.ChestAnimatorID;
				this._animator.runtimeAnimatorController = Singleton<Manager.Resources>.Instance.Animation.GetItemAnimator(chestAnimatorID);
				string chestDefaultState = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.ChestDefaultState;
				this._animator.Play(chestDefaultState, 0, 0f);
			}
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x00274B30 File Offset: 0x00272F30
		public void PlayInAnimation()
		{
			this._inQueue.Clear();
			string[] chestInStates = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.ChestInStates;
			foreach (string name_ in chestInStates)
			{
				PlayState.Info item = new PlayState.Info(name_, 0);
				this._inQueue.Enqueue(item);
			}
			this._itemInAnimEnumerator = this.StartInAnimation();
			this._itemInAnimDisposable = Observable.FromCoroutine((CancellationToken _) => this._itemInAnimEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x00274BB6 File Offset: 0x00272FB6
		public void StopInAnimation()
		{
			if (this._itemInAnimDisposable != null)
			{
				this._itemInAnimDisposable.Dispose();
				this._itemInAnimEnumerator = null;
			}
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x00274BD8 File Offset: 0x00272FD8
		private IEnumerator StartInAnimation()
		{
			Queue<PlayState.Info> queue = this._inQueue;
			Animator animator = this._animator;
			while (queue.Count > 0)
			{
				PlayState.Info state = queue.Dequeue();
				animator.Play(state.stateName, state.layer, 0f);
				yield return null;
				yield return null;
				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
				bool isInTransition = animator.IsInTransition(state.layer);
				while (isInTransition || (stateInfo.IsName(state.stateName) && stateInfo.normalizedTime < 1f))
				{
					stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
					isInTransition = animator.IsInTransition(state.layer);
					yield return null;
				}
				yield return null;
			}
			yield return null;
			this._itemInAnimEnumerator = null;
			yield break;
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x00274BF4 File Offset: 0x00272FF4
		public void PlayOutAnimation()
		{
			this._outQueue.Clear();
			string[] chestOutStates = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.ChestOutStates;
			foreach (string name_ in chestOutStates)
			{
				PlayState.Info item = new PlayState.Info(name_, 0);
				this._outQueue.Enqueue(item);
			}
			this._itemOutAnimEnumerator = this.StartOutAnimation();
			this._itemOutAnimDisposable = Observable.FromCoroutine((CancellationToken _) => this._itemOutAnimEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x00274C7A File Offset: 0x0027307A
		public void StopOutAnimation()
		{
			if (this._itemOutAnimDisposable != null)
			{
				this._itemOutAnimDisposable.Dispose();
				this._itemOutAnimEnumerator = null;
			}
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x00274C9C File Offset: 0x0027309C
		private IEnumerator StartOutAnimation()
		{
			Queue<PlayState.Info> queue = this._outQueue;
			Animator animator = this._animator;
			while (queue.Count > 0)
			{
				PlayState.Info state = queue.Dequeue();
				animator.Play(state.stateName, state.layer, 0f);
				yield return null;
				yield return null;
				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
				bool isInTransition = animator.IsInTransition(state.layer);
				while (isInTransition || (stateInfo.IsName(state.stateName) && stateInfo.normalizedTime < 1f))
				{
					stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
					isInTransition = animator.IsInTransition(state.layer);
					yield return null;
				}
				yield return null;
			}
			yield return null;
			this._itemOutAnimEnumerator = null;
			yield break;
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06005CD9 RID: 23769 RVA: 0x00274CB7 File Offset: 0x002730B7
		public bool PlayingInAniamtion
		{
			get
			{
				return this._itemInAnimEnumerator != null;
			}
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06005CDA RID: 23770 RVA: 0x00274CC5 File Offset: 0x002730C5
		public bool PlayingOutAnimation
		{
			get
			{
				return this._itemOutAnimEnumerator != null;
			}
		}

		// Token: 0x04005360 RID: 21344
		private Queue<PlayState.Info> _inQueue = new Queue<PlayState.Info>();

		// Token: 0x04005361 RID: 21345
		private Queue<PlayState.Info> _outQueue = new Queue<PlayState.Info>();

		// Token: 0x04005362 RID: 21346
		private IEnumerator _itemInAnimEnumerator;

		// Token: 0x04005363 RID: 21347
		private IDisposable _itemInAnimDisposable;

		// Token: 0x04005364 RID: 21348
		private IEnumerator _itemOutAnimEnumerator;

		// Token: 0x04005365 RID: 21349
		private IDisposable _itemOutAnimDisposable;
	}
}
