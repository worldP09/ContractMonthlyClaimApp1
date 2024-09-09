namespace CMCS.Models
{
    public class SupportingDocument
    {
        public int SupportingDocumentID { get; set; }
        public string FilePath { get; set; }
        public int ClaimID { get; set; } // Foreign Key

        // Navigation property
        public Claim Claim { get; set; }

    }
}
