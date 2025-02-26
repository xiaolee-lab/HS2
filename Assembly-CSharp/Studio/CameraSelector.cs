using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Studio
{
	// Token: 0x020012CD RID: 4813
	public class CameraSelector : MonoBehaviour
	{
		// Token: 0x0600A08B RID: 41099 RVA: 0x0041FAE4 File Offset: 0x0041DEE4
		private void OnValueChanged(int _index)
		{
			if (_index == 0)
			{
				Singleton<Studio>.Instance.ChangeCamera(Singleton<Studio>.Instance.ociCamera, false, false);
			}
			else
			{
				Singleton<Studio>.Instance.ChangeCamera(this.listCamera[_index - 1], true, false);
			}
		}

		// Token: 0x0600A08C RID: 41100 RVA: 0x0041FB24 File Offset: 0x0041DF24
		public void SetCamera(OCICamera _ocic)
		{
			int num = (_ocic != null) ? this.listCamera.FindIndex((OCICamera c) => c == _ocic) : -1;
			this.dropdown.value = num + 1;
		}

		// Token: 0x0600A08D RID: 41101 RVA: 0x0041FB75 File Offset: 0x0041DF75
		public void NextCamera()
		{
			if (this.listCamera.IsNullOrEmpty<OCICamera>())
			{
				return;
			}
			this.index = (this.index + 1) % (this.listCamera.Count + 1);
			this.OnValueChanged(this.index);
		}

		// Token: 0x0600A08E RID: 41102 RVA: 0x0041FBB0 File Offset: 0x0041DFB0
		public void Init()
		{
			this.dropdown.ClearOptions();
			List<ObjectInfo> list = ObjectInfoAssist.Find(5);
			this.listCamera = (from i in list
			select Studio.GetCtrlInfo(i.dicKey) as OCICamera).ToList<OCICamera>();
			this.index = 0;
			List<TMP_Dropdown.OptionData> list2;
			if (list.IsNullOrEmpty<ObjectInfo>())
			{
				list2 = new List<TMP_Dropdown.OptionData>();
			}
			else
			{
				list2 = (from c in list
				select new TMP_Dropdown.OptionData((Studio.GetCtrlInfo(c.dicKey) as OCICamera).name)).ToList<TMP_Dropdown.OptionData>();
			}
			List<TMP_Dropdown.OptionData> list3 = list2;
			list3.Insert(0, new TMP_Dropdown.OptionData("-"));
			this.dropdown.options = list3;
			this.dropdown.interactable = !list.IsNullOrEmpty<ObjectInfo>();
			this.SetCamera(Singleton<Studio>.Instance.ociCamera);
		}

		// Token: 0x0600A08F RID: 41103 RVA: 0x0041FC83 File Offset: 0x0041E083
		private void Awake()
		{
			this.dropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChanged));
			this.dropdown.interactable = false;
		}

		// Token: 0x04007ED7 RID: 32471
		[SerializeField]
		private TMP_Dropdown dropdown;

		// Token: 0x04007ED8 RID: 32472
		private List<OCICamera> listCamera;

		// Token: 0x04007ED9 RID: 32473
		private int index;
	}
}
