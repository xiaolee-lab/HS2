using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Manager;
using UnityEngine;

// Token: 0x02000818 RID: 2072
public class CameraControl_Ver2 : BaseCameraControl_Ver2
{
	// Token: 0x1700098B RID: 2443
	// (get) Token: 0x060034B7 RID: 13495 RVA: 0x00136DAB File Offset: 0x001351AB
	// (set) Token: 0x060034B8 RID: 13496 RVA: 0x00136DB3 File Offset: 0x001351B3
	public bool isOutsideTargetTex { get; set; }

	// Token: 0x1700098C RID: 2444
	// (get) Token: 0x060034B9 RID: 13497 RVA: 0x00136DBC File Offset: 0x001351BC
	// (set) Token: 0x060034BA RID: 13498 RVA: 0x00136DC4 File Offset: 0x001351C4
	public bool isCursorLock { get; set; }

	// Token: 0x1700098D RID: 2445
	// (get) Token: 0x060034BB RID: 13499 RVA: 0x00136DCD File Offset: 0x001351CD
	// (set) Token: 0x060034BC RID: 13500 RVA: 0x00136DD5 File Offset: 0x001351D5
	public bool isConfigTargetTex { get; set; }

	// Token: 0x1700098E RID: 2446
	// (get) Token: 0x060034BD RID: 13501 RVA: 0x00136DDE File Offset: 0x001351DE
	// (set) Token: 0x060034BE RID: 13502 RVA: 0x00136DE6 File Offset: 0x001351E6
	public bool ConfigVanish
	{
		get
		{
			return this.isConfigVanish;
		}
		set
		{
			if (this.isConfigVanish != value)
			{
				this.isConfigVanish = value;
				this.visibleFroceVanish(true);
			}
		}
	}

	// Token: 0x1700098F RID: 2447
	// (get) Token: 0x060034BF RID: 13503 RVA: 0x00136E02 File Offset: 0x00135202
	// (set) Token: 0x060034C0 RID: 13504 RVA: 0x00136E0A File Offset: 0x0013520A
	public Transform targetTex { get; private set; }

	// Token: 0x060034C1 RID: 13505 RVA: 0x00136E14 File Offset: 0x00135214
	protected new void Start()
	{
		base.Start();
		this.targetTex = base.transform.Find("CameraTarget");
		if (this.targetTex)
		{
			this.targetTex.localScale = Vector3.one * 0.1f;
			this.targetRender = this.targetTex.GetComponent<Renderer>();
		}
		this.isOutsideTargetTex = true;
		this.isConfigTargetTex = true;
		this.isConfigVanish = true;
		this.isCursorLock = true;
		this.viewCollider = base.gameObject.AddComponent<CapsuleCollider>();
		this.viewCollider.radius = 0.05f;
		this.viewCollider.isTrigger = true;
		this.viewCollider.direction = 2;
		Rigidbody orAddComponent = base.gameObject.GetOrAddComponent<Rigidbody>();
		orAddComponent.useGravity = false;
		orAddComponent.isKinematic = true;
		this.isInit = true;
		this.listCollider.Clear();
	}

	// Token: 0x060034C2 RID: 13506 RVA: 0x00136EFC File Offset: 0x001352FC
	protected new void LateUpdate()
	{
		if (Singleton<Scene>.IsInstance())
		{
			if (Singleton<Scene>.Instance.NowSceneNames.Any((string s) => s == "Config"))
			{
				return;
			}
			if (!Singleton<Scene>.Instance.IsNowLoading && !Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				base.LateUpdate();
			}
		}
		if (this.targetTex)
		{
			if (this.transBase != null)
			{
				this.targetTex.position = this.transBase.TransformPoint(this.CamDat.Pos);
			}
			else
			{
				this.targetTex.position = this.CamDat.Pos;
			}
			Vector3 position = base.transform.position;
			position.y = this.targetTex.position.y;
			this.targetTex.transform.LookAt(position);
			this.targetTex.Rotate(new Vector3(90f, 0f, 0f));
			if (null != this.targetRender)
			{
				this.targetRender.enabled = (base.isControlNow & this.isOutsideTargetTex & this.isConfigTargetTex);
			}
			if (Singleton<GameCursor>.IsInstance() && this.isCursorLock)
			{
				Singleton<GameCursor>.Instance.SetCursorLock(base.isControlNow & this.isOutsideTargetTex);
			}
		}
		this.VanishProc();
	}

	// Token: 0x060034C3 RID: 13507 RVA: 0x0013707F File Offset: 0x0013547F
	private void OnDisable()
	{
		this.visibleFroceVanish(true);
	}

	// Token: 0x060034C4 RID: 13508 RVA: 0x00137088 File Offset: 0x00135488
	public void ClearListCollider()
	{
		this.listCollider.Clear();
	}

	// Token: 0x060034C5 RID: 13509 RVA: 0x00137098 File Offset: 0x00135498
	protected void OnTriggerEnter(Collider other)
	{
		Collider x2 = this.listCollider.Find((Collider x) => other.name == x.name);
		if (x2 == null)
		{
			this.listCollider.Add(other);
		}
	}

	// Token: 0x060034C6 RID: 13510 RVA: 0x001370E8 File Offset: 0x001354E8
	protected void OnTriggerStay(Collider other)
	{
		Collider x2 = this.listCollider.Find((Collider x) => other.name == x.name);
		if (x2 == null)
		{
			this.listCollider.Add(other);
		}
	}

	// Token: 0x060034C7 RID: 13511 RVA: 0x00137137 File Offset: 0x00135537
	protected void OnTriggerExit(Collider other)
	{
		this.listCollider.Clear();
	}

	// Token: 0x060034C8 RID: 13512 RVA: 0x00137144 File Offset: 0x00135544
	public void autoCamera(float _fSpeed)
	{
		this.CamDat.Rot.y = (this.CamDat.Rot.y + _fSpeed * Time.deltaTime) % 360f;
	}

	// Token: 0x060034C9 RID: 13513 RVA: 0x00137174 File Offset: 0x00135574
	public void CameraDataSave(string _strCreateAssetPath, string _strFile)
	{
		FileData fileData = new FileData(string.Empty);
		string path = fileData.Create(_strCreateAssetPath) + _strFile + ".txt";
		using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.GetEncoding("UTF-8")))
		{
			streamWriter.Write(this.CamDat.Pos.x);
			streamWriter.Write('\n');
			streamWriter.Write(this.CamDat.Pos.y);
			streamWriter.Write('\n');
			streamWriter.Write(this.CamDat.Pos.z);
			streamWriter.Write('\n');
			streamWriter.Write(this.CamDat.Dir.x);
			streamWriter.Write('\n');
			streamWriter.Write(this.CamDat.Dir.y);
			streamWriter.Write('\n');
			streamWriter.Write(this.CamDat.Dir.z);
			streamWriter.Write('\n');
			streamWriter.Write(this.CamDat.Rot.x);
			streamWriter.Write('\n');
			streamWriter.Write(this.CamDat.Rot.y);
			streamWriter.Write('\n');
			streamWriter.Write(this.CamDat.Rot.z);
			streamWriter.Write('\n');
			streamWriter.Write(this.CamDat.Fov);
			streamWriter.Write('\n');
		}
	}

	// Token: 0x060034CA RID: 13514 RVA: 0x00137308 File Offset: 0x00135708
	public void CameraDataSaveBinary(BinaryWriter bw)
	{
		bw.Write(this.CamDat.Pos.x);
		bw.Write(this.CamDat.Pos.y);
		bw.Write(this.CamDat.Pos.z);
		bw.Write(this.CamDat.Dir.x);
		bw.Write(this.CamDat.Dir.y);
		bw.Write(this.CamDat.Dir.z);
		bw.Write(this.CamDat.Rot.x);
		bw.Write(this.CamDat.Rot.y);
		bw.Write(this.CamDat.Rot.z);
		bw.Write(this.CamDat.Fov);
	}

	// Token: 0x060034CB RID: 13515 RVA: 0x001373EC File Offset: 0x001357EC
	public bool CameraDataLoadBinary(BinaryReader br, bool isUpdate)
	{
		BaseCameraControl_Ver2.CameraData cameraData = default(BaseCameraControl_Ver2.CameraData);
		cameraData.Pos.x = br.ReadSingle();
		cameraData.Pos.y = br.ReadSingle();
		cameraData.Pos.z = br.ReadSingle();
		cameraData.Dir.x = br.ReadSingle();
		cameraData.Dir.y = br.ReadSingle();
		cameraData.Dir.z = br.ReadSingle();
		cameraData.Rot.x = br.ReadSingle();
		cameraData.Rot.y = br.ReadSingle();
		cameraData.Rot.z = br.ReadSingle();
		cameraData.Fov = br.ReadSingle();
		this.CamReset.Copy(cameraData, Quaternion.identity);
		if (isUpdate)
		{
			this.CamDat.Copy(cameraData);
			if (base.thisCamera != null)
			{
				base.thisCamera.fieldOfView = cameraData.Fov;
			}
			base.CameraUpdate();
			if (!this.isInit)
			{
				this.isInit = true;
			}
		}
		return true;
	}

	// Token: 0x060034CC RID: 13516 RVA: 0x00137510 File Offset: 0x00135910
	public void visibleFroceVanish(bool _visible)
	{
		foreach (CameraControl_Ver2.VisibleObject visibleObject in this.lstMapVanish)
		{
			foreach (GameObject gameObject in visibleObject.listObj)
			{
				if (gameObject)
				{
					gameObject.SetActive(_visible);
				}
			}
			visibleObject.isVisible = _visible;
			visibleObject.delay = ((!_visible) ? 0f : 0.3f);
		}
	}

	// Token: 0x060034CD RID: 13517 RVA: 0x001375E0 File Offset: 0x001359E0
	private void visibleFroceVanish(CameraControl_Ver2.VisibleObject _obj, bool _visible)
	{
		if (_obj == null)
		{
			return;
		}
		if (_obj.listObj == null)
		{
			return;
		}
		for (int i = 0; i < _obj.listObj.Count; i++)
		{
			_obj.listObj[i].SetActive(_visible);
		}
		_obj.delay = ((!_visible) ? 0f : 0.3f);
		_obj.isVisible = _visible;
	}

	// Token: 0x060034CE RID: 13518 RVA: 0x00137650 File Offset: 0x00135A50
	private bool VanishProc()
	{
		if (!this.isConfigVanish)
		{
			return false;
		}
		int i;
		for (i = 0; i < this.lstMapVanish.Count; i++)
		{
			Collider x2 = this.listCollider.Find((Collider x) => this.lstMapVanish[i].nameCollider == x.name);
			if (x2 == null)
			{
				this.VanishDelayVisible(this.lstMapVanish[i]);
			}
			else if (this.lstMapVanish[i].isVisible)
			{
				this.visibleFroceVanish(this.lstMapVanish[i], false);
			}
		}
		return true;
	}

	// Token: 0x060034CF RID: 13519 RVA: 0x00137721 File Offset: 0x00135B21
	private bool VanishDelayVisible(CameraControl_Ver2.VisibleObject _visible)
	{
		if (_visible.isVisible)
		{
			return false;
		}
		_visible.delay += Time.deltaTime;
		if (_visible.delay >= 0.3f)
		{
			this.visibleFroceVanish(_visible, true);
		}
		return true;
	}

	// Token: 0x04003563 RID: 13667
	private bool isConfigVanish = true;

	// Token: 0x04003565 RID: 13669
	private Renderer targetRender;

	// Token: 0x04003566 RID: 13670
	private List<CameraControl_Ver2.VisibleObject> lstMapVanish = new List<CameraControl_Ver2.VisibleObject>();

	// Token: 0x04003567 RID: 13671
	private List<Collider> listCollider = new List<Collider>();

	// Token: 0x02000819 RID: 2073
	public class VisibleObject
	{
		// Token: 0x04003569 RID: 13673
		public string nameCollider;

		// Token: 0x0400356A RID: 13674
		public float delay;

		// Token: 0x0400356B RID: 13675
		public bool isVisible = true;

		// Token: 0x0400356C RID: 13676
		public List<GameObject> listObj = new List<GameObject>();
	}
}
