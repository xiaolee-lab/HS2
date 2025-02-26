using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.SaveData;
using Manager;
using ReMotion;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.Animal
{
	// Token: 0x02000BC8 RID: 3016
	public class WildFrog : GroundInsect
	{
		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06005BA9 RID: 23465 RVA: 0x0026E12A File Offset: 0x0026C52A
		// (set) Token: 0x06005BAA RID: 23466 RVA: 0x0026E132 File Offset: 0x0026C532
		public string MaterialAlphaParamName
		{
			get
			{
				return this.materialAlphaParamName;
			}
			set
			{
				this.materialAlphaParamName = value;
				this.materialAlphaParamID = UnityEngine.Shader.PropertyToID(this.materialAlphaParamName);
			}
		}

		// Token: 0x06005BAB RID: 23467 RVA: 0x0026E14C File Offset: 0x0026C54C
		public override bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			return !Manager.Map.TutorialMode && base.Entered(basePosition, distance, radiusA, radiusB, angle, forward);
		}

		// Token: 0x06005BAC RID: 23468 RVA: 0x0026E16C File Offset: 0x0026C56C
		protected override void InitializeCommandLabels()
		{
			if (this.getLabels.IsNullOrEmpty<CommandLabel.CommandInfo>())
			{
				CommonDefine commonDefine = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.CommonDefine;
				CommonDefine.CommonIconGroup commonIconGroup = (!(commonDefine != null)) ? null : commonDefine.Icon;
				Manager.Resources instance = Singleton<Manager.Resources>.Instance;
				int guideCancelID = commonIconGroup.GuideCancelID;
				Sprite icon;
				instance.itemIconTables.InputIconTable.TryGetValue(guideCancelID, out icon);
				Dictionary<int, List<string>> eventPointCommandLabelTextTable = instance.Map.EventPointCommandLabelTextTable;
				List<string> source;
				eventPointCommandLabelTextTable.TryGetValue(18, out source);
				int index = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
				this.getLabels = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = source.GetElement(index),
						Transform = base.LabelPoint,
						IsHold = false,
						Icon = icon,
						TargetSpriteInfo = ((commonIconGroup != null) ? commonIconGroup.CharaSpriteInfo : null),
						Event = delegate
						{
							bool flag = true;
							if (this.habitatPoint != null && Singleton<Manager.Resources>.IsInstance() && Singleton<Manager.Map>.IsInstance())
							{
								PlayerActor player = Singleton<Manager.Map>.Instance.Player;
								Dictionary<int, ItemTableElement> itemTableInFrogPoint = Singleton<Manager.Resources>.Instance.GameInfo.GetItemTableInFrogPoint(this.habitatPoint.ItemID);
								Actor.SearchInfo searchInfo = player.RandomAddItem(itemTableInFrogPoint, true);
								if (searchInfo.IsSuccess)
								{
									bool flag2 = false;
									foreach (Actor.ItemSearchInfo itemSearchInfo in searchInfo.ItemList)
									{
										PlayerData playerData = player.PlayerData;
										List<StuffItem> itemList = playerData.ItemList;
										StuffItem item = new StuffItem(itemSearchInfo.categoryID, itemSearchInfo.id, itemSearchInfo.count);
										int num;
										if (itemList.CanAddItem(playerData.InventorySlotMax, item, out num) && 0 < num)
										{
											int count = Mathf.Min(num, itemSearchInfo.count);
											itemList.AddItem(item, count, playerData.InventorySlotMax);
											StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(itemSearchInfo.categoryID, itemSearchInfo.id);
											MapUIContainer.AddSystemItemLog(item2, count, true);
											flag2 = true;
										}
									}
									if (!flag2)
									{
										flag = false;
										MapUIContainer.PushWarningMessage(Popup.Warning.Type.PouchIsFull);
									}
								}
								else
								{
									MapUIContainer.AddNotify(MapUIContainer.ItemGetEmptyText);
								}
							}
							if (flag)
							{
								this.Destroy();
							}
						}
					}
				};
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06005BAD RID: 23469 RVA: 0x0026E287 File Offset: 0x0026C687
		protected bool EmptyMovePoints
		{
			[CompilerGenerated]
			get
			{
				return this.movePoints.IsNullOrEmpty<Vector3>();
			}
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x0026E294 File Offset: 0x0026C694
		protected override void Awake()
		{
			base.Awake();
			if (!this.materialAlphaParamName.IsNullOrEmpty())
			{
				this.materialAlphaParamID = UnityEngine.Shader.PropertyToID(this.materialAlphaParamName);
			}
			if (base.NavMeshCon == null)
			{
				return;
			}
			base.NavMeshCon.SetEnabled(false);
			this.navPath = new NavMeshPath();
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x0026E2F1 File Offset: 0x0026C6F1
		protected override void OnDestroy()
		{
			this.Active = false;
			if (this.habitatPoint != null)
			{
				this.habitatPoint.StopUse(this);
				this.habitatPoint = null;
			}
			base.OnDestroy();
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x0026E328 File Offset: 0x0026C728
		public void Initialize(FrogHabitatPoint _habitatPoint)
		{
			this.Clear();
			this.habitatPoint = _habitatPoint;
			if (_habitatPoint == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			if (!this.habitatPoint.SetUse(this))
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			MapArea ownerArea = this.habitatPoint.OwnerArea;
			base.ChunkID = ((!(ownerArea != null)) ? 0 : ownerArea.ChunkID);
			base.LoadBody();
			base.SetStateData();
			base.NavMeshCon.Animator = this.animator;
			if (this.animator != null)
			{
				this.rootMotion = this.animator.GetOrAddComponent<AnimalRootMotion>();
			}
			SkinnedMeshRenderer componentInChildren = base.GetComponentInChildren<SkinnedMeshRenderer>(true);
			this.material = ((!(componentInChildren != null)) ? null : componentInChildren.material);
			this.rootMotion.OnMove = delegate(Vector3 p, Quaternion q)
			{
				base.NavMeshCon.Move(p - this.Position);
				Vector3 eulerAngles = (q * Quaternion.Inverse(base.Rotation)).eulerAngles;
				eulerAngles.x = (eulerAngles.z = 0f);
				base.Rotation *= Quaternion.Euler(eulerAngles);
			};
			bool flag = false;
			this.BodyEnabled = flag;
			base.MarkerEnabled = flag;
			base.NavMeshCon.SetEnabled(false);
			base.TargetMapArea = this.habitatPoint.OwnerArea;
			this.IsNearPlayer = false;
			this.SetState(AnimalState.Start, null);
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06005BB1 RID: 23473 RVA: 0x0026E457 File Offset: 0x0026C857
		// (set) Token: 0x06005BB2 RID: 23474 RVA: 0x0026E45F File Offset: 0x0026C85F
		public bool IsNearPlayer { get; private set; }

		// Token: 0x06005BB3 RID: 23475 RVA: 0x0026E468 File Offset: 0x0026C868
		private void ToEscapeState(PlayerActor _player)
		{
			this.IsNearPlayer = true;
			base.TargetActor = _player;
			if (base.CurrentState == AnimalState.Idle || base.CurrentState == AnimalState.Locomotion)
			{
				this.SetState(AnimalState.Escape, null);
			}
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x0026E499 File Offset: 0x0026C899
		protected override void OnNearPlayerActorEnter(PlayerActor _player)
		{
			if (!this.IsNearPlayer)
			{
				this.ToEscapeState(_player);
			}
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x0026E4AD File Offset: 0x0026C8AD
		protected override void OnNearPlayerActorStay(PlayerActor _player)
		{
			if (!this.IsNearPlayer)
			{
				this.ToEscapeState(_player);
			}
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x0026E4C1 File Offset: 0x0026C8C1
		protected override void OnFarPlayerActorExit(PlayerActor _player)
		{
			this.IsNearPlayer = false;
			if (base.CurrentState == AnimalState.Escape)
			{
				this.SetState(AnimalState.Idle, null);
			}
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x0026E4DF File Offset: 0x0026C8DF
		protected override void EnterStart()
		{
			this.ClearMovePointList();
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x0026E4E8 File Offset: 0x0026C8E8
		protected override void OnStart()
		{
			Vector3 randomMoveAreaPoint = this.GetRandomMoveAreaPoint();
			if (!NavMesh.SamplePosition(randomMoveAreaPoint, out this.navHit, 5f, base.NavMeshCon.AreaMask) || !this.navHit.hit)
			{
				return;
			}
			this.Position = this.navHit.position;
			bool flag = true;
			this.BodyEnabled = flag;
			base.MarkerEnabled = flag;
			this.StartAddMovePoint();
			base.NavMeshCon.SetEnabled(true);
			this.Active = true;
			this.SetState(AnimalState.Idle, null);
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x0026E570 File Offset: 0x0026C970
		protected override void EnterIdle()
		{
			base.PlayInAnim(AnimationCategoryID.Idle, null);
			base.NavMeshCon.MoveUpdateEnabled = false;
			base.StateTimeLimit = UnityEngine.Random.Range(this.idleTimeSecond.x, this.idleTimeSecond.y);
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x0026E5A7 File Offset: 0x0026C9A7
		protected override void OnIdle()
		{
			base.StateCounter += Time.deltaTime;
			if (base.StateCounter < base.StateTimeLimit)
			{
				return;
			}
			this.SetState(AnimalState.Locomotion, null);
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x0026E5D5 File Offset: 0x0026C9D5
		protected override void ExitIdle()
		{
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x0026E5D8 File Offset: 0x0026C9D8
		private void SetLocomotionPoint()
		{
			this.locomotionPossible = false;
			if (this.setLocomotionPointDisposable != null)
			{
				this.setLocomotionPointDisposable.Dispose();
			}
			this.setLocomotionPointDisposable = (from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject).TakeWhile((long _) => !base.NavMeshCon.IsMoving)
			where base.isActiveAndEnabled
			where !this.movePoints.IsNullOrEmpty<Vector3>()
			where !base.NavMeshCon.Agent.pathPending
			select _).Subscribe(delegate(long _)
			{
				base.NavMeshCon.SetDestination(this.movePoints.Pop<Vector3>(), false);
			}, delegate()
			{
				this.locomotionPossible = true;
			});
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x0026E67B File Offset: 0x0026CA7B
		protected override void EnterLocomotion()
		{
			base.PlayInAnim(AnimationCategoryID.Locomotion, 0, null);
			base.NavMeshCon.MoveUpdateEnabled = true;
			this.locomotionCount = UnityEngine.Random.Range(1, 3);
			this.SetLocomotionPoint();
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x0026E6A8 File Offset: 0x0026CAA8
		protected override void OnLocomotion()
		{
			if (!this.locomotionPossible)
			{
				return;
			}
			if (base.NavMeshCon.IsMoving)
			{
				return;
			}
			if (!base.NavMeshCon.IsReached)
			{
				return;
			}
			this.locomotionCount--;
			if (this.locomotionCount <= 0)
			{
				this.SetState(AnimalState.Idle, null);
				return;
			}
			this.SetLocomotionPoint();
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x0026E70C File Offset: 0x0026CB0C
		protected override void ExitLocomotion()
		{
			if (this.setLocomotionPointDisposable != null)
			{
				this.setLocomotionPointDisposable.Dispose();
			}
			this.setLocomotionPointDisposable = null;
			base.NavMeshCon.Refresh();
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x0026E738 File Offset: 0x0026CB38
		protected override void EnterEscape()
		{
			base.PlayInAnim(AnimationCategoryID.Locomotion, 1, null);
			this.StopAddMovePoint();
			this.ClearMovePointList();
			base.Rotation = this.LookOppositeAxisY(base.TargetActor.Position);
			this.prevNormalizedTime = 0f;
			base.StopPlayAnimChange();
			base.StateTimeLimit = this.escapeDestroyTimeSecond;
			base.NavMeshCon.MoveUpdateEnabled = false;
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x0026E79C File Offset: 0x0026CB9C
		protected override void OnEscape()
		{
			float num = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
			num = Mathf.Repeat(num, 1f);
			if (num < this.prevNormalizedTime)
			{
				base.Rotation = this.LookOppositeAxisY(base.TargetActor.Position);
			}
			this.prevNormalizedTime = num;
			if (base.StateTimeLimit < (base.StateCounter += Time.deltaTime))
			{
				this.SetState(AnimalState.Depop, null);
				return;
			}
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x0026E81D File Offset: 0x0026CC1D
		protected override void ExitEscape()
		{
			if (base.CurrentState != AnimalState.Depop)
			{
				base.NavMeshCon.Refresh();
				this.StartAddMovePoint();
				base.AutoChangeAnimation = true;
			}
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x0026E844 File Offset: 0x0026CC44
		protected override void EnterDepop()
		{
			base.AutoChangeAnimation = false;
			if (this.material == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			float _startAlpha = this.material.GetFloat(this.materialAlphaParamID);
			float _endAlpha = 0f;
			ObservableEasing.Linear(this.fadeOutTimeSecond, false).TakeUntilDestroy(base.gameObject).Subscribe(delegate(float x)
			{
				this.material.SetFloat(this.materialAlphaParamID, Mathf.Lerp(_startAlpha, _endAlpha, x));
			}, delegate()
			{
				if (this.CurrentState == AnimalState.Depop)
				{
					this.SetState(AnimalState.Destroyed, null);
				}
			});
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x0026E8D8 File Offset: 0x0026CCD8
		protected override void OnDepop()
		{
			float num = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
			num = Mathf.Repeat(num, 1f);
			if (num < this.prevNormalizedTime)
			{
				base.Rotation = this.LookOppositeAxisY(base.TargetActor.Position);
			}
			this.prevNormalizedTime = num;
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x0026E930 File Offset: 0x0026CD30
		protected override void ExitDepop()
		{
			if (this.habitatPoint != null)
			{
				this.habitatPoint.StopUse(this);
				this.habitatPoint = null;
			}
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x0026E958 File Offset: 0x0026CD58
		private Quaternion LookTargetAxisY(Vector3 _target)
		{
			Vector3 vector = _target - this.Position;
			vector.y = 0f;
			return Quaternion.LookRotation(vector.normalized, Vector3.up);
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x0026E990 File Offset: 0x0026CD90
		private Quaternion LookOppositeAxisY(Vector3 _target)
		{
			Vector3 vector = this.Position - _target;
			vector.y = 0f;
			return Quaternion.LookRotation(vector.normalized, Vector3.up);
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x0026E9C7 File Offset: 0x0026CDC7
		private float DistanceXY(Vector3 _p1, Vector3 _p2)
		{
			_p1.y = _p2.y;
			return Vector3.Distance(_p1, _p2);
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x0026E9E0 File Offset: 0x0026CDE0
		public void StartAddMovePoint()
		{
			this.path = new NavMeshPath();
			if (this.addMovePointDisposable != null)
			{
				this.addMovePointDisposable.Dispose();
			}
			this.addMovePointDisposable = (from _ in Observable.IntervalFrame(10, FrameCountType.Update).TakeUntilDestroy(base.gameObject)
			where this.movePoints.Count < 3
			select _).Subscribe(delegate(long _)
			{
				Vector3 vector = (!this.movePoints.IsNullOrEmpty<Vector3>()) ? this.movePoints[this.movePoints.Count - 1] : this.Position;
				Vector3 randomMoveAreaPoint = this.GetRandomMoveAreaPoint();
				if (this.OnValue(this.nextPointDistance, Vector3.Distance(vector, randomMoveAreaPoint)))
				{
					return;
				}
				if (NavMesh.CalculatePath(vector, randomMoveAreaPoint, base.NavMeshCon.AreaMask, this.path) && this.path.status == NavMeshPathStatus.PathComplete && !this.path.corners.IsNullOrEmpty<Vector3>())
				{
					this.movePoints.Add(this.path.corners[this.path.corners.Length - 1]);
				}
			});
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x0026EA4B File Offset: 0x0026CE4B
		public void StopAddMovePoint()
		{
			if (this.addMovePointDisposable != null)
			{
				this.addMovePointDisposable.Dispose();
			}
			this.addMovePointDisposable = null;
		}

		// Token: 0x06005BCB RID: 23499 RVA: 0x0026EA6C File Offset: 0x0026CE6C
		public void ClearMovePointList()
		{
			this.movePoints.Clear();
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x0026EA7C File Offset: 0x0026CE7C
		public Vector3 GetRandomMoveAreaPoint()
		{
			Vector3 position = this.habitatPoint.Position;
			float moveRadius = this.habitatPoint.MoveRadius;
			return base.GetRandomPosOnCircle(moveRadius) + position;
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x0026EAAE File Offset: 0x0026CEAE
		public bool OnValue(Vector3 _minMax, float _value)
		{
			return _minMax.x <= _value && _value <= _minMax.y;
		}

		// Token: 0x040052DC RID: 21212
		[SerializeField]
		[ReadOnly]
		[HideInEditorMode]
		private FrogHabitatPoint habitatPoint;

		// Token: 0x040052DD RID: 21213
		[SerializeField]
		[MinMaxSlider(0f, 200f, true)]
		[Tooltip("次の座標を生成する時の最短/最大距離")]
		private Vector2 nextPointDistance = new Vector2(5f, 15f);

		// Token: 0x040052DE RID: 21214
		[SerializeField]
		[MinMaxSlider(0.1f, 60f, true)]
		[Tooltip("待機時間")]
		private Vector2 idleTimeSecond = Vector2.zero;

		// Token: 0x040052DF RID: 21215
		[SerializeField]
		[Min(0.001f)]
		[Tooltip("逃げの状態が何秒続いたら消えるか")]
		private float escapeDestroyTimeSecond = 4f;

		// Token: 0x040052E0 RID: 21216
		[SerializeField]
		[Min(0.001f)]
		[Tooltip("透明になるフェードアウトの時間")]
		private float fadeOutTimeSecond = 0.8f;

		// Token: 0x040052E1 RID: 21217
		[SerializeField]
		[ReadOnly]
		private Material material;

		// Token: 0x040052E2 RID: 21218
		[SerializeField]
		private string materialAlphaParamName = "_AlphaEx";

		// Token: 0x040052E3 RID: 21219
		private int materialAlphaParamID;

		// Token: 0x040052E4 RID: 21220
		protected List<Vector3> movePoints = new List<Vector3>();

		// Token: 0x040052E5 RID: 21221
		protected int locomotionCount;

		// Token: 0x040052E6 RID: 21222
		protected NavMeshHit navHit = default(NavMeshHit);

		// Token: 0x040052E7 RID: 21223
		protected NavMeshPath navPath;

		// Token: 0x040052E9 RID: 21225
		private bool locomotionPossible;

		// Token: 0x040052EA RID: 21226
		private IDisposable setLocomotionPointDisposable;

		// Token: 0x040052EB RID: 21227
		private float prevNormalizedTime;

		// Token: 0x040052EC RID: 21228
		private IDisposable addMovePointDisposable;

		// Token: 0x040052ED RID: 21229
		private NavMeshPath path;
	}
}
