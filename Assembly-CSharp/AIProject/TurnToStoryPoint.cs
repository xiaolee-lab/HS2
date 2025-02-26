using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D28 RID: 3368
	[TaskCategory("")]
	public class TurnToStoryPoint : AgentAction
	{
		// Token: 0x06006B93 RID: 27539 RVA: 0x002E1E38 File Offset: 0x002E0238
		public override void OnStart()
		{
			base.OnStart();
			this._agent = base.Agent;
			this._point = ((this._agent != null) ? this._agent.TargetStoryPoint : null);
			this._path = new NavMeshPath();
			if (this._missing = (this._agent == null || this._point == null))
			{
				return;
			}
			NavMesh.CalculatePath(this._agent.Position, this._point.Position, this._agent.NavMeshAgent.areaMask, this._path);
			if (this._missing = (this._path.status != NavMeshPathStatus.PathComplete))
			{
				return;
			}
			this._targetPoint = this._path.corners.GetElement(1);
			PlayState.AnimStateInfo idleStateInfo = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.IdleStateInfo;
			this._agent.Animation.PlayTurnAnimation(this._targetPoint, 1f, idleStateInfo, false);
			this._agent.TutorialLocomoCaseID = 100;
			this._changeTime = Singleton<Manager.Resources>.Instance.CommonDefine.Tutorial.WalkToRunTime;
		}

		// Token: 0x06006B94 RID: 27540 RVA: 0x002E1F74 File Offset: 0x002E0374
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
			Observable.Timer(TimeSpan.FromSeconds((double)this._changeTime)).TakeUntilDestroy(this._agent).Subscribe(delegate(long _)
			{
				this._agent.TutorialLocomoCaseID = 101;
			});
			return TaskStatus.Success;
		}

		// Token: 0x04005A92 RID: 23186
		private AgentActor _agent;

		// Token: 0x04005A93 RID: 23187
		private StoryPoint _point;

		// Token: 0x04005A94 RID: 23188
		private bool _missing = true;

		// Token: 0x04005A95 RID: 23189
		private Vector3 _targetPoint = Vector3.zero;

		// Token: 0x04005A96 RID: 23190
		private NavMeshPath _path;

		// Token: 0x04005A97 RID: 23191
		private float _changeTime;
	}
}
