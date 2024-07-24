using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MarsPlanetW.Pages
{
    public class IndexModel : PageModel
    {
        public string TerrestrialDate { get; set; }
        public string MinTemp { get; set; }
        public string MaxTemp { get; set; }

        public async Task OnGet()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync("https://api.maas2.apollorion.com/");
                var data = JObject.Parse(response);

                double minTempC = data["min_temp"].Value<double>();
                double maxTempC = data["max_temp"].Value<double>();
                string terrestrialDate = data["terrestrial_date"].Value<string>();

                MinTemp = (minTempC * 9 / 5 + 32).ToString("F1"); 
                MaxTemp = (maxTempC * 9 / 5 + 32).ToString("F1");
                TerrestrialDate = DateTime.Parse(terrestrialDate).ToString("M/d/yyyy"); 
            }
        }
    }
}
