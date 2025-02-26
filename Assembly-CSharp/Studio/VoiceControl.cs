using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012EB RID: 4843
	public class VoiceControl : MonoBehaviour
	{
		// Token: 0x1700220F RID: 8719
		// (set) Token: 0x0600A1A5 RID: 41381 RVA: 0x00425639 File Offset: 0x00423A39
		public OCIChar ociChar
		{
			set
			{
				this.m_OCIChar = value;
				if (this.m_OCIChar != null)
				{
					this.InitList();
				}
			}
		}

		// Token: 0x17002210 RID: 8720
		// (get) Token: 0x0600A1A6 RID: 41382 RVA: 0x00425653 File Offset: 0x00423A53
		// (set) Token: 0x0600A1A7 RID: 41383 RVA: 0x00425660 File Offset: 0x00423A60
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

		// Token: 0x0600A1A8 RID: 41384 RVA: 0x00425680 File Offset: 0x00423A80
		public void InitList()
		{
			for (int i = 0; i < this.transformRoot.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			this.select = -1;
			this.listNode.Clear();
			foreach (VoiceCtrl.VoiceInfo voiceInfo in this.m_OCIChar.voiceCtrl.list)
			{
				Info.LoadCommonInfo voiceInfo2 = Singleton<Info>.Instance.GetVoiceInfo(voiceInfo.group, voiceInfo.category, voiceInfo.no);
				if (voiceInfo2 != null)
				{
					this.AddNode(voiceInfo2.name);
				}
			}
			this.scrollRect.verticalNormalizedPosition = 1f;
			this.imagePlayNow.enabled = (this.m_OCIChar != null && this.m_OCIChar.voiceCtrl.isPlay);
			this.buttonRepeat.image.sprite = this.spriteRepeat[(int)this.m_OCIChar.voiceRepeat];
			this.voiceRegistrationList.ociChar = this.m_OCIChar;
		}

		// Token: 0x0600A1A9 RID: 41385 RVA: 0x004257D4 File Offset: 0x00423BD4
		private void AddNode(string _name)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectNode);
			gameObject.transform.SetParent(this.transformRoot, false);
			VoicePlayNode vpn = gameObject.GetComponent<VoicePlayNode>();
			vpn.active = true;
			vpn.addOnClick = delegate()
			{
				this.OnClickSelect(vpn);
			};
			vpn.addOnClickDelete = delegate()
			{
				this.OnClickDelete(vpn);
			};
			vpn.text = _name;
			this.listNode.Add(vpn);
		}

		// Token: 0x0600A1AA RID: 41386 RVA: 0x00425870 File Offset: 0x00423C70
		private void OnClickRepeat()
		{
			int num = Enum.GetNames(typeof(VoiceCtrl.Repeat)).Length;
			int num2 = (int)this.m_OCIChar.voiceRepeat;
			num2 = (num2 + 1) % num;
			this.m_OCIChar.voiceRepeat = (VoiceCtrl.Repeat)num2;
			this.buttonRepeat.image.sprite = this.spriteRepeat[num2];
		}

		// Token: 0x0600A1AB RID: 41387 RVA: 0x004258C5 File Offset: 0x00423CC5
		private void OnClickStop()
		{
			this.m_OCIChar.StopVoice();
		}

		// Token: 0x0600A1AC RID: 41388 RVA: 0x004258D4 File Offset: 0x00423CD4
		private void OnClickPlay()
		{
			bool enabled = this.m_OCIChar.PlayVoice(this.select);
			this.imagePlayNow.enabled = enabled;
		}

		// Token: 0x0600A1AD RID: 41389 RVA: 0x00425900 File Offset: 0x00423D00
		private void OnClickPlayAll()
		{
			OCIChar[] array = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
			select Studio.GetCtrlInfo(v) as OCIChar into v
			where v != null
			select v).ToArray<OCIChar>();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i].PlayVoice(0);
			}
		}

		// Token: 0x0600A1AE RID: 41390 RVA: 0x00425984 File Offset: 0x00423D84
		private void OnClickStopAll()
		{
			OCIChar[] array = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
			select Studio.GetCtrlInfo(v) as OCIChar into v
			where v != null
			select v).ToArray<OCIChar>();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i].StopVoice();
			}
		}

		// Token: 0x0600A1AF RID: 41391 RVA: 0x00425A04 File Offset: 0x00423E04
		private void OnClickExpansion()
		{
			bool flag = !this.objBeneath.activeSelf;
			this.objBeneath.SetActive(flag);
			this.buttonExpansion.image.sprite = this.spriteExpansion[(!flag) ? 0 : 1];
		}

		// Token: 0x0600A1B0 RID: 41392 RVA: 0x00425A50 File Offset: 0x00423E50
		private void OnClickSave()
		{
			this.voiceRegistrationList.active = !this.voiceRegistrationList.active;
			if (this.voiceRegistrationList.active)
			{
				this.voiceRegistrationList.ociChar = this.m_OCIChar;
			}
		}

		// Token: 0x0600A1B1 RID: 41393 RVA: 0x00425A8C File Offset: 0x00423E8C
		private void OnClickDeleteAll()
		{
			int count = this.listNode.Count;
			for (int i = 0; i < count; i++)
			{
				this.listNode[i].Destroy();
			}
			this.listNode.Clear();
			this.scrollRect.verticalNormalizedPosition = 1f;
			this.m_OCIChar.DeleteAllVoice();
		}

		// Token: 0x0600A1B2 RID: 41394 RVA: 0x00425AF0 File Offset: 0x00423EF0
		private void OnClickSelect(VoicePlayNode _vpn)
		{
			if (MathfEx.RangeEqualOn<int>(0, this.select, this.listNode.Count))
			{
				this.listNode[this.select].select = false;
			}
			this.select = this.listNode.FindIndex((VoicePlayNode v) => v == _vpn);
			this.listNode[this.select].select = true;
		}

		// Token: 0x0600A1B3 RID: 41395 RVA: 0x00425B74 File Offset: 0x00423F74
		private void OnClickDelete(VoicePlayNode _vpn)
		{
			int num = this.listNode.FindIndex((VoicePlayNode v) => v == _vpn);
			this.listNode.RemoveAt(num);
			_vpn.Destroy();
			this.scrollRect.verticalNormalizedPosition = 1f;
			this.m_OCIChar.DeleteVoice(num);
			if (this.select == num)
			{
				this.select = -1;
			}
		}

		// Token: 0x0600A1B4 RID: 41396 RVA: 0x00425BEC File Offset: 0x00423FEC
		private void Start()
		{
			this.buttonRepeat.onClick.AddListener(new UnityAction(this.OnClickRepeat));
			this.buttonStop.onClick.AddListener(new UnityAction(this.OnClickStop));
			this.buttonPlay.onClick.AddListener(new UnityAction(this.OnClickPlay));
			this.buttonPlayAll.onClick.AddListener(new UnityAction(this.OnClickPlayAll));
			this.buttonStopAll.onClick.AddListener(new UnityAction(this.OnClickStopAll));
			this.buttonExpansion.onClick.AddListener(new UnityAction(this.OnClickExpansion));
			this.buttonSave.onClick.AddListener(new UnityAction(this.OnClickSave));
			this.buttonDeleteAll.onClick.AddListener(new UnityAction(this.OnClickDeleteAll));
		}

		// Token: 0x0600A1B5 RID: 41397 RVA: 0x00425CD9 File Offset: 0x004240D9
		private void Update()
		{
			if (this.imagePlayNow.enabled)
			{
				this.imagePlayNow.enabled = (this.m_OCIChar != null && this.m_OCIChar.voiceCtrl.isPlay);
			}
		}

		// Token: 0x04007FC6 RID: 32710
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x04007FC7 RID: 32711
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007FC8 RID: 32712
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x04007FC9 RID: 32713
		[SerializeField]
		private Button buttonRepeat;

		// Token: 0x04007FCA RID: 32714
		[SerializeField]
		private Sprite[] spriteRepeat;

		// Token: 0x04007FCB RID: 32715
		[SerializeField]
		private Button buttonStop;

		// Token: 0x04007FCC RID: 32716
		[SerializeField]
		private Button buttonPlay;

		// Token: 0x04007FCD RID: 32717
		[SerializeField]
		private Image imagePlayNow;

		// Token: 0x04007FCE RID: 32718
		[SerializeField]
		private Button buttonPlayAll;

		// Token: 0x04007FCF RID: 32719
		[SerializeField]
		private Button buttonStopAll;

		// Token: 0x04007FD0 RID: 32720
		[SerializeField]
		private Button buttonExpansion;

		// Token: 0x04007FD1 RID: 32721
		[SerializeField]
		private Sprite[] spriteExpansion;

		// Token: 0x04007FD2 RID: 32722
		[SerializeField]
		private GameObject objBeneath;

		// Token: 0x04007FD3 RID: 32723
		[SerializeField]
		private Button buttonSave;

		// Token: 0x04007FD4 RID: 32724
		[SerializeField]
		private Button buttonDeleteAll;

		// Token: 0x04007FD5 RID: 32725
		[SerializeField]
		private VoiceRegistrationList voiceRegistrationList;

		// Token: 0x04007FD6 RID: 32726
		private OCIChar m_OCIChar;

		// Token: 0x04007FD7 RID: 32727
		private List<VoicePlayNode> listNode = new List<VoicePlayNode>();

		// Token: 0x04007FD8 RID: 32728
		private int select = -1;
	}
}
