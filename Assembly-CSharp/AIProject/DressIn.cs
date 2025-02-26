using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C9D RID: 3229
	[TaskCategory("")]
	public class DressIn : AgentStateAction
	{
		// Token: 0x0600692A RID: 26922 RVA: 0x002CB63C File Offset: 0x002C9A3C
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.DressIn;
			List<string> dressCoordinateList = Singleton<Game>.Instance.Environment.DressCoordinateList;
			string element = dressCoordinateList.GetElement(UnityEngine.Random.Range(0, dressCoordinateList.Count));
			agent.AgentData.BathCoordinateFileName = element;
			base.OnStart();
		}

		// Token: 0x0600692B RID: 26923 RVA: 0x002CB690 File Offset: 0x002C9A90
		protected override void OnCompletedStateTask()
		{
			base.Agent.AgentData.PlayedDressIn = true;
		}
	}
}
