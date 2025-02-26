using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x02000093 RID: 147
	[RequireComponent(typeof(Toggle))]
	public class TreeViewExpander : MonoBehaviour
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000F148 File Offset: 0x0000D548
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000F150 File Offset: 0x0000D550
		public bool CanExpand
		{
			get
			{
				return this.m_canExpand;
			}
			set
			{
				this.m_canExpand = value;
				this.UpdateState();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000F15F File Offset: 0x0000D55F
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000F16C File Offset: 0x0000D56C
		public bool IsOn
		{
			get
			{
				return this.m_toggle.isOn;
			}
			set
			{
				this.m_toggle.isOn = value;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000F17A File Offset: 0x0000D57A
		private void UpdateState()
		{
			if (this.m_started)
			{
				this.DoUpdateState();
			}
			else
			{
				base.StartCoroutine(this.CoUpdateState());
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000F1A0 File Offset: 0x0000D5A0
		private IEnumerator CoUpdateState()
		{
			yield return new WaitForEndOfFrame();
			this.DoUpdateState();
			yield break;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000F1BC File Offset: 0x0000D5BC
		private void DoUpdateState()
		{
			if (this.CanExpand)
			{
				this.m_toggle.interactable = true;
				if (this.IsOn)
				{
					if (this.OffGraphic != null)
					{
						this.OffGraphic.enabled = false;
					}
				}
				else if (this.OffGraphic != null)
				{
					this.OffGraphic.enabled = true;
				}
			}
			else
			{
				if (this.m_toggle != null)
				{
					this.m_toggle.interactable = false;
				}
				if (this.OffGraphic != null)
				{
					this.OffGraphic.enabled = false;
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000F26C File Offset: 0x0000D66C
		private void Awake()
		{
			this.m_toggle = base.GetComponent<Toggle>();
			if (this.OffGraphic != null)
			{
				this.OffGraphic.enabled = false;
			}
			this.UpdateState();
			this.m_toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000F2C4 File Offset: 0x0000D6C4
		private void Start()
		{
			this.m_started = true;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000F2CD File Offset: 0x0000D6CD
		private void OnEnable()
		{
			if (this.m_toggle != null)
			{
				this.UpdateState();
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F2E6 File Offset: 0x0000D6E6
		private void OnDestroy()
		{
			if (this.m_toggle != null)
			{
				this.m_toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged));
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000F315 File Offset: 0x0000D715
		private void OnValueChanged(bool value)
		{
			this.UpdateState();
		}

		// Token: 0x0400028E RID: 654
		public Graphic OffGraphic;

		// Token: 0x0400028F RID: 655
		private Toggle m_toggle;

		// Token: 0x04000290 RID: 656
		private bool m_canExpand;

		// Token: 0x04000291 RID: 657
		private bool m_started;
	}
}
