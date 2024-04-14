using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CrossoverSkillData
{
    // number of crossover skills each crossover has
    const int CROSSOVERSKILLSCOUNT = 10;

    List<List<SkillData>> skills = new List<List<SkillData>>();

    public void Initialize(MinionData minion)
    {
        for (int i = 1; i < minion.functions.Count(); i++ )
        {
            if (skills[i] == null)
            {
                skills[i] = new List<SkillData>();

                for (int j = 1; j <= CROSSOVERSKILLSCOUNT; j++)
                {
                    skills[i].Add(new SkillData($"crossoverskill {j}", 0));
                }
            }
        }
    }

    public void Reset(MinionData minion)
    {
        List<List<SkillData>> skills = new List<List<SkillData>>();
        Initialize(minion);
    }

}
