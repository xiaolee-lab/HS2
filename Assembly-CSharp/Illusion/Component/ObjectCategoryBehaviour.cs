using System;
using System.Collections.Generic;
using UnityEngine;

namespace Illusion.Component
{
	// Token: 0x0200102B RID: 4139
	public class ObjectCategoryBehaviour : MonoBehaviour
	{
		// Token: 0x06008AB0 RID: 35504 RVA: 0x003A4AE1 File Offset: 0x003A2EE1
		public GameObject GetObject(int _array)
		{
			if (this.lstObj.Count <= _array)
			{
				return null;
			}
			return this.lstObj[_array];
		}

		// Token: 0x06008AB1 RID: 35505 RVA: 0x003A4B04 File Offset: 0x003A2F04
		public int GetAllEnable()
		{
			int num = 0;
			for (int i = 0; i < this.lstObj.Count; i++)
			{
				if (!(this.lstObj[i] == null))
				{
					if (this.lstObj[i].activeSelf)
					{
						num++;
					}
				}
			}
			return (num != this.lstObj.Count) ? ((num != 0) ? 2 : 0) : 1;
		}

		// Token: 0x06008AB2 RID: 35506 RVA: 0x003A4B89 File Offset: 0x003A2F89
		public int GetCount()
		{
			return this.lstObj.Count;
		}

		// Token: 0x06008AB3 RID: 35507 RVA: 0x003A4B96 File Offset: 0x003A2F96
		public bool GetActive(int _array)
		{
			return this.lstObj.Count > _array && !(this.lstObj[_array] == null) && this.lstObj[_array].activeSelf;
		}

		// Token: 0x06008AB4 RID: 35508 RVA: 0x003A4BD8 File Offset: 0x003A2FD8
		public void SetActive(bool _active, int _array = -1)
		{
			if (_array < 0)
			{
				for (int i = 0; i < this.lstObj.Count; i++)
				{
					if (!(this.lstObj[i] == null))
					{
						if (this.lstObj[i].activeSelf != _active)
						{
							this.lstObj[i].gameObject.SetActive(_active);
						}
					}
				}
			}
			else if (this.lstObj.Count > _array && this.lstObj[_array] && this.lstObj[_array].activeSelf != _active)
			{
				this.lstObj[_array].gameObject.SetActive(_active);
			}
		}

		// Token: 0x06008AB5 RID: 35509 RVA: 0x003A4CAC File Offset: 0x003A30AC
		public void SetActiveToggle(int _array)
		{
			for (int i = 0; i < this.lstObj.Count; i++)
			{
				if (!(this.lstObj[i] == null))
				{
					this.lstObj[i].gameObject.SetActive(_array == i);
				}
			}
		}

		// Token: 0x06008AB6 RID: 35510 RVA: 0x003A4D0B File Offset: 0x003A310B
		public bool IsEmpty(int _array)
		{
			return this.lstObj.Count <= _array || this.lstObj[_array] == null;
		}

		// Token: 0x04007120 RID: 28960
		public List<GameObject> lstObj;
	}
}
