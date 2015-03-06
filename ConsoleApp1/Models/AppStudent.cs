namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("app_student")]
    public class AppStudent
    {
        public AppStudent()
        {
            this.CreateTime = DateTime.Now;
            this.LastupdateTime = DateTime.Now;
        }

        [Column("student_sno", Order=1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sno { get; set; }

        [Column("class_id", Order = 2), Key]
        [MaxLength(32)]
        [MinLength(3)]
        [Required]
        public string ClassID { get; set; }

        [Column("student_id", Order = 3), Key]
        [MaxLength(32)]
        [MinLength(1)]
        [Required]
        public string ID { get; set; }

        [Column("student_name")]
        [MaxLength(128)]
        [MinLength(2)]
        [Required]
        public string Name { get; set; }

        [Column("password")]
        [MaxLength(64)]
        [MinLength(4)]
        [Required]
        public string Password { get; set; }

        [Column("email")]
        [MaxLength(128)]
        [MinLength(5)]
        [Required]
        public string Email { get; set; }

        [Column("gender")]
        [MaxLength(1)]
        [Required]
        public string Gender { get; set; }

        [Column("create_time")]
        [Required]
        public DateTime CreateTime { get; set; }

        [Column("lastupdate_time")]
        public DateTime? LastupdateTime { get; set; }

        [ForeignKey("ClassID")]
        public virtual AppClass AppClass { get; set; }

    }
}
