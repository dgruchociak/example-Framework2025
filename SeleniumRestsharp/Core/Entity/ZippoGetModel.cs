using Newtonsoft.Json;

namespace SeleniumAndRestSharp.Core.Entity;

public class ZippoGetModel
{
    [JsonProperty("post code")]
    public string postcode { get; set; }
    public string country { get; set; }

    [JsonProperty("country abbreviation")]
    public string countryabbreviation { get; set; }
    public List<Place> places { get; set; }
    
    public class Place
    {
        [JsonProperty("place name")]
        public string placename { get; set; }
        public string longitude { get; set; }
        public string state { get; set; }

        [JsonProperty("state abbreviation")]
        public string stateabbreviation { get; set; }
        public string latitude { get; set; }
    }
}