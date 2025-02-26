using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C0B RID: 3083
	public class ForcedHideObject : MonoBehaviour
	{
		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06005F25 RID: 24357 RVA: 0x0028404D File Offset: 0x0028244D
		// (set) Token: 0x06005F26 RID: 24358 RVA: 0x00284055 File Offset: 0x00282455
		private bool ForcedHideFlag
		{
			get
			{
				return this._forcedHideFlag;
			}
			set
			{
				this._forcedHideFlag = value;
			}
		}

		// Token: 0x06005F27 RID: 24359 RVA: 0x0028405E File Offset: 0x0028245E
		public void Init()
		{
			if (!this._firstInit)
			{
				return;
			}
			this._firstInit = false;
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x00284073 File Offset: 0x00282473
		private void OnEnable()
		{
			if (this._forcedHideFlag)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x0028408C File Offset: 0x0028248C
		private void LateUpdate()
		{
			if (this._firstInit)
			{
				return;
			}
			if (this._forcedHideFlag)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x002840B1 File Offset: 0x002824B1
		public void SetActive(bool active)
		{
			if (active)
			{
				this.Show();
			}
			else
			{
				this.Hide();
			}
		}

		// Token: 0x06005F2B RID: 24363 RVA: 0x002840CA File Offset: 0x002824CA
		public void Show()
		{
			this._forcedHideFlag = false;
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
		}

		// Token: 0x06005F2C RID: 24364 RVA: 0x002840EF File Offset: 0x002824EF
		public void Hide()
		{
			this._forcedHideFlag = true;
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06005F2D RID: 24365 RVA: 0x00284114 File Offset: 0x00282514
		public bool Active
		{
			[CompilerGenerated]
			get
			{
				return base.gameObject != null && base.gameObject.activeSelf;
			}
		}

		// Token: 0x0400549A RID: 21658
		private bool _forcedHideFlag;

		// Token: 0x0400549B RID: 21659
		private bool _firstInit = true;
	}
}
