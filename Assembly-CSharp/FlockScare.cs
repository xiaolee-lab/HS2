using System;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class FlockScare : MonoBehaviour
{
	// Token: 0x06000C62 RID: 3170 RVA: 0x0003076C File Offset: 0x0002EB6C
	private void CheckProximityToLandingSpots()
	{
		this.IterateLandingSpots();
		if (this.currentController._activeLandingSpots > 0 && this.CheckDistanceToLandingSpot(this.landingSpotControllers[this.lsc]))
		{
			this.landingSpotControllers[this.lsc].ScareAll();
		}
		base.Invoke("CheckProximityToLandingSpots", this.scareInterval);
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x000307CC File Offset: 0x0002EBCC
	private void IterateLandingSpots()
	{
		this.ls += this.checkEveryNthLandingSpot;
		this.currentController = this.landingSpotControllers[this.lsc];
		int childCount = this.currentController.transform.childCount;
		if (this.ls > childCount - 1)
		{
			this.ls -= childCount;
			if (this.lsc < this.landingSpotControllers.Length - 1)
			{
				this.lsc++;
			}
			else
			{
				this.lsc = 0;
			}
		}
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x0003085C File Offset: 0x0002EC5C
	private bool CheckDistanceToLandingSpot(LandingSpotController lc)
	{
		Transform transform = lc.transform;
		Transform child = transform.GetChild(this.ls);
		LandingSpot component = child.GetComponent<LandingSpot>();
		if (component.landingChild != null)
		{
			float sqrMagnitude = (child.position - base.transform.position).sqrMagnitude;
			if (sqrMagnitude < this.distanceToScare * this.distanceToScare)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x000308CC File Offset: 0x0002ECCC
	private void Invoker()
	{
		for (int i = 0; i < this.InvokeAmounts; i++)
		{
			float num = this.scareInterval / (float)this.InvokeAmounts * (float)i;
			base.Invoke("CheckProximityToLandingSpots", this.scareInterval + num);
		}
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00030915 File Offset: 0x0002ED15
	private void OnEnable()
	{
		base.CancelInvoke("CheckProximityToLandingSpots");
		if (this.landingSpotControllers.Length > 0)
		{
			this.Invoker();
		}
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x00030936 File Offset: 0x0002ED36
	private void OnDisable()
	{
		base.CancelInvoke("CheckProximityToLandingSpots");
	}

	// Token: 0x04000B3C RID: 2876
	public LandingSpotController[] landingSpotControllers;

	// Token: 0x04000B3D RID: 2877
	public float scareInterval = 0.1f;

	// Token: 0x04000B3E RID: 2878
	public float distanceToScare = 2f;

	// Token: 0x04000B3F RID: 2879
	public int checkEveryNthLandingSpot = 1;

	// Token: 0x04000B40 RID: 2880
	public int InvokeAmounts = 1;

	// Token: 0x04000B41 RID: 2881
	private int lsc;

	// Token: 0x04000B42 RID: 2882
	private int ls;

	// Token: 0x04000B43 RID: 2883
	private LandingSpotController currentController;
}
