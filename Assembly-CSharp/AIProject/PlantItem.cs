using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C25 RID: 3109
	public class PlantItem : MonoBehaviour
	{
		// Token: 0x06005FFF RID: 24575 RVA: 0x00287828 File Offset: 0x00285C28
		private void Start()
		{
			foreach (GameObject gameObject in this._states)
			{
				gameObject.SetActive(false);
			}
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x0028785C File Offset: 0x00285C5C
		public void ChangeState(int state)
		{
			for (int i = 0; i < this._states.Length; i++)
			{
				GameObject gameObject = this._states[i];
				bool flag = i == state;
				if (gameObject.activeSelf != flag)
				{
					this._states[i].SetActive(flag);
				}
			}
		}

		// Token: 0x04005567 RID: 21863
		[SerializeField]
		private GameObject[] _states;
	}
}
