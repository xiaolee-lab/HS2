using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DD RID: 733
public class FlockController : MonoBehaviour
{
	// Token: 0x06000C57 RID: 3159 RVA: 0x000301C4 File Offset: 0x0002E5C4
	public void Start()
	{
		this._thisT = base.transform;
		if (this._positionSphereDepth == -1f)
		{
			this._positionSphereDepth = this._positionSphere;
		}
		if (this._spawnSphereDepth == -1f)
		{
			this._spawnSphereDepth = this._spawnSphere;
		}
		this._posBuffer = this._thisT.position + this._startPosOffset;
		if (!this._slowSpawn)
		{
			this.AddChild(this._childAmount);
		}
		if (this._randomPositionTimer > 0f)
		{
			base.InvokeRepeating("SetFlockRandomPosition", this._randomPositionTimer, this._randomPositionTimer);
		}
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x00030270 File Offset: 0x0002E670
	public void AddChild(int amount)
	{
		if (this._groupChildToNewTransform)
		{
			this.InstantiateGroup();
		}
		for (int i = 0; i < amount; i++)
		{
			FlockChild flockChild = UnityEngine.Object.Instantiate<FlockChild>(this._childPrefab);
			flockChild._spawner = this;
			this._roamers.Add(flockChild);
			this.AddChildToParent(flockChild.transform);
		}
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x000302CB File Offset: 0x0002E6CB
	public void AddChildToParent(Transform obj)
	{
		if (this._groupChildToFlock)
		{
			obj.parent = base.transform;
			return;
		}
		if (this._groupChildToNewTransform)
		{
			obj.parent = this._groupTransform;
			return;
		}
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x00030300 File Offset: 0x0002E700
	public void RemoveChild(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			FlockChild flockChild = this._roamers[this._roamers.Count - 1];
			this._roamers.RemoveAt(this._roamers.Count - 1);
			UnityEngine.Object.Destroy(flockChild.gameObject);
		}
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0003035C File Offset: 0x0002E75C
	public void Update()
	{
		if (this._activeChildren > 0f)
		{
			if (this._updateDivisor > 1)
			{
				this._updateCounter++;
				this._updateCounter %= this._updateDivisor;
				this._newDelta = Time.deltaTime * (float)this._updateDivisor;
			}
			else
			{
				this._newDelta = Time.deltaTime;
			}
		}
		this.UpdateChildAmount();
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x000303D0 File Offset: 0x0002E7D0
	public void InstantiateGroup()
	{
		if (this._groupTransform != null)
		{
			return;
		}
		GameObject gameObject = new GameObject();
		this._groupTransform = gameObject.transform;
		this._groupTransform.position = this._thisT.position;
		if (this._groupName != string.Empty)
		{
			gameObject.name = this._groupName;
			return;
		}
		gameObject.name = this._thisT.name + " Fish Container";
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00030454 File Offset: 0x0002E854
	public void UpdateChildAmount()
	{
		if (this._childAmount >= 0 && this._childAmount < this._roamers.Count)
		{
			this.RemoveChild(1);
			return;
		}
		if (this._childAmount > this._roamers.Count)
		{
			this.AddChild(1);
		}
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x000304A8 File Offset: 0x0002E8A8
	public void OnDrawGizmos()
	{
		if (this._thisT == null)
		{
			this._thisT = base.transform;
		}
		if (!Application.isPlaying && this._posBuffer != this._thisT.position + this._startPosOffset)
		{
			this._posBuffer = this._thisT.position + this._startPosOffset;
		}
		if (this._positionSphereDepth == -1f)
		{
			this._positionSphereDepth = this._positionSphere;
		}
		if (this._spawnSphereDepth == -1f)
		{
			this._spawnSphereDepth = this._spawnSphere;
		}
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(this._posBuffer, new Vector3(this._spawnSphere * 2f, this._spawnSphereHeight * 2f, this._spawnSphereDepth * 2f));
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(this._thisT.position, new Vector3(this._positionSphere * 2f + this._spawnSphere * 2f, this._positionSphereHeight * 2f + this._spawnSphereHeight * 2f, this._positionSphereDepth * 2f + this._spawnSphereDepth * 2f));
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x00030600 File Offset: 0x0002EA00
	public void SetFlockRandomPosition()
	{
		Vector3 zero = Vector3.zero;
		zero.x = UnityEngine.Random.Range(-this._positionSphere, this._positionSphere) + this._thisT.position.x;
		zero.z = UnityEngine.Random.Range(-this._positionSphereDepth, this._positionSphereDepth) + this._thisT.position.z;
		zero.y = UnityEngine.Random.Range(-this._positionSphereHeight, this._positionSphereHeight) + this._thisT.position.y;
		this._posBuffer = zero;
		if (this._forceChildWaypoints)
		{
			for (int i = 0; i < this._roamers.Count; i++)
			{
				this._roamers[i].Wander(UnityEngine.Random.value * this._forcedRandomDelay);
			}
		}
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x000306EC File Offset: 0x0002EAEC
	public void destroyBirds()
	{
		for (int i = 0; i < this._roamers.Count; i++)
		{
			UnityEngine.Object.Destroy(this._roamers[i].gameObject);
		}
		this._childAmount = 0;
		this._roamers.Clear();
	}

	// Token: 0x04000B09 RID: 2825
	public FlockChild _childPrefab;

	// Token: 0x04000B0A RID: 2826
	public int _childAmount = 250;

	// Token: 0x04000B0B RID: 2827
	public bool _slowSpawn;

	// Token: 0x04000B0C RID: 2828
	public float _spawnSphere = 3f;

	// Token: 0x04000B0D RID: 2829
	public float _spawnSphereHeight = 3f;

	// Token: 0x04000B0E RID: 2830
	public float _spawnSphereDepth = -1f;

	// Token: 0x04000B0F RID: 2831
	public float _minSpeed = 6f;

	// Token: 0x04000B10 RID: 2832
	public float _maxSpeed = 10f;

	// Token: 0x04000B11 RID: 2833
	public float _minScale = 0.7f;

	// Token: 0x04000B12 RID: 2834
	public float _maxScale = 1f;

	// Token: 0x04000B13 RID: 2835
	public float _soarFrequency;

	// Token: 0x04000B14 RID: 2836
	public string _soarAnimation = "Soar";

	// Token: 0x04000B15 RID: 2837
	public string _flapAnimation = "Flap";

	// Token: 0x04000B16 RID: 2838
	public string _idleAnimation = "Idle";

	// Token: 0x04000B17 RID: 2839
	public float _diveValue = 7f;

	// Token: 0x04000B18 RID: 2840
	public float _diveFrequency = 0.5f;

	// Token: 0x04000B19 RID: 2841
	public float _minDamping = 1f;

	// Token: 0x04000B1A RID: 2842
	public float _maxDamping = 2f;

	// Token: 0x04000B1B RID: 2843
	public float _waypointDistance = 1f;

	// Token: 0x04000B1C RID: 2844
	public float _minAnimationSpeed = 2f;

	// Token: 0x04000B1D RID: 2845
	public float _maxAnimationSpeed = 4f;

	// Token: 0x04000B1E RID: 2846
	public float _randomPositionTimer = 10f;

	// Token: 0x04000B1F RID: 2847
	public float _positionSphere = 25f;

	// Token: 0x04000B20 RID: 2848
	public float _positionSphereHeight = 25f;

	// Token: 0x04000B21 RID: 2849
	public float _positionSphereDepth = -1f;

	// Token: 0x04000B22 RID: 2850
	public bool _childTriggerPos;

	// Token: 0x04000B23 RID: 2851
	public bool _forceChildWaypoints;

	// Token: 0x04000B24 RID: 2852
	public float _forcedRandomDelay = 1.5f;

	// Token: 0x04000B25 RID: 2853
	public bool _flatFly;

	// Token: 0x04000B26 RID: 2854
	public bool _flatSoar;

	// Token: 0x04000B27 RID: 2855
	public bool _birdAvoid;

	// Token: 0x04000B28 RID: 2856
	public int _birdAvoidHorizontalForce = 1000;

	// Token: 0x04000B29 RID: 2857
	public bool _birdAvoidDown;

	// Token: 0x04000B2A RID: 2858
	public bool _birdAvoidUp;

	// Token: 0x04000B2B RID: 2859
	public int _birdAvoidVerticalForce = 300;

	// Token: 0x04000B2C RID: 2860
	public float _birdAvoidDistanceMax = 4.5f;

	// Token: 0x04000B2D RID: 2861
	public float _birdAvoidDistanceMin = 5f;

	// Token: 0x04000B2E RID: 2862
	public float _soarMaxTime;

	// Token: 0x04000B2F RID: 2863
	public LayerMask _avoidanceMask = -1;

	// Token: 0x04000B30 RID: 2864
	public List<FlockChild> _roamers;

	// Token: 0x04000B31 RID: 2865
	public Vector3 _posBuffer;

	// Token: 0x04000B32 RID: 2866
	public int _updateDivisor = 1;

	// Token: 0x04000B33 RID: 2867
	public float _newDelta;

	// Token: 0x04000B34 RID: 2868
	public int _updateCounter;

	// Token: 0x04000B35 RID: 2869
	public float _activeChildren;

	// Token: 0x04000B36 RID: 2870
	public bool _groupChildToNewTransform;

	// Token: 0x04000B37 RID: 2871
	public Transform _groupTransform;

	// Token: 0x04000B38 RID: 2872
	public string _groupName = string.Empty;

	// Token: 0x04000B39 RID: 2873
	public bool _groupChildToFlock;

	// Token: 0x04000B3A RID: 2874
	public Vector3 _startPosOffset;

	// Token: 0x04000B3B RID: 2875
	public Transform _thisT;
}
