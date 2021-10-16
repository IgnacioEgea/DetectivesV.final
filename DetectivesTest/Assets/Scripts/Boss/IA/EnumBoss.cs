public class EnumBoss
{
    public static class BossIA
    {

        public enum BossGeneralStates
        {
            Pre,
            Battle,
            Post,
        }
        public enum BossStates
        {
            Return,
            Idle,
            Dialogue,
            Chase,
            Weak,
            Attack,
            Repositioning,
            Search,
            Death,
            Default,
        }

        public enum BossStages
        {
            Normal,
            HalfLife,
            LowLife,
        }

        public enum EstadosAtaque
        {
            ListoParaAtacar,
            Ejecutandoataque,
            Ejecutandoataque2,
            PreparandoSiguienteAtaque,
        }

    }
}
