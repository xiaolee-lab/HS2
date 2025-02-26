using System;
using UnityEngine;

// Token: 0x02000A5B RID: 2651
public class FPControl : MonoBehaviour
{
	// Token: 0x06004EA8 RID: 20136 RVA: 0x001E2B80 File Offset: 0x001E0F80
	private void Start()
	{
		Vector3 forward = this.myCamera.transform.forward;
		this._phi = Mathf.Asin(forward.y);
		this._theta = Mathf.Atan2(forward.x, forward.z);
		this._mx = Input.mousePosition.x;
		this._my = Input.mousePosition.y;
	}

	// Token: 0x06004EA9 RID: 20137 RVA: 0x001E2BF0 File Offset: 0x001E0FF0
	private void Update()
	{
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		float x = Input.mousePosition.x;
		float y = Input.mousePosition.y;
		float num = x - this._mx;
		float num2 = y - this._my;
		this._mx = x;
		this._my = y;
		Vector3 forward = this.myCamera.transform.forward;
		forward.y = 0f;
		forward.Normalize();
		Vector3 a = Vector3.Cross(Vector3.up, forward);
		Vector3 a2 = this.walkSpeed * (forward * axis2 + a * axis) + Vector3.down * 3f;
		this.cc.Move(a2 * Time.deltaTime);
		this._theta -= num * this.rotSpeed;
		if (this._theta > 3.1415927f)
		{
			this._theta -= 6.2831855f;
		}
		else if (this._theta <= -3.1415927f)
		{
			this._theta += 6.2831855f;
		}
		this._phi += num2 * this.rotSpeed;
		if (this._phi > 1f)
		{
			this._phi = 1f;
		}
		else if (this._phi < -1f)
		{
			this._phi = -1f;
		}
		Vector3 b = new Vector3(Mathf.Cos(this._phi) * Mathf.Cos(this._theta), Mathf.Sin(this._phi), Mathf.Cos(this._phi) * Mathf.Sin(this._theta));
		this.myCamera.transform.LookAt(this.myCamera.transform.position + b, Vector3.up);
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Shoot();
		}
	}

	// Token: 0x06004EAA RID: 20138 RVA: 0x001E2E04 File Offset: 0x001E1204
	public void Shoot()
	{
		Ray ray = this.myCamera.ScreenPointToRay(new Vector3((float)this.myCamera.pixelWidth * 0.5f, (float)this.myCamera.pixelHeight * 0.5f, 0f));
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit))
		{
			Transform transform = raycastHit.collider.transform;
			DungeonControl dungeonControl = Utils.FindComponentInParents<DungeonControl>(transform);
			if (dungeonControl != null)
			{
				dungeonControl.AddCell(raycastHit.point, 2f);
			}
		}
	}

	// Token: 0x04004795 RID: 18325
	public Camera myCamera;

	// Token: 0x04004796 RID: 18326
	public CharacterController cc;

	// Token: 0x04004797 RID: 18327
	public float walkSpeed = 3f;

	// Token: 0x04004798 RID: 18328
	private float _theta;

	// Token: 0x04004799 RID: 18329
	private float _phi;

	// Token: 0x0400479A RID: 18330
	public float rotSpeed = 1f;

	// Token: 0x0400479B RID: 18331
	private float _mx;

	// Token: 0x0400479C RID: 18332
	private float _my;
}
