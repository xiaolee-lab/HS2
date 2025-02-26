using System;
using AIChara;
using Manager;

namespace Studio
{
	// Token: 0x02001216 RID: 4630
	public class OCICharMale : OCIChar
	{
		// Token: 0x1700203A RID: 8250
		// (get) Token: 0x06009804 RID: 38916 RVA: 0x003EBBB9 File Offset: 0x003E9FB9
		public ChaControl male
		{
			get
			{
				return this.charInfo;
			}
		}

		// Token: 0x06009805 RID: 38917 RVA: 0x003EBBC1 File Offset: 0x003E9FC1
		public override void OnDelete()
		{
			base.OnDelete();
			Singleton<Character>.Instance.DeleteChara(this.male, false);
		}

		// Token: 0x06009806 RID: 38918 RVA: 0x003EBBDB File Offset: 0x003E9FDB
		public override void SetClothesStateAll(int _state)
		{
			base.SetClothesStateAll(_state);
			this.male.SetClothesStateAll((byte)_state);
		}

		// Token: 0x06009807 RID: 38919 RVA: 0x003EBBF1 File Offset: 0x003E9FF1
		public override void ChangeChara(string _path)
		{
			base.ChangeChara(_path);
		}

		// Token: 0x06009808 RID: 38920 RVA: 0x003EBBFA File Offset: 0x003E9FFA
		public override void LoadClothesFile(string _path)
		{
			base.LoadClothesFile(_path);
		}
	}
}
