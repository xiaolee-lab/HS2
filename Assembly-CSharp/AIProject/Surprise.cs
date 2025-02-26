using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CE7 RID: 3303
	[TaskCategory("")]
	public class Surprise : AgentAction
	{
		// Token: 0x06006A8D RID: 27277 RVA: 0x002D6984 File Offset: 0x002D4D84
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			agent.HPositionID = agent.HPositionSubID;
			agent.Animation.StopAllAnimCoroutine();
			agent.Animation.InStates.Clear();
			if (agent.SurprisePoseID != null)
			{
				PoseKeyPair value = agent.SurprisePoseID.Value;
				PlayState playState = Singleton<Resources>.Instance.Animation.AgentActionAnimTable[value.postureID][value.poseID];
				agent.Animation.LoadEventKeyTable(value.postureID, value.poseID);
				if (!playState.MainStateInfo.InStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
				{
					foreach (PlayState.Info item in playState.MainStateInfo.InStateInfo.StateInfos)
					{
						agent.Animation.InStates.Enqueue(item);
					}
				}
			}
			agent.Animation.OutStates.Clear();
			agent.MotivationInEncounter = agent.AgentData.StatsTable[5];
			List<PlayState.ItemInfo> itemList;
			if (Singleton<Resources>.Instance.Animation.SurpriseItemList.TryGetValue(agent.Animation.Animator.name, out itemList))
			{
				agent.LoadEventItems(itemList);
			}
			agent.Animation.PlayInLocoAnimation(false, 0f, 0);
			agent.UpdateMotivation = true;
		}

		// Token: 0x06006A8E RID: 27278 RVA: 0x002D6B0C File Offset: 0x002D4F0C
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.SurprisePoseID == null)
			{
				return TaskStatus.Success;
			}
			if (agent.Animation.PlayingInLocoAnimation)
			{
				return TaskStatus.Running;
			}
			if (agent.MotivationInEncounter <= 0f)
			{
				this.Complete();
				return TaskStatus.Success;
			}
			if (agent.ReleasableCommand && agent.IsFarPlayerInSurprise)
			{
				this.Complete();
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006A8F RID: 27279 RVA: 0x002D6B80 File Offset: 0x002D4F80
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.Animation.CrossFadeScreen(-1f);
			agent.SetStand(agent.Animation.RecoveryPoint, false, 0f, 0);
			agent.Animation.RecoveryPoint = null;
			agent.ResetActionFlag();
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
				agent.PrevActionPoint = agent.TargetInSightActionPoint;
			}
			agent.TargetInSightActionPoint = null;
			agent.SurprisePoseID = null;
			Desire.Type key;
			if (Desire.ModeTable.TryGetValue(agent.PrevMode, out key))
			{
				int desireKey = Desire.GetDesireKey(key);
				agent.SetDesire(desireKey, 0f);
			}
			agent.Animation.ResetDefaultAnimatorController();
			agent.ChaControl.SetClothesStateAll(0);
			agent.ActivateNavMeshAgent();
		}

		// Token: 0x06006A90 RID: 27280 RVA: 0x002D6C68 File Offset: 0x002D5068
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			agent.ClearItems();
			agent.ChangeDynamicNavMeshAgentAvoidance();
			agent.UpdateMotivation = false;
		}

		// Token: 0x06006A91 RID: 27281 RVA: 0x002D6C90 File Offset: 0x002D5090
		public override void OnPause(bool paused)
		{
			AgentActor agent = base.Agent;
			if (paused)
			{
				this._updatedMotivation = paused;
				agent.UpdateMotivation = false;
			}
			else
			{
				agent.UpdateMotivation = this._updatedMotivation;
			}
		}

		// Token: 0x04005A14 RID: 23060
		private bool _updatedMotivation;
	}
}
