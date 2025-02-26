using System;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class FlockWaypointTrigger : MonoBehaviour
{
	// Token: 0x06000C69 RID: 3177 RVA: 0x00030958 File Offset: 0x0002ED58
	public void Start()
	{
		if (this._flockChild == null)
		{
			this._flockChild = base.transform.parent.GetComponent<FlockChild>();
		}
		float num = UnityEngine.Random.Range(this._timer, this._timer * 3f);
		base.InvokeRepeating("Trigger", num, num);
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x000309B1 File Offset: 0x0002EDB1
	public void Trigger()
	{
		this._flockChild.Wander(0f);
	}

	// Token: 0x04000B44 RID: 2884
	public float _timer = 1f;

	// Token: 0x04000B45 RID: 2885
	public FlockChild _flockChild;
}
