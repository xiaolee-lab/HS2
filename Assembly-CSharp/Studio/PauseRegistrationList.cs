using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012E8 RID: 4840
	public class PauseRegistrationList : MonoBehaviour
	{
		// Token: 0x1700220A RID: 8714
		// (get) Token: 0x0600A186 RID: 41350 RVA: 0x00424BC0 File Offset: 0x00422FC0
		// (set) Token: 0x0600A187 RID: 41351 RVA: 0x00424BC8 File Offset: 0x00422FC8
		public OCIChar ociChar
		{
			get
			{
				return this.m_OCIChar;
			}
			set
			{
				this.m_OCIChar = value;
				if (this.m_OCIChar != null)
				{
					this.InitList();
				}
			}
		}

		// Token: 0x1700220B RID: 8715
		// (get) Token: 0x0600A188 RID: 41352 RVA: 0x00424BE2 File Offset: 0x00422FE2
		// (set) Token: 0x0600A189 RID: 41353 RVA: 0x00424BEF File Offset: 0x00422FEF
		public bool active
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				if (base.gameObject.activeSelf != value)
				{
					base.gameObject.SetActive(value);
				}
			}
		}

		// Token: 0x0600A18A RID: 41354 RVA: 0x00424C0E File Offset: 0x0042300E
		private void OnClickClose()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600A18B RID: 41355 RVA: 0x00424C1C File Offset: 0x0042301C
		private void OnEndEditName(string _text)
		{
			this.buttonSave.interactable = !_text.IsNullOrEmpty();
		}

		// Token: 0x0600A18C RID: 41356 RVA: 0x00424C32 File Offset: 0x00423032
		private void OnClickSave()
		{
			PauseCtrl.Save(this.ociChar, this.inputName.text);
			this.InitList();
		}

		// Token: 0x0600A18D RID: 41357 RVA: 0x00424C50 File Offset: 0x00423050
		private void OnClickLoad()
		{
			PauseCtrl.Load(this.ociChar, this.listPath[this.select]);
		}

		// Token: 0x0600A18E RID: 41358 RVA: 0x00424C70 File Offset: 0x00423070
		private void OnClickDelete()
		{
			CheckScene.sprite = this.spriteDelete;
			CheckScene.unityActionYes = new UnityAction(this.OnSelectDeleteYes);
			CheckScene.unityActionNo = new UnityAction(this.OnSelectDeleteNo);
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioCheck",
				isAdd = true
			}, false);
		}

		// Token: 0x0600A18F RID: 41359 RVA: 0x00424CCE File Offset: 0x004230CE
		private void OnSelectDeleteYes()
		{
			Singleton<Scene>.Instance.UnLoad();
			File.Delete(this.listPath[this.select]);
			this.InitList();
		}

		// Token: 0x0600A190 RID: 41360 RVA: 0x00424CF7 File Offset: 0x004230F7
		private void OnSelectDeleteNo()
		{
			Singleton<Scene>.Instance.UnLoad();
		}

		// Token: 0x0600A191 RID: 41361 RVA: 0x00424D04 File Offset: 0x00423104
		private void OnClickSelect(int _no)
		{
			StudioNode studioNode = null;
			if (this.dicNode.TryGetValue(this.select, out studioNode))
			{
				studioNode.select = false;
			}
			this.select = _no;
			if (this.dicNode.TryGetValue(this.select, out studioNode))
			{
				studioNode.select = true;
			}
			if (this.select != -1)
			{
				this.buttonLoad.interactable = true;
				this.buttonDelete.interactable = true;
			}
		}

		// Token: 0x0600A192 RID: 41362 RVA: 0x00424D7C File Offset: 0x0042317C
		private void InitList()
		{
			for (int i = 0; i < this.transformRoot.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			this.select = -1;
			this.buttonLoad.interactable = false;
			this.buttonDelete.interactable = false;
			int sex = this.m_OCIChar.oiCharInfo.sex;
			this.listPath = (from v in Directory.GetFiles(UserData.Create("studio/pose"), "*.dat")
			where PauseCtrl.CheckIdentifyingCode(v, sex)
			select v).ToList<string>();
			this.dicNode.Clear();
			for (int j = 0; j < this.listPath.Count; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabNode);
				gameObject.transform.SetParent(this.transformRoot, false);
				StudioNode component = gameObject.GetComponent<StudioNode>();
				component.active = true;
				int no = j;
				component.addOnClick = delegate()
				{
					this.OnClickSelect(no);
				};
				component.text = PauseCtrl.LoadName(this.listPath[j]);
				this.dicNode.Add(j, component);
			}
		}

		// Token: 0x0600A193 RID: 41363 RVA: 0x00424EE0 File Offset: 0x004232E0
		private void Awake()
		{
			this.buttonClose.onClick.AddListener(new UnityAction(this.OnClickClose));
			this.inputName.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditName));
			this.buttonSave.onClick.AddListener(new UnityAction(this.OnClickSave));
			this.buttonSave.interactable = false;
			this.buttonLoad.onClick.AddListener(new UnityAction(this.OnClickLoad));
			this.buttonDelete.onClick.AddListener(new UnityAction(this.OnClickDelete));
		}

		// Token: 0x04007FAC RID: 32684
		[SerializeField]
		private Button buttonClose;

		// Token: 0x04007FAD RID: 32685
		[SerializeField]
		private InputField inputName;

		// Token: 0x04007FAE RID: 32686
		[SerializeField]
		private Button buttonSave;

		// Token: 0x04007FAF RID: 32687
		[SerializeField]
		private Button buttonLoad;

		// Token: 0x04007FB0 RID: 32688
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007FB1 RID: 32689
		[SerializeField]
		private GameObject prefabNode;

		// Token: 0x04007FB2 RID: 32690
		[SerializeField]
		private Button buttonDelete;

		// Token: 0x04007FB3 RID: 32691
		[SerializeField]
		private Sprite spriteDelete;

		// Token: 0x04007FB4 RID: 32692
		private OCIChar m_OCIChar;

		// Token: 0x04007FB5 RID: 32693
		private List<string> listPath;

		// Token: 0x04007FB6 RID: 32694
		private Dictionary<int, StudioNode> dicNode = new Dictionary<int, StudioNode>();

		// Token: 0x04007FB7 RID: 32695
		private int select = -1;
	}
}
