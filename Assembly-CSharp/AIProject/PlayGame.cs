using System;
using System.Collections.Generic;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CDA RID: 3290
	[TaskCategory("")]
	public class PlayGame : AgentStateAction
	{
		// Token: 0x06006A64 RID: 27236 RVA: 0x002D48AE File Offset: 0x002D2CAE
		public override void OnStart()
		{
			base.Agent.EventKey = EventType.Play;
			base.OnStart();
		}

		// Token: 0x06006A65 RID: 27237 RVA: 0x002D48C4 File Offset: 0x002D2CC4
		protected override void OnCompletedStateTask()
		{
			AgentActor agent = base.Agent;
			int desireKey = Desire.GetDesireKey(Desire.Type.Game);
			agent.SetDesire(desireKey, 0f);
			PoseKeyPair wateringID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.WateringID;
			if (agent.ActionID == wateringID.postureID && agent.PoseID == wateringID.poseID)
			{
				float? num = null;
				FarmPoint farmPoint = null;
				foreach (FarmPoint farmPoint2 in Singleton<Manager.Map>.Instance.PointAgent.FarmPoints)
				{
					float num2 = Vector3.Distance(agent.Position, farmPoint2.Position);
					if (num != null)
					{
						if (num2 < num.Value)
						{
							num = new float?(num2);
							farmPoint = farmPoint2;
						}
					}
					else
					{
						num = new float?(num2);
						farmPoint = farmPoint2;
					}
				}
				foreach (KeyValuePair<int, FarmPoint> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.RuntimeFarmPointTable)
				{
					if (keyValuePair.Value.Kind == FarmPoint.FarmKind.Plant)
					{
						float num3 = Vector3.Distance(agent.Position, keyValuePair.Value.Position);
						if (num != null)
						{
							if (num3 < num.Value)
							{
								num = new float?(num3);
								farmPoint = keyValuePair.Value;
							}
						}
						else
						{
							num = new float?(num3);
							farmPoint = keyValuePair.Value;
						}
					}
				}
				List<AIProject.SaveData.Environment.PlantInfo> list;
				if (farmPoint != null && Singleton<Game>.Instance.Environment.FarmlandTable.TryGetValue(farmPoint.RegisterID, out list))
				{
					foreach (AIProject.SaveData.Environment.PlantInfo plantInfo in list)
					{
						if (plantInfo != null)
						{
							plantInfo.AddTimer(60f);
						}
					}
					if (agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(12))
					{
						foreach (AIProject.SaveData.Environment.PlantInfo plantInfo2 in list)
						{
							if (plantInfo2 != null)
							{
								plantInfo2.AddTimer(20f);
							}
						}
					}
				}
				agent.AgentData.SetAppendEventFlagCheck(2, true);
			}
			agent.AgentData.AddAppendEventFlagParam(3, 1);
		}
	}
}
