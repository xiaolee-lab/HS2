using System;
using System.Collections.Generic;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D2C RID: 3372
	[TaskCategory("")]
	public class WaitStoryPoint : AgentAction
	{
		// Token: 0x06006BA2 RID: 27554 RVA: 0x002E2168 File Offset: 0x002E0568
		public override void OnStart()
		{
			base.OnStart();
			this._agent = base.Agent;
			this._animation = this._agent.Animation;
			this._navMeshAgent = this._agent.NavMeshAgent;
			this._point = base.Agent.TargetStoryPoint;
			this._loopStateName = string.Empty;
			this._agent.TutorialCanTalk = false;
			if (this._missing = (this._agent == null || this._animation == null || this._navMeshAgent == null || this._point == null))
			{
				return;
			}
			if (this._adjustAngle)
			{
				this.PlayTurnAnimation();
			}
			else
			{
				this.PlayIdleAnimation();
			}
			this._agent.DeactivateNavMeshAgent();
		}

		// Token: 0x06006BA3 RID: 27555 RVA: 0x002E2249 File Offset: 0x002E0649
		public override TaskStatus OnUpdate()
		{
			if (this._missing)
			{
				return TaskStatus.Failure;
			}
			if (this._agent.Animation.PlayingTurnAnimation)
			{
				return TaskStatus.Running;
			}
			this._agent.TutorialCanTalk = this.CanTalk();
			return TaskStatus.Running;
		}

		// Token: 0x06006BA4 RID: 27556 RVA: 0x002E2284 File Offset: 0x002E0684
		public override void OnEnd()
		{
			if (this._agent != null)
			{
				this._agent.ClearItems();
				this._agent.ClearParticles();
				this._agent.TutorialCanTalk = false;
			}
			else
			{
				this._agent = base.Agent;
				this._agent.ClearItems();
				this._agent.ClearParticles();
				this._agent.TutorialCanTalk = false;
			}
			base.OnEnd();
		}

		// Token: 0x06006BA5 RID: 27557 RVA: 0x002E2300 File Offset: 0x002E0700
		private bool CanTalk()
		{
			Tutorial.ActionType tutorialType = this._agent.TutorialType;
			if (tutorialType != Tutorial.ActionType.PassFishingRod && tutorialType != Tutorial.ActionType.FoodRequest && tutorialType != Tutorial.ActionType.WaitAtBase && tutorialType != Tutorial.ActionType.GrilledFishRequest)
			{
				return false;
			}
			switch (tutorialType)
			{
			case Tutorial.ActionType.PassFishingRod:
				return true;
			case Tutorial.ActionType.FoodRequest:
				if (Singleton<Manager.Map>.IsInstance() && Singleton<Manager.Resources>.IsInstance() && Manager.Map.GetTutorialProgress() == 7)
				{
					Manager.Map instance = Singleton<Manager.Map>.Instance;
					List<StuffItem> list;
					if (instance == null)
					{
						list = null;
					}
					else
					{
						PlayerActor player = instance.Player;
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
					List<StuffItem> list2 = list;
					Manager.Resources instance2 = Singleton<Manager.Resources>.Instance;
					List<FishingDefinePack.ItemIDPair> list3;
					if (instance2 == null)
					{
						list3 = null;
					}
					else
					{
						FishingDefinePack fishingDefinePack = instance2.FishingDefinePack;
						if (fishingDefinePack == null)
						{
							list3 = null;
						}
						else
						{
							FishingDefinePack.IDGroup idinfo = fishingDefinePack.IDInfo;
							list3 = ((idinfo != null) ? idinfo.FishList : null);
						}
					}
					List<FishingDefinePack.ItemIDPair> list4 = list3;
					if (list2.IsNullOrEmpty<StuffItem>() || list4.IsNullOrEmpty<FishingDefinePack.ItemIDPair>())
					{
						return false;
					}
					using (List<FishingDefinePack.ItemIDPair>.Enumerator enumerator = list4.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							FishingDefinePack.ItemIDPair fishID = enumerator.Current;
							if (list2.Exists((StuffItem x) => x.CategoryID == fishID.CategoryID && x.ID == fishID.ItemID && 0 < x.Count))
							{
								return true;
							}
						}
					}
					break;
				}
				break;
			case Tutorial.ActionType.WaitAtBase:
				return true;
			case Tutorial.ActionType.GrilledFishRequest:
				if (Singleton<Manager.Map>.IsInstance() && Singleton<Manager.Resources>.IsInstance() && Manager.Map.GetTutorialProgress() == 13)
				{
					WaitStoryPoint.<CanTalk>c__AnonStorey1 <CanTalk>c__AnonStorey2 = new WaitStoryPoint.<CanTalk>c__AnonStorey1();
					Manager.Map instance3 = Singleton<Manager.Map>.Instance;
					List<StuffItem> list5;
					if (instance3 == null)
					{
						list5 = null;
					}
					else
					{
						PlayerActor player2 = instance3.Player;
						if (player2 == null)
						{
							list5 = null;
						}
						else
						{
							PlayerData playerData2 = player2.PlayerData;
							list5 = ((playerData2 != null) ? playerData2.ItemList : null);
						}
					}
					List<StuffItem> list6 = list5;
					WaitStoryPoint.<CanTalk>c__AnonStorey1 <CanTalk>c__AnonStorey3 = <CanTalk>c__AnonStorey2;
					Manager.Resources instance4 = Singleton<Manager.Resources>.Instance;
					FishingDefinePack.ItemIDPair? grilledFishID;
					if (instance4 == null)
					{
						grilledFishID = null;
					}
					else
					{
						FishingDefinePack fishingDefinePack2 = instance4.FishingDefinePack;
						if (fishingDefinePack2 == null)
						{
							grilledFishID = null;
						}
						else
						{
							FishingDefinePack.IDGroup idinfo2 = fishingDefinePack2.IDInfo;
							grilledFishID = ((idinfo2 != null) ? new FishingDefinePack.ItemIDPair?(idinfo2.GrilledFish) : null);
						}
					}
					<CanTalk>c__AnonStorey3.grilledFishID = grilledFishID;
					return !list6.IsNullOrEmpty<StuffItem>() && <CanTalk>c__AnonStorey2.grilledFishID != null && list6.Exists((StuffItem x) => x.CategoryID == <CanTalk>c__AnonStorey2.grilledFishID.Value.CategoryID && x.ID == <CanTalk>c__AnonStorey2.grilledFishID.Value.ItemID && 0 < x.Count);
				}
				break;
			}
			return false;
		}

		// Token: 0x06006BA6 RID: 27558 RVA: 0x002E2568 File Offset: 0x002E0968
		private void PlayTurnAnimation()
		{
			PlayState.AnimStateInfo tutorialPersonalIdleState = this._agent.GetTutorialPersonalIdleState();
			Vector3 vector = this._point.Forward * 30f + this._agent.Position;
			float num = Vector3.Angle(vector - this._agent.Position, this._agent.Forward);
			if (Singleton<Manager.Resources>.IsInstance())
			{
				float turnEnableAngle = Singleton<Manager.Resources>.Instance.LocomotionProfile.TurnEnableAngle;
				if (num < turnEnableAngle)
				{
					this._agent.Locomotor.transform.LookAt(vector, Vector3.up);
					Vector3 eulerAngles = this._agent.Locomotor.transform.eulerAngles;
					eulerAngles.x = (eulerAngles.z = 0f);
					this._agent.Locomotor.transform.eulerAngles = eulerAngles;
					return;
				}
			}
			this._agent.Animation.StopAllAnimCoroutine();
			this._animation.PlayTurnAnimation(vector, 1f, tutorialPersonalIdleState, false);
		}

		// Token: 0x06006BA7 RID: 27559 RVA: 0x002E2670 File Offset: 0x002E0A70
		private void PlayIdleAnimation()
		{
			if (Singleton<Manager.Resources>.IsInstance())
			{
				PoseKeyPair poseKeyPair;
				if (!this.TryGetPoseID(this._agent.ChaControl.fileParam.personality, out poseKeyPair))
				{
					return;
				}
				PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[poseKeyPair.postureID][poseKeyPair.poseID];
				this._loopStateName = playState.MainStateInfo.InStateInfo.StateInfos.GetElement(playState.MainStateInfo.InStateInfo.StateInfos.Length - 1).stateName;
				if (this._agent.Animation.Animator.GetCurrentAnimatorStateInfo(0).IsName(this._loopStateName))
				{
					return;
				}
				this._animation.InitializeStates(playState);
				this._animation.LoadEventKeyTable(poseKeyPair.postureID, poseKeyPair.poseID);
				ActorAnimInfo actorAnimInfo = new ActorAnimInfo
				{
					layer = playState.Layer,
					inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
					inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
					outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
					outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
					directionType = playState.DirectionType,
					isLoop = playState.MainStateInfo.IsLoop,
					endEnableBlend = playState.EndEnableBlend,
					endBlendSec = playState.EndBlendRate
				};
				this._animation.AnimInfo = actorAnimInfo;
				ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
				this._animation.StopAllAnimCoroutine();
				this._animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, actorAnimInfo2.inFadeOutTime, actorAnimInfo2.layer);
			}
		}

		// Token: 0x06006BA8 RID: 27560 RVA: 0x002E284C File Offset: 0x002E0C4C
		private bool TryGetPoseID(int personalID, out PoseKeyPair pair)
		{
			Dictionary<int, PoseKeyPair> tutorialIdlePoseTable = Singleton<Manager.Resources>.Instance.AgentProfile.TutorialIdlePoseTable;
			if (tutorialIdlePoseTable.IsNullOrEmpty<int, PoseKeyPair>())
			{
				pair = default(PoseKeyPair);
				return false;
			}
			if (tutorialIdlePoseTable.TryGetValue(personalID, out pair))
			{
				return true;
			}
			if (tutorialIdlePoseTable.TryGetValue(0, out pair))
			{
				return true;
			}
			pair = default(PoseKeyPair);
			return false;
		}

		// Token: 0x04005A9C RID: 23196
		[SerializeField]
		private bool _adjustAngle;

		// Token: 0x04005A9D RID: 23197
		private AgentActor _agent;

		// Token: 0x04005A9E RID: 23198
		private ActorAnimation _animation;

		// Token: 0x04005A9F RID: 23199
		private NavMeshAgent _navMeshAgent;

		// Token: 0x04005AA0 RID: 23200
		private StoryPoint _point;

		// Token: 0x04005AA1 RID: 23201
		private string _loopStateName = string.Empty;

		// Token: 0x04005AA2 RID: 23202
		private bool _missing;
	}
}
