using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
struct SkillData
{
    public string name;
    public int level;

    public SkillData(string name, int level)
    {
        this.name = name;
        this.level = level;
    }
}
