using System;
using System.Diagnostics;
using ConfigScene;
using Illusion.Elements.Xml;
using UnityEngine;

namespace Manager
{
	// Token: 0x020008DD RID: 2269
	public sealed class Config : Singleton<Config>
	{
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x00161E2F File Offset: 0x0016022F
		// (set) Token: 0x06003C30 RID: 15408 RVA: 0x00161E36 File Offset: 0x00160236
		public static bool initialized { get; private set; }

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x00161E3E File Offset: 0x0016023E
		// (set) Token: 0x06003C32 RID: 15410 RVA: 0x00161E45 File Offset: 0x00160245
		public static ActionSystem ActData { get; private set; }

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x00161E4D File Offset: 0x0016024D
		// (set) Token: 0x06003C34 RID: 15412 RVA: 0x00161E54 File Offset: 0x00160254
		public static GraphicSystem GraphicData { get; private set; }

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x00161E5C File Offset: 0x0016025C
		// (set) Token: 0x06003C36 RID: 15414 RVA: 0x00161E63 File Offset: 0x00160263
		public static SoundSystem SoundData { get; private set; }

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x00161E6B File Offset: 0x0016026B
		// (set) Token: 0x06003C38 RID: 15416 RVA: 0x00161E72 File Offset: 0x00160272
		public static GameConfigSystem GameData { get; private set; }

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x00161E7A File Offset: 0x0016027A
		// (set) Token: 0x06003C3A RID: 15418 RVA: 0x00161E81 File Offset: 0x00160281
		public static HSystem HData { get; private set; }

		// Token: 0x06003C3B RID: 15419 RVA: 0x00161E89 File Offset: 0x00160289
		public void Reset()
		{
			if (this.xmlCtrl != null)
			{
				this.xmlCtrl.Init();
			}
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x00161EA1 File Offset: 0x001602A1
		public void Load()
		{
			this.xmlCtrl.Read();
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x00161EAE File Offset: 0x001602AE
		public void Save()
		{
			this.xmlCtrl.Write();
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x00161EBB File Offset: 0x001602BB
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x00161ED4 File Offset: 0x001602D4
		private void Start()
		{
			Config.ActData = new ActionSystem("Action");
			Config.GraphicData = new GraphicSystem("Graphic");
			Config.SoundData = new SoundSystem("Sound");
			Config.GameData = new GameConfigSystem("Game");
			Config.HData = new HSystem("H");
			this.xmlCtrl = new Control("config", "system.xml", "System", new Data[]
			{
				Config.ActData,
				Config.GraphicData,
				Config.SoundData,
				Config.GameData,
				Config.HData
			});
			this.Load();
			Config.initialized = true;
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x00161F80 File Offset: 0x00160380
		[Conditional("__DEBUG_PROC__")]
		private void DBLog(object o)
		{
		}

		// Token: 0x04003AA4 RID: 15012
		private const string UserPath = "config";

		// Token: 0x04003AA5 RID: 15013
		private const string FileName = "system.xml";

		// Token: 0x04003AA6 RID: 15014
		private const string RootName = "System";

		// Token: 0x04003AA7 RID: 15015
		private Control xmlCtrl;
	}
}
