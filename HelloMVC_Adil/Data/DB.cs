using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using HelloMVC_Adil.Models;

namespace HelloMVC_Adil.DB
{
    // select * from db_a71f72_niitest.yas_employees

    //  id    name_     title     salary
    public class DB
    {
        public MySqlConnection conn;
        public MySqlCommand cmd { get; set; }
        public DB()
        {
            conn = new MySqlConnection("Server=MYSQL5030.site4now.net;Database=db_a71f72_niitest;Uid=a71f72_niitest;Pwd=qwerty123");
        }


        public List<dbTest> getAllBooks()
        {
            List<dbTest> books = new List<dbTest>();
            using (conn)
            {
                string query = string.Format(@"SELECT `yas_employees`.`id`,
                                                    `yas_employees`.`name_`,
                                                    `yas_employees`.`title`,
                                                    `yas_employees`.`salary`
                                                FROM `db_a71f72_niitest`.`yas_employees`;
                                                ;");    // запрос
                using (cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                books.Add(new dbTest()
                                {
                                    id = sdr.GetInt32("id"),    //заполняем поля
                                    name_ = sdr.GetString("name_"),
                                    title = sdr.GetInt32("title"),
                                    salary = sdr.GetInt32("salary")
                                });
                            };
                        }
                    }
                    catch (Exception ee)
                    {

                    }
                    finally
                    {
                        cmd.Dispose();
                    }
                }
            }
            return books;
        }

        internal void add(dbTest mb)
        {
            using (conn)
            {
                string query = string.Format(@"INSERT INTO `db_a71f72_niitest`.`yas_employees`
                                                            (
                                                            `id`,
                                                            `name_`,
                                                            `title`,
                                                            `salary`)
                                                            VALUES
                                                            (
                                                            {0},
                                                            '{1}',
                                                            {2},
                                                            {3});
                                                            ;", mb.id, mb.name_, mb.title, mb.salary);  //формируем запрос на добавление данных
                using (cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ee)
                    {

                    }
                    finally
                    {
                        cmd.Dispose();
                    }
                }
            }
        }



        internal void edit(dbTest mb)
        {
            using (conn)
            {
                // UPDATE `db_a71f72_niitest`.`yas_employees` SET `id` = '106', `name_` = 'edit_temp', `title` = '42', `salary` = '37' WHERE (`id` = '105');
                // INSERT INTO `db_a71f72_niitest`.`yas_employees` (`id`, `name_`, `title`, `salary`) VALUES ('107', 'temp_update', '3773', '45');
                // {0}, '{1}'
                string query = string.Format(@"UPDATE `db_a71f72_niitest`.`yas_employees`
                                                            SET
                                                            `id`={0},
                                                            `name_`='{1}',
                                                            `title`={2},
                                                            `salary`={3}
                                                            WHERE
                                                            (
                                                            `id`={0}
                                                            );
                                                            ;", mb.id, mb.name_, mb.title, mb.salary);  //формируем запрос на edit данных
                using (cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ee)
                    {

                    }
                    finally
                    {
                        cmd.Dispose();
                    }
                }
            }
        }



        internal void delete(dbTest mb)
        {
            using (conn)
            {
                // UPDATE `db_a71f72_niitest`.`yas_employees` SET `id` = '106', `name_` = 'edit_temp', `title` = '42', `salary` = '37' WHERE (`id` = '105');
                // INSERT INTO `db_a71f72_niitest`.`yas_employees` (`id`, `name_`, `title`, `salary`) VALUES ('107', 'temp_update', '3773', '45');
                // DELETE FROM `db_a71f72_niitest`.`yas_employees` WHERE (`id` = '107');
                string query = string.Format(@"DELETE FROM `db_a71f72_niitest`.`yas_employees`
                                                            WHERE
                                                            (
                                                            `id`={0}
                                                            );
                                                            ;", mb.id, mb.name_, mb.title, mb.salary);  //формируем запрос на delete данных
                using (cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ee)
                    {

                    }
                    finally
                    {
                        cmd.Dispose();
                    }
                }
            }
        }


        public dbTest getOnlyOne(int id)
        {
            dbTest books = new dbTest();
            using (conn)
            {
                string query = string.Format(@"SELECT `yas_employees`.`id`,
                                                    `yas_employees`.`name_`,
                                                    `yas_employees`.`title`,
                                                    `yas_employees`.`salary`
                                                FROM `db_a71f72_niitest`.`yas_employees`
                                                WHERE(`id`="+id+");;");    // запрос
                using (cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.Read())
                            {
                                books=new dbTest()
                                {
                                    id = sdr.GetInt32("id"),    //заполняем поля
                                    name_ = sdr.GetString("name_"),
                                    title = sdr.GetInt32("title"),
                                    salary = sdr.GetInt32("salary")
                                };
                            };
                        }
                    }
                    catch (Exception ee)
                    {

                    }
                    finally
                    {
                        cmd.Dispose();
                    }
                }
            }
            return books;
        }
    }
}