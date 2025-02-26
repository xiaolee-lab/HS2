using System;
using System.IO;
using AIProject;
using Illusion.Extensions;
using Manager;
using SceneAssist;
using UnityEngine;
using UnityEngine.UI;

namespace GameLoadCharaFileSystem
{
	// Token: 0x02000876 RID: 2166
	[DisallowMultipleComponent]
	public class GameCharaFileInfoComponent : MonoBehaviour
	{
		// Token: 0x06003725 RID: 14117 RVA: 0x00146AD8 File Offset: 0x00144ED8
		public void SetData(int _index, GameCharaFileInfo _info, Action<bool> _onClickAction)
		{
			bool flag = _info != null;
			if (flag)
			{
				flag = (_info.FullPath.IsNullOrEmpty() || (!_info.FullPath.IsNullOrEmpty() && File.Exists(_info.FullPath)) || _info.pngData != null);
			}
			this.rows[_index].tgl.gameObject.SetActiveIfDifferent(flag);
			this.rows[_index].tgl.onValueChanged.RemoveAllListeners();
			if (!flag)
			{
				return;
			}
			this.rows[_index].tgl.onValueChanged.RemoveAllListeners();
			this.rows[_index].tgl.onValueChanged.AddListener(delegate(bool _isOn)
			{
				_onClickAction(_isOn);
			});
			this.rows[_index].tgl.SetIsOnWithoutCallback(false);
			this.rows[_index].tgl.interactable = !_info.isInSaveData;
			this.rows[_index].objEntry.SetActiveIfDifferent(_info.isInSaveData);
			this.rows[_index].objNurturing.SetActiveIfDifferent(_info.gameRegistration);
			if (this.rows[_index].imgThumb.texture != null && this.rows[_index].imgThumb.texture != this.texEmpty)
			{
				UnityEngine.Object.Destroy(this.rows[_index].imgThumb.texture);
				this.rows[_index].imgThumb.texture = null;
			}
			if (_info.pngData != null || !_info.FullPath.IsNullOrEmpty())
			{
				this.rows[_index].imgThumb.texture = PngAssist.ChangeTextureFromByte(_info.pngData ?? PngFile.LoadPngBytes(_info.FullPath), 0, 0, TextureFormat.ARGB32, false);
			}
			else
			{
				this.rows[_index].imgThumb.texture = this.texEmpty;
			}
			this.rows[_index].objSelect.SetActiveIfDifferent(false);
			int sel = _index;
			this.rows[_index].pointerAction.listActionEnter.Add(delegate
			{
				if (!this.rows[sel].tgl.interactable)
				{
					return;
				}
				this.rows[sel].objSelect.SetActiveIfDifferent(true);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			this.rows[_index].pointerAction.listActionExit.Add(delegate
			{
				this.rows[sel].objSelect.SetActiveIfDifferent(false);
			});
			this.rows[_index].info = _info;
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x00146D5B File Offset: 0x0014515B
		public void SetToggleON(int _index, bool _isOn)
		{
			this.rows[_index].tgl.SetIsOnWithoutCallback(_isOn);
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x00146D70 File Offset: 0x00145170
		public GameCharaFileInfo GetListInfo(int _index)
		{
			return this.rows[_index].info;
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x00146D7F File Offset: 0x0014517F
		public void SetListInfo(int _index, GameCharaFileInfo _info)
		{
			this.rows[_index].info = _info;
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x00146D8F File Offset: 0x0014518F
		public void Disable(bool disable)
		{
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x00146D91 File Offset: 0x00145191
		public void Disvisible(bool disvisible)
		{
		}

		// Token: 0x040037C6 RID: 14278
		[SerializeField]
		private GameCharaFileInfoComponent.RowInfo[] rows;

		// Token: 0x040037C7 RID: 14279
		[SerializeField]
		private Texture2D texEmpty;

		// Token: 0x02000877 RID: 2167
		[Serializable]
		public class RowInfo
		{
			// Token: 0x040037C8 RID: 14280
			public GameCharaFileInfo info;

			// Token: 0x040037C9 RID: 14281
			public Toggle tgl;

			// Token: 0x040037CA RID: 14282
			public RawImage imgThumb;

			// Token: 0x040037CB RID: 14283
			public GameObject objSelect;

			// Token: 0x040037CC RID: 14284
			public PointerEnterExitAction pointerAction;

			// Token: 0x040037CD RID: 14285
			public GameObject objEntry;

			// Token: 0x040037CE RID: 14286
			public GameObject objNurturing;
		}
	}
}
