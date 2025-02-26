using System;
using System.Collections.Generic;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x02000A19 RID: 2585
	public class ObjectCategoryBehaviour : MonoBehaviour
	{
		// Token: 0x06004CE4 RID: 19684 RVA: 0x001D8B5C File Offset: 0x001D6F5C
		public GameObject GetObject(int _array)
		{
			if (this.lstObj.Count <= _array)
			{
				return null;
			}
			return this.lstObj[_array];
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x001D8B80 File Offset: 0x001D6F80
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

		// Token: 0x06004CE6 RID: 19686 RVA: 0x001D8C05 File Offset: 0x001D7005
		public int GetCount()
		{
			return this.lstObj.Count;
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x001D8C12 File Offset: 0x001D7012
		public bool GetActive(int _array)
		{
			return this.lstObj.Count > _array && !(this.lstObj[_array] == null) && this.lstObj[_array].activeSelf;
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x001D8C54 File Offset: 0x001D7054
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

		// Token: 0x06004CE9 RID: 19689 RVA: 0x001D8D28 File Offset: 0x001D7128
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

		// Token: 0x06004CEA RID: 19690 RVA: 0x001D8D87 File Offset: 0x001D7187
		public bool IsEmpty(int _array)
		{
			return this.lstObj.Count <= _array || this.lstObj[_array] == null;
		}

		// Token: 0x04004674 RID: 18036
		public List<GameObject> lstObj;
	}
}
