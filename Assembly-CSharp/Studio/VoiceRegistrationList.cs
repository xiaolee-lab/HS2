using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012EF RID: 4847
	public class VoiceRegistrationList : MonoBehaviour
	{
		// Token: 0x17002216 RID: 8726
		// (get) Token: 0x0600A1CE RID: 41422 RVA: 0x004263F4 File Offset: 0x004247F4
		// (set) Token: 0x0600A1CF RID: 41423 RVA: 0x004263FC File Offset: 0x004247FC
		public OCIChar ociChar { get; set; }

		// Token: 0x17002217 RID: 8727
		// (get) Token: 0x0600A1D0 RID: 41424 RVA: 0x00426405 File Offset: 0x00424805
		// (set) Token: 0x0600A1D1 RID: 41425 RVA: 0x00426412 File Offset: 0x00424812
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
					if (base.gameObject.activeSelf)
					{
						this.InitList();
					}
				}
			}
		}

		// Token: 0x0600A1D2 RID: 41426 RVA: 0x00426447 File Offset: 0x00424847
		private void OnClickClose()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600A1D3 RID: 41427 RVA: 0x00426455 File Offset: 0x00424855
		private void OnEndEditName(string _text)
		{
			this.buttonSave.interactable = !_text.IsNullOrEmpty();
		}

		// Token: 0x0600A1D4 RID: 41428 RVA: 0x0042646B File Offset: 0x0042486B
		private void OnClickSave()
		{
			this.ociChar.voiceCtrl.SaveList(this.inputName.text);
			this.InitList();
		}

		// Token: 0x0600A1D5 RID: 41429 RVA: 0x0042648E File Offset: 0x0042488E
		private void OnClickLoad()
		{
			this.ociChar.voiceCtrl.LoadList(this.listPath[this.select], false);
			this.voiceControl.InitList();
		}

		// Token: 0x0600A1D6 RID: 41430 RVA: 0x004264BE File Offset: 0x004248BE
		private void OnClickImport()
		{
			this.ociChar.voiceCtrl.LoadList(this.listPath[this.select], true);
			this.voiceControl.InitList();
		}

		// Token: 0x0600A1D7 RID: 41431 RVA: 0x004264F0 File Offset: 0x004248F0
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

		// Token: 0x0600A1D8 RID: 41432 RVA: 0x0042654E File Offset: 0x0042494E
		private void OnSelectDeleteYes()
		{
			Singleton<Scene>.Instance.UnLoad();
			File.Delete(this.listPath[this.select]);
			this.InitList();
		}

		// Token: 0x0600A1D9 RID: 41433 RVA: 0x00426577 File Offset: 0x00424977
		private void OnSelectDeleteNo()
		{
			Singleton<Scene>.Instance.UnLoad();
		}

		// Token: 0x0600A1DA RID: 41434 RVA: 0x00426584 File Offset: 0x00424984
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
				this.buttonImport.interactable = true;
				this.buttonDelete.interactable = true;
			}
		}

		// Token: 0x0600A1DB RID: 41435 RVA: 0x00426608 File Offset: 0x00424A08
		private void InitList()
		{
			for (int i = 0; i < this.transformRoot.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			this.select = -1;
			this.inputName.text = string.Empty;
			this.buttonSave.interactable = false;
			this.buttonLoad.interactable = false;
			this.buttonImport.interactable = false;
			this.buttonDelete.interactable = false;
			IEnumerable<string> files = Directory.GetFiles(UserData.Create("studio/voicelist"), "*.dat");
			if (VoiceRegistrationList.<>f__mg$cache0 == null)
			{
				VoiceRegistrationList.<>f__mg$cache0 = new Func<string, bool>(VoiceCtrl.CheckIdentifyingCode);
			}
			this.listPath = files.Where(VoiceRegistrationList.<>f__mg$cache0).ToList<string>();
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
				component.text = VoiceCtrl.LoadListName(this.listPath[j]);
				this.dicNode.Add(j, component);
			}
		}

		// Token: 0x0600A1DC RID: 41436 RVA: 0x0042677C File Offset: 0x00424B7C
		private void Awake()
		{
			this.buttonClose.onClick.AddListener(new UnityAction(this.OnClickClose));
			this.inputName.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditName));
			this.buttonSave.onClick.AddListener(new UnityAction(this.OnClickSave));
			this.buttonSave.interactable = false;
			this.buttonLoad.onClick.AddListener(new UnityAction(this.OnClickLoad));
			this.buttonImport.onClick.AddListener(new UnityAction(this.OnClickImport));
			this.buttonDelete.onClick.AddListener(new UnityAction(this.OnClickDelete));
		}

		// Token: 0x04007FEE RID: 32750
		[SerializeField]
		private Button buttonClose;

		// Token: 0x04007FEF RID: 32751
		[SerializeField]
		private InputField inputName;

		// Token: 0x04007FF0 RID: 32752
		[SerializeField]
		private Button buttonSave;

		// Token: 0x04007FF1 RID: 32753
		[SerializeField]
		private Button buttonLoad;

		// Token: 0x04007FF2 RID: 32754
		[SerializeField]
		private Button buttonImport;

		// Token: 0x04007FF3 RID: 32755
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007FF4 RID: 32756
		[SerializeField]
		private GameObject prefabNode;

		// Token: 0x04007FF5 RID: 32757
		[SerializeField]
		private Button buttonDelete;

		// Token: 0x04007FF6 RID: 32758
		[SerializeField]
		private Sprite spriteDelete;

		// Token: 0x04007FF7 RID: 32759
		[SerializeField]
		private VoiceControl voiceControl;

		// Token: 0x04007FF9 RID: 32761
		private List<string> listPath;

		// Token: 0x04007FFA RID: 32762
		private Dictionary<int, StudioNode> dicNode = new Dictionary<int, StudioNode>();

		// Token: 0x04007FFB RID: 32763
		private int select = -1;

		// Token: 0x04007FFC RID: 32764
		[CompilerGenerated]
		private static Func<string, bool> <>f__mg$cache0;
	}
}
