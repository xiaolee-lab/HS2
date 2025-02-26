using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BD4 RID: 3028
	public class ActionPointGroup : MonoBehaviour
	{
		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x0027493A File Offset: 0x00272D3A
		public ActionPoint[] JoinPoints
		{
			[CompilerGenerated]
			get
			{
				return this._joinPoints;
			}
		}

		// Token: 0x06005CC9 RID: 23753 RVA: 0x00274944 File Offset: 0x00272D44
		private void Start()
		{
			foreach (ActionPoint actionPoint in this._joinPoints)
			{
				if (!(actionPoint == null))
				{
					foreach (ActionPoint actionPoint2 in this._joinPoints)
					{
						if (!(actionPoint2 == null))
						{
							if (!(actionPoint == actionPoint2))
							{
								actionPoint.GroupActionPoints.Add(actionPoint2);
							}
						}
					}
				}
			}
		}

		// Token: 0x0400535B RID: 21339
		[SerializeField]
		private ActionPoint[] _joinPoints;
	}
}
