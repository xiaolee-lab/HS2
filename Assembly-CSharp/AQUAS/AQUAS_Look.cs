using System;
using System.Collections.Generic;
using UnityEngine;

namespace AQUAS
{
	// Token: 0x02000065 RID: 101
	public class AQUAS_Look : MonoBehaviour
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00008672 File Offset: 0x00006A72
		private void Update()
		{
			this.MouseLookAveraged();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000867C File Offset: 0x00006A7C
		private void MouseLookAveraged()
		{
			this.rotAverageX = 0f;
			this.rotAverageY = 0f;
			this.mouseDeltaX = 0f;
			this.mouseDeltaY = 0f;
			this.mouseDeltaX += Input.GetAxis("Mouse X") * this._sensitivityX;
			this.mouseDeltaY += Input.GetAxis("Mouse Y") * this._sensitivityY;
			this._rotArrayX.Add(this.mouseDeltaX);
			this._rotArrayY.Add(this.mouseDeltaY);
			if (this._rotArrayX.Count >= this._averageFromThisManySteps)
			{
				this._rotArrayX.RemoveAt(0);
			}
			if (this._rotArrayY.Count >= this._averageFromThisManySteps)
			{
				this._rotArrayY.RemoveAt(0);
			}
			for (int i = 0; i < this._rotArrayX.Count; i++)
			{
				this.rotAverageX += this._rotArrayX[i];
			}
			for (int j = 0; j < this._rotArrayY.Count; j++)
			{
				this.rotAverageY += this._rotArrayY[j];
			}
			this.rotAverageX /= (float)this._rotArrayX.Count;
			this.rotAverageY /= (float)this._rotArrayY.Count;
			this._playerRootT.Rotate(0f, this.rotAverageX, 0f, Space.World);
			this._cameraT.Rotate(-this.rotAverageY, 0f, 0f, Space.Self);
		}

		// Token: 0x040001AF RID: 431
		[Header("Info")]
		private List<float> _rotArrayX = new List<float>();

		// Token: 0x040001B0 RID: 432
		private List<float> _rotArrayY = new List<float>();

		// Token: 0x040001B1 RID: 433
		private float rotAverageX;

		// Token: 0x040001B2 RID: 434
		private float rotAverageY;

		// Token: 0x040001B3 RID: 435
		private float mouseDeltaX;

		// Token: 0x040001B4 RID: 436
		private float mouseDeltaY;

		// Token: 0x040001B5 RID: 437
		[Header("Settings")]
		public bool _isLocked;

		// Token: 0x040001B6 RID: 438
		public float _sensitivityX = 1.5f;

		// Token: 0x040001B7 RID: 439
		public float _sensitivityY = 1.5f;

		// Token: 0x040001B8 RID: 440
		[Tooltip("The more steps, the smoother it will be.")]
		public int _averageFromThisManySteps = 3;

		// Token: 0x040001B9 RID: 441
		[Header("References")]
		[Tooltip("Object to be rotated when mouse moves left/right.")]
		public Transform _playerRootT;

		// Token: 0x040001BA RID: 442
		[Tooltip("Object to be rotated when mouse moves up/down.")]
		public Transform _cameraT;
	}
}
