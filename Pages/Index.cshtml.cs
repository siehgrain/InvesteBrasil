using Microsoft.AspNetCore.Mvc;
using YahooFinanceApi;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;

namespace InvesteBrasil.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        class StockData
        {
            public async Task<string> getStockData(string simbolo, DateTime dataInicio,DateTime dataFinal)
            {
                try
                {
                    var dadosHistoricos = await Yahoo.GetHistoricalAsync(simbolo, dataInicio, dataFinal);
                    var seguranca = await Yahoo.Symbols(simbolo).Fields(Field.LongName).QueryAsync();
                    var ticker = seguranca[simbolo];
                    var nomeEmpresa = ticker[Field.LongName];

                    for(int i = 0;i < dadosHistoricos.Count;i++)
                    {
                        Console.WriteLine(nomeEmpresa + "Preço No Fechamento do Mercado" + Math.Round(dadosHistoricos[i].Close));
                    }
                }
                catch
                {

                }
                return 1;
            }
            public int Id { get; set; }
        }
    }
}