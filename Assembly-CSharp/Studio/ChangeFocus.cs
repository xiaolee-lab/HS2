using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012CF RID: 4815
	public class ChangeFocus : MonoBehaviour
	{
		// Token: 0x170021EB RID: 8683
		// (get) Token: 0x0600A095 RID: 41109 RVA: 0x0041FD12 File Offset: 0x0041E112
		// (set) Token: 0x0600A096 RID: 41110 RVA: 0x0041FD1A File Offset: 0x0041E11A
		public int select
		{
			get
			{
				return this.m_Select;
			}
			set
			{
				this.m_Select = value;
				base.enabled = (this.m_Select != -1);
			}
		}

		// Token: 0x0600A097 RID: 41111 RVA: 0x0041FD38 File Offset: 0x0041E138
		private void ChangeTarget()
		{
			if (Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift))
			{
				this.m_Select--;
				if (this.m_Select < 0)
				{
					this.m_Select = this.selectable.Length - 1;
				}
			}
			else
			{
				this.m_Select = (this.m_Select + 1) % this.selectable.Length;
			}
			if (this.selectable[this.m_Select])
			{
				this.selectable[this.m_Select].Select();
			}
		}

		// Token: 0x0600A098 RID: 41112 RVA: 0x0041FDD0 File Offset: 0x0041E1D0
		private void Start()
		{
			this.select = -1;
			(from _ in this.UpdateAsObservable()
			where base.enabled
			where this.@select != -1
			where Input.GetKeyDown(KeyCode.Tab)
			select _).Subscribe(delegate(Unit _)
			{
				this.ChangeTarget();
			});
		}

		// Token: 0x04007EDD RID: 32477
		[SerializeField]
		private Selectable[] selectable;

		// Token: 0x04007EDE RID: 32478
		private int m_Select = -1;
	}
}
