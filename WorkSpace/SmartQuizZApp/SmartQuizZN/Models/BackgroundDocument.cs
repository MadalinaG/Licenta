﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartQuizZN.Models
{
    class BackgroundDocument
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int TopicID { get; set; }
        public int TestID { get; set; }
        public int AddedByID { get; set; }
        public DateTime AddedTime { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}