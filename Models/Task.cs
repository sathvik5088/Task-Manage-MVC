using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPP.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        public string task_Name { get; set; }


        public string description { get; set; }

        public string is_Completed { get; set; }

        [DataType(DataType.Date)]
        public DateTime due_Date { get; set; }
    }
}
