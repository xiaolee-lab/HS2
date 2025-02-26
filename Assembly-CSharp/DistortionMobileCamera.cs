using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000454 RID: 1108
public class DistortionMobileCamera : MonoBehaviour
{
	// Token: 0x06001447 RID: 5191 RVA: 0x0007EFB8 File Offset: 0x0007D3B8
	private void Start()
	{
		if (this.UseRealTime)
		{
			this.Initialize();
			return;
		}
		this.fpsMove = new WaitForSeconds(1f / (float)this.FPSWhenMoveCamera);
		this.fpsStatic = new WaitForSeconds(1f / (float)this.FPSWhenStaticCamera);
		this.canUpdateCamera = true;
		if (this.FPSWhenMoveCamera > 0)
		{
			base.StartCoroutine(this.RepeatCameraMove());
		}
		if (this.FPSWhenStaticCamera > 0)
		{
			base.StartCoroutine(this.RepeatCameraStatic());
		}
		this.Initialize();
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x0007F048 File Offset: 0x0007D448
	private void Update()
	{
		if (this.UseRealTime)
		{
			return;
		}
		if (this.cameraInstance == null)
		{
			return;
		}
		if (Vector3.SqrMagnitude(this.instanceCameraTransform.position - this.oldPosition) <= 1E-05f && this.instanceCameraTransform.rotation == this.oldRotation)
		{
			this.frameCountWhenCameraIsStatic++;
			if (this.frameCountWhenCameraIsStatic >= 50)
			{
				this.isStaticUpdate = true;
			}
		}
		else
		{
			this.frameCountWhenCameraIsStatic = 0;
			this.isStaticUpdate = false;
		}
		this.oldPosition = this.instanceCameraTransform.position;
		this.oldRotation = this.instanceCameraTransform.rotation;
		if (this.canUpdateCamera)
		{
			if (!this.cameraInstance.enabled)
			{
				this.cameraInstance.enabled = true;
			}
			if (this.FPSWhenMoveCamera > 0)
			{
				this.canUpdateCamera = false;
			}
		}
		else if (this.cameraInstance.enabled)
		{
			this.cameraInstance.enabled = false;
		}
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x0007F168 File Offset: 0x0007D568
	private IEnumerator RepeatCameraMove()
	{
		for (;;)
		{
			if (!this.isStaticUpdate)
			{
				this.canUpdateCamera = true;
			}
			yield return this.fpsMove;
		}
		yield break;
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x0007F184 File Offset: 0x0007D584
	private IEnumerator RepeatCameraStatic()
	{
		for (;;)
		{
			if (this.isStaticUpdate)
			{
				this.canUpdateCamera = true;
			}
			yield return this.fpsStatic;
		}
		yield break;
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x0007F19F File Offset: 0x0007D59F
	private void OnBecameVisible()
	{
		if (this.goCamera != null)
		{
			this.goCamera.SetActive(true);
		}
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x0007F1BE File Offset: 0x0007D5BE
	private void OnBecameInvisible()
	{
		if (this.goCamera != null)
		{
			this.goCamera.SetActive(false);
		}
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x0007F1E0 File Offset: 0x0007D5E0
	private void Initialize()
	{
		this.goCamera = new GameObject("RenderTextureCamera");
		this.cameraInstance = this.goCamera.AddComponent<Camera>();
		Camera main = Camera.main;
		this.cameraInstance.CopyFrom(main);
		this.cameraInstance.depth += 1f;
		this.cameraInstance.cullingMask = this.CullingMask;
		this.cameraInstance.renderingPath = this.RenderingPath;
		this.goCamera.transform.parent = main.transform;
		this.renderTexture = new RenderTexture(Mathf.RoundToInt((float)Screen.width * this.TextureScale), Mathf.RoundToInt((float)Screen.height * this.TextureScale), 16, this.RenderTextureFormat);
		this.renderTexture.DiscardContents();
		this.renderTexture.filterMode = this.FilterMode;
		this.cameraInstance.targetTexture = this.renderTexture;
		this.instanceCameraTransform = this.cameraInstance.transform;
		this.oldPosition = this.instanceCameraTransform.position;
		Shader.SetGlobalTexture("_GrabTextureMobile", this.renderTexture);
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x0007F30C File Offset: 0x0007D70C
	private void OnDisable()
	{
		if (this.goCamera)
		{
			UnityEngine.Object.DestroyImmediate(this.goCamera);
			this.goCamera = null;
		}
		if (this.renderTexture)
		{
			UnityEngine.Object.DestroyImmediate(this.renderTexture);
			this.renderTexture = null;
		}
	}

	// Token: 0x04001718 RID: 5912
	public float TextureScale = 1f;

	// Token: 0x04001719 RID: 5913
	public RenderTextureFormat RenderTextureFormat;

	// Token: 0x0400171A RID: 5914
	public FilterMode FilterMode;

	// Token: 0x0400171B RID: 5915
	public LayerMask CullingMask = -17;

	// Token: 0x0400171C RID: 5916
	public RenderingPath RenderingPath;

	// Token: 0x0400171D RID: 5917
	public int FPSWhenMoveCamera = 40;

	// Token: 0x0400171E RID: 5918
	public int FPSWhenStaticCamera = 20;

	// Token: 0x0400171F RID: 5919
	public bool UseRealTime;

	// Token: 0x04001720 RID: 5920
	private RenderTexture renderTexture;

	// Token: 0x04001721 RID: 5921
	private Camera cameraInstance;

	// Token: 0x04001722 RID: 5922
	private GameObject goCamera;

	// Token: 0x04001723 RID: 5923
	private Vector3 oldPosition;

	// Token: 0x04001724 RID: 5924
	private Quaternion oldRotation;

	// Token: 0x04001725 RID: 5925
	private Transform instanceCameraTransform;

	// Token: 0x04001726 RID: 5926
	private bool canUpdateCamera;

	// Token: 0x04001727 RID: 5927
	private bool isStaticUpdate;

	// Token: 0x04001728 RID: 5928
	private WaitForSeconds fpsMove;

	// Token: 0x04001729 RID: 5929
	private WaitForSeconds fpsStatic;

	// Token: 0x0400172A RID: 5930
	private const int DropedFrames = 50;

	// Token: 0x0400172B RID: 5931
	private int frameCountWhenCameraIsStatic;
}
