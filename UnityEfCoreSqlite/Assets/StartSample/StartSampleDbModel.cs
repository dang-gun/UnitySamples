
using System;
using System.ComponentModel.DataAnnotations;

namespace StartSample
{
    public class StartSampleDbModel
    {

        /// <summary>
        /// ����Ű
        /// </summary>
        [Key]
        public int idStartSampleDbModel { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public int Int1 { get; set; }

        /// <summary>
        /// ���ڿ�
        /// </summary>
        public string Str1 { get; set; }

        /// <summary>
        /// ��¥
        /// </summary>
        public DateTime Date1 { get; set; }


    }
}