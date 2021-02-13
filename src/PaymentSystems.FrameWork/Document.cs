namespace PaymentSystems.FrameWork
{
    public record Document
    {
        public string Id { get; init; }
        public long?  Position { get; set; }
    }
}