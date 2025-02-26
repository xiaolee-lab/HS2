using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class FlockChild : MonoBehaviour
{
	// Token: 0x06000C3D RID: 3133 RVA: 0x0002EFB4 File Offset: 0x0002D3B4
	public void Start()
	{
		this.FindRequiredComponents();
		this.Wander(0f);
		this.SetRandomScale();
		this._thisT.position = this.findWaypoint();
		this.RandomizeStartAnimationFrame();
		this.InitAvoidanceValues();
		this._speed = this._spawner._minSpeed;
		this._spawner._activeChildren += 1f;
		this._instantiated = true;
		if (this._spawner._updateDivisor > 1)
		{
			int num = this._spawner._updateDivisor - 1;
			FlockChild._updateNextSeed++;
			this._updateSeed = FlockChild._updateNextSeed;
			FlockChild._updateNextSeed %= num;
		}
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x0002F068 File Offset: 0x0002D468
	public void Update()
	{
		if (this._spawner._updateDivisor <= 1 || this._spawner._updateCounter == this._updateSeed)
		{
			this.SoarTimeLimit();
			this.CheckForDistanceToWaypoint();
			this.RotationBasedOnWaypointOrAvoidance();
			this.LimitRotationOfModel();
		}
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x0002F0B4 File Offset: 0x0002D4B4
	public void OnDisable()
	{
		base.CancelInvoke();
		this._spawner._activeChildren -= 1f;
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x0002F0D4 File Offset: 0x0002D4D4
	public void OnEnable()
	{
		if (this._instantiated)
		{
			this._spawner._activeChildren += 1f;
			if (this._landing)
			{
				this._model.GetComponent<Animation>().Play(this._spawner._idleAnimation);
			}
			else
			{
				this._model.GetComponent<Animation>().Play(this._spawner._flapAnimation);
			}
		}
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x0002F14C File Offset: 0x0002D54C
	public void FindRequiredComponents()
	{
		if (this._thisT == null)
		{
			this._thisT = base.transform;
		}
		if (this._model == null)
		{
			this._model = this._thisT.Find("Model").gameObject;
		}
		if (this._modelT == null)
		{
			this._modelT = this._model.transform;
		}
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x0002F1C4 File Offset: 0x0002D5C4
	public void RandomizeStartAnimationFrame()
	{
		IEnumerator enumerator = this._model.GetComponent<Animation>().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AnimationState animationState = (AnimationState)obj;
				animationState.time = UnityEngine.Random.value * animationState.length;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x0002F23C File Offset: 0x0002D63C
	public void InitAvoidanceValues()
	{
		this._avoidValue = UnityEngine.Random.Range(0.3f, 0.1f);
		if (this._spawner._birdAvoidDistanceMax != this._spawner._birdAvoidDistanceMin)
		{
			this._avoidDistance = UnityEngine.Random.Range(this._spawner._birdAvoidDistanceMax, this._spawner._birdAvoidDistanceMin);
			return;
		}
		this._avoidDistance = this._spawner._birdAvoidDistanceMin;
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x0002F2AC File Offset: 0x0002D6AC
	public void SetRandomScale()
	{
		float num = UnityEngine.Random.Range(this._spawner._minScale, this._spawner._maxScale);
		this._thisT.localScale = new Vector3(num, num, num);
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x0002F2E8 File Offset: 0x0002D6E8
	public void SoarTimeLimit()
	{
		if (this._soar && this._spawner._soarMaxTime > 0f)
		{
			if (this._soarTimer > this._spawner._soarMaxTime)
			{
				this.Flap();
				this._soarTimer = 0f;
			}
			else
			{
				this._soarTimer += this._spawner._newDelta;
			}
		}
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x0002F35C File Offset: 0x0002D75C
	public void CheckForDistanceToWaypoint()
	{
		if (!this._landing && (this._thisT.position - this._wayPoint).magnitude < this._spawner._waypointDistance + this._stuckCounter)
		{
			this.Wander(0f);
			this._stuckCounter = 0f;
		}
		else if (!this._landing)
		{
			this._stuckCounter += this._spawner._newDelta;
		}
		else
		{
			this._stuckCounter = 0f;
		}
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x0002F3F8 File Offset: 0x0002D7F8
	public void RotationBasedOnWaypointOrAvoidance()
	{
		Vector3 vector = this._wayPoint - this._thisT.position;
		if (this._targetSpeed > -1f && vector != Vector3.zero)
		{
			Quaternion b = Quaternion.LookRotation(vector);
			this._thisT.rotation = Quaternion.Slerp(this._thisT.rotation, b, this._spawner._newDelta * this._damping);
		}
		if (this._spawner._childTriggerPos && (this._thisT.position - this._spawner._posBuffer).magnitude < 1f)
		{
			this._spawner.SetFlockRandomPosition();
		}
		this._speed = Mathf.Lerp(this._speed, this._targetSpeed, this._spawner._newDelta * 2.5f);
		if (this._move)
		{
			this._thisT.position += this._thisT.forward * this._speed * this._spawner._newDelta;
			if (this._avoid && this._spawner._birdAvoid)
			{
				this.Avoidance();
			}
		}
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x0002F54C File Offset: 0x0002D94C
	public bool Avoidance()
	{
		RaycastHit raycastHit = default(RaycastHit);
		Vector3 forward = this._modelT.forward;
		bool result = false;
		Quaternion rotation = Quaternion.identity;
		Vector3 eulerAngles = Vector3.zero;
		Vector3 position = Vector3.zero;
		position = this._thisT.position;
		rotation = this._thisT.rotation;
		eulerAngles = this._thisT.rotation.eulerAngles;
		if (Physics.Raycast(this._thisT.position, forward + this._modelT.right * this._avoidValue, out raycastHit, this._avoidDistance, this._spawner._avoidanceMask))
		{
			eulerAngles.y -= (float)this._spawner._birdAvoidHorizontalForce * this._spawner._newDelta * this._damping;
			rotation.eulerAngles = eulerAngles;
			this._thisT.rotation = rotation;
			result = true;
		}
		else if (Physics.Raycast(this._thisT.position, forward + this._modelT.right * -this._avoidValue, out raycastHit, this._avoidDistance, this._spawner._avoidanceMask))
		{
			eulerAngles.y += (float)this._spawner._birdAvoidHorizontalForce * this._spawner._newDelta * this._damping;
			rotation.eulerAngles = eulerAngles;
			this._thisT.rotation = rotation;
			result = true;
		}
		if (this._spawner._birdAvoidDown && !this._landing && Physics.Raycast(this._thisT.position, -Vector3.up, out raycastHit, this._avoidDistance, this._spawner._avoidanceMask))
		{
			eulerAngles.x -= (float)this._spawner._birdAvoidVerticalForce * this._spawner._newDelta * this._damping;
			rotation.eulerAngles = eulerAngles;
			this._thisT.rotation = rotation;
			position.y += (float)this._spawner._birdAvoidVerticalForce * this._spawner._newDelta * 0.01f;
			this._thisT.position = position;
			result = true;
		}
		else if (this._spawner._birdAvoidUp && !this._landing && Physics.Raycast(this._thisT.position, Vector3.up, out raycastHit, this._avoidDistance, this._spawner._avoidanceMask))
		{
			eulerAngles.x += (float)this._spawner._birdAvoidVerticalForce * this._spawner._newDelta * this._damping;
			rotation.eulerAngles = eulerAngles;
			this._thisT.rotation = rotation;
			position.y -= (float)this._spawner._birdAvoidVerticalForce * this._spawner._newDelta * 0.01f;
			this._thisT.position = position;
			result = true;
		}
		return result;
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x0002F874 File Offset: 0x0002DC74
	public void LimitRotationOfModel()
	{
		Quaternion localRotation = Quaternion.identity;
		Vector3 eulerAngles = Vector3.zero;
		localRotation = this._modelT.localRotation;
		eulerAngles = localRotation.eulerAngles;
		if ((((this._soar && this._spawner._flatSoar) || (this._spawner._flatFly && !this._soar)) && this._wayPoint.y > this._thisT.position.y) || this._landing)
		{
			eulerAngles.x = Mathf.LerpAngle(this._modelT.localEulerAngles.x, -this._thisT.localEulerAngles.x, this._spawner._newDelta * 1.75f);
			localRotation.eulerAngles = eulerAngles;
			this._modelT.localRotation = localRotation;
		}
		else
		{
			eulerAngles.x = Mathf.LerpAngle(this._modelT.localEulerAngles.x, 0f, this._spawner._newDelta * 1.75f);
			localRotation.eulerAngles = eulerAngles;
			this._modelT.localRotation = localRotation;
		}
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x0002F9B0 File Offset: 0x0002DDB0
	public void Wander(float delay)
	{
		if (!this._landing)
		{
			this._damping = UnityEngine.Random.Range(this._spawner._minDamping, this._spawner._maxDamping);
			this._targetSpeed = UnityEngine.Random.Range(this._spawner._minSpeed, this._spawner._maxSpeed);
			base.Invoke("SetRandomMode", delay);
		}
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x0002FA18 File Offset: 0x0002DE18
	public void SetRandomMode()
	{
		base.CancelInvoke("SetRandomMode");
		if (!this._dived && UnityEngine.Random.value < this._spawner._soarFrequency)
		{
			this.Soar();
		}
		else if (!this._dived && UnityEngine.Random.value < this._spawner._diveFrequency)
		{
			this.Dive();
		}
		else
		{
			this.Flap();
		}
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x0002FA8C File Offset: 0x0002DE8C
	public void Flap()
	{
		if (this._move)
		{
			if (this._model != null)
			{
				this._model.GetComponent<Animation>().CrossFade(this._spawner._flapAnimation, 0.5f);
			}
			this._soar = false;
			this.animationSpeed();
			this._wayPoint = this.findWaypoint();
			this._dived = false;
		}
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0002FAF8 File Offset: 0x0002DEF8
	public Vector3 findWaypoint()
	{
		Vector3 zero = Vector3.zero;
		zero.x = UnityEngine.Random.Range(-this._spawner._spawnSphere, this._spawner._spawnSphere) + this._spawner._posBuffer.x;
		zero.z = UnityEngine.Random.Range(-this._spawner._spawnSphereDepth, this._spawner._spawnSphereDepth) + this._spawner._posBuffer.z;
		zero.y = UnityEngine.Random.Range(-this._spawner._spawnSphereHeight, this._spawner._spawnSphereHeight) + this._spawner._posBuffer.y;
		return zero;
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0002FBA8 File Offset: 0x0002DFA8
	public void Soar()
	{
		if (this._move)
		{
			this._model.GetComponent<Animation>().CrossFade(this._spawner._soarAnimation, 1.5f);
			this._wayPoint = this.findWaypoint();
			this._soar = true;
		}
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x0002FBE8 File Offset: 0x0002DFE8
	public void Dive()
	{
		if (this._spawner._soarAnimation != null)
		{
			this._model.GetComponent<Animation>().CrossFade(this._spawner._soarAnimation, 1.5f);
		}
		else
		{
			IEnumerator enumerator = this._model.GetComponent<Animation>().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					AnimationState animationState = (AnimationState)obj;
					if (this._thisT.position.y < this._wayPoint.y + 25f)
					{
						animationState.speed = 0.1f;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		this._wayPoint = this.findWaypoint();
		this._wayPoint.y = this._wayPoint.y - this._spawner._diveValue;
		this._dived = true;
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0002FCE4 File Offset: 0x0002E0E4
	public void animationSpeed()
	{
		IEnumerator enumerator = this._model.GetComponent<Animation>().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AnimationState animationState = (AnimationState)obj;
				if (!this._dived && !this._landing)
				{
					animationState.speed = UnityEngine.Random.Range(this._spawner._minAnimationSpeed, this._spawner._maxAnimationSpeed);
				}
				else
				{
					animationState.speed = this._spawner._maxAnimationSpeed;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x04000AE8 RID: 2792
	[HideInInspector]
	public FlockController _spawner;

	// Token: 0x04000AE9 RID: 2793
	[HideInInspector]
	public Vector3 _wayPoint;

	// Token: 0x04000AEA RID: 2794
	public float _speed;

	// Token: 0x04000AEB RID: 2795
	[HideInInspector]
	public bool _dived = true;

	// Token: 0x04000AEC RID: 2796
	[HideInInspector]
	public float _stuckCounter;

	// Token: 0x04000AED RID: 2797
	[HideInInspector]
	public float _damping;

	// Token: 0x04000AEE RID: 2798
	[HideInInspector]
	public bool _soar = true;

	// Token: 0x04000AEF RID: 2799
	[HideInInspector]
	public bool _landing;

	// Token: 0x04000AF0 RID: 2800
	[HideInInspector]
	public float _targetSpeed;

	// Token: 0x04000AF1 RID: 2801
	[HideInInspector]
	public bool _move = true;

	// Token: 0x04000AF2 RID: 2802
	public GameObject _model;

	// Token: 0x04000AF3 RID: 2803
	public Transform _modelT;

	// Token: 0x04000AF4 RID: 2804
	[HideInInspector]
	public float _avoidValue;

	// Token: 0x04000AF5 RID: 2805
	[HideInInspector]
	public float _avoidDistance;

	// Token: 0x04000AF6 RID: 2806
	private float _soarTimer;

	// Token: 0x04000AF7 RID: 2807
	private bool _instantiated;

	// Token: 0x04000AF8 RID: 2808
	private static int _updateNextSeed;

	// Token: 0x04000AF9 RID: 2809
	private int _updateSeed = -1;

	// Token: 0x04000AFA RID: 2810
	[HideInInspector]
	public bool _avoid = true;

	// Token: 0x04000AFB RID: 2811
	public Transform _thisT;

	// Token: 0x04000AFC RID: 2812
	public Vector3 _landingPosOffset;
}
