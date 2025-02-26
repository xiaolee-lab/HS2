using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace ADV.Backup
{
	// Token: 0x020006B5 RID: 1717
	internal class CameraData
	{
		// Token: 0x060028A1 RID: 10401 RVA: 0x000F091C File Offset: 0x000EED1C
		public CameraData(Camera cam)
		{
			this.blurBK = null;
			this.dofBK = null;
			if (cam == null)
			{
				return;
			}
			this.rect = cam.rect;
			this.parent = cam.transform.parent;
			this.fov = cam.fieldOfView;
			this.far = cam.farClipPlane;
			Blur component = cam.GetComponent<Blur>();
			if (component != null)
			{
				this.blurBK = new CameraData.BlurBK(component);
			}
			DepthOfField component2 = cam.GetComponent<DepthOfField>();
			if (component2 != null)
			{
				this.dofBK = new CameraData.DOFBK(component2);
			}
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000F09C0 File Offset: 0x000EEDC0
		public void Load(Camera cam)
		{
			if (cam == null)
			{
				return;
			}
			cam.rect = this.rect;
			cam.transform.parent = this.parent;
			cam.fieldOfView = this.fov;
			cam.farClipPlane = this.far;
			this.blurBK.SafeProc(delegate(CameraData.BlurBK p)
			{
				p.Set(cam.GetComponent<Blur>());
			});
			this.dofBK.SafeProc(delegate(CameraData.DOFBK p)
			{
				p.Set(cam.GetComponent<DepthOfField>());
			});
		}

		// Token: 0x04002A2A RID: 10794
		private Rect rect;

		// Token: 0x04002A2B RID: 10795
		private Transform parent;

		// Token: 0x04002A2C RID: 10796
		private float fov;

		// Token: 0x04002A2D RID: 10797
		private float far;

		// Token: 0x04002A2E RID: 10798
		private CameraData.BlurBK blurBK;

		// Token: 0x04002A2F RID: 10799
		private CameraData.DOFBK dofBK;

		// Token: 0x020006B6 RID: 1718
		private class BlurBK
		{
			// Token: 0x060028A3 RID: 10403 RVA: 0x000F0A65 File Offset: 0x000EEE65
			public BlurBK(Blur blur)
			{
				if (blur == null)
				{
					return;
				}
				this.enabled = blur.enabled;
				this.iterations = blur.iterations;
				this.blurSpread = blur.blurSpread;
			}

			// Token: 0x060028A4 RID: 10404 RVA: 0x000F0A9E File Offset: 0x000EEE9E
			public void Set(Blur blur)
			{
				if (blur == null)
				{
					return;
				}
				blur.enabled = this.enabled;
				blur.iterations = this.iterations;
				blur.blurSpread = this.blurSpread;
			}

			// Token: 0x04002A30 RID: 10800
			private bool enabled;

			// Token: 0x04002A31 RID: 10801
			private int iterations;

			// Token: 0x04002A32 RID: 10802
			private float blurSpread;
		}

		// Token: 0x020006B7 RID: 1719
		private class DOFBK
		{
			// Token: 0x060028A5 RID: 10405 RVA: 0x000F0AD4 File Offset: 0x000EEED4
			public DOFBK(DepthOfField dof)
			{
				if (dof == null)
				{
					return;
				}
				this.enabled = dof.enabled;
				this.focalTransform = dof.focalTransform;
				this.focalLength = dof.focalLength;
				this.focalSize = dof.focalSize;
				this.aperture = dof.aperture;
			}

			// Token: 0x060028A6 RID: 10406 RVA: 0x000F0B30 File Offset: 0x000EEF30
			public void Set(DepthOfField dof)
			{
				if (dof == null)
				{
					return;
				}
				dof.enabled = this.enabled;
				dof.focalTransform = this.focalTransform;
				dof.focalLength = this.focalLength;
				dof.focalSize = this.focalSize;
				dof.aperture = this.aperture;
			}

			// Token: 0x04002A33 RID: 10803
			private bool enabled;

			// Token: 0x04002A34 RID: 10804
			private Transform focalTransform;

			// Token: 0x04002A35 RID: 10805
			private float focalLength;

			// Token: 0x04002A36 RID: 10806
			private float focalSize;

			// Token: 0x04002A37 RID: 10807
			private float aperture;
		}
	}
}
