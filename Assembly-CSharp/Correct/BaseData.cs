using System;
using UnityEngine;

namespace Correct
{
	// Token: 0x02000B38 RID: 2872
	public class BaseData : MonoBehaviour
	{
		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x0600542F RID: 21551 RVA: 0x0025211F File Offset: 0x0025051F
		// (set) Token: 0x06005430 RID: 21552 RVA: 0x00252127 File Offset: 0x00250527
		public Vector3 pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06005431 RID: 21553 RVA: 0x00252130 File Offset: 0x00250530
		// (set) Token: 0x06005432 RID: 21554 RVA: 0x00252138 File Offset: 0x00250538
		public Vector3 ang
		{
			get
			{
				return this._ang;
			}
			set
			{
				this._rot = Quaternion.Euler(value);
				this._ang = value;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06005433 RID: 21555 RVA: 0x0025214D File Offset: 0x0025054D
		// (set) Token: 0x06005434 RID: 21556 RVA: 0x00252155 File Offset: 0x00250555
		public Quaternion rot
		{
			get
			{
				return this._rot;
			}
			set
			{
				this._rot = value;
				this._ang = this._rot.eulerAngles;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06005435 RID: 21557 RVA: 0x0025216F File Offset: 0x0025056F
		public static GameObject handle
		{
			get
			{
				return BaseData._handle;
			}
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x00252176 File Offset: 0x00250576
		private void Reset()
		{
			this.bone = null;
			this._pos = Vector3.zero;
			this._rot = Quaternion.identity;
			this._ang = Vector3.zero;
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x002521A0 File Offset: 0x002505A0
		private void Awake()
		{
			if (BaseData._handle == null)
			{
				BaseData._handle = GameObject.Find("IKhandle");
			}
		}

		// Token: 0x04004F0A RID: 20234
		public Transform bone;

		// Token: 0x04004F0B RID: 20235
		[SerializeField]
		private Vector3 _pos;

		// Token: 0x04004F0C RID: 20236
		[SerializeField]
		private Vector3 _ang;

		// Token: 0x04004F0D RID: 20237
		[SerializeField]
		private Quaternion _rot;

		// Token: 0x04004F0E RID: 20238
		[SerializeField]
		private static GameObject _handle;
	}
}
