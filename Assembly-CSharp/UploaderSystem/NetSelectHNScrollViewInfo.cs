using System;
using UnityEngine;
using UnityEngine.UI;

namespace UploaderSystem
{
	// Token: 0x0200101A RID: 4122
	[DisallowMultipleComponent]
	public class NetSelectHNScrollViewInfo : MonoBehaviour
	{
		// Token: 0x06008A72 RID: 35442 RVA: 0x003A3ACC File Offset: 0x003A1ECC
		public void SetData(NetworkInfo.SelectHNInfo _data, Action<bool> _onValueChange)
		{
			this.row.tglItem.onValueChanged.RemoveAllListeners();
			this.row.tglItem.onValueChanged.AddListener(delegate(bool _isOn)
			{
				_onValueChange(_isOn);
			});
			this.row.text.text = _data.drawname;
			this.row.info = _data;
		}

		// Token: 0x06008A73 RID: 35443 RVA: 0x003A3B3E File Offset: 0x003A1F3E
		public void SetToggleON(bool _isOn)
		{
			this.row.tglItem.isOn = _isOn;
		}

		// Token: 0x06008A74 RID: 35444 RVA: 0x003A3B51 File Offset: 0x003A1F51
		public NetworkInfo.SelectHNInfo GetListInfo()
		{
			return this.row.info;
		}

		// Token: 0x040070BA RID: 28858
		[SerializeField]
		private NetSelectHNScrollViewInfo.RowInfo row;

		// Token: 0x0200101B RID: 4123
		[Serializable]
		public class RowInfo
		{
			// Token: 0x040070BB RID: 28859
			public Toggle tglItem;

			// Token: 0x040070BC RID: 28860
			public Text text;

			// Token: 0x040070BD RID: 28861
			public NetworkInfo.SelectHNInfo info;
		}
	}
}
