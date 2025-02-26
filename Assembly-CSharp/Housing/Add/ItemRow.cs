using System;
using AIProject;
using Illusion.Extensions;
using Manager;
using SuperScrollView;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Housing.Add
{
	// Token: 0x020008A0 RID: 2208
	public class ItemRow : LoopListViewItem2
	{
		// Token: 0x0600395D RID: 14685 RVA: 0x00151EDC File Offset: 0x001502DC
		public void SetData(int _index, AddUICtrl.FileInfo _info, Action _action, bool _select)
		{
			ItemRow.CellInfo cell = this.cellInfos.SafeGet(_index);
			if (cell == null)
			{
				return;
			}
			if (_info == null)
			{
				cell.Active = false;
				return;
			}
			ItemRow.CellInfo cell2 = cell;
			string bundle = _info.loadInfo.thumbnailPath.bundle;
			string file = _info.loadInfo.thumbnailPath.file;
			string manifest = _info.loadInfo.thumbnailPath.manifest;
			cell2.Texture = CommonLib.LoadAsset<Texture2D>(bundle, file, false, manifest);
			cell.button.onClick.RemoveAllListeners();
			cell.button.onClick.AddListener(delegate()
			{
				_action();
				cell.Select = true;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			});
			cell.Init();
			cell.Select = _select;
			cell.Unlock = _info.Unlock;
			cell.Active = true;
		}

		// Token: 0x04003906 RID: 14598
		[SerializeField]
		private ItemRow.CellInfo[] cellInfos;

		// Token: 0x020008A1 RID: 2209
		[Serializable]
		private class CellInfo
		{
			// Token: 0x17000A50 RID: 2640
			// (set) Token: 0x0600395F RID: 14687 RVA: 0x00151FE1 File Offset: 0x001503E1
			public bool Active
			{
				set
				{
					this.gameObject.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x17000A51 RID: 2641
			// (get) Token: 0x06003960 RID: 14688 RVA: 0x00151FF0 File Offset: 0x001503F0
			// (set) Token: 0x06003961 RID: 14689 RVA: 0x00151FFD File Offset: 0x001503FD
			public bool Select
			{
				get
				{
					return this.toggleSelect.isOn;
				}
				set
				{
					this.toggleSelect.isOn = value;
				}
			}

			// Token: 0x17000A52 RID: 2642
			// (set) Token: 0x06003962 RID: 14690 RVA: 0x0015200B File Offset: 0x0015040B
			public Texture2D Texture
			{
				set
				{
					this.rawImage.texture = value;
					this.rawImage.color = ((!(value != null)) ? Color.clear : Color.white);
				}
			}

			// Token: 0x17000A53 RID: 2643
			// (set) Token: 0x06003963 RID: 14691 RVA: 0x0015203F File Offset: 0x0015043F
			public bool Unlock
			{
				set
				{
					this.imageLock.enabled = !value;
				}
			}

			// Token: 0x06003964 RID: 14692 RVA: 0x00152050 File Offset: 0x00150450
			public void Init()
			{
				if (this.isInit)
				{
					return;
				}
				this.button.OnPointerEnterAsObservable().TakeUntilDestroy(this.imageCursor).Subscribe(delegate(PointerEventData _)
				{
					this.imageCursor.enabled = true;
				}).AddTo(this.gameObject);
				this.button.OnPointerExitAsObservable().TakeUntilDestroy(this.imageCursor).Subscribe(delegate(PointerEventData _)
				{
					this.imageCursor.enabled = false;
				}).AddTo(this.gameObject);
				this.isInit = true;
			}

			// Token: 0x04003907 RID: 14599
			public GameObject gameObject;

			// Token: 0x04003908 RID: 14600
			public RawImage rawImage;

			// Token: 0x04003909 RID: 14601
			public Button button;

			// Token: 0x0400390A RID: 14602
			public Image imageSelect;

			// Token: 0x0400390B RID: 14603
			public Toggle toggleSelect;

			// Token: 0x0400390C RID: 14604
			public Image imageLock;

			// Token: 0x0400390D RID: 14605
			public Image imageCursor;

			// Token: 0x0400390E RID: 14606
			private bool isInit;
		}
	}
}
