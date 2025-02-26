using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000409 RID: 1033
[RequireComponent(typeof(NavMeshAgent))]
public class RandomWalk : MonoBehaviour
{
	// Token: 0x0600126F RID: 4719 RVA: 0x0007314E File Offset: 0x0007154E
	private void Start()
	{
		this.m_agent = base.GetComponent<NavMeshAgent>();
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x0007315C File Offset: 0x0007155C
	private void Update()
	{
		if (this.m_agent.pathPending || this.m_agent.remainingDistance > 0.1f)
		{
			return;
		}
		this.m_agent.destination = this.m_Range * UnityEngine.Random.insideUnitCircle;
	}

	// Token: 0x040014F4 RID: 5364
	public float m_Range = 25f;

	// Token: 0x040014F5 RID: 5365
	private NavMeshAgent m_agent;
}
