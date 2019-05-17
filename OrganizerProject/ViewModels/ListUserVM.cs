using OrganizerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizerProject.ViewModels
{
    public class ListUserVM
    {
        public List<UserVM> userVMList;
        public User user { get; set; }
    }
}