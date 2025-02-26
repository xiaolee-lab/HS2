using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class LandingSpotController : MonoBehaviour
{
	// Token: 0x06000C77 RID: 3191 RVA: 0x00031BDC File Offset: 0x0002FFDC
	public void Start()
	{
		if (this._thisT == null)
		{
			this._thisT = base.transform;
		}
		if (this._flock == null)
		{
			this._flock = (FlockController)UnityEngine.Object.FindObjectOfType(typeof(FlockController));
		}
		if (this._landOnStart)
		{
			base.StartCoroutine(this.InstantLandOnStart(0.1f));
		}
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x00031C4E File Offset: 0x0003004E
	public void ScareAll()
	{
		this.ScareAll(0f, 1f);
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x00031C60 File Offset: 0x00030060
	public void ScareAll(float minDelay, float maxDelay)
	{
		for (int i = 0; i < this._thisT.childCount; i++)
		{
			if (this._thisT.GetChild(i).GetComponent<LandingSpot>() != null)
			{
				LandingSpot component = this._thisT.GetChild(i).GetComponent<LandingSpot>();
				component.Invoke("ReleaseFlockChild", UnityEngine.Random.Range(minDelay, maxDelay));
			}
		}
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x00031CCC File Offset: 0x000300CC
	public void LandAll()
	{
		for (int i = 0; i < this._thisT.childCount; i++)
		{
			if (this._thisT.GetChild(i).GetComponent<LandingSpot>() != null)
			{
				LandingSpot component = this._thisT.GetChild(i).GetComponent<LandingSpot>();
				base.StartCoroutine(component.GetFlockChild(0f, 2f));
			}
		}
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x00031D3C File Offset: 0x0003013C
	public IEnumerator InstantLandOnStart(float delay)
	{
		yield return new WaitForSeconds(delay);
		for (int i = 0; i < this._thisT.childCount; i++)
		{
			if (this._thisT.GetChild(i).GetComponent<LandingSpot>() != null)
			{
				LandingSpot component = this._thisT.GetChild(i).GetComponent<LandingSpot>();
				component.InstantLand();
			}
		}
		yield break;
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x00031D60 File Offset: 0x00030160
	public IEnumerator InstantLand(float delay)
	{
		yield return new WaitForSeconds(delay);
		for (int i = 0; i < this._thisT.childCount; i++)
		{
			if (this._thisT.GetChild(i).GetComponent<LandingSpot>() != null)
			{
				LandingSpot component = this._thisT.GetChild(i).GetComponent<LandingSpot>();
				component.InstantLand();
			}
		}
		yield break;
	}

	// Token: 0x04000B50 RID: 2896
	public bool _randomRotate = true;

	// Token: 0x04000B51 RID: 2897
	public Vector2 _autoCatchDelay = new Vector2(10f, 20f);

	// Token: 0x04000B52 RID: 2898
	public Vector2 _autoDismountDelay = new Vector2(10f, 20f);

	// Token: 0x04000B53 RID: 2899
	public float _maxBirdDistance = 20f;

	// Token: 0x04000B54 RID: 2900
	public float _minBirdDistance = 5f;

	// Token: 0x04000B55 RID: 2901
	public bool _takeClosest;

	// Token: 0x04000B56 RID: 2902
	public FlockController _flock;

	// Token: 0x04000B57 RID: 2903
	public bool _landOnStart;

	// Token: 0x04000B58 RID: 2904
	public bool _soarLand = true;

	// Token: 0x04000B59 RID: 2905
	public bool _onlyBirdsAbove;

	// Token: 0x04000B5A RID: 2906
	public float _landingSpeedModifier = 0.5f;

	// Token: 0x04000B5B RID: 2907
	public float _landingTurnSpeedModifier = 5f;

	// Token: 0x04000B5C RID: 2908
	public Transform _featherPS;

	// Token: 0x04000B5D RID: 2909
	public Transform _thisT;

	// Token: 0x04000B5E RID: 2910
	public int _activeLandingSpots;

	// Token: 0x04000B5F RID: 2911
	public float _snapLandDistance = 0.1f;

	// Token: 0x04000B60 RID: 2912
	public float _landedRotateSpeed = 0.01f;

	// Token: 0x04000B61 RID: 2913
	public float _gizmoSize = 0.2f;
}
