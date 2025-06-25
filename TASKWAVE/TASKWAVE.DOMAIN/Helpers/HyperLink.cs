using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TASKWAVE.DOMAIN.Helpers
{
    public class HyperLink
    {
        public string Url { get; set; }
        public string Text { get; set; }
        public string Title { get; set; } 
        
        public string TypeIcon { get; set; }
        public HyperLink(string url, string text, string title, TypeOfIcon typeIcon)
        {
            Url = url;
            Text = text;
            Title = title;

            switch (typeIcon)
            {
                case TypeOfIcon.Home:
                    TypeIcon = "bi-house-door-fill-nav-menu";
                    break;
                case TypeOfIcon.List:
                    TypeIcon = "bi-list-nested-nav-menu";
                    break;
                case TypeOfIcon.Add:
                    TypeIcon = "bi-plus-square-fill-nav-menu";
                    break;
                case TypeOfIcon.Calendar:
                    TypeIcon = "bi-calendar-week-nav-menu";
                    break;
                case TypeOfIcon.ClipBoard:
                    TypeIcon = "bi-clipboard-data-fill-nav-menu";
                    break;
                case TypeOfIcon.History:
                    TypeIcon = "bi-clock-history-nav-menu";
                    break;
                case TypeOfIcon.Reports:
                    TypeIcon = "bi-file-earmark-medical-fill-nav-menu";
                    break;
            }
        }

        public enum TypeOfIcon
        {
            Home,
            Add,
            List,
            Calendar,
            ClipBoard,
            History,
            Reports
        }

    }
}
