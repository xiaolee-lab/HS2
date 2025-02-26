using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ConfigScene
{
	// Token: 0x02000868 RID: 2152
	public class SoundData
	{
		// Token: 0x060036DE RID: 14046 RVA: 0x0014599B File Offset: 0x00143D9B
		public SoundData()
		{
			this._volume = 100;
			this._munte = false;
		}

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x060036DF RID: 14047 RVA: 0x001459B4 File Offset: 0x00143DB4
		// (remove) Token: 0x060036E0 RID: 14048 RVA: 0x001459EC File Offset: 0x00143DEC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<SoundData> ChangeEvent;

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x00145A22 File Offset: 0x00143E22
		// (set) Token: 0x060036E2 RID: 14050 RVA: 0x00145A2C File Offset: 0x00143E2C
		public int Volume
		{
			get
			{
				return this._volume;
			}
			set
			{
				bool flag = this._volume != value;
				this._volume = value;
				if (flag && !this.ChangeEvent.IsNullOrEmpty())
				{
					this.ChangeEvent(this);
				}
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x00145A6F File Offset: 0x00143E6F
		// (set) Token: 0x060036E4 RID: 14052 RVA: 0x00145A78 File Offset: 0x00143E78
		public bool Mute
		{
			get
			{
				return this._munte;
			}
			set
			{
				bool flag = this._munte != value;
				this._munte = value;
				if (flag && !this.ChangeEvent.IsNullOrEmpty())
				{
					this.ChangeEvent(this);
				}
			}
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x00145ABB File Offset: 0x00143EBB
		public float GetVolume()
		{
			return (!this.Mute) ? ((float)this.Volume * 0.01f) : 0f;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x00145ADF File Offset: 0x00143EDF
		public static implicit operator string(SoundData _sd)
		{
			return string.Format("Volume[{0}] : Mute[{1}]", _sd.Volume, _sd.Mute);
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x00145B01 File Offset: 0x00143F01
		public void Refresh()
		{
			if (this.ChangeEvent != null)
			{
				this.ChangeEvent(this);
			}
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x00145B1C File Offset: 0x00143F1C
		public void Parse(string _str)
		{
			Match match = Regex.Match(_str, "Volume\\[([0-9]*)\\] : Mute\\[([a-zA-Z]*)\\]");
			if (match.Success)
			{
				this.Volume = int.Parse(match.Groups[1].Value);
				this.Mute = bool.Parse(match.Groups[2].Value);
			}
		}

		// Token: 0x0400377B RID: 14203
		private int _volume;

		// Token: 0x0400377C RID: 14204
		private bool _munte;
	}
}
