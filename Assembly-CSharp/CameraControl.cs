using System;
using System.IO;
using Manager;
using UnityEngine;

// Token: 0x020010B9 RID: 4281
public class CameraControl : BaseCameraControl
{
	// Token: 0x17001EFA RID: 7930
	// (get) Token: 0x06008ED2 RID: 36562 RVA: 0x003B6876 File Offset: 0x003B4C76
	// (set) Token: 0x06008ED3 RID: 36563 RVA: 0x003B687E File Offset: 0x003B4C7E
	public bool isOutsideTargetTex { get; set; }

	// Token: 0x06008ED4 RID: 36564 RVA: 0x003B6887 File Offset: 0x003B4C87
	public void SetCenterSC()
	{
	}

	// Token: 0x06008ED5 RID: 36565 RVA: 0x003B6889 File Offset: 0x003B4C89
	public void ChangeDepthOfFieldSetting()
	{
		if (null == Singleton<Manager.Config>.Instance)
		{
			return;
		}
	}

	// Token: 0x06008ED6 RID: 36566 RVA: 0x003B689C File Offset: 0x003B4C9C
	public void UpdateDepthOfFieldSetting()
	{
		if (this.disableShortcut)
		{
			return;
		}
	}

	// Token: 0x06008ED7 RID: 36567 RVA: 0x003B68AC File Offset: 0x003B4CAC
	protected override void Start()
	{
		base.Start();
		this.targetTex = base.transform.Find("CameraTarget");
		if (this.targetTex)
		{
			this.targetTex.localScale = Vector3.one * 0.01f;
		}
		this.isOutsideTargetTex = true;
	}

	// Token: 0x06008ED8 RID: 36568 RVA: 0x003B6908 File Offset: 0x003B4D08
	protected new void LateUpdate()
	{
		this.UpdateDepthOfFieldSetting();
		if (Singleton<Scene>.Instance.sceneFade.IsFadeNow)
		{
			return;
		}
		if (Singleton<Scene>.Instance.IsOverlap)
		{
			return;
		}
		base.LateUpdate();
		if (!this.disableShortcut && UnityEngine.Input.GetKeyDown(KeyCode.Alpha6))
		{
			this.SetCenterSC();
		}
	}

	// Token: 0x06008ED9 RID: 36569 RVA: 0x003B6964 File Offset: 0x003B4D64
	protected new bool InputTouchProc()
	{
		if (base.InputTouchProc())
		{
			float deltaTime = Time.deltaTime;
			if (this.pinchInOut)
			{
				float rate = this.pinchInOut.Rate;
				if (this.pinchInOut.NowState == PinchInOut.State.ScalUp)
				{
					this.CamDat.Dir.z = this.CamDat.Dir.z + rate * deltaTime * this.zoomSpeed;
				}
				else if (this.pinchInOut.NowState == PinchInOut.State.ScalDown)
				{
					this.CamDat.Dir.z = this.CamDat.Dir.z - rate * deltaTime * this.zoomSpeed;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x06008EDA RID: 36570 RVA: 0x003B6A0C File Offset: 0x003B4E0C
	private void Save(BinaryWriter Writer)
	{
		Writer.Write(this.CamDat.Pos.x);
		Writer.Write(this.CamDat.Pos.y);
		Writer.Write(this.CamDat.Pos.z);
		Writer.Write(this.CamDat.Dir.x);
		Writer.Write(this.CamDat.Dir.y);
		Writer.Write(this.CamDat.Dir.z);
		Vector3 eulerAngles = base.transform.rotation.eulerAngles;
		Writer.Write(eulerAngles.x);
		Writer.Write(eulerAngles.y);
		Writer.Write(eulerAngles.z);
	}

	// Token: 0x06008EDB RID: 36571 RVA: 0x003B6AD8 File Offset: 0x003B4ED8
	private void Load(BinaryReader Reader)
	{
		this.CamDat.Pos.x = Reader.ReadSingle();
		this.CamDat.Pos.y = Reader.ReadSingle();
		this.CamDat.Pos.z = Reader.ReadSingle();
		this.CamDat.Dir.x = Reader.ReadSingle();
		this.CamDat.Dir.y = Reader.ReadSingle();
		this.CamDat.Dir.z = Reader.ReadSingle();
		Vector3 eulerAngles = base.transform.rotation.eulerAngles;
		eulerAngles.x = Reader.ReadSingle();
		eulerAngles.y = Reader.ReadSingle();
		eulerAngles.z = Reader.ReadSingle();
		base.transform.rotation = Quaternion.Euler(eulerAngles);
	}

	// Token: 0x04007373 RID: 29555
	public SmartTouch smartTouch;

	// Token: 0x04007374 RID: 29556
	public PinchInOut pinchInOut;

	// Token: 0x04007376 RID: 29558
	private Transform targetTex;

	// Token: 0x04007377 RID: 29559
	public bool disableShortcut;
}
