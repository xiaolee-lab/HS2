using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F8E RID: 3982
	public class TrialCommonDefine : SerializedScriptableObject
	{
		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x060084FA RID: 34042 RVA: 0x00373F19 File Offset: 0x00372319
		public TrialCommonDefine.FileNameGroup FileNames
		{
			[CompilerGenerated]
			get
			{
				return this._fileNames;
			}
		}

		// Token: 0x04006B95 RID: 27541
		[SerializeField]
		private TrialCommonDefine.FileNameGroup _fileNames = new TrialCommonDefine.FileNameGroup();

		// Token: 0x02000F8F RID: 3983
		[Serializable]
		public class FileNameGroup
		{
			// Token: 0x17001D19 RID: 7449
			// (get) Token: 0x060084FC RID: 34044 RVA: 0x00373F34 File Offset: 0x00372334
			public string MainCameraName
			{
				[CompilerGenerated]
				get
				{
					return this._mainCameraName;
				}
			}

			// Token: 0x04006B96 RID: 27542
			[SerializeField]
			private string _mainCameraName = string.Empty;
		}
	}
}
