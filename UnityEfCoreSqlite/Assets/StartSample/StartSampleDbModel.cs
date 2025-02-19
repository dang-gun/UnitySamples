
using System;
using System.ComponentModel.DataAnnotations;

namespace StartSample
{
    public class StartSampleDbModel
    {

        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        public int idStartSampleDbModel { get; set; }

        /// <summary>
        /// 숫자
        /// </summary>
        public int Int1 { get; set; }

        /// <summary>
        /// 문자열
        /// </summary>
        public string Str1 { get; set; }

        /// <summary>
        /// 날짜
        /// </summary>
        public DateTime Date1 { get; set; }


    }
}