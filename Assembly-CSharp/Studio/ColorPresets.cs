using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Illusion.Component.UI;
using Illusion.Extensions;
using IllusionUtility.GetUtility;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012D2 RID: 4818
	public class ColorPresets : MonoBehaviour
	{
		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x0600A0AC RID: 41132 RVA: 0x00420154 File Offset: 0x0041E554
		// (remove) Token: 0x0600A0AD RID: 41133 RVA: 0x0042018C File Offset: 0x0041E58C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Color> updateColorAction;

		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x0600A0AE RID: 41134 RVA: 0x004201C4 File Offset: 0x0041E5C4
		// (remove) Token: 0x0600A0AF RID: 41135 RVA: 0x004201FC File Offset: 0x0041E5FC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action clickAction;

		// Token: 0x170021EF RID: 8687
		// (get) Token: 0x0600A0B0 RID: 41136 RVA: 0x00420232 File Offset: 0x0041E632
		// (set) Token: 0x0600A0B1 RID: 41137 RVA: 0x0042023A File Offset: 0x0041E63A
		public Color color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
				this.SetColor(this._color);
			}
		}

		// Token: 0x0600A0B2 RID: 41138 RVA: 0x00420250 File Offset: 0x0041E650
		private void Awake()
		{
			if (null == this.objNew)
			{
				this.objNew = base.transform.FindLoop("New");
			}
			if (this.objNew)
			{
				Transform transform = this.objNew.transform.Find("imgColor");
				if (transform)
				{
					this.imgNew = transform.GetComponent<Image>();
				}
				this.trfParent = this.objNew.transform.parent;
			}
			if (null == this.objTemplate)
			{
				this.objTemplate = base.transform.FindLoop("TemplateColor");
			}
			this.lstInfo.Clear();
		}

		// Token: 0x0600A0B3 RID: 41139 RVA: 0x0042030C File Offset: 0x0041E70C
		private void Start()
		{
			this.saveDir = UserData.Path + "Custom/";
			this.LoadPresets();
			for (int i = 0; i < this.tglFile.Length; i++)
			{
				this.tglFile[i].isOn = false;
			}
			this.tglFile[this.colorInfo.select].isOn = true;
			this.SetPreset(false);
			if (this.objNew)
			{
				this.trfParent = this.objNew.transform.parent;
				Button component = this.objNew.GetComponent<Button>();
				if (component)
				{
					component.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						this.AddNewPreset(this.color, false);
						this.SavePresets();
					});
				}
				this.objNew.SetActiveIfDifferent(5 != this.colorInfo.select);
			}
			if (null != this.btnDelete)
			{
				this.btnDelete.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.SetPreset(true);
				});
				this.btnDelete.gameObject.SetActiveIfDifferent(5 != this.colorInfo.select);
			}
			for (int j = 0; j < this.tglFile.Length; j++)
			{
				int no = j;
				this.tglFile[j].onValueChanged.AddListener(delegate(bool isOn)
				{
					if (isOn)
					{
						this.colorInfo.select = no;
						this.SetPreset(false);
						this.SavePresets();
						this.btnDelete.gameObject.SetActiveIfDifferent(5 != no);
						this.objNew.SetActiveIfDifferent(5 != no);
					}
				});
			}
		}

		// Token: 0x0600A0B4 RID: 41140 RVA: 0x00420488 File Offset: 0x0041E888
		public int GetSelectIndex()
		{
			for (int i = 0; i < this.tglFile.Length; i++)
			{
				if (this.tglFile[i].isOn)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x0600A0B5 RID: 41141 RVA: 0x004204C3 File Offset: 0x0041E8C3
		public void SetColor(Color c)
		{
			if (this.objNew && this.imgNew)
			{
				this.imgNew.color = c;
			}
		}

		// Token: 0x0600A0B6 RID: 41142 RVA: 0x004204F4 File Offset: 0x0041E8F4
		public void AddNewPreset(Color addColor, bool load = false)
		{
			GameObject addObj = UnityEngine.Object.Instantiate<GameObject>(this.objTemplate, this.trfParent);
			if (null != addObj)
			{
				int idx = this.GetSelectIndex();
				addObj.name = string.Format("PresetColor", Array.Empty<object>());
				addObj.transform.SetSiblingIndex(this.lstInfo.Count);
				this.objNew.transform.SetSiblingIndex(90);
				UI_ColorPresetsInfo cpi = addObj.GetComponent<UI_ColorPresetsInfo>();
				cpi.color = addColor;
				if (cpi.image)
				{
					cpi.image.color = addColor;
				}
				this.lstInfo.Add(cpi);
				MouseButtonCheck mouseButtonCheck = addObj.AddComponent<MouseButtonCheck>();
				mouseButtonCheck.buttonType = MouseButtonCheck.ButtonType.Right;
				mouseButtonCheck.onPointerUp.AddListener(delegate(PointerEventData data)
				{
					if (this.colorInfo.select != 5)
					{
						this.lstInfo.Remove(cpi);
						this.colorInfo.SetList(idx, (from info in this.lstInfo
						select info.color).ToList<Color>());
						this.SavePresets();
						UnityEngine.Object.Destroy(addObj);
					}
				});
				if (this.colorInfo.select == 5)
				{
					UI_OnMouseOverMessage component = addObj.GetComponent<UI_OnMouseOverMessage>();
					if (component)
					{
						component.comment = "左クリックで適用";
					}
				}
				MouseButtonCheck mouseButtonCheck2 = addObj.AddComponent<MouseButtonCheck>();
				mouseButtonCheck2.buttonType = MouseButtonCheck.ButtonType.Left;
				mouseButtonCheck2.onPointerUp.AddListener(delegate(PointerEventData data)
				{
					this.SetColor(cpi.color);
					this.updateColorAction.Call(cpi.color);
					this.clickAction.Call();
				});
				if (!load)
				{
					this.colorInfo.SetList(idx, (from info in this.lstInfo
					select info.color).ToList<Color>());
				}
				addObj.SetActiveIfDifferent(true);
				if (90 <= this.lstInfo.Count)
				{
					this.objNew.SetActiveIfDifferent(false);
				}
			}
		}

		// Token: 0x0600A0B7 RID: 41143 RVA: 0x004206E4 File Offset: 0x0041EAE4
		public void SetPreset(bool delete = false)
		{
			int count = this.lstInfo.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				UnityEngine.Object.Destroy(this.lstInfo[i].gameObject);
			}
			this.lstInfo.Clear();
			int select = this.colorInfo.select;
			if (delete)
			{
				this.colorInfo.DeleteList(select);
			}
			List<Color> list = this.colorInfo.GetList(select);
			count = list.Count;
			for (int j = 0; j < count; j++)
			{
				this.AddNewPreset(list[j], true);
			}
		}

		// Token: 0x0600A0B8 RID: 41144 RVA: 0x0042078C File Offset: 0x0041EB8C
		public void SavePresets()
		{
			string path = this.saveDir + this.saveFile;
			if (!Directory.Exists(this.saveDir))
			{
				Directory.CreateDirectory(this.saveDir);
			}
			string contents = JsonUtility.ToJson(this.colorInfo);
			File.WriteAllText(path, contents);
		}

		// Token: 0x0600A0B9 RID: 41145 RVA: 0x004207DC File Offset: 0x0041EBDC
		public void LoadPresets()
		{
			string path = this.saveDir + this.saveFile;
			if (!File.Exists(path))
			{
				return;
			}
			string json = File.ReadAllText(path);
			this.colorInfo = JsonUtility.FromJson<ColorPresets.ColorInfo>(json);
		}

		// Token: 0x04007EEB RID: 32491
		private ColorPresets.ColorInfo colorInfo = new ColorPresets.ColorInfo();

		// Token: 0x04007EEC RID: 32492
		private string saveDir = string.Empty;

		// Token: 0x04007EED RID: 32493
		private readonly string saveFile = "ColorPresets.json";

		// Token: 0x04007EEE RID: 32494
		private const int presetMax = 90;

		// Token: 0x04007EF1 RID: 32497
		[SerializeField]
		private Toggle[] tglFile;

		// Token: 0x04007EF2 RID: 32498
		[SerializeField]
		private GameObject objTemplate;

		// Token: 0x04007EF3 RID: 32499
		[SerializeField]
		private GameObject objNew;

		// Token: 0x04007EF4 RID: 32500
		private Image imgNew;

		// Token: 0x04007EF5 RID: 32501
		private Transform trfParent;

		// Token: 0x04007EF6 RID: 32502
		[SerializeField]
		private Button btnDelete;

		// Token: 0x04007EF7 RID: 32503
		private List<UI_ColorPresetsInfo> lstInfo = new List<UI_ColorPresetsInfo>();

		// Token: 0x04007EF8 RID: 32504
		private Color _color = Color.white;

		// Token: 0x020012D3 RID: 4819
		public class ColorInfo
		{
			// Token: 0x0600A0BE RID: 41150 RVA: 0x0042089C File Offset: 0x0041EC9C
			public void SetList(int idx, List<Color> lst)
			{
				switch (idx)
				{
				case 0:
					this.lstColor01 = lst;
					break;
				case 1:
					this.lstColor02 = lst;
					break;
				case 2:
					this.lstColor03 = lst;
					break;
				case 3:
					this.lstColor04 = lst;
					break;
				case 4:
					this.lstColor05 = lst;
					break;
				case 5:
					this.lstColorSample = lst;
					break;
				}
			}

			// Token: 0x0600A0BF RID: 41151 RVA: 0x0042091C File Offset: 0x0041ED1C
			public List<Color> GetList(int idx)
			{
				switch (idx)
				{
				case 0:
					return this.lstColor01;
				case 1:
					return this.lstColor02;
				case 2:
					return this.lstColor03;
				case 3:
					return this.lstColor04;
				case 4:
					return this.lstColor05;
				case 5:
					return this.lstColorSample;
				default:
					return null;
				}
			}

			// Token: 0x0600A0C0 RID: 41152 RVA: 0x00420978 File Offset: 0x0041ED78
			public void DeleteList(int idx)
			{
				switch (idx)
				{
				case 0:
					this.lstColor01.Clear();
					break;
				case 1:
					this.lstColor02.Clear();
					break;
				case 2:
					this.lstColor03.Clear();
					break;
				case 3:
					this.lstColor04.Clear();
					break;
				case 4:
					this.lstColor05.Clear();
					break;
				case 5:
					this.lstColorSample.Clear();
					break;
				}
			}

			// Token: 0x04007EFA RID: 32506
			public int select = 5;

			// Token: 0x04007EFB RID: 32507
			public List<Color> lstColor01 = new List<Color>();

			// Token: 0x04007EFC RID: 32508
			public List<Color> lstColor02 = new List<Color>();

			// Token: 0x04007EFD RID: 32509
			public List<Color> lstColor03 = new List<Color>();

			// Token: 0x04007EFE RID: 32510
			public List<Color> lstColor04 = new List<Color>();

			// Token: 0x04007EFF RID: 32511
			public List<Color> lstColor05 = new List<Color>();

			// Token: 0x04007F00 RID: 32512
			public List<Color> lstColorSample = new List<Color>();
		}
	}
}
