using System;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000E2B RID: 3627
	public interface ICommandable
	{
		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06007169 RID: 29033
		int InstanceID { get; }

		// Token: 0x0600716A RID: 29034
		bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward);

		// Token: 0x0600716B RID: 29035
		bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB);

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x0600716C RID: 29036
		bool IsImpossible { get; }

		// Token: 0x0600716D RID: 29037
		bool SetImpossible(bool value, Actor actor);

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x0600716E RID: 29038
		bool IsNeutralCommand { get; }

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x0600716F RID: 29039
		Vector3 Position { get; }

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06007170 RID: 29040
		Vector3 CommandCenter { get; }

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06007171 RID: 29041
		CommandLabel.CommandInfo[] Labels { get; }

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06007172 RID: 29042
		CommandLabel.CommandInfo[] DateLabels { get; }

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06007173 RID: 29043
		ObjectLayer Layer { get; }

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06007174 RID: 29044
		CommandType CommandType { get; }
	}
}
