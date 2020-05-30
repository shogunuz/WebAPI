using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class WorkerHolidayPub
    {
        private int _id;
        private int _idForHoliday;
        private string _fio;
        private string _position;
        private string _date;


        [Key]
        public int IdForH;

        [Column(TypeName = "int")]
        [Required]
        public int PMId;

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string FIO;

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string Position;


        [Column(TypeName = "nvarchar(150)")]
        [Required]
        public string Date;


    }
}
