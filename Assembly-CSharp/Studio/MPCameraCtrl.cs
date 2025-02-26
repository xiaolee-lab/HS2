using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012F9 RID: 4857
	public class MPCameraCtrl : MonoBehaviour
	{
		// Token: 0x1700222A RID: 8746
		// (get) Token: 0x0600A203 RID: 41475 RVA: 0x00427072 File Offset: 0x00425472
		// (set) Token: 0x0600A204 RID: 41476 RVA: 0x0042707A File Offset: 0x0042547A
		public OCICamera ociCamera
		{
			get
			{
				return this.m_OCICamera;
			}
			set
			{
				this.m_OCICamera = value;
				this.UpdateInfo();
			}
		}

		// Token: 0x1700222B RID: 8747
		// (get) Token: 0x0600A205 RID: 41477 RVA: 0x00427089 File Offset: 0x00425489
		// (set) Token: 0x0600A206 RID: 41478 RVA: 0x00427091 File Offset: 0x00425491
		public bool active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
				if (this.m_Active)
				{
					base.gameObject.SetActive(this.m_OCICamera != null);
				}
				else
				{
					base.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x0600A207 RID: 41479 RVA: 0x004270CD File Offset: 0x004254CD
		public bool Deselect(OCICamera _ociCamera)
		{
			if (this.m_OCICamera != _ociCamera)
			{
				return false;
			}
			this.ociCamera = null;
			this.active = false;
			return true;
		}

		// Token: 0x0600A208 RID: 41480 RVA: 0x004270EC File Offset: 0x004254EC
		public void UpdateInfo()
		{
			if (this.m_OCICamera == null)
			{
				return;
			}
			this.isUpdateInfo = true;
			this.inputName.text = this.m_OCICamera.name;
			this.toggleActive.isOn = this.m_OCICamera.cameraInfo.active;
			this.isUpdateInfo = false;
		}

		// Token: 0x0600A209 RID: 41481 RVA: 0x00427144 File Offset: 0x00425544
		private void OnEndEditName(string _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCICamera.name = _value;
			Singleton<Studio>.Instance.cameraSelector.Init();
		}

		// Token: 0x0600A20A RID: 41482 RVA: 0x0042716D File Offset: 0x0042556D
		private void OnValueChangedActive(bool _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			Singleton<Studio>.Instance.ChangeCamera(this.m_OCICamera, _value, false);
		}

		// Token: 0x0600A20B RID: 41483 RVA: 0x0042718D File Offset: 0x0042558D
		private void Start()
		{
			this.inputName.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditName));
			this.toggleActive.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedActive));
		}

		// Token: 0x0400800E RID: 32782
		[SerializeField]
		private TMP_InputField inputName;

		// Token: 0x0400800F RID: 32783
		[SerializeField]
		private Toggle toggleActive;

		// Token: 0x04008010 RID: 32784
		private OCICamera m_OCICamera;

		// Token: 0x04008011 RID: 32785
		private bool m_Active;

		// Token: 0x04008012 RID: 32786
		private bool isUpdateInfo;
	}
}
