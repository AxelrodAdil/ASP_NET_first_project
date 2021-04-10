using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using HelloMVC_Adil.Models;

namespace HelloMVC_Adil.Data
{
    public class WebPageDB
    {
        // select * from db_a71f72_niitest.yas_titles

        public MySqlConnection conn;
        public MySqlCommand cmd { get; set; }
        public WebPageDB()
        {
            conn = new MySqlConnection("Server=MYSQL5030.site4now.net;Database=db_a71f72_niitest;Uid=a71f72_niitest;Pwd=qwerty123");
        }


        public List<dbWeb> getAllBooks()
        {
            List<dbWeb> books = new List<dbWeb>();
            using (conn)
            {
                string query = string.Format(@"SELECT `yas_titles`.`titleId`,
                                                    `yas_titles`.`tName`,
                                                    `yas_titles`.`locationId`
                                                FROM `db_a71f72_niitest`.`yas_titles`;
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
                                books.Add(new dbWeb()
                                {
                                    titleId = sdr.GetInt32("titleId"),    //заполняем поля
                                    tName = sdr.GetString("tName"),
                                    locationId = sdr.GetInt32("locationId")
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

        internal void add(dbWeb mb)
        {
            using (conn)
            {
                string query = string.Format(@"INSERT INTO `db_a71f72_niitest`.`yas_titles`
                                                            (
                                                            `titleId`,
                                                            `tName`,
                                                            `locationId`)
                                                            VALUES
                                                            (
                                                            {0},
                                                            '{1}',
                                                            {2});
                                                            ;", mb.titleId, mb.tName, mb.locationId);  //формируем запрос на добавление данных
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



        internal void edit(dbWeb mb)
        {
            using (conn)
            {
                // {0}, '{1}'
                string query = string.Format(@"UPDATE `db_a71f72_niitest`.`yas_titles`
                                                            SET
                                                            `titleId`={0},
                                                            `tName`='{1}',
                                                            `locationId`={2}
                                                            WHERE
                                                            (
                                                            `id`={0}
                                                            );
                                                            ;", mb.titleId, mb.tName, mb.locationId);  //формируем запрос на edit данных
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



        internal void delete(dbWeb mb)
        {
            using (conn)
            {
                // DELETE FROM `db_a71f72_niitest`.`yas_titles` WHERE (`id` = '107');
                string query = string.Format(@"DELETE FROM `db_a71f72_niitest`.`yas_titles`
                                                            WHERE
                                                            (
                                                            `titleId`={0}
                                                            );
                                                            ;", mb.titleId, mb.tName, mb.locationId);  //формируем запрос на delete данных
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


        public dbWeb getOnlyOne(int id)
        {
            dbWeb books = new dbWeb();
            using (conn)
            {
                string query = string.Format(@"SELECT `yas_titles`.`titleId`,
                                                    `yas_titles`.`tName`,
                                                    `yas_titles`.`locationId`
                                                FROM `db_a71f72_niitest`.`yas_titles`
                                                WHERE(`titleId`=" + id + ");;");    // запрос
                using (cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.Read())
                            {
                                books = new dbWeb()
                                {
                                    titleId = sdr.GetInt32("titleId"),    //заполняем поля
                                    tName = sdr.GetString("tName"),
                                    locationId = sdr.GetInt32("locationId")
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