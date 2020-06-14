using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class WorkerHoliday : WorkerDetail
    {
        private int _idForHoliday;
        private DateTime _dateStart;
        private DateTime _dateEnd;
        private new int _id;

        [Key]
        public int IdForH
        {
            get => _idForHoliday;
            set => _idForHoliday = value;
        }

        [Column(TypeName = "int")]
        [Required]
        public new int PMId
        {
            get => _id;
            set => _id = value;
        }

        [Column(TypeName = "nvarchar(150)")]
        [Required]
        public DateTime DateStart
        {
            get => _dateStart;
            set => _dateStart = value;
        }

        [Column(TypeName = "nvarchar(150)")]
        [Required]
        public DateTime DateEnd
        {
            get => _dateEnd;
            set => _dateEnd = value;
        }


    }
}
