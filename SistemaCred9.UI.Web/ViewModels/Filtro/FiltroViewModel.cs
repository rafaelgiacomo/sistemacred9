using SistemaCred9.Modelo;
using SistemaCred9.Modelo.Panorama;
using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaCred9.Web.UI.ViewModels.Filtro
{
    public class FiltroViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Código Agrupamento")]
        public int CodAgrupamento { get; set; }

        [Required]
        [Display(Name = "Limite Qtd Registros")]
        [DataType(DataType.Text)]
        public long? LimiteRegistros { get; set; }

        [Required]
        [Display(Name = "Data Nascimento Min")]
        [DataType(DataType.Text)]
        public string MinDataNascimento { get; set; }

        public string AgrupamentoDescricao { get; set; }

        [Required]
        public string[] BancosSelecionados { get; set; }
        public string[] EspeciesSelecionadas { get; set; }
        public List<FiltroBanco> BancosDisponiveis { get; set; }
        public List<FiltroEspecie> EspeciesDisponiveis { get; set; }
        public List<Agrupamento> ListaAgrupamentos { get; set; }

        public FiltroViewModel() : base()
        {
            BancosDisponiveis = new List<FiltroBanco>();
            EspeciesDisponiveis = new List<FiltroEspecie>();
            ListaAgrupamentos = new List<Agrupamento>();
        }

        public FiltroViewModel(List<FiltroBanco> bancos, List<FiltroEspecie> especies) : base()
        {
            BancosDisponiveis = bancos;
            EspeciesDisponiveis = especies;
        }
    }
}