using System;
using IllusionUtility.GetUtility;
using UnityEngine;

namespace AIProject.MiniGames.Fishing
{
	// Token: 0x02000F32 RID: 3890
	public class FishingRodInfo
	{
		// Token: 0x0600807C RID: 32892 RVA: 0x003674A4 File Offset: 0x003658A4
		public FishingRodInfo(GameObject _rod, RuntimeAnimatorController _controller, string _tipName)
		{
			this.Rod = _rod;
			this.AnimController = _controller;
			this.TipName = _tipName;
		}

		// Token: 0x170019CE RID: 6606
		// (get) Token: 0x0600807D RID: 32893 RVA: 0x003674C1 File Offset: 0x003658C1
		// (set) Token: 0x0600807E RID: 32894 RVA: 0x003674C9 File Offset: 0x003658C9
		public GameObject Rod { get; private set; }

		// Token: 0x170019CF RID: 6607
		// (get) Token: 0x0600807F RID: 32895 RVA: 0x003674D2 File Offset: 0x003658D2
		// (set) Token: 0x06008080 RID: 32896 RVA: 0x003674DA File Offset: 0x003658DA
		public RuntimeAnimatorController AnimController { get; private set; }

		// Token: 0x170019D0 RID: 6608
		// (get) Token: 0x06008081 RID: 32897 RVA: 0x003674E3 File Offset: 0x003658E3
		// (set) Token: 0x06008082 RID: 32898 RVA: 0x003674EB File Offset: 0x003658EB
		public string TipName { get; private set; }

		// Token: 0x06008083 RID: 32899 RVA: 0x003674F4 File Offset: 0x003658F4
		public GameObject GetObj(string _name)
		{
			GameObject rod = this.Rod;
			return (rod != null) ? rod.transform.FindLoop(_name) : null;
		}
	}
}
