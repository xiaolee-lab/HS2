using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using AIProject.Player;
using AIProject.SaveData;
using IllusionUtility.GetUtility;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000BF6 RID: 3062
	public class DevicePoint : Point, ICommandable
	{
		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06005DD5 RID: 24021 RVA: 0x0027A619 File Offset: 0x00278A19
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this._id;
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06005DD6 RID: 24022 RVA: 0x0027A621 File Offset: 0x00278A21
		public Transform PivotPoint
		{
			[CompilerGenerated]
			get
			{
				return this._pivotPoint;
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x0027A629 File Offset: 0x00278A29
		public List<Transform> RecoverPoints
		{
			[CompilerGenerated]
			get
			{
				return this._recoverPoints;
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06005DD8 RID: 24024 RVA: 0x0027A631 File Offset: 0x00278A31
		public Transform PlayerRecoverPoint
		{
			[CompilerGenerated]
			get
			{
				return this._playerRecoverPoint;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06005DD9 RID: 24025 RVA: 0x0027A639 File Offset: 0x00278A39
		public int InstanceID
		{
			get
			{
				if (this._hashCode == null)
				{
					this._hashCode = new int?(base.GetInstanceID());
				}
				return this._hashCode.Value;
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06005DDA RID: 24026 RVA: 0x0027A667 File Offset: 0x00278A67
		// (set) Token: 0x06005DDB RID: 24027 RVA: 0x0027A66F File Offset: 0x00278A6F
		public bool IsImpossible { get; private set; }

		// Token: 0x06005DDC RID: 24028 RVA: 0x0027A678 File Offset: 0x00278A78
		public bool SetImpossible(bool value, Actor actor)
		{
			return true;
		}

		// Token: 0x06005DDD RID: 24029 RVA: 0x0027A67C File Offset: 0x00278A7C
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (this.TutorialHideMode())
			{
				return false;
			}
			if (distance > radiusA)
			{
				return false;
			}
			Vector3 position = this.Position;
			position.y = 0f;
			float num = angle / 2f;
			float num2 = Vector3.Angle(position - basePosition, forward);
			return num2 <= num;
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x0027A6D4 File Offset: 0x00278AD4
		public bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB)
		{
			if (this._pathForCalc == null)
			{
				this._pathForCalc = new NavMeshPath();
			}
			bool flag = true;
			if (nmAgent.isActiveAndEnabled)
			{
				nmAgent.CalculatePath(this.Position, this._pathForCalc);
				flag &= (this._pathForCalc.status == NavMeshPathStatus.PathComplete);
				float num = 0f;
				Vector3[] corners = this._pathForCalc.corners;
				for (int i = 0; i < corners.Length - 1; i++)
				{
					float num2 = Vector3.Distance(corners[i], corners[i + 1]);
					num += num2;
					float num3 = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
					if (num > num3)
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06005DDF RID: 24031 RVA: 0x0027A7A4 File Offset: 0x00278BA4
		public bool IsNeutralCommand
		{
			get
			{
				AgentData agentData;
				return !this.TutorialHideMode() && Singleton<Game>.Instance.WorldData.AgentTable.TryGetValue(this._id, out agentData) && agentData.OpenState;
			}
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x0027A7E7 File Offset: 0x00278BE7
		public bool TutorialHideMode()
		{
			return Map.TutorialMode;
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06005DE1 RID: 24033 RVA: 0x0027A7F6 File Offset: 0x00278BF6
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06005DE2 RID: 24034 RVA: 0x0027A803 File Offset: 0x00278C03
		public Vector3 CommandCenter
		{
			get
			{
				if (this._commandBasePoint != null)
				{
					return this._commandBasePoint.position;
				}
				return base.transform.position;
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06005DE3 RID: 24035 RVA: 0x0027A830 File Offset: 0x00278C30
		public CommandLabel.CommandInfo[] Labels
		{
			get
			{
				PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
				if (playerActor != null && playerActor.PlayerController.State is Onbu)
				{
					return null;
				}
				return this._labels;
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06005DE4 RID: 24036 RVA: 0x0027A881 File Offset: 0x00278C81
		// (set) Token: 0x06005DE5 RID: 24037 RVA: 0x0027A889 File Offset: 0x00278C89
		public CommandLabel.CommandInfo[] DateLabels { get; private set; }

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06005DE6 RID: 24038 RVA: 0x0027A892 File Offset: 0x00278C92
		public ObjectLayer Layer { get; } = 2;

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06005DE7 RID: 24039 RVA: 0x0027A89A File Offset: 0x00278C9A
		public CommandType CommandType { get; }

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06005DE8 RID: 24040 RVA: 0x0027A8A2 File Offset: 0x00278CA2
		public Animator Animator
		{
			[CompilerGenerated]
			get
			{
				return this._animator;
			}
		}

		// Token: 0x06005DE9 RID: 24041 RVA: 0x0027A8AC File Offset: 0x00278CAC
		protected override void Start()
		{
			if (DevicePointAnimData.AnimatorItemTable.TryGetValue(this._id, out this._animator))
			{
				RuntimeAnimatorController itemAnimator = Singleton<Manager.Resources>.Instance.Animation.GetItemAnimator(Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.PodAnimatorID);
				this._animator.runtimeAnimatorController = itemAnimator;
			}
			if (this._commandBasePoint == null)
			{
				GameObject gameObject = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.CommandTargetName);
				this._commandBasePoint = (((gameObject != null) ? gameObject.transform : null) ?? base.transform);
			}
			if (this._pivotPoint == null)
			{
				GameObject gameObject2 = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.DevicePointPivotTargetName);
				this._pivotPoint = (((gameObject2 != null) ? gameObject2.transform : null) ?? base.transform);
			}
			if (this._recoverPoints.IsNullOrEmpty<Transform>() || this._recoverPoints.Count < 4)
			{
				this._recoverPoints.Clear();
				string[] devicePointRecoveryTargetNames = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.DevicePointRecoveryTargetNames;
				foreach (string name in devicePointRecoveryTargetNames)
				{
					GameObject gameObject3 = base.transform.FindLoop(name);
					if (gameObject3 != null)
					{
						this._recoverPoints.Add(gameObject3.transform);
					}
				}
			}
			if (this._playerRecoverPoint == null)
			{
				GameObject gameObject4 = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.DevicePointPlayerRecoveryTargetName);
				this._playerRecoverPoint = ((gameObject4 != null) ? gameObject4.transform : null);
			}
			base.Start();
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			DefinePack.MapGroup mapDefines = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines;
			int deviceIconID = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.DeviceIconID;
			Sprite icon2;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(deviceIconID, out icon2);
			GameObject gameObject5 = base.transform.FindLoop(mapDefines.DevicePointLabelTargetName);
			Transform transform = ((gameObject5 != null) ? gameObject5.transform : null) ?? base.transform;
			this._labels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					Text = "データ端末",
					Icon = icon2,
					IsHold = true,
					TargetSpriteInfo = icon.ActionSpriteInfo,
					Transform = transform,
					Condition = null,
					Event = delegate
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.BootDevice);
						MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
						Singleton<Map>.Instance.Player.CurrentDevicePoint = this;
						Singleton<Map>.Instance.Player.StashData();
						Singleton<Map>.Instance.Player.Controller.ChangeState("DeviceMenu");
					}
				}
			};
		}

		// Token: 0x06005DEA RID: 24042 RVA: 0x0027AB74 File Offset: 0x00278F74
		public void PlayInAnimation()
		{
			this._inQueue.Clear();
			string[] podInStates = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.PodInStates;
			foreach (string name_ in podInStates)
			{
				PlayState.Info item = new PlayState.Info(name_, 0);
				this._inQueue.Enqueue(item);
			}
			this._inAnimEnumerator = this.StartInAnimation();
			this._inAnimDisposable = Observable.FromCoroutine((CancellationToken _) => this._inAnimEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06005DEB RID: 24043 RVA: 0x0027ABFA File Offset: 0x00278FFA
		public void StopInAnimation()
		{
			if (this._inAnimDisposable != null)
			{
				this._inAnimDisposable.Dispose();
				this._inAnimEnumerator = null;
			}
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x0027AC1C File Offset: 0x0027901C
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
			this._inAnimEnumerator = null;
			yield break;
		}

		// Token: 0x06005DED RID: 24045 RVA: 0x0027AC38 File Offset: 0x00279038
		public void PlayOutAnimation()
		{
			this._outQueue.Clear();
			string[] podOutStates = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.PodOutStates;
			foreach (string name_ in podOutStates)
			{
				PlayState.Info item = new PlayState.Info(name_, 0);
				this._outQueue.Enqueue(item);
			}
			this._outAnimEnumerator = this.StartOutAnimation();
			this._outAnimDisposable = Observable.FromCoroutine((CancellationToken _) => this._outAnimEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06005DEE RID: 24046 RVA: 0x0027ACBE File Offset: 0x002790BE
		public void StopOutAnimation()
		{
			if (this._outAnimDisposable != null)
			{
				this._outAnimDisposable.Dispose();
				this._outAnimEnumerator = null;
			}
		}

		// Token: 0x06005DEF RID: 24047 RVA: 0x0027ACE0 File Offset: 0x002790E0
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
			this._outAnimEnumerator = null;
			yield break;
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06005DF0 RID: 24048 RVA: 0x0027ACFB File Offset: 0x002790FB
		public bool PlayingInAnimation
		{
			get
			{
				return this._inAnimEnumerator != null;
			}
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06005DF1 RID: 24049 RVA: 0x0027AD09 File Offset: 0x00279109
		public bool PlayingOutAnimation
		{
			get
			{
				return this._outAnimEnumerator != null;
			}
		}

		// Token: 0x040053E1 RID: 21473
		[SerializeField]
		private int _id;

		// Token: 0x040053E2 RID: 21474
		[SerializeField]
		private bool _enabledRangeCheck = true;

		// Token: 0x040053E3 RID: 21475
		[SerializeField]
		private float _radius = 1f;

		// Token: 0x040053E4 RID: 21476
		[SerializeField]
		private Transform _commandBasePoint;

		// Token: 0x040053E5 RID: 21477
		[SerializeField]
		private Transform _pivotPoint;

		// Token: 0x040053E6 RID: 21478
		[SerializeField]
		private List<Transform> _recoverPoints = new List<Transform>();

		// Token: 0x040053E7 RID: 21479
		[SerializeField]
		private Transform _playerRecoverPoint;

		// Token: 0x040053E8 RID: 21480
		private int? _hashCode;

		// Token: 0x040053EA RID: 21482
		private NavMeshPath _pathForCalc;

		// Token: 0x040053EB RID: 21483
		private CommandLabel.CommandInfo[] _labels;

		// Token: 0x040053EF RID: 21487
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private Animator _animator;

		// Token: 0x040053F0 RID: 21488
		private Queue<PlayState.Info> _inQueue = new Queue<PlayState.Info>();

		// Token: 0x040053F1 RID: 21489
		private Queue<PlayState.Info> _outQueue = new Queue<PlayState.Info>();

		// Token: 0x040053F2 RID: 21490
		private IEnumerator _inAnimEnumerator;

		// Token: 0x040053F3 RID: 21491
		private IDisposable _inAnimDisposable;

		// Token: 0x040053F4 RID: 21492
		private IEnumerator _outAnimEnumerator;

		// Token: 0x040053F5 RID: 21493
		private IDisposable _outAnimDisposable;
	}
}
