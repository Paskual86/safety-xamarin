using Newtonsoft.Json;

namespace SafetyBP.Dtos.Requests
{
    public class SafetySpontaneousDiversionDetailRequestDto
    {
        [JsonProperty("motivo")]
        public string Reason { get; set; }
        [JsonProperty("sector")]
        public int SectorId { get; set; }
        [JsonProperty("foto")]
        public string Photo { get; set; }
        [JsonProperty("comentario")]
        public string Comment { get; set; }
        [JsonProperty("riesgo")]
        public byte Risk { get; set; }
    }
}
