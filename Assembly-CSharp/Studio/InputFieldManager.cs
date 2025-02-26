using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200123C RID: 4668
	[AddComponentMenu("Studio/Manager/Input Field", 1000)]
	public class InputFieldManager : Singleton<InputFieldManager>
	{
		// Token: 0x170020B2 RID: 8370
		// (set) Token: 0x06009989 RID: 39305 RVA: 0x003F3C7A File Offset: 0x003F207A
		public static StudioInputField studioInputField
		{
			set
			{
				if (Singleton<InputFieldManager>.IsInstance())
				{
					Singleton<InputFieldManager>.Instance.m_StudioInputField = value;
					Singleton<InputFieldManager>.Instance.enabled = (value != null);
				}
			}
		}

		// Token: 0x170020B3 RID: 8371
		// (get) Token: 0x0600998A RID: 39306 RVA: 0x003F3CA2 File Offset: 0x003F20A2
		public static bool isFocused
		{
			get
			{
				return Singleton<InputFieldManager>.IsInstance() && Singleton<InputFieldManager>.Instance.m_StudioInputField && Singleton<InputFieldManager>.Instance.m_StudioInputField.isFocused;
			}
		}

		// Token: 0x0600998B RID: 39307 RVA: 0x003F3CDD File Offset: 0x003F20DD
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			InputFieldManager.studioInputField = null;
		}

		// Token: 0x0600998C RID: 39308 RVA: 0x003F3CFC File Offset: 0x003F20FC
		private void Update()
		{
			if (this.m_StudioInputField && this.m_StudioInputField.isFocused && Input.anyKey && !Input.GetMouseButton(0))
			{
				Input.ResetInputAxes();
			}
		}

		// Token: 0x04007AE2 RID: 31458
		private StudioInputField m_StudioInputField;
	}
}
