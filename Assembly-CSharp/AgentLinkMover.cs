using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020003FA RID: 1018
[RequireComponent(typeof(NavMeshAgent))]
public class AgentLinkMover : MonoBehaviour
{
	// Token: 0x06001225 RID: 4645 RVA: 0x000715F8 File Offset: 0x0006F9F8
	private IEnumerator Start()
	{
		NavMeshAgent agent = base.GetComponent<NavMeshAgent>();
		agent.autoTraverseOffMeshLink = false;
		for (;;)
		{
			if (agent.isOnOffMeshLink)
			{
				if (this.m_Method == OffMeshLinkMoveMethod.NormalSpeed)
				{
					yield return base.StartCoroutine(this.NormalSpeed(agent));
				}
				else if (this.m_Method == OffMeshLinkMoveMethod.Parabola)
				{
					yield return base.StartCoroutine(this.Parabola(agent, 2f, 0.5f));
				}
				else if (this.m_Method == OffMeshLinkMoveMethod.Curve)
				{
					yield return base.StartCoroutine(this.Curve(agent, 0.5f));
				}
				agent.CompleteOffMeshLink();
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x00071614 File Offset: 0x0006FA14
	private IEnumerator NormalSpeed(NavMeshAgent agent)
	{
		Vector3 endPos = agent.currentOffMeshLinkData.endPos + Vector3.up * agent.baseOffset;
		while (agent.transform.position != endPos)
		{
			agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x00071630 File Offset: 0x0006FA30
	private IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
	{
		OffMeshLinkData data = agent.currentOffMeshLinkData;
		Vector3 startPos = agent.transform.position;
		Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
		float normalizedTime = 0f;
		while (normalizedTime < 1f)
		{
			float yOffset = height * 4f * (normalizedTime - normalizedTime * normalizedTime);
			agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
			normalizedTime += Time.deltaTime / duration;
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x0007165C File Offset: 0x0006FA5C
	private IEnumerator Curve(NavMeshAgent agent, float duration)
	{
		OffMeshLinkData data = agent.currentOffMeshLinkData;
		Vector3 startPos = agent.transform.position;
		Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
		float normalizedTime = 0f;
		while (normalizedTime < 1f)
		{
			float yOffset = this.m_Curve.Evaluate(normalizedTime);
			agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
			normalizedTime += Time.deltaTime / duration;
			yield return null;
		}
		yield break;
	}

	// Token: 0x040014AF RID: 5295
	public OffMeshLinkMoveMethod m_Method = OffMeshLinkMoveMethod.Parabola;

	// Token: 0x040014B0 RID: 5296
	public AnimationCurve m_Curve = new AnimationCurve();
}
