using System;

namespace AIProject.Animal
{
	// Token: 0x02000B98 RID: 2968
	public struct OneState
	{
		// Token: 0x0600589A RID: 22682 RVA: 0x0026084E File Offset: 0x0025EC4E
		public OneState(AnimalState _state, float _proportion)
		{
			this.state = _state;
			this.proportion = _proportion;
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x0026085E File Offset: 0x0025EC5E
		public bool Equal(OneState _state)
		{
			return this.state == _state.state && this.proportion == _state.proportion;
		}

		// Token: 0x04005138 RID: 20792
		public AnimalState state;

		// Token: 0x04005139 RID: 20793
		public float proportion;
	}
}
