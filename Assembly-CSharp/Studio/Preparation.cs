using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011E2 RID: 4578
	public class Preparation : MonoBehaviour
	{
		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x06009675 RID: 38517 RVA: 0x003E1A47 File Offset: 0x003DFE47
		public Transform LookAtTarget
		{
			[CompilerGenerated]
			get
			{
				return this._lookAtTarget;
			}
		}

		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x06009676 RID: 38518 RVA: 0x003E1A4F File Offset: 0x003DFE4F
		public CharAnimeCtrl CharAnimeCtrl
		{
			[CompilerGenerated]
			get
			{
				return this._charAnimeCtrl;
			}
		}

		// Token: 0x17001FE5 RID: 8165
		// (get) Token: 0x06009677 RID: 38519 RVA: 0x003E1A57 File Offset: 0x003DFE57
		public IKCtrl IKCtrl
		{
			[CompilerGenerated]
			get
			{
				return this._IKCtrl;
			}
		}

		// Token: 0x17001FE6 RID: 8166
		// (get) Token: 0x06009678 RID: 38520 RVA: 0x003E1A5F File Offset: 0x003DFE5F
		public PVCopy PvCopy
		{
			[CompilerGenerated]
			get
			{
				return this._pvCopy;
			}
		}

		// Token: 0x17001FE7 RID: 8167
		// (get) Token: 0x06009679 RID: 38521 RVA: 0x003E1A67 File Offset: 0x003DFE67
		public YureCtrl YureCtrl
		{
			[CompilerGenerated]
			get
			{
				return this._yureCtrl;
			}
		}

		// Token: 0x040078F3 RID: 30963
		[SerializeField]
		private Transform _lookAtTarget;

		// Token: 0x040078F4 RID: 30964
		[SerializeField]
		private CharAnimeCtrl _charAnimeCtrl;

		// Token: 0x040078F5 RID: 30965
		[SerializeField]
		private IKCtrl _IKCtrl;

		// Token: 0x040078F6 RID: 30966
		[SerializeField]
		private PVCopy _pvCopy;

		// Token: 0x040078F7 RID: 30967
		[SerializeField]
		private YureCtrl _yureCtrl;
	}
}
