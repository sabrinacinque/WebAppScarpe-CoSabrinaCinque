namespace WebAppScarpe_CoSabrinaCinque.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CoverImagePath { get; set; }
        public string AdditionalImagePath1 { get; set; }
        public string AdditionalImagePath2 { get; set; }
    }
}
