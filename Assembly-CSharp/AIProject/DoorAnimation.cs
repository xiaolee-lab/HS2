using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BD8 RID: 3032
	[RequireComponent(typeof(DoorPoint))]
	public class DoorAnimation : ActionPointAnimation
	{
		// Token: 0x06005CDE RID: 23774 RVA: 0x00275154 File Offset: 0x00273554
		protected override void OnStart()
		{
			this._animator = DoorAnimData.Table[this._id];
			if (this._animator != null)
			{
				this._animator.runtimeAnimatorController = Singleton<Manager.Resources>.Instance.Animation.GetItemAnimator(this._animatorID);
				string doorDefaultState = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.DoorDefaultState;
				this._animator.Play(doorDefaultState, 0, 0f);
				if ((this._linkedDoorPoint = base.GetComponent<DoorPoint>()) != null)
				{
					this._animator.OnEnableAsObservable().TakeUntilDestroy(this._linkedDoorPoint).TakeUntilDestroy(this).Subscribe(delegate(Unit _)
					{
						if (!Singleton<Manager.Resources>.IsInstance())
						{
							return;
						}
						CommonDefine.ItemAnimGroup itemAnims = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims;
						string text = null;
						DoorPoint.OpenPattern openState = this._linkedDoorPoint.OpenState;
						if (openState != DoorPoint.OpenPattern.Close)
						{
							if (openState != DoorPoint.OpenPattern.OpenRight)
							{
								if (openState == DoorPoint.OpenPattern.OpenLeft)
								{
									text = itemAnims.DoorOpenIdleLeft;
								}
							}
							else
							{
								text = itemAnims.DoorOpenIdleRight;
							}
						}
						else
						{
							text = itemAnims.DoorCloseLoopState;
						}
						if (text.IsNullOrEmpty())
						{
							return;
						}
						this._animator.Play(text, 0, 0f);
					});
				}
			}
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x00275218 File Offset: 0x00273618
		public void Load(PlayState.Info[] states)
		{
			foreach (PlayState.Info item in states)
			{
				this._queue.Enqueue(item);
			}
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x00275254 File Offset: 0x00273654
		public virtual void PlayMoveSE(bool open)
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Camera cameraComponent = Map.GetCameraComponent();
			Transform transform = (cameraComponent != null) ? cameraComponent.transform : null;
			if (transform == null)
			{
				return;
			}
			SoundPack soundPack = Singleton<Manager.Resources>.Instance.SoundPack;
			DoorMatType key;
			if (!DoorAnimData.MatTable.TryGetValue(this._id, out key))
			{
				return;
			}
			SoundPack.DoorSEIDInfo doorSEIDInfo;
			if (!soundPack.DoorIDTable.TryGetValue(key, out doorSEIDInfo))
			{
				return;
			}
			int clipID = (!open) ? doorSEIDInfo.CloseID : doorSEIDInfo.OpenID;
			SoundPack.Data3D data3D;
			if (!soundPack.TryGetActionSEData(clipID, out data3D))
			{
				return;
			}
			Vector3 position = base.transform.position;
			float num = Mathf.Pow(data3D.MaxDistance + soundPack.Game3DInfo.MarginMaxDistance, 2f);
			float sqrMagnitude = (position - transform.position).sqrMagnitude;
			if (num < sqrMagnitude)
			{
				return;
			}
			AudioSource audioSource = soundPack.Play(data3D, Sound.Type.GameSE3D, 0f);
			if (audioSource == null)
			{
				return;
			}
			audioSource.Stop();
			audioSource.transform.position = position;
			audioSource.Play();
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x00275388 File Offset: 0x00273788
		public void PlayAnimation(bool enableFade, float fadeTime, float fadeOutTime, int layer)
		{
			this._animEnumerator = this.StartAnimation(enableFade, fadeTime, fadeOutTime, layer);
			this._animDisposable = Observable.FromCoroutine((CancellationToken _) => this._animEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x002753B8 File Offset: 0x002737B8
		public void StopAnimation()
		{
			if (this._animDisposable != null)
			{
				this._animDisposable.Dispose();
				this._animEnumerator = null;
			}
		}

		// Token: 0x06005CE3 RID: 23779 RVA: 0x002753D8 File Offset: 0x002737D8
		private IEnumerator StartAnimation(bool enableFade, float fadeTime, float fadeOutTime, int layer)
		{
			while (this._queue.Count > 0)
			{
				PlayState.Info state = this._queue.Dequeue();
				if (enableFade)
				{
					base.CrossFadeAnimation(state.stateName, fadeTime, layer, 0f);
					IConnectableObservable<long> waiter = Observable.Timer(TimeSpan.FromSeconds((double)fadeTime)).Publish<long>();
					waiter.Connect();
					yield return waiter.ToYieldInstruction<long>();
				}
				else
				{
					base.PlayAnimation(state.stateName, layer, 0f);
					yield return null;
				}
				yield return null;
				AnimatorStateInfo stateInfo = base.Animator.GetCurrentAnimatorStateInfo(state.layer);
				bool isInTransition = base.Animator.IsInTransition(state.layer);
				while (isInTransition || (stateInfo.IsName(state.stateName) && stateInfo.normalizedTime < fadeOutTime))
				{
					stateInfo = base.Animator.GetCurrentAnimatorStateInfo(state.layer);
					isInTransition = base.Animator.IsInTransition(state.layer);
					yield return null;
				}
				yield return null;
			}
			yield return null;
			this._animEnumerator = null;
			yield break;
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x00275410 File Offset: 0x00273810
		public void PlayCloseAnimation(DoorPoint.OpenPattern pattern)
		{
			this.PlayMoveSE(false);
			this._closeAnimEnumerator = this.StartCloseAnimation(pattern);
			this._closeAnimDisposable = Observable.FromCoroutine((CancellationToken _) => this._closeAnimEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x00275443 File Offset: 0x00273843
		private void StopCloseAnimation(DoorPoint.OpenPattern pattern)
		{
			if (this._closeAnimDisposable != null)
			{
				this._closeAnimDisposable.Dispose();
				this._closeAnimEnumerator = null;
			}
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x00275464 File Offset: 0x00273864
		private IEnumerator StartCloseAnimation(DoorPoint.OpenPattern pattern)
		{
			string stateName = string.Empty;
			if (pattern != DoorPoint.OpenPattern.OpenRight)
			{
				if (pattern == DoorPoint.OpenPattern.OpenLeft)
				{
					stateName = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.DoorCloseLeft;
				}
			}
			else
			{
				stateName = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.DoorCloseRight;
			}
			base.PlayAnimation(stateName, 0, 0f);
			yield return null;
			yield return null;
			AnimatorStateInfo stateInfo = base.Animator.GetCurrentAnimatorStateInfo(0);
			bool isInTransition = base.Animator.IsInTransition(0);
			while (isInTransition || (stateInfo.IsName(stateName) && stateInfo.normalizedTime < 1f))
			{
				stateInfo = base.Animator.GetCurrentAnimatorStateInfo(0);
				isInTransition = base.Animator.IsInTransition(0);
				yield return null;
			}
			yield return null;
			this._closeAnimEnumerator = null;
			yield break;
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06005CE7 RID: 23783 RVA: 0x00275486 File Offset: 0x00273886
		public bool PlayingOpenAnim
		{
			get
			{
				return this._animEnumerator != null;
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06005CE8 RID: 23784 RVA: 0x00275494 File Offset: 0x00273894
		public bool PlayingCloseAnim
		{
			get
			{
				return this._closeAnimEnumerator != null;
			}
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x002754A4 File Offset: 0x002738A4
		public void PlayCloseLoop()
		{
			string doorCloseLoopState = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.DoorCloseLoopState;
			base.Animator.Play(doorCloseLoopState);
		}

		// Token: 0x04005366 RID: 21350
		private Queue<PlayState.Info> _queue = new Queue<PlayState.Info>();

		// Token: 0x04005367 RID: 21351
		[SerializeField]
		protected int _animatorID = 1;

		// Token: 0x04005368 RID: 21352
		private DoorPoint _linkedDoorPoint;

		// Token: 0x04005369 RID: 21353
		private IEnumerator _animEnumerator;

		// Token: 0x0400536A RID: 21354
		private IDisposable _animDisposable;

		// Token: 0x0400536B RID: 21355
		private IEnumerator _closeAnimEnumerator;

		// Token: 0x0400536C RID: 21356
		private IDisposable _closeAnimDisposable;
	}
}
