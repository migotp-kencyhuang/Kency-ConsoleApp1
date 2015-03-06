namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("app_class")]
    public class AppClass
    {
        public AppClass()
        {
            this.CreateTime = DateTime.Now;
            this.LastupdateTime = DateTime.Now;
        }

        [Column("class_id"), Key]
        [MaxLength(32)]
        [MinLength(3)]
        [Required]
        public string ID { get; set; }

        [Column("class_name")]
        [MaxLength(128)]
        [MinLength(2)]
        public string Name { get; set; }

        [Column("email")]
        [MaxLength(128)]
        [MinLength(5)]
        [Required]
        public string Email { get; set; }

        [Column("teacher")]
        [MaxLength(128)]
        public string Teacher { get; set; }

        [Column("stu_count")]
        public int Stu_Count { get; set; }

        [Column("create_time")]
        [Required]
        public DateTime CreateTime { get; set; }

        [Column("lastupdate_time")]
        public DateTime? LastupdateTime { get; set; }

        public virtual ICollection<AppStudent> AppStudent { get; set; }

    }
}
