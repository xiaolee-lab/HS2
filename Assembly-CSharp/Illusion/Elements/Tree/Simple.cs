using System;
using System.Collections.Generic;
using System.Linq;

namespace Illusion.Elements.Tree
{
	// Token: 0x02001072 RID: 4210
	public class Simple<T>
	{
		// Token: 0x06008D25 RID: 36133 RVA: 0x003B07AB File Offset: 0x003AEBAB
		public Simple(T data, int level = 0)
		{
			this.level = level;
			this.data = data;
			this.children = new List<Simple<T>>();
		}

		// Token: 0x06008D26 RID: 36134 RVA: 0x003B07CC File Offset: 0x003AEBCC
		public void RootAction(Action<Simple<T>> act)
		{
			for (Simple<T> simple = this; simple != null; simple = simple.parent)
			{
				act(simple);
			}
		}

		// Token: 0x06008D27 RID: 36135 RVA: 0x003B07F4 File Offset: 0x003AEBF4
		public bool RootCheck(Func<Simple<T>, bool> func)
		{
			for (Simple<T> simple = this; simple != null; simple = simple.parent)
			{
				if (func(simple))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001ED7 RID: 7895
		// (get) Token: 0x06008D28 RID: 36136 RVA: 0x003B0824 File Offset: 0x003AEC24
		// (set) Token: 0x06008D29 RID: 36137 RVA: 0x003B082C File Offset: 0x003AEC2C
		public int level { get; private set; }

		// Token: 0x17001ED8 RID: 7896
		// (get) Token: 0x06008D2A RID: 36138 RVA: 0x003B0835 File Offset: 0x003AEC35
		// (set) Token: 0x06008D2B RID: 36139 RVA: 0x003B083D File Offset: 0x003AEC3D
		public Simple<T> parent { get; private set; }

		// Token: 0x17001ED9 RID: 7897
		// (get) Token: 0x06008D2C RID: 36140 RVA: 0x003B0846 File Offset: 0x003AEC46
		// (set) Token: 0x06008D2D RID: 36141 RVA: 0x003B084E File Offset: 0x003AEC4E
		public List<Simple<T>> children { get; private set; }

		// Token: 0x17001EDA RID: 7898
		// (get) Token: 0x06008D2E RID: 36142 RVA: 0x003B0857 File Offset: 0x003AEC57
		// (set) Token: 0x06008D2F RID: 36143 RVA: 0x003B085F File Offset: 0x003AEC5F
		public T data { get; private set; }

		// Token: 0x06008D30 RID: 36144 RVA: 0x003B0868 File Offset: 0x003AEC68
		public Simple<T> AddChild(T child)
		{
			Simple<T> simple = new Simple<T>(child, this.level + 1);
			simple.parent = this;
			this.children.Add(simple);
			return simple;
		}

		// Token: 0x06008D31 RID: 36145 RVA: 0x003B0898 File Offset: 0x003AEC98
		public bool RemoveChild(T child)
		{
			return this.children.Remove(this.children.FirstOrDefault(delegate(Simple<T> p)
			{
				T data = p.data;
				return data.Equals(child);
			}));
		}
	}
}
