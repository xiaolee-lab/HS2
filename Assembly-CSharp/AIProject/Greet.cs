using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CC9 RID: 3273
	[TaskCategory("")]
	public class Greet : AgentAction
	{
		// Token: 0x060069EB RID: 27115 RVA: 0x002D1748 File Offset: 0x002CFB48
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			if (agent.EquipedItem != null)
			{
				return;
			}
			base.OnStart();
			agent.StateType = State.Type.Greet;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			this.StartActionAnimation();
			this._onEnd.Take(1).Subscribe(delegate(Unit _)
			{
				agent.Animation.StopAllAnimCoroutine();
				bool outEnableFade = this._outEnableFade;
				float outFadeTime = this._outFadeTime;
				agent.Animation.PlayOutAnimation(outEnableFade, outFadeTime, this._layer);
			});
			Quaternion rotation = base.Agent.Rotation;
			Vector3 value = base.Agent.TargetInSightActor.Position - base.Agent.Position;
			value.y = 0f;
			Vector3 forward = Vector3.Normalize(value);
			Quaternion forwardRotation = Quaternion.LookRotation(forward);
			ObservableEasing.Linear(0.2f, false).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> x)
			{
				this.Agent.Rotation = Quaternion.Slerp(rotation, forwardRotation, x.Value);
			});
			agent.AgentData.Greeted = true;
		}

		// Token: 0x060069EC RID: 27116 RVA: 0x002D1850 File Offset: 0x002CFC50
		public override void OnEnd()
		{
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
		}

		// Token: 0x060069ED RID: 27117 RVA: 0x002D1860 File Offset: 0x002CFC60
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.EquipedItem != null)
			{
				return TaskStatus.Success;
			}
			if (agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			if (this._onEnd != null)
			{
				this._onEnd.OnNext(Unit.Default);
			}
			if (agent.Animation.PlayingOutAnimation)
			{
				return TaskStatus.Running;
			}
			agent.TargetInSightActor = null;
			return TaskStatus.Success;
		}

		// Token: 0x060069EE RID: 27118 RVA: 0x002D18CC File Offset: 0x002CFCCC
		private void StartActionAnimation()
		{
			AgentActor agent = base.Agent;
			PoseKeyPair greetPoseID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.GreetPoseID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[greetPoseID.postureID][greetPoseID.poseID];
			agent.Animation.LoadEventKeyTable(greetPoseID.postureID, greetPoseID.poseID);
			this._layer = playState.Layer;
			this._inEnableFade = playState.MainStateInfo.InStateInfo.EnableFade;
			this._inFadeTime = playState.MainStateInfo.InStateInfo.FadeSecond;
			agent.Animation.InitializeStates(playState.MainStateInfo.InStateInfo.StateInfos, playState.MainStateInfo.OutStateInfo.StateInfos, playState.MainStateInfo.AssetBundleInfo);
			agent.Animation.StopAllAnimCoroutine();
			agent.Animation.PlayInAnimation(this._inEnableFade, this._inFadeTime, 1f, this._layer);
		}

		// Token: 0x040059C9 RID: 22985
		private bool _inEnableFade;

		// Token: 0x040059CA RID: 22986
		private float _inFadeTime = 0.1f;

		// Token: 0x040059CB RID: 22987
		private bool _outEnableFade;

		// Token: 0x040059CC RID: 22988
		private float _outFadeTime = 0.1f;

		// Token: 0x040059CD RID: 22989
		private int _layer = -1;

		// Token: 0x040059CE RID: 22990
		private Subject<Unit> _onEnd = new Subject<Unit>();
	}
}
