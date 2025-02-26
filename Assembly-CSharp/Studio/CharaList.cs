using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AIChara;
using Illusion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020011D3 RID: 4563
	public class CharaList : MonoBehaviour
	{
		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x060095C7 RID: 38343 RVA: 0x003DE590 File Offset: 0x003DC990
		// (set) Token: 0x060095C8 RID: 38344 RVA: 0x003DE598 File Offset: 0x003DC998
		public bool isInit { get; private set; }

		// Token: 0x060095C9 RID: 38345 RVA: 0x003DE5A4 File Offset: 0x003DC9A4
		public void InitCharaList(bool _force = false)
		{
			if (this.isInit && !_force)
			{
				return;
			}
			this.charaFileSort.DeleteAllNode();
			if (this.sex == 1)
			{
				this.InitFemaleList();
			}
			else
			{
				this.InitMaleList();
			}
			int count = this.charaFileSort.cfiList.Count;
			for (int i = 0; i < count; i++)
			{
				CharaFileInfo info = this.charaFileSort.cfiList[i];
				info.index = i;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectNode);
				if (!gameObject.activeSelf)
				{
					gameObject.SetActive(true);
				}
				gameObject.transform.SetParent(this.charaFileSort.root, false);
				info.node = gameObject.GetComponent<ListNode>();
				info.button = gameObject.GetComponent<Button>();
				info.node.AddActionToButton(delegate
				{
					this.OnSelectChara(info.index);
				});
				info.node.text = info.name;
				info.node.listEnterAction.Add(delegate
				{
					this.LoadCharaImage(info.index);
				});
			}
			this.imageChara.color = Color.clear;
			this.charaFileSort.Sort(0, false);
			this.buttonLoad.interactable = false;
			this.buttonChange.interactable = false;
			this.isInit = true;
		}

		// Token: 0x060095CA RID: 38346 RVA: 0x003DE72C File Offset: 0x003DCB2C
		private void OnSelectChara(int _idx)
		{
			if (this.charaFileSort.select == _idx)
			{
				return;
			}
			this.charaFileSort.select = _idx;
			this.buttonLoad.interactable = true;
			ObjectCtrlInfo ctrlInfo = Studio.GetCtrlInfo(Singleton<Studio>.Instance.treeNodeCtrl.selectNode);
			OCIChar ocichar = ctrlInfo as OCIChar;
			this.buttonChange.interactable = (ocichar != null && ocichar.oiCharInfo.sex == this.sex);
			this.isDelay = true;
			Observable.Timer(TimeSpan.FromMilliseconds(250.0)).Subscribe(delegate(long _)
			{
				this.isDelay = false;
			}).AddTo(this);
		}

		// Token: 0x060095CB RID: 38347 RVA: 0x003DE7DC File Offset: 0x003DCBDC
		private void LoadCharaImage(int _idx)
		{
			if (this.isDelay)
			{
				return;
			}
			CharaFileInfo charaFileInfo = this.charaFileSort.cfiList[_idx];
			this.imageChara.texture = PngAssist.LoadTexture(charaFileInfo.file);
			this.imageChara.color = Color.white;
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x060095CC RID: 38348 RVA: 0x003DE838 File Offset: 0x003DCC38
		public void OnSort(int _type)
		{
			this.charaFileSort.select = -1;
			this.buttonLoad.interactable = false;
			this.buttonChange.interactable = false;
			this.charaFileSort.Sort(_type);
		}

		// Token: 0x060095CD RID: 38349 RVA: 0x003DE86A File Offset: 0x003DCC6A
		public void LoadCharaFemale()
		{
			Singleton<Studio>.Instance.AddFemale(this.charaFileSort.selectPath);
		}

		// Token: 0x060095CE RID: 38350 RVA: 0x003DE884 File Offset: 0x003DCC84
		public void ChangeCharaFemale()
		{
			OCIChar[] array = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
			select Studio.GetCtrlInfo(v) as OCIChar into v
			where v != null
			where v.oiCharInfo.sex == 1
			select v).ToArray<OCIChar>();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i].ChangeChara(this.charaFileSort.selectPath);
			}
		}

		// Token: 0x060095CF RID: 38351 RVA: 0x003DE930 File Offset: 0x003DCD30
		private void InitFemaleList()
		{
			List<string> list = new List<string>();
			string folder = UserData.Path + "chara/female";
			Illusion.Utils.File.GetAllFiles(folder, "*.png", ref list);
			this.charaFileSort.cfiList.Clear();
			foreach (string text in list)
			{
				ChaFileControl chaFileControl = new ChaFileControl();
				if (chaFileControl.LoadCharaFile(text, 1, true, true))
				{
					this.charaFileSort.cfiList.Add(new CharaFileInfo(string.Empty, string.Empty)
					{
						file = text,
						name = chaFileControl.parameter.fullname,
						time = File.GetLastWriteTime(text)
					});
				}
			}
		}

		// Token: 0x060095D0 RID: 38352 RVA: 0x003DEA1C File Offset: 0x003DCE1C
		public void LoadCharaMale()
		{
			Singleton<Studio>.Instance.AddMale(this.charaFileSort.selectPath);
		}

		// Token: 0x060095D1 RID: 38353 RVA: 0x003DEA34 File Offset: 0x003DCE34
		public void ChangeCharaMale()
		{
			OCIChar[] array = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
			select Studio.GetCtrlInfo(v) as OCIChar into v
			where v != null
			where v.oiCharInfo.sex == 0
			select v).ToArray<OCIChar>();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i].ChangeChara(this.charaFileSort.selectPath);
			}
		}

		// Token: 0x060095D2 RID: 38354 RVA: 0x003DEAE0 File Offset: 0x003DCEE0
		private void InitMaleList()
		{
			List<string> list = new List<string>();
			string folder = UserData.Path + "chara/male";
			Illusion.Utils.File.GetAllFiles(folder, "*.png", ref list);
			this.charaFileSort.cfiList.Clear();
			foreach (string text in list)
			{
				ChaFileControl chaFileControl = new ChaFileControl();
				if (chaFileControl.LoadCharaFile(text, 0, true, true))
				{
					this.charaFileSort.cfiList.Add(new CharaFileInfo(string.Empty, string.Empty)
					{
						file = text,
						name = chaFileControl.parameter.fullname,
						time = File.GetLastWriteTime(text)
					});
				}
			}
		}

		// Token: 0x060095D3 RID: 38355 RVA: 0x003DEBCC File Offset: 0x003DCFCC
		private void OnSelect(TreeNodeObject _node)
		{
			if (_node == null)
			{
				return;
			}
			if (!Singleton<Studio>.IsInstance())
			{
				return;
			}
			ObjectCtrlInfo objectCtrlInfo = null;
			if (!Singleton<Studio>.Instance.dicInfo.TryGetValue(_node, out objectCtrlInfo))
			{
				this.buttonChange.interactable = false;
				return;
			}
			if (objectCtrlInfo.kind != 0)
			{
				this.buttonChange.interactable = false;
				return;
			}
			OCIChar ocichar = objectCtrlInfo as OCIChar;
			if (ocichar == null || ocichar.oiCharInfo.sex != this.sex)
			{
				this.buttonChange.interactable = false;
				return;
			}
			if (this.charaFileSort.select != -1)
			{
				this.buttonChange.interactable = true;
			}
		}

		// Token: 0x060095D4 RID: 38356 RVA: 0x003DEC7C File Offset: 0x003DD07C
		private void OnDeselect(TreeNodeObject _node)
		{
			if (_node == null)
			{
				return;
			}
			if (!Singleton<Studio>.IsInstance())
			{
				return;
			}
			OCIChar[] self = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
			select Studio.GetCtrlInfo(v) as OCIChar into v
			where v != null
			where v.oiCharInfo.sex == this.sex
			select v).ToArray<OCIChar>();
			this.buttonChange.interactable = !self.IsNullOrEmpty<OCIChar>();
		}

		// Token: 0x060095D5 RID: 38357 RVA: 0x003DED1C File Offset: 0x003DD11C
		private void OnDelete(ObjectCtrlInfo _info)
		{
			if (_info == null)
			{
				return;
			}
			if (_info.kind != 0)
			{
				return;
			}
			OCIChar ocichar = _info as OCIChar;
			if (ocichar == null || ocichar.oiCharInfo.sex != this.sex)
			{
				return;
			}
			if (this.charaFileSort.select != -1)
			{
				this.buttonChange.interactable = false;
			}
		}

		// Token: 0x060095D6 RID: 38358 RVA: 0x003DED80 File Offset: 0x003DD180
		private void Awake()
		{
			this.isInit = false;
			this.InitCharaList(false);
			TreeNodeCtrl treeNodeCtrl = Singleton<Studio>.Instance.treeNodeCtrl;
			treeNodeCtrl.onSelect = (Action<TreeNodeObject>)Delegate.Combine(treeNodeCtrl.onSelect, new Action<TreeNodeObject>(this.OnSelect));
			Studio instance = Singleton<Studio>.Instance;
			instance.onDelete = (Action<ObjectCtrlInfo>)Delegate.Combine(instance.onDelete, new Action<ObjectCtrlInfo>(this.OnDelete));
			TreeNodeCtrl treeNodeCtrl2 = Singleton<Studio>.Instance.treeNodeCtrl;
			treeNodeCtrl2.onDeselect = (Action<TreeNodeObject>)Delegate.Combine(treeNodeCtrl2.onDeselect, new Action<TreeNodeObject>(this.OnDeselect));
		}

		// Token: 0x0400789A RID: 30874
		[SerializeField]
		private int sex = 1;

		// Token: 0x0400789B RID: 30875
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x0400789C RID: 30876
		[SerializeField]
		private RawImage imageChara;

		// Token: 0x0400789D RID: 30877
		[SerializeField]
		private CharaFileSort charaFileSort = new CharaFileSort();

		// Token: 0x0400789E RID: 30878
		[SerializeField]
		private Button buttonLoad;

		// Token: 0x0400789F RID: 30879
		[SerializeField]
		private Button buttonChange;

		// Token: 0x040078A1 RID: 30881
		private bool isDelay;
	}
}
