using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.Player;
using IllusionUtility.GetUtility;
using Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000BFD RID: 3069
	public class DoorPoint : ActionPoint
	{
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06005E18 RID: 24088 RVA: 0x0027B7AB File Offset: 0x00279BAB
		public DoorPoint.OpenTypeState OpenType
		{
			[CompilerGenerated]
			get
			{
				return this._openType;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06005E19 RID: 24089 RVA: 0x0027B7B3 File Offset: 0x00279BB3
		// (set) Token: 0x06005E1A RID: 24090 RVA: 0x0027B7BB File Offset: 0x00279BBB
		public NavMeshObstacle ObstacleInOpenRight
		{
			get
			{
				return this._obstacleInOpenRight;
			}
			set
			{
				this._obstacleInOpenRight = value;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06005E1B RID: 24091 RVA: 0x0027B7C4 File Offset: 0x00279BC4
		// (set) Token: 0x06005E1C RID: 24092 RVA: 0x0027B7CC File Offset: 0x00279BCC
		public NavMeshObstacle ObstacleInOpenLeft
		{
			get
			{
				return this._obstacleInOpenLeft;
			}
			set
			{
				this._obstacleInOpenLeft = value;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06005E1D RID: 24093 RVA: 0x0027B7D5 File Offset: 0x00279BD5
		// (set) Token: 0x06005E1E RID: 24094 RVA: 0x0027B7DD File Offset: 0x00279BDD
		public NavMeshObstacle ObstacleInClose
		{
			get
			{
				return this._obstacleInClose;
			}
			set
			{
				this._obstacleInClose = value;
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06005E1F RID: 24095 RVA: 0x0027B7E6 File Offset: 0x00279BE6
		// (set) Token: 0x06005E20 RID: 24096 RVA: 0x0027B7EE File Offset: 0x00279BEE
		public DoorAnimation DoorAnimation { get; private set; }

		// Token: 0x06005E21 RID: 24097 RVA: 0x0027B7F8 File Offset: 0x00279BF8
		public void SetOpenState(DoorPoint.OpenPattern pattern, bool isSelf)
		{
			if (this.OpenState == pattern)
			{
				return;
			}
			this.OpenState = pattern;
			if (isSelf)
			{
				if (this._doorPointFragment != null)
				{
					this._doorPointFragment.SetOpenState(pattern, isSelf);
				}
				if (this._offMeshLink != null)
				{
					this._offMeshLink.activated = (pattern == DoorPoint.OpenPattern.Close);
				}
				if (this._obstacleInOpenRight != null)
				{
					this._obstacleInOpenRight.gameObject.SetActive(pattern == DoorPoint.OpenPattern.OpenRight);
				}
				if (this._obstacleInOpenLeft != null)
				{
					this._obstacleInOpenLeft.gameObject.SetActive(pattern == DoorPoint.OpenPattern.OpenLeft);
				}
				if (this._obstacleInClose != null)
				{
					this._obstacleInClose.gameObject.SetActive(pattern == DoorPoint.OpenPattern.Close);
				}
				return;
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06005E22 RID: 24098 RVA: 0x0027B8D2 File Offset: 0x00279CD2
		// (set) Token: 0x06005E23 RID: 24099 RVA: 0x0027B8DA File Offset: 0x00279CDA
		public DoorPoint.OpenPattern OpenState { get; private set; }

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06005E24 RID: 24100 RVA: 0x0027B8E3 File Offset: 0x00279CE3
		public bool IsOpen
		{
			[CompilerGenerated]
			get
			{
				return this.OpenState != DoorPoint.OpenPattern.Close;
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06005E25 RID: 24101 RVA: 0x0027B8F4 File Offset: 0x00279CF4
		public override CommandLabel.CommandInfo[] Labels
		{
			get
			{
				if (Singleton<Manager.Map>.Instance.Player.PlayerController.State is Onbu)
				{
					DoorPoint.OpenPattern openState = this.OpenState;
					if (openState != DoorPoint.OpenPattern.OpenRight && openState != DoorPoint.OpenPattern.OpenLeft)
					{
						return this._sickOpeningLabels;
					}
					return this._sickClosingLabels;
				}
				else
				{
					DoorPoint.OpenPattern openState2 = this.OpenState;
					if (openState2 != DoorPoint.OpenPattern.OpenRight && openState2 != DoorPoint.OpenPattern.OpenLeft)
					{
						return this._openingLabels;
					}
					return this._closingLabels;
				}
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06005E26 RID: 24102 RVA: 0x0027B970 File Offset: 0x00279D70
		public override CommandLabel.CommandInfo[] DateLabels
		{
			get
			{
				DoorPoint.OpenPattern openState = this.OpenState;
				if (openState != DoorPoint.OpenPattern.OpenRight && openState != DoorPoint.OpenPattern.OpenLeft)
				{
					return this._openingLabels;
				}
				return this._closingLabels;
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06005E27 RID: 24103 RVA: 0x0027B9A4 File Offset: 0x00279DA4
		public override bool IsNeutralCommand
		{
			get
			{
				return base.IsNeutralCommand && base.isActiveAndEnabled;
			}
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x0027B9C4 File Offset: 0x00279DC4
		public override bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			Vector3 position = this.DoorAnimation.Animator.transform.position;
			position.y = 0f;
			Vector3 b = basePosition;
			b.y = 0f;
			Vector3 eulerAngles = Quaternion.LookRotation(position - b).eulerAngles;
			Vector3 position2 = base.transform.position;
			position2.y = 0f;
			if (Mathf.Abs(Quaternion.LookRotation(position - position2).eulerAngles.y - eulerAngles.y) > 90f)
			{
				return false;
			}
			if (((this._openType == DoorPoint.OpenTypeState.Right || this._openType == DoorPoint.OpenTypeState.Right90) && this.OpenState == DoorPoint.OpenPattern.OpenLeft) || ((this._openType == DoorPoint.OpenTypeState.Left || this._openType == DoorPoint.OpenTypeState.Left90) && this.OpenState == DoorPoint.OpenPattern.OpenRight))
			{
				if (distance < Singleton<Manager.Resources>.Instance.LocomotionProfile.MinDistanceDoor)
				{
					return false;
				}
				if (distance > Singleton<Manager.Resources>.Instance.LocomotionProfile.MaxDistanceDoor)
				{
					return false;
				}
			}
			else if (distance > radiusB)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06005E29 RID: 24105 RVA: 0x0027BAEC File Offset: 0x00279EEC
		public override bool SetImpossible(bool value, Actor actor)
		{
			if (base.IsImpossible == value)
			{
				return false;
			}
			if (!value && this._actionSlot.Actor != actor)
			{
				return false;
			}
			base.IsImpossible = value;
			if (value)
			{
				base.SetSlot(actor);
			}
			else
			{
				base.ReleaseSlot(actor);
			}
			return true;
		}

		// Token: 0x06005E2A RID: 24106 RVA: 0x0027BB48 File Offset: 0x00279F48
		protected override void InitSub()
		{
			this._offMeshLink = base.GetComponent<OffMeshLink>();
			this.DoorAnimation = base.GetComponent<DoorAnimation>();
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			EventType playerEventMask = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.PlayerEventMask;
			List<CommandLabel.CommandInfo> list = ListPool<CommandLabel.CommandInfo>.Get();
			List<CommandLabel.CommandInfo> list2 = ListPool<CommandLabel.CommandInfo>.Get();
			using (Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>.Enumerator enumerator = ActionPoint.LabelTable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> pair = enumerator.Current;
					DoorPoint $this = this;
					if (this._playerEventType.Contains(pair.Key))
					{
						if (playerEventMask.Contains(pair.Key))
						{
							UnityEx.ValueTuple<int, string> valueTuple;
							if (AIProject.Definitions.Action.NameTable.TryGetValue(pair.Key, out valueTuple))
							{
								ActionPointInfo actionPointInfo = this._playerInfos.Find((ActionPointInfo x) => x.eventTypeMask == pair.Key);
								string actionName = actionPointInfo.actionName;
								Sprite icon2;
								Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(actionPointInfo.iconID, out icon2);
								GameObject gameObject = base.transform.FindLoop(actionPointInfo.labelNullName);
								Transform transform = ((gameObject != null) ? gameObject.transform : null) ?? base.transform;
								if (pair.Key == EventType.DoorOpen)
								{
									list.Add(new CommandLabel.CommandInfo
									{
										Text = actionName,
										Icon = icon2,
										IsHold = false,
										TargetSpriteInfo = icon.ActionSpriteInfo,
										Transform = transform,
										Event = delegate
										{
											pair.Value.Item3(Singleton<Manager.Map>.Instance.Player, $this);
										}
									});
									if (actionPointInfo.doorOpenType > -1)
									{
										this._openType = (DoorPoint.OpenTypeState)actionPointInfo.doorOpenType;
									}
								}
								else if (pair.Key == EventType.DoorClose)
								{
									list2.Add(new CommandLabel.CommandInfo
									{
										Text = actionName,
										Icon = icon2,
										IsHold = false,
										TargetSpriteInfo = icon.ActionSpriteInfo,
										Transform = transform,
										Event = delegate
										{
											pair.Value.Item3(Singleton<Manager.Map>.Instance.Player, $this);
										}
									});
									if (actionPointInfo.doorOpenType > -1)
									{
										this._openType = (DoorPoint.OpenTypeState)actionPointInfo.doorOpenType;
									}
								}
							}
						}
					}
				}
			}
			this._openingLabels = list.ToArray();
			this._closingLabels = list2.ToArray();
			ListPool<CommandLabel.CommandInfo>.Release(list);
			ListPool<CommandLabel.CommandInfo>.Release(list2);
			List<CommandLabel.CommandInfo> list3 = ListPool<CommandLabel.CommandInfo>.Get();
			List<CommandLabel.CommandInfo> list4 = ListPool<CommandLabel.CommandInfo>.Get();
			using (Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>.Enumerator enumerator2 = ActionPoint.SickLabelTable.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> pair = enumerator2.Current;
					DoorPoint $this = this;
					if (this._playerEventType.Contains(pair.Key))
					{
						UnityEx.ValueTuple<int, string> valueTuple2;
						if (AIProject.Definitions.Action.NameTable.TryGetValue(pair.Key, out valueTuple2))
						{
							ActionPointInfo actionPointInfo2 = this._playerInfos.Find((ActionPointInfo x) => x.eventTypeMask == pair.Key);
							string actionName2 = actionPointInfo2.actionName;
							Sprite icon3;
							Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(actionPointInfo2.iconID, out icon3);
							GameObject gameObject2 = base.transform.FindLoop(actionPointInfo2.labelNullName);
							Transform transform2 = ((gameObject2 != null) ? gameObject2.transform : null) ?? base.transform;
							if (pair.Key == EventType.DoorOpen)
							{
								list3.Add(new CommandLabel.CommandInfo
								{
									Text = actionName2,
									Icon = icon3,
									IsHold = false,
									TargetSpriteInfo = icon.ActionSpriteInfo,
									Transform = transform2,
									Event = delegate
									{
										pair.Value.Item3(Singleton<Manager.Map>.Instance.Player, $this);
									}
								});
								if (actionPointInfo2.doorOpenType > -1)
								{
									this._openType = (DoorPoint.OpenTypeState)actionPointInfo2.doorOpenType;
								}
							}
							else if (pair.Key == EventType.DoorClose)
							{
								list4.Add(new CommandLabel.CommandInfo
								{
									Text = actionName2,
									Icon = icon3,
									IsHold = false,
									TargetSpriteInfo = icon.ActionSpriteInfo,
									Transform = transform2,
									Event = delegate
									{
										pair.Value.Item3(Singleton<Manager.Map>.Instance.Player, $this);
									}
								});
								if (actionPointInfo2.doorOpenType > -1)
								{
									this._openType = (DoorPoint.OpenTypeState)actionPointInfo2.doorOpenType;
								}
							}
						}
					}
				}
			}
			this._sickOpeningLabels = list3.ToArray();
			this._sickClosingLabels = list4.ToArray();
			ListPool<CommandLabel.CommandInfo>.Release(list3);
			ListPool<CommandLabel.CommandInfo>.Release(list4);
		}

		// Token: 0x04005413 RID: 21523
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private DoorPoint.OpenTypeState _openType;

		// Token: 0x04005414 RID: 21524
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private NavMeshObstacle _obstacleInOpenRight;

		// Token: 0x04005415 RID: 21525
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private NavMeshObstacle _obstacleInOpenLeft;

		// Token: 0x04005416 RID: 21526
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private NavMeshObstacle _obstacleInClose;

		// Token: 0x04005417 RID: 21527
		[SerializeField]
		private DoorPoint _doorPointFragment;

		// Token: 0x04005418 RID: 21528
		private OffMeshLink _offMeshLink;

		// Token: 0x0400541B RID: 21531
		private CommandLabel.CommandInfo[] _openingLabels;

		// Token: 0x0400541C RID: 21532
		private CommandLabel.CommandInfo[] _closingLabels;

		// Token: 0x0400541D RID: 21533
		private CommandLabel.CommandInfo[] _sickOpeningLabels;

		// Token: 0x0400541E RID: 21534
		private CommandLabel.CommandInfo[] _sickClosingLabels;

		// Token: 0x02000BFE RID: 3070
		public enum OpenTypeState
		{
			// Token: 0x04005420 RID: 21536
			Right,
			// Token: 0x04005421 RID: 21537
			Left,
			// Token: 0x04005422 RID: 21538
			Right90,
			// Token: 0x04005423 RID: 21539
			Left90
		}

		// Token: 0x02000BFF RID: 3071
		public enum OpenPattern
		{
			// Token: 0x04005425 RID: 21541
			Close,
			// Token: 0x04005426 RID: 21542
			OpenRight,
			// Token: 0x04005427 RID: 21543
			OpenLeft
		}
	}
}
