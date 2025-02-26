using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CAE RID: 3246
	[TaskCategory("")]
	public class ViewLocation : AgentAction
	{
		// Token: 0x06006974 RID: 26996 RVA: 0x002CE790 File Offset: 0x002CCB90
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Location;
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			base.OnStart();
			agent.DisableActionFlag();
		}

		// Token: 0x06006975 RID: 26997 RVA: 0x002CE7C7 File Offset: 0x002CCBC7
		public override TaskStatus OnUpdate()
		{
			this.Complete();
			return TaskStatus.Success;
		}

		// Token: 0x06006976 RID: 26998 RVA: 0x002CE7D0 File Offset: 0x002CCBD0
		private void Complete()
		{
			AgentActor agent = base.Agent;
			int item = AIProject.Definitions.Action.NameTable[EventType.Location].Item1;
			agent.UpdateStatus(item, 0);
			int desireKey = Desire.GetDesireKey(Desire.Type.Location);
			agent.SetDesire(desireKey, 0f);
			agent.AgentData.LocationCount++;
			if (agent.AgentData.LocationCount >= agent.AgentData.LocationTaskCount)
			{
				switch (agent.CurrentPoint.RegisterID)
				{
				case 269:
					agent.ChaControl.fileGameInfo.favoritePlace = 11;
					break;
				case 270:
					agent.ChaControl.fileGameInfo.favoritePlace = 9;
					break;
				case 271:
					agent.ChaControl.fileGameInfo.favoritePlace = 10;
					break;
				case 272:
					agent.ChaControl.fileGameInfo.favoritePlace = 8;
					break;
				case 273:
					agent.ChaControl.fileGameInfo.favoritePlace = 7;
					break;
				case 274:
					agent.ChaControl.fileGameInfo.favoritePlace = 5;
					break;
				case 275:
					agent.ChaControl.fileGameInfo.favoritePlace = 6;
					break;
				case 276:
					agent.ChaControl.fileGameInfo.favoritePlace = 4;
					break;
				case 277:
					agent.ChaControl.fileGameInfo.favoritePlace = 3;
					break;
				case 278:
					agent.ChaControl.fileGameInfo.favoritePlace = 1;
					break;
				case 279:
					agent.ChaControl.fileGameInfo.favoritePlace = 2;
					break;
				}
				agent.AgentData.LocationTaskCount = UnityEngine.Random.Range(1, 3);
			}
			agent.CauseSick();
			agent.ResetActionFlag();
			agent.CurrentPoint.SetActiveMapItemObjs(true);
			agent.CurrentPoint.ReleaseSlot(agent);
			agent.CurrentPoint = null;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
		}
	}
}
