using System;
using System.Collections.Generic;
using UnityEngine;

namespace CTS
{
	// Token: 0x02000690 RID: 1680
	public class CTSLook : MonoBehaviour
	{
		// Token: 0x06002798 RID: 10136 RVA: 0x000E9A90 File Offset: 0x000E7E90
		private void Update()
		{
			this.MouseLookAveraged();
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000E9A98 File Offset: 0x000E7E98
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

		// Token: 0x040027D7 RID: 10199
		[Header("Info")]
		private List<float> _rotArrayX = new List<float>();

		// Token: 0x040027D8 RID: 10200
		private List<float> _rotArrayY = new List<float>();

		// Token: 0x040027D9 RID: 10201
		private float rotAverageX;

		// Token: 0x040027DA RID: 10202
		private float rotAverageY;

		// Token: 0x040027DB RID: 10203
		private float mouseDeltaX;

		// Token: 0x040027DC RID: 10204
		private float mouseDeltaY;

		// Token: 0x040027DD RID: 10205
		[Header("Settings")]
		public bool _isLocked;

		// Token: 0x040027DE RID: 10206
		public float _sensitivityX = 1.5f;

		// Token: 0x040027DF RID: 10207
		public float _sensitivityY = 1.5f;

		// Token: 0x040027E0 RID: 10208
		[Tooltip("The more steps, the smoother it will be.")]
		public int _averageFromThisManySteps = 3;

		// Token: 0x040027E1 RID: 10209
		[Header("References")]
		[Tooltip("Object to be rotated when mouse moves left/right.")]
		public Transform _playerRootT;

		// Token: 0x040027E2 RID: 10210
		[Tooltip("Object to be rotated when mouse moves up/down.")]
		public Transform _cameraT;
	}
}
