using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020003FB RID: 1019
[RequireComponent(typeof(NavMeshAgent))]
public class ClickToMove : MonoBehaviour
{
	// Token: 0x0600122A RID: 4650 RVA: 0x00071C5E File Offset: 0x0007005E
	private void Start()
	{
		this.m_Agent = base.GetComponent<NavMeshAgent>();
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x00071C6C File Offset: 0x0007006C
	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray.origin, ray.direction, out this.m_HitInfo))
			{
				this.m_Agent.destination = this.m_HitInfo.point;
			}
		}
	}

	// Token: 0x040014B1 RID: 5297
	private NavMeshAgent m_Agent;

	// Token: 0x040014B2 RID: 5298
	private RaycastHit m_HitInfo = default(RaycastHit);
}
