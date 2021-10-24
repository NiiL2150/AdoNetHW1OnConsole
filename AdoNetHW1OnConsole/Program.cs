using AdoNetHW1OnConsole.DbFactory;
using AdoNetHW1OnConsole.QueryList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdoNetHW1OnConsole
{
    class Program
    {
        static private IDbFactory _dbFactory;
        static private IDbQueryList _queryList;
        static private string _config;

        static void Main(string[] args)
        {
            string _tmpstr = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Store; Integrated Security = true;";
            Init(new SqlFactory(), new SqlQueryList(), _tmpstr);
            IDbConnection connection = _dbFactory.CreateConnection(_config);
            IDbCommand command;
            IDataReader reader;
            string query = "";

            using (connection)
            {
                connection.Open();
                int choice;
                do
                {
                    Console.Clear();
                    Console.WriteLine("0 Quit\n" +
                    "1 Insert\n" +
                    "2 DisplayAll\n" +
                    "3 DisplayAllNames\n" +
                    "4 DisplayAllColors\n" +
                    "5 DisplayMaxCal\n" +
                    "6 DisplayMinCal\n" +
                    "7 DisplayAvgCal\n" +
                    "8 DisplayCountFruits\n" +
                    "9 DisplayCountVegies\n" +
                    "10 DisplayCountByColor\n" +
                    "11 DisplayCountAllColors\n" +
                    "12 DisplayCalSection\n" +
                    "13 DisplayCalLess\n" +
                    "14 DisplayCalMore\n" +
                    "15 DisplayRedOrYellow\n");

                    choice = Int32.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            int tmp3, tmp4;
                            Console.Write("Name: ");
                            string tmp1 = Console.ReadLine();
                            
                            Console.Write("Color: ");
                            string tmp2 = Console.ReadLine();
                            
                            Console.Write("1 for fruit, 2 for vegetable: ");
                            do
                            {
                                tmp3 = Int32.Parse(Console.ReadLine());
                            } while (tmp3 != 1 && tmp3 != 2);
                            
                            Console.Write("Num of cals: ");
                            do
                            {
                                tmp4 = Int32.Parse(Console.ReadLine());
                            } while (tmp4 < 0);
                            
                            query = _queryList.Insert(tmp1, tmp2, tmp3, tmp4);
                            break;
                        case 2:
                            query = _queryList.DisplayAll();
                            break;
                        case 3:
                            query = _queryList.DisplayAllNames();
                            break;
                        case 4:
                            query = _queryList.DisplayAllColors();
                            break;
                        case 5:
                            query = _queryList.DisplayMaxCal();
                            break;
                        case 6:
                            query = _queryList.DisplayMinCal();
                            break;
                        case 7:
                            query = _queryList.DisplayAvgCal();
                            break;
                        case 8:
                            query = _queryList.DisplayCountFruits();
                            break;
                        case 9:
                            query = _queryList.DisplayCountVegies();
                            break;
                        case 10:
                            string tmp = Console.ReadLine();
                            Console.Write("Insert color: ");
                            query = _queryList.DisplayCountByColor(tmp);
                            break;
                        case 11:
                            query = _queryList.DisplayCountAllColors();
                            break;
                        case 12:
                            int tmp5 = Int32.Parse(Console.ReadLine());
                            int tmp6 = Int32.Parse(Console.ReadLine());
                            if(tmp5 < 0)
                            {
                                tmp5 = -tmp5;
                            }
                            if(tmp6 < 0)
                            {
                                tmp6 = -tmp6;
                            }
                            if (tmp5 > tmp6)
                            {
                                int tmp56 = tmp5;
                                tmp5 = tmp6;
                                tmp6 = tmp56;
                            }
                            query = _queryList.DisplayCalSection(tmp5, tmp6);
                            break;
                        case 13:
                            int tmp7 = Int32.Parse(Console.ReadLine());
                            if (tmp7 < 0)
                            {
                                tmp7 = -tmp7;
                            }
                            query = _queryList.DisplayCalLess(tmp7);
                            break;
                        case 14:
                            int tmp8 = Int32.Parse(Console.ReadLine());
                            if (tmp8 < 0)
                            {
                                tmp8 = -tmp8;
                            }
                            query = _queryList.DisplayCalMore(tmp8);
                            break;
                        case 15:
                            query = _queryList.DisplayRedOrYellow();
                            break;
                        default:
                            query = _queryList.DisplayAll();
                            break;
                    }
                    command = _dbFactory.CreateCommand(query, connection);

                    reader = command.ExecuteReader();
                    do
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader.GetName(i),-15}");
                        }
                        Console.WriteLine();
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write($"{reader[i].ToString(),-15}");
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                    } while (reader.NextResult());
                    reader.Close();
                    Console.ReadKey();
                } while (choice != 0);
            }
        }

        static void Init(IDbFactory dataBaseFactory, IDbQueryList queryList, string config)
        {
            _dbFactory = dataBaseFactory;
            _queryList = queryList;
            _config = config;
            TableInit(_queryList.CreateTableTypes());
            TableInit(_queryList.CreateTableFruitVeg());
        }

        static void TableInit(string query)
        {
            try
            {
                using (IDbConnection connection = _dbFactory.CreateConnection(_config))
                {
                    connection.Open();
                    IDbCommand command =
                        _dbFactory.CreateCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Table created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Table is already created", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
