using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetHW1OnConsole.QueryList
{
    public class SqlQueryList : IDbQueryList
    {
        public string CreateTableTypes()
        {
            return @"CREATE TABLE Types(
                    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    [Name] NVARCHAR(100) NOT NULL
                    );

                    INSERT INTO Types([Name])
                    VALUES (N'Fruit'), (N'Vegetable');";
        }

        public string CreateTableFruitVeg()
        {
            return @"CREATE TABLE FruitVegetables(
	                Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                [Name] NVARCHAR(100) NOT NULL,
                    [Color] NVARCHAR(100) NOT NULL,
	                TypeId int NOT NULL FOREIGN KEY REFERENCES Types(Id),
	                Cal int NOT NULL
                    );";
        }

        public string Insert(string name, string color, int type, int cal)
        {
            return $@"INSERT INTO FruitVegetables([Name], [Color], TypeId, Cal)
                    Values (N'{name}', N'{color}', {type}, {cal})";
        }

        public string DisplayAll()
        {
            return @"SELECT FV.Id, FV.[Name], FV.Color, FV.[Cal], T.[Name] AS [Type] 
                    FROM FruitVegetables AS FV
                    JOIN Types AS T ON FV.TypeId = T.Id";
        }

        public string DisplayAllColors()
        {
            return @"SELECT FV.Color
                    FROM FruitVegetables AS FV
                    GROUP BY FV.Color";
        }

        public string DisplayAllNames()
        {
            return @"SELECT FV.[Name]
                    FROM FruitVegetables AS FV
                    GROUP BY FV.[Name]";
        }

        public string DisplayAvgCal()
        {
            return @"SELECT AVG(sub.Cal) FROM 
                    (SELECT DISTINCT [Name], Color, Cal
                    FROM FruitVegetables AS FV) as sub";
        }
        public string DisplayMaxCal()
        {
            return @"SELECT Max(Cal) as MaxCal
                    FROM FruitVegetables AS FV";
        }

        public string DisplayMinCal()
        {
            return @"SELECT Min(Cal) as MinCal
                    FROM FruitVegetables AS FV";
        }

        public string DisplayCalLess(int max)
        {
            return $@"SELECT FV.[Name]
                    FROM FruitVegetables AS FV
                    WHERE FV.Cal <= {max}";
        }

        public string DisplayCalMore(int min)
        {
            return $@"SELECT FV.[Name]
                    FROM FruitVegetables AS FV
                    WHERE FV.Cal >= {min}";
        }

        public string DisplayCalSection(int min, int max)
        {
            return $@"SELECT FV.[Name]
                    FROM FruitVegetables AS FV
                    WHERE FV.Cal >= {min}
                    AND FV.Cal <= {max}";
        }

        public string DisplayCountAllColors()
        {
            return @"SELECT FV.Color, COUNT(FV.Color) 
                    FROM FruitVegetables as FV
                    GROUP BY FV.Color";
        }

        public string DisplayCountByColor(string color)
        {
            return $@"SELECT COUNT(FV.Color) 
                    FROM FruitVegetables as FV
                    WHERE FV.Color = '{color}'";
        }

        public string DisplayCountFruits()
        {
            return @"SELECT COUNT(FV.Id)
                    FROM FruitVegetables as FV
                    WHERE FV.[TypeId] = 1";
        }

        public string DisplayCountVegies()
        {
            return @"SELECT COUNT(FV.Id)
                    FROM FruitVegetables as FV
                    WHERE FV.[TypeId] = 2";
        }


        public string DisplayRedOrYellow()
        {
            return @"SELECT COUNT(FV.Color) 
                    FROM FruitVegetables as FV
                    WHERE FV.Color = 'Yellow'
                    OR FV.Color = 'Red'";
        }
    }
}
