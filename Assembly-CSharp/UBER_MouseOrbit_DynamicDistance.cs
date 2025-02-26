using System;
using UnityEngine;

// Token: 0x0200061A RID: 1562
[AddComponentMenu("UBER/Mouse Orbit - Dynamic Distance")]
public class UBER_MouseOrbit_DynamicDistance : MonoBehaviour
{
	// Token: 0x0600252D RID: 9517 RVA: 0x000D401C File Offset: 0x000D241C
	private void Start()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		this.x = eulerAngles.y;
		this.y = eulerAngles.x;
		this.Reset();
	}

	// Token: 0x0600252E RID: 9518 RVA: 0x000D4055 File Offset: 0x000D2455
	public void DisableSteering(bool state)
	{
		this.disableSteering = state;
	}

	// Token: 0x0600252F RID: 9519 RVA: 0x000D4060 File Offset: 0x000D2460
	public void Reset()
	{
		this.lastLMBState = Input.GetMouseButton(0);
		this.disableSteering = false;
		this.cur_distance = this.distance;
		this.cur_xSpeed = 0f;
		this.cur_ySpeed = 0f;
		this.req_xSpeed = 0f;
		this.req_ySpeed = 0f;
		this.surfaceColliders = null;
		this.cur_ObjxSpeed = 0f;
		this.cur_ObjySpeed = 0f;
		this.req_ObjxSpeed = 0f;
		this.req_ObjySpeed = 0f;
		if (this.target)
		{
			Renderer[] componentsInChildren = this.target.GetComponentsInChildren<Renderer>();
			Bounds bounds = default(Bounds);
			bool flag = false;
			foreach (Renderer renderer in componentsInChildren)
			{
				if (!flag)
				{
					flag = true;
					bounds = renderer.bounds;
				}
				else
				{
					bounds.Encapsulate(renderer.bounds);
				}
			}
			Vector3 size = bounds.size;
			float num = (size.x <= size.y) ? size.y : size.x;
			num = ((size.z <= num) ? num : size.z);
			this.bounds_MaxSize = num;
			this.cur_distance += this.bounds_MaxSize * 1.2f;
			this.surfaceColliders = this.target.GetComponentsInChildren<Collider>();
		}
	}

	// Token: 0x06002530 RID: 9520 RVA: 0x000D41D8 File Offset: 0x000D25D8
	private void LateUpdate()
	{
		if (this.target && this.targetFocus)
		{
			if (!this.lastLMBState && Input.GetMouseButton(0))
			{
				this.DraggingObject = false;
				if (this.surfaceColliders != null)
				{
					RaycastHit raycastHit = default(RaycastHit);
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					foreach (Collider collider in this.surfaceColliders)
					{
						if (collider.Raycast(ray, out raycastHit, float.PositiveInfinity))
						{
							this.DraggingObject = true;
							break;
						}
					}
				}
			}
			else if (this.lastLMBState && !Input.GetMouseButton(0))
			{
				this.DraggingObject = false;
			}
			this.lastLMBState = Input.GetMouseButton(0);
			if (this.DraggingObject)
			{
				if (Input.GetMouseButton(0) && !this.disableSteering)
				{
					this.req_ObjxSpeed += (Input.GetAxis("Mouse X") * this.xObjSpeed * 0.02f - this.req_ObjxSpeed) * Time.deltaTime * 10f;
					this.req_ObjySpeed += (Input.GetAxis("Mouse Y") * this.yObjSpeed * 0.02f - this.req_ObjySpeed) * Time.deltaTime * 10f;
				}
				else
				{
					this.req_ObjxSpeed += (0f - this.req_ObjxSpeed) * Time.deltaTime * 4f;
					this.req_ObjySpeed += (0f - this.req_ObjySpeed) * Time.deltaTime * 4f;
				}
				this.req_xSpeed += (0f - this.req_xSpeed) * Time.deltaTime * 4f;
				this.req_ySpeed += (0f - this.req_ySpeed) * Time.deltaTime * 4f;
			}
			else
			{
				if (Input.GetMouseButton(0) && !this.disableSteering)
				{
					this.req_xSpeed += (Input.GetAxis("Mouse X") * this.xSpeed * 0.02f - this.req_xSpeed) * Time.deltaTime * 10f;
					this.req_ySpeed += (Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f - this.req_ySpeed) * Time.deltaTime * 10f;
				}
				else
				{
					this.req_xSpeed += (0f - this.req_xSpeed) * Time.deltaTime * 4f;
					this.req_ySpeed += (0f - this.req_ySpeed) * Time.deltaTime * 4f;
				}
				this.req_ObjxSpeed += (0f - this.req_ObjxSpeed) * Time.deltaTime * 4f;
				this.req_ObjySpeed += (0f - this.req_ObjySpeed) * Time.deltaTime * 4f;
			}
			this.distance -= Input.GetAxis("Mouse ScrollWheel") * this.ZoomWheelSpeed;
			this.distance = Mathf.Clamp(this.distance, this.minDistance, this.maxDistance);
			this.cur_ObjxSpeed += (this.req_ObjxSpeed - this.cur_ObjxSpeed) * Time.deltaTime * 20f;
			this.cur_ObjySpeed += (this.req_ObjySpeed - this.cur_ObjySpeed) * Time.deltaTime * 20f;
			this.target.transform.RotateAround(this.targetFocus.position, Vector3.Cross(this.targetFocus.position - base.transform.position, base.transform.right), -this.cur_ObjxSpeed);
			this.target.transform.RotateAround(this.targetFocus.position, Vector3.Cross(this.targetFocus.position - base.transform.position, base.transform.up), -this.cur_ObjySpeed);
			this.cur_xSpeed += (this.req_xSpeed - this.cur_xSpeed) * Time.deltaTime * 20f;
			this.cur_ySpeed += (this.req_ySpeed - this.cur_ySpeed) * Time.deltaTime * 20f;
			this.x += this.cur_xSpeed;
			this.y -= this.cur_ySpeed;
			this.y = UBER_MouseOrbit_DynamicDistance.ClampAngle(this.y, this.yMinLimit + this.normal_angle, this.yMaxLimit + this.normal_angle);
			if (this.surfaceColliders != null)
			{
				RaycastHit raycastHit2 = default(RaycastHit);
				Vector3 vector = Vector3.Normalize(this.targetFocus.position - base.transform.position);
				float num = 0.01f;
				bool flag = false;
				foreach (Collider collider2 in this.surfaceColliders)
				{
					if (collider2.Raycast(new Ray(base.transform.position - vector * this.bounds_MaxSize, vector), out raycastHit2, float.PositiveInfinity))
					{
						num = Mathf.Max(Vector3.Distance(raycastHit2.point, this.targetFocus.position) + this.distance, num);
						flag = true;
					}
				}
				if (flag)
				{
					this.cur_distance += (num - this.cur_distance) * Time.deltaTime * 4f;
				}
			}
			Quaternion rotation = Quaternion.Euler(this.y, this.x, 0f);
			Vector3 position = rotation * new Vector3(0f, 0f, -this.cur_distance) + this.targetFocus.position;
			base.transform.rotation = rotation;
			base.transform.position = position;
		}
	}

	// Token: 0x06002531 RID: 9521 RVA: 0x000D4810 File Offset: 0x000D2C10
	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x06002532 RID: 9522 RVA: 0x000D4842 File Offset: 0x000D2C42
	public void set_normal_angle(float a)
	{
		this.normal_angle = a;
	}

	// Token: 0x040024BC RID: 9404
	public GameObject target;

	// Token: 0x040024BD RID: 9405
	public Transform targetFocus;

	// Token: 0x040024BE RID: 9406
	public float distance = 1f;

	// Token: 0x040024BF RID: 9407
	[Range(0.1f, 4f)]
	public float ZoomWheelSpeed = 4f;

	// Token: 0x040024C0 RID: 9408
	public float minDistance = 0.5f;

	// Token: 0x040024C1 RID: 9409
	public float maxDistance = 10f;

	// Token: 0x040024C2 RID: 9410
	public float xSpeed = 250f;

	// Token: 0x040024C3 RID: 9411
	public float ySpeed = 120f;

	// Token: 0x040024C4 RID: 9412
	public float xObjSpeed = 250f;

	// Token: 0x040024C5 RID: 9413
	public float yObjSpeed = 120f;

	// Token: 0x040024C6 RID: 9414
	public float yMinLimit = -20f;

	// Token: 0x040024C7 RID: 9415
	public float yMaxLimit = 80f;

	// Token: 0x040024C8 RID: 9416
	private float x;

	// Token: 0x040024C9 RID: 9417
	private float y;

	// Token: 0x040024CA RID: 9418
	private float normal_angle;

	// Token: 0x040024CB RID: 9419
	private float cur_distance;

	// Token: 0x040024CC RID: 9420
	private float cur_xSpeed;

	// Token: 0x040024CD RID: 9421
	private float cur_ySpeed;

	// Token: 0x040024CE RID: 9422
	private float req_xSpeed;

	// Token: 0x040024CF RID: 9423
	private float req_ySpeed;

	// Token: 0x040024D0 RID: 9424
	private float cur_ObjxSpeed;

	// Token: 0x040024D1 RID: 9425
	private float cur_ObjySpeed;

	// Token: 0x040024D2 RID: 9426
	private float req_ObjxSpeed;

	// Token: 0x040024D3 RID: 9427
	private float req_ObjySpeed;

	// Token: 0x040024D4 RID: 9428
	private bool DraggingObject;

	// Token: 0x040024D5 RID: 9429
	private bool lastLMBState;

	// Token: 0x040024D6 RID: 9430
	private Collider[] surfaceColliders;

	// Token: 0x040024D7 RID: 9431
	private float bounds_MaxSize = 20f;

	// Token: 0x040024D8 RID: 9432
	[HideInInspector]
	public bool disableSteering;
}
