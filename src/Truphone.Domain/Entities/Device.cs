namespace Truphone.Domain.Entities
{
    public record Device : BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}
