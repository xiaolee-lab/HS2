using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B87 RID: 2951
	public class AnimalMarker : MonoBehaviour
	{
		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x060057C9 RID: 22473 RVA: 0x0025DC3F File Offset: 0x0025C03F
		public static Dictionary<int, AnimalBase> AnimalMarkerTable { get; } = new Dictionary<int, AnimalBase>();

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x060057CA RID: 22474 RVA: 0x0025DC46 File Offset: 0x0025C046
		public static List<int> Keys { get; } = new List<int>();

		// Token: 0x060057CB RID: 22475 RVA: 0x0025DC50 File Offset: 0x0025C050
		private void Awake()
		{
			if (!this._animal)
			{
				this._animal = base.GetComponent<AnimalBase>();
			}
			if (!this._animal)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
			this.InstanceID = this._animal.InstanceID;
			this.IsAnimalActive = true;
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x0025DCA8 File Offset: 0x0025C0A8
		private void OnEnable()
		{
			if (this._animal)
			{
				AnimalMarker.AnimalMarkerTable[this.InstanceID] = this._animal;
				if (!AnimalMarker.Keys.Contains(this.InstanceID))
				{
					AnimalMarker.Keys.Add(this.InstanceID);
				}
			}
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x0025DD00 File Offset: 0x0025C100
		private void OnDisable()
		{
			if (this.IsAnimalActive)
			{
				AnimalMarker.AnimalMarkerTable.Remove(this.InstanceID);
				AnimalMarker.Keys.RemoveAll((int x) => x == this.InstanceID);
			}
		}

		// Token: 0x040050BC RID: 20668
		private int InstanceID;

		// Token: 0x040050BD RID: 20669
		private bool IsAnimalActive;

		// Token: 0x040050BE RID: 20670
		private AnimalBase _animal;
	}
}
