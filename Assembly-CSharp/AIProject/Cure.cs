using System;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CBA RID: 3258
	[TaskCategory("")]
	public class Cure : AgentAction
	{
		// Token: 0x060069B5 RID: 27061 RVA: 0x002D035C File Offset: 0x002CE75C
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StateType = State.Type.Immobility;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			PoseKeyPair cureID = Singleton<Resources>.Instance.AgentProfile.PoseIDTable.CureID;
			PlayState playState = Singleton<Resources>.Instance.Animation.AgentActionAnimTable[cureID.postureID][cureID.poseID];
			agent.Animation.LoadEventKeyTable(cureID.postureID, cureID.poseID);
			this._layer = playState.Layer;
			this._inEnableFade = playState.MainStateInfo.InStateInfo.EnableFade;
			this._inFadeSecond = playState.MainStateInfo.InStateInfo.FadeSecond;
			agent.Animation.InitializeStates(playState);
			agent.Animation.PlayInAnimation(this._inEnableFade, this._inFadeSecond, playState.MainStateInfo.FadeOutTime, this._layer);
		}

		// Token: 0x060069B6 RID: 27062 RVA: 0x002D044B File Offset: 0x002CE84B
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			this.OnComplete(base.Agent);
			return TaskStatus.Success;
		}

		// Token: 0x060069B7 RID: 27063 RVA: 0x002D0474 File Offset: 0x002CE874
		private void OnComplete(AgentActor agent)
		{
			ItemIDKeyPair gauzeID = Singleton<Resources>.Instance.CommonDefine.ItemIDDefine.GauzeID;
			StuffItem stuffItem = agent.AgentData.ItemList.Find((StuffItem x) => x.CategoryID == gauzeID.categoryID && x.ID == gauzeID.itemID);
			StuffItem item = new StuffItem(stuffItem.CategoryID, stuffItem.ID, 1);
			agent.AgentData.ItemList.RemoveItem(item);
		}

		// Token: 0x060069B8 RID: 27064 RVA: 0x002D04E3 File Offset: 0x002CE8E3
		public override void OnEnd()
		{
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
		}

		// Token: 0x040059B6 RID: 22966
		private int _layer;

		// Token: 0x040059B7 RID: 22967
		private bool _inEnableFade;

		// Token: 0x040059B8 RID: 22968
		private float _inFadeSecond;
	}
}
