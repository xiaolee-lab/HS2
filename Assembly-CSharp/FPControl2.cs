using System;
using UnityEngine;

// Token: 0x02000A5F RID: 2655
public class FPControl2 : MonoBehaviour
{
	// Token: 0x06004EB6 RID: 20150 RVA: 0x001E2FC8 File Offset: 0x001E13C8
	private void ChangeWeapon()
	{
		if (this._currentWeapon != null)
		{
			this._currentWeapon.OnRemove();
		}
		this._currentWeaponIdx = (this._currentWeaponIdx + 1) % this.weapons.Length;
		this._currentWeapon = this.weapons[this._currentWeaponIdx];
		this._currentWeapon.OnEquip();
	}

	// Token: 0x06004EB7 RID: 20151 RVA: 0x001E3028 File Offset: 0x001E1428
	private void Start()
	{
		Vector3 forward = this.myCamera.transform.forward;
		this._phi = Mathf.Asin(forward.y);
		this._theta = Mathf.Atan2(forward.x, forward.z);
		this._mx = Input.mousePosition.x;
		this._my = Input.mousePosition.y;
		this.ChangeWeapon();
	}

	// Token: 0x06004EB8 RID: 20152 RVA: 0x001E30A0 File Offset: 0x001E14A0
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
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			this.ChangeWeapon();
		}
	}

	// Token: 0x06004EB9 RID: 20153 RVA: 0x001E32D8 File Offset: 0x001E16D8
	public void Shoot()
	{
		Ray ray = this.myCamera.ScreenPointToRay(new Vector3((float)this.myCamera.pixelWidth * 0.5f, (float)this.myCamera.pixelHeight * 0.5f, 0f));
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit))
		{
			Transform transform = raycastHit.collider.transform;
			DungeonControl2 dungeonControl = Utils.FindComponentInParents<DungeonControl2>(transform);
			if (dungeonControl != null)
			{
				this._currentWeapon.Shoot(dungeonControl, this.myCamera.transform.position, raycastHit.point);
			}
		}
	}

	// Token: 0x040047A2 RID: 18338
	public Camera myCamera;

	// Token: 0x040047A3 RID: 18339
	public CharacterController cc;

	// Token: 0x040047A4 RID: 18340
	public float walkSpeed = 3f;

	// Token: 0x040047A5 RID: 18341
	private float _theta;

	// Token: 0x040047A6 RID: 18342
	private float _phi;

	// Token: 0x040047A7 RID: 18343
	public float rotSpeed = 1f;

	// Token: 0x040047A8 RID: 18344
	private float _mx;

	// Token: 0x040047A9 RID: 18345
	private float _my;

	// Token: 0x040047AA RID: 18346
	private int _currentWeaponIdx = -1;

	// Token: 0x040047AB RID: 18347
	private Weapon _currentWeapon;

	// Token: 0x040047AC RID: 18348
	public Weapon[] weapons;
}
