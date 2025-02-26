using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C0C RID: 3084
	public class HarborDoorAnimation : MonoBehaviour
	{
		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06005F2F RID: 24367 RVA: 0x0028415A File Offset: 0x0028255A
		public int LinkID
		{
			[CompilerGenerated]
			get
			{
				return this._linkID;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06005F30 RID: 24368 RVA: 0x00284162 File Offset: 0x00282562
		public PoseKeyPair PoseInfo
		{
			[CompilerGenerated]
			get
			{
				return this._poseInfo;
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06005F31 RID: 24369 RVA: 0x0028416A File Offset: 0x0028256A
		public Transform BasePoint
		{
			[CompilerGenerated]
			get
			{
				return this._basePoint;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06005F32 RID: 24370 RVA: 0x00284172 File Offset: 0x00282572
		public Transform RecoveryPoint
		{
			[CompilerGenerated]
			get
			{
				return this._recoveryPoint;
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06005F33 RID: 24371 RVA: 0x0028417A File Offset: 0x0028257A
		// (set) Token: 0x06005F34 RID: 24372 RVA: 0x00284182 File Offset: 0x00282582
		public HarborDoorAnimData AnimData { get; protected set; }

		// Token: 0x06005F35 RID: 24373 RVA: 0x0028418C File Offset: 0x0028258C
		private void Start()
		{
			HarborDoorAnimData harborDoorAnimData = null;
			bool? flag = (HarborDoorAnimData.Table != null) ? new bool?(HarborDoorAnimData.Table.TryGetValue(this._linkID, out harborDoorAnimData)) : null;
			if (flag == null || !flag.Value || harborDoorAnimData == null)
			{
				return;
			}
			this.AnimData = harborDoorAnimData;
			this._doorAnimator = harborDoorAnimData.DoorAnimator;
		}

		// Token: 0x0400549C RID: 21660
		[SerializeField]
		private int _linkID;

		// Token: 0x0400549D RID: 21661
		[SerializeField]
		private PoseKeyPair _poseInfo = default(PoseKeyPair);

		// Token: 0x0400549E RID: 21662
		[SerializeField]
		private Transform _basePoint;

		// Token: 0x0400549F RID: 21663
		[SerializeField]
		private Transform _recoveryPoint;

		// Token: 0x040054A0 RID: 21664
		private Animator _doorAnimator;
	}
}
