using System;
namespace FlexitHisMVC.Models
{
    public class Department
    {
     
            public long ID { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public long typeID { get; set; }
            public long buildingID { get; set; }
            public string buildingName { get; set; }
            public long groupID { get; set; }
            public int genderID { get; set; }
            public int docIsRequired { get; set; }
            public int isActive { get; set; }
            public int isRandevuActive { get; set; }


        
    }
}

