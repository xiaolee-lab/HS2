using System;
using UnityEngine;

// Token: 0x02000446 RID: 1094
public class ProjectileCollisionBehaviour : MonoBehaviour
{
	// Token: 0x06001409 RID: 5129 RVA: 0x0007D210 File Offset: 0x0007B610
	private void GetEffectSettingsComponent(Transform tr)
	{
		Transform parent = tr.parent;
		if (parent != null)
		{
			this.effectSettings = parent.GetComponentInChildren<EffectSettings>();
			if (this.effectSettings == null)
			{
				this.GetEffectSettingsComponent(parent.transform);
			}
		}
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x0007D25C File Offset: 0x0007B65C
	private void Start()
	{
		this.t = base.transform;
		this.GetEffectSettingsComponent(this.t);
		if (this.effectSettings == null)
		{
		}
		if (!this.IsRootMove)
		{
			this.startParentPosition = base.transform.parent.position;
		}
		if (this.GoLight != null)
		{
			this.tLight = this.GoLight.transform;
		}
		this.InitializeDefault();
		this.isInitializedOnStart = true;
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x0007D2E2 File Offset: 0x0007B6E2
	private void OnEnable()
	{
		if (this.isInitializedOnStart)
		{
			this.InitializeDefault();
		}
	}

	// Token: 0x0600140C RID: 5132 RVA: 0x0007D2F5 File Offset: 0x0007B6F5
	private void OnDisable()
	{
		if (this.ResetParentPositionOnDisable && this.isInitializedOnStart && !this.IsRootMove)
		{
			base.transform.parent.position = this.startParentPosition;
		}
	}

	// Token: 0x0600140D RID: 5133 RVA: 0x0007D330 File Offset: 0x0007B730
	private void InitializeDefault()
	{
		this.hit = default(RaycastHit);
		this.onCollision = false;
		this.smootRandomPos = default(Vector3);
		this.oldSmootRandomPos = default(Vector3);
		this.deltaSpeed = 0f;
		this.startTime = 0f;
		this.randomSpeed = 0f;
		this.randomRadiusX = 0f;
		this.randomRadiusY = 0f;
		this.randomDirection1 = 0;
		this.randomDirection2 = 0;
		this.randomDirection3 = 0;
		this.frameDroped = false;
		this.tRoot = ((!this.IsRootMove) ? base.transform.parent : this.effectSettings.transform);
		this.startPosition = this.tRoot.position;
		if (this.effectSettings.Target != null)
		{
			this.tTarget = this.effectSettings.Target.transform;
		}
		else if (!this.effectSettings.UseMoveVector)
		{
		}
		if ((double)this.effectSettings.EffectRadius > 0.001)
		{
			Vector2 vector = UnityEngine.Random.insideUnitCircle * this.effectSettings.EffectRadius;
			this.randomTargetOffsetXZVector = new Vector3(vector.x, 0f, vector.y);
		}
		else
		{
			this.randomTargetOffsetXZVector = Vector3.zero;
		}
		if (!this.effectSettings.UseMoveVector)
		{
			this.forwardDirection = this.tRoot.position + (this.tTarget.position + this.randomTargetOffsetXZVector - this.tRoot.position).normalized * this.effectSettings.MoveDistance;
			this.GetTargetHit();
		}
		else
		{
			this.forwardDirection = this.tRoot.position + this.effectSettings.MoveVector * this.effectSettings.MoveDistance;
		}
		if (this.IsLookAt)
		{
			if (!this.effectSettings.UseMoveVector)
			{
				this.tRoot.LookAt(this.tTarget);
			}
			else
			{
				this.tRoot.LookAt(this.forwardDirection);
			}
		}
		this.InitRandomVariables();
	}

	// Token: 0x0600140E RID: 5134 RVA: 0x0007D58C File Offset: 0x0007B98C
	private void Update()
	{
		if (!this.frameDroped)
		{
			this.frameDroped = true;
			return;
		}
		if (((!this.effectSettings.UseMoveVector && this.tTarget == null) || this.onCollision) && this.frameDroped)
		{
			return;
		}
		Vector3 vector;
		if (!this.effectSettings.UseMoveVector)
		{
			vector = ((!this.effectSettings.IsHomingMove) ? this.forwardDirection : this.tTarget.position);
		}
		else
		{
			vector = this.forwardDirection;
		}
		float num = Vector3.Distance(this.tRoot.position, vector);
		float num2 = this.effectSettings.MoveSpeed * Time.deltaTime;
		if (num2 > num)
		{
			num2 = num;
		}
		if (num <= this.effectSettings.ColliderRadius)
		{
			this.hit = default(RaycastHit);
			this.CollisionEnter();
		}
		Vector3 normalized = (vector - this.tRoot.position).normalized;
		RaycastHit raycastHit;
		if (Physics.Raycast(this.tRoot.position, normalized, out raycastHit, num2 + this.effectSettings.ColliderRadius, this.effectSettings.LayerMask))
		{
			this.hit = raycastHit;
			vector = raycastHit.point - normalized * this.effectSettings.ColliderRadius;
			this.CollisionEnter();
		}
		if (this.IsCenterLightPosition && this.GoLight != null)
		{
			this.tLight.position = (this.startPosition + this.tRoot.position) / 2f;
		}
		Vector3 b = default(Vector3);
		if (this.RandomMoveCoordinates != RandomMoveCoordinates.None)
		{
			this.UpdateSmootRandomhPos();
			b = this.smootRandomPos - this.oldSmootRandomPos;
		}
		float num3 = 1f;
		if (this.Acceleration.length > 0)
		{
			float time = (Time.time - this.startTime) / this.AcceleraionTime;
			num3 = this.Acceleration.Evaluate(time);
		}
		Vector3 vector2 = Vector3.MoveTowards(this.tRoot.position, vector, this.effectSettings.MoveSpeed * Time.deltaTime * num3);
		Vector3 vector3 = vector2 + b;
		if (this.IsLookAt && this.effectSettings.IsHomingMove)
		{
			this.tRoot.LookAt(vector3);
		}
		if (this.IsLocalSpaceRandomMove && this.IsRootMove)
		{
			this.tRoot.position = vector2;
			this.t.localPosition += b;
		}
		else
		{
			this.tRoot.position = vector3;
		}
		this.oldSmootRandomPos = this.smootRandomPos;
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x0007D85C File Offset: 0x0007BC5C
	private void CollisionEnter()
	{
		if (this.EffectOnHitObject != null && this.hit.transform != null)
		{
			Transform transform = this.hit.transform;
			Renderer componentInChildren = transform.GetComponentInChildren<Renderer>();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EffectOnHitObject);
			gameObject.transform.parent = componentInChildren.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.GetComponent<AddMaterialOnHit>().UpdateMaterial(this.hit);
		}
		if (this.AttachAfterCollision)
		{
			this.tRoot.parent = this.hit.transform;
		}
		if (this.SendCollisionMessage)
		{
			CollisionInfo e = new CollisionInfo
			{
				Hit = this.hit
			};
			this.effectSettings.OnCollisionHandler(e);
			if (this.hit.transform != null)
			{
				ShieldCollisionBehaviour component = this.hit.transform.GetComponent<ShieldCollisionBehaviour>();
				if (component != null)
				{
					component.ShieldCollisionEnter(e);
				}
			}
		}
		this.onCollision = true;
	}

	// Token: 0x06001410 RID: 5136 RVA: 0x0007D974 File Offset: 0x0007BD74
	private void InitRandomVariables()
	{
		this.deltaSpeed = this.RandomMoveSpeed * UnityEngine.Random.Range(1f, 1000f * this.RandomRange + 1f) / 1000f - 1f;
		this.startTime = Time.time;
		this.randomRadiusX = UnityEngine.Random.Range(this.RandomMoveRadius / 20f, this.RandomMoveRadius * 100f) / 100f;
		this.randomRadiusY = UnityEngine.Random.Range(this.RandomMoveRadius / 20f, this.RandomMoveRadius * 100f) / 100f;
		this.randomSpeed = UnityEngine.Random.Range(this.RandomMoveSpeed / 20f, this.RandomMoveSpeed * 100f) / 100f;
		this.randomDirection1 = ((UnityEngine.Random.Range(0, 2) <= 0) ? -1 : 1);
		this.randomDirection2 = ((UnityEngine.Random.Range(0, 2) <= 0) ? -1 : 1);
		this.randomDirection3 = ((UnityEngine.Random.Range(0, 2) <= 0) ? -1 : 1);
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x0007DA8C File Offset: 0x0007BE8C
	private void GetTargetHit()
	{
		Ray ray = new Ray(this.tRoot.position, Vector3.Normalize(this.tTarget.position + this.randomTargetOffsetXZVector - this.tRoot.position));
		Collider componentInChildren = this.tTarget.GetComponentInChildren<Collider>();
		RaycastHit raycastHit;
		if (componentInChildren != null && componentInChildren.Raycast(ray, out raycastHit, this.effectSettings.MoveDistance))
		{
			this.hit = raycastHit;
		}
	}

	// Token: 0x06001412 RID: 5138 RVA: 0x0007DB10 File Offset: 0x0007BF10
	private void UpdateSmootRandomhPos()
	{
		float num = Time.time - this.startTime;
		float num2 = num * this.randomSpeed;
		float f = num * this.deltaSpeed;
		float num4;
		float num5;
		if (this.IsDeviation)
		{
			float num3 = Vector3.Distance(this.tRoot.position, this.hit.point) / this.effectSettings.MoveDistance;
			num4 = (float)this.randomDirection2 * Mathf.Sin(num2) * this.randomRadiusX * num3;
			num5 = (float)this.randomDirection3 * Mathf.Sin(num2 + (float)this.randomDirection1 * 3.1415927f / 2f * num + Mathf.Sin(f)) * this.randomRadiusY * num3;
		}
		else
		{
			num4 = (float)this.randomDirection2 * Mathf.Sin(num2) * this.randomRadiusX;
			num5 = (float)this.randomDirection3 * Mathf.Sin(num2 + (float)this.randomDirection1 * 3.1415927f / 2f * num + Mathf.Sin(f)) * this.randomRadiusY;
		}
		if (this.RandomMoveCoordinates == RandomMoveCoordinates.XY)
		{
			this.smootRandomPos = new Vector3(num4, num5, 0f);
		}
		if (this.RandomMoveCoordinates == RandomMoveCoordinates.XZ)
		{
			this.smootRandomPos = new Vector3(num4, 0f, num5);
		}
		if (this.RandomMoveCoordinates == RandomMoveCoordinates.YZ)
		{
			this.smootRandomPos = new Vector3(0f, num4, num5);
		}
		if (this.RandomMoveCoordinates == RandomMoveCoordinates.XYZ)
		{
			this.smootRandomPos = new Vector3(num4, num5, (num4 + num5) / 2f * (float)this.randomDirection1);
		}
	}

	// Token: 0x040016AA RID: 5802
	public float RandomMoveRadius;

	// Token: 0x040016AB RID: 5803
	public float RandomMoveSpeed;

	// Token: 0x040016AC RID: 5804
	public float RandomRange;

	// Token: 0x040016AD RID: 5805
	public RandomMoveCoordinates RandomMoveCoordinates;

	// Token: 0x040016AE RID: 5806
	public GameObject EffectOnHitObject;

	// Token: 0x040016AF RID: 5807
	public GameObject GoLight;

	// Token: 0x040016B0 RID: 5808
	public AnimationCurve Acceleration;

	// Token: 0x040016B1 RID: 5809
	public float AcceleraionTime = 1f;

	// Token: 0x040016B2 RID: 5810
	public bool IsCenterLightPosition;

	// Token: 0x040016B3 RID: 5811
	public bool IsLookAt;

	// Token: 0x040016B4 RID: 5812
	public bool AttachAfterCollision;

	// Token: 0x040016B5 RID: 5813
	public bool IsRootMove = true;

	// Token: 0x040016B6 RID: 5814
	public bool IsLocalSpaceRandomMove;

	// Token: 0x040016B7 RID: 5815
	public bool IsDeviation;

	// Token: 0x040016B8 RID: 5816
	public bool SendCollisionMessage = true;

	// Token: 0x040016B9 RID: 5817
	public bool ResetParentPositionOnDisable;

	// Token: 0x040016BA RID: 5818
	private EffectSettings effectSettings;

	// Token: 0x040016BB RID: 5819
	private Transform tRoot;

	// Token: 0x040016BC RID: 5820
	private Transform tTarget;

	// Token: 0x040016BD RID: 5821
	private Transform t;

	// Token: 0x040016BE RID: 5822
	private Transform tLight;

	// Token: 0x040016BF RID: 5823
	private Vector3 forwardDirection;

	// Token: 0x040016C0 RID: 5824
	private Vector3 startPosition;

	// Token: 0x040016C1 RID: 5825
	private Vector3 startParentPosition;

	// Token: 0x040016C2 RID: 5826
	private RaycastHit hit;

	// Token: 0x040016C3 RID: 5827
	private Vector3 smootRandomPos;

	// Token: 0x040016C4 RID: 5828
	private Vector3 oldSmootRandomPos;

	// Token: 0x040016C5 RID: 5829
	private float deltaSpeed;

	// Token: 0x040016C6 RID: 5830
	private float startTime;

	// Token: 0x040016C7 RID: 5831
	private float randomSpeed;

	// Token: 0x040016C8 RID: 5832
	private float randomRadiusX;

	// Token: 0x040016C9 RID: 5833
	private float randomRadiusY;

	// Token: 0x040016CA RID: 5834
	private int randomDirection1;

	// Token: 0x040016CB RID: 5835
	private int randomDirection2;

	// Token: 0x040016CC RID: 5836
	private int randomDirection3;

	// Token: 0x040016CD RID: 5837
	private bool onCollision;

	// Token: 0x040016CE RID: 5838
	private bool isInitializedOnStart;

	// Token: 0x040016CF RID: 5839
	private Vector3 randomTargetOffsetXZVector;

	// Token: 0x040016D0 RID: 5840
	private bool frameDroped;
}
