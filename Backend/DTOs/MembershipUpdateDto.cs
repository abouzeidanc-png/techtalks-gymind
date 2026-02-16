namespace GYMIND.API.DTOs
{
    public record MembershipUpdateDto
    {
        public bool IsMember { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool MarkAsRemoved { get; set; }

    }  
    
}

