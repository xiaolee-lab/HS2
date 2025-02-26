using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.Animal
{
	// Token: 0x02000BC9 RID: 3017
	public class WildGround : AnimalGroundDesire
	{
		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06005BD9 RID: 23513 RVA: 0x0026EEC1 File Offset: 0x0026D2C1
		public ItemIDKeyPair FoodItemID
		{
			[CompilerGenerated]
			get
			{
				return this.FoodItemID;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06005BDA RID: 23514 RVA: 0x0026EEC9 File Offset: 0x0026D2C9
		public ItemIDKeyPair GetItemID
		{
			[CompilerGenerated]
			get
			{
				return this.getItemID;
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06005BDB RID: 23515 RVA: 0x0026EED1 File Offset: 0x0026D2D1
		public override bool WaitPossible
		{
			[CompilerGenerated]
			get
			{
				return base.WaitPossible || base.CurrentState == AnimalState.Sleep;
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06005BDC RID: 23516 RVA: 0x0026EEEB File Offset: 0x0026D2EB
		public bool IsCapturable
		{
			[CompilerGenerated]
			get
			{
				return base.IsLovely || this.lovelyModeDisposable != null;
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06005BDD RID: 23517 RVA: 0x0026EF07 File Offset: 0x0026D307
		// (set) Token: 0x06005BDE RID: 23518 RVA: 0x0026EF0F File Offset: 0x0026D30F
		public bool IsEscape { get; protected set; }

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06005BDF RID: 23519 RVA: 0x0026EF18 File Offset: 0x0026D318
		// (set) Token: 0x06005BE0 RID: 23520 RVA: 0x0026EF20 File Offset: 0x0026D320
		public bool SetIsEscape { get; private set; }

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06005BE1 RID: 23521 RVA: 0x0026EF29 File Offset: 0x0026D329
		public bool ToDepopState
		{
			[CompilerGenerated]
			get
			{
				return base.IsRain || this.SetIsEscape;
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06005BE2 RID: 23522 RVA: 0x0026EF3F File Offset: 0x0026D33F
		public override bool DepopPossible
		{
			get
			{
				return base.CurrentState == AnimalState.Idle || base.CurrentState == AnimalState.Locomotion;
			}
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x0026EF59 File Offset: 0x0026D359
		public override bool IsWithAgentFree(AgentActor _actor)
		{
			return base.IsWithAgentFree(_actor);
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06005BE4 RID: 23524 RVA: 0x0026EF62 File Offset: 0x0026D362
		public float EscapePercent
		{
			[CompilerGenerated]
			get
			{
				return this.escapePercent;
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06005BE5 RID: 23525 RVA: 0x0026EF6A File Offset: 0x0026D36A
		public float GetPercent
		{
			[CompilerGenerated]
			get
			{
				return this.getPercent;
			}
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x0026EF74 File Offset: 0x0026D374
		public void Initialize(GroundAnimalHabitatPoint _habitatPoint)
		{
			this.Clear();
			this.habitatPoint = _habitatPoint;
			if (_habitatPoint == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			if (!this.habitatPoint.Available || !this.habitatPoint.SetUse(this))
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			base.DeactivateNavMeshElements();
			this.SetWaypoints(this.habitatPoint);
			this.stateController.Initialize(this);
			this.desireController.Initialize(this);
			base.SearchAction.SetSearchEnabled(false, true);
			base.SearchActor.SetSearchEnabled(false, true);
			base.LoadBody();
			base.SetStateData();
			this.BodyEnabled = false;
			float num = UnityEngine.Random.Range(0f, 100f);
			this.IsEscape = (num < this.escapePercent);
			this.SetIsEscape = false;
			this.SetState(AnimalState.Repop, null);
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x0026F05C File Offset: 0x0026D45C
		protected void SetWaypoints(GroundAnimalHabitatPoint _habitatPoint)
		{
			if (this.MoveArea != null)
			{
				this.MoveArea.Clear();
			}
			if (this.MoveArea == null)
			{
				this.MoveArea = new LocomotionArea();
			}
			if (_habitatPoint == null)
			{
				return;
			}
			this.MoveArea.SetWaypoint(_habitatPoint.Waypoints);
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x0026F0B5 File Offset: 0x0026D4B5
		protected override void OnDestroy()
		{
			this.Active = false;
			if (this.habitatPoint != null)
			{
				this.habitatPoint.StopUse(this);
				this.habitatPoint = null;
			}
			this.Dispose();
			base.OnDestroy();
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x0026F0F0 File Offset: 0x0026D4F0
		private void Dispose()
		{
			if (this.setDepopPointDisposable != null)
			{
				this.setDepopPointDisposable.Dispose();
			}
			if (this.lovelyModeDisposable != null)
			{
				this.lovelyModeDisposable.Dispose();
			}
			if (this.badMoodDisposable != null)
			{
				this.badMoodDisposable.Dispose();
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06005BEA RID: 23530 RVA: 0x0026F145 File Offset: 0x0026D545
		// (set) Token: 0x06005BEB RID: 23531 RVA: 0x0026F154 File Offset: 0x0026D554
		public override bool BadMood
		{
			get
			{
				return this.badMoodDisposable != null;
			}
			set
			{
				if (this.badMoodDisposable != null)
				{
					this.badMoodDisposable.Dispose();
				}
				this.badMoodDisposable = null;
				if (value)
				{
					if (this.lovelyModeDisposable != null)
					{
						this.lovelyModeDisposable.Dispose();
					}
					this.lovelyModeDisposable = null;
					float num = 600f;
					if (Singleton<Manager.Resources>.IsInstance())
					{
						AnimalDefinePack animalDefinePack = Singleton<Manager.Resources>.Instance.AnimalDefinePack;
						if (((animalDefinePack != null) ? animalDefinePack.GroundWildInfo : null) != null)
						{
							num = animalDefinePack.GroundWildInfo.BadMoodTime;
						}
					}
					this.badMoodDisposable = Observable.Timer(TimeSpan.FromSeconds((double)num)).TakeUntilDestroy(this).Subscribe(delegate(long _)
					{
						this.badMoodDisposable = null;
						this.ChangeAnimalLabel();
					});
				}
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06005BEC RID: 23532 RVA: 0x0026F20C File Offset: 0x0026D60C
		public override bool IsNeutralCommand
		{
			get
			{
				return !this.BadMood && base.isNeutralCommand && (base.CurrentState == AnimalState.Idle || base.CurrentState == AnimalState.Locomotion || base.CurrentState == AnimalState.Grooming || base.CurrentState == AnimalState.Peck || base.CurrentState == AnimalState.Action0 || base.CurrentState == AnimalState.Sleep || base.IsLovely);
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06005BED RID: 23533 RVA: 0x0026F286 File Offset: 0x0026D686
		public override CommandLabel.CommandInfo[] Labels
		{
			get
			{
				if (!this.IsNeutralCommand)
				{
					return AnimalBase.emptyLabels;
				}
				if (!this.IsCapturable)
				{
					return this.giveFoodLabels;
				}
				return this.getAnimalLabels;
			}
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x0026F2B1 File Offset: 0x0026D6B1
		public override bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			return base.CurrentState != AnimalState.Escape && base.Entered(basePosition, distance, radiusA, radiusB, angle, forward);
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x0026F2D4 File Offset: 0x0026D6D4
		private void ChangeAnimalLabel()
		{
			LabelTypes labelType = base.LabelType;
			LabelTypes labelTypes;
			if (!this.IsNeutralCommand)
			{
				labelTypes = LabelTypes.None;
			}
			else if (!this.IsCapturable)
			{
				labelTypes = LabelTypes.GiveFood;
			}
			else
			{
				labelTypes = LabelTypes.GetAnimal;
			}
			if (labelTypes != labelType)
			{
				base.LabelType = labelTypes;
			}
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x0026F320 File Offset: 0x0026D720
		private void ChangeGetAnimalLabels()
		{
			LabelTypes labelType = base.LabelType;
			LabelTypes labelTypes;
			if (!this.IsNeutralCommand)
			{
				labelTypes = LabelTypes.None;
			}
			else if (!this.IsCapturable)
			{
				labelTypes = LabelTypes.GiveFood;
			}
			else
			{
				labelTypes = LabelTypes.GetAnimal;
			}
			if (labelTypes != labelType)
			{
				base.LabelType = labelTypes;
			}
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x0026F36A File Offset: 0x0026D76A
		private void ChangeEmptyLabels()
		{
			base.LabelType = LabelTypes.None;
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x0026F374 File Offset: 0x0026D774
		protected override void InitializeCommandLabels()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			if (this.giveFoodLabels.IsNullOrEmpty<CommandLabel.CommandInfo>())
			{
				Manager.Resources instance = Singleton<Manager.Resources>.Instance;
				CommonDefine.CommonIconGroup icon = instance.CommonDefine.Icon;
				StuffItemInfo stuffItemInfo = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.GameInfo.GetItem(this.foodItemID.categoryID, this.foodItemID.itemID);
				int guideCancelID = icon.GuideCancelID;
				Sprite icon2;
				instance.itemIconTables.InputIconTable.TryGetValue(guideCancelID, out icon2);
				string _foodItemName = ((stuffItemInfo != null) ? stuffItemInfo.Name : null) ?? "エサ";
				Dictionary<int, List<string>> eventPointCommandLabelTextTable = instance.Map.EventPointCommandLabelTextTable;
				List<string> source;
				eventPointCommandLabelTextTable.TryGetValue(19, out source);
				List<string> _textList1;
				eventPointCommandLabelTextTable.TryGetValue(20, out _textList1);
				int _langIdx = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
				this.giveFoodLabels = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = string.Format(source.GetElement(_langIdx) ?? "{0}をあげる", _foodItemName),
						Transform = base.LabelPoint,
						IsHold = false,
						Icon = icon2,
						TargetSpriteInfo = ((icon != null) ? icon.CharaSpriteInfo : null),
						Condition = ((PlayerActor _player) => this.HaveFoodItem()),
						ErrorText = ((PlayerActor _player) => string.Format(_textList1.GetElement(_langIdx) ?? "{0}を持っていません。", _foodItemName)),
						Event = delegate
						{
							this.ChangeEmptyLabels();
							List<StuffItem> list;
							if (Singleton<Map>.IsInstance())
							{
								PlayerActor player = Singleton<Map>.Instance.Player;
								if (player == null)
								{
									list = null;
								}
								else
								{
									PlayerData playerData = player.PlayerData;
									list = ((playerData != null) ? playerData.ItemList : null);
								}
							}
							else
							{
								list = null;
							}
							List<StuffItem> list2 = list;
							if (!list2.IsNullOrEmpty<StuffItem>())
							{
								list2.RemoveItem(new StuffItem(this.foodItemID.categoryID, this.foodItemID.itemID, 1));
							}
							if (this.CurrentState != AnimalState.Sleep)
							{
								this.SetState(AnimalState.LovelyIdle, delegate
								{
									this.ChangeGetAnimalLabels();
								});
							}
							else
							{
								this.ChangeGetAnimalLabels();
							}
							float num = 120f;
							if (Singleton<Manager.Resources>.IsInstance())
							{
								AnimalDefinePack animalDefinePack = Singleton<Manager.Resources>.Instance.AnimalDefinePack;
								if (((animalDefinePack != null) ? animalDefinePack.GroundWildInfo : null) != null)
								{
									num = animalDefinePack.GroundWildInfo.LovelyTime;
								}
							}
							if (this.lovelyModeDisposable != null)
							{
								this.lovelyModeDisposable.Dispose();
							}
							this.lovelyModeDisposable = Observable.Timer(TimeSpan.FromSeconds((double)num)).TakeUntilDestroy(this.gameObject).Do(delegate(long _)
							{
								this.lovelyModeDisposable = null;
							}).Subscribe(delegate(long _)
							{
								if (this.IsLovely)
								{
									this.ChangeEmptyLabels();
								}
								AnimalState currentState = this.CurrentState;
								if (currentState != AnimalState.LovelyIdle)
								{
									if (currentState != AnimalState.LovelyFollow)
									{
										this.ChangeAnimalLabel();
									}
									else
									{
										this.SetState(AnimalState.Idle, delegate
										{
											this.ChangeAnimalLabel();
										});
									}
								}
								else
								{
									this.SetState(AnimalState.Locomotion, delegate
									{
										this.ChangeAnimalLabel();
									});
								}
							});
						}
					}
				};
			}
			StuffItemInfo stuffItemInfo2 = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.GameInfo.GetItem(this.getItemID.categoryID, this.getItemID.itemID);
			string _getItemName = ((stuffItemInfo2 != null) ? stuffItemInfo2.Name : null) ?? "捕獲アイテム";
			if (this.getAnimalLabels.IsNullOrEmpty<CommandLabel.CommandInfo>())
			{
				Manager.Resources instance2 = Singleton<Manager.Resources>.Instance;
				CommonDefine.CommonIconGroup icon3 = instance2.CommonDefine.Icon;
				int guideCancelID2 = icon3.GuideCancelID;
				Sprite icon4;
				instance2.itemIconTables.InputIconTable.TryGetValue(guideCancelID2, out icon4);
				Dictionary<int, List<string>> eventPointCommandLabelTextTable2 = instance2.Map.EventPointCommandLabelTextTable;
				List<string> source2;
				eventPointCommandLabelTextTable2.TryGetValue(21, out source2);
				List<string> _textList1;
				eventPointCommandLabelTextTable2.TryGetValue(20, out _textList1);
				int _langIdx = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
				this.getAnimalLabels = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = string.Format(source2.GetElement(_langIdx) ?? "{0}を捕まえる", base.Name),
						Transform = base.LabelPoint,
						IsHold = false,
						Icon = icon4,
						Condition = ((PlayerActor _player) => this.HaveGetItem()),
						ErrorText = ((PlayerActor _player) => string.Format(_textList1.GetElement(_langIdx) ?? "{0}を持っていません。", _getItemName)),
						TargetSpriteInfo = ((icon3 != null) ? icon3.CharaSpriteInfo : null),
						Event = delegate
						{
							this.ChangeEmptyLabels();
							this.SetState(AnimalState.WithPlayer, null);
							PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
							if (playerActor != null)
							{
								playerActor.Animal = this;
								playerActor.PlayerController.ChangeState("CatchAnimal");
							}
						}
					}
				};
			}
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x0026F6E8 File Offset: 0x0026DAE8
		private bool HaveFoodItem()
		{
			List<StuffItem> list;
			if (Singleton<Map>.IsInstance())
			{
				PlayerActor player = Singleton<Map>.Instance.Player;
				if (player == null)
				{
					list = null;
				}
				else
				{
					PlayerData playerData = player.PlayerData;
					list = ((playerData != null) ? playerData.ItemList : null);
				}
			}
			else
			{
				list = null;
			}
			List<StuffItem> list2 = list;
			return !list2.IsNullOrEmpty<StuffItem>() && list2.Exists((StuffItem x) => x.CategoryID == this.foodItemID.categoryID && x.ID == this.foodItemID.itemID && 0 < x.Count);
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x0026F750 File Offset: 0x0026DB50
		private bool HaveGetItem()
		{
			List<StuffItem> list;
			if (Singleton<Map>.IsInstance())
			{
				PlayerActor player = Singleton<Map>.Instance.Player;
				if (player == null)
				{
					list = null;
				}
				else
				{
					PlayerData playerData = player.PlayerData;
					list = ((playerData != null) ? playerData.ItemList : null);
				}
			}
			else
			{
				list = null;
			}
			List<StuffItem> list2 = list;
			return !list2.IsNullOrEmpty<StuffItem>() && list2.Exists((StuffItem x) => x.CategoryID == this.getItemID.categoryID && x.ID == this.getItemID.itemID && 0 < x.Count);
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x0026F7B5 File Offset: 0x0026DBB5
		protected override void OnNearPlayerActorEnter(PlayerActor _player)
		{
			this.ChangeAnimalLabel();
		}

		// Token: 0x06005BF6 RID: 23542 RVA: 0x0026F7C0 File Offset: 0x0026DBC0
		protected override void OnFarPlayerActorEnter(PlayerActor _player)
		{
			if (this.WaitPossible)
			{
				if (this.lovelyModeDisposable != null)
				{
					if (!base.IsLovely && base.CurrentState != AnimalState.Sleep)
					{
						base.LabelType = LabelTypes.None;
						this.SetState(AnimalState.LovelyFollow, delegate
						{
							base.LabelType = LabelTypes.GetAnimal;
						});
						return;
					}
				}
				else if (this.IsEscape)
				{
					bool flag = true;
					if (flag)
					{
						base.Target = _player.Locomotor.transform;
						this.SetState(AnimalState.Escape, null);
						return;
					}
				}
			}
		}

		// Token: 0x06005BF7 RID: 23543 RVA: 0x0026F84C File Offset: 0x0026DC4C
		protected override void OnFarPlayerActorStay(PlayerActor _player)
		{
			if (this.WaitPossible && this.IsEscape && base.CurrentState != AnimalState.Escape)
			{
				bool flag = true;
				if (flag)
				{
					this.SetIsEscape = true;
					base.Target = _player.Locomotor.transform;
					this.SetState(AnimalState.Escape, null);
				}
			}
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x0026F8A8 File Offset: 0x0026DCA8
		public override void OnWeatherChanged(EnvironmentSimulator _simulator)
		{
			base.OnWeatherChanged(_simulator);
			if (!this.DepopPossible)
			{
				return;
			}
			if (this.weather != Weather.Rain && this.weather != Weather.Storm)
			{
				return;
			}
			this.TryToDepopState();
		}

		// Token: 0x06005BF9 RID: 23545 RVA: 0x0026F8F0 File Offset: 0x0026DCF0
		protected override bool DesireFilledEvent(DesireType _desireType)
		{
			Dictionary<DesireType, List<AnimalState>> targetStateTable = this.desireController.TargetStateTable;
			List<AnimalState> source;
			if (!targetStateTable.TryGetValue(_desireType, out source) || source.IsNullOrEmpty<AnimalState>())
			{
				return false;
			}
			this.MissingActionPoint();
			AnimalState animalState = source.Rand<AnimalState>();
			if (animalState != AnimalState.LovelyFollow && animalState != AnimalState.LovelyIdle)
			{
				this.SetState(animalState, null);
				return true;
			}
			return false;
		}

		// Token: 0x06005BFA RID: 23546 RVA: 0x0026F950 File Offset: 0x0026DD50
		protected override bool ChangedCandidateDesire(DesireType _desireType)
		{
			return false;
		}

		// Token: 0x06005BFB RID: 23547 RVA: 0x0026F954 File Offset: 0x0026DD54
		protected override void EnterRepop()
		{
			base.ActivateNavMeshObstacle();
			base.AutoChangeAnimation = false;
			base.AnimationEndUpdate = false;
			base.EnabledStateUpdate = false;
			Transform outsidePoint = this.habitatPoint.OutsidePoint;
			this.destination = new Vector3?(this.habitatPoint.InsidePoint.position);
			this.targetPosition = this.habitatPoint.InsidePoint.position;
			this.Position = outsidePoint.position;
			Vector3 eulerAngles = outsidePoint.eulerAngles;
			eulerAngles.x = (eulerAngles.z = 0f);
			base.EulerAngles = eulerAngles;
			this.BodyEnabled = true;
			base.MarkerEnabled = true;
			base.PlayInAnim(AnimationCategoryID.Locomotion, 0, null);
			this.speed = 0f;
			base.ClearWaypoint();
			base.StartCheckCoroutine();
			base.EnabledStateUpdate = true;
		}

		// Token: 0x06005BFC RID: 23548 RVA: 0x0026FA20 File Offset: 0x0026DE20
		protected override void OnRepop()
		{
			if (Time.timeScale == 0f)
			{
				return;
			}
			float deltaTime = Time.deltaTime;
			Vector3 forward = base.Forward;
			Vector3 to = this.targetPosition - this.Position;
			forward.y = (to.y = 0f);
			float num = Vector3.SignedAngle(forward, to, Vector3.up);
			if (!Mathf.Approximately(num, 0f))
			{
				float num2 = Mathf.Abs(num);
				Quaternion rotation = base.Rotation;
				float num3 = this.addAngle * deltaTime;
				if (num2 < num3)
				{
					num3 = num2;
				}
				num3 *= Mathf.Sign(num);
				base.Rotation = rotation * Quaternion.Euler(0f, num3, 0f);
			}
			Vector3 vector = this.destination.Value - this.Position;
			this.speed = Mathf.Clamp(this.speed + base.Agent.acceleration * deltaTime, 0f, base.WalkSpeed);
			Vector3 b = vector.normalized * this.speed * deltaTime;
			if (vector.sqrMagnitude <= b.magnitude)
			{
				base.AnimationEndUpdate = true;
				this.Position = this.destination.Value;
				if (Singleton<Manager.Resources>.IsInstance())
				{
					AnimalDefinePack animalDefinePack = Singleton<Manager.Resources>.Instance.AnimalDefinePack;
					if (((animalDefinePack != null) ? animalDefinePack.SystemInfo : null) != null)
					{
						float num4 = animalDefinePack.SystemInfo.PopPointCoolTimeRange.RandomRange();
					}
				}
				this.SetState(AnimalState.Locomotion, null);
				base.SearchActor.SetSearchEnabled(true, false);
				return;
			}
			this.Position += b;
		}

		// Token: 0x06005BFD RID: 23549 RVA: 0x0026FBDC File Offset: 0x0026DFDC
		protected override void AnimationRepop()
		{
			float value = (base.WalkSpeed == 0f) ? 0f : (this.speed / base.WalkSpeed);
			value = Mathf.Clamp(value, 0f, 1f) * 0.5f;
			string paramName = AnimalBase.DefaultLocomotionParamName;
			if (Singleton<Manager.Resources>.IsInstance())
			{
				AnimalDefinePack animalDefinePack = Singleton<Manager.Resources>.Instance.AnimalDefinePack;
				AnimalDefinePack.AnimatorInfoGroup animatorInfoGroup = (animalDefinePack != null) ? animalDefinePack.AnimatorInfo : null;
				if (animatorInfoGroup != null)
				{
					paramName = animatorInfoGroup.LocomotionParamName;
				}
			}
			base.SetFloat(paramName, value);
		}

		// Token: 0x06005BFE RID: 23550 RVA: 0x0026FC68 File Offset: 0x0026E068
		protected override void ExitRepop()
		{
			base.AutoChangeAnimation = true;
			base.AnimationEndUpdate = true;
			base.EnabledStateUpdate = true;
			base.Relocate(LocateTypes.NavMesh);
			base.LabelType = LabelTypes.GiveFood;
			float num = 1440f;
			if (Singleton<Manager.Resources>.IsInstance())
			{
				AnimalDefinePack animalDefinePack = Singleton<Manager.Resources>.Instance.AnimalDefinePack;
				if (((animalDefinePack != null) ? animalDefinePack.GroundWildInfo : null) != null)
				{
					num = animalDefinePack.GroundWildInfo.DestroyTimeSeconds;
				}
			}
			if (this.setDepopPointDisposable != null)
			{
				this.setDepopPointDisposable.Dispose();
			}
			this.setDepopPointDisposable = (from _ in Observable.Timer(TimeSpan.FromSeconds((double)num), TimeSpan.FromSeconds(5.0)).TakeUntilDestroy(base.gameObject)
			where this.Active && base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				if (!this.DepopPossible)
				{
					return;
				}
				if (this.TryToDepopState())
				{
					return;
				}
			});
			this.Active = true;
			EnvironmentSimulator environmentSimulator = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Simulator;
			if (environmentSimulator != null)
			{
				this.timeZone = environmentSimulator.TimeZone;
				this.weather = environmentSimulator.Weather;
			}
		}

		// Token: 0x06005BFF RID: 23551 RVA: 0x0026FD80 File Offset: 0x0026E180
		private bool TryToDepopState()
		{
			if (this.habitatPoint == null)
			{
				return false;
			}
			if (this.calculatePath == null)
			{
				this.calculatePath = new NavMeshPath();
			}
			if (!NavMesh.CalculatePath(this.Position, this.habitatPoint.InsidePoint.position, base.Agent.areaMask, this.calculatePath) || this.calculatePath.status != NavMeshPathStatus.PathComplete)
			{
				return false;
			}
			this.RemoveActionPoint();
			this.SetState(AnimalState.ToDepop, null);
			return true;
		}

		// Token: 0x06005C00 RID: 23552 RVA: 0x0026FE0C File Offset: 0x0026E20C
		protected override void EnterToDepop()
		{
			base.ActivateNavMeshAgent();
			base.ClearCurrentWaypoint();
			base.PlayInAnim(AnimationCategoryID.Locomotion, 0, null);
			base.SetAgentSpeed(AnimalGround.LocomotionTypes.Walk);
			this.targetPosition = this.habitatPoint.InsidePoint.position;
			base.Agent.SetDestination(this.targetPosition);
			base.Agent.isStopped = false;
			this.stoppingDistance = base.Agent.stoppingDistance + 0.25f;
			base.LabelType = LabelTypes.None;
			base.LocomotionCount = 1;
		}

		// Token: 0x06005C01 RID: 23553 RVA: 0x0026FE90 File Offset: 0x0026E290
		protected override void OnToDepop()
		{
			if (base.AgentPathPending)
			{
				return;
			}
			if (base.IsNearPoint(this.targetPosition, this.stoppingDistance))
			{
				this.SetState(AnimalState.Depop, null);
				return;
			}
			if (base.Agent.isActiveAndEnabled)
			{
				base.Agent.SetDestination(this.targetPosition);
			}
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x0026FEEB File Offset: 0x0026E2EB
		protected override void AnimationToDepop()
		{
			base.WalkAnimationUpdate();
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x0026FEF4 File Offset: 0x0026E2F4
		protected override void EnterDepop()
		{
			base.AutoChangeAnimation = false;
			base.AnimationEndUpdate = false;
			base.ActivateNavMeshObstacle();
			this.targetPosition = this.habitatPoint.OutsidePoint.position;
			base.PlayInAnim(AnimationCategoryID.Locomotion, 0, null);
			this.speed = base.Agent.velocity.magnitude;
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x0026FF50 File Offset: 0x0026E350
		protected override void OnDepop()
		{
			if (Time.timeScale == 0f)
			{
				return;
			}
			float deltaTime = Time.deltaTime;
			Vector3 forward = base.Forward;
			Vector3 to = this.targetPosition - this.Position;
			forward.y = (to.y = 0f);
			float num = Vector3.SignedAngle(forward, to, Vector3.up);
			if (!Mathf.Approximately(num, 0f))
			{
				float num2 = Mathf.Abs(num);
				Quaternion rotation = base.Rotation;
				float num3 = this.addAngle * deltaTime;
				if (num2 < num3)
				{
					num3 = num2;
				}
				num3 *= Mathf.Sign(num);
				base.Rotation = rotation * Quaternion.Euler(0f, num3, 0f);
			}
			Vector3 vector = this.targetPosition - this.Position;
			this.speed = Mathf.Clamp(this.speed + base.Agent.acceleration * deltaTime, 0f, base.WalkSpeed);
			Vector3 b = vector.normalized * this.speed * deltaTime;
			if (vector.sqrMagnitude <= b.sqrMagnitude)
			{
				base.CrossFade(-1f);
				this.Position = this.targetPosition;
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			this.Position += b;
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x002700B4 File Offset: 0x0026E4B4
		protected override void AnimationDepop()
		{
			float value = (base.WalkSpeed == 0f) ? 0f : (this.speed / base.WalkSpeed);
			value = Mathf.Clamp(value, 0f, 1f) * 0.5f;
			string paramName = AnimalBase.DefaultLocomotionParamName;
			if (Singleton<Manager.Resources>.IsInstance())
			{
				AnimalDefinePack animalDefinePack = Singleton<Manager.Resources>.Instance.AnimalDefinePack;
				AnimalDefinePack.AnimatorInfoGroup animatorInfoGroup = (animalDefinePack != null) ? animalDefinePack.AnimatorInfo : null;
				if (animatorInfoGroup != null)
				{
					paramName = animatorInfoGroup.LocomotionParamName;
				}
			}
			base.SetFloat(paramName, value);
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x00270140 File Offset: 0x0026E540
		protected override void ExitDepop()
		{
			this.RemoveActionPoint();
			if (this.habitatPoint != null)
			{
				this.habitatPoint.StopUse(this);
				this.habitatPoint = null;
			}
			base.StateTimeLimit = 1f;
			base.AutoChangeAnimation = true;
			base.AnimationEndUpdate = true;
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x00270194 File Offset: 0x0026E594
		protected override void OnLocomotion()
		{
			if (this.Wait())
			{
				return;
			}
			if (base.AgentPathPending)
			{
				return;
			}
			if (this.ToDepopState)
			{
				base.StateCounter += Time.deltaTime;
				if (base.StateTimeLimit < base.StateCounter)
				{
					base.StateCounter = 0f;
					if (this.TryToDepopState())
					{
						return;
					}
				}
			}
			if (base.IsNearPoint())
			{
				if (this.ToDepopState && this.TryToDepopState())
				{
					return;
				}
				if (!base.HasActionPoint)
				{
					base.ChangeNextWaypoint();
				}
				else if (!base.SetNextState())
				{
					base.StateEndEvent();
					return;
				}
			}
			else if (base.HasNotAgentPath)
			{
				if (base.HasActionPoint)
				{
					base.Agent.SetDestination(this.actionPoint.Destination);
				}
				else
				{
					base.ChangeNextWaypoint();
				}
			}
			if (base.Agent.isActiveAndEnabled && base.Agent.isOnNavMesh && base.Agent.hasPath)
			{
				base.Agent.SetDestination(base.Agent.destination);
			}
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x002702D0 File Offset: 0x0026E6D0
		protected override void EnterLovelyIdle()
		{
			base.ActivateNavMeshAgent();
			int poseID = (base.AnimalType != AnimalTypes.Cat) ? 0 : 1;
			base.PlayInAnim(AnimationCategoryID.Idle, poseID, null);
			if (!base.IsPrevLovely)
			{
				this.EnterLovely();
			}
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x00270314 File Offset: 0x0026E714
		protected override void OnLovelyIdle()
		{
			if (this.Wait())
			{
				return;
			}
			Vector3 vector = base.FollowActor.Position - this.Position;
			float num = 20f;
			float num2 = 10f;
			if (Singleton<Manager.Resources>.IsInstance())
			{
				AnimalDefinePack animalDefinePack = Singleton<Manager.Resources>.Instance.AnimalDefinePack;
				AnimalDefinePack.GroundAnimalInfoGroup groundAnimalInfoGroup = (animalDefinePack != null) ? animalDefinePack.GroundAnimalInfo : null;
				if (groundAnimalInfoGroup != null)
				{
					num = groundAnimalInfoGroup.FollowStopDistance;
					num2 = groundAnimalInfoGroup.FollowIdleSpace;
				}
			}
			float num3 = num + num2;
			if (num3 * num3 < vector.sqrMagnitude)
			{
				this.SetState(AnimalState.LovelyFollow, null);
			}
		}

		// Token: 0x06005C0A RID: 23562 RVA: 0x002703A7 File Offset: 0x0026E7A7
		protected override void ExitLovelyIdle()
		{
			if (!base.IsLovely)
			{
				this.ExitLovely();
			}
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x002703BC File Offset: 0x0026E7BC
		protected override void EnterLovelyFollow()
		{
			base.ActivateNavMeshAgent();
			base.AnimationEndUpdate = false;
			base.PlayInAnim(AnimationCategoryID.Locomotion, 0, null);
			base.StateTimeLimit = 10f;
			if (!base.IsPrevLovely)
			{
				this.EnterLovely();
			}
			base.SetAgentSpeed(AnimalGround.LocomotionTypes.Walk);
			this.followWaitCounter = 0f;
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x00270410 File Offset: 0x0026E810
		protected override void OnLovelyFollow()
		{
			bool isStopped = base.IsNearPoint(base.FollowActor.Position);
			base.Agent.isStopped = isStopped;
			if (!base.Agent.isStopped)
			{
				this.followWaitCounter = 0f;
				base.Agent.SetDestination(base.FollowActor.Position);
			}
			else
			{
				this.followWaitCounter += Time.deltaTime;
				if (2f <= this.followWaitCounter)
				{
					this.followWaitCounter = 0f;
					this.SetState(AnimalState.LovelyIdle, null);
					return;
				}
				base.Agent.SetDestination(base.FollowActor.Position);
			}
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x002704BF File Offset: 0x0026E8BF
		protected override void ExitLovelyFollow()
		{
			base.AnimationEndUpdate = true;
			if (!base.IsLovely)
			{
				this.ExitLovely();
			}
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x002704D9 File Offset: 0x0026E8D9
		protected override void AnimationLovelyFollow()
		{
			base.WalkAnimationUpdate();
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x002704E4 File Offset: 0x0026E8E4
		private void EnterLovely()
		{
			base.FollowActor = ((!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player);
			float num = 5f;
			if (Singleton<Manager.Resources>.IsInstance())
			{
				AnimalDefinePack animalDefinePack = Singleton<Manager.Resources>.Instance.AnimalDefinePack;
				AnimalDefinePack.GroundAnimalInfoGroup groundAnimalInfoGroup = (animalDefinePack != null) ? animalDefinePack.GroundAnimalInfo : null;
				if (groundAnimalInfoGroup != null)
				{
					num = groundAnimalInfoGroup.FollowStopDistance;
				}
			}
			base.Agent.stoppingDistance = num;
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x00270554 File Offset: 0x0026E954
		private void ExitLovely()
		{
			base.Agent.stoppingDistance = base.FirstStoppingDistance;
			base.FollowActor = null;
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x00270570 File Offset: 0x0026E970
		protected override void EnterSleep()
		{
			this.ModelInfo.EyesShapeInfo.SetBlendShape(100f);
			base.ActivateNavMeshObstacle();
			this.EnterAction(delegate()
			{
				if (this.lovelyModeDisposable == null)
				{
					this.SetState(AnimalState.Locomotion, null);
				}
				else
				{
					this.SetState(AnimalState.LovelyFollow, null);
				}
			});
			base.PlayInAnim(AnimationCategoryID.Sleep, null);
			base.SetSchedule(this.CurrentAnimState);
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x002705C0 File Offset: 0x0026E9C0
		protected bool SetEscapePoint()
		{
			if (!base.Target)
			{
				return false;
			}
			Waypoint waypoint = null;
			base.SetAgentSpeed(AnimalGround.LocomotionTypes.Run);
			if (this.MoveArea.GetRandomPoint(this.Position, base.Target.position, base.SearchActor.FarSearchRadius, (this.Position - base.Target.position).normalized, 135f, ref waypoint, LocomotionArea.AreaType.All) && base.Agent.SetDestination(waypoint.transform.position))
			{
				base.Agent.isStopped = false;
				base.Agent.updateRotation = true;
				base.ClearCurrentWaypoint();
				waypoint.Reserver = this;
				this.currentWaypoint = waypoint;
				this.destination = null;
				base.LocomotionCount = 1;
				return true;
			}
			return false;
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x0027069A File Offset: 0x0026EA9A
		protected override void EnterEscape()
		{
			base.ActivateNavMeshAgent();
			this.CancelActionPoint();
			base.AnimationEndUpdate = false;
			base.PlayInAnim(AnimationCategoryID.Locomotion, 0, null);
			base.SetAgentSpeed(AnimalGround.LocomotionTypes.Run);
			this.SetIsEscape = true;
			if (!this.SetEscapePoint())
			{
				this.SetState(AnimalState.Locomotion, null);
				return;
			}
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x002706DC File Offset: 0x0026EADC
		protected override void OnEscape()
		{
			if (!base.IsNearPoint() && (!base.IsActiveAgent || base.Agent.hasPath || base.Agent.pathPending))
			{
				if (base.Agent.isActiveAndEnabled && base.Agent.isOnNavMesh && base.Agent.hasPath)
				{
					base.Agent.SetDestination(base.Agent.destination);
				}
				return;
			}
			if (base.SearchActor.CheckPlayerOnFarSearchArea() && this.SetEscapePoint())
			{
				return;
			}
			this.SetState(AnimalState.Locomotion, null);
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x0027078B File Offset: 0x0026EB8B
		protected override void ExitEscape()
		{
			base.ClearCurrentWaypoint();
			base.Target = null;
			base.Agent.velocity = Vector3.zero;
			base.AnimationEndUpdate = true;
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x002707B1 File Offset: 0x0026EBB1
		protected override void AnimationEscape()
		{
			base.RunAnimationUpdate();
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x002707BC File Offset: 0x0026EBBC
		public void StartAvoid(Vector3 _avoidPoint, Action _endEvent = null)
		{
			this.BadMood = true;
			this.SetState(AnimalState.Locomotion, delegate
			{
				if (this.CurrentState != AnimalState.Locomotion)
				{
					return;
				}
				Waypoint nextWaypoint = null;
				if (!this.MoveArea.GetRandomPoint(this.Position, _avoidPoint, this.SearchActor.NearSearchRadius, (this.Position - _avoidPoint).normalized, 200f, ref nextWaypoint, LocomotionArea.AreaType.Normal) || !this.SetNextWaypoint(nextWaypoint))
				{
					this.SetState(AnimalState.Idle, null);
					return;
				}
			});
		}

		// Token: 0x040052EE RID: 21230
		private GroundAnimalHabitatPoint habitatPoint;

		// Token: 0x040052EF RID: 21231
		[SerializeField]
		[Tooltip("視界に入って逃げる確率")]
		private float escapePercent = 80f;

		// Token: 0x040052F0 RID: 21232
		[SerializeField]
		[Tooltip("餌を上げてペットになる確率")]
		private float getPercent = 30f;

		// Token: 0x040052F1 RID: 21233
		[SerializeField]
		private ItemIDKeyPair foodItemID = default(ItemIDKeyPair);

		// Token: 0x040052F2 RID: 21234
		[SerializeField]
		private ItemIDKeyPair getItemID = default(ItemIDKeyPair);

		// Token: 0x040052F5 RID: 21237
		private IDisposable setDepopPointDisposable;

		// Token: 0x040052F6 RID: 21238
		private IDisposable lovelyModeDisposable;

		// Token: 0x040052F7 RID: 21239
		private IDisposable badMoodDisposable;

		// Token: 0x040052F8 RID: 21240
		protected CommandLabel.CommandInfo[] giveFoodLabels;

		// Token: 0x040052F9 RID: 21241
		protected CommandLabel.CommandInfo[] getAnimalLabels;

		// Token: 0x040052FA RID: 21242
		private Vector3 targetPosition = Vector3.zero;

		// Token: 0x040052FB RID: 21243
		private float stoppingDistance = 1f;

		// Token: 0x040052FC RID: 21244
		private float speed;

		// Token: 0x040052FD RID: 21245
		protected float followWaitCounter;
	}
}
