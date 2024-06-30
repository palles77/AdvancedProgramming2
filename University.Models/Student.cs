using System;
using System.Collections.Generic;

namespace University.Models
{
    public class Student
    {
        public long StudentId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty ;
        public string Place_of_Birth { get; set; } = string.Empty;
        public string Addres_line_1 { get; set; } = string.Empty;
        public string Postal_Code {  get; set; } = string.Empty;
        public string PESEL { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; } = null;
 
        public virtual ICollection<Subject>? Subjects { get; set; } = null;


    }
}
