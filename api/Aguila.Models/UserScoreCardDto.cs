namespace Aguila.Models
{
    public class UserScoreCardDto
    {
        public int Id { get; set; }
        public string Descr { get; set; } // Number of Rounds, Total or Category Name
        public decimal value1 { get; set; }
        public decimal value2 { get; set; }
        public decimal value3 { get; set; }
        public decimal value4 { get; set; } //ScoreCard API required to provide the value of Pole 9.
    }
}
