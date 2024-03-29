﻿using System;
using System.Net.NetworkInformation;
using FlexitHisMVC.Models;
using MySql.Data.MySqlClient;

namespace FlexitHisMVC.Data
{
    public class KassaRepo
    {
        private readonly string ConnectionString;

        public KassaRepo(string conString)
        {
            ConnectionString = conString;
        }
        public List<Kassa> GetAllKassaListByHospital(int hospitalID)
        {
            List<Kassa> kassaList = new List<Kassa>();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {

                connection.Open();

                using (MySqlCommand kassaCom = new MySqlCommand("SELECT *  FROM kassa a where hospitalID =@hospitalID;", connection))
                {

                    kassaCom.Parameters.AddWithValue("@hospitalID", hospitalID);

                    using (MySqlDataReader kassaReader = kassaCom.ExecuteReader())
                    {
                        if (kassaReader.HasRows)
                        {
                            while (kassaReader.Read())
                            {
                                Kassa kassa = new Kassa();
                                kassa.id = Convert.ToInt32(kassaReader["id"]);
                                kassa.name = kassaReader["name"].ToString();
                                kassaList.Add(kassa);
                            }


                        }

                    }

                }
                connection.Close();
            }
            return kassaList;
        }
        public List<Kassa> GetUserKassaByHospital(int hospitalID, int userID)

        {

            List<Kassa> kassaList = new List<Kassa>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {


                    connection.Open();
                    using (MySqlCommand com = new MySqlCommand($@"SELECT *,(select name from kassa where id=a.kassaID)as name
FROM kassa_user_rel a where kassaID in (select id from kassa where hospitalID=@hospitalID) and userID = @userID", connection))
                    {

                        com.Parameters.AddWithValue("@hospitalID", hospitalID);
                        com.Parameters.AddWithValue("@userID", userID);
                        MySqlDataReader reader = com.ExecuteReader();
                        if (reader.HasRows)
                        {


                            while (reader.Read())
                            {

                                Kassa kassa = new Kassa();
                                kassa.id = Convert.ToInt32(reader["id"]);
                                kassa.kassaID = Convert.ToInt32(reader["kassaID"]);
                                kassa.readOnly = Convert.ToInt32(reader["read_only"]);
                                kassa.fullAccess = Convert.ToInt32(reader["full_access"]);
                                kassa.name = reader["name"].ToString();
                                kassaList.Add(kassa);


                            }

                            //response.data.Reverse();

                            //response.status = 1;
                        }

                    }
                    connection.Close();


                }


            }
            catch (Exception ex)
            {
                //FlexitHis_API.StandardMessages.CallSerilog(ex);
                Console.WriteLine(ex.Message);

            }


            return kassaList;
        }
        public List<Kassa> GetUserAllowedKassaList(int userID)
        {
            List<Kassa> kassaList = new List<Kassa>();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {

                connection.Open();

                using (MySqlCommand kassaCom = new MySqlCommand("SELECT *,(select name from kassa where id = a.kassaID)as kassaName FROM kassa_user_rel a where userID =@userID;", connection))
                {

                    kassaCom.Parameters.AddWithValue("@userID", userID);

                    using (MySqlDataReader kassaReader = kassaCom.ExecuteReader())
                    {
                        if (kassaReader.HasRows)
                        {
                            while (kassaReader.Read())
                            {
                                Kassa kassa = new Kassa();
                                kassa.id = Convert.ToInt32(kassaReader["id"]);

                                kassa.kassaID = Convert.ToInt32(kassaReader["kassaID"]);
                                kassa.name = kassaReader["kassaName"].ToString();
                                kassaList.Add(kassa);
                            }


                        }

                    }

                }
                connection.Close();
            }
            return kassaList;
        }
        public int InsertKassaToUser(int userID, int kassaID, bool read_only, bool full_access)
        {
            int lastID = 0;

            try
            {
                if (userID > 0 && kassaID > 0)
                {
                    using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                    {


                        var sql = "";

                        connection.Open();

                        sql = @"Insert INTO kassa_user_rel (kassaID,userID,read_only,full_access )
SELECT @kassaID,@userID,@read_only,@full_access FROM DUAL
WHERE NOT EXISTS 
  (SELECT * FROM kassa_user_rel WHERE kassaID=@kassaID and userID=@userID )";



                        using (MySqlCommand com = new MySqlCommand(sql, connection))
                        {
                            com.Parameters.AddWithValue("@kassaID", kassaID);
                            com.Parameters.AddWithValue("@userID", userID);
                            com.Parameters.AddWithValue("@read_only", read_only);
                            com.Parameters.AddWithValue("@full_access", full_access);


                            lastID = com.ExecuteNonQuery();


                        }
                        connection.Close();



                    }
                }



            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);


            }
            return lastID;
        }
        public int RemoveKassaFromUser(int userID, int kassaID)
        {
            int lastID = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {


                    var sql = "";

                    connection.Open();

                    sql = @"DELETE FROM kassa_user_rel WHERE userID = @userID and kassaID = @kassaID;";



                    using (MySqlCommand com = new MySqlCommand(sql, connection))
                    {
                        com.Parameters.AddWithValue("@kassaID", kassaID);
                        com.Parameters.AddWithValue("@userID", userID);


                        lastID = com.ExecuteNonQuery();


                    }
                    connection.Close();



                }


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);


            }
            return lastID;
        }
    }
}

