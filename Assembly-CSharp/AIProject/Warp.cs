using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C96 RID: 3222
	[TaskCategory("")]
	public class Warp : AgentAction
	{
		// Token: 0x06006910 RID: 26896 RVA: 0x002CA880 File Offset: 0x002C8C80
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StateType = State.Type.Immobility;
		}

		// Token: 0x06006911 RID: 26897 RVA: 0x002CA8A4 File Offset: 0x002C8CA4
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			if (agent.CurrentPoint == null)
			{
				this.ClearDesire(agent);
				return TaskStatus.Success;
			}
			agent.CurrentPoint.SetActiveMapItemObjs(false);
			WarpPoint warpPoint = agent.CurrentPoint as WarpPoint;
			if (warpPoint == null)
			{
				agent.CurrentPoint = null;
				this.ClearDesire(agent);
				return TaskStatus.Success;
			}
			WarpPoint warpPoint2 = warpPoint.PairPoint();
			Renderer[] renderers = warpPoint2.Renderers;
			bool flag = false;
			if (!renderers.IsNullOrEmpty<Renderer>())
			{
				foreach (Renderer renderer in renderers)
				{
					if (renderer.isVisible)
					{
						flag = true;
						break;
					}
				}
			}
			bool isVisibleInCamera = agent.ChaControl.IsVisibleInCamera;
			if (isVisibleInCamera)
			{
				ActorCameraControl cameraControl = Singleton<Manager.Map>.Instance.Player.CameraControl;
				float num = Vector3.Distance(agent.Position, cameraControl.transform.position);
				float crossFadeEnableDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.CrossFadeEnableDistance;
				if (num <= crossFadeEnableDistance)
				{
					cameraControl.CrossFade.FadeStart(-1f);
				}
			}
			else if (flag)
			{
				Singleton<Manager.Map>.Instance.Player.CameraControl.CrossFade.FadeStart(-1f);
			}
			this.PlayWarpSE(400, warpPoint.transform);
			this.PlayWarpSE(401, warpPoint2.transform);
			agent.NavMeshWarp(warpPoint2.transform, 0, 100f);
			this.ClearDesire(agent);
			return TaskStatus.Success;
		}

		// Token: 0x06006912 RID: 26898 RVA: 0x002CAA64 File Offset: 0x002C8E64
		private void PlayWarpSE(int clipID, Transform t)
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Camera cameraComponent = Manager.Map.GetCameraComponent();
			Transform transform = (cameraComponent != null) ? cameraComponent.transform : null;
			if (transform == null)
			{
				return;
			}
			SoundPack soundPack = Singleton<Manager.Resources>.Instance.SoundPack;
			SoundPack.Data3D data3D;
			if (!soundPack.TryGetActionSEData(clipID, out data3D))
			{
				return;
			}
			Vector3 position = t.position;
			float num = Mathf.Pow(data3D.MaxDistance + soundPack.Game3DInfo.MarginMaxDistance, 2f);
			float sqrMagnitude = (position - transform.position).sqrMagnitude;
			if (num < sqrMagnitude)
			{
				return;
			}
			AudioSource audioSource = soundPack.Play(data3D, Sound.Type.GameSE3D, 0f);
			if (audioSource == null)
			{
				return;
			}
			audioSource.Stop();
			audioSource.transform.position = position;
			audioSource.Play();
		}

		// Token: 0x06006913 RID: 26899 RVA: 0x002CAB48 File Offset: 0x002C8F48
		private void ClearDesire(AgentActor agent)
		{
			int desireKey = Desire.GetDesireKey(Desire.Type.Game);
			agent.SetDesire(desireKey, 0f);
		}
	}
}
