using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C97 RID: 3223
	[TaskCategory("")]
	public class YandereWarp : AgentAction
	{
		// Token: 0x06006915 RID: 26901 RVA: 0x002CAB78 File Offset: 0x002C8F78
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StateType = State.Type.Immobility;
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x002CAB9C File Offset: 0x002C8F9C
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			Vector3 position = player.Position;
			Vector3 position2 = player.CameraControl.CameraComponent.transform.position;
			position2.y = position.y;
			Quaternion rotation = Quaternion.Euler(0f, player.CameraControl.CinemachineBrain.transform.rotation.eulerAngles.y, 0f);
			Vector3 a = rotation * Vector3.back;
			Vector3 vector = position2 + a * Singleton<Manager.Resources>.Instance.AgentProfile.YandereWarpPosCorrect;
			Quaternion rotation2 = Quaternion.LookRotation(position - vector);
			agent.AgentData.YandereWarpLimitation = true;
			float num = UnityEngine.Random.value * 100f;
			if (num > Singleton<Manager.Resources>.Instance.AgentProfile.YandereWarpProb)
			{
				return TaskStatus.Success;
			}
			int num2 = agent.TryNavMeshWarp(vector, rotation2, 0, 100f);
			if (num2 == -1)
			{
				agent.YandereWarpRetryReserve();
			}
			return TaskStatus.Success;
		}
	}
}
