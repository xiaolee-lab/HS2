using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C74 RID: 3188
	public class StateEventList : MonoBehaviour
	{
		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x0600688E RID: 26766 RVA: 0x002C9216 File Offset: 0x002C7616
		// (set) Token: 0x0600688F RID: 26767 RVA: 0x002C921E File Offset: 0x002C761E
		public bool DebugMode
		{
			get
			{
				return this._debugMode;
			}
			set
			{
				this._debugMode = value;
			}
		}

		// Token: 0x04005948 RID: 22856
		[SerializeField]
		private bool _debugMode;

		// Token: 0x02000C75 RID: 3189
		[Serializable]
		public class StateEvent
		{
			// Token: 0x04005949 RID: 22857
			public string stateName;

			// Token: 0x0400594A RID: 22858
			public float time;

			// Token: 0x0400594B RID: 22859
			public StateEventList.StateEventType type;

			// Token: 0x0400594C RID: 22860
			public StateEventList.JointType jointTarget;

			// Token: 0x0400594D RID: 22861
			public int id;
		}

		// Token: 0x02000C76 RID: 3190
		public enum StateEventType
		{
			// Token: 0x0400594F RID: 22863
			SE,
			// Token: 0x04005950 RID: 22864
			FootStep,
			// Token: 0x04005951 RID: 22865
			ActivateItem,
			// Token: 0x04005952 RID: 22866
			DeactivateItem
		}

		// Token: 0x02000C77 RID: 3191
		public enum JointType
		{
			// Token: 0x04005954 RID: 22868
			LeftHand,
			// Token: 0x04005955 RID: 22869
			RightHand,
			// Token: 0x04005956 RID: 22870
			LeftFoot,
			// Token: 0x04005957 RID: 22871
			RightFoot
		}
	}
}
