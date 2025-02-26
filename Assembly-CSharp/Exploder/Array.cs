using System;

namespace Exploder
{
	// Token: 0x02000396 RID: 918
	public class Array<T>
	{
		// Token: 0x06001036 RID: 4150 RVA: 0x0005AB9D File Offset: 0x00058F9D
		public Array(int size)
		{
			this.array = new T[size];
			this.size = size;
			this.index = 0;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0005ABBF File Offset: 0x00058FBF
		public void Initialize(int newSize)
		{
			if (newSize > this.size)
			{
				this.array = new T[newSize];
				this.size = newSize;
			}
			this.Clear();
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0005ABE6 File Offset: 0x00058FE6
		public int Count
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170000E3 RID: 227
		public T this[int key]
		{
			get
			{
				return this.array[key];
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0005ABFC File Offset: 0x00058FFC
		public void Clear()
		{
			for (int i = 0; i < this.index; i++)
			{
				this.array[i] = default(T);
			}
			this.index = 0;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0005AC3C File Offset: 0x0005903C
		public void Add(T data)
		{
			this.array[this.index++] = data;
			if (this.index >= this.size)
			{
				T[] array = new T[this.size * 2];
				for (int i = 0; i < this.size; i++)
				{
					array[i] = this.array[i];
				}
				this.array = array;
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0005ACB8 File Offset: 0x000590B8
		public void Reverse()
		{
			for (int i = 0; i < this.index / 2; i++)
			{
				T t = this.array[i];
				this.array[i] = this.array[this.index - i - 1];
				this.array[this.index - i - 1] = t;
			}
		}

		// Token: 0x040011F3 RID: 4595
		private T[] array;

		// Token: 0x040011F4 RID: 4596
		private int size;

		// Token: 0x040011F5 RID: 4597
		private int index;
	}
}
