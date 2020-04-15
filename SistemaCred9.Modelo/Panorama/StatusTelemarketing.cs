namespace SistemaCred9.Modelo.Panorama
{
    public class StatusTelemarketing
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Data { get; set; }
        public Cliente Cliente { get; set; }

        #region Contantes

        public const int FALECIDO = 5;
        public const int ACIMA_DA_IDADE = 12;
        public const int TEL_INCORRETO = 16;
        public const int SEM_INTERESSE_OUVIU = 18;
        public const int NAO_ATENDE = 30;
        public const int NAO_COBRE = 7;
        public const int SEM_INTERESSE_NAO_QUIS = 124;
        public const int MUDO = 145;
        public const int CAIXA_POSTAL = 146;
        public const int DIFICIL_LOCALIZACAO = 148;
        public const int LIG_CHAMANDO = 152;
        public const int JA_FEZ = 153;
        public const int TURNO_MANHA = 160;
        public const int TURNO_TARDE = 161;
        public const int AGENDAMENTO = 162;
        public const int FECHADO = 211;

        #endregion
    }
}
