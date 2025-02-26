using System;

namespace Exploder
{
	// Token: 0x02000397 RID: 919
	internal class ArrayDictionary<T>
	{
		// Token: 0x0600103D RID: 4157 RVA: 0x0005AD22 File Offset: 0x00059122
		public ArrayDictionary(int size)
		{
			this.dictionary = new ArrayDictionary<T>.DicItem[size];
			this.Size = size;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0005AD3D File Offset: 0x0005913D
		public bool ContainsKey(int key)
		{
			return key < this.Size && this.dictionary[key].valid;
		}

		// Token: 0x170000E4 RID: 228
		public T this[int key]
		{
			get
			{
				return this.dictionary[key].data;
			}
			set
			{
				this.dictionary[key].data = value;
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0005AD88 File Offset: 0x00059188
		public void Clear()
		{
			for (int i = 0; i < this.Size; i++)
			{
				this.dictionary[i].data = default(T);
				this.dictionary[i].valid = false;
			}
			this.Count = 0;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0005ADDF File Offset: 0x000591DF
		public void Add(int key, T data)
		{
			this.dictionary[key].valid = true;
			this.dictionary[key].data = data;
			this.Count++;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0005AE13 File Offset: 0x00059213
		public void Remove(int key)
		{
			this.dictionary[key].valid = false;
			this.Count--;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0005AE38 File Offset: 0x00059238
		public T[] ToArray()
		{
			T[] array = new T[this.Count];
			int num = 0;
			for (int i = 0; i < this.Size; i++)
			{
				if (this.dictionary[i].valid)
				{
					array[num++] = this.dictionary[i].data;
					if (num == this.Count)
					{
						return array;
					}
				}
			}
			return null;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0005AEAC File Offset: 0x000592AC
		public bool TryGetValue(int key, out T value)
		{
			ArrayDictionary<T>.DicItem dicItem = this.dictionary[key];
			if (dicItem.valid)
			{
				value = dicItem.data;
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0005AEF8 File Offset: 0x000592F8
		public T GetFirstValue()
		{
			for (int i = 0; i < this.Size; i++)
			{
				ArrayDictionary<T>.DicItem dicItem = this.dictionary[i];
				if (dicItem.valid)
				{
					return dicItem.data;
				}
			}
			return default(T);
		}

		// Token: 0x040011F6 RID: 4598
		public int Count;

		// Token: 0x040011F7 RID: 4599
		public int Size;

		// Token: 0x040011F8 RID: 4600
		private readonly ArrayDictionary<T>.DicItem[] dictionary;

		// Token: 0x02000398 RID: 920
		private struct DicItem
		{
			// Token: 0x040011F9 RID: 4601
			public T data;

			// Token: 0x040011FA RID: 4602
			public bool valid;
		}
	}
}
