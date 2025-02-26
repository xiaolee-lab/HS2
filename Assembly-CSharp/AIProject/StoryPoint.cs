using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C2D RID: 3117
	public class StoryPoint : Point
	{
		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06006071 RID: 24689 RVA: 0x00289276 File Offset: 0x00287676
		public int PointID
		{
			[CompilerGenerated]
			get
			{
				return this._pointID;
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06006072 RID: 24690 RVA: 0x0028927E File Offset: 0x0028767E
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06006073 RID: 24691 RVA: 0x0028928B File Offset: 0x0028768B
		public Vector3 EulerAngles
		{
			[CompilerGenerated]
			get
			{
				return base.transform.eulerAngles;
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06006074 RID: 24692 RVA: 0x00289298 File Offset: 0x00287698
		public Quaternion Rotation
		{
			[CompilerGenerated]
			get
			{
				return base.transform.rotation;
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06006075 RID: 24693 RVA: 0x002892A5 File Offset: 0x002876A5
		public Vector3 LocalPosition
		{
			[CompilerGenerated]
			get
			{
				return base.transform.localPosition;
			}
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06006076 RID: 24694 RVA: 0x002892B2 File Offset: 0x002876B2
		public Vector3 LocalEulerAngles
		{
			[CompilerGenerated]
			get
			{
				return base.transform.localEulerAngles;
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06006077 RID: 24695 RVA: 0x002892BF File Offset: 0x002876BF
		public Quaternion LocalRotation
		{
			[CompilerGenerated]
			get
			{
				return base.transform.localRotation;
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06006078 RID: 24696 RVA: 0x002892CC File Offset: 0x002876CC
		public Vector3 Forward
		{
			[CompilerGenerated]
			get
			{
				return base.transform.forward;
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06006079 RID: 24697 RVA: 0x002892D9 File Offset: 0x002876D9
		public Vector3 Right
		{
			[CompilerGenerated]
			get
			{
				return base.transform.right;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x0600607A RID: 24698 RVA: 0x002892E6 File Offset: 0x002876E6
		public Vector3 Back
		{
			[CompilerGenerated]
			get
			{
				return base.transform.forward * -1f;
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x0600607B RID: 24699 RVA: 0x002892FD File Offset: 0x002876FD
		public Vector3 Left
		{
			[CompilerGenerated]
			get
			{
				return base.transform.right * -1f;
			}
		}

		// Token: 0x040055A5 RID: 21925
		[SerializeField]
		private int _pointID;
	}
}
