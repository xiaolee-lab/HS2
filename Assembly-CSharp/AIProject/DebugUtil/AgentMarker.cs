using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject.DebugUtil
{
	// Token: 0x02000E1A RID: 3610
	[AddComponentMenu("YK/Debug/AgentMarker")]
	public class AgentMarker : MonoBehaviour
	{
		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06006FF8 RID: 28664 RVA: 0x002FEBA7 File Offset: 0x002FCFA7
		public static Dictionary<int, AgentActor> AgentMarkerTable { get; } = new Dictionary<int, AgentActor>();

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06006FF9 RID: 28665 RVA: 0x002FEBAE File Offset: 0x002FCFAE
		public static List<int> Keys { get; } = new List<int>();

		// Token: 0x06006FFA RID: 28666 RVA: 0x002FEBB5 File Offset: 0x002FCFB5
		private void Awake()
		{
			this._agent = base.GetComponent<AgentActor>();
		}

		// Token: 0x06006FFB RID: 28667 RVA: 0x002FEBC4 File Offset: 0x002FCFC4
		private void OnEnable()
		{
			if (this._agent != null)
			{
				int instanceID = this._agent.GetInstanceID();
				AgentMarker.AgentMarkerTable[instanceID] = this._agent;
				AgentMarker.Keys.Add(instanceID);
			}
		}

		// Token: 0x06006FFC RID: 28668 RVA: 0x002FEC0C File Offset: 0x002FD00C
		private void OnDisable()
		{
			if (this._agent != null)
			{
				int instanceID = this._agent.GetInstanceID();
				AgentMarker.AgentMarkerTable.Remove(instanceID);
				AgentMarker.Keys.RemoveAll((int x) => x == instanceID);
			}
		}

		// Token: 0x04005C2F RID: 23599
		private AgentActor _agent;
	}
}
