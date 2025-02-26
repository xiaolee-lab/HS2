using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AIProject.Definitions;
using AIProject.Player;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000CD9 RID: 3289
	[TaskCategory("")]
	public class PhotoPose : AgentAction
	{
		// Token: 0x06006A52 RID: 27218 RVA: 0x002D3E80 File Offset: 0x002D2280
		public override void OnStart()
		{
			base.OnStart();
			this._agent = base.Agent;
			this._agent.RuntimeMotivationInPhoto = this._agent.AgentData.StatsTable[5];
			this._prevStateType = this._agent.StateType;
			this._agent.StateType = State.Type.Immobility;
			this._poseIDList = ListPool<int>.Get();
			NavMeshAgent navMeshAgent = this._agent.NavMeshAgent;
			navMeshAgent.isStopped = true;
			this._prevPriority = navMeshAgent.avoidancePriority;
			navMeshAgent.avoidancePriority = Singleton<Manager.Resources>.Instance.AgentProfile.AvoidancePriorityStationary;
			this._player = Singleton<Manager.Map>.Instance.Player;
			this._playerCameraCon = this._player.CameraControl;
			this._prevShotType = this._playerCameraCon.ShotType;
			this._isFadeOut = false;
			this._poseStateTable = null;
			this._poseInfo = new UnityEx.ValueTuple<PoseKeyPair, bool>(default(PoseKeyPair), false);
			int personality = this._agent.ChaControl.fileParam.personality;
			Dictionary<int, Dictionary<int, UnityEx.ValueTuple<PoseKeyPair, bool>>> agentGravurePoseTable = Singleton<Manager.Resources>.Instance.Animation.AgentGravurePoseTable;
			Dictionary<int, UnityEx.ValueTuple<PoseKeyPair, bool>> dictionary;
			if (agentGravurePoseTable.TryGetValue(personality, out dictionary) && !dictionary.IsNullOrEmpty<int, UnityEx.ValueTuple<PoseKeyPair, bool>>())
			{
				this._poseStateTable = new ReadOnlyDictionary<int, UnityEx.ValueTuple<PoseKeyPair, bool>>(dictionary);
				this._poseIDList.AddRange(this._poseStateTable.Keys);
				this._lastPoseID = this._poseIDList[UnityEngine.Random.Range(0, this._poseIDList.Count)];
				this._poseStateTable.TryGetValue(this._lastPoseID, out this._poseInfo);
			}
			this._poseOutAnimAction = new Subject<Unit>();
			this._poseOutAnimAction.TakeUntilDestroy(this._agent).Take(1).Subscribe(delegate(Unit _)
			{
				this._isFadeOut = true;
				this.PlayOutAnimation(this._poseInfo);
				this._isWait = ((AgentActor actor) => actor.Animation.PlayingOutAnimation);
			});
			this._poseReplayAnimAction = new Subject<Unit>();
			this._poseReplayAnimAction.TakeUntilDestroy(this._agent).Subscribe(delegate(Unit _)
			{
				if (this._poseIDList.IsNullOrEmpty<int>())
				{
					this._poseOutAnimAction.OnNext(Unit.Default);
					return;
				}
				if (this._poseIDList.Count == 1)
				{
					this._agent.ClearItems();
					this.PlayInAnimation(this._poseInfo);
				}
				else
				{
					List<int> list = ListPool<int>.Get();
					list.AddRange(this._poseIDList);
					list.Remove(this._lastPoseID);
					this._lastPoseID = list[UnityEngine.Random.Range(0, list.Count)];
					ListPool<int>.Release(list);
					this._poseStateTable.TryGetValue(this._lastPoseID, out this._poseInfo);
					this._agent.ClearItems();
					this.PlayInAnimation(this._poseInfo);
				}
				this._isWait = ((AgentActor actor) => actor.Animation.PlayingInAnimation);
				this._onEndAction = null;
			});
			this._poseLoopEndAnimAction = new Subject<Unit>();
			this._poseLoopEndAnimAction.TakeUntilDestroy(this._agent).Subscribe(delegate(Unit _)
			{
				this.PlayOutAnimation(this._poseInfo);
				this._isWait = ((AgentActor actor) => actor.Animation.PlayingOutAnimation);
				this._onEndAction = this._poseReplayAnimAction;
			});
			this.PlayInAnimation(this._poseInfo);
			this._isWait = ((AgentActor actor) => actor.Animation.PlayingInAnimation);
			this._onEndAction = null;
		}

		// Token: 0x06006A53 RID: 27219 RVA: 0x002D40D4 File Offset: 0x002D24D4
		private PlayState GetPlayState(PoseKeyPair poseKey)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return null;
			}
			Dictionary<int, Dictionary<int, PlayState>> agentActionAnimTable = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable;
			Dictionary<int, PlayState> dictionary;
			PlayState result;
			if (agentActionAnimTable.TryGetValue(poseKey.postureID, out dictionary) && dictionary.TryGetValue(poseKey.poseID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06006A54 RID: 27220 RVA: 0x002D4128 File Offset: 0x002D2528
		private void PlayInAnimation(UnityEx.ValueTuple<PoseKeyPair, bool> poseInfo)
		{
			this._loopCounter = 0f;
			this._loopTime = 0f;
			PlayState playState = this.GetPlayState(poseInfo.Item1);
			if (playState != null && this._agent != null)
			{
				this._agent.Animation.StopAllAnimCoroutine();
				this._agent.Animation.InitializeStates(playState);
				this._agent.Animation.PlayInAnimation(playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.MainStateInfo.FadeOutTime, playState.Layer);
				this._loopTime = UnityEngine.Random.Range((float)playState.MainStateInfo.LoopMin, (float)playState.MainStateInfo.LoopMax);
				this._agent.Animation.LoadEventKeyTable(poseInfo.Item1.postureID, poseInfo.Item1.poseID);
				this._agent.LoadEventItems(playState);
			}
			bool lookCamera = this._lookCamera;
			Transform transform = this.CameraTarget(this._prevShotType);
			this._lookCamera = (poseInfo.Item2 && this._agent != null && transform != null);
			if (this._lookCamera)
			{
				this._agent.ChaControl.ChangeLookNeckTarget(1, transform, 0.5f, 0f, 1f, 0.8f);
				this._agent.ChaControl.ChangeLookNeckPtn(1, 1f);
				this._agent.ChaControl.ChangeLookEyesTarget(1, transform, 0.5f, 0f, 1f, 2f);
				this._agent.ChaControl.ChangeLookEyesPtn(1);
			}
			else if (lookCamera && this._agent != null)
			{
				this._agent.ChaControl.ChangeLookNeckPtn(3, 1f);
				this._agent.ChaControl.ChangeLookEyesPtn(3);
			}
		}

		// Token: 0x06006A55 RID: 27221 RVA: 0x002D432C File Offset: 0x002D272C
		private void PlayOutAnimation(UnityEx.ValueTuple<PoseKeyPair, bool> poseInfo)
		{
			PlayState playState = this.GetPlayState(poseInfo.Item1);
			if (playState != null)
			{
				this._agent.Animation.StopAllAnimCoroutine();
				this._agent.Animation.PlayOutAnimation(playState.MainStateInfo.OutStateInfo.EnableFade, playState.MainStateInfo.OutStateInfo.FadeSecond, playState.Layer);
			}
		}

		// Token: 0x06006A56 RID: 27222 RVA: 0x002D4394 File Offset: 0x002D2794
		private bool EndState()
		{
			return (this._agent.ReleasableCommand && this._agent.IsFarPlayerByPhotoMode) || !(this._player.PlayerController.State is Photo) || (this._agent.RuntimeMotivationInPhoto <= 0f && false);
		}

		// Token: 0x06006A57 RID: 27223 RVA: 0x002D43F8 File Offset: 0x002D27F8
		private Transform CameraTarget()
		{
			ShotType shotType = this._playerCameraCon.ShotType;
			return (shotType != ShotType.PointOfView) ? this._player.FovTargetPointTable[Actor.FovBodyPart.Head] : this._playerCameraCon.CameraComponent.transform;
		}

		// Token: 0x06006A58 RID: 27224 RVA: 0x002D443E File Offset: 0x002D283E
		private Transform CameraTarget(ShotType shotType)
		{
			return (shotType != ShotType.PointOfView) ? this._player.FovTargetPointTable[Actor.FovBodyPart.Head] : this._playerCameraCon.CameraComponent.transform;
		}

		// Token: 0x06006A59 RID: 27225 RVA: 0x002D4470 File Offset: 0x002D2870
		public override TaskStatus OnUpdate()
		{
			ShotType shotType = this._playerCameraCon.ShotType;
			if (this._prevShotType != shotType)
			{
				if (this._lookCamera)
				{
					Transform trfTarg = this.CameraTarget(shotType);
					this._agent.ChaControl.ChangeLookNeckTarget(1, trfTarg, 0.5f, 0f, 1f, 0.8f);
					this._agent.ChaControl.ChangeLookNeckPtn(1, 1f);
					this._agent.ChaControl.ChangeLookEyesTarget(1, trfTarg, 0.5f, 0f, 1f, 2f);
					this._agent.ChaControl.ChangeLookEyesPtn(1);
				}
				this._prevShotType = shotType;
			}
			TaskStatus taskStatus = this.Update();
			if (taskStatus != TaskStatus.Running)
			{
				this._agent.Animation.StopAllAnimCoroutine();
			}
			return taskStatus;
		}

		// Token: 0x06006A5A RID: 27226 RVA: 0x002D4544 File Offset: 0x002D2944
		private TaskStatus Update()
		{
			if (this._isFadeOut)
			{
				if (this._agent.Animation.PlayingOutAnimation)
				{
					return TaskStatus.Running;
				}
				this._agent.ClearItems();
				return TaskStatus.Success;
			}
			else
			{
				bool? flag = (this._isWait != null) ? new bool?(this._isWait(this._agent)) : null;
				if (flag != null && flag.Value)
				{
					return TaskStatus.Running;
				}
				if (this._onEndAction != null)
				{
					Subject<Unit> onEndAction = this._onEndAction;
					this._onEndAction = null;
					onEndAction.OnNext(Unit.Default);
					return TaskStatus.Running;
				}
				if (this.EndState())
				{
					this._poseOutAnimAction.OnNext(Unit.Default);
					return TaskStatus.Running;
				}
				this._loopCounter += Time.deltaTime;
				if (this._loopTime <= this._loopCounter)
				{
					this._loopCounter = this._loopTime + 1f;
					if (this._poseLoopEndAnimAction != null)
					{
						this._poseLoopEndAnimAction.OnNext(Unit.Default);
					}
					return TaskStatus.Running;
				}
				return TaskStatus.Running;
			}
		}

		// Token: 0x06006A5B RID: 27227 RVA: 0x002D4668 File Offset: 0x002D2A68
		public override void OnEnd()
		{
			NavMeshAgent navMeshAgent = this._agent.NavMeshAgent;
			navMeshAgent.avoidancePriority = this._prevPriority;
			this._agent.StateType = this._prevStateType;
			if (this._lookCamera)
			{
				if (this._agent != null)
				{
					this._agent.ChaControl.ChangeLookNeckPtn(3, 1f);
					this._agent.ChaControl.ChangeLookEyesPtn(3);
				}
				this._lookCamera = false;
			}
			this._agent.Animation.StopAllAnimCoroutine();
			ListPool<int>.Release(this._poseIDList);
			base.OnEnd();
		}

		// Token: 0x040059E6 RID: 23014
		private PlayerActor _player;

		// Token: 0x040059E7 RID: 23015
		private AgentActor _agent;

		// Token: 0x040059E8 RID: 23016
		private int _prevPriority = 50;

		// Token: 0x040059E9 RID: 23017
		private State.Type _prevStateType = State.Type.Normal;

		// Token: 0x040059EA RID: 23018
		private UnityEx.ValueTuple<PoseKeyPair, bool> _poseInfo = default(UnityEx.ValueTuple<PoseKeyPair, bool>);

		// Token: 0x040059EB RID: 23019
		private bool _lookCamera;

		// Token: 0x040059EC RID: 23020
		private float _loopTime = float.MaxValue;

		// Token: 0x040059ED RID: 23021
		private float _loopCounter;

		// Token: 0x040059EE RID: 23022
		private ReadOnlyDictionary<int, UnityEx.ValueTuple<PoseKeyPair, bool>> _poseStateTable;

		// Token: 0x040059EF RID: 23023
		private List<int> _poseIDList;

		// Token: 0x040059F0 RID: 23024
		private int _lastPoseID = -1;

		// Token: 0x040059F1 RID: 23025
		private ActorCameraControl _playerCameraCon;

		// Token: 0x040059F2 RID: 23026
		private ShotType _prevShotType = ShotType.PointOfView;

		// Token: 0x040059F3 RID: 23027
		private Subject<Unit> _onEndAction;

		// Token: 0x040059F4 RID: 23028
		private Subject<Unit> _poseOutAnimAction;

		// Token: 0x040059F5 RID: 23029
		private Subject<Unit> _poseReplayAnimAction;

		// Token: 0x040059F6 RID: 23030
		private Subject<Unit> _poseLoopEndAnimAction;

		// Token: 0x040059F7 RID: 23031
		private Func<AgentActor, bool> _isWait;

		// Token: 0x040059F8 RID: 23032
		private bool _isFadeOut;
	}
}
