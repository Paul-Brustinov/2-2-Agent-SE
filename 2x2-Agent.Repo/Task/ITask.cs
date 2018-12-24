using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2x2_Agent.Repo;

namespace _2x2_Agent.Repo
{


//    <GridViewColumn Header = "Id" DisplayMemberBinding="{Binding Id}"/>
//<GridViewColumn Header = "ClassName" DisplayMemberBinding="{Binding ShortClassName}"/>
//<GridViewColumn Header = "ShortDescription" />
//< GridViewColumn Header="Memo" DisplayMemberBinding="{Binding Memo}"/>


    public interface ITask
    {

        int Id { get; set; }
        string ShortDescription { get; }

        string Memo { get; set; }

        Task<List<LogItem>> StartAsync();

        string ClassName { get; }
        string ShortClassName { get; }

    }
}
