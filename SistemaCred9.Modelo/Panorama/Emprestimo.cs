using System;

namespace SistemaCred9.Modelo.Panorama
{
    public class Emprestimo
    {
        private int _qtdAberto;


        public int Id { get; set; }
        public int BeneficioId { get; set; }
        public string Banco { get; set; }
        public int Prazo { get; set; }
        public int ParcelasEmAberto 
        { 
            get
            {
                if (_qtdAberto == 0 && Prazo > 0)
                {
                    var dataAtual = DateTime.Now;

                    if (DataPrimeiroVencimento.HasValue)
                    {
                        var qtdPagas = ((dataAtual.Year - DataPrimeiroVencimento.Value.Year) * 12) + dataAtual.Month - DataPrimeiroVencimento.Value.Month;
                        _qtdAberto = (Prazo - qtdPagas);
                    }
                    else if (DataLiberadoEm.HasValue)
                    {
                        var qtdPagas = ((dataAtual.Year - DataLiberadoEm.Value.Year) * 12) + dataAtual.Month - DataLiberadoEm.Value.Month;
                        _qtdAberto = (Prazo - qtdPagas);
                    }
                }

                return _qtdAberto;
            }
            set
            {
                _qtdAberto = value;
            } 
        }
        public double ValorParcela { get; set; }
        public double Saldo { get; set; }
        public DateTime? DataPrimeiroVencimento { get; set; }
        public DateTime? DataLiberadoEm { get; set; }
    }
}
