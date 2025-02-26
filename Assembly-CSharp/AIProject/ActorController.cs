using System;
using System.Runtime.CompilerServices;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C79 RID: 3193
	public abstract class ActorController : MonoBehaviour
	{
		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x0600689E RID: 26782 RVA: 0x002C94DD File Offset: 0x002C78DD
		public IState State
		{
			[CompilerGenerated]
			get
			{
				return this._state;
			}
		}

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x0600689F RID: 26783 RVA: 0x002C94E5 File Offset: 0x002C78E5
		public Actor Actor
		{
			[CompilerGenerated]
			get
			{
				return this._actor;
			}
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x060068A0 RID: 26784 RVA: 0x002C94ED File Offset: 0x002C78ED
		// (set) Token: 0x060068A1 RID: 26785 RVA: 0x002C9511 File Offset: 0x002C7911
		public bool FaceLightActive
		{
			get
			{
				return this.FaceLight != null && this.FaceLight.enabled;
			}
			set
			{
				if (this.FaceLight != null && this.FaceLight.enabled != value)
				{
					this.FaceLight.enabled = value;
				}
			}
		}

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x060068A2 RID: 26786 RVA: 0x002C9541 File Offset: 0x002C7941
		// (set) Token: 0x060068A3 RID: 26787 RVA: 0x002C9549 File Offset: 0x002C7949
		public Light FaceLight { get; protected set; }

		// Token: 0x060068A4 RID: 26788
		public abstract void StartBehavior();

		// Token: 0x060068A5 RID: 26789 RVA: 0x002C9552 File Offset: 0x002C7952
		protected virtual void Start()
		{
			(from _ in Observable.EveryFixedUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnFixedUpdate();
			});
		}

		// Token: 0x060068A6 RID: 26790 RVA: 0x002C9587 File Offset: 0x002C7987
		private void OnFixedUpdate()
		{
			if (Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				return;
			}
			this.SubFixedUpdate();
		}

		// Token: 0x060068A7 RID: 26791
		protected abstract void SubFixedUpdate();

		// Token: 0x060068A8 RID: 26792 RVA: 0x002C959F File Offset: 0x002C799F
		public void OnAnimatorStateEnter(AnimatorStateInfo info)
		{
			if (this._state != null)
			{
				this._state.OnAnimatorStateEnter(this, info);
			}
		}

		// Token: 0x060068A9 RID: 26793 RVA: 0x002C95BB File Offset: 0x002C79BB
		public void OnAnimatorStateExit(AnimatorStateInfo info)
		{
			if (this._state != null)
			{
				this._state.OnAnimatorStateExit(this, info);
			}
		}

		// Token: 0x060068AA RID: 26794
		public abstract void ChangeState(string stateName);

		// Token: 0x060068AB RID: 26795 RVA: 0x002C95D8 File Offset: 0x002C79D8
		public void InitializeFaceLight(GameObject root)
		{
			if (this.FaceLight != null)
			{
				UnityEngine.Object.Destroy(this.FaceLight.gameObject);
			}
			if (!Singleton<Manager.Resources>.IsInstance() || root == null)
			{
				return;
			}
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			string fadeLightParentName = locomotionProfile.FadeLightParentName;
			if (fadeLightParentName.IsNullOrEmpty())
			{
				return;
			}
			GameObject gameObject = root.transform.FindLoop(fadeLightParentName);
			if (gameObject == null)
			{
				return;
			}
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string actorPrefab = definePack.ABPaths.ActorPrefab;
			GameObject gameObject2 = AssetUtility.LoadAsset<GameObject>(actorPrefab, "FaceLight", definePack.ABManifests.Default);
			if (gameObject2 == null)
			{
				return;
			}
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject2, gameObject.transform, false);
			if (gameObject3 == null)
			{
				return;
			}
			this.FaceLight = gameObject3.GetComponent<Light>();
			if (this.FaceLight == null)
			{
				UnityEngine.Object.Destroy(gameObject3);
				return;
			}
			this.FaceLight.transform.localRotation = Quaternion.identity;
			this.FaceLight.transform.localPosition = locomotionProfile.FaceLightOffset;
			if (this.FaceLight.enabled)
			{
				this.FaceLight.enabled = false;
			}
		}

		// Token: 0x0400595C RID: 22876
		protected IState _state;

		// Token: 0x0400595D RID: 22877
		[SerializeField]
		protected Actor _actor;
	}
}
