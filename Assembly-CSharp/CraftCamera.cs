using System;
using AIProject;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000ED1 RID: 3793
public class CraftCamera : VirtualCameraController
{
	// Token: 0x06007C1F RID: 31775 RVA: 0x003469FB File Offset: 0x00344DFB
	private new void Start()
	{
		this.Init();
	}

	// Token: 0x06007C20 RID: 31776 RVA: 0x00346A04 File Offset: 0x00344E04
	private new void Init()
	{
		base.Init();
		this.bLock = false;
		this.nFloorCnt = 0;
		this.limitPos = 10f;
		this.limitDir = 50f;
		this.isLimitPos = true;
		this.isLimitDir = true;
		this.isLimitRotX = true;
		this.CamDat.Pos = this.initPos;
		this.craft = true;
	}

	// Token: 0x06007C21 RID: 31777 RVA: 0x00346A68 File Offset: 0x00344E68
	protected override void LateUpdate()
	{
		if (this.bLock)
		{
			return;
		}
		base.LateUpdate();
		if (!base.isControlNow)
		{
			base.isControlNow |= this.InputMouseWheelZoomProc();
		}
		if (!(EventSystem.current != null) || EventSystem.current.IsPointerOverGameObject())
		{
			base.isControlNow = false;
		}
	}

	// Token: 0x06007C22 RID: 31778 RVA: 0x00346ACC File Offset: 0x00344ECC
	public void CameraUp(int TargetFloorCnt)
	{
		if (base.isControlNow)
		{
			return;
		}
		this.nFloorCnt = TargetFloorCnt;
		this.CamDat.Pos.y = this.CamReset.Pos.y + (float)(5 * this.nFloorCnt);
		this.ForceMoveCam(0f);
	}

	// Token: 0x06007C23 RID: 31779 RVA: 0x00346B24 File Offset: 0x00344F24
	private bool InputMouseWheelZoomProc()
	{
		bool result = false;
		this.isZoomNow = false;
		float num = this.input.ScrollValue() * this.zoomSpeed;
		if (num != 0f)
		{
			this.CamDat.Fov = this.CamDat.Fov - num;
			this.CamDat.Fov = Mathf.Clamp(this.CamDat.Fov, this.CamReset.Fov, this.limitFov);
			this.Lens.FieldOfView = this.CamDat.Fov;
			this.m_State.Lens = this.Lens;
			result = true;
			this.isZoomNow = true;
		}
		return result;
	}

	// Token: 0x06007C24 RID: 31780 RVA: 0x00346BC8 File Offset: 0x00344FC8
	public void setLimitPos(float limit)
	{
		this.limitPos += limit;
	}

	// Token: 0x040063E1 RID: 25569
	public bool bLock;

	// Token: 0x040063E2 RID: 25570
	public int nFloorCnt;

	// Token: 0x040063E3 RID: 25571
	public Vector3 initPos;

	// Token: 0x040063E4 RID: 25572
	public float zoomSpeed;
}
