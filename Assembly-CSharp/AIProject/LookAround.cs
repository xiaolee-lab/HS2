using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CD5 RID: 3285
	[TaskCategory("")]
	public class LookAround : AgentAction
	{
		// Token: 0x06006A3B RID: 27195 RVA: 0x002D2C50 File Offset: 0x002D1050
		public override void OnStart()
		{
			base.OnStart();
			base.Agent.StopNavMeshAgent();
			base.Agent.AnimalFovAngleOffsetY = 0f;
			int id = base.Agent.AgentData.SickState.ID;
			PlayState playState = new PlayState();
			Dictionary<int, Dictionary<int, PlayState>> agentActionAnimTable = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable;
			AgentProfile.PoseIDCollection poseIDTable = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable;
			this.isSick = (-1 < id);
			this.isCold = false;
			if (this.isSick)
			{
				if (id != 0)
				{
					PoseKeyPair[] normalIDList = poseIDTable.NormalIDList;
					PoseKeyPair element = normalIDList.GetElement(UnityEngine.Random.Range(0, normalIDList.Length));
					playState = agentActionAnimTable[element.postureID][element.poseID];
				}
				else
				{
					this.isCold = true;
					PoseKeyPair coughID = poseIDTable.CoughID;
					playState = agentActionAnimTable[coughID.postureID][coughID.poseID];
				}
			}
			else
			{
				List<PoseKeyPair> list = ListPool<PoseKeyPair>.Get();
				list.AddRange(poseIDTable.NormalIDList);
				Weather weather = Singleton<Map>.Instance.Simulator.Weather;
				if (weather == Weather.Clear || weather == Weather.Cloud1)
				{
					list.Add(poseIDTable.ClearPoseID);
				}
				PoseKeyPair element2 = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				ListPool<PoseKeyPair>.Release(list);
				playState = agentActionAnimTable[element2.postureID][element2.poseID];
			}
			this.layer = playState.Layer;
			this.inEnableFade = playState.MainStateInfo.InStateInfo.EnableFade;
			this.inFadeTime = playState.MainStateInfo.InStateInfo.FadeSecond;
			base.Agent.Animation.OnceActionStates.Clear();
			if (!playState.MainStateInfo.InStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item in playState.MainStateInfo.InStateInfo.StateInfos)
				{
					base.Agent.Animation.OnceActionStates.Add(item);
				}
			}
			base.Agent.Animation.OutStates.Clear();
			if (!playState.MainStateInfo.OutStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item2 in playState.MainStateInfo.OutStateInfo.StateInfos)
				{
					base.Agent.Animation.OutStates.Enqueue(item2);
				}
			}
			this.duration = Singleton<Manager.Resources>.Instance.AgentProfile.StandDurationMinMax.RandomValue;
			this.onEndEvent = new Subject<Unit>();
			this.onEndEvent.Take(1).Subscribe(delegate(Unit _)
			{
				base.Agent.Animation.PlayOutAnimation(this.outEnableFade, this.outFadeTime, this.layer);
			});
			this.onStartEvent = delegate()
			{
				base.Agent.Animation.PlayOnceActionAnimation(this.inEnableFade, this.inFadeTime, this.layer);
			};
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x002D2F5C File Offset: 0x002D135C
		public override TaskStatus OnUpdate()
		{
			this.elapsedTime += Time.deltaTime;
			if (this.elapsedTime < this.duration)
			{
				return TaskStatus.Running;
			}
			if (this.onStartEvent != null)
			{
				this.onStartEvent();
				this.onStartEvent = null;
			}
			if (base.Agent.Animation.PlayingOnceActionAnimation)
			{
				if (!this.isCold)
				{
					float normalizedTime = base.Agent.Animation.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
					float num = normalizedTime * 360f * (float)this.lookNum;
					base.Agent.AnimalFovAngleOffsetY = Mathf.Sin(num * 0.017453292f) * this.aroundAngle;
				}
				return TaskStatus.Running;
			}
			if (this.onEndEvent != null)
			{
				this.onEndEvent.OnNext(Unit.Default);
			}
			if (base.Agent.Animation.PlayingOutAnimation)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06006A3D RID: 27197 RVA: 0x002D304F File Offset: 0x002D144F
		public override void OnEnd()
		{
			this.onEndEvent = null;
			base.Agent.Animation.StopOnceActionAnimCoroutine();
			base.Agent.Animation.StopOutAnimCoroutine();
			base.Agent.AnimalFovAngleOffsetY = 0f;
		}

		// Token: 0x040059D6 RID: 22998
		[SerializeField]
		private int lookNum = 1;

		// Token: 0x040059D7 RID: 22999
		[SerializeField]
		private float aroundAngle = 45f;

		// Token: 0x040059D8 RID: 23000
		private float duration;

		// Token: 0x040059D9 RID: 23001
		private float elapsedTime;

		// Token: 0x040059DA RID: 23002
		private int layer;

		// Token: 0x040059DB RID: 23003
		private bool inEnableFade;

		// Token: 0x040059DC RID: 23004
		private float inFadeTime;

		// Token: 0x040059DD RID: 23005
		private bool outEnableFade;

		// Token: 0x040059DE RID: 23006
		private float outFadeTime;

		// Token: 0x040059DF RID: 23007
		private Subject<Unit> onEndEvent;

		// Token: 0x040059E0 RID: 23008
		private System.Action onStartEvent;

		// Token: 0x040059E1 RID: 23009
		private bool isSick;

		// Token: 0x040059E2 RID: 23010
		private bool isCold;
	}
}
