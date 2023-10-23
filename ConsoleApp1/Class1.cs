using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Note
{
    public string Title;
    public string Description;
    public DateTime Date;
    public DateTime DueDate;

    public Note(string title, string description, DateTime date)
    {
        Title = title;
        Description = description;
        Date = date;
        DueDate = date;
    }
}
