using System;
using Illusion.Extensions;
using Manager;
using SceneAssist;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E5C RID: 3676
	[DisallowMultipleComponent]
	public class GameCoordinateFileInfoComponent : MonoBehaviour
	{
		// Token: 0x06007452 RID: 29778 RVA: 0x003178B0 File Offset: 0x00315CB0
		public void SetData(int index, GameCoordinateFileInfo info, Action<bool> onClickAction)
		{
			bool flag = info != null;
			GameCoordinateFileInfoComponent.RowInfo rowInfo = this._rows[index];
			rowInfo.toggle.gameObject.SetActiveIfDifferent(flag);
			rowInfo.toggle.onValueChanged.RemoveAllListeners();
			if (!flag)
			{
				return;
			}
			rowInfo.toggle.onValueChanged.RemoveAllListeners();
			rowInfo.toggle.onValueChanged.AddListener(delegate(bool isOn)
			{
				Action<bool> onClickAction2 = onClickAction;
				if (onClickAction2 != null)
				{
					onClickAction2(isOn);
				}
			});
			rowInfo.toggle.SetIsOnWithoutCallback(false);
			rowInfo.toggle.interactable = !info.IsInSaveData;
			rowInfo.imageThumb.texture = PngAssist.ChangeTextureFromByte(info.PngData ?? PngFile.LoadPngBytes(info.FullPath), 0, 0, TextureFormat.ARGB32, false);
			rowInfo.objSelect.SetActiveIfDifferent(false);
			int sel = index;
			rowInfo.pointerAction.listActionEnter.Add(delegate
			{
				if (!this._rows[sel].toggle.interactable)
				{
					return;
				}
				this._rows[sel].objSelect.SetActiveIfDifferent(true);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			rowInfo.pointerAction.listActionExit.Add(delegate
			{
				this._rows[sel].objSelect.SetActiveIfDifferent(false);
			});
			rowInfo.info = info;
		}

		// Token: 0x06007453 RID: 29779 RVA: 0x003179D6 File Offset: 0x00315DD6
		public void SetToggleOn(int index, bool isOn)
		{
			this._rows[index].toggle.SetIsOnWithoutCallback(isOn);
		}

		// Token: 0x06007454 RID: 29780 RVA: 0x003179EB File Offset: 0x00315DEB
		public GameCoordinateFileInfo GetListInfo(int index)
		{
			return this._rows[index].info;
		}

		// Token: 0x04005F0D RID: 24333
		[SerializeField]
		private GameCoordinateFileInfoComponent.RowInfo[] _rows;

		// Token: 0x02000E5D RID: 3677
		[Serializable]
		public class RowInfo
		{
			// Token: 0x04005F0E RID: 24334
			public GameCoordinateFileInfo info;

			// Token: 0x04005F0F RID: 24335
			public Toggle toggle;

			// Token: 0x04005F10 RID: 24336
			public RawImage imageThumb;

			// Token: 0x04005F11 RID: 24337
			public GameObject objSelect;

			// Token: 0x04005F12 RID: 24338
			public PointerEnterExitAction pointerAction;
		}
	}
}
