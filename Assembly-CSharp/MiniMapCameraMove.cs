using System;
using Manager;
using UnityEngine;

// Token: 0x02000FB8 RID: 4024
public class MiniMapCameraMove : MonoBehaviour
{
	// Token: 0x060085D5 RID: 34261 RVA: 0x0037B90E File Offset: 0x00379D0E
	private void Start()
	{
	}

	// Token: 0x060085D6 RID: 34262 RVA: 0x0037B910 File Offset: 0x00379D10
	public void Init()
	{
		Vector3 position = Singleton<Map>.Instance.Player.Position;
		position.y = base.transform.position.y;
		base.transform.position = position;
	}

	// Token: 0x060085D7 RID: 34263 RVA: 0x0037B953 File Offset: 0x00379D53
	private void Update()
	{
		if (!Singleton<Map>.IsInstance())
		{
			return;
		}
		this.MoveCamera();
	}

	// Token: 0x060085D8 RID: 34264 RVA: 0x0037B968 File Offset: 0x00379D68
	private void MoveCamera()
	{
		if (Singleton<Map>.Instance.Player == null)
		{
			return;
		}
		Vector3 position = Singleton<Map>.Instance.Player.Position;
		position.x = Mathf.Clamp(position.x, this.MoveMinX, this.MoveMaxX);
		position.y = base.transform.position.y;
		position.z = Mathf.Clamp(position.z, this.MoveMinZ, this.MoveMaxZ);
		base.transform.position = position;
	}

	// Token: 0x04006CA9 RID: 27817
	public float MoveMinX = -23.6f;

	// Token: 0x04006CAA RID: 27818
	public float MoveMaxX = 24.8f;

	// Token: 0x04006CAB RID: 27819
	public float MoveMinZ = -47.6f;

	// Token: 0x04006CAC RID: 27820
	public float MoveMaxZ = 47f;
}
