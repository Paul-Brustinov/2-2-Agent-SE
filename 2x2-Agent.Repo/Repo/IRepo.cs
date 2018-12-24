using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2x2_Agent.Repo
{
    interface IRepo<T>
    {
        void Load();

        void Save();

        void AppendSave();

        //T GetItemById(int id);
    }
}
