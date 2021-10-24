using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetHW1OnConsole.QueryList
{
    public enum FruitVegetablesType
    {
        Fruit = 0,
        Vegetable = 1
    }

    public interface IDbQueryList
    {
        string CreateTableTypes();
        string CreateTableFruitVeg();
        string Insert(string name, string color, int type, int cal);
        string DisplayAll();
        string DisplayAllNames();
        string DisplayAllColors();
        string DisplayMaxCal();
        string DisplayMinCal();
        string DisplayAvgCal();
        string DisplayCountFruits();
        string DisplayCountVegies();
        string DisplayCountByColor(string color);
        string DisplayCountAllColors();
        string DisplayCalSection(int min, int max);
        string DisplayCalLess(int max);
        string DisplayCalMore(int min);
        string DisplayRedOrYellow();
    }
}

