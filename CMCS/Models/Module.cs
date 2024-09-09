namespace CMCS.Models
{
    public class Module
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public int HoursTaught { get; set; }
        public int LecturerID { get; set; } // Foreign Key

        // Navigation property
        public Lecturer Lecturer { get; set; }
    }
}
