using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;

namespace AIProject
{
	// Token: 0x02000CE4 RID: 3300
	[TaskCategory("")]
	public class Stand : AgentAction
	{
		// Token: 0x06006A82 RID: 27266 RVA: 0x002D5E74 File Offset: 0x002D4274
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			if (agent.EquipedItem != null)
			{
				ItemCache equipedItem = agent.EquipedItem;
				if (equipedItem.EventItemID == Singleton<Resources>.Instance.LocomotionProfile.ObonEventItemID)
				{
					return;
				}
				ItemIDKeyPair umbrellaID = Singleton<Resources>.Instance.CommonDefine.ItemIDDefine.UmbrellaID;
				EquipEventItemInfo equipEventItemInfo = Singleton<Resources>.Instance.GameInfo.CommonEquipEventItemTable[umbrellaID.categoryID][umbrellaID.itemID];
				if (equipedItem.EventItemID == equipEventItemInfo.EventItemID)
				{
					return;
				}
			}
			base.OnStart();
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			int id = agent.AgentData.SickState.ID;
			this._info = null;
			Dictionary<int, Dictionary<int, PlayState>> agentActionAnimTable = Singleton<Resources>.Instance.Animation.AgentActionAnimTable;
			AgentProfile.PoseIDCollection poseIDTable = Singleton<Resources>.Instance.AgentProfile.PoseIDTable;
			PoseKeyPair? poseKeyPair = null;
			if (id != 0)
			{
				if (id != 4)
				{
					if (agent.AgentData.StatsTable[2] <= 0f)
					{
						poseKeyPair = new PoseKeyPair?(poseIDTable.StandHurtID);
					}
					else
					{
						EnvironmentSimulator simulator = Singleton<Map>.Instance.Simulator;
						Temperature temperature = simulator.Temperature;
						if (temperature == Temperature.Cold)
						{
							poseKeyPair = new PoseKeyPair?(poseIDTable.ColdPoseID);
						}
						else if (temperature == Temperature.Hot)
						{
							poseKeyPair = new PoseKeyPair?(poseIDTable.ColdPoseID);
						}
					}
				}
				else
				{
					poseKeyPair = new PoseKeyPair?(poseIDTable.StandHurtID);
				}
			}
			else
			{
				poseKeyPair = new PoseKeyPair?(poseIDTable.CoughID);
			}
			if (poseKeyPair == null)
			{
				return;
			}
			this._info = agentActionAnimTable[poseKeyPair.Value.postureID][poseKeyPair.Value.poseID];
			if (this._info == null)
			{
				return;
			}
			agent.Animation.LoadEventKeyTable(poseKeyPair.Value.postureID, poseKeyPair.Value.poseID);
			this._layer = this._info.Layer;
			this._inEnableFade = this._info.MainStateInfo.InStateInfo.EnableFade;
			this._inFadeTime = this._info.MainStateInfo.InStateInfo.FadeSecond;
			agent.Animation.OnceActionStates.Clear();
			if (!this._info.MainStateInfo.InStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item in this._info.MainStateInfo.InStateInfo.StateInfos)
				{
					agent.Animation.OnceActionStates.Add(item);
				}
			}
			agent.Animation.OutStates.Clear();
			if (!this._info.MainStateInfo.OutStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item2 in this._info.MainStateInfo.OutStateInfo.StateInfos)
				{
					agent.Animation.OutStates.Enqueue(item2);
				}
			}
			agent.Animation.PlayOnceActionAnimation(this._inEnableFade, this._inFadeTime, this._layer);
			this._onEndActionDisposable = this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				agent.Animation.StopAllAnimCoroutine();
				agent.Animation.PlayOutAnimation(this._outEnableFade, this._outFadeTime, this._layer);
			});
		}

		// Token: 0x06006A83 RID: 27267 RVA: 0x002D6260 File Offset: 0x002D4660
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.EquipedItem != null)
			{
				ItemCache equipedItem = base.Agent.EquipedItem;
				if (equipedItem.EventItemID == Singleton<Resources>.Instance.LocomotionProfile.ObonEventItemID)
				{
					return TaskStatus.Success;
				}
			}
			if (this._info == null)
			{
				return TaskStatus.Success;
			}
			if (base.Agent.Animation.PlayingOnceActionAnimation)
			{
				return TaskStatus.Running;
			}
			if (this._onEndAction != null)
			{
				this._onEndAction.OnNext(Unit.Default);
			}
			if (base.Agent.Animation.PlayingOutAnimation)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06006A84 RID: 27268 RVA: 0x002D6300 File Offset: 0x002D4700
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			agent.Animation.StopOnceActionAnimCoroutine();
			agent.Animation.StopOutAnimCoroutine();
			if (this._onEndActionDisposable != null)
			{
				this._onEndActionDisposable.Dispose();
			}
			agent.ChangeDynamicNavMeshAgentAvoidance();
		}

		// Token: 0x04005A08 RID: 23048
		private int _layer;

		// Token: 0x04005A09 RID: 23049
		private bool _inEnableFade;

		// Token: 0x04005A0A RID: 23050
		private float _inFadeTime;

		// Token: 0x04005A0B RID: 23051
		private bool _outEnableFade;

		// Token: 0x04005A0C RID: 23052
		private float _outFadeTime;

		// Token: 0x04005A0D RID: 23053
		protected Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x04005A0E RID: 23054
		protected IDisposable _onEndActionDisposable;

		// Token: 0x04005A0F RID: 23055
		private PlayState _info;
	}
}
