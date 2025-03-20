
using System.Net;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
namespace cSharpFundamentalsGetJSON
{

    public class Item
    {
        public DateTime data;
        public float open;
        public float high;
        public float low;
        public float close;
        public int  volume;
    }
    public class MyData
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Skills { get; set; }
    }


    class GetJSON
    {
        public void ProduceJSONFile(string strKeyAlphaVantage, string strURLAlphaVantage)
        {
            var strCmdText = "/C curl \"" + strURLAlphaVantage + "\"" + " > pippo.txt";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        public string LeggiJSON (string cartella, string nomeFile)
        {
            // Open the text file using a stream reader.

            using StreamReader reader = new(cartella + nomeFile);

            // Read the stream as a string.
            return reader.ReadToEnd();

        }

        static void Main(string[] args)
        {
            //MainAsync(args);

            var strStockSymbol = "IBM";
            //var strapiToken = "";
            //var strURLYahoo = "--request GET --url 'https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-timeseries?symbol=IBM&region=US' --header 'x-rapidapi-host: apidojo-yahoo-finance-v1.p.rapidapi.com' --header 'x-rapidapi-key: 9844109a8dmsh61f3fd03bb1ee02p124780jsn568f47c4dbf2'";
            //var strURLKeyCloud  = "https://cloud.iexapis.com/stable/stock/" + strStockSymbol + "/quote?token=" + strapiToken;
            //var strUrlProfitcomToken = "0699109fe0bd40259c84d34ad29dee8c";
            //var strUrlProfitCom = "https://api.profit.com/data-api/reference/exchanges?token=0699109fe0bd40259c84d34ad29dee8c&type=stocks&code=US&name=USA%20Stocks";
            var strKeyAlphaVantage = "LNT25N3GIPC6K3DZ";
            //var function = "TIME_SERIES_INTRADAY";

            var strURLAlphaVantageIntraday = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=" + strStockSymbol + "&interval=5min&apikey=" + strKeyAlphaVantage;
            GetJSON getJSON = new GetJSON();

            getJSON.ProduceJSONFile(strKeyAlphaVantage, strURLAlphaVantageIntraday);


            //var strCmdText = "/C curl \"" + strURLAlphaVantage + "\"" + " > pippo.txt";
            //System.Diagnostics.Process.Start("CMD.exe", strCmdText);

            // Open the text file using a stream reader.
            //using StreamReader reader = new("C:\\Users\\grippst1\\source\\repos\\ConsoleAppGetJSON\\ConsoleAppGetJSON\\bin\\Debug\\net8.0\\pippo.txt");

            // Read the stream as a string.
            //string text = reader.ReadToEnd();

            string cartella = "C:\\Users\\grippst1\\source\\repos\\ConsoleAppGetJSON\\ConsoleAppGetJSON\\bin\\Debug\\net8.0\\";
            string nomeFile = "pippo.txt";

            string nomeFileTest = "test.txt";

            MyData data = new MyData
            {
                Name = "Jane Doe",
                Age = 25,
                Skills = new List<string> { "Java", "C++" }
            };
            //TextWriter myTextWriter = System.IO.File.CreateText(cartella + nomeFileTest);
            //string jsonOutput = JsonSerializer.Serialize(myTextWriter, data);



            string text = getJSON.LeggiJSON(cartella, nomeFileTest);

            string? miaStringa = JsonConvert.DeserializeObject<string>(text);
    

            //List<Item> items = JsonConvert.DeserializeObject<List<Item>>(text);
            //JsonTextReader reader = new JsonTextReader(new StreamReader(text));

            // Write the text to the
            // console.
            Console.WriteLine(text);

        }
        static async Task MainAsync(string[] args) { 
            //string json;
            //using (var client = new WebClient())
            //{
            //client.Credentials = new NetworkCredential("stefano.grippa@reti.it", "mypassword");
            //json = client.DownloadString(
            //"https://reqbin.com/echo");


            // https://stackoverflow.com/questions/7929013/making-a-curl-call-in-c-sharp
            var client = new HttpClient();

            // Create the HttpContent for the form to be posted.
            var requestContent = new FormUrlEncodedContent(new[] {
    new KeyValuePair<string, string>("text", "This is a block of text"),
});

            // Get the response.
            HttpResponseMessage response = await client.PostAsync(
                "http://api.repustate.com/v2/demokey/score.json",
                requestContent);

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }

        }
    }
    
}

